<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChOutputDoDetail
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
    Public WithEvents fraOutputAddress As System.Windows.Forms.GroupBox


    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents fraCylinderCH As System.Windows.Forms.GroupBox
    Public WithEvents cmbOutputMovement As System.Windows.Forms.ComboBox
    Public WithEvents cmbChOutType As System.Windows.Forms.ComboBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblNo As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblRowNo As System.Windows.Forms.Label


    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.fraOutputAddress = New System.Windows.Forms.GroupBox()
        Me.txtPin = New System.Windows.Forms.TextBox()
        Me.txtPortNo = New System.Windows.Forms.TextBox()
        Me.txtFuNo = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.fraCylinderCH = New System.Windows.Forms.GroupBox()
        Me.grdChNo1 = New Editor.clsDataGridViewPlus()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbOutputMovement = New System.Windows.Forms.ComboBox()
        Me.cmbChOutType = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblNo = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblRowNo = New System.Windows.Forms.Label()
        Me.chkMaskOR = New System.Windows.Forms.CheckBox()
        Me.chkMaskAnd = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grdOutputMovement = New Editor.clsDataGridViewPlus()
        Me.fraOutputAddress.SuspendLayout()
        Me.fraCylinderCH.SuspendLayout()
        CType(Me.grdChNo1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdOutputMovement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(176, 464)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 7
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(300, 464)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'fraOutputAddress
        '
        Me.fraOutputAddress.BackColor = System.Drawing.SystemColors.Control
        Me.fraOutputAddress.Controls.Add(Me.txtPin)
        Me.fraOutputAddress.Controls.Add(Me.txtPortNo)
        Me.fraOutputAddress.Controls.Add(Me.txtFuNo)
        Me.fraOutputAddress.Controls.Add(Me.Label21)
        Me.fraOutputAddress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraOutputAddress.Location = New System.Drawing.Point(248, 308)
        Me.fraOutputAddress.Name = "fraOutputAddress"
        Me.fraOutputAddress.Padding = New System.Windows.Forms.Padding(0)
        Me.fraOutputAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraOutputAddress.Size = New System.Drawing.Size(152, 85)
        Me.fraOutputAddress.TabIndex = 6
        Me.fraOutputAddress.TabStop = False
        Me.fraOutputAddress.Text = "Output"
        '
        'txtPin
        '
        Me.txtPin.AcceptsReturn = True
        Me.txtPin.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPin.Enabled = False
        Me.txtPin.Location = New System.Drawing.Point(100, 44)
        Me.txtPin.MaxLength = 0
        Me.txtPin.Name = "txtPin"
        Me.txtPin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPin.Size = New System.Drawing.Size(40, 19)
        Me.txtPin.TabIndex = 93
        '
        'txtPortNo
        '
        Me.txtPortNo.AcceptsReturn = True
        Me.txtPortNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPortNo.Enabled = False
        Me.txtPortNo.Location = New System.Drawing.Point(58, 44)
        Me.txtPortNo.MaxLength = 0
        Me.txtPortNo.Name = "txtPortNo"
        Me.txtPortNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNo.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNo.TabIndex = 92
        '
        'txtFuNo
        '
        Me.txtFuNo.AcceptsReturn = True
        Me.txtFuNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFuNo.Enabled = False
        Me.txtFuNo.Location = New System.Drawing.Point(16, 44)
        Me.txtFuNo.MaxLength = 0
        Me.txtFuNo.Name = "txtFuNo"
        Me.txtFuNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNo.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNo.TabIndex = 91
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(16, 28)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(65, 12)
        Me.Label21.TabIndex = 90
        Me.Label21.Text = "FU Address"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraCylinderCH
        '
        Me.fraCylinderCH.BackColor = System.Drawing.SystemColors.Control
        Me.fraCylinderCH.Controls.Add(Me.grdChNo1)
        Me.fraCylinderCH.Controls.Add(Me.Label14)
        Me.fraCylinderCH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCylinderCH.Location = New System.Drawing.Point(20, 124)
        Me.fraCylinderCH.Name = "fraCylinderCH"
        Me.fraCylinderCH.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCylinderCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCylinderCH.Size = New System.Drawing.Size(212, 332)
        Me.fraCylinderCH.TabIndex = 5
        Me.fraCylinderCH.TabStop = False
        Me.fraCylinderCH.Text = "Source CH No."
        '
        'grdChNo1
        '
        Me.grdChNo1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChNo1.Location = New System.Drawing.Point(44, 44)
        Me.grdChNo1.Name = "grdChNo1"
        Me.grdChNo1.RowTemplate.Height = 21
        Me.grdChNo1.Size = New System.Drawing.Size(131, 271)
        Me.grdChNo1.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(16, 24)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(179, 12)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "If OR Nocheck 2-24 is Invalid"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label14.Visible = False
        '
        'cmbOutputMovement
        '
        Me.cmbOutputMovement.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOutputMovement.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOutputMovement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOutputMovement.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOutputMovement.Location = New System.Drawing.Point(117, 76)
        Me.cmbOutputMovement.Name = "cmbOutputMovement"
        Me.cmbOutputMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOutputMovement.Size = New System.Drawing.Size(120, 20)
        Me.cmbOutputMovement.TabIndex = 3
        '
        'cmbChOutType
        '
        Me.cmbChOutType.BackColor = System.Drawing.SystemColors.Window
        Me.cmbChOutType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbChOutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChOutType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbChOutType.Location = New System.Drawing.Point(132, 24)
        Me.cmbChOutType.Name = "cmbChOutType"
        Me.cmbChOutType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbChOutType.Size = New System.Drawing.Size(105, 20)
        Me.cmbChOutType.TabIndex = 0
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(16, 28)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(101, 12)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "CHOUT Type Setup"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNo
        '
        Me.lblNo.AutoSize = True
        Me.lblNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNo.Location = New System.Drawing.Point(52, 7)
        Me.lblNo.Name = "lblNo"
        Me.lblNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNo.Size = New System.Drawing.Size(23, 12)
        Me.lblNo.TabIndex = 4
        Me.lblNo.Text = "001"
        Me.lblNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(16, 79)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(95, 12)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Output Movement"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRowNo
        '
        Me.lblRowNo.AutoSize = True
        Me.lblRowNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblRowNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRowNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRowNo.Location = New System.Drawing.Point(20, 7)
        Me.lblRowNo.Name = "lblRowNo"
        Me.lblRowNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRowNo.Size = New System.Drawing.Size(23, 12)
        Me.lblRowNo.TabIndex = 1
        Me.lblRowNo.Text = "No."
        Me.lblRowNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkMaskOR
        '
        Me.chkMaskOR.AutoSize = True
        Me.chkMaskOR.Location = New System.Drawing.Point(252, 24)
        Me.chkMaskOR.Name = "chkMaskOR"
        Me.chkMaskOR.Size = New System.Drawing.Size(36, 16)
        Me.chkMaskOR.TabIndex = 1
        Me.chkMaskOR.Text = "OR"
        Me.chkMaskOR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkMaskOR.UseVisualStyleBackColor = True
        '
        'chkMaskAnd
        '
        Me.chkMaskAnd.AutoSize = True
        Me.chkMaskAnd.Location = New System.Drawing.Point(252, 48)
        Me.chkMaskAnd.Name = "chkMaskAnd"
        Me.chkMaskAnd.Size = New System.Drawing.Size(42, 16)
        Me.chkMaskAnd.TabIndex = 2
        Me.chkMaskAnd.Text = "AND"
        Me.chkMaskAnd.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbStatus)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(252, 408)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(152, 40)
        Me.GroupBox1.TabIndex = 10
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
        Me.cmbStatus.Location = New System.Drawing.Point(80, 16)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatus.Size = New System.Drawing.Size(64, 20)
        Me.cmbStatus.TabIndex = 112
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(5, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(71, 12)
        Me.Label1.TabIndex = 113
        Me.Label1.Text = "MotorStatus"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdOutputMovement
        '
        Me.grdOutputMovement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdOutputMovement.Location = New System.Drawing.Point(256, 76)
        Me.grdOutputMovement.Name = "grdOutputMovement"
        Me.grdOutputMovement.RowTemplate.Height = 21
        Me.grdOutputMovement.Size = New System.Drawing.Size(143, 213)
        Me.grdOutputMovement.TabIndex = 4
        '
        'frmChOutputDoDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(424, 506)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkMaskAnd)
        Me.Controls.Add(Me.chkMaskOR)
        Me.Controls.Add(Me.grdOutputMovement)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.fraOutputAddress)
        Me.Controls.Add(Me.fraCylinderCH)
        Me.Controls.Add(Me.cmbOutputMovement)
        Me.Controls.Add(Me.cmbChOutType)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblNo)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblRowNo)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChOutputDoDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "DO DETAILS"
        Me.fraOutputAddress.ResumeLayout(False)
        Me.fraOutputAddress.PerformLayout()
        Me.fraCylinderCH.ResumeLayout(False)
        Me.fraCylinderCH.PerformLayout()
        CType(Me.grdChNo1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdOutputMovement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkMaskAnd As System.Windows.Forms.CheckBox
    Friend WithEvents chkMaskOR As System.Windows.Forms.CheckBox
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents txtPin As System.Windows.Forms.TextBox
    Public WithEvents txtPortNo As System.Windows.Forms.TextBox
    Public WithEvents txtFuNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdChNo1 As Editor.clsDataGridViewPlus
    Friend WithEvents grdOutputMovement As Editor.clsDataGridViewPlus
#End Region

End Class
