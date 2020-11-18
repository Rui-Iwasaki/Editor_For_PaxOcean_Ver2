<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrtLocalUnit
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

    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents cmbPageRangeTo As System.Windows.Forms.ComboBox
    Public WithEvents cmbPageRangeFrom As System.Windows.Forms.ComboBox
    Public WithEvents optPageRangePages As System.Windows.Forms.RadioButton
    Public WithEvents optPageRangeAll As System.Windows.Forms.RadioButton
    Public WithEvents lblTo As System.Windows.Forms.Label
    Public WithEvents lblFrom As System.Windows.Forms.Label
    Public WithEvents fraPageRange As System.Windows.Forms.GroupBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.fraPageRange = New System.Windows.Forms.GroupBox()
        Me.cmbPageRangeTo = New System.Windows.Forms.ComboBox()
        Me.cmbPageRangeFrom = New System.Windows.Forms.ComboBox()
        Me.optPageRangePages = New System.Windows.Forms.RadioButton()
        Me.optPageRangeAll = New System.Windows.Forms.RadioButton()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblPrinter = New System.Windows.Forms.TextBox()
        Me.fraOption = New System.Windows.Forms.GroupBox()
        Me.chkPagePrint = New System.Windows.Forms.CheckBox()
        Me.chkFormatType = New System.Windows.Forms.CheckBox()
        Me.fraPageRange.SuspendLayout()
        Me.fraOption.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(171, 357)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(81, 33)
        Me.cmdExit.TabIndex = 5
        Me.cmdExit.Text = "EXIT"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(171, 318)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(81, 33)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "PRINT"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Location = New System.Drawing.Point(30, 318)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(81, 33)
        Me.cmdPreview.TabIndex = 2
        Me.cmdPreview.Text = "PREVIEW"
        Me.cmdPreview.UseVisualStyleBackColor = True
        '
        'fraPageRange
        '
        Me.fraPageRange.BackColor = System.Drawing.SystemColors.Control
        Me.fraPageRange.Controls.Add(Me.cmbPageRangeTo)
        Me.fraPageRange.Controls.Add(Me.cmbPageRangeFrom)
        Me.fraPageRange.Controls.Add(Me.optPageRangePages)
        Me.fraPageRange.Controls.Add(Me.optPageRangeAll)
        Me.fraPageRange.Controls.Add(Me.lblTo)
        Me.fraPageRange.Controls.Add(Me.lblFrom)
        Me.fraPageRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPageRange.Location = New System.Drawing.Point(22, 80)
        Me.fraPageRange.Name = "fraPageRange"
        Me.fraPageRange.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPageRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPageRange.Size = New System.Drawing.Size(241, 145)
        Me.fraPageRange.TabIndex = 0
        Me.fraPageRange.TabStop = False
        Me.fraPageRange.Text = "PRINT PAGE RANGE"
        '
        'cmbPageRangeTo
        '
        Me.cmbPageRangeTo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPageRangeTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPageRangeTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPageRangeTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPageRangeTo.Location = New System.Drawing.Point(149, 102)
        Me.cmbPageRangeTo.Name = "cmbPageRangeTo"
        Me.cmbPageRangeTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPageRangeTo.Size = New System.Drawing.Size(81, 20)
        Me.cmbPageRangeTo.TabIndex = 3
        '
        'cmbPageRangeFrom
        '
        Me.cmbPageRangeFrom.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPageRangeFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPageRangeFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPageRangeFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPageRangeFrom.Location = New System.Drawing.Point(149, 71)
        Me.cmbPageRangeFrom.Name = "cmbPageRangeFrom"
        Me.cmbPageRangeFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPageRangeFrom.Size = New System.Drawing.Size(81, 20)
        Me.cmbPageRangeFrom.TabIndex = 2
        '
        'optPageRangePages
        '
        Me.optPageRangePages.BackColor = System.Drawing.SystemColors.Control
        Me.optPageRangePages.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPageRangePages.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPageRangePages.Location = New System.Drawing.Point(8, 68)
        Me.optPageRangePages.Name = "optPageRangePages"
        Me.optPageRangePages.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPageRangePages.Size = New System.Drawing.Size(65, 25)
        Me.optPageRangePages.TabIndex = 1
        Me.optPageRangePages.TabStop = True
        Me.optPageRangePages.Text = "PAGES"
        Me.optPageRangePages.UseVisualStyleBackColor = True
        '
        'optPageRangeAll
        '
        Me.optPageRangeAll.BackColor = System.Drawing.SystemColors.Control
        Me.optPageRangeAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPageRangeAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPageRangeAll.Location = New System.Drawing.Point(8, 32)
        Me.optPageRangeAll.Name = "optPageRangeAll"
        Me.optPageRangeAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPageRangeAll.Size = New System.Drawing.Size(65, 25)
        Me.optPageRangeAll.TabIndex = 0
        Me.optPageRangeAll.TabStop = True
        Me.optPageRangeAll.Text = "ALL"
        Me.optPageRangeAll.UseVisualStyleBackColor = True
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.SystemColors.Control
        Me.lblTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTo.Location = New System.Drawing.Point(117, 105)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTo.Size = New System.Drawing.Size(17, 12)
        Me.lblTo.TabIndex = 6
        Me.lblTo.Text = "TO"
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.SystemColors.Control
        Me.lblFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFrom.Location = New System.Drawing.Point(105, 74)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFrom.Size = New System.Drawing.Size(29, 12)
        Me.lblFrom.TabIndex = 5
        Me.lblFrom.Text = "FROM"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        'lblPrinter
        '
        Me.lblPrinter.BackColor = System.Drawing.SystemColors.MenuBar
        Me.lblPrinter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblPrinter.Location = New System.Drawing.Point(75, 16)
        Me.lblPrinter.Multiline = True
        Me.lblPrinter.Name = "lblPrinter"
        Me.lblPrinter.Size = New System.Drawing.Size(188, 59)
        Me.lblPrinter.TabIndex = 8
        '
        'fraOption
        '
        Me.fraOption.BackColor = System.Drawing.SystemColors.Control
        Me.fraOption.Controls.Add(Me.chkFormatType)
        Me.fraOption.Controls.Add(Me.chkPagePrint)
        Me.fraOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraOption.Location = New System.Drawing.Point(22, 234)
        Me.fraOption.Name = "fraOption"
        Me.fraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.fraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraOption.Size = New System.Drawing.Size(241, 70)
        Me.fraOption.TabIndex = 9
        Me.fraOption.TabStop = False
        Me.fraOption.Text = "OPTION"
        '
        'chkPagePrint
        '
        Me.chkPagePrint.AutoSize = True
        Me.chkPagePrint.BackColor = System.Drawing.SystemColors.Control
        Me.chkPagePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPagePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPagePrint.Location = New System.Drawing.Point(8, 24)
        Me.chkPagePrint.Name = "chkPagePrint"
        Me.chkPagePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPagePrint.Size = New System.Drawing.Size(174, 16)
        Me.chkPagePrint.TabIndex = 2
        Me.chkPagePrint.Text = "Prints including Page No."
        Me.chkPagePrint.UseVisualStyleBackColor = True
        '
        'chkFormatType
        '
        Me.chkFormatType.AutoSize = True
        Me.chkFormatType.BackColor = System.Drawing.SystemColors.Control
        Me.chkFormatType.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFormatType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFormatType.Location = New System.Drawing.Point(8, 46)
        Me.chkFormatType.Name = "chkFormatType"
        Me.chkFormatType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFormatType.Size = New System.Drawing.Size(156, 16)
        Me.chkFormatType.TabIndex = 3
        Me.chkFormatType.Text = "Formats in its infancy"
        Me.chkFormatType.UseVisualStyleBackColor = True
        '
        'frmPrtLocalUnit
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(284, 405)
        Me.Controls.Add(Me.fraOption)
        Me.Controls.Add(Me.lblPrinter)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdPreview)
        Me.Controls.Add(Me.fraPageRange)
        Me.Controls.Add(Me.Label11)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrtLocalUnit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "LOCAL UNIT PRINT"
        Me.fraPageRange.ResumeLayout(False)
        Me.fraPageRange.PerformLayout()
        Me.fraOption.ResumeLayout(False)
        Me.fraOption.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPrinter As System.Windows.Forms.TextBox
    Public WithEvents fraOption As System.Windows.Forms.GroupBox
    Public WithEvents chkPagePrint As System.Windows.Forms.CheckBox
    Public WithEvents chkFormatType As System.Windows.Forms.CheckBox
#End Region

End Class
