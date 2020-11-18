<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExtMenu
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
    Public WithEvents cmdMenu31 As System.Windows.Forms.Button
    Public WithEvents cmdMenu15 As System.Windows.Forms.Button
    Public WithEvents cmdMenu14 As System.Windows.Forms.Button
    Public WithEvents cmdMenu13 As System.Windows.Forms.Button
    Public WithEvents cmdMenu11 As System.Windows.Forms.Button
    Public WithEvents cmdMenu12 As System.Windows.Forms.Button
    Public WithEvents cmdMenu10 As System.Windows.Forms.Button
    Public WithEvents Frame12 As System.Windows.Forms.GroupBox
    Public WithEvents cmdMenu4 As System.Windows.Forms.Button
    Public WithEvents cmdMenu1 As System.Windows.Forms.Button
    Public WithEvents cmdMenu3 As System.Windows.Forms.Button
    Public WithEvents cmdMenu2 As System.Windows.Forms.Button
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdMenu31 = New System.Windows.Forms.Button()
        Me.Frame12 = New System.Windows.Forms.GroupBox()
        Me.cmdMenu15 = New System.Windows.Forms.Button()
        Me.cmdMenu14 = New System.Windows.Forms.Button()
        Me.cmdMenu13 = New System.Windows.Forms.Button()
        Me.cmdMenu11 = New System.Windows.Forms.Button()
        Me.cmdMenu12 = New System.Windows.Forms.Button()
        Me.cmdMenu10 = New System.Windows.Forms.Button()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me.cmdMenu4 = New System.Windows.Forms.Button()
        Me.cmdMenu1 = New System.Windows.Forms.Button()
        Me.cmdMenu3 = New System.Windows.Forms.Button()
        Me.cmdMenu2 = New System.Windows.Forms.Button()
        Me.grpCombine = New System.Windows.Forms.GroupBox()
        Me.optCombine = New System.Windows.Forms.RadioButton()
        Me.optStandard = New System.Windows.Forms.RadioButton()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Frame12.SuspendLayout()
        Me.Frame10.SuspendLayout()
        Me.grpCombine.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(434, 346)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(120, 40)
        Me.cmdExit.TabIndex = 8
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdMenu31
        '
        Me.cmdMenu31.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu31.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu31.Location = New System.Drawing.Point(21, 24)
        Me.cmdMenu31.Name = "cmdMenu31"
        Me.cmdMenu31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu31.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu31.TabIndex = 4
        Me.cmdMenu31.Text = "Timer Set"
        Me.cmdMenu31.UseVisualStyleBackColor = True
        '
        'Frame12
        '
        Me.Frame12.BackColor = System.Drawing.SystemColors.Control
        Me.Frame12.Controls.Add(Me.cmdMenu15)
        Me.Frame12.Controls.Add(Me.cmdMenu14)
        Me.Frame12.Controls.Add(Me.cmdMenu13)
        Me.Frame12.Controls.Add(Me.cmdMenu11)
        Me.Frame12.Controls.Add(Me.cmdMenu12)
        Me.Frame12.Controls.Add(Me.cmdMenu10)
        Me.Frame12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame12.Location = New System.Drawing.Point(204, 12)
        Me.Frame12.Name = "Frame12"
        Me.Frame12.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame12.Size = New System.Drawing.Size(160, 320)
        Me.Frame12.TabIndex = 1
        Me.Frame12.TabStop = False
        Me.Frame12.Text = "Ext Alarm Panel"
        '
        'cmdMenu15
        '
        Me.cmdMenu15.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu15.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu15.Location = New System.Drawing.Point(21, 264)
        Me.cmdMenu15.Name = "cmdMenu15"
        Me.cmdMenu15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu15.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu15.TabIndex = 5
        Me.cmdMenu15.Text = "LCD Duty" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Display Set"
        Me.cmdMenu15.UseVisualStyleBackColor = True
        '
        'cmdMenu14
        '
        Me.cmdMenu14.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu14.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu14.Location = New System.Drawing.Point(21, 216)
        Me.cmdMenu14.Name = "cmdMenu14"
        Me.cmdMenu14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu14.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu14.TabIndex = 4
        Me.cmdMenu14.Text = "LCD EXT Group Display Set"
        Me.cmdMenu14.UseVisualStyleBackColor = True
        '
        'cmdMenu13
        '
        Me.cmdMenu13.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu13.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu13.Location = New System.Drawing.Point(21, 168)
        Me.cmdMenu13.Name = "cmdMenu13"
        Me.cmdMenu13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu13.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu13.TabIndex = 3
        Me.cmdMenu13.Text = "Buzzer Delay" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Timer Set"
        Me.cmdMenu13.UseVisualStyleBackColor = True
        '
        'cmdMenu11
        '
        Me.cmdMenu11.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu11.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu11.Location = New System.Drawing.Point(21, 72)
        Me.cmdMenu11.Name = "cmdMenu11"
        Me.cmdMenu11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu11.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu11.TabIndex = 1
        Me.cmdMenu11.Text = "D/L EXT Alarm Group Output Set"
        Me.cmdMenu11.UseVisualStyleBackColor = True
        '
        'cmdMenu12
        '
        Me.cmdMenu12.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu12.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu12.Location = New System.Drawing.Point(21, 120)
        Me.cmdMenu12.Name = "cmdMenu12"
        Me.cmdMenu12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu12.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu12.TabIndex = 2
        Me.cmdMenu12.Text = "EXT Panel Set"
        Me.cmdMenu12.UseVisualStyleBackColor = True
        '
        'cmdMenu10
        '
        Me.cmdMenu10.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu10.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu10.Location = New System.Drawing.Point(21, 24)
        Me.cmdMenu10.Name = "cmdMenu10"
        Me.cmdMenu10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu10.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu10.TabIndex = 0
        Me.cmdMenu10.Text = "System Set"
        Me.cmdMenu10.UseVisualStyleBackColor = True
        '
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me.cmdMenu4)
        Me.Frame10.Controls.Add(Me.cmdMenu1)
        Me.Frame10.Controls.Add(Me.cmdMenu3)
        Me.Frame10.Controls.Add(Me.cmdMenu2)
        Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame10.Location = New System.Drawing.Point(17, 12)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(160, 224)
        Me.Frame10.TabIndex = 0
        Me.Frame10.TabStop = False
        Me.Frame10.Text = "ECC Circuit Set"
        '
        'cmdMenu4
        '
        Me.cmdMenu4.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu4.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu4.Location = New System.Drawing.Point(18, 168)
        Me.cmdMenu4.Name = "cmdMenu4"
        Me.cmdMenu4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu4.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu4.TabIndex = 3
        Me.cmdMenu4.Text = "Dead Man Alarm Set"
        Me.cmdMenu4.UseVisualStyleBackColor = True
        '
        'cmdMenu1
        '
        Me.cmdMenu1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu1.Location = New System.Drawing.Point(18, 24)
        Me.cmdMenu1.Name = "cmdMenu1"
        Me.cmdMenu1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu1.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu1.TabIndex = 0
        Me.cmdMenu1.Text = "Duty Set"
        Me.cmdMenu1.UseVisualStyleBackColor = True
        '
        'cmdMenu3
        '
        Me.cmdMenu3.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu3.Location = New System.Drawing.Point(18, 120)
        Me.cmdMenu3.Name = "cmdMenu3"
        Me.cmdMenu3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu3.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu3.TabIndex = 2
        Me.cmdMenu3.Text = "Patrol Man Call Set"
        Me.cmdMenu3.UseVisualStyleBackColor = True
        '
        'cmdMenu2
        '
        Me.cmdMenu2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu2.Location = New System.Drawing.Point(18, 72)
        Me.cmdMenu2.Name = "cmdMenu2"
        Me.cmdMenu2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu2.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu2.TabIndex = 1
        Me.cmdMenu2.Text = "Engineer Call Set"
        Me.cmdMenu2.UseVisualStyleBackColor = True
        '
        'grpCombine
        '
        Me.grpCombine.BackColor = System.Drawing.SystemColors.Control
        Me.grpCombine.Controls.Add(Me.optCombine)
        Me.grpCombine.Controls.Add(Me.optStandard)
        Me.grpCombine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpCombine.Location = New System.Drawing.Point(394, 249)
        Me.grpCombine.Name = "grpCombine"
        Me.grpCombine.Padding = New System.Windows.Forms.Padding(0)
        Me.grpCombine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grpCombine.Size = New System.Drawing.Size(160, 83)
        Me.grpCombine.TabIndex = 5
        Me.grpCombine.TabStop = False
        Me.grpCombine.Text = "Combine"
        '
        'optCombine
        '
        Me.optCombine.AutoSize = True
        Me.optCombine.Location = New System.Drawing.Point(43, 49)
        Me.optCombine.Name = "optCombine"
        Me.optCombine.Size = New System.Drawing.Size(65, 16)
        Me.optCombine.TabIndex = 8
        Me.optCombine.TabStop = True
        Me.optCombine.Text = "Combine"
        Me.optCombine.UseVisualStyleBackColor = True
        '
        'optStandard
        '
        Me.optStandard.AutoSize = True
        Me.optStandard.Location = New System.Drawing.Point(43, 24)
        Me.optStandard.Name = "optStandard"
        Me.optStandard.Size = New System.Drawing.Size(71, 16)
        Me.optStandard.TabIndex = 7
        Me.optStandard.TabStop = True
        Me.optStandard.Text = "Standard"
        Me.optStandard.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(308, 346)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(120, 40)
        Me.cmdSave.TabIndex = 7
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(35, 346)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(120, 40)
        Me.cmdPrint.TabIndex = 6
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.cmdMenu31)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(394, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(160, 83)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Timer"
        '
        'frmExtMenu
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(571, 398)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.grpCombine)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.Frame12)
        Me.Controls.Add(Me.Frame10)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExtMenu"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "EXT ALARM EDITOR"
        Me.Frame12.ResumeLayout(False)
        Me.Frame10.ResumeLayout(False)
        Me.grpCombine.ResumeLayout(False)
        Me.grpCombine.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents grpCombine As System.Windows.Forms.GroupBox
    Friend WithEvents optCombine As System.Windows.Forms.RadioButton
    Friend WithEvents optStandard As System.Windows.Forms.RadioButton
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
#End Region

End Class
