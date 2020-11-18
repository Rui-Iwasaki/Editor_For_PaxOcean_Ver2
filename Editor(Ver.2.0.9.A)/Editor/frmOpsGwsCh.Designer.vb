<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsGwsCh
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
        Me.cmdMake = New System.Windows.Forms.Button()
        Me.cmdImport = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grdCH = New Editor.clsDataGridViewPlus()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmbPort = New System.Windows.Forms.ComboBox()
        Me.lblGwsFile = New System.Windows.Forms.Label()
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdMake
        '
        Me.cmdMake.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdMake.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMake.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMake.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMake.Location = New System.Drawing.Point(25, 322)
        Me.cmdMake.Name = "cmdMake"
        Me.cmdMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMake.Size = New System.Drawing.Size(120, 32)
        Me.cmdMake.TabIndex = 47
        Me.cmdMake.Text = "Make Data"
        Me.cmdMake.UseVisualStyleBackColor = True
        '
        'cmdImport
        '
        Me.cmdImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdImport.BackColor = System.Drawing.SystemColors.Control
        Me.cmdImport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdImport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdImport.Location = New System.Drawing.Point(295, 322)
        Me.cmdImport.Name = "cmdImport"
        Me.cmdImport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdImport.Size = New System.Drawing.Size(120, 32)
        Me.cmdImport.TabIndex = 45
        Me.cmdImport.Text = "CSV Import"
        Me.cmdImport.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(22, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(95, 12)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Transmission CH"
        '
        'grdCH
        '
        Me.grdCH.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCH.Location = New System.Drawing.Point(25, 84)
        Me.grdCH.Name = "grdCH"
        Me.grdCH.RowTemplate.Height = 21
        Me.grdCH.Size = New System.Drawing.Size(390, 230)
        Me.grdCH.TabIndex = 44
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(311, 391)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(104, 33)
        Me.cmdExit.TabIndex = 49
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(201, 391)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(104, 33)
        Me.cmdSave.TabIndex = 48
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmbPort
        '
        Me.cmbPort.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPort.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPort.Location = New System.Drawing.Point(25, 24)
        Me.cmbPort.Name = "cmbPort"
        Me.cmbPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPort.Size = New System.Drawing.Size(97, 20)
        Me.cmbPort.TabIndex = 50
        '
        'lblGwsFile
        '
        Me.lblGwsFile.AutoSize = True
        Me.lblGwsFile.Location = New System.Drawing.Point(178, 32)
        Me.lblGwsFile.Name = "lblGwsFile"
        Me.lblGwsFile.Size = New System.Drawing.Size(41, 12)
        Me.lblGwsFile.TabIndex = 51
        Me.lblGwsFile.Text = "Label1"
        '
        'frmOpsGwsCh
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(448, 477)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblGwsFile)
        Me.Controls.Add(Me.cmbPort)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdMake)
        Me.Controls.Add(Me.cmdImport)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grdCH)
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsGwsCh"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "GWS CH SET"
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdMake As System.Windows.Forms.Button
    Public WithEvents cmdImport As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grdCH As Editor.clsDataGridViewPlus
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmbPort As System.Windows.Forms.ComboBox
    Friend WithEvents lblGwsFile As System.Windows.Forms.Label
End Class
