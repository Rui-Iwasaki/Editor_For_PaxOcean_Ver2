<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChListPulse
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
        Me.txtString = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtRangeHi = New System.Windows.Forms.TextBox()
        Me.txtrangeLo = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblDecPoint = New System.Windows.Forms.Label()
        Me.txtDecPoint = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtFilterCoeficient = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.txtPin = New System.Windows.Forms.TextBox()
        Me.txtPortNo = New System.Windows.Forms.TextBox()
        Me.txtFuNo = New System.Windows.Forms.TextBox()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.cmbDataType = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.fraHiHi0 = New System.Windows.Forms.GroupBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtStatusHi = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtGRep2Hi = New System.Windows.Forms.TextBox()
        Me.txtGRep1Hi = New System.Windows.Forms.TextBox()
        Me.txtExtGHi = New System.Windows.Forms.TextBox()
        Me.txtDelayHi = New System.Windows.Forms.TextBox()
        Me.txtValueHi = New System.Windows.Forms.TextBox()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtAlmMimic = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cmbAlmLvl = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtTagNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbTime = New System.Windows.Forms.ComboBox()
        Me.chkMrepose = New System.Windows.Forms.CheckBox()
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
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtDelayTimer = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
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
        Me.cmdBeforeCH = New System.Windows.Forms.Button()
        Me.cmdNextCH = New System.Windows.Forms.Button()
        Me.lblDummy = New System.Windows.Forms.Label()
        Me.GroupBox4.SuspendLayout()
        Me.fraHiHi0.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(828, 388)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(705, 388)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'txtString
        '
        Me.txtString.AcceptsReturn = True
        Me.txtString.Location = New System.Drawing.Point(120, 194)
        Me.txtString.MaxLength = 0
        Me.txtString.Name = "txtString"
        Me.txtString.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtString.Size = New System.Drawing.Size(48, 19)
        Me.txtString.TabIndex = 6
        Me.txtString.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label29)
        Me.GroupBox4.Controls.Add(Me.txtRangeHi)
        Me.GroupBox4.Controls.Add(Me.txtrangeLo)
        Me.GroupBox4.Controls.Add(Me.Label28)
        Me.GroupBox4.Controls.Add(Me.cmbStatus)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.lblDecPoint)
        Me.GroupBox4.Controls.Add(Me.txtDecPoint)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.txtFilterCoeficient)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.txtUnit)
        Me.GroupBox4.Controls.Add(Me.txtPin)
        Me.GroupBox4.Controls.Add(Me.txtPortNo)
        Me.GroupBox4.Controls.Add(Me.txtFuNo)
        Me.GroupBox4.Controls.Add(Me.cmbUnit)
        Me.GroupBox4.Controls.Add(Me.cmbDataType)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.txtString)
        Me.GroupBox4.Controls.Add(Me.fraHiHi0)
        Me.GroupBox4.Controls.Add(Me.Label21)
        Me.GroupBox4.Controls.Add(Me.Label38)
        Me.GroupBox4.Controls.Add(Me.Label45)
        Me.GroupBox4.Location = New System.Drawing.Point(475, 12)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(466, 368)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Pulse"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(175, 144)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(11, 12)
        Me.Label29.TabIndex = 127
        Me.Label29.Text = "-"
        '
        'txtRangeHi
        '
        Me.txtRangeHi.Enabled = False
        Me.txtRangeHi.Location = New System.Drawing.Point(192, 141)
        Me.txtRangeHi.Name = "txtRangeHi"
        Me.txtRangeHi.Size = New System.Drawing.Size(86, 19)
        Me.txtRangeHi.TabIndex = 126
        Me.txtRangeHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtrangeLo
        '
        Me.txtrangeLo.Enabled = False
        Me.txtrangeLo.Location = New System.Drawing.Point(120, 141)
        Me.txtrangeLo.Name = "txtrangeLo"
        Me.txtrangeLo.Size = New System.Drawing.Size(49, 19)
        Me.txtrangeLo.TabIndex = 125
        Me.txtrangeLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(74, 144)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(35, 12)
        Me.Label28.TabIndex = 124
        Me.Label28.Text = "Range"
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatus.Location = New System.Drawing.Point(121, 83)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatus.Size = New System.Drawing.Size(157, 20)
        Me.cmbStatus.TabIndex = 121
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(68, 89)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(41, 12)
        Me.Label15.TabIndex = 123
        Me.Label15.Text = "Status"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDecPoint
        '
        Me.lblDecPoint.BackColor = System.Drawing.SystemColors.Control
        Me.lblDecPoint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDecPoint.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDecPoint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDecPoint.Location = New System.Drawing.Point(358, 26)
        Me.lblDecPoint.Name = "lblDecPoint"
        Me.lblDecPoint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDecPoint.Size = New System.Drawing.Size(80, 19)
        Me.lblDecPoint.TabIndex = 120
        Me.lblDecPoint.Text = "99999999.9"
        Me.lblDecPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblDecPoint.Visible = False
        '
        'txtDecPoint
        '
        Me.txtDecPoint.AcceptsReturn = True
        Me.txtDecPoint.Location = New System.Drawing.Point(120, 168)
        Me.txtDecPoint.MaxLength = 0
        Me.txtDecPoint.Name = "txtDecPoint"
        Me.txtDecPoint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDecPoint.Size = New System.Drawing.Size(48, 19)
        Me.txtDecPoint.TabIndex = 7
        Me.txtDecPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(8, 171)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(101, 12)
        Me.Label11.TabIndex = 118
        Me.Label11.Text = "Decimal Position"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtFilterCoeficient
        '
        Me.txtFilterCoeficient.AcceptsReturn = True
        Me.txtFilterCoeficient.Location = New System.Drawing.Point(316, 190)
        Me.txtFilterCoeficient.MaxLength = 0
        Me.txtFilterCoeficient.Name = "txtFilterCoeficient"
        Me.txtFilterCoeficient.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFilterCoeficient.Size = New System.Drawing.Size(48, 19)
        Me.txtFilterCoeficient.TabIndex = 8
        Me.txtFilterCoeficient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(198, 195)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(107, 12)
        Me.Label12.TabIndex = 116
        Me.Label12.Text = "Filter Coeficient"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(371, 195)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(47, 12)
        Me.Label5.TabIndex = 117
        Me.Label5.Text = "* 4msec"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtUnit
        '
        Me.txtUnit.AcceptsReturn = True
        Me.txtUnit.Location = New System.Drawing.Point(283, 113)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(116, 19)
        Me.txtUnit.TabIndex = 5
        '
        'txtPin
        '
        Me.txtPin.AcceptsReturn = True
        Me.txtPin.Location = New System.Drawing.Point(205, 26)
        Me.txtPin.MaxLength = 0
        Me.txtPin.Name = "txtPin"
        Me.txtPin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPin.Size = New System.Drawing.Size(40, 19)
        Me.txtPin.TabIndex = 2
        '
        'txtPortNo
        '
        Me.txtPortNo.AcceptsReturn = True
        Me.txtPortNo.Location = New System.Drawing.Point(163, 26)
        Me.txtPortNo.MaxLength = 0
        Me.txtPortNo.Name = "txtPortNo"
        Me.txtPortNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNo.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNo.TabIndex = 1
        '
        'txtFuNo
        '
        Me.txtFuNo.AcceptsReturn = True
        Me.txtFuNo.Location = New System.Drawing.Point(121, 26)
        Me.txtFuNo.MaxLength = 0
        Me.txtFuNo.Name = "txtFuNo"
        Me.txtFuNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNo.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNo.TabIndex = 0
        '
        'cmbUnit
        '
        Me.cmbUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbUnit.Location = New System.Drawing.Point(121, 112)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbUnit.Size = New System.Drawing.Size(157, 20)
        Me.cmbUnit.TabIndex = 4
        '
        'cmbDataType
        '
        Me.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbDataType.FormattingEnabled = True
        Me.cmbDataType.Location = New System.Drawing.Point(121, 53)
        Me.cmbDataType.Name = "cmbDataType"
        Me.cmbDataType.Size = New System.Drawing.Size(157, 20)
        Me.cmbDataType.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(54, 58)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(59, 12)
        Me.Label8.TabIndex = 104
        Me.Label8.Text = "Data Type"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraHiHi0
        '
        Me.fraHiHi0.BackColor = System.Drawing.SystemColors.Control
        Me.fraHiHi0.Controls.Add(Me.lblStatus)
        Me.fraHiHi0.Controls.Add(Me.txtStatusHi)
        Me.fraHiHi0.Controls.Add(Me.Label14)
        Me.fraHiHi0.Controls.Add(Me.txtGRep2Hi)
        Me.fraHiHi0.Controls.Add(Me.txtGRep1Hi)
        Me.fraHiHi0.Controls.Add(Me.txtExtGHi)
        Me.fraHiHi0.Controls.Add(Me.txtDelayHi)
        Me.fraHiHi0.Controls.Add(Me.txtValueHi)
        Me.fraHiHi0.Controls.Add(Me.Label128)
        Me.fraHiHi0.Controls.Add(Me.Label129)
        Me.fraHiHi0.Controls.Add(Me.Label20)
        Me.fraHiHi0.Controls.Add(Me.Label30)
        Me.fraHiHi0.Controls.Add(Me.Label40)
        Me.fraHiHi0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraHiHi0.Location = New System.Drawing.Point(18, 244)
        Me.fraHiHi0.Name = "fraHiHi0"
        Me.fraHiHi0.Padding = New System.Windows.Forms.Padding(0)
        Me.fraHiHi0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraHiHi0.Size = New System.Drawing.Size(420, 95)
        Me.fraHiHi0.TabIndex = 9
        Me.fraHiHi0.TabStop = False
        Me.fraHiHi0.Text = "Alarm"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatus.Location = New System.Drawing.Point(354, 26)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.Size = New System.Drawing.Size(41, 12)
        Me.lblStatus.TabIndex = 117
        Me.lblStatus.Text = "Status"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtStatusHi
        '
        Me.txtStatusHi.AcceptsReturn = True
        Me.txtStatusHi.Location = New System.Drawing.Point(336, 48)
        Me.txtStatusHi.MaxLength = 0
        Me.txtStatusHi.Name = "txtStatusHi"
        Me.txtStatusHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusHi.Size = New System.Drawing.Size(72, 19)
        Me.txtStatusHi.TabIndex = 116
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(17, 51)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(17, 12)
        Me.Label14.TabIndex = 68
        Me.Label14.Text = "HI"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGRep2Hi
        '
        Me.txtGRep2Hi.AcceptsReturn = True
        Me.txtGRep2Hi.Location = New System.Drawing.Point(282, 48)
        Me.txtGRep2Hi.MaxLength = 0
        Me.txtGRep2Hi.Name = "txtGRep2Hi"
        Me.txtGRep2Hi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2Hi.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep2Hi.TabIndex = 4
        Me.txtGRep2Hi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1Hi
        '
        Me.txtGRep1Hi.AcceptsReturn = True
        Me.txtGRep1Hi.Location = New System.Drawing.Point(228, 48)
        Me.txtGRep1Hi.MaxLength = 0
        Me.txtGRep1Hi.Name = "txtGRep1Hi"
        Me.txtGRep1Hi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1Hi.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep1Hi.TabIndex = 3
        Me.txtGRep1Hi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExtGHi
        '
        Me.txtExtGHi.AcceptsReturn = True
        Me.txtExtGHi.Location = New System.Drawing.Point(120, 48)
        Me.txtExtGHi.MaxLength = 0
        Me.txtExtGHi.Name = "txtExtGHi"
        Me.txtExtGHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGHi.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGHi.TabIndex = 1
        Me.txtExtGHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelayHi
        '
        Me.txtDelayHi.AcceptsReturn = True
        Me.txtDelayHi.Location = New System.Drawing.Point(174, 48)
        Me.txtDelayHi.MaxLength = 0
        Me.txtDelayHi.Name = "txtDelayHi"
        Me.txtDelayHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayHi.Size = New System.Drawing.Size(48, 19)
        Me.txtDelayHi.TabIndex = 2
        Me.txtDelayHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValueHi
        '
        Me.txtValueHi.AcceptsReturn = True
        Me.txtValueHi.Location = New System.Drawing.Point(42, 48)
        Me.txtValueHi.MaxLength = 0
        Me.txtValueHi.Name = "txtValueHi"
        Me.txtValueHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtValueHi.Size = New System.Drawing.Size(72, 19)
        Me.txtValueHi.TabIndex = 0
        Me.txtValueHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label128
        '
        Me.Label128.AutoSize = True
        Me.Label128.BackColor = System.Drawing.SystemColors.Control
        Me.Label128.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label128.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label128.Location = New System.Drawing.Point(180, 26)
        Me.Label128.Name = "Label128"
        Me.Label128.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label128.Size = New System.Drawing.Size(35, 12)
        Me.Label128.TabIndex = 60
        Me.Label128.Text = "Delay"
        Me.Label128.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label129
        '
        Me.Label129.AutoSize = True
        Me.Label129.BackColor = System.Drawing.SystemColors.Control
        Me.Label129.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label129.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label129.Location = New System.Drawing.Point(60, 26)
        Me.Label129.Name = "Label129"
        Me.Label129.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label129.Size = New System.Drawing.Size(35, 12)
        Me.Label129.TabIndex = 59
        Me.Label129.Text = "Value"
        Me.Label129.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(124, 26)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(35, 12)
        Me.Label20.TabIndex = 58
        Me.Label20.Text = "EXT.G"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(232, 26)
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
        Me.Label40.Location = New System.Drawing.Point(288, 26)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(41, 12)
        Me.Label40.TabIndex = 56
        Me.Label40.Text = "G REP2"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(46, 29)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(65, 12)
        Me.Label21.TabIndex = 88
        Me.Label21.Text = "FU Address"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(80, 116)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(29, 12)
        Me.Label38.TabIndex = 85
        Me.Label38.Text = "Unit"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(68, 197)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(41, 12)
        Me.Label45.TabIndex = 82
        Me.Label45.Text = "String"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtAlmMimic)
        Me.GroupBox1.Controls.Add(Me.Label31)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.cmbAlmLvl)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtTagNo)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbTime)
        Me.GroupBox1.Controls.Add(Me.chkMrepose)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.cmbSysNo)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label36)
        Me.GroupBox1.Controls.Add(Me.txtItemName)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtChNo)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label37)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(444, 368)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Common"
        '
        'txtAlmMimic
        '
        Me.txtAlmMimic.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtAlmMimic.Location = New System.Drawing.Point(272, 324)
        Me.txtAlmMimic.MaxLength = 0
        Me.txtAlmMimic.Name = "txtAlmMimic"
        Me.txtAlmMimic.Size = New System.Drawing.Size(48, 19)
        Me.txtAlmMimic.TabIndex = 130
        Me.txtAlmMimic.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtAlmMimic.Visible = False
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(270, 307)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(101, 12)
        Me.Label31.TabIndex = 131
        Me.Label31.Text = "Fire Alarm Mimic"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label31.Visible = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(215, 244)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(71, 12)
        Me.Label27.TabIndex = 129
        Me.Label27.Text = "Alarm Level"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbAlmLvl
        '
        Me.cmbAlmLvl.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAlmLvl.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbAlmLvl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlmLvl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbAlmLvl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAlmLvl.Location = New System.Drawing.Point(292, 240)
        Me.cmbAlmLvl.Name = "cmbAlmLvl"
        Me.cmbAlmLvl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAlmLvl.Size = New System.Drawing.Size(97, 20)
        Me.cmbAlmLvl.TabIndex = 128
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(182, 58)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(47, 12)
        Me.Label26.TabIndex = 127
        Me.Label26.Text = "Tag No."
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTagNo
        '
        Me.txtTagNo.AcceptsReturn = True
        Me.txtTagNo.Location = New System.Drawing.Point(236, 53)
        Me.txtTagNo.MaxLength = 16
        Me.txtTagNo.Name = "txtTagNo"
        Me.txtTagNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTagNo.Size = New System.Drawing.Size(120, 19)
        Me.txtTagNo.TabIndex = 126
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(20, 244)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(119, 12)
        Me.Label6.TabIndex = 125
        Me.Label6.Text = "Unit of Delay Timer"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbTime
        '
        Me.cmbTime.BackColor = System.Drawing.SystemColors.Window
        Me.cmbTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbTime.Location = New System.Drawing.Point(144, 240)
        Me.cmbTime.Name = "cmbTime"
        Me.cmbTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbTime.Size = New System.Drawing.Size(64, 20)
        Me.cmbTime.TabIndex = 5
        '
        'chkMrepose
        '
        Me.chkMrepose.BackColor = System.Drawing.SystemColors.Control
        Me.chkMrepose.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMrepose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMrepose.Location = New System.Drawing.Point(256, 272)
        Me.chkMrepose.Name = "chkMrepose"
        Me.chkMrepose.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkMrepose.Size = New System.Drawing.Size(120, 19)
        Me.chkMrepose.TabIndex = 114
        Me.chkMrepose.Text = "Manual Repose"
        Me.chkMrepose.UseVisualStyleBackColor = True
        Me.chkMrepose.Visible = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtShareChid)
        Me.GroupBox6.Controls.Add(Me.lblShareChid)
        Me.GroupBox6.Controls.Add(Me.lblShareType)
        Me.GroupBox6.Controls.Add(Me.cmbShareType)
        Me.GroupBox6.Location = New System.Drawing.Point(16, 272)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(228, 80)
        Me.GroupBox6.TabIndex = 6
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
        Me.GroupBox3.Controls.Add(Me.Label22)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox3.Location = New System.Drawing.Point(16, 144)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox3.Size = New System.Drawing.Size(396, 84)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Flag"
        '
        'lblBitSet
        '
        Me.lblBitSet.AutoSize = True
        Me.lblBitSet.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitSet.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBitSet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitSet.Location = New System.Drawing.Point(83, 66)
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
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(312, 24)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(23, 12)
        Me.Label22.TabIndex = 80
        Me.Label22.Text = "PLC"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(204, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(17, 12)
        Me.Label2.TabIndex = 77
        Me.Label2.Text = "RL"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(168, 24)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(17, 12)
        Me.Label19.TabIndex = 76
        Me.Label19.Text = "WK"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(132, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(23, 12)
        Me.Label1.TabIndex = 75
        Me.Label1.Text = "GWS"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(96, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(23, 12)
        Me.Label4.TabIndex = 74
        Me.Label4.Text = "SIO"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(60, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(17, 12)
        Me.Label9.TabIndex = 73
        Me.Label9.Text = "SC"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(24, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(23, 12)
        Me.Label10.TabIndex = 72
        Me.Label10.Text = "Dmy"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.txtDelayTimer)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.txtExtGroup)
        Me.GroupBox2.Controls.Add(Me.txtGRep1)
        Me.GroupBox2.Controls.Add(Me.txtGRep2)
        Me.GroupBox2.Enabled = False
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox2.Location = New System.Drawing.Point(424, 244)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox2.Size = New System.Drawing.Size(256, 79)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Alarm"
        Me.GroupBox2.Visible = False
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
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(73, 27)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(35, 12)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "Delay"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(19, 27)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(35, 12)
        Me.Label16.TabIndex = 58
        Me.Label16.Text = "EXT.G"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(127, 27)
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
        Me.Label18.Location = New System.Drawing.Point(181, 27)
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
        Me.txtExtGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes
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
        Me.txtGRep1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
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
        Me.txtGRep2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtGRep2.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep2.TabIndex = 3
        Me.txtGRep2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbSysNo
        '
        Me.cmbSysNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSysNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbSysNo.FormattingEnabled = True
        Me.cmbSysNo.Location = New System.Drawing.Point(90, 26)
        Me.cmbSysNo.Name = "cmbSysNo"
        Me.cmbSysNo.Size = New System.Drawing.Size(110, 20)
        Me.cmbSysNo.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.Location = New System.Drawing.Point(90, 109)
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
        Me.Label36.Location = New System.Drawing.Point(26, 112)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label36.Size = New System.Drawing.Size(47, 12)
        Me.Label36.TabIndex = 99
        Me.Label36.Text = "Remarks"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.Location = New System.Drawing.Point(90, 82)
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
        Me.Label3.Location = New System.Drawing.Point(16, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 71
        Me.Label3.Text = "Item Name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtChNo
        '
        Me.txtChNo.AcceptsReturn = True
        Me.txtChNo.Location = New System.Drawing.Point(90, 55)
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
        Me.Label7.Location = New System.Drawing.Point(37, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 69
        Me.Label7.Text = "CH No."
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(35, 29)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(47, 12)
        Me.Label37.TabIndex = 67
        Me.Label37.Text = "Sys No."
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdBeforeCH
        '
        Me.cmdBeforeCH.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBeforeCH.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBeforeCH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBeforeCH.Location = New System.Drawing.Point(16, 388)
        Me.cmdBeforeCH.Name = "cmdBeforeCH"
        Me.cmdBeforeCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBeforeCH.Size = New System.Drawing.Size(113, 33)
        Me.cmdBeforeCH.TabIndex = 2
        Me.cmdBeforeCH.Text = "Before CH"
        Me.cmdBeforeCH.UseVisualStyleBackColor = True
        '
        'cmdNextCH
        '
        Me.cmdNextCH.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNextCH.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNextCH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNextCH.Location = New System.Drawing.Point(140, 388)
        Me.cmdNextCH.Name = "cmdNextCH"
        Me.cmdNextCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNextCH.Size = New System.Drawing.Size(113, 33)
        Me.cmdNextCH.TabIndex = 3
        Me.cmdNextCH.Text = "Next CH"
        Me.cmdNextCH.UseVisualStyleBackColor = True
        '
        'lblDummy
        '
        Me.lblDummy.AutoSize = True
        Me.lblDummy.Location = New System.Drawing.Point(846, 3)
        Me.lblDummy.Name = "lblDummy"
        Me.lblDummy.Size = New System.Drawing.Size(95, 12)
        Me.lblDummy.TabIndex = 11
        Me.lblDummy.Text = "F5:DummySetting"
        '
        'frmChListPulse
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(959, 432)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblDummy)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.cmdBeforeCH)
        Me.Controls.Add(Me.cmdNextCH)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 30)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChListPulse"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CHANNEL LIST PULSE REVOLUTION"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.fraHiHi0.ResumeLayout(False)
        Me.fraHiHi0.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtString As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Public WithEvents fraHiHi0 As System.Windows.Forms.GroupBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents txtGRep2Hi As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1Hi As System.Windows.Forms.TextBox
    Public WithEvents txtExtGHi As System.Windows.Forms.TextBox
    Public WithEvents txtDelayHi As System.Windows.Forms.TextBox
    Public WithEvents txtValueHi As System.Windows.Forms.TextBox
    Public WithEvents Label128 As System.Windows.Forms.Label
    Public WithEvents Label129 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents txtDelayTimer As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
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
    Friend WithEvents cmbDataType As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Public WithEvents txtPin As System.Windows.Forms.TextBox
    Public WithEvents txtPortNo As System.Windows.Forms.TextBox
    Public WithEvents txtFuNo As System.Windows.Forms.TextBox
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
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents txtUnit As System.Windows.Forms.TextBox
    Public WithEvents txtFilterCoeficient As System.Windows.Forms.TextBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents cmdBeforeCH As System.Windows.Forms.Button
    Public WithEvents cmdNextCH As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Public WithEvents txtShareChid As System.Windows.Forms.TextBox
    Public WithEvents lblShareChid As System.Windows.Forms.Label
    Public WithEvents lblShareType As System.Windows.Forms.Label
    Public WithEvents cmbShareType As System.Windows.Forms.ComboBox
    Public WithEvents chkMrepose As System.Windows.Forms.CheckBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents cmbTime As System.Windows.Forms.ComboBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblDecPoint As System.Windows.Forms.Label
    Public WithEvents txtDecPoint As System.Windows.Forms.TextBox
    Public WithEvents lblBitSet As System.Windows.Forms.Label
    Public WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents lblStatus As System.Windows.Forms.Label
    Public WithEvents txtStatusHi As System.Windows.Forms.TextBox
    Friend WithEvents lblDummy As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents txtTagNo As System.Windows.Forms.TextBox
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents cmbAlmLvl As System.Windows.Forms.ComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtRangeHi As System.Windows.Forms.TextBox
    Friend WithEvents txtrangeLo As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents txtAlmMimic As System.Windows.Forms.TextBox
    Public WithEvents Label31 As System.Windows.Forms.Label
#End Region

End Class
