<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChTerminalPreview
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.chkFuName = New System.Windows.Forms.CheckBox()
        Me.chkPagePrint = New System.Windows.Forms.CheckBox()
        Me.chkSecretChannel = New System.Windows.Forms.CheckBox()
        Me.chkDummyData = New System.Windows.Forms.CheckBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'chkFuName
        '
        Me.chkFuName.AutoSize = True
        Me.chkFuName.BackColor = System.Drawing.SystemColors.Control
        Me.chkFuName.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFuName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFuName.Location = New System.Drawing.Point(12, 78)
        Me.chkFuName.Name = "chkFuName"
        Me.chkFuName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFuName.Size = New System.Drawing.Size(178, 16)
        Me.chkFuName.TabIndex = 123
        Me.chkFuName.Text = "FIELD UNIT NAME(Japanese)"
        Me.chkFuName.UseVisualStyleBackColor = True
        Me.chkFuName.Visible = False
        '
        'chkPagePrint
        '
        Me.chkPagePrint.AutoSize = True
        Me.chkPagePrint.BackColor = System.Drawing.SystemColors.Control
        Me.chkPagePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPagePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPagePrint.Location = New System.Drawing.Point(12, 56)
        Me.chkPagePrint.Name = "chkPagePrint"
        Me.chkPagePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPagePrint.Size = New System.Drawing.Size(152, 16)
        Me.chkPagePrint.TabIndex = 122
        Me.chkPagePrint.Text = "Prints including Page No."
        Me.chkPagePrint.UseVisualStyleBackColor = True
        '
        'chkSecretChannel
        '
        Me.chkSecretChannel.AutoSize = True
        Me.chkSecretChannel.BackColor = System.Drawing.SystemColors.Control
        Me.chkSecretChannel.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSecretChannel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSecretChannel.Location = New System.Drawing.Point(12, 12)
        Me.chkSecretChannel.Name = "chkSecretChannel"
        Me.chkSecretChannel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSecretChannel.Size = New System.Drawing.Size(192, 16)
        Me.chkSecretChannel.TabIndex = 120
        Me.chkSecretChannel.Text = "Prints including a secret channel"
        Me.chkSecretChannel.UseVisualStyleBackColor = True
        '
        'chkDummyData
        '
        Me.chkDummyData.AutoSize = True
        Me.chkDummyData.BackColor = System.Drawing.SystemColors.Control
        Me.chkDummyData.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDummyData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDummyData.Location = New System.Drawing.Point(12, 34)
        Me.chkDummyData.Name = "chkDummyData"
        Me.chkDummyData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDummyData.Size = New System.Drawing.Size(179, 16)
        Me.chkDummyData.TabIndex = 121
        Me.chkDummyData.Text = "Prints including a dummy data"
        Me.chkDummyData.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(6, 122)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(109, 32)
        Me.btnPreview.TabIndex = 124
        Me.btnPreview.Text = "Print Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Location = New System.Drawing.Point(121, 122)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(109, 32)
        Me.btnExit.TabIndex = 125
        Me.btnExit.Text = "Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmChTerminalPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(238, 170)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.chkFuName)
        Me.Controls.Add(Me.chkPagePrint)
        Me.Controls.Add(Me.chkSecretChannel)
        Me.Controls.Add(Me.chkDummyData)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChTerminalPreview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Teminal Print Preview"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents chkFuName As System.Windows.Forms.CheckBox
    Public WithEvents chkPagePrint As System.Windows.Forms.CheckBox
    Public WithEvents chkSecretChannel As System.Windows.Forms.CheckBox
    Public WithEvents chkDummyData As System.Windows.Forms.CheckBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
