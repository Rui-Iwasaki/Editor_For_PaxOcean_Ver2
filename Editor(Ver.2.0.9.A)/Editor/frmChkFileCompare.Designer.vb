<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChkFileCompare
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
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.fraWait = New System.Windows.Forms.Panel()
        Me.lblWait2 = New System.Windows.Forms.Label()
        Me.lblWait1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optJapanese = New System.Windows.Forms.RadioButton()
        Me.optEnglish = New System.Windows.Forms.RadioButton()
        Me.cmdCSVoutput = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkADR = New System.Windows.Forms.CheckBox()
        Me.chkMC = New System.Windows.Forms.CheckBox()
        Me.btnChkSelect = New System.Windows.Forms.Button()
        Me.Compile_Read = New System.Windows.Forms.RadioButton()
        Me.Save_Read = New System.Windows.Forms.RadioButton()
        Me.CF_Read = New System.Windows.Forms.RadioButton()
        Me.txtTargetFile = New System.Windows.Forms.TextBox()
        Me.txtSourceFile = New System.Windows.Forms.TextBox()
        Me.lblTargetStatus = New System.Windows.Forms.Label()
        Me.cmdComapre = New System.Windows.Forms.Button()
        Me.lblSourceStatus = New System.Windows.Forms.Label()
        Me.cmdTargetOpen = New System.Windows.Forms.Button()
        Me.cmdSourceOpen = New System.Windows.Forms.Button()
        Me.txtTargetPath = New System.Windows.Forms.TextBox()
        Me.txtSourcePath = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtResult = New System.Windows.Forms.RichTextBox()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.cmdLogTxt = New System.Windows.Forms.Button()
        Me.prgBar = New System.Windows.Forms.ProgressBar()
        Me.grdCompare = New Editor.clsDataGridViewPlus()
        Me.cmbChOutType = New System.Windows.Forms.ComboBox()
        Me.cmbDataChk = New System.Windows.Forms.ComboBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.fraWait.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(738, 645)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(120, 40)
        Me.cmdPrint.TabIndex = 9
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(868, 645)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(120, 40)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'fraWait
        '
        Me.fraWait.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fraWait.BackColor = System.Drawing.Color.Gray
        Me.fraWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.fraWait.Controls.Add(Me.lblWait2)
        Me.fraWait.Controls.Add(Me.lblWait1)
        Me.fraWait.Location = New System.Drawing.Point(19, 636)
        Me.fraWait.Name = "fraWait"
        Me.fraWait.Size = New System.Drawing.Size(359, 49)
        Me.fraWait.TabIndex = 14
        Me.fraWait.Visible = False
        '
        'lblWait2
        '
        Me.lblWait2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWait2.ForeColor = System.Drawing.Color.White
        Me.lblWait2.Location = New System.Drawing.Point(165, 12)
        Me.lblWait2.Name = "lblWait2"
        Me.lblWait2.Size = New System.Drawing.Size(182, 23)
        Me.lblWait2.TabIndex = 1
        Me.lblWait2.Text = "Please wait..."
        Me.lblWait2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWait1
        '
        Me.lblWait1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWait1.ForeColor = System.Drawing.Color.White
        Me.lblWait1.Location = New System.Drawing.Point(3, 9)
        Me.lblWait1.Name = "lblWait1"
        Me.lblWait1.Size = New System.Drawing.Size(182, 28)
        Me.lblWait1.TabIndex = 0
        Me.lblWait1.Text = "Now Searching"
        Me.lblWait1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.optJapanese)
        Me.GroupBox1.Controls.Add(Me.optEnglish)
        Me.GroupBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 645)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(182, 53)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Language"
        Me.GroupBox1.Visible = False
        '
        'optJapanese
        '
        Me.optJapanese.AutoSize = True
        Me.optJapanese.Location = New System.Drawing.Point(88, 23)
        Me.optJapanese.Name = "optJapanese"
        Me.optJapanese.Size = New System.Drawing.Size(71, 16)
        Me.optJapanese.TabIndex = 0
        Me.optJapanese.Text = "Japanese"
        Me.optJapanese.UseVisualStyleBackColor = True
        '
        'optEnglish
        '
        Me.optEnglish.AutoSize = True
        Me.optEnglish.Checked = True
        Me.optEnglish.Location = New System.Drawing.Point(15, 23)
        Me.optEnglish.Name = "optEnglish"
        Me.optEnglish.Size = New System.Drawing.Size(65, 16)
        Me.optEnglish.TabIndex = 0
        Me.optEnglish.TabStop = True
        Me.optEnglish.Text = "English"
        Me.optEnglish.UseVisualStyleBackColor = True
        '
        'cmdCSVoutput
        '
        Me.cmdCSVoutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdCSVoutput.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCSVoutput.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCSVoutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCSVoutput.Location = New System.Drawing.Point(472, 645)
        Me.cmdCSVoutput.Name = "cmdCSVoutput"
        Me.cmdCSVoutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCSVoutput.Size = New System.Drawing.Size(120, 40)
        Me.cmdCSVoutput.TabIndex = 2
        Me.cmdCSVoutput.Text = "CSV File"
        Me.cmdCSVoutput.UseVisualStyleBackColor = True
        Me.cmdCSVoutput.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.chkADR)
        Me.GroupBox2.Controls.Add(Me.chkMC)
        Me.GroupBox2.Controls.Add(Me.btnChkSelect)
        Me.GroupBox2.Controls.Add(Me.Compile_Read)
        Me.GroupBox2.Controls.Add(Me.Save_Read)
        Me.GroupBox2.Controls.Add(Me.CF_Read)
        Me.GroupBox2.Controls.Add(Me.txtTargetFile)
        Me.GroupBox2.Controls.Add(Me.txtSourceFile)
        Me.GroupBox2.Controls.Add(Me.lblTargetStatus)
        Me.GroupBox2.Controls.Add(Me.cmdComapre)
        Me.GroupBox2.Controls.Add(Me.lblSourceStatus)
        Me.GroupBox2.Controls.Add(Me.cmdTargetOpen)
        Me.GroupBox2.Controls.Add(Me.cmdSourceOpen)
        Me.GroupBox2.Controls.Add(Me.txtTargetPath)
        Me.GroupBox2.Controls.Add(Me.txtSourcePath)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(972, 123)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "FileInfo"
        '
        'chkADR
        '
        Me.chkADR.AutoSize = True
        Me.chkADR.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkADR.Location = New System.Drawing.Point(522, 18)
        Me.chkADR.Name = "chkADR"
        Me.chkADR.Size = New System.Drawing.Size(187, 20)
        Me.chkADR.TabIndex = 14
        Me.chkADR.Text = "(M/C) FU Adr Compare"
        Me.chkADR.UseVisualStyleBackColor = True
        Me.chkADR.Visible = False
        '
        'chkMC
        '
        Me.chkMC.AutoSize = True
        Me.chkMC.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMC.Location = New System.Drawing.Point(727, 18)
        Me.chkMC.Name = "chkMC"
        Me.chkMC.Size = New System.Drawing.Size(115, 20)
        Me.chkMC.TabIndex = 13
        Me.chkMC.Text = "M/C Compare"
        Me.chkMC.UseVisualStyleBackColor = True
        '
        'btnChkSelect
        '
        Me.btnChkSelect.Location = New System.Drawing.Point(891, 18)
        Me.btnChkSelect.Name = "btnChkSelect"
        Me.btnChkSelect.Size = New System.Drawing.Size(75, 23)
        Me.btnChkSelect.TabIndex = 12
        Me.btnChkSelect.Text = "ChkSelect"
        Me.btnChkSelect.UseVisualStyleBackColor = True
        '
        'Compile_Read
        '
        Me.Compile_Read.AutoSize = True
        Me.Compile_Read.Location = New System.Drawing.Point(214, 30)
        Me.Compile_Read.Name = "Compile_Read"
        Me.Compile_Read.Size = New System.Drawing.Size(107, 16)
        Me.Compile_Read.TabIndex = 11
        Me.Compile_Read.TabStop = True
        Me.Compile_Read.Text = "Compile Folder"
        Me.Compile_Read.UseVisualStyleBackColor = True
        '
        'Save_Read
        '
        Me.Save_Read.AutoSize = True
        Me.Save_Read.Location = New System.Drawing.Point(110, 30)
        Me.Save_Read.Name = "Save_Read"
        Me.Save_Read.Size = New System.Drawing.Size(89, 16)
        Me.Save_Read.TabIndex = 10
        Me.Save_Read.TabStop = True
        Me.Save_Read.Text = "SAVE Folder"
        Me.Save_Read.UseVisualStyleBackColor = True
        '
        'CF_Read
        '
        Me.CF_Read.AutoSize = True
        Me.CF_Read.Checked = True
        Me.CF_Read.Location = New System.Drawing.Point(20, 30)
        Me.CF_Read.Name = "CF_Read"
        Me.CF_Read.Size = New System.Drawing.Size(65, 16)
        Me.CF_Read.TabIndex = 9
        Me.CF_Read.TabStop = True
        Me.CF_Read.Text = "CF CARD"
        Me.CF_Read.UseVisualStyleBackColor = True
        '
        'txtTargetFile
        '
        Me.txtTargetFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTargetFile.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.txtTargetFile.Location = New System.Drawing.Point(653, 89)
        Me.txtTargetFile.Name = "txtTargetFile"
        Me.txtTargetFile.ReadOnly = True
        Me.txtTargetFile.Size = New System.Drawing.Size(58, 19)
        Me.txtTargetFile.TabIndex = 6
        Me.txtTargetFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSourceFile
        '
        Me.txtSourceFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSourceFile.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.txtSourceFile.Location = New System.Drawing.Point(653, 63)
        Me.txtSourceFile.Name = "txtSourceFile"
        Me.txtSourceFile.ReadOnly = True
        Me.txtSourceFile.Size = New System.Drawing.Size(58, 19)
        Me.txtSourceFile.TabIndex = 6
        Me.txtSourceFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblTargetStatus
        '
        Me.lblTargetStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTargetStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTargetStatus.Location = New System.Drawing.Point(775, 85)
        Me.lblTargetStatus.Name = "lblTargetStatus"
        Me.lblTargetStatus.Size = New System.Drawing.Size(77, 19)
        Me.lblTargetStatus.TabIndex = 5
        Me.lblTargetStatus.Text = "File Load"
        Me.lblTargetStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdComapre
        '
        Me.cmdComapre.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdComapre.BackColor = System.Drawing.SystemColors.Control
        Me.cmdComapre.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdComapre.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdComapre.Location = New System.Drawing.Point(866, 62)
        Me.cmdComapre.Name = "cmdComapre"
        Me.cmdComapre.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdComapre.Size = New System.Drawing.Size(84, 44)
        Me.cmdComapre.TabIndex = 3
        Me.cmdComapre.Text = "Compare"
        Me.cmdComapre.UseVisualStyleBackColor = True
        '
        'lblSourceStatus
        '
        Me.lblSourceStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSourceStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSourceStatus.Location = New System.Drawing.Point(775, 62)
        Me.lblSourceStatus.Name = "lblSourceStatus"
        Me.lblSourceStatus.Size = New System.Drawing.Size(77, 19)
        Me.lblSourceStatus.TabIndex = 5
        Me.lblSourceStatus.Text = "Ready"
        Me.lblSourceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdTargetOpen
        '
        Me.cmdTargetOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdTargetOpen.Location = New System.Drawing.Point(717, 87)
        Me.cmdTargetOpen.Name = "cmdTargetOpen"
        Me.cmdTargetOpen.Size = New System.Drawing.Size(52, 22)
        Me.cmdTargetOpen.TabIndex = 4
        Me.cmdTargetOpen.Text = "Open"
        Me.cmdTargetOpen.UseVisualStyleBackColor = True
        '
        'cmdSourceOpen
        '
        Me.cmdSourceOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSourceOpen.Location = New System.Drawing.Point(717, 60)
        Me.cmdSourceOpen.Name = "cmdSourceOpen"
        Me.cmdSourceOpen.Size = New System.Drawing.Size(52, 22)
        Me.cmdSourceOpen.TabIndex = 4
        Me.cmdSourceOpen.Text = "Open"
        Me.cmdSourceOpen.UseVisualStyleBackColor = True
        '
        'txtTargetPath
        '
        Me.txtTargetPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTargetPath.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.txtTargetPath.Location = New System.Drawing.Point(84, 89)
        Me.txtTargetPath.Name = "txtTargetPath"
        Me.txtTargetPath.ReadOnly = True
        Me.txtTargetPath.Size = New System.Drawing.Size(563, 19)
        Me.txtTargetPath.TabIndex = 3
        '
        'txtSourcePath
        '
        Me.txtSourcePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSourcePath.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.txtSourcePath.Location = New System.Drawing.Point(84, 62)
        Me.txtSourcePath.Name = "txtSourcePath"
        Me.txtSourcePath.ReadOnly = True
        Me.txtSourcePath.Size = New System.Drawing.Size(563, 19)
        Me.txtSourcePath.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Target"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Location = New System.Drawing.Point(651, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "File"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.Location = New System.Drawing.Point(773, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Status"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Source"
        '
        'txtResult
        '
        Me.txtResult.Location = New System.Drawing.Point(18, 141)
        Me.txtResult.Name = "txtResult"
        Me.txtResult.Size = New System.Drawing.Size(970, 489)
        Me.txtResult.TabIndex = 16
        Me.txtResult.Text = ""
        '
        'cmdLogTxt
        '
        Me.cmdLogTxt.Location = New System.Drawing.Point(607, 645)
        Me.cmdLogTxt.Name = "cmdLogTxt"
        Me.cmdLogTxt.Size = New System.Drawing.Size(120, 40)
        Me.cmdLogTxt.TabIndex = 17
        Me.cmdLogTxt.Text = "Log Folder"
        Me.cmdLogTxt.UseVisualStyleBackColor = True
        '
        'prgBar
        '
        Me.prgBar.Location = New System.Drawing.Point(395, 652)
        Me.prgBar.Name = "prgBar"
        Me.prgBar.Size = New System.Drawing.Size(197, 23)
        Me.prgBar.TabIndex = 18
        '
        'grdCompare
        '
        Me.grdCompare.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCompare.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCompare.Location = New System.Drawing.Point(778, 308)
        Me.grdCompare.Name = "grdCompare"
        Me.grdCompare.RowTemplate.Height = 21
        Me.grdCompare.Size = New System.Drawing.Size(80, 42)
        Me.grdCompare.TabIndex = 15
        Me.grdCompare.Visible = False
        '
        'cmbChOutType
        '
        Me.cmbChOutType.BackColor = System.Drawing.SystemColors.Window
        Me.cmbChOutType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbChOutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChOutType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbChOutType.Location = New System.Drawing.Point(36, 165)
        Me.cmbChOutType.Name = "cmbChOutType"
        Me.cmbChOutType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbChOutType.Size = New System.Drawing.Size(105, 20)
        Me.cmbChOutType.TabIndex = 19
        '
        'cmbDataChk
        '
        Me.cmbDataChk.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDataChk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDataChk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataChk.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDataChk.Location = New System.Drawing.Point(36, 191)
        Me.cmbDataChk.Name = "cmbDataChk"
        Me.cmbDataChk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDataChk.Size = New System.Drawing.Size(133, 20)
        Me.cmbDataChk.TabIndex = 20
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatus.Location = New System.Drawing.Point(36, 226)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatus.Size = New System.Drawing.Size(68, 20)
        Me.cmbStatus.TabIndex = 114
        '
        'frmChkFileCompare
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(1003, 710)
        Me.Controls.Add(Me.prgBar)
        Me.Controls.Add(Me.cmdLogTxt)
        Me.Controls.Add(Me.txtResult)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.fraWait)
        Me.Controls.Add(Me.grdCompare)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdCSVoutput)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmbChOutType)
        Me.Controls.Add(Me.cmbDataChk)
        Me.Controls.Add(Me.cmbStatus)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChkFileCompare"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FILE COMPARE"
        Me.fraWait.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdCompare, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents fraWait As System.Windows.Forms.Panel
    Friend WithEvents lblWait1 As System.Windows.Forms.Label
    Friend WithEvents lblWait2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optJapanese As System.Windows.Forms.RadioButton
    Friend WithEvents optEnglish As System.Windows.Forms.RadioButton
    Public WithEvents cmdCSVoutput As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdComapre As System.Windows.Forms.Button
    Friend WithEvents txtSourcePath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdTargetOpen As System.Windows.Forms.Button
    Friend WithEvents cmdSourceOpen As System.Windows.Forms.Button
    Friend WithEvents txtTargetPath As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblSourceStatus As System.Windows.Forms.Label
    Friend WithEvents lblTargetStatus As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTargetFile As System.Windows.Forms.TextBox
    Friend WithEvents txtSourceFile As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grdCompare As Editor.clsDataGridViewPlus
    Friend WithEvents Compile_Read As System.Windows.Forms.RadioButton
    Friend WithEvents Save_Read As System.Windows.Forms.RadioButton
    Friend WithEvents CF_Read As System.Windows.Forms.RadioButton
    Friend WithEvents txtResult As System.Windows.Forms.RichTextBox
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents cmdLogTxt As System.Windows.Forms.Button
    Friend WithEvents prgBar As System.Windows.Forms.ProgressBar
    Friend WithEvents btnChkSelect As System.Windows.Forms.Button
    Friend WithEvents chkMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkADR As System.Windows.Forms.CheckBox
    Public WithEvents cmbChOutType As System.Windows.Forms.ComboBox
    Public WithEvents cmbDataChk As System.Windows.Forms.ComboBox
    Public WithEvents cmbStatus As System.Windows.Forms.ComboBox
#End Region

End Class
