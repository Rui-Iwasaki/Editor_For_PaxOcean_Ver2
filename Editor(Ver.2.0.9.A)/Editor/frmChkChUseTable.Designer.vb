<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChkChUseTable
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChkChUseTable))
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.txtMsg = New System.Windows.Forms.TextBox()
        Me.fraWait = New System.Windows.Forms.Panel()
        Me.lblWait2 = New System.Windows.Forms.Label()
        Me.lblWait1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optJapanese = New System.Windows.Forms.RadioButton()
        Me.optEnglish = New System.Windows.Forms.RadioButton()
        Me.cmdOutput = New System.Windows.Forms.Button()
        Me.dlgOutput = New System.Windows.Forms.SaveFileDialog()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.optAllCH = New System.Windows.Forms.RadioButton()
        Me.optCH = New System.Windows.Forms.RadioButton()
        Me.txtChNo = New System.Windows.Forms.TextBox()
        Me.fraWait.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
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
        Me.cmdExit.Location = New System.Drawing.Point(601, 537)
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
        Me.txtMsg.Location = New System.Drawing.Point(12, 100)
        Me.txtMsg.Multiline = True
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.ReadOnly = True
        Me.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMsg.Size = New System.Drawing.Size(709, 419)
        Me.txtMsg.TabIndex = 13
        Me.txtMsg.TabStop = False
        '
        'fraWait
        '
        Me.fraWait.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fraWait.BackColor = System.Drawing.Color.Gray
        Me.fraWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.fraWait.Controls.Add(Me.lblWait2)
        Me.fraWait.Controls.Add(Me.lblWait1)
        Me.fraWait.Location = New System.Drawing.Point(511, 17)
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
        Me.GroupBox1.Controls.Add(Me.optJapanese)
        Me.GroupBox1.Controls.Add(Me.optEnglish)
        Me.GroupBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 525)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(182, 53)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Language"
        '
        'optJapanese
        '
        Me.optJapanese.AutoSize = True
        Me.optJapanese.Location = New System.Drawing.Point(88, 23)
        Me.optJapanese.Name = "optJapanese"
        Me.optJapanese.Size = New System.Drawing.Size(71, 16)
        Me.optJapanese.TabIndex = 0
        Me.optJapanese.Text = "Japanese"
        Me.optJapanese.UseVisualStyleBackColor = True
        '
        'optEnglish
        '
        Me.optEnglish.AutoSize = True
        Me.optEnglish.Checked = True
        Me.optEnglish.Location = New System.Drawing.Point(15, 23)
        Me.optEnglish.Name = "optEnglish"
        Me.optEnglish.Size = New System.Drawing.Size(65, 16)
        Me.optEnglish.TabIndex = 0
        Me.optEnglish.TabStop = True
        Me.optEnglish.Text = "English"
        Me.optEnglish.UseVisualStyleBackColor = True
        '
        'cmdOutput
        '
        Me.cmdOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdOutput.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOutput.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOutput.Location = New System.Drawing.Point(208, 536)
        Me.cmdOutput.Name = "cmdOutput"
        Me.cmdOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOutput.Size = New System.Drawing.Size(120, 40)
        Me.cmdOutput.TabIndex = 2
        Me.cmdOutput.Text = "CSV Output"
        Me.cmdOutput.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdSearch)
        Me.GroupBox2.Controls.Add(Me.optAllCH)
        Me.GroupBox2.Controls.Add(Me.optCH)
        Me.GroupBox2.Controls.Add(Me.txtChNo)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(420, 80)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'cmdSearch
        '
        Me.cmdSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Location = New System.Drawing.Point(280, 24)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(120, 40)
        Me.cmdSearch.TabIndex = 3
        Me.cmdSearch.Text = "Search"
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'optAllCH
        '
        Me.optAllCH.AutoSize = True
        Me.optAllCH.Location = New System.Drawing.Point(32, 52)
        Me.optAllCH.Name = "optAllCH"
        Me.optAllCH.Size = New System.Drawing.Size(95, 16)
        Me.optAllCH.TabIndex = 2
        Me.optAllCH.Text = "All Channels"
        Me.optAllCH.UseVisualStyleBackColor = True
        '
        'optCH
        '
        Me.optCH.AutoSize = True
        Me.optCH.Checked = True
        Me.optCH.Location = New System.Drawing.Point(32, 24)
        Me.optCH.Name = "optCH"
        Me.optCH.Size = New System.Drawing.Size(59, 16)
        Me.optCH.TabIndex = 0
        Me.optCH.TabStop = True
        Me.optCH.Text = "CH No."
        Me.optCH.UseVisualStyleBackColor = True
        '
        'txtChNo
        '
        Me.txtChNo.AcceptsReturn = True
        Me.txtChNo.Location = New System.Drawing.Point(100, 22)
        Me.txtChNo.MaxLength = 0
        Me.txtChNo.Name = "txtChNo"
        Me.txtChNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtChNo.Size = New System.Drawing.Size(48, 19)
        Me.txtChNo.TabIndex = 1
        Me.txtChNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmChkChUseTable
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(733, 590)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.fraWait)
        Me.Controls.Add(Me.txtMsg)
        Me.Controls.Add(Me.cmdOutput)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdExit)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChkChUseTable"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CHANNEL USE TABLE"
        Me.fraWait.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
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
    Friend WithEvents optJapanese As System.Windows.Forms.RadioButton
    Friend WithEvents optEnglish As System.Windows.Forms.RadioButton
    Public WithEvents cmdOutput As System.Windows.Forms.Button
    Friend WithEvents dlgOutput As System.Windows.Forms.SaveFileDialog
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents optAllCH As System.Windows.Forms.RadioButton
    Friend WithEvents optCH As System.Windows.Forms.RadioButton
    Public WithEvents txtChNo As System.Windows.Forms.TextBox
#End Region

End Class
