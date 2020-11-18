<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSeqSetInputData_GAI
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

    Public WithEvents optDataLow As System.Windows.Forms.RadioButton
    Public WithEvents optDataHigh As System.Windows.Forms.RadioButton
    Public WithEvents chkRunSelect4 As System.Windows.Forms.CheckBox
    Public WithEvents chkRunSelect3 As System.Windows.Forms.CheckBox
    Public WithEvents chkRunSelect2 As System.Windows.Forms.CheckBox
    Public WithEvents chkRunSelect1 As System.Windows.Forms.CheckBox
    Public WithEvents fraDataMotorRunSelect As System.Windows.Forms.GroupBox
    Public WithEvents optDataMotorStBy As System.Windows.Forms.RadioButton
    Public WithEvents optDataMotorRun As System.Windows.Forms.RadioButton
    Public WithEvents fraDataMotorSelect As System.Windows.Forms.GroupBox
    Public WithEvents optDataAnalog As System.Windows.Forms.RadioButton
    Public WithEvents optDataDigital As System.Windows.Forms.RadioButton
    Public WithEvents optDataPulse As System.Windows.Forms.RadioButton
    Public WithEvents optDataRunning As System.Windows.Forms.RadioButton
    Public WithEvents optDataMotor As System.Windows.Forms.RadioButton
    Public WithEvents fraData As System.Windows.Forms.GroupBox
    Public WithEvents optDataExtGroup As System.Windows.Forms.RadioButton
    Public WithEvents optDataCalc As System.Windows.Forms.RadioButton
    Public WithEvents optDataManual As System.Windows.Forms.RadioButton
    Public WithEvents optDataAlarm As System.Windows.Forms.RadioButton
    Public WithEvents optDataData As System.Windows.Forms.RadioButton
    Public WithEvents optTypeOneShot As System.Windows.Forms.RadioButton
    Public WithEvents optTypeNonInvert As System.Windows.Forms.RadioButton
    Public WithEvents optTypeInver As System.Windows.Forms.RadioButton
    Public WithEvents fraInputType As System.Windows.Forms.GroupBox
    Public WithEvents fraInputDataSelect As System.Windows.Forms.GroupBox
    Public WithEvents txtData As System.Windows.Forms.TextBox
    Public WithEvents txtInputSetNo As System.Windows.Forms.TextBox
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents lblData As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.fraData = New System.Windows.Forms.GroupBox()
        Me.fraDataCompositeSelect = New System.Windows.Forms.GroupBox()
        Me.chkCompositeSelect8 = New System.Windows.Forms.CheckBox()
        Me.chkCompositeSelect7 = New System.Windows.Forms.CheckBox()
        Me.chkCompositeSelect6 = New System.Windows.Forms.CheckBox()
        Me.chkCompositeSelect5 = New System.Windows.Forms.CheckBox()
        Me.chkCompositeSelect4 = New System.Windows.Forms.CheckBox()
        Me.chkCompositeSelect3 = New System.Windows.Forms.CheckBox()
        Me.chkCompositeSelect2 = New System.Windows.Forms.CheckBox()
        Me.chkCompositeSelect1 = New System.Windows.Forms.CheckBox()
        Me.optDataComposite = New System.Windows.Forms.RadioButton()
        Me.optDataLow = New System.Windows.Forms.RadioButton()
        Me.optDataHigh = New System.Windows.Forms.RadioButton()
        Me.fraDataMotorSelect = New System.Windows.Forms.GroupBox()
        Me.fraDataMotorRunSelect = New System.Windows.Forms.GroupBox()
        Me.chkRunSelect4 = New System.Windows.Forms.CheckBox()
        Me.chkRunSelect3 = New System.Windows.Forms.CheckBox()
        Me.chkRunSelect2 = New System.Windows.Forms.CheckBox()
        Me.chkRunSelect1 = New System.Windows.Forms.CheckBox()
        Me.optDataMotorStBy = New System.Windows.Forms.RadioButton()
        Me.optDataMotorRun = New System.Windows.Forms.RadioButton()
        Me.optDataAnalog = New System.Windows.Forms.RadioButton()
        Me.optDataDigital = New System.Windows.Forms.RadioButton()
        Me.optDataPulse = New System.Windows.Forms.RadioButton()
        Me.optDataRunning = New System.Windows.Forms.RadioButton()
        Me.optDataMotor = New System.Windows.Forms.RadioButton()
        Me.fraInputDataSelect = New System.Windows.Forms.GroupBox()
        Me.optDataFixed = New System.Windows.Forms.RadioButton()
        Me.optDataExtGroup = New System.Windows.Forms.RadioButton()
        Me.optDataCalc = New System.Windows.Forms.RadioButton()
        Me.optDataManual = New System.Windows.Forms.RadioButton()
        Me.optDataAlarm = New System.Windows.Forms.RadioButton()
        Me.optDataData = New System.Windows.Forms.RadioButton()
        Me.fraInputType = New System.Windows.Forms.GroupBox()
        Me.optTypeOneShot = New System.Windows.Forms.RadioButton()
        Me.optTypeNonInvert = New System.Windows.Forms.RadioButton()
        Me.optTypeInver = New System.Windows.Forms.RadioButton()
        Me.txtData = New System.Windows.Forms.TextBox()
        Me.txtInputSetNo = New System.Windows.Forms.TextBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.lblData = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.fraExtGroup = New System.Windows.Forms.GroupBox()
        Me.txtExtInputMask = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.optExtBzOut = New System.Windows.Forms.RadioButton()
        Me.opExtGroupOut = New System.Windows.Forms.RadioButton()
        Me.fraManual = New System.Windows.Forms.GroupBox()
        Me.txtManualInputReferenceStatus = New System.Windows.Forms.TextBox()
        Me.txtManualInputInputMask = New System.Windows.Forms.TextBox()
        Me.txtManualInputInputType = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.fraCalc = New System.Windows.Forms.GroupBox()
        Me.optOpeOutput6 = New System.Windows.Forms.RadioButton()
        Me.optOpeOutput5 = New System.Windows.Forms.RadioButton()
        Me.optOpeOutput4 = New System.Windows.Forms.RadioButton()
        Me.optOpeOutput3 = New System.Windows.Forms.RadioButton()
        Me.optOpeOutput2 = New System.Windows.Forms.RadioButton()
        Me.optOpeOutput1 = New System.Windows.Forms.RadioButton()
        Me.fraAlarm = New System.Windows.Forms.GroupBox()
        Me.fraAlarmCompositeSelect = New System.Windows.Forms.GroupBox()
        Me.chkAlarmCompositeSelectFB = New System.Windows.Forms.CheckBox()
        Me.chkAlarmCompositeSelect9 = New System.Windows.Forms.CheckBox()
        Me.chkAlarmCompositeSelect8 = New System.Windows.Forms.CheckBox()
        Me.chkAlarmCompositeSelect7 = New System.Windows.Forms.CheckBox()
        Me.chkAlarmCompositeSelect6 = New System.Windows.Forms.CheckBox()
        Me.chkAlarmCompositeSelect5 = New System.Windows.Forms.CheckBox()
        Me.chkAlarmCompositeSelect4 = New System.Windows.Forms.CheckBox()
        Me.chkAlarmCompositeSelect3 = New System.Windows.Forms.CheckBox()
        Me.chkAlarmCompositeSelect2 = New System.Windows.Forms.CheckBox()
        Me.chkAlarmCompositeSelect1 = New System.Windows.Forms.CheckBox()
        Me.optAlarmComposite = New System.Windows.Forms.RadioButton()
        Me.optAlarmMotor = New System.Windows.Forms.RadioButton()
        Me.optAlarmDigital = New System.Windows.Forms.RadioButton()
        Me.optAlarmAnalog = New System.Windows.Forms.RadioButton()
        Me.fraAlarmAnalogSelect = New System.Windows.Forms.GroupBox()
        Me.fraAlarmAnalogAlarmSelect = New System.Windows.Forms.GroupBox()
        Me.chkAlarmSelectLoLo = New System.Windows.Forms.CheckBox()
        Me.chkAlarmSelectLo = New System.Windows.Forms.CheckBox()
        Me.chkAlarmSelectHi = New System.Windows.Forms.CheckBox()
        Me.chkAlarmSelectHiHI = New System.Windows.Forms.CheckBox()
        Me.optAlarmAnalogAnalog = New System.Windows.Forms.RadioButton()
        Me.optAlarmAnalogSensor = New System.Windows.Forms.RadioButton()
        Me.optAlarmHigh = New System.Windows.Forms.RadioButton()
        Me.optAlarmLow = New System.Windows.Forms.RadioButton()
        Me.pnlIO = New System.Windows.Forms.Panel()
        Me.optIOSelOutput = New System.Windows.Forms.RadioButton()
        Me.optIOSelInput = New System.Windows.Forms.RadioButton()
        Me.fraData.SuspendLayout()
        Me.fraDataCompositeSelect.SuspendLayout()
        Me.fraDataMotorSelect.SuspendLayout()
        Me.fraDataMotorRunSelect.SuspendLayout()
        Me.fraInputDataSelect.SuspendLayout()
        Me.fraInputType.SuspendLayout()
        Me.fraExtGroup.SuspendLayout()
        Me.fraManual.SuspendLayout()
        Me.fraCalc.SuspendLayout()
        Me.fraAlarm.SuspendLayout()
        Me.fraAlarmCompositeSelect.SuspendLayout()
        Me.fraAlarmAnalogSelect.SuspendLayout()
        Me.fraAlarmAnalogAlarmSelect.SuspendLayout()
        Me.pnlIO.SuspendLayout()
        Me.SuspendLayout()
        '
        'fraData
        '
        Me.fraData.BackColor = System.Drawing.SystemColors.Control
        Me.fraData.Controls.Add(Me.fraDataCompositeSelect)
        Me.fraData.Controls.Add(Me.optDataComposite)
        Me.fraData.Controls.Add(Me.optDataLow)
        Me.fraData.Controls.Add(Me.optDataHigh)
        Me.fraData.Controls.Add(Me.fraDataMotorSelect)
        Me.fraData.Controls.Add(Me.optDataAnalog)
        Me.fraData.Controls.Add(Me.optDataDigital)
        Me.fraData.Controls.Add(Me.optDataPulse)
        Me.fraData.Controls.Add(Me.optDataRunning)
        Me.fraData.Controls.Add(Me.optDataMotor)
        Me.fraData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraData.Location = New System.Drawing.Point(332, 80)
        Me.fraData.Name = "fraData"
        Me.fraData.Padding = New System.Windows.Forms.Padding(0)
        Me.fraData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraData.Size = New System.Drawing.Size(372, 467)
        Me.fraData.TabIndex = 16
        Me.fraData.TabStop = False
        Me.fraData.Text = "DATA"
        '
        'fraDataCompositeSelect
        '
        Me.fraDataCompositeSelect.BackColor = System.Drawing.SystemColors.Control
        Me.fraDataCompositeSelect.Controls.Add(Me.chkCompositeSelect8)
        Me.fraDataCompositeSelect.Controls.Add(Me.chkCompositeSelect7)
        Me.fraDataCompositeSelect.Controls.Add(Me.chkCompositeSelect6)
        Me.fraDataCompositeSelect.Controls.Add(Me.chkCompositeSelect5)
        Me.fraDataCompositeSelect.Controls.Add(Me.chkCompositeSelect4)
        Me.fraDataCompositeSelect.Controls.Add(Me.chkCompositeSelect3)
        Me.fraDataCompositeSelect.Controls.Add(Me.chkCompositeSelect2)
        Me.fraDataCompositeSelect.Controls.Add(Me.chkCompositeSelect1)
        Me.fraDataCompositeSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDataCompositeSelect.Location = New System.Drawing.Point(46, 326)
        Me.fraDataCompositeSelect.Name = "fraDataCompositeSelect"
        Me.fraDataCompositeSelect.Padding = New System.Windows.Forms.Padding(0)
        Me.fraDataCompositeSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDataCompositeSelect.Size = New System.Drawing.Size(310, 77)
        Me.fraDataCompositeSelect.TabIndex = 33
        Me.fraDataCompositeSelect.TabStop = False
        Me.fraDataCompositeSelect.Text = "COMPOSITE SELECT"
        '
        'chkCompositeSelect8
        '
        Me.chkCompositeSelect8.AutoSize = True
        Me.chkCompositeSelect8.BackColor = System.Drawing.SystemColors.Control
        Me.chkCompositeSelect8.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCompositeSelect8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCompositeSelect8.Location = New System.Drawing.Point(227, 47)
        Me.chkCompositeSelect8.Name = "chkCompositeSelect8"
        Me.chkCompositeSelect8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCompositeSelect8.Size = New System.Drawing.Size(48, 16)
        Me.chkCompositeSelect8.TabIndex = 33
        Me.chkCompositeSelect8.Text = "BIT8"
        Me.chkCompositeSelect8.UseVisualStyleBackColor = True
        '
        'chkCompositeSelect7
        '
        Me.chkCompositeSelect7.AutoSize = True
        Me.chkCompositeSelect7.BackColor = System.Drawing.SystemColors.Control
        Me.chkCompositeSelect7.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCompositeSelect7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCompositeSelect7.Location = New System.Drawing.Point(163, 47)
        Me.chkCompositeSelect7.Name = "chkCompositeSelect7"
        Me.chkCompositeSelect7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCompositeSelect7.Size = New System.Drawing.Size(48, 16)
        Me.chkCompositeSelect7.TabIndex = 32
        Me.chkCompositeSelect7.Text = "BIT7"
        Me.chkCompositeSelect7.UseVisualStyleBackColor = True
        '
        'chkCompositeSelect6
        '
        Me.chkCompositeSelect6.AutoSize = True
        Me.chkCompositeSelect6.BackColor = System.Drawing.SystemColors.Control
        Me.chkCompositeSelect6.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCompositeSelect6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCompositeSelect6.Location = New System.Drawing.Point(99, 47)
        Me.chkCompositeSelect6.Name = "chkCompositeSelect6"
        Me.chkCompositeSelect6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCompositeSelect6.Size = New System.Drawing.Size(48, 16)
        Me.chkCompositeSelect6.TabIndex = 31
        Me.chkCompositeSelect6.Text = "BIT6"
        Me.chkCompositeSelect6.UseVisualStyleBackColor = True
        '
        'chkCompositeSelect5
        '
        Me.chkCompositeSelect5.AutoSize = True
        Me.chkCompositeSelect5.BackColor = System.Drawing.SystemColors.Control
        Me.chkCompositeSelect5.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCompositeSelect5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCompositeSelect5.Location = New System.Drawing.Point(35, 47)
        Me.chkCompositeSelect5.Name = "chkCompositeSelect5"
        Me.chkCompositeSelect5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCompositeSelect5.Size = New System.Drawing.Size(48, 16)
        Me.chkCompositeSelect5.TabIndex = 30
        Me.chkCompositeSelect5.Text = "BIT5"
        Me.chkCompositeSelect5.UseVisualStyleBackColor = True
        '
        'chkCompositeSelect4
        '
        Me.chkCompositeSelect4.AutoSize = True
        Me.chkCompositeSelect4.BackColor = System.Drawing.SystemColors.Control
        Me.chkCompositeSelect4.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCompositeSelect4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCompositeSelect4.Location = New System.Drawing.Point(227, 22)
        Me.chkCompositeSelect4.Name = "chkCompositeSelect4"
        Me.chkCompositeSelect4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCompositeSelect4.Size = New System.Drawing.Size(48, 16)
        Me.chkCompositeSelect4.TabIndex = 29
        Me.chkCompositeSelect4.Text = "BIT4"
        Me.chkCompositeSelect4.UseVisualStyleBackColor = True
        '
        'chkCompositeSelect3
        '
        Me.chkCompositeSelect3.AutoSize = True
        Me.chkCompositeSelect3.BackColor = System.Drawing.SystemColors.Control
        Me.chkCompositeSelect3.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCompositeSelect3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCompositeSelect3.Location = New System.Drawing.Point(163, 22)
        Me.chkCompositeSelect3.Name = "chkCompositeSelect3"
        Me.chkCompositeSelect3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCompositeSelect3.Size = New System.Drawing.Size(48, 16)
        Me.chkCompositeSelect3.TabIndex = 28
        Me.chkCompositeSelect3.Text = "BIT3"
        Me.chkCompositeSelect3.UseVisualStyleBackColor = True
        '
        'chkCompositeSelect2
        '
        Me.chkCompositeSelect2.AutoSize = True
        Me.chkCompositeSelect2.BackColor = System.Drawing.SystemColors.Control
        Me.chkCompositeSelect2.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCompositeSelect2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCompositeSelect2.Location = New System.Drawing.Point(99, 22)
        Me.chkCompositeSelect2.Name = "chkCompositeSelect2"
        Me.chkCompositeSelect2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCompositeSelect2.Size = New System.Drawing.Size(48, 16)
        Me.chkCompositeSelect2.TabIndex = 27
        Me.chkCompositeSelect2.Text = "BIT2"
        Me.chkCompositeSelect2.UseVisualStyleBackColor = True
        '
        'chkCompositeSelect1
        '
        Me.chkCompositeSelect1.AutoSize = True
        Me.chkCompositeSelect1.BackColor = System.Drawing.SystemColors.Control
        Me.chkCompositeSelect1.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCompositeSelect1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCompositeSelect1.Location = New System.Drawing.Point(35, 22)
        Me.chkCompositeSelect1.Name = "chkCompositeSelect1"
        Me.chkCompositeSelect1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCompositeSelect1.Size = New System.Drawing.Size(48, 16)
        Me.chkCompositeSelect1.TabIndex = 26
        Me.chkCompositeSelect1.Text = "BIT1"
        Me.chkCompositeSelect1.UseVisualStyleBackColor = True
        '
        'optDataComposite
        '
        Me.optDataComposite.AutoSize = True
        Me.optDataComposite.BackColor = System.Drawing.SystemColors.Control
        Me.optDataComposite.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataComposite.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataComposite.Location = New System.Drawing.Point(29, 300)
        Me.optDataComposite.Name = "optDataComposite"
        Me.optDataComposite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataComposite.Size = New System.Drawing.Size(77, 16)
        Me.optDataComposite.TabIndex = 32
        Me.optDataComposite.TabStop = True
        Me.optDataComposite.Text = "COMPOSITE"
        Me.optDataComposite.UseVisualStyleBackColor = True
        '
        'optDataLow
        '
        Me.optDataLow.AutoSize = True
        Me.optDataLow.BackColor = System.Drawing.SystemColors.Control
        Me.optDataLow.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataLow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataLow.Location = New System.Drawing.Point(29, 434)
        Me.optDataLow.Name = "optDataLow"
        Me.optDataLow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataLow.Size = New System.Drawing.Size(119, 16)
        Me.optDataLow.TabIndex = 31
        Me.optDataLow.TabStop = True
        Me.optDataLow.Text = "Low Edge Trigger"
        Me.optDataLow.UseVisualStyleBackColor = True
        '
        'optDataHigh
        '
        Me.optDataHigh.AutoSize = True
        Me.optDataHigh.BackColor = System.Drawing.SystemColors.Control
        Me.optDataHigh.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataHigh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataHigh.Location = New System.Drawing.Point(29, 409)
        Me.optDataHigh.Name = "optDataHigh"
        Me.optDataHigh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataHigh.Size = New System.Drawing.Size(125, 16)
        Me.optDataHigh.TabIndex = 30
        Me.optDataHigh.TabStop = True
        Me.optDataHigh.Text = "High Edge Trigger"
        Me.optDataHigh.UseVisualStyleBackColor = True
        '
        'fraDataMotorSelect
        '
        Me.fraDataMotorSelect.BackColor = System.Drawing.SystemColors.Control
        Me.fraDataMotorSelect.Controls.Add(Me.fraDataMotorRunSelect)
        Me.fraDataMotorSelect.Controls.Add(Me.optDataMotorStBy)
        Me.fraDataMotorSelect.Controls.Add(Me.optDataMotorRun)
        Me.fraDataMotorSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDataMotorSelect.Location = New System.Drawing.Point(46, 155)
        Me.fraDataMotorSelect.Name = "fraDataMotorSelect"
        Me.fraDataMotorSelect.Padding = New System.Windows.Forms.Padding(0)
        Me.fraDataMotorSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDataMotorSelect.Size = New System.Drawing.Size(310, 139)
        Me.fraDataMotorSelect.TabIndex = 22
        Me.fraDataMotorSelect.TabStop = False
        Me.fraDataMotorSelect.Text = "MOTOR SELECT"
        '
        'fraDataMotorRunSelect
        '
        Me.fraDataMotorRunSelect.BackColor = System.Drawing.SystemColors.Control
        Me.fraDataMotorRunSelect.Controls.Add(Me.chkRunSelect4)
        Me.fraDataMotorRunSelect.Controls.Add(Me.chkRunSelect3)
        Me.fraDataMotorRunSelect.Controls.Add(Me.chkRunSelect2)
        Me.fraDataMotorRunSelect.Controls.Add(Me.chkRunSelect1)
        Me.fraDataMotorRunSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDataMotorRunSelect.Location = New System.Drawing.Point(36, 48)
        Me.fraDataMotorRunSelect.Name = "fraDataMotorRunSelect"
        Me.fraDataMotorRunSelect.Padding = New System.Windows.Forms.Padding(0)
        Me.fraDataMotorRunSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDataMotorRunSelect.Size = New System.Drawing.Size(264, 49)
        Me.fraDataMotorRunSelect.TabIndex = 25
        Me.fraDataMotorRunSelect.TabStop = False
        Me.fraDataMotorRunSelect.Text = "RUN SELECT"
        '
        'chkRunSelect4
        '
        Me.chkRunSelect4.AutoSize = True
        Me.chkRunSelect4.BackColor = System.Drawing.SystemColors.Control
        Me.chkRunSelect4.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRunSelect4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRunSelect4.Location = New System.Drawing.Point(200, 20)
        Me.chkRunSelect4.Name = "chkRunSelect4"
        Me.chkRunSelect4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRunSelect4.Size = New System.Drawing.Size(48, 16)
        Me.chkRunSelect4.TabIndex = 29
        Me.chkRunSelect4.Text = "RUN4"
        Me.chkRunSelect4.UseVisualStyleBackColor = True
        '
        'chkRunSelect3
        '
        Me.chkRunSelect3.AutoSize = True
        Me.chkRunSelect3.BackColor = System.Drawing.SystemColors.Control
        Me.chkRunSelect3.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRunSelect3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRunSelect3.Location = New System.Drawing.Point(136, 20)
        Me.chkRunSelect3.Name = "chkRunSelect3"
        Me.chkRunSelect3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRunSelect3.Size = New System.Drawing.Size(48, 16)
        Me.chkRunSelect3.TabIndex = 28
        Me.chkRunSelect3.Text = "RUN3"
        Me.chkRunSelect3.UseVisualStyleBackColor = True
        '
        'chkRunSelect2
        '
        Me.chkRunSelect2.AutoSize = True
        Me.chkRunSelect2.BackColor = System.Drawing.SystemColors.Control
        Me.chkRunSelect2.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRunSelect2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRunSelect2.Location = New System.Drawing.Point(72, 20)
        Me.chkRunSelect2.Name = "chkRunSelect2"
        Me.chkRunSelect2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRunSelect2.Size = New System.Drawing.Size(48, 16)
        Me.chkRunSelect2.TabIndex = 27
        Me.chkRunSelect2.Text = "RUN2"
        Me.chkRunSelect2.UseVisualStyleBackColor = True
        '
        'chkRunSelect1
        '
        Me.chkRunSelect1.AutoSize = True
        Me.chkRunSelect1.BackColor = System.Drawing.SystemColors.Control
        Me.chkRunSelect1.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRunSelect1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRunSelect1.Location = New System.Drawing.Point(8, 20)
        Me.chkRunSelect1.Name = "chkRunSelect1"
        Me.chkRunSelect1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRunSelect1.Size = New System.Drawing.Size(48, 16)
        Me.chkRunSelect1.TabIndex = 26
        Me.chkRunSelect1.Text = "RUN1"
        Me.chkRunSelect1.UseVisualStyleBackColor = True
        '
        'optDataMotorStBy
        '
        Me.optDataMotorStBy.AutoSize = True
        Me.optDataMotorStBy.BackColor = System.Drawing.SystemColors.Control
        Me.optDataMotorStBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataMotorStBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataMotorStBy.Location = New System.Drawing.Point(16, 109)
        Me.optDataMotorStBy.Name = "optDataMotorStBy"
        Me.optDataMotorStBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataMotorStBy.Size = New System.Drawing.Size(53, 16)
        Me.optDataMotorStBy.TabIndex = 24
        Me.optDataMotorStBy.Text = "ST/BY"
        Me.optDataMotorStBy.UseVisualStyleBackColor = True
        '
        'optDataMotorRun
        '
        Me.optDataMotorRun.BackColor = System.Drawing.SystemColors.Control
        Me.optDataMotorRun.Checked = True
        Me.optDataMotorRun.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataMotorRun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataMotorRun.Location = New System.Drawing.Point(16, 20)
        Me.optDataMotorRun.Name = "optDataMotorRun"
        Me.optDataMotorRun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataMotorRun.Size = New System.Drawing.Size(77, 21)
        Me.optDataMotorRun.TabIndex = 23
        Me.optDataMotorRun.TabStop = True
        Me.optDataMotorRun.Text = "RUN"
        Me.optDataMotorRun.UseVisualStyleBackColor = True
        '
        'optDataAnalog
        '
        Me.optDataAnalog.AutoSize = True
        Me.optDataAnalog.BackColor = System.Drawing.SystemColors.Control
        Me.optDataAnalog.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataAnalog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataAnalog.Location = New System.Drawing.Point(29, 30)
        Me.optDataAnalog.Name = "optDataAnalog"
        Me.optDataAnalog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataAnalog.Size = New System.Drawing.Size(59, 16)
        Me.optDataAnalog.TabIndex = 21
        Me.optDataAnalog.TabStop = True
        Me.optDataAnalog.Text = "ANALOG"
        Me.optDataAnalog.UseVisualStyleBackColor = True
        '
        'optDataDigital
        '
        Me.optDataDigital.AutoSize = True
        Me.optDataDigital.BackColor = System.Drawing.SystemColors.Control
        Me.optDataDigital.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataDigital.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataDigital.Location = New System.Drawing.Point(29, 55)
        Me.optDataDigital.Name = "optDataDigital"
        Me.optDataDigital.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataDigital.Size = New System.Drawing.Size(65, 16)
        Me.optDataDigital.TabIndex = 20
        Me.optDataDigital.TabStop = True
        Me.optDataDigital.Text = "DIGITAL"
        Me.optDataDigital.UseVisualStyleBackColor = True
        '
        'optDataPulse
        '
        Me.optDataPulse.AutoSize = True
        Me.optDataPulse.BackColor = System.Drawing.SystemColors.Control
        Me.optDataPulse.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataPulse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataPulse.Location = New System.Drawing.Point(29, 80)
        Me.optDataPulse.Name = "optDataPulse"
        Me.optDataPulse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataPulse.Size = New System.Drawing.Size(53, 16)
        Me.optDataPulse.TabIndex = 19
        Me.optDataPulse.TabStop = True
        Me.optDataPulse.Text = "PULSE"
        Me.optDataPulse.UseVisualStyleBackColor = True
        '
        'optDataRunning
        '
        Me.optDataRunning.AutoSize = True
        Me.optDataRunning.BackColor = System.Drawing.SystemColors.Control
        Me.optDataRunning.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataRunning.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataRunning.Location = New System.Drawing.Point(29, 105)
        Me.optDataRunning.Name = "optDataRunning"
        Me.optDataRunning.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataRunning.Size = New System.Drawing.Size(95, 16)
        Me.optDataRunning.TabIndex = 18
        Me.optDataRunning.TabStop = True
        Me.optDataRunning.Text = "RUNNING HOUR"
        Me.optDataRunning.UseVisualStyleBackColor = True
        '
        'optDataMotor
        '
        Me.optDataMotor.AutoSize = True
        Me.optDataMotor.BackColor = System.Drawing.SystemColors.Control
        Me.optDataMotor.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataMotor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataMotor.Location = New System.Drawing.Point(29, 130)
        Me.optDataMotor.Name = "optDataMotor"
        Me.optDataMotor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataMotor.Size = New System.Drawing.Size(53, 16)
        Me.optDataMotor.TabIndex = 17
        Me.optDataMotor.TabStop = True
        Me.optDataMotor.Text = "MOTOR"
        Me.optDataMotor.UseVisualStyleBackColor = True
        '
        'fraInputDataSelect
        '
        Me.fraInputDataSelect.BackColor = System.Drawing.SystemColors.Control
        Me.fraInputDataSelect.Controls.Add(Me.optDataFixed)
        Me.fraInputDataSelect.Controls.Add(Me.optDataExtGroup)
        Me.fraInputDataSelect.Controls.Add(Me.optDataCalc)
        Me.fraInputDataSelect.Controls.Add(Me.optDataManual)
        Me.fraInputDataSelect.Controls.Add(Me.optDataAlarm)
        Me.fraInputDataSelect.Controls.Add(Me.optDataData)
        Me.fraInputDataSelect.Controls.Add(Me.fraInputType)
        Me.fraInputDataSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraInputDataSelect.Location = New System.Drawing.Point(16, 80)
        Me.fraInputDataSelect.Name = "fraInputDataSelect"
        Me.fraInputDataSelect.Padding = New System.Windows.Forms.Padding(0)
        Me.fraInputDataSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraInputDataSelect.Size = New System.Drawing.Size(301, 169)
        Me.fraInputDataSelect.TabIndex = 2
        Me.fraInputDataSelect.TabStop = False
        Me.fraInputDataSelect.Text = "Input Data Select"
        '
        'optDataFixed
        '
        Me.optDataFixed.AutoSize = True
        Me.optDataFixed.BackColor = System.Drawing.SystemColors.Control
        Me.optDataFixed.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataFixed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataFixed.Location = New System.Drawing.Point(150, 155)
        Me.optDataFixed.Name = "optDataFixed"
        Me.optDataFixed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataFixed.Size = New System.Drawing.Size(53, 16)
        Me.optDataFixed.TabIndex = 6
        Me.optDataFixed.TabStop = True
        Me.optDataFixed.Text = "Fixed"
        Me.optDataFixed.UseVisualStyleBackColor = True
        Me.optDataFixed.Visible = False
        '
        'optDataExtGroup
        '
        Me.optDataExtGroup.AutoSize = True
        Me.optDataExtGroup.BackColor = System.Drawing.SystemColors.Control
        Me.optDataExtGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataExtGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataExtGroup.Location = New System.Drawing.Point(143, 63)
        Me.optDataExtGroup.Name = "optDataExtGroup"
        Me.optDataExtGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataExtGroup.Size = New System.Drawing.Size(113, 16)
        Me.optDataExtGroup.TabIndex = 4
        Me.optDataExtGroup.TabStop = True
        Me.optDataExtGroup.Text = "EXT GROUP Input"
        Me.optDataExtGroup.UseVisualStyleBackColor = True
        '
        'optDataCalc
        '
        Me.optDataCalc.AutoSize = True
        Me.optDataCalc.BackColor = System.Drawing.SystemColors.Control
        Me.optDataCalc.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataCalc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataCalc.Location = New System.Drawing.Point(143, 30)
        Me.optDataCalc.Name = "optDataCalc"
        Me.optDataCalc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataCalc.Size = New System.Drawing.Size(125, 16)
        Me.optDataCalc.TabIndex = 3
        Me.optDataCalc.TabStop = True
        Me.optDataCalc.Text = "Calculation Input"
        Me.optDataCalc.UseVisualStyleBackColor = True
        '
        'optDataManual
        '
        Me.optDataManual.AutoSize = True
        Me.optDataManual.BackColor = System.Drawing.SystemColors.Control
        Me.optDataManual.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataManual.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataManual.Location = New System.Drawing.Point(36, 155)
        Me.optDataManual.Name = "optDataManual"
        Me.optDataManual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataManual.Size = New System.Drawing.Size(95, 16)
        Me.optDataManual.TabIndex = 2
        Me.optDataManual.TabStop = True
        Me.optDataManual.Text = "Manual Input"
        Me.optDataManual.UseVisualStyleBackColor = True
        Me.optDataManual.Visible = False
        '
        'optDataAlarm
        '
        Me.optDataAlarm.AutoSize = True
        Me.optDataAlarm.BackColor = System.Drawing.SystemColors.Control
        Me.optDataAlarm.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataAlarm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataAlarm.Location = New System.Drawing.Point(29, 63)
        Me.optDataAlarm.Name = "optDataAlarm"
        Me.optDataAlarm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataAlarm.Size = New System.Drawing.Size(53, 16)
        Me.optDataAlarm.TabIndex = 1
        Me.optDataAlarm.TabStop = True
        Me.optDataAlarm.Text = "ALARM"
        Me.optDataAlarm.UseVisualStyleBackColor = True
        '
        'optDataData
        '
        Me.optDataData.AutoSize = True
        Me.optDataData.BackColor = System.Drawing.SystemColors.Control
        Me.optDataData.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDataData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDataData.Location = New System.Drawing.Point(29, 30)
        Me.optDataData.Name = "optDataData"
        Me.optDataData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDataData.Size = New System.Drawing.Size(47, 16)
        Me.optDataData.TabIndex = 0
        Me.optDataData.TabStop = True
        Me.optDataData.Text = "DATA"
        Me.optDataData.UseVisualStyleBackColor = True
        '
        'fraInputType
        '
        Me.fraInputType.BackColor = System.Drawing.SystemColors.Control
        Me.fraInputType.Controls.Add(Me.optTypeOneShot)
        Me.fraInputType.Controls.Add(Me.optTypeNonInvert)
        Me.fraInputType.Controls.Add(Me.optTypeInver)
        Me.fraInputType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraInputType.Location = New System.Drawing.Point(20, 93)
        Me.fraInputType.Name = "fraInputType"
        Me.fraInputType.Padding = New System.Windows.Forms.Padding(0)
        Me.fraInputType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraInputType.Size = New System.Drawing.Size(274, 56)
        Me.fraInputType.TabIndex = 5
        Me.fraInputType.TabStop = False
        Me.fraInputType.Text = "Input Type"
        '
        'optTypeOneShot
        '
        Me.optTypeOneShot.AutoSize = True
        Me.optTypeOneShot.BackColor = System.Drawing.SystemColors.Control
        Me.optTypeOneShot.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTypeOneShot.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTypeOneShot.Location = New System.Drawing.Point(184, 24)
        Me.optTypeOneShot.Name = "optTypeOneShot"
        Me.optTypeOneShot.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTypeOneShot.Size = New System.Drawing.Size(71, 16)
        Me.optTypeOneShot.TabIndex = 8
        Me.optTypeOneShot.TabStop = True
        Me.optTypeOneShot.Text = "one shot"
        Me.optTypeOneShot.UseVisualStyleBackColor = True
        '
        'optTypeNonInvert
        '
        Me.optTypeNonInvert.AutoSize = True
        Me.optTypeNonInvert.BackColor = System.Drawing.SystemColors.Control
        Me.optTypeNonInvert.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTypeNonInvert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTypeNonInvert.Location = New System.Drawing.Point(15, 24)
        Me.optTypeNonInvert.Name = "optTypeNonInvert"
        Me.optTypeNonInvert.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTypeNonInvert.Size = New System.Drawing.Size(83, 16)
        Me.optTypeNonInvert.TabIndex = 6
        Me.optTypeNonInvert.TabStop = True
        Me.optTypeNonInvert.Text = "non invert"
        Me.optTypeNonInvert.UseVisualStyleBackColor = True
        '
        'optTypeInver
        '
        Me.optTypeInver.AutoSize = True
        Me.optTypeInver.BackColor = System.Drawing.SystemColors.Control
        Me.optTypeInver.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTypeInver.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTypeInver.Location = New System.Drawing.Point(112, 24)
        Me.optTypeInver.Name = "optTypeInver"
        Me.optTypeInver.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTypeInver.Size = New System.Drawing.Size(59, 16)
        Me.optTypeInver.TabIndex = 7
        Me.optTypeInver.TabStop = True
        Me.optTypeInver.Text = "invert"
        Me.optTypeInver.UseVisualStyleBackColor = True
        '
        'txtData
        '
        Me.txtData.AcceptsReturn = True
        Me.txtData.Location = New System.Drawing.Point(98, 47)
        Me.txtData.MaxLength = 0
        Me.txtData.Name = "txtData"
        Me.txtData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtData.Size = New System.Drawing.Size(76, 19)
        Me.txtData.TabIndex = 1
        Me.txtData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInputSetNo
        '
        Me.txtInputSetNo.AcceptsReturn = True
        Me.txtInputSetNo.BackColor = System.Drawing.SystemColors.Control
        Me.txtInputSetNo.Location = New System.Drawing.Point(98, 16)
        Me.txtInputSetNo.MaxLength = 0
        Me.txtInputSetNo.Name = "txtInputSetNo"
        Me.txtInputSetNo.ReadOnly = True
        Me.txtInputSetNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInputSetNo.Size = New System.Drawing.Size(76, 19)
        Me.txtInputSetNo.TabIndex = 0
        Me.txtInputSetNo.TabStop = False
        Me.txtInputSetNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(465, 567)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 3
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(591, 567)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 4
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'lblData
        '
        Me.lblData.AutoSize = True
        Me.lblData.BackColor = System.Drawing.SystemColors.Control
        Me.lblData.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblData.Location = New System.Drawing.Point(50, 50)
        Me.lblData.Name = "lblData"
        Me.lblData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblData.Size = New System.Drawing.Size(35, 12)
        Me.lblData.TabIndex = 5
        Me.lblData.Text = "CH No"
        Me.lblData.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(20, 19)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(77, 12)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Input Set No"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraExtGroup
        '
        Me.fraExtGroup.BackColor = System.Drawing.SystemColors.Control
        Me.fraExtGroup.Controls.Add(Me.txtExtInputMask)
        Me.fraExtGroup.Controls.Add(Me.Label1)
        Me.fraExtGroup.Controls.Add(Me.optExtBzOut)
        Me.fraExtGroup.Controls.Add(Me.opExtGroupOut)
        Me.fraExtGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraExtGroup.Location = New System.Drawing.Point(472, 29)
        Me.fraExtGroup.Name = "fraExtGroup"
        Me.fraExtGroup.Padding = New System.Windows.Forms.Padding(0)
        Me.fraExtGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraExtGroup.Size = New System.Drawing.Size(372, 133)
        Me.fraExtGroup.TabIndex = 58
        Me.fraExtGroup.TabStop = False
        Me.fraExtGroup.Text = "EXT GROUP Input"
        '
        'txtExtInputMask
        '
        Me.txtExtInputMask.AcceptsReturn = True
        Me.txtExtInputMask.Location = New System.Drawing.Point(108, 91)
        Me.txtExtInputMask.MaxLength = 5
        Me.txtExtInputMask.Name = "txtExtInputMask"
        Me.txtExtInputMask.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtInputMask.Size = New System.Drawing.Size(76, 19)
        Me.txtExtInputMask.TabIndex = 63
        Me.txtExtInputMask.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(39, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 12)
        Me.Label1.TabIndex = 62
        Me.Label1.Text = "InputMask"
        '
        'optExtBzOut
        '
        Me.optExtBzOut.AutoSize = True
        Me.optExtBzOut.BackColor = System.Drawing.SystemColors.Control
        Me.optExtBzOut.Cursor = System.Windows.Forms.Cursors.Default
        Me.optExtBzOut.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optExtBzOut.Location = New System.Drawing.Point(38, 65)
        Me.optExtBzOut.Name = "optExtBzOut"
        Me.optExtBzOut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optExtBzOut.Size = New System.Drawing.Size(83, 16)
        Me.optExtBzOut.TabIndex = 61
        Me.optExtBzOut.TabStop = True
        Me.optExtBzOut.Text = "EXT BZ OUT"
        Me.optExtBzOut.UseVisualStyleBackColor = True
        '
        'opExtGroupOut
        '
        Me.opExtGroupOut.AutoSize = True
        Me.opExtGroupOut.BackColor = System.Drawing.SystemColors.Control
        Me.opExtGroupOut.Cursor = System.Windows.Forms.Cursors.Default
        Me.opExtGroupOut.ForeColor = System.Drawing.SystemColors.ControlText
        Me.opExtGroupOut.Location = New System.Drawing.Point(38, 39)
        Me.opExtGroupOut.Name = "opExtGroupOut"
        Me.opExtGroupOut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.opExtGroupOut.Size = New System.Drawing.Size(101, 16)
        Me.opExtGroupOut.TabIndex = 60
        Me.opExtGroupOut.TabStop = True
        Me.opExtGroupOut.Text = "EXT GROUP OUT"
        Me.opExtGroupOut.UseVisualStyleBackColor = True
        '
        'fraManual
        '
        Me.fraManual.BackColor = System.Drawing.SystemColors.Control
        Me.fraManual.Controls.Add(Me.txtManualInputReferenceStatus)
        Me.fraManual.Controls.Add(Me.txtManualInputInputMask)
        Me.fraManual.Controls.Add(Me.txtManualInputInputType)
        Me.fraManual.Controls.Add(Me.Label10)
        Me.fraManual.Controls.Add(Me.Label12)
        Me.fraManual.Controls.Add(Me.Label13)
        Me.fraManual.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraManual.Location = New System.Drawing.Point(134, 341)
        Me.fraManual.Name = "fraManual"
        Me.fraManual.Padding = New System.Windows.Forms.Padding(0)
        Me.fraManual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraManual.Size = New System.Drawing.Size(372, 151)
        Me.fraManual.TabIndex = 59
        Me.fraManual.TabStop = False
        Me.fraManual.Text = "Manual Input"
        '
        'txtManualInputReferenceStatus
        '
        Me.txtManualInputReferenceStatus.AcceptsReturn = True
        Me.txtManualInputReferenceStatus.Location = New System.Drawing.Point(146, 38)
        Me.txtManualInputReferenceStatus.MaxLength = 0
        Me.txtManualInputReferenceStatus.Name = "txtManualInputReferenceStatus"
        Me.txtManualInputReferenceStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtManualInputReferenceStatus.Size = New System.Drawing.Size(127, 19)
        Me.txtManualInputReferenceStatus.TabIndex = 0
        '
        'txtManualInputInputMask
        '
        Me.txtManualInputInputMask.AcceptsReturn = True
        Me.txtManualInputInputMask.Location = New System.Drawing.Point(146, 69)
        Me.txtManualInputInputMask.MaxLength = 0
        Me.txtManualInputInputMask.Name = "txtManualInputInputMask"
        Me.txtManualInputInputMask.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtManualInputInputMask.Size = New System.Drawing.Size(127, 19)
        Me.txtManualInputInputMask.TabIndex = 1
        '
        'txtManualInputInputType
        '
        Me.txtManualInputInputType.AcceptsReturn = True
        Me.txtManualInputInputType.Location = New System.Drawing.Point(146, 101)
        Me.txtManualInputInputType.MaxLength = 0
        Me.txtManualInputInputType.Name = "txtManualInputInputType"
        Me.txtManualInputInputType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtManualInputInputType.Size = New System.Drawing.Size(127, 19)
        Me.txtManualInputInputType.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(38, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(101, 12)
        Me.Label10.TabIndex = 50
        Me.Label10.Text = "Reference Status"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(74, 71)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(65, 12)
        Me.Label12.TabIndex = 49
        Me.Label12.Text = "Input Mask"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(74, 103)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(65, 12)
        Me.Label13.TabIndex = 48
        Me.Label13.Text = "Input Type"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraCalc
        '
        Me.fraCalc.BackColor = System.Drawing.SystemColors.Control
        Me.fraCalc.Controls.Add(Me.optOpeOutput6)
        Me.fraCalc.Controls.Add(Me.optOpeOutput5)
        Me.fraCalc.Controls.Add(Me.optOpeOutput4)
        Me.fraCalc.Controls.Add(Me.optOpeOutput3)
        Me.fraCalc.Controls.Add(Me.optOpeOutput2)
        Me.fraCalc.Controls.Add(Me.optOpeOutput1)
        Me.fraCalc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCalc.Location = New System.Drawing.Point(427, 50)
        Me.fraCalc.Name = "fraCalc"
        Me.fraCalc.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCalc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCalc.Size = New System.Drawing.Size(372, 199)
        Me.fraCalc.TabIndex = 61
        Me.fraCalc.TabStop = False
        Me.fraCalc.Text = "Calculation Input"
        '
        'optOpeOutput6
        '
        Me.optOpeOutput6.AutoSize = True
        Me.optOpeOutput6.BackColor = System.Drawing.SystemColors.Control
        Me.optOpeOutput6.Cursor = System.Windows.Forms.Cursors.Default
        Me.optOpeOutput6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOpeOutput6.Location = New System.Drawing.Point(45, 163)
        Me.optOpeOutput6.Name = "optOpeOutput6"
        Me.optOpeOutput6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOpeOutput6.Size = New System.Drawing.Size(131, 16)
        Me.optOpeOutput6.TabIndex = 56
        Me.optOpeOutput6.TabStop = True
        Me.optOpeOutput6.Text = "Operation Output 6"
        Me.optOpeOutput6.UseVisualStyleBackColor = True
        '
        'optOpeOutput5
        '
        Me.optOpeOutput5.AutoSize = True
        Me.optOpeOutput5.BackColor = System.Drawing.SystemColors.Control
        Me.optOpeOutput5.Cursor = System.Windows.Forms.Cursors.Default
        Me.optOpeOutput5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOpeOutput5.Location = New System.Drawing.Point(45, 138)
        Me.optOpeOutput5.Name = "optOpeOutput5"
        Me.optOpeOutput5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOpeOutput5.Size = New System.Drawing.Size(131, 16)
        Me.optOpeOutput5.TabIndex = 56
        Me.optOpeOutput5.TabStop = True
        Me.optOpeOutput5.Text = "Operation Output 5"
        Me.optOpeOutput5.UseVisualStyleBackColor = True
        '
        'optOpeOutput4
        '
        Me.optOpeOutput4.AutoSize = True
        Me.optOpeOutput4.BackColor = System.Drawing.SystemColors.Control
        Me.optOpeOutput4.Cursor = System.Windows.Forms.Cursors.Default
        Me.optOpeOutput4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOpeOutput4.Location = New System.Drawing.Point(45, 113)
        Me.optOpeOutput4.Name = "optOpeOutput4"
        Me.optOpeOutput4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOpeOutput4.Size = New System.Drawing.Size(131, 16)
        Me.optOpeOutput4.TabIndex = 55
        Me.optOpeOutput4.TabStop = True
        Me.optOpeOutput4.Text = "Operation Output 4"
        Me.optOpeOutput4.UseVisualStyleBackColor = True
        '
        'optOpeOutput3
        '
        Me.optOpeOutput3.AutoSize = True
        Me.optOpeOutput3.BackColor = System.Drawing.SystemColors.Control
        Me.optOpeOutput3.Cursor = System.Windows.Forms.Cursors.Default
        Me.optOpeOutput3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOpeOutput3.Location = New System.Drawing.Point(45, 88)
        Me.optOpeOutput3.Name = "optOpeOutput3"
        Me.optOpeOutput3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOpeOutput3.Size = New System.Drawing.Size(131, 16)
        Me.optOpeOutput3.TabIndex = 54
        Me.optOpeOutput3.TabStop = True
        Me.optOpeOutput3.Text = "Operation Output 3"
        Me.optOpeOutput3.UseVisualStyleBackColor = True
        '
        'optOpeOutput2
        '
        Me.optOpeOutput2.AutoSize = True
        Me.optOpeOutput2.BackColor = System.Drawing.SystemColors.Control
        Me.optOpeOutput2.Cursor = System.Windows.Forms.Cursors.Default
        Me.optOpeOutput2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOpeOutput2.Location = New System.Drawing.Point(45, 63)
        Me.optOpeOutput2.Name = "optOpeOutput2"
        Me.optOpeOutput2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOpeOutput2.Size = New System.Drawing.Size(131, 16)
        Me.optOpeOutput2.TabIndex = 53
        Me.optOpeOutput2.TabStop = True
        Me.optOpeOutput2.Text = "Operation Output 2"
        Me.optOpeOutput2.UseVisualStyleBackColor = True
        '
        'optOpeOutput1
        '
        Me.optOpeOutput1.AutoSize = True
        Me.optOpeOutput1.BackColor = System.Drawing.SystemColors.Control
        Me.optOpeOutput1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optOpeOutput1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optOpeOutput1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOpeOutput1.Location = New System.Drawing.Point(45, 38)
        Me.optOpeOutput1.Name = "optOpeOutput1"
        Me.optOpeOutput1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOpeOutput1.Size = New System.Drawing.Size(131, 16)
        Me.optOpeOutput1.TabIndex = 52
        Me.optOpeOutput1.TabStop = True
        Me.optOpeOutput1.Text = "Operation Output 1"
        Me.optOpeOutput1.UseVisualStyleBackColor = True
        '
        'fraAlarm
        '
        Me.fraAlarm.BackColor = System.Drawing.SystemColors.Control
        Me.fraAlarm.Controls.Add(Me.fraAlarmCompositeSelect)
        Me.fraAlarm.Controls.Add(Me.optAlarmComposite)
        Me.fraAlarm.Controls.Add(Me.optAlarmMotor)
        Me.fraAlarm.Controls.Add(Me.optAlarmDigital)
        Me.fraAlarm.Controls.Add(Me.optAlarmAnalog)
        Me.fraAlarm.Controls.Add(Me.fraAlarmAnalogSelect)
        Me.fraAlarm.Controls.Add(Me.optAlarmHigh)
        Me.fraAlarm.Controls.Add(Me.optAlarmLow)
        Me.fraAlarm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAlarm.Location = New System.Drawing.Point(316, 50)
        Me.fraAlarm.Name = "fraAlarm"
        Me.fraAlarm.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAlarm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAlarm.Size = New System.Drawing.Size(372, 455)
        Me.fraAlarm.TabIndex = 62
        Me.fraAlarm.TabStop = False
        Me.fraAlarm.Text = "ALARM"
        '
        'fraAlarmCompositeSelect
        '
        Me.fraAlarmCompositeSelect.BackColor = System.Drawing.SystemColors.Control
        Me.fraAlarmCompositeSelect.Controls.Add(Me.chkAlarmCompositeSelectFB)
        Me.fraAlarmCompositeSelect.Controls.Add(Me.chkAlarmCompositeSelect9)
        Me.fraAlarmCompositeSelect.Controls.Add(Me.chkAlarmCompositeSelect8)
        Me.fraAlarmCompositeSelect.Controls.Add(Me.chkAlarmCompositeSelect7)
        Me.fraAlarmCompositeSelect.Controls.Add(Me.chkAlarmCompositeSelect6)
        Me.fraAlarmCompositeSelect.Controls.Add(Me.chkAlarmCompositeSelect5)
        Me.fraAlarmCompositeSelect.Controls.Add(Me.chkAlarmCompositeSelect4)
        Me.fraAlarmCompositeSelect.Controls.Add(Me.chkAlarmCompositeSelect3)
        Me.fraAlarmCompositeSelect.Controls.Add(Me.chkAlarmCompositeSelect2)
        Me.fraAlarmCompositeSelect.Controls.Add(Me.chkAlarmCompositeSelect1)
        Me.fraAlarmCompositeSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAlarmCompositeSelect.Location = New System.Drawing.Point(50, 275)
        Me.fraAlarmCompositeSelect.Name = "fraAlarmCompositeSelect"
        Me.fraAlarmCompositeSelect.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAlarmCompositeSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAlarmCompositeSelect.Size = New System.Drawing.Size(302, 103)
        Me.fraAlarmCompositeSelect.TabIndex = 44
        Me.fraAlarmCompositeSelect.TabStop = False
        Me.fraAlarmCompositeSelect.Text = "COMPOSITE SELECT"
        '
        'chkAlarmCompositeSelectFB
        '
        Me.chkAlarmCompositeSelectFB.AutoSize = True
        Me.chkAlarmCompositeSelectFB.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmCompositeSelectFB.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmCompositeSelectFB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmCompositeSelectFB.Location = New System.Drawing.Point(99, 72)
        Me.chkAlarmCompositeSelectFB.Name = "chkAlarmCompositeSelectFB"
        Me.chkAlarmCompositeSelectFB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmCompositeSelectFB.Size = New System.Drawing.Size(108, 16)
        Me.chkAlarmCompositeSelectFB.TabIndex = 34
        Me.chkAlarmCompositeSelectFB.Text = "FeedBack Alarm"
        Me.chkAlarmCompositeSelectFB.UseVisualStyleBackColor = True
        '
        'chkAlarmCompositeSelect9
        '
        Me.chkAlarmCompositeSelect9.AutoSize = True
        Me.chkAlarmCompositeSelect9.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmCompositeSelect9.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmCompositeSelect9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmCompositeSelect9.Location = New System.Drawing.Point(35, 72)
        Me.chkAlarmCompositeSelect9.Name = "chkAlarmCompositeSelect9"
        Me.chkAlarmCompositeSelect9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmCompositeSelect9.Size = New System.Drawing.Size(42, 16)
        Me.chkAlarmCompositeSelect9.TabIndex = 34
        Me.chkAlarmCompositeSelect9.Text = "ST9"
        Me.chkAlarmCompositeSelect9.UseVisualStyleBackColor = True
        '
        'chkAlarmCompositeSelect8
        '
        Me.chkAlarmCompositeSelect8.AutoSize = True
        Me.chkAlarmCompositeSelect8.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmCompositeSelect8.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmCompositeSelect8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmCompositeSelect8.Location = New System.Drawing.Point(227, 47)
        Me.chkAlarmCompositeSelect8.Name = "chkAlarmCompositeSelect8"
        Me.chkAlarmCompositeSelect8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmCompositeSelect8.Size = New System.Drawing.Size(42, 16)
        Me.chkAlarmCompositeSelect8.TabIndex = 33
        Me.chkAlarmCompositeSelect8.Text = "ST8"
        Me.chkAlarmCompositeSelect8.UseVisualStyleBackColor = True
        '
        'chkAlarmCompositeSelect7
        '
        Me.chkAlarmCompositeSelect7.AutoSize = True
        Me.chkAlarmCompositeSelect7.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmCompositeSelect7.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmCompositeSelect7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmCompositeSelect7.Location = New System.Drawing.Point(163, 47)
        Me.chkAlarmCompositeSelect7.Name = "chkAlarmCompositeSelect7"
        Me.chkAlarmCompositeSelect7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmCompositeSelect7.Size = New System.Drawing.Size(42, 16)
        Me.chkAlarmCompositeSelect7.TabIndex = 32
        Me.chkAlarmCompositeSelect7.Text = "ST7"
        Me.chkAlarmCompositeSelect7.UseVisualStyleBackColor = True
        '
        'chkAlarmCompositeSelect6
        '
        Me.chkAlarmCompositeSelect6.AutoSize = True
        Me.chkAlarmCompositeSelect6.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmCompositeSelect6.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmCompositeSelect6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmCompositeSelect6.Location = New System.Drawing.Point(99, 47)
        Me.chkAlarmCompositeSelect6.Name = "chkAlarmCompositeSelect6"
        Me.chkAlarmCompositeSelect6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmCompositeSelect6.Size = New System.Drawing.Size(42, 16)
        Me.chkAlarmCompositeSelect6.TabIndex = 31
        Me.chkAlarmCompositeSelect6.Text = "ST6"
        Me.chkAlarmCompositeSelect6.UseVisualStyleBackColor = True
        '
        'chkAlarmCompositeSelect5
        '
        Me.chkAlarmCompositeSelect5.AutoSize = True
        Me.chkAlarmCompositeSelect5.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmCompositeSelect5.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmCompositeSelect5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmCompositeSelect5.Location = New System.Drawing.Point(35, 47)
        Me.chkAlarmCompositeSelect5.Name = "chkAlarmCompositeSelect5"
        Me.chkAlarmCompositeSelect5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmCompositeSelect5.Size = New System.Drawing.Size(42, 16)
        Me.chkAlarmCompositeSelect5.TabIndex = 30
        Me.chkAlarmCompositeSelect5.Text = "ST5"
        Me.chkAlarmCompositeSelect5.UseVisualStyleBackColor = True
        '
        'chkAlarmCompositeSelect4
        '
        Me.chkAlarmCompositeSelect4.AutoSize = True
        Me.chkAlarmCompositeSelect4.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmCompositeSelect4.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmCompositeSelect4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmCompositeSelect4.Location = New System.Drawing.Point(227, 22)
        Me.chkAlarmCompositeSelect4.Name = "chkAlarmCompositeSelect4"
        Me.chkAlarmCompositeSelect4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmCompositeSelect4.Size = New System.Drawing.Size(42, 16)
        Me.chkAlarmCompositeSelect4.TabIndex = 29
        Me.chkAlarmCompositeSelect4.Text = "ST4"
        Me.chkAlarmCompositeSelect4.UseVisualStyleBackColor = True
        '
        'chkAlarmCompositeSelect3
        '
        Me.chkAlarmCompositeSelect3.AutoSize = True
        Me.chkAlarmCompositeSelect3.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmCompositeSelect3.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmCompositeSelect3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmCompositeSelect3.Location = New System.Drawing.Point(163, 22)
        Me.chkAlarmCompositeSelect3.Name = "chkAlarmCompositeSelect3"
        Me.chkAlarmCompositeSelect3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmCompositeSelect3.Size = New System.Drawing.Size(42, 16)
        Me.chkAlarmCompositeSelect3.TabIndex = 28
        Me.chkAlarmCompositeSelect3.Text = "ST3"
        Me.chkAlarmCompositeSelect3.UseVisualStyleBackColor = True
        '
        'chkAlarmCompositeSelect2
        '
        Me.chkAlarmCompositeSelect2.AutoSize = True
        Me.chkAlarmCompositeSelect2.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmCompositeSelect2.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmCompositeSelect2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmCompositeSelect2.Location = New System.Drawing.Point(99, 22)
        Me.chkAlarmCompositeSelect2.Name = "chkAlarmCompositeSelect2"
        Me.chkAlarmCompositeSelect2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmCompositeSelect2.Size = New System.Drawing.Size(42, 16)
        Me.chkAlarmCompositeSelect2.TabIndex = 27
        Me.chkAlarmCompositeSelect2.Text = "ST2"
        Me.chkAlarmCompositeSelect2.UseVisualStyleBackColor = True
        '
        'chkAlarmCompositeSelect1
        '
        Me.chkAlarmCompositeSelect1.AutoSize = True
        Me.chkAlarmCompositeSelect1.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmCompositeSelect1.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmCompositeSelect1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmCompositeSelect1.Location = New System.Drawing.Point(35, 22)
        Me.chkAlarmCompositeSelect1.Name = "chkAlarmCompositeSelect1"
        Me.chkAlarmCompositeSelect1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmCompositeSelect1.Size = New System.Drawing.Size(42, 16)
        Me.chkAlarmCompositeSelect1.TabIndex = 26
        Me.chkAlarmCompositeSelect1.Text = "ST1"
        Me.chkAlarmCompositeSelect1.UseVisualStyleBackColor = True
        '
        'optAlarmComposite
        '
        Me.optAlarmComposite.AutoSize = True
        Me.optAlarmComposite.BackColor = System.Drawing.SystemColors.Control
        Me.optAlarmComposite.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAlarmComposite.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAlarmComposite.Location = New System.Drawing.Point(29, 248)
        Me.optAlarmComposite.Name = "optAlarmComposite"
        Me.optAlarmComposite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAlarmComposite.Size = New System.Drawing.Size(77, 16)
        Me.optAlarmComposite.TabIndex = 43
        Me.optAlarmComposite.TabStop = True
        Me.optAlarmComposite.Text = "COMPOSITE"
        Me.optAlarmComposite.UseVisualStyleBackColor = True
        '
        'optAlarmMotor
        '
        Me.optAlarmMotor.AutoSize = True
        Me.optAlarmMotor.BackColor = System.Drawing.SystemColors.Control
        Me.optAlarmMotor.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAlarmMotor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAlarmMotor.Location = New System.Drawing.Point(29, 226)
        Me.optAlarmMotor.Name = "optAlarmMotor"
        Me.optAlarmMotor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAlarmMotor.Size = New System.Drawing.Size(53, 16)
        Me.optAlarmMotor.TabIndex = 43
        Me.optAlarmMotor.TabStop = True
        Me.optAlarmMotor.Text = "MOTOR"
        Me.optAlarmMotor.UseVisualStyleBackColor = True
        '
        'optAlarmDigital
        '
        Me.optAlarmDigital.AutoSize = True
        Me.optAlarmDigital.BackColor = System.Drawing.SystemColors.Control
        Me.optAlarmDigital.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAlarmDigital.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAlarmDigital.Location = New System.Drawing.Point(29, 204)
        Me.optAlarmDigital.Name = "optAlarmDigital"
        Me.optAlarmDigital.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAlarmDigital.Size = New System.Drawing.Size(65, 16)
        Me.optAlarmDigital.TabIndex = 42
        Me.optAlarmDigital.TabStop = True
        Me.optAlarmDigital.Text = "DIGITAL"
        Me.optAlarmDigital.UseVisualStyleBackColor = True
        '
        'optAlarmAnalog
        '
        Me.optAlarmAnalog.AutoSize = True
        Me.optAlarmAnalog.BackColor = System.Drawing.SystemColors.Control
        Me.optAlarmAnalog.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAlarmAnalog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAlarmAnalog.Location = New System.Drawing.Point(29, 30)
        Me.optAlarmAnalog.Name = "optAlarmAnalog"
        Me.optAlarmAnalog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAlarmAnalog.Size = New System.Drawing.Size(59, 16)
        Me.optAlarmAnalog.TabIndex = 41
        Me.optAlarmAnalog.TabStop = True
        Me.optAlarmAnalog.Text = "ANALOG"
        Me.optAlarmAnalog.UseVisualStyleBackColor = True
        '
        'fraAlarmAnalogSelect
        '
        Me.fraAlarmAnalogSelect.BackColor = System.Drawing.SystemColors.Control
        Me.fraAlarmAnalogSelect.Controls.Add(Me.fraAlarmAnalogAlarmSelect)
        Me.fraAlarmAnalogSelect.Controls.Add(Me.optAlarmAnalogAnalog)
        Me.fraAlarmAnalogSelect.Controls.Add(Me.optAlarmAnalogSensor)
        Me.fraAlarmAnalogSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAlarmAnalogSelect.Location = New System.Drawing.Point(47, 55)
        Me.fraAlarmAnalogSelect.Name = "fraAlarmAnalogSelect"
        Me.fraAlarmAnalogSelect.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAlarmAnalogSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAlarmAnalogSelect.Size = New System.Drawing.Size(276, 143)
        Me.fraAlarmAnalogSelect.TabIndex = 35
        Me.fraAlarmAnalogSelect.TabStop = False
        Me.fraAlarmAnalogSelect.Text = "ANALOG SELECT"
        '
        'fraAlarmAnalogAlarmSelect
        '
        Me.fraAlarmAnalogAlarmSelect.BackColor = System.Drawing.SystemColors.Control
        Me.fraAlarmAnalogAlarmSelect.Controls.Add(Me.chkAlarmSelectLoLo)
        Me.fraAlarmAnalogAlarmSelect.Controls.Add(Me.chkAlarmSelectLo)
        Me.fraAlarmAnalogAlarmSelect.Controls.Add(Me.chkAlarmSelectHi)
        Me.fraAlarmAnalogAlarmSelect.Controls.Add(Me.chkAlarmSelectHiHI)
        Me.fraAlarmAnalogAlarmSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAlarmAnalogAlarmSelect.Location = New System.Drawing.Point(24, 48)
        Me.fraAlarmAnalogAlarmSelect.Name = "fraAlarmAnalogAlarmSelect"
        Me.fraAlarmAnalogAlarmSelect.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAlarmAnalogAlarmSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAlarmAnalogAlarmSelect.Size = New System.Drawing.Size(224, 60)
        Me.fraAlarmAnalogAlarmSelect.TabIndex = 41
        Me.fraAlarmAnalogAlarmSelect.TabStop = False
        Me.fraAlarmAnalogAlarmSelect.Text = "ALARM SELECT"
        '
        'chkAlarmSelectLoLo
        '
        Me.chkAlarmSelectLoLo.AutoSize = True
        Me.chkAlarmSelectLoLo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmSelectLoLo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmSelectLoLo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmSelectLoLo.Location = New System.Drawing.Point(160, 28)
        Me.chkAlarmSelectLoLo.Name = "chkAlarmSelectLoLo"
        Me.chkAlarmSelectLoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmSelectLoLo.Size = New System.Drawing.Size(48, 16)
        Me.chkAlarmSelectLoLo.TabIndex = 27
        Me.chkAlarmSelectLoLo.Text = "LOLO"
        Me.chkAlarmSelectLoLo.UseVisualStyleBackColor = True
        '
        'chkAlarmSelectLo
        '
        Me.chkAlarmSelectLo.AutoSize = True
        Me.chkAlarmSelectLo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmSelectLo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmSelectLo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmSelectLo.Location = New System.Drawing.Point(112, 28)
        Me.chkAlarmSelectLo.Name = "chkAlarmSelectLo"
        Me.chkAlarmSelectLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmSelectLo.Size = New System.Drawing.Size(36, 16)
        Me.chkAlarmSelectLo.TabIndex = 26
        Me.chkAlarmSelectLo.Text = "LO"
        Me.chkAlarmSelectLo.UseVisualStyleBackColor = True
        '
        'chkAlarmSelectHi
        '
        Me.chkAlarmSelectHi.AutoSize = True
        Me.chkAlarmSelectHi.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmSelectHi.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmSelectHi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmSelectHi.Location = New System.Drawing.Point(68, 28)
        Me.chkAlarmSelectHi.Name = "chkAlarmSelectHi"
        Me.chkAlarmSelectHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmSelectHi.Size = New System.Drawing.Size(36, 16)
        Me.chkAlarmSelectHi.TabIndex = 25
        Me.chkAlarmSelectHi.Text = "HI"
        Me.chkAlarmSelectHi.UseVisualStyleBackColor = True
        '
        'chkAlarmSelectHiHI
        '
        Me.chkAlarmSelectHiHI.AutoSize = True
        Me.chkAlarmSelectHiHI.BackColor = System.Drawing.SystemColors.Control
        Me.chkAlarmSelectHiHI.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAlarmSelectHiHI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAlarmSelectHiHI.Location = New System.Drawing.Point(12, 28)
        Me.chkAlarmSelectHiHI.Name = "chkAlarmSelectHiHI"
        Me.chkAlarmSelectHiHI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAlarmSelectHiHI.Size = New System.Drawing.Size(48, 16)
        Me.chkAlarmSelectHiHI.TabIndex = 24
        Me.chkAlarmSelectHiHI.Text = "HIHI"
        Me.chkAlarmSelectHiHI.UseVisualStyleBackColor = True
        '
        'optAlarmAnalogAnalog
        '
        Me.optAlarmAnalogAnalog.AutoSize = True
        Me.optAlarmAnalogAnalog.BackColor = System.Drawing.SystemColors.Control
        Me.optAlarmAnalogAnalog.Checked = True
        Me.optAlarmAnalogAnalog.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAlarmAnalogAnalog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAlarmAnalogAnalog.Location = New System.Drawing.Point(16, 24)
        Me.optAlarmAnalogAnalog.Name = "optAlarmAnalogAnalog"
        Me.optAlarmAnalogAnalog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAlarmAnalogAnalog.Size = New System.Drawing.Size(59, 16)
        Me.optAlarmAnalogAnalog.TabIndex = 40
        Me.optAlarmAnalogAnalog.TabStop = True
        Me.optAlarmAnalogAnalog.Text = "ANALOG"
        Me.optAlarmAnalogAnalog.UseVisualStyleBackColor = True
        '
        'optAlarmAnalogSensor
        '
        Me.optAlarmAnalogSensor.AutoSize = True
        Me.optAlarmAnalogSensor.BackColor = System.Drawing.SystemColors.Control
        Me.optAlarmAnalogSensor.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAlarmAnalogSensor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAlarmAnalogSensor.Location = New System.Drawing.Point(16, 114)
        Me.optAlarmAnalogSensor.Name = "optAlarmAnalogSensor"
        Me.optAlarmAnalogSensor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAlarmAnalogSensor.Size = New System.Drawing.Size(59, 16)
        Me.optAlarmAnalogSensor.TabIndex = 39
        Me.optAlarmAnalogSensor.Text = "SENSOR"
        Me.optAlarmAnalogSensor.UseVisualStyleBackColor = True
        '
        'optAlarmHigh
        '
        Me.optAlarmHigh.AutoSize = True
        Me.optAlarmHigh.BackColor = System.Drawing.SystemColors.Control
        Me.optAlarmHigh.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAlarmHigh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAlarmHigh.Location = New System.Drawing.Point(29, 390)
        Me.optAlarmHigh.Name = "optAlarmHigh"
        Me.optAlarmHigh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAlarmHigh.Size = New System.Drawing.Size(125, 16)
        Me.optAlarmHigh.TabIndex = 34
        Me.optAlarmHigh.TabStop = True
        Me.optAlarmHigh.Text = "High Edge Trigger"
        Me.optAlarmHigh.UseVisualStyleBackColor = True
        '
        'optAlarmLow
        '
        Me.optAlarmLow.AutoSize = True
        Me.optAlarmLow.BackColor = System.Drawing.SystemColors.Control
        Me.optAlarmLow.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAlarmLow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAlarmLow.Location = New System.Drawing.Point(29, 417)
        Me.optAlarmLow.Name = "optAlarmLow"
        Me.optAlarmLow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAlarmLow.Size = New System.Drawing.Size(119, 16)
        Me.optAlarmLow.TabIndex = 33
        Me.optAlarmLow.TabStop = True
        Me.optAlarmLow.Text = "Low Edge Trigger"
        Me.optAlarmLow.UseVisualStyleBackColor = True
        '
        'pnlIO
        '
        Me.pnlIO.Controls.Add(Me.optIOSelOutput)
        Me.pnlIO.Controls.Add(Me.optIOSelInput)
        Me.pnlIO.Location = New System.Drawing.Point(180, 46)
        Me.pnlIO.Name = "pnlIO"
        Me.pnlIO.Size = New System.Drawing.Size(137, 21)
        Me.pnlIO.TabIndex = 63
        '
        'optIOSelOutput
        '
        Me.optIOSelOutput.AutoSize = True
        Me.optIOSelOutput.Location = New System.Drawing.Point(62, 3)
        Me.optIOSelOutput.Name = "optIOSelOutput"
        Me.optIOSelOutput.Size = New System.Drawing.Size(59, 16)
        Me.optIOSelOutput.TabIndex = 26
        Me.optIOSelOutput.TabStop = True
        Me.optIOSelOutput.Text = "Output"
        Me.optIOSelOutput.UseVisualStyleBackColor = True
        '
        'optIOSelInput
        '
        Me.optIOSelInput.AutoSize = True
        Me.optIOSelInput.Location = New System.Drawing.Point(3, 3)
        Me.optIOSelInput.Name = "optIOSelInput"
        Me.optIOSelInput.Size = New System.Drawing.Size(53, 16)
        Me.optIOSelInput.TabIndex = 26
        Me.optIOSelInput.TabStop = True
        Me.optIOSelInput.Text = "Input"
        Me.optIOSelInput.UseVisualStyleBackColor = True
        '
        'frmSeqSetInputData_GAI
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(724, 612)
        Me.ControlBox = False
        Me.Controls.Add(Me.fraData)
        Me.Controls.Add(Me.fraAlarm)
        Me.Controls.Add(Me.pnlIO)
        Me.Controls.Add(Me.fraManual)
        Me.Controls.Add(Me.fraExtGroup)
        Me.Controls.Add(Me.fraInputDataSelect)
        Me.Controls.Add(Me.txtData)
        Me.Controls.Add(Me.txtInputSetNo)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.lblData)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.fraCalc)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSeqSetInputData_GAI"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CONTROL SEQUENCE INPUT"
        Me.fraData.ResumeLayout(False)
        Me.fraData.PerformLayout()
        Me.fraDataCompositeSelect.ResumeLayout(False)
        Me.fraDataCompositeSelect.PerformLayout()
        Me.fraDataMotorSelect.ResumeLayout(False)
        Me.fraDataMotorSelect.PerformLayout()
        Me.fraDataMotorRunSelect.ResumeLayout(False)
        Me.fraDataMotorRunSelect.PerformLayout()
        Me.fraInputDataSelect.ResumeLayout(False)
        Me.fraInputDataSelect.PerformLayout()
        Me.fraInputType.ResumeLayout(False)
        Me.fraInputType.PerformLayout()
        Me.fraExtGroup.ResumeLayout(False)
        Me.fraExtGroup.PerformLayout()
        Me.fraManual.ResumeLayout(False)
        Me.fraManual.PerformLayout()
        Me.fraCalc.ResumeLayout(False)
        Me.fraCalc.PerformLayout()
        Me.fraAlarm.ResumeLayout(False)
        Me.fraAlarm.PerformLayout()
        Me.fraAlarmCompositeSelect.ResumeLayout(False)
        Me.fraAlarmCompositeSelect.PerformLayout()
        Me.fraAlarmAnalogSelect.ResumeLayout(False)
        Me.fraAlarmAnalogSelect.PerformLayout()
        Me.fraAlarmAnalogAlarmSelect.ResumeLayout(False)
        Me.fraAlarmAnalogAlarmSelect.PerformLayout()
        Me.pnlIO.ResumeLayout(False)
        Me.pnlIO.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents fraExtGroup As System.Windows.Forms.GroupBox
    Public WithEvents optExtBzOut As System.Windows.Forms.RadioButton
    Public WithEvents opExtGroupOut As System.Windows.Forms.RadioButton
    Public WithEvents fraManual As System.Windows.Forms.GroupBox
    Public WithEvents txtManualInputReferenceStatus As System.Windows.Forms.TextBox
    Public WithEvents txtManualInputInputMask As System.Windows.Forms.TextBox
    Public WithEvents txtManualInputInputType As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents fraCalc As System.Windows.Forms.GroupBox
    Public WithEvents optOpeOutput5 As System.Windows.Forms.RadioButton
    Public WithEvents optOpeOutput4 As System.Windows.Forms.RadioButton
    Public WithEvents optOpeOutput3 As System.Windows.Forms.RadioButton
    Public WithEvents optOpeOutput2 As System.Windows.Forms.RadioButton
    Public WithEvents optOpeOutput1 As System.Windows.Forms.RadioButton
    Public WithEvents fraAlarm As System.Windows.Forms.GroupBox
    Public WithEvents optAlarmMotor As System.Windows.Forms.RadioButton
    Public WithEvents optAlarmDigital As System.Windows.Forms.RadioButton
    Public WithEvents optAlarmAnalog As System.Windows.Forms.RadioButton
    Public WithEvents fraAlarmAnalogSelect As System.Windows.Forms.GroupBox
    Public WithEvents fraAlarmAnalogAlarmSelect As System.Windows.Forms.GroupBox
    Public WithEvents chkAlarmSelectLoLo As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmSelectLo As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmSelectHi As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmSelectHiHI As System.Windows.Forms.CheckBox
    Public WithEvents optAlarmAnalogAnalog As System.Windows.Forms.RadioButton
    Public WithEvents optAlarmAnalogSensor As System.Windows.Forms.RadioButton
    Public WithEvents optAlarmHigh As System.Windows.Forms.RadioButton
    Public WithEvents optAlarmLow As System.Windows.Forms.RadioButton
    Public WithEvents txtExtInputMask As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents fraDataCompositeSelect As System.Windows.Forms.GroupBox
    Public WithEvents chkCompositeSelect8 As System.Windows.Forms.CheckBox
    Public WithEvents chkCompositeSelect7 As System.Windows.Forms.CheckBox
    Public WithEvents chkCompositeSelect6 As System.Windows.Forms.CheckBox
    Public WithEvents chkCompositeSelect5 As System.Windows.Forms.CheckBox
    Public WithEvents chkCompositeSelect4 As System.Windows.Forms.CheckBox
    Public WithEvents chkCompositeSelect3 As System.Windows.Forms.CheckBox
    Public WithEvents chkCompositeSelect2 As System.Windows.Forms.CheckBox
    Public WithEvents chkCompositeSelect1 As System.Windows.Forms.CheckBox
    Public WithEvents optDataComposite As System.Windows.Forms.RadioButton
    Public WithEvents optAlarmComposite As System.Windows.Forms.RadioButton
    Public WithEvents fraAlarmCompositeSelect As System.Windows.Forms.GroupBox
    Public WithEvents chkAlarmCompositeSelect8 As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmCompositeSelect7 As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmCompositeSelect6 As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmCompositeSelect5 As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmCompositeSelect4 As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmCompositeSelect3 As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmCompositeSelect2 As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmCompositeSelect1 As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmCompositeSelectFB As System.Windows.Forms.CheckBox
    Public WithEvents chkAlarmCompositeSelect9 As System.Windows.Forms.CheckBox
    Friend WithEvents pnlIO As System.Windows.Forms.Panel
    Friend WithEvents optIOSelOutput As System.Windows.Forms.RadioButton
    Friend WithEvents optIOSelInput As System.Windows.Forms.RadioButton
    Public WithEvents optOpeOutput6 As System.Windows.Forms.RadioButton
    Public WithEvents optDataFixed As System.Windows.Forms.RadioButton
#End Region

End Class
