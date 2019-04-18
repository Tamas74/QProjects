Imports System.IO
Imports System.Windows.Forms

Public Class frmAboutus


    Private Sub frmAboutus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

      
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName
        Dim strDate As String = Format(System.IO.File.GetLastWriteTime(System.Windows.Forms.Application.StartupPath & "\" & aModuleName), "dd MMM, yyyy") & " " & Format(System.IO.File.GetLastWriteTime(System.Windows.Forms.Application.StartupPath & "\" & aModuleName), "hh:mm:ss tt")
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).

        ' original' Me.LabelProductName.Text = My.Application.Info.ProductName & " " & "4.0"
        'by vishal
        Me.lblProductName.Text = Application.ProductVersion
        'Me.lblProductName.Text = My.Application.Info.ProductName & " " & My.Application.Info.Version.ToString()
        'Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.lblCopyRight.Text = My.Application.Info.Copyright
        Me.lblCompanyName.Text = My.Application.Info.CompanyName
        ' Me.lblGlostreamLink.Text = My.Application.Info.Description
        Me.lblModifiedDate.Text = "Software Last Modified " & strDate


        Dim oFile As New FileInfo(Application.StartupPath + "\\RxSniffer.EXE")
        If oFile.Exists = True Then
            Dim oFileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(oFile.FullName)
            lblBuildID.Text = oFileVersionInfo.FileVersion
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
