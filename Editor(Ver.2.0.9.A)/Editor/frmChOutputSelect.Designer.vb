<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChOutputSelect
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

    Public WithEvents cmdAO As System.Windows.Forms.Button
    Public WithEvents cmdDO As System.Windows.Forms.Button
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdAO = New System.Windows.Forms.Button
        Me.cmdDO = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmdAO
        '
        Me.cmdAO.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAO.Location = New System.Drawing.Point(36, 72)
        Me.cmdAO.Name = "cmdAO"
        Me.cmdAO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAO.Size = New System.Drawing.Size(113, 33)
        Me.cmdAO.TabIndex = 1
        Me.cmdAO.Text = "AO Setting"
        Me.cmdAO.UseVisualStyleBackColor = True
        '
        'cmdDO
        '
        Me.cmdDO.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDO.Location = New System.Drawing.Point(36, 20)
        Me.cmdDO.Name = "cmdDO"
        Me.cmdDO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDO.Size = New System.Drawing.Size(113, 33)
        Me.cmdDO.TabIndex = 0
        Me.cmdDO.Text = "DO Setting"
        Me.cmdDO.UseVisualStyleBackColor = True
        '
        'frmChOutputSelect
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(184, 126)
        Me.Controls.Add(Me.cmdAO)
        Me.Controls.Add(Me.cmdDO)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChOutputSelect"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "OUTPUT SETTING"
        Me.ResumeLayout(False)

    End Sub
#End Region

End Class
