<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrtLocalUnitPreview
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

    Public WithEvents txtPage As System.Windows.Forms.TextBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdNextPage As System.Windows.Forms.Button
    Public WithEvents cmdBeforePage As System.Windows.Forms.Button
    Public WithEvents cmdAllPrint As System.Windows.Forms.Button
    Public WithEvents cmdPagesPrint As System.Windows.Forms.Button
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblMaxPage As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.Panel
    Public WithEvents imgPreview As System.Windows.Forms.PictureBox

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Frame1 = New System.Windows.Forms.Panel
        Me.txtPage = New System.Windows.Forms.TextBox
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdNextPage = New System.Windows.Forms.Button
        Me.cmdBeforePage = New System.Windows.Forms.Button
        Me.cmdAllPrint = New System.Windows.Forms.Button
        Me.cmdPagesPrint = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblMaxPage = New System.Windows.Forms.Label
        Me.imgPreview = New System.Windows.Forms.PictureBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Frame1.SuspendLayout()
        CType(Me.imgPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.Color.SteelBlue
        Me.Frame1.Controls.Add(Me.txtPage)
        Me.Frame1.Controls.Add(Me.cmdClose)
        Me.Frame1.Controls.Add(Me.cmdNextPage)
        Me.Frame1.Controls.Add(Me.cmdBeforePage)
        Me.Frame1.Controls.Add(Me.cmdAllPrint)
        Me.Frame1.Controls.Add(Me.cmdPagesPrint)
        Me.Frame1.Controls.Add(Me.Label11)
        Me.Frame1.Controls.Add(Me.lblMaxPage)
        Me.Frame1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(990, 73)
        Me.Frame1.TabIndex = 0
        '
        'txtPage
        '
        Me.txtPage.AcceptsReturn = True



        Me.txtPage.Location = New System.Drawing.Point(578, 28)
        Me.txtPage.MaxLength = 0
        Me.txtPage.Name = "txtPage"
        Me.txtPage.ReadOnly = True
        Me.txtPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPage.Size = New System.Drawing.Size(49, 19)
        Me.txtPage.TabIndex = 8
        Me.txtPage.Text = "1"
        Me.txtPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(848, 16)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(121, 41)
        Me.cmdClose.TabIndex = 5
        Me.cmdClose.Text = "CLOSE"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdNextPage
        '
        Me.cmdNextPage.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNextPage.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNextPage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNextPage.Location = New System.Drawing.Point(678, 16)
        Me.cmdNextPage.Name = "cmdNextPage"
        Me.cmdNextPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNextPage.Size = New System.Drawing.Size(57, 41)
        Me.cmdNextPage.TabIndex = 4
        Me.cmdNextPage.Text = "＞＞"
        Me.cmdNextPage.UseVisualStyleBackColor = True
        '
        'cmdBeforePage
        '
        Me.cmdBeforePage.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBeforePage.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBeforePage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBeforePage.Location = New System.Drawing.Point(488, 16)
        Me.cmdBeforePage.Name = "cmdBeforePage"
        Me.cmdBeforePage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBeforePage.Size = New System.Drawing.Size(57, 41)
        Me.cmdBeforePage.TabIndex = 3
        Me.cmdBeforePage.Text = "＜＜"
        Me.cmdBeforePage.UseVisualStyleBackColor = True
        '
        'cmdAllPrint
        '
        Me.cmdAllPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAllPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAllPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAllPrint.Location = New System.Drawing.Point(232, 16)
        Me.cmdAllPrint.Name = "cmdAllPrint"
        Me.cmdAllPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAllPrint.Size = New System.Drawing.Size(209, 41)
        Me.cmdAllPrint.TabIndex = 2
        Me.cmdAllPrint.Text = "ALL PRINT"
        Me.cmdAllPrint.UseVisualStyleBackColor = True
        '
        'cmdPagesPrint
        '
        Me.cmdPagesPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPagesPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPagesPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPagesPrint.Location = New System.Drawing.Point(8, 16)
        Me.cmdPagesPrint.Name = "cmdPagesPrint"
        Me.cmdPagesPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPagesPrint.Size = New System.Drawing.Size(209, 41)
        Me.cmdPagesPrint.TabIndex = 1
        Me.cmdPagesPrint.Text = "PAGES PRINT"
        Me.cmdPagesPrint.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(634, 32)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(9, 17)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "/"
        '
        'lblMaxPage
        '
        Me.lblMaxPage.BackColor = System.Drawing.Color.Transparent
        Me.lblMaxPage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMaxPage.ForeColor = System.Drawing.Color.White
        Me.lblMaxPage.Location = New System.Drawing.Point(642, 32)
        Me.lblMaxPage.Name = "lblMaxPage"
        Me.lblMaxPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMaxPage.Size = New System.Drawing.Size(9, 17)
        Me.lblMaxPage.TabIndex = 6
        Me.lblMaxPage.Text = "3"
        '
        'imgPreview
        '
        Me.imgPreview.BackColor = System.Drawing.SystemColors.Window
        Me.imgPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgPreview.Location = New System.Drawing.Point(55, 1)
        Me.imgPreview.Name = "imgPreview"
        Me.imgPreview.Size = New System.Drawing.Size(830, 620)
        Me.imgPreview.TabIndex = 1
        Me.imgPreview.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.imgPreview)
        Me.Panel1.Location = New System.Drawing.Point(12, 73)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(965, 634)
        Me.Panel1.TabIndex = 2
        '
        'frmPrtLocalUnitPreview
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(989, 709)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrtLocalUnitPreview"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "LOCAL UNIT PRINT PREVIEW"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.imgPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
#End Region

End Class
