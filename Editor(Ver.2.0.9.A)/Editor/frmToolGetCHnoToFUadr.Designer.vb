<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmToolGetCHnoToFUadr
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
        Me.dlgGetCSV = New System.Windows.Forms.OpenFileDialog()
        Me.btnGO = New System.Windows.Forms.Button()
        Me.btnEND = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'dlgGetCSV
        '
        Me.dlgGetCSV.FileName = "CSVファイル指定"
        '
        'btnGO
        '
        Me.btnGO.Location = New System.Drawing.Point(12, 12)
        Me.btnGO.Name = "btnGO"
        Me.btnGO.Size = New System.Drawing.Size(218, 24)
        Me.btnGO.TabIndex = 0
        Me.btnGO.Text = "ファイル指定と実行"
        Me.btnGO.UseVisualStyleBackColor = True
        '
        'btnEND
        '
        Me.btnEND.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEND.Location = New System.Drawing.Point(188, 90)
        Me.btnEND.Name = "btnEND"
        Me.btnEND.Size = New System.Drawing.Size(42, 20)
        Me.btnEND.TabIndex = 1
        Me.btnEND.Text = "終了"
        Me.btnEND.UseVisualStyleBackColor = True
        '
        'frmToolGetCHnoToFUadr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnEND
        Me.ClientSize = New System.Drawing.Size(252, 121)
        Me.Controls.Add(Me.btnEND)
        Me.Controls.Add(Me.btnGO)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmToolGetCHnoToFUadr"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CHNoからFUadrを求める"
        Me.ResumeLayout(False)

    End Sub

	Friend WithEvents dlgGetCSV As OpenFileDialog
	Friend WithEvents btnGO As Button
	Friend WithEvents btnEND As Button
End Class
