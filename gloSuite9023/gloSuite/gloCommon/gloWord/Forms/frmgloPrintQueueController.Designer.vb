﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmgloPrintQueueController
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmgloPrintQueueController))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblPageNoOfDocument = New System.Windows.Forms.Label()
        Me.lblCopies = New System.Windows.Forms.Label()
        Me.panel3 = New System.Windows.Forms.Panel()
        Me.btnRestart = New System.Windows.Forms.Button()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblPages = New System.Windows.Forms.Label()
        Me.pbDocument = New System.Windows.Forms.ProgressBar()
        Me.lblPrinterNameValue = New System.Windows.Forms.Label()
        Me.lblPrinterName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.panel3.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Silver
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(1, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(339, 1)
        Me.Label5.TabIndex = 24
        '
        'lblPageNoOfDocument
        '
        Me.lblPageNoOfDocument.AutoSize = True
        Me.lblPageNoOfDocument.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblPageNoOfDocument.Location = New System.Drawing.Point(328, 10)
        Me.lblPageNoOfDocument.Name = "lblPageNoOfDocument"
        Me.lblPageNoOfDocument.Size = New System.Drawing.Size(0, 13)
        Me.lblPageNoOfDocument.TabIndex = 4
        '
        'lblCopies
        '
        Me.lblCopies.AutoEllipsis = True
        Me.lblCopies.AutoSize = True
        Me.lblCopies.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblCopies.Location = New System.Drawing.Point(328, 10)
        Me.lblCopies.Name = "lblCopies"
        Me.lblCopies.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblCopies.Size = New System.Drawing.Size(3, 13)
        Me.lblCopies.TabIndex = 3
        '
        'panel3
        '
        Me.panel3.Controls.Add(Me.btnRestart)
        Me.panel3.Controls.Add(Me.btnPause)
        Me.panel3.Controls.Add(Me.btnPlay)
        Me.panel3.Controls.Add(Me.btnCancel)
        Me.panel3.Controls.Add(Me.lblPages)
        Me.panel3.Controls.Add(Me.pbDocument)
        Me.panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel3.Location = New System.Drawing.Point(1, 36)
        Me.panel3.Name = "panel3"
        Me.panel3.Padding = New System.Windows.Forms.Padding(10)
        Me.panel3.Size = New System.Drawing.Size(339, 82)
        Me.panel3.TabIndex = 18
        '
        'btnRestart
        '
        Me.btnRestart.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRestart.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnRestart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRestart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestart.Image = CType(resources.GetObject("btnRestart.Image"), System.Drawing.Image)
        Me.btnRestart.Location = New System.Drawing.Point(205, 23)
        Me.btnRestart.Name = "btnRestart"
        Me.btnRestart.Size = New System.Drawing.Size(31, 31)
        Me.btnRestart.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.btnRestart, "Restart")
        Me.btnRestart.UseVisualStyleBackColor = True
        '
        'btnPause
        '
        Me.btnPause.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnPause.Enabled = False
        Me.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPause.Image = CType(resources.GetObject("btnPause.Image"), System.Drawing.Image)
        Me.btnPause.Location = New System.Drawing.Point(236, 23)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(31, 31)
        Me.btnPause.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.btnPause, "Pause")
        Me.btnPause.UseVisualStyleBackColor = True
        '
        'btnPlay
        '
        Me.btnPlay.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnPlay.Enabled = False
        Me.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPlay.Image = CType(resources.GetObject("btnPlay.Image"), System.Drawing.Image)
        Me.btnPlay.Location = New System.Drawing.Point(267, 23)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(31, 31)
        Me.btnPlay.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.btnPlay, "Play")
        Me.btnPlay.UseVisualStyleBackColor = True
        Me.btnPlay.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(298, 23)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(31, 31)
        Me.btnCancel.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btnCancel, "Close")
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblPages
        '
        Me.lblPages.AutoEllipsis = True
        Me.lblPages.AutoSize = True
        Me.lblPages.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPages.Location = New System.Drawing.Point(10, 10)
        Me.lblPages.Name = "lblPages"
        Me.lblPages.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblPages.Size = New System.Drawing.Size(78, 13)
        Me.lblPages.TabIndex = 2
        Me.lblPages.Text = "Please Wait... "
        '
        'pbDocument
        '
        Me.pbDocument.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pbDocument.Location = New System.Drawing.Point(10, 54)
        Me.pbDocument.Name = "pbDocument"
        Me.pbDocument.Size = New System.Drawing.Size(319, 18)
        Me.pbDocument.TabIndex = 9
        '
        'lblPrinterNameValue
        '
        Me.lblPrinterNameValue.AutoEllipsis = True
        Me.lblPrinterNameValue.AutoSize = True
        Me.lblPrinterNameValue.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPrinterNameValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrinterNameValue.Location = New System.Drawing.Point(74, 10)
        Me.lblPrinterNameValue.Name = "lblPrinterNameValue"
        Me.lblPrinterNameValue.Size = New System.Drawing.Size(77, 13)
        Me.lblPrinterNameValue.TabIndex = 8
        Me.lblPrinterNameValue.Text = "printer name"
        '
        'lblPrinterName
        '
        Me.lblPrinterName.AutoSize = True
        Me.lblPrinterName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPrinterName.Location = New System.Drawing.Point(10, 10)
        Me.lblPrinterName.Name = "lblPrinterName"
        Me.lblPrinterName.Size = New System.Drawing.Size(64, 13)
        Me.lblPrinterName.TabIndex = 7
        Me.lblPrinterName.Text = "Printing To :"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Silver
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label1.Location = New System.Drawing.Point(340, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 82)
        Me.Label1.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Silver
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 82)
        Me.Label2.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Silver
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(0, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(341, 1)
        Me.Label3.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Silver
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(0, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(341, 1)
        Me.Label4.TabIndex = 23
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.lblPageNoOfDocument)
        Me.panel1.Controls.Add(Me.lblCopies)
        Me.panel1.Controls.Add(Me.lblPrinterNameValue)
        Me.panel1.Controls.Add(Me.lblPrinterName)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel1.Location = New System.Drawing.Point(0, 0)
        Me.panel1.Name = "panel1"
        Me.panel1.Padding = New System.Windows.Forms.Padding(10)
        Me.panel1.Size = New System.Drawing.Size(341, 35)
        Me.panel1.TabIndex = 19
        '
        'frmgloPrintQueueController
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(341, 119)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.panel3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximumSize = New System.Drawing.Size(347, 147)
        Me.MinimumSize = New System.Drawing.Size(347, 147)
        Me.Name = "frmgloPrintQueueController"
        Me.Text = "Print Job Progress"
        Me.panel3.ResumeLayout(False)
        Me.panel3.PerformLayout()
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblPageNoOfDocument As System.Windows.Forms.Label
    Private WithEvents lblCopies As System.Windows.Forms.Label
    Private WithEvents panel3 As System.Windows.Forms.Panel
    Private WithEvents btnRestart As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents btnPause As System.Windows.Forms.Button
    Private WithEvents btnPlay As System.Windows.Forms.Button
    Private WithEvents btnCancel As System.Windows.Forms.Button
    Private WithEvents lblPages As System.Windows.Forms.Label
    Private WithEvents pbDocument As System.Windows.Forms.ProgressBar
    Private WithEvents lblPrinterNameValue As System.Windows.Forms.Label
    Private WithEvents lblPrinterName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents panel1 As System.Windows.Forms.Panel
End Class
