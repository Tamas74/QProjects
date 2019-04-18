Public Class frmDMS_Support
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
    Public WithEvents Img_Category As System.Windows.Forms.PictureBox
    Public WithEvents Img_Month As System.Windows.Forms.PictureBox
    Public WithEvents Img_Document As System.Windows.Forms.PictureBox
    Public WithEvents Img_Note As System.Windows.Forms.PictureBox
    Public WithEvents Img_Go As System.Windows.Forms.PictureBox
    Public WithEvents Img_MonthUncategory As System.Windows.Forms.PictureBox
    Public WithEvents Img_Blanck As System.Windows.Forms.PictureBox
    Friend WithEvents picPreviousPageDown As System.Windows.Forms.PictureBox
    Friend WithEvents picNextPageDown As System.Windows.Forms.PictureBox
    Friend WithEvents picZoomOutDown As System.Windows.Forms.PictureBox
    Friend WithEvents picZoomInDown As System.Windows.Forms.PictureBox
    Friend WithEvents picPreviousPage As System.Windows.Forms.PictureBox
    Friend WithEvents picNextPage As System.Windows.Forms.PictureBox
    Friend WithEvents picZoomOut As System.Windows.Forms.PictureBox
    Friend WithEvents picZoomIn As System.Windows.Forms.PictureBox
    Public WithEvents Img_ScannedDocument As System.Windows.Forms.PictureBox
    Public WithEvents Img_Document_NotReviwed As System.Windows.Forms.PictureBox
    Public WithEvents Img_Reviwed As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDMS_Support))
        Me.Img_Category = New System.Windows.Forms.PictureBox
        Me.Img_Month = New System.Windows.Forms.PictureBox
        Me.Img_Document = New System.Windows.Forms.PictureBox
        Me.Img_Note = New System.Windows.Forms.PictureBox
        Me.Img_Go = New System.Windows.Forms.PictureBox
        Me.Img_MonthUncategory = New System.Windows.Forms.PictureBox
        Me.Img_Blanck = New System.Windows.Forms.PictureBox
        Me.picPreviousPageDown = New System.Windows.Forms.PictureBox
        Me.picNextPageDown = New System.Windows.Forms.PictureBox
        Me.picZoomOutDown = New System.Windows.Forms.PictureBox
        Me.picZoomInDown = New System.Windows.Forms.PictureBox
        Me.picPreviousPage = New System.Windows.Forms.PictureBox
        Me.picNextPage = New System.Windows.Forms.PictureBox
        Me.picZoomOut = New System.Windows.Forms.PictureBox
        Me.picZoomIn = New System.Windows.Forms.PictureBox
        Me.Img_ScannedDocument = New System.Windows.Forms.PictureBox
        Me.Img_Document_NotReviwed = New System.Windows.Forms.PictureBox
        Me.Img_Reviwed = New System.Windows.Forms.PictureBox
        CType(Me.Img_Category, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_Month, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_Document, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_Note, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_Go, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_MonthUncategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_Blanck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPreviousPageDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picNextPageDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picZoomOutDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picZoomInDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPreviousPage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picNextPage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picZoomOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picZoomIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_ScannedDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_Document_NotReviwed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_Reviwed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Img_Category
        '
        Me.Img_Category.Image = CType(resources.GetObject("Img_Category.Image"), System.Drawing.Image)
        Me.Img_Category.Location = New System.Drawing.Point(12, 12)
        Me.Img_Category.Name = "Img_Category"
        Me.Img_Category.Size = New System.Drawing.Size(110, 26)
        Me.Img_Category.TabIndex = 0
        Me.Img_Category.TabStop = False
        '
        'Img_Month
        '
        Me.Img_Month.Image = CType(resources.GetObject("Img_Month.Image"), System.Drawing.Image)
        Me.Img_Month.Location = New System.Drawing.Point(14, 54)
        Me.Img_Month.Name = "Img_Month"
        Me.Img_Month.Size = New System.Drawing.Size(110, 26)
        Me.Img_Month.TabIndex = 1
        Me.Img_Month.TabStop = False
        '
        'Img_Document
        '
        Me.Img_Document.Image = CType(resources.GetObject("Img_Document.Image"), System.Drawing.Image)
        Me.Img_Document.Location = New System.Drawing.Point(16, 98)
        Me.Img_Document.Name = "Img_Document"
        Me.Img_Document.Size = New System.Drawing.Size(110, 26)
        Me.Img_Document.TabIndex = 2
        Me.Img_Document.TabStop = False
        '
        'Img_Note
        '
        Me.Img_Note.Image = CType(resources.GetObject("Img_Note.Image"), System.Drawing.Image)
        Me.Img_Note.Location = New System.Drawing.Point(18, 142)
        Me.Img_Note.Name = "Img_Note"
        Me.Img_Note.Size = New System.Drawing.Size(110, 26)
        Me.Img_Note.TabIndex = 3
        Me.Img_Note.TabStop = False
        '
        'Img_Go
        '
        Me.Img_Go.Image = CType(resources.GetObject("Img_Go.Image"), System.Drawing.Image)
        Me.Img_Go.Location = New System.Drawing.Point(18, 192)
        Me.Img_Go.Name = "Img_Go"
        Me.Img_Go.Size = New System.Drawing.Size(110, 26)
        Me.Img_Go.TabIndex = 4
        Me.Img_Go.TabStop = False
        '
        'Img_MonthUncategory
        '
        Me.Img_MonthUncategory.Image = CType(resources.GetObject("Img_MonthUncategory.Image"), System.Drawing.Image)
        Me.Img_MonthUncategory.Location = New System.Drawing.Point(138, 54)
        Me.Img_MonthUncategory.Name = "Img_MonthUncategory"
        Me.Img_MonthUncategory.Size = New System.Drawing.Size(110, 26)
        Me.Img_MonthUncategory.TabIndex = 5
        Me.Img_MonthUncategory.TabStop = False
        '
        'Img_Blanck
        '
        Me.Img_Blanck.Location = New System.Drawing.Point(140, 142)
        Me.Img_Blanck.Name = "Img_Blanck"
        Me.Img_Blanck.Size = New System.Drawing.Size(110, 26)
        Me.Img_Blanck.TabIndex = 6
        Me.Img_Blanck.TabStop = False
        '
        'picPreviousPageDown
        '
        Me.picPreviousPageDown.Image = CType(resources.GetObject("picPreviousPageDown.Image"), System.Drawing.Image)
        Me.picPreviousPageDown.Location = New System.Drawing.Point(62, 350)
        Me.picPreviousPageDown.Name = "picPreviousPageDown"
        Me.picPreviousPageDown.Size = New System.Drawing.Size(30, 30)
        Me.picPreviousPageDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPreviousPageDown.TabIndex = 16
        Me.picPreviousPageDown.TabStop = False
        '
        'picNextPageDown
        '
        Me.picNextPageDown.Image = CType(resources.GetObject("picNextPageDown.Image"), System.Drawing.Image)
        Me.picNextPageDown.Location = New System.Drawing.Point(64, 308)
        Me.picNextPageDown.Name = "picNextPageDown"
        Me.picNextPageDown.Size = New System.Drawing.Size(30, 30)
        Me.picNextPageDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picNextPageDown.TabIndex = 15
        Me.picNextPageDown.TabStop = False
        '
        'picZoomOutDown
        '
        Me.picZoomOutDown.Image = CType(resources.GetObject("picZoomOutDown.Image"), System.Drawing.Image)
        Me.picZoomOutDown.Location = New System.Drawing.Point(64, 268)
        Me.picZoomOutDown.Name = "picZoomOutDown"
        Me.picZoomOutDown.Size = New System.Drawing.Size(30, 30)
        Me.picZoomOutDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picZoomOutDown.TabIndex = 14
        Me.picZoomOutDown.TabStop = False
        '
        'picZoomInDown
        '
        Me.picZoomInDown.Image = CType(resources.GetObject("picZoomInDown.Image"), System.Drawing.Image)
        Me.picZoomInDown.Location = New System.Drawing.Point(66, 230)
        Me.picZoomInDown.Name = "picZoomInDown"
        Me.picZoomInDown.Size = New System.Drawing.Size(30, 30)
        Me.picZoomInDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picZoomInDown.TabIndex = 13
        Me.picZoomInDown.TabStop = False
        '
        'picPreviousPage
        '
        Me.picPreviousPage.Image = CType(resources.GetObject("picPreviousPage.Image"), System.Drawing.Image)
        Me.picPreviousPage.Location = New System.Drawing.Point(16, 348)
        Me.picPreviousPage.Name = "picPreviousPage"
        Me.picPreviousPage.Size = New System.Drawing.Size(30, 30)
        Me.picPreviousPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPreviousPage.TabIndex = 12
        Me.picPreviousPage.TabStop = False
        '
        'picNextPage
        '
        Me.picNextPage.Image = CType(resources.GetObject("picNextPage.Image"), System.Drawing.Image)
        Me.picNextPage.Location = New System.Drawing.Point(18, 308)
        Me.picNextPage.Name = "picNextPage"
        Me.picNextPage.Size = New System.Drawing.Size(30, 30)
        Me.picNextPage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picNextPage.TabIndex = 11
        Me.picNextPage.TabStop = False
        '
        'picZoomOut
        '
        Me.picZoomOut.Image = CType(resources.GetObject("picZoomOut.Image"), System.Drawing.Image)
        Me.picZoomOut.Location = New System.Drawing.Point(18, 268)
        Me.picZoomOut.Name = "picZoomOut"
        Me.picZoomOut.Size = New System.Drawing.Size(30, 30)
        Me.picZoomOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picZoomOut.TabIndex = 10
        Me.picZoomOut.TabStop = False
        '
        'picZoomIn
        '
        Me.picZoomIn.Image = CType(resources.GetObject("picZoomIn.Image"), System.Drawing.Image)
        Me.picZoomIn.Location = New System.Drawing.Point(20, 230)
        Me.picZoomIn.Name = "picZoomIn"
        Me.picZoomIn.Size = New System.Drawing.Size(30, 30)
        Me.picZoomIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picZoomIn.TabIndex = 9
        Me.picZoomIn.TabStop = False
        '
        'Img_ScannedDocument
        '
        Me.Img_ScannedDocument.Image = CType(resources.GetObject("Img_ScannedDocument.Image"), System.Drawing.Image)
        Me.Img_ScannedDocument.Location = New System.Drawing.Point(152, 194)
        Me.Img_ScannedDocument.Name = "Img_ScannedDocument"
        Me.Img_ScannedDocument.Size = New System.Drawing.Size(110, 26)
        Me.Img_ScannedDocument.TabIndex = 17
        Me.Img_ScannedDocument.TabStop = False
        '
        'Img_Document_NotReviwed
        '
        Me.Img_Document_NotReviwed.Image = CType(resources.GetObject("Img_Document_NotReviwed.Image"), System.Drawing.Image)
        Me.Img_Document_NotReviwed.Location = New System.Drawing.Point(138, 100)
        Me.Img_Document_NotReviwed.Name = "Img_Document_NotReviwed"
        Me.Img_Document_NotReviwed.Size = New System.Drawing.Size(110, 26)
        Me.Img_Document_NotReviwed.TabIndex = 18
        Me.Img_Document_NotReviwed.TabStop = False
        '
        'Img_Reviwed
        '
        Me.Img_Reviwed.Image = CType(resources.GetObject("Img_Reviwed.Image"), System.Drawing.Image)
        Me.Img_Reviwed.Location = New System.Drawing.Point(274, 142)
        Me.Img_Reviwed.Name = "Img_Reviwed"
        Me.Img_Reviwed.Size = New System.Drawing.Size(110, 26)
        Me.Img_Reviwed.TabIndex = 19
        Me.Img_Reviwed.TabStop = False
        '
        'frmDMS_Support
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(424, 390)
        Me.Controls.Add(Me.Img_Reviwed)
        Me.Controls.Add(Me.Img_Document_NotReviwed)
        Me.Controls.Add(Me.Img_ScannedDocument)
        Me.Controls.Add(Me.picPreviousPageDown)
        Me.Controls.Add(Me.picNextPageDown)
        Me.Controls.Add(Me.picZoomOutDown)
        Me.Controls.Add(Me.picZoomInDown)
        Me.Controls.Add(Me.picPreviousPage)
        Me.Controls.Add(Me.picNextPage)
        Me.Controls.Add(Me.picZoomOut)
        Me.Controls.Add(Me.picZoomIn)
        Me.Controls.Add(Me.Img_Blanck)
        Me.Controls.Add(Me.Img_MonthUncategory)
        Me.Controls.Add(Me.Img_Go)
        Me.Controls.Add(Me.Img_Note)
        Me.Controls.Add(Me.Img_Document)
        Me.Controls.Add(Me.Img_Month)
        Me.Controls.Add(Me.Img_Category)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDMS_Support"
        Me.Text = "frmDMS_Support"
        CType(Me.Img_Category, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_Month, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_Document, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_Note, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_Go, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_MonthUncategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_Blanck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPreviousPageDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picNextPageDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picZoomOutDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picZoomInDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPreviousPage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picNextPage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picZoomOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picZoomIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_ScannedDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_Document_NotReviwed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_Reviwed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
