<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmToolChkChList
	Inherits System.Windows.Forms.Form

	'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
	<System.Diagnostics.DebuggerNonUserCode()>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Windows フォーム デザイナーで必要です。
	Private components As System.ComponentModel.IContainer

	'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
	'Windows フォーム デザイナーを使用して変更できます。  
	'コード エディターを使って変更しないでください。
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdGo = New System.Windows.Forms.Button()
        Me.txtCHNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAll = New System.Windows.Forms.Button()
        Me.btnOFF = New System.Windows.Forms.Button()
        Me.lblCompile = New System.Windows.Forms.Label()
        Me.grdChNo = New Editor.clsDataGridViewPlus()
        CType(Me.grdChNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(500, 616)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 4
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdGo
        '
        Me.cmdGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdGo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGo.Location = New System.Drawing.Point(381, 615)
        Me.cmdGo.Name = "cmdGo"
        Me.cmdGo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGo.Size = New System.Drawing.Size(113, 33)
        Me.cmdGo.TabIndex = 5
        Me.cmdGo.Text = "Go"
        Me.cmdGo.UseVisualStyleBackColor = True
        '
        'txtCHNo
        '
        Me.txtCHNo.AcceptsReturn = True
        Me.txtCHNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCHNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCHNo.Location = New System.Drawing.Point(54, 554)
        Me.txtCHNo.MaxLength = 4
        Me.txtCHNo.Name = "txtCHNo"
        Me.txtCHNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCHNo.Size = New System.Drawing.Size(45, 19)
        Me.txtCHNo.TabIndex = 8
        Me.txtCHNo.Text = "0000"
        Me.txtCHNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 557)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 12)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "CHNo"
        '
        'btnAll
        '
        Me.btnAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAll.BackColor = System.Drawing.SystemColors.Control
        Me.btnAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAll.Location = New System.Drawing.Point(413, 550)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAll.Size = New System.Drawing.Size(97, 28)
        Me.btnAll.TabIndex = 10
        Me.btnAll.Text = "AllCheck"
        Me.btnAll.UseVisualStyleBackColor = True
        '
        'btnOFF
        '
        Me.btnOFF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOFF.BackColor = System.Drawing.SystemColors.Control
        Me.btnOFF.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnOFF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnOFF.Location = New System.Drawing.Point(516, 550)
        Me.btnOFF.Name = "btnOFF"
        Me.btnOFF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnOFF.Size = New System.Drawing.Size(97, 28)
        Me.btnOFF.TabIndex = 11
        Me.btnOFF.Text = "AllOFF"
        Me.btnOFF.UseVisualStyleBackColor = True
        '
        'lblCompile
        '
        Me.lblCompile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCompile.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCompile.ForeColor = System.Drawing.Color.Red
        Me.lblCompile.Location = New System.Drawing.Point(10, 632)
        Me.lblCompile.Name = "lblCompile"
        Me.lblCompile.Size = New System.Drawing.Size(105, 20)
        Me.lblCompile.TabIndex = 12
        Me.lblCompile.Text = "No Compile"
        '
        'grdChNo
        '
        Me.grdChNo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdChNo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdChNo.Location = New System.Drawing.Point(12, 12)
        Me.grdChNo.Name = "grdChNo"
        Me.grdChNo.RowTemplate.Height = 21
        Me.grdChNo.Size = New System.Drawing.Size(601, 536)
        Me.grdChNo.TabIndex = 6
        '
        'frmToolChkChList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(625, 661)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblCompile)
        Me.Controls.Add(Me.btnOFF)
        Me.Controls.Add(Me.btnAll)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCHNo)
        Me.Controls.Add(Me.grdChNo)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdGo)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmToolChkChList"
        Me.Text = "CH LIST CHECK"
        CType(Me.grdChNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

	Friend WithEvents grdChNo As clsDataGridViewPlus
	Public WithEvents cmdExit As Button
	Public WithEvents cmdGo As Button
	Public WithEvents txtCHNo As TextBox
    Friend WithEvents Label1 As Label
    Public WithEvents btnAll As System.Windows.Forms.Button
    Public WithEvents btnOFF As System.Windows.Forms.Button
    Friend WithEvents lblCompile As System.Windows.Forms.Label
End Class
