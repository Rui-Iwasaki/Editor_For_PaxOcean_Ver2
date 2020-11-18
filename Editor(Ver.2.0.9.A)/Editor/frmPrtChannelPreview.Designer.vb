<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrtChannelPreview
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
    Public WithEvents lblPage As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.Panel

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrtChannelPreview))
        Me.Frame1 = New System.Windows.Forms.Panel()
        Me.txtPage = New System.Windows.Forms.TextBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdNextPage = New System.Windows.Forms.Button()
        Me.cmdBeforePage = New System.Windows.Forms.Button()
        Me.cmdAllPrint = New System.Windows.Forms.Button()
        Me.cmdPagesPrint = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblPage = New System.Windows.Forms.Label()
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.pnlWallpaper = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbControlType = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbExtDevice = New System.Windows.Forms.ComboBox()
        Me.lblDeviceStatus = New System.Windows.Forms.Label()
        Me.cmbDataType = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.picPreview = New System.Windows.Forms.PictureBox()
        Me.Frame1.SuspendLayout()
        Me.pnlWallpaper.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Frame1.Controls.Add(Me.lblPage)
        Me.Frame1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(1171, 73)
        Me.Frame1.TabIndex = 0
        '
        'txtPage
        '
        Me.txtPage.AcceptsReturn = True
        Me.txtPage.Location = New System.Drawing.Point(568, 28)
        Me.txtPage.MaxLength = 0
        Me.txtPage.Name = "txtPage"
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
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(1030, 16)
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
        Me.cmdNextPage.Location = New System.Drawing.Point(680, 16)
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
        Me.Label11.Location = New System.Drawing.Point(624, 31)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(9, 17)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "/"
        '
        'lblPage
        '
        Me.lblPage.BackColor = System.Drawing.Color.Transparent
        Me.lblPage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPage.ForeColor = System.Drawing.Color.White
        Me.lblPage.Location = New System.Drawing.Point(632, 31)
        Me.lblPage.Name = "lblPage"
        Me.lblPage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPage.Size = New System.Drawing.Size(46, 17)
        Me.lblPage.TabIndex = 6
        Me.lblPage.Text = "3"
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'pnlWallpaper
        '
        Me.pnlWallpaper.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlWallpaper.AutoScroll = True
        Me.pnlWallpaper.Controls.Add(Me.GroupBox1)
        Me.pnlWallpaper.Controls.Add(Me.picPreview)
        Me.pnlWallpaper.Location = New System.Drawing.Point(0, 79)
        Me.pnlWallpaper.Name = "pnlWallpaper"
        Me.pnlWallpaper.Size = New System.Drawing.Size(1165, 659)
        Me.pnlWallpaper.TabIndex = 115
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbControlType)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbExtDevice)
        Me.GroupBox1.Controls.Add(Me.lblDeviceStatus)
        Me.GroupBox1.Controls.Add(Me.cmbDataType)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmbUnit)
        Me.GroupBox1.Controls.Add(Me.Label38)
        Me.GroupBox1.Controls.Add(Me.cmbStatus)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(37, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(460, 70)
        Me.GroupBox1.TabIndex = 115
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Visible False"
        Me.GroupBox1.Visible = False
        '
        'cmbControlType
        '
        Me.cmbControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbControlType.FormattingEnabled = True
        Me.cmbControlType.Location = New System.Drawing.Point(372, 20)
        Me.cmbControlType.Name = "cmbControlType"
        Me.cmbControlType.Size = New System.Drawing.Size(73, 20)
        Me.cmbControlType.TabIndex = 121
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(288, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 120
        Me.Label2.Text = "Control Type"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbExtDevice
        '
        Me.cmbExtDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExtDevice.FormattingEnabled = True
        Me.cmbExtDevice.Location = New System.Drawing.Point(203, 44)
        Me.cmbExtDevice.Name = "cmbExtDevice"
        Me.cmbExtDevice.Size = New System.Drawing.Size(73, 20)
        Me.cmbExtDevice.TabIndex = 118
        '
        'lblDeviceStatus
        '
        Me.lblDeviceStatus.AutoSize = True
        Me.lblDeviceStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeviceStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDeviceStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeviceStatus.Location = New System.Drawing.Point(135, 48)
        Me.lblDeviceStatus.Name = "lblDeviceStatus"
        Me.lblDeviceStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblDeviceStatus.Size = New System.Drawing.Size(65, 12)
        Me.lblDeviceStatus.TabIndex = 119
        Me.lblDeviceStatus.Text = "Ext Device"
        Me.lblDeviceStatus.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbDataType
        '
        Me.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataType.FormattingEnabled = True
        Me.cmbDataType.Location = New System.Drawing.Point(203, 20)
        Me.cmbDataType.Name = "cmbDataType"
        Me.cmbDataType.Size = New System.Drawing.Size(73, 20)
        Me.cmbDataType.TabIndex = 116
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(136, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(59, 12)
        Me.Label8.TabIndex = 117
        Me.Label8.Text = "Data Type"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbUnit
        '
        Me.cmbUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbUnit.Location = New System.Drawing.Point(52, 44)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbUnit.Size = New System.Drawing.Size(72, 20)
        Me.cmbUnit.TabIndex = 115
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(16, 48)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(29, 12)
        Me.Label38.TabIndex = 114
        Me.Label38.Text = "Unit"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatus.Location = New System.Drawing.Point(52, 20)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatus.Size = New System.Drawing.Size(72, 20)
        Me.cmbStatus.TabIndex = 112
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 113
        Me.Label1.Text = "Status"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'picPreview
        '
        Me.picPreview.BackColor = System.Drawing.Color.White
        Me.picPreview.Location = New System.Drawing.Point(5, 6)
        Me.picPreview.Name = "picPreview"
        Me.picPreview.Size = New System.Drawing.Size(1155, 770)
        Me.picPreview.TabIndex = 116
        Me.picPreview.TabStop = False
        '
        'frmPrtChannelPreview
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(1170, 741)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.pnlWallpaper)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrtChannelPreview"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CHANNEL LIST PRINT PREVIEW"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.pnlWallpaper.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents pnlWallpaper As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbControlType As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbExtDevice As System.Windows.Forms.ComboBox
    Public WithEvents lblDeviceStatus As System.Windows.Forms.Label
    Friend WithEvents cmbDataType As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picPreview As System.Windows.Forms.PictureBox
#End Region

End Class
