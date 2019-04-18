<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClinicalChart
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

                Try
                    'Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpicEndDate, dtpicStartDate}
                    'Dim cntControls() As System.Windows.Forms.Control = {dtpicEndDate, dtpicStartDate}

                    components.Dispose()
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                    Catch
                    End Try


                    'If (IsNothing(dtpControls) = False) Then
                    '    If dtpControls.Length > 0 Then
                    '        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    '    End If
                    'End If


                    'If (IsNothing(cntControls) = False) Then
                    '    If cntControls.Length > 0 Then
                    '        gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                    '    End If
                    'End If

                Catch
                End Try
              
                Try
                    If (IsNothing(SaveFileDialogXML) = False) Then
                        SaveFileDialogXML.Dispose()
                        SaveFileDialogXML = Nothing
                    End If
                Catch

                End Try

                Try
                    If IsNothing(_PatientStrip) = False Then
                        _PatientStrip.Dispose()
                        _PatientStrip = Nothing
                    End If
                Catch
                End Try

                ' PrintDialog1 Clean up
                Try

                    If Not IsNothing(PrintDialog1) Then
                        PrintDialog1.Dispose()
                        PrintDialog1 = Nothing
                    End If

                Catch
                End Try

                ' PrintDocument1 Clean up
                Try

                    If Not IsNothing(printDocument1) Then
                        Try

                            'printDocument1.BeginPrint() REM -= new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
                            'printDocument1.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(printDocument1)
                        Catch

                        End Try

                        printDocument1.Dispose()
                        printDocument1 = Nothing

                    End If

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClinicalChart))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lblTo = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SaveFileDialogXML = New System.Windows.Forms.SaveFileDialog()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.tlbbtn_DeelectAll = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_SelectAll = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_Show = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Print_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_FAX_32 = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Export = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_SendSS = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Queue = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.printDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.C1ClinicalChart = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.mskDOS = New System.Windows.Forms.MaskedTextBox()
        Me.mskEndDate = New System.Windows.Forms.MaskedTextBox()
        Me.mskStartDate = New System.Windows.Forms.MaskedTextBox()
        Me.pnlClaimPrinter = New System.Windows.Forms.Panel()
        Me.cmbClaimPrinter = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.rbPrintData = New System.Windows.Forms.RadioButton()
        Me.rbPrintOnForm = New System.Windows.Forms.RadioButton()
        Me.pnlProgress = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.prgGeneratefile = New System.Windows.Forms.ProgressBar()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.C1ClinicalChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnlClaimPrinter.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlProgress.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(107, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Start Date "
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1534, 1)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "label4"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Comment.ico")
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.Color.Transparent
        Me.lblTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(262, 34)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(62, 14)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "End Date "
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1534, 1)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1538, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 58)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "label4"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 58)
        Me.lbl_LeftBrd.TabIndex = 4
        Me.lbl_LeftBrd.Text = "label4"
        '
        'tlbbtn_DeelectAll
        '
        Me.tlbbtn_DeelectAll.Image = CType(resources.GetObject("tlbbtn_DeelectAll.Image"), System.Drawing.Image)
        Me.tlbbtn_DeelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_DeelectAll.Name = "tlbbtn_DeelectAll"
        Me.tlbbtn_DeelectAll.Size = New System.Drawing.Size(81, 50)
        Me.tlbbtn_DeelectAll.Tag = "DeSelectAll"
        Me.tlbbtn_DeelectAll.Text = "&Deselect All"
        Me.tlbbtn_DeelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_SelectAll
        '
        Me.tlbbtn_SelectAll.Image = CType(resources.GetObject("tlbbtn_SelectAll.Image"), System.Drawing.Image)
        Me.tlbbtn_SelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_SelectAll.Name = "tlbbtn_SelectAll"
        Me.tlbbtn_SelectAll.Size = New System.Drawing.Size(67, 50)
        Me.tlbbtn_SelectAll.Tag = "SelectAll"
        Me.tlbbtn_SelectAll.Text = "&Select All"
        Me.tlbbtn_SelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_Show, Me.tblbtn_Print_32, Me.tblbtn_FAX_32, Me.tlbbtn_Export, Me.tlbbtn_SelectAll, Me.tlbbtn_DeelectAll, Me.tlbbtn_SendSS, Me.tblbtn_Queue, Me.tlbbtn_Close})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1536, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "toolStrip1"
        '
        'tlbbtn_Show
        '
        Me.tlbbtn_Show.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_Show.Image = Global.gloEMR.My.Resources.Resources.Show
        Me.tlbbtn_Show.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Show.Margin = New System.Windows.Forms.Padding(1)
        Me.tlbbtn_Show.Name = "tlbbtn_Show"
        Me.tlbbtn_Show.Size = New System.Drawing.Size(46, 50)
        Me.tlbbtn_Show.Tag = "Show"
        Me.tlbbtn_Show.Text = "S&how"
        Me.tlbbtn_Show.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Print_32
        '
        Me.tblbtn_Print_32.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_Print_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtn_Print_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Print_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Print_32.Image = CType(resources.GetObject("tblbtn_Print_32.Image"), System.Drawing.Image)
        Me.tblbtn_Print_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Print_32.Margin = New System.Windows.Forms.Padding(1)
        Me.tblbtn_Print_32.Name = "tblbtn_Print_32"
        Me.tblbtn_Print_32.Size = New System.Drawing.Size(45, 50)
        Me.tblbtn_Print_32.Tag = "Print"
        Me.tblbtn_Print_32.Text = "&Print "
        Me.tblbtn_Print_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Print_32.ToolTipText = "Print"
        '
        'tblbtn_FAX_32
        '
        Me.tblbtn_FAX_32.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_FAX_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtn_FAX_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_FAX_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_FAX_32.Image = CType(resources.GetObject("tblbtn_FAX_32.Image"), System.Drawing.Image)
        Me.tblbtn_FAX_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_FAX_32.Margin = New System.Windows.Forms.Padding(1)
        Me.tblbtn_FAX_32.Name = "tblbtn_FAX_32"
        Me.tblbtn_FAX_32.Size = New System.Drawing.Size(39, 50)
        Me.tblbtn_FAX_32.Tag = "FAX"
        Me.tblbtn_FAX_32.Text = " &Fax "
        Me.tblbtn_FAX_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_FAX_32.ToolTipText = "Fax"
        '
        'tlbbtn_Export
        '
        Me.tlbbtn_Export.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Export.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Export.Image = CType(resources.GetObject("tlbbtn_Export.Image"), System.Drawing.Image)
        Me.tlbbtn_Export.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Export.Name = "tlbbtn_Export"
        Me.tlbbtn_Export.Size = New System.Drawing.Size(94, 50)
        Me.tlbbtn_Export.Tag = "Export"
        Me.tlbbtn_Export.Text = "&Export To File"
        Me.tlbbtn_Export.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_SendSS
        '
        Me.tlbbtn_SendSS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_SendSS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_SendSS.Image = CType(resources.GetObject("tlbbtn_SendSS.Image"), System.Drawing.Image)
        Me.tlbbtn_SendSS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_SendSS.Name = "tlbbtn_SendSS"
        Me.tlbbtn_SendSS.Size = New System.Drawing.Size(167, 50)
        Me.tlbbtn_SendSS.Tag = "SendSS"
        Me.tlbbtn_SendSS.Text = "Provider D&IRECT Message"
        Me.tlbbtn_SendSS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_SendSS.ToolTipText = "Provider DIRECT Message"
        '
        'tblbtn_Queue
        '
        Me.tblbtn_Queue.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_Queue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtn_Queue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Queue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Queue.Image = CType(resources.GetObject("tblbtn_Queue.Image"), System.Drawing.Image)
        Me.tblbtn_Queue.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Queue.Margin = New System.Windows.Forms.Padding(1)
        Me.tblbtn_Queue.Name = "tblbtn_Queue"
        Me.tblbtn_Queue.Size = New System.Drawing.Size(50, 50)
        Me.tblbtn_Queue.Tag = "Queue"
        Me.tblbtn_Queue.Text = "&Queue"
        Me.tblbtn_Queue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Queue.ToolTipText = "Queue"
        '
        'tlbbtn_Close
        '
        Me.tlbbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Close.Image = CType(resources.GetObject("tlbbtn_Close.Image"), System.Drawing.Image)
        Me.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Close.Name = "tlbbtn_Close"
        Me.tlbbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtn_Close.Tag = "Close"
        Me.tlbbtn_Close.Text = "&Close"
        Me.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.C1ClinicalChart)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(0, 64)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(1542, 480)
        Me.Panel3.TabIndex = 109
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1534, 1)
        Me.Label10.TabIndex = 114
        Me.Label10.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 476)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1534, 1)
        Me.Label9.TabIndex = 113
        Me.Label9.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(1538, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 477)
        Me.Label8.TabIndex = 112
        Me.Label8.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 477)
        Me.Label7.TabIndex = 111
        Me.Label7.Text = "label4"
        '
        'C1ClinicalChart
        '
        Me.C1ClinicalChart.BackColor = System.Drawing.Color.GhostWhite
        Me.C1ClinicalChart.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1ClinicalChart.ColumnInfo = "1,0,0,0,0,105,Columns:"
        Me.C1ClinicalChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1ClinicalChart.ExtendLastCol = True
        Me.C1ClinicalChart.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1ClinicalChart.Location = New System.Drawing.Point(3, 0)
        Me.C1ClinicalChart.Name = "C1ClinicalChart"
        Me.C1ClinicalChart.Rows.Count = 1
        Me.C1ClinicalChart.Rows.DefaultSize = 21
        Me.C1ClinicalChart.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1ClinicalChart.Size = New System.Drawing.Size(1536, 477)
        Me.C1ClinicalChart.StyleInfo = resources.GetString("C1ClinicalChart.StyleInfo")
        Me.C1ClinicalChart.TabIndex = 106
        '
        'Panel2
        '
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.mskDOS)
        Me.Panel2.Controls.Add(Me.mskEndDate)
        Me.Panel2.Controls.Add(Me.mskStartDate)
        Me.Panel2.Controls.Add(Me.pnlClaimPrinter)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.pnlProgress)
        Me.Panel2.Controls.Add(Me.CheckBox1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.lblTo)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(1542, 64)
        Me.Panel2.TabIndex = 109
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(410, 34)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(31, 14)
        Me.Label13.TabIndex = 20
        Me.Label13.Text = "DOS"
        '
        'mskDOS
        '
        Me.mskDOS.Location = New System.Drawing.Point(443, 30)
        Me.mskDOS.Mask = "00/00/0000"
        Me.mskDOS.Name = "mskDOS"
        Me.mskDOS.Size = New System.Drawing.Size(82, 22)
        Me.mskDOS.TabIndex = 19
        Me.mskDOS.ValidatingType = GetType(Date)
        '
        'mskEndDate
        '
        Me.mskEndDate.Location = New System.Drawing.Point(321, 30)
        Me.mskEndDate.Mask = "00/00/0000"
        Me.mskEndDate.Name = "mskEndDate"
        Me.mskEndDate.Size = New System.Drawing.Size(82, 22)
        Me.mskEndDate.TabIndex = 18
        Me.mskEndDate.ValidatingType = GetType(Date)
        '
        'mskStartDate
        '
        Me.mskStartDate.Location = New System.Drawing.Point(172, 30)
        Me.mskStartDate.Mask = "00/00/0000"
        Me.mskStartDate.Name = "mskStartDate"
        Me.mskStartDate.Size = New System.Drawing.Size(82, 22)
        Me.mskStartDate.TabIndex = 17
        Me.mskStartDate.ValidatingType = GetType(Date)
        '
        'pnlClaimPrinter
        '
        Me.pnlClaimPrinter.Controls.Add(Me.cmbClaimPrinter)
        Me.pnlClaimPrinter.Controls.Add(Me.Label12)
        Me.pnlClaimPrinter.Location = New System.Drawing.Point(833, 26)
        Me.pnlClaimPrinter.Name = "pnlClaimPrinter"
        Me.pnlClaimPrinter.Size = New System.Drawing.Size(440, 26)
        Me.pnlClaimPrinter.TabIndex = 16
        '
        'cmbClaimPrinter
        '
        Me.cmbClaimPrinter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbClaimPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClaimPrinter.FormattingEnabled = True
        Me.cmbClaimPrinter.Location = New System.Drawing.Point(161, 1)
        Me.cmbClaimPrinter.Name = "cmbClaimPrinter"
        Me.cmbClaimPrinter.Size = New System.Drawing.Size(275, 22)
        Me.cmbClaimPrinter.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(2, 5)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(158, 14)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Claim Printer Configuration :"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Controls.Add(Me.rbPrintData)
        Me.Panel5.Controls.Add(Me.rbPrintOnForm)
        Me.Panel5.Location = New System.Drawing.Point(531, 26)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(299, 26)
        Me.Panel5.TabIndex = 15
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(1, 6)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(71, 14)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Print Claim :"
        '
        'rbPrintData
        '
        Me.rbPrintData.AutoSize = True
        Me.rbPrintData.Location = New System.Drawing.Point(188, 4)
        Me.rbPrintData.Name = "rbPrintData"
        Me.rbPrintData.Size = New System.Drawing.Size(109, 18)
        Me.rbPrintData.TabIndex = 3
        Me.rbPrintData.Text = "Only Claim Data"
        Me.rbPrintData.UseVisualStyleBackColor = True
        '
        'rbPrintOnForm
        '
        Me.rbPrintOnForm.AutoSize = True
        Me.rbPrintOnForm.Checked = True
        Me.rbPrintOnForm.Location = New System.Drawing.Point(74, 4)
        Me.rbPrintOnForm.Name = "rbPrintOnForm"
        Me.rbPrintOnForm.Size = New System.Drawing.Size(113, 18)
        Me.rbPrintOnForm.TabIndex = 2
        Me.rbPrintOnForm.TabStop = True
        Me.rbPrintOnForm.Text = "With Claim Form"
        Me.rbPrintOnForm.UseVisualStyleBackColor = True
        '
        'pnlProgress
        '
        Me.pnlProgress.Controls.Add(Me.lblStatus)
        Me.pnlProgress.Controls.Add(Me.prgGeneratefile)
        Me.pnlProgress.Location = New System.Drawing.Point(4, 70)
        Me.pnlProgress.Name = "pnlProgress"
        Me.pnlProgress.Size = New System.Drawing.Size(918, 42)
        Me.pnlProgress.TabIndex = 13
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(8, 2)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(589, 18)
        Me.lblStatus.TabIndex = 12
        Me.lblStatus.Text = "Generating file..."
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblStatus.Visible = False
        '
        'prgGeneratefile
        '
        Me.prgGeneratefile.BackColor = System.Drawing.Color.White
        Me.prgGeneratefile.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.prgGeneratefile.ForeColor = System.Drawing.Color.LimeGreen
        Me.prgGeneratefile.Location = New System.Drawing.Point(0, 23)
        Me.prgGeneratefile.Maximum = 250
        Me.prgGeneratefile.Name = "prgGeneratefile"
        Me.prgGeneratefile.Size = New System.Drawing.Size(918, 19)
        Me.prgGeneratefile.Step = 25
        Me.prgGeneratefile.TabIndex = 11
        Me.prgGeneratefile.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(-403, 37)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(82, 18)
        Me.CheckBox1.TabIndex = 10
        Me.CheckBox1.Text = "Select All"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(778, 14)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Select elements of the patient’s chart to print, fax, or export.  Use the start a" & _
    "nd end dates below to limit or expand what may be selected."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 14)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Selection Dates :"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.Controls.Add(Me.ToolStrip1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(1542, 60)
        Me.Panel1.TabIndex = 111
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel3)
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 60)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1542, 544)
        Me.Panel4.TabIndex = 112
        '
        'frmClinicalChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1542, 604)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmClinicalChart"
        Me.Text = "Clinical Chart"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.C1ClinicalChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlClaimPrinter.ResumeLayout(False)
        Me.pnlClaimPrinter.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.pnlProgress.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialogXML As System.Windows.Forms.SaveFileDialog
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Friend WithEvents tlbbtn_DeelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_SelectAll As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlbbtn_Export As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Private WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents tblbtn_Print_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_FAX_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents C1ClinicalChart As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents prgGeneratefile As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlProgress As System.Windows.Forms.Panel
    Friend WithEvents tlbbtn_SendSS As System.Windows.Forms.ToolStripButton
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Private WithEvents printDocument1 As System.Drawing.Printing.PrintDocument
    Private WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents rbPrintData As System.Windows.Forms.RadioButton
    Friend WithEvents rbPrintOnForm As System.Windows.Forms.RadioButton
    Friend WithEvents tblbtn_Queue As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlClaimPrinter As System.Windows.Forms.Panel
    Friend WithEvents cmbClaimPrinter As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents mskEndDate As System.Windows.Forms.MaskedTextBox
    Private WithEvents mskStartDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents tlbbtn_Show As System.Windows.Forms.ToolStripButton
    Private WithEvents mskDOS As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
End Class
