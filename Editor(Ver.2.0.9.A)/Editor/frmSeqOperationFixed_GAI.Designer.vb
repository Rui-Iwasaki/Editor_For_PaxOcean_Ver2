<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSeqOperationFixed_GAI
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
    Public WithEvents txtExpression As System.Windows.Forms.TextBox
    Public WithEvents cmbTableNo As System.Windows.Forms.ComboBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.txtExpression = New System.Windows.Forms.TextBox()
        Me.cmbTableNo = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.grdCH = New Editor.clsDataGridViewPlus()
        Me.grdVariableName = New Editor.clsDataGridViewPlus()
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdVariableName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(523, 450)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 5
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(404, 450)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 4
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'txtExpression
        '
        Me.txtExpression.AcceptsReturn = True
        Me.txtExpression.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtExpression.Location = New System.Drawing.Point(85, 46)
        Me.txtExpression.MaxLength = 72
        Me.txtExpression.Name = "txtExpression"
        Me.txtExpression.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExpression.Size = New System.Drawing.Size(551, 19)
        Me.txtExpression.TabIndex = 1
        '
        'cmbTableNo
        '
        Me.cmbTableNo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbTableNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbTableNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTableNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbTableNo.Location = New System.Drawing.Point(85, 12)
        Me.cmbTableNo.Name = "cmbTableNo"
        Me.cmbTableNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbTableNo.Size = New System.Drawing.Size(91, 20)
        Me.cmbTableNo.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(14, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(65, 12)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Expression"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(20, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(59, 12)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Table No."
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdCH
        '
        Me.grdCH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCH.Location = New System.Drawing.Point(193, 80)
        Me.grdCH.Name = "grdCH"
        Me.grdCH.RowTemplate.Height = 21
        Me.grdCH.Size = New System.Drawing.Size(443, 355)
        Me.grdCH.TabIndex = 3
        '
        'grdVariableName
        '
        Me.grdVariableName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVariableName.Location = New System.Drawing.Point(16, 80)
        Me.grdVariableName.Name = "grdVariableName"
        Me.grdVariableName.RowTemplate.Height = 21
        Me.grdVariableName.Size = New System.Drawing.Size(152, 188)
        Me.grdVariableName.TabIndex = 2
        '
        'frmSeqOperationFixed_GAI
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(655, 495)
        Me.ControlBox = False
        Me.Controls.Add(Me.grdCH)
        Me.Controls.Add(Me.grdVariableName)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.txtExpression)
        Me.Controls.Add(Me.cmbTableNo)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSeqOperationFixed_GAI"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "OPERATION FIXED NUMBER TABLE SET"
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdVariableName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdVariableName As Editor.clsDataGridViewPlus
    Friend WithEvents grdCH As Editor.clsDataGridViewPlus
#End Region

End Class
