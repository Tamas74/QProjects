Imports System.IO

Public Class frmLoadDashBoard


    Private Sub frmLoadDashBoard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '09-Oct-14 Aniket: Show major version E.g. 8.X on the splash screen
        ' Dim objGlobalMisc As New gloGlobal.clsMISC

        Try

            Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName
            lbl_mktngversion.Refresh()
            lblDescription.Refresh()
            lblLastModifiedDate.Text = Format(File.GetLastWriteTime(gstrgloEMRStartupPath & "\" & aModuleName), "MMM dd, yyyy")
            '09-Oct-14 Aniket: Show major version E.g. 8.X on the splash screen
            lbl_mktngversion.Text = gloGlobal.clsMISC.GetMajorVersion(Application.ProductVersion)
            lbl_mktngversion.Refresh()
            lblCopyrghTag.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightMain
            lblCopyrghTag.Refresh()
            Label2.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightSub
            Label2.Refresh()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


End Class