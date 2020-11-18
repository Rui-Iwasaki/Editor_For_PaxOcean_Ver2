<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSysFcu
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
    Public WithEvents cmbFCUNo As System.Windows.Forms.ComboBox
    Public WithEvents chkModBus As System.Windows.Forms.CheckBox
    Public WithEvents chkCanBus As System.Windows.Forms.CheckBox
    Public WithEvents Label10 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmbFCUNo = New System.Windows.Forms.ComboBox()
        Me.chkModBus = New System.Windows.Forms.CheckBox()
        Me.chkCanBus = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkSIO = New System.Windows.Forms.CheckBox()
        Me.numCorrectTime = New System.Windows.Forms.NumericUpDown()
        Me.chkShareChUse = New System.Windows.Forms.CheckBox()
        Me.chkEventLogBackup = New System.Windows.Forms.CheckBox()
        Me.CmbFCUCnt = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkFCU = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbPart = New System.Windows.Forms.ComboBox()
        Me.chkPtJPt = New System.Windows.Forms.CheckBox()
        CType(Me.numCorrectTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(158, 194)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 8
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(20, 194)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 7
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmbFCUNo
        '
        Me.cmbFCUNo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbFCUNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbFCUNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFCUNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbFCUNo.Location = New System.Drawing.Point(79, 44)
        Me.cmbFCUNo.Name = "cmbFCUNo"
        Me.cmbFCUNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbFCUNo.Size = New System.Drawing.Size(62, 20)
        Me.cmbFCUNo.TabIndex = 3
        '
        'chkModBus
        '
        Me.chkModBus.AutoSize = True
        Me.chkModBus.BackColor = System.Drawing.SystemColors.Control
        Me.chkModBus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkModBus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkModBus.Location = New System.Drawing.Point(285, 61)
        Me.chkModBus.Name = "chkModBus"
        Me.chkModBus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkModBus.Size = New System.Drawing.Size(114, 16)
        Me.chkModBus.TabIndex = 2
        Me.chkModBus.Text = "ModBus Function"
        Me.chkModBus.UseVisualStyleBackColor = True
        Me.chkModBus.Visible = False
        '
        'chkCanBus
        '
        Me.chkCanBus.AutoSize = True
        Me.chkCanBus.BackColor = System.Drawing.SystemColors.Control
        Me.chkCanBus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCanBus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCanBus.Location = New System.Drawing.Point(285, 37)
        Me.chkCanBus.Name = "chkCanBus"
        Me.chkCanBus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCanBus.Size = New System.Drawing.Size(174, 16)
        Me.chkCanBus.TabIndex = 1
        Me.chkCanBus.Text = "CanBus Function(Included)"
        Me.chkCanBus.UseVisualStyleBackColor = True
        Me.chkCanBus.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(12, 47)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(41, 12)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "FCU No"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(119, 12)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "FCU-FU Collect Time"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(214, 161)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "x100ms"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkSIO
        '
        Me.chkSIO.AutoSize = True
        Me.chkSIO.BackColor = System.Drawing.SystemColors.Control
        Me.chkSIO.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSIO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSIO.Location = New System.Drawing.Point(12, 79)
        Me.chkSIO.Name = "chkSIO"
        Me.chkSIO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSIO.Size = New System.Drawing.Size(120, 16)
        Me.chkSIO.TabIndex = 11
        Me.chkSIO.Text = "FCU Extend Board"
        Me.chkSIO.UseVisualStyleBackColor = True
        '
        'numCorrectTime
        '
        Me.numCorrectTime.Location = New System.Drawing.Point(158, 158)
        Me.numCorrectTime.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.numCorrectTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numCorrectTime.Name = "numCorrectTime"
        Me.numCorrectTime.Size = New System.Drawing.Size(49, 19)
        Me.numCorrectTime.TabIndex = 12
        Me.numCorrectTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numCorrectTime.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'chkShareChUse
        '
        Me.chkShareChUse.AutoSize = True
        Me.chkShareChUse.BackColor = System.Drawing.SystemColors.Control
        Me.chkShareChUse.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShareChUse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShareChUse.Location = New System.Drawing.Point(11, 103)
        Me.chkShareChUse.Name = "chkShareChUse"
        Me.chkShareChUse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShareChUse.Size = New System.Drawing.Size(96, 16)
        Me.chkShareChUse.TabIndex = 13
        Me.chkShareChUse.Text = "Share CH Use"
        Me.chkShareChUse.UseVisualStyleBackColor = True
        '
        'chkEventLogBackup
        '
        Me.chkEventLogBackup.AutoSize = True
        Me.chkEventLogBackup.BackColor = System.Drawing.SystemColors.Control
        Me.chkEventLogBackup.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkEventLogBackup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEventLogBackup.Location = New System.Drawing.Point(224, 103)
        Me.chkEventLogBackup.Name = "chkEventLogBackup"
        Me.chkEventLogBackup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkEventLogBackup.Size = New System.Drawing.Size(246, 16)
        Me.chkEventLogBackup.TabIndex = 0
        Me.chkEventLogBackup.Text = "Event Log Backup(MemoryCard Exchange)"
        Me.chkEventLogBackup.UseVisualStyleBackColor = True
        Me.chkEventLogBackup.Visible = False
        '
        'CmbFCUCnt
        '
        Me.CmbFCUCnt.BackColor = System.Drawing.SystemColors.Window
        Me.CmbFCUCnt.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmbFCUCnt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFCUCnt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CmbFCUCnt.Location = New System.Drawing.Point(79, 18)
        Me.CmbFCUCnt.Name = "CmbFCUCnt"
        Me.CmbFCUCnt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmbFCUCnt.Size = New System.Drawing.Size(62, 20)
        Me.CmbFCUCnt.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(12, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "FCU Count"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkFCU
        '
        Me.chkFCU.AutoSize = True
        Me.chkFCU.BackColor = System.Drawing.SystemColors.Control
        Me.chkFCU.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFCU.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFCU.Location = New System.Drawing.Point(158, 79)
        Me.chkFCU.Name = "chkFCU"
        Me.chkFCU.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFCU.Size = New System.Drawing.Size(108, 16)
        Me.chkFCU.TabIndex = 16
        Me.chkFCU.Text = "FCU Extend FCU"
        Me.chkFCU.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbPart)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(159, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(113, 52)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Part Setting"
        '
        'cmbPart
        '
        Me.cmbPart.FormattingEnabled = True
        Me.cmbPart.Location = New System.Drawing.Point(6, 18)
        Me.cmbPart.Name = "cmbPart"
        Me.cmbPart.Size = New System.Drawing.Size(101, 20)
        Me.cmbPart.TabIndex = 0
        '
        'chkPtJPt
        '
        Me.chkPtJPt.AutoSize = True
        Me.chkPtJPt.BackColor = System.Drawing.SystemColors.Control
        Me.chkPtJPt.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPtJPt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPtJPt.Location = New System.Drawing.Point(11, 125)
        Me.chkPtJPt.Name = "chkPtJPt"
        Me.chkPtJPt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPtJPt.Size = New System.Drawing.Size(126, 16)
        Me.chkPtJPt.TabIndex = 18
        Me.chkPtJPt.Text = "PT or JPT(ON=JPT)"
        Me.chkPtJPt.UseVisualStyleBackColor = True
        '
        'frmSysFcu
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(284, 248)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkPtJPt)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkFCU)
        Me.Controls.Add(Me.CmbFCUCnt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.chkShareChUse)
        Me.Controls.Add(Me.numCorrectTime)
        Me.Controls.Add(Me.chkSIO)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmbFCUNo)
        Me.Controls.Add(Me.chkModBus)
        Me.Controls.Add(Me.chkCanBus)
        Me.Controls.Add(Me.chkEventLogBackup)
        Me.Controls.Add(Me.Label10)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSysFcu"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FCU SETTING"
        CType(Me.numCorrectTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents chkSIO As System.Windows.Forms.CheckBox
    Friend WithEvents numCorrectTime As System.Windows.Forms.NumericUpDown
    Public WithEvents chkShareChUse As System.Windows.Forms.CheckBox
    Public WithEvents chkEventLogBackup As System.Windows.Forms.CheckBox
    Public WithEvents CmbFCUCnt As System.Windows.Forms.ComboBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents chkFCU As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPart As System.Windows.Forms.ComboBox
    Public WithEvents chkPtJPt As System.Windows.Forms.CheckBox
#End Region

End Class
