Public Class frmMSTROS
    Inherits System.Windows.Forms.Form

    '''' While Add, return this Paramerters to View-From so the Newly Added Records get Highlighted 
    Public _Description As String
    Public _CategoryID As Long
    ''''
    Private objclsROS As New clsROS
    Private CategoryId As Long
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_MSTROS As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private ROSId As Long

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal lnCategoryID As Long, Optional ByVal strROS As String = "")
        MyBase.New()
        CategoryId = lnCategoryID
        _Description = strROS
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal lnCategoryID As Long, ByVal lnROSId As Long)
        MyBase.New()
        CategoryId = lnCategoryID
        ROSId = lnROSId
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

    Private errprovider As New ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTROS))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_MSTROS = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_MSTROS.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Description :"
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(98, 18)
        Me.txtDescription.MaxLength = 255
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(235, 22)
        Me.txtDescription.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Comments :"
        '
        'txtComments
        '
        Me.txtComments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.ForeColor = System.Drawing.Color.Black
        Me.txtComments.Location = New System.Drawing.Point(98, 50)
        Me.txtComments.MaxLength = 255
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(235, 22)
        Me.txtComments.TabIndex = 1
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_MSTROS)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(352, 53)
        Me.pnl_tlsp.TabIndex = 14
        '
        'tlsp_MSTROS
        '
        Me.tlsp_MSTROS.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_MSTROS.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_MSTROS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_MSTROS.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tlsp_MSTROS.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_MSTROS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_MSTROS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_MSTROS.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_MSTROS.Name = "tlsp_MSTROS"
        Me.tlsp_MSTROS.Size = New System.Drawing.Size(352, 53)
        Me.tlsp_MSTROS.TabIndex = 0
        Me.tlsp_MSTROS.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel1.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel1.Controls.Add(Me.lbl_pnlRight)
        Me.Panel1.Controls.Add(Me.lbl_pnlTop)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtComments)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(352, 90)
        Me.Panel1.TabIndex = 15
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 86)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(344, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 83)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(348, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 83)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(346, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(9, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(14, 14)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "*"
        '
        'frmMSTROS
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(352, 143)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMSTROS"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ROS"
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_MSTROS.ResumeLayout(False)
        Me.tlsp_MSTROS.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmMSTROS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ROSId <> 0 Then
            Try
                Dim arrlist As ArrayList
                arrlist = objclsROS.FetchDataForUpdate(ROSId)
                If arrlist.Count > 0 Then
                    txtDescription.Text = CType(arrlist.Item(0), System.String)
                    txtComments.Text = CType(arrlist.Item(1), System.String)
                End If
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            txtDescription.Text = _Description
        End If
    End Sub

    Private Sub CloseMSTROS()
        'errprovider.SetError(txtDescription, "")
        _Description = ""
        objclsROS = Nothing
        Me.Close()
    End Sub

    Private Sub SaveMSTROS()
        If Trim(txtDescription.Text) = "" Then
            'errprovider.SetError(txtDescription, "Description is Required")
            MessageBox.Show("Description is Required", "ROS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtDescription.Focus()
            Exit Sub
        Else
            'errprovider.SetError(txtDescription, "")
            If Not objclsROS.ValidateDescription(CategoryId, Trim(txtDescription.Text), ROSId) Then
                'errprovider.SetError(txtDescription, "Duplicate Description")
                MessageBox.Show("Duplicate Description", "ROS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescription.Focus()
                Exit Sub
            Else
                'errprovider.SetError(txtDescription, "")
            End If
        End If

        Dim formclose As Boolean = True
        If ROSId = 0 Then
            Try
                If objclsROS.AddData(Trim(txtDescription.Text), Trim(txtComments.Text), CategoryId) <> 0 Then
                    _Description = Trim(txtDescription.Text)
                Else
                    _Description = ""
                End If


            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                objclsROS = Nothing
                Me.Close()
            End Try
        Else
            Try

                Dim Result As Boolean = CheckIsHistoryItemExistsinPortal(ROSId)
                If Result Then
                    formclose = False
                    Exit Sub
                End If



                objclsROS.UpdateData(txtDescription.Text, txtComments.Text, ROSId)
            Catch ex As SqlClient.SqlException
                MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If formclose = True Then
                    objclsROS = Nothing
                    Me.Close()
                End If
            End Try
        End If

    End Sub

    Private Function CheckIsHistoryItemExistsinPortal(ByVal HistoryID As Long) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dt2 As DataTable = Nothing

        Try

            oParameters = New gloDatabaseLayer.DBParameters()
            oParameters.Add("@nhistoryid", HistoryID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@IsDelete", False, ParameterDirection.Input, SqlDbType.Bit)
            oParameters.Add("@ItemType", 1, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("WS_IsHistoryItemExistsinHealthform", oParameters, _dt)
            If _dt IsNot Nothing AndAlso _dt.Rows.Count > 0 Then
                If MessageBox.Show("Selected ROS item is used in patient portal forms. After this modification all existing patient portal forms data and any new incoming data from portal will be associated to this modified ROS item." + System.Environment.NewLine + System.Environment.NewLine + "Do you want to continue with the modification?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                    Try

                        ' Set IsRepublish Required to 1 & Delete Entry 
                        oParameters = New gloDatabaseLayer.DBParameters()
                        oParameters.Add("@nhistoryid", HistoryID, ParameterDirection.Input, SqlDbType.BigInt)
                        oParameters.Add("@IsDelete", False, ParameterDirection.Input, SqlDbType.Bit)
                        oParameters.Add("@IsUpdatePatientForm", True, ParameterDirection.Input, SqlDbType.Bit)
                        oParameters.Add("@ItemType", 1, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.Connect(False)
                        oDB.Retrive("WS_IsHistoryItemExistsinHealthform", oParameters, _dt2)

                    Catch ex As Exception
                    End Try
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If

        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Throw dbEx
        Finally
            If oParameters IsNot Nothing Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If

            If Not IsNothing(_dt) Then
                _dt.Dispose()
                _dt = Nothing
            End If

            If Not IsNothing(_dt2) Then
                _dt2.Dispose()
                _dt2 = Nothing
            End If

        End Try

        Return False

    End Function

    Private Sub tlsp_MSTROS_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_MSTROS.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveMSTROS()

                Case "Close"
                    CloseMSTROS()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub
End Class
