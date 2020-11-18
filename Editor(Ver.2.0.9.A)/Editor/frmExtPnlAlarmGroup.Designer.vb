<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExtPnlAlarmGroup
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
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.grdHead = New Editor.clsDataGridViewPlus()
        Me.grdMachCargo = New Editor.clsDataGridViewPlus()
        Me.grdAlarmGroup = New Editor.clsDataGridViewPlus()
        CType(Me.grdHead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMachCargo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAlarmGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(714, 316)
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
        Me.cmdSave.Location = New System.Drawing.Point(595, 316)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 3
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(12, 316)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(113, 33)
        Me.cmdPrint.TabIndex = 2
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'grdHead
        '
        Me.grdHead.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdHead.Location = New System.Drawing.Point(12, 13)
        Me.grdHead.Name = "grdHead"
        Me.grdHead.RowTemplate.Height = 21
        Me.grdHead.Size = New System.Drawing.Size(672, 19)
        Me.grdHead.TabIndex = 11
        Me.grdHead.TabStop = False
        '
        'grdMachCargo
        '
        Me.grdMachCargo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdMachCargo.Location = New System.Drawing.Point(694, 32)
        Me.grdMachCargo.Name = "grdMachCargo"
        Me.grdMachCargo.RowTemplate.Height = 21
        Me.grdMachCargo.Size = New System.Drawing.Size(133, 271)
        Me.grdMachCargo.TabIndex = 13
        '
        'grdAlarmGroup
        '
        Me.grdAlarmGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAlarmGroup.Location = New System.Drawing.Point(12, 30)
        Me.grdAlarmGroup.Name = "grdAlarmGroup"
        Me.grdAlarmGroup.RowTemplate.Height = 21
        Me.grdAlarmGroup.Size = New System.Drawing.Size(672, 272)
        Me.grdAlarmGroup.TabIndex = 12
        '
        'frmExtPnlAlarmGroup
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(840, 362)
        Me.ControlBox = False
        Me.Controls.Add(Me.grdHead)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.grdMachCargo)
        Me.Controls.Add(Me.grdAlarmGroup)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExtPnlAlarmGroup"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "D/L EXT ALARM GROUP LED OUTPUT"
        CType(Me.grdHead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMachCargo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAlarmGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents grdHead As Editor.clsDataGridViewPlus
    Friend WithEvents grdAlarmGroup As Editor.clsDataGridViewPlus
    Friend WithEvents grdMachCargo As Editor.clsDataGridViewPlus
#End Region

End Class
