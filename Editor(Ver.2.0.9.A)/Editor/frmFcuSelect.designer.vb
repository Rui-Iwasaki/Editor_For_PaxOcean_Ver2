<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFcuSelect
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
        Me.rbtSelectFcu1 = New System.Windows.Forms.RadioButton()
        Me.rbtSelectFcu2 = New System.Windows.Forms.RadioButton()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'rbtSelectFcu1
        '
        Me.rbtSelectFcu1.AutoSize = True
        Me.rbtSelectFcu1.Checked = True
        Me.rbtSelectFcu1.Location = New System.Drawing.Point(43, 71)
        Me.rbtSelectFcu1.Name = "rbtSelectFcu1"
        Me.rbtSelectFcu1.Size = New System.Drawing.Size(69, 19)
        Me.rbtSelectFcu1.TabIndex = 0
        Me.rbtSelectFcu1.TabStop = True
        Me.rbtSelectFcu1.Text = "FCU 1"
        Me.rbtSelectFcu1.UseVisualStyleBackColor = True
        '
        'rbtSelectFcu2
        '
        Me.rbtSelectFcu2.AutoSize = True
        Me.rbtSelectFcu2.Location = New System.Drawing.Point(43, 114)
        Me.rbtSelectFcu2.Name = "rbtSelectFcu2"
        Me.rbtSelectFcu2.Size = New System.Drawing.Size(69, 19)
        Me.rbtSelectFcu2.TabIndex = 1
        Me.rbtSelectFcu2.Text = "FCU 2"
        Me.rbtSelectFcu2.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(27, 189)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(100, 51)
        Me.btnOpen.TabIndex = 2
        Me.btnOpen.Text = "OPEN"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(156, 15)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Select FCU No. to edit."
        '
        'frmFcuSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(219, 263)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.rbtSelectFcu2)
        Me.Controls.Add(Me.rbtSelectFcu1)
        Me.Name = "frmFcuSelect"
        Me.Text = "EDIT FCU SELECT"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents rbtSelectFcu1 As RadioButton
    Friend WithEvents rbtSelectFcu2 As RadioButton
    Friend WithEvents btnOpen As Button
    Friend WithEvents Label1 As Label
End Class
