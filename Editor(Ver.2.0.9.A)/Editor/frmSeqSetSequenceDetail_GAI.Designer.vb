﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSeqSetSequenceDetail_GAI
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

    Public WithEvents cmbFcuFuOutputType As System.Windows.Forms.ComboBox
    Public WithEvents txtFcuFuOutputTime As System.Windows.Forms.TextBox
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label113 As System.Windows.Forms.Label
    Public WithEvents Label111 As System.Windows.Forms.Label
    Public WithEvents Label110 As System.Windows.Forms.Label
    Public WithEvents fraFcuFuOutput As System.Windows.Forms.GroupBox
    Public WithEvents txtChOutputOffdelay As System.Windows.Forms.TextBox
    Public WithEvents cmbChOutputType As System.Windows.Forms.ComboBox
    Public WithEvents optChInvert As System.Windows.Forms.RadioButton
    Public WithEvents optChNonInvert As System.Windows.Forms.RadioButton
    Public WithEvents fraOutput As System.Windows.Forms.GroupBox
    Public WithEvents txtChOutputData As System.Windows.Forms.TextBox
    Public WithEvents txtChNo As System.Windows.Forms.TextBox
    Public WithEvents cmbChStatus As System.Windows.Forms.ComboBox
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents fraChOutput As System.Windows.Forms.GroupBox
    Public WithEvents fraOutputSet As System.Windows.Forms.GroupBox
    Public WithEvents cmdSelect As System.Windows.Forms.Button
    Public WithEvents txtLogic As System.Windows.Forms.TextBox
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents fraInputSet As System.Windows.Forms.GroupBox
    Public WithEvents txtSeqID As System.Windows.Forms.TextBox
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.fraOutputSet = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optDiscontinuance = New System.Windows.Forms.RadioButton()
        Me.optContinuance = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.fraFcuFuOutput = New System.Windows.Forms.GroupBox()
        Me.txtFcuFuTerminalNo = New System.Windows.Forms.TextBox()
        Me.txtFcuFuPortNo = New System.Windows.Forms.TextBox()
        Me.txtFcuFuFuNo = New System.Windows.Forms.TextBox()
        Me.cmbFcuFuOutputType = New System.Windows.Forms.ComboBox()
        Me.txtFcuFuOutputTime = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.fraChOutput = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optIOSelOutput = New System.Windows.Forms.RadioButton()
        Me.optIOSelInput = New System.Windows.Forms.RadioButton()
        Me.txtChOutputOffdelay = New System.Windows.Forms.TextBox()
        Me.cmbChOutputType = New System.Windows.Forms.ComboBox()
        Me.fraOutput = New System.Windows.Forms.GroupBox()
        Me.optChInvert = New System.Windows.Forms.RadioButton()
        Me.optChNonInvert = New System.Windows.Forms.RadioButton()
        Me.txtChOutputData = New System.Windows.Forms.TextBox()
        Me.txtChNo = New System.Windows.Forms.TextBox()
        Me.cmbChStatus = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.txtLogic = New System.Windows.Forms.TextBox()
        Me.fraInputSet = New System.Windows.Forms.GroupBox()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.txtSeqID = New System.Windows.Forms.TextBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtOutFU = New System.Windows.Forms.TextBox()
        Me.txtOutCh = New System.Windows.Forms.TextBox()
        Me.txtInCh08 = New System.Windows.Forms.TextBox()
        Me.txtInCh07 = New System.Windows.Forms.TextBox()
        Me.txtInCh06 = New System.Windows.Forms.TextBox()
        Me.txtInCh05 = New System.Windows.Forms.TextBox()
        Me.txtInCh04 = New System.Windows.Forms.TextBox()
        Me.txtInCh03 = New System.Windows.Forms.TextBox()
        Me.txtInCh02 = New System.Windows.Forms.TextBox()
        Me.txtInCH01 = New System.Windows.Forms.TextBox()
        Me.picLogic = New System.Windows.Forms.PictureBox()
        Me.cmdOperation = New System.Windows.Forms.Button()
        Me.cmdLinear = New System.Windows.Forms.Button()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.grdLogic = New Editor.clsDataGridViewPlus()
        Me.grdOutChInfo = New Editor.clsDataGridViewPlus()
        Me.grdInChInfo = New Editor.clsDataGridViewPlus()
        Me.grdCH = New Editor.clsDataGridViewPlus()
        Me.fraOutputSet.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.fraFcuFuOutput.SuspendLayout()
        Me.fraChOutput.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.fraOutput.SuspendLayout()
        Me.fraInputSet.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picLogic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLogic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOutChInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdInChInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fraOutputSet
        '
        Me.fraOutputSet.BackColor = System.Drawing.SystemColors.Control
        Me.fraOutputSet.Controls.Add(Me.GroupBox2)
        Me.fraOutputSet.Controls.Add(Me.fraFcuFuOutput)
        Me.fraOutputSet.Controls.Add(Me.fraChOutput)
        Me.fraOutputSet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraOutputSet.Location = New System.Drawing.Point(864, 41)
        Me.fraOutputSet.Name = "fraOutputSet"
        Me.fraOutputSet.Padding = New System.Windows.Forms.Padding(0)
        Me.fraOutputSet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraOutputSet.Size = New System.Drawing.Size(318, 570)
        Me.fraOutputSet.TabIndex = 10
        Me.fraOutputSet.TabStop = False
        Me.fraOutputSet.Text = "Output Set"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optDiscontinuance)
        Me.GroupBox2.Controls.Add(Me.optContinuance)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(17, 462)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(285, 99)
        Me.GroupBox2.TabIndex = 27
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Continuance"
        '
        'optDiscontinuance
        '
        Me.optDiscontinuance.AutoSize = True
        Me.optDiscontinuance.BackColor = System.Drawing.SystemColors.Control
        Me.optDiscontinuance.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDiscontinuance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDiscontinuance.Location = New System.Drawing.Point(149, 58)
        Me.optDiscontinuance.Name = "optDiscontinuance"
        Me.optDiscontinuance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDiscontinuance.Size = New System.Drawing.Size(47, 16)
        Me.optDiscontinuance.TabIndex = 22
        Me.optDiscontinuance.Text = "Stop"
        Me.optDiscontinuance.UseVisualStyleBackColor = True
        '
        'optContinuance
        '
        Me.optContinuance.AutoSize = True
        Me.optContinuance.BackColor = System.Drawing.SystemColors.Control
        Me.optContinuance.Checked = True
        Me.optContinuance.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContinuance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContinuance.Location = New System.Drawing.Point(42, 58)
        Me.optContinuance.Name = "optContinuance"
        Me.optContinuance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContinuance.Size = New System.Drawing.Size(71, 16)
        Me.optContinuance.TabIndex = 21
        Me.optContinuance.TabStop = True
        Me.optContinuance.Text = "Continue"
        Me.optContinuance.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(243, 53)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Whether to continue processing in case of Machinery Failure or Sensor Failure:"
        '
        'fraFcuFuOutput
        '
        Me.fraFcuFuOutput.BackColor = System.Drawing.SystemColors.Control
        Me.fraFcuFuOutput.Controls.Add(Me.txtFcuFuTerminalNo)
        Me.fraFcuFuOutput.Controls.Add(Me.txtFcuFuPortNo)
        Me.fraFcuFuOutput.Controls.Add(Me.txtFcuFuFuNo)
        Me.fraFcuFuOutput.Controls.Add(Me.cmbFcuFuOutputType)
        Me.fraFcuFuOutput.Controls.Add(Me.txtFcuFuOutputTime)
        Me.fraFcuFuOutput.Controls.Add(Me.Label18)
        Me.fraFcuFuOutput.Controls.Add(Me.Label113)
        Me.fraFcuFuOutput.Controls.Add(Me.Label111)
        Me.fraFcuFuOutput.Controls.Add(Me.Label110)
        Me.fraFcuFuOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraFcuFuOutput.Location = New System.Drawing.Point(17, 320)
        Me.fraFcuFuOutput.Name = "fraFcuFuOutput"
        Me.fraFcuFuOutput.Padding = New System.Windows.Forms.Padding(0)
        Me.fraFcuFuOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraFcuFuOutput.Size = New System.Drawing.Size(285, 136)
        Me.fraFcuFuOutput.TabIndex = 26
        Me.fraFcuFuOutput.TabStop = False
        Me.fraFcuFuOutput.Text = "FCU/FU Output"
        '
        'txtFcuFuTerminalNo
        '
        Me.txtFcuFuTerminalNo.AcceptsReturn = True
        Me.txtFcuFuTerminalNo.Location = New System.Drawing.Point(179, 24)
        Me.txtFcuFuTerminalNo.MaxLength = 0
        Me.txtFcuFuTerminalNo.Name = "txtFcuFuTerminalNo"
        Me.txtFcuFuTerminalNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFcuFuTerminalNo.Size = New System.Drawing.Size(40, 19)
        Me.txtFcuFuTerminalNo.TabIndex = 111
        '
        'txtFcuFuPortNo
        '
        Me.txtFcuFuPortNo.AcceptsReturn = True
        Me.txtFcuFuPortNo.Location = New System.Drawing.Point(137, 24)
        Me.txtFcuFuPortNo.MaxLength = 0
        Me.txtFcuFuPortNo.Name = "txtFcuFuPortNo"
        Me.txtFcuFuPortNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFcuFuPortNo.Size = New System.Drawing.Size(40, 19)
        Me.txtFcuFuPortNo.TabIndex = 110
        '
        'txtFcuFuFuNo
        '
        Me.txtFcuFuFuNo.AcceptsReturn = True
        Me.txtFcuFuFuNo.Location = New System.Drawing.Point(95, 24)
        Me.txtFcuFuFuNo.MaxLength = 0
        Me.txtFcuFuFuNo.Name = "txtFcuFuFuNo"
        Me.txtFcuFuFuNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFcuFuFuNo.Size = New System.Drawing.Size(40, 19)
        Me.txtFcuFuFuNo.TabIndex = 109
        '
        'cmbFcuFuOutputType
        '
        Me.cmbFcuFuOutputType.BackColor = System.Drawing.SystemColors.Window
        Me.cmbFcuFuOutputType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbFcuFuOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFcuFuOutputType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbFcuFuOutputType.Location = New System.Drawing.Point(95, 56)
        Me.cmbFcuFuOutputType.Name = "cmbFcuFuOutputType"
        Me.cmbFcuFuOutputType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbFcuFuOutputType.Size = New System.Drawing.Size(128, 20)
        Me.cmbFcuFuOutputType.TabIndex = 39
        '
        'txtFcuFuOutputTime
        '
        Me.txtFcuFuOutputTime.AcceptsReturn = True
        Me.txtFcuFuOutputTime.Location = New System.Drawing.Point(95, 88)
        Me.txtFcuFuOutputTime.MaxLength = 0
        Me.txtFcuFuOutputTime.Name = "txtFcuFuOutputTime"
        Me.txtFcuFuOutputTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFcuFuOutputTime.Size = New System.Drawing.Size(99, 19)
        Me.txtFcuFuOutputTime.TabIndex = 32
        Me.txtFcuFuOutputTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(17, 27)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(65, 12)
        Me.Label18.TabIndex = 38
        Me.Label18.Text = "FU Address"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.BackColor = System.Drawing.SystemColors.Control
        Me.Label113.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label113.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label113.Location = New System.Drawing.Point(200, 92)
        Me.Label113.Name = "Label113"
        Me.Label113.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label113.Size = New System.Drawing.Size(71, 12)
        Me.Label113.TabIndex = 34
        Me.Label113.Text = "*100 [msec]"
        Me.Label113.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.BackColor = System.Drawing.SystemColors.Control
        Me.Label111.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label111.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label111.Location = New System.Drawing.Point(13, 83)
        Me.Label111.Name = "Label111"
        Me.Label111.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label111.Size = New System.Drawing.Size(71, 24)
        Me.Label111.TabIndex = 33
        Me.Label111.Text = "One Shot" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Output Time"
        Me.Label111.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label110
        '
        Me.Label110.AutoSize = True
        Me.Label110.BackColor = System.Drawing.SystemColors.Control
        Me.Label110.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label110.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label110.Location = New System.Drawing.Point(15, 58)
        Me.Label110.Name = "Label110"
        Me.Label110.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label110.Size = New System.Drawing.Size(71, 12)
        Me.Label110.TabIndex = 31
        Me.Label110.Text = "Output Type"
        Me.Label110.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraChOutput
        '
        Me.fraChOutput.BackColor = System.Drawing.SystemColors.Control
        Me.fraChOutput.Controls.Add(Me.grdOutChInfo)
        Me.fraChOutput.Controls.Add(Me.Panel1)
        Me.fraChOutput.Controls.Add(Me.txtChOutputOffdelay)
        Me.fraChOutput.Controls.Add(Me.cmbChOutputType)
        Me.fraChOutput.Controls.Add(Me.fraOutput)
        Me.fraChOutput.Controls.Add(Me.txtChOutputData)
        Me.fraChOutput.Controls.Add(Me.txtChNo)
        Me.fraChOutput.Controls.Add(Me.cmbChStatus)
        Me.fraChOutput.Controls.Add(Me.Label17)
        Me.fraChOutput.Controls.Add(Me.Label16)
        Me.fraChOutput.Controls.Add(Me.Label15)
        Me.fraChOutput.Controls.Add(Me.Label14)
        Me.fraChOutput.Controls.Add(Me.Label13)
        Me.fraChOutput.Controls.Add(Me.Label12)
        Me.fraChOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraChOutput.Location = New System.Drawing.Point(16, 24)
        Me.fraChOutput.Name = "fraChOutput"
        Me.fraChOutput.Padding = New System.Windows.Forms.Padding(0)
        Me.fraChOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraChOutput.Size = New System.Drawing.Size(286, 290)
        Me.fraChOutput.TabIndex = 11
        Me.fraChOutput.TabStop = False
        Me.fraChOutput.Text = "CH Output"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.optIOSelOutput)
        Me.Panel1.Controls.Add(Me.optIOSelInput)
        Me.Panel1.Location = New System.Drawing.Point(123, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(134, 21)
        Me.Panel1.TabIndex = 27
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
        'txtChOutputOffdelay
        '
        Me.txtChOutputOffdelay.AcceptsReturn = True
        Me.txtChOutputOffdelay.Location = New System.Drawing.Point(110, 218)
        Me.txtChOutputOffdelay.MaxLength = 0
        Me.txtChOutputOffdelay.Name = "txtChOutputOffdelay"
        Me.txtChOutputOffdelay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChOutputOffdelay.Size = New System.Drawing.Size(99, 19)
        Me.txtChOutputOffdelay.TabIndex = 23
        Me.txtChOutputOffdelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbChOutputType
        '
        Me.cmbChOutputType.BackColor = System.Drawing.SystemColors.Window
        Me.cmbChOutputType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbChOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChOutputType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbChOutputType.Location = New System.Drawing.Point(110, 185)
        Me.cmbChOutputType.Name = "cmbChOutputType"
        Me.cmbChOutputType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbChOutputType.Size = New System.Drawing.Size(128, 20)
        Me.cmbChOutputType.TabIndex = 21
        '
        'fraOutput
        '
        Me.fraOutput.BackColor = System.Drawing.SystemColors.Control
        Me.fraOutput.Controls.Add(Me.optChInvert)
        Me.fraOutput.Controls.Add(Me.optChNonInvert)
        Me.fraOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraOutput.Location = New System.Drawing.Point(68, 247)
        Me.fraOutput.Name = "fraOutput"
        Me.fraOutput.Padding = New System.Windows.Forms.Padding(0)
        Me.fraOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraOutput.Size = New System.Drawing.Size(176, 40)
        Me.fraOutput.TabIndex = 18
        Me.fraOutput.TabStop = False
        Me.fraOutput.Text = "Output"
        '
        'optChInvert
        '
        Me.optChInvert.AutoSize = True
        Me.optChInvert.BackColor = System.Drawing.SystemColors.Control
        Me.optChInvert.Cursor = System.Windows.Forms.Cursors.Default
        Me.optChInvert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optChInvert.Location = New System.Drawing.Point(116, 16)
        Me.optChInvert.Name = "optChInvert"
        Me.optChInvert.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optChInvert.Size = New System.Drawing.Size(59, 16)
        Me.optChInvert.TabIndex = 20
        Me.optChInvert.Text = "invert"
        Me.optChInvert.UseVisualStyleBackColor = True
        '
        'optChNonInvert
        '
        Me.optChNonInvert.AutoSize = True
        Me.optChNonInvert.BackColor = System.Drawing.SystemColors.Control
        Me.optChNonInvert.Checked = True
        Me.optChNonInvert.Cursor = System.Windows.Forms.Cursors.Default
        Me.optChNonInvert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optChNonInvert.Location = New System.Drawing.Point(20, 16)
        Me.optChNonInvert.Name = "optChNonInvert"
        Me.optChNonInvert.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optChNonInvert.Size = New System.Drawing.Size(83, 16)
        Me.optChNonInvert.TabIndex = 19
        Me.optChNonInvert.TabStop = True
        Me.optChNonInvert.Text = "non invert"
        Me.optChNonInvert.UseVisualStyleBackColor = True
        '
        'txtChOutputData
        '
        Me.txtChOutputData.AcceptsReturn = True
        Me.txtChOutputData.Location = New System.Drawing.Point(110, 153)
        Me.txtChOutputData.MaxLength = 0
        Me.txtChOutputData.Name = "txtChOutputData"
        Me.txtChOutputData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChOutputData.Size = New System.Drawing.Size(99, 19)
        Me.txtChOutputData.TabIndex = 16
        Me.txtChOutputData.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtChNo
        '
        Me.txtChNo.AcceptsReturn = True
        Me.txtChNo.Location = New System.Drawing.Point(58, 24)
        Me.txtChNo.MaxLength = 0
        Me.txtChNo.Name = "txtChNo"
        Me.txtChNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChNo.Size = New System.Drawing.Size(56, 19)
        Me.txtChNo.TabIndex = 13
        Me.txtChNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbChStatus
        '
        Me.cmbChStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbChStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbChStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbChStatus.Location = New System.Drawing.Point(110, 120)
        Me.cmbChStatus.Name = "cmbChStatus"
        Me.cmbChStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbChStatus.Size = New System.Drawing.Size(128, 20)
        Me.cmbChStatus.TabIndex = 12
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(212, 221)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(71, 12)
        Me.Label17.TabIndex = 25
        Me.Label17.Text = "*100 [msec]"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(8, 221)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(95, 12)
        Me.Label16.TabIndex = 24
        Me.Label16.Text = "Output Offdelay"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(8, 189)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(71, 12)
        Me.Label15.TabIndex = 22
        Me.Label15.Text = "Output Type"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(8, 156)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(71, 12)
        Me.Label14.TabIndex = 17
        Me.Label14.Text = "Output Data"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(8, 27)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(35, 12)
        Me.Label13.TabIndex = 15
        Me.Label13.Text = "CH No"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(8, 123)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(41, 12)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Status"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdSelect
        '
        Me.cmdSelect.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelect.Location = New System.Drawing.Point(353, 21)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSelect.Size = New System.Drawing.Size(49, 25)
        Me.cmdSelect.TabIndex = 9
        Me.cmdSelect.Text = "Select"
        Me.cmdSelect.UseVisualStyleBackColor = True
        '
        'txtLogic
        '
        Me.txtLogic.AcceptsReturn = True
        Me.txtLogic.Location = New System.Drawing.Point(52, 24)
        Me.txtLogic.MaxLength = 0
        Me.txtLogic.Name = "txtLogic"
        Me.txtLogic.ReadOnly = True
        Me.txtLogic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLogic.Size = New System.Drawing.Size(297, 19)
        Me.txtLogic.TabIndex = 7
        '
        'fraInputSet
        '
        Me.fraInputSet.BackColor = System.Drawing.SystemColors.Control
        Me.fraInputSet.Controls.Add(Me.grdInChInfo)
        Me.fraInputSet.Controls.Add(Me.grdCH)
        Me.fraInputSet.Controls.Add(Me.cmdDelete)
        Me.fraInputSet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraInputSet.Location = New System.Drawing.Point(12, 43)
        Me.fraInputSet.Name = "fraInputSet"
        Me.fraInputSet.Padding = New System.Windows.Forms.Padding(0)
        Me.fraInputSet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraInputSet.Size = New System.Drawing.Size(430, 506)
        Me.fraInputSet.TabIndex = 4
        Me.fraInputSet.TabStop = False
        Me.fraInputSet.Text = "Input Set"
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Location = New System.Drawing.Point(301, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(113, 33)
        Me.cmdDelete.TabIndex = 6
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'txtSeqID
        '
        Me.txtSeqID.AcceptsReturn = True
        Me.txtSeqID.BackColor = System.Drawing.SystemColors.Control
        Me.txtSeqID.Location = New System.Drawing.Point(96, 16)
        Me.txtSeqID.MaxLength = 0
        Me.txtSeqID.Name = "txtSeqID"
        Me.txtSeqID.ReadOnly = True
        Me.txtSeqID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeqID.Size = New System.Drawing.Size(83, 19)
        Me.txtSeqID.TabIndex = 2
        Me.txtSeqID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(945, 626)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(1069, 626)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 0
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(11, 26)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(35, 12)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Logic"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(16, 19)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(71, 12)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Sequence ID"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtOutFU)
        Me.GroupBox1.Controls.Add(Me.txtOutCh)
        Me.GroupBox1.Controls.Add(Me.txtInCh08)
        Me.GroupBox1.Controls.Add(Me.txtInCh07)
        Me.GroupBox1.Controls.Add(Me.txtInCh06)
        Me.GroupBox1.Controls.Add(Me.txtInCh05)
        Me.GroupBox1.Controls.Add(Me.txtInCh04)
        Me.GroupBox1.Controls.Add(Me.txtInCh03)
        Me.GroupBox1.Controls.Add(Me.txtInCh02)
        Me.GroupBox1.Controls.Add(Me.txtInCH01)
        Me.GroupBox1.Controls.Add(Me.picLogic)
        Me.GroupBox1.Controls.Add(Me.cmdOperation)
        Me.GroupBox1.Controls.Add(Me.txtLogic)
        Me.GroupBox1.Controls.Add(Me.grdLogic)
        Me.GroupBox1.Controls.Add(Me.cmdLinear)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cmdSelect)
        Me.GroupBox1.Location = New System.Drawing.Point(448, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(410, 568)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Logic Set"
        '
        'txtOutFU
        '
        Me.txtOutFU.AcceptsReturn = True
        Me.txtOutFU.BackColor = System.Drawing.SystemColors.Control
        Me.txtOutFU.Location = New System.Drawing.Point(340, 230)
        Me.txtOutFU.MaxLength = 0
        Me.txtOutFU.Name = "txtOutFU"
        Me.txtOutFU.ReadOnly = True
        Me.txtOutFU.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOutFU.Size = New System.Drawing.Size(62, 19)
        Me.txtOutFU.TabIndex = 26
        Me.txtOutFU.Text = "20-99-999"
        Me.txtOutFU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtOutCh
        '
        Me.txtOutCh.AcceptsReturn = True
        Me.txtOutCh.BackColor = System.Drawing.SystemColors.Control
        Me.txtOutCh.Location = New System.Drawing.Point(340, 205)
        Me.txtOutCh.MaxLength = 0
        Me.txtOutCh.Name = "txtOutCh"
        Me.txtOutCh.ReadOnly = True
        Me.txtOutCh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOutCh.Size = New System.Drawing.Size(40, 19)
        Me.txtOutCh.TabIndex = 25
        Me.txtOutCh.Text = "1101"
        Me.txtOutCh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInCh08
        '
        Me.txtInCh08.AcceptsReturn = True
        Me.txtInCh08.BackColor = System.Drawing.SystemColors.Control
        Me.txtInCh08.Location = New System.Drawing.Point(7, 342)
        Me.txtInCh08.MaxLength = 0
        Me.txtInCh08.Name = "txtInCh08"
        Me.txtInCh08.ReadOnly = True
        Me.txtInCh08.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInCh08.Size = New System.Drawing.Size(40, 19)
        Me.txtInCh08.TabIndex = 24
        Me.txtInCh08.Text = "0801"
        Me.txtInCh08.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInCh07
        '
        Me.txtInCh07.AcceptsReturn = True
        Me.txtInCh07.BackColor = System.Drawing.SystemColors.Control
        Me.txtInCh07.Location = New System.Drawing.Point(7, 305)
        Me.txtInCh07.MaxLength = 0
        Me.txtInCh07.Name = "txtInCh07"
        Me.txtInCh07.ReadOnly = True
        Me.txtInCh07.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInCh07.Size = New System.Drawing.Size(40, 19)
        Me.txtInCh07.TabIndex = 23
        Me.txtInCh07.Text = "0701"
        Me.txtInCh07.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInCh06
        '
        Me.txtInCh06.AcceptsReturn = True
        Me.txtInCh06.BackColor = System.Drawing.SystemColors.Control
        Me.txtInCh06.Location = New System.Drawing.Point(7, 270)
        Me.txtInCh06.MaxLength = 0
        Me.txtInCh06.Name = "txtInCh06"
        Me.txtInCh06.ReadOnly = True
        Me.txtInCh06.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInCh06.Size = New System.Drawing.Size(40, 19)
        Me.txtInCh06.TabIndex = 22
        Me.txtInCh06.Text = "0601"
        Me.txtInCh06.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInCh05
        '
        Me.txtInCh05.AcceptsReturn = True
        Me.txtInCh05.BackColor = System.Drawing.SystemColors.Control
        Me.txtInCh05.Location = New System.Drawing.Point(7, 236)
        Me.txtInCh05.MaxLength = 0
        Me.txtInCh05.Name = "txtInCh05"
        Me.txtInCh05.ReadOnly = True
        Me.txtInCh05.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInCh05.Size = New System.Drawing.Size(40, 19)
        Me.txtInCh05.TabIndex = 21
        Me.txtInCh05.Text = "0501"
        Me.txtInCh05.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInCh04
        '
        Me.txtInCh04.AcceptsReturn = True
        Me.txtInCh04.BackColor = System.Drawing.SystemColors.Control
        Me.txtInCh04.Location = New System.Drawing.Point(6, 204)
        Me.txtInCh04.MaxLength = 0
        Me.txtInCh04.Name = "txtInCh04"
        Me.txtInCh04.ReadOnly = True
        Me.txtInCh04.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInCh04.Size = New System.Drawing.Size(40, 19)
        Me.txtInCh04.TabIndex = 20
        Me.txtInCh04.Text = "0401"
        Me.txtInCh04.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInCh03
        '
        Me.txtInCh03.AcceptsReturn = True
        Me.txtInCh03.BackColor = System.Drawing.SystemColors.Control
        Me.txtInCh03.Location = New System.Drawing.Point(6, 171)
        Me.txtInCh03.MaxLength = 0
        Me.txtInCh03.Name = "txtInCh03"
        Me.txtInCh03.ReadOnly = True
        Me.txtInCh03.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInCh03.Size = New System.Drawing.Size(40, 19)
        Me.txtInCh03.TabIndex = 19
        Me.txtInCh03.Text = "0301"
        Me.txtInCh03.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInCh02
        '
        Me.txtInCh02.AcceptsReturn = True
        Me.txtInCh02.BackColor = System.Drawing.SystemColors.Control
        Me.txtInCh02.Location = New System.Drawing.Point(6, 135)
        Me.txtInCh02.MaxLength = 0
        Me.txtInCh02.Name = "txtInCh02"
        Me.txtInCh02.ReadOnly = True
        Me.txtInCh02.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInCh02.Size = New System.Drawing.Size(40, 19)
        Me.txtInCh02.TabIndex = 18
        Me.txtInCh02.Text = "0201"
        Me.txtInCh02.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInCH01
        '
        Me.txtInCH01.AcceptsReturn = True
        Me.txtInCH01.BackColor = System.Drawing.SystemColors.Control
        Me.txtInCH01.Location = New System.Drawing.Point(6, 100)
        Me.txtInCH01.MaxLength = 0
        Me.txtInCH01.Name = "txtInCH01"
        Me.txtInCH01.ReadOnly = True
        Me.txtInCH01.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInCH01.Size = New System.Drawing.Size(40, 19)
        Me.txtInCH01.TabIndex = 17
        Me.txtInCH01.Text = "0101"
        Me.txtInCH01.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'picLogic
        '
        Me.picLogic.Location = New System.Drawing.Point(52, 49)
        Me.picLogic.Name = "picLogic"
        Me.picLogic.Size = New System.Drawing.Size(282, 355)
        Me.picLogic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picLogic.TabIndex = 16
        Me.picLogic.TabStop = False
        '
        'cmdOperation
        '
        Me.cmdOperation.Location = New System.Drawing.Point(303, 529)
        Me.cmdOperation.Name = "cmdOperation"
        Me.cmdOperation.Size = New System.Drawing.Size(86, 25)
        Me.cmdOperation.TabIndex = 15
        Me.cmdOperation.Text = "Operation"
        Me.cmdOperation.UseVisualStyleBackColor = True
        '
        'cmdLinear
        '
        Me.cmdLinear.Location = New System.Drawing.Point(211, 529)
        Me.cmdLinear.Name = "cmdLinear"
        Me.cmdLinear.Size = New System.Drawing.Size(86, 25)
        Me.cmdLinear.TabIndex = 14
        Me.cmdLinear.Text = "Linear Table"
        Me.cmdLinear.UseVisualStyleBackColor = True
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(250, 16)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(123, 19)
        Me.txtRemarks.TabIndex = 0
        Me.txtRemarks.Text = "1234567890123456"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(197, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(47, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Remarks"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatus.Location = New System.Drawing.Point(429, 11)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatus.Size = New System.Drawing.Size(72, 20)
        Me.cmbStatus.TabIndex = 113
        Me.cmbStatus.Visible = False
        '
        'cmbUnit
        '
        Me.cmbUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbUnit.Location = New System.Drawing.Point(507, 11)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbUnit.Size = New System.Drawing.Size(72, 20)
        Me.cmbUnit.TabIndex = 116
        Me.cmbUnit.Visible = False
        '
        'grdLogic
        '
        Me.grdLogic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdLogic.Location = New System.Drawing.Point(9, 410)
        Me.grdLogic.Name = "grdLogic"
        Me.grdLogic.RowTemplate.Height = 21
        Me.grdLogic.Size = New System.Drawing.Size(380, 108)
        Me.grdLogic.TabIndex = 12
        '
        'grdOutChInfo
        '
        Me.grdOutChInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdOutChInfo.Location = New System.Drawing.Point(10, 49)
        Me.grdOutChInfo.Name = "grdOutChInfo"
        Me.grdOutChInfo.RowTemplate.Height = 21
        Me.grdOutChInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdOutChInfo.Size = New System.Drawing.Size(262, 65)
        Me.grdOutChInfo.TabIndex = 28
        '
        'grdInChInfo
        '
        Me.grdInChInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdInChInfo.Location = New System.Drawing.Point(6, 244)
        Me.grdInChInfo.Name = "grdInChInfo"
        Me.grdInChInfo.RowTemplate.Height = 21
        Me.grdInChInfo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.grdInChInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdInChInfo.Size = New System.Drawing.Size(408, 210)
        Me.grdInChInfo.TabIndex = 14
        '
        'grdCH
        '
        Me.grdCH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCH.Location = New System.Drawing.Point(6, 51)
        Me.grdCH.MultiSelect = False
        Me.grdCH.Name = "grdCH"
        Me.grdCH.RowTemplate.Height = 21
        Me.grdCH.Size = New System.Drawing.Size(408, 187)
        Me.grdCH.TabIndex = 13
        '
        'frmSeqSetSequenceDetail_GAI
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(1194, 671)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmbUnit)
        Me.Controls.Add(Me.cmbStatus)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.fraOutputSet)
        Me.Controls.Add(Me.fraInputSet)
        Me.Controls.Add(Me.txtSeqID)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label11)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSeqSetSequenceDetail_GAI"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CONTROL SEQUENCE SET"
        Me.fraOutputSet.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.fraFcuFuOutput.ResumeLayout(False)
        Me.fraFcuFuOutput.PerformLayout()
        Me.fraChOutput.ResumeLayout(False)
        Me.fraChOutput.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.fraOutput.ResumeLayout(False)
        Me.fraOutput.PerformLayout()
        Me.fraInputSet.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picLogic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLogic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOutChInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdInChInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents txtFcuFuTerminalNo As System.Windows.Forms.TextBox
    Public WithEvents txtFcuFuPortNo As System.Windows.Forms.TextBox
    Public WithEvents txtFcuFuFuNo As System.Windows.Forms.TextBox
    Friend WithEvents grdLogic As Editor.clsDataGridViewPlus
    Friend WithEvents grdCH As Editor.clsDataGridViewPlus
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents optIOSelOutput As System.Windows.Forms.RadioButton
    Friend WithEvents optIOSelInput As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents optDiscontinuance As System.Windows.Forms.RadioButton
    Public WithEvents optContinuance As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdLinear As System.Windows.Forms.Button
    Friend WithEvents cmdOperation As System.Windows.Forms.Button
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents picLogic As System.Windows.Forms.PictureBox
    Friend WithEvents grdOutChInfo As Editor.clsDataGridViewPlus
    Friend WithEvents grdInChInfo As Editor.clsDataGridViewPlus
    Public WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Public WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Public WithEvents txtOutFU As System.Windows.Forms.TextBox
    Public WithEvents txtOutCh As System.Windows.Forms.TextBox
    Public WithEvents txtInCh08 As System.Windows.Forms.TextBox
    Public WithEvents txtInCh07 As System.Windows.Forms.TextBox
    Public WithEvents txtInCh06 As System.Windows.Forms.TextBox
    Public WithEvents txtInCh05 As System.Windows.Forms.TextBox
    Public WithEvents txtInCh04 As System.Windows.Forms.TextBox
    Public WithEvents txtInCh03 As System.Windows.Forms.TextBox
    Public WithEvents txtInCh02 As System.Windows.Forms.TextBox
    Public WithEvents txtInCH01 As System.Windows.Forms.TextBox
#End Region

End Class
