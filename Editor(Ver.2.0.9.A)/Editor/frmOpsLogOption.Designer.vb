<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsLogOption
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtFileNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.grpType = New System.Windows.Forms.GroupBox()
        Me.radType1 = New System.Windows.Forms.RadioButton()
        Me.grdLog = New Editor.clsDataGridViewPlus()
        Me.grpType.SuspendLayout()
        CType(Me.grdLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtFileNo
        '
        Me.txtFileNo.Location = New System.Drawing.Point(134, 25)
        Me.txtFileNo.Name = "txtFileNo"
        Me.txtFileNo.Size = New System.Drawing.Size(63, 19)
        Me.txtFileNo.TabIndex = 0
        Me.txtFileNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Log Format FileNo."
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(599, 430)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(103, 29)
        Me.cmdSave.TabIndex = 2
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.Location = New System.Drawing.Point(729, 430)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(103, 29)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'grpType
        '
        Me.grpType.Controls.Add(Me.radType1)
        Me.grpType.Location = New System.Drawing.Point(256, 12)
        Me.grpType.Name = "grpType"
        Me.grpType.Size = New System.Drawing.Size(188, 48)
        Me.grpType.TabIndex = 6
        Me.grpType.TabStop = False
        '
        'radType1
        '
        Me.radType1.AutoSize = True
        Me.radType1.Location = New System.Drawing.Point(17, 18)
        Me.radType1.Name = "radType1"
        Me.radType1.Size = New System.Drawing.Size(84, 16)
        Me.radType1.TabIndex = 0
        Me.radType1.TabStop = True
        Me.radType1.Text = "Type H3020"
        Me.radType1.UseVisualStyleBackColor = True
        '
        'grdLog
        '
        Me.grdLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdLog.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.grdLog.Location = New System.Drawing.Point(12, 77)
        Me.grdLog.Name = "grdLog"
        Me.grdLog.RowTemplate.Height = 21
        Me.grdLog.Size = New System.Drawing.Size(837, 316)
        Me.grdLog.TabIndex = 5
        '
        'frmOpsLogOption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(861, 471)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpType)
        Me.Controls.Add(Me.grdLog)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFileNo)
        Me.Name = "frmOpsLogOption"
        Me.Text = "LogOptionSetting"
        Me.grpType.ResumeLayout(False)
        Me.grpType.PerformLayout()
        CType(Me.grdLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtFileNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents grdLog As Editor.clsDataGridViewPlus
    Friend WithEvents grpType As System.Windows.Forms.GroupBox
    Friend WithEvents radType1 As System.Windows.Forms.RadioButton
End Class
