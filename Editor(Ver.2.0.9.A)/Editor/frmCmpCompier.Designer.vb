<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCmpCompier
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCmpCompier))
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdCompile = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.txtMsg = New System.Windows.Forms.TextBox()
        Me.fraWait = New System.Windows.Forms.Panel()
        Me.lblWait2 = New System.Windows.Forms.Label()
        Me.lblWait1 = New System.Windows.Forms.Label()
        Me.cmdErrorDisplay = New System.Windows.Forms.Button()
        Me.optJapanese = New System.Windows.Forms.RadioButton()
        Me.optEnglish = New System.Windows.Forms.RadioButton()
        Me.cmdOutput = New System.Windows.Forms.Button()
        Me.dlgOutput = New System.Windows.Forms.SaveFileDialog()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.optChkBlank = New System.Windows.Forms.RadioButton()
        Me.optNotChkBlank = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.optDefaultCopy = New System.Windows.Forms.RadioButton()
        Me.optDefaultNotCopy = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.fdgFolder = New System.Windows.Forms.FolderBrowserDialog()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.cmbStatusAnalog = New System.Windows.Forms.ComboBox()
        Me.chkCHIDinit = New System.Windows.Forms.CheckBox()
        Me.fraWait.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(475, 552)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(100, 40)
        Me.cmdPrint.TabIndex = 9
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        Me.cmdPrint.Visible = False
        '
        'cmdCompile
        '
        Me.cmdCompile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCompile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCompile.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCompile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCompile.Location = New System.Drawing.Point(581, 552)
        Me.cmdCompile.Name = "cmdCompile"
        Me.cmdCompile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCompile.Size = New System.Drawing.Size(100, 40)
        Me.cmdCompile.TabIndex = 10
        Me.cmdCompile.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(687, 552)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(100, 40)
        Me.cmdExit.TabIndex = 11
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
        Me.txtMsg.Location = New System.Drawing.Point(12, 12)
        Me.txtMsg.Multiline = True
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.ReadOnly = True
        Me.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMsg.Size = New System.Drawing.Size(775, 479)
        Me.txtMsg.TabIndex = 13
        '
        'fraWait
        '
        Me.fraWait.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fraWait.BackColor = System.Drawing.Color.Gray
        Me.fraWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.fraWait.Controls.Add(Me.lblWait2)
        Me.fraWait.Controls.Add(Me.lblWait1)
        Me.fraWait.Location = New System.Drawing.Point(576, 17)
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
        Me.lblWait1.Location = New System.Drawing.Point(3, 9)
        Me.lblWait1.Name = "lblWait1"
        Me.lblWait1.Size = New System.Drawing.Size(182, 28)
        Me.lblWait1.TabIndex = 0
        Me.lblWait1.Text = "Now Compiling"
        Me.lblWait1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdErrorDisplay
        '
        Me.cmdErrorDisplay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdErrorDisplay.BackColor = System.Drawing.SystemColors.Control
        Me.cmdErrorDisplay.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdErrorDisplay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdErrorDisplay.Location = New System.Drawing.Point(369, 552)
        Me.cmdErrorDisplay.Name = "cmdErrorDisplay"
        Me.cmdErrorDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdErrorDisplay.Size = New System.Drawing.Size(100, 40)
        Me.cmdErrorDisplay.TabIndex = 9
        Me.cmdErrorDisplay.Text = "Add Check Msg"
        Me.cmdErrorDisplay.UseVisualStyleBackColor = True
        '
        'optJapanese
        '
        Me.optJapanese.AutoSize = True
        Me.optJapanese.Location = New System.Drawing.Point(76, 3)
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
        Me.optEnglish.Location = New System.Drawing.Point(3, 3)
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
        Me.cmdOutput.Location = New System.Drawing.Point(475, 552)
        Me.cmdOutput.Name = "cmdOutput"
        Me.cmdOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOutput.Size = New System.Drawing.Size(100, 40)
        Me.cmdOutput.TabIndex = 9
        Me.cmdOutput.Text = "Log Output"
        Me.cmdOutput.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Panel3)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Panel2)
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 497)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(351, 95)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Option"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.optChkBlank)
        Me.Panel3.Controls.Add(Me.optNotChkBlank)
        Me.Panel3.Location = New System.Drawing.Point(178, 69)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(156, 21)
        Me.Panel3.TabIndex = 19
        '
        'optChkBlank
        '
        Me.optChkBlank.AutoSize = True
        Me.optChkBlank.Checked = True
        Me.optChkBlank.Cursor = System.Windows.Forms.Cursors.Default
        Me.optChkBlank.Location = New System.Drawing.Point(3, 3)
        Me.optChkBlank.Name = "optChkBlank"
        Me.optChkBlank.Size = New System.Drawing.Size(41, 16)
        Me.optChkBlank.TabIndex = 0
        Me.optChkBlank.TabStop = True
        Me.optChkBlank.Text = "Yes"
        Me.optChkBlank.UseVisualStyleBackColor = True
        '
        'optNotChkBlank
        '
        Me.optNotChkBlank.AutoSize = True
        Me.optNotChkBlank.Location = New System.Drawing.Point(76, 3)
        Me.optNotChkBlank.Name = "optNotChkBlank"
        Me.optNotChkBlank.Size = New System.Drawing.Size(35, 16)
        Me.optNotChkBlank.TabIndex = 0
        Me.optNotChkBlank.Text = "No"
        Me.optNotChkBlank.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(161, 12)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Fill in the blank(CH List)"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.optDefaultCopy)
        Me.Panel2.Controls.Add(Me.optDefaultNotCopy)
        Me.Panel2.Location = New System.Drawing.Point(178, 42)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(156, 21)
        Me.Panel2.TabIndex = 17
        '
        'optDefaultCopy
        '
        Me.optDefaultCopy.AutoSize = True
        Me.optDefaultCopy.Checked = True
        Me.optDefaultCopy.Location = New System.Drawing.Point(3, 3)
        Me.optDefaultCopy.Name = "optDefaultCopy"
        Me.optDefaultCopy.Size = New System.Drawing.Size(47, 16)
        Me.optDefaultCopy.TabIndex = 0
        Me.optDefaultCopy.TabStop = True
        Me.optDefaultCopy.Text = "Copy"
        Me.optDefaultCopy.UseVisualStyleBackColor = True
        '
        'optDefaultNotCopy
        '
        Me.optDefaultNotCopy.AutoSize = True
        Me.optDefaultNotCopy.Location = New System.Drawing.Point(76, 3)
        Me.optDefaultNotCopy.Name = "optDefaultNotCopy"
        Me.optDefaultNotCopy.Size = New System.Drawing.Size(71, 16)
        Me.optDefaultNotCopy.TabIndex = 0
        Me.optDefaultNotCopy.Text = "Not Copy"
        Me.optDefaultNotCopy.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.optEnglish)
        Me.Panel1.Controls.Add(Me.optJapanese)
        Me.Panel1.Location = New System.Drawing.Point(178, 15)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(156, 21)
        Me.Panel1.TabIndex = 17
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Default Data"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Language"
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatus.Location = New System.Drawing.Point(685, 424)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatus.Size = New System.Drawing.Size(68, 20)
        Me.cmbStatus.TabIndex = 113
        '
        'cmbStatusAnalog
        '
        Me.cmbStatusAnalog.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatusAnalog.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatusAnalog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatusAnalog.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatusAnalog.Location = New System.Drawing.Point(685, 450)
        Me.cmbStatusAnalog.Name = "cmbStatusAnalog"
        Me.cmbStatusAnalog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatusAnalog.Size = New System.Drawing.Size(68, 20)
        Me.cmbStatusAnalog.TabIndex = 114
        '
        'chkCHIDinit
        '
        Me.chkCHIDinit.AutoSize = True
        Me.chkCHIDinit.Location = New System.Drawing.Point(543, 502)
        Me.chkCHIDinit.Name = "chkCHIDinit"
        Me.chkCHIDinit.Size = New System.Drawing.Size(78, 16)
        Me.chkCHIDinit.TabIndex = 115
        Me.chkCHIDinit.Text = "CHID Init"
        Me.chkCHIDinit.UseVisualStyleBackColor = True
        '
        'frmCmpCompier
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(799, 604)
        Me.Controls.Add(Me.chkCHIDinit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmdCompile)
        Me.Controls.Add(Me.fraWait)
        Me.Controls.Add(Me.cmdErrorDisplay)
        Me.Controls.Add(Me.cmdOutput)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.txtMsg)
        Me.Controls.Add(Me.cmbStatus)
        Me.Controls.Add(Me.cmbStatusAnalog)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCmpCompier"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "COMPILER"
        Me.fraWait.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdCompile As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents txtMsg As System.Windows.Forms.TextBox
    Friend WithEvents fraWait As System.Windows.Forms.Panel
    Friend WithEvents lblWait1 As System.Windows.Forms.Label
    Friend WithEvents lblWait2 As System.Windows.Forms.Label
    Public WithEvents cmdErrorDisplay As System.Windows.Forms.Button
    Friend WithEvents optJapanese As System.Windows.Forms.RadioButton
    Friend WithEvents optEnglish As System.Windows.Forms.RadioButton
    Public WithEvents cmdOutput As System.Windows.Forms.Button
    Friend WithEvents dlgOutput As System.Windows.Forms.SaveFileDialog
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents optDefaultNotCopy As System.Windows.Forms.RadioButton
    Friend WithEvents optDefaultCopy As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents fdgFolder As System.Windows.Forms.FolderBrowserDialog
    Public WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Public WithEvents cmbStatusAnalog As System.Windows.Forms.ComboBox
    Friend WithEvents chkCHIDinit As System.Windows.Forms.CheckBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents optChkBlank As System.Windows.Forms.RadioButton
    Friend WithEvents optNotChkBlank As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
#End Region

End Class
