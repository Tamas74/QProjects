<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_PastWordNotes
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
                Try

                    If (IsNothing(dgExams) = False) Then
                        dgExams.TableStyles.Clear()
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dgExams)
                        dgExams.Dispose()
                        dgExams = Nothing
                    End If
                Catch ex As Exception

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUC_PastWordNotes))
        Me.pnlExam1 = New System.Windows.Forms.Panel()
        Me.pnlPastExamView = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.wdPastMessages = New AxDSOFramer.AxFramerControl()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.dgExams = New gloUserControlLibrary.clsDataGrid()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.pnlExam1.SuspendLayout()
        Me.pnlPastExamView.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.wdPastMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.dgExams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlExam1
        '
        Me.pnlExam1.Controls.Add(Me.pnlPastExamView)
        Me.pnlExam1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExam1.Location = New System.Drawing.Point(0, 0)
        Me.pnlExam1.Name = "pnlExam1"
        Me.pnlExam1.Size = New System.Drawing.Size(174, 334)
        Me.pnlExam1.TabIndex = 0
        '
        'pnlPastExamView
        '
        Me.pnlPastExamView.BackColor = System.Drawing.Color.Transparent
        Me.pnlPastExamView.Controls.Add(Me.Panel3)
        Me.pnlPastExamView.Controls.Add(Me.Splitter1)
        Me.pnlPastExamView.Controls.Add(Me.Panel2)
        Me.pnlPastExamView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPastExamView.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPastExamView.Location = New System.Drawing.Point(0, 0)
        Me.pnlPastExamView.Name = "pnlPastExamView"
        Me.pnlPastExamView.Size = New System.Drawing.Size(174, 334)
        Me.pnlPastExamView.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.wdPastMessages)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 173)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(174, 161)
        Me.Panel3.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(1, 160)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(172, 1)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 160)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "label4"
        '
        'wdPastMessages
        '
        Me.wdPastMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPastMessages.Enabled = True
        Me.wdPastMessages.Location = New System.Drawing.Point(0, 1)
        Me.wdPastMessages.Name = "wdPastMessages"
        Me.wdPastMessages.OcxState = CType(resources.GetObject("wdPastMessages.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPastMessages.Size = New System.Drawing.Size(173, 160)
        Me.wdPastMessages.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(173, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 160)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "label3"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(174, 1)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 170)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(174, 3)
        Me.Splitter1.TabIndex = 42
        Me.Splitter1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.dgExams)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(174, 170)
        Me.Panel2.TabIndex = 0
        '
        'dgExams
        '
        Me.dgExams.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgExams.BackgroundColor = System.Drawing.Color.White
        Me.dgExams.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgExams.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgExams.CaptionForeColor = System.Drawing.Color.White
        Me.dgExams.CaptionVisible = False
        Me.dgExams.DataMember = ""
        Me.dgExams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgExams.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgExams.FullRowSelect = True
        Me.dgExams.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgExams.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgExams.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgExams.HeaderForeColor = System.Drawing.Color.White
        Me.dgExams.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.dgExams.Location = New System.Drawing.Point(1, 1)
        Me.dgExams.Name = "dgExams"
        Me.dgExams.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgExams.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgExams.ReadOnly = True
        Me.dgExams.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.dgExams.SelectionForeColor = System.Drawing.Color.Black
        Me.dgExams.Size = New System.Drawing.Size(172, 168)
        Me.dgExams.TabIndex = 13
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(1, 169)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(172, 1)
        Me.Label16.TabIndex = 12
        Me.Label16.Text = "label2"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 169)
        Me.Label17.TabIndex = 11
        Me.Label17.Text = "label4"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel1)
        Me.Panel4.Location = New System.Drawing.Point(48, 40)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(54, 28)
        Me.Panel4.TabIndex = 41
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.lblName)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(54, 25)
        Me.Panel1.TabIndex = 19
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(1, 24)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(52, 1)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "label2"
        '
        'lblName
        '
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.White
        Me.lblName.Location = New System.Drawing.Point(1, 1)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(52, 24)
        Me.lblName.TabIndex = 1
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 1)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 24)
        Me.Label21.TabIndex = 7
        Me.Label21.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(53, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 24)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "label3"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(54, 1)
        Me.Label23.TabIndex = 5
        Me.Label23.Text = "label1"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(173, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 169)
        Me.Label18.TabIndex = 10
        Me.Label18.Text = "label3"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(174, 1)
        Me.Label19.TabIndex = 9
        Me.Label19.Text = "label1"
        '
        'gloUC_PastWordNotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnlExam1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloUC_PastWordNotes"
        Me.Size = New System.Drawing.Size(174, 334)
        Me.pnlExam1.ResumeLayout(False)
        Me.pnlPastExamView.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.wdPastMessages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.dgExams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlExam1 As System.Windows.Forms.Panel
    Friend WithEvents pnlPastExamView As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents wdPastMessages As AxDSOFramer.AxFramerControl
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dgExams As gloUserControlLibrary.clsDataGrid
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label

End Class
