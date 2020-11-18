<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChListValve
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

    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdBeforeCH = New System.Windows.Forms.Button()
        Me.cmdNextCH = New System.Windows.Forms.Button()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtAlmMimic = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.cmbAlmLvl = New System.Windows.Forms.ComboBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtTagNo = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbTime = New System.Windows.Forms.ComboBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtShareChid = New System.Windows.Forms.TextBox()
        Me.lblShareChid = New System.Windows.Forms.Label()
        Me.lblShareType = New System.Windows.Forms.Label()
        Me.cmbShareType = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblBitSet = New System.Windows.Forms.Label()
        Me.txtSP = New System.Windows.Forms.TextBox()
        Me.txtPLC = New System.Windows.Forms.TextBox()
        Me.txtEP = New System.Windows.Forms.TextBox()
        Me.txtAC = New System.Windows.Forms.TextBox()
        Me.txtRL = New System.Windows.Forms.TextBox()
        Me.txtWK = New System.Windows.Forms.TextBox()
        Me.txtGWS = New System.Windows.Forms.TextBox()
        Me.txtSio = New System.Windows.Forms.TextBox()
        Me.txtSC = New System.Windows.Forms.TextBox()
        Me.txtDmy = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.fraAlarm = New System.Windows.Forms.GroupBox()
        Me.txtDelayTimer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtExtGroup = New System.Windows.Forms.TextBox()
        Me.txtGRep1 = New System.Windows.Forms.TextBox()
        Me.txtGRep2 = New System.Windows.Forms.TextBox()
        Me.cmbSysNo = New System.Windows.Forms.ComboBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtChNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.fraComposite = New System.Windows.Forms.GroupBox()
        Me.cmdCompositeEdit = New System.Windows.Forms.Button()
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.txtCompositeIndex = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtPulseWidth = New System.Windows.Forms.TextBox()
        Me.lblPulseWidth = New System.Windows.Forms.Label()
        Me.lblControlType = New System.Windows.Forms.Label()
        Me.cmbControlType = New System.Windows.Forms.ComboBox()
        Me.txtStatusDo8 = New System.Windows.Forms.TextBox()
        Me.txtStatusDo5 = New System.Windows.Forms.TextBox()
        Me.txtStatusDo7 = New System.Windows.Forms.TextBox()
        Me.txtStatusDo4 = New System.Windows.Forms.TextBox()
        Me.txtStatusDo6 = New System.Windows.Forms.TextBox()
        Me.txtStatusDo3 = New System.Windows.Forms.TextBox()
        Me.txtStatusDo2 = New System.Windows.Forms.TextBox()
        Me.txtStatusDo1 = New System.Windows.Forms.TextBox()
        Me.txtPinAo = New System.Windows.Forms.TextBox()
        Me.txtPortNoAo = New System.Windows.Forms.TextBox()
        Me.txtFuNoAo = New System.Windows.Forms.TextBox()
        Me.txtPinAi = New System.Windows.Forms.TextBox()
        Me.txtPortNoAi = New System.Windows.Forms.TextBox()
        Me.txtFuNoAi = New System.Windows.Forms.TextBox()
        Me.txtPinDo = New System.Windows.Forms.TextBox()
        Me.txtPortNoDo = New System.Windows.Forms.TextBox()
        Me.txtFuNoDo = New System.Windows.Forms.TextBox()
        Me.txtPinDi = New System.Windows.Forms.TextBox()
        Me.txtPortNoDi = New System.Windows.Forms.TextBox()
        Me.txtFuNoDi = New System.Windows.Forms.TextBox()
        Me.lblDo8 = New System.Windows.Forms.Label()
        Me.lblDo5 = New System.Windows.Forms.Label()
        Me.lblDo7 = New System.Windows.Forms.Label()
        Me.lblDo4 = New System.Windows.Forms.Label()
        Me.lblDo6 = New System.Windows.Forms.Label()
        Me.lblDo3 = New System.Windows.Forms.Label()
        Me.lblDo2 = New System.Windows.Forms.Label()
        Me.lblDi1 = New System.Windows.Forms.Label()
        Me.lblDo1 = New System.Windows.Forms.Label()
        Me.lblDi2 = New System.Windows.Forms.Label()
        Me.lblDi8 = New System.Windows.Forms.Label()
        Me.lblDi5 = New System.Windows.Forms.Label()
        Me.lblDi6 = New System.Windows.Forms.Label()
        Me.lblDi7 = New System.Windows.Forms.Label()
        Me.lblDi3 = New System.Windows.Forms.Label()
        Me.lblDi4 = New System.Windows.Forms.Label()
        Me.chkStatusAlarm = New System.Windows.Forms.CheckBox()
        Me.lblDiStart = New System.Windows.Forms.Label()
        Me.txtAlarmTimeup = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblAoTerminal = New System.Windows.Forms.Label()
        Me.lblDoStart = New System.Windows.Forms.Label()
        Me.lblAiTerminal = New System.Windows.Forms.Label()
        Me.txtStatusOut = New System.Windows.Forms.TextBox()
        Me.cmbStatusOut = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtStatusIn = New System.Windows.Forms.TextBox()
        Me.cmbStatusIn = New System.Windows.Forms.ComboBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cmbExtDevice = New System.Windows.Forms.ComboBox()
        Me.txtBitCount = New System.Windows.Forms.TextBox()
        Me.lblBitCount = New System.Windows.Forms.Label()
        Me.cmbDataType = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.fraHiHi0 = New System.Windows.Forms.GroupBox()
        Me.fraFeAlarmInfo2 = New System.Windows.Forms.Panel()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtVar = New System.Windows.Forms.TextBox()
        Me.txtSt = New System.Windows.Forms.TextBox()
        Me.txtHys2 = New System.Windows.Forms.TextBox()
        Me.txtHys1 = New System.Windows.Forms.TextBox()
        Me.txtSp2 = New System.Windows.Forms.TextBox()
        Me.txtSp1 = New System.Windows.Forms.TextBox()
        Me.lblVar = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtStatusFa = New System.Windows.Forms.TextBox()
        Me.txtGRep2Fa = New System.Windows.Forms.TextBox()
        Me.txtGRep1Fa = New System.Windows.Forms.TextBox()
        Me.txtExtGFa = New System.Windows.Forms.TextBox()
        Me.txtDelayFa = New System.Windows.Forms.TextBox()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.fraAiInfo = New System.Windows.Forms.GroupBox()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.lblNormalHif = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtRangeTo = New System.Windows.Forms.TextBox()
        Me.txtRangeFrom = New System.Windows.Forms.TextBox()
        Me.lblRange = New System.Windows.Forms.Label()
        Me.txtOffset = New System.Windows.Forms.TextBox()
        Me.lblNormal = New System.Windows.Forms.Label()
        Me.lblString = New System.Windows.Forms.Label()
        Me.txtLowNormal = New System.Windows.Forms.TextBox()
        Me.chkCenterGraph = New System.Windows.Forms.CheckBox()
        Me.txtHighNormal = New System.Windows.Forms.TextBox()
        Me.lblOffset = New System.Windows.Forms.Label()
        Me.txtString = New System.Windows.Forms.TextBox()
        Me.fraInputAlrm = New System.Windows.Forms.GroupBox()
        Me.txtStatusSF = New System.Windows.Forms.TextBox()
        Me.txtStatusLoLo = New System.Windows.Forms.TextBox()
        Me.txtStatusLo = New System.Windows.Forms.TextBox()
        Me.txtStatusHi = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtStatusHiHi = New System.Windows.Forms.TextBox()
        Me.txtValueHiHi = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbValueSensorFailure = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtExtGSensorFailure = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtGRep2SensorFailure = New System.Windows.Forms.TextBox()
        Me.txtGRep2LoLo = New System.Windows.Forms.TextBox()
        Me.txtGRep2Lo = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.txtGRep1SensorFailure = New System.Windows.Forms.TextBox()
        Me.txtGRep1LoLo = New System.Windows.Forms.TextBox()
        Me.txtExtGLoLo = New System.Windows.Forms.TextBox()
        Me.txtGRep2Hi = New System.Windows.Forms.TextBox()
        Me.txtDelaySensorFailure = New System.Windows.Forms.TextBox()
        Me.txtDelayLoLo = New System.Windows.Forms.TextBox()
        Me.txtGRep1Lo = New System.Windows.Forms.TextBox()
        Me.txtDelayLo = New System.Windows.Forms.TextBox()
        Me.txtValueLoLo = New System.Windows.Forms.TextBox()
        Me.txtGRep1Hi = New System.Windows.Forms.TextBox()
        Me.txtExtGLo = New System.Windows.Forms.TextBox()
        Me.txtValueLo = New System.Windows.Forms.TextBox()
        Me.txtExtGHi = New System.Windows.Forms.TextBox()
        Me.txtDelayHiHi = New System.Windows.Forms.TextBox()
        Me.txtDelayHi = New System.Windows.Forms.TextBox()
        Me.txtGRep2HiHi = New System.Windows.Forms.TextBox()
        Me.txtExtGHiHi = New System.Windows.Forms.TextBox()
        Me.txtValueHi = New System.Windows.Forms.TextBox()
        Me.txtGRep1HiHi = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.lblDummy = New System.Windows.Forms.Label()
        Me.TabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.fraAlarm.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.fraComposite.SuspendLayout()
        Me.fraHiHi0.SuspendLayout()
        Me.fraFeAlarmInfo2.SuspendLayout()
        Me.fraAiInfo.SuspendLayout()
        Me.fraInputAlrm.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(670, 636)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 4
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(548, 636)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 3
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdBeforeCH
        '
        Me.cmdBeforeCH.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBeforeCH.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBeforeCH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBeforeCH.Location = New System.Drawing.Point(16, 636)
        Me.cmdBeforeCH.Name = "cmdBeforeCH"
        Me.cmdBeforeCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBeforeCH.Size = New System.Drawing.Size(113, 33)
        Me.cmdBeforeCH.TabIndex = 1
        Me.cmdBeforeCH.Text = "Before CH"
        Me.cmdBeforeCH.UseVisualStyleBackColor = True
        '
        'cmdNextCH
        '
        Me.cmdNextCH.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNextCH.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNextCH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNextCH.Location = New System.Drawing.Point(140, 636)
        Me.cmdNextCH.Name = "cmdNextCH"
        Me.cmdNextCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNextCH.Size = New System.Drawing.Size(113, 33)
        Me.cmdNextCH.TabIndex = 2
        Me.cmdNextCH.Text = "Next CH"
        Me.cmdNextCH.UseVisualStyleBackColor = True
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabPage1)
        Me.TabControl.Controls.Add(Me.TabPage2)
        Me.TabControl.Location = New System.Drawing.Point(8, 8)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(780, 620)
        Me.TabControl.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.Transparent
        Me.TabPage1.Controls.Add(Me.txtAlmMimic)
        Me.TabPage1.Controls.Add(Me.Label45)
        Me.TabPage1.Controls.Add(Me.Label44)
        Me.TabPage1.Controls.Add(Me.cmbAlmLvl)
        Me.TabPage1.Controls.Add(Me.Label42)
        Me.TabPage1.Controls.Add(Me.txtTagNo)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.cmbTime)
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.fraAlarm)
        Me.TabPage1.Controls.Add(Me.cmbSysNo)
        Me.TabPage1.Controls.Add(Me.txtRemarks)
        Me.TabPage1.Controls.Add(Me.Label36)
        Me.TabPage1.Controls.Add(Me.txtItemName)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.txtChNo)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Label37)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(772, 594)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Common"
        '
        'txtAlmMimic
        '
        Me.txtAlmMimic.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtAlmMimic.Location = New System.Drawing.Point(265, 322)
        Me.txtAlmMimic.MaxLength = 0
        Me.txtAlmMimic.Name = "txtAlmMimic"
        Me.txtAlmMimic.Size = New System.Drawing.Size(48, 19)
        Me.txtAlmMimic.TabIndex = 139
        Me.txtAlmMimic.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtAlmMimic.Visible = False
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(263, 305)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(101, 12)
        Me.Label45.TabIndex = 140
        Me.Label45.Text = "Fire Alarm Mimic"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label45.Visible = False
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(16, 140)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(71, 12)
        Me.Label44.TabIndex = 138
        Me.Label44.Text = "Alarm Level"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbAlmLvl
        '
        Me.cmbAlmLvl.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAlmLvl.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbAlmLvl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlmLvl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbAlmLvl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAlmLvl.Location = New System.Drawing.Point(95, 137)
        Me.cmbAlmLvl.Name = "cmbAlmLvl"
        Me.cmbAlmLvl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAlmLvl.Size = New System.Drawing.Size(121, 20)
        Me.cmbAlmLvl.TabIndex = 137
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(202, 53)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(47, 12)
        Me.Label42.TabIndex = 136
        Me.Label42.Text = "Tag No."
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTagNo
        '
        Me.txtTagNo.AcceptsReturn = True
        Me.txtTagNo.Location = New System.Drawing.Point(253, 50)
        Me.txtTagNo.MaxLength = 16
        Me.txtTagNo.Name = "txtTagNo"
        Me.txtTagNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtTagNo.Size = New System.Drawing.Size(107, 19)
        Me.txtTagNo.TabIndex = 135
        Me.txtTagNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(241, 140)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(119, 12)
        Me.Label20.TabIndex = 134
        Me.Label20.Text = "Unit of Delay Timer"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbTime
        '
        Me.cmbTime.BackColor = System.Drawing.SystemColors.Window
        Me.cmbTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbTime.Location = New System.Drawing.Point(366, 137)
        Me.cmbTime.Name = "cmbTime"
        Me.cmbTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbTime.Size = New System.Drawing.Size(64, 20)
        Me.cmbTime.TabIndex = 5
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtShareChid)
        Me.GroupBox6.Controls.Add(Me.lblShareChid)
        Me.GroupBox6.Controls.Add(Me.lblShareType)
        Me.GroupBox6.Controls.Add(Me.cmbShareType)
        Me.GroupBox6.Location = New System.Drawing.Point(23, 277)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(228, 80)
        Me.GroupBox6.TabIndex = 7
        Me.GroupBox6.TabStop = False
        '
        'txtShareChid
        '
        Me.txtShareChid.AcceptsReturn = True
        Me.txtShareChid.Location = New System.Drawing.Point(108, 48)
        Me.txtShareChid.MaxLength = 0
        Me.txtShareChid.Name = "txtShareChid"
        Me.txtShareChid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtShareChid.Size = New System.Drawing.Size(48, 19)
        Me.txtShareChid.TabIndex = 2
        Me.txtShareChid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblShareChid
        '
        Me.lblShareChid.AutoSize = True
        Me.lblShareChid.BackColor = System.Drawing.SystemColors.Control
        Me.lblShareChid.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShareChid.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShareChid.Location = New System.Drawing.Point(12, 52)
        Me.lblShareChid.Name = "lblShareChid"
        Me.lblShareChid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShareChid.Size = New System.Drawing.Size(83, 12)
        Me.lblShareChid.TabIndex = 106
        Me.lblShareChid.Text = "Remote CH No."
        Me.lblShareChid.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblShareType
        '
        Me.lblShareType.AutoSize = True
        Me.lblShareType.BackColor = System.Drawing.SystemColors.Control
        Me.lblShareType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShareType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShareType.Location = New System.Drawing.Point(32, 24)
        Me.lblShareType.Name = "lblShareType"
        Me.lblShareType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShareType.Size = New System.Drawing.Size(65, 12)
        Me.lblShareType.TabIndex = 105
        Me.lblShareType.Text = "Share Type"
        Me.lblShareType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbShareType
        '
        Me.cmbShareType.BackColor = System.Drawing.SystemColors.Window
        Me.cmbShareType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbShareType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbShareType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbShareType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbShareType.Location = New System.Drawing.Point(108, 20)
        Me.cmbShareType.Name = "cmbShareType"
        Me.cmbShareType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbShareType.Size = New System.Drawing.Size(88, 20)
        Me.cmbShareType.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.lblBitSet)
        Me.GroupBox3.Controls.Add(Me.txtSP)
        Me.GroupBox3.Controls.Add(Me.txtPLC)
        Me.GroupBox3.Controls.Add(Me.txtEP)
        Me.GroupBox3.Controls.Add(Me.txtAC)
        Me.GroupBox3.Controls.Add(Me.txtRL)
        Me.GroupBox3.Controls.Add(Me.txtWK)
        Me.GroupBox3.Controls.Add(Me.txtGWS)
        Me.GroupBox3.Controls.Add(Me.txtSio)
        Me.GroupBox3.Controls.Add(Me.txtSC)
        Me.GroupBox3.Controls.Add(Me.txtDmy)
        Me.GroupBox3.Controls.Add(Me.Label25)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox3.Location = New System.Drawing.Point(23, 178)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox3.Size = New System.Drawing.Size(396, 82)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Flag"
        '
        'lblBitSet
        '
        Me.lblBitSet.AutoSize = True
        Me.lblBitSet.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitSet.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBitSet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitSet.Location = New System.Drawing.Point(82, 66)
        Me.lblBitSet.Name = "lblBitSet"
        Me.lblBitSet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBitSet.Size = New System.Drawing.Size(101, 12)
        Me.lblBitSet.TabIndex = 83
        Me.lblBitSet.Text = "Enter the bitset"
        Me.lblBitSet.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblBitSet.Visible = False
        '
        'txtSP
        '
        Me.txtSP.AcceptsReturn = True
        Me.txtSP.Location = New System.Drawing.Point(348, 44)
        Me.txtSP.MaxLength = 0
        Me.txtSP.Name = "txtSP"
        Me.txtSP.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSP.Size = New System.Drawing.Size(28, 19)
        Me.txtSP.TabIndex = 9
        Me.txtSP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPLC
        '
        Me.txtPLC.AcceptsReturn = True
        Me.txtPLC.Location = New System.Drawing.Point(312, 44)
        Me.txtPLC.MaxLength = 0
        Me.txtPLC.Name = "txtPLC"
        Me.txtPLC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPLC.Size = New System.Drawing.Size(28, 19)
        Me.txtPLC.TabIndex = 8
        Me.txtPLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtEP
        '
        Me.txtEP.AcceptsReturn = True
        Me.txtEP.Location = New System.Drawing.Point(276, 44)
        Me.txtEP.MaxLength = 0
        Me.txtEP.Name = "txtEP"
        Me.txtEP.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtEP.Size = New System.Drawing.Size(28, 19)
        Me.txtEP.TabIndex = 7
        Me.txtEP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAC
        '
        Me.txtAC.AcceptsReturn = True
        Me.txtAC.Location = New System.Drawing.Point(240, 44)
        Me.txtAC.MaxLength = 0
        Me.txtAC.Name = "txtAC"
        Me.txtAC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtAC.Size = New System.Drawing.Size(28, 19)
        Me.txtAC.TabIndex = 6
        Me.txtAC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRL
        '
        Me.txtRL.AcceptsReturn = True
        Me.txtRL.Location = New System.Drawing.Point(204, 44)
        Me.txtRL.MaxLength = 0
        Me.txtRL.Name = "txtRL"
        Me.txtRL.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtRL.Size = New System.Drawing.Size(28, 19)
        Me.txtRL.TabIndex = 5
        Me.txtRL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWK
        '
        Me.txtWK.AcceptsReturn = True
        Me.txtWK.Location = New System.Drawing.Point(168, 44)
        Me.txtWK.MaxLength = 0
        Me.txtWK.Name = "txtWK"
        Me.txtWK.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtWK.Size = New System.Drawing.Size(28, 19)
        Me.txtWK.TabIndex = 4
        Me.txtWK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtGWS
        '
        Me.txtGWS.AcceptsReturn = True
        Me.txtGWS.Location = New System.Drawing.Point(132, 44)
        Me.txtGWS.MaxLength = 0
        Me.txtGWS.Name = "txtGWS"
        Me.txtGWS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtGWS.Size = New System.Drawing.Size(28, 19)
        Me.txtGWS.TabIndex = 3
        Me.txtGWS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSio
        '
        Me.txtSio.AcceptsReturn = True
        Me.txtSio.Location = New System.Drawing.Point(96, 44)
        Me.txtSio.MaxLength = 0
        Me.txtSio.Name = "txtSio"
        Me.txtSio.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSio.Size = New System.Drawing.Size(28, 19)
        Me.txtSio.TabIndex = 2
        Me.txtSio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSC
        '
        Me.txtSC.AcceptsReturn = True
        Me.txtSC.Location = New System.Drawing.Point(60, 44)
        Me.txtSC.MaxLength = 0
        Me.txtSC.Name = "txtSC"
        Me.txtSC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSC.Size = New System.Drawing.Size(28, 19)
        Me.txtSC.TabIndex = 1
        Me.txtSC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDmy
        '
        Me.txtDmy.AcceptsReturn = True
        Me.txtDmy.Location = New System.Drawing.Point(24, 44)
        Me.txtDmy.MaxLength = 0
        Me.txtDmy.Name = "txtDmy"
        Me.txtDmy.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtDmy.Size = New System.Drawing.Size(28, 19)
        Me.txtDmy.TabIndex = 0
        Me.txtDmy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(352, 24)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(29, 12)
        Me.Label25.TabIndex = 81
        Me.Label25.Text = "LOCK"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(312, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(23, 12)
        Me.Label4.TabIndex = 80
        Me.Label4.Text = "PLC"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(276, 24)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(17, 12)
        Me.Label23.TabIndex = 79
        Me.Label23.Text = "EP"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(240, 24)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(17, 12)
        Me.Label24.TabIndex = 78
        Me.Label24.Text = "AC"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(204, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(17, 12)
        Me.Label9.TabIndex = 77
        Me.Label9.Text = "RL"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(168, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(17, 12)
        Me.Label10.TabIndex = 76
        Me.Label10.Text = "WK"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(132, 24)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(23, 12)
        Me.Label14.TabIndex = 75
        Me.Label14.Text = "GWS"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(96, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(23, 12)
        Me.Label15.TabIndex = 74
        Me.Label15.Text = "SIO"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(60, 24)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(17, 12)
        Me.Label16.TabIndex = 73
        Me.Label16.Text = "SC"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(24, 24)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(23, 12)
        Me.Label26.TabIndex = 72
        Me.Label26.Text = "Dmy"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraAlarm
        '
        Me.fraAlarm.BackColor = System.Drawing.SystemColors.Control
        Me.fraAlarm.Controls.Add(Me.txtDelayTimer)
        Me.fraAlarm.Controls.Add(Me.Label1)
        Me.fraAlarm.Controls.Add(Me.Label2)
        Me.fraAlarm.Controls.Add(Me.Label17)
        Me.fraAlarm.Controls.Add(Me.Label18)
        Me.fraAlarm.Controls.Add(Me.txtExtGroup)
        Me.fraAlarm.Controls.Add(Me.txtGRep1)
        Me.fraAlarm.Controls.Add(Me.txtGRep2)
        Me.fraAlarm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAlarm.Location = New System.Drawing.Point(24, 380)
        Me.fraAlarm.Name = "fraAlarm"
        Me.fraAlarm.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAlarm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAlarm.Size = New System.Drawing.Size(256, 76)
        Me.fraAlarm.TabIndex = 4
        Me.fraAlarm.TabStop = False
        Me.fraAlarm.Text = "Alarm"
        '
        'txtDelayTimer
        '
        Me.txtDelayTimer.AcceptsReturn = True
        Me.txtDelayTimer.Location = New System.Drawing.Point(76, 45)
        Me.txtDelayTimer.MaxLength = 0
        Me.txtDelayTimer.Name = "txtDelayTimer"
        Me.txtDelayTimer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayTimer.Size = New System.Drawing.Size(48, 19)
        Me.txtDelayTimer.TabIndex = 1
        Me.txtDelayTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(76, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(35, 12)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "Delay"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(24, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(35, 12)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "EXT.G"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(132, 27)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(41, 12)
        Me.Label17.TabIndex = 57
        Me.Label17.Text = "G REP1"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(184, 27)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(41, 12)
        Me.Label18.TabIndex = 56
        Me.Label18.Text = "G REP2"
        '
        'txtExtGroup
        '
        Me.txtExtGroup.AcceptsReturn = True
        Me.txtExtGroup.Location = New System.Drawing.Point(22, 45)
        Me.txtExtGroup.MaxLength = 0
        Me.txtExtGroup.Name = "txtExtGroup"
        Me.txtExtGroup.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGroup.TabIndex = 0
        Me.txtExtGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1
        '
        Me.txtGRep1.AcceptsReturn = True
        Me.txtGRep1.Location = New System.Drawing.Point(130, 45)
        Me.txtGRep1.MaxLength = 0
        Me.txtGRep1.Name = "txtGRep1"
        Me.txtGRep1.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep1.TabIndex = 2
        Me.txtGRep1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep2
        '
        Me.txtGRep2.AcceptsReturn = True
        Me.txtGRep2.Location = New System.Drawing.Point(184, 45)
        Me.txtGRep2.MaxLength = 0
        Me.txtGRep2.Name = "txtGRep2"
        Me.txtGRep2.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep2.TabIndex = 3
        Me.txtGRep2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbSysNo
        '
        Me.cmbSysNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSysNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbSysNo.FormattingEnabled = True
        Me.cmbSysNo.Location = New System.Drawing.Point(95, 21)
        Me.cmbSysNo.Name = "cmbSysNo"
        Me.cmbSysNo.Size = New System.Drawing.Size(110, 20)
        Me.cmbSysNo.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.Location = New System.Drawing.Point(95, 104)
        Me.txtRemarks.MaxLength = 16
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(265, 19)
        Me.txtRemarks.TabIndex = 3
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(31, 107)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label36.Size = New System.Drawing.Size(47, 12)
        Me.Label36.TabIndex = 130
        Me.Label36.Text = "Remarks"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.Location = New System.Drawing.Point(95, 77)
        Me.txtItemName.MaxLength = 30
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(265, 19)
        Me.txtItemName.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(21, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 129
        Me.Label3.Text = "Item Name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtChNo
        '
        Me.txtChNo.AcceptsReturn = True
        Me.txtChNo.Location = New System.Drawing.Point(95, 50)
        Me.txtChNo.MaxLength = 0
        Me.txtChNo.Name = "txtChNo"
        Me.txtChNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtChNo.Size = New System.Drawing.Size(48, 19)
        Me.txtChNo.TabIndex = 1
        Me.txtChNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(42, 53)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 128
        Me.Label7.Text = "CH No."
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(40, 24)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(47, 12)
        Me.Label37.TabIndex = 127
        Me.Label37.Text = "Sys No."
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.Transparent
        Me.TabPage2.Controls.Add(Me.fraComposite)
        Me.TabPage2.Controls.Add(Me.txtPulseWidth)
        Me.TabPage2.Controls.Add(Me.lblPulseWidth)
        Me.TabPage2.Controls.Add(Me.lblControlType)
        Me.TabPage2.Controls.Add(Me.cmbControlType)
        Me.TabPage2.Controls.Add(Me.txtStatusDo8)
        Me.TabPage2.Controls.Add(Me.txtStatusDo5)
        Me.TabPage2.Controls.Add(Me.txtStatusDo7)
        Me.TabPage2.Controls.Add(Me.txtStatusDo4)
        Me.TabPage2.Controls.Add(Me.txtStatusDo6)
        Me.TabPage2.Controls.Add(Me.txtStatusDo3)
        Me.TabPage2.Controls.Add(Me.txtStatusDo2)
        Me.TabPage2.Controls.Add(Me.txtStatusDo1)
        Me.TabPage2.Controls.Add(Me.txtPinAo)
        Me.TabPage2.Controls.Add(Me.txtPortNoAo)
        Me.TabPage2.Controls.Add(Me.txtFuNoAo)
        Me.TabPage2.Controls.Add(Me.txtPinAi)
        Me.TabPage2.Controls.Add(Me.txtPortNoAi)
        Me.TabPage2.Controls.Add(Me.txtFuNoAi)
        Me.TabPage2.Controls.Add(Me.txtPinDo)
        Me.TabPage2.Controls.Add(Me.txtPortNoDo)
        Me.TabPage2.Controls.Add(Me.txtFuNoDo)
        Me.TabPage2.Controls.Add(Me.txtPinDi)
        Me.TabPage2.Controls.Add(Me.txtPortNoDi)
        Me.TabPage2.Controls.Add(Me.txtFuNoDi)
        Me.TabPage2.Controls.Add(Me.lblDo8)
        Me.TabPage2.Controls.Add(Me.lblDo5)
        Me.TabPage2.Controls.Add(Me.lblDo7)
        Me.TabPage2.Controls.Add(Me.lblDo4)
        Me.TabPage2.Controls.Add(Me.lblDo6)
        Me.TabPage2.Controls.Add(Me.lblDo3)
        Me.TabPage2.Controls.Add(Me.lblDo2)
        Me.TabPage2.Controls.Add(Me.lblDi1)
        Me.TabPage2.Controls.Add(Me.lblDo1)
        Me.TabPage2.Controls.Add(Me.lblDi2)
        Me.TabPage2.Controls.Add(Me.lblDi8)
        Me.TabPage2.Controls.Add(Me.lblDi5)
        Me.TabPage2.Controls.Add(Me.lblDi6)
        Me.TabPage2.Controls.Add(Me.lblDi7)
        Me.TabPage2.Controls.Add(Me.lblDi3)
        Me.TabPage2.Controls.Add(Me.lblDi4)
        Me.TabPage2.Controls.Add(Me.chkStatusAlarm)
        Me.TabPage2.Controls.Add(Me.lblDiStart)
        Me.TabPage2.Controls.Add(Me.txtAlarmTimeup)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.lblAoTerminal)
        Me.TabPage2.Controls.Add(Me.lblDoStart)
        Me.TabPage2.Controls.Add(Me.lblAiTerminal)
        Me.TabPage2.Controls.Add(Me.txtStatusOut)
        Me.TabPage2.Controls.Add(Me.cmbStatusOut)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.txtStatusIn)
        Me.TabPage2.Controls.Add(Me.cmbStatusIn)
        Me.TabPage2.Controls.Add(Me.Label27)
        Me.TabPage2.Controls.Add(Me.cmbExtDevice)
        Me.TabPage2.Controls.Add(Me.txtBitCount)
        Me.TabPage2.Controls.Add(Me.lblBitCount)
        Me.TabPage2.Controls.Add(Me.cmbDataType)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.fraHiHi0)
        Me.TabPage2.Controls.Add(Me.fraAiInfo)
        Me.TabPage2.Controls.Add(Me.fraInputAlrm)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(772, 594)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Valve"
        '
        'fraComposite
        '
        Me.fraComposite.BackColor = System.Drawing.SystemColors.Control
        Me.fraComposite.Controls.Add(Me.cmdCompositeEdit)
        Me.fraComposite.Controls.Add(Me.cmdSelect)
        Me.fraComposite.Controls.Add(Me.txtCompositeIndex)
        Me.fraComposite.Controls.Add(Me.Label19)
        Me.fraComposite.Location = New System.Drawing.Point(368, 412)
        Me.fraComposite.Name = "fraComposite"
        Me.fraComposite.Size = New System.Drawing.Size(244, 72)
        Me.fraComposite.TabIndex = 32
        Me.fraComposite.TabStop = False
        Me.fraComposite.Text = "Composite"
        '
        'cmdCompositeEdit
        '
        Me.cmdCompositeEdit.Location = New System.Drawing.Point(185, 32)
        Me.cmdCompositeEdit.Name = "cmdCompositeEdit"
        Me.cmdCompositeEdit.Size = New System.Drawing.Size(57, 24)
        Me.cmdCompositeEdit.TabIndex = 181
        Me.cmdCompositeEdit.Text = "Edit"
        Me.cmdCompositeEdit.UseVisualStyleBackColor = True
        '
        'cmdSelect
        '
        Me.cmdSelect.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelect.Location = New System.Drawing.Point(229, 32)
        Me.cmdSelect.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSelect.Size = New System.Drawing.Size(52, 24)
        Me.cmdSelect.TabIndex = 1
        Me.cmdSelect.Text = "SELECT"
        Me.cmdSelect.UseVisualStyleBackColor = True
        Me.cmdSelect.Visible = False
        '
        'txtCompositeIndex
        '
        Me.txtCompositeIndex.AcceptsReturn = True
        Me.txtCompositeIndex.Enabled = False
        Me.txtCompositeIndex.Location = New System.Drawing.Point(131, 33)
        Me.txtCompositeIndex.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCompositeIndex.MaxLength = 0
        Me.txtCompositeIndex.Name = "txtCompositeIndex"
        Me.txtCompositeIndex.ReadOnly = True
        Me.txtCompositeIndex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompositeIndex.Size = New System.Drawing.Size(48, 19)
        Me.txtCompositeIndex.TabIndex = 180
        Me.txtCompositeIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(12, 36)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label19.Size = New System.Drawing.Size(113, 12)
        Me.Label19.TabIndex = 179
        Me.Label19.Text = "Composite Table No"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPulseWidth
        '
        Me.txtPulseWidth.AcceptsReturn = True
        Me.txtPulseWidth.Location = New System.Drawing.Point(155, 380)
        Me.txtPulseWidth.MaxLength = 0
        Me.txtPulseWidth.Name = "txtPulseWidth"
        Me.txtPulseWidth.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPulseWidth.Size = New System.Drawing.Size(44, 19)
        Me.txtPulseWidth.TabIndex = 29
        '
        'lblPulseWidth
        '
        Me.lblPulseWidth.AutoSize = True
        Me.lblPulseWidth.BackColor = System.Drawing.SystemColors.Control
        Me.lblPulseWidth.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPulseWidth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPulseWidth.Location = New System.Drawing.Point(34, 384)
        Me.lblPulseWidth.Name = "lblPulseWidth"
        Me.lblPulseWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPulseWidth.Size = New System.Drawing.Size(113, 12)
        Me.lblPulseWidth.TabIndex = 233
        Me.lblPulseWidth.Text = "Output pulse width"
        Me.lblPulseWidth.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblControlType
        '
        Me.lblControlType.AutoSize = True
        Me.lblControlType.BackColor = System.Drawing.SystemColors.Control
        Me.lblControlType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblControlType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblControlType.Location = New System.Drawing.Point(30, 352)
        Me.lblControlType.Name = "lblControlType"
        Me.lblControlType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblControlType.Size = New System.Drawing.Size(119, 12)
        Me.lblControlType.TabIndex = 231
        Me.lblControlType.Text = "Output control type"
        Me.lblControlType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbControlType
        '
        Me.cmbControlType.BackColor = System.Drawing.SystemColors.Window
        Me.cmbControlType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbControlType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbControlType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbControlType.Location = New System.Drawing.Point(155, 348)
        Me.cmbControlType.Name = "cmbControlType"
        Me.cmbControlType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbControlType.Size = New System.Drawing.Size(124, 20)
        Me.cmbControlType.TabIndex = 28
        '
        'txtStatusDo8
        '
        Me.txtStatusDo8.AcceptsReturn = True
        Me.txtStatusDo8.Location = New System.Drawing.Point(647, 195)
        Me.txtStatusDo8.MaxLength = 0
        Me.txtStatusDo8.Name = "txtStatusDo8"
        Me.txtStatusDo8.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo8.TabIndex = 17
        Me.txtStatusDo8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStatusDo5
        '
        Me.txtStatusDo5.AcceptsReturn = True
        Me.txtStatusDo5.Location = New System.Drawing.Point(383, 195)
        Me.txtStatusDo5.MaxLength = 0
        Me.txtStatusDo5.Name = "txtStatusDo5"
        Me.txtStatusDo5.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo5.TabIndex = 14
        Me.txtStatusDo5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStatusDo7
        '
        Me.txtStatusDo7.AcceptsReturn = True
        Me.txtStatusDo7.Location = New System.Drawing.Point(559, 195)
        Me.txtStatusDo7.MaxLength = 0
        Me.txtStatusDo7.Name = "txtStatusDo7"
        Me.txtStatusDo7.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo7.TabIndex = 16
        Me.txtStatusDo7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStatusDo4
        '
        Me.txtStatusDo4.AcceptsReturn = True
        Me.txtStatusDo4.Location = New System.Drawing.Point(295, 195)
        Me.txtStatusDo4.MaxLength = 0
        Me.txtStatusDo4.Name = "txtStatusDo4"
        Me.txtStatusDo4.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo4.TabIndex = 13
        Me.txtStatusDo4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStatusDo6
        '
        Me.txtStatusDo6.AcceptsReturn = True
        Me.txtStatusDo6.Location = New System.Drawing.Point(471, 195)
        Me.txtStatusDo6.MaxLength = 0
        Me.txtStatusDo6.Name = "txtStatusDo6"
        Me.txtStatusDo6.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo6.TabIndex = 15
        Me.txtStatusDo6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStatusDo3
        '
        Me.txtStatusDo3.AcceptsReturn = True
        Me.txtStatusDo3.Location = New System.Drawing.Point(207, 195)
        Me.txtStatusDo3.MaxLength = 0
        Me.txtStatusDo3.Name = "txtStatusDo3"
        Me.txtStatusDo3.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo3.TabIndex = 12
        Me.txtStatusDo3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStatusDo2
        '
        Me.txtStatusDo2.AcceptsReturn = True
        Me.txtStatusDo2.Location = New System.Drawing.Point(119, 195)
        Me.txtStatusDo2.MaxLength = 0
        Me.txtStatusDo2.Name = "txtStatusDo2"
        Me.txtStatusDo2.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo2.TabIndex = 11
        Me.txtStatusDo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStatusDo1
        '
        Me.txtStatusDo1.AcceptsReturn = True
        Me.txtStatusDo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStatusDo1.Location = New System.Drawing.Point(30, 195)
        Me.txtStatusDo1.MaxLength = 0
        Me.txtStatusDo1.Name = "txtStatusDo1"
        Me.txtStatusDo1.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo1.TabIndex = 10
        Me.txtStatusDo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPinAo
        '
        Me.txtPinAo.AcceptsReturn = True
        Me.txtPinAo.Location = New System.Drawing.Point(239, 282)
        Me.txtPinAo.MaxLength = 0
        Me.txtPinAo.Name = "txtPinAo"
        Me.txtPinAo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinAo.Size = New System.Drawing.Size(40, 19)
        Me.txtPinAo.TabIndex = 26
        '
        'txtPortNoAo
        '
        Me.txtPortNoAo.AcceptsReturn = True
        Me.txtPortNoAo.Location = New System.Drawing.Point(197, 282)
        Me.txtPortNoAo.MaxLength = 0
        Me.txtPortNoAo.Name = "txtPortNoAo"
        Me.txtPortNoAo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNoAo.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNoAo.TabIndex = 25
        '
        'txtFuNoAo
        '
        Me.txtFuNoAo.AcceptsReturn = True
        Me.txtFuNoAo.Location = New System.Drawing.Point(155, 282)
        Me.txtFuNoAo.MaxLength = 0
        Me.txtFuNoAo.Name = "txtFuNoAo"
        Me.txtFuNoAo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNoAo.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNoAo.TabIndex = 24
        '
        'txtPinAi
        '
        Me.txtPinAi.AcceptsReturn = True
        Me.txtPinAi.Location = New System.Drawing.Point(239, 254)
        Me.txtPinAi.MaxLength = 0
        Me.txtPinAi.Name = "txtPinAi"
        Me.txtPinAi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinAi.Size = New System.Drawing.Size(40, 19)
        Me.txtPinAi.TabIndex = 23
        '
        'txtPortNoAi
        '
        Me.txtPortNoAi.AcceptsReturn = True
        Me.txtPortNoAi.Location = New System.Drawing.Point(197, 254)
        Me.txtPortNoAi.MaxLength = 0
        Me.txtPortNoAi.Name = "txtPortNoAi"
        Me.txtPortNoAi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNoAi.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNoAi.TabIndex = 22
        '
        'txtFuNoAi
        '
        Me.txtFuNoAi.AcceptsReturn = True
        Me.txtFuNoAi.Location = New System.Drawing.Point(155, 254)
        Me.txtFuNoAi.MaxLength = 0
        Me.txtFuNoAi.Name = "txtFuNoAi"
        Me.txtFuNoAi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNoAi.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNoAi.TabIndex = 21
        '
        'txtPinDo
        '
        Me.txtPinDo.AcceptsReturn = True
        Me.txtPinDo.Location = New System.Drawing.Point(240, 220)
        Me.txtPinDo.MaxLength = 0
        Me.txtPinDo.Name = "txtPinDo"
        Me.txtPinDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinDo.Size = New System.Drawing.Size(40, 19)
        Me.txtPinDo.TabIndex = 20
        '
        'txtPortNoDo
        '
        Me.txtPortNoDo.AcceptsReturn = True
        Me.txtPortNoDo.Location = New System.Drawing.Point(198, 220)
        Me.txtPortNoDo.MaxLength = 0
        Me.txtPortNoDo.Name = "txtPortNoDo"
        Me.txtPortNoDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNoDo.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNoDo.TabIndex = 19
        '
        'txtFuNoDo
        '
        Me.txtFuNoDo.AcceptsReturn = True
        Me.txtFuNoDo.Location = New System.Drawing.Point(156, 220)
        Me.txtFuNoDo.MaxLength = 0
        Me.txtFuNoDo.Name = "txtFuNoDo"
        Me.txtFuNoDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNoDo.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNoDo.TabIndex = 18
        '
        'txtPinDi
        '
        Me.txtPinDi.AcceptsReturn = True
        Me.txtPinDi.Location = New System.Drawing.Point(239, 106)
        Me.txtPinDi.MaxLength = 0
        Me.txtPinDi.Name = "txtPinDi"
        Me.txtPinDi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinDi.Size = New System.Drawing.Size(40, 19)
        Me.txtPinDi.TabIndex = 8
        '
        'txtPortNoDi
        '
        Me.txtPortNoDi.AcceptsReturn = True
        Me.txtPortNoDi.Location = New System.Drawing.Point(197, 106)
        Me.txtPortNoDi.MaxLength = 0
        Me.txtPortNoDi.Name = "txtPortNoDi"
        Me.txtPortNoDi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNoDi.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNoDi.TabIndex = 7
        '
        'txtFuNoDi
        '
        Me.txtFuNoDi.AcceptsReturn = True
        Me.txtFuNoDi.Location = New System.Drawing.Point(155, 106)
        Me.txtFuNoDi.MaxLength = 0
        Me.txtFuNoDi.Name = "txtFuNoDi"
        Me.txtFuNoDi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNoDi.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNoDi.TabIndex = 6
        '
        'lblDo8
        '
        Me.lblDo8.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo8.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo8.Location = New System.Drawing.Point(646, 166)
        Me.lblDo8.Name = "lblDo8"
        Me.lblDo8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo8.Size = New System.Drawing.Size(86, 23)
        Me.lblDo8.TabIndex = 224
        Me.lblDo8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDo5
        '
        Me.lblDo5.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo5.Location = New System.Drawing.Point(382, 166)
        Me.lblDo5.Name = "lblDo5"
        Me.lblDo5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo5.Size = New System.Drawing.Size(86, 23)
        Me.lblDo5.TabIndex = 224
        Me.lblDo5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDo7
        '
        Me.lblDo7.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo7.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo7.Location = New System.Drawing.Point(558, 166)
        Me.lblDo7.Name = "lblDo7"
        Me.lblDo7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo7.Size = New System.Drawing.Size(86, 23)
        Me.lblDo7.TabIndex = 223
        Me.lblDo7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDo4
        '
        Me.lblDo4.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo4.Location = New System.Drawing.Point(294, 166)
        Me.lblDo4.Name = "lblDo4"
        Me.lblDo4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo4.Size = New System.Drawing.Size(86, 23)
        Me.lblDo4.TabIndex = 223
        Me.lblDo4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDo6
        '
        Me.lblDo6.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo6.Location = New System.Drawing.Point(470, 166)
        Me.lblDo6.Name = "lblDo6"
        Me.lblDo6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo6.Size = New System.Drawing.Size(86, 23)
        Me.lblDo6.TabIndex = 222
        Me.lblDo6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDo3
        '
        Me.lblDo3.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo3.Location = New System.Drawing.Point(206, 166)
        Me.lblDo3.Name = "lblDo3"
        Me.lblDo3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo3.Size = New System.Drawing.Size(86, 23)
        Me.lblDo3.TabIndex = 222
        Me.lblDo3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDo2
        '
        Me.lblDo2.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo2.Location = New System.Drawing.Point(118, 166)
        Me.lblDo2.Name = "lblDo2"
        Me.lblDo2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo2.Size = New System.Drawing.Size(86, 23)
        Me.lblDo2.TabIndex = 221
        Me.lblDo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi1
        '
        Me.lblDi1.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi1.Location = New System.Drawing.Point(30, 133)
        Me.lblDi1.Name = "lblDi1"
        Me.lblDi1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi1.Size = New System.Drawing.Size(86, 23)
        Me.lblDi1.TabIndex = 215
        Me.lblDi1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDo1
        '
        Me.lblDo1.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo1.Location = New System.Drawing.Point(30, 166)
        Me.lblDo1.Name = "lblDo1"
        Me.lblDo1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo1.Size = New System.Drawing.Size(86, 23)
        Me.lblDo1.TabIndex = 220
        Me.lblDo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi2
        '
        Me.lblDi2.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi2.Location = New System.Drawing.Point(118, 133)
        Me.lblDi2.Name = "lblDi2"
        Me.lblDi2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi2.Size = New System.Drawing.Size(86, 23)
        Me.lblDi2.TabIndex = 216
        Me.lblDi2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi8
        '
        Me.lblDi8.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi8.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi8.Location = New System.Drawing.Point(646, 133)
        Me.lblDi8.Name = "lblDi8"
        Me.lblDi8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi8.Size = New System.Drawing.Size(86, 23)
        Me.lblDi8.TabIndex = 219
        Me.lblDi8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi5
        '
        Me.lblDi5.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi5.Location = New System.Drawing.Point(382, 133)
        Me.lblDi5.Name = "lblDi5"
        Me.lblDi5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi5.Size = New System.Drawing.Size(86, 23)
        Me.lblDi5.TabIndex = 219
        Me.lblDi5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi6
        '
        Me.lblDi6.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi6.Location = New System.Drawing.Point(470, 133)
        Me.lblDi6.Name = "lblDi6"
        Me.lblDi6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi6.Size = New System.Drawing.Size(86, 23)
        Me.lblDi6.TabIndex = 217
        Me.lblDi6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi7
        '
        Me.lblDi7.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi7.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi7.Location = New System.Drawing.Point(558, 133)
        Me.lblDi7.Name = "lblDi7"
        Me.lblDi7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi7.Size = New System.Drawing.Size(86, 23)
        Me.lblDi7.TabIndex = 218
        Me.lblDi7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi3
        '
        Me.lblDi3.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi3.Location = New System.Drawing.Point(206, 133)
        Me.lblDi3.Name = "lblDi3"
        Me.lblDi3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi3.Size = New System.Drawing.Size(86, 23)
        Me.lblDi3.TabIndex = 217
        Me.lblDi3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi4
        '
        Me.lblDi4.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi4.Location = New System.Drawing.Point(294, 133)
        Me.lblDi4.Name = "lblDi4"
        Me.lblDi4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi4.Size = New System.Drawing.Size(86, 23)
        Me.lblDi4.TabIndex = 218
        Me.lblDi4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkStatusAlarm
        '
        Me.chkStatusAlarm.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatusAlarm.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatusAlarm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatusAlarm.Location = New System.Drawing.Point(232, 382)
        Me.chkStatusAlarm.Name = "chkStatusAlarm"
        Me.chkStatusAlarm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkStatusAlarm.Size = New System.Drawing.Size(112, 19)
        Me.chkStatusAlarm.TabIndex = 30
        Me.chkStatusAlarm.Text = "Status Alarm    "
        Me.chkStatusAlarm.UseVisualStyleBackColor = True
        Me.chkStatusAlarm.Visible = False
        '
        'lblDiStart
        '
        Me.lblDiStart.AutoSize = True
        Me.lblDiStart.BackColor = System.Drawing.SystemColors.Control
        Me.lblDiStart.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiStart.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.lblDiStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDiStart.Location = New System.Drawing.Point(27, 108)
        Me.lblDiStart.Name = "lblDiStart"
        Me.lblDiStart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiStart.Size = New System.Drawing.Size(119, 12)
        Me.lblDiStart.TabIndex = 195
        Me.lblDiStart.Text = "DI Start FU Address"
        Me.lblDiStart.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAlarmTimeup
        '
        Me.txtAlarmTimeup.AcceptsReturn = True
        Me.txtAlarmTimeup.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.txtAlarmTimeup.Location = New System.Drawing.Point(156, 316)
        Me.txtAlarmTimeup.MaxLength = 0
        Me.txtAlarmTimeup.Name = "txtAlarmTimeup"
        Me.txtAlarmTimeup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlarmTimeup.Size = New System.Drawing.Size(48, 19)
        Me.txtAlarmTimeup.TabIndex = 27
        Me.txtAlarmTimeup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(40, 319)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(107, 12)
        Me.Label12.TabIndex = 202
        Me.Label12.Text = "Alarm Timer Count"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(211, 319)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(23, 12)
        Me.Label13.TabIndex = 203
        Me.Label13.Text = "sec"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAoTerminal
        '
        Me.lblAoTerminal.AutoSize = True
        Me.lblAoTerminal.BackColor = System.Drawing.SystemColors.Control
        Me.lblAoTerminal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAoTerminal.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.lblAoTerminal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAoTerminal.Location = New System.Drawing.Point(62, 285)
        Me.lblAoTerminal.Name = "lblAoTerminal"
        Me.lblAoTerminal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAoTerminal.Size = New System.Drawing.Size(83, 12)
        Me.lblAoTerminal.TabIndex = 214
        Me.lblAoTerminal.Text = "AO FU Address"
        Me.lblAoTerminal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDoStart
        '
        Me.lblDoStart.AutoSize = True
        Me.lblDoStart.BackColor = System.Drawing.SystemColors.Control
        Me.lblDoStart.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDoStart.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.lblDoStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDoStart.Location = New System.Drawing.Point(28, 223)
        Me.lblDoStart.Name = "lblDoStart"
        Me.lblDoStart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDoStart.Size = New System.Drawing.Size(119, 12)
        Me.lblDoStart.TabIndex = 207
        Me.lblDoStart.Text = "DO Start FU Address"
        Me.lblDoStart.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAiTerminal
        '
        Me.lblAiTerminal.AutoSize = True
        Me.lblAiTerminal.BackColor = System.Drawing.SystemColors.Control
        Me.lblAiTerminal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAiTerminal.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.lblAiTerminal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAiTerminal.Location = New System.Drawing.Point(62, 256)
        Me.lblAiTerminal.Name = "lblAiTerminal"
        Me.lblAiTerminal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAiTerminal.Size = New System.Drawing.Size(83, 12)
        Me.lblAiTerminal.TabIndex = 213
        Me.lblAiTerminal.Text = "AI FU Address"
        Me.lblAiTerminal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtStatusOut
        '
        Me.txtStatusOut.AcceptsReturn = True
        Me.txtStatusOut.Location = New System.Drawing.Point(240, 72)
        Me.txtStatusOut.MaxLength = 0
        Me.txtStatusOut.Name = "txtStatusOut"
        Me.txtStatusOut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusOut.Size = New System.Drawing.Size(127, 19)
        Me.txtStatusOut.TabIndex = 5
        '
        'cmbStatusOut
        '
        Me.cmbStatusOut.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatusOut.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatusOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatusOut.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbStatusOut.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatusOut.Location = New System.Drawing.Point(79, 71)
        Me.cmbStatusOut.Name = "cmbStatusOut"
        Me.cmbStatusOut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatusOut.Size = New System.Drawing.Size(157, 20)
        Me.cmbStatusOut.TabIndex = 4
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(19, 74)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 194
        Me.Label11.Text = "Status O"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtStatusIn
        '
        Me.txtStatusIn.AcceptsReturn = True
        Me.txtStatusIn.Location = New System.Drawing.Point(239, 44)
        Me.txtStatusIn.MaxLength = 0
        Me.txtStatusIn.Name = "txtStatusIn"
        Me.txtStatusIn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusIn.Size = New System.Drawing.Size(127, 19)
        Me.txtStatusIn.TabIndex = 3
        '
        'cmbStatusIn
        '
        Me.cmbStatusIn.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatusIn.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatusIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatusIn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbStatusIn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatusIn.Location = New System.Drawing.Point(79, 44)
        Me.cmbStatusIn.Name = "cmbStatusIn"
        Me.cmbStatusIn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatusIn.Size = New System.Drawing.Size(157, 20)
        Me.cmbStatusIn.TabIndex = 2
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(19, 47)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(53, 12)
        Me.Label27.TabIndex = 193
        Me.Label27.Text = "Status I"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbExtDevice
        '
        Me.cmbExtDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExtDevice.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbExtDevice.FormattingEnabled = True
        Me.cmbExtDevice.Location = New System.Drawing.Point(239, 16)
        Me.cmbExtDevice.Name = "cmbExtDevice"
        Me.cmbExtDevice.Size = New System.Drawing.Size(246, 20)
        Me.cmbExtDevice.TabIndex = 1
        '
        'txtBitCount
        '
        Me.txtBitCount.AcceptsReturn = True
        Me.txtBitCount.Location = New System.Drawing.Point(388, 106)
        Me.txtBitCount.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtBitCount.MaxLength = 0
        Me.txtBitCount.Name = "txtBitCount"
        Me.txtBitCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBitCount.Size = New System.Drawing.Size(48, 19)
        Me.txtBitCount.TabIndex = 9
        Me.txtBitCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBitCount
        '
        Me.lblBitCount.AutoSize = True
        Me.lblBitCount.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBitCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitCount.Location = New System.Drawing.Point(292, 109)
        Me.lblBitCount.Name = "lblBitCount"
        Me.lblBitCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBitCount.Size = New System.Drawing.Size(89, 12)
        Me.lblBitCount.TabIndex = 192
        Me.lblBitCount.Text = "Terminal Count"
        Me.lblBitCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbDataType
        '
        Me.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbDataType.FormattingEnabled = True
        Me.cmbDataType.Location = New System.Drawing.Point(79, 16)
        Me.cmbDataType.Name = "cmbDataType"
        Me.cmbDataType.Size = New System.Drawing.Size(157, 20)
        Me.cmbDataType.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(12, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(59, 12)
        Me.Label8.TabIndex = 191
        Me.Label8.Text = "Data Type"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraHiHi0
        '
        Me.fraHiHi0.BackColor = System.Drawing.SystemColors.Control
        Me.fraHiHi0.Controls.Add(Me.fraFeAlarmInfo2)
        Me.fraHiHi0.Controls.Add(Me.lblStatus)
        Me.fraHiHi0.Controls.Add(Me.txtStatusFa)
        Me.fraHiHi0.Controls.Add(Me.txtGRep2Fa)
        Me.fraHiHi0.Controls.Add(Me.txtGRep1Fa)
        Me.fraHiHi0.Controls.Add(Me.txtExtGFa)
        Me.fraHiHi0.Controls.Add(Me.txtDelayFa)
        Me.fraHiHi0.Controls.Add(Me.Label128)
        Me.fraHiHi0.Controls.Add(Me.Label28)
        Me.fraHiHi0.Controls.Add(Me.Label30)
        Me.fraHiHi0.Controls.Add(Me.Label40)
        Me.fraHiHi0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraHiHi0.Location = New System.Drawing.Point(12, 412)
        Me.fraHiHi0.Name = "fraHiHi0"
        Me.fraHiHi0.Padding = New System.Windows.Forms.Padding(0)
        Me.fraHiHi0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraHiHi0.Size = New System.Drawing.Size(348, 176)
        Me.fraHiHi0.TabIndex = 31
        Me.fraHiHi0.TabStop = False
        Me.fraHiHi0.Text = "Feedback Alarm"
        '
        'fraFeAlarmInfo2
        '
        Me.fraFeAlarmInfo2.Controls.Add(Me.Label39)
        Me.fraFeAlarmInfo2.Controls.Add(Me.txtVar)
        Me.fraFeAlarmInfo2.Controls.Add(Me.txtSt)
        Me.fraFeAlarmInfo2.Controls.Add(Me.txtHys2)
        Me.fraFeAlarmInfo2.Controls.Add(Me.txtHys1)
        Me.fraFeAlarmInfo2.Controls.Add(Me.txtSp2)
        Me.fraFeAlarmInfo2.Controls.Add(Me.txtSp1)
        Me.fraFeAlarmInfo2.Controls.Add(Me.lblVar)
        Me.fraFeAlarmInfo2.Controls.Add(Me.Label48)
        Me.fraFeAlarmInfo2.Controls.Add(Me.Label43)
        Me.fraFeAlarmInfo2.Controls.Add(Me.Label46)
        Me.fraFeAlarmInfo2.Controls.Add(Me.Label6)
        Me.fraFeAlarmInfo2.Controls.Add(Me.Label5)
        Me.fraFeAlarmInfo2.Location = New System.Drawing.Point(4, 68)
        Me.fraFeAlarmInfo2.Name = "fraFeAlarmInfo2"
        Me.fraFeAlarmInfo2.Size = New System.Drawing.Size(340, 100)
        Me.fraFeAlarmInfo2.TabIndex = 7
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(308, 3)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(35, 24)
        Me.Label39.TabIndex = 204
        Me.Label39.Text = "* 100" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "msec"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtVar
        '
        Me.txtVar.AcceptsReturn = True
        Me.txtVar.Location = New System.Drawing.Point(260, 28)
        Me.txtVar.MaxLength = 0
        Me.txtVar.Name = "txtVar"
        Me.txtVar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVar.Size = New System.Drawing.Size(68, 19)
        Me.txtVar.TabIndex = 5
        Me.txtVar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSt
        '
        Me.txtSt.AcceptsReturn = True
        Me.txtSt.Location = New System.Drawing.Point(260, 4)
        Me.txtSt.MaxLength = 0
        Me.txtSt.Name = "txtSt"
        Me.txtSt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSt.Size = New System.Drawing.Size(44, 19)
        Me.txtSt.TabIndex = 4
        Me.txtSt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtHys2
        '
        Me.txtHys2.AcceptsReturn = True
        Me.txtHys2.Location = New System.Drawing.Point(112, 76)
        Me.txtHys2.MaxLength = 0
        Me.txtHys2.Name = "txtHys2"
        Me.txtHys2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHys2.Size = New System.Drawing.Size(48, 19)
        Me.txtHys2.TabIndex = 3
        Me.txtHys2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtHys1
        '
        Me.txtHys1.AcceptsReturn = True
        Me.txtHys1.Location = New System.Drawing.Point(112, 52)
        Me.txtHys1.MaxLength = 0
        Me.txtHys1.Name = "txtHys1"
        Me.txtHys1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHys1.Size = New System.Drawing.Size(48, 19)
        Me.txtHys1.TabIndex = 2
        Me.txtHys1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSp2
        '
        Me.txtSp2.AcceptsReturn = True
        Me.txtSp2.Location = New System.Drawing.Point(112, 28)
        Me.txtSp2.MaxLength = 0
        Me.txtSp2.Name = "txtSp2"
        Me.txtSp2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSp2.Size = New System.Drawing.Size(48, 19)
        Me.txtSp2.TabIndex = 1
        Me.txtSp2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSp1
        '
        Me.txtSp1.AcceptsReturn = True
        Me.txtSp1.Location = New System.Drawing.Point(112, 4)
        Me.txtSp1.MaxLength = 0
        Me.txtSp1.Name = "txtSp1"
        Me.txtSp1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSp1.Size = New System.Drawing.Size(48, 19)
        Me.txtSp1.TabIndex = 0
        Me.txtSp1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblVar
        '
        Me.lblVar.AutoSize = True
        Me.lblVar.BackColor = System.Drawing.SystemColors.Control
        Me.lblVar.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVar.Location = New System.Drawing.Point(196, 32)
        Me.lblVar.Name = "lblVar"
        Me.lblVar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVar.Size = New System.Drawing.Size(59, 12)
        Me.lblVar.TabIndex = 136
        Me.lblVar.Text = "Variation"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(172, 8)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(83, 12)
        Me.Label48.TabIndex = 135
        Me.Label48.Text = "Sampling time"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(4, 80)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(107, 12)
        Me.Label43.TabIndex = 134
        Me.Label43.Text = "Hysteresis(Close)"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(4, 56)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(101, 12)
        Me.Label46.TabIndex = 133
        Me.Label46.Text = "Hysteresis(Open)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(12, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(95, 12)
        Me.Label6.TabIndex = 132
        Me.Label6.Text = "Control value 2"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(12, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(95, 12)
        Me.Label5.TabIndex = 131
        Me.Label5.Text = "Control value 1"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatus.Location = New System.Drawing.Point(253, 24)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.Size = New System.Drawing.Size(41, 12)
        Me.lblStatus.TabIndex = 116
        Me.lblStatus.Text = "Status"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtStatusFa
        '
        Me.txtStatusFa.AcceptsReturn = True
        Me.txtStatusFa.Location = New System.Drawing.Point(237, 40)
        Me.txtStatusFa.MaxLength = 0
        Me.txtStatusFa.Name = "txtStatusFa"
        Me.txtStatusFa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusFa.Size = New System.Drawing.Size(72, 19)
        Me.txtStatusFa.TabIndex = 6
        '
        'txtGRep2Fa
        '
        Me.txtGRep2Fa.AcceptsReturn = True
        Me.txtGRep2Fa.Location = New System.Drawing.Point(181, 40)
        Me.txtGRep2Fa.MaxLength = 0
        Me.txtGRep2Fa.Name = "txtGRep2Fa"
        Me.txtGRep2Fa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2Fa.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep2Fa.TabIndex = 4
        Me.txtGRep2Fa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1Fa
        '
        Me.txtGRep1Fa.AcceptsReturn = True
        Me.txtGRep1Fa.Location = New System.Drawing.Point(127, 40)
        Me.txtGRep1Fa.MaxLength = 0
        Me.txtGRep1Fa.Name = "txtGRep1Fa"
        Me.txtGRep1Fa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1Fa.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep1Fa.TabIndex = 3
        Me.txtGRep1Fa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExtGFa
        '
        Me.txtExtGFa.AcceptsReturn = True
        Me.txtExtGFa.Location = New System.Drawing.Point(19, 40)
        Me.txtExtGFa.MaxLength = 0
        Me.txtExtGFa.Name = "txtExtGFa"
        Me.txtExtGFa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGFa.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGFa.TabIndex = 1
        Me.txtExtGFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelayFa
        '
        Me.txtDelayFa.AcceptsReturn = True
        Me.txtDelayFa.Location = New System.Drawing.Point(73, 40)
        Me.txtDelayFa.MaxLength = 0
        Me.txtDelayFa.Name = "txtDelayFa"
        Me.txtDelayFa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayFa.Size = New System.Drawing.Size(48, 19)
        Me.txtDelayFa.TabIndex = 2
        Me.txtDelayFa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label128
        '
        Me.Label128.AutoSize = True
        Me.Label128.BackColor = System.Drawing.SystemColors.Control
        Me.Label128.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label128.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label128.Location = New System.Drawing.Point(77, 24)
        Me.Label128.Name = "Label128"
        Me.Label128.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label128.Size = New System.Drawing.Size(35, 12)
        Me.Label128.TabIndex = 60
        Me.Label128.Text = "Delay"
        Me.Label128.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(20, 24)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(35, 12)
        Me.Label28.TabIndex = 58
        Me.Label28.Text = "EXT.G"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(129, 24)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(41, 12)
        Me.Label30.TabIndex = 57
        Me.Label30.Text = "G REP1"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(182, 24)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(41, 12)
        Me.Label40.TabIndex = 56
        Me.Label40.Text = "G REP2"
        '
        'fraAiInfo
        '
        Me.fraAiInfo.Controls.Add(Me.txtUnit)
        Me.fraAiInfo.Controls.Add(Me.cmbUnit)
        Me.fraAiInfo.Controls.Add(Me.Label47)
        Me.fraAiInfo.Controls.Add(Me.lblNormalHif)
        Me.fraAiInfo.Controls.Add(Me.Label41)
        Me.fraAiInfo.Controls.Add(Me.txtRangeTo)
        Me.fraAiInfo.Controls.Add(Me.txtRangeFrom)
        Me.fraAiInfo.Controls.Add(Me.lblRange)
        Me.fraAiInfo.Controls.Add(Me.txtOffset)
        Me.fraAiInfo.Controls.Add(Me.lblNormal)
        Me.fraAiInfo.Controls.Add(Me.lblString)
        Me.fraAiInfo.Controls.Add(Me.txtLowNormal)
        Me.fraAiInfo.Controls.Add(Me.chkCenterGraph)
        Me.fraAiInfo.Controls.Add(Me.txtHighNormal)
        Me.fraAiInfo.Controls.Add(Me.lblOffset)
        Me.fraAiInfo.Controls.Add(Me.txtString)
        Me.fraAiInfo.Location = New System.Drawing.Point(368, 412)
        Me.fraAiInfo.Name = "fraAiInfo"
        Me.fraAiInfo.Size = New System.Drawing.Size(388, 128)
        Me.fraAiInfo.TabIndex = 31
        Me.fraAiInfo.TabStop = False
        '
        'txtUnit
        '
        Me.txtUnit.AcceptsReturn = True
        Me.txtUnit.Location = New System.Drawing.Point(210, 96)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(116, 19)
        Me.txtUnit.TabIndex = 115
        '
        'cmbUnit
        '
        Me.cmbUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbUnit.Location = New System.Drawing.Point(84, 96)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbUnit.Size = New System.Drawing.Size(120, 20)
        Me.cmbUnit.TabIndex = 114
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.SystemColors.Control
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label47.Location = New System.Drawing.Point(46, 98)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(29, 12)
        Me.Label47.TabIndex = 116
        Me.Label47.Text = "Unit"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNormalHif
        '
        Me.lblNormalHif.AutoSize = True
        Me.lblNormalHif.BackColor = System.Drawing.SystemColors.Control
        Me.lblNormalHif.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNormalHif.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNormalHif.Location = New System.Drawing.Point(160, 48)
        Me.lblNormalHif.Name = "lblNormalHif"
        Me.lblNormalHif.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNormalHif.Size = New System.Drawing.Size(11, 12)
        Me.lblNormalHif.TabIndex = 113
        Me.lblNormalHif.Text = "-"
        Me.lblNormalHif.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(160, 24)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(11, 12)
        Me.Label41.TabIndex = 112
        Me.Label41.Text = "-"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtRangeTo
        '
        Me.txtRangeTo.AcceptsReturn = True
        Me.txtRangeTo.Location = New System.Drawing.Point(172, 19)
        Me.txtRangeTo.MaxLength = 0
        Me.txtRangeTo.Name = "txtRangeTo"
        Me.txtRangeTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRangeTo.Size = New System.Drawing.Size(72, 19)
        Me.txtRangeTo.TabIndex = 2
        Me.txtRangeTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRangeFrom
        '
        Me.txtRangeFrom.AcceptsReturn = True
        Me.txtRangeFrom.Location = New System.Drawing.Point(84, 19)
        Me.txtRangeFrom.MaxLength = 0
        Me.txtRangeFrom.Name = "txtRangeFrom"
        Me.txtRangeFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRangeFrom.Size = New System.Drawing.Size(72, 19)
        Me.txtRangeFrom.TabIndex = 1
        Me.txtRangeFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblRange
        '
        Me.lblRange.AutoSize = True
        Me.lblRange.BackColor = System.Drawing.SystemColors.Control
        Me.lblRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRange.Location = New System.Drawing.Point(12, 20)
        Me.lblRange.Name = "lblRange"
        Me.lblRange.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblRange.Size = New System.Drawing.Size(65, 12)
        Me.lblRange.TabIndex = 111
        Me.lblRange.Text = "Range Type"
        Me.lblRange.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtOffset
        '
        Me.txtOffset.AcceptsReturn = True
        Me.txtOffset.Location = New System.Drawing.Point(84, 69)
        Me.txtOffset.MaxLength = 0
        Me.txtOffset.Name = "txtOffset"
        Me.txtOffset.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOffset.Size = New System.Drawing.Size(72, 19)
        Me.txtOffset.TabIndex = 5
        Me.txtOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblNormal
        '
        Me.lblNormal.AutoSize = True
        Me.lblNormal.BackColor = System.Drawing.SystemColors.Control
        Me.lblNormal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNormal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNormal.Location = New System.Drawing.Point(36, 47)
        Me.lblNormal.Name = "lblNormal"
        Me.lblNormal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNormal.Size = New System.Drawing.Size(41, 12)
        Me.lblNormal.TabIndex = 92
        Me.lblNormal.Text = "Normal"
        Me.lblNormal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblString
        '
        Me.lblString.AutoSize = True
        Me.lblString.BackColor = System.Drawing.SystemColors.Control
        Me.lblString.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblString.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblString.Location = New System.Drawing.Point(280, 48)
        Me.lblString.Name = "lblString"
        Me.lblString.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblString.Size = New System.Drawing.Size(41, 12)
        Me.lblString.TabIndex = 91
        Me.lblString.Text = "String"
        Me.lblString.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLowNormal
        '
        Me.txtLowNormal.AcceptsReturn = True
        Me.txtLowNormal.Location = New System.Drawing.Point(84, 44)
        Me.txtLowNormal.MaxLength = 0
        Me.txtLowNormal.Name = "txtLowNormal"
        Me.txtLowNormal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLowNormal.Size = New System.Drawing.Size(72, 19)
        Me.txtLowNormal.TabIndex = 3
        Me.txtLowNormal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkCenterGraph
        '
        Me.chkCenterGraph.BackColor = System.Drawing.SystemColors.Control
        Me.chkCenterGraph.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCenterGraph.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCenterGraph.Location = New System.Drawing.Point(240, 71)
        Me.chkCenterGraph.Name = "chkCenterGraph"
        Me.chkCenterGraph.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkCenterGraph.Size = New System.Drawing.Size(100, 19)
        Me.chkCenterGraph.TabIndex = 7
        Me.chkCenterGraph.Text = "Center Graph"
        Me.chkCenterGraph.UseVisualStyleBackColor = True
        '
        'txtHighNormal
        '
        Me.txtHighNormal.AcceptsReturn = True
        Me.txtHighNormal.Location = New System.Drawing.Point(172, 44)
        Me.txtHighNormal.MaxLength = 0
        Me.txtHighNormal.Name = "txtHighNormal"
        Me.txtHighNormal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHighNormal.Size = New System.Drawing.Size(72, 19)
        Me.txtHighNormal.TabIndex = 4
        Me.txtHighNormal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblOffset
        '
        Me.lblOffset.AutoSize = True
        Me.lblOffset.BackColor = System.Drawing.SystemColors.Control
        Me.lblOffset.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOffset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOffset.Location = New System.Drawing.Point(36, 72)
        Me.lblOffset.Name = "lblOffset"
        Me.lblOffset.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOffset.Size = New System.Drawing.Size(41, 12)
        Me.lblOffset.TabIndex = 90
        Me.lblOffset.Text = "Offset"
        Me.lblOffset.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtString
        '
        Me.txtString.AcceptsReturn = True
        Me.txtString.Location = New System.Drawing.Point(325, 44)
        Me.txtString.MaxLength = 0
        Me.txtString.Name = "txtString"
        Me.txtString.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtString.Size = New System.Drawing.Size(48, 19)
        Me.txtString.TabIndex = 6
        Me.txtString.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'fraInputAlrm
        '
        Me.fraInputAlrm.BackColor = System.Drawing.SystemColors.Control
        Me.fraInputAlrm.Controls.Add(Me.txtStatusSF)
        Me.fraInputAlrm.Controls.Add(Me.txtStatusLoLo)
        Me.fraInputAlrm.Controls.Add(Me.txtStatusLo)
        Me.fraInputAlrm.Controls.Add(Me.txtStatusHi)
        Me.fraInputAlrm.Controls.Add(Me.Label21)
        Me.fraInputAlrm.Controls.Add(Me.txtStatusHiHi)
        Me.fraInputAlrm.Controls.Add(Me.txtValueHiHi)
        Me.fraInputAlrm.Controls.Add(Me.Label22)
        Me.fraInputAlrm.Controls.Add(Me.cmbValueSensorFailure)
        Me.fraInputAlrm.Controls.Add(Me.Label29)
        Me.fraInputAlrm.Controls.Add(Me.Label31)
        Me.fraInputAlrm.Controls.Add(Me.txtExtGSensorFailure)
        Me.fraInputAlrm.Controls.Add(Me.Label32)
        Me.fraInputAlrm.Controls.Add(Me.txtGRep2SensorFailure)
        Me.fraInputAlrm.Controls.Add(Me.txtGRep2LoLo)
        Me.fraInputAlrm.Controls.Add(Me.txtGRep2Lo)
        Me.fraInputAlrm.Controls.Add(Me.Label64)
        Me.fraInputAlrm.Controls.Add(Me.txtGRep1SensorFailure)
        Me.fraInputAlrm.Controls.Add(Me.txtGRep1LoLo)
        Me.fraInputAlrm.Controls.Add(Me.txtExtGLoLo)
        Me.fraInputAlrm.Controls.Add(Me.txtGRep2Hi)
        Me.fraInputAlrm.Controls.Add(Me.txtDelaySensorFailure)
        Me.fraInputAlrm.Controls.Add(Me.txtDelayLoLo)
        Me.fraInputAlrm.Controls.Add(Me.txtGRep1Lo)
        Me.fraInputAlrm.Controls.Add(Me.txtDelayLo)
        Me.fraInputAlrm.Controls.Add(Me.txtValueLoLo)
        Me.fraInputAlrm.Controls.Add(Me.txtGRep1Hi)
        Me.fraInputAlrm.Controls.Add(Me.txtExtGLo)
        Me.fraInputAlrm.Controls.Add(Me.txtValueLo)
        Me.fraInputAlrm.Controls.Add(Me.txtExtGHi)
        Me.fraInputAlrm.Controls.Add(Me.txtDelayHiHi)
        Me.fraInputAlrm.Controls.Add(Me.txtDelayHi)
        Me.fraInputAlrm.Controls.Add(Me.txtGRep2HiHi)
        Me.fraInputAlrm.Controls.Add(Me.txtExtGHiHi)
        Me.fraInputAlrm.Controls.Add(Me.txtValueHi)
        Me.fraInputAlrm.Controls.Add(Me.txtGRep1HiHi)
        Me.fraInputAlrm.Controls.Add(Me.Label33)
        Me.fraInputAlrm.Controls.Add(Me.Label129)
        Me.fraInputAlrm.Controls.Add(Me.Label34)
        Me.fraInputAlrm.Controls.Add(Me.Label35)
        Me.fraInputAlrm.Controls.Add(Me.Label38)
        Me.fraInputAlrm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraInputAlrm.Location = New System.Drawing.Point(368, 228)
        Me.fraInputAlrm.Name = "fraInputAlrm"
        Me.fraInputAlrm.Padding = New System.Windows.Forms.Padding(0)
        Me.fraInputAlrm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraInputAlrm.Size = New System.Drawing.Size(388, 180)
        Me.fraInputAlrm.TabIndex = 30
        Me.fraInputAlrm.TabStop = False
        Me.fraInputAlrm.Text = "Input Alarm"
        '
        'txtStatusSF
        '
        Me.txtStatusSF.AcceptsReturn = True
        Me.txtStatusSF.Enabled = False
        Me.txtStatusSF.Location = New System.Drawing.Point(309, 146)
        Me.txtStatusSF.MaxLength = 0
        Me.txtStatusSF.Name = "txtStatusSF"
        Me.txtStatusSF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusSF.Size = New System.Drawing.Size(60, 19)
        Me.txtStatusSF.TabIndex = 29
        '
        'txtStatusLoLo
        '
        Me.txtStatusLoLo.AcceptsReturn = True
        Me.txtStatusLoLo.Location = New System.Drawing.Point(309, 119)
        Me.txtStatusLoLo.MaxLength = 0
        Me.txtStatusLoLo.Name = "txtStatusLoLo"
        Me.txtStatusLoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusLoLo.Size = New System.Drawing.Size(60, 19)
        Me.txtStatusLoLo.TabIndex = 23
        '
        'txtStatusLo
        '
        Me.txtStatusLo.AcceptsReturn = True
        Me.txtStatusLo.Location = New System.Drawing.Point(309, 92)
        Me.txtStatusLo.MaxLength = 0
        Me.txtStatusLo.Name = "txtStatusLo"
        Me.txtStatusLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusLo.Size = New System.Drawing.Size(60, 19)
        Me.txtStatusLo.TabIndex = 17
        '
        'txtStatusHi
        '
        Me.txtStatusHi.AcceptsReturn = True
        Me.txtStatusHi.Location = New System.Drawing.Point(309, 65)
        Me.txtStatusHi.MaxLength = 0
        Me.txtStatusHi.Name = "txtStatusHi"
        Me.txtStatusHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusHi.Size = New System.Drawing.Size(60, 19)
        Me.txtStatusHi.TabIndex = 11
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(316, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(41, 12)
        Me.Label21.TabIndex = 115
        Me.Label21.Text = "Status"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtStatusHiHi
        '
        Me.txtStatusHiHi.AcceptsReturn = True
        Me.txtStatusHiHi.Location = New System.Drawing.Point(309, 38)
        Me.txtStatusHiHi.MaxLength = 0
        Me.txtStatusHiHi.Name = "txtStatusHiHi"
        Me.txtStatusHiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusHiHi.Size = New System.Drawing.Size(60, 19)
        Me.txtStatusHiHi.TabIndex = 5
        '
        'txtValueHiHi
        '
        Me.txtValueHiHi.AcceptsReturn = True
        Me.txtValueHiHi.Location = New System.Drawing.Point(52, 38)
        Me.txtValueHiHi.MaxLength = 0
        Me.txtValueHiHi.Name = "txtValueHiHi"
        Me.txtValueHiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtValueHiHi.Size = New System.Drawing.Size(72, 19)
        Me.txtValueHiHi.TabIndex = 0
        Me.txtValueHiHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(8, 146)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(47, 24)
        Me.Label22.TabIndex = 112
        Me.Label22.Text = "Sensor" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Failure"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbValueSensorFailure
        '
        Me.cmbValueSensorFailure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbValueSensorFailure.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbValueSensorFailure.FormattingEnabled = True
        Me.cmbValueSensorFailure.Location = New System.Drawing.Point(74, 146)
        Me.cmbValueSensorFailure.Name = "cmbValueSensorFailure"
        Me.cmbValueSensorFailure.Size = New System.Drawing.Size(48, 20)
        Me.cmbValueSensorFailure.TabIndex = 24
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(15, 122)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(29, 12)
        Me.Label29.TabIndex = 70
        Me.Label29.Text = "LOLO"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(26, 95)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(17, 12)
        Me.Label31.TabIndex = 69
        Me.Label31.Text = "LO"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtExtGSensorFailure
        '
        Me.txtExtGSensorFailure.AcceptsReturn = True
        Me.txtExtGSensorFailure.Location = New System.Drawing.Point(128, 146)
        Me.txtExtGSensorFailure.MaxLength = 0
        Me.txtExtGSensorFailure.Name = "txtExtGSensorFailure"
        Me.txtExtGSensorFailure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGSensorFailure.Size = New System.Drawing.Size(42, 19)
        Me.txtExtGSensorFailure.TabIndex = 25
        Me.txtExtGSensorFailure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(28, 68)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(17, 12)
        Me.Label32.TabIndex = 68
        Me.Label32.Text = "HI"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGRep2SensorFailure
        '
        Me.txtGRep2SensorFailure.AcceptsReturn = True
        Me.txtGRep2SensorFailure.Enabled = False
        Me.txtGRep2SensorFailure.Location = New System.Drawing.Point(264, 146)
        Me.txtGRep2SensorFailure.MaxLength = 0
        Me.txtGRep2SensorFailure.Name = "txtGRep2SensorFailure"
        Me.txtGRep2SensorFailure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2SensorFailure.Size = New System.Drawing.Size(42, 19)
        Me.txtGRep2SensorFailure.TabIndex = 28
        Me.txtGRep2SensorFailure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep2LoLo
        '
        Me.txtGRep2LoLo.AcceptsReturn = True
        Me.txtGRep2LoLo.Location = New System.Drawing.Point(264, 119)
        Me.txtGRep2LoLo.MaxLength = 0
        Me.txtGRep2LoLo.Name = "txtGRep2LoLo"
        Me.txtGRep2LoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2LoLo.Size = New System.Drawing.Size(42, 19)
        Me.txtGRep2LoLo.TabIndex = 22
        Me.txtGRep2LoLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep2Lo
        '
        Me.txtGRep2Lo.AcceptsReturn = True
        Me.txtGRep2Lo.Location = New System.Drawing.Point(264, 92)
        Me.txtGRep2Lo.MaxLength = 0
        Me.txtGRep2Lo.Name = "txtGRep2Lo"
        Me.txtGRep2Lo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2Lo.Size = New System.Drawing.Size(42, 19)
        Me.txtGRep2Lo.TabIndex = 16
        Me.txtGRep2Lo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.BackColor = System.Drawing.SystemColors.Control
        Me.Label64.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label64.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label64.Location = New System.Drawing.Point(16, 41)
        Me.Label64.Name = "Label64"
        Me.Label64.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label64.Size = New System.Drawing.Size(29, 12)
        Me.Label64.TabIndex = 59
        Me.Label64.Text = "HIHI"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGRep1SensorFailure
        '
        Me.txtGRep1SensorFailure.AcceptsReturn = True
        Me.txtGRep1SensorFailure.Enabled = False
        Me.txtGRep1SensorFailure.Location = New System.Drawing.Point(219, 146)
        Me.txtGRep1SensorFailure.MaxLength = 0
        Me.txtGRep1SensorFailure.Name = "txtGRep1SensorFailure"
        Me.txtGRep1SensorFailure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1SensorFailure.Size = New System.Drawing.Size(42, 19)
        Me.txtGRep1SensorFailure.TabIndex = 27
        Me.txtGRep1SensorFailure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1LoLo
        '
        Me.txtGRep1LoLo.AcceptsReturn = True
        Me.txtGRep1LoLo.Location = New System.Drawing.Point(219, 119)
        Me.txtGRep1LoLo.MaxLength = 0
        Me.txtGRep1LoLo.Name = "txtGRep1LoLo"
        Me.txtGRep1LoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1LoLo.Size = New System.Drawing.Size(42, 19)
        Me.txtGRep1LoLo.TabIndex = 21
        Me.txtGRep1LoLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExtGLoLo
        '
        Me.txtExtGLoLo.AcceptsReturn = True
        Me.txtExtGLoLo.Location = New System.Drawing.Point(128, 119)
        Me.txtExtGLoLo.MaxLength = 0
        Me.txtExtGLoLo.Name = "txtExtGLoLo"
        Me.txtExtGLoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGLoLo.Size = New System.Drawing.Size(42, 19)
        Me.txtExtGLoLo.TabIndex = 19
        Me.txtExtGLoLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep2Hi
        '
        Me.txtGRep2Hi.AcceptsReturn = True
        Me.txtGRep2Hi.Location = New System.Drawing.Point(264, 65)
        Me.txtGRep2Hi.MaxLength = 0
        Me.txtGRep2Hi.Name = "txtGRep2Hi"
        Me.txtGRep2Hi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2Hi.Size = New System.Drawing.Size(42, 19)
        Me.txtGRep2Hi.TabIndex = 10
        Me.txtGRep2Hi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelaySensorFailure
        '
        Me.txtDelaySensorFailure.AcceptsReturn = True
        Me.txtDelaySensorFailure.Location = New System.Drawing.Point(174, 146)
        Me.txtDelaySensorFailure.MaxLength = 0
        Me.txtDelaySensorFailure.Name = "txtDelaySensorFailure"
        Me.txtDelaySensorFailure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelaySensorFailure.Size = New System.Drawing.Size(42, 19)
        Me.txtDelaySensorFailure.TabIndex = 26
        Me.txtDelaySensorFailure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelayLoLo
        '
        Me.txtDelayLoLo.AcceptsReturn = True
        Me.txtDelayLoLo.Location = New System.Drawing.Point(174, 119)
        Me.txtDelayLoLo.MaxLength = 0
        Me.txtDelayLoLo.Name = "txtDelayLoLo"
        Me.txtDelayLoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayLoLo.Size = New System.Drawing.Size(42, 19)
        Me.txtDelayLoLo.TabIndex = 20
        Me.txtDelayLoLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1Lo
        '
        Me.txtGRep1Lo.AcceptsReturn = True
        Me.txtGRep1Lo.Location = New System.Drawing.Point(219, 92)
        Me.txtGRep1Lo.MaxLength = 0
        Me.txtGRep1Lo.Name = "txtGRep1Lo"
        Me.txtGRep1Lo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1Lo.Size = New System.Drawing.Size(42, 19)
        Me.txtGRep1Lo.TabIndex = 15
        Me.txtGRep1Lo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelayLo
        '
        Me.txtDelayLo.AcceptsReturn = True
        Me.txtDelayLo.Location = New System.Drawing.Point(174, 92)
        Me.txtDelayLo.MaxLength = 0
        Me.txtDelayLo.Name = "txtDelayLo"
        Me.txtDelayLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayLo.Size = New System.Drawing.Size(42, 19)
        Me.txtDelayLo.TabIndex = 14
        Me.txtDelayLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValueLoLo
        '
        Me.txtValueLoLo.AcceptsReturn = True
        Me.txtValueLoLo.Location = New System.Drawing.Point(52, 119)
        Me.txtValueLoLo.MaxLength = 0
        Me.txtValueLoLo.Name = "txtValueLoLo"
        Me.txtValueLoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtValueLoLo.Size = New System.Drawing.Size(72, 19)
        Me.txtValueLoLo.TabIndex = 18
        Me.txtValueLoLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1Hi
        '
        Me.txtGRep1Hi.AcceptsReturn = True
        Me.txtGRep1Hi.Location = New System.Drawing.Point(219, 65)
        Me.txtGRep1Hi.MaxLength = 0
        Me.txtGRep1Hi.Name = "txtGRep1Hi"
        Me.txtGRep1Hi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1Hi.Size = New System.Drawing.Size(42, 19)
        Me.txtGRep1Hi.TabIndex = 9
        Me.txtGRep1Hi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExtGLo
        '
        Me.txtExtGLo.AcceptsReturn = True
        Me.txtExtGLo.Location = New System.Drawing.Point(128, 92)
        Me.txtExtGLo.MaxLength = 0
        Me.txtExtGLo.Name = "txtExtGLo"
        Me.txtExtGLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGLo.Size = New System.Drawing.Size(42, 19)
        Me.txtExtGLo.TabIndex = 13
        Me.txtExtGLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValueLo
        '
        Me.txtValueLo.AcceptsReturn = True
        Me.txtValueLo.Location = New System.Drawing.Point(52, 92)
        Me.txtValueLo.MaxLength = 0
        Me.txtValueLo.Name = "txtValueLo"
        Me.txtValueLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtValueLo.Size = New System.Drawing.Size(72, 19)
        Me.txtValueLo.TabIndex = 12
        Me.txtValueLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExtGHi
        '
        Me.txtExtGHi.AcceptsReturn = True
        Me.txtExtGHi.Location = New System.Drawing.Point(128, 65)
        Me.txtExtGHi.MaxLength = 0
        Me.txtExtGHi.Name = "txtExtGHi"
        Me.txtExtGHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGHi.Size = New System.Drawing.Size(42, 19)
        Me.txtExtGHi.TabIndex = 7
        Me.txtExtGHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelayHiHi
        '
        Me.txtDelayHiHi.AcceptsReturn = True
        Me.txtDelayHiHi.Location = New System.Drawing.Point(174, 38)
        Me.txtDelayHiHi.MaxLength = 0
        Me.txtDelayHiHi.Name = "txtDelayHiHi"
        Me.txtDelayHiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayHiHi.Size = New System.Drawing.Size(42, 19)
        Me.txtDelayHiHi.TabIndex = 2
        Me.txtDelayHiHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelayHi
        '
        Me.txtDelayHi.AcceptsReturn = True
        Me.txtDelayHi.Location = New System.Drawing.Point(174, 65)
        Me.txtDelayHi.MaxLength = 0
        Me.txtDelayHi.Name = "txtDelayHi"
        Me.txtDelayHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayHi.Size = New System.Drawing.Size(42, 19)
        Me.txtDelayHi.TabIndex = 8
        Me.txtDelayHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep2HiHi
        '
        Me.txtGRep2HiHi.AcceptsReturn = True
        Me.txtGRep2HiHi.Location = New System.Drawing.Point(264, 38)
        Me.txtGRep2HiHi.MaxLength = 0
        Me.txtGRep2HiHi.Name = "txtGRep2HiHi"
        Me.txtGRep2HiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2HiHi.Size = New System.Drawing.Size(42, 19)
        Me.txtGRep2HiHi.TabIndex = 4
        Me.txtGRep2HiHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExtGHiHi
        '
        Me.txtExtGHiHi.AcceptsReturn = True
        Me.txtExtGHiHi.Location = New System.Drawing.Point(128, 38)
        Me.txtExtGHiHi.MaxLength = 0
        Me.txtExtGHiHi.Name = "txtExtGHiHi"
        Me.txtExtGHiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGHiHi.Size = New System.Drawing.Size(42, 19)
        Me.txtExtGHiHi.TabIndex = 1
        Me.txtExtGHiHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValueHi
        '
        Me.txtValueHi.AcceptsReturn = True
        Me.txtValueHi.Location = New System.Drawing.Point(52, 65)
        Me.txtValueHi.MaxLength = 0
        Me.txtValueHi.Name = "txtValueHi"
        Me.txtValueHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtValueHi.Size = New System.Drawing.Size(72, 19)
        Me.txtValueHi.TabIndex = 6
        Me.txtValueHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1HiHi
        '
        Me.txtGRep1HiHi.AcceptsReturn = True
        Me.txtGRep1HiHi.Location = New System.Drawing.Point(219, 38)
        Me.txtGRep1HiHi.MaxLength = 0
        Me.txtGRep1HiHi.Name = "txtGRep1HiHi"
        Me.txtGRep1HiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1HiHi.Size = New System.Drawing.Size(42, 19)
        Me.txtGRep1HiHi.TabIndex = 3
        Me.txtGRep1HiHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(178, 20)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(35, 12)
        Me.Label33.TabIndex = 60
        Me.Label33.Text = "Delay"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label129
        '
        Me.Label129.AutoSize = True
        Me.Label129.BackColor = System.Drawing.SystemColors.Control
        Me.Label129.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label129.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label129.Location = New System.Drawing.Point(68, 20)
        Me.Label129.Name = "Label129"
        Me.Label129.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label129.Size = New System.Drawing.Size(35, 12)
        Me.Label129.TabIndex = 59
        Me.Label129.Text = "Value"
        Me.Label129.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(130, 20)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(35, 12)
        Me.Label34.TabIndex = 58
        Me.Label34.Text = "EXT.G"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(222, 20)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(41, 12)
        Me.Label35.TabIndex = 57
        Me.Label35.Text = "G REP1"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(267, 20)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(41, 12)
        Me.Label38.TabIndex = 56
        Me.Label38.Text = "G REP2"
        '
        'lblDummy
        '
        Me.lblDummy.AutoSize = True
        Me.lblDummy.Location = New System.Drawing.Point(693, 2)
        Me.lblDummy.Name = "lblDummy"
        Me.lblDummy.Size = New System.Drawing.Size(95, 12)
        Me.lblDummy.TabIndex = 11
        Me.lblDummy.Text = "F5:DummySetting"
        '
        'frmChListValve
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(795, 677)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblDummy)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.cmdBeforeCH)
        Me.Controls.Add(Me.cmdNextCH)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 30)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChListValve"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CHANNEL LIST VALVE"
        Me.TabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.fraAlarm.ResumeLayout(False)
        Me.fraAlarm.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.fraComposite.ResumeLayout(False)
        Me.fraComposite.PerformLayout()
        Me.fraHiHi0.ResumeLayout(False)
        Me.fraHiHi0.PerformLayout()
        Me.fraFeAlarmInfo2.ResumeLayout(False)
        Me.fraFeAlarmInfo2.PerformLayout()
        Me.fraAiInfo.ResumeLayout(False)
        Me.fraAiInfo.PerformLayout()
        Me.fraInputAlrm.ResumeLayout(False)
        Me.fraInputAlrm.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdBeforeCH As System.Windows.Forms.Button
    Public WithEvents cmdNextCH As System.Windows.Forms.Button
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents cmbTime As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Public WithEvents txtShareChid As System.Windows.Forms.TextBox
    Public WithEvents lblShareChid As System.Windows.Forms.Label
    Public WithEvents lblShareType As System.Windows.Forms.Label
    Public WithEvents cmbShareType As System.Windows.Forms.ComboBox
    Public WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents txtSP As System.Windows.Forms.TextBox
    Public WithEvents txtPLC As System.Windows.Forms.TextBox
    Public WithEvents txtEP As System.Windows.Forms.TextBox
    Public WithEvents txtAC As System.Windows.Forms.TextBox
    Public WithEvents txtRL As System.Windows.Forms.TextBox
    Public WithEvents txtWK As System.Windows.Forms.TextBox
    Public WithEvents txtGWS As System.Windows.Forms.TextBox
    Public WithEvents txtSio As System.Windows.Forms.TextBox
    Public WithEvents txtSC As System.Windows.Forms.TextBox
    Public WithEvents txtDmy As System.Windows.Forms.TextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents fraAlarm As System.Windows.Forms.GroupBox
    Public WithEvents txtDelayTimer As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents txtExtGroup As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1 As System.Windows.Forms.TextBox
    Public WithEvents txtGRep2 As System.Windows.Forms.TextBox
    Friend WithEvents cmbSysNo As System.Windows.Forms.ComboBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtChNo As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents fraAiInfo As System.Windows.Forms.GroupBox
    Public WithEvents lblNormalHif As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents txtRangeTo As System.Windows.Forms.TextBox
    Public WithEvents txtRangeFrom As System.Windows.Forms.TextBox
    Public WithEvents lblRange As System.Windows.Forms.Label
    Public WithEvents txtOffset As System.Windows.Forms.TextBox
    Public WithEvents lblNormal As System.Windows.Forms.Label
    Public WithEvents lblString As System.Windows.Forms.Label
    Public WithEvents txtLowNormal As System.Windows.Forms.TextBox
    Public WithEvents chkCenterGraph As System.Windows.Forms.CheckBox
    Public WithEvents txtHighNormal As System.Windows.Forms.TextBox
    Public WithEvents lblOffset As System.Windows.Forms.Label
    Public WithEvents txtString As System.Windows.Forms.TextBox
    Public WithEvents fraInputAlrm As System.Windows.Forms.GroupBox
    Public WithEvents txtStatusSF As System.Windows.Forms.TextBox
    Public WithEvents txtStatusLoLo As System.Windows.Forms.TextBox
    Public WithEvents txtStatusLo As System.Windows.Forms.TextBox
    Public WithEvents txtStatusHi As System.Windows.Forms.TextBox
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents txtStatusHiHi As System.Windows.Forms.TextBox
    Public WithEvents txtValueHiHi As System.Windows.Forms.TextBox
    Public WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbValueSensorFailure As System.Windows.Forms.ComboBox
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents txtExtGSensorFailure As System.Windows.Forms.TextBox
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents txtGRep2SensorFailure As System.Windows.Forms.TextBox
    Public WithEvents txtGRep2LoLo As System.Windows.Forms.TextBox
    Public WithEvents txtGRep2Lo As System.Windows.Forms.TextBox
    Public WithEvents Label64 As System.Windows.Forms.Label
    Public WithEvents txtGRep1SensorFailure As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1LoLo As System.Windows.Forms.TextBox
    Public WithEvents txtExtGLoLo As System.Windows.Forms.TextBox
    Public WithEvents txtGRep2Hi As System.Windows.Forms.TextBox
    Public WithEvents txtDelaySensorFailure As System.Windows.Forms.TextBox
    Public WithEvents txtDelayLoLo As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1Lo As System.Windows.Forms.TextBox
    Public WithEvents txtDelayLo As System.Windows.Forms.TextBox
    Public WithEvents txtValueLoLo As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1Hi As System.Windows.Forms.TextBox
    Public WithEvents txtExtGLo As System.Windows.Forms.TextBox
    Public WithEvents txtValueLo As System.Windows.Forms.TextBox
    Public WithEvents txtExtGHi As System.Windows.Forms.TextBox
    Public WithEvents txtDelayHiHi As System.Windows.Forms.TextBox
    Public WithEvents txtDelayHi As System.Windows.Forms.TextBox
    Public WithEvents txtGRep2HiHi As System.Windows.Forms.TextBox
    Public WithEvents txtExtGHiHi As System.Windows.Forms.TextBox
    Public WithEvents txtValueHi As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1HiHi As System.Windows.Forms.TextBox
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label129 As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents txtStatusDo5 As System.Windows.Forms.TextBox
    Public WithEvents txtStatusDo4 As System.Windows.Forms.TextBox
    Public WithEvents txtStatusDo3 As System.Windows.Forms.TextBox
    Public WithEvents txtStatusDo2 As System.Windows.Forms.TextBox
    Public WithEvents txtStatusDo1 As System.Windows.Forms.TextBox
    Public WithEvents txtPinAo As System.Windows.Forms.TextBox
    Public WithEvents txtPortNoAo As System.Windows.Forms.TextBox
    Public WithEvents txtFuNoAo As System.Windows.Forms.TextBox
    Public WithEvents txtPinAi As System.Windows.Forms.TextBox
    Public WithEvents txtPortNoAi As System.Windows.Forms.TextBox
    Public WithEvents txtFuNoAi As System.Windows.Forms.TextBox
    Public WithEvents txtPinDo As System.Windows.Forms.TextBox
    Public WithEvents txtPortNoDo As System.Windows.Forms.TextBox
    Public WithEvents txtFuNoDo As System.Windows.Forms.TextBox
    Public WithEvents txtPinDi As System.Windows.Forms.TextBox
    Public WithEvents txtPortNoDi As System.Windows.Forms.TextBox
    Public WithEvents txtFuNoDi As System.Windows.Forms.TextBox
    Public WithEvents lblDo5 As System.Windows.Forms.Label
    Public WithEvents lblDo4 As System.Windows.Forms.Label
    Public WithEvents lblDo3 As System.Windows.Forms.Label
    Public WithEvents lblDo2 As System.Windows.Forms.Label
    Public WithEvents lblDi1 As System.Windows.Forms.Label
    Public WithEvents lblDo1 As System.Windows.Forms.Label
    Public WithEvents lblDi2 As System.Windows.Forms.Label
    Public WithEvents lblDi5 As System.Windows.Forms.Label
    Public WithEvents lblDi3 As System.Windows.Forms.Label
    Public WithEvents lblDi4 As System.Windows.Forms.Label
    Public WithEvents chkStatusAlarm As System.Windows.Forms.CheckBox
    Public WithEvents lblDiStart As System.Windows.Forms.Label
    Public WithEvents txtAlarmTimeup As System.Windows.Forms.TextBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblAoTerminal As System.Windows.Forms.Label
    Public WithEvents lblDoStart As System.Windows.Forms.Label
    Public WithEvents lblAiTerminal As System.Windows.Forms.Label
    Public WithEvents txtStatusOut As System.Windows.Forms.TextBox
    Public WithEvents cmbStatusOut As System.Windows.Forms.ComboBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents txtStatusIn As System.Windows.Forms.TextBox
    Public WithEvents cmbStatusIn As System.Windows.Forms.ComboBox
    Public WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents cmbExtDevice As System.Windows.Forms.ComboBox
    Public WithEvents txtBitCount As System.Windows.Forms.TextBox
    Public WithEvents lblBitCount As System.Windows.Forms.Label
    Friend WithEvents cmbDataType As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents fraHiHi0 As System.Windows.Forms.GroupBox
    Public WithEvents lblStatus As System.Windows.Forms.Label
    Public WithEvents txtStatusFa As System.Windows.Forms.TextBox
    Public WithEvents txtGRep2Fa As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1Fa As System.Windows.Forms.TextBox
    Public WithEvents txtExtGFa As System.Windows.Forms.TextBox
    Public WithEvents txtDelayFa As System.Windows.Forms.TextBox
    Public WithEvents Label128 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents fraComposite As System.Windows.Forms.GroupBox
    Public WithEvents cmdSelect As System.Windows.Forms.Button
    Public WithEvents txtCompositeIndex As System.Windows.Forms.TextBox
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents txtPulseWidth As System.Windows.Forms.TextBox
    Public WithEvents lblPulseWidth As System.Windows.Forms.Label
    Public WithEvents lblControlType As System.Windows.Forms.Label
    Public WithEvents cmbControlType As System.Windows.Forms.ComboBox
    Friend WithEvents fraFeAlarmInfo2 As System.Windows.Forms.Panel
    Public WithEvents txtVar As System.Windows.Forms.TextBox
    Public WithEvents txtSt As System.Windows.Forms.TextBox
    Public WithEvents txtHys2 As System.Windows.Forms.TextBox
    Public WithEvents txtHys1 As System.Windows.Forms.TextBox
    Public WithEvents txtSp2 As System.Windows.Forms.TextBox
    Public WithEvents txtSp1 As System.Windows.Forms.TextBox
    Public WithEvents lblVar As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents txtUnit As System.Windows.Forms.TextBox
    Public WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Public WithEvents Label47 As System.Windows.Forms.Label
    Public WithEvents txtStatusDo8 As System.Windows.Forms.TextBox
    Public WithEvents txtStatusDo7 As System.Windows.Forms.TextBox
    Public WithEvents txtStatusDo6 As System.Windows.Forms.TextBox
    Public WithEvents lblDo8 As System.Windows.Forms.Label
    Public WithEvents lblDo7 As System.Windows.Forms.Label
    Public WithEvents lblDo6 As System.Windows.Forms.Label
    Public WithEvents lblDi8 As System.Windows.Forms.Label
    Public WithEvents lblDi6 As System.Windows.Forms.Label
    Public WithEvents lblDi7 As System.Windows.Forms.Label
    Public WithEvents lblBitSet As System.Windows.Forms.Label
    Friend WithEvents cmdCompositeEdit As System.Windows.Forms.Button
    Friend WithEvents lblDummy As System.Windows.Forms.Label
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents txtTagNo As System.Windows.Forms.TextBox
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents cmbAlmLvl As System.Windows.Forms.ComboBox
    Public WithEvents txtAlmMimic As System.Windows.Forms.TextBox
    Public WithEvents Label45 As System.Windows.Forms.Label
#End Region

End Class
