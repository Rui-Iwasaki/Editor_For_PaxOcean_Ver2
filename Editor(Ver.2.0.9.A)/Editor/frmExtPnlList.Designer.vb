<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExtPnlList
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

    Public WithEvents cmbEngineerCall As System.Windows.Forms.ComboBox
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmbEngineerCall = New System.Windows.Forms.ComboBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbCE = New System.Windows.Forms.ComboBox()
        Me.cmbPR = New System.Windows.Forms.ComboBox()
        Me.cmbWH = New System.Windows.Forms.ComboBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtComm3 = New System.Windows.Forms.TextBox()
        Me.txtComm2 = New System.Windows.Forms.TextBox()
        Me.txtComm1 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.grdEXT = New Editor.clsDataGridViewPlus()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdEXT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbEngineerCall
        '
        Me.cmbEngineerCall.BackColor = System.Drawing.SystemColors.Window
        Me.cmbEngineerCall.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbEngineerCall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEngineerCall.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbEngineerCall.Location = New System.Drawing.Point(881, 70)
        Me.cmbEngineerCall.Name = "cmbEngineerCall"
        Me.cmbEngineerCall.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbEngineerCall.Size = New System.Drawing.Size(80, 20)
        Me.cmbEngineerCall.TabIndex = 1
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(762, 556)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 3
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(881, 556)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 4
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(847, 45)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(149, 12)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Engineer All Call SW No."
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(12, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(167, 12)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Special Specifications Only"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbCE)
        Me.GroupBox1.Controls.Add(Me.cmbPR)
        Me.GroupBox1.Controls.Add(Me.cmbWH)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(340, 54)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Gainsboro
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(245, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(92, 19)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "C/E"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Gainsboro
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(152, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(92, 19)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "P/R"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Gainsboro
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(59, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(92, 19)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "W/H"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Gainsboro
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(2, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(56, 23)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "EXT"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbCE
        '
        Me.cmbCE.BackColor = System.Drawing.SystemColors.Window
        Me.cmbCE.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbCE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCE.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbCE.Location = New System.Drawing.Point(245, 30)
        Me.cmbCE.Name = "cmbCE"
        Me.cmbCE.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbCE.Size = New System.Drawing.Size(92, 20)
        Me.cmbCE.TabIndex = 2
        '
        'cmbPR
        '
        Me.cmbPR.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPR.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPR.Location = New System.Drawing.Point(152, 30)
        Me.cmbPR.Name = "cmbPR"
        Me.cmbPR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPR.Size = New System.Drawing.Size(92, 20)
        Me.cmbPR.TabIndex = 1
        '
        'cmbWH
        '
        Me.cmbWH.BackColor = System.Drawing.SystemColors.Window
        Me.cmbWH.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbWH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbWH.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbWH.Location = New System.Drawing.Point(59, 30)
        Me.cmbWH.Name = "cmbWH"
        Me.cmbWH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbWH.Size = New System.Drawing.Size(92, 20)
        Me.cmbWH.TabIndex = 0
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(12, 556)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(113, 33)
        Me.cmdPrint.TabIndex = 35
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(422, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(107, 12)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Common Option Set"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtComm3)
        Me.GroupBox2.Controls.Add(Me.txtComm2)
        Me.GroupBox2.Controls.Add(Me.txtComm1)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(426, 36)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(247, 54)
        Me.GroupBox2.TabIndex = 37
        Me.GroupBox2.TabStop = False
        '
        'txtComm3
        '
        Me.txtComm3.Location = New System.Drawing.Point(187, 33)
        Me.txtComm3.Name = "txtComm3"
        Me.txtComm3.Size = New System.Drawing.Size(54, 19)
        Me.txtComm3.TabIndex = 39
        Me.txtComm3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtComm2
        '
        Me.txtComm2.Location = New System.Drawing.Point(127, 33)
        Me.txtComm2.Name = "txtComm2"
        Me.txtComm2.Size = New System.Drawing.Size(54, 19)
        Me.txtComm2.TabIndex = 38
        Me.txtComm2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtComm1
        '
        Me.txtComm1.Location = New System.Drawing.Point(67, 33)
        Me.txtComm1.Name = "txtComm1"
        Me.txtComm1.Size = New System.Drawing.Size(54, 19)
        Me.txtComm1.TabIndex = 37
        Me.txtComm1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Gainsboro
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(187, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(54, 19)
        Me.Label9.TabIndex = 36
        Me.Label9.Text = "COMM3"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Gainsboro
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(127, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(54, 19)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "COMM2"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Gainsboro
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(67, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(54, 19)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "COMM1"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Gainsboro
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(5, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(56, 18)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "SET NO"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdEXT
        '
        Me.grdEXT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdEXT.Location = New System.Drawing.Point(12, 104)
        Me.grdEXT.Name = "grdEXT"
        Me.grdEXT.RowTemplate.Height = 21
        Me.grdEXT.Size = New System.Drawing.Size(982, 439)
        Me.grdEXT.TabIndex = 2
        '
        'frmExtPnlList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(1010, 614)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grdEXT)
        Me.Controls.Add(Me.cmbEngineerCall)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExtPnlList"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "EXT PANEL SET"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdEXT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cmbCE As System.Windows.Forms.ComboBox
    Public WithEvents cmbPR As System.Windows.Forms.ComboBox
    Public WithEvents cmbWH As System.Windows.Forms.ComboBox
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents grdEXT As Editor.clsDataGridViewPlus
    Public WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtComm3 As System.Windows.Forms.TextBox
    Friend WithEvents txtComm2 As System.Windows.Forms.TextBox
    Friend WithEvents txtComm1 As System.Windows.Forms.TextBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
#End Region

End Class
