<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExtMenu_GAI
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
    Public WithEvents cmdMenu14 As System.Windows.Forms.Button
    Public WithEvents cmdMenu13 As System.Windows.Forms.Button
    Public WithEvents cmdMenu12 As System.Windows.Forms.Button
    Public WithEvents Frame12 As System.Windows.Forms.GroupBox
    Public WithEvents cmdMenu1 As System.Windows.Forms.Button
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdMenu31 = New System.Windows.Forms.Button()
        Me.Frame12 = New System.Windows.Forms.GroupBox()
        Me.cmdMenu1 = New System.Windows.Forms.Button()
        Me.cmdMenu14 = New System.Windows.Forms.Button()
        Me.cmdMenu13 = New System.Windows.Forms.Button()
        Me.cmdMenu12 = New System.Windows.Forms.Button()
        Me.Frame12.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(229, 264)
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
        Me.cmdMenu31.Location = New System.Drawing.Point(196, 26)
        Me.cmdMenu31.Name = "cmdMenu31"
        Me.cmdMenu31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu31.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu31.TabIndex = 4
        Me.cmdMenu31.Text = "Call Timer Set"
        Me.cmdMenu31.UseVisualStyleBackColor = True
        '
        'Frame12
        '
        Me.Frame12.BackColor = System.Drawing.SystemColors.Control
        Me.Frame12.Controls.Add(Me.cmdMenu31)
        Me.Frame12.Controls.Add(Me.cmdMenu1)
        Me.Frame12.Controls.Add(Me.cmdMenu14)
        Me.Frame12.Controls.Add(Me.cmdMenu13)
        Me.Frame12.Controls.Add(Me.cmdMenu12)
        Me.Frame12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame12.Location = New System.Drawing.Point(12, 12)
        Me.Frame12.Name = "Frame12"
        Me.Frame12.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame12.Size = New System.Drawing.Size(337, 235)
        Me.Frame12.TabIndex = 1
        Me.Frame12.TabStop = False
        Me.Frame12.Text = "Ext Alarm Panel"
        '
        'cmdMenu1
        '
        Me.cmdMenu1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu1.Location = New System.Drawing.Point(21, 26)
        Me.cmdMenu1.Name = "cmdMenu1"
        Me.cmdMenu1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu1.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu1.TabIndex = 0
        Me.cmdMenu1.Text = "Ext Alarm Set"
        Me.cmdMenu1.UseVisualStyleBackColor = True
        '
        'cmdMenu14
        '
        Me.cmdMenu14.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu14.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu14.Location = New System.Drawing.Point(21, 178)
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
        Me.cmdMenu13.Location = New System.Drawing.Point(21, 132)
        Me.cmdMenu13.Name = "cmdMenu13"
        Me.cmdMenu13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu13.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu13.TabIndex = 3
        Me.cmdMenu13.Text = "Buzzer Delay" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Timer Set"
        Me.cmdMenu13.UseVisualStyleBackColor = True
        '
        'cmdMenu12
        '
        Me.cmdMenu12.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMenu12.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMenu12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu12.Location = New System.Drawing.Point(21, 86)
        Me.cmdMenu12.Name = "cmdMenu12"
        Me.cmdMenu12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMenu12.Size = New System.Drawing.Size(120, 40)
        Me.cmdMenu12.TabIndex = 2
        Me.cmdMenu12.Text = "EXT Panel Set"
        Me.cmdMenu12.UseVisualStyleBackColor = True
        '
        'frmExtMenu_GAI
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(374, 338)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.Frame12)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExtMenu_GAI"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "EXT ALARM EDITOR"
        Me.Frame12.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region

End Class
