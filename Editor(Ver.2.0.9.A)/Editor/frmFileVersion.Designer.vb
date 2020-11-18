<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFileVersion
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

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.numVersion = New System.Windows.Forms.NumericUpDown()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.lstVersion = New System.Windows.Forms.ListBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.SaveChkLabel = New System.Windows.Forms.Label()
        Me.chkExcelOUT = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.numVersion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.numVersion)
        Me.GroupBox1.Location = New System.Drawing.Point(126, 187)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(137, 79)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Save Version"
        Me.GroupBox1.Visible = False
        '
        'numVersion
        '
        Me.numVersion.Enabled = False
        Me.numVersion.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVersion.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.numVersion.Location = New System.Drawing.Point(38, 34)
        Me.numVersion.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.numVersion.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numVersion.Name = "numVersion"
        Me.numVersion.Size = New System.Drawing.Size(61, 20)
        Me.numVersion.TabIndex = 9
        Me.numVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numVersion.ThousandsSeparator = True
        Me.numVersion.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numVersion.Visible = False
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(234, 85)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(94, 27)
        Me.cmdOK.TabIndex = 7
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(343, 85)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(94, 27)
        Me.cmdCancel.TabIndex = 6
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'lstVersion
        '
        Me.lstVersion.FormattingEnabled = True
        Me.lstVersion.ItemHeight = 12
        Me.lstVersion.Location = New System.Drawing.Point(11, 23)
        Me.lstVersion.Name = "lstVersion"
        Me.lstVersion.Size = New System.Drawing.Size(87, 76)
        Me.lstVersion.TabIndex = 8
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lstVersion)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 154)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(108, 112)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Exist Version"
        Me.GroupBox2.Visible = False
        '
        'SaveChkLabel
        '
        Me.SaveChkLabel.AutoSize = True
        Me.SaveChkLabel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SaveChkLabel.Location = New System.Drawing.Point(30, 36)
        Me.SaveChkLabel.Name = "SaveChkLabel"
        Me.SaveChkLabel.Size = New System.Drawing.Size(49, 13)
        Me.SaveChkLabel.TabIndex = 11
        Me.SaveChkLabel.Text = "Label1"
        '
        'chkExcelOUT
        '
        Me.chkExcelOUT.AutoSize = True
        Me.chkExcelOUT.Checked = True
        Me.chkExcelOUT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExcelOUT.Location = New System.Drawing.Point(359, 63)
        Me.chkExcelOUT.Name = "chkExcelOUT"
        Me.chkExcelOUT.Size = New System.Drawing.Size(78, 16)
        Me.chkExcelOUT.TabIndex = 12
        Me.chkExcelOUT.Text = "Excel OUT"
        Me.chkExcelOUT.UseVisualStyleBackColor = True
        Me.chkExcelOUT.Visible = False
        '
        'frmFileVersion
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(449, 130)
        Me.Controls.Add(Me.chkExcelOUT)
        Me.Controls.Add(Me.SaveChkLabel)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFileVersion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Confirmation"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.numVersion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents lstVersion As System.Windows.Forms.ListBox
    Friend WithEvents numVersion As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents SaveChkLabel As System.Windows.Forms.Label
    Friend WithEvents chkExcelOUT As System.Windows.Forms.CheckBox
End Class
