<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChControlUseNotuseDetail
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
    Public WithEvents cmbFlg As System.Windows.Forms.ComboBox
    Public WithEvents txtCount As System.Windows.Forms.TextBox

    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmbFlg = New System.Windows.Forms.ComboBox()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.fraCmp = New System.Windows.Forms.GroupBox()
        Me.lblNo = New System.Windows.Forms.Label()
        Me.lblNoHead = New System.Windows.Forms.Label()
        Me.grdUse = New Editor.clsDataGridViewPlus()
        Me.fraCmp.SuspendLayout()
        CType(Me.grdUse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(400, 392)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(276, 392)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmbFlg
        '
        Me.cmbFlg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFlg.BackColor = System.Drawing.SystemColors.Window
        Me.cmbFlg.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFlg.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbFlg.Location = New System.Drawing.Point(376, 14)
        Me.cmbFlg.Name = "cmbFlg"
        Me.cmbFlg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbFlg.Size = New System.Drawing.Size(140, 20)
        Me.cmbFlg.TabIndex = 3
        '
        'txtCount
        '
        Me.txtCount.AcceptsReturn = True
        Me.txtCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCount.Location = New System.Drawing.Point(270, 15)
        Me.txtCount.MaxLength = 0
        Me.txtCount.Name = "txtCount"
        Me.txtCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCount.Size = New System.Drawing.Size(55, 19)
        Me.txtCount.TabIndex = 2
        Me.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(347, 18)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(23, 12)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "Flg"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(225, 18)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(35, 12)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = "Count"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraCmp
        '
        Me.fraCmp.BackColor = System.Drawing.SystemColors.Control
        Me.fraCmp.Controls.Add(Me.lblNo)
        Me.fraCmp.Controls.Add(Me.lblNoHead)
        Me.fraCmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCmp.Location = New System.Drawing.Point(16, 4)
        Me.fraCmp.Name = "fraCmp"
        Me.fraCmp.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCmp.Size = New System.Drawing.Size(50, 36)
        Me.fraCmp.TabIndex = 8
        Me.fraCmp.TabStop = False
        '
        'lblNo
        '
        Me.lblNo.AutoSize = True
        Me.lblNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNo.Location = New System.Drawing.Point(26, 14)
        Me.lblNo.Name = "lblNo"
        Me.lblNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNo.Size = New System.Drawing.Size(17, 12)
        Me.lblNo.TabIndex = 9
        Me.lblNo.Text = "99"
        Me.lblNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNoHead
        '
        Me.lblNoHead.AutoSize = True
        Me.lblNoHead.BackColor = System.Drawing.SystemColors.Control
        Me.lblNoHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNoHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNoHead.Location = New System.Drawing.Point(4, 14)
        Me.lblNoHead.Name = "lblNoHead"
        Me.lblNoHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNoHead.Size = New System.Drawing.Size(23, 12)
        Me.lblNoHead.TabIndex = 2
        Me.lblNoHead.Text = "NO."
        Me.lblNoHead.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdUse
        '
        Me.grdUse.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdUse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdUse.Location = New System.Drawing.Point(16, 47)
        Me.grdUse.Name = "grdUse"
        Me.grdUse.RowTemplate.Height = 21
        Me.grdUse.Size = New System.Drawing.Size(500, 334)
        Me.grdUse.TabIndex = 100
        '
        'frmChControlUseNotuseDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(528, 437)
        Me.ControlBox = False
        Me.Controls.Add(Me.fraCmp)
        Me.Controls.Add(Me.grdUse)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmbFlg)
        Me.Controls.Add(Me.txtCount)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label16)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChControlUseNotuseDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CONTROL USE/NOTUSE SET DETAILS"
        Me.fraCmp.ResumeLayout(False)
        Me.fraCmp.PerformLayout()
        CType(Me.grdUse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents fraCmp As System.Windows.Forms.GroupBox
    Public WithEvents lblNo As System.Windows.Forms.Label
    Public WithEvents lblNoHead As System.Windows.Forms.Label
    Friend WithEvents grdUse As Editor.clsDataGridViewPlus
#End Region

End Class
