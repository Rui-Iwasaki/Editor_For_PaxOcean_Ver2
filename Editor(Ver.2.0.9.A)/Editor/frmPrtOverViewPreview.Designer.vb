<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrtOverViewPreview
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

    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents Frame1 As System.Windows.Forms.Panel
    Public WithEvents imgPreview As System.Windows.Forms.PictureBox
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Frame1 = New System.Windows.Forms.Panel()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.imgPreview = New System.Windows.Forms.PictureBox()
        Me.pnlWallpaper = New System.Windows.Forms.Panel()
        Me.Frame1.SuspendLayout()
        CType(Me.imgPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlWallpaper.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.Color.SteelBlue
        Me.Frame1.Controls.Add(Me.cmdClose)
        Me.Frame1.Controls.Add(Me.cmdPrint)
        Me.Frame1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(1121, 73)
        Me.Frame1.TabIndex = 0
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(976, 16)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(121, 41)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "CLOSE"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(8, 16)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(209, 41)
        Me.cmdPrint.TabIndex = 1
        Me.cmdPrint.Text = "PRINT"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'imgPreview
        '
        Me.imgPreview.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.imgPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgPreview.Location = New System.Drawing.Point(15, 3)
        Me.imgPreview.Name = "imgPreview"
        Me.imgPreview.Size = New System.Drawing.Size(1054, 730)
        Me.imgPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgPreview.TabIndex = 1
        Me.imgPreview.TabStop = False
        '
        'pnlWallpaper
        '
        Me.pnlWallpaper.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlWallpaper.AutoScroll = True
        Me.pnlWallpaper.Controls.Add(Me.imgPreview)
        Me.pnlWallpaper.Location = New System.Drawing.Point(8, 79)
        Me.pnlWallpaper.Name = "pnlWallpaper"
        Me.pnlWallpaper.Size = New System.Drawing.Size(1100, 660)
        Me.pnlWallpaper.TabIndex = 117
        '
        'frmPrtOverViewPreview
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(1115, 751)
        Me.Controls.Add(Me.pnlWallpaper)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrtOverViewPreview"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "OVER VIEW PRINT PREVIEW"
        Me.Frame1.ResumeLayout(False)
        CType(Me.imgPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlWallpaper.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlWallpaper As System.Windows.Forms.Panel
#End Region

End Class
