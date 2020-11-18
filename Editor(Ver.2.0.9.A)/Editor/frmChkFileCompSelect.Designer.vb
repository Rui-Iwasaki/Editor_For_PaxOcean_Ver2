<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChkFileCompSelect
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
        Me.btnEXIT = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.grdChk = New Editor.clsDataGridViewPlus()
        Me.btnDetail = New System.Windows.Forms.Button()
        CType(Me.grdChk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnEXIT
        '
        Me.btnEXIT.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEXIT.Location = New System.Drawing.Point(167, 489)
        Me.btnEXIT.Name = "btnEXIT"
        Me.btnEXIT.Size = New System.Drawing.Size(135, 33)
        Me.btnEXIT.TabIndex = 0
        Me.btnEXIT.Text = "EXIT"
        Me.btnEXIT.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(10, 489)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(135, 33)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "SAVE"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grdChk
        '
        Me.grdChk.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdChk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChk.Location = New System.Drawing.Point(12, 13)
        Me.grdChk.Name = "grdChk"
        Me.grdChk.RowTemplate.Height = 21
        Me.grdChk.Size = New System.Drawing.Size(290, 423)
        Me.grdChk.TabIndex = 7
        '
        'btnDetail
        '
        Me.btnDetail.Location = New System.Drawing.Point(12, 442)
        Me.btnDetail.Name = "btnDetail"
        Me.btnDetail.Size = New System.Drawing.Size(135, 33)
        Me.btnDetail.TabIndex = 8
        Me.btnDetail.Text = "ChInfo Detail"
        Me.btnDetail.UseVisualStyleBackColor = True
        '
        'frmChkFileCompSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnEXIT
        Me.ClientSize = New System.Drawing.Size(314, 534)
        Me.Controls.Add(Me.btnDetail)
        Me.Controls.Add(Me.grdChk)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnEXIT)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmChkFileCompSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Compare Select"
        CType(Me.grdChk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnEXIT As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grdChk As Editor.clsDataGridViewPlus
    Friend WithEvents btnDetail As System.Windows.Forms.Button
End Class
