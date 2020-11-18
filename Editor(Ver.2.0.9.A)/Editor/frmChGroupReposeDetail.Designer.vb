<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChGroupReposeDetail
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

    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdOnRepose6 As System.Windows.Forms.Button
    Public WithEvents cmdOffRepose6 As System.Windows.Forms.Button
    Public WithEvents cmdOnRepose5 As System.Windows.Forms.Button
    Public WithEvents cmdOffRepose5 As System.Windows.Forms.Button
    Public WithEvents cmdOnRepose4 As System.Windows.Forms.Button
    Public WithEvents cmdOffRepose4 As System.Windows.Forms.Button
    Public WithEvents cmdOnRepose3 As System.Windows.Forms.Button
    Public WithEvents cmdOffRepose3 As System.Windows.Forms.Button
    Public WithEvents cmdOnRepose2 As System.Windows.Forms.Button
    Public WithEvents cmdOffRepose2 As System.Windows.Forms.Button
    Public WithEvents cmdOnRepose1 As System.Windows.Forms.Button
    Public WithEvents cmdOffRepose1 As System.Windows.Forms.Button

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdOnRepose6 = New System.Windows.Forms.Button()
        Me.cmdOffRepose6 = New System.Windows.Forms.Button()
        Me.cmdOnRepose5 = New System.Windows.Forms.Button()
        Me.cmdOffRepose5 = New System.Windows.Forms.Button()
        Me.cmdOnRepose4 = New System.Windows.Forms.Button()
        Me.cmdOffRepose4 = New System.Windows.Forms.Button()
        Me.cmdOnRepose3 = New System.Windows.Forms.Button()
        Me.cmdOffRepose3 = New System.Windows.Forms.Button()
        Me.cmdOnRepose2 = New System.Windows.Forms.Button()
        Me.cmdOffRepose2 = New System.Windows.Forms.Button()
        Me.cmdOnRepose1 = New System.Windows.Forms.Button()
        Me.cmdOffRepose1 = New System.Windows.Forms.Button()
        Me.lblIdNo = New System.Windows.Forms.Label()
        Me.lblID = New System.Windows.Forms.Label()
        Me.grdHead = New Editor.clsDataGridViewPlus()
        Me.grdRepose = New Editor.clsDataGridViewPlus()
        CType(Me.grdHead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRepose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(548, 371)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 0
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(424, 371)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 13
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdOnRepose6
        '
        Me.cmdOnRepose6.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOnRepose6.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOnRepose6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOnRepose6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOnRepose6.Location = New System.Drawing.Point(488, 309)
        Me.cmdOnRepose6.Name = "cmdOnRepose6"
        Me.cmdOnRepose6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOnRepose6.Size = New System.Drawing.Size(172, 24)
        Me.cmdOnRepose6.TabIndex = 11
        Me.cmdOnRepose6.Text = "Expect for the RUN,REPOSE"
        Me.cmdOnRepose6.UseVisualStyleBackColor = True
        '
        'cmdOffRepose6
        '
        Me.cmdOffRepose6.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOffRepose6.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOffRepose6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOffRepose6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOffRepose6.Location = New System.Drawing.Point(488, 333)
        Me.cmdOffRepose6.Name = "cmdOffRepose6"
        Me.cmdOffRepose6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOffRepose6.Size = New System.Drawing.Size(172, 24)
        Me.cmdOffRepose6.TabIndex = 12
        Me.cmdOffRepose6.Text = "OFF time REPOSE"
        Me.cmdOffRepose6.UseVisualStyleBackColor = True
        '
        'cmdOnRepose5
        '
        Me.cmdOnRepose5.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOnRepose5.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOnRepose5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOnRepose5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOnRepose5.Location = New System.Drawing.Point(488, 261)
        Me.cmdOnRepose5.Name = "cmdOnRepose5"
        Me.cmdOnRepose5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOnRepose5.Size = New System.Drawing.Size(172, 24)
        Me.cmdOnRepose5.TabIndex = 9
        Me.cmdOnRepose5.Text = "Expect for the RUN,REPOSE"
        Me.cmdOnRepose5.UseVisualStyleBackColor = True
        '
        'cmdOffRepose5
        '
        Me.cmdOffRepose5.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOffRepose5.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOffRepose5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOffRepose5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOffRepose5.Location = New System.Drawing.Point(488, 285)
        Me.cmdOffRepose5.Name = "cmdOffRepose5"
        Me.cmdOffRepose5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOffRepose5.Size = New System.Drawing.Size(172, 24)
        Me.cmdOffRepose5.TabIndex = 10
        Me.cmdOffRepose5.Text = "OFF time REPOSE"
        Me.cmdOffRepose5.UseVisualStyleBackColor = True
        '
        'cmdOnRepose4
        '
        Me.cmdOnRepose4.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOnRepose4.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOnRepose4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOnRepose4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOnRepose4.Location = New System.Drawing.Point(488, 213)
        Me.cmdOnRepose4.Name = "cmdOnRepose4"
        Me.cmdOnRepose4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOnRepose4.Size = New System.Drawing.Size(172, 24)
        Me.cmdOnRepose4.TabIndex = 7
        Me.cmdOnRepose4.Text = "Expect for the RUN,REPOSE"
        Me.cmdOnRepose4.UseVisualStyleBackColor = True
        '
        'cmdOffRepose4
        '
        Me.cmdOffRepose4.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOffRepose4.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOffRepose4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOffRepose4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOffRepose4.Location = New System.Drawing.Point(488, 237)
        Me.cmdOffRepose4.Name = "cmdOffRepose4"
        Me.cmdOffRepose4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOffRepose4.Size = New System.Drawing.Size(172, 24)
        Me.cmdOffRepose4.TabIndex = 8
        Me.cmdOffRepose4.Text = "OFF time REPOSE"
        Me.cmdOffRepose4.UseVisualStyleBackColor = True
        '
        'cmdOnRepose3
        '
        Me.cmdOnRepose3.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOnRepose3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOnRepose3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOnRepose3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOnRepose3.Location = New System.Drawing.Point(488, 165)
        Me.cmdOnRepose3.Name = "cmdOnRepose3"
        Me.cmdOnRepose3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOnRepose3.Size = New System.Drawing.Size(172, 24)
        Me.cmdOnRepose3.TabIndex = 5
        Me.cmdOnRepose3.Text = "Expect for the RUN,REPOSE"
        Me.cmdOnRepose3.UseVisualStyleBackColor = True
        '
        'cmdOffRepose3
        '
        Me.cmdOffRepose3.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOffRepose3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOffRepose3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOffRepose3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOffRepose3.Location = New System.Drawing.Point(488, 189)
        Me.cmdOffRepose3.Name = "cmdOffRepose3"
        Me.cmdOffRepose3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOffRepose3.Size = New System.Drawing.Size(172, 24)
        Me.cmdOffRepose3.TabIndex = 6
        Me.cmdOffRepose3.Text = "OFF time REPOSE"
        Me.cmdOffRepose3.UseVisualStyleBackColor = True
        '
        'cmdOnRepose2
        '
        Me.cmdOnRepose2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOnRepose2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOnRepose2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOnRepose2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOnRepose2.Location = New System.Drawing.Point(488, 117)
        Me.cmdOnRepose2.Name = "cmdOnRepose2"
        Me.cmdOnRepose2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOnRepose2.Size = New System.Drawing.Size(172, 24)
        Me.cmdOnRepose2.TabIndex = 3
        Me.cmdOnRepose2.Text = "Expect for the RUN,REPOSE"
        Me.cmdOnRepose2.UseVisualStyleBackColor = True
        '
        'cmdOffRepose2
        '
        Me.cmdOffRepose2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOffRepose2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOffRepose2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOffRepose2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOffRepose2.Location = New System.Drawing.Point(488, 141)
        Me.cmdOffRepose2.Name = "cmdOffRepose2"
        Me.cmdOffRepose2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOffRepose2.Size = New System.Drawing.Size(172, 24)
        Me.cmdOffRepose2.TabIndex = 4
        Me.cmdOffRepose2.Text = "OFF time REPOSE"
        Me.cmdOffRepose2.UseVisualStyleBackColor = True
        '
        'cmdOnRepose1
        '
        Me.cmdOnRepose1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOnRepose1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOnRepose1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOnRepose1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOnRepose1.Location = New System.Drawing.Point(488, 69)
        Me.cmdOnRepose1.Name = "cmdOnRepose1"
        Me.cmdOnRepose1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOnRepose1.Size = New System.Drawing.Size(172, 24)
        Me.cmdOnRepose1.TabIndex = 1
        Me.cmdOnRepose1.Text = "Expect for the RUN,REPOSE"
        Me.cmdOnRepose1.UseVisualStyleBackColor = True
        '
        'cmdOffRepose1
        '
        Me.cmdOffRepose1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOffRepose1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOffRepose1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOffRepose1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOffRepose1.Location = New System.Drawing.Point(488, 93)
        Me.cmdOffRepose1.Name = "cmdOffRepose1"
        Me.cmdOffRepose1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOffRepose1.Size = New System.Drawing.Size(172, 24)
        Me.cmdOffRepose1.TabIndex = 2
        Me.cmdOffRepose1.Text = "OFF time REPOSE"
        Me.cmdOffRepose1.UseVisualStyleBackColor = True
        '
        'lblIdNo
        '
        Me.lblIdNo.AutoSize = True
        Me.lblIdNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblIdNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIdNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIdNo.Location = New System.Drawing.Point(41, 15)
        Me.lblIdNo.Name = "lblIdNo"
        Me.lblIdNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIdNo.Size = New System.Drawing.Size(29, 12)
        Me.lblIdNo.TabIndex = 59
        Me.lblIdNo.Text = "1111"
        Me.lblIdNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.BackColor = System.Drawing.SystemColors.Control
        Me.lblID.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblID.Location = New System.Drawing.Point(19, 15)
        Me.lblID.Name = "lblID"
        Me.lblID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblID.Size = New System.Drawing.Size(23, 12)
        Me.lblID.TabIndex = 58
        Me.lblID.Text = "No."
        Me.lblID.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdHead
        '
        Me.grdHead.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdHead.Location = New System.Drawing.Point(16, 32)
        Me.grdHead.Name = "grdHead"
        Me.grdHead.RowTemplate.Height = 21
        Me.grdHead.Size = New System.Drawing.Size(468, 19)
        Me.grdHead.TabIndex = 31
        Me.grdHead.TabStop = False
        '
        'grdRepose
        '
        Me.grdRepose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdRepose.Location = New System.Drawing.Point(16, 49)
        Me.grdRepose.Name = "grdRepose"
        Me.grdRepose.RowTemplate.Height = 21
        Me.grdRepose.Size = New System.Drawing.Size(468, 308)
        Me.grdRepose.TabIndex = 32
        '
        'frmChGroupReposeDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(677, 415)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblIdNo)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.grdHead)
        Me.Controls.Add(Me.grdRepose)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdOnRepose6)
        Me.Controls.Add(Me.cmdOffRepose6)
        Me.Controls.Add(Me.cmdOnRepose5)
        Me.Controls.Add(Me.cmdOffRepose5)
        Me.Controls.Add(Me.cmdOnRepose4)
        Me.Controls.Add(Me.cmdOffRepose4)
        Me.Controls.Add(Me.cmdOnRepose3)
        Me.Controls.Add(Me.cmdOffRepose3)
        Me.Controls.Add(Me.cmdOnRepose2)
        Me.Controls.Add(Me.cmdOffRepose2)
        Me.Controls.Add(Me.cmdOnRepose1)
        Me.Controls.Add(Me.cmdOffRepose1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChGroupReposeDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "GROUP REPOSE SET DETAILS"
        CType(Me.grdHead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRepose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents lblIdNo As System.Windows.Forms.Label
    Public WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents grdRepose As Editor.clsDataGridViewPlus
    Friend WithEvents grdHead As Editor.clsDataGridViewPlus
#End Region

End Class
