<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSysPrinter
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

#Region "Windows フォーム デザイナによって生成されたコード "
    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    Public WithEvents txtHCPrinterDriver As System.Windows.Forms.TextBox
    Public WithEvents txtAlarmPrinter1Driver As System.Windows.Forms.TextBox
    Public WithEvents txtAlarmPrinter2Driver As System.Windows.Forms.TextBox
    Public WithEvents chkAlarmPrinter2Machinery As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmPrinter1Machinery As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmPrinter1Cargo As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmPrinter2Cargo As System.Windows.Forms.CheckBox
    Public WithEvents txtLogPrinter2Driver As System.Windows.Forms.TextBox
    Public WithEvents txtLogPrinter1Driver As System.Windows.Forms.TextBox
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label111 As System.Windows.Forms.Label
    Public WithEvents chkDemandLog As System.Windows.Forms.CheckBox
    Public WithEvents chkNoonLog As System.Windows.Forms.CheckBox
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents chkHCPrinterCargo As System.Windows.Forms.CheckBox
    Public WithEvents chkHCPrinterMachinery As System.Windows.Forms.CheckBox
    Public WithEvents cmbHCPrinter As System.Windows.Forms.ComboBox
    Public WithEvents fraHCPrinter As System.Windows.Forms.GroupBox
    Public WithEvents cmbAlarmPrintType As System.Windows.Forms.ComboBox
    Public WithEvents cmbAlarmPrinter1 As System.Windows.Forms.ComboBox
    Public WithEvents cmbAlarmPrinter2 As System.Windows.Forms.ComboBox
    Public WithEvents chkEventPrintNone As System.Windows.Forms.CheckBox
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents fraAlarmPrinter As System.Windows.Forms.GroupBox
    Public WithEvents chkLogPrinter2Cargo As System.Windows.Forms.CheckBox
    Public WithEvents chkLogPrinter1Cargo As System.Windows.Forms.CheckBox
    Public WithEvents chkLogPrinter1Machinery As System.Windows.Forms.CheckBox
    Public WithEvents chkLogPrinter2Machinery As System.Windows.Forms.CheckBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents cmbLogPrinter1 As System.Windows.Forms.ComboBox
    Public WithEvents cmbLogPrinter2 As System.Windows.Forms.ComboBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents chkLogPrinter1PaperSizeA3 As System.Windows.Forms.CheckBox
    Public WithEvents fraLogPrinter As System.Windows.Forms.GroupBox
    Public WithEvents Label113 As System.Windows.Forms.Label
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtHCPrinterDevice = New System.Windows.Forms.TextBox()
        Me.txtHCPrinterDriver = New System.Windows.Forms.TextBox()
        Me.txtAlarmPrinter2Device = New System.Windows.Forms.TextBox()
        Me.txtAlarmPrinter1Device = New System.Windows.Forms.TextBox()
        Me.txtAlarmPrinter1Driver = New System.Windows.Forms.TextBox()
        Me.txtAlarmPrinter2Driver = New System.Windows.Forms.TextBox()
        Me.chkAlarmPrinter2Machinery = New System.Windows.Forms.CheckBox()
        Me.chkAlarmPrinter1Machinery = New System.Windows.Forms.CheckBox()
        Me.chkAlarmPrinter1Cargo = New System.Windows.Forms.CheckBox()
        Me.chkAlarmPrinter2Cargo = New System.Windows.Forms.CheckBox()
        Me.txtLogPrinter2Device = New System.Windows.Forms.TextBox()
        Me.txtLogPrinter1Device = New System.Windows.Forms.TextBox()
        Me.txtLogPrinter2Driver = New System.Windows.Forms.TextBox()
        Me.txtLogPrinter1Driver = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.chkDemandLog = New System.Windows.Forms.CheckBox()
        Me.chkNoonLog = New System.Windows.Forms.CheckBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.fraHCPrinter = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbPrinterNameHC = New System.Windows.Forms.ComboBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.chkHCPrinter1PaperSizeA3 = New System.Windows.Forms.CheckBox()
        Me.chkHCPrinterCargo = New System.Windows.Forms.CheckBox()
        Me.txtHcPrinterIP3 = New System.Windows.Forms.TextBox()
        Me.chkHCPrinterMachinery = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtHcPrinterIP2 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtHcPrinterIP4 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.chkHcPrinter1EnableBackup = New System.Windows.Forms.CheckBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtHcPrinterIP1 = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.cmbHCPrinter = New System.Windows.Forms.ComboBox()
        Me.fraAlarmPrinter = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtAlarmPrinterIP23 = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtAlarmPrinterIP22 = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtAlarmPrinterIP24 = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.chkAlarmPrinter2EnableBackup = New System.Windows.Forms.CheckBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtAlarmPrinterIP21 = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.cmbAlarmPrinter2 = New System.Windows.Forms.ComboBox()
        Me.cmbAlarmPrintType = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtAlarmPrinterIP11 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtAlarmPrinterIP13 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtAlarmPrinterIP12 = New System.Windows.Forms.TextBox()
        Me.chkAlarmPrinter1EnableBackup = New System.Windows.Forms.CheckBox()
        Me.txtAlarmPrinterIP14 = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.cmbAlarmPrinter1 = New System.Windows.Forms.ComboBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.chkEventPrintNone = New System.Windows.Forms.CheckBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.fraLogPrinter = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cmbPrinterNameL2 = New System.Windows.Forms.ComboBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.chkLogPrinter2Color = New System.Windows.Forms.CheckBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.chkLogPrinter2PaperSizeA3 = New System.Windows.Forms.CheckBox()
        Me.chkLogPrinter2Cargo = New System.Windows.Forms.CheckBox()
        Me.txtLogPrinterIP23 = New System.Windows.Forms.TextBox()
        Me.chkLogPrinter2Machinery = New System.Windows.Forms.CheckBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtLogPrinterIP22 = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtLogPrinterIP24 = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.chkLogPrinter2EnableBackup = New System.Windows.Forms.CheckBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.txtLogPrinterIP21 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbLogPrinter2 = New System.Windows.Forms.ComboBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbPrinterNameL1 = New System.Windows.Forms.ComboBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.chkLogPrinter1Color = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtLogPrinterIP11 = New System.Windows.Forms.TextBox()
        Me.chkLogPrinter1PaperSizeA3 = New System.Windows.Forms.CheckBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.cmbLogPrinter1 = New System.Windows.Forms.ComboBox()
        Me.chkLogPrinter1Cargo = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtLogPrinterIP13 = New System.Windows.Forms.TextBox()
        Me.chkLogPrinter1EnableBackup = New System.Windows.Forms.CheckBox()
        Me.chkLogPrinter1Machinery = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtLogPrinterIP14 = New System.Windows.Forms.TextBox()
        Me.txtLogPrinterIP12 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.chkMachineryCargoPrint = New System.Windows.Forms.CheckBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtAutoCnt = New System.Windows.Forms.TextBox()
        Me.fraHCPrinter.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.fraAlarmPrinter.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.fraLogPrinter.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtHCPrinterDevice
        '
        Me.txtHCPrinterDevice.AcceptsReturn = True
        Me.txtHCPrinterDevice.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtHCPrinterDevice.Location = New System.Drawing.Point(81, 152)
        Me.txtHCPrinterDevice.MaxLength = 32
        Me.txtHCPrinterDevice.Name = "txtHCPrinterDevice"
        Me.txtHCPrinterDevice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHCPrinterDevice.Size = New System.Drawing.Size(237, 19)
        Me.txtHCPrinterDevice.TabIndex = 8
        '
        'txtHCPrinterDriver
        '
        Me.txtHCPrinterDriver.AcceptsReturn = True
        Me.txtHCPrinterDriver.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtHCPrinterDriver.Location = New System.Drawing.Point(81, 125)
        Me.txtHCPrinterDriver.MaxLength = 16
        Me.txtHCPrinterDriver.Name = "txtHCPrinterDriver"
        Me.txtHCPrinterDriver.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHCPrinterDriver.Size = New System.Drawing.Size(133, 19)
        Me.txtHCPrinterDriver.TabIndex = 7
        '
        'txtAlarmPrinter2Device
        '
        Me.txtAlarmPrinter2Device.AcceptsReturn = True
        Me.txtAlarmPrinter2Device.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtAlarmPrinter2Device.Location = New System.Drawing.Point(81, 128)
        Me.txtAlarmPrinter2Device.MaxLength = 32
        Me.txtAlarmPrinter2Device.Name = "txtAlarmPrinter2Device"
        Me.txtAlarmPrinter2Device.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlarmPrinter2Device.Size = New System.Drawing.Size(237, 19)
        Me.txtAlarmPrinter2Device.TabIndex = 8
        '
        'txtAlarmPrinter1Device
        '
        Me.txtAlarmPrinter1Device.AcceptsReturn = True
        Me.txtAlarmPrinter1Device.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtAlarmPrinter1Device.Location = New System.Drawing.Point(84, 128)
        Me.txtAlarmPrinter1Device.MaxLength = 32
        Me.txtAlarmPrinter1Device.Name = "txtAlarmPrinter1Device"
        Me.txtAlarmPrinter1Device.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlarmPrinter1Device.Size = New System.Drawing.Size(237, 19)
        Me.txtAlarmPrinter1Device.TabIndex = 8
        '
        'txtAlarmPrinter1Driver
        '
        Me.txtAlarmPrinter1Driver.AcceptsReturn = True
        Me.txtAlarmPrinter1Driver.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtAlarmPrinter1Driver.Location = New System.Drawing.Point(84, 101)
        Me.txtAlarmPrinter1Driver.MaxLength = 16
        Me.txtAlarmPrinter1Driver.Name = "txtAlarmPrinter1Driver"
        Me.txtAlarmPrinter1Driver.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlarmPrinter1Driver.Size = New System.Drawing.Size(133, 19)
        Me.txtAlarmPrinter1Driver.TabIndex = 7
        '
        'txtAlarmPrinter2Driver
        '
        Me.txtAlarmPrinter2Driver.AcceptsReturn = True
        Me.txtAlarmPrinter2Driver.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtAlarmPrinter2Driver.Location = New System.Drawing.Point(81, 101)
        Me.txtAlarmPrinter2Driver.MaxLength = 16
        Me.txtAlarmPrinter2Driver.Name = "txtAlarmPrinter2Driver"
        Me.txtAlarmPrinter2Driver.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlarmPrinter2Driver.Size = New System.Drawing.Size(133, 19)
        Me.txtAlarmPrinter2Driver.TabIndex = 7
        '
        'chkAlarmPrinter2Machinery
        '
        Me.chkAlarmPrinter2Machinery.AutoSize = True
        Me.chkAlarmPrinter2Machinery.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmPrinter2Machinery.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmPrinter2Machinery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmPrinter2Machinery.Location = New System.Drawing.Point(81, 76)
        Me.chkAlarmPrinter2Machinery.Name = "chkAlarmPrinter2Machinery"
        Me.chkAlarmPrinter2Machinery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmPrinter2Machinery.Size = New System.Drawing.Size(78, 16)
        Me.chkAlarmPrinter2Machinery.TabIndex = 5
        Me.chkAlarmPrinter2Machinery.Text = "Machinery"
        Me.chkAlarmPrinter2Machinery.UseVisualStyleBackColor = True
        '
        'chkAlarmPrinter1Machinery
        '
        Me.chkAlarmPrinter1Machinery.AutoSize = True
        Me.chkAlarmPrinter1Machinery.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmPrinter1Machinery.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmPrinter1Machinery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmPrinter1Machinery.Location = New System.Drawing.Point(84, 76)
        Me.chkAlarmPrinter1Machinery.Name = "chkAlarmPrinter1Machinery"
        Me.chkAlarmPrinter1Machinery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmPrinter1Machinery.Size = New System.Drawing.Size(78, 16)
        Me.chkAlarmPrinter1Machinery.TabIndex = 5
        Me.chkAlarmPrinter1Machinery.Text = "Machinery"
        Me.chkAlarmPrinter1Machinery.UseVisualStyleBackColor = True
        '
        'chkAlarmPrinter1Cargo
        '
        Me.chkAlarmPrinter1Cargo.AutoSize = True
        Me.chkAlarmPrinter1Cargo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmPrinter1Cargo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmPrinter1Cargo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmPrinter1Cargo.Location = New System.Drawing.Point(168, 76)
        Me.chkAlarmPrinter1Cargo.Name = "chkAlarmPrinter1Cargo"
        Me.chkAlarmPrinter1Cargo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmPrinter1Cargo.Size = New System.Drawing.Size(54, 16)
        Me.chkAlarmPrinter1Cargo.TabIndex = 6
        Me.chkAlarmPrinter1Cargo.Text = "Cargo"
        Me.chkAlarmPrinter1Cargo.UseVisualStyleBackColor = True
        '
        'chkAlarmPrinter2Cargo
        '
        Me.chkAlarmPrinter2Cargo.AutoSize = True
        Me.chkAlarmPrinter2Cargo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmPrinter2Cargo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmPrinter2Cargo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmPrinter2Cargo.Location = New System.Drawing.Point(165, 76)
        Me.chkAlarmPrinter2Cargo.Name = "chkAlarmPrinter2Cargo"
        Me.chkAlarmPrinter2Cargo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmPrinter2Cargo.Size = New System.Drawing.Size(54, 16)
        Me.chkAlarmPrinter2Cargo.TabIndex = 6
        Me.chkAlarmPrinter2Cargo.Text = "Cargo"
        Me.chkAlarmPrinter2Cargo.UseVisualStyleBackColor = True
        '
        'txtLogPrinter2Device
        '
        Me.txtLogPrinter2Device.AcceptsReturn = True
        Me.txtLogPrinter2Device.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtLogPrinter2Device.Location = New System.Drawing.Point(83, 155)
        Me.txtLogPrinter2Device.MaxLength = 32
        Me.txtLogPrinter2Device.Name = "txtLogPrinter2Device"
        Me.txtLogPrinter2Device.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLogPrinter2Device.Size = New System.Drawing.Size(237, 19)
        Me.txtLogPrinter2Device.TabIndex = 8
        '
        'txtLogPrinter1Device
        '
        Me.txtLogPrinter1Device.AcceptsReturn = True
        Me.txtLogPrinter1Device.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtLogPrinter1Device.Location = New System.Drawing.Point(83, 155)
        Me.txtLogPrinter1Device.MaxLength = 32
        Me.txtLogPrinter1Device.Name = "txtLogPrinter1Device"
        Me.txtLogPrinter1Device.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLogPrinter1Device.Size = New System.Drawing.Size(237, 19)
        Me.txtLogPrinter1Device.TabIndex = 8
        Me.txtLogPrinter1Device.Text = "12345678901234567890123456789012"
        '
        'txtLogPrinter2Driver
        '
        Me.txtLogPrinter2Driver.AcceptsReturn = True
        Me.txtLogPrinter2Driver.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtLogPrinter2Driver.Location = New System.Drawing.Point(83, 128)
        Me.txtLogPrinter2Driver.MaxLength = 16
        Me.txtLogPrinter2Driver.Name = "txtLogPrinter2Driver"
        Me.txtLogPrinter2Driver.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLogPrinter2Driver.Size = New System.Drawing.Size(133, 19)
        Me.txtLogPrinter2Driver.TabIndex = 7
        '
        'txtLogPrinter1Driver
        '
        Me.txtLogPrinter1Driver.AcceptsReturn = True
        Me.txtLogPrinter1Driver.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtLogPrinter1Driver.Location = New System.Drawing.Point(83, 128)
        Me.txtLogPrinter1Driver.MaxLength = 16
        Me.txtLogPrinter1Driver.Name = "txtLogPrinter1Driver"
        Me.txtLogPrinter1Driver.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLogPrinter1Driver.Size = New System.Drawing.Size(133, 19)
        Me.txtLogPrinter1Driver.TabIndex = 7
        Me.txtLogPrinter1Driver.Text = "1234567890123456"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(10, 158)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(41, 12)
        Me.Label20.TabIndex = 45
        Me.Label20.Text = "Device"
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.BackColor = System.Drawing.SystemColors.Control
        Me.Label111.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label111.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label111.Location = New System.Drawing.Point(10, 131)
        Me.Label111.Name = "Label111"
        Me.Label111.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label111.Size = New System.Drawing.Size(41, 12)
        Me.Label111.TabIndex = 41
        Me.Label111.Text = "Driver"
        Me.Label111.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkDemandLog
        '
        Me.chkDemandLog.AutoSize = True
        Me.chkDemandLog.BackColor = System.Drawing.SystemColors.Control
        Me.chkDemandLog.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDemandLog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDemandLog.Location = New System.Drawing.Point(741, 321)
        Me.chkDemandLog.Name = "chkDemandLog"
        Me.chkDemandLog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDemandLog.Size = New System.Drawing.Size(162, 16)
        Me.chkDemandLog.TabIndex = 4
        Me.chkDemandLog.Text = "DemandLog Changing Page"
        Me.chkDemandLog.UseVisualStyleBackColor = True
        '
        'chkNoonLog
        '
        Me.chkNoonLog.AutoSize = True
        Me.chkNoonLog.BackColor = System.Drawing.SystemColors.Control
        Me.chkNoonLog.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkNoonLog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkNoonLog.Location = New System.Drawing.Point(741, 296)
        Me.chkNoonLog.Name = "chkNoonLog"
        Me.chkNoonLog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkNoonLog.Size = New System.Drawing.Size(198, 16)
        Me.chkNoonLog.TabIndex = 3
        Me.chkNoonLog.Text = "NoonLog Group Name Under Line"
        Me.chkNoonLog.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(865, 476)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 8
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(989, 476)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 9
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'fraHCPrinter
        '
        Me.fraHCPrinter.BackColor = System.Drawing.SystemColors.Control
        Me.fraHCPrinter.Controls.Add(Me.GroupBox1)
        Me.fraHCPrinter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraHCPrinter.Location = New System.Drawing.Point(741, 8)
        Me.fraHCPrinter.Name = "fraHCPrinter"
        Me.fraHCPrinter.Padding = New System.Windows.Forms.Padding(0)
        Me.fraHCPrinter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraHCPrinter.Size = New System.Drawing.Size(363, 267)
        Me.fraHCPrinter.TabIndex = 2
        Me.fraHCPrinter.TabStop = False
        Me.fraHCPrinter.Text = "Hard Copy Printer"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbPrinterNameHC)
        Me.GroupBox1.Controls.Add(Me.Label54)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.chkHCPrinter1PaperSizeA3)
        Me.GroupBox1.Controls.Add(Me.txtHCPrinterDevice)
        Me.GroupBox1.Controls.Add(Me.chkHCPrinterCargo)
        Me.GroupBox1.Controls.Add(Me.txtHCPrinterDriver)
        Me.GroupBox1.Controls.Add(Me.txtHcPrinterIP3)
        Me.GroupBox1.Controls.Add(Me.chkHCPrinterMachinery)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtHcPrinterIP2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtHcPrinterIP4)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label33)
        Me.GroupBox1.Controls.Add(Me.chkHcPrinter1EnableBackup)
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.txtHcPrinterIP1)
        Me.GroupBox1.Controls.Add(Me.Label35)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label31)
        Me.GroupBox1.Controls.Add(Me.Label51)
        Me.GroupBox1.Controls.Add(Me.cmbHCPrinter)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(331, 239)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Printer1"
        '
        'cmbPrinterNameHC
        '
        Me.cmbPrinterNameHC.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPrinterNameHC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPrinterNameHC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPrinterNameHC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPrinterNameHC.Location = New System.Drawing.Point(81, 99)
        Me.cmbPrinterNameHC.Name = "cmbPrinterNameHC"
        Me.cmbPrinterNameHC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPrinterNameHC.Size = New System.Drawing.Size(133, 20)
        Me.cmbPrinterNameHC.TabIndex = 70
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.BackColor = System.Drawing.SystemColors.Control
        Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label54.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label54.Location = New System.Drawing.Point(8, 102)
        Me.Label54.Name = "Label54"
        Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label54.Size = New System.Drawing.Size(71, 12)
        Me.Label54.TabIndex = 71
        Me.Label54.Text = "PrinterName"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(10, 203)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(65, 12)
        Me.Label16.TabIndex = 69
        Me.Label16.Text = "Paper Size"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkHCPrinter1PaperSizeA3
        '
        Me.chkHCPrinter1PaperSizeA3.AutoSize = True
        Me.chkHCPrinter1PaperSizeA3.BackColor = System.Drawing.SystemColors.Control
        Me.chkHCPrinter1PaperSizeA3.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHCPrinter1PaperSizeA3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHCPrinter1PaperSizeA3.Location = New System.Drawing.Point(81, 202)
        Me.chkHCPrinter1PaperSizeA3.Name = "chkHCPrinter1PaperSizeA3"
        Me.chkHCPrinter1PaperSizeA3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHCPrinter1PaperSizeA3.Size = New System.Drawing.Size(36, 16)
        Me.chkHCPrinter1PaperSizeA3.TabIndex = 68
        Me.chkHCPrinter1PaperSizeA3.Text = "A3"
        Me.chkHCPrinter1PaperSizeA3.UseVisualStyleBackColor = True
        '
        'chkHCPrinterCargo
        '
        Me.chkHCPrinterCargo.AutoSize = True
        Me.chkHCPrinterCargo.BackColor = System.Drawing.SystemColors.Control
        Me.chkHCPrinterCargo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHCPrinterCargo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHCPrinterCargo.Location = New System.Drawing.Point(160, 76)
        Me.chkHCPrinterCargo.Name = "chkHCPrinterCargo"
        Me.chkHCPrinterCargo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHCPrinterCargo.Size = New System.Drawing.Size(54, 16)
        Me.chkHCPrinterCargo.TabIndex = 6
        Me.chkHCPrinterCargo.Text = "Cargo"
        Me.chkHCPrinterCargo.UseVisualStyleBackColor = True
        '
        'txtHcPrinterIP3
        '
        Me.txtHcPrinterIP3.Location = New System.Drawing.Point(150, 49)
        Me.txtHcPrinterIP3.Name = "txtHcPrinterIP3"
        Me.txtHcPrinterIP3.Size = New System.Drawing.Size(29, 19)
        Me.txtHcPrinterIP3.TabIndex = 3
        Me.txtHcPrinterIP3.Text = "255"
        Me.txtHcPrinterIP3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkHCPrinterMachinery
        '
        Me.chkHCPrinterMachinery.AutoSize = True
        Me.chkHCPrinterMachinery.BackColor = System.Drawing.SystemColors.Control
        Me.chkHCPrinterMachinery.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHCPrinterMachinery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHCPrinterMachinery.Location = New System.Drawing.Point(81, 76)
        Me.chkHCPrinterMachinery.Name = "chkHCPrinterMachinery"
        Me.chkHCPrinterMachinery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHCPrinterMachinery.Size = New System.Drawing.Size(78, 16)
        Me.chkHCPrinterMachinery.TabIndex = 5
        Me.chkHCPrinterMachinery.Text = "Machinery"
        Me.chkHCPrinterMachinery.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(10, 180)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Enable"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label3.Visible = False
        '
        'txtHcPrinterIP2
        '
        Me.txtHcPrinterIP2.Location = New System.Drawing.Point(115, 49)
        Me.txtHcPrinterIP2.Name = "txtHcPrinterIP2"
        Me.txtHcPrinterIP2.Size = New System.Drawing.Size(29, 19)
        Me.txtHcPrinterIP2.TabIndex = 2
        Me.txtHcPrinterIP2.Text = "255"
        Me.txtHcPrinterIP2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(10, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Print Part"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtHcPrinterIP4
        '
        Me.txtHcPrinterIP4.Location = New System.Drawing.Point(185, 49)
        Me.txtHcPrinterIP4.Name = "txtHcPrinterIP4"
        Me.txtHcPrinterIP4.Size = New System.Drawing.Size(29, 19)
        Me.txtHcPrinterIP4.TabIndex = 4
        Me.txtHcPrinterIP4.Text = "255"
        Me.txtHcPrinterIP4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(10, 155)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(41, 12)
        Me.Label14.TabIndex = 45
        Me.Label14.Text = "Device"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(178, 55)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(11, 12)
        Me.Label33.TabIndex = 67
        Me.Label33.Text = "."
        '
        'chkHcPrinter1EnableBackup
        '
        Me.chkHcPrinter1EnableBackup.AutoSize = True
        Me.chkHcPrinter1EnableBackup.BackColor = System.Drawing.SystemColors.Control
        Me.chkHcPrinter1EnableBackup.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHcPrinter1EnableBackup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHcPrinter1EnableBackup.Location = New System.Drawing.Point(81, 179)
        Me.chkHcPrinter1EnableBackup.Name = "chkHcPrinter1EnableBackup"
        Me.chkHcPrinter1EnableBackup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHcPrinter1EnableBackup.Size = New System.Drawing.Size(96, 16)
        Me.chkHcPrinter1EnableBackup.TabIndex = 10
        Me.chkHcPrinter1EnableBackup.Text = "Backup Print"
        Me.chkHcPrinter1EnableBackup.UseVisualStyleBackColor = True
        Me.chkHcPrinter1EnableBackup.Visible = False
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(143, 55)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(11, 12)
        Me.Label34.TabIndex = 65
        Me.Label34.Text = "."
        '
        'txtHcPrinterIP1
        '
        Me.txtHcPrinterIP1.Location = New System.Drawing.Point(81, 49)
        Me.txtHcPrinterIP1.Name = "txtHcPrinterIP1"
        Me.txtHcPrinterIP1.Size = New System.Drawing.Size(29, 19)
        Me.txtHcPrinterIP1.TabIndex = 1
        Me.txtHcPrinterIP1.Text = "255"
        Me.txtHcPrinterIP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(108, 55)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(11, 12)
        Me.Label35.TabIndex = 63
        Me.Label35.Text = "."
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(10, 23)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(47, 12)
        Me.Label21.TabIndex = 6
        Me.Label21.Text = "Printer"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(10, 128)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(41, 12)
        Me.Label31.TabIndex = 41
        Me.Label31.Text = "Driver"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.SystemColors.Control
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(10, 52)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(17, 12)
        Me.Label51.TabIndex = 8
        Me.Label51.Text = "IP"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbHCPrinter
        '
        Me.cmbHCPrinter.BackColor = System.Drawing.SystemColors.Window
        Me.cmbHCPrinter.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbHCPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHCPrinter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbHCPrinter.Location = New System.Drawing.Point(81, 20)
        Me.cmbHCPrinter.Name = "cmbHCPrinter"
        Me.cmbHCPrinter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbHCPrinter.Size = New System.Drawing.Size(133, 20)
        Me.cmbHCPrinter.TabIndex = 0
        '
        'fraAlarmPrinter
        '
        Me.fraAlarmPrinter.BackColor = System.Drawing.SystemColors.Control
        Me.fraAlarmPrinter.Controls.Add(Me.GroupBox6)
        Me.fraAlarmPrinter.Controls.Add(Me.cmbAlarmPrintType)
        Me.fraAlarmPrinter.Controls.Add(Me.GroupBox5)
        Me.fraAlarmPrinter.Controls.Add(Me.chkEventPrintNone)
        Me.fraAlarmPrinter.Controls.Add(Me.Label18)
        Me.fraAlarmPrinter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAlarmPrinter.Location = New System.Drawing.Point(16, 296)
        Me.fraAlarmPrinter.Name = "fraAlarmPrinter"
        Me.fraAlarmPrinter.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAlarmPrinter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAlarmPrinter.Size = New System.Drawing.Size(709, 255)
        Me.fraAlarmPrinter.TabIndex = 1
        Me.fraAlarmPrinter.TabStop = False
        Me.fraAlarmPrinter.Text = "Alarm Printer"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtAlarmPrinter2Device)
        Me.GroupBox6.Controls.Add(Me.chkAlarmPrinter2Machinery)
        Me.GroupBox6.Controls.Add(Me.txtAlarmPrinter2Driver)
        Me.GroupBox6.Controls.Add(Me.chkAlarmPrinter2Cargo)
        Me.GroupBox6.Controls.Add(Me.txtAlarmPrinterIP23)
        Me.GroupBox6.Controls.Add(Me.Label30)
        Me.GroupBox6.Controls.Add(Me.txtAlarmPrinterIP22)
        Me.GroupBox6.Controls.Add(Me.Label42)
        Me.GroupBox6.Controls.Add(Me.txtAlarmPrinterIP24)
        Me.GroupBox6.Controls.Add(Me.Label45)
        Me.GroupBox6.Controls.Add(Me.Label25)
        Me.GroupBox6.Controls.Add(Me.chkAlarmPrinter2EnableBackup)
        Me.GroupBox6.Controls.Add(Me.Label27)
        Me.GroupBox6.Controls.Add(Me.txtAlarmPrinterIP21)
        Me.GroupBox6.Controls.Add(Me.Label29)
        Me.GroupBox6.Controls.Add(Me.Label48)
        Me.GroupBox6.Controls.Add(Me.Label49)
        Me.GroupBox6.Controls.Add(Me.Label50)
        Me.GroupBox6.Controls.Add(Me.cmbAlarmPrinter2)
        Me.GroupBox6.Location = New System.Drawing.Point(365, 26)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(331, 184)
        Me.GroupBox6.TabIndex = 1
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Printer2"
        '
        'txtAlarmPrinterIP23
        '
        Me.txtAlarmPrinterIP23.Location = New System.Drawing.Point(150, 49)
        Me.txtAlarmPrinterIP23.Name = "txtAlarmPrinterIP23"
        Me.txtAlarmPrinterIP23.Size = New System.Drawing.Size(29, 19)
        Me.txtAlarmPrinterIP23.TabIndex = 3
        Me.txtAlarmPrinterIP23.Text = "255"
        Me.txtAlarmPrinterIP23.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(10, 156)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(41, 12)
        Me.Label30.TabIndex = 47
        Me.Label30.Text = "Enable"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAlarmPrinterIP22
        '
        Me.txtAlarmPrinterIP22.Location = New System.Drawing.Point(115, 49)
        Me.txtAlarmPrinterIP22.Name = "txtAlarmPrinterIP22"
        Me.txtAlarmPrinterIP22.Size = New System.Drawing.Size(29, 19)
        Me.txtAlarmPrinterIP22.TabIndex = 2
        Me.txtAlarmPrinterIP22.Text = "255"
        Me.txtAlarmPrinterIP22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(10, 77)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(65, 12)
        Me.Label42.TabIndex = 10
        Me.Label42.Text = "Print Part"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAlarmPrinterIP24
        '
        Me.txtAlarmPrinterIP24.Location = New System.Drawing.Point(185, 49)
        Me.txtAlarmPrinterIP24.Name = "txtAlarmPrinterIP24"
        Me.txtAlarmPrinterIP24.Size = New System.Drawing.Size(29, 19)
        Me.txtAlarmPrinterIP24.TabIndex = 4
        Me.txtAlarmPrinterIP24.Text = "255"
        Me.txtAlarmPrinterIP24.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(10, 131)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(41, 12)
        Me.Label45.TabIndex = 45
        Me.Label45.Text = "Device"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(178, 55)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(11, 12)
        Me.Label25.TabIndex = 60
        Me.Label25.Text = "."
        '
        'chkAlarmPrinter2EnableBackup
        '
        Me.chkAlarmPrinter2EnableBackup.AutoSize = True
        Me.chkAlarmPrinter2EnableBackup.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmPrinter2EnableBackup.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmPrinter2EnableBackup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmPrinter2EnableBackup.Location = New System.Drawing.Point(81, 155)
        Me.chkAlarmPrinter2EnableBackup.Name = "chkAlarmPrinter2EnableBackup"
        Me.chkAlarmPrinter2EnableBackup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmPrinter2EnableBackup.Size = New System.Drawing.Size(96, 16)
        Me.chkAlarmPrinter2EnableBackup.TabIndex = 10
        Me.chkAlarmPrinter2EnableBackup.Text = "Backup Print"
        Me.chkAlarmPrinter2EnableBackup.UseVisualStyleBackColor = True
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(143, 55)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(11, 12)
        Me.Label27.TabIndex = 55
        Me.Label27.Text = "."
        '
        'txtAlarmPrinterIP21
        '
        Me.txtAlarmPrinterIP21.Location = New System.Drawing.Point(81, 49)
        Me.txtAlarmPrinterIP21.Name = "txtAlarmPrinterIP21"
        Me.txtAlarmPrinterIP21.Size = New System.Drawing.Size(29, 19)
        Me.txtAlarmPrinterIP21.TabIndex = 1
        Me.txtAlarmPrinterIP21.Text = "255"
        Me.txtAlarmPrinterIP21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(108, 55)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(11, 12)
        Me.Label29.TabIndex = 52
        Me.Label29.Text = "."
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(10, 23)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(47, 12)
        Me.Label48.TabIndex = 6
        Me.Label48.Text = "Printer"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.BackColor = System.Drawing.SystemColors.Control
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(10, 104)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(41, 12)
        Me.Label49.TabIndex = 41
        Me.Label49.Text = "Driver"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.SystemColors.Control
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(10, 52)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(17, 12)
        Me.Label50.TabIndex = 8
        Me.Label50.Text = "IP"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbAlarmPrinter2
        '
        Me.cmbAlarmPrinter2.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAlarmPrinter2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbAlarmPrinter2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlarmPrinter2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAlarmPrinter2.Location = New System.Drawing.Point(81, 20)
        Me.cmbAlarmPrinter2.Name = "cmbAlarmPrinter2"
        Me.cmbAlarmPrinter2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAlarmPrinter2.Size = New System.Drawing.Size(133, 20)
        Me.cmbAlarmPrinter2.TabIndex = 0
        '
        'cmbAlarmPrintType
        '
        Me.cmbAlarmPrintType.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAlarmPrintType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbAlarmPrintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlarmPrintType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAlarmPrintType.Location = New System.Drawing.Point(81, 221)
        Me.cmbAlarmPrintType.Name = "cmbAlarmPrintType"
        Me.cmbAlarmPrintType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAlarmPrintType.Size = New System.Drawing.Size(135, 20)
        Me.cmbAlarmPrintType.TabIndex = 2
        Me.cmbAlarmPrintType.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtAlarmPrinterIP11)
        Me.GroupBox5.Controls.Add(Me.Label2)
        Me.GroupBox5.Controls.Add(Me.txtAlarmPrinter1Device)
        Me.GroupBox5.Controls.Add(Me.chkAlarmPrinter1Machinery)
        Me.GroupBox5.Controls.Add(Me.txtAlarmPrinter1Driver)
        Me.GroupBox5.Controls.Add(Me.chkAlarmPrinter1Cargo)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.txtAlarmPrinterIP13)
        Me.GroupBox5.Controls.Add(Me.Label13)
        Me.GroupBox5.Controls.Add(Me.txtAlarmPrinterIP12)
        Me.GroupBox5.Controls.Add(Me.chkAlarmPrinter1EnableBackup)
        Me.GroupBox5.Controls.Add(Me.txtAlarmPrinterIP14)
        Me.GroupBox5.Controls.Add(Me.Label43)
        Me.GroupBox5.Controls.Add(Me.Label26)
        Me.GroupBox5.Controls.Add(Me.Label46)
        Me.GroupBox5.Controls.Add(Me.Label47)
        Me.GroupBox5.Controls.Add(Me.Label28)
        Me.GroupBox5.Controls.Add(Me.cmbAlarmPrinter1)
        Me.GroupBox5.Controls.Add(Me.Label32)
        Me.GroupBox5.Location = New System.Drawing.Point(13, 26)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(331, 184)
        Me.GroupBox5.TabIndex = 0
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Printer1"
        '
        'txtAlarmPrinterIP11
        '
        Me.txtAlarmPrinterIP11.Location = New System.Drawing.Point(84, 49)
        Me.txtAlarmPrinterIP11.Name = "txtAlarmPrinterIP11"
        Me.txtAlarmPrinterIP11.Size = New System.Drawing.Size(29, 19)
        Me.txtAlarmPrinterIP11.TabIndex = 1
        Me.txtAlarmPrinterIP11.Text = "255"
        Me.txtAlarmPrinterIP11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(10, 156)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "Enable"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(10, 77)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(65, 12)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Print Part"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAlarmPrinterIP13
        '
        Me.txtAlarmPrinterIP13.Location = New System.Drawing.Point(153, 49)
        Me.txtAlarmPrinterIP13.Name = "txtAlarmPrinterIP13"
        Me.txtAlarmPrinterIP13.Size = New System.Drawing.Size(29, 19)
        Me.txtAlarmPrinterIP13.TabIndex = 3
        Me.txtAlarmPrinterIP13.Text = "255"
        Me.txtAlarmPrinterIP13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(10, 131)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(41, 12)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "Device"
        '
        'txtAlarmPrinterIP12
        '
        Me.txtAlarmPrinterIP12.Location = New System.Drawing.Point(118, 49)
        Me.txtAlarmPrinterIP12.Name = "txtAlarmPrinterIP12"
        Me.txtAlarmPrinterIP12.Size = New System.Drawing.Size(29, 19)
        Me.txtAlarmPrinterIP12.TabIndex = 2
        Me.txtAlarmPrinterIP12.Text = "255"
        Me.txtAlarmPrinterIP12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkAlarmPrinter1EnableBackup
        '
        Me.chkAlarmPrinter1EnableBackup.AutoSize = True
        Me.chkAlarmPrinter1EnableBackup.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmPrinter1EnableBackup.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmPrinter1EnableBackup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmPrinter1EnableBackup.Location = New System.Drawing.Point(84, 155)
        Me.chkAlarmPrinter1EnableBackup.Name = "chkAlarmPrinter1EnableBackup"
        Me.chkAlarmPrinter1EnableBackup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmPrinter1EnableBackup.Size = New System.Drawing.Size(96, 16)
        Me.chkAlarmPrinter1EnableBackup.TabIndex = 10
        Me.chkAlarmPrinter1EnableBackup.Text = "Backup Print"
        Me.chkAlarmPrinter1EnableBackup.UseVisualStyleBackColor = True
        '
        'txtAlarmPrinterIP14
        '
        Me.txtAlarmPrinterIP14.Location = New System.Drawing.Point(188, 49)
        Me.txtAlarmPrinterIP14.Name = "txtAlarmPrinterIP14"
        Me.txtAlarmPrinterIP14.Size = New System.Drawing.Size(29, 19)
        Me.txtAlarmPrinterIP14.TabIndex = 4
        Me.txtAlarmPrinterIP14.Text = "255"
        Me.txtAlarmPrinterIP14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(10, 23)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(47, 12)
        Me.Label43.TabIndex = 6
        Me.Label43.Text = "Printer"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(181, 55)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(11, 12)
        Me.Label26.TabIndex = 59
        Me.Label26.Text = "."
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(10, 104)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(41, 12)
        Me.Label46.TabIndex = 41
        Me.Label46.Text = "Driver"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.SystemColors.Control
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label47.Location = New System.Drawing.Point(10, 52)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(17, 12)
        Me.Label47.TabIndex = 8
        Me.Label47.Text = "IP"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(146, 55)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(11, 12)
        Me.Label28.TabIndex = 56
        Me.Label28.Text = "."
        '
        'cmbAlarmPrinter1
        '
        Me.cmbAlarmPrinter1.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAlarmPrinter1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbAlarmPrinter1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlarmPrinter1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAlarmPrinter1.Location = New System.Drawing.Point(84, 20)
        Me.cmbAlarmPrinter1.Name = "cmbAlarmPrinter1"
        Me.cmbAlarmPrinter1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAlarmPrinter1.Size = New System.Drawing.Size(132, 20)
        Me.cmbAlarmPrinter1.TabIndex = 0
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(111, 55)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(11, 12)
        Me.Label32.TabIndex = 51
        Me.Label32.Text = "."
        '
        'chkEventPrintNone
        '
        Me.chkEventPrintNone.AutoSize = True
        Me.chkEventPrintNone.BackColor = System.Drawing.SystemColors.Control
        Me.chkEventPrintNone.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkEventPrintNone.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEventPrintNone.Location = New System.Drawing.Point(238, 223)
        Me.chkEventPrintNone.Name = "chkEventPrintNone"
        Me.chkEventPrintNone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkEventPrintNone.Size = New System.Drawing.Size(120, 16)
        Me.chkEventPrintNone.TabIndex = 3
        Me.chkEventPrintNone.Text = "Event Print None"
        Me.chkEventPrintNone.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(10, 224)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(65, 12)
        Me.Label18.TabIndex = 39
        Me.Label18.Text = "Print Type"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label18.Visible = False
        '
        'fraLogPrinter
        '
        Me.fraLogPrinter.BackColor = System.Drawing.SystemColors.Control
        Me.fraLogPrinter.Controls.Add(Me.GroupBox4)
        Me.fraLogPrinter.Controls.Add(Me.GroupBox3)
        Me.fraLogPrinter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraLogPrinter.Location = New System.Drawing.Point(16, 8)
        Me.fraLogPrinter.Name = "fraLogPrinter"
        Me.fraLogPrinter.Padding = New System.Windows.Forms.Padding(0)
        Me.fraLogPrinter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraLogPrinter.Size = New System.Drawing.Size(709, 279)
        Me.fraLogPrinter.TabIndex = 0
        Me.fraLogPrinter.TabStop = False
        Me.fraLogPrinter.Text = "Log Printer"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cmbPrinterNameL2)
        Me.GroupBox4.Controls.Add(Me.Label53)
        Me.GroupBox4.Controls.Add(Me.chkLogPrinter2Color)
        Me.GroupBox4.Controls.Add(Me.Label22)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.chkLogPrinter2PaperSizeA3)
        Me.GroupBox4.Controls.Add(Me.txtLogPrinter2Device)
        Me.GroupBox4.Controls.Add(Me.chkLogPrinter2Cargo)
        Me.GroupBox4.Controls.Add(Me.txtLogPrinter2Driver)
        Me.GroupBox4.Controls.Add(Me.txtLogPrinterIP23)
        Me.GroupBox4.Controls.Add(Me.chkLogPrinter2Machinery)
        Me.GroupBox4.Controls.Add(Me.Label37)
        Me.GroupBox4.Controls.Add(Me.txtLogPrinterIP22)
        Me.GroupBox4.Controls.Add(Me.Label38)
        Me.GroupBox4.Controls.Add(Me.txtLogPrinterIP24)
        Me.GroupBox4.Controls.Add(Me.Label39)
        Me.GroupBox4.Controls.Add(Me.chkLogPrinter2EnableBackup)
        Me.GroupBox4.Controls.Add(Me.Label24)
        Me.GroupBox4.Controls.Add(Me.Label23)
        Me.GroupBox4.Controls.Add(Me.Label40)
        Me.GroupBox4.Controls.Add(Me.txtLogPrinterIP21)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.cmbLogPrinter2)
        Me.GroupBox4.Controls.Add(Me.Label41)
        Me.GroupBox4.Controls.Add(Me.Label44)
        Me.GroupBox4.Location = New System.Drawing.Point(360, 15)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(331, 252)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Printer2"
        '
        'cmbPrinterNameL2
        '
        Me.cmbPrinterNameL2.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPrinterNameL2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPrinterNameL2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPrinterNameL2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPrinterNameL2.Location = New System.Drawing.Point(83, 102)
        Me.cmbPrinterNameL2.Name = "cmbPrinterNameL2"
        Me.cmbPrinterNameL2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPrinterNameL2.Size = New System.Drawing.Size(133, 20)
        Me.cmbPrinterNameL2.TabIndex = 53
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.SystemColors.Control
        Me.Label53.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label53.Location = New System.Drawing.Point(10, 105)
        Me.Label53.Name = "Label53"
        Me.Label53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label53.Size = New System.Drawing.Size(71, 12)
        Me.Label53.TabIndex = 54
        Me.Label53.Text = "PrinterName"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkLogPrinter2Color
        '
        Me.chkLogPrinter2Color.AutoSize = True
        Me.chkLogPrinter2Color.BackColor = System.Drawing.SystemColors.Control
        Me.chkLogPrinter2Color.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLogPrinter2Color.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLogPrinter2Color.Location = New System.Drawing.Point(83, 226)
        Me.chkLogPrinter2Color.Name = "chkLogPrinter2Color"
        Me.chkLogPrinter2Color.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLogPrinter2Color.Size = New System.Drawing.Size(90, 16)
        Me.chkLogPrinter2Color.TabIndex = 51
        Me.chkLogPrinter2Color.Text = "Color Print"
        Me.chkLogPrinter2Color.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(8, 227)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(35, 12)
        Me.Label22.TabIndex = 50
        Me.Label22.Text = "Color"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(8, 206)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(65, 12)
        Me.Label15.TabIndex = 49
        Me.Label15.Text = "Paper Size"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkLogPrinter2PaperSizeA3
        '
        Me.chkLogPrinter2PaperSizeA3.AutoSize = True
        Me.chkLogPrinter2PaperSizeA3.BackColor = System.Drawing.SystemColors.Control
        Me.chkLogPrinter2PaperSizeA3.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLogPrinter2PaperSizeA3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLogPrinter2PaperSizeA3.Location = New System.Drawing.Point(83, 205)
        Me.chkLogPrinter2PaperSizeA3.Name = "chkLogPrinter2PaperSizeA3"
        Me.chkLogPrinter2PaperSizeA3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLogPrinter2PaperSizeA3.Size = New System.Drawing.Size(36, 16)
        Me.chkLogPrinter2PaperSizeA3.TabIndex = 11
        Me.chkLogPrinter2PaperSizeA3.Text = "A3"
        Me.chkLogPrinter2PaperSizeA3.UseVisualStyleBackColor = True
        '
        'chkLogPrinter2Cargo
        '
        Me.chkLogPrinter2Cargo.AutoSize = True
        Me.chkLogPrinter2Cargo.BackColor = System.Drawing.SystemColors.Control
        Me.chkLogPrinter2Cargo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLogPrinter2Cargo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLogPrinter2Cargo.Location = New System.Drawing.Point(167, 76)
        Me.chkLogPrinter2Cargo.Name = "chkLogPrinter2Cargo"
        Me.chkLogPrinter2Cargo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLogPrinter2Cargo.Size = New System.Drawing.Size(54, 16)
        Me.chkLogPrinter2Cargo.TabIndex = 6
        Me.chkLogPrinter2Cargo.Text = "Cargo"
        Me.chkLogPrinter2Cargo.UseVisualStyleBackColor = True
        '
        'txtLogPrinterIP23
        '
        Me.txtLogPrinterIP23.Location = New System.Drawing.Point(152, 49)
        Me.txtLogPrinterIP23.Name = "txtLogPrinterIP23"
        Me.txtLogPrinterIP23.Size = New System.Drawing.Size(29, 19)
        Me.txtLogPrinterIP23.TabIndex = 3
        Me.txtLogPrinterIP23.Text = "255"
        Me.txtLogPrinterIP23.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkLogPrinter2Machinery
        '
        Me.chkLogPrinter2Machinery.AutoSize = True
        Me.chkLogPrinter2Machinery.BackColor = System.Drawing.SystemColors.Control
        Me.chkLogPrinter2Machinery.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLogPrinter2Machinery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLogPrinter2Machinery.Location = New System.Drawing.Point(83, 76)
        Me.chkLogPrinter2Machinery.Name = "chkLogPrinter2Machinery"
        Me.chkLogPrinter2Machinery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLogPrinter2Machinery.Size = New System.Drawing.Size(78, 16)
        Me.chkLogPrinter2Machinery.TabIndex = 5
        Me.chkLogPrinter2Machinery.Text = "Machinery"
        Me.chkLogPrinter2Machinery.UseVisualStyleBackColor = True
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(8, 183)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(41, 12)
        Me.Label37.TabIndex = 47
        Me.Label37.Text = "Enable"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLogPrinterIP22
        '
        Me.txtLogPrinterIP22.Location = New System.Drawing.Point(117, 49)
        Me.txtLogPrinterIP22.Name = "txtLogPrinterIP22"
        Me.txtLogPrinterIP22.Size = New System.Drawing.Size(29, 19)
        Me.txtLogPrinterIP22.TabIndex = 2
        Me.txtLogPrinterIP22.Text = "255"
        Me.txtLogPrinterIP22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(8, 77)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(65, 12)
        Me.Label38.TabIndex = 10
        Me.Label38.Text = "Print Part"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLogPrinterIP24
        '
        Me.txtLogPrinterIP24.Location = New System.Drawing.Point(187, 49)
        Me.txtLogPrinterIP24.Name = "txtLogPrinterIP24"
        Me.txtLogPrinterIP24.Size = New System.Drawing.Size(29, 19)
        Me.txtLogPrinterIP24.TabIndex = 4
        Me.txtLogPrinterIP24.Text = "255"
        Me.txtLogPrinterIP24.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(8, 158)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(41, 12)
        Me.Label39.TabIndex = 45
        Me.Label39.Text = "Device"
        '
        'chkLogPrinter2EnableBackup
        '
        Me.chkLogPrinter2EnableBackup.AutoSize = True
        Me.chkLogPrinter2EnableBackup.BackColor = System.Drawing.SystemColors.Control
        Me.chkLogPrinter2EnableBackup.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLogPrinter2EnableBackup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLogPrinter2EnableBackup.Location = New System.Drawing.Point(83, 182)
        Me.chkLogPrinter2EnableBackup.Name = "chkLogPrinter2EnableBackup"
        Me.chkLogPrinter2EnableBackup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLogPrinter2EnableBackup.Size = New System.Drawing.Size(96, 16)
        Me.chkLogPrinter2EnableBackup.TabIndex = 10
        Me.chkLogPrinter2EnableBackup.Text = "Backup Print"
        Me.chkLogPrinter2EnableBackup.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(180, 55)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(11, 12)
        Me.Label24.TabIndex = 46
        Me.Label24.Text = "."
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(145, 55)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(11, 12)
        Me.Label23.TabIndex = 44
        Me.Label23.Text = "."
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(8, 23)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(47, 12)
        Me.Label40.TabIndex = 6
        Me.Label40.Text = "Printer"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLogPrinterIP21
        '
        Me.txtLogPrinterIP21.Location = New System.Drawing.Point(83, 49)
        Me.txtLogPrinterIP21.Name = "txtLogPrinterIP21"
        Me.txtLogPrinterIP21.Size = New System.Drawing.Size(29, 19)
        Me.txtLogPrinterIP21.TabIndex = 1
        Me.txtLogPrinterIP21.Text = "255"
        Me.txtLogPrinterIP21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(110, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(11, 12)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "."
        '
        'cmbLogPrinter2
        '
        Me.cmbLogPrinter2.BackColor = System.Drawing.SystemColors.Window
        Me.cmbLogPrinter2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbLogPrinter2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLogPrinter2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbLogPrinter2.Location = New System.Drawing.Point(83, 20)
        Me.cmbLogPrinter2.Name = "cmbLogPrinter2"
        Me.cmbLogPrinter2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbLogPrinter2.Size = New System.Drawing.Size(133, 20)
        Me.cmbLogPrinter2.TabIndex = 0
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(8, 131)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(41, 12)
        Me.Label41.TabIndex = 41
        Me.Label41.Text = "Driver"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(8, 52)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(17, 12)
        Me.Label44.TabIndex = 8
        Me.Label44.Text = "IP"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbPrinterNameL1)
        Me.GroupBox3.Controls.Add(Me.Label52)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.chkLogPrinter1Color)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.txtLogPrinterIP11)
        Me.GroupBox3.Controls.Add(Me.chkLogPrinter1PaperSizeA3)
        Me.GroupBox3.Controls.Add(Me.Label36)
        Me.GroupBox3.Controls.Add(Me.cmbLogPrinter1)
        Me.GroupBox3.Controls.Add(Me.txtLogPrinter1Device)
        Me.GroupBox3.Controls.Add(Me.chkLogPrinter1Cargo)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label20)
        Me.GroupBox3.Controls.Add(Me.txtLogPrinter1Driver)
        Me.GroupBox3.Controls.Add(Me.txtLogPrinterIP13)
        Me.GroupBox3.Controls.Add(Me.chkLogPrinter1EnableBackup)
        Me.GroupBox3.Controls.Add(Me.chkLogPrinter1Machinery)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label111)
        Me.GroupBox3.Controls.Add(Me.txtLogPrinterIP14)
        Me.GroupBox3.Controls.Add(Me.txtLogPrinterIP12)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 15)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(331, 252)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Printer1"
        '
        'cmbPrinterNameL1
        '
        Me.cmbPrinterNameL1.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPrinterNameL1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPrinterNameL1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPrinterNameL1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPrinterNameL1.Location = New System.Drawing.Point(83, 102)
        Me.cmbPrinterNameL1.Name = "cmbPrinterNameL1"
        Me.cmbPrinterNameL1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPrinterNameL1.Size = New System.Drawing.Size(133, 20)
        Me.cmbPrinterNameL1.TabIndex = 51
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.BackColor = System.Drawing.SystemColors.Control
        Me.Label52.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label52.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label52.Location = New System.Drawing.Point(10, 105)
        Me.Label52.Name = "Label52"
        Me.Label52.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label52.Size = New System.Drawing.Size(71, 12)
        Me.Label52.TabIndex = 52
        Me.Label52.Text = "PrinterName"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(10, 228)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(35, 12)
        Me.Label19.TabIndex = 50
        Me.Label19.Text = "Color"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkLogPrinter1Color
        '
        Me.chkLogPrinter1Color.AutoSize = True
        Me.chkLogPrinter1Color.BackColor = System.Drawing.SystemColors.Control
        Me.chkLogPrinter1Color.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLogPrinter1Color.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLogPrinter1Color.Location = New System.Drawing.Point(83, 227)
        Me.chkLogPrinter1Color.Name = "chkLogPrinter1Color"
        Me.chkLogPrinter1Color.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLogPrinter1Color.Size = New System.Drawing.Size(90, 16)
        Me.chkLogPrinter1Color.TabIndex = 49
        Me.chkLogPrinter1Color.Text = "Color Print"
        Me.chkLogPrinter1Color.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(10, 206)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "Paper Size"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLogPrinterIP11
        '
        Me.txtLogPrinterIP11.Location = New System.Drawing.Point(83, 49)
        Me.txtLogPrinterIP11.Name = "txtLogPrinterIP11"
        Me.txtLogPrinterIP11.Size = New System.Drawing.Size(29, 19)
        Me.txtLogPrinterIP11.TabIndex = 1
        Me.txtLogPrinterIP11.Text = "255"
        Me.txtLogPrinterIP11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkLogPrinter1PaperSizeA3
        '
        Me.chkLogPrinter1PaperSizeA3.AutoSize = True
        Me.chkLogPrinter1PaperSizeA3.BackColor = System.Drawing.SystemColors.Control
        Me.chkLogPrinter1PaperSizeA3.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLogPrinter1PaperSizeA3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLogPrinter1PaperSizeA3.Location = New System.Drawing.Point(83, 205)
        Me.chkLogPrinter1PaperSizeA3.Name = "chkLogPrinter1PaperSizeA3"
        Me.chkLogPrinter1PaperSizeA3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLogPrinter1PaperSizeA3.Size = New System.Drawing.Size(36, 16)
        Me.chkLogPrinter1PaperSizeA3.TabIndex = 11
        Me.chkLogPrinter1PaperSizeA3.Text = "A3"
        Me.chkLogPrinter1PaperSizeA3.UseVisualStyleBackColor = True
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(10, 183)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(41, 12)
        Me.Label36.TabIndex = 47
        Me.Label36.Text = "Enable"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbLogPrinter1
        '
        Me.cmbLogPrinter1.BackColor = System.Drawing.SystemColors.Window
        Me.cmbLogPrinter1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbLogPrinter1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLogPrinter1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbLogPrinter1.Location = New System.Drawing.Point(83, 20)
        Me.cmbLogPrinter1.Name = "cmbLogPrinter1"
        Me.cmbLogPrinter1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbLogPrinter1.Size = New System.Drawing.Size(133, 20)
        Me.cmbLogPrinter1.TabIndex = 0
        '
        'chkLogPrinter1Cargo
        '
        Me.chkLogPrinter1Cargo.AutoSize = True
        Me.chkLogPrinter1Cargo.BackColor = System.Drawing.SystemColors.Control
        Me.chkLogPrinter1Cargo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLogPrinter1Cargo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLogPrinter1Cargo.Location = New System.Drawing.Point(167, 76)
        Me.chkLogPrinter1Cargo.Name = "chkLogPrinter1Cargo"
        Me.chkLogPrinter1Cargo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLogPrinter1Cargo.Size = New System.Drawing.Size(54, 16)
        Me.chkLogPrinter1Cargo.TabIndex = 6
        Me.chkLogPrinter1Cargo.Text = "Cargo"
        Me.chkLogPrinter1Cargo.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(10, 77)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(65, 12)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Print Part"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLogPrinterIP13
        '
        Me.txtLogPrinterIP13.Location = New System.Drawing.Point(152, 49)
        Me.txtLogPrinterIP13.Name = "txtLogPrinterIP13"
        Me.txtLogPrinterIP13.Size = New System.Drawing.Size(29, 19)
        Me.txtLogPrinterIP13.TabIndex = 3
        Me.txtLogPrinterIP13.Text = "255"
        Me.txtLogPrinterIP13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkLogPrinter1EnableBackup
        '
        Me.chkLogPrinter1EnableBackup.AutoSize = True
        Me.chkLogPrinter1EnableBackup.BackColor = System.Drawing.SystemColors.Control
        Me.chkLogPrinter1EnableBackup.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLogPrinter1EnableBackup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLogPrinter1EnableBackup.Location = New System.Drawing.Point(83, 183)
        Me.chkLogPrinter1EnableBackup.Name = "chkLogPrinter1EnableBackup"
        Me.chkLogPrinter1EnableBackup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLogPrinter1EnableBackup.Size = New System.Drawing.Size(96, 16)
        Me.chkLogPrinter1EnableBackup.TabIndex = 10
        Me.chkLogPrinter1EnableBackup.Text = "Backup Print"
        Me.chkLogPrinter1EnableBackup.UseVisualStyleBackColor = True
        '
        'chkLogPrinter1Machinery
        '
        Me.chkLogPrinter1Machinery.AutoSize = True
        Me.chkLogPrinter1Machinery.BackColor = System.Drawing.SystemColors.Control
        Me.chkLogPrinter1Machinery.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLogPrinter1Machinery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLogPrinter1Machinery.Location = New System.Drawing.Point(83, 76)
        Me.chkLogPrinter1Machinery.Name = "chkLogPrinter1Machinery"
        Me.chkLogPrinter1Machinery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLogPrinter1Machinery.Size = New System.Drawing.Size(78, 16)
        Me.chkLogPrinter1Machinery.TabIndex = 5
        Me.chkLogPrinter1Machinery.Text = "Machinery"
        Me.chkLogPrinter1Machinery.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(10, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(47, 12)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Printer"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLogPrinterIP14
        '
        Me.txtLogPrinterIP14.Location = New System.Drawing.Point(187, 49)
        Me.txtLogPrinterIP14.Name = "txtLogPrinterIP14"
        Me.txtLogPrinterIP14.Size = New System.Drawing.Size(29, 19)
        Me.txtLogPrinterIP14.TabIndex = 4
        Me.txtLogPrinterIP14.Text = "255"
        Me.txtLogPrinterIP14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtLogPrinterIP12
        '
        Me.txtLogPrinterIP12.Location = New System.Drawing.Point(117, 49)
        Me.txtLogPrinterIP12.Name = "txtLogPrinterIP12"
        Me.txtLogPrinterIP12.Size = New System.Drawing.Size(29, 19)
        Me.txtLogPrinterIP12.TabIndex = 2
        Me.txtLogPrinterIP12.Text = "255"
        Me.txtLogPrinterIP12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(110, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(11, 12)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(145, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(11, 12)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(10, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "IP"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(180, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(11, 12)
        Me.Label8.TabIndex = 46
        Me.Label8.Text = "."
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.BackColor = System.Drawing.SystemColors.Control
        Me.Label113.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label113.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label113.Location = New System.Drawing.Point(648, 48)
        Me.Label113.Name = "Label113"
        Me.Label113.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label113.Size = New System.Drawing.Size(53, 12)
        Me.Label113.TabIndex = 43
        Me.Label113.Text = "Printer1"
        Me.Label113.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkMachineryCargoPrint
        '
        Me.chkMachineryCargoPrint.AutoSize = True
        Me.chkMachineryCargoPrint.BackColor = System.Drawing.SystemColors.Control
        Me.chkMachineryCargoPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMachineryCargoPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMachineryCargoPrint.Location = New System.Drawing.Point(741, 346)
        Me.chkMachineryCargoPrint.Name = "chkMachineryCargoPrint"
        Me.chkMachineryCargoPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMachineryCargoPrint.Size = New System.Drawing.Size(150, 16)
        Me.chkMachineryCargoPrint.TabIndex = 5
        Me.chkMachineryCargoPrint.Text = "Machinery/Cargo Print"
        Me.chkMachineryCargoPrint.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(741, 371)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(161, 12)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "Auto Print Events per page"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAutoCnt
        '
        Me.txtAutoCnt.AcceptsReturn = True
        Me.txtAutoCnt.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtAutoCnt.Location = New System.Drawing.Point(917, 368)
        Me.txtAutoCnt.MaxLength = 16
        Me.txtAutoCnt.Name = "txtAutoCnt"
        Me.txtAutoCnt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAutoCnt.Size = New System.Drawing.Size(51, 19)
        Me.txtAutoCnt.TabIndex = 7
        Me.txtAutoCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmSysPrinter
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(1118, 600)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtAutoCnt)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.chkMachineryCargoPrint)
        Me.Controls.Add(Me.chkDemandLog)
        Me.Controls.Add(Me.chkNoonLog)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.fraHCPrinter)
        Me.Controls.Add(Me.fraAlarmPrinter)
        Me.Controls.Add(Me.fraLogPrinter)
        Me.Controls.Add(Me.Label113)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSysPrinter"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "PRINTER SETTING"
        Me.fraHCPrinter.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.fraAlarmPrinter.ResumeLayout(False)
        Me.fraAlarmPrinter.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.fraLogPrinter.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtLogPrinter1Device As System.Windows.Forms.TextBox
    Public WithEvents txtHCPrinterDevice As System.Windows.Forms.TextBox
    Public WithEvents txtAlarmPrinter2Device As System.Windows.Forms.TextBox
    Public WithEvents txtAlarmPrinter1Device As System.Windows.Forms.TextBox
    Public WithEvents txtLogPrinter2Device As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents chkMachineryCargoPrint As System.Windows.Forms.CheckBox
    Friend WithEvents txtLogPrinterIP13 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtLogPrinterIP12 As System.Windows.Forms.TextBox
    Friend WithEvents txtLogPrinterIP11 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtLogPrinterIP24 As System.Windows.Forms.TextBox
    Friend WithEvents txtLogPrinterIP14 As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtLogPrinterIP23 As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtLogPrinterIP22 As System.Windows.Forms.TextBox
    Friend WithEvents txtLogPrinterIP21 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtHcPrinterIP4 As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtHcPrinterIP3 As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtHcPrinterIP2 As System.Windows.Forms.TextBox
    Friend WithEvents txtHcPrinterIP1 As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtAlarmPrinterIP24 As System.Windows.Forms.TextBox
    Friend WithEvents txtAlarmPrinterIP14 As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtAlarmPrinterIP23 As System.Windows.Forms.TextBox
    Friend WithEvents txtAlarmPrinterIP13 As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtAlarmPrinterIP22 As System.Windows.Forms.TextBox
    Friend WithEvents txtAlarmPrinterIP12 As System.Windows.Forms.TextBox
    Friend WithEvents txtAlarmPrinterIP21 As System.Windows.Forms.TextBox
    Friend WithEvents txtAlarmPrinterIP11 As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents chkLogPrinter1EnableBackup As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents chkLogPrinter2EnableBackup As System.Windows.Forms.CheckBox
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents chkAlarmPrinter1EnableBackup As System.Windows.Forms.CheckBox
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents chkAlarmPrinter2EnableBackup As System.Windows.Forms.CheckBox
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents Label49 As System.Windows.Forms.Label
    Public WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents chkHcPrinter1EnableBackup As System.Windows.Forms.CheckBox
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label51 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents chkLogPrinter2PaperSizeA3 As System.Windows.Forms.CheckBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents chkHCPrinter1PaperSizeA3 As System.Windows.Forms.CheckBox
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents txtAutoCnt As System.Windows.Forms.TextBox
    Public WithEvents chkLogPrinter2Color As System.Windows.Forms.CheckBox
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents chkLogPrinter1Color As System.Windows.Forms.CheckBox
    Public WithEvents cmbPrinterNameHC As System.Windows.Forms.ComboBox
    Public WithEvents Label54 As System.Windows.Forms.Label
    Public WithEvents cmbPrinterNameL2 As System.Windows.Forms.ComboBox
    Public WithEvents Label53 As System.Windows.Forms.Label
    Public WithEvents cmbPrinterNameL1 As System.Windows.Forms.ComboBox
    Public WithEvents Label52 As System.Windows.Forms.Label
#End Region

End Class
