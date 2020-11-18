<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExtPnlLcdDutyDisplay
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

    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.lblPos1 = New System.Windows.Forms.Label()
        Me.lblPos2 = New System.Windows.Forms.Label()
        Me.lblPos3 = New System.Windows.Forms.Label()
        Me.lblPos4 = New System.Windows.Forms.Label()
        Me.lblPos5 = New System.Windows.Forms.Label()
        Me.lblPos6 = New System.Windows.Forms.Label()
        Me.lblPos7 = New System.Windows.Forms.Label()
        Me.lblPos8 = New System.Windows.Forms.Label()
        Me.grdPanel = New Editor.clsDataGridViewPlus()
        Me.grdDutyName = New Editor.clsDataGridViewPlus()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.grdPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDutyName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(517, 647)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 2
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(636, 648)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(714, 52)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(35, 12)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Right"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(275, 52)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(29, 12)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Left"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(173, 647)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(113, 33)
        Me.cmdPrint.TabIndex = 36
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'lblPos1
        '
        Me.lblPos1.BackColor = System.Drawing.Color.White
        Me.lblPos1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPos1.Location = New System.Drawing.Point(268, 30)
        Me.lblPos1.Name = "lblPos1"
        Me.lblPos1.Size = New System.Drawing.Size(61, 20)
        Me.lblPos1.TabIndex = 37
        Me.lblPos1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPos2
        '
        Me.lblPos2.BackColor = System.Drawing.Color.White
        Me.lblPos2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPos2.Location = New System.Drawing.Point(328, 30)
        Me.lblPos2.Name = "lblPos2"
        Me.lblPos2.Size = New System.Drawing.Size(61, 20)
        Me.lblPos2.TabIndex = 38
        Me.lblPos2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPos3
        '
        Me.lblPos3.BackColor = System.Drawing.Color.White
        Me.lblPos3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPos3.Location = New System.Drawing.Point(388, 30)
        Me.lblPos3.Name = "lblPos3"
        Me.lblPos3.Size = New System.Drawing.Size(61, 20)
        Me.lblPos3.TabIndex = 39
        Me.lblPos3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPos4
        '
        Me.lblPos4.BackColor = System.Drawing.Color.White
        Me.lblPos4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPos4.Location = New System.Drawing.Point(448, 30)
        Me.lblPos4.Name = "lblPos4"
        Me.lblPos4.Size = New System.Drawing.Size(61, 20)
        Me.lblPos4.TabIndex = 40
        Me.lblPos4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPos5
        '
        Me.lblPos5.BackColor = System.Drawing.Color.White
        Me.lblPos5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPos5.Location = New System.Drawing.Point(508, 30)
        Me.lblPos5.Name = "lblPos5"
        Me.lblPos5.Size = New System.Drawing.Size(61, 20)
        Me.lblPos5.TabIndex = 41
        Me.lblPos5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPos6
        '
        Me.lblPos6.BackColor = System.Drawing.Color.White
        Me.lblPos6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPos6.Location = New System.Drawing.Point(568, 30)
        Me.lblPos6.Name = "lblPos6"
        Me.lblPos6.Size = New System.Drawing.Size(61, 20)
        Me.lblPos6.TabIndex = 42
        Me.lblPos6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPos7
        '
        Me.lblPos7.BackColor = System.Drawing.Color.White
        Me.lblPos7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPos7.Location = New System.Drawing.Point(628, 30)
        Me.lblPos7.Name = "lblPos7"
        Me.lblPos7.Size = New System.Drawing.Size(61, 20)
        Me.lblPos7.TabIndex = 43
        Me.lblPos7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPos8
        '
        Me.lblPos8.BackColor = System.Drawing.Color.White
        Me.lblPos8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPos8.Location = New System.Drawing.Point(688, 30)
        Me.lblPos8.Name = "lblPos8"
        Me.lblPos8.Size = New System.Drawing.Size(61, 20)
        Me.lblPos8.TabIndex = 44
        Me.lblPos8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdPanel
        '
        Me.grdPanel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPanel.Location = New System.Drawing.Point(173, 68)
        Me.grdPanel.Name = "grdPanel"
        Me.grdPanel.RowTemplate.Height = 21
        Me.grdPanel.Size = New System.Drawing.Size(578, 440)
        Me.grdPanel.TabIndex = 1
        '
        'grdDutyName
        '
        Me.grdDutyName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDutyName.Location = New System.Drawing.Point(12, 30)
        Me.grdDutyName.Name = "grdDutyName"
        Me.grdDutyName.RowTemplate.Height = 21
        Me.grdDutyName.Size = New System.Drawing.Size(147, 650)
        Me.grdDutyName.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(215, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(47, 12)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "example"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmExtPnlLcdDutyDisplay
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(767, 693)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblPos8)
        Me.Controls.Add(Me.lblPos7)
        Me.Controls.Add(Me.lblPos6)
        Me.Controls.Add(Me.lblPos5)
        Me.Controls.Add(Me.lblPos4)
        Me.Controls.Add(Me.lblPos3)
        Me.Controls.Add(Me.lblPos2)
        Me.Controls.Add(Me.lblPos1)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.grdPanel)
        Me.Controls.Add(Me.grdDutyName)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label13)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExtPnlLcdDutyDisplay"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "LCD DUTY DISPLAY SET"
        CType(Me.grdPanel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDutyName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents grdDutyName As Editor.clsDataGridViewPlus
    Friend WithEvents grdPanel As Editor.clsDataGridViewPlus
    Friend WithEvents lblPos1 As System.Windows.Forms.Label
    Friend WithEvents lblPos2 As System.Windows.Forms.Label
    Friend WithEvents lblPos3 As System.Windows.Forms.Label
    Friend WithEvents lblPos4 As System.Windows.Forms.Label
    Friend WithEvents lblPos5 As System.Windows.Forms.Label
    Friend WithEvents lblPos6 As System.Windows.Forms.Label
    Friend WithEvents lblPos7 As System.Windows.Forms.Label
    Friend WithEvents lblPos8 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

End Class
