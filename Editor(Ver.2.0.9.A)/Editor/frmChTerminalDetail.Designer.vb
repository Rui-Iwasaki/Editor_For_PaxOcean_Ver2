<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChTerminalDetail
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
    Public WithEvents cmdSave As System.Windows.Forms.Button

    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents lblType2 As System.Windows.Forms.Label
    Public WithEvents lblType1 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblFCU As System.Windows.Forms.Label
    Public WithEvents LblFieldSt As System.Windows.Forms.Label


    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblType2 = New System.Windows.Forms.Label()
        Me.lblType1 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblFCU = New System.Windows.Forms.Label()
        Me.LblFieldSt = New System.Windows.Forms.Label()
        Me.cmdRtbSet = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbDataType = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdOutput = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbFunction = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbOutputMovement = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbChOutType = New System.Windows.Forms.ComboBox()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.cmdBefore = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.lblFUNo = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbDoTerm_b = New System.Windows.Forms.ComboBox()
        Me.cmbDoTerm_c = New System.Windows.Forms.ComboBox()
        Me.cmbDoTerm_d = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbDoTerm_a = New System.Windows.Forms.ComboBox()
        Me.grdTerminal = New Editor.clsDataGridViewPlus()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdTerminal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(873, 431)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 7
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(749, 431)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 6
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(348, 10)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(11, 12)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "-"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblType2
        '
        Me.lblType2.BackColor = System.Drawing.SystemColors.Control
        Me.lblType2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblType2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblType2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType2.Location = New System.Drawing.Point(365, 9)
        Me.lblType2.Name = "lblType2"
        Me.lblType2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType2.Size = New System.Drawing.Size(49, 16)
        Me.lblType2.TabIndex = 10
        Me.lblType2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblType1
        '
        Me.lblType1.BackColor = System.Drawing.SystemColors.Control
        Me.lblType1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblType1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblType1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType1.Location = New System.Drawing.Point(258, 9)
        Me.lblType1.Name = "lblType1"
        Me.lblType1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType1.Size = New System.Drawing.Size(84, 16)
        Me.lblType1.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(223, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(29, 12)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Type"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFCU
        '
        Me.lblFCU.BackColor = System.Drawing.SystemColors.Control
        Me.lblFCU.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFCU.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFCU.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFCU.Location = New System.Drawing.Point(85, 9)
        Me.lblFCU.Name = "lblFCU"
        Me.lblFCU.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFCU.Size = New System.Drawing.Size(132, 16)
        Me.lblFCU.TabIndex = 2
        '
        'LblFieldSt
        '
        Me.LblFieldSt.BackColor = System.Drawing.SystemColors.Control
        Me.LblFieldSt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblFieldSt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblFieldSt.Location = New System.Drawing.Point(6, 9)
        Me.LblFieldSt.Name = "LblFieldSt"
        Me.LblFieldSt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblFieldSt.Size = New System.Drawing.Size(73, 18)
        Me.LblFieldSt.TabIndex = 1
        Me.LblFieldSt.Text = "FCU_1  FCU"
        Me.LblFieldSt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdRtbSet
        '
        Me.cmdRtbSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdRtbSet.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRtbSet.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRtbSet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRtbSet.Location = New System.Drawing.Point(136, 431)
        Me.cmdRtbSet.Name = "cmdRtbSet"
        Me.cmdRtbSet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRtbSet.Size = New System.Drawing.Size(113, 33)
        Me.cmdRtbSet.TabIndex = 4
        Me.cmdRtbSet.Text = "Function Set"
        Me.cmdRtbSet.UseVisualStyleBackColor = True
        Me.cmdRtbSet.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbDataType)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmbUnit)
        Me.GroupBox1.Controls.Add(Me.Label38)
        Me.GroupBox1.Controls.Add(Me.cmbStatus)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 352)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(420, 72)
        Me.GroupBox1.TabIndex = 112
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Visible False"
        Me.GroupBox1.Visible = False
        '
        'cmbDataType
        '
        Me.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataType.FormattingEnabled = True
        Me.cmbDataType.Location = New System.Drawing.Point(255, 45)
        Me.cmbDataType.Name = "cmbDataType"
        Me.cmbDataType.Size = New System.Drawing.Size(157, 20)
        Me.cmbDataType.TabIndex = 116
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(188, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(59, 12)
        Me.Label8.TabIndex = 117
        Me.Label8.Text = "Data Type"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbUnit
        '
        Me.cmbUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbUnit.Location = New System.Drawing.Point(52, 44)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbUnit.Size = New System.Drawing.Size(120, 20)
        Me.cmbUnit.TabIndex = 115
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(16, 48)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(29, 12)
        Me.Label38.TabIndex = 114
        Me.Label38.Text = "Unit"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatus.Location = New System.Drawing.Point(52, 20)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatus.Size = New System.Drawing.Size(157, 20)
        Me.cmbStatus.TabIndex = 112
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 113
        Me.Label1.Text = "Status"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdOutput
        '
        Me.cmdOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdOutput.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOutput.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOutput.Location = New System.Drawing.Point(12, 431)
        Me.cmdOutput.Name = "cmdOutput"
        Me.cmdOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOutput.Size = New System.Drawing.Size(113, 33)
        Me.cmdOutput.TabIndex = 5
        Me.cmdOutput.Text = "OutPut Set"
        Me.cmdOutput.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.cmbFunction)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cmbOutputMovement)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.cmbChOutType)
        Me.GroupBox2.Location = New System.Drawing.Point(444, 352)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(468, 72)
        Me.GroupBox2.TabIndex = 114
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Visible False"
        Me.GroupBox2.Visible = False
        '
        'cmbFunction
        '
        Me.cmbFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFunction.FormattingEnabled = True
        Me.cmbFunction.Location = New System.Drawing.Point(304, 24)
        Me.cmbFunction.Name = "cmbFunction"
        Me.cmbFunction.Size = New System.Drawing.Size(147, 20)
        Me.cmbFunction.TabIndex = 111
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(244, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 110
        Me.Label3.Text = "Function"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbOutputMovement
        '
        Me.cmbOutputMovement.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOutputMovement.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOutputMovement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOutputMovement.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOutputMovement.Location = New System.Drawing.Point(132, 44)
        Me.cmbOutputMovement.Name = "cmbOutputMovement"
        Me.cmbOutputMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOutputMovement.Size = New System.Drawing.Size(105, 20)
        Me.cmbOutputMovement.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(28, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(95, 12)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Output Movement"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(12, 24)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(101, 12)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "CHOUT Type Setup"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbChOutType
        '
        Me.cmbChOutType.BackColor = System.Drawing.SystemColors.Window
        Me.cmbChOutType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbChOutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChOutType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbChOutType.Location = New System.Drawing.Point(132, 20)
        Me.cmbChOutType.Name = "cmbChOutType"
        Me.cmbChOutType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbChOutType.Size = New System.Drawing.Size(105, 20)
        Me.cmbChOutType.TabIndex = 3
        '
        'cmdNext
        '
        Me.cmdNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdNext.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNext.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNext.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNext.Location = New System.Drawing.Point(914, 9)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNext.Size = New System.Drawing.Size(72, 20)
        Me.cmdNext.TabIndex = 9
        Me.cmdNext.Text = "Next"
        Me.cmdNext.UseVisualStyleBackColor = True
        '
        'cmdBefore
        '
        Me.cmdBefore.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdBefore.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBefore.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBefore.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBefore.Location = New System.Drawing.Point(836, 9)
        Me.cmdBefore.Name = "cmdBefore"
        Me.cmdBefore.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBefore.Size = New System.Drawing.Size(72, 20)
        Me.cmdBefore.TabIndex = 8
        Me.cmdBefore.Text = "Before"
        Me.cmdBefore.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.Location = New System.Drawing.Point(612, 432)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(109, 32)
        Me.btnPreview.TabIndex = 115
        Me.btnPreview.Text = "Print"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'cmdClear
        '
        Me.cmdClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClear.Location = New System.Drawing.Point(490, 432)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClear.Size = New System.Drawing.Size(104, 32)
        Me.cmdClear.TabIndex = 116
        Me.cmdClear.Text = "And Or Clear"
        Me.cmdClear.UseVisualStyleBackColor = True
        Me.cmdClear.Visible = False
        '
        'lblFUNo
        '
        Me.lblFUNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblFUNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFUNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFUNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFUNo.Location = New System.Drawing.Point(458, 9)
        Me.lblFUNo.Name = "lblFUNo"
        Me.lblFUNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFUNo.Size = New System.Drawing.Size(84, 16)
        Me.lblFUNo.TabIndex = 118
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(429, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 117
        Me.Label5.Text = "FUNo"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbDoTerm_b
        '
        Me.cmbDoTerm_b.FormattingEnabled = True
        Me.cmbDoTerm_b.Location = New System.Drawing.Point(652, 7)
        Me.cmbDoTerm_b.Name = "cmbDoTerm_b"
        Me.cmbDoTerm_b.Size = New System.Drawing.Size(69, 20)
        Me.cmbDoTerm_b.TabIndex = 120
        '
        'cmbDoTerm_c
        '
        Me.cmbDoTerm_c.FormattingEnabled = True
        Me.cmbDoTerm_c.Location = New System.Drawing.Point(741, 7)
        Me.cmbDoTerm_c.Name = "cmbDoTerm_c"
        Me.cmbDoTerm_c.Size = New System.Drawing.Size(69, 20)
        Me.cmbDoTerm_c.TabIndex = 121
        '
        'cmbDoTerm_d
        '
        Me.cmbDoTerm_d.FormattingEnabled = True
        Me.cmbDoTerm_d.Location = New System.Drawing.Point(831, 8)
        Me.cmbDoTerm_d.Name = "cmbDoTerm_d"
        Me.cmbDoTerm_d.Size = New System.Drawing.Size(69, 20)
        Me.cmbDoTerm_d.TabIndex = 122
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(548, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(11, 12)
        Me.Label4.TabIndex = 123
        Me.Label4.Text = "a"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(637, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(11, 12)
        Me.Label6.TabIndex = 124
        Me.Label6.Text = "b"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(727, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(11, 12)
        Me.Label7.TabIndex = 125
        Me.Label7.Text = "c"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(815, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(11, 12)
        Me.Label9.TabIndex = 126
        Me.Label9.Text = "d"
        '
        'cmbDoTerm_a
        '
        Me.cmbDoTerm_a.FormattingEnabled = True
        Me.cmbDoTerm_a.Location = New System.Drawing.Point(562, 8)
        Me.cmbDoTerm_a.Name = "cmbDoTerm_a"
        Me.cmbDoTerm_a.Size = New System.Drawing.Size(69, 20)
        Me.cmbDoTerm_a.TabIndex = 127
        '
        'grdTerminal
        '
        Me.grdTerminal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTerminal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTerminal.Location = New System.Drawing.Point(12, 35)
        Me.grdTerminal.Name = "grdTerminal"
        Me.grdTerminal.RowTemplate.Height = 21
        Me.grdTerminal.Size = New System.Drawing.Size(973, 388)
        Me.grdTerminal.TabIndex = 3
        Me.grdTerminal.TabStop = False
        '
        'frmChTerminalDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(999, 470)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmbDoTerm_a)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbDoTerm_d)
        Me.Controls.Add(Me.cmbDoTerm_c)
        Me.Controls.Add(Me.cmbDoTerm_b)
        Me.Controls.Add(Me.lblFUNo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.cmdBefore)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmdOutput)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdRtbSet)
        Me.Controls.Add(Me.grdTerminal)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.lblType2)
        Me.Controls.Add(Me.lblType1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblFCU)
        Me.Controls.Add(Me.LblFieldSt)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChTerminalDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "TERMINAL INPUT"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdTerminal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdRtbSet As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDataType As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents cmdOutput As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents cmbOutputMovement As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents cmbChOutType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFunction As System.Windows.Forms.ComboBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents cmdNext As System.Windows.Forms.Button
    Public WithEvents cmdBefore As System.Windows.Forms.Button
    Friend WithEvents grdTerminal As Editor.clsDataGridViewPlus
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Public WithEvents cmdClear As System.Windows.Forms.Button
    Public WithEvents lblFUNo As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbDoTerm_b As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDoTerm_c As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDoTerm_d As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbDoTerm_a As System.Windows.Forms.ComboBox
#End Region

End Class
