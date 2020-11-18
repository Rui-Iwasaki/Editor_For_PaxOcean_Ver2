<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsGraphBarGraph
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

    Public WithEvents optDivision0 As System.Windows.Forms.RadioButton
    Public WithEvents optDivision1 As System.Windows.Forms.RadioButton
    Public WithEvents optDivision2 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtGraphTitle As System.Windows.Forms.TextBox
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents chk20Graph As System.Windows.Forms.CheckBox
    Public WithEvents txtCylinderCount As System.Windows.Forms.TextBox
    Public WithEvents txtNameItemDown As System.Windows.Forms.TextBox
    Public WithEvents txtNameItemUp As System.Windows.Forms.TextBox

    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.optDivision0 = New System.Windows.Forms.RadioButton()
        Me.optDivision1 = New System.Windows.Forms.RadioButton()
        Me.optDivision2 = New System.Windows.Forms.RadioButton()
        Me.txtGraphTitle = New System.Windows.Forms.TextBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.chk20Graph = New System.Windows.Forms.CheckBox()
        Me.txtCylinderCount = New System.Windows.Forms.TextBox()
        Me.txtNameItemDown = New System.Windows.Forms.TextBox()
        Me.txtNameItemUp = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grdCylinder = New Editor.clsDataGridViewPlus()
        Me.fra20Graph = New System.Windows.Forms.GroupBox()
        Me.optLine2 = New System.Windows.Forms.RadioButton()
        Me.optLine1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optDisplayPercentage = New System.Windows.Forms.RadioButton()
        Me.optDisplayMeasurement = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Frame2.SuspendLayout()
        CType(Me.grdCylinder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fra20Graph.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.optDivision0)
        Me.Frame2.Controls.Add(Me.optDivision1)
        Me.Frame2.Controls.Add(Me.optDivision2)
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(489, 219)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(161, 87)
        Me.Frame2.TabIndex = 9
        Me.Frame2.TabStop = False
        '
        'optDivision0
        '
        Me.optDivision0.AutoSize = True
        Me.optDivision0.BackColor = System.Drawing.SystemColors.Control
        Me.optDivision0.Checked = True
        Me.optDivision0.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDivision0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDivision0.Location = New System.Drawing.Point(16, 12)
        Me.optDivision0.Name = "optDivision0"
        Me.optDivision0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDivision0.Size = New System.Drawing.Size(83, 16)
        Me.optDivision0.TabIndex = 0
        Me.optDivision0.TabStop = True
        Me.optDivision0.Text = "4 Division"
        Me.optDivision0.UseVisualStyleBackColor = True
        '
        'optDivision1
        '
        Me.optDivision1.AutoSize = True
        Me.optDivision1.BackColor = System.Drawing.SystemColors.Control
        Me.optDivision1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDivision1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDivision1.Location = New System.Drawing.Point(16, 36)
        Me.optDivision1.Name = "optDivision1"
        Me.optDivision1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDivision1.Size = New System.Drawing.Size(83, 16)
        Me.optDivision1.TabIndex = 1
        Me.optDivision1.TabStop = True
        Me.optDivision1.Text = "6 Division"
        Me.optDivision1.UseVisualStyleBackColor = True
        '
        'optDivision2
        '
        Me.optDivision2.AutoSize = True
        Me.optDivision2.BackColor = System.Drawing.SystemColors.Control
        Me.optDivision2.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDivision2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDivision2.Location = New System.Drawing.Point(16, 60)
        Me.optDivision2.Name = "optDivision2"
        Me.optDivision2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDivision2.Size = New System.Drawing.Size(107, 16)
        Me.optDivision2.TabIndex = 2
        Me.optDivision2.TabStop = True
        Me.optDivision2.Text = "3 * 5 Division"
        Me.optDivision2.UseVisualStyleBackColor = True
        '
        'txtGraphTitle
        '
        Me.txtGraphTitle.AcceptsReturn = True
        Me.txtGraphTitle.Location = New System.Drawing.Point(489, 42)
        Me.txtGraphTitle.MaxLength = 0
        Me.txtGraphTitle.Name = "txtGraphTitle"
        Me.txtGraphTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGraphTitle.Size = New System.Drawing.Size(280, 19)
        Me.txtGraphTitle.TabIndex = 4
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(537, 321)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 2
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(656, 321)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'chk20Graph
        '
        Me.chk20Graph.AutoSize = True
        Me.chk20Graph.BackColor = System.Drawing.SystemColors.Control
        Me.chk20Graph.Cursor = System.Windows.Forms.Cursors.Default
        Me.chk20Graph.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chk20Graph.Location = New System.Drawing.Point(20, 18)
        Me.chk20Graph.Name = "chk20Graph"
        Me.chk20Graph.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chk20Graph.Size = New System.Drawing.Size(72, 16)
        Me.chk20Graph.TabIndex = 7
        Me.chk20Graph.Text = "20 Graph"
        Me.chk20Graph.UseVisualStyleBackColor = True
        '
        'txtCylinderCount
        '
        Me.txtCylinderCount.AcceptsReturn = True
        Me.txtCylinderCount.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCylinderCount.Location = New System.Drawing.Point(489, 128)
        Me.txtCylinderCount.MaxLength = 0
        Me.txtCylinderCount.Name = "txtCylinderCount"
        Me.txtCylinderCount.ReadOnly = True
        Me.txtCylinderCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCylinderCount.Size = New System.Drawing.Size(27, 19)
        Me.txtCylinderCount.TabIndex = 3
        Me.txtCylinderCount.TabStop = False
        Me.txtCylinderCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNameItemDown
        '
        Me.txtNameItemDown.AcceptsReturn = True
        Me.txtNameItemDown.Location = New System.Drawing.Point(489, 96)
        Me.txtNameItemDown.MaxLength = 0
        Me.txtNameItemDown.Name = "txtNameItemDown"
        Me.txtNameItemDown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNameItemDown.Size = New System.Drawing.Size(100, 19)
        Me.txtNameItemDown.TabIndex = 6
        '
        'txtNameItemUp
        '
        Me.txtNameItemUp.AcceptsReturn = True
        Me.txtNameItemUp.Location = New System.Drawing.Point(489, 69)
        Me.txtNameItemUp.MaxLength = 0
        Me.txtNameItemUp.Name = "txtNameItemUp"
        Me.txtNameItemUp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNameItemUp.Size = New System.Drawing.Size(100, 19)
        Me.txtNameItemUp.TabIndex = 5
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(410, 45)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(71, 12)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Graph Title"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(422, 230)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(59, 12)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "Bar Graph"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(356, 131)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(125, 12)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "Setup Cylinder Count"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(452, 99)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(29, 12)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Down"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(464, 72)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(17, 12)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "Up"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(386, 72)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(59, 12)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Name Item"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(24, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(41, 12)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "CH No."
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(392, 245)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Setup Uniquely"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdCylinder
        '
        Me.grdCylinder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCylinder.Location = New System.Drawing.Point(23, 34)
        Me.grdCylinder.Name = "grdCylinder"
        Me.grdCylinder.RowTemplate.Height = 21
        Me.grdCylinder.Size = New System.Drawing.Size(320, 272)
        Me.grdCylinder.TabIndex = 3
        '
        'fra20Graph
        '
        Me.fra20Graph.Controls.Add(Me.optLine2)
        Me.fra20Graph.Controls.Add(Me.optLine1)
        Me.fra20Graph.Location = New System.Drawing.Point(656, 153)
        Me.fra20Graph.Name = "fra20Graph"
        Me.fra20Graph.Size = New System.Drawing.Size(96, 67)
        Me.fra20Graph.TabIndex = 16
        Me.fra20Graph.TabStop = False
        Me.fra20Graph.Text = "Line"
        '
        'optLine2
        '
        Me.optLine2.AutoSize = True
        Me.optLine2.BackColor = System.Drawing.SystemColors.Control
        Me.optLine2.Checked = True
        Me.optLine2.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLine2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLine2.Location = New System.Drawing.Point(16, 40)
        Me.optLine2.Name = "optLine2"
        Me.optLine2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLine2.Size = New System.Drawing.Size(59, 16)
        Me.optLine2.TabIndex = 19
        Me.optLine2.TabStop = True
        Me.optLine2.Text = "2 Line"
        Me.optLine2.UseVisualStyleBackColor = True
        '
        'optLine1
        '
        Me.optLine1.AutoSize = True
        Me.optLine1.BackColor = System.Drawing.SystemColors.Control
        Me.optLine1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLine1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLine1.Location = New System.Drawing.Point(16, 18)
        Me.optLine1.Name = "optLine1"
        Me.optLine1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLine1.Size = New System.Drawing.Size(59, 16)
        Me.optLine1.TabIndex = 18
        Me.optLine1.Text = "1 Line"
        Me.optLine1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optDisplayPercentage)
        Me.GroupBox1.Controls.Add(Me.optDisplayMeasurement)
        Me.GroupBox1.Location = New System.Drawing.Point(489, 153)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(161, 67)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        '
        'optDisplayPercentage
        '
        Me.optDisplayPercentage.AutoSize = True
        Me.optDisplayPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.optDisplayPercentage.Checked = True
        Me.optDisplayPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDisplayPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDisplayPercentage.Location = New System.Drawing.Point(16, 40)
        Me.optDisplayPercentage.Name = "optDisplayPercentage"
        Me.optDisplayPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDisplayPercentage.Size = New System.Drawing.Size(83, 16)
        Me.optDisplayPercentage.TabIndex = 19
        Me.optDisplayPercentage.TabStop = True
        Me.optDisplayPercentage.Text = "Percentage"
        Me.optDisplayPercentage.UseVisualStyleBackColor = True
        '
        'optDisplayMeasurement
        '
        Me.optDisplayMeasurement.AutoSize = True
        Me.optDisplayMeasurement.BackColor = System.Drawing.SystemColors.Control
        Me.optDisplayMeasurement.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDisplayMeasurement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDisplayMeasurement.Location = New System.Drawing.Point(16, 18)
        Me.optDisplayMeasurement.Name = "optDisplayMeasurement"
        Me.optDisplayMeasurement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDisplayMeasurement.Size = New System.Drawing.Size(125, 16)
        Me.optDisplayMeasurement.TabIndex = 18
        Me.optDisplayMeasurement.Text = "Measurement Range"
        Me.optDisplayMeasurement.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(434, 164)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(47, 12)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Display"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chk20Graph)
        Me.GroupBox2.Location = New System.Drawing.Point(26, 312)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(152, 44)
        Me.GroupBox2.TabIndex = 19
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "VisibleFalse"
        Me.GroupBox2.Visible = False
        '
        'frmOpsGraphBarGraph
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(789, 368)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.fra20Graph)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdCylinder)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.txtGraphTitle)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.txtCylinderCount)
        Me.Controls.Add(Me.txtNameItemDown)
        Me.Controls.Add(Me.txtNameItemUp)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label11)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsGraphBarGraph"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "BAR GRAPH SETUP"
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        CType(Me.grdCylinder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fra20Graph.ResumeLayout(False)
        Me.fra20Graph.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdCylinder As Editor.clsDataGridViewPlus
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents fra20Graph As System.Windows.Forms.GroupBox
    Public WithEvents optLine2 As System.Windows.Forms.RadioButton
    Public WithEvents optLine1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents optDisplayPercentage As System.Windows.Forms.RadioButton
    Public WithEvents optDisplayMeasurement As System.Windows.Forms.RadioButton
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
#End Region

End Class
