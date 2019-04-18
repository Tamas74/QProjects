Imports System.Printing

Module mdlcreatetiff




    Public Function IsTiffGenerated(ByVal _sFAXPrinter As String) As Boolean


        'code comment start by nilesh on 20110106
        '        While System.IO.File.Exists(FileName) = False

        'Open:
        '            Try
        '                Rename(FileName, FileName)
        '            Catch ex As Exception
        '                GoTo Open
        '            End Try
        '            'ProgressBar1.Value = ProgressBar1.Value + ProgressBar1.Step
        '            'If ProgressBar1.Value = ProgressBar1.Maximum Then
        '            '    ProgressBar1.Value = 0
        '            'End If
        '        End While
        'code comment end by nilesh on 20110106

        'code start by nilesh on 20110528
        'below code verify the printing queue and status of each document present in queue. this will put wait until all documents get printed successfully.

        '/////////////////////////////////////
        'START CODE
        'Dim jobCompletedValue As Boolean = True

        'Dim retVal As System.Windows.Forms.DialogResult
        'Dim retString As String = String.Empty
        'Dim enumerationFlags() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections}
        '' USer enum   PrintingCompleted = 0 ;         PrintingOK = 1;        PrintingError = 2
        'Dim myStatus As MYPrintingStatus

        'Try
        '    Using myPrintServer As PrintServer = New PrintServer()
        '        If Not IsNothing(myPrintServer) Then

        '            Using myPrintQueues As PrintQueueCollection = myPrintServer.GetPrintQueues(enumerationFlags)

        '                If Not IsNothing(myPrintQueues) Then
        '                    For Each pq As PrintQueue In myPrintQueues
        '                        If pq.FullName <> _sFAXPrinter Then Continue For

        '                        pq.Refresh()
        '                        Using jobs As PrintJobInfoCollection = pq.GetPrintJobInfoCollection()
        '                            If Not IsNothing(jobs) Then
        '                                For Each job As PrintSystemJobInfo In jobs
        '                                    If Not IsNothing(job) Then
        '                                        Do
        '                                            retString = String.Empty

        '                                            job.Refresh()
        '                                            Application.DoEvents()

        '                                            myStatus = SpotTroubleUsingJobAttributes(job, retString)

        '                                            Select Case myStatus
        '                                                Case MYPrintingStatus.PrintingCompleted
        '                                                    Exit Do

        '                                                Case MYPrintingStatus.PrintingOK
        '                                                    Continue Do

        '                                                Case MYPrintingStatus.PrintingError
        '                                                    retVal = MessageBox.Show(retString, "Do you want to Continue?", MessageBoxButtons.YesNo)

        '                                                    If (retVal = System.Windows.Forms.DialogResult.Yes) Then
        '                                                        Continue Do
        '                                                    Else
        '                                                        jobCompletedValue = False
        '                                                        Exit Do
        '                                                    End If

        '                                            End Select
        '                                        Loop

        '                                        If Not IsNothing(job) Then
        '                                            job.Dispose()
        '                                        End If
        '                                    End If
        '                                Next

        '                                If Not IsNothing(jobs) Then
        '                                    jobs.Dispose()
        '                                End If
        '                            End If
        '                        End Using

        '                    Next

        '                    If Not IsNothing(myPrintQueues) Then
        '                        myPrintQueues.Dispose()
        '                    End If
        '                Else
        '                    jobCompletedValue = False
        '                End If

        '            End Using

        '            If Not IsNothing(myPrintServer) Then
        '                myPrintServer.Dispose()
        '            End If
        '        Else
        '            MessageBox.Show("Printer not found", "gloEMR Fax", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            jobCompletedValue = False
        '        End If
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, "gloEMR Fax", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    jobCompletedValue = False
        'Finally
        '    retString = String.Empty

        'End Try
        'END CODE

        'value jobCompletedValue to get if tiff is generated with options on fax Printer,
        Dim jobCompletedValue As Boolean = True

        Dim retVal As System.Windows.Forms.DialogResult
        Dim retString As String = String.Empty
        Dim enumerationFlags() As EnumeratedPrintQueueTypes = {EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections}
        ' EnumeratedPrintQueueTypes.Local - Gives local Print Queue in machine, ' EnumeratedPrintQueueTypes.Connections - Gives queued jobs from other machines to this Print server, 

        ' USer enum   PrintingCompleted = 0 ;         PrintingOK = 1;        PrintingError = 2
        Dim myStatus As MYPrintingStatus

        Try
            Using myPrintServer As PrintServer = New PrintServer()
                If Not IsNothing(myPrintServer) Then

                    Using myPrintQueues As PrintQueueCollection = myPrintServer.GetPrintQueues(enumerationFlags)

                        If Not IsNothing(myPrintQueues) Then
                            For Each pq As PrintQueue In myPrintQueues
                                If pq.FullName <> _sFAXPrinter Then Continue For

                                pq.Refresh()
                                Using jobs As PrintJobInfoCollection = pq.GetPrintJobInfoCollection()
                                    If Not IsNothing(jobs) Then
                                        For Each job As PrintSystemJobInfo In jobs
                                            If Not IsNothing(job) Then
                                                Do
                                                    retString = String.Empty

                                                    job.Refresh()
                                                    Application.DoEvents()

                                                    myStatus = SpotTroubleUsingJobAttributes(job, retString)

                                                    Select Case myStatus
                                                        Case MYPrintingStatus.PrintingCompleted
                                                            Exit Do

                                                        Case MYPrintingStatus.PrintingOK
                                                            Continue Do

                                                        Case MYPrintingStatus.PrintingError
                                                            retVal = MessageBox.Show(retString & Environment.NewLine & "Do you want to wait till job complete?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

                                                            If (retVal = System.Windows.Forms.DialogResult.Yes) Then
                                                                Continue Do
                                                            Else
                                                                jobCompletedValue = False
                                                                Exit Do
                                                            End If

                                                    End Select
                                                Loop

                                                'If Not IsNothing(job) Then
                                                '    job.Dispose()
                                                'End If
                                            End If
                                        Next

                                        'If Not IsNothing(jobs) Then
                                        '    jobs.Dispose()
                                        'End If
                                    End If
                                End Using

                            Next

                            'If Not IsNothing(myPrintQueues) Then
                            '    myPrintQueues.Dispose()
                            'End If
                        Else
                            jobCompletedValue = False
                        End If

                    End Using

                    'If Not IsNothing(myPrintServer) Then
                    '    myPrintServer.Dispose()
                    'End If
                Else
                    MessageBox.Show("Printer not found", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    jobCompletedValue = False
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            jobCompletedValue = False
        Finally
            retString = String.Empty

        End Try

        Return jobCompletedValue
    End Function

    Private Enum MYPrintingStatus As Integer
        PrintingCompleted = 0
        PrintingOK = 1
        PrintingError = 2
    End Enum

    Private Function SpotTroubleUsingJobAttributes(ByVal theJob As PrintSystemJobInfo, ByRef retString As String) As MYPrintingStatus
        Try

            'Dim retString As String = "\r\n"
            If ((theJob.JobStatus And PrintJobStatus.Completed) = PrintJobStatus.Completed) OrElse ((theJob.JobStatus And PrintJobStatus.Printed) = PrintJobStatus.Printed) Then
                retString = ""
                Return MYPrintingStatus.PrintingCompleted
            End If
            If ((theJob.JobStatus And PrintJobStatus.Deleted) = PrintJobStatus.Deleted) OrElse ((theJob.JobStatus And PrintJobStatus.Deleting) = PrintJobStatus.Deleting) Then
                retString = ""
                Return MYPrintingStatus.PrintingCompleted
            End If


            If (theJob.JobStatus And PrintJobStatus.Blocked) = PrintJobStatus.Blocked Then
                retString += "The job is blocked." & Environment.NewLine
            End If

            If (theJob.JobStatus And PrintJobStatus.Error) = PrintJobStatus.Error Then
                retString += "The job has errored." & Environment.NewLine
            End If

            If (theJob.JobStatus And PrintJobStatus.Offline) = PrintJobStatus.Offline Then
                retString += "The printer " & theJob.HostingPrintQueue.Name.ToString & " is offline." & Environment.NewLine
            End If

            If (theJob.JobStatus And PrintJobStatus.PaperOut) = PrintJobStatus.PaperOut Then
                retString += "The printer " & theJob.HostingPrintQueue.Name.ToString & " is out of paper of the size required by the job." & Environment.NewLine
            End If

            If ((theJob.JobStatus And PrintJobStatus.Paused) = PrintJobStatus.Paused) OrElse ((theJob.HostingPrintQueue.QueueStatus And PrintQueueStatus.Paused) = PrintQueueStatus.Paused) Then
                retString += "The printer " & theJob.HostingPrintQueue.Name.ToString & " is Paused." & Environment.NewLine
                'HandlePausedJob is defined in the complete example.
            End If

            If (theJob.JobStatus And PrintJobStatus.UserIntervention) = PrintJobStatus.UserIntervention Then
                retString += "The printer " & theJob.HostingPrintQueue.Name.ToString & " needs human intervention." & Environment.NewLine
            End If

            If (theJob.JobStatus And PrintJobStatus.Printing) = PrintJobStatus.Printing Then
                retString += "The job is printing now." & Environment.NewLine
                Return MYPrintingStatus.PrintingOK
            End If

            If (theJob.JobStatus And PrintJobStatus.Spooling) = PrintJobStatus.Spooling Then
                retString += "The job is spooling now." & Environment.NewLine
                Return MYPrintingStatus.PrintingOK
            End If



            Return MYPrintingStatus.PrintingError

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function 'end SpotTroubleUsingJobAttributes

    'code end by nilesh on 20110528



End Module
