<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChListSelectType
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

    Public WithEvents cmdPulseRevolution As System.Windows.Forms.Button
    Public WithEvents cmdDigitalComposite As System.Windows.Forms.Button
    Public WithEvents cmdValve As System.Windows.Forms.Button
    Public WithEvents cmdMotor As System.Windows.Forms.Button
    Public WithEvents cmdAnalog As System.Windows.Forms.Button
    Public WithEvents cmdDigital As System.Windows.Forms.Button
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdPulseRevolution = New System.Windows.Forms.Button()
        Me.cmdDigitalComposite = New System.Windows.Forms.Button()
        Me.cmdValve = New System.Windows.Forms.Button()
        Me.cmdMotor = New System.Windows.Forms.Button()
        Me.cmdAnalog = New System.Windows.Forms.Button()
        Me.cmdDigital = New System.Windows.Forms.Button()
        Me.cmdPID = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdPulseRevolution
        '
        Me.cmdPulseRevolution.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPulseRevolution.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPulseRevolution.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPulseRevolution.Location = New System.Drawing.Point(14, 207)
        Me.cmdPulseRevolution.Name = "cmdPulseRevolution"
        Me.cmdPulseRevolution.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPulseRevolution.Size = New System.Drawing.Size(118, 33)
        Me.cmdPulseRevolution.TabIndex = 5
        Me.cmdPulseRevolution.Text = "Pulse Revolution"
        Me.cmdPulseRevolution.UseVisualStyleBackColor = True
        '
        'cmdDigitalComposite
        '
        Me.cmdDigitalComposite.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDigitalComposite.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDigitalComposite.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDigitalComposite.Location = New System.Drawing.Point(14, 168)
        Me.cmdDigitalComposite.Name = "cmdDigitalComposite"
        Me.cmdDigitalComposite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDigitalComposite.Size = New System.Drawing.Size(118, 33)
        Me.cmdDigitalComposite.TabIndex = 4
        Me.cmdDigitalComposite.Text = "Digital Composite"
        Me.cmdDigitalComposite.UseVisualStyleBackColor = True
        '
        'cmdValve
        '
        Me.cmdValve.BackColor = System.Drawing.SystemColors.Control
        Me.cmdValve.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdValve.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdValve.Location = New System.Drawing.Point(14, 129)
        Me.cmdValve.Name = "cmdValve"
        Me.cmdValve.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdValve.Size = New System.Drawing.Size(118, 33)
        Me.cmdValve.TabIndex = 3
        Me.cmdValve.Text = "Valve"
        Me.cmdValve.UseVisualStyleBackColor = True
        '
        'cmdMotor
        '
        Me.cmdMotor.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMotor.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMotor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMotor.Location = New System.Drawing.Point(14, 90)
        Me.cmdMotor.Name = "cmdMotor"
        Me.cmdMotor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMotor.Size = New System.Drawing.Size(118, 33)
        Me.cmdMotor.TabIndex = 2
        Me.cmdMotor.Text = "Motor"
        Me.cmdMotor.UseVisualStyleBackColor = True
        '
        'cmdAnalog
        '
        Me.cmdAnalog.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAnalog.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAnalog.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAnalog.Location = New System.Drawing.Point(14, 12)
        Me.cmdAnalog.Name = "cmdAnalog"
        Me.cmdAnalog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAnalog.Size = New System.Drawing.Size(118, 33)
        Me.cmdAnalog.TabIndex = 0
        Me.cmdAnalog.Text = "Analog"
        Me.cmdAnalog.UseVisualStyleBackColor = True
        '
        'cmdDigital
        '
        Me.cmdDigital.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDigital.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDigital.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDigital.Location = New System.Drawing.Point(14, 51)
        Me.cmdDigital.Name = "cmdDigital"
        Me.cmdDigital.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDigital.Size = New System.Drawing.Size(118, 33)
        Me.cmdDigital.TabIndex = 1
        Me.cmdDigital.Text = "Digital"
        Me.cmdDigital.UseVisualStyleBackColor = True
        '
        'cmdPID
        '
        Me.cmdPID.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPID.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPID.Location = New System.Drawing.Point(12, 246)
        Me.cmdPID.Name = "cmdPID"
        Me.cmdPID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPID.Size = New System.Drawing.Size(118, 33)
        Me.cmdPID.TabIndex = 6
        Me.cmdPID.Text = "PID"
        Me.cmdPID.UseVisualStyleBackColor = True
        '
        'frmChListSelectType
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(144, 304)
        Me.Controls.Add(Me.cmdPID)
        Me.Controls.Add(Me.cmdPulseRevolution)
        Me.Controls.Add(Me.cmdDigitalComposite)
        Me.Controls.Add(Me.cmdValve)
        Me.Controls.Add(Me.cmdMotor)
        Me.Controls.Add(Me.cmdAnalog)
        Me.Controls.Add(Me.cmdDigital)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 30)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChListSelectType"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CH SELECT"
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents cmdPID As System.Windows.Forms.Button
#End Region

End Class
