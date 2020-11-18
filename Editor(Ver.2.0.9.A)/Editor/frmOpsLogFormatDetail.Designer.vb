<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsLogFormatDetail
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

    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmbGroupNo As System.Windows.Forms.ComboBox

    Public WithEvents Label15 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmbGroupNo = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblChList = New System.Windows.Forms.Label()
        Me.grpVisibleFalse = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.grdDetails = New Editor.clsDataGridViewPlus()
        Me.grpVisibleFalse.SuspendLayout()
        CType(Me.grdDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(216, 472)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 2
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(335, 472)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmbGroupNo
        '
        Me.cmbGroupNo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbGroupNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbGroupNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGroupNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbGroupNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbGroupNo.Location = New System.Drawing.Point(12, 32)
        Me.cmbGroupNo.Name = "cmbGroupNo"
        Me.cmbGroupNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbGroupNo.Size = New System.Drawing.Size(435, 23)
        Me.cmbGroupNo.TabIndex = 0
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(10, 14)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(79, 15)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "GROUP LIST"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblChList
        '
        Me.lblChList.AutoSize = True
        Me.lblChList.BackColor = System.Drawing.SystemColors.Control
        Me.lblChList.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblChList.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChList.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblChList.Location = New System.Drawing.Point(12, 74)
        Me.lblChList.Name = "lblChList"
        Me.lblChList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblChList.Size = New System.Drawing.Size(93, 15)
        Me.lblChList.TabIndex = 4
        Me.lblChList.Text = "CHANNEL LIST"
        Me.lblChList.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grpVisibleFalse
        '
        Me.grpVisibleFalse.Controls.Add(Me.Label1)
        Me.grpVisibleFalse.Controls.Add(Me.cmbUnit)
        Me.grpVisibleFalse.Location = New System.Drawing.Point(95, 307)
        Me.grpVisibleFalse.Name = "grpVisibleFalse"
        Me.grpVisibleFalse.Size = New System.Drawing.Size(234, 72)
        Me.grpVisibleFalse.TabIndex = 5
        Me.grpVisibleFalse.TabStop = False
        Me.grpVisibleFalse.Text = "Visible False"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(30, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(29, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Unit"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbUnit
        '
        Me.cmbUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbUnit.Location = New System.Drawing.Point(95, 27)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbUnit.Size = New System.Drawing.Size(113, 23)
        Me.cmbUnit.TabIndex = 1
        '
        'grdDetails
        '
        Me.grdDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetails.Location = New System.Drawing.Point(12, 92)
        Me.grdDetails.Name = "grdDetails"
        Me.grdDetails.RowTemplate.Height = 21
        Me.grdDetails.Size = New System.Drawing.Size(436, 356)
        Me.grdDetails.TabIndex = 1
        '
        'frmOpsLogFormatDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(463, 517)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpVisibleFalse)
        Me.Controls.Add(Me.lblChList)
        Me.Controls.Add(Me.grdDetails)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmbGroupNo)
        Me.Controls.Add(Me.Label15)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsLogFormatDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "LOG FORMAT SET DETAILS"
        Me.grpVisibleFalse.ResumeLayout(False)
        Me.grpVisibleFalse.PerformLayout()
        CType(Me.grdDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdDetails As Editor.clsDataGridViewPlus
    Public WithEvents lblChList As System.Windows.Forms.Label
    Friend WithEvents grpVisibleFalse As System.Windows.Forms.GroupBox
    Public WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

End Class
