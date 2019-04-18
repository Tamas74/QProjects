<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_LabTest
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlButton = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPrecaution = New System.Windows.Forms.TextBox
        Me.txtInstruction = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnUp = New System.Windows.Forms.Button
        Me.btnDown = New System.Windows.Forms.Button
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.lblLOINCCode = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.lblStorageTemperature = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.lblSpecimen = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.lblCollectionContainer = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlPatientDetail = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.pnlButton.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlPatientDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlButton
        '
        Me.pnlButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlButton.Controls.Add(Me.Panel1)
        Me.pnlButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlButton.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlButton.Location = New System.Drawing.Point(0, 0)
        Me.pnlButton.Name = "pnlButton"
        Me.pnlButton.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlButton.Size = New System.Drawing.Size(752, 27)
        Me.pnlButton.TabIndex = 82
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.btnUp)
        Me.Panel1.Controls.Add(Me.btnDown)
        Me.Panel1.Controls.Add(Me.lbl_pnlTop)
        Me.Panel1.Controls.Add(Me.lbl_pnlRight)
        Me.Panel1.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel1.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(752, 24)
        Me.Panel1.TabIndex = 58
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.txtPrecaution)
        Me.Panel4.Controls.Add(Me.txtInstruction)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(341, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(271, 16)
        Me.Panel4.TabIndex = 53
        Me.Panel4.Visible = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 17)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Precaution :"
        '
        'txtPrecaution
        '
        Me.txtPrecaution.Location = New System.Drawing.Point(73, 1)
        Me.txtPrecaution.Multiline = True
        Me.txtPrecaution.Name = "txtPrecaution"
        Me.txtPrecaution.Size = New System.Drawing.Size(40, 17)
        Me.txtPrecaution.TabIndex = 47
        '
        'txtInstruction
        '
        Me.txtInstruction.Location = New System.Drawing.Point(199, 3)
        Me.txtInstruction.Multiline = True
        Me.txtInstruction.Name = "txtInstruction"
        Me.txtInstruction.Size = New System.Drawing.Size(69, 19)
        Me.txtInstruction.TabIndex = 52
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(120, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 19)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "Instruction :"
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnUp.Location = New System.Drawing.Point(703, 1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(24, 22)
        Me.btnUp.TabIndex = 1
        Me.btnUp.UseVisualStyleBackColor = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDown.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDown.Location = New System.Drawing.Point(727, 1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(24, 22)
        Me.btnDown.TabIndex = 2
        Me.btnDown.UseVisualStyleBackColor = False
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(1, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(750, 1)
        Me.lbl_pnlTop.TabIndex = 54
        Me.lbl_pnlTop.Text = "label1"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(751, 0)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlRight.TabIndex = 55
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlLeft.TabIndex = 56
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(0, 23)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(752, 1)
        Me.lbl_pnlBottom.TabIndex = 57
        Me.lbl_pnlBottom.Text = "label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(752, 24)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "   Test Details :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.Label8)
        Me.Panel10.Controls.Add(Me.Panel11)
        Me.Panel10.Controls.Add(Me.Panel8)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel10.Location = New System.Drawing.Point(451, 1)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(300, 53)
        Me.Panel10.TabIndex = 68
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Location = New System.Drawing.Point(0, 52)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(300, 1)
        Me.Label8.TabIndex = 74
        Me.Label8.Text = "label2"
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.lblLOINCCode)
        Me.Panel11.Controls.Add(Me.Label6)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 19)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(300, 19)
        Me.Panel11.TabIndex = 73
        '
        'lblLOINCCode
        '
        Me.lblLOINCCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLOINCCode.Location = New System.Drawing.Point(139, 0)
        Me.lblLOINCCode.Name = "lblLOINCCode"
        Me.lblLOINCCode.Size = New System.Drawing.Size(161, 19)
        Me.lblLOINCCode.TabIndex = 39
        Me.lblLOINCCode.Text = "LOINC Code"
        Me.lblLOINCCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLOINCCode.Visible = False
        '
        'Label6
        '
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(139, 19)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "LOINC Code :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label6.Visible = False
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.lblStorageTemperature)
        Me.Panel8.Controls.Add(Me.Label4)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(300, 19)
        Me.Panel8.TabIndex = 72
        '
        'lblStorageTemperature
        '
        Me.lblStorageTemperature.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblStorageTemperature.Location = New System.Drawing.Point(139, 0)
        Me.lblStorageTemperature.Name = "lblStorageTemperature"
        Me.lblStorageTemperature.Size = New System.Drawing.Size(161, 19)
        Me.lblStorageTemperature.TabIndex = 39
        Me.lblStorageTemperature.Text = "Storage Temperature"
        Me.lblStorageTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 19)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Storage Temperature :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.lblSpecimen)
        Me.Panel9.Controls.Add(Me.Label3)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(1, 1)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(450, 19)
        Me.Panel9.TabIndex = 69
        '
        'lblSpecimen
        '
        Me.lblSpecimen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSpecimen.Location = New System.Drawing.Point(125, 0)
        Me.lblSpecimen.Name = "lblSpecimen"
        Me.lblSpecimen.Size = New System.Drawing.Size(325, 19)
        Me.lblSpecimen.TabIndex = 38
        Me.lblSpecimen.Text = "Specimen"
        Me.lblSpecimen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 19)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Specimen :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.lblCollectionContainer)
        Me.Panel7.Controls.Add(Me.Label2)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(1, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(450, 19)
        Me.Panel7.TabIndex = 70
        '
        'lblCollectionContainer
        '
        Me.lblCollectionContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCollectionContainer.Location = New System.Drawing.Point(125, 0)
        Me.lblCollectionContainer.Name = "lblCollectionContainer"
        Me.lblCollectionContainer.Size = New System.Drawing.Size(325, 19)
        Me.lblCollectionContainer.TabIndex = 39
        Me.lblCollectionContainer.Text = "Collection Container"
        Me.lblCollectionContainer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 19)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Collection Container :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPatientDetail
        '
        Me.pnlPatientDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPatientDetail.Controls.Add(Me.Label12)
        Me.pnlPatientDetail.Controls.Add(Me.Panel7)
        Me.pnlPatientDetail.Controls.Add(Me.Panel9)
        Me.pnlPatientDetail.Controls.Add(Me.Panel10)
        Me.pnlPatientDetail.Controls.Add(Me.Label9)
        Me.pnlPatientDetail.Controls.Add(Me.Label10)
        Me.pnlPatientDetail.Controls.Add(Me.Label11)
        Me.pnlPatientDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientDetail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPatientDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlPatientDetail.Location = New System.Drawing.Point(0, 27)
        Me.pnlPatientDetail.Name = "pnlPatientDetail"
        Me.pnlPatientDetail.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlPatientDetail.Size = New System.Drawing.Size(752, 57)
        Me.pnlPatientDetail.TabIndex = 83
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Location = New System.Drawing.Point(1, 53)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(450, 1)
        Me.Label12.TabIndex = 75
        Me.Label12.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Location = New System.Drawing.Point(0, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 53)
        Me.Label9.TabIndex = 73
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Location = New System.Drawing.Point(751, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 53)
        Me.Label10.TabIndex = 72
        Me.Label10.Text = "label3"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(752, 1)
        Me.Label11.TabIndex = 71
        Me.Label11.Text = "label1"
        '
        'gloUC_LabTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.pnlPatientDetail)
        Me.Controls.Add(Me.pnlButton)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloUC_LabTest"
        Me.Size = New System.Drawing.Size(752, 84)
        Me.pnlButton.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.pnlPatientDetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlButton As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents txtPrecaution As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents txtInstruction As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents lblLOINCCode As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents lblStorageTemperature As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents lblSpecimen As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents lblCollectionContainer As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlPatientDetail As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
