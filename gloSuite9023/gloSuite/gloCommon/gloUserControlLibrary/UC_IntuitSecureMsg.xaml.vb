Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows
Imports System.Windows.Controls


Public Class UC_IntuitSecureMsg
    Dim oslectedBorder As Border
    Private Sub Border2_MouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs)
        Dim obd As Border = DirectCast(sender, Border)
        Dim sb = DirectCast(FindResource("sbBorderEnter"), Storyboard).Clone()
        Storyboard.SetTarget(sb, obd)
        sb.Begin()
    End Sub

    'Private Sub Border1_MouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles Border1.MouseEnter
    '    Dim obd As Border = DirectCast(sender, Border)
    '    Dim sb = DirectCast(FindResource("sbBorderEnter"), Storyboard).Clone()
    '    Storyboard.SetTarget(sb, obd)
    '    sb.Begin()
    'End Sub

    Private Sub UC_IntuitSecureMsg_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Try

            cmbType.Items.Add("Type 1")
            cmbType.Items.Add("Type 2")

            For i As Integer = 0 To 1



                Dim ostackMain As StackPanel, ostackInner As StackPanel
                Dim olbl As TextBlock
                Dim oimgAckw As Image


                Dim obrd As Border
                Dim selectedIndex As Int16 = 0
                Dim internalOrderID As Int64 = 0
                obrd = New Border()
                '' obrd.Tag = Convert.ToString(dt.Rows(i)("OrderID"))
                obrd.Style = DirectCast(FindResource("BorderBackroundStyle"), Style)
                obrd.BorderThickness = New Thickness(1)
                obrd.CornerRadius = New CornerRadius(6)
                obrd.BorderBrush = Brushes.Black
                RemoveHandler obrd.MouseEnter, AddressOf obrd_MouseEnter
                AddHandler obrd.MouseEnter, AddressOf obrd_MouseEnter
                ''  AddHandler obrd.MouseDown, AddressOf obrd_MouseDown
                RemoveHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged
                AddHandler lstAttachment.SelectionChanged, AddressOf lstAttachment_SelectionChanged


                ostackMain = New StackPanel()
                ostackMain.Orientation = Orientation.Vertical
                ostackMain.Margin = New Thickness(2)
                ostackMain.CanVerticallyScroll = True
                ostackInner = New StackPanel()
                ostackInner.Orientation = Orientation.Horizontal
                ostackInner.Margin = New Thickness(1)
                ostackInner.CanVerticallyScroll = True
                '' ostackInner.Style = DirectCast(FindResource("BorderBackroundStyle"), Style)
                oimgAckw = New Image()
                oimgAckw.Margin = New Thickness(5, 0, 5, 0)
                oimgAckw.Source = New System.Windows.Media.Imaging.BitmapImage(New Uri("/gloUserControlLibrary;component/Image/Forward Email.png", UriKind.Relative))
                oimgAckw.Height = 20
                oimgAckw.Width = 20
                oimgAckw.Stretch = Stretch.Fill
                oimgAckw.HorizontalAlignment = Windows.HorizontalAlignment.Left
                ostackInner.Children.Add(oimgAckw)

                'OrderName
                olbl = New TextBlock()
                olbl.Margin = New Thickness(2, 0, 0, 0)
                olbl.TextWrapping = TextWrapping.Wrap
                olbl.Width = 215
                olbl.Foreground = Brushes.Black
                olbl.FontFamily = New FontFamily("Tahoma")

                olbl.Text = "Attachment 1"
                olbl.HorizontalAlignment = Windows.HorizontalAlignment.Left
                ostackInner.Children.Add(olbl)

                ostackMain.Children.Add(ostackInner)

                obrd.Child = ostackMain

                lstAttachment.ScrollIntoView(lstAttachment)
                lstAttachment.Items.Add(obrd)

                lstAttachment.SelectedIndex = 0


            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub
    Private Sub obrdinner_Mouseenter(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Try


            Dim obd As Border = DirectCast(sender, Border)
            Dim sb = DirectCast(FindResource("sbBorderEnter"), Storyboard).Clone()
            Storyboard.SetTarget(sb, obd)
            sb.Begin()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub obrd_MouseEnter(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim obd As Border = DirectCast(sender, Border)
        Dim sb As New Storyboard
        Try
            sb = DirectCast(FindResource("sbBorderEnter"), Storyboard).Clone()
            Storyboard.SetTarget(sb, obd)
            sb.Begin()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            If IsNothing(obd) = False Then
                obd = Nothing
            End If
            If IsNothing(sb) = False Then
                sb = Nothing
            End If
        End Try
    End Sub

    Private Sub lstAttachment_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) 'Handles lstLabTest.SelectionChanged
        Try
            If lstAttachment.Items.Count > 0 Then
                If oslectedBorder IsNot Nothing Then
                    oslectedBorder.Style = DirectCast(FindResource("BorderBackroundStyle"), Style)
                End If

                Dim oitem As Border = DirectCast(lstAttachment.SelectedItem, Border)
                oitem.Style = DirectCast(FindResource("BorderSelectedStyle"), Style)
                oslectedBorder = oitem

            End If

        Catch exc As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), False)
        End Try
    End Sub




  
    Private Sub btnAttachment_MouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles btnAttachment.MouseEnter
        Dim obd As New Button
        Try


            obd = DirectCast(sender, Button)
            Dim sb = DirectCast(FindResource("sbBorderEnter"), Storyboard).Clone()
            Storyboard.SetTarget(sb, obd)
            sb.Begin()
            btnAttachment.FontWeight = FontWeights.Bold
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            If IsNothing(obd) = False Then
                obd = Nothing
            End If
        End Try
    End Sub

    Private Sub btnAttachment_MouseLeave(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles btnAttachment.MouseLeave
        btnAttachment.FontWeight = FontWeights.Normal
    End Sub
End Class
