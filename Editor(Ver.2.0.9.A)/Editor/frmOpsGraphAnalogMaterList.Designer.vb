<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsGraphAnalogMaterList
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

    Public WithEvents txtGraphTitle As System.Windows.Forms.TextBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmbDisplayType As System.Windows.Forms.ComboBox

    Public WithEvents Label11 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtGraphTitle = New System.Windows.Forms.TextBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmbDisplayType = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grdAnalogMeter = New Editor.clsDataGridViewPlus()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.grdAnalogMeter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtGraphTitle
        '
        Me.txtGraphTitle.AcceptsReturn = True
        Me.txtGraphTitle.Location = New System.Drawing.Point(418, 28)
        Me.txtGraphTitle.MaxLength = 0
        Me.txtGraphTitle.Name = "txtGraphTitle"
        Me.txtGraphTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGraphTitle.Size = New System.Drawing.Size(314, 19)
        Me.txtGraphTitle.TabIndex = 4
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(619, 171)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(500, 171)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 2
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmbDisplayType
        '
        Me.cmbDisplayType.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDisplayType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDisplayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDisplayType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDisplayType.Location = New System.Drawing.Point(418, 87)
        Me.cmbDisplayType.Name = "cmbDisplayType"
        Me.cmbDisplayType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDisplayType.Size = New System.Drawing.Size(105, 20)
        Me.cmbDisplayType.TabIndex = 5
        Me.cmbDisplayType.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(335, 31)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(71, 12)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Graph Title"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdAnalogMeter
        '
        Me.grdAnalogMeter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAnalogMeter.Location = New System.Drawing.Point(12, 16)
        Me.grdAnalogMeter.Name = "grdAnalogMeter"
        Me.grdAnalogMeter.RowTemplate.Height = 21
        Me.grdAnalogMeter.Size = New System.Drawing.Size(312, 188)
        Me.grdAnalogMeter.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(330, 181)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(161, 12)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "0:Auto Calculation (Scale)"
        '
        'frmOpsGraphAnalogMaterList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(748, 220)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdAnalogMeter)
        Me.Controls.Add(Me.txtGraphTitle)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmbDisplayType)
        Me.Controls.Add(Me.Label11)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsGraphAnalogMaterList"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ANALOG METER SETUP"
        CType(Me.grdAnalogMeter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdAnalogMeter As Editor.clsDataGridViewPlus
    Friend WithEvents Label1 As System.Windows.Forms.Label
#End Region

End Class
