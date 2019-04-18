Imports System.Windows.Forms
Imports gloUserControlLibrary
Imports gloEMRReports

Imports gloEmdeonCommon.Form1
Namespace gloEmdeonCommon
    Class MainMenu
        Public Sub New()
            gstrMessageBoxCaption = GetMessageBoxCaption()
        End Sub
        Public Shared Sub SetFAXPrinterOutputDirectorySettings1(ByVal strfaxoutputdirectory As String)
            Try
                bsuccess = AxBlackIceDEVMODE1.SetOutputDirectory(strfaxoutputdirectory, pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling active X function: 'SetOutputDirectory'")
                End If
                bsuccess = AxBlackIceDEVMODE1.SaveBlackIceDEVMODE(gstrFAXPrinterName, pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error saving the devmode")
                    Exit Sub
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)

            End Try
        End Sub
        Public Shared Function SetFAXPrinterDefaultSettings1() As Boolean
            Try

                If gstrFAXPrinterName = "" Then
                    '    MessageBox.Show("You must set the Fax printer name for sending Fax", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    Dim frm As New frmSettings_New
                    '    frm.ShowDialog()
                    SetFAXPrinterDefaultSettings1 = Nothing
                    Exit Function
                End If
                Dim frm As Form1
                frm = New Form1()
                frm.Visible = False
                frm.ShowInTaskbar = False
                'frm.Show()
                frm.Visible = False
                If (IsNothing(AxBlackIceDEVMODE1)) Then
                    AxBlackIceDEVMODE1 = New AxBLACKICEDEVMODELib.AxBlackIceDEVMODE

                End If
                CType(AxBlackIceDEVMODE1, System.ComponentModel.ISupportInitialize).BeginInit()
                Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
                'Controls.Add(AxBlackIceDEVMODE1)
                AxBlackIceDEVMODE1.Enabled = True
                AxBlackIceDEVMODE1.Visible = False
                AxBlackIceDEVMODE1.Location = New System.Drawing.Point(594, 24)
                AxBlackIceDEVMODE1.Name = "AxBlackIceDEVMODE1"
                'AxBlackIceDEVMODE1.OcxState =
                AxBlackIceDEVMODE1.OcxState = CType(resources.GetObject("AxBlackIceDEVMODE1.OcxState"), System.Windows.Forms.AxHost.State)
                AxBlackIceDEVMODE1.Size = New System.Drawing.Size(16, 16)
                CType(AxBlackIceDEVMODE1, System.ComponentModel.ISupportInitialize).EndInit()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Initialize, " Initializing FAX Driver End", gloAuditTrail.ActivityOutCome.Success)

                pBlackIceDEVMODE = AxBlackIceDEVMODE1.LoadBlackIceDEVMODE(gstrFAXPrinterName)

                If pBlackIceDEVMODE = 0 Then
                    MsgBox("Cannot open '" & gstrFAXPrinterName & "' Printer driver", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                    '  F_AllControlEnabled((False))
                    SetFAXPrinterDefaultSettings1 = Nothing
                    Exit Function
                End If

                ' Output directory
                bsuccess = AxBlackIceDEVMODE1.SetOutputDirectory(gstrFAXOutputDirectory, pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'SetOutputDirectory'")
                End If

                ' File format TIFF group 4

                bsuccess = AxBlackIceDEVMODE1.SetFileFormat(7, pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'SetFileFormat'")
                End If


                ' Orientation (Potrait/Landscape

                bsuccess = AxBlackIceDEVMODE1.SetOrientation(1, pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'SetOrientation'")
                End If

                'disable generation of group file
                bsuccess = AxBlackIceDEVMODE1.DisableGroupFile(pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'DisableGroupFile'")
                End If

                ''if group file is generated disable deletion of group file
                bsuccess = AxBlackIceDEVMODE1.DisableDeleteGroupFile(pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'DisableDeleteGroupFile'")
                End If

                'The document will not be forced to be printed always using the printer’s resolution, regardless to the DPI setting stored in the document 
                bsuccess = AxBlackIceDEVMODE1.DisableForcePrinterDPI(pBlackIceDEVMODE)
                If Not bsuccess Then
                    MsgBox("Error in calling Active X function: 'DisableForcePrinterDPI'")
                End If

                bsuccess = AxBlackIceDEVMODE1.DisableAdvancedPaperSize(pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'DisableAdvancedPaperSize'")
                End If

                'vertical and horizontal resolution values.
                bsuccess = AxBlackIceDEVMODE1.SetXDPI(200, pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'SetXDPI'")
                End If
                bsuccess = AxBlackIceDEVMODE1.SetYDPI(200, pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'SetYDPI'")
                End If

                ' Color depth
                bsuccess = AxBlackIceDEVMODE1.SetColorDepth(BITS_8, pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    'MsgBox "Error in calling Active X function: 'SetColorDepth'"
                End If

                'If this box is checked, the driver will set the page number tag of every page in the output TIFF file.
                bsuccess = AxBlackIceDEVMODE1.EnablePageNumbering(pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'EnablePageNumbering'")
                End If

                '3 indicates Exact filename.
                'i.e., we ourself generate the filename, we are not using any Blackice filename generation method .
                bsuccess = AxBlackIceDEVMODE1.SetFileGenerationMethod(3, pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'SetFileGenerationMethod'")
                End If

                bsuccess = AxBlackIceDEVMODE1.DisableFaxOutput(pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'EnableFaxOutput'")
                End If

                'If this box is checked, the driver will set the page number tag of every page in the output TIFF file.
                bsuccess = AxBlackIceDEVMODE1.DisableWriteText(pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'DisableWriteText'")
                End If


                'GammaLink compatible TIFF output requires this setting to be checked, because they can only send TIFF images with reverse bit order.
                ' the driver will create a TIFF file that is compatible with the requirements listed in File Format for Internet Fax.
                bsuccess = AxBlackIceDEVMODE1.DisableInternetTiffFormat(pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'DisableInternetTiffformat'")
                End If

                'sarika 11/10/2007
                'if you are not capturing printer messages for end of printing, for example you can disable this in the printer options and it can get you a little more speed.
                bsuccess = AxBlackIceDEVMODE1.DisableMessagingInterface(pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'DisableMessagingInterface'")
                End If

                'The Photo Quality option enables or disables the dithering of the Black Ice driver.

                'code commented by sarika 21st nov 07
                bsuccess = AxBlackIceDEVMODE1.SetDithering(DITHER_SHARP, pBlackIceDEVMODE)
                '---
                ''code added by sarika 21st nov 07
                ' bsuccess = AxBlackIceDEVMODE1.SetDithering(DITHER_FS4, pBlackIceDEVMODE)
                '---
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'SetDithering'")
                End If

                ''code added by sarika 7th dec 07
                bsuccess = AxBlackIceDEVMODE1.EnableMultipageImage(pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling Active X function: 'EnableMultipageImage'")
                End If
                '---

                '' ''code added by sarika sarika 7th dec 07
                ''bsuccess = AxBlackIceDEVMODE1.DisableMultipageImage(pBlackIceDEVMODE)
                ''If (bsuccess = False) Then
                ''    MsgBox("Error in calling Active X function: 'EnableMultipageImage'")
                ''End If
                ' ''---

                'to save the settings applied to the black ice printer deriver
                ' The Smooth, Sharp, and Stucki filters produce better quality output.
                bsuccess = AxBlackIceDEVMODE1.SaveBlackIceDEVMODE(gstrFAXPrinterName, pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error saving the devmode")
                    SetFAXPrinterDefaultSettings1 = Nothing
                    Exit Function
                End If

                frm = Nothing
                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            Finally

            End Try


        End Function
        Public Shared Function SetFAXPrinterDocumentSettings1(ByVal strFAXDocumentName As String) As Boolean
            Try
                'If gstrFAXPrinterName = "" Then
                '    MessageBox.Show("Please select the Fax printer name", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Return False
                'End If

                bsuccess = AxBlackIceDEVMODE1.SetImageFileName(strFAXDocumentName & ".tif", pBlackIceDEVMODE)

                If (bsuccess = False) Then
                    MsgBox("Error in calling active X function: 'SetImageFileName'")
                End If

                'all the files will be appended in a single tiff file
                bsuccess = AxBlackIceDEVMODE1.EnableKeepExistingFiles(pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    MsgBox("Error in calling active X function: 'EnableKeepExistingFiles'")
                End If

                bsuccess = AxBlackIceDEVMODE1.SaveBlackIceDEVMODE(gstrFAXPrinterName, pBlackIceDEVMODE)
                If (bsuccess = False) Then
                    'MsgBox("Error saving the devmode")
                    Return False
                    Exit Function
                End If
                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                Return False
            Finally


            End Try
        End Function

    End Class

    Public Class EmdeonCommon1
       
        'Private Sub MenuEvent_Fax()
        '    Try
        '        _OrderParamter.CloseAfterSave = False
        '        blnSaved = True
        '        MenuEvent_Save(0)
        '        If blnSaved = False Then
        '            'order not saved 
        '            Exit Sub
        '        End If
        '        _OrderParamter.IsEditMode = True
        '        'sarika Labs Denormalization 20090323
        '        '            FaxLabOrder(_OrderParamter.OrderID, oLabActor_Order1.ArrTestID)
        '        FaxLabOrder(_OrderParamter.OrderID, oLabActor_Order1.ArrTestName)

        '        '--
        '        _OrderParamter.CloseAfterSave = True
        '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Fax, "Lab Request Orders fax", gloAuditTrail.ActivityOutCome.Success)
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End Try
        'End Sub
    End Class
End Namespace