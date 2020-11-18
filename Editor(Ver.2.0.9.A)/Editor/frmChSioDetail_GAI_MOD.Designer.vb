<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChSioDetail_GAI_MOD
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

    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmbStopBit As System.Windows.Forms.ComboBox
    Public WithEvents txtInitialTransmit As System.Windows.Forms.TextBox
    Public WithEvents txtInitialTimeout As System.Windows.Forms.TextBox
    Public WithEvents cmbDuplet As System.Windows.Forms.ComboBox
    Public WithEvents cmbParityBit As System.Windows.Forms.ComboBox
    Public WithEvents cmbCom As System.Windows.Forms.ComboBox
    Public WithEvents Label113 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmbPort As System.Windows.Forms.ComboBox
    Public WithEvents Label11 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.frame1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbRetry = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbStopBit = New System.Windows.Forms.ComboBox()
        Me.txtInitialTransmit = New System.Windows.Forms.TextBox()
        Me.txtInitialTimeout = New System.Windows.Forms.TextBox()
        Me.cmbDuplet = New System.Windows.Forms.ComboBox()
        Me.cmbParityBit = New System.Windows.Forms.ComboBox()
        Me.cmbCom = New System.Windows.Forms.ComboBox()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbPort = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grdCH = New Editor.clsDataGridViewPlus()
        Me.frame1.SuspendLayout()
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(587, 628)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 500
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(711, 628)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 600
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'frame1
        '
        Me.frame1.BackColor = System.Drawing.SystemColors.Control
        Me.frame1.Controls.Add(Me.Label4)
        Me.frame1.Controls.Add(Me.Label3)
        Me.frame1.Controls.Add(Me.cmbRetry)
        Me.frame1.Controls.Add(Me.Label7)
        Me.frame1.Controls.Add(Me.Label6)
        Me.frame1.Controls.Add(Me.Label5)
        Me.frame1.Controls.Add(Me.Label1)
        Me.frame1.Controls.Add(Me.cmbStopBit)
        Me.frame1.Controls.Add(Me.txtInitialTransmit)
        Me.frame1.Controls.Add(Me.txtInitialTimeout)
        Me.frame1.Controls.Add(Me.cmbDuplet)
        Me.frame1.Controls.Add(Me.cmbParityBit)
        Me.frame1.Controls.Add(Me.cmbCom)
        Me.frame1.Controls.Add(Me.Label113)
        Me.frame1.Controls.Add(Me.Label18)
        Me.frame1.Controls.Add(Me.Label16)
        Me.frame1.Controls.Add(Me.Label15)
        Me.frame1.Controls.Add(Me.Label14)
        Me.frame1.Controls.Add(Me.Label13)
        Me.frame1.Controls.Add(Me.Label12)
        Me.frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frame1.Location = New System.Drawing.Point(16, 58)
        Me.frame1.Name = "frame1"
        Me.frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frame1.Size = New System.Drawing.Size(278, 513)
        Me.frame1.TabIndex = 2
        Me.frame1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(188, 189)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "msec"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(188, 164)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "msec"
        '
        'cmbRetry
        '
        Me.cmbRetry.BackColor = System.Drawing.SystemColors.Window
        Me.cmbRetry.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbRetry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRetry.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbRetry.Location = New System.Drawing.Point(118, 207)
        Me.cmbRetry.Name = "cmbRetry"
        Me.cmbRetry.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbRetry.Size = New System.Drawing.Size(97, 20)
        Me.cmbRetry.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(221, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(23, 12)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "bit"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(221, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(23, 12)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "bps"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(116, 128)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(35, 12)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "8 bit"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 210)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(71, 12)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Retry Count"
        '
        'cmbStopBit
        '
        Me.cmbStopBit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStopBit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStopBit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStopBit.Location = New System.Drawing.Point(118, 44)
        Me.cmbStopBit.Name = "cmbStopBit"
        Me.cmbStopBit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStopBit.Size = New System.Drawing.Size(97, 20)
        Me.cmbStopBit.TabIndex = 2
        '
        'txtInitialTransmit
        '
        Me.txtInitialTransmit.AcceptsReturn = True
        Me.txtInitialTransmit.Location = New System.Drawing.Point(118, 157)
        Me.txtInitialTransmit.MaxLength = 0
        Me.txtInitialTransmit.Name = "txtInitialTransmit"
        Me.txtInitialTransmit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInitialTransmit.Size = New System.Drawing.Size(64, 19)
        Me.txtInitialTransmit.TabIndex = 5
        Me.txtInitialTransmit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtInitialTimeout
        '
        Me.txtInitialTimeout.AcceptsReturn = True
        Me.txtInitialTimeout.Location = New System.Drawing.Point(118, 182)
        Me.txtInitialTimeout.MaxLength = 0
        Me.txtInitialTimeout.Name = "txtInitialTimeout"
        Me.txtInitialTimeout.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInitialTimeout.Size = New System.Drawing.Size(64, 19)
        Me.txtInitialTimeout.TabIndex = 6
        Me.txtInitialTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbDuplet
        '
        Me.cmbDuplet.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDuplet.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDuplet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDuplet.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDuplet.Location = New System.Drawing.Point(118, 96)
        Me.cmbDuplet.Name = "cmbDuplet"
        Me.cmbDuplet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDuplet.Size = New System.Drawing.Size(106, 20)
        Me.cmbDuplet.TabIndex = 4
        '
        'cmbParityBit
        '
        Me.cmbParityBit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbParityBit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbParityBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParityBit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbParityBit.Location = New System.Drawing.Point(118, 70)
        Me.cmbParityBit.Name = "cmbParityBit"
        Me.cmbParityBit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbParityBit.Size = New System.Drawing.Size(97, 20)
        Me.cmbParityBit.TabIndex = 3
        '
        'cmbCom
        '
        Me.cmbCom.BackColor = System.Drawing.SystemColors.Window
        Me.cmbCom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbCom.Location = New System.Drawing.Point(118, 18)
        Me.cmbCom.Name = "cmbCom"
        Me.cmbCom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbCom.Size = New System.Drawing.Size(97, 20)
        Me.cmbCom.TabIndex = 1
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.BackColor = System.Drawing.SystemColors.Control
        Me.Label113.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label113.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label113.Location = New System.Drawing.Point(7, 47)
        Me.Label113.Name = "Label113"
        Me.Label113.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label113.Size = New System.Drawing.Size(53, 12)
        Me.Label113.TabIndex = 32
        Me.Label113.Text = "Stop bit"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(8, 160)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(101, 12)
        Me.Label18.TabIndex = 15
        Me.Label18.Text = "Query send cycle"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(8, 185)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(101, 12)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "Response Timeout"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(8, 99)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(65, 12)
        Me.Label15.TabIndex = 12
        Me.Label15.Text = "Duplex set"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(8, 128)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(53, 12)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "Data bit"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(8, 73)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(65, 12)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Parity bit"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(8, 21)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(53, 12)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "Baudrate"
        '
        'cmbPort
        '
        Me.cmbPort.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPort.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPort.Location = New System.Drawing.Point(95, 32)
        Me.cmbPort.Name = "cmbPort"
        Me.cmbPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPort.Size = New System.Drawing.Size(97, 20)
        Me.cmbPort.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(24, 32)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(59, 12)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Port  No."
        '
        'grdCH
        '
        Me.grdCH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCH.Location = New System.Drawing.Point(320, 12)
        Me.grdCH.Name = "grdCH"
        Me.grdCH.RowTemplate.Height = 21
        Me.grdCH.Size = New System.Drawing.Size(504, 610)
        Me.grdCH.TabIndex = 37
        '
        'frmChSioDetail_GAI_MOD
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(841, 704)
        Me.ControlBox = False
        Me.Controls.Add(Me.grdCH)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.frame1)
        Me.Controls.Add(Me.cmbPort)
        Me.Controls.Add(Me.Label11)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChSioDetail_GAI_MOD"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SIO SETUP DETAILS(MODBUS-RTU)"
        Me.frame1.ResumeLayout(False)
        Me.frame1.PerformLayout()
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdCH As Editor.clsDataGridViewPlus
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents cmbRetry As System.Windows.Forms.ComboBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
#End Region

End Class
