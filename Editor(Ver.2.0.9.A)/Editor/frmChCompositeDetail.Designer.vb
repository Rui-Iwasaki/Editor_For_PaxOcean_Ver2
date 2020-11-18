<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChCompositeDetail
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

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtFilterCoeficient = New System.Windows.Forms.TextBox()
        Me.grdBitStatusMap = New Editor.clsDataGridViewPlus()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.grdAnyMap = New Editor.clsDataGridViewPlus()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblDummy = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdBitStatusMap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAnyMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(630, 341)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(754, 341)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtFilterCoeficient)
        Me.GroupBox1.Controls.Add(Me.grdBitStatusMap)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.grdAnyMap)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(855, 323)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detail"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(18, 27)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(89, 12)
        Me.Label13.TabIndex = 141
        Me.Label13.Text = "Bit Status Map"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtFilterCoeficient
        '
        Me.txtFilterCoeficient.AcceptsReturn = True
        Me.txtFilterCoeficient.Location = New System.Drawing.Point(733, 279)
        Me.txtFilterCoeficient.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtFilterCoeficient.MaxLength = 0
        Me.txtFilterCoeficient.Name = "txtFilterCoeficient"
        Me.txtFilterCoeficient.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFilterCoeficient.Size = New System.Drawing.Size(42, 19)
        Me.txtFilterCoeficient.TabIndex = 2
        Me.txtFilterCoeficient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFilterCoeficient.Visible = False
        '
        'grdBitStatusMap
        '
        Me.grdBitStatusMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdBitStatusMap.Location = New System.Drawing.Point(11, 43)
        Me.grdBitStatusMap.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.grdBitStatusMap.Name = "grdBitStatusMap"
        Me.grdBitStatusMap.RowTemplate.Height = 21
        Me.grdBitStatusMap.Size = New System.Drawing.Size(832, 188)
        Me.grdBitStatusMap.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(621, 282)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(107, 12)
        Me.Label6.TabIndex = 143
        Me.Label6.Text = "Filter Coeficient"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label6.Visible = False
        '
        'grdAnyMap
        '
        Me.grdAnyMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAnyMap.Location = New System.Drawing.Point(11, 260)
        Me.grdAnyMap.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.grdAnyMap.Name = "grdAnyMap"
        Me.grdAnyMap.RowTemplate.Height = 21
        Me.grdAnyMap.Size = New System.Drawing.Size(592, 41)
        Me.grdAnyMap.TabIndex = 1
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(781, 282)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(47, 12)
        Me.Label19.TabIndex = 144
        Me.Label19.Text = "* 4msec"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label19.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(18, 244)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(251, 12)
        Me.Label1.TabIndex = 142
        Me.Label1.Text = "When Bit Status doesn’t apply to any map"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDummy
        '
        Me.lblDummy.AutoSize = True
        Me.lblDummy.Location = New System.Drawing.Point(772, 3)
        Me.lblDummy.Name = "lblDummy"
        Me.lblDummy.Size = New System.Drawing.Size(95, 12)
        Me.lblDummy.TabIndex = 11
        Me.lblDummy.Text = "F5:DummySetting"
        '
        'frmChCompositeDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(878, 383)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblDummy)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChCompositeDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Composite Detail"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdBitStatusMap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAnyMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents txtFilterCoeficient As System.Windows.Forms.TextBox
    Friend WithEvents grdBitStatusMap As Editor.clsDataGridViewPlus
    Public WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grdAnyMap As Editor.clsDataGridViewPlus
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblDummy As System.Windows.Forms.Label
#End Region
End Class
