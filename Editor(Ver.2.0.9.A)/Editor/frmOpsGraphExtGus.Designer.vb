<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsGraphExtGus
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
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents txtTcTitile As System.Windows.Forms.TextBox
    Public WithEvents txtTcCount As System.Windows.Forms.TextBox
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents fraTC As System.Windows.Forms.GroupBox
    Public WithEvents txtAvgChNo As System.Windows.Forms.TextBox
    Public WithEvents txtCylinderCount As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents fraCylinder As System.Windows.Forms.GroupBox
    Public WithEvents txtNameItemDown As System.Windows.Forms.TextBox
    Public WithEvents txtNameItemUp As System.Windows.Forms.TextBox
    Public WithEvents txtGraphTitle As System.Windows.Forms.TextBox
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.txtTcTitile = New System.Windows.Forms.TextBox()
        Me.fraTC = New System.Windows.Forms.GroupBox()
        Me.grdTC = New Editor.clsDataGridViewPlus()
        Me.txtTcCount = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtAvgChNo = New System.Windows.Forms.TextBox()
        Me.fraCylinder = New System.Windows.Forms.GroupBox()
        Me.txtCylinderCount = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.grdCylinder = New Editor.clsDataGridViewPlus()
        Me.txtTcComm2 = New System.Windows.Forms.TextBox()
        Me.txtTcComm1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNameItemDown = New System.Windows.Forms.TextBox()
        Me.txtNameItemUp = New System.Windows.Forms.TextBox()
        Me.txtGraphTitle = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.fra20Graph = New System.Windows.Forms.GroupBox()
        Me.optLine2 = New System.Windows.Forms.RadioButton()
        Me.optLine1 = New System.Windows.Forms.RadioButton()
        Me.numDevMark = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.fraTC.SuspendLayout()
        CType(Me.grdTC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraCylinder.SuspendLayout()
        CType(Me.grdCylinder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fra20Graph.SuspendLayout()
        CType(Me.numDevMark, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(699, 542)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(580, 542)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(113, 33)
        Me.cmdOk.TabIndex = 2
        Me.cmdOk.Text = "OK"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'txtTcTitile
        '
        Me.txtTcTitile.AcceptsReturn = True
        Me.txtTcTitile.Location = New System.Drawing.Point(580, 447)
        Me.txtTcTitile.MaxLength = 0
        Me.txtTcTitile.Name = "txtTcTitile"
        Me.txtTcTitile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTcTitile.Size = New System.Drawing.Size(209, 19)
        Me.txtTcTitile.TabIndex = 12
        '
        'fraTC
        '
        Me.fraTC.BackColor = System.Drawing.SystemColors.Control
        Me.fraTC.Controls.Add(Me.grdTC)
        Me.fraTC.Controls.Add(Me.txtTcCount)
        Me.fraTC.Controls.Add(Me.Label18)
        Me.fraTC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTC.Location = New System.Drawing.Point(464, 108)
        Me.fraTC.Name = "fraTC"
        Me.fraTC.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTC.Size = New System.Drawing.Size(364, 320)
        Me.fraTC.TabIndex = 7
        Me.fraTC.TabStop = False
        Me.fraTC.Text = "Turbo Charger CH No."
        '
        'grdTC
        '
        Me.grdTC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTC.Location = New System.Drawing.Point(16, 53)
        Me.grdTC.Name = "grdTC"
        Me.grdTC.RowTemplate.Height = 21
        Me.grdTC.Size = New System.Drawing.Size(332, 198)
        Me.grdTC.TabIndex = 23
        '
        'txtTcCount
        '
        Me.txtTcCount.AcceptsReturn = True
        Me.txtTcCount.Location = New System.Drawing.Point(119, 26)
        Me.txtTcCount.MaxLength = 0
        Me.txtTcCount.Name = "txtTcCount"
        Me.txtTcCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTcCount.Size = New System.Drawing.Size(27, 19)
        Me.txtTcCount.TabIndex = 20
        Me.txtTcCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(18, 29)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(95, 12)
        Me.Label18.TabIndex = 22
        Me.Label18.Text = "Setup T/C Count"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAvgChNo
        '
        Me.txtAvgChNo.AcceptsReturn = True
        Me.txtAvgChNo.Location = New System.Drawing.Point(159, 447)
        Me.txtAvgChNo.MaxLength = 0
        Me.txtAvgChNo.Name = "txtAvgChNo"
        Me.txtAvgChNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAvgChNo.Size = New System.Drawing.Size(64, 19)
        Me.txtAvgChNo.TabIndex = 8
        Me.txtAvgChNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'fraCylinder
        '
        Me.fraCylinder.BackColor = System.Drawing.SystemColors.Control
        Me.fraCylinder.Controls.Add(Me.txtCylinderCount)
        Me.fraCylinder.Controls.Add(Me.Label13)
        Me.fraCylinder.Controls.Add(Me.grdCylinder)
        Me.fraCylinder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCylinder.Location = New System.Drawing.Point(12, 108)
        Me.fraCylinder.Name = "fraCylinder"
        Me.fraCylinder.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCylinder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCylinder.Size = New System.Drawing.Size(440, 320)
        Me.fraCylinder.TabIndex = 6
        Me.fraCylinder.TabStop = False
        Me.fraCylinder.Text = "Cylinder, Devision CH No."
        '
        'txtCylinderCount
        '
        Me.txtCylinderCount.AcceptsReturn = True
        Me.txtCylinderCount.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCylinderCount.Location = New System.Drawing.Point(147, 25)
        Me.txtCylinderCount.MaxLength = 0
        Me.txtCylinderCount.Name = "txtCylinderCount"
        Me.txtCylinderCount.ReadOnly = True
        Me.txtCylinderCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCylinderCount.Size = New System.Drawing.Size(27, 19)
        Me.txtCylinderCount.TabIndex = 8
        Me.txtCylinderCount.TabStop = False
        Me.txtCylinderCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(20, 29)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(125, 12)
        Me.Label13.TabIndex = 9
        Me.Label13.Text = "Setup Cylinder Count"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdCylinder
        '
        Me.grdCylinder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCylinder.Location = New System.Drawing.Point(16, 53)
        Me.grdCylinder.Name = "grdCylinder"
        Me.grdCylinder.RowTemplate.Height = 21
        Me.grdCylinder.Size = New System.Drawing.Size(410, 240)
        Me.grdCylinder.TabIndex = 10
        '
        'txtTcComm2
        '
        Me.txtTcComm2.AcceptsReturn = True
        Me.txtTcComm2.Location = New System.Drawing.Point(580, 497)
        Me.txtTcComm2.MaxLength = 0
        Me.txtTcComm2.Name = "txtTcComm2"
        Me.txtTcComm2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTcComm2.Size = New System.Drawing.Size(113, 19)
        Me.txtTcComm2.TabIndex = 26
        '
        'txtTcComm1
        '
        Me.txtTcComm1.AcceptsReturn = True
        Me.txtTcComm1.Location = New System.Drawing.Point(580, 472)
        Me.txtTcComm1.MaxLength = 0
        Me.txtTcComm1.Name = "txtTcComm1"
        Me.txtTcComm1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTcComm1.Size = New System.Drawing.Size(113, 19)
        Me.txtTcComm1.TabIndex = 24
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(496, 475)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "T/C Comment1"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtNameItemDown
        '
        Me.txtNameItemDown.AcceptsReturn = True
        Me.txtNameItemDown.Location = New System.Drawing.Point(120, 70)
        Me.txtNameItemDown.MaxLength = 0
        Me.txtNameItemDown.Name = "txtNameItemDown"
        Me.txtNameItemDown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNameItemDown.Size = New System.Drawing.Size(116, 19)
        Me.txtNameItemDown.TabIndex = 5
        '
        'txtNameItemUp
        '
        Me.txtNameItemUp.AcceptsReturn = True
        Me.txtNameItemUp.Location = New System.Drawing.Point(120, 45)
        Me.txtNameItemUp.MaxLength = 0
        Me.txtNameItemUp.Name = "txtNameItemUp"
        Me.txtNameItemUp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNameItemUp.Size = New System.Drawing.Size(116, 19)
        Me.txtNameItemUp.TabIndex = 4
        '
        'txtGraphTitle
        '
        Me.txtGraphTitle.AcceptsReturn = True
        Me.txtGraphTitle.Location = New System.Drawing.Point(120, 20)
        Me.txtGraphTitle.MaxLength = 0
        Me.txtGraphTitle.Name = "txtGraphTitle"
        Me.txtGraphTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGraphTitle.Size = New System.Drawing.Size(318, 19)
        Me.txtGraphTitle.TabIndex = 3
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(478, 450)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(95, 12)
        Me.Label19.TabIndex = 23
        Me.Label19.Text = "T/C Graph Title"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(135, 484)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(17, 12)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "+-"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(59, 450)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(89, 12)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Average CH No."
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(41, 474)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(89, 24)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "Deviation Mark" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Up,Down Limit"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(85, 73)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(29, 12)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "Down"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(97, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(17, 12)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Up"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(26, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(59, 12)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Name Item"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(43, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(71, 12)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Graph Title"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fra20Graph
        '
        Me.fra20Graph.Controls.Add(Me.optLine2)
        Me.fra20Graph.Controls.Add(Me.optLine1)
        Me.fra20Graph.Location = New System.Drawing.Point(61, 507)
        Me.fra20Graph.Name = "fra20Graph"
        Me.fra20Graph.Size = New System.Drawing.Size(162, 46)
        Me.fra20Graph.TabIndex = 11
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
        Me.optLine2.Location = New System.Drawing.Point(93, 18)
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
        'numDevMark
        '
        Me.numDevMark.Location = New System.Drawing.Point(159, 482)
        Me.numDevMark.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.numDevMark.Name = "numDevMark"
        Me.numDevMark.Size = New System.Drawing.Size(64, 19)
        Me.numDevMark.TabIndex = 9
        Me.numDevMark.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numDevMark.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(496, 500)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "T/C Comment2"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmOpsGraphExtGus
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(842, 591)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.numDevMark)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTcComm2)
        Me.Controls.Add(Me.fra20Graph)
        Me.Controls.Add(Me.txtTcComm1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.txtTcTitile)
        Me.Controls.Add(Me.fraTC)
        Me.Controls.Add(Me.txtAvgChNo)
        Me.Controls.Add(Me.fraCylinder)
        Me.Controls.Add(Me.txtNameItemDown)
        Me.Controls.Add(Me.txtNameItemUp)
        Me.Controls.Add(Me.txtGraphTitle)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label14)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsGraphExtGus"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "DEVIATION GRAPH"
        Me.fraTC.ResumeLayout(False)
        Me.fraTC.PerformLayout()
        CType(Me.grdTC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraCylinder.ResumeLayout(False)
        Me.fraCylinder.PerformLayout()
        CType(Me.grdCylinder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fra20Graph.ResumeLayout(False)
        Me.fra20Graph.PerformLayout()
        CType(Me.numDevMark, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents fra20Graph As System.Windows.Forms.GroupBox
    Public WithEvents optLine2 As System.Windows.Forms.RadioButton
    Public WithEvents optLine1 As System.Windows.Forms.RadioButton
    Public WithEvents txtTcComm1 As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtTcComm2 As System.Windows.Forms.TextBox
    Friend WithEvents numDevMark As System.Windows.Forms.NumericUpDown
    Friend WithEvents grdTC As Editor.clsDataGridViewPlus
    Friend WithEvents grdCylinder As Editor.clsDataGridViewPlus
    Public WithEvents Label2 As System.Windows.Forms.Label
#End Region

End Class
