<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientSummeryScreen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Dim dtpContextMenustrip As ContextMenuStrip() = {cntPatientAssociation}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenustrip)
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(dtpContextMenustrip)
                Catch ex As Exception

                End Try


                Try
                    If (IsNothing(gloUC_PatientStrip1) = False) Then
                        gloUC_PatientStrip1.Dispose()
                        gloUC_PatientStrip1 = Nothing
                    End If
                Catch ex As Exception

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientSummeryScreen))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnltrvAssociation = New System.Windows.Forms.Panel
        Me.trvPatientAssoication = New System.Windows.Forms.TreeView
        Me.ImgPatient = New System.Windows.Forms.ImageList(Me.components)
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.PnltrvAssociatinType = New System.Windows.Forms.Panel
        Me.pnltrvAssociatType = New System.Windows.Forms.Panel
        Me.trvAssociatType = New System.Windows.Forms.TreeView
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.pnltxtsearch = New System.Windows.Forms.Panel
        Me.txtsearch = New System.Windows.Forms.TextBox
        Me.btnClear = New System.Windows.Forms.Button
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.pnlbtnRadiology = New System.Windows.Forms.Panel
        Me.btnRadiology = New System.Windows.Forms.Button
        Me.pnlbtnLabs = New System.Windows.Forms.Panel
        Me.btnLabs = New System.Windows.Forms.Button
        Me.pnlbtnScanDoc = New System.Windows.Forms.Panel
        Me.btnScanDoc = New System.Windows.Forms.Button
        Me.pnlbtnExam = New System.Windows.Forms.Panel
        Me.btnExam = New System.Windows.Forms.Button
        Me.pnlPatientList = New System.Windows.Forms.Panel
        Me.GloPatientDataGrid = New gloUserControlLibrary.gloPatientDataGrid
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnchangepatient = New System.Windows.Forms.Button
        Me.txtpatientName = New System.Windows.Forms.TextBox
        Me.lblPatientName = New System.Windows.Forms.Label
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.tlsPatientSummary = New gloGlobal.gloToolStripIgnoreFocus
        Me.tls_ChangePatient = New System.Windows.Forms.ToolStripButton
        Me.tls_Refresh = New System.Windows.Forms.ToolStripButton
        Me.tls_Save = New System.Windows.Forms.ToolStripButton
        Me.tls_Close = New System.Windows.Forms.ToolStripButton
        Me.cntPatientAssociation = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDelete = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.pnltrvAssociation.SuspendLayout()
        Me.PnltrvAssociatinType.SuspendLayout()
        Me.pnltrvAssociatType.SuspendLayout()
        Me.pnltxtsearch.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlbtnRadiology.SuspendLayout()
        Me.pnlbtnLabs.SuspendLayout()
        Me.pnlbtnScanDoc.SuspendLayout()
        Me.pnlbtnExam.SuspendLayout()
        Me.pnlPatientList.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.tlsPatientSummary.SuspendLayout()
        Me.cntPatientAssociation.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.pnltrvAssociation)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.PnltrvAssociatinType)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(0, 279)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(808, 394)
        Me.pnlMain.TabIndex = 4
        '
        'pnltrvAssociation
        '
        Me.pnltrvAssociation.Controls.Add(Me.trvPatientAssoication)
        Me.pnltrvAssociation.Controls.Add(Me.Label2)
        Me.pnltrvAssociation.Controls.Add(Me.Label1)
        Me.pnltrvAssociation.Controls.Add(Me.Label19)
        Me.pnltrvAssociation.Controls.Add(Me.Label20)
        Me.pnltrvAssociation.Controls.Add(Me.Label21)
        Me.pnltrvAssociation.Controls.Add(Me.Label22)
        Me.pnltrvAssociation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvAssociation.Location = New System.Drawing.Point(0, 0)
        Me.pnltrvAssociation.Name = "pnltrvAssociation"
        Me.pnltrvAssociation.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltrvAssociation.Size = New System.Drawing.Size(590, 394)
        Me.pnltrvAssociation.TabIndex = 1
        '
        'trvPatientAssoication
        '
        Me.trvPatientAssoication.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvPatientAssoication.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvPatientAssoication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvPatientAssoication.ForeColor = System.Drawing.Color.Black
        Me.trvPatientAssoication.ImageIndex = 5
        Me.trvPatientAssoication.ImageList = Me.ImgPatient
        Me.trvPatientAssoication.Indent = 20
        Me.trvPatientAssoication.ItemHeight = 20
        Me.trvPatientAssoication.Location = New System.Drawing.Point(8, 5)
        Me.trvPatientAssoication.Name = "trvPatientAssoication"
        Me.trvPatientAssoication.SelectedImageIndex = 0
        Me.trvPatientAssoication.ShowLines = False
        Me.trvPatientAssoication.ShowPlusMinus = False
        Me.trvPatientAssoication.ShowRootLines = False
        Me.trvPatientAssoication.Size = New System.Drawing.Size(581, 385)
        Me.trvPatientAssoication.TabIndex = 0
        '
        'ImgPatient
        '
        Me.ImgPatient.ImageStream = CType(resources.GetObject("ImgPatient.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgPatient.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgPatient.Images.SetKeyName(0, "Patient.ico")
        Me.ImgPatient.Images.SetKeyName(1, "Patient Exam.ico")
        Me.ImgPatient.Images.SetKeyName(2, "Radiology_01.ico")
        Me.ImgPatient.Images.SetKeyName(3, "Lab.ico")
        Me.ImgPatient.Images.SetKeyName(4, "")
        Me.ImgPatient.Images.SetKeyName(5, "Bullet06.ico")
        Me.ImgPatient.Images.SetKeyName(6, "Orders.ico")
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(4, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(4, 385)
        Me.Label2.TabIndex = 39
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(4, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(585, 4)
        Me.Label1.TabIndex = 38
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(4, 390)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(585, 1)
        Me.Label19.TabIndex = 12
        Me.Label19.Text = "label2"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(3, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 390)
        Me.Label20.TabIndex = 11
        Me.Label20.Text = "label4"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(589, 1)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 390)
        Me.Label21.TabIndex = 10
        Me.Label21.Text = "label3"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(587, 1)
        Me.Label22.TabIndex = 9
        Me.Label22.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter1.Location = New System.Drawing.Point(590, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 394)
        Me.Splitter1.TabIndex = 6
        Me.Splitter1.TabStop = False
        '
        'PnltrvAssociatinType
        '
        Me.PnltrvAssociatinType.Controls.Add(Me.pnltrvAssociatType)
        Me.PnltrvAssociatinType.Controls.Add(Me.pnltxtsearch)
        Me.PnltrvAssociatinType.Controls.Add(Me.pnlbtnRadiology)
        Me.PnltrvAssociatinType.Controls.Add(Me.pnlbtnLabs)
        Me.PnltrvAssociatinType.Controls.Add(Me.pnlbtnScanDoc)
        Me.PnltrvAssociatinType.Controls.Add(Me.pnlbtnExam)
        Me.PnltrvAssociatinType.Dock = System.Windows.Forms.DockStyle.Right
        Me.PnltrvAssociatinType.Location = New System.Drawing.Point(593, 0)
        Me.PnltrvAssociatinType.Name = "PnltrvAssociatinType"
        Me.PnltrvAssociatinType.Size = New System.Drawing.Size(215, 394)
        Me.PnltrvAssociatinType.TabIndex = 0
        '
        'pnltrvAssociatType
        '
        Me.pnltrvAssociatType.Controls.Add(Me.trvAssociatType)
        Me.pnltrvAssociatType.Controls.Add(Me.Label3)
        Me.pnltrvAssociatType.Controls.Add(Me.Label4)
        Me.pnltrvAssociatType.Controls.Add(Me.Label9)
        Me.pnltrvAssociatType.Controls.Add(Me.Label10)
        Me.pnltrvAssociatType.Controls.Add(Me.Label11)
        Me.pnltrvAssociatType.Controls.Add(Me.Label12)
        Me.pnltrvAssociatType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvAssociatType.Location = New System.Drawing.Point(0, 56)
        Me.pnltrvAssociatType.Name = "pnltrvAssociatType"
        Me.pnltrvAssociatType.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltrvAssociatType.Size = New System.Drawing.Size(215, 248)
        Me.pnltrvAssociatType.TabIndex = 1
        '
        'trvAssociatType
        '
        Me.trvAssociatType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAssociatType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAssociatType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvAssociatType.ForeColor = System.Drawing.Color.Black
        Me.trvAssociatType.HideSelection = False
        Me.trvAssociatType.ImageIndex = 5
        Me.trvAssociatType.ImageList = Me.ImgPatient
        Me.trvAssociatType.Indent = 20
        Me.trvAssociatType.ItemHeight = 20
        Me.trvAssociatType.Location = New System.Drawing.Point(5, 5)
        Me.trvAssociatType.Name = "trvAssociatType"
        Me.trvAssociatType.SelectedImageIndex = 0
        Me.trvAssociatType.ShowLines = False
        Me.trvAssociatType.ShowPlusMinus = False
        Me.trvAssociatType.ShowRootLines = False
        Me.trvAssociatType.Size = New System.Drawing.Size(206, 239)
        Me.trvAssociatType.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(1, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(4, 239)
        Me.Label3.TabIndex = 41
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(1, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(210, 4)
        Me.Label4.TabIndex = 40
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(1, 244)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(210, 1)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 244)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(211, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 244)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(212, 1)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "label1"
        '
        'pnltxtsearch
        '
        Me.pnltxtsearch.BackColor = System.Drawing.Color.Transparent
        Me.pnltxtsearch.Controls.Add(Me.txtsearch)
        Me.pnltxtsearch.Controls.Add(Me.btnClear)
        Me.pnltxtsearch.Controls.Add(Me.Label13)
        Me.pnltxtsearch.Controls.Add(Me.Label14)
        Me.pnltxtsearch.Controls.Add(Me.PictureBox3)
        Me.pnltxtsearch.Controls.Add(Me.Label15)
        Me.pnltxtsearch.Controls.Add(Me.Label16)
        Me.pnltxtsearch.Controls.Add(Me.Label17)
        Me.pnltxtsearch.Controls.Add(Me.Label18)
        Me.pnltxtsearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltxtsearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltxtsearch.ForeColor = System.Drawing.Color.Black
        Me.pnltxtsearch.Location = New System.Drawing.Point(0, 30)
        Me.pnltxtsearch.Name = "pnltxtsearch"
        Me.pnltxtsearch.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltxtsearch.Size = New System.Drawing.Size(215, 26)
        Me.pnltxtsearch.TabIndex = 0
        '
        'txtsearch
        '
        Me.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.ForeColor = System.Drawing.Color.Black
        Me.txtsearch.Location = New System.Drawing.Point(28, 5)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(162, 15)
        Me.txtsearch.TabIndex = 0
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(190, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 15)
        Me.btnClear.TabIndex = 44
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Location = New System.Drawing.Point(28, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(183, 4)
        Me.Label13.TabIndex = 37
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.White
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Location = New System.Drawing.Point(28, 20)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(183, 2)
        Me.Label14.TabIndex = 38
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.White
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(27, 21)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 9
        Me.PictureBox3.TabStop = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Location = New System.Drawing.Point(1, 22)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(210, 1)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "label1"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(1, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(210, 1)
        Me.Label16.TabIndex = 36
        Me.Label16.Text = "label1"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 23)
        Me.Label17.TabIndex = 39
        Me.Label17.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Location = New System.Drawing.Point(211, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 23)
        Me.Label18.TabIndex = 40
        Me.Label18.Text = "label4"
        '
        'pnlbtnRadiology
        '
        Me.pnlbtnRadiology.Controls.Add(Me.btnRadiology)
        Me.pnlbtnRadiology.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnRadiology.Location = New System.Drawing.Point(0, 304)
        Me.pnlbtnRadiology.Name = "pnlbtnRadiology"
        Me.pnlbtnRadiology.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnRadiology.Size = New System.Drawing.Size(215, 30)
        Me.pnlbtnRadiology.TabIndex = 2
        '
        'btnRadiology
        '
        Me.btnRadiology.BackgroundImage = CType(resources.GetObject("btnRadiology.BackgroundImage"), System.Drawing.Image)
        Me.btnRadiology.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRadiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRadiology.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRadiology.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRadiology.Location = New System.Drawing.Point(0, 0)
        Me.btnRadiology.Name = "btnRadiology"
        Me.btnRadiology.Size = New System.Drawing.Size(212, 27)
        Me.btnRadiology.TabIndex = 1
        Me.btnRadiology.Text = "Order Templates"
        Me.btnRadiology.UseVisualStyleBackColor = True
        '
        'pnlbtnLabs
        '
        Me.pnlbtnLabs.Controls.Add(Me.btnLabs)
        Me.pnlbtnLabs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnLabs.Location = New System.Drawing.Point(0, 334)
        Me.pnlbtnLabs.Name = "pnlbtnLabs"
        Me.pnlbtnLabs.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnLabs.Size = New System.Drawing.Size(215, 30)
        Me.pnlbtnLabs.TabIndex = 3
        '
        'btnLabs
        '
        Me.btnLabs.BackgroundImage = CType(resources.GetObject("btnLabs.BackgroundImage"), System.Drawing.Image)
        Me.btnLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLabs.Location = New System.Drawing.Point(0, 0)
        Me.btnLabs.Name = "btnLabs"
        Me.btnLabs.Size = New System.Drawing.Size(212, 27)
        Me.btnLabs.TabIndex = 2
        Me.btnLabs.Text = "Orders"
        Me.btnLabs.UseVisualStyleBackColor = True
        '
        'pnlbtnScanDoc
        '
        Me.pnlbtnScanDoc.Controls.Add(Me.btnScanDoc)
        Me.pnlbtnScanDoc.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnScanDoc.Location = New System.Drawing.Point(0, 364)
        Me.pnlbtnScanDoc.Name = "pnlbtnScanDoc"
        Me.pnlbtnScanDoc.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnScanDoc.Size = New System.Drawing.Size(215, 30)
        Me.pnlbtnScanDoc.TabIndex = 4
        '
        'btnScanDoc
        '
        Me.btnScanDoc.BackgroundImage = CType(resources.GetObject("btnScanDoc.BackgroundImage"), System.Drawing.Image)
        Me.btnScanDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnScanDoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnScanDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnScanDoc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnScanDoc.Location = New System.Drawing.Point(0, 0)
        Me.btnScanDoc.Name = "btnScanDoc"
        Me.btnScanDoc.Size = New System.Drawing.Size(212, 27)
        Me.btnScanDoc.TabIndex = 3
        Me.btnScanDoc.Text = "Scanned Documents"
        Me.btnScanDoc.UseVisualStyleBackColor = True
        '
        'pnlbtnExam
        '
        Me.pnlbtnExam.Controls.Add(Me.btnExam)
        Me.pnlbtnExam.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnExam.Location = New System.Drawing.Point(0, 0)
        Me.pnlbtnExam.Name = "pnlbtnExam"
        Me.pnlbtnExam.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnExam.Size = New System.Drawing.Size(215, 30)
        Me.pnlbtnExam.TabIndex = 5
        '
        'btnExam
        '
        Me.btnExam.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Orange1
        Me.btnExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExam.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExam.Location = New System.Drawing.Point(0, 0)
        Me.btnExam.Name = "btnExam"
        Me.btnExam.Size = New System.Drawing.Size(212, 27)
        Me.btnExam.TabIndex = 0
        Me.btnExam.Text = "Exams"
        Me.btnExam.UseVisualStyleBackColor = False
        '
        'pnlPatientList
        '
        Me.pnlPatientList.Controls.Add(Me.GloPatientDataGrid)
        Me.pnlPatientList.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientList.Location = New System.Drawing.Point(0, 56)
        Me.pnlPatientList.Name = "pnlPatientList"
        Me.pnlPatientList.Size = New System.Drawing.Size(808, 223)
        Me.pnlPatientList.TabIndex = 3
        Me.pnlPatientList.Visible = False
        '
        'GloPatientDataGrid
        '
        Me.GloPatientDataGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GloPatientDataGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloPatientDataGrid.FirstName = Nothing
        Me.GloPatientDataGrid.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GloPatientDataGrid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GloPatientDataGrid.LastName = Nothing
        Me.GloPatientDataGrid.Location = New System.Drawing.Point(0, 0)
        Me.GloPatientDataGrid.MiddleName = Nothing
        Me.GloPatientDataGrid.Name = "GloPatientDataGrid"
        Me.GloPatientDataGrid.PatientCode = Nothing
        Me.GloPatientDataGrid.PatientID = CType(0, Long)
        Me.GloPatientDataGrid.Size = New System.Drawing.Size(808, 223)
        Me.GloPatientDataGrid.TabIndex = 0
        Me.GloPatientDataGrid.Visible = False
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.Label7)
        Me.pnlTop.Controls.Add(Me.Label8)
        Me.pnlTop.Controls.Add(Me.btnchangepatient)
        Me.pnlTop.Controls.Add(Me.txtpatientName)
        Me.pnlTop.Controls.Add(Me.lblPatientName)
        Me.pnlTop.Location = New System.Drawing.Point(0, 54)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(808, 39)
        Me.pnlTop.TabIndex = 1
        Me.pnlTop.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(800, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 32)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(804, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 32)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(802, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'btnchangepatient
        '
        Me.btnchangepatient.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnchangepatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnchangepatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnchangepatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnchangepatient.Location = New System.Drawing.Point(449, 6)
        Me.btnchangepatient.Name = "btnchangepatient"
        Me.btnchangepatient.Size = New System.Drawing.Size(126, 26)
        Me.btnchangepatient.TabIndex = 2
        Me.btnchangepatient.Text = "Change Patient"
        Me.btnchangepatient.UseVisualStyleBackColor = True
        Me.btnchangepatient.Visible = False
        '
        'txtpatientName
        '
        Me.txtpatientName.BackColor = System.Drawing.Color.White
        Me.txtpatientName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpatientName.Location = New System.Drawing.Point(115, 7)
        Me.txtpatientName.Name = "txtpatientName"
        Me.txtpatientName.ReadOnly = True
        Me.txtpatientName.Size = New System.Drawing.Size(328, 23)
        Me.txtpatientName.TabIndex = 0
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.Location = New System.Drawing.Point(12, 12)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(97, 14)
        Me.lblPatientName.TabIndex = 0
        Me.lblPatientName.Text = "Patient Name:"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tlsPatientSummary)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(808, 56)
        Me.pnlToolStrip.TabIndex = 0
        '
        'tlsPatientSummary
        '
        Me.tlsPatientSummary.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsPatientSummary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsPatientSummary.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsPatientSummary.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsPatientSummary.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tls_ChangePatient, Me.tls_Refresh, Me.tls_Save, Me.tls_Close})
        Me.tlsPatientSummary.Location = New System.Drawing.Point(0, 0)
        Me.tlsPatientSummary.Name = "tlsPatientSummary"
        Me.tlsPatientSummary.Size = New System.Drawing.Size(808, 53)
        Me.tlsPatientSummary.TabIndex = 0
        Me.tlsPatientSummary.Text = "ToolStrip1"
        '
        'tls_ChangePatient
        '
        Me.tls_ChangePatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_ChangePatient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tls_ChangePatient.Image = CType(resources.GetObject("tls_ChangePatient.Image"), System.Drawing.Image)
        Me.tls_ChangePatient.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_ChangePatient.Name = "tls_ChangePatient"
        Me.tls_ChangePatient.Size = New System.Drawing.Size(106, 50)
        Me.tls_ChangePatient.Tag = "ChangePatient"
        Me.tls_ChangePatient.Text = "Change &Patient"
        Me.tls_ChangePatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_ChangePatient.ToolTipText = "Change Patient"
        '
        'tls_Refresh
        '
        Me.tls_Refresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Refresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tls_Refresh.Image = CType(resources.GetObject("tls_Refresh.Image"), System.Drawing.Image)
        Me.tls_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Refresh.Name = "tls_Refresh"
        Me.tls_Refresh.Size = New System.Drawing.Size(58, 50)
        Me.tls_Refresh.Tag = "Refresh"
        Me.tls_Refresh.Text = "&Refresh"
        Me.tls_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_Refresh.ToolTipText = "Refresh  "
        '
        'tls_Save
        '
        Me.tls_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Save.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tls_Save.Image = CType(resources.GetObject("tls_Save.Image"), System.Drawing.Image)
        Me.tls_Save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tls_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Save.Name = "tls_Save"
        Me.tls_Save.Size = New System.Drawing.Size(66, 50)
        Me.tls_Save.Tag = "Save"
        Me.tls_Save.Text = "&Save&&Cls"
        Me.tls_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_Save.ToolTipText = "Save and Close"
        '
        'tls_Close
        '
        Me.tls_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tls_Close.Image = CType(resources.GetObject("tls_Close.Image"), System.Drawing.Image)
        Me.tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Close.Name = "tls_Close"
        Me.tls_Close.Size = New System.Drawing.Size(43, 50)
        Me.tls_Close.Tag = "Close"
        Me.tls_Close.Text = "&Close"
        Me.tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_Close.ToolTipText = "Close  "
        '
        'cntPatientAssociation
        '
        Me.cntPatientAssociation.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDelete})
        Me.cntPatientAssociation.Name = "cntPatientAssociation"
        Me.cntPatientAssociation.Size = New System.Drawing.Size(186, 26)
        '
        'mnuDelete
        '
        Me.mnuDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuDelete.Image = CType(resources.GetObject("mnuDelete.Image"), System.Drawing.Image)
        Me.mnuDelete.Name = "mnuDelete"
        Me.mnuDelete.Size = New System.Drawing.Size(185, 22)
        Me.mnuDelete.Text = "Delete Association"
        '
        'frmPatientSummeryScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(808, 673)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlPatientList)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientSummeryScreen"
        Me.Text = "Patient Summary"
        Me.pnlMain.ResumeLayout(False)
        Me.pnltrvAssociation.ResumeLayout(False)
        Me.PnltrvAssociatinType.ResumeLayout(False)
        Me.pnltrvAssociatType.ResumeLayout(False)
        Me.pnltxtsearch.ResumeLayout(False)
        Me.pnltxtsearch.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlbtnRadiology.ResumeLayout(False)
        Me.pnlbtnLabs.ResumeLayout(False)
        Me.pnlbtnScanDoc.ResumeLayout(False)
        Me.pnlbtnExam.ResumeLayout(False)
        Me.pnlPatientList.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tlsPatientSummary.ResumeLayout(False)
        Me.tlsPatientSummary.PerformLayout()
        Me.cntPatientAssociation.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents PnltrvAssociatinType As System.Windows.Forms.Panel
    Friend WithEvents pnltrvAssociation As System.Windows.Forms.Panel
    Friend WithEvents trvPatientAssoication As System.Windows.Forms.TreeView
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents trvAssociatType As System.Windows.Forms.TreeView
    Friend WithEvents btnRadiology As System.Windows.Forms.Button
    Friend WithEvents btnLabs As System.Windows.Forms.Button
    Friend WithEvents btnScanDoc As System.Windows.Forms.Button
    Friend WithEvents btnExam As System.Windows.Forms.Button
    Friend WithEvents btnchangepatient As System.Windows.Forms.Button
    Friend WithEvents txtpatientName As System.Windows.Forms.TextBox
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents pnlPatientList As System.Windows.Forms.Panel
    Friend WithEvents GloPatientDataGrid As gloUserControlLibrary.gloPatientDataGrid
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tlsPatientSummary As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tls_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_Refresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImgPatient As System.Windows.Forms.ImageList
    Friend WithEvents cntPatientAssociation As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnLabs As System.Windows.Forms.Panel
    Friend WithEvents pnlbtnScanDoc As System.Windows.Forms.Panel
    Friend WithEvents pnlbtnRadiology As System.Windows.Forms.Panel
    Friend WithEvents pnltrvAssociatType As System.Windows.Forms.Panel
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnExam As System.Windows.Forms.Panel
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pnltxtsearch As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label

    Private Sub GloPatientDataGrid_GetAllPatient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloPatientDataGrid.GetAllPatient_Click

    End Sub
    Friend WithEvents tls_ChangePatient As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
