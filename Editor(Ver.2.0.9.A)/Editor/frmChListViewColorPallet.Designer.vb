<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChListViewColorPallet
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

    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optColor6 = New System.Windows.Forms.RadioButton()
        Me.optColor5 = New System.Windows.Forms.RadioButton()
        Me.optColor4 = New System.Windows.Forms.RadioButton()
        Me.optColor3 = New System.Windows.Forms.RadioButton()
        Me.optColor2 = New System.Windows.Forms.RadioButton()
        Me.optColor1 = New System.Windows.Forms.RadioButton()
        Me.lblColor1 = New System.Windows.Forms.Label()
        Me.lblColor6 = New System.Windows.Forms.Label()
        Me.lblColor5 = New System.Windows.Forms.Label()
        Me.lblColor4 = New System.Windows.Forms.Label()
        Me.lblColor3 = New System.Windows.Forms.Label()
        Me.lblColor2 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(44, 164)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 18
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(172, 164)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 17
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optColor6)
        Me.GroupBox1.Controls.Add(Me.optColor5)
        Me.GroupBox1.Controls.Add(Me.optColor4)
        Me.GroupBox1.Controls.Add(Me.optColor3)
        Me.GroupBox1.Controls.Add(Me.optColor2)
        Me.GroupBox1.Controls.Add(Me.optColor1)
        Me.GroupBox1.Controls.Add(Me.lblColor1)
        Me.GroupBox1.Controls.Add(Me.lblColor6)
        Me.GroupBox1.Controls.Add(Me.lblColor5)
        Me.GroupBox1.Controls.Add(Me.lblColor4)
        Me.GroupBox1.Controls.Add(Me.lblColor3)
        Me.GroupBox1.Controls.Add(Me.lblColor2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(312, 133)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "COLOR PALLET"
        '
        'optColor6
        '
        Me.optColor6.BackColor = System.Drawing.SystemColors.Control
        Me.optColor6.Cursor = System.Windows.Forms.Cursors.Default
        Me.optColor6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optColor6.Location = New System.Drawing.Point(232, 96)
        Me.optColor6.Name = "optColor6"
        Me.optColor6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optColor6.Size = New System.Drawing.Size(17, 17)
        Me.optColor6.TabIndex = 25
        Me.optColor6.TabStop = True
        Me.optColor6.Text = "Option2"
        Me.optColor6.UseVisualStyleBackColor = True
        '
        'optColor5
        '
        Me.optColor5.BackColor = System.Drawing.SystemColors.Control
        Me.optColor5.Cursor = System.Windows.Forms.Cursors.Default
        Me.optColor5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optColor5.Location = New System.Drawing.Point(128, 96)
        Me.optColor5.Name = "optColor5"
        Me.optColor5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optColor5.Size = New System.Drawing.Size(17, 17)
        Me.optColor5.TabIndex = 24
        Me.optColor5.TabStop = True
        Me.optColor5.Text = "Option2"
        Me.optColor5.UseVisualStyleBackColor = True
        '
        'optColor4
        '
        Me.optColor4.BackColor = System.Drawing.SystemColors.Control
        Me.optColor4.Cursor = System.Windows.Forms.Cursors.Default
        Me.optColor4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optColor4.Location = New System.Drawing.Point(24, 96)
        Me.optColor4.Name = "optColor4"
        Me.optColor4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optColor4.Size = New System.Drawing.Size(17, 17)
        Me.optColor4.TabIndex = 23
        Me.optColor4.TabStop = True
        Me.optColor4.Text = "Option1"
        Me.optColor4.UseVisualStyleBackColor = True
        '
        'optColor3
        '
        Me.optColor3.BackColor = System.Drawing.SystemColors.Control
        Me.optColor3.Cursor = System.Windows.Forms.Cursors.Default
        Me.optColor3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optColor3.Location = New System.Drawing.Point(232, 40)
        Me.optColor3.Name = "optColor3"
        Me.optColor3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optColor3.Size = New System.Drawing.Size(17, 17)
        Me.optColor3.TabIndex = 19
        Me.optColor3.TabStop = True
        Me.optColor3.Text = "Option2"
        Me.optColor3.UseVisualStyleBackColor = True
        '
        'optColor2
        '
        Me.optColor2.BackColor = System.Drawing.SystemColors.Control
        Me.optColor2.Cursor = System.Windows.Forms.Cursors.Default
        Me.optColor2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optColor2.Location = New System.Drawing.Point(128, 40)
        Me.optColor2.Name = "optColor2"
        Me.optColor2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optColor2.Size = New System.Drawing.Size(17, 17)
        Me.optColor2.TabIndex = 18
        Me.optColor2.TabStop = True
        Me.optColor2.Text = "Option2"
        Me.optColor2.UseVisualStyleBackColor = True
        '
        'optColor1
        '
        Me.optColor1.BackColor = System.Drawing.SystemColors.Control
        Me.optColor1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optColor1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optColor1.Location = New System.Drawing.Point(24, 40)
        Me.optColor1.Name = "optColor1"
        Me.optColor1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optColor1.Size = New System.Drawing.Size(17, 17)
        Me.optColor1.TabIndex = 17
        Me.optColor1.TabStop = True
        Me.optColor1.Text = "Option1"
        Me.optColor1.UseVisualStyleBackColor = False
        '
        'lblColor1
        '
        Me.lblColor1.BackColor = System.Drawing.Color.Black
        Me.lblColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblColor1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblColor1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblColor1.Location = New System.Drawing.Point(56, 32)
        Me.lblColor1.Name = "lblColor1"
        Me.lblColor1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblColor1.Size = New System.Drawing.Size(25, 25)
        Me.lblColor1.TabIndex = 32
        '
        'lblColor6
        '
        Me.lblColor6.BackColor = System.Drawing.Color.Indigo
        Me.lblColor6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblColor6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblColor6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblColor6.Location = New System.Drawing.Point(264, 88)
        Me.lblColor6.Name = "lblColor6"
        Me.lblColor6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblColor6.Size = New System.Drawing.Size(25, 25)
        Me.lblColor6.TabIndex = 28
        '
        'lblColor5
        '
        Me.lblColor5.BackColor = System.Drawing.Color.Red
        Me.lblColor5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblColor5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblColor5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblColor5.Location = New System.Drawing.Point(160, 88)
        Me.lblColor5.Name = "lblColor5"
        Me.lblColor5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblColor5.Size = New System.Drawing.Size(25, 25)
        Me.lblColor5.TabIndex = 27
        '
        'lblColor4
        '
        Me.lblColor4.BackColor = System.Drawing.Color.Cyan
        Me.lblColor4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblColor4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblColor4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblColor4.Location = New System.Drawing.Point(56, 88)
        Me.lblColor4.Name = "lblColor4"
        Me.lblColor4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblColor4.Size = New System.Drawing.Size(25, 25)
        Me.lblColor4.TabIndex = 26
        '
        'lblColor3
        '
        Me.lblColor3.BackColor = System.Drawing.Color.Green
        Me.lblColor3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblColor3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblColor3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblColor3.Location = New System.Drawing.Point(264, 32)
        Me.lblColor3.Name = "lblColor3"
        Me.lblColor3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblColor3.Size = New System.Drawing.Size(25, 25)
        Me.lblColor3.TabIndex = 22
        '
        'lblColor2
        '
        Me.lblColor2.BackColor = System.Drawing.Color.Blue
        Me.lblColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblColor2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblColor2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblColor2.Location = New System.Drawing.Point(160, 32)
        Me.lblColor2.Name = "lblColor2"
        Me.lblColor2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblColor2.Size = New System.Drawing.Size(25, 25)
        Me.lblColor2.TabIndex = 21
        '
        'frmChListViewColorPallet
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(334, 217)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChListViewColorPallet"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "COLOR PALLET"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents optColor6 As System.Windows.Forms.RadioButton
    Public WithEvents optColor5 As System.Windows.Forms.RadioButton
    Public WithEvents optColor4 As System.Windows.Forms.RadioButton
    Public WithEvents optColor3 As System.Windows.Forms.RadioButton
    Public WithEvents optColor2 As System.Windows.Forms.RadioButton
    Public WithEvents optColor1 As System.Windows.Forms.RadioButton
    Public WithEvents lblColor1 As System.Windows.Forms.Label
    Public WithEvents lblColor6 As System.Windows.Forms.Label
    Public WithEvents lblColor5 As System.Windows.Forms.Label
    Public WithEvents lblColor4 As System.Windows.Forms.Label
    Public WithEvents lblColor3 As System.Windows.Forms.Label
    Public WithEvents lblColor2 As System.Windows.Forms.Label
#End Region

End Class
