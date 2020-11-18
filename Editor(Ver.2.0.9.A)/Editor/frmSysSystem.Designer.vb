<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSysSystem
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

    Public WithEvents txtShipName As System.Windows.Forms.TextBox
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmbEthernetLine2 As System.Windows.Forms.ComboBox
    Public WithEvents chkGWS2 As System.Windows.Forms.CheckBox
    Public WithEvents cmbEthernetLine1 As System.Windows.Forms.ComboBox
    Public WithEvents chkGWS1 As System.Windows.Forms.CheckBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents fraGWS As System.Windows.Forms.GroupBox
    Public WithEvents cmbLanguage As System.Windows.Forms.ComboBox
    Public WithEvents cmbDataFormat As System.Windows.Forms.ComboBox
    Public WithEvents cmbSystemClock As System.Windows.Forms.ComboBox
    Public WithEvents chkSeparate As System.Windows.Forms.CheckBox
    Public WithEvents cmbCombine As System.Windows.Forms.ComboBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents fraCombine As System.Windows.Forms.GroupBox
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtShipName = New System.Windows.Forms.TextBox()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.fraGWS = New System.Windows.Forms.GroupBox()
        Me.cmbEthernetLine2 = New System.Windows.Forms.ComboBox()
        Me.chkGWS2 = New System.Windows.Forms.CheckBox()
        Me.cmbEthernetLine1 = New System.Windows.Forms.ComboBox()
        Me.chkGWS1 = New System.Windows.Forms.CheckBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbLanguage = New System.Windows.Forms.ComboBox()
        Me.cmbDataFormat = New System.Windows.Forms.ComboBox()
        Me.cmbSystemClock = New System.Windows.Forms.ComboBox()
        Me.fraCombine = New System.Windows.Forms.GroupBox()
        Me.chkSeparate = New System.Windows.Forms.CheckBox()
        Me.cmbCombine = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chkCombine = New System.Windows.Forms.CheckBox()
        Me.chkSystemStatusNone = New System.Windows.Forms.CheckBox()
        Me.chkSystemStatusFCU = New System.Windows.Forms.CheckBox()
        Me.chkSystemStatusOPS = New System.Windows.Forms.CheckBox()
        Me.fraSystemStatus = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbManual = New System.Windows.Forms.ComboBox()
        Me.cmbGL_SPEC = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkHistoryAuto = New System.Windows.Forms.CheckBox()
        Me.fraBS_FS = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFSCHNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBSCHNo = New System.Windows.Forms.TextBox()
        Me.fra_FCU_Set = New System.Windows.Forms.GroupBox()
        Me.chkPtJPt = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkDataSW = New System.Windows.Forms.CheckBox()
        Me.cmbPart = New System.Windows.Forms.ComboBox()
        Me.chkFCU = New System.Windows.Forms.CheckBox()
        Me.CmbFCUCnt = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkShareChUse = New System.Windows.Forms.CheckBox()
        Me.numCorrectTime = New System.Windows.Forms.NumericUpDown()
        Me.chkSIO = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbFCUNo = New System.Windows.Forms.ComboBox()
        Me.chkEventLogBackup = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtHoanGno = New System.Windows.Forms.TextBox()
        Me.chkHOAN = New System.Windows.Forms.CheckBox()
        Me.chkNEWDES = New System.Windows.Forms.CheckBox()
        Me.grpHoan = New System.Windows.Forms.GroupBox()
        Me.optMimic = New System.Windows.Forms.RadioButton()
        Me.optOverView = New System.Windows.Forms.RadioButton()
        Me.grpMode = New System.Windows.Forms.GroupBox()
        Me.chkDataSW2 = New System.Windows.Forms.CheckBox()
        Me.fraGWS.SuspendLayout()
        Me.fraCombine.SuspendLayout()
        Me.fraSystemStatus.SuspendLayout()
        Me.fraBS_FS.SuspendLayout()
        Me.fra_FCU_Set.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.numCorrectTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHoan.SuspendLayout()
        Me.grpMode.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtShipName
        '
        Me.txtShipName.AcceptsReturn = True
        Me.txtShipName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtShipName.Location = New System.Drawing.Point(261, 38)
        Me.txtShipName.MaxLength = 0
        Me.txtShipName.Name = "txtShipName"
        Me.txtShipName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShipName.Size = New System.Drawing.Size(244, 19)
        Me.txtShipName.TabIndex = 6
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(445, 383)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 9
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(321, 383)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 8
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'fraGWS
        '
        Me.fraGWS.BackColor = System.Drawing.SystemColors.Control
        Me.fraGWS.Controls.Add(Me.cmbEthernetLine2)
        Me.fraGWS.Controls.Add(Me.chkGWS2)
        Me.fraGWS.Controls.Add(Me.cmbEthernetLine1)
        Me.fraGWS.Controls.Add(Me.chkGWS1)
        Me.fraGWS.Controls.Add(Me.Label14)
        Me.fraGWS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraGWS.Location = New System.Drawing.Point(468, 12)
        Me.fraGWS.Name = "fraGWS"
        Me.fraGWS.Padding = New System.Windows.Forms.Padding(0)
        Me.fraGWS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraGWS.Size = New System.Drawing.Size(40, 19)
        Me.fraGWS.TabIndex = 7
        Me.fraGWS.TabStop = False
        Me.fraGWS.Text = "GWS"
        Me.fraGWS.Visible = False
        '
        'cmbEthernetLine2
        '
        Me.cmbEthernetLine2.BackColor = System.Drawing.SystemColors.Window
        Me.cmbEthernetLine2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbEthernetLine2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEthernetLine2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbEthernetLine2.Location = New System.Drawing.Point(120, 72)
        Me.cmbEthernetLine2.Name = "cmbEthernetLine2"
        Me.cmbEthernetLine2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbEthernetLine2.Size = New System.Drawing.Size(105, 20)
        Me.cmbEthernetLine2.TabIndex = 3
        '
        'chkGWS2
        '
        Me.chkGWS2.AutoSize = True
        Me.chkGWS2.BackColor = System.Drawing.SystemColors.Control
        Me.chkGWS2.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGWS2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGWS2.Location = New System.Drawing.Point(48, 76)
        Me.chkGWS2.Name = "chkGWS2"
        Me.chkGWS2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGWS2.Size = New System.Drawing.Size(48, 16)
        Me.chkGWS2.TabIndex = 2
        Me.chkGWS2.Text = "GWS2"
        Me.chkGWS2.UseVisualStyleBackColor = True
        '
        'cmbEthernetLine1
        '
        Me.cmbEthernetLine1.BackColor = System.Drawing.SystemColors.Window
        Me.cmbEthernetLine1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbEthernetLine1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEthernetLine1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbEthernetLine1.Location = New System.Drawing.Point(120, 36)
        Me.cmbEthernetLine1.Name = "cmbEthernetLine1"
        Me.cmbEthernetLine1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbEthernetLine1.Size = New System.Drawing.Size(105, 20)
        Me.cmbEthernetLine1.TabIndex = 1
        '
        'chkGWS1
        '
        Me.chkGWS1.AutoSize = True
        Me.chkGWS1.BackColor = System.Drawing.SystemColors.Control
        Me.chkGWS1.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGWS1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGWS1.Location = New System.Drawing.Point(48, 40)
        Me.chkGWS1.Name = "chkGWS1"
        Me.chkGWS1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGWS1.Size = New System.Drawing.Size(48, 16)
        Me.chkGWS1.TabIndex = 0
        Me.chkGWS1.Text = "GWS1"
        Me.chkGWS1.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(134, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(83, 12)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "Ethernet Line"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbLanguage
        '
        Me.cmbLanguage.BackColor = System.Drawing.SystemColors.Window
        Me.cmbLanguage.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLanguage.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbLanguage.Location = New System.Drawing.Point(136, 80)
        Me.cmbLanguage.Name = "cmbLanguage"
        Me.cmbLanguage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbLanguage.Size = New System.Drawing.Size(105, 20)
        Me.cmbLanguage.TabIndex = 2
        '
        'cmbDataFormat
        '
        Me.cmbDataFormat.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDataFormat.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDataFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataFormat.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDataFormat.Location = New System.Drawing.Point(136, 48)
        Me.cmbDataFormat.Name = "cmbDataFormat"
        Me.cmbDataFormat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDataFormat.Size = New System.Drawing.Size(105, 20)
        Me.cmbDataFormat.TabIndex = 1
        '
        'cmbSystemClock
        '
        Me.cmbSystemClock.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSystemClock.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbSystemClock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSystemClock.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSystemClock.Location = New System.Drawing.Point(136, 16)
        Me.cmbSystemClock.Name = "cmbSystemClock"
        Me.cmbSystemClock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbSystemClock.Size = New System.Drawing.Size(105, 20)
        Me.cmbSystemClock.TabIndex = 0
        '
        'fraCombine
        '
        Me.fraCombine.BackColor = System.Drawing.SystemColors.Control
        Me.fraCombine.Controls.Add(Me.chkDataSW2)
        Me.fraCombine.Controls.Add(Me.chkSeparate)
        Me.fraCombine.Controls.Add(Me.cmbCombine)
        Me.fraCombine.Controls.Add(Me.Label13)
        Me.fraCombine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCombine.Location = New System.Drawing.Point(18, 288)
        Me.fraCombine.Name = "fraCombine"
        Me.fraCombine.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCombine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCombine.Size = New System.Drawing.Size(205, 82)
        Me.fraCombine.TabIndex = 4
        Me.fraCombine.TabStop = False
        '
        'chkSeparate
        '
        Me.chkSeparate.BackColor = System.Drawing.SystemColors.Control
        Me.chkSeparate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSeparate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSeparate.Location = New System.Drawing.Point(10, 35)
        Me.chkSeparate.Name = "chkSeparate"
        Me.chkSeparate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSeparate.Size = New System.Drawing.Size(158, 16)
        Me.chkSeparate.TabIndex = 1
        Me.chkSeparate.Text = "fs/bs Separate"
        Me.chkSeparate.UseVisualStyleBackColor = True
        '
        'cmbCombine
        '
        Me.cmbCombine.BackColor = System.Drawing.SystemColors.Window
        Me.cmbCombine.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbCombine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCombine.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbCombine.Location = New System.Drawing.Point(61, 9)
        Me.cmbCombine.Name = "cmbCombine"
        Me.cmbCombine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbCombine.Size = New System.Drawing.Size(130, 20)
        Me.cmbCombine.TabIndex = 0
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(8, 12)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(47, 12)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Combine"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(262, 20)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(59, 12)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "Ship Name"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(35, 83)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(95, 12)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "System Language"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(28, 52)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(101, 12)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Date Format Code"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(16, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(113, 12)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "System Clock Setup"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkCombine
        '
        Me.chkCombine.AutoSize = True
        Me.chkCombine.BackColor = System.Drawing.SystemColors.Control
        Me.chkCombine.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCombine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCombine.Location = New System.Drawing.Point(18, 274)
        Me.chkCombine.Name = "chkCombine"
        Me.chkCombine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCombine.Size = New System.Drawing.Size(66, 16)
        Me.chkCombine.TabIndex = 26
        Me.chkCombine.Text = "Combine"
        Me.chkCombine.UseVisualStyleBackColor = True
        '
        'chkSystemStatusNone
        '
        Me.chkSystemStatusNone.BackColor = System.Drawing.SystemColors.Control
        Me.chkSystemStatusNone.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSystemStatusNone.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSystemStatusNone.Location = New System.Drawing.Point(44, 24)
        Me.chkSystemStatusNone.Name = "chkSystemStatusNone"
        Me.chkSystemStatusNone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSystemStatusNone.Size = New System.Drawing.Size(131, 18)
        Me.chkSystemStatusNone.TabIndex = 0
        Me.chkSystemStatusNone.Text = "None"
        Me.chkSystemStatusNone.UseVisualStyleBackColor = True
        '
        'chkSystemStatusFCU
        '
        Me.chkSystemStatusFCU.BackColor = System.Drawing.SystemColors.Control
        Me.chkSystemStatusFCU.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSystemStatusFCU.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSystemStatusFCU.Location = New System.Drawing.Point(44, 48)
        Me.chkSystemStatusFCU.Name = "chkSystemStatusFCU"
        Me.chkSystemStatusFCU.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSystemStatusFCU.Size = New System.Drawing.Size(181, 18)
        Me.chkSystemStatusFCU.TabIndex = 1
        Me.chkSystemStatusFCU.Text = "FCU A/B Change"
        Me.chkSystemStatusFCU.UseVisualStyleBackColor = True
        '
        'chkSystemStatusOPS
        '
        Me.chkSystemStatusOPS.BackColor = System.Drawing.SystemColors.Control
        Me.chkSystemStatusOPS.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSystemStatusOPS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSystemStatusOPS.Location = New System.Drawing.Point(44, 72)
        Me.chkSystemStatusOPS.Name = "chkSystemStatusOPS"
        Me.chkSystemStatusOPS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSystemStatusOPS.Size = New System.Drawing.Size(158, 17)
        Me.chkSystemStatusOPS.TabIndex = 2
        Me.chkSystemStatusOPS.Text = "OPS Status None"
        Me.chkSystemStatusOPS.UseVisualStyleBackColor = True
        '
        'fraSystemStatus
        '
        Me.fraSystemStatus.BackColor = System.Drawing.SystemColors.Control
        Me.fraSystemStatus.Controls.Add(Me.chkSystemStatusOPS)
        Me.fraSystemStatus.Controls.Add(Me.chkSystemStatusFCU)
        Me.fraSystemStatus.Controls.Add(Me.chkSystemStatusNone)
        Me.fraSystemStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSystemStatus.Location = New System.Drawing.Point(520, 11)
        Me.fraSystemStatus.Name = "fraSystemStatus"
        Me.fraSystemStatus.Padding = New System.Windows.Forms.Padding(0)
        Me.fraSystemStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSystemStatus.Size = New System.Drawing.Size(44, 20)
        Me.fraSystemStatus.TabIndex = 5
        Me.fraSystemStatus.TabStop = False
        Me.fraSystemStatus.Text = "System Status Auto Setting"
        Me.fraSystemStatus.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(35, 115)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(95, 12)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Manual Language"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbManual
        '
        Me.cmbManual.BackColor = System.Drawing.SystemColors.Window
        Me.cmbManual.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbManual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbManual.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbManual.Location = New System.Drawing.Point(136, 112)
        Me.cmbManual.Name = "cmbManual"
        Me.cmbManual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbManual.Size = New System.Drawing.Size(105, 20)
        Me.cmbManual.TabIndex = 3
        '
        'cmbGL_SPEC
        '
        Me.cmbGL_SPEC.BackColor = System.Drawing.SystemColors.Window
        Me.cmbGL_SPEC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbGL_SPEC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGL_SPEC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbGL_SPEC.Location = New System.Drawing.Point(136, 143)
        Me.cmbGL_SPEC.Name = "cmbGL_SPEC"
        Me.cmbGL_SPEC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbGL_SPEC.Size = New System.Drawing.Size(105, 20)
        Me.cmbGL_SPEC.TabIndex = 28
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(58, 143)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(71, 12)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "DNV GL Spec"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkHistoryAuto
        '
        Me.chkHistoryAuto.AutoSize = True
        Me.chkHistoryAuto.BackColor = System.Drawing.SystemColors.Control
        Me.chkHistoryAuto.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHistoryAuto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHistoryAuto.Location = New System.Drawing.Point(285, 63)
        Me.chkHistoryAuto.Name = "chkHistoryAuto"
        Me.chkHistoryAuto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHistoryAuto.Size = New System.Drawing.Size(168, 16)
        Me.chkHistoryAuto.TabIndex = 30
        Me.chkHistoryAuto.Text = "History Mode Auto Update"
        Me.chkHistoryAuto.UseVisualStyleBackColor = True
        '
        'fraBS_FS
        '
        Me.fraBS_FS.Controls.Add(Me.Label4)
        Me.fraBS_FS.Controls.Add(Me.txtFSCHNo)
        Me.fraBS_FS.Controls.Add(Me.Label3)
        Me.fraBS_FS.Controls.Add(Me.txtBSCHNo)
        Me.fraBS_FS.Location = New System.Drawing.Point(18, 188)
        Me.fraBS_FS.Name = "fraBS_FS"
        Me.fraBS_FS.Size = New System.Drawing.Size(168, 69)
        Me.fraBS_FS.TabIndex = 31
        Me.fraBS_FS.TabStop = False
        Me.fraBS_FS.Text = "BS/FS"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 12)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "FS OUT CHNo."
        '
        'txtFSCHNo
        '
        Me.txtFSCHNo.Location = New System.Drawing.Point(99, 41)
        Me.txtFSCHNo.Name = "txtFSCHNo"
        Me.txtFSCHNo.Size = New System.Drawing.Size(54, 19)
        Me.txtFSCHNo.TabIndex = 2
        Me.txtFSCHNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 12)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "BS OUT CHNo."
        '
        'txtBSCHNo
        '
        Me.txtBSCHNo.Location = New System.Drawing.Point(99, 16)
        Me.txtBSCHNo.Name = "txtBSCHNo"
        Me.txtBSCHNo.Size = New System.Drawing.Size(54, 19)
        Me.txtBSCHNo.TabIndex = 0
        Me.txtBSCHNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'fra_FCU_Set
        '
        Me.fra_FCU_Set.Controls.Add(Me.chkPtJPt)
        Me.fra_FCU_Set.Controls.Add(Me.GroupBox1)
        Me.fra_FCU_Set.Controls.Add(Me.chkFCU)
        Me.fra_FCU_Set.Controls.Add(Me.CmbFCUCnt)
        Me.fra_FCU_Set.Controls.Add(Me.Label5)
        Me.fra_FCU_Set.Controls.Add(Me.chkShareChUse)
        Me.fra_FCU_Set.Controls.Add(Me.numCorrectTime)
        Me.fra_FCU_Set.Controls.Add(Me.chkSIO)
        Me.fra_FCU_Set.Controls.Add(Me.Label6)
        Me.fra_FCU_Set.Controls.Add(Me.Label7)
        Me.fra_FCU_Set.Controls.Add(Me.cmbFCUNo)
        Me.fra_FCU_Set.Controls.Add(Me.chkEventLogBackup)
        Me.fra_FCU_Set.Controls.Add(Me.Label8)
        Me.fra_FCU_Set.Location = New System.Drawing.Point(264, 88)
        Me.fra_FCU_Set.Name = "fra_FCU_Set"
        Me.fra_FCU_Set.Size = New System.Drawing.Size(294, 214)
        Me.fra_FCU_Set.TabIndex = 32
        Me.fra_FCU_Set.TabStop = False
        Me.fra_FCU_Set.Text = "FCU Set"
        '
        'chkPtJPt
        '
        Me.chkPtJPt.AutoSize = True
        Me.chkPtJPt.BackColor = System.Drawing.SystemColors.Control
        Me.chkPtJPt.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPtJPt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPtJPt.Location = New System.Drawing.Point(18, 153)
        Me.chkPtJPt.Name = "chkPtJPt"
        Me.chkPtJPt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPtJPt.Size = New System.Drawing.Size(126, 16)
        Me.chkPtJPt.TabIndex = 31
        Me.chkPtJPt.Text = "PT or JPT(ON=JPT)"
        Me.chkPtJPt.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkDataSW)
        Me.GroupBox1.Controls.Add(Me.cmbPart)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(166, 18)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(113, 70)
        Me.GroupBox1.TabIndex = 30
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Part Setting"
        '
        'chkDataSW
        '
        Me.chkDataSW.AutoSize = True
        Me.chkDataSW.BackColor = System.Drawing.SystemColors.Control
        Me.chkDataSW.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDataSW.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDataSW.Location = New System.Drawing.Point(6, 47)
        Me.chkDataSW.Name = "chkDataSW"
        Me.chkDataSW.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDataSW.Size = New System.Drawing.Size(90, 16)
        Me.chkDataSW.TabIndex = 32
        Me.chkDataSW.Text = "DataSet 1SW"
        Me.chkDataSW.UseVisualStyleBackColor = True
        '
        'cmbPart
        '
        Me.cmbPart.FormattingEnabled = True
        Me.cmbPart.Location = New System.Drawing.Point(6, 18)
        Me.cmbPart.Name = "cmbPart"
        Me.cmbPart.Size = New System.Drawing.Size(101, 20)
        Me.cmbPart.TabIndex = 0
        '
        'chkFCU
        '
        Me.chkFCU.AutoSize = True
        Me.chkFCU.BackColor = System.Drawing.SystemColors.Control
        Me.chkFCU.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFCU.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFCU.Location = New System.Drawing.Point(162, 107)
        Me.chkFCU.Name = "chkFCU"
        Me.chkFCU.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFCU.Size = New System.Drawing.Size(108, 16)
        Me.chkFCU.TabIndex = 29
        Me.chkFCU.Text = "FCU Extend FCU"
        Me.chkFCU.UseVisualStyleBackColor = True
        '
        'CmbFCUCnt
        '
        Me.CmbFCUCnt.BackColor = System.Drawing.SystemColors.Window
        Me.CmbFCUCnt.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmbFCUCnt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFCUCnt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CmbFCUCnt.Location = New System.Drawing.Point(86, 26)
        Me.CmbFCUCnt.Name = "CmbFCUCnt"
        Me.CmbFCUCnt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmbFCUCnt.Size = New System.Drawing.Size(62, 20)
        Me.CmbFCUCnt.TabIndex = 28
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(19, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(59, 12)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "FCU Count"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkShareChUse
        '
        Me.chkShareChUse.AutoSize = True
        Me.chkShareChUse.BackColor = System.Drawing.SystemColors.Control
        Me.chkShareChUse.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShareChUse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShareChUse.Location = New System.Drawing.Point(18, 131)
        Me.chkShareChUse.Name = "chkShareChUse"
        Me.chkShareChUse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShareChUse.Size = New System.Drawing.Size(96, 16)
        Me.chkShareChUse.TabIndex = 26
        Me.chkShareChUse.Text = "Share CH Use"
        Me.chkShareChUse.UseVisualStyleBackColor = True
        '
        'numCorrectTime
        '
        Me.numCorrectTime.Location = New System.Drawing.Point(162, 183)
        Me.numCorrectTime.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.numCorrectTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numCorrectTime.Name = "numCorrectTime"
        Me.numCorrectTime.Size = New System.Drawing.Size(49, 19)
        Me.numCorrectTime.TabIndex = 25
        Me.numCorrectTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numCorrectTime.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'chkSIO
        '
        Me.chkSIO.AutoSize = True
        Me.chkSIO.BackColor = System.Drawing.SystemColors.Control
        Me.chkSIO.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSIO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSIO.Location = New System.Drawing.Point(19, 107)
        Me.chkSIO.Name = "chkSIO"
        Me.chkSIO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSIO.Size = New System.Drawing.Size(120, 16)
        Me.chkSIO.TabIndex = 24
        Me.chkSIO.Text = "FCU Extend Board"
        Me.chkSIO.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(218, 186)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(41, 12)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "x100ms"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(16, 186)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(119, 12)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "FCU-FU Collect Time"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbFCUNo
        '
        Me.cmbFCUNo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbFCUNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbFCUNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFCUNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbFCUNo.Location = New System.Drawing.Point(86, 52)
        Me.cmbFCUNo.Name = "cmbFCUNo"
        Me.cmbFCUNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbFCUNo.Size = New System.Drawing.Size(62, 20)
        Me.cmbFCUNo.TabIndex = 20
        '
        'chkEventLogBackup
        '
        Me.chkEventLogBackup.BackColor = System.Drawing.SystemColors.Control
        Me.chkEventLogBackup.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkEventLogBackup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEventLogBackup.Location = New System.Drawing.Point(231, 142)
        Me.chkEventLogBackup.Name = "chkEventLogBackup"
        Me.chkEventLogBackup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkEventLogBackup.Size = New System.Drawing.Size(31, 16)
        Me.chkEventLogBackup.TabIndex = 19
        Me.chkEventLogBackup.Text = "Event Log Backup(MemoryCard Exchange)"
        Me.chkEventLogBackup.UseVisualStyleBackColor = True
        Me.chkEventLogBackup.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(19, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(41, 12)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "FCU No"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtHoanGno
        '
        Me.txtHoanGno.Location = New System.Drawing.Point(65, 37)
        Me.txtHoanGno.Name = "txtHoanGno"
        Me.txtHoanGno.ReadOnly = True
        Me.txtHoanGno.Size = New System.Drawing.Size(54, 19)
        Me.txtHoanGno.TabIndex = 33
        Me.txtHoanGno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtHoanGno.Visible = False
        '
        'chkHOAN
        '
        Me.chkHOAN.AutoSize = True
        Me.chkHOAN.Location = New System.Drawing.Point(6, 40)
        Me.chkHOAN.Name = "chkHOAN"
        Me.chkHOAN.Size = New System.Drawing.Size(138, 16)
        Me.chkHOAN.TabIndex = 35
        Me.chkHOAN.Text = "Japanese Menu(Hoan)"
        Me.chkHOAN.UseVisualStyleBackColor = True
        '
        'chkNEWDES
        '
        Me.chkNEWDES.AutoSize = True
        Me.chkNEWDES.Location = New System.Drawing.Point(6, 18)
        Me.chkNEWDES.Name = "chkNEWDES"
        Me.chkNEWDES.Size = New System.Drawing.Size(108, 16)
        Me.chkNEWDES.TabIndex = 36
        Me.chkNEWDES.Text = "OPS New Design"
        Me.chkNEWDES.UseVisualStyleBackColor = True
        '
        'grpHoan
        '
        Me.grpHoan.Controls.Add(Me.optMimic)
        Me.grpHoan.Controls.Add(Me.optOverView)
        Me.grpHoan.Controls.Add(Me.txtHoanGno)
        Me.grpHoan.Location = New System.Drawing.Point(409, 308)
        Me.grpHoan.Name = "grpHoan"
        Me.grpHoan.Size = New System.Drawing.Size(149, 60)
        Me.grpHoan.TabIndex = 37
        Me.grpHoan.TabStop = False
        Me.grpHoan.Text = "Startup Screen"
        '
        'optMimic
        '
        Me.optMimic.AutoSize = True
        Me.optMimic.Location = New System.Drawing.Point(6, 38)
        Me.optMimic.Name = "optMimic"
        Me.optMimic.Size = New System.Drawing.Size(53, 16)
        Me.optMimic.TabIndex = 35
        Me.optMimic.Text = "MIMIC"
        Me.optMimic.UseVisualStyleBackColor = True
        '
        'optOverView
        '
        Me.optOverView.AutoSize = True
        Me.optOverView.Checked = True
        Me.optOverView.Location = New System.Drawing.Point(6, 17)
        Me.optOverView.Name = "optOverView"
        Me.optOverView.Size = New System.Drawing.Size(71, 16)
        Me.optOverView.TabIndex = 34
        Me.optOverView.TabStop = True
        Me.optOverView.Text = "OVERVIEW"
        Me.optOverView.UseVisualStyleBackColor = True
        '
        'grpMode
        '
        Me.grpMode.Controls.Add(Me.chkNEWDES)
        Me.grpMode.Controls.Add(Me.chkHOAN)
        Me.grpMode.Location = New System.Drawing.Point(240, 308)
        Me.grpMode.Name = "grpMode"
        Me.grpMode.Size = New System.Drawing.Size(155, 62)
        Me.grpMode.TabIndex = 38
        Me.grpMode.TabStop = False
        Me.grpMode.Text = "Mode"
        '
        'chkDataSW2
        '
        Me.chkDataSW2.AutoSize = True
        Me.chkDataSW2.BackColor = System.Drawing.SystemColors.Control
        Me.chkDataSW2.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDataSW2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDataSW2.Location = New System.Drawing.Point(10, 57)
        Me.chkDataSW2.Name = "chkDataSW2"
        Me.chkDataSW2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDataSW2.Size = New System.Drawing.Size(90, 16)
        Me.chkDataSW2.TabIndex = 34
        Me.chkDataSW2.Text = "DataSet 1SW"
        Me.chkDataSW2.UseVisualStyleBackColor = True
        '
        'frmSysSystem
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(574, 428)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpMode)
        Me.Controls.Add(Me.grpHoan)
        Me.Controls.Add(Me.fra_FCU_Set)
        Me.Controls.Add(Me.fraBS_FS)
        Me.Controls.Add(Me.chkHistoryAuto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbGL_SPEC)
        Me.Controls.Add(Me.cmbManual)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkCombine)
        Me.Controls.Add(Me.txtShipName)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.fraGWS)
        Me.Controls.Add(Me.fraSystemStatus)
        Me.Controls.Add(Me.cmbLanguage)
        Me.Controls.Add(Me.cmbDataFormat)
        Me.Controls.Add(Me.cmbSystemClock)
        Me.Controls.Add(Me.fraCombine)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSysSystem"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "OVERALL SETTING"
        Me.fraGWS.ResumeLayout(False)
        Me.fraGWS.PerformLayout()
        Me.fraCombine.ResumeLayout(False)
        Me.fraCombine.PerformLayout()
        Me.fraSystemStatus.ResumeLayout(False)
        Me.fraBS_FS.ResumeLayout(False)
        Me.fraBS_FS.PerformLayout()
        Me.fra_FCU_Set.ResumeLayout(False)
        Me.fra_FCU_Set.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numCorrectTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpHoan.ResumeLayout(False)
        Me.grpHoan.PerformLayout()
        Me.grpMode.ResumeLayout(False)
        Me.grpMode.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents chkCombine As System.Windows.Forms.CheckBox
    Public WithEvents chkSystemStatusNone As System.Windows.Forms.CheckBox
    Public WithEvents chkSystemStatusFCU As System.Windows.Forms.CheckBox
    Public WithEvents chkSystemStatusOPS As System.Windows.Forms.CheckBox
    Public WithEvents fraSystemStatus As System.Windows.Forms.GroupBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cmbManual As System.Windows.Forms.ComboBox
    Public WithEvents cmbGL_SPEC As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents chkHistoryAuto As System.Windows.Forms.CheckBox
    Friend WithEvents fraBS_FS As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFSCHNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBSCHNo As System.Windows.Forms.TextBox
    Friend WithEvents fra_FCU_Set As System.Windows.Forms.GroupBox
    Public WithEvents chkPtJPt As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPart As System.Windows.Forms.ComboBox
    Public WithEvents chkFCU As System.Windows.Forms.CheckBox
    Public WithEvents CmbFCUCnt As System.Windows.Forms.ComboBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents chkShareChUse As System.Windows.Forms.CheckBox
    Friend WithEvents numCorrectTime As System.Windows.Forms.NumericUpDown
    Public WithEvents chkSIO As System.Windows.Forms.CheckBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents cmbFCUNo As System.Windows.Forms.ComboBox
    Public WithEvents chkEventLogBackup As System.Windows.Forms.CheckBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtHoanGno As System.Windows.Forms.TextBox
    Friend WithEvents chkHOAN As System.Windows.Forms.CheckBox
    Friend WithEvents chkNEWDES As System.Windows.Forms.CheckBox
    Friend WithEvents grpHoan As System.Windows.Forms.GroupBox
    Friend WithEvents grpMode As System.Windows.Forms.GroupBox
    Friend WithEvents optMimic As System.Windows.Forms.RadioButton
    Friend WithEvents optOverView As System.Windows.Forms.RadioButton
    Public WithEvents chkDataSW As System.Windows.Forms.CheckBox
    Public WithEvents chkDataSW2 As System.Windows.Forms.CheckBox
#End Region

End Class
