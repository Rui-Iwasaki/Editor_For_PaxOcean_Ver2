<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsPulldownMenu
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

    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.grdMenuName = New Editor.clsDataGridViewPlus()
        Me.optVDU = New System.Windows.Forms.RadioButton()
        Me.optOPS = New System.Windows.Forms.RadioButton()
        CType(Me.grdMenuName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(430, 307)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 1
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(311, 307)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 2
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'grdMenuName
        '
        Me.grdMenuName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdMenuName.Location = New System.Drawing.Point(21, 23)
        Me.grdMenuName.Name = "grdMenuName"
        Me.grdMenuName.RowTemplate.Height = 21
        Me.grdMenuName.Size = New System.Drawing.Size(522, 271)
        Me.grdMenuName.TabIndex = 3
        '
        'optVDU
        '
        Me.optVDU.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optVDU.Appearance = System.Windows.Forms.Appearance.Button
        Me.optVDU.BackColor = System.Drawing.SystemColors.Control
        Me.optVDU.Cursor = System.Windows.Forms.Cursors.Default
        Me.optVDU.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optVDU.Location = New System.Drawing.Point(140, 307)
        Me.optVDU.Name = "optVDU"
        Me.optVDU.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optVDU.Size = New System.Drawing.Size(113, 33)
        Me.optVDU.TabIndex = 154
        Me.optVDU.TabStop = True
        Me.optVDU.Text = "Ext.VDU"
        Me.optVDU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optVDU.UseVisualStyleBackColor = True
        '
        'optOPS
        '
        Me.optOPS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optOPS.Appearance = System.Windows.Forms.Appearance.Button
        Me.optOPS.BackColor = System.Drawing.SystemColors.Control
        Me.optOPS.Cursor = System.Windows.Forms.Cursors.Default
        Me.optOPS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOPS.Location = New System.Drawing.Point(21, 307)
        Me.optOPS.Name = "optOPS"
        Me.optOPS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOPS.Size = New System.Drawing.Size(113, 33)
        Me.optOPS.TabIndex = 153
        Me.optOPS.TabStop = True
        Me.optOPS.Text = "OPS"
        Me.optOPS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optOPS.UseVisualStyleBackColor = True
        '
        'frmOpsPulldownMenu
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(566, 350)
        Me.ControlBox = False
        Me.Controls.Add(Me.optVDU)
        Me.Controls.Add(Me.optOPS)
        Me.Controls.Add(Me.grdMenuName)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsPulldownMenu"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "PULLDOWN MENU SET"
        CType(Me.grdMenuName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents grdMenuName As Editor.clsDataGridViewPlus
    Public WithEvents optVDU As System.Windows.Forms.RadioButton
    Public WithEvents optOPS As System.Windows.Forms.RadioButton
#End Region

End Class
