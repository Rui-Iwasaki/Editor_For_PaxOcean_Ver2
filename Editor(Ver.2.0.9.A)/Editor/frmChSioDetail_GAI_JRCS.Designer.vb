<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChSioDetail_GAI_JRCS
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
    Public WithEvents txtUseallyTransmit As System.Windows.Forms.TextBox
    Public WithEvents cmbDataBit As System.Windows.Forms.ComboBox
    Public WithEvents cmbParityBit As System.Windows.Forms.ComboBox
    Public WithEvents cmbCom As System.Windows.Forms.ComboBox
    Public WithEvents Label113 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
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
        Me.chkON = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbStopBit = New System.Windows.Forms.ComboBox()
        Me.txtUseallyTransmit = New System.Windows.Forms.TextBox()
        Me.cmbDataBit = New System.Windows.Forms.ComboBox()
        Me.cmbParityBit = New System.Windows.Forms.ComboBox()
        Me.cmbCom = New System.Windows.Forms.ComboBox()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbPort = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
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
        Me.cmdOK.Location = New System.Drawing.Point(649, 339)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 39
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(773, 339)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 40
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'frame1
        '
        Me.frame1.BackColor = System.Drawing.SystemColors.Control
        Me.frame1.Controls.Add(Me.chkON)
        Me.frame1.Controls.Add(Me.Label3)
        Me.frame1.Controls.Add(Me.Label1)
        Me.frame1.Controls.Add(Me.cmbStopBit)
        Me.frame1.Controls.Add(Me.txtUseallyTransmit)
        Me.frame1.Controls.Add(Me.cmbDataBit)
        Me.frame1.Controls.Add(Me.cmbParityBit)
        Me.frame1.Controls.Add(Me.cmbCom)
        Me.frame1.Controls.Add(Me.Label113)
        Me.frame1.Controls.Add(Me.Label19)
        Me.frame1.Controls.Add(Me.Label14)
        Me.frame1.Controls.Add(Me.Label13)
        Me.frame1.Controls.Add(Me.Label12)
        Me.frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frame1.Location = New System.Drawing.Point(16, 64)
        Me.frame1.Name = "frame1"
        Me.frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frame1.Size = New System.Drawing.Size(420, 241)
        Me.frame1.TabIndex = 2
        Me.frame1.TabStop = False
        '
        'chkON
        '
        Me.chkON.AutoSize = True
        Me.chkON.Location = New System.Drawing.Point(334, 63)
        Me.chkON.Name = "chkON"
        Me.chkON.Size = New System.Drawing.Size(36, 16)
        Me.chkON.TabIndex = 45
        Me.chkON.Text = "ON"
        Me.chkON.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(197, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(71, 12)
        Me.Label3.TabIndex = 44
        Me.Label3.Text = "Sensor Fail"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(381, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "ms"
        '
        'cmbStopBit
        '
        Me.cmbStopBit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStopBit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStopBit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStopBit.Location = New System.Drawing.Point(79, 139)
        Me.cmbStopBit.Name = "cmbStopBit"
        Me.cmbStopBit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStopBit.Size = New System.Drawing.Size(97, 20)
        Me.cmbStopBit.TabIndex = 10
        '
        'txtUseallyTransmit
        '
        Me.txtUseallyTransmit.AcceptsReturn = True
        Me.txtUseallyTransmit.Location = New System.Drawing.Point(334, 22)
        Me.txtUseallyTransmit.MaxLength = 0
        Me.txtUseallyTransmit.Name = "txtUseallyTransmit"
        Me.txtUseallyTransmit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUseallyTransmit.Size = New System.Drawing.Size(64, 19)
        Me.txtUseallyTransmit.TabIndex = 21
        Me.txtUseallyTransmit.Text = "10000"
        Me.txtUseallyTransmit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbDataBit
        '
        Me.cmbDataBit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDataBit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataBit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDataBit.Location = New System.Drawing.Point(79, 99)
        Me.cmbDataBit.Name = "cmbDataBit"
        Me.cmbDataBit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDataBit.Size = New System.Drawing.Size(97, 20)
        Me.cmbDataBit.TabIndex = 9
        '
        'cmbParityBit
        '
        Me.cmbParityBit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbParityBit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbParityBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParityBit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbParityBit.Location = New System.Drawing.Point(79, 59)
        Me.cmbParityBit.Name = "cmbParityBit"
        Me.cmbParityBit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbParityBit.Size = New System.Drawing.Size(97, 20)
        Me.cmbParityBit.TabIndex = 7
        '
        'cmbCom
        '
        Me.cmbCom.BackColor = System.Drawing.SystemColors.Window
        Me.cmbCom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbCom.Location = New System.Drawing.Point(79, 19)
        Me.cmbCom.Name = "cmbCom"
        Me.cmbCom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbCom.Size = New System.Drawing.Size(97, 20)
        Me.cmbCom.TabIndex = 5
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.BackColor = System.Drawing.SystemColors.Control
        Me.Label113.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label113.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label113.Location = New System.Drawing.Point(7, 142)
        Me.Label113.Name = "Label113"
        Me.Label113.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label113.Size = New System.Drawing.Size(53, 12)
        Me.Label113.TabIndex = 32
        Me.Label113.Text = "Stop bit"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(197, 22)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(131, 24)
        Me.Label19.TabIndex = 16
        Me.Label19.Text = "Transmit waiting time" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(usually)"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(7, 102)
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
        Me.Label13.Location = New System.Drawing.Point(8, 62)
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
        Me.Label12.Location = New System.Drawing.Point(8, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(47, 12)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "COM bps"
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(441, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(95, 12)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "Transmission CH"
        '
        'grdCH
        '
        Me.grdCH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCH.Location = New System.Drawing.Point(444, 72)
        Me.grdCH.Name = "grdCH"
        Me.grdCH.RowTemplate.Height = 21
        Me.grdCH.Size = New System.Drawing.Size(450, 233)
        Me.grdCH.TabIndex = 37
        '
        'frmChSioDetail_GAI_JRCS
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(898, 404)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
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
        Me.Name = "frmChSioDetail_GAI_JRCS"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SIO SETUP DETAILS(JRCS STANDARD)"
        Me.frame1.ResumeLayout(False)
        Me.frame1.PerformLayout()
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grdCH As Editor.clsDataGridViewPlus
    Friend WithEvents chkON As System.Windows.Forms.CheckBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

End Class
