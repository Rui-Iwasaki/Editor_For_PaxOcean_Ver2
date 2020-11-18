<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChListMotor
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

    Public WithEvents cmbStatusIn As System.Windows.Forms.ComboBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents lblDo5 As System.Windows.Forms.Label
    Public WithEvents lblDo4 As System.Windows.Forms.Label
    Public WithEvents lblDo3 As System.Windows.Forms.Label
    Public WithEvents lblDo2 As System.Windows.Forms.Label
    Public WithEvents lblDo1 As System.Windows.Forms.Label
    Public WithEvents lblDi5 As System.Windows.Forms.Label
    Public WithEvents lblDi4 As System.Windows.Forms.Label
    Public WithEvents lblDi3 As System.Windows.Forms.Label
    Public WithEvents lblDi2 As System.Windows.Forms.Label
    Public WithEvents lblDi1 As System.Windows.Forms.Label
    Public WithEvents lblDoStart As System.Windows.Forms.Label
    Public WithEvents lblStatusDi5 As System.Windows.Forms.Label
    Public WithEvents lblStatusDi4 As System.Windows.Forms.Label
    Public WithEvents lblStatusDi3 As System.Windows.Forms.Label
    Public WithEvents lblStatusDi2 As System.Windows.Forms.Label
    Public WithEvents lblStatusDi1 As System.Windows.Forms.Label
    Public WithEvents lblDiStart As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmbStatusIn = New System.Windows.Forms.ComboBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.lblDo5 = New System.Windows.Forms.Label()
        Me.lblDo4 = New System.Windows.Forms.Label()
        Me.lblDo3 = New System.Windows.Forms.Label()
        Me.lblDo2 = New System.Windows.Forms.Label()
        Me.lblDo1 = New System.Windows.Forms.Label()
        Me.lblDi5 = New System.Windows.Forms.Label()
        Me.lblDi4 = New System.Windows.Forms.Label()
        Me.lblDi3 = New System.Windows.Forms.Label()
        Me.lblDi2 = New System.Windows.Forms.Label()
        Me.lblDi1 = New System.Windows.Forms.Label()
        Me.lblDoStart = New System.Windows.Forms.Label()
        Me.lblStatusDi5 = New System.Windows.Forms.Label()
        Me.lblStatusDi4 = New System.Windows.Forms.Label()
        Me.lblStatusDi3 = New System.Windows.Forms.Label()
        Me.lblStatusDi2 = New System.Windows.Forms.Label()
        Me.lblStatusDi1 = New System.Windows.Forms.Label()
        Me.lblDiStart = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnHANEI = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbRUN = New System.Windows.Forms.ComboBox()
        Me.cmbM = New System.Windows.Forms.ComboBox()
        Me.cmbR = New System.Windows.Forms.ComboBox()
        Me.lblBitPosDi5 = New System.Windows.Forms.Label()
        Me.lblBitPosDi4 = New System.Windows.Forms.Label()
        Me.lblBitPosDi3 = New System.Windows.Forms.Label()
        Me.lblBitPosDi2 = New System.Windows.Forms.Label()
        Me.lblBitPosDi1 = New System.Windows.Forms.Label()
        Me.lblControlType = New System.Windows.Forms.Label()
        Me.cmbControlType = New System.Windows.Forms.ComboBox()
        Me.txtPulseWidth = New System.Windows.Forms.TextBox()
        Me.lblPulseWidth = New System.Windows.Forms.Label()
        Me.fraHiHi0 = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtAlarmTimeup = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbTime = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtStatusDo = New System.Windows.Forms.TextBox()
        Me.txtGRep2Do = New System.Windows.Forms.TextBox()
        Me.txtGRep1Do = New System.Windows.Forms.TextBox()
        Me.txtExtGDo = New System.Windows.Forms.TextBox()
        Me.txtDelayDo = New System.Windows.Forms.TextBox()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.txtStatusDo5 = New System.Windows.Forms.TextBox()
        Me.txtStatusDo4 = New System.Windows.Forms.TextBox()
        Me.txtStatusDo3 = New System.Windows.Forms.TextBox()
        Me.txtStatusDo2 = New System.Windows.Forms.TextBox()
        Me.txtStatusDo1 = New System.Windows.Forms.TextBox()
        Me.chkStatusAlarm = New System.Windows.Forms.CheckBox()
        Me.txtFilterCoeficient = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPinDo = New System.Windows.Forms.TextBox()
        Me.txtPortNoDo = New System.Windows.Forms.TextBox()
        Me.txtFuNoDo = New System.Windows.Forms.TextBox()
        Me.txtPinDi = New System.Windows.Forms.TextBox()
        Me.txtPortNoDi = New System.Windows.Forms.TextBox()
        Me.txtFuNoDi = New System.Windows.Forms.TextBox()
        Me.txtStatusOut = New System.Windows.Forms.TextBox()
        Me.cmbStatusOut = New System.Windows.Forms.ComboBox()
        Me.lblStatusOut = New System.Windows.Forms.Label()
        Me.txtStatusIn = New System.Windows.Forms.TextBox()
        Me.cmbExtDevice = New System.Windows.Forms.ComboBox()
        Me.cmbDataType = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtAlmMimic = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cmbAlmLvl = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTagNo = New System.Windows.Forms.TextBox()
        Me.chkMrepose = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtShareChid = New System.Windows.Forms.TextBox()
        Me.lblShareChid = New System.Windows.Forms.Label()
        Me.lblShareType = New System.Windows.Forms.Label()
        Me.cmbShareType = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtMotorCol = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
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
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtDelayTimer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.GroupBox2.SuspendLayout()
        Me.fraHiHi0.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbStatusIn
        '
        Me.cmbStatusIn.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatusIn.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatusIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatusIn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbStatusIn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatusIn.Location = New System.Drawing.Point(556, 55)
        Me.cmbStatusIn.Name = "cmbStatusIn"
        Me.cmbStatusIn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatusIn.Size = New System.Drawing.Size(116, 20)
        Me.cmbStatusIn.TabIndex = 2
        Me.cmbStatusIn.Visible = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(636, 653)
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
        Me.cmdOK.Location = New System.Drawing.Point(514, 653)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'lblDo5
        '
        Me.lblDo5.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo5.Location = New System.Drawing.Point(624, 137)
        Me.lblDo5.Name = "lblDo5"
        Me.lblDo5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo5.Size = New System.Drawing.Size(86, 23)
        Me.lblDo5.TabIndex = 25
        Me.lblDo5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDo4
        '
        Me.lblDo4.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo4.Location = New System.Drawing.Point(536, 137)
        Me.lblDo4.Name = "lblDo4"
        Me.lblDo4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo4.Size = New System.Drawing.Size(86, 23)
        Me.lblDo4.TabIndex = 24
        Me.lblDo4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDo3
        '
        Me.lblDo3.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo3.Location = New System.Drawing.Point(448, 137)
        Me.lblDo3.Name = "lblDo3"
        Me.lblDo3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo3.Size = New System.Drawing.Size(86, 23)
        Me.lblDo3.TabIndex = 23
        Me.lblDo3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDo2
        '
        Me.lblDo2.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo2.Location = New System.Drawing.Point(360, 137)
        Me.lblDo2.Name = "lblDo2"
        Me.lblDo2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo2.Size = New System.Drawing.Size(86, 23)
        Me.lblDo2.TabIndex = 22
        Me.lblDo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDo1
        '
        Me.lblDo1.BackColor = System.Drawing.SystemColors.Control
        Me.lblDo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDo1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDo1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDo1.Location = New System.Drawing.Point(272, 136)
        Me.lblDo1.Name = "lblDo1"
        Me.lblDo1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDo1.Size = New System.Drawing.Size(86, 23)
        Me.lblDo1.TabIndex = 21
        Me.lblDo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi5
        '
        Me.lblDi5.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi5.Location = New System.Drawing.Point(624, 108)
        Me.lblDi5.Name = "lblDi5"
        Me.lblDi5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi5.Size = New System.Drawing.Size(86, 23)
        Me.lblDi5.TabIndex = 20
        Me.lblDi5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi4
        '
        Me.lblDi4.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi4.Location = New System.Drawing.Point(536, 108)
        Me.lblDi4.Name = "lblDi4"
        Me.lblDi4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi4.Size = New System.Drawing.Size(86, 23)
        Me.lblDi4.TabIndex = 19
        Me.lblDi4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi3
        '
        Me.lblDi3.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi3.Location = New System.Drawing.Point(448, 108)
        Me.lblDi3.Name = "lblDi3"
        Me.lblDi3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi3.Size = New System.Drawing.Size(86, 23)
        Me.lblDi3.TabIndex = 18
        Me.lblDi3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi2
        '
        Me.lblDi2.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi2.Location = New System.Drawing.Point(360, 108)
        Me.lblDi2.Name = "lblDi2"
        Me.lblDi2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi2.Size = New System.Drawing.Size(86, 23)
        Me.lblDi2.TabIndex = 17
        Me.lblDi2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi1
        '
        Me.lblDi1.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi1.Location = New System.Drawing.Point(272, 107)
        Me.lblDi1.Name = "lblDi1"
        Me.lblDi1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi1.Size = New System.Drawing.Size(86, 23)
        Me.lblDi1.TabIndex = 16
        Me.lblDi1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDoStart
        '
        Me.lblDoStart.AutoSize = True
        Me.lblDoStart.BackColor = System.Drawing.SystemColors.Control
        Me.lblDoStart.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDoStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDoStart.Location = New System.Drawing.Point(16, 137)
        Me.lblDoStart.Name = "lblDoStart"
        Me.lblDoStart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDoStart.Size = New System.Drawing.Size(119, 12)
        Me.lblDoStart.TabIndex = 15
        Me.lblDoStart.Text = "DO Start FU Address"
        Me.lblDoStart.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblStatusDi5
        '
        Me.lblStatusDi5.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatusDi5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusDi5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatusDi5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatusDi5.Location = New System.Drawing.Point(624, 83)
        Me.lblStatusDi5.Name = "lblStatusDi5"
        Me.lblStatusDi5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatusDi5.Size = New System.Drawing.Size(86, 19)
        Me.lblStatusDi5.TabIndex = 10
        Me.lblStatusDi5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatusDi4
        '
        Me.lblStatusDi4.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatusDi4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusDi4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatusDi4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatusDi4.Location = New System.Drawing.Point(536, 83)
        Me.lblStatusDi4.Name = "lblStatusDi4"
        Me.lblStatusDi4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatusDi4.Size = New System.Drawing.Size(86, 19)
        Me.lblStatusDi4.TabIndex = 9
        Me.lblStatusDi4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatusDi3
        '
        Me.lblStatusDi3.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatusDi3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusDi3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatusDi3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatusDi3.Location = New System.Drawing.Point(448, 83)
        Me.lblStatusDi3.Name = "lblStatusDi3"
        Me.lblStatusDi3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatusDi3.Size = New System.Drawing.Size(86, 19)
        Me.lblStatusDi3.TabIndex = 8
        Me.lblStatusDi3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatusDi2
        '
        Me.lblStatusDi2.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatusDi2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusDi2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatusDi2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatusDi2.Location = New System.Drawing.Point(360, 83)
        Me.lblStatusDi2.Name = "lblStatusDi2"
        Me.lblStatusDi2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatusDi2.Size = New System.Drawing.Size(86, 19)
        Me.lblStatusDi2.TabIndex = 7
        Me.lblStatusDi2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatusDi1
        '
        Me.lblStatusDi1.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatusDi1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusDi1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatusDi1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatusDi1.Location = New System.Drawing.Point(272, 83)
        Me.lblStatusDi1.Name = "lblStatusDi1"
        Me.lblStatusDi1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatusDi1.Size = New System.Drawing.Size(86, 19)
        Me.lblStatusDi1.TabIndex = 6
        Me.lblStatusDi1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDiStart
        '
        Me.lblDiStart.AutoSize = True
        Me.lblDiStart.BackColor = System.Drawing.SystemColors.Control
        Me.lblDiStart.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDiStart.Location = New System.Drawing.Point(16, 111)
        Me.lblDiStart.Name = "lblDiStart"
        Me.lblDiStart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiStart.Size = New System.Drawing.Size(119, 12)
        Me.lblDiStart.TabIndex = 4
        Me.lblDiStart.Text = "DI Start FU Address"
        Me.lblDiStart.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(496, 58)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Status I"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label11.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnHANEI)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.cmbRUN)
        Me.GroupBox2.Controls.Add(Me.cmbM)
        Me.GroupBox2.Controls.Add(Me.cmbR)
        Me.GroupBox2.Controls.Add(Me.lblBitPosDi5)
        Me.GroupBox2.Controls.Add(Me.lblBitPosDi4)
        Me.GroupBox2.Controls.Add(Me.lblBitPosDi3)
        Me.GroupBox2.Controls.Add(Me.lblBitPosDi2)
        Me.GroupBox2.Controls.Add(Me.lblBitPosDi1)
        Me.GroupBox2.Controls.Add(Me.lblControlType)
        Me.GroupBox2.Controls.Add(Me.cmbControlType)
        Me.GroupBox2.Controls.Add(Me.txtPulseWidth)
        Me.GroupBox2.Controls.Add(Me.lblPulseWidth)
        Me.GroupBox2.Controls.Add(Me.fraHiHi0)
        Me.GroupBox2.Controls.Add(Me.txtStatusDo5)
        Me.GroupBox2.Controls.Add(Me.txtStatusDo4)
        Me.GroupBox2.Controls.Add(Me.txtStatusDo3)
        Me.GroupBox2.Controls.Add(Me.txtStatusDo2)
        Me.GroupBox2.Controls.Add(Me.txtStatusDo1)
        Me.GroupBox2.Controls.Add(Me.chkStatusAlarm)
        Me.GroupBox2.Controls.Add(Me.txtFilterCoeficient)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtPinDo)
        Me.GroupBox2.Controls.Add(Me.txtPortNoDo)
        Me.GroupBox2.Controls.Add(Me.txtFuNoDo)
        Me.GroupBox2.Controls.Add(Me.txtPinDi)
        Me.GroupBox2.Controls.Add(Me.txtPortNoDi)
        Me.GroupBox2.Controls.Add(Me.txtFuNoDi)
        Me.GroupBox2.Controls.Add(Me.txtStatusOut)
        Me.GroupBox2.Controls.Add(Me.cmbStatusOut)
        Me.GroupBox2.Controls.Add(Me.lblStatusOut)
        Me.GroupBox2.Controls.Add(Me.txtStatusIn)
        Me.GroupBox2.Controls.Add(Me.cmbExtDevice)
        Me.GroupBox2.Controls.Add(Me.cmbDataType)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.cmbStatusIn)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.lblDiStart)
        Me.GroupBox2.Controls.Add(Me.lblStatusDi1)
        Me.GroupBox2.Controls.Add(Me.lblStatusDi2)
        Me.GroupBox2.Controls.Add(Me.lblStatusDi3)
        Me.GroupBox2.Controls.Add(Me.lblStatusDi4)
        Me.GroupBox2.Controls.Add(Me.lblStatusDi5)
        Me.GroupBox2.Controls.Add(Me.lblDo5)
        Me.GroupBox2.Controls.Add(Me.lblDo4)
        Me.GroupBox2.Controls.Add(Me.lblDo3)
        Me.GroupBox2.Controls.Add(Me.lblDoStart)
        Me.GroupBox2.Controls.Add(Me.lblDo2)
        Me.GroupBox2.Controls.Add(Me.lblDi1)
        Me.GroupBox2.Controls.Add(Me.lblDo1)
        Me.GroupBox2.Controls.Add(Me.lblDi2)
        Me.GroupBox2.Controls.Add(Me.lblDi5)
        Me.GroupBox2.Controls.Add(Me.lblDi3)
        Me.GroupBox2.Controls.Add(Me.lblDi4)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 304)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(740, 343)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "MOTOR"
        '
        'btnHANEI
        '
        Me.btnHANEI.Location = New System.Drawing.Point(78, 55)
        Me.btnHANEI.Name = "btnHANEI"
        Me.btnHANEI.Size = New System.Drawing.Size(32, 23)
        Me.btnHANEI.TabIndex = 249
        Me.btnHANEI.Text = "GO"
        Me.btnHANEI.UseVisualStyleBackColor = True
        Me.btnHANEI.Visible = False
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(220, 15)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label29.Size = New System.Drawing.Size(101, 12)
        Me.Label29.TabIndex = 248
        Me.Label29.Text = "STATUS:(RUN etc)"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(94, 15)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label28.Size = New System.Drawing.Size(95, 12)
        Me.Label28.TabIndex = 247
        Me.Label28.Text = "INSG:(M0,M1,M2)"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(15, 15)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label20.Size = New System.Drawing.Size(65, 12)
        Me.Label20.TabIndex = 246
        Me.Label20.Text = "S:("""",R,J)"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbRUN
        '
        Me.cmbRUN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRUN.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbRUN.FormattingEnabled = True
        Me.cmbRUN.Location = New System.Drawing.Point(222, 30)
        Me.cmbRUN.Name = "cmbRUN"
        Me.cmbRUN.Size = New System.Drawing.Size(194, 20)
        Me.cmbRUN.TabIndex = 245
        '
        'cmbM
        '
        Me.cmbM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbM.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbM.FormattingEnabled = True
        Me.cmbM.Location = New System.Drawing.Point(94, 30)
        Me.cmbM.Name = "cmbM"
        Me.cmbM.Size = New System.Drawing.Size(122, 20)
        Me.cmbM.TabIndex = 244
        '
        'cmbR
        '
        Me.cmbR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbR.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbR.FormattingEnabled = True
        Me.cmbR.Location = New System.Drawing.Point(15, 30)
        Me.cmbR.Name = "cmbR"
        Me.cmbR.Size = New System.Drawing.Size(70, 20)
        Me.cmbR.TabIndex = 243
        '
        'lblBitPosDi5
        '
        Me.lblBitPosDi5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBitPosDi5.Location = New System.Drawing.Point(688, 187)
        Me.lblBitPosDi5.Name = "lblBitPosDi5"
        Me.lblBitPosDi5.Size = New System.Drawing.Size(22, 15)
        Me.lblBitPosDi5.TabIndex = 238
        Me.lblBitPosDi5.Visible = False
        '
        'lblBitPosDi4
        '
        Me.lblBitPosDi4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBitPosDi4.Location = New System.Drawing.Point(600, 187)
        Me.lblBitPosDi4.Name = "lblBitPosDi4"
        Me.lblBitPosDi4.Size = New System.Drawing.Size(22, 15)
        Me.lblBitPosDi4.TabIndex = 238
        Me.lblBitPosDi4.Visible = False
        '
        'lblBitPosDi3
        '
        Me.lblBitPosDi3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBitPosDi3.Location = New System.Drawing.Point(512, 187)
        Me.lblBitPosDi3.Name = "lblBitPosDi3"
        Me.lblBitPosDi3.Size = New System.Drawing.Size(22, 15)
        Me.lblBitPosDi3.TabIndex = 238
        Me.lblBitPosDi3.Visible = False
        '
        'lblBitPosDi2
        '
        Me.lblBitPosDi2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBitPosDi2.Location = New System.Drawing.Point(424, 187)
        Me.lblBitPosDi2.Name = "lblBitPosDi2"
        Me.lblBitPosDi2.Size = New System.Drawing.Size(22, 15)
        Me.lblBitPosDi2.TabIndex = 238
        Me.lblBitPosDi2.Visible = False
        '
        'lblBitPosDi1
        '
        Me.lblBitPosDi1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBitPosDi1.Location = New System.Drawing.Point(336, 187)
        Me.lblBitPosDi1.Name = "lblBitPosDi1"
        Me.lblBitPosDi1.Size = New System.Drawing.Size(22, 15)
        Me.lblBitPosDi1.TabIndex = 238
        Me.lblBitPosDi1.Visible = False
        '
        'lblControlType
        '
        Me.lblControlType.AutoSize = True
        Me.lblControlType.BackColor = System.Drawing.SystemColors.Control
        Me.lblControlType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblControlType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblControlType.Location = New System.Drawing.Point(19, 267)
        Me.lblControlType.Name = "lblControlType"
        Me.lblControlType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblControlType.Size = New System.Drawing.Size(119, 12)
        Me.lblControlType.TabIndex = 237
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
        Me.cmbControlType.Location = New System.Drawing.Point(144, 263)
        Me.cmbControlType.Name = "cmbControlType"
        Me.cmbControlType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbControlType.Size = New System.Drawing.Size(80, 20)
        Me.cmbControlType.TabIndex = 20
        '
        'txtPulseWidth
        '
        Me.txtPulseWidth.AcceptsReturn = True
        Me.txtPulseWidth.Location = New System.Drawing.Point(144, 291)
        Me.txtPulseWidth.MaxLength = 0
        Me.txtPulseWidth.Name = "txtPulseWidth"
        Me.txtPulseWidth.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPulseWidth.Size = New System.Drawing.Size(48, 19)
        Me.txtPulseWidth.TabIndex = 21
        '
        'lblPulseWidth
        '
        Me.lblPulseWidth.AutoSize = True
        Me.lblPulseWidth.BackColor = System.Drawing.SystemColors.Control
        Me.lblPulseWidth.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPulseWidth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPulseWidth.Location = New System.Drawing.Point(24, 295)
        Me.lblPulseWidth.Name = "lblPulseWidth"
        Me.lblPulseWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPulseWidth.Size = New System.Drawing.Size(113, 12)
        Me.lblPulseWidth.TabIndex = 235
        Me.lblPulseWidth.Text = "Output pulse width"
        Me.lblPulseWidth.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraHiHi0
        '
        Me.fraHiHi0.BackColor = System.Drawing.SystemColors.Control
        Me.fraHiHi0.Controls.Add(Me.Label12)
        Me.fraHiHi0.Controls.Add(Me.Label13)
        Me.fraHiHi0.Controls.Add(Me.txtAlarmTimeup)
        Me.fraHiHi0.Controls.Add(Me.Label14)
        Me.fraHiHi0.Controls.Add(Me.cmbTime)
        Me.fraHiHi0.Controls.Add(Me.lblStatus)
        Me.fraHiHi0.Controls.Add(Me.txtStatusDo)
        Me.fraHiHi0.Controls.Add(Me.txtGRep2Do)
        Me.fraHiHi0.Controls.Add(Me.txtGRep1Do)
        Me.fraHiHi0.Controls.Add(Me.txtExtGDo)
        Me.fraHiHi0.Controls.Add(Me.txtDelayDo)
        Me.fraHiHi0.Controls.Add(Me.Label128)
        Me.fraHiHi0.Controls.Add(Me.Label26)
        Me.fraHiHi0.Controls.Add(Me.Label30)
        Me.fraHiHi0.Controls.Add(Me.Label40)
        Me.fraHiHi0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraHiHi0.Location = New System.Drawing.Point(268, 215)
        Me.fraHiHi0.Name = "fraHiHi0"
        Me.fraHiHi0.Padding = New System.Windows.Forms.Padding(0)
        Me.fraHiHi0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraHiHi0.Size = New System.Drawing.Size(452, 116)
        Me.fraHiHi0.TabIndex = 22
        Me.fraHiHi0.TabStop = False
        Me.fraHiHi0.Text = "Feedback Alarm"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(16, 83)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(107, 12)
        Me.Label12.TabIndex = 142
        Me.Label12.Text = "Alarm Timer Count"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(182, 83)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(23, 12)
        Me.Label13.TabIndex = 143
        Me.Label13.Text = "sec"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAlarmTimeup
        '
        Me.txtAlarmTimeup.AcceptsReturn = True
        Me.txtAlarmTimeup.Location = New System.Drawing.Point(128, 80)
        Me.txtAlarmTimeup.MaxLength = 0
        Me.txtAlarmTimeup.Name = "txtAlarmTimeup"
        Me.txtAlarmTimeup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlarmTimeup.Size = New System.Drawing.Size(48, 19)
        Me.txtAlarmTimeup.TabIndex = 7
        Me.txtAlarmTimeup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(320, 28)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(119, 12)
        Me.Label14.TabIndex = 141
        Me.Label14.Text = "Unit of Delay Timer"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbTime
        '
        Me.cmbTime.BackColor = System.Drawing.SystemColors.Window
        Me.cmbTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbTime.Location = New System.Drawing.Point(372, 48)
        Me.cmbTime.Name = "cmbTime"
        Me.cmbTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbTime.Size = New System.Drawing.Size(64, 20)
        Me.cmbTime.TabIndex = 6
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatus.Location = New System.Drawing.Point(246, 28)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.Size = New System.Drawing.Size(41, 12)
        Me.lblStatus.TabIndex = 116
        Me.lblStatus.Text = "Status"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtStatusDo
        '
        Me.txtStatusDo.AcceptsReturn = True
        Me.txtStatusDo.Location = New System.Drawing.Point(230, 50)
        Me.txtStatusDo.MaxLength = 0
        Me.txtStatusDo.Name = "txtStatusDo"
        Me.txtStatusDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusDo.Size = New System.Drawing.Size(72, 19)
        Me.txtStatusDo.TabIndex = 5
        '
        'txtGRep2Do
        '
        Me.txtGRep2Do.AcceptsReturn = True
        Me.txtGRep2Do.Location = New System.Drawing.Point(176, 50)
        Me.txtGRep2Do.MaxLength = 0
        Me.txtGRep2Do.Name = "txtGRep2Do"
        Me.txtGRep2Do.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2Do.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep2Do.TabIndex = 4
        Me.txtGRep2Do.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1Do
        '
        Me.txtGRep1Do.AcceptsReturn = True
        Me.txtGRep1Do.Location = New System.Drawing.Point(122, 50)
        Me.txtGRep1Do.MaxLength = 0
        Me.txtGRep1Do.Name = "txtGRep1Do"
        Me.txtGRep1Do.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1Do.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep1Do.TabIndex = 3
        Me.txtGRep1Do.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExtGDo
        '
        Me.txtExtGDo.AcceptsReturn = True
        Me.txtExtGDo.Location = New System.Drawing.Point(14, 50)
        Me.txtExtGDo.MaxLength = 0
        Me.txtExtGDo.Name = "txtExtGDo"
        Me.txtExtGDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGDo.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGDo.TabIndex = 1
        Me.txtExtGDo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelayDo
        '
        Me.txtDelayDo.AcceptsReturn = True
        Me.txtDelayDo.Location = New System.Drawing.Point(68, 50)
        Me.txtDelayDo.MaxLength = 0
        Me.txtDelayDo.Name = "txtDelayDo"
        Me.txtDelayDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayDo.Size = New System.Drawing.Size(48, 19)
        Me.txtDelayDo.TabIndex = 2
        Me.txtDelayDo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label128
        '
        Me.Label128.AutoSize = True
        Me.Label128.BackColor = System.Drawing.SystemColors.Control
        Me.Label128.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label128.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label128.Location = New System.Drawing.Point(69, 28)
        Me.Label128.Name = "Label128"
        Me.Label128.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label128.Size = New System.Drawing.Size(35, 12)
        Me.Label128.TabIndex = 60
        Me.Label128.Text = "Delay"
        Me.Label128.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(15, 28)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(35, 12)
        Me.Label26.TabIndex = 58
        Me.Label26.Text = "EXT.G"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(123, 28)
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
        Me.Label40.Location = New System.Drawing.Point(177, 28)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(41, 12)
        Me.Label40.TabIndex = 56
        Me.Label40.Text = "G REP2"
        '
        'txtStatusDo5
        '
        Me.txtStatusDo5.AcceptsReturn = True
        Me.txtStatusDo5.Location = New System.Drawing.Point(624, 165)
        Me.txtStatusDo5.MaxLength = 0
        Me.txtStatusDo5.Name = "txtStatusDo5"
        Me.txtStatusDo5.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo5.TabIndex = 17
        Me.txtStatusDo5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStatusDo4
        '
        Me.txtStatusDo4.AcceptsReturn = True
        Me.txtStatusDo4.Location = New System.Drawing.Point(536, 165)
        Me.txtStatusDo4.MaxLength = 0
        Me.txtStatusDo4.Name = "txtStatusDo4"
        Me.txtStatusDo4.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo4.TabIndex = 16
        Me.txtStatusDo4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStatusDo3
        '
        Me.txtStatusDo3.AcceptsReturn = True
        Me.txtStatusDo3.Location = New System.Drawing.Point(448, 165)
        Me.txtStatusDo3.MaxLength = 0
        Me.txtStatusDo3.Name = "txtStatusDo3"
        Me.txtStatusDo3.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo3.TabIndex = 15
        Me.txtStatusDo3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStatusDo2
        '
        Me.txtStatusDo2.AcceptsReturn = True
        Me.txtStatusDo2.Location = New System.Drawing.Point(360, 165)
        Me.txtStatusDo2.MaxLength = 0
        Me.txtStatusDo2.Name = "txtStatusDo2"
        Me.txtStatusDo2.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo2.TabIndex = 14
        Me.txtStatusDo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStatusDo1
        '
        Me.txtStatusDo1.AcceptsReturn = True
        Me.txtStatusDo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtStatusDo1.Location = New System.Drawing.Point(272, 165)
        Me.txtStatusDo1.MaxLength = 0
        Me.txtStatusDo1.Name = "txtStatusDo1"
        Me.txtStatusDo1.Size = New System.Drawing.Size(86, 19)
        Me.txtStatusDo1.TabIndex = 13
        Me.txtStatusDo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkStatusAlarm
        '
        Me.chkStatusAlarm.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatusAlarm.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatusAlarm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatusAlarm.Location = New System.Drawing.Point(60, 207)
        Me.chkStatusAlarm.Name = "chkStatusAlarm"
        Me.chkStatusAlarm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkStatusAlarm.Size = New System.Drawing.Size(98, 19)
        Me.chkStatusAlarm.TabIndex = 18
        Me.chkStatusAlarm.Text = "Status Alarm"
        Me.chkStatusAlarm.UseVisualStyleBackColor = True
        Me.chkStatusAlarm.Visible = False
        '
        'txtFilterCoeficient
        '
        Me.txtFilterCoeficient.AcceptsReturn = True
        Me.txtFilterCoeficient.Location = New System.Drawing.Point(144, 235)
        Me.txtFilterCoeficient.MaxLength = 0
        Me.txtFilterCoeficient.Name = "txtFilterCoeficient"
        Me.txtFilterCoeficient.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFilterCoeficient.Size = New System.Drawing.Size(48, 19)
        Me.txtFilterCoeficient.TabIndex = 19
        Me.txtFilterCoeficient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(198, 238)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(47, 12)
        Me.Label15.TabIndex = 99
        Me.Label15.Text = "* 4msec"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(31, 238)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(107, 12)
        Me.Label6.TabIndex = 98
        Me.Label6.Text = "Filter Coeficient"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPinDo
        '
        Me.txtPinDo.AcceptsReturn = True
        Me.txtPinDo.Location = New System.Drawing.Point(228, 135)
        Me.txtPinDo.MaxLength = 0
        Me.txtPinDo.Name = "txtPinDo"
        Me.txtPinDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinDo.Size = New System.Drawing.Size(40, 19)
        Me.txtPinDo.TabIndex = 12
        '
        'txtPortNoDo
        '
        Me.txtPortNoDo.AcceptsReturn = True
        Me.txtPortNoDo.Location = New System.Drawing.Point(186, 135)
        Me.txtPortNoDo.MaxLength = 0
        Me.txtPortNoDo.Name = "txtPortNoDo"
        Me.txtPortNoDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNoDo.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNoDo.TabIndex = 11
        '
        'txtFuNoDo
        '
        Me.txtFuNoDo.AcceptsReturn = True
        Me.txtFuNoDo.Location = New System.Drawing.Point(144, 135)
        Me.txtFuNoDo.MaxLength = 0
        Me.txtFuNoDo.Name = "txtFuNoDo"
        Me.txtFuNoDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNoDo.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNoDo.TabIndex = 10
        '
        'txtPinDi
        '
        Me.txtPinDi.AcceptsReturn = True
        Me.txtPinDi.Location = New System.Drawing.Point(228, 107)
        Me.txtPinDi.MaxLength = 0
        Me.txtPinDi.Name = "txtPinDi"
        Me.txtPinDi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinDi.Size = New System.Drawing.Size(40, 19)
        Me.txtPinDi.TabIndex = 9
        '
        'txtPortNoDi
        '
        Me.txtPortNoDi.AcceptsReturn = True
        Me.txtPortNoDi.Location = New System.Drawing.Point(186, 107)
        Me.txtPortNoDi.MaxLength = 0
        Me.txtPortNoDi.Name = "txtPortNoDi"
        Me.txtPortNoDi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNoDi.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNoDi.TabIndex = 8
        '
        'txtFuNoDi
        '
        Me.txtFuNoDi.AcceptsReturn = True
        Me.txtFuNoDi.Location = New System.Drawing.Point(144, 107)
        Me.txtFuNoDi.MaxLength = 0
        Me.txtFuNoDi.Name = "txtFuNoDi"
        Me.txtFuNoDi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNoDi.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNoDi.TabIndex = 7
        '
        'txtStatusOut
        '
        Me.txtStatusOut.AcceptsReturn = True
        Me.txtStatusOut.Location = New System.Drawing.Point(112, 187)
        Me.txtStatusOut.MaxLength = 0
        Me.txtStatusOut.Name = "txtStatusOut"
        Me.txtStatusOut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusOut.Size = New System.Drawing.Size(127, 19)
        Me.txtStatusOut.TabIndex = 5
        Me.txtStatusOut.Visible = False
        '
        'cmbStatusOut
        '
        Me.cmbStatusOut.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatusOut.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatusOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatusOut.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbStatusOut.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatusOut.Location = New System.Drawing.Point(112, 165)
        Me.cmbStatusOut.Name = "cmbStatusOut"
        Me.cmbStatusOut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatusOut.Size = New System.Drawing.Size(157, 20)
        Me.cmbStatusOut.TabIndex = 4
        '
        'lblStatusOut
        '
        Me.lblStatusOut.AutoSize = True
        Me.lblStatusOut.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatusOut.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatusOut.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatusOut.Location = New System.Drawing.Point(17, 172)
        Me.lblStatusOut.Name = "lblStatusOut"
        Me.lblStatusOut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatusOut.Size = New System.Drawing.Size(89, 12)
        Me.lblStatusOut.TabIndex = 124
        Me.lblStatusOut.Text = "Status(Output)"
        Me.lblStatusOut.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtStatusIn
        '
        Me.txtStatusIn.AcceptsReturn = True
        Me.txtStatusIn.Location = New System.Drawing.Point(676, 55)
        Me.txtStatusIn.MaxLength = 0
        Me.txtStatusIn.Name = "txtStatusIn"
        Me.txtStatusIn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusIn.Size = New System.Drawing.Size(51, 19)
        Me.txtStatusIn.TabIndex = 3
        Me.txtStatusIn.Visible = False
        '
        'cmbExtDevice
        '
        Me.cmbExtDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExtDevice.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbExtDevice.FormattingEnabled = True
        Me.cmbExtDevice.Location = New System.Drawing.Point(422, 29)
        Me.cmbExtDevice.Name = "cmbExtDevice"
        Me.cmbExtDevice.Size = New System.Drawing.Size(240, 20)
        Me.cmbExtDevice.TabIndex = 1
        '
        'cmbDataType
        '
        Me.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbDataType.FormattingEnabled = True
        Me.cmbDataType.Location = New System.Drawing.Point(111, 56)
        Me.cmbDataType.Name = "cmbDataType"
        Me.cmbDataType.Size = New System.Drawing.Size(157, 20)
        Me.cmbDataType.TabIndex = 0
        Me.cmbDataType.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(17, 59)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(59, 12)
        Me.Label8.TabIndex = 113
        Me.Label8.Text = "Data Type"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label8.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtAlmMimic)
        Me.GroupBox1.Controls.Add(Me.Label31)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.cmbAlmLvl)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtTagNo)
        Me.GroupBox1.Controls.Add(Me.chkMrepose)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
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
        Me.GroupBox1.Size = New System.Drawing.Size(740, 284)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Common"
        '
        'txtAlmMimic
        '
        Me.txtAlmMimic.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtAlmMimic.Location = New System.Drawing.Point(640, 167)
        Me.txtAlmMimic.MaxLength = 0
        Me.txtAlmMimic.Name = "txtAlmMimic"
        Me.txtAlmMimic.Size = New System.Drawing.Size(48, 19)
        Me.txtAlmMimic.TabIndex = 115
        Me.txtAlmMimic.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtAlmMimic.Visible = False
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(633, 153)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(101, 12)
        Me.Label31.TabIndex = 116
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
        Me.Label27.Location = New System.Drawing.Point(283, 172)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(71, 12)
        Me.Label27.TabIndex = 114
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
        Me.cmbAlmLvl.Location = New System.Drawing.Point(360, 168)
        Me.cmbAlmLvl.Name = "cmbAlmLvl"
        Me.cmbAlmLvl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAlmLvl.Size = New System.Drawing.Size(97, 20)
        Me.cmbAlmLvl.TabIndex = 113
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(175, 53)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(47, 12)
        Me.Label5.TabIndex = 70
        Me.Label5.Text = "Tag No."
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTagNo
        '
        Me.txtTagNo.AcceptsReturn = True
        Me.txtTagNo.Location = New System.Drawing.Point(228, 50)
        Me.txtTagNo.MaxLength = 16
        Me.txtTagNo.Name = "txtTagNo"
        Me.txtTagNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTagNo.Size = New System.Drawing.Size(127, 19)
        Me.txtTagNo.TabIndex = 103
        '
        'chkMrepose
        '
        Me.chkMrepose.BackColor = System.Drawing.SystemColors.Control
        Me.chkMrepose.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMrepose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMrepose.Location = New System.Drawing.Point(472, 168)
        Me.chkMrepose.Name = "chkMrepose"
        Me.chkMrepose.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkMrepose.Size = New System.Drawing.Size(120, 19)
        Me.chkMrepose.TabIndex = 102
        Me.chkMrepose.Text = "Manual Repose"
        Me.chkMrepose.UseVisualStyleBackColor = True
        Me.chkMrepose.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtShareChid)
        Me.GroupBox5.Controls.Add(Me.lblShareChid)
        Me.GroupBox5.Controls.Add(Me.lblShareType)
        Me.GroupBox5.Controls.Add(Me.cmbShareType)
        Me.GroupBox5.Location = New System.Drawing.Point(452, 204)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(214, 72)
        Me.GroupBox5.TabIndex = 6
        Me.GroupBox5.TabStop = False
        '
        'txtShareChid
        '
        Me.txtShareChid.AcceptsReturn = True
        Me.txtShareChid.Location = New System.Drawing.Point(108, 44)
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
        Me.lblShareChid.Location = New System.Drawing.Point(12, 48)
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
        Me.lblShareType.Location = New System.Drawing.Point(32, 20)
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
        Me.cmbShareType.Location = New System.Drawing.Point(108, 16)
        Me.cmbShareType.Name = "cmbShareType"
        Me.cmbShareType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbShareType.Size = New System.Drawing.Size(88, 20)
        Me.cmbShareType.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.txtMotorCol)
        Me.GroupBox3.Controls.Add(Me.Label32)
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
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox3.Location = New System.Drawing.Point(16, 202)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox3.Size = New System.Drawing.Size(428, 74)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Flag"
        '
        'txtMotorCol
        '
        Me.txtMotorCol.AcceptsReturn = True
        Me.txtMotorCol.Location = New System.Drawing.Point(388, 36)
        Me.txtMotorCol.MaxLength = 0
        Me.txtMotorCol.Name = "txtMotorCol"
        Me.txtMotorCol.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtMotorCol.Size = New System.Drawing.Size(28, 19)
        Me.txtMotorCol.TabIndex = 86
        Me.txtMotorCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(383, 12)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(41, 24)
        Me.Label32.TabIndex = 85
        Me.Label32.Text = "-C,-D" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "invert"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBitSet
        '
        Me.lblBitSet.AutoSize = True
        Me.lblBitSet.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitSet.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBitSet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitSet.Location = New System.Drawing.Point(84, 58)
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
        Me.txtSP.Location = New System.Drawing.Point(348, 36)
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
        Me.txtPLC.Location = New System.Drawing.Point(312, 36)
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
        Me.txtEP.Location = New System.Drawing.Point(276, 36)
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
        Me.txtAC.Location = New System.Drawing.Point(240, 36)
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
        Me.txtRL.Location = New System.Drawing.Point(204, 36)
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
        Me.txtWK.Location = New System.Drawing.Point(168, 36)
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
        Me.txtGWS.Location = New System.Drawing.Point(132, 36)
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
        Me.txtSio.Location = New System.Drawing.Point(96, 36)
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
        Me.txtSC.Location = New System.Drawing.Point(60, 36)
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
        Me.txtDmy.Location = New System.Drawing.Point(24, 36)
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
        Me.Label25.Location = New System.Drawing.Point(352, 20)
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
        Me.Label22.Location = New System.Drawing.Point(312, 20)
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
        Me.Label23.Location = New System.Drawing.Point(276, 20)
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
        Me.Label24.Location = New System.Drawing.Point(240, 20)
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
        Me.Label2.Location = New System.Drawing.Point(204, 20)
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
        Me.Label19.Location = New System.Drawing.Point(168, 20)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(17, 12)
        Me.Label19.TabIndex = 76
        Me.Label19.Text = "WK"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(132, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(23, 12)
        Me.Label21.TabIndex = 75
        Me.Label21.Text = "GWS"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(96, 20)
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
        Me.Label9.Location = New System.Drawing.Point(60, 20)
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
        Me.Label10.Location = New System.Drawing.Point(24, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(23, 12)
        Me.Label10.TabIndex = 72
        Me.Label10.Text = "Dmy"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox4.Controls.Add(Me.txtDelayTimer)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.txtExtGroup)
        Me.GroupBox4.Controls.Add(Me.txtGRep1)
        Me.GroupBox4.Controls.Add(Me.txtGRep2)
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox4.Location = New System.Drawing.Point(16, 130)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox4.Size = New System.Drawing.Size(256, 68)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Alarm"
        '
        'txtDelayTimer
        '
        Me.txtDelayTimer.AcceptsReturn = True
        Me.txtDelayTimer.Enabled = False
        Me.txtDelayTimer.Location = New System.Drawing.Point(76, 42)
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
        Me.Label1.Location = New System.Drawing.Point(76, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(35, 12)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "Delay"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(24, 24)
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
        Me.Label17.Location = New System.Drawing.Point(132, 24)
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
        Me.Label18.Location = New System.Drawing.Point(184, 24)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(41, 12)
        Me.Label18.TabIndex = 56
        Me.Label18.Text = "G REP2"
        '
        'txtExtGroup
        '
        Me.txtExtGroup.AcceptsReturn = True
        Me.txtExtGroup.Location = New System.Drawing.Point(22, 42)
        Me.txtExtGroup.MaxLength = 0
        Me.txtExtGroup.Name = "txtExtGroup"
        Me.txtExtGroup.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGroup.TabIndex = 0
        Me.txtExtGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1
        '
        Me.txtGRep1.AcceptsReturn = True
        Me.txtGRep1.Location = New System.Drawing.Point(130, 42)
        Me.txtGRep1.MaxLength = 0
        Me.txtGRep1.Name = "txtGRep1"
        Me.txtGRep1.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep1.TabIndex = 2
        Me.txtGRep1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep2
        '
        Me.txtGRep2.AcceptsReturn = True
        Me.txtGRep2.Location = New System.Drawing.Point(184, 42)
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
        Me.cmbSysNo.Location = New System.Drawing.Point(90, 21)
        Me.cmbSysNo.Name = "cmbSysNo"
        Me.cmbSysNo.Size = New System.Drawing.Size(110, 20)
        Me.cmbSysNo.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.Location = New System.Drawing.Point(90, 104)
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
        Me.Label36.Location = New System.Drawing.Point(28, 107)
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
        Me.txtItemName.Location = New System.Drawing.Point(90, 77)
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
        Me.Label3.Location = New System.Drawing.Point(20, 80)
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
        Me.txtChNo.Location = New System.Drawing.Point(90, 48)
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
        Me.Label7.Location = New System.Drawing.Point(37, 53)
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
        Me.Label37.Location = New System.Drawing.Point(35, 24)
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
        Me.cmdBeforeCH.Location = New System.Drawing.Point(12, 653)
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
        Me.cmdNextCH.Location = New System.Drawing.Point(136, 653)
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
        Me.lblDummy.Location = New System.Drawing.Point(657, 3)
        Me.lblDummy.Name = "lblDummy"
        Me.lblDummy.Size = New System.Drawing.Size(95, 12)
        Me.lblDummy.TabIndex = 11
        Me.lblDummy.Text = "F5:DummySetting"
        '
        'frmChListMotor
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(765, 698)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblDummy)
        Me.Controls.Add(Me.cmdBeforeCH)
        Me.Controls.Add(Me.cmdNextCH)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 30)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChListMotor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CHANNEL LIST MOTOR"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.fraHiHi0.ResumeLayout(False)
        Me.fraHiHi0.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents chkStatusAlarm As System.Windows.Forms.CheckBox
    Public WithEvents txtFilterCoeficient As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Public WithEvents txtDelayTimer As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
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
    Friend WithEvents cmbExtDevice As System.Windows.Forms.ComboBox
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
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents txtStatusIn As System.Windows.Forms.TextBox
    Public WithEvents txtStatusOut As System.Windows.Forms.TextBox
    Public WithEvents cmbStatusOut As System.Windows.Forms.ComboBox
    Public WithEvents lblStatusOut As System.Windows.Forms.Label
    Public WithEvents txtPinDi As System.Windows.Forms.TextBox
    Public WithEvents txtPortNoDi As System.Windows.Forms.TextBox
    Public WithEvents txtFuNoDi As System.Windows.Forms.TextBox
    Public WithEvents txtPinDo As System.Windows.Forms.TextBox
    Public WithEvents txtPortNoDo As System.Windows.Forms.TextBox
    Public WithEvents txtFuNoDo As System.Windows.Forms.TextBox
    Public WithEvents cmdBeforeCH As System.Windows.Forms.Button
    Public WithEvents cmdNextCH As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Public WithEvents txtShareChid As System.Windows.Forms.TextBox
    Public WithEvents lblShareChid As System.Windows.Forms.Label
    Public WithEvents lblShareType As System.Windows.Forms.Label
    Public WithEvents cmbShareType As System.Windows.Forms.ComboBox
    Public WithEvents chkMrepose As System.Windows.Forms.CheckBox
    Public WithEvents fraHiHi0 As System.Windows.Forms.GroupBox
    Public WithEvents txtGRep2Do As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1Do As System.Windows.Forms.TextBox
    Public WithEvents txtExtGDo As System.Windows.Forms.TextBox
    Public WithEvents txtDelayDo As System.Windows.Forms.TextBox
    Public WithEvents Label128 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents txtStatusDo4 As System.Windows.Forms.TextBox
    Public WithEvents txtStatusDo3 As System.Windows.Forms.TextBox
    Public WithEvents txtStatusDo2 As System.Windows.Forms.TextBox
    Public WithEvents txtStatusDo1 As System.Windows.Forms.TextBox
    Public WithEvents txtStatusDo5 As System.Windows.Forms.TextBox
    Public WithEvents txtStatusDo As System.Windows.Forms.TextBox
    Public WithEvents lblStatus As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents cmbTime As System.Windows.Forms.ComboBox
    Public WithEvents txtPulseWidth As System.Windows.Forms.TextBox
    Public WithEvents lblPulseWidth As System.Windows.Forms.Label
    Public WithEvents lblControlType As System.Windows.Forms.Label
    Public WithEvents cmbControlType As System.Windows.Forms.ComboBox
    Friend WithEvents lblBitPosDi5 As System.Windows.Forms.Label
    Friend WithEvents lblBitPosDi4 As System.Windows.Forms.Label
    Friend WithEvents lblBitPosDi3 As System.Windows.Forms.Label
    Friend WithEvents lblBitPosDi2 As System.Windows.Forms.Label
    Friend WithEvents lblBitPosDi1 As System.Windows.Forms.Label
    Public WithEvents lblBitSet As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents txtAlarmTimeup As System.Windows.Forms.TextBox
    Friend WithEvents lblDummy As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents txtTagNo As System.Windows.Forms.TextBox
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents cmbAlmLvl As System.Windows.Forms.ComboBox
    Friend WithEvents btnHANEI As System.Windows.Forms.Button
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbRUN As System.Windows.Forms.ComboBox
    Friend WithEvents cmbM As System.Windows.Forms.ComboBox
    Friend WithEvents cmbR As System.Windows.Forms.ComboBox
    Public WithEvents txtAlmMimic As System.Windows.Forms.TextBox
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents txtMotorCol As System.Windows.Forms.TextBox
#End Region

End Class
