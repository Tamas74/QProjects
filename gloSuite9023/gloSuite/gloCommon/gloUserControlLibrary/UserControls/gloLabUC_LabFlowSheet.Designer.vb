<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloLabUC_LabFlowSheet
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

                Try
                    If (IsNothing(dtpToDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpToDate)
                        Catch ex As Exception

                        End Try


                        dtpToDate.Dispose()
                        dtpToDate = Nothing
                    End If
                Catch
                End Try

                Try
                    If (IsNothing(dtpFromDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpFromDate)
                        Catch ex As Exception

                        End Try


                        dtpFromDate.Dispose()
                        dtpFromDate = Nothing
                    End If
                Catch
                End Try

                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                components.Dispose()
            End If
            If (IsNothing(_dtOrderIDDataTable) = False) Then
                _dtOrderIDDataTable.Dispose()
                _dtOrderIDDataTable = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloLabUC_LabFlowSheet))
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkRange = New System.Windows.Forms.CheckBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.pnlDate = New System.Windows.Forms.Panel()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblTodate = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.ChkSelectAll = New System.Windows.Forms.CheckBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.pnlControl = New System.Windows.Forms.Panel()
        Me.ImgResult_Comment = New System.Windows.Forms.PictureBox()
        Me.ImgAttachment = New System.Windows.Forms.PictureBox()
        Me.ImgResultHeader = New System.Windows.Forms.PictureBox()
        Me.ImgResult = New System.Windows.Forms.PictureBox()
        Me.ImgTest = New System.Windows.Forms.PictureBox()
        Me.ImgResultHeader_Flask = New System.Windows.Forms.PictureBox()
        Me.ImgResult_Flask = New System.Windows.Forms.PictureBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.c1LabFlowSheet = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.tpPrintLabFlowSheet = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlDate.SuspendLayout()
        Me.pnlControl.SuspendLayout()
        CType(Me.ImgResult_Comment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgAttachment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgResultHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgTest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgResultHeader_Flask, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgResult_Flask, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.c1LabFlowSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.Panel4)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlHeader.Size = New System.Drawing.Size(865, 30)
        Me.pnlHeader.TabIndex = 21
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.chkRange)
        Me.Panel4.Controls.Add(Me.btnPrint)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.pnlDate)
        Me.Panel4.Controls.Add(Me.ChkSelectAll)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(865, 27)
        Me.Panel4.TabIndex = 8
        '
        'chkRange
        '
        Me.chkRange.AutoSize = True
        Me.chkRange.BackColor = System.Drawing.Color.Transparent
        Me.chkRange.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkRange.Location = New System.Drawing.Point(483, 5)
        Me.chkRange.Name = "chkRange"
        Me.chkRange.Size = New System.Drawing.Size(175, 18)
        Me.chkRange.TabIndex = 48
        Me.chkRange.Text = "Include result normal range"
        Me.chkRange.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.Transparent
        Me.btnPrint.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnPrint.FlatAppearance.BorderSize = 0
        Me.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.Location = New System.Drawing.Point(840, 1)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(24, 25)
        Me.btnPrint.TabIndex = 47
        Me.btnPrint.UseVisualStyleBackColor = False
        Me.btnPrint.Visible = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Location = New System.Drawing.Point(864, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 25)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "label3"
        '
        'pnlDate
        '
        Me.pnlDate.BackColor = System.Drawing.Color.Transparent
        Me.pnlDate.Controls.Add(Me.dtpToDate)
        Me.pnlDate.Controls.Add(Me.lblTodate)
        Me.pnlDate.Controls.Add(Me.dtpFromDate)
        Me.pnlDate.Controls.Add(Me.lblFromDate)
        Me.pnlDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlDate.Location = New System.Drawing.Point(90, 1)
        Me.pnlDate.Name = "pnlDate"
        Me.pnlDate.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlDate.Size = New System.Drawing.Size(371, 25)
        Me.pnlDate.TabIndex = 11
        '
        'dtpToDate
        '
        Me.dtpToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(274, 3)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpToDate.TabIndex = 13
        '
        'lblTodate
        '
        Me.lblTodate.AutoSize = True
        Me.lblTodate.BackColor = System.Drawing.Color.Transparent
        Me.lblTodate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTodate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTodate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblTodate.Location = New System.Drawing.Point(244, 3)
        Me.lblTodate.Name = "lblTodate"
        Me.lblTodate.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.lblTodate.Size = New System.Drawing.Size(30, 17)
        Me.lblTodate.TabIndex = 12
        Me.lblTodate.Text = "To :"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDate.Location = New System.Drawing.Point(153, 3)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(91, 20)
        Me.dtpFromDate.TabIndex = 11
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.BackColor = System.Drawing.Color.Transparent
        Me.lblFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFromDate.Location = New System.Drawing.Point(0, 3)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Padding = New System.Windows.Forms.Padding(13, 3, 0, 0)
        Me.lblFromDate.Size = New System.Drawing.Size(153, 17)
        Me.lblFromDate.TabIndex = 10
        Me.lblFromDate.Text = "Specimen Date From :"
        '
        'ChkSelectAll
        '
        Me.ChkSelectAll.AutoSize = True
        Me.ChkSelectAll.BackColor = System.Drawing.Color.Transparent
        Me.ChkSelectAll.Dock = System.Windows.Forms.DockStyle.Left
        Me.ChkSelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ChkSelectAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ChkSelectAll.Location = New System.Drawing.Point(1, 1)
        Me.ChkSelectAll.Name = "ChkSelectAll"
        Me.ChkSelectAll.Padding = New System.Windows.Forms.Padding(7, 3, 0, 0)
        Me.ChkSelectAll.Size = New System.Drawing.Size(89, 25)
        Me.ChkSelectAll.TabIndex = 50
        Me.ChkSelectAll.Text = "Select All"
        Me.ChkSelectAll.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(1, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(864, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label1"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(1, 26)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(864, 1)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 27)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "label4"
        '
        'pnlControl
        '
        Me.pnlControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlControl.Controls.Add(Me.ImgResult_Comment)
        Me.pnlControl.Controls.Add(Me.ImgAttachment)
        Me.pnlControl.Controls.Add(Me.ImgResultHeader)
        Me.pnlControl.Controls.Add(Me.ImgResult)
        Me.pnlControl.Controls.Add(Me.ImgTest)
        Me.pnlControl.Controls.Add(Me.ImgResultHeader_Flask)
        Me.pnlControl.Controls.Add(Me.ImgResult_Flask)
        Me.pnlControl.Controls.Add(Me.label4)
        Me.pnlControl.Controls.Add(Me.label3)
        Me.pnlControl.Controls.Add(Me.label2)
        Me.pnlControl.Controls.Add(Me.label1)
        Me.pnlControl.Controls.Add(Me.c1LabFlowSheet)
        Me.pnlControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlControl.Location = New System.Drawing.Point(0, 30)
        Me.pnlControl.Name = "pnlControl"
        Me.pnlControl.Size = New System.Drawing.Size(865, 599)
        Me.pnlControl.TabIndex = 22
        '
        'ImgResult_Comment
        '
        Me.ImgResult_Comment.BackColor = System.Drawing.Color.Transparent
        Me.ImgResult_Comment.Image = CType(resources.GetObject("ImgResult_Comment.Image"), System.Drawing.Image)
        Me.ImgResult_Comment.Location = New System.Drawing.Point(766, 429)
        Me.ImgResult_Comment.Name = "ImgResult_Comment"
        Me.ImgResult_Comment.Size = New System.Drawing.Size(26, 20)
        Me.ImgResult_Comment.TabIndex = 48
        Me.ImgResult_Comment.TabStop = False
        Me.ImgResult_Comment.Visible = False
        '
        'ImgAttachment
        '
        Me.ImgAttachment.BackColor = System.Drawing.Color.Transparent
        Me.ImgAttachment.Image = CType(resources.GetObject("ImgAttachment.Image"), System.Drawing.Image)
        Me.ImgAttachment.Location = New System.Drawing.Point(766, 385)
        Me.ImgAttachment.Name = "ImgAttachment"
        Me.ImgAttachment.Size = New System.Drawing.Size(21, 24)
        Me.ImgAttachment.TabIndex = 47
        Me.ImgAttachment.TabStop = False
        Me.ImgAttachment.Visible = False
        '
        'ImgResultHeader
        '
        Me.ImgResultHeader.BackColor = System.Drawing.Color.Transparent
        Me.ImgResultHeader.Image = CType(resources.GetObject("ImgResultHeader.Image"), System.Drawing.Image)
        Me.ImgResultHeader.Location = New System.Drawing.Point(766, 536)
        Me.ImgResultHeader.Name = "ImgResultHeader"
        Me.ImgResultHeader.Size = New System.Drawing.Size(23, 20)
        Me.ImgResultHeader.TabIndex = 34
        Me.ImgResultHeader.TabStop = False
        Me.ImgResultHeader.Visible = False
        '
        'ImgResult
        '
        Me.ImgResult.BackColor = System.Drawing.Color.Transparent
        Me.ImgResult.Image = CType(resources.GetObject("ImgResult.Image"), System.Drawing.Image)
        Me.ImgResult.Location = New System.Drawing.Point(766, 497)
        Me.ImgResult.Name = "ImgResult"
        Me.ImgResult.Size = New System.Drawing.Size(23, 20)
        Me.ImgResult.TabIndex = 33
        Me.ImgResult.TabStop = False
        Me.ImgResult.Visible = False
        '
        'ImgTest
        '
        Me.ImgTest.BackColor = System.Drawing.Color.Transparent
        Me.ImgTest.Image = CType(resources.GetObject("ImgTest.Image"), System.Drawing.Image)
        Me.ImgTest.Location = New System.Drawing.Point(766, 467)
        Me.ImgTest.Name = "ImgTest"
        Me.ImgTest.Size = New System.Drawing.Size(21, 24)
        Me.ImgTest.TabIndex = 31
        Me.ImgTest.TabStop = False
        Me.ImgTest.Visible = False
        '
        'ImgResultHeader_Flask
        '
        Me.ImgResultHeader_Flask.BackColor = System.Drawing.Color.Transparent
        Me.ImgResultHeader_Flask.Image = CType(resources.GetObject("ImgResultHeader_Flask.Image"), System.Drawing.Image)
        Me.ImgResultHeader_Flask.Location = New System.Drawing.Point(422, 287)
        Me.ImgResultHeader_Flask.Name = "ImgResultHeader_Flask"
        Me.ImgResultHeader_Flask.Size = New System.Drawing.Size(21, 24)
        Me.ImgResultHeader_Flask.TabIndex = 51
        Me.ImgResultHeader_Flask.TabStop = False
        Me.ImgResultHeader_Flask.Visible = False
        '
        'ImgResult_Flask
        '
        Me.ImgResult_Flask.BackColor = System.Drawing.Color.Transparent
        Me.ImgResult_Flask.Image = CType(resources.GetObject("ImgResult_Flask.Image"), System.Drawing.Image)
        Me.ImgResult_Flask.Location = New System.Drawing.Point(430, 295)
        Me.ImgResult_Flask.Name = "ImgResult_Flask"
        Me.ImgResult_Flask.Size = New System.Drawing.Size(21, 24)
        Me.ImgResult_Flask.TabIndex = 52
        Me.ImgResult_Flask.TabStop = False
        Me.ImgResult_Flask.Visible = False
        '
        'label4
        '
        Me.label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.label4.Location = New System.Drawing.Point(1, 0)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(863, 1)
        Me.label4.TabIndex = 12
        Me.label4.Text = "label1"
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.label3.Location = New System.Drawing.Point(864, 0)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(1, 598)
        Me.label3.TabIndex = 11
        Me.label3.Text = "label3"
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.label2.Location = New System.Drawing.Point(0, 0)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(1, 598)
        Me.label2.TabIndex = 10
        Me.label2.Text = "label4"
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label1.Location = New System.Drawing.Point(0, 598)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(865, 1)
        Me.label1.TabIndex = 9
        Me.label1.Text = "label2"
        '
        'c1LabFlowSheet
        '
        Me.c1LabFlowSheet.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1LabFlowSheet.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1LabFlowSheet.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1LabFlowSheet.ColumnInfo = "10,1,0,0,0,95,Columns:0{Visible:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.c1LabFlowSheet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1LabFlowSheet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1LabFlowSheet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1LabFlowSheet.Location = New System.Drawing.Point(0, 0)
        Me.c1LabFlowSheet.Name = "c1LabFlowSheet"
        Me.c1LabFlowSheet.Rows.DefaultSize = 19
        Me.c1LabFlowSheet.Size = New System.Drawing.Size(865, 599)
        Me.c1LabFlowSheet.StyleInfo = resources.GetString("c1LabFlowSheet.StyleInfo")
        Me.c1LabFlowSheet.TabIndex = 8
        '
        'gloLabUC_LabFlowSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlControl)
        Me.Controls.Add(Me.pnlHeader)
        Me.Name = "gloLabUC_LabFlowSheet"
        Me.Size = New System.Drawing.Size(865, 629)
        Me.pnlHeader.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.pnlDate.ResumeLayout(False)
        Me.pnlDate.PerformLayout()
        Me.pnlControl.ResumeLayout(False)
        CType(Me.ImgResult_Comment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgAttachment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgResultHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgTest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgResultHeader_Flask, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgResult_Flask, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.c1LabFlowSheet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Private WithEvents pnlDate As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents pnlControl As System.Windows.Forms.Panel
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents c1LabFlowSheet As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ImgResultHeader As System.Windows.Forms.PictureBox
    Friend WithEvents ImgResult As System.Windows.Forms.PictureBox
    Friend WithEvents ImgTest As System.Windows.Forms.PictureBox
    Friend WithEvents chkRange As System.Windows.Forms.CheckBox
    Public WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Public WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents tpPrintLabFlowSheet As System.Windows.Forms.ToolTip
    Friend WithEvents ChkSelectAll As System.Windows.Forms.CheckBox
    Private WithEvents lblTodate As System.Windows.Forms.Label
    Private WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents ImgResult_Comment As System.Windows.Forms.PictureBox
    Friend WithEvents ImgAttachment As System.Windows.Forms.PictureBox
    Friend WithEvents ImgResultHeader_Flask As System.Windows.Forms.PictureBox
    Friend WithEvents ImgResult_Flask As System.Windows.Forms.PictureBox

End Class
