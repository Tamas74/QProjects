Public Class DIScreeningResults
    Inherits System.Windows.Forms.UserControl

    Public Event OkClk(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event MonoClk(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal monograph As String)
    Public Event SavePatient_Education()
    Private m_Monograph As String
    Private m_ReadOnly As Boolean
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Protected Friend txtdesc As gloRichtextbox
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Protected Friend WithEvents btnMono As System.Windows.Forms.Button
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlMonograph As System.Windows.Forms.Panel
    Protected Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlfill As System.Windows.Forms.Panel

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label17 = New System.Windows.Forms.Label
        Me.lblHeader = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.btnMono = New System.Windows.Forms.Button
        Me.pnlMonograph = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.pnlfill = New System.Windows.Forms.Panel
        Me.txtdesc = New gloDIControl.gloRichtextbox(Me.components)
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlTop.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlMonograph.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlfill.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.Panel4)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(648, 30)
        Me.pnlTop.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = Global.gloDIControl.My.Resources.Resources.Img_LongButton
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Controls.Add(Me.lblHeader)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(642, 24)
        Me.Panel4.TabIndex = 7
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Location = New System.Drawing.Point(544, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 22)
        Me.Label17.TabIndex = 4
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(13, 5)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(132, 14)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Heading of the form"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 22)
        Me.Label14.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.Transparent
        Me.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(545, 1)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(96, 22)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Location = New System.Drawing.Point(0, 23)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(641, 1)
        Me.Label15.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(641, 1)
        Me.Label16.TabIndex = 0
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Location = New System.Drawing.Point(641, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 24)
        Me.Label13.TabIndex = 3
        '
        'btnMono
        '
        Me.btnMono.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMono.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnMono.FlatAppearance.BorderSize = 0
        Me.btnMono.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnMono.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnMono.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMono.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMono.Location = New System.Drawing.Point(545, 1)
        Me.btnMono.Name = "btnMono"
        Me.btnMono.Size = New System.Drawing.Size(96, 23)
        Me.btnMono.TabIndex = 4
        Me.btnMono.Text = "More Info"
        '
        'pnlMonograph
        '
        Me.pnlMonograph.BackColor = System.Drawing.Color.Transparent
        Me.pnlMonograph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMonograph.Controls.Add(Me.Panel3)
        Me.pnlMonograph.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMonograph.Location = New System.Drawing.Point(0, 30)
        Me.pnlMonograph.Name = "pnlMonograph"
        Me.pnlMonograph.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlMonograph.Size = New System.Drawing.Size(648, 28)
        Me.pnlMonograph.TabIndex = 5
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = Global.gloDIControl.My.Resources.Resources.Img_LongButton
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.btnMono)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(642, 25)
        Me.Panel3.TabIndex = 7
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Location = New System.Drawing.Point(0, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 23)
        Me.Label18.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Location = New System.Drawing.Point(544, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 23)
        Me.Label9.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Location = New System.Drawing.Point(641, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 23)
        Me.Label10.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(0, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(642, 1)
        Me.Label11.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(642, 1)
        Me.Label12.TabIndex = 0
        '
        'pnlfill
        '
        Me.pnlfill.Controls.Add(Me.txtdesc)
        Me.pnlfill.Controls.Add(Me.Label6)
        Me.pnlfill.Controls.Add(Me.Label5)
        Me.pnlfill.Controls.Add(Me.Label3)
        Me.pnlfill.Controls.Add(Me.Label4)
        Me.pnlfill.Controls.Add(Me.Label2)
        Me.pnlfill.Controls.Add(Me.Label1)
        Me.pnlfill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlfill.Location = New System.Drawing.Point(0, 58)
        Me.pnlfill.Name = "pnlfill"
        Me.pnlfill.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlfill.Size = New System.Drawing.Size(648, 118)
        Me.pnlfill.TabIndex = 6
        '
        'txtdesc
        '
        Me.txtdesc.BackColor = System.Drawing.Color.White
        Me.txtdesc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtdesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtdesc.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.ForeColor = System.Drawing.Color.Black
        Me.txtdesc.Location = New System.Drawing.Point(7, 4)
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.Size = New System.Drawing.Size(637, 110)
        Me.txtdesc.TabIndex = 0
        Me.txtdesc.Text = ""
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(7, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(637, 3)
        Me.Label6.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Location = New System.Drawing.Point(4, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(3, 113)
        Me.Label5.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 113)
        Me.Label3.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(644, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 113)
        Me.Label4.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(642, 1)
        Me.Label2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(642, 1)
        Me.Label1.TabIndex = 0
        '
        'DIScreeningResults
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnlfill)
        Me.Controls.Add(Me.pnlMonograph)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "DIScreeningResults"
        Me.Size = New System.Drawing.Size(648, 176)
        Me.pnlTop.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.pnlMonograph.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlfill.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public WriteOnly Property ScreenType() As Int16
        Set(ByVal Value As Int16)
            Select Case Value
                Case 1
                    lblHeader.Text = "Adverse Drug Effects"
                Case 2
                    lblHeader.Text = "Prior Adverse Reaction"
                Case 3
                    lblHeader.Text = "Drug to Drug Interaction"
                Case 4
                    lblHeader.Text = "Duplicate Therapy Screening"
                    pnlMonograph.Visible = False
                Case 5
                    lblHeader.Text = "Drug to Food Interaction"
                Case 8
                    lblHeader.Text = "Drug to Disease Interaction"
            End Select
        End Set
    End Property
    Public Property Monograph() As String
        Get
            Return m_Monograph
        End Get
        Set(ByVal Value As String)
            m_Monograph = Value
        End Set
    End Property
    Public Property description() As String
        Get
            Return txtdesc.Text
        End Get
        Set(ByVal Value As String)
            txtdesc.Text = Value
        End Set
    End Property
    Public WriteOnly Property Header() As String
        Set(ByVal Value As String)
            lblHeader.Text = Value
        End Set
    End Property
    Public WriteOnly Property SetTitleVisibility() As Boolean
        Set(ByVal Value As Boolean)
            pnlTop.Visible = Value
        End Set
    End Property
    Public WriteOnly Property SetMonoVisibility(Optional ByVal blntype As Boolean = False) As Boolean
        Set(ByVal Value As Boolean)
            If blntype = True Then
                pnlMonograph.Visible = Value
            Else
                btnMono.Visible = Value
            End If
        End Set
    End Property
    Public WriteOnly Property MakeReadOnly() As Boolean
        Set(ByVal Value As Boolean)
            txtdesc.ReadOnly = True
        End Set
    End Property
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        RaiseEvent OkClk(sender, e)
    End Sub
    Private Sub btnMono_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMono.Click
        RaiseEvent MonoClk(sender, e, m_Monograph)
    End Sub
    Private Sub DIScreeningResults_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMono.MouseHover, btnOK.MouseHover
        CType(sender, System.Windows.Forms.Button).BackgroundImage = Global.gloDIControl.My.Resources.Img_LongYellow
        CType(sender, System.Windows.Forms.Button).BackgroundImageLayout = Windows.Forms.ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMono.MouseLeave, btnOK.MouseLeave
        CType(sender, System.Windows.Forms.Button).BackgroundImage = Global.gloDIControl.My.Resources.Img_LongButton
        CType(sender, System.Windows.Forms.Button).BackgroundImageLayout = Windows.Forms.ImageLayout.Stretch
    End Sub

    Protected Sub SavePatientEducation()
        RaiseEvent SavePatient_Education()
    End Sub
End Class
