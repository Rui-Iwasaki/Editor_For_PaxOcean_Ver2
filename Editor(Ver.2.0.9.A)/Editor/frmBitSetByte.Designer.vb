<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBitSetByte
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

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkBit8 = New System.Windows.Forms.CheckBox()
        Me.chkBit0 = New System.Windows.Forms.CheckBox()
        Me.chkBit1 = New System.Windows.Forms.CheckBox()
        Me.chkBit2 = New System.Windows.Forms.CheckBox()
        Me.chkBit3 = New System.Windows.Forms.CheckBox()
        Me.chkBit4 = New System.Windows.Forms.CheckBox()
        Me.chkBit5 = New System.Windows.Forms.CheckBox()
        Me.chkBit6 = New System.Windows.Forms.CheckBox()
        Me.chkBit7 = New System.Windows.Forms.CheckBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.lblValue = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkBit8)
        Me.GroupBox1.Controls.Add(Me.chkBit0)
        Me.GroupBox1.Controls.Add(Me.chkBit1)
        Me.GroupBox1.Controls.Add(Me.chkBit2)
        Me.GroupBox1.Controls.Add(Me.chkBit3)
        Me.GroupBox1.Controls.Add(Me.chkBit4)
        Me.GroupBox1.Controls.Add(Me.chkBit5)
        Me.GroupBox1.Controls.Add(Me.chkBit6)
        Me.GroupBox1.Controls.Add(Me.chkBit7)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(550, 68)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "BitSet"
        '
        'chkBit8
        '
        Me.chkBit8.AutoSize = True
        Me.chkBit8.Location = New System.Drawing.Point(10, 32)
        Me.chkBit8.Name = "chkBit8"
        Me.chkBit8.Size = New System.Drawing.Size(54, 16)
        Me.chkBit8.TabIndex = 8
        Me.chkBit8.Text = "Port9"
        Me.chkBit8.UseVisualStyleBackColor = True
        '
        'chkBit0
        '
        Me.chkBit0.AutoSize = True
        Me.chkBit0.Location = New System.Drawing.Point(490, 32)
        Me.chkBit0.Name = "chkBit0"
        Me.chkBit0.Size = New System.Drawing.Size(54, 16)
        Me.chkBit0.TabIndex = 0
        Me.chkBit0.Text = "Port1"
        Me.chkBit0.UseVisualStyleBackColor = True
        '
        'chkBit1
        '
        Me.chkBit1.AutoSize = True
        Me.chkBit1.Location = New System.Drawing.Point(430, 32)
        Me.chkBit1.Name = "chkBit1"
        Me.chkBit1.Size = New System.Drawing.Size(54, 16)
        Me.chkBit1.TabIndex = 1
        Me.chkBit1.Text = "Port2"
        Me.chkBit1.UseVisualStyleBackColor = True
        '
        'chkBit2
        '
        Me.chkBit2.AutoSize = True
        Me.chkBit2.Location = New System.Drawing.Point(370, 32)
        Me.chkBit2.Name = "chkBit2"
        Me.chkBit2.Size = New System.Drawing.Size(54, 16)
        Me.chkBit2.TabIndex = 2
        Me.chkBit2.Text = "Port3"
        Me.chkBit2.UseVisualStyleBackColor = True
        '
        'chkBit3
        '
        Me.chkBit3.AutoSize = True
        Me.chkBit3.Location = New System.Drawing.Point(310, 32)
        Me.chkBit3.Name = "chkBit3"
        Me.chkBit3.Size = New System.Drawing.Size(54, 16)
        Me.chkBit3.TabIndex = 3
        Me.chkBit3.Text = "Port4"
        Me.chkBit3.UseVisualStyleBackColor = True
        '
        'chkBit4
        '
        Me.chkBit4.AutoSize = True
        Me.chkBit4.Location = New System.Drawing.Point(250, 32)
        Me.chkBit4.Name = "chkBit4"
        Me.chkBit4.Size = New System.Drawing.Size(54, 16)
        Me.chkBit4.TabIndex = 4
        Me.chkBit4.Text = "Port5"
        Me.chkBit4.UseVisualStyleBackColor = True
        '
        'chkBit5
        '
        Me.chkBit5.AutoSize = True
        Me.chkBit5.Location = New System.Drawing.Point(190, 32)
        Me.chkBit5.Name = "chkBit5"
        Me.chkBit5.Size = New System.Drawing.Size(54, 16)
        Me.chkBit5.TabIndex = 5
        Me.chkBit5.Text = "Port6"
        Me.chkBit5.UseVisualStyleBackColor = True
        '
        'chkBit6
        '
        Me.chkBit6.AutoSize = True
        Me.chkBit6.Location = New System.Drawing.Point(130, 32)
        Me.chkBit6.Name = "chkBit6"
        Me.chkBit6.Size = New System.Drawing.Size(54, 16)
        Me.chkBit6.TabIndex = 6
        Me.chkBit6.Text = "Port7"
        Me.chkBit6.UseVisualStyleBackColor = True
        '
        'chkBit7
        '
        Me.chkBit7.AutoSize = True
        Me.chkBit7.Location = New System.Drawing.Point(70, 32)
        Me.chkBit7.Name = "chkBit7"
        Me.chkBit7.Size = New System.Drawing.Size(54, 16)
        Me.chkBit7.TabIndex = 7
        Me.chkBit7.Text = "Port8"
        Me.chkBit7.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(485, 84)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(77, 28)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(398, 84)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(77, 28)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'lblValue
        '
        Me.lblValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblValue.Location = New System.Drawing.Point(56, 88)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(51, 20)
        Me.lblValue.TabIndex = 3
        Me.lblValue.Text = "255"
        Me.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 91)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(35, 12)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Value"
        '
        'frmBitSetByte
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(574, 117)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblValue)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 30)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBitSetByte"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "BitSet"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkBit1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkBit2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkBit3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkBit4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkBit5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkBit6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkBit7 As System.Windows.Forms.CheckBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents chkBit0 As System.Windows.Forms.CheckBox
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chkBit8 As System.Windows.Forms.CheckBox
#End Region

End Class
