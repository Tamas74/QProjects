Imports System.Data.SqlClient

Public Class frmPwdSettings
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'issue no:1553
    Public Sub New(ByVal _blnSetComplexisityOnSetting As Boolean, ByVal _NoofCapitalLetters As Integer, ByVal _NoofLetters As Integer, ByVal _NoofDigits As Integer, ByVal _NoOfSpecialChars As Integer, ByVal _NoofDays As Integer, ByVal _numMinPSWLenght As Integer)
        MyBase.New()
        InitializeComponent()

        _blnSetComplexityValue = _blnSetComplexisityOnSetting ' this shows set value to textbox but that all not saved to dB

        nNoofCapitalLetters = _NoofCapitalLetters
        nNoofLetters = _NoofLetters
        nNoofDigits = _NoofDigits
        nNoOfSpecialChars = _NoOfSpecialChars
        nNoofDays = _NoofDays
        nPSWNumMinLength = _numMinPSWLenght

    End Sub

    'Form overrides dispose to clean up the component list.

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtnumCapLetters As System.Windows.Forms.TextBox
    Friend WithEvents txtnumLetters As System.Windows.Forms.TextBox
    Friend WithEvents txtnumDigits As System.Windows.Forms.TextBox
    Friend WithEvents txtnumSpChars As System.Windows.Forms.TextBox
    Friend WithEvents txtnumMinLength As System.Windows.Forms.TextBox
    Friend WithEvents txtnumdays As System.Windows.Forms.TextBox
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOk As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPwdSettings))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtnumCapLetters = New System.Windows.Forms.TextBox
        Me.txtnumLetters = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtnumDigits = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtnumSpChars = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtnumMinLength = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtnumdays = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstrip = New System.Windows.Forms.ToolStrip
        Me.btnOk = New System.Windows.Forms.ToolStripButton
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Panel3.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(31, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Minimum No. Of Capital Letters :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtnumCapLetters
        '
        Me.txtnumCapLetters.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnumCapLetters.ForeColor = System.Drawing.Color.Black
        Me.txtnumCapLetters.Location = New System.Drawing.Point(219, 15)
        Me.txtnumCapLetters.Name = "txtnumCapLetters"
        Me.txtnumCapLetters.Size = New System.Drawing.Size(145, 22)
        Me.txtnumCapLetters.TabIndex = 0
        '
        'txtnumLetters
        '
        Me.txtnumLetters.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnumLetters.ForeColor = System.Drawing.Color.Black
        Me.txtnumLetters.Location = New System.Drawing.Point(219, 47)
        Me.txtnumLetters.Name = "txtnumLetters"
        Me.txtnumLetters.Size = New System.Drawing.Size(145, 22)
        Me.txtnumLetters.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(70, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(145, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Minimum No. Of Letters :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtnumDigits
        '
        Me.txtnumDigits.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnumDigits.ForeColor = System.Drawing.Color.Black
        Me.txtnumDigits.Location = New System.Drawing.Point(219, 79)
        Me.txtnumDigits.Name = "txtnumDigits"
        Me.txtnumDigits.Size = New System.Drawing.Size(145, 22)
        Me.txtnumDigits.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(80, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(135, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Minimum No. Of Digits :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtnumSpChars
        '
        Me.txtnumSpChars.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnumSpChars.ForeColor = System.Drawing.Color.Black
        Me.txtnumSpChars.Location = New System.Drawing.Point(219, 111)
        Me.txtnumSpChars.Name = "txtnumSpChars"
        Me.txtnumSpChars.Size = New System.Drawing.Size(145, 22)
        Me.txtnumSpChars.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(29, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(186, 14)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Minimum No. Of Special Letters :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtnumMinLength
        '
        Me.txtnumMinLength.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnumMinLength.ForeColor = System.Drawing.Color.Black
        Me.txtnumMinLength.Location = New System.Drawing.Point(219, 143)
        Me.txtnumMinLength.Name = "txtnumMinLength"
        Me.txtnumMinLength.Size = New System.Drawing.Size(145, 22)
        Me.txtnumMinLength.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(17, 147)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(198, 14)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Minimum Length of the Password :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtnumdays
        '
        Me.txtnumdays.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnumdays.ForeColor = System.Drawing.Color.Black
        Me.txtnumdays.Location = New System.Drawing.Point(219, 175)
        Me.txtnumdays.Name = "txtnumdays"
        Me.txtnumdays.Size = New System.Drawing.Size(145, 22)
        Me.txtnumdays.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(98, 179)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(117, 14)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "TimeFrame in Days :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.txtnumMinLength)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.txtnumSpChars)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.txtnumDigits)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.txtnumLetters)
        Me.Panel3.Controls.Add(Me.txtnumCapLetters)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.txtnumdays)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 56)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel3.Size = New System.Drawing.Size(381, 213)
        Me.Panel3.TabIndex = 15
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(381, 56)
        Me.pnl_tlsp_Top.TabIndex = 18
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOk, Me.btnCancel})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(381, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.Image = CType(resources.GetObject("btnOk.Image"), System.Drawing.Image)
        Me.btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(66, 50)
        Me.btnOk.Text = "&Save&&Cls"
        Me.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOk.ToolTipText = "Save and Close"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.btnCancel.Text = "&Close"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.ToolTipText = "Close"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(4, 209)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(373, 1)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 206)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(377, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 206)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(375, 1)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "label1"
        '
        'frmPwdSettings
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(381, 269)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPwdSettings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Password Settings"
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    'sarika 26th june 07
    Public strSQL As String = ""
    Public blnSetComplexisityOnSetting As Boolean = False
    'issue no :1553
    Public nNoofCapitalLetters As Integer = 0
    Public nNoofLetters As Integer = 0
    Public nNoofDigits As Integer = 0
    Public nNoOfSpecialChars As Integer = 0
    Public nNoofDays As Integer = 0
    Public nPSWNumMinLength As Integer = 1


    '---
    Dim _MinLength As Integer = 0
    Dim _blnSetComplexityValue As Boolean = False

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        strSQL = ""

        '26th Mar

        '  If CInt(Val(txtnumDigits.Text.Trim)) <> 0 Then
        If CInt(Val(txtnumLetters.Text.Trim)) <> 0 Then
            If CInt(Val(txtnumMinLength.Text.Trim)) >= 8 Then
                blnSetComplexisityOnSetting = True
                'issue no:1553
                nNoofCapitalLetters = CInt(Val(Trim(txtnumCapLetters.Text)))
                nNoofLetters = CInt(Val(Trim(txtnumLetters.Text)))
                nNoofDigits = CInt(Val(Trim(txtnumDigits.Text)))
                nNoOfSpecialChars = CInt(Val(Trim(txtnumSpChars.Text)))
                nNoofDays = CInt(Val(Trim(txtnumdays.Text)))
                nPSWNumMinLength = CInt(Val(Trim(txtnumMinLength.Text)))

            End If
        End If
        'End If

        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim cnt As Integer = 0

        ' Dim oDataReader As SqlDataReader
        Dim objfrmSettings As New frmSettings_New

        Try
            'If txtnumDigits.Text.Trim <> "" Then
            '    If CInt(Val(txtnumDigits.Text.Trim)) = 0 Then
            '        MsgBox("The Password complexity requires atleast 1 digit")
            '        txtnumDigits.Text = ""
            '        txtnumDigits.Focus()
            '        Exit Sub
            '    End If
            'Else
            '    MsgBox("The Password complexity requires atleast 1 digit")
            '    txtnumDigits.Text = ""
            '    txtnumDigits.Focus()
            '    Exit Sub
            'End If

            If CInt(Val(txtnumLetters.Text.Trim)) = 0 Then
                'MsgBox("The Password complexity requires atleast 1 letter")
                MessageBox.Show("The password complexity requires atleast 1 letter", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtnumLetters.Text = ""
                txtnumLetters.Focus()
                Exit Sub
            End If

            If (CInt(Val(txtnumCapLetters.Text.Trim)) > CInt(Val(txtnumLetters.Text.Trim))) Then
                'MsgBox("The number of capital letters should be less than or equal to the number of letters.")
                MessageBox.Show("The number of capital letters should be less than or equal to the number of letters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtnumCapLetters.Text = ""
                txtnumCapLetters.Focus()
                Exit Sub
            End If


            If CInt(Val(txtnumMinLength.Text.Trim)) < 8 Then
                'MsgBox("The minimum length of the password should be 8.")
                MessageBox.Show("The minimum length of the password should be 8", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtnumMinLength.Text = ""
                txtnumMinLength.Focus()
                Exit Sub
            End If

            If (CInt(Val(txtnumLetters.Text.Trim)) + CInt(Val(txtnumDigits.Text.Trim)) + CInt(Val(txtnumSpChars.Text.Trim)) < 8) Then
                'MsgBox("The sum of number of letters , number of digits and number of special characters should be atleast 8.")
                MessageBox.Show("The sum of number of letters , number of digits and number of special characters should be atleast 8", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtnumLetters.Text = ""
                txtnumDigits.Text = ""
                txtnumSpChars.Text = ""
                txtnumLetters.Focus()
                Exit Sub
            End If

            objfrmSettings.NoofCapitalLetters = CInt(Val(Trim(txtnumCapLetters.Text)))
            objfrmSettings.NoofLetters = CInt(Val(Trim(txtnumLetters.Text)))
            objfrmSettings.NoofDigits = CInt(Val(Trim(txtnumDigits.Text)))
            objfrmSettings.NoOfSpecialChars = CInt(Val(Trim(txtnumSpChars.Text)))
            objfrmSettings.NoofDays = CInt(Val(Trim(txtnumdays.Text)))

            'issue no:1553
            nNoofCapitalLetters = CInt(Val(Trim(txtnumCapLetters.Text)))
            nNoofLetters = CInt(Val(Trim(txtnumLetters.Text)))
            nNoofDigits = CInt(Val(Trim(txtnumDigits.Text)))
            nNoOfSpecialChars = CInt(Val(Trim(txtnumSpChars.Text)))
            nNoofDays = CInt(Val(Trim(txtnumdays.Text)))
            nPSWNumMinLength = CInt(Val(Trim(txtnumMinLength.Text)))


            conn.Open()
            strSQL = "select count(*) from PwdSettings"
            cmd = New SqlCommand(strSQL, conn)
            cnt = cmd.ExecuteScalar

            'cmd = New SqlCommand("Delete from PwdSettings", conn)
            'cmd.ExecuteNonQuery()

            If cnt = 0 Then
                'insert(row)
                strSQL = "insert into PwdSettings(ExpCapitalLetters,ExpNoOfLetters,ExpNoOfDigits,ExpNoOfSpecChars,ExpPwdLength,ExpTimeFrameinDays) " & _
                          " values(" & CInt(Val(txtnumCapLetters.Text.Trim)) & "," & CInt(Val(txtnumLetters.Text.Trim)) & "," & CInt(Val(txtnumDigits.Text.Trim)) & "," & CInt(Val(txtnumSpChars.Text.Trim)) & "," & CInt(Val(txtnumMinLength.Text.Trim)) & "," & CInt(Val(txtnumdays.Text.Trim)) & ")"

            Else
                'update row
                strSQL = "Update PwdSettings set ExpCapitalLetters = " & CInt(Val(txtnumCapLetters.Text.Trim)) & " ,ExpNoOfLetters = " & CInt(Val(txtnumLetters.Text.Trim)) & " ,ExpNoOfDigits = " & CInt(Val(txtnumDigits.Text.Trim)) & _
                         ",ExpNoOfSpecChars = " & CInt(Val(txtnumSpChars.Text.Trim)) & ",ExpPwdLength = " & CInt(Val(txtnumMinLength.Text.Trim)) & ",ExpTimeFrameinDays = " & CInt(Val(txtnumdays.Text.Trim))
            End If

            ' objfrmSettings.strSQL = _strSQL

            'cmd = New SqlCommand(_strSQL, conn)
            'cmd.ExecuteNonQuery()
            ' objfrmSettings.strSQL = _strSQL

            blnSetComplexisityOnSetting = True

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error while setting password complexity settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    MsgBox(ex.Message, MsgBoxStyle.OKOnly, "Password Settings")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub frmPwdSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim cnt As Integer = 0
        Dim _strSQL As String = ""
        Dim oDataReader As SqlDataReader

        Try
            If _blnSetComplexityValue = True Then

                'Set values to textbox
                txtnumCapLetters.Text = nNoofCapitalLetters
                txtnumLetters.Text = nNoofLetters
                txtnumDigits.Text = nNoofDigits
                txtnumSpChars.Text = nNoOfSpecialChars
                txtnumMinLength.Text = nPSWNumMinLength
                txtnumdays.Text = nNoofDays


                _blnSetComplexityValue = False
                Exit Sub
            End If

            conn.Open()
            _strSQL = "select * from PwdSettings"
            cmd = New SqlCommand(_strSQL, conn)

            oDataReader = cmd.ExecuteReader

            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        If Not IsDBNull(oDataReader.Item("ExpCapitalLetters")) Then
                            txtnumCapLetters.Text = oDataReader.Item("ExpCapitalLetters")
                        Else
                            txtnumCapLetters.Text = 0
                        End If
                        If Not IsDBNull(oDataReader.Item("ExpNoOfLetters")) Then
                            txtnumLetters.Text = oDataReader.Item("ExpNoOfLetters")
                        Else
                            txtnumLetters.Text = 0
                        End If
                        If Not IsDBNull(oDataReader.Item("ExpNoOfDigits")) Then
                            txtnumDigits.Text = oDataReader.Item("ExpNoOfDigits")
                        Else
                            txtnumDigits.Text = 0
                        End If
                        If Not IsDBNull(oDataReader.Item("ExpNoOfSpecChars")) Then
                            txtnumSpChars.Text = oDataReader.Item("ExpNoOfSpecChars")
                        Else
                            txtnumSpChars.Text = 0
                        End If
                        If Not IsDBNull(oDataReader.Item("ExpPwdLength")) Then
                            txtnumMinLength.Text = oDataReader.Item("ExpPwdLength")
                        Else
                            txtnumMinLength.Text = 1
                        End If
                        If Not IsDBNull(oDataReader.Item("ExpTimeFrameinDays")) Then
                            txtnumdays.Text = oDataReader.Item("ExpTimeFrameinDays")
                        Else
                            txtnumdays.Text = 0
                        End If

                        ' Mahesh 20070221
                        '_blnSetComplexisityOnSetting = True

                    End While
                End If
                oDataReader.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error while loading password settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub


    Private Sub txtnumCapLetters_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnumCapLetters.KeyPress
        Try
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            Else
                If (e.KeyChar = ChrW(8)) Then
                    Exit Sub
                Else
                    txtnumCapLetters.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtnumdays_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnumdays.KeyPress
        Try
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            Else
                If (e.KeyChar = ChrW(8)) Then
                    Exit Sub
                Else
                    txtnumdays.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtnumDigits_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnumDigits.KeyPress
        Try
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            Else
                If (e.KeyChar = ChrW(8)) Then
                    Exit Sub
                Else
                    txtnumDigits.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtnumLetters_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnumLetters.KeyPress
        Try
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            Else
                If (e.KeyChar = ChrW(8)) Then
                    Exit Sub
                Else
                    txtnumLetters.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtnumMinLength_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnumMinLength.KeyPress
        Try
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            Else
                If (e.KeyChar = ChrW(8)) Then
                    Exit Sub
                Else
                    txtnumMinLength.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtnumSpChars_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnumSpChars.KeyPress
        Try
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            Else
                If (e.KeyChar = ChrW(8)) Then
                    Exit Sub
                Else
                    txtnumSpChars.Focus()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub txtnumCapLetters_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtnumCapLetters.TextChanged
    '    txtnumMinLength.Text = CInt(Val(txtnumDigits.Text.Trim)) + CInt(Val(txtnumLetters.Text.Trim)) + CInt(Val(txtnumSpChars.Text.Trim))
    'End Sub

    'Private Sub txtnumLetters_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtnumLetters.TextChanged
    '    txtnumMinLength.Text = CInt(Val(txtnumDigits.Text.Trim)) + CInt(Val(txtnumLetters.Text.Trim)) + CInt(Val(txtnumSpChars.Text.Trim))
    'End Sub

    'Private Sub txtnumDigits_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtnumDigits.TextChanged
    '    txtnumMinLength.Text = CInt(Val(txtnumDigits.Text.Trim)) + CInt(Val(txtnumLetters.Text.Trim)) + CInt(Val(txtnumSpChars.Text.Trim))
    'End Sub

    'Private Sub txtnumSpChars_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtnumSpChars.TextChanged
    '    txtnumMinLength.Text = CInt(Val(txtnumDigits.Text.Trim)) + CInt(Val(txtnumLetters.Text.Trim)) + CInt(Val(txtnumSpChars.Text.Trim))
    'End Sub


    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub
End Class
