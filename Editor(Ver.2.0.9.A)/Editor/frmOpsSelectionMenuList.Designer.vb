<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsSelectionMenuList
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

    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.optVDU = New System.Windows.Forms.RadioButton()
        Me.optOPS = New System.Windows.Forms.RadioButton()
        Me.grdSelectionList = New Editor.clsDataGridViewPlus()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.grdSelectionList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(575, 488)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 4
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(575, 527)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 1
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdDelete
        '
        Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Location = New System.Drawing.Point(575, 449)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(113, 33)
        Me.cmdDelete.TabIndex = 4
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = True
        Me.cmdDelete.Visible = False
        '
        'optVDU
        '
        Me.optVDU.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optVDU.Appearance = System.Windows.Forms.Appearance.Button
        Me.optVDU.BackColor = System.Drawing.SystemColors.Control
        Me.optVDU.Cursor = System.Windows.Forms.Cursors.Default
        Me.optVDU.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optVDU.Location = New System.Drawing.Point(575, 83)
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
        Me.optOPS.Location = New System.Drawing.Point(575, 44)
        Me.optOPS.Name = "optOPS"
        Me.optOPS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOPS.Size = New System.Drawing.Size(113, 33)
        Me.optOPS.TabIndex = 153
        Me.optOPS.TabStop = True
        Me.optOPS.Text = "OPS"
        Me.optOPS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optOPS.UseVisualStyleBackColor = True
        '
        'grdSelectionList
        '
        Me.grdSelectionList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSelectionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSelectionList.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.grdSelectionList.Location = New System.Drawing.Point(12, 12)
        Me.grdSelectionList.Name = "grdSelectionList"
        Me.grdSelectionList.RowTemplate.Height = 21
        Me.grdSelectionList.Size = New System.Drawing.Size(557, 548)
        Me.grdSelectionList.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 573)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(215, 12)
        Me.Label1.TabIndex = 155
        Me.Label1.Text = "Max. 80 channels can be registered."
        '
        'frmOpsSelectionMenuList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(700, 604)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.optVDU)
        Me.Controls.Add(Me.optOPS)
        Me.Controls.Add(Me.grdSelectionList)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdExit)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsSelectionMenuList"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SELECTION MENU LIST"
        CType(Me.grdSelectionList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdSelectionList As Editor.clsDataGridViewPlus
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents optVDU As System.Windows.Forms.RadioButton
    Public WithEvents optOPS As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
#End Region
End Class
