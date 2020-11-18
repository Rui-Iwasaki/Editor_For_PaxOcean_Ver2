<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmToolMenu
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
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnChkChList = New System.Windows.Forms.Button()
        Me.btnGetCHnoToFUadr = New System.Windows.Forms.Button()
        Me.btnGrpDisp = New System.Windows.Forms.Button()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Location = New System.Drawing.Point(12, 70)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(192, 31)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "EXIT"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnChkChList
        '
        Me.btnChkChList.Location = New System.Drawing.Point(12, 12)
        Me.btnChkChList.Name = "btnChkChList"
        Me.btnChkChList.Size = New System.Drawing.Size(192, 31)
        Me.btnChkChList.TabIndex = 1
        Me.btnChkChList.Text = "Search CH"
        Me.btnChkChList.UseVisualStyleBackColor = True
        '
        'btnGetCHnoToFUadr
        '
        Me.btnGetCHnoToFUadr.Location = New System.Drawing.Point(52, 83)
        Me.btnGetCHnoToFUadr.Name = "btnGetCHnoToFUadr"
        Me.btnGetCHnoToFUadr.Size = New System.Drawing.Size(76, 31)
        Me.btnGetCHnoToFUadr.TabIndex = 2
        Me.btnGetCHnoToFUadr.Text = "Get CHno To FU Adress"
        Me.btnGetCHnoToFUadr.UseVisualStyleBackColor = True
        Me.btnGetCHnoToFUadr.Visible = False
        '
        'btnGrpDisp
        '
        Me.btnGrpDisp.Location = New System.Drawing.Point(84, 92)
        Me.btnGrpDisp.Name = "btnGrpDisp"
        Me.btnGrpDisp.Size = New System.Drawing.Size(76, 31)
        Me.btnGrpDisp.TabIndex = 3
        Me.btnGrpDisp.Text = "GrpDisp"
        Me.btnGrpDisp.UseVisualStyleBackColor = True
        Me.btnGrpDisp.Visible = False
        '
        'btnOutput
        '
        Me.btnOutput.Location = New System.Drawing.Point(104, 107)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(76, 31)
        Me.btnOutput.TabIndex = 4
        Me.btnOutput.Text = "OutPutDisp"
        Me.btnOutput.UseVisualStyleBackColor = True
        Me.btnOutput.Visible = False
        '
        'frmToolMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(211, 126)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnOutput)
        Me.Controls.Add(Me.btnGrpDisp)
        Me.Controls.Add(Me.btnGetCHnoToFUadr)
        Me.Controls.Add(Me.btnChkChList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmToolMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Other Tool Menu"
        Me.ResumeLayout(False)

    End Sub
	Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnChkChList As System.Windows.Forms.Button
    Friend WithEvents btnGetCHnoToFUadr As System.Windows.Forms.Button
    Friend WithEvents btnGrpDisp As System.Windows.Forms.Button
    Friend WithEvents btnOutput As Button
End Class
