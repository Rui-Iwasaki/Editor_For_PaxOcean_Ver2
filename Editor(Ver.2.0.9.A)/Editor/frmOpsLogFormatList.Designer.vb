<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsLogFormatList
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

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.optCargo = New System.Windows.Forms.RadioButton()
        Me.optMachinery = New System.Windows.Forms.RadioButton()
        Me.cmdInsert = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
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
        Me.grpVisibleFalse = New System.Windows.Forms.GroupBox()
        Me.cmbSave = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.chkScrollSync = New System.Windows.Forms.CheckBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cmdCounter = New System.Windows.Forms.Button()
        Me.optModeGroup = New System.Windows.Forms.RadioButton()
        Me.optModeInd = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmdOption = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.grdLogCol2 = New Editor.clsDataGridViewPlus()
        Me.grdLogCol1 = New Editor.clsDataGridViewPlus()
        Me.btnALL = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.grpVisibleFalse.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdLogCol2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLogCol1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(971, 518)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 4
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(852, 518)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 3
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'optCargo
        '
        Me.optCargo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.optCargo.Appearance = System.Windows.Forms.Appearance.Button
        Me.optCargo.BackColor = System.Drawing.SystemColors.Control
        Me.optCargo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCargo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCargo.Location = New System.Drawing.Point(204, 12)
        Me.optCargo.Name = "optCargo"
        Me.optCargo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCargo.Size = New System.Drawing.Size(113, 33)
        Me.optCargo.TabIndex = 152
        Me.optCargo.TabStop = True
        Me.optCargo.Text = "Cargo"
        Me.optCargo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optCargo.UseVisualStyleBackColor = True
        '
        'optMachinery
        '
        Me.optMachinery.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.optMachinery.Appearance = System.Windows.Forms.Appearance.Button
        Me.optMachinery.BackColor = System.Drawing.SystemColors.Control
        Me.optMachinery.Cursor = System.Windows.Forms.Cursors.Default
        Me.optMachinery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMachinery.Location = New System.Drawing.Point(55, 12)
        Me.optMachinery.Name = "optMachinery"
        Me.optMachinery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optMachinery.Size = New System.Drawing.Size(113, 33)
        Me.optMachinery.TabIndex = 151
        Me.optMachinery.TabStop = True
        Me.optMachinery.Text = "Machinery"
        Me.optMachinery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optMachinery.UseVisualStyleBackColor = True
        '
        'cmdInsert
        '
        Me.cmdInsert.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdInsert.BackColor = System.Drawing.SystemColors.Control
        Me.cmdInsert.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdInsert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdInsert.Location = New System.Drawing.Point(16, 519)
        Me.cmdInsert.Name = "cmdInsert"
        Me.cmdInsert.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdInsert.Size = New System.Drawing.Size(113, 33)
        Me.cmdInsert.TabIndex = 153
        Me.cmdInsert.Text = "Insert"
        Me.cmdInsert.UseVisualStyleBackColor = True
        '
        'cmdDelete
        '
        Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Location = New System.Drawing.Point(135, 519)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(113, 33)
        Me.cmdDelete.TabIndex = 154
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(19, 25)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(29, 12)
        Me.Label12.TabIndex = 156
        Me.Label12.Text = "M.V."
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(618, 102)
        Me.GroupBox1.TabIndex = 157
        Me.GroupBox1.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(113, 73)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(95, 12)
        Me.Label10.TabIndex = 166
        Me.Label10.Text = "_______________"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(113, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(95, 12)
        Me.Label9.TabIndex = 165
        Me.Label9.Text = "_______________"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(113, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(95, 12)
        Me.Label8.TabIndex = 164
        Me.Label8.Text = "_______________"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(447, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(149, 12)
        Me.Label7.TabIndex = 163
        Me.Label7.Text = "DATE 1/ 1/  2010  PAGE 1"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(446, 49)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(149, 12)
        Me.Label6.TabIndex = 162
        Me.Label6.Text = "SHEET NO. ______________"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(489, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(107, 12)
        Me.Label5.TabIndex = 161
        Me.Label5.Text = "TO ______________"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(368, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(107, 12)
        Me.Label4.TabIndex = 160
        Me.Label4.Text = "FROM ____________"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(210, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(149, 12)
        Me.Label3.TabIndex = 159
        Me.Label3.Text = "LYING AT _______________"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(19, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(89, 12)
        Me.Label2.TabIndex = 158
        Me.Label2.Text = "CHIEF ENGINEER"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(19, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 157
        Me.Label1.Text = "VOYAGE NO."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grpVisibleFalse
        '
        Me.grpVisibleFalse.Controls.Add(Me.cmbSave)
        Me.grpVisibleFalse.Controls.Add(Me.Label11)
        Me.grpVisibleFalse.Controls.Add(Me.cmbUnit)
        Me.grpVisibleFalse.Controls.Add(Me.Label38)
        Me.grpVisibleFalse.Location = New System.Drawing.Point(135, 345)
        Me.grpVisibleFalse.Name = "grpVisibleFalse"
        Me.grpVisibleFalse.Size = New System.Drawing.Size(268, 84)
        Me.grpVisibleFalse.TabIndex = 158
        Me.grpVisibleFalse.TabStop = False
        Me.grpVisibleFalse.Text = "VisibleFalse"
        '
        'cmbSave
        '
        Me.cmbSave.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbSave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSave.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSave.Location = New System.Drawing.Point(160, 50)
        Me.cmbSave.Name = "cmbSave"
        Me.cmbSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbSave.Size = New System.Drawing.Size(72, 20)
        Me.cmbSave.TabIndex = 121
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(34, 53)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(107, 12)
        Me.Label11.TabIndex = 120
        Me.Label11.Text = "SaveFormatStrings"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbUnit
        '
        Me.cmbUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbUnit.Location = New System.Drawing.Point(160, 24)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbUnit.Size = New System.Drawing.Size(72, 20)
        Me.cmbUnit.TabIndex = 119
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(34, 27)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(29, 12)
        Me.Label38.TabIndex = 118
        Me.Label38.Text = "Unit"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkScrollSync
        '
        Me.chkScrollSync.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkScrollSync.AutoSize = True
        Me.chkScrollSync.Location = New System.Drawing.Point(994, 484)
        Me.chkScrollSync.Name = "chkScrollSync"
        Me.chkScrollSync.Size = New System.Drawing.Size(90, 16)
        Me.chkScrollSync.TabIndex = 159
        Me.chkScrollSync.Text = "Scroll Sync"
        Me.chkScrollSync.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'cmdCounter
        '
        Me.cmdCounter.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cmdCounter.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCounter.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCounter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCounter.Location = New System.Drawing.Point(227, 12)
        Me.cmdCounter.Name = "cmdCounter"
        Me.cmdCounter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCounter.Size = New System.Drawing.Size(90, 33)
        Me.cmdCounter.TabIndex = 164
        Me.cmdCounter.Text = "Counter Set"
        Me.cmdCounter.UseVisualStyleBackColor = True
        '
        'optModeGroup
        '
        Me.optModeGroup.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.optModeGroup.Appearance = System.Windows.Forms.Appearance.Button
        Me.optModeGroup.BackColor = System.Drawing.SystemColors.Control
        Me.optModeGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.optModeGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optModeGroup.Location = New System.Drawing.Point(141, 12)
        Me.optModeGroup.Name = "optModeGroup"
        Me.optModeGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optModeGroup.Size = New System.Drawing.Size(80, 33)
        Me.optModeGroup.TabIndex = 165
        Me.optModeGroup.TabStop = True
        Me.optModeGroup.Text = "Group"
        Me.optModeGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optModeGroup.UseVisualStyleBackColor = True
        '
        'optModeInd
        '
        Me.optModeInd.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.optModeInd.Appearance = System.Windows.Forms.Appearance.Button
        Me.optModeInd.BackColor = System.Drawing.SystemColors.Control
        Me.optModeInd.Cursor = System.Windows.Forms.Cursors.Default
        Me.optModeInd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optModeInd.Location = New System.Drawing.Point(55, 12)
        Me.optModeInd.Name = "optModeInd"
        Me.optModeInd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optModeInd.Size = New System.Drawing.Size(80, 33)
        Me.optModeInd.TabIndex = 164
        Me.optModeInd.TabStop = True
        Me.optModeInd.Text = "Individual"
        Me.optModeInd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optModeInd.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.optModeInd)
        Me.GroupBox2.Controls.Add(Me.cmdCounter)
        Me.GroupBox2.Controls.Add(Me.optModeGroup)
        Me.GroupBox2.Location = New System.Drawing.Point(644, 62)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(340, 52)
        Me.GroupBox2.TabIndex = 166
        Me.GroupBox2.TabStop = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(13, 22)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(29, 12)
        Me.Label13.TabIndex = 167
        Me.Label13.Text = "MODE"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.optMachinery)
        Me.GroupBox3.Controls.Add(Me.optCargo)
        Me.GroupBox3.Location = New System.Drawing.Point(644, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(340, 52)
        Me.GroupBox3.TabIndex = 167
        Me.GroupBox3.TabStop = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(13, 22)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(29, 12)
        Me.Label14.TabIndex = 167
        Me.Label14.Text = "PART"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdOption
        '
        Me.cmdOption.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdOption.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOption.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOption.Location = New System.Drawing.Point(384, 518)
        Me.cmdOption.Name = "cmdOption"
        Me.cmdOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOption.Size = New System.Drawing.Size(113, 33)
        Me.cmdOption.TabIndex = 168
        Me.cmdOption.Text = "Option"
        Me.cmdOption.UseVisualStyleBackColor = True
        '
        'cmdClear
        '
        Me.cmdClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClear.Location = New System.Drawing.Point(731, 519)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClear.Size = New System.Drawing.Size(104, 32)
        Me.cmdClear.TabIndex = 169
        Me.cmdClear.Text = "Log Clear"
        Me.cmdClear.UseVisualStyleBackColor = True
        '
        'grdLogCol2
        '
        Me.grdLogCol2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdLogCol2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdLogCol2.Location = New System.Drawing.Point(553, 120)
        Me.grdLogCol2.Name = "grdLogCol2"
        Me.grdLogCol2.RowTemplate.Height = 21
        Me.grdLogCol2.Size = New System.Drawing.Size(531, 358)
        Me.grdLogCol2.TabIndex = 1
        '
        'grdLogCol1
        '
        Me.grdLogCol1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdLogCol1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdLogCol1.Location = New System.Drawing.Point(16, 120)
        Me.grdLogCol1.Name = "grdLogCol1"
        Me.grdLogCol1.RowTemplate.Height = 21
        Me.grdLogCol1.Size = New System.Drawing.Size(531, 358)
        Me.grdLogCol1.TabIndex = 0
        '
        'btnALL
        '
        Me.btnALL.Location = New System.Drawing.Point(998, 22)
        Me.btnALL.Name = "btnALL"
        Me.btnALL.Size = New System.Drawing.Size(86, 35)
        Me.btnALL.TabIndex = 170
        Me.btnALL.Text = "Auto All Set"
        Me.btnALL.UseVisualStyleBackColor = True
        '
        'frmOpsLogFormatList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(1106, 563)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnALL)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdOption)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.chkScrollSync)
        Me.Controls.Add(Me.grpVisibleFalse)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdInsert)
        Me.Controls.Add(Me.grdLogCol2)
        Me.Controls.Add(Me.grdLogCol1)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsLogFormatList"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "LOG FORMAT SET"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpVisibleFalse.ResumeLayout(False)
        Me.grpVisibleFalse.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.grdLogCol2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLogCol1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdLogCol1 As Editor.clsDataGridViewPlus
    Friend WithEvents grdLogCol2 As Editor.clsDataGridViewPlus
    Public WithEvents optCargo As System.Windows.Forms.RadioButton
    Public WithEvents optMachinery As System.Windows.Forms.RadioButton
    Public WithEvents cmdInsert As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents grpVisibleFalse As System.Windows.Forms.GroupBox
    Public WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Public WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents chkScrollSync As System.Windows.Forms.CheckBox
    Public WithEvents cmbSave As System.Windows.Forms.ComboBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents optModeInd As System.Windows.Forms.RadioButton
    Public WithEvents optModeGroup As System.Windows.Forms.RadioButton
    Public WithEvents cmdCounter As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents cmdOption As System.Windows.Forms.Button
    Public WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents btnALL As System.Windows.Forms.Button
#End Region

End Class
