<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsPulldownSub_GAI_Print
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
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.chkPrint1 = New System.Windows.Forms.CheckBox()
        Me.chkPrint2 = New System.Windows.Forms.CheckBox()
        Me.chkPrint6 = New System.Windows.Forms.CheckBox()
        Me.chkPrint3 = New System.Windows.Forms.CheckBox()
        Me.chkPrint5 = New System.Windows.Forms.CheckBox()
        Me.chkPrint7 = New System.Windows.Forms.CheckBox()
        Me.chkPrint4 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(57, 277)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(113, 32)
        Me.cmdOk.TabIndex = 4
        Me.cmdOk.Text = "OK"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(176, 277)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 32)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'chkPrint1
        '
        Me.chkPrint1.Location = New System.Drawing.Point(29, 12)
        Me.chkPrint1.Name = "chkPrint1"
        Me.chkPrint1.Size = New System.Drawing.Size(185, 29)
        Me.chkPrint1.TabIndex = 5
        Me.chkPrint1.Text = "DEMAND TIME LOG"
        Me.chkPrint1.UseVisualStyleBackColor = True
        '
        'chkPrint2
        '
        Me.chkPrint2.Location = New System.Drawing.Point(29, 47)
        Me.chkPrint2.Name = "chkPrint2"
        Me.chkPrint2.Size = New System.Drawing.Size(185, 29)
        Me.chkPrint2.TabIndex = 5
        Me.chkPrint2.Text = "SET VALUE && DELAY TIMER"
        Me.chkPrint2.UseVisualStyleBackColor = True
        '
        'chkPrint6
        '
        Me.chkPrint6.Location = New System.Drawing.Point(29, 187)
        Me.chkPrint6.Name = "chkPrint6"
        Me.chkPrint6.Size = New System.Drawing.Size(185, 29)
        Me.chkPrint6.TabIndex = 5
        Me.chkPrint6.Text = "ALARM, RECOVERY LOG"
        Me.chkPrint6.UseVisualStyleBackColor = True
        '
        'chkPrint3
        '
        Me.chkPrint3.Location = New System.Drawing.Point(29, 82)
        Me.chkPrint3.Name = "chkPrint3"
        Me.chkPrint3.Size = New System.Drawing.Size(185, 29)
        Me.chkPrint3.TabIndex = 5
        Me.chkPrint3.Text = "MONOCHROME HARD COPY"
        Me.chkPrint3.UseVisualStyleBackColor = True
        '
        'chkPrint5
        '
        Me.chkPrint5.Location = New System.Drawing.Point(29, 152)
        Me.chkPrint5.Name = "chkPrint5"
        Me.chkPrint5.Size = New System.Drawing.Size(185, 29)
        Me.chkPrint5.TabIndex = 5
        Me.chkPrint5.Text = "SAVE USB MEMORY"
        Me.chkPrint5.UseVisualStyleBackColor = True
        '
        'chkPrint7
        '
        Me.chkPrint7.Location = New System.Drawing.Point(29, 222)
        Me.chkPrint7.Name = "chkPrint7"
        Me.chkPrint7.Size = New System.Drawing.Size(185, 29)
        Me.chkPrint7.TabIndex = 5
        Me.chkPrint7.Text = "SAVE DEMAND TIME LOG"
        Me.chkPrint7.UseVisualStyleBackColor = True
        '
        'chkPrint4
        '
        Me.chkPrint4.Location = New System.Drawing.Point(29, 117)
        Me.chkPrint4.Name = "chkPrint4"
        Me.chkPrint4.Size = New System.Drawing.Size(185, 29)
        Me.chkPrint4.TabIndex = 5
        Me.chkPrint4.Text = "COLOR HARD COPY"
        Me.chkPrint4.UseVisualStyleBackColor = True
        '
        'frmOpsPulldownSub_GAI_Print
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(305, 352)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkPrint4)
        Me.Controls.Add(Me.chkPrint7)
        Me.Controls.Add(Me.chkPrint5)
        Me.Controls.Add(Me.chkPrint3)
        Me.Controls.Add(Me.chkPrint6)
        Me.Controls.Add(Me.chkPrint2)
        Me.Controls.Add(Me.chkPrint1)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.cmdCancel)
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsPulldownSub_GAI_Print"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "PRINT SUB MENU"
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents chkPrint1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrint2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrint6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrint3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrint5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrint7 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrint4 As System.Windows.Forms.CheckBox
End Class
