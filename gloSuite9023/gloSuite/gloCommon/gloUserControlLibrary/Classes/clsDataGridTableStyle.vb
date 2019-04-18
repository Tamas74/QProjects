Public Class clsDataGridTableStyle
    Inherits DataGridTableStyle

    Sub New(ByVal strMappingName As String, Optional ByVal blnReadOnly As Boolean = True)
        'MyBase.AlternatingBackColor = Color.FromArgb(207, 227, 254)
        'MyBase.SelectionBackColor = Color.Blue
        'MyBase.GridLineColor = Color.White
        ''.ForeColor = Color.White
        'MyBase.HeaderBackColor = Color.Blue
        'MyBase.HeaderForeColor = Color.White
        'MyBase.HeaderFont = New Font("Arial", 10, FontStyle.Bold)

        'MyBase.SelectionForeColor = Color.White
        'MyBase.AllowSorting = True
        'MyBase.ReadOnly = blnReadOnly
        'MyBase.MappingName = strMappingName
        'MyBase.PreferredColumnWidth = 125
        'MyBase.PreferredRowHeight = 15
        'MyBase.RowHeadersVisible = False



        'Nomal(231, 237, 249)
        'Active(White)
        'Fixed(51, 125, 207)
        'Heiligt(LightBlue)
        'Frozen(Beige)

        'Old color code for flexigrid
        'MyBase.AlternatingBackColor = Color.White
        'MyBase.SelectionBackColor = Color.LightBlue
        'MyBase.GridLineColor = Color.White
        ''.ForeColor = Color.White
        'MyBase.HeaderBackColor = Color.FromArgb(51, 125, 207)
        'MyBase.HeaderForeColor = Color.White
        'MyBase.HeaderFont = New Font("Arial", 10, FontStyle.Bold)

        'MyBase.SelectionForeColor = Color.Red
        'MyBase.AllowSorting = True
        'MyBase.ReadOnly = blnReadOnly
        'MyBase.MappingName = strMappingName
        'MyBase.PreferredColumnWidth = 125
        'MyBase.PreferredRowHeight = 15
        'MyBase.RowHeadersVisible = False
        'MyBase.BackColor = Color.FromArgb(231, 237, 249)

        'New color code for Flexigid
        MyBase.AlternatingBackColor = Color.FromArgb(222, 231, 250) '(214, 235, 248)
        MyBase.SelectionBackColor = Color.FromArgb(254, 207, 102) '(255, 197, 108)
        MyBase.SelectionForeColor = Color.Black 'Color.FromArgb(31, 73, 125) 
        MyBase.GridLineColor = Color.FromArgb(159, 181, 221) 'Color.Gray
        MyBase.ForeColor = Color.FromArgb(31, 73, 125)
        MyBase.HeaderBackColor = Color.FromArgb(86, 126, 211) '(9, 96, 162)
        MyBase.HeaderForeColor = Color.White
        MyBase.HeaderFont = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        MyBase.AllowSorting = True
        MyBase.ReadOnly = blnReadOnly
        MyBase.MappingName = strMappingName
        MyBase.PreferredColumnWidth = 125
        MyBase.PreferredRowHeight = 15
        MyBase.RowHeadersVisible = False
        MyBase.BackColor = Color.FromArgb(240, 247, 255) 'Color.GhostWhite
    End Sub

    Public WriteOnly Property RowHeaderVisible() As Boolean
        Set(ByVal Value As Boolean)
            MyBase.RowHeadersVisible = Value
        End Set
    End Property

End Class
