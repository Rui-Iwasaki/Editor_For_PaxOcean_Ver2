<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsGraphAnalogMeterDetail
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
    Public WithEvents cmbNameSideColorSymbol As System.Windows.Forms.ComboBox
    Public WithEvents cmbPointerColorChange As System.Windows.Forms.ComboBox
    Public WithEvents cmbPointerFrame As System.Windows.Forms.ComboBox
    Public WithEvents cmbMark As System.Windows.Forms.ComboBox
    Public WithEvents cmbDisplayPoint As System.Windows.Forms.ComboBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmbNameSideColorSymbol = New System.Windows.Forms.ComboBox()
        Me.cmbPointerColorChange = New System.Windows.Forms.ComboBox()
        Me.cmbPointerFrame = New System.Windows.Forms.ComboBox()
        Me.cmbMark = New System.Windows.Forms.ComboBox()
        Me.cmbDisplayPoint = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(153, 205)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(19, 205)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 2
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmbNameSideColorSymbol
        '
        Me.cmbNameSideColorSymbol.BackColor = System.Drawing.SystemColors.Window
        Me.cmbNameSideColorSymbol.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbNameSideColorSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNameSideColorSymbol.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbNameSideColorSymbol.Location = New System.Drawing.Point(148, 20)
        Me.cmbNameSideColorSymbol.Name = "cmbNameSideColorSymbol"
        Me.cmbNameSideColorSymbol.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbNameSideColorSymbol.Size = New System.Drawing.Size(105, 20)
        Me.cmbNameSideColorSymbol.TabIndex = 7
        '
        'cmbPointerColorChange
        '
        Me.cmbPointerColorChange.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPointerColorChange.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPointerColorChange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPointerColorChange.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPointerColorChange.Location = New System.Drawing.Point(161, 75)
        Me.cmbPointerColorChange.Name = "cmbPointerColorChange"
        Me.cmbPointerColorChange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPointerColorChange.Size = New System.Drawing.Size(105, 20)
        Me.cmbPointerColorChange.TabIndex = 6
        '
        'cmbPointerFrame
        '
        Me.cmbPointerFrame.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPointerFrame.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPointerFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPointerFrame.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPointerFrame.Location = New System.Drawing.Point(161, 105)
        Me.cmbPointerFrame.Name = "cmbPointerFrame"
        Me.cmbPointerFrame.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPointerFrame.Size = New System.Drawing.Size(105, 20)
        Me.cmbPointerFrame.TabIndex = 5
        Me.cmbPointerFrame.Visible = False
        '
        'cmbMark
        '
        Me.cmbMark.BackColor = System.Drawing.SystemColors.Window
        Me.cmbMark.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbMark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMark.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbMark.Location = New System.Drawing.Point(161, 44)
        Me.cmbMark.Name = "cmbMark"
        Me.cmbMark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbMark.Size = New System.Drawing.Size(105, 20)
        Me.cmbMark.TabIndex = 4
        '
        'cmbDisplayPoint
        '
        Me.cmbDisplayPoint.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDisplayPoint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDisplayPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDisplayPoint.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDisplayPoint.Location = New System.Drawing.Point(161, 12)
        Me.cmbDisplayPoint.Name = "cmbDisplayPoint"
        Me.cmbDisplayPoint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDisplayPoint.Size = New System.Drawing.Size(105, 20)
        Me.cmbDisplayPoint.TabIndex = 3
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(5, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(137, 12)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "Name Side Color Symbol"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(30, 78)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(125, 12)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "Pointer Color Change"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(71, 108)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(83, 12)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "Pointer Frame"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label12.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(29, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(125, 12)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "Mark Numerical Value"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(23, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(131, 12)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "CH Name Display Point"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbNameSideColorSymbol)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 138)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(261, 55)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "VisibleFalse"
        Me.GroupBox1.Visible = False
        '
        'frmOpsGraphAnalogMeterDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(285, 259)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmbPointerColorChange)
        Me.Controls.Add(Me.cmbPointerFrame)
        Me.Controls.Add(Me.cmbMark)
        Me.Controls.Add(Me.cmbDisplayPoint)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.GroupBox1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsGraphAnalogMeterDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ANALOG METER DETAILS"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
#End Region

End Class
