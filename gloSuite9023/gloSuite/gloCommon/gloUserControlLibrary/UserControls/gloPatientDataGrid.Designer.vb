<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloPatientDataGrid
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try

                If (IsNothing(dgPatient) = False) Then
                    dgPatient.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgPatient)
                    dgPatient.Dispose()
                    dgPatient = Nothing
                End If
            Catch ex As Exception

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloPatientDataGrid))
        Me.pnlPatients = New System.Windows.Forms.Panel
        Me.pnlPatientGrid = New System.Windows.Forms.Panel
        Me.dgPatient = New System.Windows.Forms.DataGrid
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnlPatientSearch = New System.Windows.Forms.Panel
        Me.pnlSearchPatient = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.picAdvSearch = New System.Windows.Forms.PictureBox
        Me.txtSearchPatient = New System.Windows.Forms.TextBox
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.btnGetAllPatients = New System.Windows.Forms.Button
        Me.lblSearchCriteria = New System.Windows.Forms.Label
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.pnl_tls = New System.Windows.Forms.Panel
        Me.tlsNewAppointment = New gloGlobal.gloToolStripIgnoreFocus
        Me.btn_tls_OK = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Cancel = New System.Windows.Forms.ToolStripButton
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlPatients.SuspendLayout()
        Me.pnlPatientGrid.SuspendLayout()
        CType(Me.dgPatient, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPatientSearch.SuspendLayout()
        Me.pnlSearchPatient.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.picAdvSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tls.SuspendLayout()
        Me.tlsNewAppointment.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlPatients
        '
        Me.pnlPatients.Controls.Add(Me.pnlPatientGrid)
        Me.pnlPatients.Controls.Add(Me.pnlPatientSearch)
        Me.pnlPatients.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatients.Location = New System.Drawing.Point(0, 61)
        Me.pnlPatients.Name = "pnlPatients"
        Me.pnlPatients.Size = New System.Drawing.Size(806, 362)
        Me.pnlPatients.TabIndex = 20
        '
        'pnlPatientGrid
        '
        Me.pnlPatientGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPatientGrid.Controls.Add(Me.dgPatient)
        Me.pnlPatientGrid.Controls.Add(Me.Label1)
        Me.pnlPatientGrid.Controls.Add(Me.Label2)
        Me.pnlPatientGrid.Controls.Add(Me.Label3)
        Me.pnlPatientGrid.Controls.Add(Me.Label4)
        Me.pnlPatientGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientGrid.Location = New System.Drawing.Point(0, 30)
        Me.pnlPatientGrid.Name = "pnlPatientGrid"
        Me.pnlPatientGrid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPatientGrid.Size = New System.Drawing.Size(806, 332)
        Me.pnlPatientGrid.TabIndex = 22
        '
        'dgPatient
        '
        Me.dgPatient.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgPatient.BackgroundColor = System.Drawing.Color.White
        Me.dgPatient.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgPatient.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgPatient.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgPatient.CaptionForeColor = System.Drawing.Color.White
        Me.dgPatient.CaptionVisible = False
        Me.dgPatient.DataMember = ""
        Me.dgPatient.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgPatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgPatient.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgPatient.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgPatient.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgPatient.HeaderForeColor = System.Drawing.Color.White
        Me.dgPatient.Location = New System.Drawing.Point(4, 1)
        Me.dgPatient.Name = "dgPatient"
        Me.dgPatient.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgPatient.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgPatient.ReadOnly = True
        Me.dgPatient.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgPatient.SelectionForeColor = System.Drawing.Color.Black
        Me.dgPatient.Size = New System.Drawing.Size(798, 327)
        Me.dgPatient.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(4, 328)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(798, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 328)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(802, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 328)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(800, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'pnlPatientSearch
        '
        Me.pnlPatientSearch.Controls.Add(Me.pnlSearchPatient)
        Me.pnlPatientSearch.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlPatientSearch.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlPatientSearch.Controls.Add(Me.lbl_pnlRight)
        Me.pnlPatientSearch.Controls.Add(Me.lbl_pnlTop)
        Me.pnlPatientSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientSearch.Name = "pnlPatientSearch"
        Me.pnlPatientSearch.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPatientSearch.Size = New System.Drawing.Size(806, 30)
        Me.pnlPatientSearch.TabIndex = 3
        '
        'pnlSearchPatient
        '
        Me.pnlSearchPatient.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearchPatient.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.pnlSearchPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearchPatient.Controls.Add(Me.Panel1)
        Me.pnlSearchPatient.Controls.Add(Me.txtSearchPatient)
        Me.pnlSearchPatient.Controls.Add(Me.btnRefresh)
        Me.pnlSearchPatient.Controls.Add(Me.btnGetAllPatients)
        Me.pnlSearchPatient.Controls.Add(Me.lblSearchCriteria)
        Me.pnlSearchPatient.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearchPatient.Location = New System.Drawing.Point(4, 1)
        Me.pnlSearchPatient.Name = "pnlSearchPatient"
        Me.pnlSearchPatient.Size = New System.Drawing.Size(798, 25)
        Me.pnlSearchPatient.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.picAdvSearch)
        Me.Panel1.Location = New System.Drawing.Point(314, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(23, 23)
        Me.Panel1.TabIndex = 52
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Location = New System.Drawing.Point(22, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 21)
        Me.Label8.TabIndex = 55
        Me.Label8.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 21)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 1)
        Me.Label6.TabIndex = 53
        Me.Label6.Text = "label2"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(0, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(23, 1)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "label2"
        '
        'picAdvSearch
        '
        Me.picAdvSearch.BackColor = System.Drawing.Color.Transparent
        Me.picAdvSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picAdvSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picAdvSearch.Image = CType(resources.GetObject("picAdvSearch.Image"), System.Drawing.Image)
        Me.picAdvSearch.Location = New System.Drawing.Point(0, 0)
        Me.picAdvSearch.Name = "picAdvSearch"
        Me.picAdvSearch.Size = New System.Drawing.Size(23, 23)
        Me.picAdvSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picAdvSearch.TabIndex = 51
        Me.picAdvSearch.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picAdvSearch, "Advance Search")
        '
        'txtSearchPatient
        '
        Me.txtSearchPatient.ForeColor = System.Drawing.Color.Black
        Me.txtSearchPatient.Location = New System.Drawing.Point(87, 1)
        Me.txtSearchPatient.Name = "txtSearchPatient"
        Me.txtSearchPatient.Size = New System.Drawing.Size(168, 22)
        Me.txtSearchPatient.TabIndex = 4
        '
        'btnRefresh
        '
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(287, 1)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(23, 23)
        Me.btnRefresh.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Refresh")
        '
        'btnGetAllPatients
        '
        Me.btnGetAllPatients.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGetAllPatients.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGetAllPatients.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnGetAllPatients.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnGetAllPatients.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnGetAllPatients.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGetAllPatients.Image = CType(resources.GetObject("btnGetAllPatients.Image"), System.Drawing.Image)
        Me.btnGetAllPatients.Location = New System.Drawing.Point(259, 1)
        Me.btnGetAllPatients.Name = "btnGetAllPatients"
        Me.btnGetAllPatients.Size = New System.Drawing.Size(23, 23)
        Me.btnGetAllPatients.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.btnGetAllPatients, "Clear")
        '
        'lblSearchCriteria
        '
        Me.lblSearchCriteria.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSearchCriteria.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchCriteria.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSearchCriteria.Location = New System.Drawing.Point(0, 0)
        Me.lblSearchCriteria.Name = "lblSearchCriteria"
        Me.lblSearchCriteria.Size = New System.Drawing.Size(798, 25)
        Me.lblSearchCriteria.TabIndex = 5
        Me.lblSearchCriteria.Text = "  Search"
        Me.lblSearchCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 26)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(798, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 26)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(802, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 26)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(800, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnl_tls
        '
        Me.pnl_tls.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls.Controls.Add(Me.tlsNewAppointment)
        Me.pnl_tls.Controls.Add(Me.Label10)
        Me.pnl_tls.Controls.Add(Me.Label12)
        Me.pnl_tls.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls.Name = "pnl_tls"
        Me.pnl_tls.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_tls.Size = New System.Drawing.Size(806, 61)
        Me.pnl_tls.TabIndex = 21
        '
        'tlsNewAppointment
        '
        Me.tlsNewAppointment.BackColor = System.Drawing.Color.Transparent
        Me.tlsNewAppointment.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.tlsNewAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsNewAppointment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlsNewAppointment.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsNewAppointment.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_OK, Me.btn_tls_Cancel})
        Me.tlsNewAppointment.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsNewAppointment.Location = New System.Drawing.Point(4, 4)
        Me.tlsNewAppointment.Name = "tlsNewAppointment"
        Me.tlsNewAppointment.Size = New System.Drawing.Size(799, 54)
        Me.tlsNewAppointment.TabIndex = 0
        Me.tlsNewAppointment.Text = "toolStrip1"
        '
        'btn_tls_OK
        '
        Me.btn_tls_OK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_OK.Image = CType(resources.GetObject("btn_tls_OK.Image"), System.Drawing.Image)
        Me.btn_tls_OK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_OK.Name = "btn_tls_OK"
        Me.btn_tls_OK.Size = New System.Drawing.Size(66, 50)
        Me.btn_tls_OK.Tag = "OK"
        Me.btn_tls_OK.Text = "&Save&&Cls"
        Me.btn_tls_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_OK.ToolTipText = "Save and Close"
        '
        'btn_tls_Cancel
        '
        Me.btn_tls_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Cancel.Image = CType(resources.GetObject("btn_tls_Cancel.Image"), System.Drawing.Image)
        Me.btn_tls_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Cancel.Name = "btn_tls_Cancel"
        Me.btn_tls_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Cancel.Tag = "Cancel"
        Me.btn_tls_Cancel.Text = "&Close"
        Me.btn_tls_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Cancel.ToolTipText = "Close"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 54)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(800, 1)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "label1"
        '
        'gloPatientDataGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnlPatients)
        Me.Controls.Add(Me.pnl_tls)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloPatientDataGrid"
        Me.Size = New System.Drawing.Size(806, 423)
        Me.pnlPatients.ResumeLayout(False)
        Me.pnlPatientGrid.ResumeLayout(False)
        CType(Me.dgPatient, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPatientSearch.ResumeLayout(False)
        Me.pnlSearchPatient.ResumeLayout(False)
        Me.pnlSearchPatient.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.picAdvSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tls.ResumeLayout(False)
        Me.pnl_tls.PerformLayout()
        Me.tlsNewAppointment.ResumeLayout(False)
        Me.tlsNewAppointment.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlPatients As System.Windows.Forms.Panel
    Friend WithEvents pnlPatientSearch As System.Windows.Forms.Panel
    Friend WithEvents pnlSearchPatient As System.Windows.Forms.Panel
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnGetAllPatients As System.Windows.Forms.Button
    Public WithEvents lblSearchCriteria As System.Windows.Forms.Label
    Public WithEvents txtSearchPatient As System.Windows.Forms.TextBox
    Friend WithEvents pnlPatientGrid As System.Windows.Forms.Panel
    Public WithEvents dgPatient As System.Windows.Forms.DataGrid
    Friend WithEvents picAdvSearch As System.Windows.Forms.PictureBox
    Private WithEvents pnl_tls As System.Windows.Forms.Panel
    Private WithEvents tlsNewAppointment As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_OK As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class
