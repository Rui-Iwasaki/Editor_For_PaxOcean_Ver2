<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrtOverView
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

    Public WithEvents chkShipNo As System.Windows.Forms.CheckBox
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents Label11 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.chkShipNo = New System.Windows.Forms.CheckBox()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtHistoryNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPrinter = New System.Windows.Forms.TextBox()
        Me.cmbSelectPart = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'chkShipNo
        '
        Me.chkShipNo.AutoSize = True
        Me.chkShipNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkShipNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShipNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShipNo.Location = New System.Drawing.Point(125, 81)
        Me.chkShipNo.Name = "chkShipNo"
        Me.chkShipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShipNo.Size = New System.Drawing.Size(96, 16)
        Me.chkShipNo.TabIndex = 0
        Me.chkShipNo.Text = "S.No Display"
        Me.chkShipNo.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(160, 142)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(81, 33)
        Me.cmdExit.TabIndex = 4
        Me.cmdExit.Text = "EXIT"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(160, 103)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(81, 33)
        Me.cmdPrint.TabIndex = 3
        Me.cmdPrint.Text = "PRINT"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Location = New System.Drawing.Point(20, 103)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(81, 33)
        Me.cmdPreview.TabIndex = 1
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
        Me.txtHistoryNo.Location = New System.Drawing.Point(207, 184)
        Me.txtHistoryNo.MaxLength = 0
        Me.txtHistoryNo.Name = "txtHistoryNo"
        Me.txtHistoryNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHistoryNo.Size = New System.Drawing.Size(34, 19)
        Me.txtHistoryNo.TabIndex = 8
        Me.txtHistoryNo.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(138, 184)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "History No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label1.Visible = False
        '
        'lblPrinter
        '
        Me.lblPrinter.BackColor = System.Drawing.SystemColors.MenuBar
        Me.lblPrinter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblPrinter.Location = New System.Drawing.Point(75, 16)
        Me.lblPrinter.Multiline = True
        Me.lblPrinter.Name = "lblPrinter"
        Me.lblPrinter.Size = New System.Drawing.Size(166, 59)
        Me.lblPrinter.TabIndex = 10
        '
        'cmbSelectPart
        '
        Me.cmbSelectPart.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSelectPart.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbSelectPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSelectPart.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSelectPart.Location = New System.Drawing.Point(95, 181)
        Me.cmbSelectPart.Name = "cmbSelectPart"
        Me.cmbSelectPart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbSelectPart.Size = New System.Drawing.Size(28, 20)
        Me.cmbSelectPart.TabIndex = 155
        Me.cmbSelectPart.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(18, 184)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(71, 12)
        Me.Label2.TabIndex = 154
        Me.Label2.Text = "Select Part"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label2.Visible = False
        '
        'frmPrtOverView
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(261, 206)
        Me.Controls.Add(Me.cmbSelectPart)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblPrinter)
        Me.Controls.Add(Me.txtHistoryNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkShipNo)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdPreview)
        Me.Controls.Add(Me.Label11)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrtOverView"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "OVER VIEW PRINT"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtHistoryNo As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblPrinter As System.Windows.Forms.TextBox
    Public WithEvents cmbSelectPart As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
#End Region

End Class
