<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChExhGusGroup
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
    Public WithEvents cmbNo As System.Windows.Forms.ComboBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmbNo = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtAvgCH = New System.Windows.Forms.TextBox()
        Me.txtReposeCH = New System.Windows.Forms.TextBox()
        Me.grdDevCH = New Editor.clsDataGridViewPlus()
        Me.grdCylinderCH = New Editor.clsDataGridViewPlus()
        CType(Me.grdDevCH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCylinderCH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(299, 590)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 2
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
        Me.cmdExit.Location = New System.Drawing.Point(418, 590)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 1
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmbNo
        '
        Me.cmbNo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbNo.Location = New System.Drawing.Point(91, 41)
        Me.cmbNo.Name = "cmbNo"
        Me.cmbNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbNo.Size = New System.Drawing.Size(105, 20)
        Me.cmbNo.TabIndex = 5
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(387, 19)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(77, 12)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "Deviation CH"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(222, 19)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(59, 12)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Source CH"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(26, 109)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(59, 12)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "Repose CH"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(20, 77)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(65, 12)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Average CH"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(62, 45)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(23, 12)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "No."
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAvgCH
        '
        Me.txtAvgCH.AcceptsReturn = True
        Me.txtAvgCH.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtAvgCH.Location = New System.Drawing.Point(91, 74)
        Me.txtAvgCH.MaxLength = 16
        Me.txtAvgCH.Name = "txtAvgCH"
        Me.txtAvgCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAvgCH.Size = New System.Drawing.Size(105, 19)
        Me.txtAvgCH.TabIndex = 6
        Me.txtAvgCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtReposeCH
        '
        Me.txtReposeCH.AcceptsReturn = True
        Me.txtReposeCH.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtReposeCH.Location = New System.Drawing.Point(91, 106)
        Me.txtReposeCH.MaxLength = 16
        Me.txtReposeCH.Name = "txtReposeCH"
        Me.txtReposeCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReposeCH.Size = New System.Drawing.Size(105, 19)
        Me.txtReposeCH.TabIndex = 7
        Me.txtReposeCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'grdDevCH
        '
        Me.grdDevCH.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdDevCH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDevCH.Location = New System.Drawing.Point(387, 39)
        Me.grdDevCH.Name = "grdDevCH"
        Me.grdDevCH.RowTemplate.Height = 21
        Me.grdDevCH.ShowCellToolTips = False
        Me.grdDevCH.Size = New System.Drawing.Size(144, 532)
        Me.grdDevCH.TabIndex = 9
        '
        'grdCylinderCH
        '
        Me.grdCylinderCH.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdCylinderCH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCylinderCH.Location = New System.Drawing.Point(223, 39)
        Me.grdCylinderCH.Name = "grdCylinderCH"
        Me.grdCylinderCH.RowTemplate.Height = 21
        Me.grdCylinderCH.ShowCellToolTips = False
        Me.grdCylinderCH.Size = New System.Drawing.Size(144, 532)
        Me.grdCylinderCH.TabIndex = 8
        '
        'frmChExhGusGroup
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(553, 638)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtReposeCH)
        Me.Controls.Add(Me.txtAvgCH)
        Me.Controls.Add(Me.grdDevCH)
        Me.Controls.Add(Me.grdCylinderCH)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmbNo)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChExhGusGroup"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CYLINDER DEVIATION SET"
        CType(Me.grdDevCH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCylinderCH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtAvgCH As System.Windows.Forms.TextBox
    Public WithEvents txtReposeCH As System.Windows.Forms.TextBox
    Friend WithEvents grdCylinderCH As Editor.clsDataGridViewPlus
    Friend WithEvents grdDevCH As Editor.clsDataGridViewPlus
#End Region

End Class
