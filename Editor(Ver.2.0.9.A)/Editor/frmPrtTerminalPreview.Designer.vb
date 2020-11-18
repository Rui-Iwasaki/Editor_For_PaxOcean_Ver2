<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrtTerminalPreview
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


    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtMaxPages = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmdPagesPrint = New System.Windows.Forms.Button()
        Me.cmdAllPrint = New System.Windows.Forms.Button()
        Me.cmdBeforePage = New System.Windows.Forms.Button()
        Me.cmdNextPage = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.txtPage = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDrawingNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFUpage = New System.Windows.Forms.TextBox()
        Me.picPreview = New System.Windows.Forms.PictureBox()
        Me.pnlWallpaper = New System.Windows.Forms.Panel()
        Me.Frame1.SuspendLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlWallpaper.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtMaxPages
        '
        Me.txtMaxPages.BackColor = System.Drawing.Color.Transparent
        Me.txtMaxPages.Cursor = System.Windows.Forms.Cursors.Default
        Me.txtMaxPages.ForeColor = System.Drawing.Color.White
        Me.txtMaxPages.Location = New System.Drawing.Point(810, 42)
        Me.txtMaxPages.Name = "txtMaxPages"
        Me.txtMaxPages.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaxPages.Size = New System.Drawing.Size(25, 17)
        Me.txtMaxPages.TabIndex = 6
        Me.txtMaxPages.Text = "3"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(802, 42)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(9, 17)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "/"
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
        'cmdBeforePage
        '
        Me.cmdBeforePage.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBeforePage.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBeforePage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBeforePage.Location = New System.Drawing.Point(471, 16)
        Me.cmdBeforePage.Name = "cmdBeforePage"
        Me.cmdBeforePage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBeforePage.Size = New System.Drawing.Size(57, 41)
        Me.cmdBeforePage.TabIndex = 3
        Me.cmdBeforePage.Text = "＜＜"
        Me.cmdBeforePage.UseVisualStyleBackColor = True
        '
        'cmdNextPage
        '
        Me.cmdNextPage.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNextPage.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNextPage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNextPage.Location = New System.Drawing.Point(631, 16)
        Me.cmdNextPage.Name = "cmdNextPage"
        Me.cmdNextPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNextPage.Size = New System.Drawing.Size(57, 41)
        Me.cmdNextPage.TabIndex = 4
        Me.cmdNextPage.Text = "＞＞"
        Me.cmdNextPage.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(848, 16)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(121, 41)
        Me.cmdClose.TabIndex = 5
        Me.cmdClose.Text = "CLOSE"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'txtPage
        '
        Me.txtPage.BackColor = System.Drawing.Color.White
        Me.txtPage.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtPage.Location = New System.Drawing.Point(746, 38)
        Me.txtPage.MaxLength = 0
        Me.txtPage.Name = "txtPage"
        Me.txtPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPage.Size = New System.Drawing.Size(49, 19)
        Me.txtPage.TabIndex = 8
        Me.txtPage.Text = "1"
        Me.txtPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.Color.SteelBlue
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.txtDrawingNo)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Controls.Add(Me.txtFUpage)
        Me.Frame1.Controls.Add(Me.txtPage)
        Me.Frame1.Controls.Add(Me.cmdClose)
        Me.Frame1.Controls.Add(Me.cmdNextPage)
        Me.Frame1.Controls.Add(Me.cmdBeforePage)
        Me.Frame1.Controls.Add(Me.cmdAllPrint)
        Me.Frame1.Controls.Add(Me.cmdPagesPrint)
        Me.Frame1.Controls.Add(Me.Label11)
        Me.Frame1.Controls.Add(Me.txtMaxPages)
        Me.Frame1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(990, 73)
        Me.Frame1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(536, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(89, 20)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "DRAWING NO"
        '
        'txtDrawingNo
        '
        Me.txtDrawingNo.BackColor = System.Drawing.Color.White
        Me.txtDrawingNo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDrawingNo.Location = New System.Drawing.Point(567, 38)
        Me.txtDrawingNo.MaxLength = 0
        Me.txtDrawingNo.Name = "txtDrawingNo"
        Me.txtDrawingNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDrawingNo.Size = New System.Drawing.Size(30, 19)
        Me.txtDrawingNo.TabIndex = 11
        Me.txtDrawingNo.Text = "1"
        Me.txtDrawingNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(770, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(25, 20)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "FU"
        '
        'txtFUpage
        '
        Me.txtFUpage.BackColor = System.Drawing.Color.White
        Me.txtFUpage.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtFUpage.Location = New System.Drawing.Point(801, 12)
        Me.txtFUpage.MaxLength = 0
        Me.txtFUpage.Name = "txtFUpage"
        Me.txtFUpage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFUpage.Size = New System.Drawing.Size(30, 19)
        Me.txtFUpage.TabIndex = 9
        Me.txtFUpage.Text = "1"
        Me.txtFUpage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'picPreview
        '
        Me.picPreview.BackColor = System.Drawing.Color.White
        Me.picPreview.Location = New System.Drawing.Point(72, 1)
        Me.picPreview.Name = "picPreview"
        Me.picPreview.Size = New System.Drawing.Size(810, 410)
        Me.picPreview.TabIndex = 2
        Me.picPreview.TabStop = False
        '
        'pnlWallpaper
        '
        Me.pnlWallpaper.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlWallpaper.AutoScroll = True
        Me.pnlWallpaper.Controls.Add(Me.picPreview)
        Me.pnlWallpaper.Location = New System.Drawing.Point(0, 74)
        Me.pnlWallpaper.Name = "pnlWallpaper"
        Me.pnlWallpaper.Size = New System.Drawing.Size(990, 633)
        Me.pnlWallpaper.TabIndex = 3
        '
        'frmPrtTerminalPreview
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(989, 709)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.pnlWallpaper)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrtTerminalPreview"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "TERMINAL PRINT PREVIEW"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlWallpaper.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents txtMaxPages As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents cmdPagesPrint As System.Windows.Forms.Button
    Public WithEvents cmdAllPrint As System.Windows.Forms.Button
    Public WithEvents cmdBeforePage As System.Windows.Forms.Button
    Public WithEvents cmdNextPage As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents txtPage As System.Windows.Forms.TextBox
    Public WithEvents Frame1 As System.Windows.Forms.Panel
    Friend WithEvents picPreview As System.Windows.Forms.PictureBox
    Friend WithEvents pnlWallpaper As System.Windows.Forms.Panel
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtFUpage As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtDrawingNo As System.Windows.Forms.TextBox
#End Region

End Class
