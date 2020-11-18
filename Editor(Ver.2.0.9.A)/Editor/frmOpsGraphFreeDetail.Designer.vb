<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsGraphFreeDetail
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

    Public WithEvents fraBit As System.Windows.Forms.GroupBox
    Public WithEvents fraIndicatorDetail As System.Windows.Forms.GroupBox
    Public WithEvents txtSelectChannel As System.Windows.Forms.TextBox
    Public WithEvents optGraphTypeBar As System.Windows.Forms.RadioButton
    Public WithEvents optGraphTypeCounter As System.Windows.Forms.RadioButton
    Public WithEvents optGraphTypeAnalog As System.Windows.Forms.RadioButton
    Public WithEvents optGraphTypeIndicator As System.Windows.Forms.RadioButton
    Public WithEvents fraGraphType As System.Windows.Forms.GroupBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmbGroupList As System.Windows.Forms.ComboBox

    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.fraIndicatorDetail = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optIndTypeSensor = New System.Windows.Forms.RadioButton()
        Me.optIndTypeRepose = New System.Windows.Forms.RadioButton()
        Me.optIndTypeNoSet = New System.Windows.Forms.RadioButton()
        Me.optIndTypeAlarm = New System.Windows.Forms.RadioButton()
        Me.optIndTypeData = New System.Windows.Forms.RadioButton()
        Me.fraBit = New System.Windows.Forms.GroupBox()
        Me.grdBit = New Editor.clsDataGridViewPlus()
        Me.cmbDeviceStatus = New System.Windows.Forms.ComboBox()
        Me.txtSelectChannel = New System.Windows.Forms.TextBox()
        Me.fraGraphType = New System.Windows.Forms.GroupBox()
        Me.optGraphTypeBar = New System.Windows.Forms.RadioButton()
        Me.optGraphTypeCounter = New System.Windows.Forms.RadioButton()
        Me.optGraphTypeAnalog = New System.Windows.Forms.RadioButton()
        Me.optGraphTypeIndicator = New System.Windows.Forms.RadioButton()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmbGroupList = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.fraChannelSelect = New System.Windows.Forms.GroupBox()
        Me.grdCH = New Editor.clsDataGridViewPlus()
        Me.fra = New System.Windows.Forms.GroupBox()
        Me.fraAnalogDetail = New System.Windows.Forms.GroupBox()
        Me.txtAnalogColor = New System.Windows.Forms.TextBox()
        Me.txtAnalogScale = New System.Windows.Forms.TextBox()
        Me.lblAnalogColor = New System.Windows.Forms.Label()
        Me.lblAnalogScale = New System.Windows.Forms.Label()
        Me.txtChTypeName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtChTypeCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtChDataType = New System.Windows.Forms.TextBox()
        Me.fraIndicatorDetail.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.fraBit.SuspendLayout()
        CType(Me.grdBit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraGraphType.SuspendLayout()
        Me.fraChannelSelect.SuspendLayout()
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fra.SuspendLayout()
        Me.fraAnalogDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'fraIndicatorDetail
        '
        Me.fraIndicatorDetail.BackColor = System.Drawing.SystemColors.Control
        Me.fraIndicatorDetail.Controls.Add(Me.GroupBox2)
        Me.fraIndicatorDetail.Controls.Add(Me.fraBit)
        Me.fraIndicatorDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraIndicatorDetail.Location = New System.Drawing.Point(21, 191)
        Me.fraIndicatorDetail.Name = "fraIndicatorDetail"
        Me.fraIndicatorDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.fraIndicatorDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraIndicatorDetail.Size = New System.Drawing.Size(312, 335)
        Me.fraIndicatorDetail.TabIndex = 5
        Me.fraIndicatorDetail.TabStop = False
        Me.fraIndicatorDetail.Text = "Indicator Detail"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optIndTypeSensor)
        Me.GroupBox2.Controls.Add(Me.optIndTypeRepose)
        Me.GroupBox2.Controls.Add(Me.optIndTypeNoSet)
        Me.GroupBox2.Controls.Add(Me.optIndTypeAlarm)
        Me.GroupBox2.Controls.Add(Me.optIndTypeData)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 24)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(278, 101)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Display Type"
        '
        'optIndTypeSensor
        '
        Me.optIndTypeSensor.AutoSize = True
        Me.optIndTypeSensor.Location = New System.Drawing.Point(94, 68)
        Me.optIndTypeSensor.Name = "optIndTypeSensor"
        Me.optIndTypeSensor.Size = New System.Drawing.Size(89, 16)
        Me.optIndTypeSensor.TabIndex = 0
        Me.optIndTypeSensor.TabStop = True
        Me.optIndTypeSensor.Text = "Sensor Fail"
        Me.optIndTypeSensor.UseVisualStyleBackColor = True
        '
        'optIndTypeRepose
        '
        Me.optIndTypeRepose.AutoSize = True
        Me.optIndTypeRepose.Location = New System.Drawing.Point(94, 46)
        Me.optIndTypeRepose.Name = "optIndTypeRepose"
        Me.optIndTypeRepose.Size = New System.Drawing.Size(59, 16)
        Me.optIndTypeRepose.TabIndex = 0
        Me.optIndTypeRepose.TabStop = True
        Me.optIndTypeRepose.Text = "Repose"
        Me.optIndTypeRepose.UseVisualStyleBackColor = True
        '
        'optIndTypeNoSet
        '
        Me.optIndTypeNoSet.AutoSize = True
        Me.optIndTypeNoSet.Location = New System.Drawing.Point(192, 24)
        Me.optIndTypeNoSet.Name = "optIndTypeNoSet"
        Me.optIndTypeNoSet.Size = New System.Drawing.Size(59, 16)
        Me.optIndTypeNoSet.TabIndex = 0
        Me.optIndTypeNoSet.TabStop = True
        Me.optIndTypeNoSet.Text = "No Set"
        Me.optIndTypeNoSet.UseVisualStyleBackColor = True
        '
        'optIndTypeAlarm
        '
        Me.optIndTypeAlarm.AutoSize = True
        Me.optIndTypeAlarm.Location = New System.Drawing.Point(94, 24)
        Me.optIndTypeAlarm.Name = "optIndTypeAlarm"
        Me.optIndTypeAlarm.Size = New System.Drawing.Size(53, 16)
        Me.optIndTypeAlarm.TabIndex = 0
        Me.optIndTypeAlarm.TabStop = True
        Me.optIndTypeAlarm.Text = "Alarm"
        Me.optIndTypeAlarm.UseVisualStyleBackColor = True
        '
        'optIndTypeData
        '
        Me.optIndTypeData.AutoSize = True
        Me.optIndTypeData.Location = New System.Drawing.Point(18, 24)
        Me.optIndTypeData.Name = "optIndTypeData"
        Me.optIndTypeData.Size = New System.Drawing.Size(47, 16)
        Me.optIndTypeData.TabIndex = 0
        Me.optIndTypeData.TabStop = True
        Me.optIndTypeData.Text = "Data"
        Me.optIndTypeData.UseVisualStyleBackColor = True
        '
        'fraBit
        '
        Me.fraBit.BackColor = System.Drawing.SystemColors.Control
        Me.fraBit.Controls.Add(Me.grdBit)
        Me.fraBit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBit.Location = New System.Drawing.Point(18, 131)
        Me.fraBit.Name = "fraBit"
        Me.fraBit.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBit.Size = New System.Drawing.Size(278, 191)
        Me.fraBit.TabIndex = 2
        Me.fraBit.TabStop = False
        Me.fraBit.Text = "Bit"
        '
        'grdBit
        '
        Me.grdBit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdBit.Location = New System.Drawing.Point(12, 16)
        Me.grdBit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.grdBit.Name = "grdBit"
        Me.grdBit.RowTemplate.Height = 21
        Me.grdBit.Size = New System.Drawing.Size(254, 166)
        Me.grdBit.TabIndex = 14
        '
        'cmbDeviceStatus
        '
        Me.cmbDeviceStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDeviceStatus.FormattingEnabled = True
        Me.cmbDeviceStatus.Location = New System.Drawing.Point(358, 569)
        Me.cmbDeviceStatus.Name = "cmbDeviceStatus"
        Me.cmbDeviceStatus.Size = New System.Drawing.Size(164, 20)
        Me.cmbDeviceStatus.TabIndex = 113
        Me.cmbDeviceStatus.Visible = False
        '
        'txtSelectChannel
        '
        Me.txtSelectChannel.AcceptsReturn = True
        Me.txtSelectChannel.Location = New System.Drawing.Point(20, 53)
        Me.txtSelectChannel.MaxLength = 0
        Me.txtSelectChannel.Name = "txtSelectChannel"
        Me.txtSelectChannel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSelectChannel.Size = New System.Drawing.Size(49, 19)
        Me.txtSelectChannel.TabIndex = 3
        Me.txtSelectChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'fraGraphType
        '
        Me.fraGraphType.BackColor = System.Drawing.SystemColors.Control
        Me.fraGraphType.Controls.Add(Me.optGraphTypeBar)
        Me.fraGraphType.Controls.Add(Me.optGraphTypeCounter)
        Me.fraGraphType.Controls.Add(Me.optGraphTypeAnalog)
        Me.fraGraphType.Controls.Add(Me.optGraphTypeIndicator)
        Me.fraGraphType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraGraphType.Location = New System.Drawing.Point(22, 99)
        Me.fraGraphType.Name = "fraGraphType"
        Me.fraGraphType.Padding = New System.Windows.Forms.Padding(0)
        Me.fraGraphType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraGraphType.Size = New System.Drawing.Size(311, 81)
        Me.fraGraphType.TabIndex = 4
        Me.fraGraphType.TabStop = False
        Me.fraGraphType.Text = "Graph Type"
        '
        'optGraphTypeBar
        '
        Me.optGraphTypeBar.BackColor = System.Drawing.SystemColors.Control
        Me.optGraphTypeBar.Cursor = System.Windows.Forms.Cursors.Default
        Me.optGraphTypeBar.Enabled = False
        Me.optGraphTypeBar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGraphTypeBar.Location = New System.Drawing.Point(25, 48)
        Me.optGraphTypeBar.Name = "optGraphTypeBar"
        Me.optGraphTypeBar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optGraphTypeBar.Size = New System.Drawing.Size(130, 21)
        Me.optGraphTypeBar.TabIndex = 2
        Me.optGraphTypeBar.TabStop = True
        Me.optGraphTypeBar.Text = "Bar (1 * 2)"
        Me.optGraphTypeBar.UseVisualStyleBackColor = True
        '
        'optGraphTypeCounter
        '
        Me.optGraphTypeCounter.BackColor = System.Drawing.SystemColors.Control
        Me.optGraphTypeCounter.Cursor = System.Windows.Forms.Cursors.Default
        Me.optGraphTypeCounter.Enabled = False
        Me.optGraphTypeCounter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGraphTypeCounter.Location = New System.Drawing.Point(25, 20)
        Me.optGraphTypeCounter.Name = "optGraphTypeCounter"
        Me.optGraphTypeCounter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optGraphTypeCounter.Size = New System.Drawing.Size(130, 21)
        Me.optGraphTypeCounter.TabIndex = 0
        Me.optGraphTypeCounter.TabStop = True
        Me.optGraphTypeCounter.Text = "Data (1 * 1)"
        Me.optGraphTypeCounter.UseVisualStyleBackColor = True
        '
        'optGraphTypeAnalog
        '
        Me.optGraphTypeAnalog.BackColor = System.Drawing.SystemColors.Control
        Me.optGraphTypeAnalog.Cursor = System.Windows.Forms.Cursors.Default
        Me.optGraphTypeAnalog.Enabled = False
        Me.optGraphTypeAnalog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGraphTypeAnalog.Location = New System.Drawing.Point(165, 20)
        Me.optGraphTypeAnalog.Name = "optGraphTypeAnalog"
        Me.optGraphTypeAnalog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optGraphTypeAnalog.Size = New System.Drawing.Size(130, 21)
        Me.optGraphTypeAnalog.TabIndex = 1
        Me.optGraphTypeAnalog.TabStop = True
        Me.optGraphTypeAnalog.Text = "Meter (2 * 2)"
        Me.optGraphTypeAnalog.UseVisualStyleBackColor = True
        '
        'optGraphTypeIndicator
        '
        Me.optGraphTypeIndicator.BackColor = System.Drawing.SystemColors.Control
        Me.optGraphTypeIndicator.Cursor = System.Windows.Forms.Cursors.Default
        Me.optGraphTypeIndicator.Enabled = False
        Me.optGraphTypeIndicator.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGraphTypeIndicator.Location = New System.Drawing.Point(165, 48)
        Me.optGraphTypeIndicator.Name = "optGraphTypeIndicator"
        Me.optGraphTypeIndicator.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optGraphTypeIndicator.Size = New System.Drawing.Size(130, 21)
        Me.optGraphTypeIndicator.TabIndex = 3
        Me.optGraphTypeIndicator.TabStop = True
        Me.optGraphTypeIndicator.Text = "Indicator (1 * 1)"
        Me.optGraphTypeIndicator.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(696, 558)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 7
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(576, 558)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 6
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmbGroupList
        '
        Me.cmbGroupList.BackColor = System.Drawing.SystemColors.Window
        Me.cmbGroupList.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbGroupList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGroupList.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbGroupList.Location = New System.Drawing.Point(75, 28)
        Me.cmbGroupList.Name = "cmbGroupList"
        Me.cmbGroupList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbGroupList.Size = New System.Drawing.Size(68, 20)
        Me.cmbGroupList.TabIndex = 1
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(22, 34)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(41, 12)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "CH No."
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(16, 65)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(77, 12)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "Channel List"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(16, 31)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(53, 12)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "Group No"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraChannelSelect
        '
        Me.fraChannelSelect.Controls.Add(Me.grdCH)
        Me.fraChannelSelect.Controls.Add(Me.cmbGroupList)
        Me.fraChannelSelect.Controls.Add(Me.Label14)
        Me.fraChannelSelect.Controls.Add(Me.Label10)
        Me.fraChannelSelect.Location = New System.Drawing.Point(12, 12)
        Me.fraChannelSelect.Name = "fraChannelSelect"
        Me.fraChannelSelect.Size = New System.Drawing.Size(421, 539)
        Me.fraChannelSelect.TabIndex = 12
        Me.fraChannelSelect.TabStop = False
        Me.fraChannelSelect.Text = "ChannelSelect"
        '
        'grdCH
        '
        Me.grdCH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCH.Location = New System.Drawing.Point(18, 86)
        Me.grdCH.Name = "grdCH"
        Me.grdCH.RowTemplate.Height = 21
        Me.grdCH.Size = New System.Drawing.Size(380, 440)
        Me.grdCH.TabIndex = 2
        '
        'fra
        '
        Me.fra.Controls.Add(Me.fraAnalogDetail)
        Me.fra.Controls.Add(Me.fraIndicatorDetail)
        Me.fra.Controls.Add(Me.txtChTypeName)
        Me.fra.Controls.Add(Me.txtSelectChannel)
        Me.fra.Controls.Add(Me.fraGraphType)
        Me.fra.Controls.Add(Me.Label1)
        Me.fra.Controls.Add(Me.Label15)
        Me.fra.Location = New System.Drawing.Point(453, 12)
        Me.fra.Name = "fra"
        Me.fra.Size = New System.Drawing.Size(356, 540)
        Me.fra.TabIndex = 13
        Me.fra.TabStop = False
        Me.fra.Text = "Graph Set"
        '
        'fraAnalogDetail
        '
        Me.fraAnalogDetail.Controls.Add(Me.txtAnalogColor)
        Me.fraAnalogDetail.Controls.Add(Me.txtAnalogScale)
        Me.fraAnalogDetail.Controls.Add(Me.lblAnalogColor)
        Me.fraAnalogDetail.Controls.Add(Me.lblAnalogScale)
        Me.fraAnalogDetail.Location = New System.Drawing.Point(198, 18)
        Me.fraAnalogDetail.Name = "fraAnalogDetail"
        Me.fraAnalogDetail.Size = New System.Drawing.Size(134, 75)
        Me.fraAnalogDetail.TabIndex = 10
        Me.fraAnalogDetail.TabStop = False
        Me.fraAnalogDetail.Text = "Analog Detail"
        '
        'txtAnalogColor
        '
        Me.txtAnalogColor.AcceptsReturn = True
        Me.txtAnalogColor.Location = New System.Drawing.Point(70, 48)
        Me.txtAnalogColor.MaxLength = 0
        Me.txtAnalogColor.Name = "txtAnalogColor"
        Me.txtAnalogColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAnalogColor.Size = New System.Drawing.Size(49, 19)
        Me.txtAnalogColor.TabIndex = 16
        Me.txtAnalogColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAnalogScale
        '
        Me.txtAnalogScale.AcceptsReturn = True
        Me.txtAnalogScale.Location = New System.Drawing.Point(70, 23)
        Me.txtAnalogScale.MaxLength = 0
        Me.txtAnalogScale.Name = "txtAnalogScale"
        Me.txtAnalogScale.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAnalogScale.Size = New System.Drawing.Size(49, 19)
        Me.txtAnalogScale.TabIndex = 14
        Me.txtAnalogScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblAnalogColor
        '
        Me.lblAnalogColor.AutoSize = True
        Me.lblAnalogColor.BackColor = System.Drawing.SystemColors.Control
        Me.lblAnalogColor.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAnalogColor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAnalogColor.Location = New System.Drawing.Point(22, 52)
        Me.lblAnalogColor.Name = "lblAnalogColor"
        Me.lblAnalogColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAnalogColor.Size = New System.Drawing.Size(35, 12)
        Me.lblAnalogColor.TabIndex = 15
        Me.lblAnalogColor.Text = "Color"
        Me.lblAnalogColor.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAnalogScale
        '
        Me.lblAnalogScale.AutoSize = True
        Me.lblAnalogScale.BackColor = System.Drawing.SystemColors.Control
        Me.lblAnalogScale.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAnalogScale.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAnalogScale.Location = New System.Drawing.Point(22, 26)
        Me.lblAnalogScale.Name = "lblAnalogScale"
        Me.lblAnalogScale.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAnalogScale.Size = New System.Drawing.Size(35, 12)
        Me.lblAnalogScale.TabIndex = 14
        Me.lblAnalogScale.Text = "Scale"
        Me.lblAnalogScale.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtChTypeName
        '
        Me.txtChTypeName.AcceptsReturn = True
        Me.txtChTypeName.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.txtChTypeName.Location = New System.Drawing.Point(81, 53)
        Me.txtChTypeName.MaxLength = 0
        Me.txtChTypeName.Name = "txtChTypeName"
        Me.txtChTypeName.ReadOnly = True
        Me.txtChTypeName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChTypeName.Size = New System.Drawing.Size(96, 19)
        Me.txtChTypeName.TabIndex = 3
        Me.txtChTypeName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(82, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(47, 12)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "CH Type"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtChTypeCode
        '
        Me.txtChTypeCode.AcceptsReturn = True
        Me.txtChTypeCode.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.txtChTypeCode.Location = New System.Drawing.Point(101, 557)
        Me.txtChTypeCode.MaxLength = 0
        Me.txtChTypeCode.Name = "txtChTypeCode"
        Me.txtChTypeCode.ReadOnly = True
        Me.txtChTypeCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChTypeCode.Size = New System.Drawing.Size(71, 19)
        Me.txtChTypeCode.TabIndex = 3
        Me.txtChTypeCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtChTypeCode.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(24, 560)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(71, 12)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "CH Type No."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label2.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(24, 581)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(71, 12)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "CH DataType"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label3.Visible = False
        '
        'txtChDataType
        '
        Me.txtChDataType.AcceptsReturn = True
        Me.txtChDataType.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.txtChDataType.Location = New System.Drawing.Point(101, 578)
        Me.txtChDataType.MaxLength = 0
        Me.txtChDataType.Name = "txtChDataType"
        Me.txtChDataType.ReadOnly = True
        Me.txtChDataType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChDataType.Size = New System.Drawing.Size(71, 19)
        Me.txtChDataType.TabIndex = 3
        Me.txtChDataType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtChDataType.Visible = False
        '
        'frmOpsGraphFreeDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(827, 601)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmbDeviceStatus)
        Me.Controls.Add(Me.fra)
        Me.Controls.Add(Me.fraChannelSelect)
        Me.Controls.Add(Me.txtChDataType)
        Me.Controls.Add(Me.txtChTypeCode)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Label2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsGraphFreeDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FREE GRAPH DETAIL"
        Me.fraIndicatorDetail.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.fraBit.ResumeLayout(False)
        CType(Me.grdBit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraGraphType.ResumeLayout(False)
        Me.fraChannelSelect.ResumeLayout(False)
        Me.fraChannelSelect.PerformLayout()
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fra.ResumeLayout(False)
        Me.fra.PerformLayout()
        Me.fraAnalogDetail.ResumeLayout(False)
        Me.fraAnalogDetail.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents fraChannelSelect As System.Windows.Forms.GroupBox
    Friend WithEvents fra As System.Windows.Forms.GroupBox
    Public WithEvents txtChTypeCode As System.Windows.Forms.TextBox
    Public WithEvents txtChTypeName As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdCH As Editor.clsDataGridViewPlus
    Friend WithEvents fraAnalogDetail As System.Windows.Forms.GroupBox
    Public WithEvents lblAnalogColor As System.Windows.Forms.Label
    Public WithEvents lblAnalogScale As System.Windows.Forms.Label
    Public WithEvents txtAnalogColor As System.Windows.Forms.TextBox
    Public WithEvents txtAnalogScale As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtChDataType As System.Windows.Forms.TextBox
    Friend WithEvents grdBit As Editor.clsDataGridViewPlus
    Friend WithEvents cmbDeviceStatus As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optIndTypeSensor As System.Windows.Forms.RadioButton
    Friend WithEvents optIndTypeRepose As System.Windows.Forms.RadioButton
    Friend WithEvents optIndTypeNoSet As System.Windows.Forms.RadioButton
    Friend WithEvents optIndTypeAlarm As System.Windows.Forms.RadioButton
    Friend WithEvents optIndTypeData As System.Windows.Forms.RadioButton
#End Region

End Class
