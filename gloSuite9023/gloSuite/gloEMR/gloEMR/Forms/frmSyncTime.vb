Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.IO


Public Class frmSyncTime
    Inherits System.Windows.Forms.Form

#Region "Class Attributes"
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SYSTEMTIME
        Public wYear As Int16
        Public wMonth As Int16
        Public wDayOfWeek As Int16
        Public wDay As Int16
        Public wHour As Int16
        Public wMinute As Int16
        Public wSecond As Int16
        Public wMilliseconds As Int16
    End Structure

    Structure TIME_OF_DAY_INFO
        Dim tod_elapsedt As Integer
        Dim tod_msecs As Integer
        Dim tod_hours As Integer
        Dim tod_mins As Integer
        Dim tod_secs As Integer
        Dim tod_hunds As Integer
        Dim tod_timezone As Integer
        Dim tod_tinterval As Integer
        Dim tod_day As Integer
        Dim tod_month As Integer
        Dim tod_year As Integer
        Dim tod_weekday As Integer
    End Structure

    Private Declare Unicode Function NetRemoteTOD Lib "netapi32" ( _
           <MarshalAs(UnmanagedType.LPWStr)> ByVal ServerName As String, _
           ByRef BufferPtr As IntPtr) As Integer

    Private Declare Function NetApiBufferFree Lib "netapi32" (ByVal Buffer As IntPtr) As Integer
    'Private bShowLogger As Boolean = True

    Private bExceptionOccured As Boolean = False
    Private localDate As DateTime
    Private serverDate As DateTime
    Private LocalTimeBeforeSync As DateTime
    'Private SelectedServerName As Skkeypair
    Dim IsformLoad As Boolean = False
    Dim IsTimerEnabled As Boolean = False
    Dim IsTimerExecuting As Boolean = False
    Dim sMessageToDisplay As String = Nothing

    Private WithEvents Timer As New Timer
#End Region


    <DllImport("kernel32.dll"), Runtime.ConstrainedExecution.ReliabilityContract(Runtime.ConstrainedExecution.Consistency.WillNotCorruptState, Runtime.ConstrainedExecution.Cer.Success), System.Security.SuppressUnmanagedCodeSecurity()> _
    Private Shared Function SetSystemTime(ByRef stru As SYSTEMTIME) As <MarshalAs(UnmanagedType.U2)> Int16

    End Function


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
    Friend WithEvents txtServerName As System.Windows.Forms.TextBox
    Friend WithEvents lblServerName As System.Windows.Forms.Label
    Friend WithEvents lblRemoteDateTimeValue As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblLocalSystemDateTime As System.Windows.Forms.Label
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tlsp_SyncTime As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbServerName As System.Windows.Forms.ComboBox
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSyncTime))
        Me.txtServerName = New System.Windows.Forms.TextBox()
        Me.lblServerName = New System.Windows.Forms.Label()
        Me.lblRemoteDateTimeValue = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblLocalSystemDateTime = New System.Windows.Forms.Label()
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel()
        Me.tlsp_SyncTime = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbServerName = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tlsp_SyncTime.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtServerName
        '
        Me.txtServerName.BackColor = System.Drawing.SystemColors.Control
        Me.txtServerName.Enabled = False
        Me.txtServerName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServerName.Location = New System.Drawing.Point(382, 41)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(13, 22)
        Me.txtServerName.TabIndex = 3
        Me.txtServerName.Visible = False
        '
        'lblServerName
        '
        Me.lblServerName.AutoSize = True
        Me.lblServerName.BackColor = System.Drawing.Color.Transparent
        Me.lblServerName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblServerName.Location = New System.Drawing.Point(29, 44)
        Me.lblServerName.Name = "lblServerName"
        Me.lblServerName.Size = New System.Drawing.Size(116, 14)
        Me.lblServerName.TabIndex = 2
        Me.lblServerName.Text = "Time Server Name :"
        Me.lblServerName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRemoteDateTimeValue
        '
        Me.lblRemoteDateTimeValue.AutoSize = True
        Me.lblRemoteDateTimeValue.BackColor = System.Drawing.Color.Transparent
        Me.lblRemoteDateTimeValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemoteDateTimeValue.Location = New System.Drawing.Point(150, 77)
        Me.lblRemoteDateTimeValue.Name = "lblRemoteDateTimeValue"
        Me.lblRemoteDateTimeValue.Size = New System.Drawing.Size(0, 14)
        Me.lblRemoteDateTimeValue.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(64, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 14)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Server Time :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(28, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Local System Time :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLocalSystemDateTime
        '
        Me.lblLocalSystemDateTime.AutoSize = True
        Me.lblLocalSystemDateTime.BackColor = System.Drawing.Color.Transparent
        Me.lblLocalSystemDateTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocalSystemDateTime.Location = New System.Drawing.Point(150, 109)
        Me.lblLocalSystemDateTime.Name = "lblLocalSystemDateTime"
        Me.lblLocalSystemDateTime.Size = New System.Drawing.Size(0, 14)
        Me.lblLocalSystemDateTime.TabIndex = 9
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tlsp_SyncTime)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(401, 53)
        Me.pnl_tlspTOP.TabIndex = 12
        '
        'tlsp_SyncTime
        '
        Me.tlsp_SyncTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tlsp_SyncTime.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_SyncTime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_SyncTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_SyncTime.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_SyncTime.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tlsp_SyncTime.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_SyncTime.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_SyncTime.Name = "tlsp_SyncTime"
        Me.tlsp_SyncTime.Size = New System.Drawing.Size(401, 53)
        Me.tlsp_SyncTime.TabIndex = 0
        Me.tlsp_SyncTime.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(72, 50)
        Me.ts_btnOk.Tag = "Sync Time"
        Me.ts_btnOk.Text = "&Sync Time"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Synchronize Time"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmbServerName)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Controls.Add(Me.lblServerName)
        Me.Panel1.Controls.Add(Me.txtServerName)
        Me.Panel1.Controls.Add(Me.lblLocalSystemDateTime)
        Me.Panel1.Controls.Add(Me.lblRemoteDateTimeValue)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(401, 137)
        Me.Panel1.TabIndex = 13
        '
        'cmbServerName
        '
        Me.cmbServerName.FormattingEnabled = True
        Me.cmbServerName.Location = New System.Drawing.Point(147, 41)
        Me.cmbServerName.Name = "cmbServerName"
        Me.cmbServerName.Size = New System.Drawing.Size(229, 22)
        Me.cmbServerName.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(12, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(350, 14)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Synchronize the local system time with the Server time."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 133)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(393, 1)
        Me.lbl_BottomBrd.TabIndex = 13
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 130)
        Me.lbl_LeftBrd.TabIndex = 12
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(397, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 130)
        Me.lbl_RightBrd.TabIndex = 11
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(395, 1)
        Me.lbl_TopBrd.TabIndex = 10
        Me.lbl_TopBrd.Text = "label1"
        '
        'frmSyncTime
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(401, 190)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSyncTime"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Synchronize Time"
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tlsp_SyncTime.ResumeLayout(False)
        Me.tlsp_SyncTime.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub ErrorOccured()
        If Timer IsNot Nothing Then
            Timer.Stop()
        End If

        Me.bExceptionOccured = True
        Me.lblRemoteDateTimeValue.Text = String.Empty
    End Sub

    Private Sub ResetError()
        Me.bExceptionOccured = False
    End Sub



    'Private Sub btnGetDateTime_Click_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'Dim dRemoteDate As Date
    '    'dRemoteDate = GetNetRemoteTOD(txtServerName.Text)
    '    'lblRemoteDateTimeValue.Text = dRemoteDate.ToString
    '    'Button1_Click(sender, e)
    'End Sub

    Private Function GetServerNameFromDB() As String

        Dim result As String = String.Empty
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try
            oDB.Connect(False)
            Dim _strSqlQuery As String = "SELECT ISNULL(sSettingsValue,'') as sSettingsValue FROM Settings WHERE sSettingsName='TIMESERVER'"
            result = oDB.ExecuteScalar_Query(_strSqlQuery)
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return result
    End Function

    Private Sub frmSyncTime_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.Timer IsNot Nothing Then
            Timer.Stop()
            Timer.Dispose()
            Timer = Nothing
        End If
    End Sub

    Private Sub frmSyncTime_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        IsformLoad = True
        Dim ServerList As New List(Of ServerNameKeyPair)
        Dim networkServer As String = Nothing
        Dim networkArray() As String = Nothing

        'txtServerName.Text = Get_ServerName()
        txtServerName.Text = GetServerNameFromDB()

        If txtServerName.Text.Length > 0 Then
            ServerList.Add(New ServerNameKeyPair(txtServerName.Text, "Local"))
        End If

        networkServer = Get_NetworkServerName()

        If Not IsNothing(networkServer) Then
            If networkServer <> "" Then
                networkArray = Split(networkServer, "|")
                If Not IsNothing(networkArray) Then
                    If networkArray.Count > 0 Then
                        For index As Integer = 0 To networkArray.Count - 1
                            ServerList.Add(New ServerNameKeyPair(networkArray(index), "Network"))
                        Next
                    End If
                End If
            End If

        End If

        cmbServerName.DataSource = ServerList
        cmbServerName.DisplayMember = "sName"
        cmbServerName.ValueMember = "sType"
        cmbServerName.SelectedIndex = -1
        IsformLoad = False

        If cmbServerName.SelectedValue = "Local" Then
            Timer.Interval = 1000
        End If
    End Sub

    Private Sub TimerEvent() Handles Timer.Tick
        Timer.Stop()
        IsTimerEnabled = False
        IsTimerExecuting = True

        SetLocalLabelTime()
        SetServerLabelTime()
        SyncTime()

        SetLocalLabelTime()
        SetServerLabelTime()

        IsTimerEnabled = True
        IsTimerExecuting = False

        Timer.Start()

    End Sub

    Private Function Get_NetworkServerName() As String

        Dim result As Object = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try
            oDB.Connect(False)
            Dim _strSqlQuery As String = "SELECT  sSettingsValue FROM Settings WHERE sSettingsName='NTPServerList'"
            result = oDB.ExecuteScalar_Query(_strSqlQuery)
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try

        Return Convert.ToString(result)
    End Function

    ' stackoverflow.com/a/3294698/162671
    Private Shared Function SwapEndianness(x As ULong) As UInteger
        Return CUInt(((x And &HFF) << 24) + ((x And &HFF00) << 8) + ((x And &HFF0000) >> 8) + ((x And &HFF000000UI) >> 24))
    End Function

#Region "Sync Time"

    Private Sub SyncTime()

        Dim TicksDifference As Int64 = 0

        If Not Me.bExceptionOccured Then
            Dim timeStru As SYSTEMTIME
            Dim result As Int32
            Dim dt As DateTime = serverDate.ToUniversalTime

            LocalTimeBeforeSync = DateTime.Now
            TicksDifference = LocalTimeBeforeSync.Ticks - localDate.Ticks
            dt.AddTicks(TicksDifference)

            timeStru.wYear = CType(dt.Year, Int16)
            timeStru.wMonth = CType(dt.Month, Int16)
            timeStru.wDay = CType(dt.Day, Int16)
            timeStru.wDayOfWeek = CType(dt.DayOfWeek, Int16)
            timeStru.wHour = CType(dt.Hour, Int16)
            timeStru.wMinute = CType(dt.Minute, Int16)
            timeStru.wSecond = CType(dt.Second, Int16)
            timeStru.wMilliseconds = CType(dt.Millisecond, Int16)
            result = SetSystemTime(timeStru)

            localDate = DateTime.Now
            serverDate = dt.ToLocalTime

            WriteAuditLog(cmbServerName.Text)

            timeStru = Nothing

        End If
    End Sub


#End Region

#Region "Combobox Selected Changed"

    Private Sub cmbServerName_Click(sender As Object, e As System.EventArgs) Handles cmbServerName.Click
        If Timer IsNot Nothing Then
            Timer.Stop()
        End If
    End Sub

    Private Sub cmbServerName_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbServerName.SelectedIndexChanged
        Try
            Timer.Stop()
            Me.Cursor = Cursors.WaitCursor
            lblLocalSystemDateTime.Text = System.DateTime.Now
            If (IsformLoad = False) Then
                If (cmbServerName.SelectedIndex <> -1) Then
                    If (cmbServerName.Text <> "") Then

                        SetServerLabelTime()

                        If bExceptionOccured Then
                            ErrorOccured()
                        Else
                            SyncTime()
                            ShowDisplayMessage(LocalTimeBeforeSync, serverDate)
                            SetLocalLabelTime()
                        End If

                        If cmbServerName.SelectedValue = "Local" Then
                            Timer.Interval = 1000
                        Else
                            Timer.Interval = 5000
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "Get Times"

    Function GetLocalServerTime(ByVal strServerName As String) As Date
        Dim pDate As New DateTime

        Try
            Dim iRet As Integer
            Dim ptodi As IntPtr
            Dim todi As TIME_OF_DAY_INFO

            strServerName = strServerName & vbNullChar
            iRet = NetRemoteTOD(strServerName, ptodi)
            If iRet = 0 Then
                todi = CType(Marshal.PtrToStructure(ptodi, GetType(TIME_OF_DAY_INFO)), TIME_OF_DAY_INFO)
                NetApiBufferFree(ptodi)

                pDate = New DateTime(todi.tod_year, todi.tod_month, todi.tod_day, todi.tod_hours, todi.tod_mins, todi.tod_secs)
                pDate = pDate.AddMinutes(-todi.tod_timezone)

                Me.serverDate = pDate
                Me.localDate = System.DateTime.Now

                'GetLocalServerTime = pDate
            Else
                ErrorOccured()
                pDate = Nothing
                MsgBox("Error retrieving time")
            End If

        Catch
            ErrorOccured()
            MsgBox("Error in GetNetRemoteTOD: " & Err.Description)
        End Try

        Return pDate
    End Function

    Public Function GetNetworkTime(ntpServer As String) As DateTime


        Dim ntpData As Byte() = Nothing
        Dim ipEndPoint As System.Net.IPEndPoint = Nothing
        Dim socket As Socket = Nothing
        Try
            'default Windows time server
            'Const ntpServer As String = "time.windows.com"

            ' NTP message size - 16 bytes of the digest (RFC 2030)
            ntpData = New Byte(47) {}

            'Setting the Leap Indicator, Version Number and Mode values
            ntpData(0) = &H1B
            'LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)
            Dim addresses() = Dns.GetHostEntry(ntpServer).AddressList

            'The UDP port number assigned to NTP is 123


            ipEndPoint = New IPEndPoint(TryCast(addresses(0), System.Net.IPAddress), 123)
            'NTP uses UDP
            socket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)

            socket.Connect(ipEndPoint)

            'Stops code hang if NTP is blocked
            socket.ReceiveTimeout = 3000

            socket.Send(ntpData)
            socket.Receive(ntpData)
            socket.Close()

            'Offset to get to the "Transmit Timestamp" field (time at which the reply 
            'departed the server for the client, in 64-bit timestamp format."
            Const serverReplyTime As Byte = 40

            'Get the seconds part
            Dim intPart As ULong = BitConverter.ToUInt32(ntpData, serverReplyTime)

            'Get the seconds fraction
            Dim fractPart As ULong = BitConverter.ToUInt32(ntpData, serverReplyTime + 4)

            'Convert From big-endian to little-endian
            intPart = SwapEndianness(intPart)
            fractPart = SwapEndianness(fractPart)

            Dim milliseconds = (intPart * 1000) + ((fractPart * 1000) / &H100000000L)

            '**UTC** time
            Dim networkDateTime As DateTime = (New DateTime(1900, 1, 1, 0, 0, 0, _
             DateTimeKind.Utc)).AddMilliseconds(CLng(milliseconds))

            serverDate = Nothing
            localDate = Nothing

            serverDate = networkDateTime.ToLocalTime
            localDate = DateTime.Now

            'localDate = Nothing
            'serverDate = Nothing

            'localDate = DateTime.Now
            'serverDate = networkDateTime.ToLocalTime()

            'Logger(DateTime.Now, networkDateTime.ToLocalTime)
            'bShowLogger = True
            Return networkDateTime.ToLocalTime()

        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
            Return Nothing
        Catch ex As SocketException
            Timer.Stop()
            'Me.bShowLogger = False
            Me.bExceptionOccured = True
            MessageBox.Show("Unable to get server time. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return Nothing
        Finally
            If socket IsNot Nothing Then
                socket.Dispose()
                socket = Nothing
            End If

            If ipEndPoint IsNot Nothing Then
                ipEndPoint = Nothing
            End If

            If ntpData IsNot Nothing Then
                ntpData = Nothing
            End If

        End Try

    End Function

#End Region

#Region "Audit Log and Display Message"














    Private Sub WriteAuditLog(ByVal ServerName As String)
        Dim sMessageToAttach As String = GenerateAuditLogMessage(LocalTimeBeforeSync, serverDate)
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Time, gloAuditTrail.ActivityType.Synchronization, sMessageToAttach, gloAuditTrail.ActivityOutCome.Success)
    End Sub



    Private Function GenerateAuditLogMessage(ByVal _LocalDate As Date, ByVal _ServerDate As Date) As String











        Dim TimeSpan As TimeSpan = Nothing
        Dim stringBuilder As New StringBuilder()
        Dim sServerTime As String = String.Empty
        Dim sMessageLine As String = String.Empty

        Dim sMessageToWrite As String = String.Empty
        Dim bIsSame As Boolean = False

        Dim sMachineNameString As String = "Machine - " + Environment.MachineName + " synchronized with " + cmbServerName.Text + ". "

        TimeSpan = _ServerDate - LocalTimeBeforeSync
        'sServerTime = _ServerDate.ToString

        Dim sServerType As String = cmbServerName.SelectedValue.ToString
        If (sServerType = "Local" AndAlso _ServerDate < _LocalDate) OrElse (sServerType = "Network" AndAlso _ServerDate < _LocalDate) Then
            sMessageLine = "Local machine time was ahead by "
        ElseIf (sServerType = "Local" AndAlso _ServerDate > _LocalDate) OrElse (sServerType = "Network" AndAlso _ServerDate > _LocalDate) Then
            sMessageLine = "Local machine time was behind by "
        ElseIf (sServerType = "Local" AndAlso _ServerDate = _LocalDate) OrElse (sServerType = "Network" AndAlso _ServerDate = _LocalDate) Then
            bIsSame = True
            sMessageLine = "Local machine time and Server time are same. No sychronization required. "
        End If

        If bIsSame Then
            sMessageToWrite = sMessageLine
        Else

            'sMessageToDisplay = sMessageLine + sMessageToWrite
            sMessageToDisplay = sMachineNameString + sMessageLine + System.Math.Round(TimeSpan.TotalSeconds, 2).ToString.Replace("-", "") + " seconds"
        End If

        Return sMessageToDisplay
    End Function

    Public Sub ShowDisplayMessage(ByVal _LocalDate As Date, ByVal _ServerDate As Date)
        Try

            Dim stringBuilder As New StringBuilder()

            Dim TimeSpan As TimeSpan = Nothing ' _ServerDate.ToLocalTime - _LocalDate
            Dim sServerTime As String = String.Empty

            TimeSpan = _ServerDate - _LocalDate
            sServerTime = _ServerDate.ToString

            'If cmbServerName.SelectedValue = "Local" Then
            '    TimeSpan = _ServerDate - _LocalDate
            '    sServerTime = _ServerDate.ToString
            'Else
            '    TimeSpan = _ServerDate.ToLocalTime - _LocalDate
            '    sServerTime = _ServerDate.ToLocalTime.ToString
            'End If

            Dim sMessageLine As String = String.Empty
            Dim sTimeString As String = Nothing

            With stringBuilder
                .AppendLine("Local Time:  " + _LocalDate)
                .AppendLine("Server Time: " + sServerTime) '_ServerDate.ToLocalTime)
                .AppendLine()

                Dim bIsSame As Boolean = False
                Dim sServerType As String = cmbServerName.SelectedValue.ToString

                If (sServerType = "Local" AndAlso _ServerDate < _LocalDate) OrElse (sServerType = "Network" AndAlso _ServerDate.ToLocalTime < _LocalDate) Then
                    sMessageLine = "Local machine time was ahead by "
                ElseIf (sServerType = "Local" AndAlso _ServerDate > _LocalDate) OrElse (sServerType = "Network" AndAlso _ServerDate.ToLocalTime > _LocalDate) Then
                    sMessageLine = "Local machine time was behind by "
                ElseIf (sServerType = "Local" AndAlso _ServerDate = _LocalDate) OrElse (sServerType = "Network" AndAlso _ServerDate.ToLocalTime = _LocalDate) Then
                    bIsSame = True
                    sMessageLine = "Local machine time and Server time are same. No sychronization required. "
                End If

                If bIsSame Then
                    sMessageToDisplay = sMessageLine
                Else
                    sTimeString = System.Math.Round(TimeSpan.TotalSeconds, 2).ToString.Replace("-", "") + " seconds"
                    sMessageToDisplay = sMessageLine + sTimeString
                End If
                '.AppendLine(System.Math.Round(TimeSpan.TotalSeconds, 2).ToString).Replace("-", "")
                .AppendLine(sMessageToDisplay)
                .AppendLine()

                .AppendLine("Time synchronized with Server successfully.")
            End With
            TimeSpan = Nothing




            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Time, gloAuditTrail.ActivityType.Synchronization, "Machine - " + Environment.MachineName + " synchronized with " + cmbServerName.Text + ". " + sMessageToDisplay, gloAuditTrail.ActivityOutCome.Success)



            If Not IsTimerExecuting Then
                MsgBox(stringBuilder.ToString)
            End If


            'ExceptionLog(ex.ToString(), false);
            'Aniket Commented because it was going in recursion
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Sync Button Click"

    Private Sub Sync_Button_Clicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_SyncTime.ItemClicked
        Try
            Select Case (e.ClickedItem.Tag.ToString())
                Case "Sync Time"
                    If Timer IsNot Nothing Then
                        Timer.Stop()
                    End If

                    SetServerLabelTime()

                    If bExceptionOccured Then
                        ErrorOccured()
                        Exit Sub
                    End If

                    SetLocalLabelTime()

                    SyncTime()

                    'WriteAuditLog(cmbServerName.Text, sMessageToDisplay)
                    ShowDisplayMessage(LocalTimeBeforeSync, serverDate)

                    SetLocalLabelTime()

                    If cmbServerName.SelectedValue = "Local" Then
                        Timer.Interval = 1000
                        Timer.Start()
                    Else
                        Timer.Interval = 5000
                        Timer.Start()
                    End If


                Case "Cancel"
                    Me.Close()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub


#End Region

#Region "Set Label Texts"
    Private Sub SetServerLabelTime()

        Try
            ResetError()
            Me.Cursor = Cursors.WaitCursor

            If cmbServerName.SelectedIndex <> -1 AndAlso cmbServerName.Text <> "" Then

                Dim sValue As String = cmbServerName.SelectedValue
                Dim sServerName As String = cmbServerName.Text

                'Dim labelDateTime As DateTime = Nothing

                If (sValue = "Local") Then
                    serverDate = GetLocalServerTime(sServerName)

                    If Not bExceptionOccured Then
                        lblRemoteDateTimeValue.Text = serverDate.ToString("M/d/yyyy hh:mm:ss tt")
                        'lblLocalSystemDateTime.Text = localDate.ToString("M/d/yyyy hh:mm:ss tt")
                    End If
                Else
                    serverDate = GetNetworkTime(sServerName).ToString("M/d/yyyy hh:mm:ss tt")

                    If Not bExceptionOccured Then
                        lblRemoteDateTimeValue.Text = serverDate.ToString("M/d/yyyy hh:mm:ss tt")
                        'lblLocalSystemDateTime.Text = localDate.ToString("M/d/yyyy hh:mm:ss tt")
                    End If
                End If
                'labelDateTime = Nothing

            Else
                Me.bExceptionOccured = True
                MessageBox.Show("Please select Time Server.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally
            Me.Cursor = Cursors.Default



        End Try


    End Sub

    Private Sub SetLocalLabelTime()
        localDate = System.DateTime.Now
        lblLocalSystemDateTime.Text = localDate.ToString("M/d/yyyy hh:mm:ss tt")
    End Sub
#End Region

End Class

Public Class ServerNameKeyPair

    Private _sName As String
    Private _sType As String

    Public Property sName() As String
        Get
            Return _sName
        End Get
        Set(ByVal value As String)
            _sName = value
        End Set
    End Property

    Public Property sType() As String
        Get
            Return _sType
        End Get
        Set(ByVal value As String)
            _sType = value
        End Set
    End Property

    Sub New(ByVal ServerName As String, ByVal ServerType As String)
        sName = ServerName
        sType = ServerType
    End Sub


End Class

