Imports System
Imports System.Drawing

Public Class gloC1FlexStyle
    Inherits C1.Win.C1FlexGrid.C1FlexGrid

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    'Shared gFont As Font = New Font("Tahoma", 9, FontStyle.Regular)
    'Shared gFont_SMALL As Font = New Font("Tahoma", 8.25!, FontStyle.Regular)
    'Shared gFont_SMALL_BOLD As Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
    'Shared gFont_BOLD = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

    Public Shared Sub Style(ByVal FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid, Optional ByVal blnShowCellLabels As Boolean = False)

 

        FlexGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        FlexGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row

        '' Normal Style
        FlexGrid.Styles.Normal.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        FlexGrid.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        FlexGrid.Styles.Normal.Font = gloGlobal.clsgloFont.gFont '' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        FlexGrid.Styles.Normal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))

        '' Alternet Style
        FlexGrid.Styles.Alternate.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        FlexGrid.Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        FlexGrid.Styles.Alternate.Font = gloGlobal.clsgloFont.gFont '' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        FlexGrid.Styles.Alternate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        'shubhangi 20100407 TO SET ALLIGNMENT OF HEADER TO LEFT
        If FlexGrid.Rows.Count > 0 Then
            FlexGrid.Rows(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        End If



        '' Fixed Style
        FlexGrid.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        FlexGrid.Styles.Fixed.Border.Color = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        FlexGrid.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD '' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        FlexGrid.Styles.Fixed.ForeColor = Color.White

        '' Heighlight Style
        FlexGrid.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        FlexGrid.Styles.Highlight.Border.Color = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        FlexGrid.Styles.Highlight.Font = gloGlobal.clsgloFont.gFont ''New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        FlexGrid.Styles.Highlight.ForeColor = Color.Black

        '' Focus Style
        FlexGrid.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        FlexGrid.Styles.Focus.Border.Color = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        FlexGrid.Styles.Focus.Font = gloGlobal.clsgloFont.gFont_BOLD '' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        FlexGrid.Styles.Focus.ForeColor = Color.Black

        '' EDITOR Style
        FlexGrid.Styles.Editor.BackColor = System.Drawing.Color.Beige
        FlexGrid.Styles.Editor.Border.Color = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        FlexGrid.Styles.Editor.Font = gloGlobal.clsgloFont.gFont ''New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        FlexGrid.Styles.Editor.ForeColor = Color.Black

        '' Search Style
        FlexGrid.Styles.Search.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        FlexGrid.Styles.Search.Border.Color = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        FlexGrid.Styles.Search.Font = gloGlobal.clsgloFont.gFont '' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        FlexGrid.Styles.Search.ForeColor = Color.White

        '' Frozen Style
        FlexGrid.Styles.Frozen.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        FlexGrid.Styles.Frozen.Border.Color = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        FlexGrid.Styles.Frozen.Font = gloGlobal.clsgloFont.gFont ''New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        FlexGrid.Styles.Frozen.ForeColor = Color.Black

        '' New Row Style
        FlexGrid.Styles.NewRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        FlexGrid.Styles.NewRow.Border.Color = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        FlexGrid.Styles.NewRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        FlexGrid.Styles.NewRow.Font = gloGlobal.clsgloFont.gFont '' New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        '' Empty Area Style
        FlexGrid.Styles.EmptyArea.BackColor = System.Drawing.Color.White ''System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        FlexGrid.Styles.EmptyArea.Border.Color = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))  ''System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        FlexGrid.Styles.EmptyArea.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))




        FlexGrid.ShowCellLabels = blnShowCellLabels


    End Sub

    Public Shared Sub ShowToolTip(ByVal oC1ToolTip As C1.Win.C1SuperTooltip.C1SuperTooltip, ByVal oGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal nLocation As System.Drawing.Point)
        Try
            Dim myFont As Font = oGrid.Font
            Dim stringsize As SizeF
            Dim colsize As Integer = 0
            Dim sText As String = ""
            Dim nRow As Integer
            Dim nCol As Integer

            If oGrid.MouseCol > -1 And oGrid.MouseRow > -1 Then
                oC1ToolTip.Font = myFont
                oC1ToolTip.MaximumWidth = 400

                nRow = oGrid.MouseRow
                nCol = oGrid.MouseCol

                If nRow > 0 Then 'And nCol > 0 Then
                    If Not oGrid.GetData(nRow, nCol) Is Nothing Then
                        'sText = oGrid.GetData(nRow, nCol).ToString()
                        'TO RESOLVED 8821
                        sText = oGrid.GetData(nRow, nCol)
                    End If
                    colsize = oGrid.Cols(nCol).WidthDisplay

                End If
                Dim oGrp As Graphics = oGrid.CreateGraphics()
                Dim chars As Integer
                Dim lines As Integer
                stringsize = oGrp.MeasureString(sText.ToString(), myFont, SizeF.Empty, StringFormat.GenericDefault(), chars, lines)
                ''Code Review Changes: Dispose Graphics object
                oGrp.Dispose()
                '' oGrid.GetCellRect(nRow, nCol).Height
                'If stringsize.Width > colsize Or lines > 1 And oGrid.GetCellRect(nRow, nCol).Height < (19 * lines) Then
                If stringsize.Width > colsize Or lines > 1 Then
                    'oC1ToolTip.SetToolTip(oGrid, sText.ToString())
                    ''TO RESOLVED 8821
                    oC1ToolTip.SetToolTip(oGrid, sText)
                Else
                    oC1ToolTip.SetToolTip(oGrid, "")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
