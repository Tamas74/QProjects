Imports System.Windows.Forms
Imports System.Drawing
Namespace gloprintfax
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

            MyBase.AlternatingBackColor = Color.FromArgb(222, 231, 250)
            MyBase.SelectionBackColor = Color.FromArgb(254, 207, 102)
            MyBase.GridLineColor = Color.FromArgb(159, 181, 221)
            MyBase.HeaderBackColor = Color.FromArgb(86, 126, 211)
            MyBase.HeaderForeColor = Color.White
            MyBase.HeaderFont = gloGlobal.clsgloFont.gFont_BOLD ' New Font("Tahoma", 9, FontStyle.Bold)

            MyBase.SelectionForeColor = Color.Black
            MyBase.AllowSorting = True
            MyBase.ReadOnly = blnReadOnly
            MyBase.MappingName = strMappingName
            MyBase.PreferredColumnWidth = 125
            MyBase.PreferredRowHeight = 15
            MyBase.RowHeadersVisible = False
            MyBase.BackColor = Color.White
        End Sub

        Public WriteOnly Property RowHeaderVisible() As Boolean
            Set(ByVal Value As Boolean)
                MyBase.RowHeadersVisible = Value
            End Set
        End Property

        'Public ReadOnly Property MyGridColumnStyles() As System.Windows.Forms.GridColumnStylesCollection
        '    Get
        '        Return MyBase.GridColumnStyles
        '    End Get
        'End Property

    End Class
End Namespace