<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSeqLinearTable_GAI
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

    Public WithEvents cmbTableNo As System.Windows.Forms.ComboBox
    Public WithEvents cmdCsvRead As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button

    Public WithEvents Label10 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmbTableNo = New System.Windows.Forms.ComboBox()
        Me.cmdCsvRead = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.grdLiner = New Editor.clsDataGridViewPlus()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.numSetCount = New System.Windows.Forms.NumericUpDown()
        CType(Me.grdLiner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numSetCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbTableNo
        '
        Me.cmbTableNo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbTableNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbTableNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTableNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbTableNo.Location = New System.Drawing.Point(80, 18)
        Me.cmbTableNo.Name = "cmbTableNo"
        Me.cmbTableNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbTableNo.Size = New System.Drawing.Size(112, 20)
        Me.cmbTableNo.TabIndex = 4
        '
        'cmdCsvRead
        '
        Me.cmdCsvRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdCsvRead.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCsvRead.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCsvRead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCsvRead.Location = New System.Drawing.Point(22, 300)
        Me.cmdCsvRead.Name = "cmdCsvRead"
        Me.cmdCsvRead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCsvRead.Size = New System.Drawing.Size(113, 33)
        Me.cmdCsvRead.TabIndex = 3
        Me.cmdCsvRead.Text = "CSV Import"
        Me.cmdCsvRead.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(147, 300)
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
        Me.cmdExit.Location = New System.Drawing.Point(272, 300)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 1
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(13, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(59, 12)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Table No."
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdLiner
        '
        Me.grdLiner.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLiner.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdLiner.Location = New System.Drawing.Point(52, 57)
        Me.grdLiner.Name = "grdLiner"
        Me.grdLiner.RowTemplate.Height = 21
        Me.grdLiner.Size = New System.Drawing.Size(300, 230)
        Me.grdLiner.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(220, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(59, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Set Count"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'numSetCount
        '
        Me.numSetCount.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSetCount.Location = New System.Drawing.Point(285, 18)
        Me.numSetCount.Maximum = New Decimal(New Integer() {1024, 0, 0, 0})
        Me.numSetCount.Name = "numSetCount"
        Me.numSetCount.Size = New System.Drawing.Size(67, 20)
        Me.numSetCount.TabIndex = 15
        Me.numSetCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmSeqLinearTable_GAI
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(406, 346)
        Me.ControlBox = False
        Me.Controls.Add(Me.numSetCount)
        Me.Controls.Add(Me.grdLiner)
        Me.Controls.Add(Me.cmbTableNo)
        Me.Controls.Add(Me.cmdCsvRead)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label10)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSeqLinearTable_GAI"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "LINER TABLE SET"
        CType(Me.grdLiner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numSetCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdLiner As Editor.clsDataGridViewPlus
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents numSetCount As System.Windows.Forms.NumericUpDown
#End Region

End Class
