'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************

Public Class clsDataGridTableStyle
    Inherits DataGridTableStyle

    Sub New(ByVal strMappingName As String, Optional ByVal blnReadOnly As Boolean = True)
        MyBase.AlternatingBackColor = Color.FromArgb(222, 231, 250)
        MyBase.SelectionBackColor = Color.FromArgb(255, 197, 108)
        MyBase.GridLineColor = Color.FromArgb(159, 181, 221)
        MyBase.ForeColor = Color.FromArgb(31, 73, 125)
        MyBase.HeaderBackColor = Color.FromArgb(86, 126, 211)
        MyBase.HeaderForeColor = Color.White
        MyBase.HeaderFont = New Font("Tahoma", 9, FontStyle.Bold)
        MyBase.SelectionForeColor = Color.Black
        MyBase.AllowSorting = True
        MyBase.ReadOnly = blnReadOnly
        MyBase.MappingName = strMappingName
        MyBase.PreferredColumnWidth = 125
        MyBase.PreferredRowHeight = 15
        MyBase.RowHeadersVisible = False
        MyBase.BackColor = Color.FromArgb(227, 239, 255)
    End Sub

    Private Sub InitializeComponent()
        '
        'clsDataGridTableStyle
        '
        Me.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackColor = System.Drawing.Color.Red
        Me.ForeColor = System.Drawing.Color.Black
        Me.GridLineColor = System.Drawing.Color.Black
        Me.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.HeaderFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HeaderForeColor = System.Drawing.Color.White
        Me.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.SelectionForeColor = System.Drawing.Color.Black

    End Sub
End Class
