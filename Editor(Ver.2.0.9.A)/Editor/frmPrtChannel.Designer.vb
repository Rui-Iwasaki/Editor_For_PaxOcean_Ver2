<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrtChannel
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
    Public WithEvents chkSecretChannel As System.Windows.Forms.CheckBox
    Public WithEvents chkDummyData As System.Windows.Forms.CheckBox
    Public WithEvents fraOption As System.Windows.Forms.GroupBox
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
        Me.fraOption = New System.Windows.Forms.GroupBox()
        Me.chkINSG = New System.Windows.Forms.CheckBox()
        Me.chkOrder = New System.Windows.Forms.CheckBox()
        Me.chkCombine = New System.Windows.Forms.CheckBox()
        Me.chkCHNo = New System.Windows.Forms.CheckBox()
        Me.chkSecretChannel = New System.Windows.Forms.CheckBox()
        Me.chkDummyData = New System.Windows.Forms.CheckBox()
        Me.fraPageRange = New System.Windows.Forms.GroupBox()
        Me.cmbGroup = New System.Windows.Forms.ComboBox()
        Me.optPageRangeGroup = New System.Windows.Forms.RadioButton()
        Me.cmbPageRangeTo = New System.Windows.Forms.ComboBox()
        Me.cmbPageRangeFrom = New System.Windows.Forms.ComboBox()
        Me.optPageRangePages = New System.Windows.Forms.RadioButton()
        Me.optPageRangeAll = New System.Windows.Forms.RadioButton()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblPrinter = New System.Windows.Forms.TextBox()
        Me.fraOption.SuspendLayout()
        Me.fraPageRange.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(171, 455)
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
        Me.cmdPrint.Location = New System.Drawing.Point(171, 416)
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
        Me.cmdPreview.Location = New System.Drawing.Point(30, 416)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(81, 33)
        Me.cmdPreview.TabIndex = 2
        Me.cmdPreview.Text = "PREVIEW"
        Me.cmdPreview.UseVisualStyleBackColor = True
        '
        'fraOption
        '
        Me.fraOption.BackColor = System.Drawing.SystemColors.Control
        Me.fraOption.Controls.Add(Me.chkINSG)
        Me.fraOption.Controls.Add(Me.chkOrder)
        Me.fraOption.Controls.Add(Me.chkCombine)
        Me.fraOption.Controls.Add(Me.chkCHNo)
        Me.fraOption.Controls.Add(Me.chkSecretChannel)
        Me.fraOption.Controls.Add(Me.chkDummyData)
        Me.fraOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraOption.Location = New System.Drawing.Point(22, 231)
        Me.fraOption.Name = "fraOption"
        Me.fraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.fraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraOption.Size = New System.Drawing.Size(261, 163)
        Me.fraOption.TabIndex = 1
        Me.fraOption.TabStop = False
        Me.fraOption.Text = "OPTION"
        '
        'chkINSG
        '
        Me.chkINSG.AutoSize = True
        Me.chkINSG.BackColor = System.Drawing.SystemColors.Control
        Me.chkINSG.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkINSG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkINSG.Location = New System.Drawing.Point(8, 136)
        Me.chkINSG.Name = "chkINSG"
        Me.chkINSG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkINSG.Size = New System.Drawing.Size(144, 16)
        Me.chkINSG.TabIndex = 5
        Me.chkINSG.Text = "R,W,S INSG not print"
        Me.chkINSG.UseVisualStyleBackColor = True
        '
        'chkOrder
        '
        Me.chkOrder.AutoSize = True
        Me.chkOrder.BackColor = System.Drawing.SystemColors.Control
        Me.chkOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOrder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOrder.Location = New System.Drawing.Point(8, 114)
        Me.chkOrder.Name = "chkOrder"
        Me.chkOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOrder.Size = New System.Drawing.Size(132, 16)
        Me.chkOrder.TabIndex = 4
        Me.chkOrder.Text = "Ascending Group No"
        Me.chkOrder.UseVisualStyleBackColor = True
        '
        'chkCombine
        '
        Me.chkCombine.AutoSize = True
        Me.chkCombine.BackColor = System.Drawing.SystemColors.Control
        Me.chkCombine.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCombine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCombine.Location = New System.Drawing.Point(8, 92)
        Me.chkCombine.Name = "chkCombine"
        Me.chkCombine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCombine.Size = New System.Drawing.Size(246, 16)
        Me.chkCombine.TabIndex = 3
        Me.chkCombine.Text = "Prints Not CombineType (Combine Only)"
        Me.chkCombine.UseVisualStyleBackColor = True
        '
        'chkCHNo
        '
        Me.chkCHNo.AutoSize = True
        Me.chkCHNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkCHNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCHNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCHNo.Location = New System.Drawing.Point(8, 70)
        Me.chkCHNo.Name = "chkCHNo"
        Me.chkCHNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCHNo.Size = New System.Drawing.Size(174, 16)
        Me.chkCHNo.TabIndex = 2
        Me.chkCHNo.Text = "Prints CHNo. at Tag mode "
        Me.chkCHNo.UseVisualStyleBackColor = True
        '
        'chkSecretChannel
        '
        Me.chkSecretChannel.AutoSize = True
        Me.chkSecretChannel.BackColor = System.Drawing.SystemColors.Control
        Me.chkSecretChannel.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSecretChannel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSecretChannel.Location = New System.Drawing.Point(8, 24)
        Me.chkSecretChannel.Name = "chkSecretChannel"
        Me.chkSecretChannel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSecretChannel.Size = New System.Drawing.Size(222, 16)
        Me.chkSecretChannel.TabIndex = 0
        Me.chkSecretChannel.Text = "Prints including a secret channel"
        Me.chkSecretChannel.UseVisualStyleBackColor = True
        '
        'chkDummyData
        '
        Me.chkDummyData.AutoSize = True
        Me.chkDummyData.BackColor = System.Drawing.SystemColors.Control
        Me.chkDummyData.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDummyData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDummyData.Location = New System.Drawing.Point(8, 48)
        Me.chkDummyData.Name = "chkDummyData"
        Me.chkDummyData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDummyData.Size = New System.Drawing.Size(198, 16)
        Me.chkDummyData.TabIndex = 1
        Me.chkDummyData.Text = "Prints including a dummy data"
        Me.chkDummyData.UseVisualStyleBackColor = True
        '
        'fraPageRange
        '
        Me.fraPageRange.BackColor = System.Drawing.SystemColors.Control
        Me.fraPageRange.Controls.Add(Me.cmbGroup)
        Me.fraPageRange.Controls.Add(Me.optPageRangeGroup)
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
        Me.fraPageRange.Size = New System.Drawing.Size(261, 145)
        Me.fraPageRange.TabIndex = 0
        Me.fraPageRange.TabStop = False
        Me.fraPageRange.Text = "PRINT PAGE RANGE"
        '
        'cmbGroup
        '
        Me.cmbGroup.BackColor = System.Drawing.SystemColors.Window
        Me.cmbGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGroup.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbGroup.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbGroup.Location = New System.Drawing.Point(79, 112)
        Me.cmbGroup.Name = "cmbGroup"
        Me.cmbGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbGroup.Size = New System.Drawing.Size(151, 20)
        Me.cmbGroup.TabIndex = 8
        '
        'optPageRangeGroup
        '
        Me.optPageRangeGroup.BackColor = System.Drawing.SystemColors.Control
        Me.optPageRangeGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPageRangeGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPageRangeGroup.Location = New System.Drawing.Point(8, 107)
        Me.optPageRangeGroup.Name = "optPageRangeGroup"
        Me.optPageRangeGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPageRangeGroup.Size = New System.Drawing.Size(65, 25)
        Me.optPageRangeGroup.TabIndex = 7
        Me.optPageRangeGroup.TabStop = True
        Me.optPageRangeGroup.Text = "GROUP"
        Me.optPageRangeGroup.UseVisualStyleBackColor = True
        '
        'cmbPageRangeTo
        '
        Me.cmbPageRangeTo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPageRangeTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPageRangeTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPageRangeTo.Location = New System.Drawing.Point(149, 78)
        Me.cmbPageRangeTo.Name = "cmbPageRangeTo"
        Me.cmbPageRangeTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPageRangeTo.Size = New System.Drawing.Size(81, 20)
        Me.cmbPageRangeTo.TabIndex = 3
        '
        'cmbPageRangeFrom
        '
        Me.cmbPageRangeFrom.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPageRangeFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPageRangeFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPageRangeFrom.Location = New System.Drawing.Point(149, 47)
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
        Me.optPageRangePages.Location = New System.Drawing.Point(8, 44)
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
        Me.optPageRangeAll.Location = New System.Drawing.Point(8, 17)
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
        Me.lblTo.Location = New System.Drawing.Point(117, 81)
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
        Me.lblFrom.Location = New System.Drawing.Point(105, 50)
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
        'frmPrtChannel
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(295, 497)
        Me.Controls.Add(Me.lblPrinter)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdPreview)
        Me.Controls.Add(Me.fraOption)
        Me.Controls.Add(Me.fraPageRange)
        Me.Controls.Add(Me.Label11)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrtChannel"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CHANNEL LIST PRINT"
        Me.fraOption.ResumeLayout(False)
        Me.fraOption.PerformLayout()
        Me.fraPageRange.ResumeLayout(False)
        Me.fraPageRange.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPrinter As System.Windows.Forms.TextBox
    Public WithEvents chkCHNo As System.Windows.Forms.CheckBox
    Public WithEvents chkCombine As System.Windows.Forms.CheckBox
    Public WithEvents optPageRangeGroup As System.Windows.Forms.RadioButton
    Public WithEvents cmbGroup As System.Windows.Forms.ComboBox
    Public WithEvents chkOrder As System.Windows.Forms.CheckBox
    Public WithEvents chkINSG As System.Windows.Forms.CheckBox
#End Region

End Class
