Public Class frmMsg_Addendum
    Inherits System.Windows.Forms.Form
    Private m_VisitID As Long
    Private m_ExamID As Long
    Friend WithEvents tlsp_Msg_Addendum As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents txtAddendum As System.Windows.Forms.TextBox
    Private WithEvents ts_btnOK As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label

    ''''' _OpenFrom =0 '' when open From frmMessages
    ''''' _OpenFrom =1 '' when open From frmVWOrders
    Private _OpenFrom As Int16 = 0

    ' Private AddendumID As Long

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal OpenFrom As Int16)
        MyBase.New()

        _OpenFrom = OpenFrom

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal VisitID As Long, ByVal ExamID As Long)
        MyBase.New()

        m_VisitID = VisitID
        m_ExamID = ExamID

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub


    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
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
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMsg_Addendum))
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.tlsp_Msg_Addendum = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnOK = New System.Windows.Forms.ToolStripButton
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.txtAddendum = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.pnlToolStrip.SuspendLayout()
        Me.tlsp_Msg_Addendum.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.tlsp_Msg_Addendum)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(445, 54)
        Me.pnlToolStrip.TabIndex = 1
        '
        'tlsp_Msg_Addendum
        '
        Me.tlsp_Msg_Addendum.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_Msg_Addendum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_Msg_Addendum.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_Msg_Addendum.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_Msg_Addendum.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOK, Me.ts_btnCancel})
        Me.tlsp_Msg_Addendum.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_Msg_Addendum.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_Msg_Addendum.Name = "tlsp_Msg_Addendum"
        Me.tlsp_Msg_Addendum.Size = New System.Drawing.Size(445, 53)
        Me.tlsp_Msg_Addendum.TabIndex = 1
        Me.tlsp_Msg_Addendum.Text = "ToolStrip1"
        '
        'ts_btnOK
        '
        Me.ts_btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOK.Image = CType(resources.GetObject("ts_btnOK.Image"), System.Drawing.Image)
        Me.ts_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOK.Name = "ts_btnOK"
        Me.ts_btnOK.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOK.Tag = "OK"
        Me.ts_btnOK.Text = "&Save&&Cls"
        Me.ts_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOK.ToolTipText = "Save and Close"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'txtAddendum
        '
        Me.txtAddendum.BackColor = System.Drawing.Color.White
        Me.txtAddendum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAddendum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAddendum.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddendum.ForeColor = System.Drawing.Color.Black
        Me.txtAddendum.Location = New System.Drawing.Point(4, 4)
        Me.txtAddendum.Multiline = True
        Me.txtAddendum.Name = "txtAddendum"
        Me.txtAddendum.Size = New System.Drawing.Size(437, 250)
        Me.txtAddendum.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.txtAddendum)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(445, 257)
        Me.Panel1.TabIndex = 3
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 253)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(437, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 250)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(441, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 250)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(439, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'frmMsg_Addendum
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(445, 311)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMsg_Addendum"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Addendum"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tlsp_Msg_Addendum.ResumeLayout(False)
        Me.tlsp_Msg_Addendum.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmMsg_Addendum_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim objclsAddendum As New clsAddendum
        Try
            'Commented on 20060116 due as per requirement
            'Dim dt As DataTable
            'dt = objclsAddendum.SelectAddendum(m_VisitID, m_ExamID)
            'txtAddendum.Tag = 0
            'If IsNothing(dt) = False Then
            '    If dt.Rows.Count > 0 Then
            '        txtAddendum.Tag = dt.Rows(0)(0)
            '        txtAddendum.Text = dt.Rows(0)(1)
            '    End If
            'End If

            txtAddendum.Tag = 0
            txtAddendum.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Addendum", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '   objclsAddendum = Nothing
        End Try
    End Sub

    'Private Sub tblAddendum_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
    '    Try
    '        Select Case e.Button.Text
    '            Case "&OK"

    '                If Trim(txtAddendum.Text) <> "" Then
    '                    If _OpenFrom = 0 Then
    '                        '' Insert Addendum to Messages
    '                        frmMessages.strAddendum = Trim(txtAddendum.Text)
    '                    ElseIf _OpenFrom = 1 Then  '' 
    '                        '' Insert Addendum to Order
    '                        'frmVWOrders.strAddendum = Trim(txtAddendum.Text)
    '                        frm_LM_Orders.strAddendum = Trim(txtAddendum.Text)
    '                    ElseIf _OpenFrom = 2 Then
    '                        frmPatientLetter.strAddendum = Trim(txtAddendum.Text)
    '                    ElseIf _OpenFrom = 3 Then
    '                        frmPatientConsent.strAddendum = Trim(txtAddendum.Text)
    '                    ElseIf _OpenFrom = 4 Then
    '                        frmPTProtocols.strAddendum = Trim(txtAddendum.Text)


    '                    End If
    '                End If
    '                Me.Close()
    '            Case "&Close"
    '                If _OpenFrom = 0 Then
    '                    frmMessages.strAddendum = ""
    '                ElseIf _OpenFrom = 1 Then
    '                    'frmVWOrders.strAddendum = ""
    '                    frm_LM_Orders.strAddendum = ""
    '                End If
    '                Me.Close()
    '        End Select
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Addendum", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_Msg_Addendum.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    If Trim(txtAddendum.Text) <> "" Then
                        If _OpenFrom = 0 Then
                            '' Insert Addendum to Messages
                            frmMessages.strAddendum = Trim(txtAddendum.Text)
                        ElseIf _OpenFrom = 1 Then  '' 
                            '' Insert Addendum to Order
                            'frmVWOrders.strAddendum = Trim(txtAddendum.Text)
                            frm_LM_Orders.strAddendum = Trim(txtAddendum.Text)
                        ElseIf _OpenFrom = 2 Then
                            frmPatientLetter.strAddendum = Trim(txtAddendum.Text)
                        ElseIf _OpenFrom = 3 Then
                            frmDisclosureMgmt.strAddendum = Trim(txtAddendum.Text)
                        ElseIf _OpenFrom = 4 Then
                            frmPTProtocols.strAddendum = Trim(txtAddendum.Text)
                            ''Sandip Darade 20090615
                            ''Case added to insert addendum to patient consent
                        ElseIf _OpenFrom = 6 Then  'opened from frmPatientConsent
                            frmPatientConsent.strAddendum = Trim(txtAddendum.Text)
                        ElseIf _OpenFrom = 7 Then
                            '  frmNurseNotes.strAddendum = Trim(txtAddendum.Text)
                        ElseIf _OpenFrom = 8 Then
                            frmTriage.strAddendum = Trim(txtAddendum.Text)
                        End If
                    Else
                        If _OpenFrom = 0 Then
                            frmMessages.strAddendum = ""
                        ElseIf _OpenFrom = 1 Then
                            frm_LM_Orders.strAddendum = ""
                        ElseIf _OpenFrom = 2 Then
                            frmPatientLetter.strAddendum = ""
                        ElseIf _OpenFrom = 3 Then
                            frmDisclosureMgmt.strAddendum = ""
                        ElseIf _OpenFrom = 4 Then
                            frmPTProtocols.strAddendum = ""
                        ElseIf _OpenFrom = 7 Then
                            ''commeneted by Mayuri:20120720-Word crash issue
                            '   frmNurseNotes.strAddendum = ""
                        ElseIf _OpenFrom = 8 Then
                            frmTriage.strAddendum = ""
                        End If
                    End If
                    '  Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                Case "Cancel"
                    If _OpenFrom = 0 Then
                        frmMessages.strAddendum = ""
                    ElseIf _OpenFrom = 1 Then
                        frm_LM_Orders.strAddendum = ""
                    ElseIf _OpenFrom = 2 Then
                        frmPatientLetter.strAddendum = ""
                    ElseIf _OpenFrom = 3 Then
                        frmDisclosureMgmt.strAddendum = ""
                    ElseIf _OpenFrom = 4 Then
                        frmPTProtocols.strAddendum = ""
                    ElseIf _OpenFrom = 7 Then
                        '  frmNurseNotes.strAddendum = ""
                    ElseIf _OpenFrom = 8 Then
                        frmTriage.strAddendum = ""
                    End If

                    '   Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Addendum", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
