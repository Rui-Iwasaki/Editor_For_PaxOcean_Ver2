<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChkFileCompSelectBig
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
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.chkCH = New System.Windows.Forms.CheckBox()
        Me.chkTer = New System.Windows.Forms.CheckBox()
        Me.chkAdr = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkTbl = New System.Windows.Forms.CheckBox()
        Me.btnDetail = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnEXIT = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Location = New System.Drawing.Point(23, 12)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(93, 16)
        Me.chkAll.TabIndex = 0
        Me.chkAll.Text = "Compare ALL"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'chkCH
        '
        Me.chkCH.AutoSize = True
        Me.chkCH.Location = New System.Drawing.Point(23, 54)
        Me.chkCH.Name = "chkCH"
        Me.chkCH.Size = New System.Drawing.Size(133, 16)
        Me.chkCH.TabIndex = 1
        Me.chkCH.Text = "Compare ChList Only"
        Me.chkCH.UseVisualStyleBackColor = True
        '
        'chkTer
        '
        Me.chkTer.AutoSize = True
        Me.chkTer.Location = New System.Drawing.Point(23, 85)
        Me.chkTer.Name = "chkTer"
        Me.chkTer.Size = New System.Drawing.Size(163, 16)
        Me.chkTer.TabIndex = 2
        Me.chkTer.Text = "Compare TerminalList Only"
        Me.chkTer.UseVisualStyleBackColor = True
        '
        'chkAdr
        '
        Me.chkAdr.AutoSize = True
        Me.chkAdr.Checked = True
        Me.chkAdr.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAdr.Location = New System.Drawing.Point(6, 18)
        Me.chkAdr.Name = "chkAdr"
        Me.chkAdr.Size = New System.Drawing.Size(125, 16)
        Me.chkAdr.TabIndex = 3
        Me.chkAdr.Text = "Include FU Address"
        Me.chkAdr.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkTbl)
        Me.GroupBox1.Controls.Add(Me.chkAdr)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 117)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(178, 68)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'chkTbl
        '
        Me.chkTbl.AutoSize = True
        Me.chkTbl.Checked = True
        Me.chkTbl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTbl.Location = New System.Drawing.Point(6, 46)
        Me.chkTbl.Name = "chkTbl"
        Me.chkTbl.Size = New System.Drawing.Size(99, 16)
        Me.chkTbl.TabIndex = 4
        Me.chkTbl.Text = "Include related"
        Me.chkTbl.UseVisualStyleBackColor = True
        '
        'btnDetail
        '
        Me.btnDetail.Location = New System.Drawing.Point(193, 30)
        Me.btnDetail.Name = "btnDetail"
        Me.btnDetail.Size = New System.Drawing.Size(135, 25)
        Me.btnDetail.TabIndex = 11
        Me.btnDetail.Text = "ChList Detail"
        Me.btnDetail.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(12, 205)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(135, 33)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "SAVE"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnEXIT
        '
        Me.btnEXIT.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEXIT.Location = New System.Drawing.Point(181, 205)
        Me.btnEXIT.Name = "btnEXIT"
        Me.btnEXIT.Size = New System.Drawing.Size(135, 33)
        Me.btnEXIT.TabIndex = 9
        Me.btnEXIT.Text = "EXIT"
        Me.btnEXIT.UseVisualStyleBackColor = True
        '
        'frmChkFileCompSelectBig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnEXIT
        Me.ClientSize = New System.Drawing.Size(340, 270)
        Me.Controls.Add(Me.btnDetail)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnEXIT)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkTer)
        Me.Controls.Add(Me.chkCH)
        Me.Controls.Add(Me.chkAll)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmChkFileCompSelectBig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Compare Select"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkCH As System.Windows.Forms.CheckBox
    Friend WithEvents chkTer As System.Windows.Forms.CheckBox
    Friend WithEvents chkAdr As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkTbl As System.Windows.Forms.CheckBox
    Friend WithEvents btnDetail As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnEXIT As System.Windows.Forms.Button
End Class
