<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChkOutputSetting
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

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChkOutputSetting))
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.txtMsg = New System.Windows.Forms.TextBox()
        Me.fraWait = New System.Windows.Forms.Panel()
        Me.lblWait2 = New System.Windows.Forms.Label()
        Me.lblWait1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optCsv = New System.Windows.Forms.RadioButton()
        Me.optText = New System.Windows.Forms.RadioButton()
        Me.cmdOutput = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.fraWait.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(208, 536)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(120, 40)
        Me.cmdPrint.TabIndex = 9
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        Me.cmdPrint.Visible = False
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(710, 535)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(120, 40)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'txtMsg
        '
        Me.txtMsg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMsg.BackColor = System.Drawing.Color.Black
        Me.txtMsg.ForeColor = System.Drawing.Color.White
        Me.txtMsg.Location = New System.Drawing.Point(16, 12)
        Me.txtMsg.Multiline = True
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.ReadOnly = True
        Me.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMsg.Size = New System.Drawing.Size(814, 507)
        Me.txtMsg.TabIndex = 13
        Me.txtMsg.TabStop = False
        Me.txtMsg.Text = resources.GetString("txtMsg.Text")
        '
        'fraWait
        '
        Me.fraWait.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fraWait.BackColor = System.Drawing.Color.Gray
        Me.fraWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.fraWait.Controls.Add(Me.lblWait2)
        Me.fraWait.Controls.Add(Me.lblWait1)
        Me.fraWait.Location = New System.Drawing.Point(620, 17)
        Me.fraWait.Name = "fraWait"
        Me.fraWait.Size = New System.Drawing.Size(188, 73)
        Me.fraWait.TabIndex = 14
        Me.fraWait.Visible = False
        '
        'lblWait2
        '
        Me.lblWait2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWait2.ForeColor = System.Drawing.Color.White
        Me.lblWait2.Location = New System.Drawing.Point(3, 37)
        Me.lblWait2.Name = "lblWait2"
        Me.lblWait2.Size = New System.Drawing.Size(182, 23)
        Me.lblWait2.TabIndex = 1
        Me.lblWait2.Text = "Please wait..."
        Me.lblWait2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWait1
        '
        Me.lblWait1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWait1.ForeColor = System.Drawing.Color.White
        Me.lblWait1.Location = New System.Drawing.Point(0, 9)
        Me.lblWait1.Name = "lblWait1"
        Me.lblWait1.Size = New System.Drawing.Size(182, 28)
        Me.lblWait1.TabIndex = 0
        Me.lblWait1.Text = "Now Searching"
        Me.lblWait1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.optCsv)
        Me.GroupBox1.Controls.Add(Me.optText)
        Me.GroupBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 525)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(182, 53)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Output Type"
        '
        'optCsv
        '
        Me.optCsv.AutoSize = True
        Me.optCsv.Location = New System.Drawing.Point(103, 23)
        Me.optCsv.Name = "optCsv"
        Me.optCsv.Size = New System.Drawing.Size(41, 16)
        Me.optCsv.TabIndex = 0
        Me.optCsv.Text = "CSV"
        Me.optCsv.UseVisualStyleBackColor = True
        '
        'optText
        '
        Me.optText.AutoSize = True
        Me.optText.Checked = True
        Me.optText.Location = New System.Drawing.Point(25, 23)
        Me.optText.Name = "optText"
        Me.optText.Size = New System.Drawing.Size(47, 16)
        Me.optText.TabIndex = 0
        Me.optText.TabStop = True
        Me.optText.Text = "Text"
        Me.optText.UseVisualStyleBackColor = True
        '
        'cmdOutput
        '
        Me.cmdOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdOutput.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOutput.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOutput.Location = New System.Drawing.Point(208, 535)
        Me.cmdOutput.Name = "cmdOutput"
        Me.cmdOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOutput.Size = New System.Drawing.Size(120, 40)
        Me.cmdOutput.TabIndex = 2
        Me.cmdOutput.Text = "File Output"
        Me.cmdOutput.UseVisualStyleBackColor = True
        '
        'cmdSearch
        '
        Me.cmdSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Location = New System.Drawing.Point(574, 535)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(120, 40)
        Me.cmdSearch.TabIndex = 3
        Me.cmdSearch.Text = "Search"
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'frmChkOutputSetting
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(845, 590)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.fraWait)
        Me.Controls.Add(Me.txtMsg)
        Me.Controls.Add(Me.cmdOutput)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdExit)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChkOutputSetting"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CHANNEL OUTPUT SETTING"
        Me.fraWait.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents txtMsg As System.Windows.Forms.TextBox
    Friend WithEvents fraWait As System.Windows.Forms.Panel
    Friend WithEvents lblWait1 As System.Windows.Forms.Label
    Friend WithEvents lblWait2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optCsv As System.Windows.Forms.RadioButton
    Friend WithEvents optText As System.Windows.Forms.RadioButton
    Public WithEvents cmdOutput As System.Windows.Forms.Button
    Public WithEvents cmdSearch As System.Windows.Forms.Button
#End Region
End Class
