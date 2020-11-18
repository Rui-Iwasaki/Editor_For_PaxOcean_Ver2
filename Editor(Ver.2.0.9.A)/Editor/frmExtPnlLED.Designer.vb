<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExtPnlLED
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

    Public WithEvents optUnman As System.Windows.Forms.RadioButton
    Public WithEvents optManUnman As System.Windows.Forms.RadioButton
    Public WithEvents optNone As System.Windows.Forms.RadioButton
    Public WithEvents optMan As System.Windows.Forms.RadioButton
    Public WithEvents fraWatchLed As System.Windows.Forms.Panel
    Public WithEvents optDutyOnly As System.Windows.Forms.RadioButton
    Public WithEvents optALL As System.Windows.Forms.RadioButton
    Public WithEvents fraDutyBzStop As System.Windows.Forms.Panel
    Public WithEvents cmbDutyNo As System.Windows.Forms.ComboBox
    Public WithEvents cmbEngineerNo As System.Windows.Forms.ComboBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblID As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.fraWatchLed = New System.Windows.Forms.Panel()
        Me.optUnman = New System.Windows.Forms.RadioButton()
        Me.optManUnman = New System.Windows.Forms.RadioButton()
        Me.optNone = New System.Windows.Forms.RadioButton()
        Me.optMan = New System.Windows.Forms.RadioButton()
        Me.fraDutyBzStop = New System.Windows.Forms.Panel()
        Me.optDutyOnly = New System.Windows.Forms.RadioButton()
        Me.optALL = New System.Windows.Forms.RadioButton()
        Me.cmbDutyNo = New System.Windows.Forms.ComboBox()
        Me.cmbEngineerNo = New System.Windows.Forms.ComboBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblID = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.optLED13 = New System.Windows.Forms.RadioButton()
        Me.optLED12 = New System.Windows.Forms.RadioButton()
        Me.optLED11 = New System.Windows.Forms.RadioButton()
        Me.optLED10 = New System.Windows.Forms.RadioButton()
        Me.optLED9 = New System.Windows.Forms.RadioButton()
        Me.optLED8 = New System.Windows.Forms.RadioButton()
        Me.optLED7 = New System.Windows.Forms.RadioButton()
        Me.optLED6 = New System.Windows.Forms.RadioButton()
        Me.optLED5 = New System.Windows.Forms.RadioButton()
        Me.optLED4 = New System.Windows.Forms.RadioButton()
        Me.optLED3 = New System.Windows.Forms.RadioButton()
        Me.optLED2 = New System.Windows.Forms.RadioButton()
        Me.optLED1 = New System.Windows.Forms.RadioButton()
        Me.optLED0 = New System.Windows.Forms.RadioButton()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblLED16_2 = New System.Windows.Forms.Label()
        Me.lblLED16_1 = New System.Windows.Forms.Label()
        Me.lblLED15_2 = New System.Windows.Forms.Label()
        Me.lblLED15_1 = New System.Windows.Forms.Label()
        Me.lblLED14_2 = New System.Windows.Forms.Label()
        Me.lblLED14_1 = New System.Windows.Forms.Label()
        Me.lblLED13_2 = New System.Windows.Forms.Label()
        Me.lblLED13_1 = New System.Windows.Forms.Label()
        Me.lblLED12_2 = New System.Windows.Forms.Label()
        Me.lblLED12_1 = New System.Windows.Forms.Label()
        Me.lblLED11_2 = New System.Windows.Forms.Label()
        Me.lblLED11_1 = New System.Windows.Forms.Label()
        Me.lblLED10_2 = New System.Windows.Forms.Label()
        Me.lblLED10_1 = New System.Windows.Forms.Label()
        Me.lblLED9_2 = New System.Windows.Forms.Label()
        Me.lblLED9_1 = New System.Windows.Forms.Label()
        Me.lblLED8_1 = New System.Windows.Forms.Label()
        Me.lblLED7_1 = New System.Windows.Forms.Label()
        Me.lblLED6_1 = New System.Windows.Forms.Label()
        Me.lblLED5_1 = New System.Windows.Forms.Label()
        Me.lblLED4_1 = New System.Windows.Forms.Label()
        Me.lblLED3_1 = New System.Windows.Forms.Label()
        Me.lblLED2_1 = New System.Windows.Forms.Label()
        Me.lblLED1_1 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblIdNo = New System.Windows.Forms.Label()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.txtWatchLed = New System.Windows.Forms.TextBox()
        Me.fraWatchLed.SuspendLayout()
        Me.fraDutyBzStop.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'fraWatchLed
        '
        Me.fraWatchLed.BackColor = System.Drawing.SystemColors.Control
        Me.fraWatchLed.Controls.Add(Me.optUnman)
        Me.fraWatchLed.Controls.Add(Me.optManUnman)
        Me.fraWatchLed.Controls.Add(Me.optNone)
        Me.fraWatchLed.Controls.Add(Me.optMan)
        Me.fraWatchLed.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraWatchLed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraWatchLed.Location = New System.Drawing.Point(23, 278)
        Me.fraWatchLed.Name = "fraWatchLed"
        Me.fraWatchLed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraWatchLed.Size = New System.Drawing.Size(105, 113)
        Me.fraWatchLed.TabIndex = 55
        '
        'optUnman
        '
        Me.optUnman.AutoSize = True
        Me.optUnman.BackColor = System.Drawing.SystemColors.Control
        Me.optUnman.Cursor = System.Windows.Forms.Cursors.Default
        Me.optUnman.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optUnman.Location = New System.Drawing.Point(4, 36)
        Me.optUnman.Name = "optUnman"
        Me.optUnman.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optUnman.Size = New System.Drawing.Size(83, 16)
        Me.optUnman.TabIndex = 59
        Me.optUnman.TabStop = True
        Me.optUnman.Text = "UNMAN Only"
        Me.optUnman.UseVisualStyleBackColor = True
        '
        'optManUnman
        '
        Me.optManUnman.AutoSize = True
        Me.optManUnman.BackColor = System.Drawing.SystemColors.Control
        Me.optManUnman.Cursor = System.Windows.Forms.Cursors.Default
        Me.optManUnman.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optManUnman.Location = New System.Drawing.Point(4, 12)
        Me.optManUnman.Name = "optManUnman"
        Me.optManUnman.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optManUnman.Size = New System.Drawing.Size(83, 16)
        Me.optManUnman.TabIndex = 58
        Me.optManUnman.TabStop = True
        Me.optManUnman.Text = "MAN, UNMAN"
        Me.optManUnman.UseVisualStyleBackColor = True
        '
        'optNone
        '
        Me.optNone.AutoSize = True
        Me.optNone.BackColor = System.Drawing.SystemColors.Control
        Me.optNone.Cursor = System.Windows.Forms.Cursors.Default
        Me.optNone.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optNone.Location = New System.Drawing.Point(4, 84)
        Me.optNone.Name = "optNone"
        Me.optNone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optNone.Size = New System.Drawing.Size(47, 16)
        Me.optNone.TabIndex = 57
        Me.optNone.TabStop = True
        Me.optNone.Text = "None"
        Me.optNone.UseVisualStyleBackColor = True
        '
        'optMan
        '
        Me.optMan.AutoSize = True
        Me.optMan.BackColor = System.Drawing.SystemColors.Control
        Me.optMan.Cursor = System.Windows.Forms.Cursors.Default
        Me.optMan.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMan.Location = New System.Drawing.Point(4, 60)
        Me.optMan.Name = "optMan"
        Me.optMan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optMan.Size = New System.Drawing.Size(71, 16)
        Me.optMan.TabIndex = 56
        Me.optMan.TabStop = True
        Me.optMan.Text = "MAN Only"
        Me.optMan.UseVisualStyleBackColor = True
        '
        'fraDutyBzStop
        '
        Me.fraDutyBzStop.BackColor = System.Drawing.SystemColors.Control
        Me.fraDutyBzStop.Controls.Add(Me.optDutyOnly)
        Me.fraDutyBzStop.Controls.Add(Me.optALL)
        Me.fraDutyBzStop.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraDutyBzStop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDutyBzStop.Location = New System.Drawing.Point(24, 186)
        Me.fraDutyBzStop.Name = "fraDutyBzStop"
        Me.fraDutyBzStop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDutyBzStop.Size = New System.Drawing.Size(89, 68)
        Me.fraDutyBzStop.TabIndex = 52
        '
        'optDutyOnly
        '
        Me.optDutyOnly.AutoSize = True
        Me.optDutyOnly.BackColor = System.Drawing.SystemColors.Control
        Me.optDutyOnly.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDutyOnly.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDutyOnly.Location = New System.Drawing.Point(4, 36)
        Me.optDutyOnly.Name = "optDutyOnly"
        Me.optDutyOnly.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDutyOnly.Size = New System.Drawing.Size(77, 16)
        Me.optDutyOnly.TabIndex = 54
        Me.optDutyOnly.TabStop = True
        Me.optDutyOnly.Text = "Duty Only"
        Me.optDutyOnly.UseVisualStyleBackColor = True
        '
        'optALL
        '
        Me.optALL.AutoSize = True
        Me.optALL.BackColor = System.Drawing.SystemColors.Control
        Me.optALL.Cursor = System.Windows.Forms.Cursors.Default
        Me.optALL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optALL.Location = New System.Drawing.Point(4, 12)
        Me.optALL.Name = "optALL"
        Me.optALL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optALL.Size = New System.Drawing.Size(41, 16)
        Me.optALL.TabIndex = 53
        Me.optALL.TabStop = True
        Me.optALL.Text = "All"
        Me.optALL.UseVisualStyleBackColor = True
        '
        'cmbDutyNo
        '
        Me.cmbDutyNo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDutyNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDutyNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDutyNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDutyNo.Location = New System.Drawing.Point(28, 120)
        Me.cmbDutyNo.Name = "cmbDutyNo"
        Me.cmbDutyNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDutyNo.Size = New System.Drawing.Size(57, 20)
        Me.cmbDutyNo.TabIndex = 9
        '
        'cmbEngineerNo
        '
        Me.cmbEngineerNo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbEngineerNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbEngineerNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEngineerNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbEngineerNo.Location = New System.Drawing.Point(28, 64)
        Me.cmbEngineerNo.Name = "cmbEngineerNo"
        Me.cmbEngineerNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbEngineerNo.Size = New System.Drawing.Size(57, 20)
        Me.cmbEngineerNo.TabIndex = 8
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(748, 416)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(113, 33)
        Me.cmdOk.TabIndex = 1
        Me.cmdOk.Text = "OK"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(872, 416)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 0
        Me.cmdExit.Text = "Cancel"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(14, 262)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(59, 12)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Watch LED"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(14, 170)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(77, 12)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "Duty BZ Stop"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(14, 101)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(77, 12)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Duty No. Set"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(14, 44)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(77, 12)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Engineer No."
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.BackColor = System.Drawing.SystemColors.Control
        Me.lblID.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblID.Location = New System.Drawing.Point(14, 12)
        Me.lblID.Name = "lblID"
        Me.lblID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblID.Size = New System.Drawing.Size(23, 12)
        Me.lblID.TabIndex = 2
        Me.lblID.Text = "ID:"
        Me.lblID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label86)
        Me.Panel1.Controls.Add(Me.Label87)
        Me.Panel1.Controls.Add(Me.Label88)
        Me.Panel1.Controls.Add(Me.Label89)
        Me.Panel1.Controls.Add(Me.Label80)
        Me.Panel1.Controls.Add(Me.Label81)
        Me.Panel1.Controls.Add(Me.Label82)
        Me.Panel1.Controls.Add(Me.Label83)
        Me.Panel1.Controls.Add(Me.Label84)
        Me.Panel1.Controls.Add(Me.Label85)
        Me.Panel1.Controls.Add(Me.Label76)
        Me.Panel1.Controls.Add(Me.Label77)
        Me.Panel1.Controls.Add(Me.Label78)
        Me.Panel1.Controls.Add(Me.Label79)
        Me.Panel1.Controls.Add(Me.Label68)
        Me.Panel1.Controls.Add(Me.Label69)
        Me.Panel1.Controls.Add(Me.Label70)
        Me.Panel1.Controls.Add(Me.Label71)
        Me.Panel1.Controls.Add(Me.Label72)
        Me.Panel1.Controls.Add(Me.Label73)
        Me.Panel1.Controls.Add(Me.Label74)
        Me.Panel1.Controls.Add(Me.Label75)
        Me.Panel1.Controls.Add(Me.Label61)
        Me.Panel1.Controls.Add(Me.Label62)
        Me.Panel1.Controls.Add(Me.Label63)
        Me.Panel1.Controls.Add(Me.Label64)
        Me.Panel1.Controls.Add(Me.Label65)
        Me.Panel1.Controls.Add(Me.Label66)
        Me.Panel1.Controls.Add(Me.Label67)
        Me.Panel1.Controls.Add(Me.Label60)
        Me.Panel1.Controls.Add(Me.Label51)
        Me.Panel1.Controls.Add(Me.Label52)
        Me.Panel1.Controls.Add(Me.Label53)
        Me.Panel1.Controls.Add(Me.Label54)
        Me.Panel1.Controls.Add(Me.Label55)
        Me.Panel1.Controls.Add(Me.Label56)
        Me.Panel1.Controls.Add(Me.Label57)
        Me.Panel1.Controls.Add(Me.Label58)
        Me.Panel1.Controls.Add(Me.Label59)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.Label27)
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Controls.Add(Me.Label30)
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.Label32)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.Label34)
        Me.Panel1.Controls.Add(Me.Label35)
        Me.Panel1.Controls.Add(Me.Label36)
        Me.Panel1.Controls.Add(Me.Label37)
        Me.Panel1.Controls.Add(Me.Label38)
        Me.Panel1.Controls.Add(Me.Label39)
        Me.Panel1.Controls.Add(Me.Label40)
        Me.Panel1.Controls.Add(Me.Label41)
        Me.Panel1.Controls.Add(Me.Label42)
        Me.Panel1.Controls.Add(Me.Label43)
        Me.Panel1.Controls.Add(Me.Label45)
        Me.Panel1.Controls.Add(Me.Label46)
        Me.Panel1.Controls.Add(Me.Label47)
        Me.Panel1.Controls.Add(Me.Label48)
        Me.Panel1.Controls.Add(Me.Label49)
        Me.Panel1.Controls.Add(Me.Label50)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.optLED13)
        Me.Panel1.Controls.Add(Me.optLED12)
        Me.Panel1.Controls.Add(Me.optLED11)
        Me.Panel1.Controls.Add(Me.optLED10)
        Me.Panel1.Controls.Add(Me.optLED9)
        Me.Panel1.Controls.Add(Me.optLED8)
        Me.Panel1.Controls.Add(Me.optLED7)
        Me.Panel1.Controls.Add(Me.optLED6)
        Me.Panel1.Controls.Add(Me.optLED5)
        Me.Panel1.Controls.Add(Me.optLED4)
        Me.Panel1.Controls.Add(Me.optLED3)
        Me.Panel1.Controls.Add(Me.optLED2)
        Me.Panel1.Controls.Add(Me.optLED1)
        Me.Panel1.Controls.Add(Me.optLED0)
        Me.Panel1.Controls.Add(Me.Label44)
        Me.Panel1.Controls.Add(Me.lblLED16_2)
        Me.Panel1.Controls.Add(Me.lblLED16_1)
        Me.Panel1.Controls.Add(Me.lblLED15_2)
        Me.Panel1.Controls.Add(Me.lblLED15_1)
        Me.Panel1.Controls.Add(Me.lblLED14_2)
        Me.Panel1.Controls.Add(Me.lblLED14_1)
        Me.Panel1.Controls.Add(Me.lblLED13_2)
        Me.Panel1.Controls.Add(Me.lblLED13_1)
        Me.Panel1.Controls.Add(Me.lblLED12_2)
        Me.Panel1.Controls.Add(Me.lblLED12_1)
        Me.Panel1.Controls.Add(Me.lblLED11_2)
        Me.Panel1.Controls.Add(Me.lblLED11_1)
        Me.Panel1.Controls.Add(Me.lblLED10_2)
        Me.Panel1.Controls.Add(Me.lblLED10_1)
        Me.Panel1.Controls.Add(Me.lblLED9_2)
        Me.Panel1.Controls.Add(Me.lblLED9_1)
        Me.Panel1.Controls.Add(Me.lblLED8_1)
        Me.Panel1.Controls.Add(Me.lblLED7_1)
        Me.Panel1.Controls.Add(Me.lblLED6_1)
        Me.Panel1.Controls.Add(Me.lblLED5_1)
        Me.Panel1.Controls.Add(Me.lblLED4_1)
        Me.Panel1.Controls.Add(Me.lblLED3_1)
        Me.Panel1.Controls.Add(Me.lblLED2_1)
        Me.Panel1.Controls.Add(Me.lblLED1_1)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(136, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(860, 400)
        Me.Panel1.TabIndex = 56
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.White
        Me.Label86.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label86.Location = New System.Drawing.Point(793, 309)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(57, 59)
        Me.Label86.TabIndex = 122
        Me.Label86.Text = "CALL ENABLE OFF"
        Me.Label86.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.PowderBlue
        Me.Label87.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label87.Location = New System.Drawing.Point(737, 309)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(57, 59)
        Me.Label87.TabIndex = 121
        Me.Label87.Text = "CALL ENABLE ON"
        Me.Label87.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label88
        '
        Me.Label88.BackColor = System.Drawing.Color.White
        Me.Label88.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label88.Location = New System.Drawing.Point(681, 309)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(57, 59)
        Me.Label88.TabIndex = 120
        Me.Label88.Text = "CALL ENABLE OFF"
        Me.Label88.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.PowderBlue
        Me.Label89.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label89.Location = New System.Drawing.Point(625, 309)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(57, 59)
        Me.Label89.TabIndex = 119
        Me.Label89.Text = "CALL ENABLE ON"
        Me.Label89.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.Gainsboro
        Me.Label80.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label80.Location = New System.Drawing.Point(737, 290)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(113, 20)
        Me.Label80.TabIndex = 118
        Me.Label80.Text = "LIGHT OFF"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.Beige
        Me.Label81.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label81.Location = New System.Drawing.Point(625, 290)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(113, 20)
        Me.Label81.TabIndex = 117
        Me.Label81.Text = "DUTY 3"
        Me.Label81.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.Gainsboro
        Me.Label82.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label82.Location = New System.Drawing.Point(737, 271)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(113, 20)
        Me.Label82.TabIndex = 116
        Me.Label82.Text = "LIGHT OFF"
        Me.Label82.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.Beige
        Me.Label83.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label83.Location = New System.Drawing.Point(625, 271)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(113, 20)
        Me.Label83.TabIndex = 115
        Me.Label83.Text = "DUTY 2"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.Beige
        Me.Label84.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label84.Location = New System.Drawing.Point(737, 252)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(113, 20)
        Me.Label84.TabIndex = 114
        Me.Label84.Text = "DUTY 1-15"
        Me.Label84.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.Beige
        Me.Label85.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label85.Location = New System.Drawing.Point(625, 252)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(113, 20)
        Me.Label85.TabIndex = 113
        Me.Label85.Text = "DUTY 1"
        Me.Label85.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label76.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label76.Location = New System.Drawing.Point(625, 233)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(225, 20)
        Me.Label76.TabIndex = 112
        Me.Label76.Text = "GROUP ALARM 12"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label77.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label77.Location = New System.Drawing.Point(625, 214)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(225, 20)
        Me.Label77.TabIndex = 111
        Me.Label77.Text = "GROUP ALARM 11"
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label78.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label78.Location = New System.Drawing.Point(625, 195)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(225, 20)
        Me.Label78.TabIndex = 110
        Me.Label78.Text = "GROUP ALARM 10"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label79.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label79.Location = New System.Drawing.Point(625, 176)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(225, 20)
        Me.Label79.TabIndex = 109
        Me.Label79.Text = "GROUP ALARM 9"
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label68.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label68.Location = New System.Drawing.Point(625, 157)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(225, 20)
        Me.Label68.TabIndex = 108
        Me.Label68.Text = "GROUP ALARM 8"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label69.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label69.Location = New System.Drawing.Point(625, 138)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(225, 20)
        Me.Label69.TabIndex = 107
        Me.Label69.Text = "GROUP ALARM 7"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label70.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label70.Location = New System.Drawing.Point(625, 119)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(225, 20)
        Me.Label70.TabIndex = 106
        Me.Label70.Text = "GROUP ALARM 6"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label71.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label71.Location = New System.Drawing.Point(625, 100)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(225, 20)
        Me.Label71.TabIndex = 105
        Me.Label71.Text = "GROUP ALARM 5"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label72.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label72.Location = New System.Drawing.Point(625, 81)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(225, 20)
        Me.Label72.TabIndex = 104
        Me.Label72.Text = "GROUP ALARM 4"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label73.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label73.Location = New System.Drawing.Point(625, 62)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(225, 20)
        Me.Label73.TabIndex = 103
        Me.Label73.Text = "GROUP ALARM 3"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label74.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label74.Location = New System.Drawing.Point(625, 43)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(225, 20)
        Me.Label74.TabIndex = 102
        Me.Label74.Text = "GROUP ALARM 2"
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label75.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label75.Location = New System.Drawing.Point(625, 24)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(225, 20)
        Me.Label75.TabIndex = 101
        Me.Label75.Text = "GROUP ALARM 1"
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label61.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label61.Location = New System.Drawing.Point(511, 157)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(113, 20)
        Me.Label61.TabIndex = 100
        Me.Label61.Text = "GROUP ALARM 8"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label62.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label62.Location = New System.Drawing.Point(511, 138)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(113, 20)
        Me.Label62.TabIndex = 99
        Me.Label62.Text = "GROUP ALARM 7"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label63.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label63.Location = New System.Drawing.Point(511, 119)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(113, 20)
        Me.Label63.TabIndex = 98
        Me.Label63.Text = "GROUP ALARM 6"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label64.Location = New System.Drawing.Point(511, 100)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(113, 20)
        Me.Label64.TabIndex = 97
        Me.Label64.Text = "GROUP ALARM 5"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label65.Location = New System.Drawing.Point(511, 81)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(113, 20)
        Me.Label65.TabIndex = 96
        Me.Label65.Text = "GROUP ALARM 4"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label66.Location = New System.Drawing.Point(511, 62)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(113, 20)
        Me.Label66.TabIndex = 95
        Me.Label66.Text = "GROUP ALARM 3"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label67.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label67.Location = New System.Drawing.Point(511, 43)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(113, 20)
        Me.Label67.TabIndex = 94
        Me.Label67.Text = "GROUP ALARM 2"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label60.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label60.Location = New System.Drawing.Point(511, 24)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(113, 20)
        Me.Label60.TabIndex = 93
        Me.Label60.Text = "GROUP ALARM 1"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.White
        Me.Label51.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label51.Location = New System.Drawing.Point(567, 309)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(57, 59)
        Me.Label51.TabIndex = 92
        Me.Label51.Text = "CALL ENABLE OFF"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.PowderBlue
        Me.Label52.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label52.Location = New System.Drawing.Point(511, 309)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(57, 59)
        Me.Label52.TabIndex = 91
        Me.Label52.Text = "CALL ENABLE ON"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.Gainsboro
        Me.Label53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label53.Location = New System.Drawing.Point(511, 290)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(113, 20)
        Me.Label53.TabIndex = 90
        Me.Label53.Text = "LIGHT OFF"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.Gainsboro
        Me.Label54.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label54.Location = New System.Drawing.Point(511, 271)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(113, 20)
        Me.Label54.TabIndex = 89
        Me.Label54.Text = "LIGHT OFF"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.Gainsboro
        Me.Label55.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label55.Location = New System.Drawing.Point(511, 252)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(113, 20)
        Me.Label55.TabIndex = 88
        Me.Label55.Text = "LIGHT OFF"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.Gainsboro
        Me.Label56.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label56.Location = New System.Drawing.Point(511, 233)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(113, 20)
        Me.Label56.TabIndex = 87
        Me.Label56.Text = "LIGHT OFF"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.Gainsboro
        Me.Label57.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label57.Location = New System.Drawing.Point(511, 214)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(113, 20)
        Me.Label57.TabIndex = 86
        Me.Label57.Text = "LIGHT OFF"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.Gainsboro
        Me.Label58.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label58.Location = New System.Drawing.Point(511, 195)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(113, 20)
        Me.Label58.TabIndex = 85
        Me.Label58.Text = "LIGHT OFF"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.Beige
        Me.Label59.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label59.Location = New System.Drawing.Point(511, 176)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(113, 20)
        Me.Label59.TabIndex = 84
        Me.Label59.Text = "DUTY 1-15"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.White
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label24.Location = New System.Drawing.Point(453, 309)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(57, 59)
        Me.Label24.TabIndex = 83
        Me.Label24.Text = "CALL ENABLE OFF"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.PowderBlue
        Me.Label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label25.Location = New System.Drawing.Point(397, 309)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(57, 59)
        Me.Label25.TabIndex = 82
        Me.Label25.Text = "CALL ENABLE ON"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.Location = New System.Drawing.Point(341, 309)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(57, 59)
        Me.Label26.TabIndex = 81
        Me.Label26.Text = "CALL ENABLE OFF"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.PowderBlue
        Me.Label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label27.Location = New System.Drawing.Point(285, 309)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(57, 59)
        Me.Label27.TabIndex = 80
        Me.Label27.Text = "CALL ENABLE ON"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Gainsboro
        Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label28.Location = New System.Drawing.Point(397, 290)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(113, 20)
        Me.Label28.TabIndex = 79
        Me.Label28.Text = "LIGHT OFF"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.Beige
        Me.Label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label29.Location = New System.Drawing.Point(285, 290)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(113, 20)
        Me.Label29.TabIndex = 78
        Me.Label29.Text = "DUTY 7"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Gainsboro
        Me.Label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label30.Location = New System.Drawing.Point(397, 271)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(113, 20)
        Me.Label30.TabIndex = 77
        Me.Label30.Text = "LIGHT OFF"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.Beige
        Me.Label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label31.Location = New System.Drawing.Point(285, 271)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(113, 20)
        Me.Label31.TabIndex = 76
        Me.Label31.Text = "DUTY 6"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Gainsboro
        Me.Label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label32.Location = New System.Drawing.Point(397, 252)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(113, 20)
        Me.Label32.TabIndex = 75
        Me.Label32.Text = "LIGHT OFF"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.Beige
        Me.Label33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label33.Location = New System.Drawing.Point(285, 252)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(113, 20)
        Me.Label33.TabIndex = 74
        Me.Label33.Text = "DUTY 5"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Gainsboro
        Me.Label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label34.Location = New System.Drawing.Point(397, 233)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(113, 20)
        Me.Label34.TabIndex = 73
        Me.Label34.Text = "LIGHT OFF"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.Beige
        Me.Label35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label35.Location = New System.Drawing.Point(285, 233)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(113, 20)
        Me.Label35.TabIndex = 72
        Me.Label35.Text = "DUTY 4"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.Gainsboro
        Me.Label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label36.Location = New System.Drawing.Point(397, 214)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(113, 20)
        Me.Label36.TabIndex = 71
        Me.Label36.Text = "LIGHT OFF"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Beige
        Me.Label37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label37.Location = New System.Drawing.Point(285, 214)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(113, 20)
        Me.Label37.TabIndex = 70
        Me.Label37.Text = "DUTY 3"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.Gainsboro
        Me.Label38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label38.Location = New System.Drawing.Point(397, 195)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(113, 20)
        Me.Label38.TabIndex = 69
        Me.Label38.Text = "LIGHT OFF"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.Beige
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Location = New System.Drawing.Point(285, 195)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(113, 20)
        Me.Label39.TabIndex = 68
        Me.Label39.Text = "DUTY 2"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.Beige
        Me.Label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label40.Location = New System.Drawing.Point(397, 176)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(113, 20)
        Me.Label40.TabIndex = 67
        Me.Label40.Text = "DUTY 1-7"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.Beige
        Me.Label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label41.Location = New System.Drawing.Point(285, 176)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(113, 20)
        Me.Label41.TabIndex = 66
        Me.Label41.Text = "DUTY 1"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.Gainsboro
        Me.Label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label42.Location = New System.Drawing.Point(285, 157)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(225, 20)
        Me.Label42.TabIndex = 65
        Me.Label42.Text = "LIGHT OFF"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.Gainsboro
        Me.Label43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label43.Location = New System.Drawing.Point(285, 138)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(225, 20)
        Me.Label43.TabIndex = 64
        Me.Label43.Text = "LIGHT OFF"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.Gainsboro
        Me.Label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label45.Location = New System.Drawing.Point(285, 119)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(225, 20)
        Me.Label45.TabIndex = 63
        Me.Label45.Text = "LIGHT OFF"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.Gainsboro
        Me.Label46.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label46.Location = New System.Drawing.Point(285, 100)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(225, 20)
        Me.Label46.TabIndex = 62
        Me.Label46.Text = "LIGHT OFF"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.Gainsboro
        Me.Label47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label47.Location = New System.Drawing.Point(285, 81)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(225, 20)
        Me.Label47.TabIndex = 61
        Me.Label47.Text = "LIGHT OFF"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.Gainsboro
        Me.Label48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label48.Location = New System.Drawing.Point(285, 62)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(225, 20)
        Me.Label48.TabIndex = 60
        Me.Label48.Text = "LIGHT OFF"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.Gainsboro
        Me.Label49.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label49.Location = New System.Drawing.Point(285, 43)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(225, 20)
        Me.Label49.TabIndex = 59
        Me.Label49.Text = "LIGHT OFF"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label50.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label50.Location = New System.Drawing.Point(285, 24)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(225, 20)
        Me.Label50.TabIndex = 58
        Me.Label50.Text = "GROUP ALARM 1 - 8"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.White
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Location = New System.Drawing.Point(227, 309)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(57, 59)
        Me.Label22.TabIndex = 57
        Me.Label22.Text = "CALL ENABLE OFF"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.PowderBlue
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label23.Location = New System.Drawing.Point(171, 309)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(57, 59)
        Me.Label23.TabIndex = 56
        Me.Label23.Text = "CALL ENABLE ON"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Silver
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label20.Location = New System.Drawing.Point(59, 5)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(791, 20)
        Me.Label20.TabIndex = 55
        Me.Label20.Text = "LED PATTERN"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Silver
        Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label21.Location = New System.Drawing.Point(8, 5)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(52, 20)
        Me.Label21.TabIndex = 54
        Me.Label21.Text = "LED"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'optLED13
        '
        Me.optLED13.AutoSize = True
        Me.optLED13.BackColor = System.Drawing.SystemColors.Control
        Me.optLED13.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED13.Location = New System.Drawing.Point(806, 372)
        Me.optLED13.Name = "optLED13"
        Me.optLED13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED13.Size = New System.Drawing.Size(35, 16)
        Me.optLED13.TabIndex = 53
        Me.optLED13.TabStop = True
        Me.optLED13.Text = "13"
        Me.optLED13.UseVisualStyleBackColor = True
        '
        'optLED12
        '
        Me.optLED12.AutoSize = True
        Me.optLED12.BackColor = System.Drawing.SystemColors.Control
        Me.optLED12.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED12.Location = New System.Drawing.Point(750, 372)
        Me.optLED12.Name = "optLED12"
        Me.optLED12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED12.Size = New System.Drawing.Size(35, 16)
        Me.optLED12.TabIndex = 52
        Me.optLED12.TabStop = True
        Me.optLED12.Text = "12"
        Me.optLED12.UseVisualStyleBackColor = True
        '
        'optLED11
        '
        Me.optLED11.AutoSize = True
        Me.optLED11.BackColor = System.Drawing.SystemColors.Control
        Me.optLED11.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED11.Location = New System.Drawing.Point(694, 372)
        Me.optLED11.Name = "optLED11"
        Me.optLED11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED11.Size = New System.Drawing.Size(35, 16)
        Me.optLED11.TabIndex = 51
        Me.optLED11.TabStop = True
        Me.optLED11.Text = "11"
        Me.optLED11.UseVisualStyleBackColor = True
        '
        'optLED10
        '
        Me.optLED10.AutoSize = True
        Me.optLED10.BackColor = System.Drawing.SystemColors.Control
        Me.optLED10.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED10.Location = New System.Drawing.Point(638, 372)
        Me.optLED10.Name = "optLED10"
        Me.optLED10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED10.Size = New System.Drawing.Size(35, 16)
        Me.optLED10.TabIndex = 50
        Me.optLED10.TabStop = True
        Me.optLED10.Text = "10"
        Me.optLED10.UseVisualStyleBackColor = True
        '
        'optLED9
        '
        Me.optLED9.AutoSize = True
        Me.optLED9.BackColor = System.Drawing.SystemColors.Control
        Me.optLED9.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED9.Location = New System.Drawing.Point(583, 372)
        Me.optLED9.Name = "optLED9"
        Me.optLED9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED9.Size = New System.Drawing.Size(29, 16)
        Me.optLED9.TabIndex = 49
        Me.optLED9.TabStop = True
        Me.optLED9.Text = "9"
        Me.optLED9.UseVisualStyleBackColor = True
        '
        'optLED8
        '
        Me.optLED8.AutoSize = True
        Me.optLED8.BackColor = System.Drawing.SystemColors.Control
        Me.optLED8.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED8.Location = New System.Drawing.Point(527, 372)
        Me.optLED8.Name = "optLED8"
        Me.optLED8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED8.Size = New System.Drawing.Size(29, 16)
        Me.optLED8.TabIndex = 48
        Me.optLED8.TabStop = True
        Me.optLED8.Text = "8"
        Me.optLED8.UseVisualStyleBackColor = True
        '
        'optLED7
        '
        Me.optLED7.AutoSize = True
        Me.optLED7.BackColor = System.Drawing.SystemColors.Control
        Me.optLED7.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED7.Location = New System.Drawing.Point(467, 372)
        Me.optLED7.Name = "optLED7"
        Me.optLED7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED7.Size = New System.Drawing.Size(29, 16)
        Me.optLED7.TabIndex = 47
        Me.optLED7.TabStop = True
        Me.optLED7.Text = "7"
        Me.optLED7.UseVisualStyleBackColor = True
        '
        'optLED6
        '
        Me.optLED6.AutoSize = True
        Me.optLED6.BackColor = System.Drawing.SystemColors.Control
        Me.optLED6.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED6.Location = New System.Drawing.Point(415, 372)
        Me.optLED6.Name = "optLED6"
        Me.optLED6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED6.Size = New System.Drawing.Size(29, 16)
        Me.optLED6.TabIndex = 46
        Me.optLED6.TabStop = True
        Me.optLED6.Text = "6"
        Me.optLED6.UseVisualStyleBackColor = True
        '
        'optLED5
        '
        Me.optLED5.AutoSize = True
        Me.optLED5.BackColor = System.Drawing.SystemColors.Control
        Me.optLED5.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED5.Location = New System.Drawing.Point(355, 372)
        Me.optLED5.Name = "optLED5"
        Me.optLED5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED5.Size = New System.Drawing.Size(29, 16)
        Me.optLED5.TabIndex = 45
        Me.optLED5.TabStop = True
        Me.optLED5.Text = "5"
        Me.optLED5.UseVisualStyleBackColor = True
        '
        'optLED4
        '
        Me.optLED4.AutoSize = True
        Me.optLED4.BackColor = System.Drawing.SystemColors.Control
        Me.optLED4.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED4.Location = New System.Drawing.Point(302, 372)
        Me.optLED4.Name = "optLED4"
        Me.optLED4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED4.Size = New System.Drawing.Size(29, 16)
        Me.optLED4.TabIndex = 44
        Me.optLED4.TabStop = True
        Me.optLED4.Text = "4"
        Me.optLED4.UseVisualStyleBackColor = True
        '
        'optLED3
        '
        Me.optLED3.AutoSize = True
        Me.optLED3.BackColor = System.Drawing.SystemColors.Control
        Me.optLED3.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED3.Location = New System.Drawing.Point(243, 372)
        Me.optLED3.Name = "optLED3"
        Me.optLED3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED3.Size = New System.Drawing.Size(29, 16)
        Me.optLED3.TabIndex = 43
        Me.optLED3.TabStop = True
        Me.optLED3.Text = "3"
        Me.optLED3.UseVisualStyleBackColor = True
        '
        'optLED2
        '
        Me.optLED2.AutoSize = True
        Me.optLED2.BackColor = System.Drawing.SystemColors.Control
        Me.optLED2.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED2.Location = New System.Drawing.Point(187, 372)
        Me.optLED2.Name = "optLED2"
        Me.optLED2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED2.Size = New System.Drawing.Size(29, 16)
        Me.optLED2.TabIndex = 42
        Me.optLED2.TabStop = True
        Me.optLED2.Text = "2"
        Me.optLED2.UseVisualStyleBackColor = True
        '
        'optLED1
        '
        Me.optLED1.AutoSize = True
        Me.optLED1.BackColor = System.Drawing.SystemColors.Control
        Me.optLED1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED1.Location = New System.Drawing.Point(131, 372)
        Me.optLED1.Name = "optLED1"
        Me.optLED1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED1.Size = New System.Drawing.Size(29, 16)
        Me.optLED1.TabIndex = 41
        Me.optLED1.TabStop = True
        Me.optLED1.Text = "1"
        Me.optLED1.UseVisualStyleBackColor = True
        '
        'optLED0
        '
        Me.optLED0.AutoSize = True
        Me.optLED0.BackColor = System.Drawing.SystemColors.Control
        Me.optLED0.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLED0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLED0.Location = New System.Drawing.Point(75, 372)
        Me.optLED0.Name = "optLED0"
        Me.optLED0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLED0.Size = New System.Drawing.Size(29, 16)
        Me.optLED0.TabIndex = 40
        Me.optLED0.TabStop = True
        Me.optLED0.Text = "0"
        Me.optLED0.UseVisualStyleBackColor = True
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.Silver
        Me.Label44.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label44.Location = New System.Drawing.Point(8, 309)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(52, 59)
        Me.Label44.TabIndex = 39
        Me.Label44.Text = "LED16 ENG'R CALL"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED16_2
        '
        Me.lblLED16_2.BackColor = System.Drawing.Color.White
        Me.lblLED16_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED16_2.Location = New System.Drawing.Point(115, 309)
        Me.lblLED16_2.Name = "lblLED16_2"
        Me.lblLED16_2.Size = New System.Drawing.Size(57, 59)
        Me.lblLED16_2.TabIndex = 38
        Me.lblLED16_2.Text = "CALL ENABLE OFF"
        Me.lblLED16_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED16_1
        '
        Me.lblLED16_1.BackColor = System.Drawing.Color.PowderBlue
        Me.lblLED16_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED16_1.Location = New System.Drawing.Point(59, 309)
        Me.lblLED16_1.Name = "lblLED16_1"
        Me.lblLED16_1.Size = New System.Drawing.Size(57, 59)
        Me.lblLED16_1.TabIndex = 37
        Me.lblLED16_1.Text = "CALL ENABLE ON"
        Me.lblLED16_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED15_2
        '
        Me.lblLED15_2.BackColor = System.Drawing.Color.Gainsboro
        Me.lblLED15_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED15_2.Location = New System.Drawing.Point(171, 290)
        Me.lblLED15_2.Name = "lblLED15_2"
        Me.lblLED15_2.Size = New System.Drawing.Size(113, 20)
        Me.lblLED15_2.TabIndex = 36
        Me.lblLED15_2.Text = "LIGHT OFF"
        Me.lblLED15_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED15_1
        '
        Me.lblLED15_1.BackColor = System.Drawing.Color.Beige
        Me.lblLED15_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED15_1.Location = New System.Drawing.Point(59, 290)
        Me.lblLED15_1.Name = "lblLED15_1"
        Me.lblLED15_1.Size = New System.Drawing.Size(113, 20)
        Me.lblLED15_1.TabIndex = 35
        Me.lblLED15_1.Text = "DUTY 7"
        Me.lblLED15_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED14_2
        '
        Me.lblLED14_2.BackColor = System.Drawing.Color.Gainsboro
        Me.lblLED14_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED14_2.Location = New System.Drawing.Point(171, 271)
        Me.lblLED14_2.Name = "lblLED14_2"
        Me.lblLED14_2.Size = New System.Drawing.Size(113, 20)
        Me.lblLED14_2.TabIndex = 34
        Me.lblLED14_2.Text = "LIGHT OFF"
        Me.lblLED14_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED14_1
        '
        Me.lblLED14_1.BackColor = System.Drawing.Color.Beige
        Me.lblLED14_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED14_1.Location = New System.Drawing.Point(59, 271)
        Me.lblLED14_1.Name = "lblLED14_1"
        Me.lblLED14_1.Size = New System.Drawing.Size(113, 20)
        Me.lblLED14_1.TabIndex = 33
        Me.lblLED14_1.Text = "DUTY 6"
        Me.lblLED14_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED13_2
        '
        Me.lblLED13_2.BackColor = System.Drawing.Color.Gainsboro
        Me.lblLED13_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED13_2.Location = New System.Drawing.Point(171, 252)
        Me.lblLED13_2.Name = "lblLED13_2"
        Me.lblLED13_2.Size = New System.Drawing.Size(113, 20)
        Me.lblLED13_2.TabIndex = 32
        Me.lblLED13_2.Text = "LIGHT OFF"
        Me.lblLED13_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED13_1
        '
        Me.lblLED13_1.BackColor = System.Drawing.Color.Beige
        Me.lblLED13_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED13_1.Location = New System.Drawing.Point(59, 252)
        Me.lblLED13_1.Name = "lblLED13_1"
        Me.lblLED13_1.Size = New System.Drawing.Size(113, 20)
        Me.lblLED13_1.TabIndex = 31
        Me.lblLED13_1.Text = "DUTY 5"
        Me.lblLED13_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED12_2
        '
        Me.lblLED12_2.BackColor = System.Drawing.Color.Gainsboro
        Me.lblLED12_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED12_2.Location = New System.Drawing.Point(171, 233)
        Me.lblLED12_2.Name = "lblLED12_2"
        Me.lblLED12_2.Size = New System.Drawing.Size(113, 20)
        Me.lblLED12_2.TabIndex = 30
        Me.lblLED12_2.Text = "LIGHT OFF"
        Me.lblLED12_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED12_1
        '
        Me.lblLED12_1.BackColor = System.Drawing.Color.Beige
        Me.lblLED12_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED12_1.Location = New System.Drawing.Point(59, 233)
        Me.lblLED12_1.Name = "lblLED12_1"
        Me.lblLED12_1.Size = New System.Drawing.Size(113, 20)
        Me.lblLED12_1.TabIndex = 29
        Me.lblLED12_1.Text = "DUTY 4"
        Me.lblLED12_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED11_2
        '
        Me.lblLED11_2.BackColor = System.Drawing.Color.Gainsboro
        Me.lblLED11_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED11_2.Location = New System.Drawing.Point(171, 214)
        Me.lblLED11_2.Name = "lblLED11_2"
        Me.lblLED11_2.Size = New System.Drawing.Size(113, 20)
        Me.lblLED11_2.TabIndex = 28
        Me.lblLED11_2.Text = "LIGHT OFF"
        Me.lblLED11_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED11_1
        '
        Me.lblLED11_1.BackColor = System.Drawing.Color.Beige
        Me.lblLED11_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED11_1.Location = New System.Drawing.Point(59, 214)
        Me.lblLED11_1.Name = "lblLED11_1"
        Me.lblLED11_1.Size = New System.Drawing.Size(113, 20)
        Me.lblLED11_1.TabIndex = 27
        Me.lblLED11_1.Text = "DUTY 3"
        Me.lblLED11_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED10_2
        '
        Me.lblLED10_2.BackColor = System.Drawing.Color.Gainsboro
        Me.lblLED10_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED10_2.Location = New System.Drawing.Point(171, 195)
        Me.lblLED10_2.Name = "lblLED10_2"
        Me.lblLED10_2.Size = New System.Drawing.Size(113, 20)
        Me.lblLED10_2.TabIndex = 26
        Me.lblLED10_2.Text = "LIGHT OFF"
        Me.lblLED10_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED10_1
        '
        Me.lblLED10_1.BackColor = System.Drawing.Color.Beige
        Me.lblLED10_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED10_1.Location = New System.Drawing.Point(59, 195)
        Me.lblLED10_1.Name = "lblLED10_1"
        Me.lblLED10_1.Size = New System.Drawing.Size(113, 20)
        Me.lblLED10_1.TabIndex = 25
        Me.lblLED10_1.Text = "DUTY 2"
        Me.lblLED10_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED9_2
        '
        Me.lblLED9_2.BackColor = System.Drawing.Color.Beige
        Me.lblLED9_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED9_2.Location = New System.Drawing.Point(171, 176)
        Me.lblLED9_2.Name = "lblLED9_2"
        Me.lblLED9_2.Size = New System.Drawing.Size(113, 20)
        Me.lblLED9_2.TabIndex = 24
        Me.lblLED9_2.Text = "DUTY 1-7"
        Me.lblLED9_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED9_1
        '
        Me.lblLED9_1.BackColor = System.Drawing.Color.Beige
        Me.lblLED9_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED9_1.Location = New System.Drawing.Point(59, 176)
        Me.lblLED9_1.Name = "lblLED9_1"
        Me.lblLED9_1.Size = New System.Drawing.Size(113, 20)
        Me.lblLED9_1.TabIndex = 23
        Me.lblLED9_1.Text = "DUTY 1"
        Me.lblLED9_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED8_1
        '
        Me.lblLED8_1.BackColor = System.Drawing.Color.LavenderBlush
        Me.lblLED8_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED8_1.Location = New System.Drawing.Point(59, 157)
        Me.lblLED8_1.Name = "lblLED8_1"
        Me.lblLED8_1.Size = New System.Drawing.Size(225, 20)
        Me.lblLED8_1.TabIndex = 22
        Me.lblLED8_1.Text = "GROUP ALARM 8"
        Me.lblLED8_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED7_1
        '
        Me.lblLED7_1.BackColor = System.Drawing.Color.LavenderBlush
        Me.lblLED7_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED7_1.Location = New System.Drawing.Point(59, 138)
        Me.lblLED7_1.Name = "lblLED7_1"
        Me.lblLED7_1.Size = New System.Drawing.Size(225, 20)
        Me.lblLED7_1.TabIndex = 21
        Me.lblLED7_1.Text = "GROUP ALARM 7"
        Me.lblLED7_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED6_1
        '
        Me.lblLED6_1.BackColor = System.Drawing.Color.LavenderBlush
        Me.lblLED6_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED6_1.Location = New System.Drawing.Point(59, 119)
        Me.lblLED6_1.Name = "lblLED6_1"
        Me.lblLED6_1.Size = New System.Drawing.Size(225, 20)
        Me.lblLED6_1.TabIndex = 20
        Me.lblLED6_1.Text = "GROUP ALARM 6"
        Me.lblLED6_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED5_1
        '
        Me.lblLED5_1.BackColor = System.Drawing.Color.LavenderBlush
        Me.lblLED5_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED5_1.Location = New System.Drawing.Point(59, 100)
        Me.lblLED5_1.Name = "lblLED5_1"
        Me.lblLED5_1.Size = New System.Drawing.Size(225, 20)
        Me.lblLED5_1.TabIndex = 19
        Me.lblLED5_1.Text = "GROUP ALARM 5"
        Me.lblLED5_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED4_1
        '
        Me.lblLED4_1.BackColor = System.Drawing.Color.LavenderBlush
        Me.lblLED4_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED4_1.Location = New System.Drawing.Point(59, 81)
        Me.lblLED4_1.Name = "lblLED4_1"
        Me.lblLED4_1.Size = New System.Drawing.Size(225, 20)
        Me.lblLED4_1.TabIndex = 18
        Me.lblLED4_1.Text = "GROUP ALARM 4"
        Me.lblLED4_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED3_1
        '
        Me.lblLED3_1.BackColor = System.Drawing.Color.LavenderBlush
        Me.lblLED3_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED3_1.Location = New System.Drawing.Point(59, 62)
        Me.lblLED3_1.Name = "lblLED3_1"
        Me.lblLED3_1.Size = New System.Drawing.Size(225, 20)
        Me.lblLED3_1.TabIndex = 17
        Me.lblLED3_1.Text = "GROUP ALARM 3"
        Me.lblLED3_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED2_1
        '
        Me.lblLED2_1.BackColor = System.Drawing.Color.LavenderBlush
        Me.lblLED2_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED2_1.Location = New System.Drawing.Point(59, 43)
        Me.lblLED2_1.Name = "lblLED2_1"
        Me.lblLED2_1.Size = New System.Drawing.Size(225, 20)
        Me.lblLED2_1.TabIndex = 16
        Me.lblLED2_1.Text = "GROUP ALARM 2"
        Me.lblLED2_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLED1_1
        '
        Me.lblLED1_1.BackColor = System.Drawing.Color.LavenderBlush
        Me.lblLED1_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLED1_1.Location = New System.Drawing.Point(59, 24)
        Me.lblLED1_1.Name = "lblLED1_1"
        Me.lblLED1_1.Size = New System.Drawing.Size(225, 20)
        Me.lblLED1_1.TabIndex = 15
        Me.lblLED1_1.Text = "GROUP ALARM 1"
        Me.lblLED1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Silver
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Location = New System.Drawing.Point(8, 290)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(52, 20)
        Me.Label19.TabIndex = 14
        Me.Label19.Text = "LED15"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Silver
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Location = New System.Drawing.Point(8, 271)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(52, 20)
        Me.Label18.TabIndex = 13
        Me.Label18.Text = "LED14"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Silver
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Location = New System.Drawing.Point(8, 252)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(52, 20)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "LED13"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Silver
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Location = New System.Drawing.Point(8, 233)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(52, 20)
        Me.Label16.TabIndex = 11
        Me.Label16.Text = "LED12"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Silver
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Location = New System.Drawing.Point(8, 214)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(52, 20)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "LED11"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Silver
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Location = New System.Drawing.Point(8, 195)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(52, 20)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "LED10"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Silver
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Location = New System.Drawing.Point(8, 176)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 20)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "LED9"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Silver
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Location = New System.Drawing.Point(8, 157)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 20)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "LED8"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Silver
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(8, 138)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 20)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "LED7"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Silver
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(8, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 20)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "LED6"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Silver
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(8, 100)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "LED5"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Silver
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(8, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 20)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "LED4"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Silver
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(8, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "LED3"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Silver
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(8, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "LED2"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Silver
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "LED1"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIdNo
        '
        Me.lblIdNo.AutoSize = True
        Me.lblIdNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblIdNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIdNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIdNo.Location = New System.Drawing.Point(34, 12)
        Me.lblIdNo.Name = "lblIdNo"
        Me.lblIdNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIdNo.Size = New System.Drawing.Size(17, 12)
        Me.lblIdNo.TabIndex = 57
        Me.lblIdNo.Text = "11"
        Me.lblIdNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(23, 416)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(113, 33)
        Me.cmdPrint.TabIndex = 58
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'txtWatchLed
        '
        Me.txtWatchLed.Location = New System.Drawing.Point(23, 278)
        Me.txtWatchLed.MaxLength = 1
        Me.txtWatchLed.Name = "txtWatchLed"
        Me.txtWatchLed.Size = New System.Drawing.Size(104, 19)
        Me.txtWatchLed.TabIndex = 59
        Me.txtWatchLed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmExtPnlLED
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(1016, 459)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtWatchLed)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.lblIdNo)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.fraWatchLed)
        Me.Controls.Add(Me.fraDutyBzStop)
        Me.Controls.Add(Me.cmbDutyNo)
        Me.Controls.Add(Me.cmbEngineerNo)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblID)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExtPnlLED"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "EXT PANEL LED PATTERN"
        Me.fraWatchLed.ResumeLayout(False)
        Me.fraWatchLed.PerformLayout()
        Me.fraDutyBzStop.ResumeLayout(False)
        Me.fraDutyBzStop.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblLED1_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED9_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED8_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED7_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED6_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED5_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED4_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED3_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED2_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED9_2 As System.Windows.Forms.Label
    Friend WithEvents lblLED15_2 As System.Windows.Forms.Label
    Friend WithEvents lblLED15_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED14_2 As System.Windows.Forms.Label
    Friend WithEvents lblLED14_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED13_2 As System.Windows.Forms.Label
    Friend WithEvents lblLED13_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED12_2 As System.Windows.Forms.Label
    Friend WithEvents lblLED12_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED11_2 As System.Windows.Forms.Label
    Friend WithEvents lblLED11_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED10_2 As System.Windows.Forms.Label
    Friend WithEvents lblLED10_1 As System.Windows.Forms.Label
    Friend WithEvents lblLED16_2 As System.Windows.Forms.Label
    Friend WithEvents lblLED16_1 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents optLED13 As System.Windows.Forms.RadioButton
    Public WithEvents optLED12 As System.Windows.Forms.RadioButton
    Public WithEvents optLED11 As System.Windows.Forms.RadioButton
    Public WithEvents optLED10 As System.Windows.Forms.RadioButton
    Public WithEvents optLED9 As System.Windows.Forms.RadioButton
    Public WithEvents optLED8 As System.Windows.Forms.RadioButton
    Public WithEvents optLED7 As System.Windows.Forms.RadioButton
    Public WithEvents optLED6 As System.Windows.Forms.RadioButton
    Public WithEvents optLED5 As System.Windows.Forms.RadioButton
    Public WithEvents optLED4 As System.Windows.Forms.RadioButton
    Public WithEvents optLED3 As System.Windows.Forms.RadioButton
    Public WithEvents optLED2 As System.Windows.Forms.RadioButton
    Public WithEvents optLED1 As System.Windows.Forms.RadioButton
    Public WithEvents optLED0 As System.Windows.Forms.RadioButton
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Public WithEvents lblIdNo As System.Windows.Forms.Label
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents txtWatchLed As System.Windows.Forms.TextBox
#End Region

End Class
