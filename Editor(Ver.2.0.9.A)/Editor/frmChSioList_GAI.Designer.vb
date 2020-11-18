<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChSioList_GAI
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
    Public WithEvents cmdDetails As System.Windows.Forms.Button

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdDetails = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmbPort = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grdSIO = New Editor.clsDataGridViewPlus()
        Me.chkSioExt = New System.Windows.Forms.CheckBox()
        CType(Me.grdSIO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(565, 240)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(104, 33)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(455, 240)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(104, 33)
        Me.cmdSave.TabIndex = 2
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdDetails
        '
        Me.cmdDetails.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDetails.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDetails.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDetails.Location = New System.Drawing.Point(16, 240)
        Me.cmdDetails.Name = "cmdDetails"
        Me.cmdDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDetails.Size = New System.Drawing.Size(104, 33)
        Me.cmdDetails.TabIndex = 1
        Me.cmdDetails.Text = "Details"
        Me.cmdDetails.UseVisualStyleBackColor = True
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClear.Location = New System.Drawing.Point(177, 310)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClear.Size = New System.Drawing.Size(50, 32)
        Me.cmdClear.TabIndex = 45
        Me.cmdClear.Text = "SIO Clear"
        Me.cmdClear.UseVisualStyleBackColor = True
        '
        'cmbPort
        '
        Me.cmbPort.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPort.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPort.Location = New System.Drawing.Point(74, 317)
        Me.cmbPort.Name = "cmbPort"
        Me.cmbPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPort.Size = New System.Drawing.Size(97, 20)
        Me.cmbPort.TabIndex = 46
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(15, 320)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 47
        Me.Label11.Text = "Port No."
        '
        'grdSIO
        '
        Me.grdSIO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSIO.Location = New System.Drawing.Point(16, 26)
        Me.grdSIO.Name = "grdSIO"
        Me.grdSIO.RowTemplate.Height = 21
        Me.grdSIO.Size = New System.Drawing.Size(653, 208)
        Me.grdSIO.TabIndex = 4
        '
        'chkSioExt
        '
        Me.chkSioExt.AutoSize = True
        Me.chkSioExt.Location = New System.Drawing.Point(17, 4)
        Me.chkSioExt.Name = "chkSioExt"
        Me.chkSioExt.Size = New System.Drawing.Size(102, 16)
        Me.chkSioExt.TabIndex = 48
        Me.chkSioExt.Text = "SIO Extension"
        Me.chkSioExt.UseVisualStyleBackColor = True
        '
        'frmChSioList_GAI
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(682, 370)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkSioExt)
        Me.Controls.Add(Me.cmbPort)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.grdSIO)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdDetails)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChSioList_GAI"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SIO SETUP"
        CType(Me.grdSIO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdSIO As Editor.clsDataGridViewPlus
    Public WithEvents cmdClear As System.Windows.Forms.Button
    Public WithEvents cmbPort As System.Windows.Forms.ComboBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkSioExt As System.Windows.Forms.CheckBox
#End Region

End Class
