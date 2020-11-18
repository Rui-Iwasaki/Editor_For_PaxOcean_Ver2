<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChListChannelList
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

    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmbGroup As System.Windows.Forms.ComboBox

    Public WithEvents lblGroupNo As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmbGroup = New System.Windows.Forms.ComboBox()
        Me.lblGroupNo = New System.Windows.Forms.Label()
        Me.grdHead1 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdCopy = New System.Windows.Forms.Button()
        Me.cmdPast = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.cmdCsvOut = New System.Windows.Forms.Button()
        Me.txtDummy = New System.Windows.Forms.TextBox()
        Me.tmrShow = New System.Windows.Forms.Timer(Me.components)
        Me.lblDummy = New System.Windows.Forms.Label()
        Me.btnPrintPrev = New System.Windows.Forms.Button()
        Me.cmbRangeType = New System.Windows.Forms.ComboBox()
        Me.cmdErrCHK = New System.Windows.Forms.Button()
        Me.cmdShareChk = New System.Windows.Forms.Button()
        Me.grdCHList = New Editor.clsDataGridViewPlus()
        CType(Me.grdHead1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdCHList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(1006, 639)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 4
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(1139, 639)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 5
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmbGroup
        '
        Me.cmbGroup.BackColor = System.Drawing.SystemColors.Window
        Me.cmbGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGroup.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbGroup.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbGroup.Location = New System.Drawing.Point(52, 12)
        Me.cmbGroup.Name = "cmbGroup"
        Me.cmbGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbGroup.Size = New System.Drawing.Size(716, 20)
        Me.cmbGroup.TabIndex = 1
        '
        'lblGroupNo
        '
        Me.lblGroupNo.AutoSize = True
        Me.lblGroupNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblGroupNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblGroupNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroupNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGroupNo.Location = New System.Drawing.Point(12, 15)
        Me.lblGroupNo.Name = "lblGroupNo"
        Me.lblGroupNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblGroupNo.Size = New System.Drawing.Size(35, 12)
        Me.lblGroupNo.TabIndex = 1
        Me.lblGroupNo.Text = "Group"
        Me.lblGroupNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdHead1
        '
        Me.grdHead1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdHead1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdHead1.Location = New System.Drawing.Point(8, 43)
        Me.grdHead1.Name = "grdHead1"
        Me.grdHead1.RowTemplate.Height = 21
        Me.grdHead1.Size = New System.Drawing.Size(1244, 17)
        Me.grdHead1.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(1354, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(19, 18)
        Me.Label1.TabIndex = 6
        '
        'cmdCopy
        '
        Me.cmdCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdCopy.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCopy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCopy.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCopy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCopy.Location = New System.Drawing.Point(384, 639)
        Me.cmdCopy.Name = "cmdCopy"
        Me.cmdCopy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCopy.Size = New System.Drawing.Size(113, 33)
        Me.cmdCopy.TabIndex = 2
        Me.cmdCopy.Text = "Line Copy"
        Me.cmdCopy.UseVisualStyleBackColor = True
        Me.cmdCopy.Visible = False
        '
        'cmdPast
        '
        Me.cmdPast.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdPast.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPast.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPast.Enabled = False
        Me.cmdPast.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPast.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPast.Location = New System.Drawing.Point(504, 639)
        Me.cmdPast.Name = "cmdPast"
        Me.cmdPast.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPast.Size = New System.Drawing.Size(113, 33)
        Me.cmdPast.TabIndex = 3
        Me.cmdPast.Text = "Past"
        Me.cmdPast.UseVisualStyleBackColor = True
        Me.cmdPast.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbStatus)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.cmbUnit)
        Me.GroupBox1.Controls.Add(Me.Label38)
        Me.GroupBox1.Location = New System.Drawing.Point(456, 524)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(281, 44)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Visible False"
        Me.GroupBox1.Visible = False
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatus.Location = New System.Drawing.Point(171, 20)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatus.Size = New System.Drawing.Size(68, 20)
        Me.cmbStatus.TabIndex = 112
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(124, 24)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(41, 12)
        Me.Label14.TabIndex = 113
        Me.Label14.Text = "Status"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbUnit
        '
        Me.cmbUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbUnit.Location = New System.Drawing.Point(48, 20)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbUnit.Size = New System.Drawing.Size(52, 20)
        Me.cmbUnit.TabIndex = 111
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(12, 22)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(29, 12)
        Me.Label38.TabIndex = 110
        Me.Label38.Text = "Unit"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdCsvOut
        '
        Me.cmdCsvOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdCsvOut.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCsvOut.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCsvOut.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCsvOut.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCsvOut.Location = New System.Drawing.Point(8, 639)
        Me.cmdCsvOut.Name = "cmdCsvOut"
        Me.cmdCsvOut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCsvOut.Size = New System.Drawing.Size(113, 33)
        Me.cmdCsvOut.TabIndex = 3
        Me.cmdCsvOut.Text = "Csv"
        Me.cmdCsvOut.UseVisualStyleBackColor = True
        '
        'txtDummy
        '
        Me.txtDummy.Location = New System.Drawing.Point(780, 50)
        Me.txtDummy.Name = "txtDummy"
        Me.txtDummy.Size = New System.Drawing.Size(40, 19)
        Me.txtDummy.TabIndex = 0
        '
        'tmrShow
        '
        '
        'lblDummy
        '
        Me.lblDummy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDummy.AutoSize = True
        Me.lblDummy.Location = New System.Drawing.Point(1157, 15)
        Me.lblDummy.Name = "lblDummy"
        Me.lblDummy.Size = New System.Drawing.Size(95, 12)
        Me.lblDummy.TabIndex = 10
        Me.lblDummy.Text = "F5:DummySetting"
        '
        'btnPrintPrev
        '
        Me.btnPrintPrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrintPrev.Location = New System.Drawing.Point(746, 639)
        Me.btnPrintPrev.Name = "btnPrintPrev"
        Me.btnPrintPrev.Size = New System.Drawing.Size(174, 33)
        Me.btnPrintPrev.TabIndex = 11
        Me.btnPrintPrev.Text = "Print"
        Me.btnPrintPrev.UseVisualStyleBackColor = True
        '
        'cmbRangeType
        '
        Me.cmbRangeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRangeType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbRangeType.FormattingEnabled = True
        Me.cmbRangeType.Location = New System.Drawing.Point(627, 364)
        Me.cmbRangeType.Name = "cmbRangeType"
        Me.cmbRangeType.Size = New System.Drawing.Size(157, 20)
        Me.cmbRangeType.TabIndex = 15
        '
        'cmdErrCHK
        '
        Me.cmdErrCHK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdErrCHK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdErrCHK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdErrCHK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdErrCHK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdErrCHK.Location = New System.Drawing.Point(127, 639)
        Me.cmdErrCHK.Name = "cmdErrCHK"
        Me.cmdErrCHK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdErrCHK.Size = New System.Drawing.Size(113, 33)
        Me.cmdErrCHK.TabIndex = 16
        Me.cmdErrCHK.Text = "Err Chk"
        Me.cmdErrCHK.UseVisualStyleBackColor = True
        '
        'cmdShareChk
        '
        Me.cmdShareChk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdShareChk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShareChk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShareChk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShareChk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShareChk.Location = New System.Drawing.Point(246, 639)
        Me.cmdShareChk.Name = "cmdShareChk"
        Me.cmdShareChk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShareChk.Size = New System.Drawing.Size(113, 33)
        Me.cmdShareChk.TabIndex = 17
        Me.cmdShareChk.Text = "Share Chk"
        Me.cmdShareChk.UseVisualStyleBackColor = True
        Me.cmdShareChk.Visible = False
        '
        'grdCHList
        '
        Me.grdCHList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCHList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCHList.Enabled = False
        Me.grdCHList.Location = New System.Drawing.Point(8, 60)
        Me.grdCHList.Name = "grdCHList"
        Me.grdCHList.RowTemplate.Height = 21
        Me.grdCHList.ShowCellToolTips = False
        Me.grdCHList.Size = New System.Drawing.Size(1244, 568)
        Me.grdCHList.TabIndex = 2
        '
        'frmChListChannelList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(1264, 682)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdShareChk)
        Me.Controls.Add(Me.cmdErrCHK)
        Me.Controls.Add(Me.btnPrintPrev)
        Me.Controls.Add(Me.grdCHList)
        Me.Controls.Add(Me.lblDummy)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdHead1)
        Me.Controls.Add(Me.cmdCsvOut)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdPast)
        Me.Controls.Add(Me.cmdCopy)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmbGroup)
        Me.Controls.Add(Me.lblGroupNo)
        Me.Controls.Add(Me.txtDummy)
        Me.Controls.Add(Me.cmbRangeType)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChListChannelList"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CHANNEL LIST"
        CType(Me.grdHead1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdCHList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdHead1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cmdCopy As System.Windows.Forms.Button
    Public WithEvents cmdPast As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents cmdCsvOut As System.Windows.Forms.Button
    Friend WithEvents grdCHList As Editor.clsDataGridViewPlus
    Friend WithEvents txtDummy As System.Windows.Forms.TextBox
    Friend WithEvents tmrShow As System.Windows.Forms.Timer
    Friend WithEvents lblDummy As System.Windows.Forms.Label
    Friend WithEvents btnPrintPrev As System.Windows.Forms.Button
    Friend WithEvents cmbRangeType As System.Windows.Forms.ComboBox
    Public WithEvents cmdErrCHK As System.Windows.Forms.Button
    Public WithEvents cmdShareChk As System.Windows.Forms.Button
#End Region

End Class
