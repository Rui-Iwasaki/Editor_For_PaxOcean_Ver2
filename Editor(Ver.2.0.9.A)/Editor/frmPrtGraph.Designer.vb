<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrtGraph
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

    Public WithEvents cmbGraph As System.Windows.Forms.ComboBox
    Public WithEvents chkShipNo As System.Windows.Forms.CheckBox
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents Label11 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmbGraph = New System.Windows.Forms.ComboBox()
        Me.chkShipNo = New System.Windows.Forms.CheckBox()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtHistoryNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbSelectPart = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.optGraphFree = New System.Windows.Forms.RadioButton()
        Me.optGraphNormal = New System.Windows.Forms.RadioButton()
        Me.cmbOpsNo = New System.Windows.Forms.ComboBox()
        Me.cmbFree = New System.Windows.Forms.ComboBox()
        Me.lblPrinter = New System.Windows.Forms.TextBox()
        Me.cmdAllPrint = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbGraph
        '
        Me.cmbGraph.BackColor = System.Drawing.SystemColors.Window
        Me.cmbGraph.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbGraph.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGraph.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbGraph.Location = New System.Drawing.Point(220, 62)
        Me.cmbGraph.Name = "cmbGraph"
        Me.cmbGraph.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbGraph.Size = New System.Drawing.Size(218, 20)
        Me.cmbGraph.TabIndex = 0
        '
        'chkShipNo
        '
        Me.chkShipNo.AutoSize = True
        Me.chkShipNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkShipNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShipNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShipNo.Location = New System.Drawing.Point(238, 278)
        Me.chkShipNo.Name = "chkShipNo"
        Me.chkShipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShipNo.Size = New System.Drawing.Size(96, 16)
        Me.chkShipNo.TabIndex = 2
        Me.chkShipNo.Text = "S.No Display"
        Me.chkShipNo.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(375, 310)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(81, 33)
        Me.cmdExit.TabIndex = 6
        Me.cmdExit.Text = "EXIT"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(288, 310)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(81, 33)
        Me.cmdPrint.TabIndex = 5
        Me.cmdPrint.Text = "PRINT"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Location = New System.Drawing.Point(18, 310)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(81, 33)
        Me.cmdPreview.TabIndex = 3
        Me.cmdPreview.Text = "PREVIEW"
        Me.cmdPreview.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(18, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(59, 12)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Printer :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtHistoryNo
        '
        Me.txtHistoryNo.AcceptsReturn = True
        Me.txtHistoryNo.Location = New System.Drawing.Point(238, 243)
        Me.txtHistoryNo.MaxLength = 0
        Me.txtHistoryNo.Name = "txtHistoryNo"
        Me.txtHistoryNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHistoryNo.Size = New System.Drawing.Size(218, 19)
        Me.txtHistoryNo.TabIndex = 1
        Me.txtHistoryNo.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(167, 246)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "History No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label1.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbSelectPart)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.optGraphFree)
        Me.GroupBox1.Controls.Add(Me.optGraphNormal)
        Me.GroupBox1.Controls.Add(Me.cmbOpsNo)
        Me.GroupBox1.Controls.Add(Me.cmbFree)
        Me.GroupBox1.Controls.Add(Me.cmbGraph)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 84)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(453, 140)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Graph"
        '
        'cmbSelectPart
        '
        Me.cmbSelectPart.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSelectPart.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbSelectPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSelectPart.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSelectPart.Location = New System.Drawing.Point(220, 25)
        Me.cmbSelectPart.Name = "cmbSelectPart"
        Me.cmbSelectPart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbSelectPart.Size = New System.Drawing.Size(218, 20)
        Me.cmbSelectPart.TabIndex = 153
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(15, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(71, 12)
        Me.Label2.TabIndex = 153
        Me.Label2.Text = "Select Part"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'optGraphFree
        '
        Me.optGraphFree.AutoSize = True
        Me.optGraphFree.Location = New System.Drawing.Point(17, 101)
        Me.optGraphFree.Name = "optGraphFree"
        Me.optGraphFree.Size = New System.Drawing.Size(47, 16)
        Me.optGraphFree.TabIndex = 9
        Me.optGraphFree.TabStop = True
        Me.optGraphFree.Text = "Free"
        Me.optGraphFree.UseVisualStyleBackColor = True
        Me.optGraphFree.Visible = False
        '
        'optGraphNormal
        '
        Me.optGraphNormal.Location = New System.Drawing.Point(17, 54)
        Me.optGraphNormal.Name = "optGraphNormal"
        Me.optGraphNormal.Size = New System.Drawing.Size(197, 34)
        Me.optGraphNormal.TabIndex = 9
        Me.optGraphNormal.TabStop = True
        Me.optGraphNormal.Text = "Cylinder && deviation,Bar,AnalogMeter"
        Me.optGraphNormal.UseVisualStyleBackColor = True
        '
        'cmbOpsNo
        '
        Me.cmbOpsNo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOpsNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOpsNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOpsNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOpsNo.Location = New System.Drawing.Point(176, 99)
        Me.cmbOpsNo.Name = "cmbOpsNo"
        Me.cmbOpsNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOpsNo.Size = New System.Drawing.Size(38, 20)
        Me.cmbOpsNo.TabIndex = 0
        Me.cmbOpsNo.Visible = False
        '
        'cmbFree
        '
        Me.cmbFree.BackColor = System.Drawing.SystemColors.Window
        Me.cmbFree.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbFree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFree.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbFree.Location = New System.Drawing.Point(220, 100)
        Me.cmbFree.Name = "cmbFree"
        Me.cmbFree.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbFree.Size = New System.Drawing.Size(218, 20)
        Me.cmbFree.TabIndex = 0
        Me.cmbFree.Visible = False
        '
        'lblPrinter
        '
        Me.lblPrinter.BackColor = System.Drawing.SystemColors.MenuBar
        Me.lblPrinter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblPrinter.Location = New System.Drawing.Point(75, 16)
        Me.lblPrinter.Multiline = True
        Me.lblPrinter.Name = "lblPrinter"
        Me.lblPrinter.Size = New System.Drawing.Size(396, 59)
        Me.lblPrinter.TabIndex = 9
        '
        'cmdAllPrint
        '
        Me.cmdAllPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAllPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAllPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAllPrint.Location = New System.Drawing.Point(194, 310)
        Me.cmdAllPrint.Name = "cmdAllPrint"
        Me.cmdAllPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAllPrint.Size = New System.Drawing.Size(81, 33)
        Me.cmdAllPrint.TabIndex = 10
        Me.cmdAllPrint.Text = "ALL PRINT"
        Me.cmdAllPrint.UseVisualStyleBackColor = True
        '
        'frmPrtGraph
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(488, 356)
        Me.Controls.Add(Me.cmdAllPrint)
        Me.Controls.Add(Me.lblPrinter)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtHistoryNo)
        Me.Controls.Add(Me.chkShipNo)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdPreview)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label11)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrtGraph"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "GRAPH VIEW PRINT"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtHistoryNo As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optGraphFree As System.Windows.Forms.RadioButton
    Friend WithEvents optGraphNormal As System.Windows.Forms.RadioButton
    Public WithEvents cmbOpsNo As System.Windows.Forms.ComboBox
    Public WithEvents cmbFree As System.Windows.Forms.ComboBox
    Friend WithEvents lblPrinter As System.Windows.Forms.TextBox
    Public WithEvents cmbSelectPart As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents cmdAllPrint As System.Windows.Forms.Button
#End Region

End Class
