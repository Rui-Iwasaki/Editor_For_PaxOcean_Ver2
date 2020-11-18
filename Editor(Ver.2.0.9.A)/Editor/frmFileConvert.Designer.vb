<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFileConvert
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

    Public WithEvents cmdOpen22k As System.Windows.Forms.Button
    Public WithEvents cmdConvert As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents lblOpenFile22k As System.Windows.Forms.Label
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdOpen22k = New System.Windows.Forms.Button()
        Me.cmdConvert = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.lblOpenFile22k = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.cmbVersion = New System.Windows.Forms.ComboBox()
        Me.cmbUpdate = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.prgBar = New System.Windows.Forms.ProgressBar()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.cmbWork = New System.Windows.Forms.ComboBox()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lstMsg = New System.Windows.Forms.ListBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdOpen22k
        '
        Me.cmdOpen22k.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOpen22k.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOpen22k.Font = New System.Drawing.Font("ＭＳ ゴシック", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdOpen22k.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOpen22k.Location = New System.Drawing.Point(550, 21)
        Me.cmdOpen22k.Name = "cmdOpen22k"
        Me.cmdOpen22k.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOpen22k.Size = New System.Drawing.Size(26, 22)
        Me.cmdOpen22k.TabIndex = 2
        Me.cmdOpen22k.Text = "..."
        Me.cmdOpen22k.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdOpen22k.UseVisualStyleBackColor = True
        '
        'cmdConvert
        '
        Me.cmdConvert.BackColor = System.Drawing.SystemColors.Control
        Me.cmdConvert.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdConvert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdConvert.Location = New System.Drawing.Point(398, 304)
        Me.cmdConvert.Name = "cmdConvert"
        Me.cmdConvert.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdConvert.Size = New System.Drawing.Size(105, 27)
        Me.cmdConvert.TabIndex = 1
        Me.cmdConvert.Text = "Convert"
        Me.cmdConvert.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(508, 304)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(97, 27)
        Me.cmdCancel.TabIndex = 0
        Me.cmdCancel.Text = "Exit"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'lblOpenFile22k
        '
        Me.lblOpenFile22k.BackColor = System.Drawing.SystemColors.Control
        Me.lblOpenFile22k.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOpenFile22k.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOpenFile22k.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpenFile22k.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOpenFile22k.Location = New System.Drawing.Point(20, 20)
        Me.lblOpenFile22k.Name = "lblOpenFile22k"
        Me.lblOpenFile22k.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOpenFile22k.Size = New System.Drawing.Size(526, 22)
        Me.lblOpenFile22k.TabIndex = 3
        Me.lblOpenFile22k.Text = "c:\"
        Me.lblOpenFile22k.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(798, 40)
        Me.ProgressBar1.Maximum = 1650
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(363, 23)
        Me.ProgressBar1.TabIndex = 6
        '
        'cmbVersion
        '
        Me.cmbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVersion.FormattingEnabled = True
        Me.cmbVersion.Location = New System.Drawing.Point(1107, 104)
        Me.cmbVersion.Name = "cmbVersion"
        Me.cmbVersion.Size = New System.Drawing.Size(54, 20)
        Me.cmbVersion.TabIndex = 7
        '
        'cmbUpdate
        '
        Me.cmbUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUpdate.FormattingEnabled = True
        Me.cmbUpdate.Location = New System.Drawing.Point(1107, 154)
        Me.cmbUpdate.Name = "cmbUpdate"
        Me.cmbUpdate.Size = New System.Drawing.Size(54, 20)
        Me.cmbUpdate.TabIndex = 8
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblOpenFile22k)
        Me.GroupBox1.Controls.Add(Me.cmdOpen22k)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 60)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(591, 52)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "22K Editor Main File"
        '
        'lblMessage
        '
        Me.lblMessage.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(12, 9)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(591, 21)
        Me.lblMessage.TabIndex = 11
        Me.lblMessage.Text = "lblMassage"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'prgBar
        '
        Me.prgBar.Location = New System.Drawing.Point(12, 28)
        Me.prgBar.Name = "prgBar"
        Me.prgBar.Size = New System.Drawing.Size(591, 21)
        Me.prgBar.TabIndex = 10
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.cmbWork)
        Me.GroupBox9.Controls.Add(Me.cmbUnit)
        Me.GroupBox9.Controls.Add(Me.cmbStatus)
        Me.GroupBox9.Location = New System.Drawing.Point(8, 296)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(240, 44)
        Me.GroupBox9.TabIndex = 114
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Visible False"
        Me.GroupBox9.Visible = False
        '
        'cmbWork
        '
        Me.cmbWork.BackColor = System.Drawing.SystemColors.Window
        Me.cmbWork.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbWork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbWork.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbWork.Location = New System.Drawing.Point(160, 16)
        Me.cmbWork.Name = "cmbWork"
        Me.cmbWork.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbWork.Size = New System.Drawing.Size(64, 20)
        Me.cmbWork.TabIndex = 115
        '
        'cmbUnit
        '
        Me.cmbUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbUnit.Location = New System.Drawing.Point(84, 16)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbUnit.Size = New System.Drawing.Size(72, 20)
        Me.cmbUnit.TabIndex = 113
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatus.Location = New System.Drawing.Point(8, 16)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatus.Size = New System.Drawing.Size(72, 20)
        Me.cmbStatus.TabIndex = 112
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lstMsg)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 116)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(591, 180)
        Me.GroupBox3.TabIndex = 117
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Execute Log"
        '
        'lstMsg
        '
        Me.lstMsg.FormattingEnabled = True
        Me.lstMsg.ItemHeight = 12
        Me.lstMsg.Location = New System.Drawing.Point(20, 20)
        Me.lstMsg.Name = "lstMsg"
        Me.lstMsg.Size = New System.Drawing.Size(556, 148)
        Me.lstMsg.TabIndex = 13
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmFileConvert
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(614, 340)
        Me.Controls.Add(Me.prgBar)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox9)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmbUpdate)
        Me.Controls.Add(Me.cmbVersion)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.cmdConvert)
        Me.Controls.Add(Me.cmdCancel)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFileConvert"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "22K CONVERTER"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents cmbVersion As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUpdate As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents prgBar As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Public WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Public WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lstMsg As System.Windows.Forms.ListBox
    Public WithEvents cmbWork As System.Windows.Forms.ComboBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
#End Region

End Class
