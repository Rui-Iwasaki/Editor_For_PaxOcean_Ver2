<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExtPnlList_GAI
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

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.grdEXT = New Editor.clsDataGridViewPlus()
        Me.fraLedAlarmGroupCount = New System.Windows.Forms.Panel()
        Me.optLedAlarmGroupCount2 = New System.Windows.Forms.RadioButton()
        Me.optLedAlarmGroupCount1 = New System.Windows.Forms.RadioButton()
        Me.optLedAlarmGroupCount4 = New System.Windows.Forms.RadioButton()
        Me.optLedAlarmGroupCount3 = New System.Windows.Forms.RadioButton()
        Me.optLedAlarmGroupCount5 = New System.Windows.Forms.RadioButton()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.fraBzPattern = New System.Windows.Forms.Panel()
        Me.optBzPattern2 = New System.Windows.Forms.RadioButton()
        Me.optBzPattern1 = New System.Windows.Forms.RadioButton()
        Me.optBzPattern3 = New System.Windows.Forms.RadioButton()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optAcceptPattern0 = New System.Windows.Forms.RadioButton()
        Me.optAcceptPattern1 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.grdEXT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraLedAlarmGroupCount.SuspendLayout()
        Me.fraBzPattern.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(762, 584)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 3
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(881, 584)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 4
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(12, 584)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(113, 33)
        Me.cmdPrint.TabIndex = 35
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'grdEXT
        '
        Me.grdEXT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdEXT.Location = New System.Drawing.Point(12, 125)
        Me.grdEXT.Name = "grdEXT"
        Me.grdEXT.RowTemplate.Height = 21
        Me.grdEXT.Size = New System.Drawing.Size(982, 439)
        Me.grdEXT.TabIndex = 2
        '
        'fraLedAlarmGroupCount
        '
        Me.fraLedAlarmGroupCount.BackColor = System.Drawing.SystemColors.Control
        Me.fraLedAlarmGroupCount.Controls.Add(Me.optLedAlarmGroupCount2)
        Me.fraLedAlarmGroupCount.Controls.Add(Me.optLedAlarmGroupCount1)
        Me.fraLedAlarmGroupCount.Controls.Add(Me.optLedAlarmGroupCount4)
        Me.fraLedAlarmGroupCount.Controls.Add(Me.optLedAlarmGroupCount3)
        Me.fraLedAlarmGroupCount.Controls.Add(Me.optLedAlarmGroupCount5)
        Me.fraLedAlarmGroupCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraLedAlarmGroupCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraLedAlarmGroupCount.Location = New System.Drawing.Point(153, 7)
        Me.fraLedAlarmGroupCount.Name = "fraLedAlarmGroupCount"
        Me.fraLedAlarmGroupCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraLedAlarmGroupCount.Size = New System.Drawing.Size(397, 33)
        Me.fraLedAlarmGroupCount.TabIndex = 36
        '
        'optLedAlarmGroupCount2
        '
        Me.optLedAlarmGroupCount2.AutoSize = True
        Me.optLedAlarmGroupCount2.BackColor = System.Drawing.SystemColors.Control
        Me.optLedAlarmGroupCount2.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLedAlarmGroupCount2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLedAlarmGroupCount2.Location = New System.Drawing.Point(92, 9)
        Me.optLedAlarmGroupCount2.Name = "optLedAlarmGroupCount2"
        Me.optLedAlarmGroupCount2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLedAlarmGroupCount2.Size = New System.Drawing.Size(29, 16)
        Me.optLedAlarmGroupCount2.TabIndex = 1
        Me.optLedAlarmGroupCount2.Text = "9"
        Me.optLedAlarmGroupCount2.UseVisualStyleBackColor = True
        '
        'optLedAlarmGroupCount1
        '
        Me.optLedAlarmGroupCount1.AutoSize = True
        Me.optLedAlarmGroupCount1.BackColor = System.Drawing.SystemColors.Control
        Me.optLedAlarmGroupCount1.Checked = True
        Me.optLedAlarmGroupCount1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLedAlarmGroupCount1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLedAlarmGroupCount1.Location = New System.Drawing.Point(8, 9)
        Me.optLedAlarmGroupCount1.Name = "optLedAlarmGroupCount1"
        Me.optLedAlarmGroupCount1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLedAlarmGroupCount1.Size = New System.Drawing.Size(29, 16)
        Me.optLedAlarmGroupCount1.TabIndex = 0
        Me.optLedAlarmGroupCount1.TabStop = True
        Me.optLedAlarmGroupCount1.Text = "8"
        Me.optLedAlarmGroupCount1.UseVisualStyleBackColor = True
        '
        'optLedAlarmGroupCount4
        '
        Me.optLedAlarmGroupCount4.AutoSize = True
        Me.optLedAlarmGroupCount4.BackColor = System.Drawing.SystemColors.Control
        Me.optLedAlarmGroupCount4.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLedAlarmGroupCount4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLedAlarmGroupCount4.Location = New System.Drawing.Point(260, 9)
        Me.optLedAlarmGroupCount4.Name = "optLedAlarmGroupCount4"
        Me.optLedAlarmGroupCount4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLedAlarmGroupCount4.Size = New System.Drawing.Size(35, 16)
        Me.optLedAlarmGroupCount4.TabIndex = 3
        Me.optLedAlarmGroupCount4.Text = "11"
        Me.optLedAlarmGroupCount4.UseVisualStyleBackColor = True
        '
        'optLedAlarmGroupCount3
        '
        Me.optLedAlarmGroupCount3.AutoSize = True
        Me.optLedAlarmGroupCount3.BackColor = System.Drawing.SystemColors.Control
        Me.optLedAlarmGroupCount3.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLedAlarmGroupCount3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLedAlarmGroupCount3.Location = New System.Drawing.Point(176, 9)
        Me.optLedAlarmGroupCount3.Name = "optLedAlarmGroupCount3"
        Me.optLedAlarmGroupCount3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLedAlarmGroupCount3.Size = New System.Drawing.Size(35, 16)
        Me.optLedAlarmGroupCount3.TabIndex = 2
        Me.optLedAlarmGroupCount3.Text = "10"
        Me.optLedAlarmGroupCount3.UseVisualStyleBackColor = True
        '
        'optLedAlarmGroupCount5
        '
        Me.optLedAlarmGroupCount5.AutoSize = True
        Me.optLedAlarmGroupCount5.BackColor = System.Drawing.SystemColors.Control
        Me.optLedAlarmGroupCount5.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLedAlarmGroupCount5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLedAlarmGroupCount5.Location = New System.Drawing.Point(344, 9)
        Me.optLedAlarmGroupCount5.Name = "optLedAlarmGroupCount5"
        Me.optLedAlarmGroupCount5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLedAlarmGroupCount5.Size = New System.Drawing.Size(35, 16)
        Me.optLedAlarmGroupCount5.TabIndex = 4
        Me.optLedAlarmGroupCount5.Text = "12"
        Me.optLedAlarmGroupCount5.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(12, 18)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(131, 12)
        Me.Label21.TabIndex = 37
        Me.Label21.Text = "LED Alarm Group Count"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraBzPattern
        '
        Me.fraBzPattern.BackColor = System.Drawing.SystemColors.Control
        Me.fraBzPattern.Controls.Add(Me.optBzPattern2)
        Me.fraBzPattern.Controls.Add(Me.optBzPattern1)
        Me.fraBzPattern.Controls.Add(Me.optBzPattern3)
        Me.fraBzPattern.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraBzPattern.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBzPattern.Location = New System.Drawing.Point(153, 46)
        Me.fraBzPattern.Name = "fraBzPattern"
        Me.fraBzPattern.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBzPattern.Size = New System.Drawing.Size(397, 33)
        Me.fraBzPattern.TabIndex = 38
        '
        'optBzPattern2
        '
        Me.optBzPattern2.AutoSize = True
        Me.optBzPattern2.BackColor = System.Drawing.SystemColors.Control
        Me.optBzPattern2.Cursor = System.Windows.Forms.Cursors.Default
        Me.optBzPattern2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBzPattern2.Location = New System.Drawing.Point(92, 9)
        Me.optBzPattern2.Name = "optBzPattern2"
        Me.optBzPattern2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optBzPattern2.Size = New System.Drawing.Size(71, 16)
        Me.optBzPattern2.TabIndex = 1
        Me.optBzPattern2.Text = "Pattern2"
        Me.optBzPattern2.UseVisualStyleBackColor = True
        '
        'optBzPattern1
        '
        Me.optBzPattern1.AutoSize = True
        Me.optBzPattern1.BackColor = System.Drawing.SystemColors.Control
        Me.optBzPattern1.Checked = True
        Me.optBzPattern1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optBzPattern1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBzPattern1.Location = New System.Drawing.Point(12, 9)
        Me.optBzPattern1.Name = "optBzPattern1"
        Me.optBzPattern1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optBzPattern1.Size = New System.Drawing.Size(71, 16)
        Me.optBzPattern1.TabIndex = 0
        Me.optBzPattern1.TabStop = True
        Me.optBzPattern1.Text = "Pattern1"
        Me.optBzPattern1.UseVisualStyleBackColor = True
        '
        'optBzPattern3
        '
        Me.optBzPattern3.AutoSize = True
        Me.optBzPattern3.BackColor = System.Drawing.SystemColors.Control
        Me.optBzPattern3.Cursor = System.Windows.Forms.Cursors.Default
        Me.optBzPattern3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBzPattern3.Location = New System.Drawing.Point(176, 9)
        Me.optBzPattern3.Name = "optBzPattern3"
        Me.optBzPattern3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optBzPattern3.Size = New System.Drawing.Size(71, 16)
        Me.optBzPattern3.TabIndex = 2
        Me.optBzPattern3.Text = "Pattern3"
        Me.optBzPattern3.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(54, 57)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(89, 12)
        Me.Label20.TabIndex = 39
        Me.Label20.Text = "Buzzer Pattern"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.optAcceptPattern0)
        Me.Panel1.Controls.Add(Me.optAcceptPattern1)
        Me.Panel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Panel1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel1.Location = New System.Drawing.Point(153, 85)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Panel1.Size = New System.Drawing.Size(397, 25)
        Me.Panel1.TabIndex = 41
        '
        'optAcceptPattern0
        '
        Me.optAcceptPattern0.AutoSize = True
        Me.optAcceptPattern0.BackColor = System.Drawing.SystemColors.Control
        Me.optAcceptPattern0.Checked = True
        Me.optAcceptPattern0.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAcceptPattern0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAcceptPattern0.Location = New System.Drawing.Point(18, 4)
        Me.optAcceptPattern0.Name = "optAcceptPattern0"
        Me.optAcceptPattern0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAcceptPattern0.Size = New System.Drawing.Size(35, 16)
        Me.optAcceptPattern0.TabIndex = 0
        Me.optAcceptPattern0.TabStop = True
        Me.optAcceptPattern0.Text = "OR"
        Me.optAcceptPattern0.UseVisualStyleBackColor = True
        '
        'optAcceptPattern1
        '
        Me.optAcceptPattern1.AutoSize = True
        Me.optAcceptPattern1.BackColor = System.Drawing.SystemColors.Control
        Me.optAcceptPattern1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAcceptPattern1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAcceptPattern1.Location = New System.Drawing.Point(176, 4)
        Me.optAcceptPattern1.Name = "optAcceptPattern1"
        Me.optAcceptPattern1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAcceptPattern1.Size = New System.Drawing.Size(41, 16)
        Me.optAcceptPattern1.TabIndex = 1
        Me.optAcceptPattern1.Text = "AND"
        Me.optAcceptPattern1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(48, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Accept Pattern"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmExtPnlList_GAI
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(1010, 651)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.fraBzPattern)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.fraLedAlarmGroupCount)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.grdEXT)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdExit)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExtPnlList_GAI"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "EXT PANEL SET"
        CType(Me.grdEXT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraLedAlarmGroupCount.ResumeLayout(False)
        Me.fraLedAlarmGroupCount.PerformLayout()
        Me.fraBzPattern.ResumeLayout(False)
        Me.fraBzPattern.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents grdEXT As Editor.clsDataGridViewPlus
    Public WithEvents fraLedAlarmGroupCount As System.Windows.Forms.Panel
    Public WithEvents optLedAlarmGroupCount2 As System.Windows.Forms.RadioButton
    Public WithEvents optLedAlarmGroupCount1 As System.Windows.Forms.RadioButton
    Public WithEvents optLedAlarmGroupCount4 As System.Windows.Forms.RadioButton
    Public WithEvents optLedAlarmGroupCount3 As System.Windows.Forms.RadioButton
    Public WithEvents optLedAlarmGroupCount5 As System.Windows.Forms.RadioButton
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents fraBzPattern As System.Windows.Forms.Panel
    Public WithEvents optBzPattern2 As System.Windows.Forms.RadioButton
    Public WithEvents optBzPattern1 As System.Windows.Forms.RadioButton
    Public WithEvents optBzPattern3 As System.Windows.Forms.RadioButton
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents optAcceptPattern0 As System.Windows.Forms.RadioButton
    Public WithEvents optAcceptPattern1 As System.Windows.Forms.RadioButton
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

End Class
