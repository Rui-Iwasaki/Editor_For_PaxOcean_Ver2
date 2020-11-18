<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChListRemoteIO
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

    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents Label14 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtDispIndex = New System.Windows.Forms.TextBox()
        Me.lblDispIndex = New System.Windows.Forms.Label()
        Me.txtGroupNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbFunction = New System.Windows.Forms.ComboBox()
        Me.txtPin = New System.Windows.Forms.TextBox()
        Me.txtPortNo = New System.Windows.Forms.TextBox()
        Me.txtFuNo = New System.Windows.Forms.TextBox()
        Me.txtChNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbDataType = New System.Windows.Forms.ComboBox()
        Me.cmbSysNo = New System.Windows.Forms.ComboBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(352, 252)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(228, 252)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(32, 171)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(53, 12)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "Function"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(14, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 73
        Me.Label1.Text = "FU Address"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtDispIndex)
        Me.GroupBox1.Controls.Add(Me.lblDispIndex)
        Me.GroupBox1.Controls.Add(Me.txtGroupNo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbFunction)
        Me.GroupBox1.Controls.Add(Me.txtPin)
        Me.GroupBox1.Controls.Add(Me.txtPortNo)
        Me.GroupBox1.Controls.Add(Me.txtFuNo)
        Me.GroupBox1.Controls.Add(Me.txtChNo)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbDataType)
        Me.GroupBox1.Controls.Add(Me.cmbSysNo)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label36)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label37)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(454, 232)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Function"
        '
        'txtDispIndex
        '
        Me.txtDispIndex.AcceptsReturn = True
        Me.txtDispIndex.Location = New System.Drawing.Point(236, 114)
        Me.txtDispIndex.MaxLength = 0
        Me.txtDispIndex.Name = "txtDispIndex"
        Me.txtDispIndex.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtDispIndex.Size = New System.Drawing.Size(48, 19)
        Me.txtDispIndex.TabIndex = 6
        Me.txtDispIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblDispIndex
        '
        Me.lblDispIndex.AutoSize = True
        Me.lblDispIndex.BackColor = System.Drawing.SystemColors.Control
        Me.lblDispIndex.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDispIndex.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDispIndex.Location = New System.Drawing.Point(163, 117)
        Me.lblDispIndex.Name = "lblDispIndex"
        Me.lblDispIndex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDispIndex.Size = New System.Drawing.Size(65, 12)
        Me.lblDispIndex.TabIndex = 113
        Me.lblDispIndex.Text = "Disp Index"
        Me.lblDispIndex.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGroupNo
        '
        Me.txtGroupNo.AcceptsReturn = True
        Me.txtGroupNo.Location = New System.Drawing.Point(91, 140)
        Me.txtGroupNo.MaxLength = 0
        Me.txtGroupNo.Name = "txtGroupNo"
        Me.txtGroupNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtGroupNo.Size = New System.Drawing.Size(48, 19)
        Me.txtGroupNo.TabIndex = 6
        Me.txtGroupNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(24, 144)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 111
        Me.Label2.Text = "Group No."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbFunction
        '
        Me.cmbFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFunction.FormattingEnabled = True
        Me.cmbFunction.Location = New System.Drawing.Point(92, 168)
        Me.cmbFunction.Name = "cmbFunction"
        Me.cmbFunction.Size = New System.Drawing.Size(238, 20)
        Me.cmbFunction.TabIndex = 7
        '
        'txtPin
        '
        Me.txtPin.AcceptsReturn = True
        Me.txtPin.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPin.Location = New System.Drawing.Point(175, 22)
        Me.txtPin.MaxLength = 0
        Me.txtPin.Name = "txtPin"
        Me.txtPin.ReadOnly = True
        Me.txtPin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPin.Size = New System.Drawing.Size(40, 19)
        Me.txtPin.TabIndex = 2
        Me.txtPin.TabStop = False
        '
        'txtPortNo
        '
        Me.txtPortNo.AcceptsReturn = True
        Me.txtPortNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPortNo.Location = New System.Drawing.Point(133, 22)
        Me.txtPortNo.MaxLength = 0
        Me.txtPortNo.Name = "txtPortNo"
        Me.txtPortNo.ReadOnly = True
        Me.txtPortNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNo.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNo.TabIndex = 1
        Me.txtPortNo.TabStop = False
        '
        'txtFuNo
        '
        Me.txtFuNo.AcceptsReturn = True
        Me.txtFuNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFuNo.Location = New System.Drawing.Point(91, 22)
        Me.txtFuNo.MaxLength = 0
        Me.txtFuNo.Name = "txtFuNo"
        Me.txtFuNo.ReadOnly = True
        Me.txtFuNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNo.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNo.TabIndex = 0
        Me.txtFuNo.TabStop = False
        '
        'txtChNo
        '
        Me.txtChNo.AcceptsReturn = True
        Me.txtChNo.Location = New System.Drawing.Point(91, 114)
        Me.txtChNo.MaxLength = 0
        Me.txtChNo.Name = "txtChNo"
        Me.txtChNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtChNo.Size = New System.Drawing.Size(48, 19)
        Me.txtChNo.TabIndex = 5
        Me.txtChNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(38, 117)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 105
        Me.Label7.Text = "CH No."
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbDataType
        '
        Me.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataType.FormattingEnabled = True
        Me.cmbDataType.Location = New System.Drawing.Point(91, 52)
        Me.cmbDataType.Name = "cmbDataType"
        Me.cmbDataType.Size = New System.Drawing.Size(110, 20)
        Me.cmbDataType.TabIndex = 3
        '
        'cmbSysNo
        '
        Me.cmbSysNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSysNo.FormattingEnabled = True
        Me.cmbSysNo.Location = New System.Drawing.Point(91, 83)
        Me.cmbSysNo.Name = "cmbSysNo"
        Me.cmbSysNo.Size = New System.Drawing.Size(110, 20)
        Me.cmbSysNo.TabIndex = 4
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.Location = New System.Drawing.Point(92, 197)
        Me.txtRemarks.MaxLength = 16
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(349, 19)
        Me.txtRemarks.TabIndex = 8
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(28, 200)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label36.Size = New System.Drawing.Size(47, 12)
        Me.Label36.TabIndex = 99
        Me.Label36.Text = "Remarks"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(24, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(59, 12)
        Me.Label8.TabIndex = 81
        Me.Label8.Text = "Data Type"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(36, 86)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(47, 12)
        Me.Label37.TabIndex = 67
        Me.Label37.Text = "Sys No."
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmChListRemoteIO
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(479, 293)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 30)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChListRemoteIO"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Function Set"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbDataType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSysNo As System.Windows.Forms.ComboBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents txtChNo As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents txtPin As System.Windows.Forms.TextBox
    Public WithEvents txtPortNo As System.Windows.Forms.TextBox
    Public WithEvents txtFuNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbFunction As System.Windows.Forms.ComboBox
    Public WithEvents txtGroupNo As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtDispIndex As System.Windows.Forms.TextBox
    Public WithEvents lblDispIndex As System.Windows.Forms.Label
#End Region
End Class
