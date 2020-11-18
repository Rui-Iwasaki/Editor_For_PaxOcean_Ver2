<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrtGraphPreview
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

    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmbGraph As System.Windows.Forms.ComboBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.Panel
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Frame1 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.imgPreview = New System.Windows.Forms.PictureBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmbGraph = New System.Windows.Forms.ComboBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmdbClose = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmdbPrint = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbStatusDigital = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbStatusAnalog = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.imgPrevie = New System.Windows.Forms.PictureBox()
        Me.pnlWallpaper = New System.Windows.Forms.Panel()
        Me.Frame1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.imgPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.imgPrevie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlWallpaper.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Frame1.Controls.Add(Me.Panel1)
        Me.Frame1.Controls.Add(Me.imgPreview)
        Me.Frame1.Controls.Add(Me.cmdPrint)
        Me.Frame1.Controls.Add(Me.cmbGraph)
        Me.Frame1.Controls.Add(Me.cmdClose)
        Me.Frame1.Controls.Add(Me.Label12)
        Me.Frame1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(-1, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(1121, 73)
        Me.Frame1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SteelBlue
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Panel1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel1.Location = New System.Drawing.Point(0, -373)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Panel1.Size = New System.Drawing.Size(1121, 73)
        Me.Panel1.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button1.Location = New System.Drawing.Point(976, 16)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button1.Size = New System.Drawing.Size(121, 41)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "CLOSE"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.Control
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button2.Location = New System.Drawing.Point(8, 16)
        Me.Button2.Name = "Button2"
        Me.Button2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button2.Size = New System.Drawing.Size(209, 41)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "PRINT"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'imgPreview
        '
        Me.imgPreview.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.imgPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgPreview.Location = New System.Drawing.Point(30, -285)
        Me.imgPreview.Name = "imgPreview"
        Me.imgPreview.Size = New System.Drawing.Size(1054, 730)
        Me.imgPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgPreview.TabIndex = 6
        Me.imgPreview.TabStop = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(21, 15)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(209, 41)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "PRINT"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmbGraph
        '
        Me.cmbGraph.BackColor = System.Drawing.SystemColors.Window
        Me.cmbGraph.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbGraph.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbGraph.Location = New System.Drawing.Point(704, 32)
        Me.cmbGraph.Name = "cmbGraph"
        Me.cmbGraph.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbGraph.Size = New System.Drawing.Size(137, 20)
        Me.cmbGraph.TabIndex = 3
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
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "CLOSE"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(656, 37)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(35, 12)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Graph"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdbClose
        '
        Me.cmdbClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdbClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdbClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdbClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdbClose.Location = New System.Drawing.Point(976, 16)
        Me.cmdbClose.Name = "cmdbClose"
        Me.cmdbClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdbClose.Size = New System.Drawing.Size(121, 41)
        Me.cmdbClose.TabIndex = 2
        Me.cmdbClose.Text = "CLOSE"
        Me.cmdbClose.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.SteelBlue
        Me.Panel2.Controls.Add(Me.cmdbClose)
        Me.Panel2.Controls.Add(Me.cmdbPrint)
        Me.Panel2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Panel2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel2.Location = New System.Drawing.Point(-2, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Panel2.Size = New System.Drawing.Size(1121, 73)
        Me.Panel2.TabIndex = 2
        '
        'cmdbPrint
        '
        Me.cmdbPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdbPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdbPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdbPrint.Location = New System.Drawing.Point(8, 16)
        Me.cmdbPrint.Name = "cmdbPrint"
        Me.cmdbPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdbPrint.Size = New System.Drawing.Size(209, 41)
        Me.cmdbPrint.TabIndex = 1
        Me.cmdbPrint.Text = "PRINT"
        Me.cmdbPrint.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbStatusDigital)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbStatusAnalog)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbUnit)
        Me.GroupBox1.Controls.Add(Me.Label38)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(195, 240)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(267, 128)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Visible False"
        Me.GroupBox1.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(97, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(41, 12)
        Me.Label3.TabIndex = 122
        Me.Label3.Text = "Analog"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbStatusDigital
        '
        Me.cmbStatusDigital.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatusDigital.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatusDigital.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatusDigital.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatusDigital.Location = New System.Drawing.Point(144, 91)
        Me.cmbStatusDigital.Name = "cmbStatusDigital"
        Me.cmbStatusDigital.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatusDigital.Size = New System.Drawing.Size(72, 20)
        Me.cmbStatusDigital.TabIndex = 120
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(91, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(47, 12)
        Me.Label2.TabIndex = 121
        Me.Label2.Text = "Digital"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbStatusAnalog
        '
        Me.cmbStatusAnalog.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatusAnalog.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatusAnalog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatusAnalog.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatusAnalog.Location = New System.Drawing.Point(144, 64)
        Me.cmbStatusAnalog.Name = "cmbStatusAnalog"
        Me.cmbStatusAnalog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatusAnalog.Size = New System.Drawing.Size(72, 20)
        Me.cmbStatusAnalog.TabIndex = 118
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(30, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "[Status]"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbUnit
        '
        Me.cmbUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbUnit.Location = New System.Drawing.Point(144, 21)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbUnit.Size = New System.Drawing.Size(72, 20)
        Me.cmbUnit.TabIndex = 117
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(30, 24)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(41, 12)
        Me.Label38.TabIndex = 116
        Me.Label38.Text = "[Unit]"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'imgPrevie
        '
        Me.imgPrevie.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.imgPrevie.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgPrevie.Location = New System.Drawing.Point(7, 3)
        Me.imgPrevie.Name = "imgPrevie"
        Me.imgPrevie.Size = New System.Drawing.Size(1100, 746)
        Me.imgPrevie.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgPrevie.TabIndex = 3
        Me.imgPrevie.TabStop = False
        '
        'pnlWallpaper
        '
        Me.pnlWallpaper.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlWallpaper.AutoScroll = True
        Me.pnlWallpaper.Controls.Add(Me.imgPrevie)
        Me.pnlWallpaper.Location = New System.Drawing.Point(5, 79)
        Me.pnlWallpaper.Name = "pnlWallpaper"
        Me.pnlWallpaper.Size = New System.Drawing.Size(1110, 650)
        Me.pnlWallpaper.TabIndex = 116
        '
        'frmPrtGraphPreview
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdbClose
        Me.ClientSize = New System.Drawing.Size(1115, 741)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlWallpaper)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrtGraphPreview"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "GRAPH VIEW PRINT PREVIEW"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.imgPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.imgPrevie, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlWallpaper.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents Button1 As System.Windows.Forms.Button
    Public WithEvents Button2 As System.Windows.Forms.Button
    Public WithEvents imgPreview As System.Windows.Forms.PictureBox
    Public WithEvents cmdbClose As System.Windows.Forms.Button
    Public WithEvents Panel2 As System.Windows.Forms.Panel
    Public WithEvents cmdbPrint As System.Windows.Forms.Button
    Public WithEvents imgPrevie As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents cmbStatusDigital As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents cmbStatusAnalog As System.Windows.Forms.ComboBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlWallpaper As System.Windows.Forms.Panel
#End Region

End Class
