<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_LabOrderDetail
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

            If (IsNothing(oC1flex) = False) Then
                Try
                    oC1flex.Dispose()
                    oC1flex = Nothing
                Catch ex As Exception

                End Try

            End If
            Try
                If (IsNothing(_Users) = False) Then
                    _Users.Dispose()
                    _Users = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(dtID) = False) Then
                    dtID.Dispose()
                    dtID = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(dtUsers) = False) Then
                    dtUsers.Dispose()
                    dtUsers = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(dtTaskUsers) = False) Then
                    dtTaskUsers.Dispose()
                    dtTaskUsers = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(ToList) = False) Then
                    ToList.Dispose()
                    ToList = Nothing
                End If
            Catch ex As Exception

            End Try
        End If
        MyBase.Dispose(disposing)

        Dim dtpControls As System.Windows.Forms.DateTimePicker() = {dtTaskDueDate}
        Dim cntControls As System.Windows.Forms.Control() = {dtTaskDueDate}

        Try
            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
            gloGlobal.cEventHelper.DisposeAllControls(cntControls)
        Catch

        End Try

    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUC_LabOrderDetail))
        Me.pnlTopHeader = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.pnlPatientDetail = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.pnlPD_RefBySamBy = New System.Windows.Forms.Panel()
        Me.pnlPD_RefBySamBy_SamBy = New System.Windows.Forms.Panel()
        Me.txtSampledBy = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnSearchSB = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnClearSB = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlPD_RefBySamBy_RefBy = New System.Windows.Forms.Panel()
        Me.txtReferredBy = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnSearchRB = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnClearRB = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlPD_TaskDescDueDate = New System.Windows.Forms.Panel()
        Me.pnlPD_TaskDescDueDate_DueDate = New System.Windows.Forms.Panel()
        Me.dtTaskDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.pnlPD_TaskDescDueDate_TaskDesc = New System.Windows.Forms.Panel()
        Me.txtTaskDesc = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.pnlPD_PrefLabAssTask = New System.Windows.Forms.Panel()
        Me.pnlPD_PrefLabAssTask_AssTask = New System.Windows.Forms.Panel()
        Me.cmbAssignedTo = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnSearchAssTo = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnClearAssTo = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.pnlPD_PrefLabAssTask_PrefLab = New System.Windows.Forms.Panel()
        Me.txtPreferredBy = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnSearchPL = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnClearPL = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblSendTo = New System.Windows.Forms.Label()
        Me.pnlPD_SendTo = New System.Windows.Forms.Panel()
        Me.rbSendToPhysician = New System.Windows.Forms.RadioButton()
        Me.rbSendToLab = New System.Windows.Forms.RadioButton()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.pnlPD_OrderNumber = New System.Windows.Forms.Panel()
        Me.lblOrderNumber = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.pnlTopHeader.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlPatientDetail.SuspendLayout()
        Me.pnlPD_RefBySamBy.SuspendLayout()
        Me.pnlPD_RefBySamBy_SamBy.SuspendLayout()
        Me.pnlPD_RefBySamBy_RefBy.SuspendLayout()
        Me.pnlPD_TaskDescDueDate.SuspendLayout()
        Me.pnlPD_TaskDescDueDate_DueDate.SuspendLayout()
        Me.pnlPD_TaskDescDueDate_TaskDesc.SuspendLayout()
        Me.pnlPD_PrefLabAssTask.SuspendLayout()
        Me.pnlPD_PrefLabAssTask_AssTask.SuspendLayout()
        Me.pnlPD_PrefLabAssTask_PrefLab.SuspendLayout()
        Me.pnlPD_SendTo.SuspendLayout()
        Me.pnlPD_OrderNumber.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopHeader
        '
        Me.pnlTopHeader.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.pnlTopHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopHeader.Controls.Add(Me.Panel1)
        Me.pnlTopHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlTopHeader.Name = "pnlTopHeader"
        Me.pnlTopHeader.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlTopHeader.Size = New System.Drawing.Size(650, 30)
        Me.pnlTopHeader.TabIndex = 82
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.btnUp)
        Me.Panel1.Controls.Add(Me.btnDown)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel1.Controls.Add(Me.lbl_pnlRight)
        Me.Panel1.Controls.Add(Me.lbl_pnlTop)
        Me.Panel1.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(650, 27)
        Me.Panel1.TabIndex = 9
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Location = New System.Drawing.Point(601, 1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(24, 25)
        Me.btnUp.TabIndex = 1
        Me.btnUp.UseVisualStyleBackColor = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(625, 1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(24, 25)
        Me.btnDown.TabIndex = 2
        Me.btnDown.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(648, 25)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "  Order Details :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 26)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(648, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(649, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 26)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(1, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(649, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 27)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'pnlPatientDetail
        '
        Me.pnlPatientDetail.AutoSize = True
        Me.pnlPatientDetail.Controls.Add(Me.Label25)
        Me.pnlPatientDetail.Controls.Add(Me.pnlPD_RefBySamBy)
        Me.pnlPatientDetail.Controls.Add(Me.pnlPD_TaskDescDueDate)
        Me.pnlPatientDetail.Controls.Add(Me.pnlPD_PrefLabAssTask)
        Me.pnlPatientDetail.Controls.Add(Me.pnlPD_SendTo)
        Me.pnlPatientDetail.Controls.Add(Me.pnlPD_OrderNumber)
        Me.pnlPatientDetail.Controls.Add(Me.Label26)
        Me.pnlPatientDetail.Controls.Add(Me.Label29)
        Me.pnlPatientDetail.Controls.Add(Me.Label30)
        Me.pnlPatientDetail.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientDetail.Location = New System.Drawing.Point(0, 30)
        Me.pnlPatientDetail.Name = "pnlPatientDetail"
        Me.pnlPatientDetail.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlPatientDetail.Size = New System.Drawing.Size(650, 139)
        Me.pnlPatientDetail.TabIndex = 0
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Location = New System.Drawing.Point(1, 135)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(648, 1)
        Me.Label25.TabIndex = 74
        Me.Label25.Text = "label2"
        '
        'pnlPD_RefBySamBy
        '
        Me.pnlPD_RefBySamBy.Controls.Add(Me.pnlPD_RefBySamBy_SamBy)
        Me.pnlPD_RefBySamBy.Controls.Add(Me.pnlPD_RefBySamBy_RefBy)
        Me.pnlPD_RefBySamBy.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPD_RefBySamBy.Location = New System.Drawing.Point(1, 107)
        Me.pnlPD_RefBySamBy.Name = "pnlPD_RefBySamBy"
        Me.pnlPD_RefBySamBy.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnlPD_RefBySamBy.Size = New System.Drawing.Size(648, 28)
        Me.pnlPD_RefBySamBy.TabIndex = 4
        '
        'pnlPD_RefBySamBy_SamBy
        '
        Me.pnlPD_RefBySamBy_SamBy.Controls.Add(Me.txtSampledBy)
        Me.pnlPD_RefBySamBy_SamBy.Controls.Add(Me.Label6)
        Me.pnlPD_RefBySamBy_SamBy.Controls.Add(Me.Label15)
        Me.pnlPD_RefBySamBy_SamBy.Controls.Add(Me.btnSearchSB)
        Me.pnlPD_RefBySamBy_SamBy.Controls.Add(Me.Label14)
        Me.pnlPD_RefBySamBy_SamBy.Controls.Add(Me.btnClearSB)
        Me.pnlPD_RefBySamBy_SamBy.Controls.Add(Me.Label13)
        Me.pnlPD_RefBySamBy_SamBy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPD_RefBySamBy_SamBy.Location = New System.Drawing.Point(438, 3)
        Me.pnlPD_RefBySamBy_SamBy.Name = "pnlPD_RefBySamBy_SamBy"
        Me.pnlPD_RefBySamBy_SamBy.Size = New System.Drawing.Size(210, 22)
        Me.pnlPD_RefBySamBy_SamBy.TabIndex = 3
        '
        'txtSampledBy
        '
        Me.txtSampledBy.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtSampledBy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSampledBy.Location = New System.Drawing.Point(101, 0)
        Me.txtSampledBy.Name = "txtSampledBy"
        Me.txtSampledBy.ReadOnly = True
        Me.txtSampledBy.Size = New System.Drawing.Size(54, 22)
        Me.txtSampledBy.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 22)
        Me.Label6.TabIndex = 70
        Me.Label6.Text = "Sampled By : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Location = New System.Drawing.Point(155, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(4, 22)
        Me.Label15.TabIndex = 69
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSearchSB
        '
        Me.btnSearchSB.BackgroundImage = CType(resources.GetObject("btnSearchSB.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchSB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchSB.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSearchSB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnSearchSB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchSB.Image = CType(resources.GetObject("btnSearchSB.Image"), System.Drawing.Image)
        Me.btnSearchSB.Location = New System.Drawing.Point(159, 0)
        Me.btnSearchSB.Name = "btnSearchSB"
        Me.btnSearchSB.Size = New System.Drawing.Size(22, 22)
        Me.btnSearchSB.TabIndex = 5
        Me.btnSearchSB.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(181, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(3, 22)
        Me.Label14.TabIndex = 67
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClearSB
        '
        Me.btnClearSB.BackgroundImage = CType(resources.GetObject("btnClearSB.BackgroundImage"), System.Drawing.Image)
        Me.btnClearSB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSB.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearSB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnClearSB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSB.Image = CType(resources.GetObject("btnClearSB.Image"), System.Drawing.Image)
        Me.btnClearSB.Location = New System.Drawing.Point(184, 0)
        Me.btnClearSB.Name = "btnClearSB"
        Me.btnClearSB.Size = New System.Drawing.Size(22, 22)
        Me.btnClearSB.TabIndex = 6
        Me.btnClearSB.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Location = New System.Drawing.Point(206, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(4, 22)
        Me.Label13.TabIndex = 42
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPD_RefBySamBy_RefBy
        '
        Me.pnlPD_RefBySamBy_RefBy.Controls.Add(Me.txtReferredBy)
        Me.pnlPD_RefBySamBy_RefBy.Controls.Add(Me.Label12)
        Me.pnlPD_RefBySamBy_RefBy.Controls.Add(Me.btnSearchRB)
        Me.pnlPD_RefBySamBy_RefBy.Controls.Add(Me.Label11)
        Me.pnlPD_RefBySamBy_RefBy.Controls.Add(Me.btnClearRB)
        Me.pnlPD_RefBySamBy_RefBy.Controls.Add(Me.Label10)
        Me.pnlPD_RefBySamBy_RefBy.Controls.Add(Me.Label4)
        Me.pnlPD_RefBySamBy_RefBy.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlPD_RefBySamBy_RefBy.Location = New System.Drawing.Point(0, 3)
        Me.pnlPD_RefBySamBy_RefBy.Name = "pnlPD_RefBySamBy_RefBy"
        Me.pnlPD_RefBySamBy_RefBy.Size = New System.Drawing.Size(438, 22)
        Me.pnlPD_RefBySamBy_RefBy.TabIndex = 2
        '
        'txtReferredBy
        '
        Me.txtReferredBy.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtReferredBy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtReferredBy.Location = New System.Drawing.Point(159, 0)
        Me.txtReferredBy.Name = "txtReferredBy"
        Me.txtReferredBy.ReadOnly = True
        Me.txtReferredBy.Size = New System.Drawing.Size(224, 22)
        Me.txtReferredBy.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(383, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(4, 22)
        Me.Label12.TabIndex = 68
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSearchRB
        '
        Me.btnSearchRB.BackgroundImage = CType(resources.GetObject("btnSearchRB.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchRB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchRB.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSearchRB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnSearchRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchRB.Image = CType(resources.GetObject("btnSearchRB.Image"), System.Drawing.Image)
        Me.btnSearchRB.Location = New System.Drawing.Point(387, 0)
        Me.btnSearchRB.Name = "btnSearchRB"
        Me.btnSearchRB.Size = New System.Drawing.Size(22, 22)
        Me.btnSearchRB.TabIndex = 2
        Me.btnSearchRB.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Location = New System.Drawing.Point(409, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(3, 22)
        Me.Label11.TabIndex = 66
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClearRB
        '
        Me.btnClearRB.BackgroundImage = CType(resources.GetObject("btnClearRB.BackgroundImage"), System.Drawing.Image)
        Me.btnClearRB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearRB.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearRB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnClearRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearRB.Image = CType(resources.GetObject("btnClearRB.Image"), System.Drawing.Image)
        Me.btnClearRB.Location = New System.Drawing.Point(412, 0)
        Me.btnClearRB.Name = "btnClearRB"
        Me.btnClearRB.Size = New System.Drawing.Size(22, 22)
        Me.btnClearRB.TabIndex = 3
        Me.btnClearRB.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Location = New System.Drawing.Point(434, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(4, 22)
        Me.Label10.TabIndex = 41
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(159, 22)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Referred By : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPD_TaskDescDueDate
        '
        Me.pnlPD_TaskDescDueDate.Controls.Add(Me.pnlPD_TaskDescDueDate_DueDate)
        Me.pnlPD_TaskDescDueDate.Controls.Add(Me.pnlPD_TaskDescDueDate_TaskDesc)
        Me.pnlPD_TaskDescDueDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPD_TaskDescDueDate.Location = New System.Drawing.Point(1, 79)
        Me.pnlPD_TaskDescDueDate.Name = "pnlPD_TaskDescDueDate"
        Me.pnlPD_TaskDescDueDate.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnlPD_TaskDescDueDate.Size = New System.Drawing.Size(648, 28)
        Me.pnlPD_TaskDescDueDate.TabIndex = 3
        '
        'pnlPD_TaskDescDueDate_DueDate
        '
        Me.pnlPD_TaskDescDueDate_DueDate.Controls.Add(Me.dtTaskDueDate)
        Me.pnlPD_TaskDescDueDate_DueDate.Controls.Add(Me.Label20)
        Me.pnlPD_TaskDescDueDate_DueDate.Controls.Add(Me.Label21)
        Me.pnlPD_TaskDescDueDate_DueDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPD_TaskDescDueDate_DueDate.Location = New System.Drawing.Point(438, 3)
        Me.pnlPD_TaskDescDueDate_DueDate.Name = "pnlPD_TaskDescDueDate_DueDate"
        Me.pnlPD_TaskDescDueDate_DueDate.Size = New System.Drawing.Size(210, 22)
        Me.pnlPD_TaskDescDueDate_DueDate.TabIndex = 3
        '
        'dtTaskDueDate
        '
        Me.dtTaskDueDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTaskDueDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtTaskDueDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTaskDueDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTaskDueDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTaskDueDate.CustomFormat = "MM/dd/yyyy hh:mm tt"
        Me.dtTaskDueDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtTaskDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTaskDueDate.Location = New System.Drawing.Point(101, 0)
        Me.dtTaskDueDate.Name = "dtTaskDueDate"
        Me.dtTaskDueDate.Size = New System.Drawing.Size(161, 22)
        Me.dtTaskDueDate.TabIndex = 2
        '
        'Label20
        '
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(101, 22)
        Me.Label20.TabIndex = 73
        Me.Label20.Text = "Task Due Date : "
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label21.Location = New System.Drawing.Point(206, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(4, 22)
        Me.Label21.TabIndex = 72
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPD_TaskDescDueDate_TaskDesc
        '
        Me.pnlPD_TaskDescDueDate_TaskDesc.Controls.Add(Me.txtTaskDesc)
        Me.pnlPD_TaskDescDueDate_TaskDesc.Controls.Add(Me.Label27)
        Me.pnlPD_TaskDescDueDate_TaskDesc.Controls.Add(Me.Label28)
        Me.pnlPD_TaskDescDueDate_TaskDesc.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlPD_TaskDescDueDate_TaskDesc.Location = New System.Drawing.Point(0, 3)
        Me.pnlPD_TaskDescDueDate_TaskDesc.Name = "pnlPD_TaskDescDueDate_TaskDesc"
        Me.pnlPD_TaskDescDueDate_TaskDesc.Size = New System.Drawing.Size(438, 22)
        Me.pnlPD_TaskDescDueDate_TaskDesc.TabIndex = 2
        '
        'txtTaskDesc
        '
        Me.txtTaskDesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTaskDesc.Location = New System.Drawing.Point(159, 0)
        Me.txtTaskDesc.MaxLength = 255
        Me.txtTaskDesc.Name = "txtTaskDesc"
        Me.txtTaskDesc.Size = New System.Drawing.Size(275, 22)
        Me.txtTaskDesc.TabIndex = 1
        '
        'Label27
        '
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Location = New System.Drawing.Point(434, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(4, 22)
        Me.Label27.TabIndex = 40
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Location = New System.Drawing.Point(0, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(159, 22)
        Me.Label28.TabIndex = 39
        Me.Label28.Text = "Task Description : "
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPD_PrefLabAssTask
        '
        Me.pnlPD_PrefLabAssTask.Controls.Add(Me.pnlPD_PrefLabAssTask_AssTask)
        Me.pnlPD_PrefLabAssTask.Controls.Add(Me.pnlPD_PrefLabAssTask_PrefLab)
        Me.pnlPD_PrefLabAssTask.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPD_PrefLabAssTask.Location = New System.Drawing.Point(1, 51)
        Me.pnlPD_PrefLabAssTask.Name = "pnlPD_PrefLabAssTask"
        Me.pnlPD_PrefLabAssTask.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnlPD_PrefLabAssTask.Size = New System.Drawing.Size(648, 28)
        Me.pnlPD_PrefLabAssTask.TabIndex = 2
        '
        'pnlPD_PrefLabAssTask_AssTask
        '
        Me.pnlPD_PrefLabAssTask_AssTask.Controls.Add(Me.cmbAssignedTo)
        Me.pnlPD_PrefLabAssTask_AssTask.Controls.Add(Me.Label7)
        Me.pnlPD_PrefLabAssTask_AssTask.Controls.Add(Me.Label19)
        Me.pnlPD_PrefLabAssTask_AssTask.Controls.Add(Me.btnSearchAssTo)
        Me.pnlPD_PrefLabAssTask_AssTask.Controls.Add(Me.Label18)
        Me.pnlPD_PrefLabAssTask_AssTask.Controls.Add(Me.Button1)
        Me.pnlPD_PrefLabAssTask_AssTask.Controls.Add(Me.Label17)
        Me.pnlPD_PrefLabAssTask_AssTask.Controls.Add(Me.btnClearAssTo)
        Me.pnlPD_PrefLabAssTask_AssTask.Controls.Add(Me.Label16)
        Me.pnlPD_PrefLabAssTask_AssTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPD_PrefLabAssTask_AssTask.Location = New System.Drawing.Point(438, 3)
        Me.pnlPD_PrefLabAssTask_AssTask.Name = "pnlPD_PrefLabAssTask_AssTask"
        Me.pnlPD_PrefLabAssTask_AssTask.Size = New System.Drawing.Size(210, 22)
        Me.pnlPD_PrefLabAssTask_AssTask.TabIndex = 3
        '
        'cmbAssignedTo
        '
        Me.cmbAssignedTo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbAssignedTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAssignedTo.FormattingEnabled = True
        Me.cmbAssignedTo.Location = New System.Drawing.Point(101, 0)
        Me.cmbAssignedTo.Name = "cmbAssignedTo"
        Me.cmbAssignedTo.Size = New System.Drawing.Size(29, 22)
        Me.cmbAssignedTo.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 22)
        Me.Label7.TabIndex = 73
        Me.Label7.Text = "Assign Task To : "
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Location = New System.Drawing.Point(130, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(4, 22)
        Me.Label19.TabIndex = 72
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSearchAssTo
        '
        Me.btnSearchAssTo.BackgroundImage = CType(resources.GetObject("btnSearchAssTo.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchAssTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchAssTo.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSearchAssTo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnSearchAssTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchAssTo.Image = CType(resources.GetObject("btnSearchAssTo.Image"), System.Drawing.Image)
        Me.btnSearchAssTo.Location = New System.Drawing.Point(134, 0)
        Me.btnSearchAssTo.Name = "btnSearchAssTo"
        Me.btnSearchAssTo.Size = New System.Drawing.Size(22, 22)
        Me.btnSearchAssTo.TabIndex = 4
        Me.btnSearchAssTo.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Location = New System.Drawing.Point(156, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(3, 22)
        Me.Label18.TabIndex = 70
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(159, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 22)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = ">>"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Label17
        '
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Location = New System.Drawing.Point(181, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(3, 22)
        Me.Label17.TabIndex = 68
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label17.Visible = False
        '
        'btnClearAssTo
        '
        Me.btnClearAssTo.BackgroundImage = CType(resources.GetObject("btnClearAssTo.BackgroundImage"), System.Drawing.Image)
        Me.btnClearAssTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearAssTo.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearAssTo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnClearAssTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearAssTo.Image = CType(resources.GetObject("btnClearAssTo.Image"), System.Drawing.Image)
        Me.btnClearAssTo.Location = New System.Drawing.Point(184, 0)
        Me.btnClearAssTo.Name = "btnClearAssTo"
        Me.btnClearAssTo.Size = New System.Drawing.Size(22, 22)
        Me.btnClearAssTo.TabIndex = 6
        Me.btnClearAssTo.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Location = New System.Drawing.Point(206, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(4, 22)
        Me.Label16.TabIndex = 43
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPD_PrefLabAssTask_PrefLab
        '
        Me.pnlPD_PrefLabAssTask_PrefLab.Controls.Add(Me.txtPreferredBy)
        Me.pnlPD_PrefLabAssTask_PrefLab.Controls.Add(Me.Label9)
        Me.pnlPD_PrefLabAssTask_PrefLab.Controls.Add(Me.btnSearchPL)
        Me.pnlPD_PrefLabAssTask_PrefLab.Controls.Add(Me.Label8)
        Me.pnlPD_PrefLabAssTask_PrefLab.Controls.Add(Me.btnClearPL)
        Me.pnlPD_PrefLabAssTask_PrefLab.Controls.Add(Me.Label5)
        Me.pnlPD_PrefLabAssTask_PrefLab.Controls.Add(Me.lblSendTo)
        Me.pnlPD_PrefLabAssTask_PrefLab.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlPD_PrefLabAssTask_PrefLab.Location = New System.Drawing.Point(0, 3)
        Me.pnlPD_PrefLabAssTask_PrefLab.Name = "pnlPD_PrefLabAssTask_PrefLab"
        Me.pnlPD_PrefLabAssTask_PrefLab.Size = New System.Drawing.Size(438, 22)
        Me.pnlPD_PrefLabAssTask_PrefLab.TabIndex = 2
        '
        'txtPreferredBy
        '
        Me.txtPreferredBy.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtPreferredBy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPreferredBy.Location = New System.Drawing.Point(159, 0)
        Me.txtPreferredBy.Name = "txtPreferredBy"
        Me.txtPreferredBy.ReadOnly = True
        Me.txtPreferredBy.Size = New System.Drawing.Size(224, 22)
        Me.txtPreferredBy.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Location = New System.Drawing.Point(383, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(4, 22)
        Me.Label9.TabIndex = 66
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSearchPL
        '
        Me.btnSearchPL.BackgroundImage = CType(resources.GetObject("btnSearchPL.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchPL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchPL.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSearchPL.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnSearchPL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchPL.Image = CType(resources.GetObject("btnSearchPL.Image"), System.Drawing.Image)
        Me.btnSearchPL.Location = New System.Drawing.Point(387, 0)
        Me.btnSearchPL.Name = "btnSearchPL"
        Me.btnSearchPL.Size = New System.Drawing.Size(22, 22)
        Me.btnSearchPL.TabIndex = 1
        Me.btnSearchPL.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Location = New System.Drawing.Point(409, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(3, 22)
        Me.Label8.TabIndex = 64
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClearPL
        '
        Me.btnClearPL.BackgroundImage = CType(resources.GetObject("btnClearPL.BackgroundImage"), System.Drawing.Image)
        Me.btnClearPL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearPL.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearPL.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnClearPL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearPL.Image = CType(resources.GetObject("btnClearPL.Image"), System.Drawing.Image)
        Me.btnClearPL.Location = New System.Drawing.Point(412, 0)
        Me.btnClearPL.Name = "btnClearPL"
        Me.btnClearPL.Size = New System.Drawing.Size(22, 22)
        Me.btnClearPL.TabIndex = 2
        Me.btnClearPL.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Location = New System.Drawing.Point(434, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(4, 22)
        Me.Label5.TabIndex = 40
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSendTo
        '
        Me.lblSendTo.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSendTo.Location = New System.Drawing.Point(0, 0)
        Me.lblSendTo.Name = "lblSendTo"
        Me.lblSendTo.Size = New System.Drawing.Size(159, 22)
        Me.lblSendTo.TabIndex = 39
        Me.lblSendTo.Text = "Preferred/Performing Lab : "
        Me.lblSendTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPD_SendTo
        '
        Me.pnlPD_SendTo.Controls.Add(Me.rbSendToPhysician)
        Me.pnlPD_SendTo.Controls.Add(Me.rbSendToLab)
        Me.pnlPD_SendTo.Controls.Add(Me.Label33)
        Me.pnlPD_SendTo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPD_SendTo.Location = New System.Drawing.Point(1, 26)
        Me.pnlPD_SendTo.Name = "pnlPD_SendTo"
        Me.pnlPD_SendTo.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnlPD_SendTo.Size = New System.Drawing.Size(648, 25)
        Me.pnlPD_SendTo.TabIndex = 1
        '
        'rbSendToPhysician
        '
        Me.rbSendToPhysician.AutoSize = True
        Me.rbSendToPhysician.Location = New System.Drawing.Point(219, 3)
        Me.rbSendToPhysician.Name = "rbSendToPhysician"
        Me.rbSendToPhysician.Size = New System.Drawing.Size(73, 18)
        Me.rbSendToPhysician.TabIndex = 1
        Me.rbSendToPhysician.TabStop = True
        Me.rbSendToPhysician.Text = "Physician"
        Me.rbSendToPhysician.UseVisualStyleBackColor = True
        '
        'rbSendToLab
        '
        Me.rbSendToLab.AutoSize = True
        Me.rbSendToLab.Location = New System.Drawing.Point(159, 3)
        Me.rbSendToLab.Name = "rbSendToLab"
        Me.rbSendToLab.Size = New System.Drawing.Size(44, 18)
        Me.rbSendToLab.TabIndex = 0
        Me.rbSendToLab.TabStop = True
        Me.rbSendToLab.Text = "Lab"
        Me.rbSendToLab.UseVisualStyleBackColor = True
        '
        'Label33
        '
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Location = New System.Drawing.Point(0, 3)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(159, 19)
        Me.Label33.TabIndex = 38
        Me.Label33.Text = "Send To : "
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPD_OrderNumber
        '
        Me.pnlPD_OrderNumber.Controls.Add(Me.lblOrderNumber)
        Me.pnlPD_OrderNumber.Controls.Add(Me.Label3)
        Me.pnlPD_OrderNumber.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPD_OrderNumber.Location = New System.Drawing.Point(1, 1)
        Me.pnlPD_OrderNumber.Name = "pnlPD_OrderNumber"
        Me.pnlPD_OrderNumber.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnlPD_OrderNumber.Size = New System.Drawing.Size(648, 25)
        Me.pnlPD_OrderNumber.TabIndex = 0
        '
        'lblOrderNumber
        '
        Me.lblOrderNumber.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblOrderNumber.Location = New System.Drawing.Point(159, 3)
        Me.lblOrderNumber.Name = "lblOrderNumber"
        Me.lblOrderNumber.Size = New System.Drawing.Size(555, 19)
        Me.lblOrderNumber.TabIndex = 39
        Me.lblOrderNumber.Text = "ORD001"
        Me.lblOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(159, 19)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "Order Number : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Location = New System.Drawing.Point(0, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 135)
        Me.Label26.TabIndex = 73
        Me.Label26.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Location = New System.Drawing.Point(649, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 135)
        Me.Label29.TabIndex = 72
        Me.Label29.Text = "label3"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Location = New System.Drawing.Point(0, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(650, 1)
        Me.Label30.TabIndex = 71
        Me.Label30.Text = "label1"
        '
        'pnlGrid
        '
        Me.pnlGrid.Controls.Add(Me.Label22)
        Me.pnlGrid.Controls.Add(Me.Label23)
        Me.pnlGrid.Controls.Add(Me.Label24)
        Me.pnlGrid.Controls.Add(Me.Label31)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 169)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGrid.Size = New System.Drawing.Size(650, 153)
        Me.pnlGrid.TabIndex = 1
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Location = New System.Drawing.Point(1, 149)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(648, 1)
        Me.Label22.TabIndex = 8
        Me.Label22.Text = "label2"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Location = New System.Drawing.Point(0, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 149)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Location = New System.Drawing.Point(649, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 149)
        Me.Label24.TabIndex = 6
        Me.Label24.Text = "label3"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Location = New System.Drawing.Point(0, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(650, 1)
        Me.Label31.TabIndex = 5
        Me.Label31.Text = "label1"
        '
        'gloUC_LabOrderDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlPatientDetail)
        Me.Controls.Add(Me.pnlTopHeader)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloUC_LabOrderDetail"
        Me.Size = New System.Drawing.Size(650, 322)
        Me.pnlTopHeader.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlPatientDetail.ResumeLayout(False)
        Me.pnlPD_RefBySamBy.ResumeLayout(False)
        Me.pnlPD_RefBySamBy_SamBy.ResumeLayout(False)
        Me.pnlPD_RefBySamBy_SamBy.PerformLayout()
        Me.pnlPD_RefBySamBy_RefBy.ResumeLayout(False)
        Me.pnlPD_RefBySamBy_RefBy.PerformLayout()
        Me.pnlPD_TaskDescDueDate.ResumeLayout(False)
        Me.pnlPD_TaskDescDueDate_DueDate.ResumeLayout(False)
        Me.pnlPD_TaskDescDueDate_TaskDesc.ResumeLayout(False)
        Me.pnlPD_TaskDescDueDate_TaskDesc.PerformLayout()
        Me.pnlPD_PrefLabAssTask.ResumeLayout(False)
        Me.pnlPD_PrefLabAssTask_AssTask.ResumeLayout(False)
        Me.pnlPD_PrefLabAssTask_PrefLab.ResumeLayout(False)
        Me.pnlPD_PrefLabAssTask_PrefLab.PerformLayout()
        Me.pnlPD_SendTo.ResumeLayout(False)
        Me.pnlPD_SendTo.PerformLayout()
        Me.pnlPD_OrderNumber.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTopHeader As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents pnlPatientDetail As System.Windows.Forms.Panel
    Friend WithEvents pnlPD_RefBySamBy As System.Windows.Forms.Panel
    Friend WithEvents pnlPD_RefBySamBy_SamBy As System.Windows.Forms.Panel
    Friend WithEvents txtSampledBy As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnSearchSB As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnClearSB As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pnlPD_RefBySamBy_RefBy As System.Windows.Forms.Panel
    Friend WithEvents txtReferredBy As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnSearchRB As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnClearRB As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlPD_PrefLabAssTask As System.Windows.Forms.Panel
    Friend WithEvents pnlPD_PrefLabAssTask_AssTask As System.Windows.Forms.Panel
    Friend WithEvents cmbAssignedTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnSearchAssTo As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnClearAssTo As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents pnlPD_PrefLabAssTask_PrefLab As System.Windows.Forms.Panel
    Friend WithEvents txtPreferredBy As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents btnSearchPL As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents btnClearPL As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblSendTo As System.Windows.Forms.Label
    Friend WithEvents pnlPD_OrderNumber As System.Windows.Forms.Panel
    Friend WithEvents lblOrderNumber As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlPD_TaskDescDueDate As System.Windows.Forms.Panel
    Friend WithEvents pnlPD_TaskDescDueDate_DueDate As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents pnlPD_TaskDescDueDate_TaskDesc As System.Windows.Forms.Panel
    Friend WithEvents txtTaskDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents dtTaskDueDate As System.Windows.Forms.DateTimePicker
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlPD_SendTo As System.Windows.Forms.Panel
    Friend WithEvents rbSendToPhysician As System.Windows.Forms.RadioButton
    Friend WithEvents rbSendToLab As System.Windows.Forms.RadioButton
    Friend WithEvents Label33 As System.Windows.Forms.Label

End Class
