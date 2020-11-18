<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChOutputAoDetail
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

    Public WithEvents txtCHNo As System.Windows.Forms.TextBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents txtDist As System.Windows.Forms.TextBox
    Public WithEvents txtCom As System.Windows.Forms.TextBox
    Public WithEvents txtCableOut As System.Windows.Forms.TextBox
    Public WithEvents txtCoreNoMinus As System.Windows.Forms.TextBox
    Public WithEvents txtCoreNoPlus As System.Windows.Forms.TextBox
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblRowNo As System.Windows.Forms.Label
    Public WithEvents lblNo As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtCHNo = New System.Windows.Forms.TextBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.txtDist = New System.Windows.Forms.TextBox()
        Me.txtCom = New System.Windows.Forms.TextBox()
        Me.txtCableOut = New System.Windows.Forms.TextBox()
        Me.txtCoreNoMinus = New System.Windows.Forms.TextBox()
        Me.txtCoreNoPlus = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblRowNo = New System.Windows.Forms.Label()
        Me.lblNo = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtPin = New System.Windows.Forms.TextBox()
        Me.txtPortNo = New System.Windows.Forms.TextBox()
        Me.txtFuNo = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtCHNo
        '
        Me.txtCHNo.AcceptsReturn = True
        Me.txtCHNo.Location = New System.Drawing.Point(87, 38)
        Me.txtCHNo.MaxLength = 0
        Me.txtCHNo.Name = "txtCHNo"
        Me.txtCHNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCHNo.Size = New System.Drawing.Size(57, 19)
        Me.txtCHNo.TabIndex = 0
        Me.txtCHNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(176, 252)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(52, 252)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 7
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'txtDist
        '
        Me.txtDist.AcceptsReturn = True
        Me.txtDist.Location = New System.Drawing.Point(87, 133)
        Me.txtDist.MaxLength = 0
        Me.txtDist.Name = "txtDist"
        Me.txtDist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDist.Size = New System.Drawing.Size(89, 19)
        Me.txtDist.TabIndex = 5
        '
        'txtCom
        '
        Me.txtCom.AcceptsReturn = True
        Me.txtCom.Location = New System.Drawing.Point(230, 101)
        Me.txtCom.MaxLength = 0
        Me.txtCom.Name = "txtCom"
        Me.txtCom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCom.Size = New System.Drawing.Size(89, 19)
        Me.txtCom.TabIndex = 4
        '
        'txtCableOut
        '
        Me.txtCableOut.AcceptsReturn = True
        Me.txtCableOut.Location = New System.Drawing.Point(87, 101)
        Me.txtCableOut.MaxLength = 0
        Me.txtCableOut.Name = "txtCableOut"
        Me.txtCableOut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCableOut.Size = New System.Drawing.Size(89, 19)
        Me.txtCableOut.TabIndex = 3
        '
        'txtCoreNoMinus
        '
        Me.txtCoreNoMinus.AcceptsReturn = True
        Me.txtCoreNoMinus.Location = New System.Drawing.Point(230, 69)
        Me.txtCoreNoMinus.MaxLength = 0
        Me.txtCoreNoMinus.Name = "txtCoreNoMinus"
        Me.txtCoreNoMinus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCoreNoMinus.Size = New System.Drawing.Size(39, 19)
        Me.txtCoreNoMinus.TabIndex = 2
        '
        'txtCoreNoPlus
        '
        Me.txtCoreNoPlus.AcceptsReturn = True
        Me.txtCoreNoPlus.Location = New System.Drawing.Point(87, 69)
        Me.txtCoreNoPlus.MaxLength = 0
        Me.txtCoreNoPlus.Name = "txtCoreNoPlus"
        Me.txtCoreNoPlus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCoreNoPlus.Size = New System.Drawing.Size(39, 19)
        Me.txtCoreNoPlus.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(48, 135)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(29, 12)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "DEST"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(190, 103)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(23, 12)
        Me.Label17.TabIndex = 9
        Me.Label17.Text = "COM"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(14, 103)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(59, 12)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "Cable OUT"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(162, 71)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(59, 12)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "CoreNo(-)"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(16, 71)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(59, 12)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "CoreNo(+)"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(34, 41)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(41, 12)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "CH No."
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRowNo
        '
        Me.lblRowNo.AutoSize = True
        Me.lblRowNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblRowNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRowNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRowNo.Location = New System.Drawing.Point(54, 14)
        Me.lblRowNo.Name = "lblRowNo"
        Me.lblRowNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRowNo.Size = New System.Drawing.Size(23, 12)
        Me.lblRowNo.TabIndex = 1
        Me.lblRowNo.Text = "No."
        Me.lblRowNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNo
        '
        Me.lblNo.AutoSize = True
        Me.lblNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNo.Location = New System.Drawing.Point(89, 15)
        Me.lblNo.Name = "lblNo"
        Me.lblNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNo.Size = New System.Drawing.Size(17, 12)
        Me.lblNo.TabIndex = 0
        Me.lblNo.Text = "01"
        Me.lblNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.txtPin)
        Me.GroupBox1.Controls.Add(Me.txtPortNo)
        Me.GroupBox1.Controls.Add(Me.txtFuNo)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(12, 160)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(300, 85)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Output"
        '
        'txtPin
        '
        Me.txtPin.AcceptsReturn = True
        Me.txtPin.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPin.Location = New System.Drawing.Point(183, 38)
        Me.txtPin.MaxLength = 0
        Me.txtPin.Name = "txtPin"
        Me.txtPin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPin.Size = New System.Drawing.Size(40, 19)
        Me.txtPin.TabIndex = 2
        '
        'txtPortNo
        '
        Me.txtPortNo.AcceptsReturn = True
        Me.txtPortNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPortNo.Location = New System.Drawing.Point(141, 38)
        Me.txtPortNo.MaxLength = 0
        Me.txtPortNo.Name = "txtPortNo"
        Me.txtPortNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNo.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNo.TabIndex = 1
        '
        'txtFuNo
        '
        Me.txtFuNo.AcceptsReturn = True
        Me.txtFuNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFuNo.Location = New System.Drawing.Point(99, 38)
        Me.txtFuNo.MaxLength = 0
        Me.txtFuNo.Name = "txtFuNo"
        Me.txtFuNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNo.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNo.TabIndex = 0
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(22, 41)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(65, 12)
        Me.Label21.TabIndex = 90
        Me.Label21.Text = "FU Address"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmChOutputAoDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(341, 293)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtCHNo)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.txtDist)
        Me.Controls.Add(Me.txtCom)
        Me.Controls.Add(Me.txtCableOut)
        Me.Controls.Add(Me.txtCoreNoMinus)
        Me.Controls.Add(Me.txtCoreNoPlus)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblRowNo)
        Me.Controls.Add(Me.lblNo)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChOutputAoDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "AO DETAILS"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents txtPin As System.Windows.Forms.TextBox
    Public WithEvents txtPortNo As System.Windows.Forms.TextBox
    Public WithEvents txtFuNo As System.Windows.Forms.TextBox
#End Region

End Class
