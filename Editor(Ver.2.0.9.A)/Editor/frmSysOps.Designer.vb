<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSysOps
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


    Public WithEvents cmbChEdit As System.Windows.Forms.ComboBox
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmbSystemAlarm As System.Windows.Forms.ComboBox
    Public WithEvents cmbChSetup As System.Windows.Forms.ComboBox
    Public WithEvents chkControlFunction As System.Windows.Forms.CheckBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmbChEdit = New System.Windows.Forms.ComboBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmbSystemAlarm = New System.Windows.Forms.ComboBox()
        Me.cmbChSetup = New System.Windows.Forms.ComboBox()
        Me.chkControlFunction = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbDutySetting = New System.Windows.Forms.ComboBox()
        Me.chkControlOnly = New System.Windows.Forms.CheckBox()
        Me.cmbAutoAlarmOrder = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkSetLV = New System.Windows.Forms.CheckBox()
        Me.lblCombinePr = New System.Windows.Forms.Label()
        Me.grdHead = New Editor.clsDataGridViewPlus()
        Me.grdOPS = New Editor.clsDataGridViewPlus()
        CType(Me.grdHead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOPS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbChEdit
        '
        Me.cmbChEdit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbChEdit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbChEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbChEdit.Location = New System.Drawing.Point(627, 54)
        Me.cmbChEdit.Name = "cmbChEdit"
        Me.cmbChEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbChEdit.Size = New System.Drawing.Size(132, 20)
        Me.cmbChEdit.TabIndex = 2
        Me.cmbChEdit.Visible = False
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(573, 461)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 5
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(699, 461)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 6
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmbSystemAlarm
        '
        Me.cmbSystemAlarm.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSystemAlarm.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbSystemAlarm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSystemAlarm.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSystemAlarm.Location = New System.Drawing.Point(186, 49)
        Me.cmbSystemAlarm.Name = "cmbSystemAlarm"
        Me.cmbSystemAlarm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbSystemAlarm.Size = New System.Drawing.Size(132, 20)
        Me.cmbSystemAlarm.TabIndex = 3
        '
        'cmbChSetup
        '
        Me.cmbChSetup.BackColor = System.Drawing.SystemColors.Window
        Me.cmbChSetup.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbChSetup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChSetup.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbChSetup.Location = New System.Drawing.Point(627, 24)
        Me.cmbChSetup.Name = "cmbChSetup"
        Me.cmbChSetup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbChSetup.Size = New System.Drawing.Size(132, 20)
        Me.cmbChSetup.TabIndex = 1
        Me.cmbChSetup.Visible = False
        '
        'chkControlFunction
        '
        Me.chkControlFunction.AutoSize = True
        Me.chkControlFunction.BackColor = System.Drawing.SystemColors.Control
        Me.chkControlFunction.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkControlFunction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkControlFunction.Location = New System.Drawing.Point(492, 64)
        Me.chkControlFunction.Name = "chkControlFunction"
        Me.chkControlFunction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkControlFunction.Size = New System.Drawing.Size(120, 16)
        Me.chkControlFunction.TabIndex = 0
        Me.chkControlFunction.Text = "Control Function"
        Me.chkControlFunction.UseVisualStyleBackColor = True
        Me.chkControlFunction.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(14, 145)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(71, 12)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "OPS setting"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(55, 52)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(77, 12)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "System Alarm"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(55, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Duty Setting"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbDutySetting
        '
        Me.cmbDutySetting.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDutySetting.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDutySetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDutySetting.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDutySetting.Location = New System.Drawing.Point(186, 21)
        Me.cmbDutySetting.Name = "cmbDutySetting"
        Me.cmbDutySetting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDutySetting.Size = New System.Drawing.Size(132, 20)
        Me.cmbDutySetting.TabIndex = 3
        '
        'chkControlOnly
        '
        Me.chkControlOnly.AutoSize = True
        Me.chkControlOnly.BackColor = System.Drawing.SystemColors.Control
        Me.chkControlOnly.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkControlOnly.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkControlOnly.Location = New System.Drawing.Point(362, 102)
        Me.chkControlOnly.Name = "chkControlOnly"
        Me.chkControlOnly.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkControlOnly.Size = New System.Drawing.Size(144, 16)
        Me.chkControlOnly.TabIndex = 18
        Me.chkControlOnly.Text = "Control:Only One OPS"
        Me.chkControlOnly.UseVisualStyleBackColor = True
        '
        'cmbAutoAlarmOrder
        '
        Me.cmbAutoAlarmOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAutoAlarmOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbAutoAlarmOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAutoAlarmOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAutoAlarmOrder.Location = New System.Drawing.Point(186, 78)
        Me.cmbAutoAlarmOrder.Name = "cmbAutoAlarmOrder"
        Me.cmbAutoAlarmOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAutoAlarmOrder.Size = New System.Drawing.Size(132, 20)
        Me.cmbAutoAlarmOrder.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(55, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(101, 12)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Auto Alarm Order"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkSetLV
        '
        Me.chkSetLV.AutoSize = True
        Me.chkSetLV.BackColor = System.Drawing.SystemColors.Control
        Me.chkSetLV.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSetLV.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSetLV.Location = New System.Drawing.Point(554, 102)
        Me.chkSetLV.Name = "chkSetLV"
        Me.chkSetLV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSetLV.Size = New System.Drawing.Size(78, 16)
        Me.chkSetLV.TabIndex = 21
        Me.chkSetLV.Text = "Set Level"
        Me.chkSetLV.UseVisualStyleBackColor = True
        '
        'lblCombinePr
        '
        Me.lblCombinePr.ForeColor = System.Drawing.Color.Blue
        Me.lblCombinePr.Location = New System.Drawing.Point(587, 83)
        Me.lblCombinePr.Name = "lblCombinePr"
        Me.lblCombinePr.Size = New System.Drawing.Size(193, 16)
        Me.lblCombinePr.TabIndex = 23
        '
        'grdHead
        '
        Me.grdHead.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdHead.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdHead.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.grdHead.Location = New System.Drawing.Point(12, 164)
        Me.grdHead.Name = "grdHead"
        Me.grdHead.RowTemplate.Height = 21
        Me.grdHead.Size = New System.Drawing.Size(800, 24)
        Me.grdHead.TabIndex = 17
        Me.grdHead.TabStop = False
        '
        'grdOPS
        '
        Me.grdOPS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdOPS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdOPS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.grdOPS.Location = New System.Drawing.Point(12, 186)
        Me.grdOPS.Name = "grdOPS"
        Me.grdOPS.RowTemplate.Height = 21
        Me.grdOPS.Size = New System.Drawing.Size(800, 259)
        Me.grdOPS.TabIndex = 4
        '
        'frmSysOps
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(824, 510)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblCombinePr)
        Me.Controls.Add(Me.chkSetLV)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbAutoAlarmOrder)
        Me.Controls.Add(Me.chkControlOnly)
        Me.Controls.Add(Me.grdHead)
        Me.Controls.Add(Me.grdOPS)
        Me.Controls.Add(Me.cmbChEdit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmbDutySetting)
        Me.Controls.Add(Me.cmbSystemAlarm)
        Me.Controls.Add(Me.cmbChSetup)
        Me.Controls.Add(Me.chkControlFunction)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSysOps"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "OPS SETTING"
        CType(Me.grdHead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOPS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Column18 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column20 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column21 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column23 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column24 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column25 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column26 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column27 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents grdOPS As clsDataGridViewPlus
    Friend WithEvents grdHead As clsDataGridViewPlus
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cmbDutySetting As System.Windows.Forms.ComboBox
    Public WithEvents chkControlOnly As System.Windows.Forms.CheckBox
    Public WithEvents cmbAutoAlarmOrder As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents chkSetLV As System.Windows.Forms.CheckBox
    Friend WithEvents lblCombinePr As System.Windows.Forms.Label
#End Region

End Class
