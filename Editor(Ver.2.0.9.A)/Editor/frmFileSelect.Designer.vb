<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFileSelect
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

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkExcel = New System.Windows.Forms.CheckBox()
        Me.txtFileNameNew = New System.Windows.Forms.TextBox()
        Me.lblVersionUP = New System.Windows.Forms.Label()
        Me.chkVersionUP = New System.Windows.Forms.CheckBox()
        Me.chkCompile = New System.Windows.Forms.CheckBox()
        Me.numVersionUP = New System.Windows.Forms.NumericUpDown()
        Me.numVersion = New System.Windows.Forms.NumericUpDown()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdRef = New System.Windows.Forms.Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.fdgFolder = New System.Windows.Forms.FolderBrowserDialog()
        Me.chkHoan = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.numVersionUP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numVersion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkHoan)
        Me.GroupBox1.Controls.Add(Me.chkExcel)
        Me.GroupBox1.Controls.Add(Me.txtFileNameNew)
        Me.GroupBox1.Controls.Add(Me.lblVersionUP)
        Me.GroupBox1.Controls.Add(Me.chkVersionUP)
        Me.GroupBox1.Controls.Add(Me.chkCompile)
        Me.GroupBox1.Controls.Add(Me.numVersionUP)
        Me.GroupBox1.Controls.Add(Me.numVersion)
        Me.GroupBox1.Controls.Add(Me.txtFileName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmdRef)
        Me.GroupBox1.Controls.Add(Me.txtFilePath)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(638, 130)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "File"
        '
        'chkExcel
        '
        Me.chkExcel.AutoSize = True
        Me.chkExcel.Checked = True
        Me.chkExcel.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExcel.Location = New System.Drawing.Point(548, 105)
        Me.chkExcel.Name = "chkExcel"
        Me.chkExcel.Size = New System.Drawing.Size(72, 16)
        Me.chkExcel.TabIndex = 12
        Me.chkExcel.Text = "Excel IN"
        Me.chkExcel.UseVisualStyleBackColor = True
        '
        'txtFileNameNew
        '
        Me.txtFileNameNew.Location = New System.Drawing.Point(226, 51)
        Me.txtFileNameNew.MaxLength = 8
        Me.txtFileNameNew.Name = "txtFileNameNew"
        Me.txtFileNameNew.Size = New System.Drawing.Size(93, 19)
        Me.txtFileNameNew.TabIndex = 11
        Me.txtFileNameNew.Visible = False
        '
        'lblVersionUP
        '
        Me.lblVersionUP.AutoSize = True
        Me.lblVersionUP.Location = New System.Drawing.Point(203, 54)
        Me.lblVersionUP.Name = "lblVersionUP"
        Me.lblVersionUP.Size = New System.Drawing.Size(17, 12)
        Me.lblVersionUP.TabIndex = 10
        Me.lblVersionUP.Text = "->"
        Me.lblVersionUP.Visible = False
        '
        'chkVersionUP
        '
        Me.chkVersionUP.AutoSize = True
        Me.chkVersionUP.Location = New System.Drawing.Point(104, 79)
        Me.chkVersionUP.Name = "chkVersionUP"
        Me.chkVersionUP.Size = New System.Drawing.Size(84, 16)
        Me.chkVersionUP.TabIndex = 9
        Me.chkVersionUP.Text = "Version UP"
        Me.chkVersionUP.UseVisualStyleBackColor = True
        '
        'chkCompile
        '
        Me.chkCompile.AutoSize = True
        Me.chkCompile.Location = New System.Drawing.Point(104, 101)
        Me.chkCompile.Name = "chkCompile"
        Me.chkCompile.Size = New System.Drawing.Size(126, 16)
        Me.chkCompile.TabIndex = 9
        Me.chkCompile.Text = "Read Compile File"
        Me.chkCompile.UseVisualStyleBackColor = True
        '
        'numVersionUP
        '
        Me.numVersionUP.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVersionUP.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.numVersionUP.Location = New System.Drawing.Point(564, 79)
        Me.numVersionUP.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.numVersionUP.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numVersionUP.Name = "numVersionUP"
        Me.numVersionUP.Size = New System.Drawing.Size(61, 20)
        Me.numVersionUP.TabIndex = 8
        Me.numVersionUP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numVersionUP.ThousandsSeparator = True
        Me.numVersionUP.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numVersionUP.Visible = False
        '
        'numVersion
        '
        Me.numVersion.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVersion.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.numVersion.Location = New System.Drawing.Point(564, 105)
        Me.numVersion.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.numVersion.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numVersion.Name = "numVersion"
        Me.numVersion.Size = New System.Drawing.Size(61, 20)
        Me.numVersion.TabIndex = 8
        Me.numVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numVersion.ThousandsSeparator = True
        Me.numVersion.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numVersion.Visible = False
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(104, 51)
        Me.txtFileName.MaxLength = 8
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(93, 19)
        Me.txtFileName.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "File Name"
        '
        'cmdRef
        '
        Me.cmdRef.Font = New System.Drawing.Font("ＭＳ ゴシック", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdRef.Location = New System.Drawing.Point(599, 24)
        Me.cmdRef.Name = "cmdRef"
        Me.cmdRef.Size = New System.Drawing.Size(26, 22)
        Me.cmdRef.TabIndex = 2
        Me.cmdRef.Text = "..."
        Me.cmdRef.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdRef.UseVisualStyleBackColor = True
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(104, 26)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(489, 19)
        Me.txtFilePath.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Saving Place"
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(443, 154)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(105, 27)
        Me.cmdOK.TabIndex = 5
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(554, 154)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(97, 27)
        Me.cmdCancel.TabIndex = 4
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'chkHoan
        '
        Me.chkHoan.AutoSize = True
        Me.chkHoan.Location = New System.Drawing.Point(398, 105)
        Me.chkHoan.Name = "chkHoan"
        Me.chkHoan.Size = New System.Drawing.Size(138, 16)
        Me.chkHoan.TabIndex = 13
        Me.chkHoan.Text = "Japanese menu(Hoan)"
        Me.chkHoan.UseVisualStyleBackColor = True
        '
        'frmFileSelect
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(662, 195)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFileSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numVersionUP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numVersion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdRef As System.Windows.Forms.Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents fdgFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents numVersion As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkCompile As System.Windows.Forms.CheckBox
    Friend WithEvents chkVersionUP As System.Windows.Forms.CheckBox
    Friend WithEvents lblVersionUP As System.Windows.Forms.Label
    Friend WithEvents numVersionUP As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtFileNameNew As System.Windows.Forms.TextBox
    Friend WithEvents chkExcel As System.Windows.Forms.CheckBox
    Friend WithEvents chkHoan As System.Windows.Forms.CheckBox
End Class
