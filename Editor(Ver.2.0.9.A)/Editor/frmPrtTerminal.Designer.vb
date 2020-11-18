<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrtTerminal
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

    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents chkSecretChannel As System.Windows.Forms.CheckBox
    Public WithEvents chkDummyData As System.Windows.Forms.CheckBox
    Public WithEvents fraOption As System.Windows.Forms.GroupBox
    Public WithEvents cmbPageRangeTo As System.Windows.Forms.ComboBox
    Public WithEvents cmbPageRangeFrom As System.Windows.Forms.ComboBox
    Public WithEvents optPageRangePages As System.Windows.Forms.RadioButton
    Public WithEvents optPageRangeAll As System.Windows.Forms.RadioButton
    Public WithEvents lblTo As System.Windows.Forms.Label
    Public WithEvents lblFrom As System.Windows.Forms.Label
    Public WithEvents fraPageRange As System.Windows.Forms.GroupBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.fraOption = New System.Windows.Forms.GroupBox()
        Me.chkExpTxtEtoJ = New System.Windows.Forms.CheckBox()
        Me.chkTerDIMsg = New System.Windows.Forms.CheckBox()
        Me.chkTerVer = New System.Windows.Forms.CheckBox()
        Me.chkRangePrint = New System.Windows.Forms.CheckBox()
        Me.chkFormatType = New System.Windows.Forms.CheckBox()
        Me.chkFuName = New System.Windows.Forms.CheckBox()
        Me.chkPagePrint = New System.Windows.Forms.CheckBox()
        Me.chkSecretChannel = New System.Windows.Forms.CheckBox()
        Me.chkDummyData = New System.Windows.Forms.CheckBox()
        Me.fraPageRange = New System.Windows.Forms.GroupBox()
        Me.cmbDrawTo = New System.Windows.Forms.ComboBox()
        Me.cmbDrawFrom = New System.Windows.Forms.ComboBox()
        Me.optDrawNo = New System.Windows.Forms.RadioButton()
        Me.lblDrawTo = New System.Windows.Forms.Label()
        Me.lblDrawFrom = New System.Windows.Forms.Label()
        Me.cmbPortTo = New System.Windows.Forms.ComboBox()
        Me.cmbFuTo = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbPort = New System.Windows.Forms.ComboBox()
        Me.cmbFU = New System.Windows.Forms.ComboBox()
        Me.optPageRangeFuAdr = New System.Windows.Forms.RadioButton()
        Me.cmbPageRangeTo = New System.Windows.Forms.ComboBox()
        Me.cmbPageRangeFrom = New System.Windows.Forms.ComboBox()
        Me.optPageRangePages = New System.Windows.Forms.RadioButton()
        Me.optPageRangeAll = New System.Windows.Forms.RadioButton()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblPrinter = New System.Windows.Forms.TextBox()
        Me.fraOption.SuspendLayout()
        Me.fraPageRange.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(296, 520)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(81, 33)
        Me.cmdExit.TabIndex = 5
        Me.cmdExit.Text = "EXIT"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(296, 481)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(81, 33)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "PRINT"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Location = New System.Drawing.Point(210, 481)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(81, 33)
        Me.cmdPreview.TabIndex = 2
        Me.cmdPreview.Text = "PREVIEW"
        Me.cmdPreview.UseVisualStyleBackColor = True
        '
        'fraOption
        '
        Me.fraOption.BackColor = System.Drawing.SystemColors.Control
        Me.fraOption.Controls.Add(Me.chkExpTxtEtoJ)
        Me.fraOption.Controls.Add(Me.chkTerDIMsg)
        Me.fraOption.Controls.Add(Me.chkTerVer)
        Me.fraOption.Controls.Add(Me.chkRangePrint)
        Me.fraOption.Controls.Add(Me.chkFormatType)
        Me.fraOption.Controls.Add(Me.chkFuName)
        Me.fraOption.Controls.Add(Me.chkPagePrint)
        Me.fraOption.Controls.Add(Me.chkSecretChannel)
        Me.fraOption.Controls.Add(Me.chkDummyData)
        Me.fraOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraOption.Location = New System.Drawing.Point(22, 268)
        Me.fraOption.Name = "fraOption"
        Me.fraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.fraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraOption.Size = New System.Drawing.Size(354, 207)
        Me.fraOption.TabIndex = 1
        Me.fraOption.TabStop = False
        Me.fraOption.Text = "OPTION"
        '
        'chkExpTxtEtoJ
        '
        Me.chkExpTxtEtoJ.AutoSize = True
        Me.chkExpTxtEtoJ.Location = New System.Drawing.Point(8, 182)
        Me.chkExpTxtEtoJ.Name = "chkExpTxtEtoJ"
        Me.chkExpTxtEtoJ.Size = New System.Drawing.Size(192, 16)
        Me.chkExpTxtEtoJ.TabIndex = 63
        Me.chkExpTxtEtoJ.Text = "Explanatory English-Japanese"
        Me.chkExpTxtEtoJ.UseVisualStyleBackColor = True
        '
        'chkTerDIMsg
        '
        Me.chkTerDIMsg.AutoSize = True
        Me.chkTerDIMsg.BackColor = System.Drawing.SystemColors.Control
        Me.chkTerDIMsg.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTerDIMsg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTerDIMsg.Location = New System.Drawing.Point(8, 160)
        Me.chkTerDIMsg.Name = "chkTerDIMsg"
        Me.chkTerDIMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTerDIMsg.Size = New System.Drawing.Size(150, 16)
        Me.chkTerDIMsg.TabIndex = 62
        Me.chkTerDIMsg.Text = "Term DI Message Print"
        Me.chkTerDIMsg.UseVisualStyleBackColor = True
        '
        'chkTerVer
        '
        Me.chkTerVer.AutoSize = True
        Me.chkTerVer.BackColor = System.Drawing.SystemColors.Control
        Me.chkTerVer.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTerVer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTerVer.Location = New System.Drawing.Point(8, 138)
        Me.chkTerVer.Name = "chkTerVer"
        Me.chkTerVer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTerVer.Size = New System.Drawing.Size(132, 16)
        Me.chkTerVer.TabIndex = 61
        Me.chkTerVer.Text = "Term Ver Not Print"
        Me.chkTerVer.UseVisualStyleBackColor = True
        '
        'chkRangePrint
        '
        Me.chkRangePrint.AutoSize = True
        Me.chkRangePrint.BackColor = System.Drawing.SystemColors.Control
        Me.chkRangePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRangePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRangePrint.Location = New System.Drawing.Point(8, 116)
        Me.chkRangePrint.Name = "chkRangePrint"
        Me.chkRangePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRangePrint.Size = New System.Drawing.Size(114, 16)
        Me.chkRangePrint.TabIndex = 5
        Me.chkRangePrint.Text = "Range Not Print"
        Me.chkRangePrint.UseVisualStyleBackColor = True
        '
        'chkFormatType
        '
        Me.chkFormatType.AutoSize = True
        Me.chkFormatType.BackColor = System.Drawing.SystemColors.Control
        Me.chkFormatType.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFormatType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFormatType.Location = New System.Drawing.Point(8, 94)
        Me.chkFormatType.Name = "chkFormatType"
        Me.chkFormatType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFormatType.Size = New System.Drawing.Size(180, 16)
        Me.chkFormatType.TabIndex = 4
        Me.chkFormatType.Text = "Formats DrawingNo 3 digits"
        Me.chkFormatType.UseVisualStyleBackColor = True
        '
        'chkFuName
        '
        Me.chkFuName.AutoSize = True
        Me.chkFuName.BackColor = System.Drawing.SystemColors.Control
        Me.chkFuName.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFuName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFuName.Location = New System.Drawing.Point(167, 116)
        Me.chkFuName.Name = "chkFuName"
        Me.chkFuName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFuName.Size = New System.Drawing.Size(174, 16)
        Me.chkFuName.TabIndex = 3
        Me.chkFuName.Text = "FIELD UNIT NAME(Japanese)"
        Me.chkFuName.UseVisualStyleBackColor = True
        Me.chkFuName.Visible = False
        '
        'chkPagePrint
        '
        Me.chkPagePrint.AutoSize = True
        Me.chkPagePrint.BackColor = System.Drawing.SystemColors.Control
        Me.chkPagePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPagePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPagePrint.Location = New System.Drawing.Point(8, 72)
        Me.chkPagePrint.Name = "chkPagePrint"
        Me.chkPagePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPagePrint.Size = New System.Drawing.Size(174, 16)
        Me.chkPagePrint.TabIndex = 2
        Me.chkPagePrint.Text = "Prints including Page No."
        Me.chkPagePrint.UseVisualStyleBackColor = True
        '
        'chkSecretChannel
        '
        Me.chkSecretChannel.AutoSize = True
        Me.chkSecretChannel.BackColor = System.Drawing.SystemColors.Control
        Me.chkSecretChannel.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSecretChannel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSecretChannel.Location = New System.Drawing.Point(8, 24)
        Me.chkSecretChannel.Name = "chkSecretChannel"
        Me.chkSecretChannel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSecretChannel.Size = New System.Drawing.Size(222, 16)
        Me.chkSecretChannel.TabIndex = 0
        Me.chkSecretChannel.Text = "Prints including a secret channel"
        Me.chkSecretChannel.UseVisualStyleBackColor = True
        '
        'chkDummyData
        '
        Me.chkDummyData.AutoSize = True
        Me.chkDummyData.BackColor = System.Drawing.SystemColors.Control
        Me.chkDummyData.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDummyData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDummyData.Location = New System.Drawing.Point(8, 48)
        Me.chkDummyData.Name = "chkDummyData"
        Me.chkDummyData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDummyData.Size = New System.Drawing.Size(198, 16)
        Me.chkDummyData.TabIndex = 1
        Me.chkDummyData.Text = "Prints including a dummy data"
        Me.chkDummyData.UseVisualStyleBackColor = True
        '
        'fraPageRange
        '
        Me.fraPageRange.BackColor = System.Drawing.SystemColors.Control
        Me.fraPageRange.Controls.Add(Me.cmbDrawTo)
        Me.fraPageRange.Controls.Add(Me.cmbDrawFrom)
        Me.fraPageRange.Controls.Add(Me.optDrawNo)
        Me.fraPageRange.Controls.Add(Me.lblDrawTo)
        Me.fraPageRange.Controls.Add(Me.lblDrawFrom)
        Me.fraPageRange.Controls.Add(Me.cmbPortTo)
        Me.fraPageRange.Controls.Add(Me.cmbFuTo)
        Me.fraPageRange.Controls.Add(Me.Label1)
        Me.fraPageRange.Controls.Add(Me.cmbPort)
        Me.fraPageRange.Controls.Add(Me.cmbFU)
        Me.fraPageRange.Controls.Add(Me.optPageRangeFuAdr)
        Me.fraPageRange.Controls.Add(Me.cmbPageRangeTo)
        Me.fraPageRange.Controls.Add(Me.cmbPageRangeFrom)
        Me.fraPageRange.Controls.Add(Me.optPageRangePages)
        Me.fraPageRange.Controls.Add(Me.optPageRangeAll)
        Me.fraPageRange.Controls.Add(Me.lblTo)
        Me.fraPageRange.Controls.Add(Me.lblFrom)
        Me.fraPageRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPageRange.Location = New System.Drawing.Point(22, 80)
        Me.fraPageRange.Name = "fraPageRange"
        Me.fraPageRange.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPageRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPageRange.Size = New System.Drawing.Size(354, 182)
        Me.fraPageRange.TabIndex = 0
        Me.fraPageRange.TabStop = False
        Me.fraPageRange.Text = "PRINT PAGE RANGE"
        '
        'cmbDrawTo
        '
        Me.cmbDrawTo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDrawTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDrawTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDrawTo.Location = New System.Drawing.Point(236, 85)
        Me.cmbDrawTo.Name = "cmbDrawTo"
        Me.cmbDrawTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDrawTo.Size = New System.Drawing.Size(81, 20)
        Me.cmbDrawTo.TabIndex = 15
        '
        'cmbDrawFrom
        '
        Me.cmbDrawFrom.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDrawFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDrawFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDrawFrom.Location = New System.Drawing.Point(114, 85)
        Me.cmbDrawFrom.Name = "cmbDrawFrom"
        Me.cmbDrawFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDrawFrom.Size = New System.Drawing.Size(81, 20)
        Me.cmbDrawFrom.TabIndex = 14
        '
        'optDrawNo
        '
        Me.optDrawNo.BackColor = System.Drawing.SystemColors.Control
        Me.optDrawNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDrawNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDrawNo.Location = New System.Drawing.Point(8, 82)
        Me.optDrawNo.Name = "optDrawNo"
        Me.optDrawNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDrawNo.Size = New System.Drawing.Size(65, 25)
        Me.optDrawNo.TabIndex = 13
        Me.optDrawNo.TabStop = True
        Me.optDrawNo.Text = "DRAWNo"
        Me.optDrawNo.UseVisualStyleBackColor = True
        '
        'lblDrawTo
        '
        Me.lblDrawTo.AutoSize = True
        Me.lblDrawTo.BackColor = System.Drawing.SystemColors.Control
        Me.lblDrawTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDrawTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDrawTo.Location = New System.Drawing.Point(213, 85)
        Me.lblDrawTo.Name = "lblDrawTo"
        Me.lblDrawTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDrawTo.Size = New System.Drawing.Size(17, 12)
        Me.lblDrawTo.TabIndex = 17
        Me.lblDrawTo.Text = "TO"
        Me.lblDrawTo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDrawFrom
        '
        Me.lblDrawFrom.AutoSize = True
        Me.lblDrawFrom.BackColor = System.Drawing.SystemColors.Control
        Me.lblDrawFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDrawFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDrawFrom.Location = New System.Drawing.Point(79, 85)
        Me.lblDrawFrom.Name = "lblDrawFrom"
        Me.lblDrawFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDrawFrom.Size = New System.Drawing.Size(29, 12)
        Me.lblDrawFrom.TabIndex = 16
        Me.lblDrawFrom.Text = "FROM"
        Me.lblDrawFrom.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbPortTo
        '
        Me.cmbPortTo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPortTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPortTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPortTo.Location = New System.Drawing.Point(300, 148)
        Me.cmbPortTo.Name = "cmbPortTo"
        Me.cmbPortTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPortTo.Size = New System.Drawing.Size(18, 20)
        Me.cmbPortTo.TabIndex = 12
        Me.cmbPortTo.Visible = False
        '
        'cmbFuTo
        '
        Me.cmbFuTo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbFuTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbFuTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbFuTo.Location = New System.Drawing.Point(188, 146)
        Me.cmbFuTo.Name = "cmbFuTo"
        Me.cmbFuTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbFuTo.Size = New System.Drawing.Size(80, 20)
        Me.cmbFuTo.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(165, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "～"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbPort
        '
        Me.cmbPort.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPort.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPort.Location = New System.Drawing.Point(276, 148)
        Me.cmbPort.Name = "cmbPort"
        Me.cmbPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPort.Size = New System.Drawing.Size(18, 20)
        Me.cmbPort.TabIndex = 9
        Me.cmbPort.Visible = False
        '
        'cmbFU
        '
        Me.cmbFU.BackColor = System.Drawing.SystemColors.Window
        Me.cmbFU.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbFU.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbFU.Location = New System.Drawing.Point(79, 146)
        Me.cmbFU.Name = "cmbFU"
        Me.cmbFU.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbFU.Size = New System.Drawing.Size(80, 20)
        Me.cmbFU.TabIndex = 8
        '
        'optPageRangeFuAdr
        '
        Me.optPageRangeFuAdr.BackColor = System.Drawing.SystemColors.Control
        Me.optPageRangeFuAdr.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPageRangeFuAdr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPageRangeFuAdr.Location = New System.Drawing.Point(8, 143)
        Me.optPageRangeFuAdr.Name = "optPageRangeFuAdr"
        Me.optPageRangeFuAdr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPageRangeFuAdr.Size = New System.Drawing.Size(65, 25)
        Me.optPageRangeFuAdr.TabIndex = 7
        Me.optPageRangeFuAdr.TabStop = True
        Me.optPageRangeFuAdr.Text = "FU Adr"
        Me.optPageRangeFuAdr.UseVisualStyleBackColor = True
        '
        'cmbPageRangeTo
        '
        Me.cmbPageRangeTo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPageRangeTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPageRangeTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPageRangeTo.Location = New System.Drawing.Point(236, 51)
        Me.cmbPageRangeTo.Name = "cmbPageRangeTo"
        Me.cmbPageRangeTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPageRangeTo.Size = New System.Drawing.Size(81, 20)
        Me.cmbPageRangeTo.TabIndex = 3
        '
        'cmbPageRangeFrom
        '
        Me.cmbPageRangeFrom.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPageRangeFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPageRangeFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPageRangeFrom.Location = New System.Drawing.Point(114, 51)
        Me.cmbPageRangeFrom.Name = "cmbPageRangeFrom"
        Me.cmbPageRangeFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPageRangeFrom.Size = New System.Drawing.Size(81, 20)
        Me.cmbPageRangeFrom.TabIndex = 2
        '
        'optPageRangePages
        '
        Me.optPageRangePages.BackColor = System.Drawing.SystemColors.Control
        Me.optPageRangePages.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPageRangePages.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPageRangePages.Location = New System.Drawing.Point(8, 48)
        Me.optPageRangePages.Name = "optPageRangePages"
        Me.optPageRangePages.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPageRangePages.Size = New System.Drawing.Size(65, 25)
        Me.optPageRangePages.TabIndex = 1
        Me.optPageRangePages.TabStop = True
        Me.optPageRangePages.Text = "PAGES"
        Me.optPageRangePages.UseVisualStyleBackColor = True
        '
        'optPageRangeAll
        '
        Me.optPageRangeAll.BackColor = System.Drawing.SystemColors.Control
        Me.optPageRangeAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPageRangeAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPageRangeAll.Location = New System.Drawing.Point(8, 12)
        Me.optPageRangeAll.Name = "optPageRangeAll"
        Me.optPageRangeAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPageRangeAll.Size = New System.Drawing.Size(65, 25)
        Me.optPageRangeAll.TabIndex = 0
        Me.optPageRangeAll.TabStop = True
        Me.optPageRangeAll.Text = "ALL"
        Me.optPageRangeAll.UseVisualStyleBackColor = True
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.SystemColors.Control
        Me.lblTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTo.Location = New System.Drawing.Point(213, 51)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTo.Size = New System.Drawing.Size(17, 12)
        Me.lblTo.TabIndex = 6
        Me.lblTo.Text = "TO"
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.SystemColors.Control
        Me.lblFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFrom.Location = New System.Drawing.Point(79, 51)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFrom.Size = New System.Drawing.Size(29, 12)
        Me.lblFrom.TabIndex = 5
        Me.lblFrom.Text = "FROM"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(18, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(59, 12)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Printer :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPrinter
        '
        Me.lblPrinter.BackColor = System.Drawing.SystemColors.MenuBar
        Me.lblPrinter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblPrinter.Location = New System.Drawing.Point(75, 16)
        Me.lblPrinter.Multiline = True
        Me.lblPrinter.Name = "lblPrinter"
        Me.lblPrinter.Size = New System.Drawing.Size(363, 59)
        Me.lblPrinter.TabIndex = 7
        Me.lblPrinter.Text = "^"
        '
        'frmPrtTerminal
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(397, 560)
        Me.Controls.Add(Me.lblPrinter)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdPreview)
        Me.Controls.Add(Me.fraOption)
        Me.Controls.Add(Me.fraPageRange)
        Me.Controls.Add(Me.Label11)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrtTerminal"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "TERMINAL PRINT"
        Me.fraOption.ResumeLayout(False)
        Me.fraOption.PerformLayout()
        Me.fraPageRange.ResumeLayout(False)
        Me.fraPageRange.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPrinter As System.Windows.Forms.TextBox
    Public WithEvents chkPagePrint As System.Windows.Forms.CheckBox
    Public WithEvents chkFuName As System.Windows.Forms.CheckBox
    Public WithEvents chkFormatType As System.Windows.Forms.CheckBox
    Public WithEvents cmbPort As System.Windows.Forms.ComboBox
    Public WithEvents cmbFU As System.Windows.Forms.ComboBox
    Public WithEvents optPageRangeFuAdr As System.Windows.Forms.RadioButton
    Public WithEvents cmbPortTo As System.Windows.Forms.ComboBox
    Public WithEvents cmbFuTo As System.Windows.Forms.ComboBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cmbDrawTo As System.Windows.Forms.ComboBox
    Public WithEvents cmbDrawFrom As System.Windows.Forms.ComboBox
    Public WithEvents optDrawNo As System.Windows.Forms.RadioButton
    Public WithEvents lblDrawTo As System.Windows.Forms.Label
    Public WithEvents lblDrawFrom As System.Windows.Forms.Label
    Public WithEvents chkRangePrint As System.Windows.Forms.CheckBox
    Public WithEvents chkTerVer As System.Windows.Forms.CheckBox
    Public WithEvents chkTerDIMsg As System.Windows.Forms.CheckBox
    Public WithEvents chkExpTxtEtoJ As System.Windows.Forms.CheckBox
#End Region

End Class
