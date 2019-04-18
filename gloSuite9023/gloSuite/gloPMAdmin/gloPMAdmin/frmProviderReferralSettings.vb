Imports C1.Win.C1FlexGrid
Public Class frmProviderReferralSettings
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents flxData As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProviderReferralSettings))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.flxData = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstrip = New System.Windows.Forms.ToolStrip
        Me.btnOK = New System.Windows.Forms.ToolStripButton
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.pnlMain.SuspendLayout()
        CType(Me.flxData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.flxData)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(496, 390)
        Me.pnlMain.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 386)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(488, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 386)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(492, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 386)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(490, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'flxData
        '
        Me.flxData.BackColor = System.Drawing.Color.GhostWhite
        Me.flxData.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.flxData.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.flxData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxData.ForeColor = System.Drawing.Color.Black
        Me.flxData.Location = New System.Drawing.Point(3, 0)
        Me.flxData.Name = "flxData"
        Me.flxData.Rows.DefaultSize = 19
        Me.flxData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.flxData.Size = New System.Drawing.Size(490, 387)
        Me.flxData.StyleInfo = resources.GetString("flxData.StyleInfo")
        Me.flxData.TabIndex = 1
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
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(496, 56)
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
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK, Me.btnCancel})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(496, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(66, 50)
        Me.btnOK.Text = "&Save&&Cls"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOK.ToolTipText = "Save and Close"
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
        'frmProviderReferralSettings
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(496, 446)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProviderReferralSettings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Provider-Referral Letter"
        Me.pnlMain.ResumeLayout(False)
        CType(Me.flxData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Const COL_PROVIDER As Byte = 0
    Private Const COL_Referral As Byte = 1
    Private Providers As New Collection
    Private ReferralLetters As New Collection
    Private ReferralLetter As String

    Private Sub RetrieveProviders()
        Dim objProvider As New clsProvider
        Providers = objProvider.Fill_Providers()
        objProvider = Nothing
        Dim nCount As Int16
        If Providers.Count > 0 Then
            For nCount = 1 To Providers.Count
                With flxData
                    .Rows.Add()
                    .SetData(.Rows.Count - 1, COL_PROVIDER, Providers(nCount))
                End With
            Next
        Else
            MessageBox.Show("No doctor information available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
    End Sub
    Private Sub RetrieveReferrals()
        ReferralLetter = ""
        Dim objReferralLetters As New clsProviderSettings
        ReferralLetters = objReferralLetters.Fill_ReferralLetters()
        objReferralLetters = Nothing

        Dim nCount As Int16
        If ReferralLetters.Count > 0 Then

            ReferralLetter = " "
            For nCount = 1 To ReferralLetters.Count
                'If (nCount = 1) Then
                '    ReferralLetter = ReferralLetters(nCount)
                'Else
                ReferralLetter += "|" & ReferralLetters(nCount)
                'End If

            Next
            ReferralLetter = ReferralLetter.Substring(0, ReferralLetter.Length - 1)

            Dim objRange As CellRange
            Dim objStyle As CellStyle = flxData.Styles.Add("Referral")
            objStyle.DataType = GetType(System.String)
            objStyle.ComboList = ReferralLetter
            objRange = flxData.GetCellRange(COL_Referral, COL_Referral, flxData.Rows.Count - 1, COL_Referral)
            objRange.Style = objStyle
        Else
            MessageBox.Show("No referral letters information available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
    End Sub

    Private Sub frmProviderReferralSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(flxData) '<<<<<<Ojeswini_8July09>>>>>>>>>>>>>>>

        Try
            Call FormatGrid()
            Call RetrieveProviders()
            If Providers.Count > 0 Then
                Call RetrieveReferrals()
                If ReferralLetters.Count > 0 Then
                    Call LoadSettings()
                End If
            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub FormatGrid()
        With flxData
            .Cols.Count = 2
            .Rows.Count = 1
            .Rows.Fixed = 1

            '******By Sandip Deshmukh 13 Oct 07 5.48PM Bug# 335
            '******Code of line Added so that the Columns header 
            '******on Drag does not Change the column Header Position
            .AllowDragging = AllowDraggingEnum.None
            '****** 13 Oct 07 5.48PM

            .Cols(COL_PROVIDER).Width = 0.4 * .Width
            .Cols(COL_Referral).Width = 0.6 * .Width - 5

            flxData.SetData(0, COL_PROVIDER, "Doctor")
            flxData.SetData(0, COL_Referral, "Default Referral Letter")

            .Cols(COL_PROVIDER).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(COL_Referral).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

            .Cols(COL_PROVIDER).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_Referral).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(COL_PROVIDER).AllowEditing = False
        End With
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim strProviderName As String = ""
        Dim strReferralLetter As String = ""
        Try
            Dim objSettings As New clsProviderSettings
            objSettings.DeleteProviderConfiguration(clsProviderSettings.enmSettingsType.ProviderReferralLetterConfiguration)
            Dim nCount As Int16

            For nCount = 1 To flxData.Rows.Count - 1
                strProviderName = flxData.GetData(nCount, COL_PROVIDER)
                strReferralLetter = flxData.GetData(nCount, COL_Referral)
                objSettings.AddProviderConfiguration(strProviderName, strReferralLetter, clsProviderSettings.enmSettingsType.ProviderReferralLetterConfiguration)

                'sarika  22nd feb
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " user has set " & strReferralLetter & " as the Default Referral letter for the Doctor " & strProviderName, gstrLoginName, gstrClientMachineName, 0)
                objAudit = Nothing
                '-------------

            Next
            objSettings = Nothing
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " Error occured while setting " & strReferralLetter & " as the Default Referral letter for the doctor " & strProviderName, gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
        End Try
    End Sub

    Private Sub LoadSettings()
        Dim strSettings As String = ""
        Dim nCount As Int16
        Dim objProviderConfiguration As New clsProviderSettings
        For nCount = 1 To flxData.Rows.Count - 1
            strSettings = objProviderConfiguration.RetrieveSettings(flxData.GetData(nCount, COL_PROVIDER), clsProviderSettings.enmSettingsType.ProviderReferralLetterConfiguration)
            flxData.SetData(nCount, COL_Referral, strSettings)
        Next
        objProviderConfiguration = Nothing
    End Sub
End Class
