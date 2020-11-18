<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMenuMain
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

    Public WithEvents cmdCompile1 As System.Windows.Forms.Button
    Public WithEvents fraCmp As System.Windows.Forms.GroupBox
    Public WithEvents cmdPrint5 As System.Windows.Forms.Button
    Public WithEvents cmdPrint4 As System.Windows.Forms.Button
    Public WithEvents cmdPrint1 As System.Windows.Forms.Button
    Public WithEvents cmdPrint3 As System.Windows.Forms.Button
    Public WithEvents cmdPrint2 As System.Windows.Forms.Button
    Public WithEvents fraPrt As System.Windows.Forms.GroupBox
    Public WithEvents cmdExtAlarm1 As System.Windows.Forms.Button
    Public WithEvents fraExt As System.Windows.Forms.GroupBox
    Public WithEvents cmdOPS4 As System.Windows.Forms.Button
    Public WithEvents cmdOPS1 As System.Windows.Forms.Button
    Public WithEvents cmdOPS3 As System.Windows.Forms.Button
    Public WithEvents cmdOPS2 As System.Windows.Forms.Button
    Public WithEvents fraOps As System.Windows.Forms.GroupBox
    Public WithEvents cmdSequence1 As System.Windows.Forms.Button
    Public WithEvents fraSeq As System.Windows.Forms.GroupBox
    Public WithEvents cmdChannel10 As System.Windows.Forms.Button
    Public WithEvents cmdChannel9 As System.Windows.Forms.Button
    Public WithEvents cmdChannel8 As System.Windows.Forms.Button
    Public WithEvents cmdChannel7 As System.Windows.Forms.Button
    Public WithEvents cmdChannel6 As System.Windows.Forms.Button
    Public WithEvents cmdChannel5 As System.Windows.Forms.Button
    Public WithEvents cmdChannel4 As System.Windows.Forms.Button
    Public WithEvents cmdChannel2 As System.Windows.Forms.Button
    Public WithEvents cmdChannel3 As System.Windows.Forms.Button
    Public WithEvents cmdChannel1 As System.Windows.Forms.Button
    Public WithEvents fraCh As System.Windows.Forms.GroupBox
    Public WithEvents cmdSystem4 As System.Windows.Forms.Button
    Public WithEvents cmdSystem2 As System.Windows.Forms.Button
    Public WithEvents cmdSystem3 As System.Windows.Forms.Button
    Public WithEvents cmdSystem1 As System.Windows.Forms.Button
    Public WithEvents fraSys As System.Windows.Forms.GroupBox
    Public WithEvents cmdFile1 As System.Windows.Forms.Button
    Public WithEvents cmdFile3 As System.Windows.Forms.Button
    Public WithEvents cmdFile2 As System.Windows.Forms.Button
    Public WithEvents fraFile As System.Windows.Forms.GroupBox
    Public WithEvents lblFileName As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenuMain))
        Me.fraCmp = New System.Windows.Forms.GroupBox()
        Me.cmdMeasuringCheck = New System.Windows.Forms.Button()
        Me.cmdErrorCheck = New System.Windows.Forms.Button()
        Me.cmdCompile1 = New System.Windows.Forms.Button()
        Me.fraPrt = New System.Windows.Forms.GroupBox()
        Me.cmdPrint5 = New System.Windows.Forms.Button()
        Me.cmdPrint4 = New System.Windows.Forms.Button()
        Me.cmdPrint1 = New System.Windows.Forms.Button()
        Me.cmdPrint3 = New System.Windows.Forms.Button()
        Me.cmdPrint2 = New System.Windows.Forms.Button()
        Me.fraExt = New System.Windows.Forms.GroupBox()
        Me.cmdExtAlarm1 = New System.Windows.Forms.Button()
        Me.fraOps = New System.Windows.Forms.GroupBox()
        Me.cmdOPS4 = New System.Windows.Forms.Button()
        Me.cmdOPS3 = New System.Windows.Forms.Button()
        Me.cmdOPS5 = New System.Windows.Forms.Button()
        Me.cmdOPS2 = New System.Windows.Forms.Button()
        Me.cmdOPS6 = New System.Windows.Forms.Button()
        Me.cmdOPS1 = New System.Windows.Forms.Button()
        Me.fraSeq = New System.Windows.Forms.GroupBox()
        Me.cmdSequence1 = New System.Windows.Forms.Button()
        Me.fraCh = New System.Windows.Forms.GroupBox()
        Me.cmdChannel12 = New System.Windows.Forms.Button()
        Me.cmdChannel10 = New System.Windows.Forms.Button()
        Me.cmdChannel9 = New System.Windows.Forms.Button()
        Me.cmdChannel8 = New System.Windows.Forms.Button()
        Me.cmdChannel7 = New System.Windows.Forms.Button()
        Me.cmdChannel6 = New System.Windows.Forms.Button()
        Me.cmdChannel5 = New System.Windows.Forms.Button()
        Me.cmdChannel4 = New System.Windows.Forms.Button()
        Me.cmdChannel2 = New System.Windows.Forms.Button()
        Me.cmdChannel1 = New System.Windows.Forms.Button()
        Me.cmdComposite = New System.Windows.Forms.Button()
        Me.cmdChannel3 = New System.Windows.Forms.Button()
        Me.fraSys = New System.Windows.Forms.GroupBox()
        Me.cmdSystem5 = New System.Windows.Forms.Button()
        Me.cmdSystem4 = New System.Windows.Forms.Button()
        Me.cmdSystem3 = New System.Windows.Forms.Button()
        Me.cmdSystem1 = New System.Windows.Forms.Button()
        Me.cmdSystem2 = New System.Windows.Forms.Button()
        Me.fraFile = New System.Windows.Forms.GroupBox()
        Me.cmdFile5 = New System.Windows.Forms.Button()
        Me.cmdFile4 = New System.Windows.Forms.Button()
        Me.cmdFile1 = New System.Windows.Forms.Button()
        Me.cmdFile3 = New System.Windows.Forms.Button()
        Me.cmdFile2 = New System.Windows.Forms.Button()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblFilePath = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblShipNoMachinery = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblFileMode = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.fraChk = New System.Windows.Forms.GroupBox()
        Me.cmdChkFileCompare = New System.Windows.Forms.Button()
        Me.cmdChkChOutput = New System.Windows.Forms.Button()
        Me.cmdChkChID = New System.Windows.Forms.Button()
        Me.cmdChkChUse = New System.Windows.Forms.Button()
        Me.cmdCelar = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CmbTag = New System.Windows.Forms.ComboBox()
        Me.GrpSys = New System.Windows.Forms.GroupBox()
        Me.CmbAlmLvl = New System.Windows.Forms.ComboBox()
        Me.fraOtherTools = New System.Windows.Forms.GroupBox()
        Me.cmdLoadCurve = New System.Windows.Forms.Button()
        Me.cmdOtherTool = New System.Windows.Forms.Button()
        Me.btnJRCSbatch = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnPIDtrendSET = New System.Windows.Forms.Button()
        Me.fraCmp.SuspendLayout()
        Me.fraPrt.SuspendLayout()
        Me.fraExt.SuspendLayout()
        Me.fraOps.SuspendLayout()
        Me.fraSeq.SuspendLayout()
        Me.fraCh.SuspendLayout()
        Me.fraSys.SuspendLayout()
        Me.fraFile.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.fraChk.SuspendLayout()
        Me.GrpSys.SuspendLayout()
        Me.fraOtherTools.SuspendLayout()
        Me.SuspendLayout()
        '
        'fraCmp
        '
        Me.fraCmp.BackColor = System.Drawing.SystemColors.Control
        Me.fraCmp.Controls.Add(Me.cmdMeasuringCheck)
        Me.fraCmp.Controls.Add(Me.cmdErrorCheck)
        Me.fraCmp.Controls.Add(Me.cmdCompile1)
        Me.fraCmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCmp.Location = New System.Drawing.Point(652, 354)
        Me.fraCmp.Name = "fraCmp"
        Me.fraCmp.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCmp.Size = New System.Drawing.Size(148, 178)
        Me.fraCmp.TabIndex = 7
        Me.fraCmp.TabStop = False
        Me.fraCmp.Text = "Compile"
        '
        'cmdMeasuringCheck
        '
        Me.cmdMeasuringCheck.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMeasuringCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMeasuringCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMeasuringCheck.Location = New System.Drawing.Point(16, 130)
        Me.cmdMeasuringCheck.Name = "cmdMeasuringCheck"
        Me.cmdMeasuringCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMeasuringCheck.Size = New System.Drawing.Size(116, 40)
        Me.cmdMeasuringCheck.TabIndex = 2
        Me.cmdMeasuringCheck.Text = "Measuring point" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Check"
        Me.cmdMeasuringCheck.UseVisualStyleBackColor = True
        '
        'cmdErrorCheck
        '
        Me.cmdErrorCheck.BackColor = System.Drawing.SystemColors.Control
        Me.cmdErrorCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdErrorCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdErrorCheck.Location = New System.Drawing.Point(16, 76)
        Me.cmdErrorCheck.Name = "cmdErrorCheck"
        Me.cmdErrorCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdErrorCheck.Size = New System.Drawing.Size(116, 40)
        Me.cmdErrorCheck.TabIndex = 1
        Me.cmdErrorCheck.Text = "Error Check"
        Me.cmdErrorCheck.UseVisualStyleBackColor = True
        '
        'cmdCompile1
        '
        Me.cmdCompile1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCompile1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCompile1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCompile1.Location = New System.Drawing.Point(16, 24)
        Me.cmdCompile1.Name = "cmdCompile1"
        Me.cmdCompile1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCompile1.Size = New System.Drawing.Size(116, 40)
        Me.cmdCompile1.TabIndex = 0
        Me.cmdCompile1.Text = "Compiler"
        Me.cmdCompile1.UseVisualStyleBackColor = True
        '
        'fraPrt
        '
        Me.fraPrt.BackColor = System.Drawing.SystemColors.Control
        Me.fraPrt.Controls.Add(Me.cmdPrint5)
        Me.fraPrt.Controls.Add(Me.cmdPrint4)
        Me.fraPrt.Controls.Add(Me.cmdPrint1)
        Me.fraPrt.Controls.Add(Me.cmdPrint3)
        Me.fraPrt.Controls.Add(Me.cmdPrint2)
        Me.fraPrt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPrt.Location = New System.Drawing.Point(492, 130)
        Me.fraPrt.Name = "fraPrt"
        Me.fraPrt.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPrt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPrt.Size = New System.Drawing.Size(148, 238)
        Me.fraPrt.TabIndex = 6
        Me.fraPrt.TabStop = False
        Me.fraPrt.Text = "Print"
        '
        'cmdPrint5
        '
        Me.cmdPrint5.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint5.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint5.Location = New System.Drawing.Point(16, 168)
        Me.cmdPrint5.Name = "cmdPrint5"
        Me.cmdPrint5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint5.Size = New System.Drawing.Size(116, 40)
        Me.cmdPrint5.TabIndex = 4
        Me.cmdPrint5.Text = "Graph View Print"
        Me.cmdPrint5.UseVisualStyleBackColor = True
        '
        'cmdPrint4
        '
        Me.cmdPrint4.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint4.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint4.Location = New System.Drawing.Point(16, 121)
        Me.cmdPrint4.Name = "cmdPrint4"
        Me.cmdPrint4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint4.Size = New System.Drawing.Size(116, 40)
        Me.cmdPrint4.TabIndex = 3
        Me.cmdPrint4.Text = "Overview Print"
        Me.cmdPrint4.UseVisualStyleBackColor = True
        '
        'cmdPrint1
        '
        Me.cmdPrint1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint1.Location = New System.Drawing.Point(16, 24)
        Me.cmdPrint1.Name = "cmdPrint1"
        Me.cmdPrint1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint1.Size = New System.Drawing.Size(116, 40)
        Me.cmdPrint1.TabIndex = 0
        Me.cmdPrint1.Text = "Channel List Print"
        Me.cmdPrint1.UseVisualStyleBackColor = True
        '
        'cmdPrint3
        '
        Me.cmdPrint3.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint3.Location = New System.Drawing.Point(16, 214)
        Me.cmdPrint3.Name = "cmdPrint3"
        Me.cmdPrint3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint3.Size = New System.Drawing.Size(116, 20)
        Me.cmdPrint3.TabIndex = 2
        Me.cmdPrint3.Text = "Field Unit Print"
        Me.cmdPrint3.UseVisualStyleBackColor = True
        Me.cmdPrint3.Visible = False
        '
        'cmdPrint2
        '
        Me.cmdPrint2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint2.Location = New System.Drawing.Point(16, 72)
        Me.cmdPrint2.Name = "cmdPrint2"
        Me.cmdPrint2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint2.Size = New System.Drawing.Size(116, 40)
        Me.cmdPrint2.TabIndex = 1
        Me.cmdPrint2.Text = "Terminal Print"
        Me.cmdPrint2.UseVisualStyleBackColor = True
        '
        'fraExt
        '
        Me.fraExt.BackColor = System.Drawing.SystemColors.Control
        Me.fraExt.Controls.Add(Me.cmdExtAlarm1)
        Me.fraExt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraExt.Location = New System.Drawing.Point(332, 214)
        Me.fraExt.Name = "fraExt"
        Me.fraExt.Padding = New System.Windows.Forms.Padding(0)
        Me.fraExt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraExt.Size = New System.Drawing.Size(148, 76)
        Me.fraExt.TabIndex = 5
        Me.fraExt.TabStop = False
        Me.fraExt.Text = "Ext Alarm"
        '
        'cmdExtAlarm1
        '
        Me.cmdExtAlarm1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExtAlarm1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExtAlarm1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExtAlarm1.Location = New System.Drawing.Point(16, 24)
        Me.cmdExtAlarm1.Name = "cmdExtAlarm1"
        Me.cmdExtAlarm1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExtAlarm1.Size = New System.Drawing.Size(116, 40)
        Me.cmdExtAlarm1.TabIndex = 0
        Me.cmdExtAlarm1.Text = "Ext Alarm Editor"
        Me.cmdExtAlarm1.UseVisualStyleBackColor = True
        '
        'fraOps
        '
        Me.fraOps.BackColor = System.Drawing.SystemColors.Control
        Me.fraOps.Controls.Add(Me.cmdLoadCurve)
        Me.fraOps.Controls.Add(Me.cmdOPS4)
        Me.fraOps.Controls.Add(Me.cmdOPS3)
        Me.fraOps.Controls.Add(Me.cmdOPS5)
        Me.fraOps.Controls.Add(Me.cmdOPS2)
        Me.fraOps.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraOps.Location = New System.Drawing.Point(332, 296)
        Me.fraOps.Name = "fraOps"
        Me.fraOps.Padding = New System.Windows.Forms.Padding(0)
        Me.fraOps.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraOps.Size = New System.Drawing.Size(148, 266)
        Me.fraOps.TabIndex = 4
        Me.fraOps.TabStop = False
        Me.fraOps.Text = "OPS Function Set"
        '
        'cmdOPS4
        '
        Me.cmdOPS4.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOPS4.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOPS4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOPS4.Location = New System.Drawing.Point(16, 168)
        Me.cmdOPS4.Name = "cmdOPS4"
        Me.cmdOPS4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOPS4.Size = New System.Drawing.Size(116, 40)
        Me.cmdOPS4.TabIndex = 3
        Me.cmdOPS4.Text = "Log Format Set"
        Me.cmdOPS4.UseVisualStyleBackColor = True
        '
        'cmdOPS3
        '
        Me.cmdOPS3.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOPS3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOPS3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOPS3.Location = New System.Drawing.Point(16, 120)
        Me.cmdOPS3.Name = "cmdOPS3"
        Me.cmdOPS3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOPS3.Size = New System.Drawing.Size(116, 40)
        Me.cmdOPS3.TabIndex = 2
        Me.cmdOPS3.Text = "Graph Set"
        Me.cmdOPS3.UseVisualStyleBackColor = True
        '
        'cmdOPS5
        '
        Me.cmdOPS5.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOPS5.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOPS5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOPS5.Location = New System.Drawing.Point(16, 72)
        Me.cmdOPS5.Name = "cmdOPS5"
        Me.cmdOPS5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOPS5.Size = New System.Drawing.Size(116, 40)
        Me.cmdOPS5.TabIndex = 1
        Me.cmdOPS5.Text = "Screen Function Set"
        Me.cmdOPS5.UseVisualStyleBackColor = True
        '
        'cmdOPS2
        '
        Me.cmdOPS2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOPS2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOPS2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOPS2.Location = New System.Drawing.Point(16, 24)
        Me.cmdOPS2.Name = "cmdOPS2"
        Me.cmdOPS2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOPS2.Size = New System.Drawing.Size(116, 40)
        Me.cmdOPS2.TabIndex = 1
        Me.cmdOPS2.Text = "Main Menu Set"
        Me.cmdOPS2.UseVisualStyleBackColor = True
        '
        'cmdOPS6
        '
        Me.cmdOPS6.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOPS6.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOPS6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOPS6.Location = New System.Drawing.Point(16, 169)
        Me.cmdOPS6.Name = "cmdOPS6"
        Me.cmdOPS6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOPS6.Size = New System.Drawing.Size(116, 40)
        Me.cmdOPS6.TabIndex = 4
        Me.cmdOPS6.Text = "GWS CH Set"
        Me.cmdOPS6.UseVisualStyleBackColor = True
        '
        'cmdOPS1
        '
        Me.cmdOPS1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOPS1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOPS1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOPS1.Location = New System.Drawing.Point(348, 606)
        Me.cmdOPS1.Name = "cmdOPS1"
        Me.cmdOPS1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOPS1.Size = New System.Drawing.Size(116, 36)
        Me.cmdOPS1.TabIndex = 0
        Me.cmdOPS1.Text = "Screen Title Set"
        Me.cmdOPS1.UseVisualStyleBackColor = True
        Me.cmdOPS1.Visible = False
        '
        'fraSeq
        '
        Me.fraSeq.BackColor = System.Drawing.SystemColors.Control
        Me.fraSeq.Controls.Add(Me.cmdSequence1)
        Me.fraSeq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSeq.Location = New System.Drawing.Point(332, 130)
        Me.fraSeq.Name = "fraSeq"
        Me.fraSeq.Padding = New System.Windows.Forms.Padding(0)
        Me.fraSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSeq.Size = New System.Drawing.Size(148, 78)
        Me.fraSeq.TabIndex = 3
        Me.fraSeq.TabStop = False
        Me.fraSeq.Text = "Sequence Set"
        '
        'cmdSequence1
        '
        Me.cmdSequence1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSequence1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSequence1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSequence1.Location = New System.Drawing.Point(16, 24)
        Me.cmdSequence1.Name = "cmdSequence1"
        Me.cmdSequence1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSequence1.Size = New System.Drawing.Size(116, 40)
        Me.cmdSequence1.TabIndex = 0
        Me.cmdSequence1.Text = "Control Sequence Set"
        Me.cmdSequence1.UseVisualStyleBackColor = True
        '
        'fraCh
        '
        Me.fraCh.BackColor = System.Drawing.SystemColors.Control
        Me.fraCh.Controls.Add(Me.cmdChannel12)
        Me.fraCh.Controls.Add(Me.cmdChannel10)
        Me.fraCh.Controls.Add(Me.cmdChannel9)
        Me.fraCh.Controls.Add(Me.cmdChannel8)
        Me.fraCh.Controls.Add(Me.cmdChannel7)
        Me.fraCh.Controls.Add(Me.cmdChannel6)
        Me.fraCh.Controls.Add(Me.cmdChannel5)
        Me.fraCh.Controls.Add(Me.cmdChannel4)
        Me.fraCh.Controls.Add(Me.cmdChannel2)
        Me.fraCh.Controls.Add(Me.cmdChannel1)
        Me.fraCh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCh.Location = New System.Drawing.Point(172, 130)
        Me.fraCh.Name = "fraCh"
        Me.fraCh.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCh.Size = New System.Drawing.Size(148, 508)
        Me.fraCh.TabIndex = 2
        Me.fraCh.TabStop = False
        Me.fraCh.Text = "Channel Set"
        '
        'cmdChannel12
        '
        Me.cmdChannel12.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChannel12.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChannel12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChannel12.Location = New System.Drawing.Point(16, 460)
        Me.cmdChannel12.Name = "cmdChannel12"
        Me.cmdChannel12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChannel12.Size = New System.Drawing.Size(116, 43)
        Me.cmdChannel12.TabIndex = 10
        Me.cmdChannel12.Text = "EXT LAN Set"
        Me.cmdChannel12.UseVisualStyleBackColor = True
        '
        'cmdChannel10
        '
        Me.cmdChannel10.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChannel10.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChannel10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChannel10.Location = New System.Drawing.Point(16, 358)
        Me.cmdChannel10.Name = "cmdChannel10"
        Me.cmdChannel10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChannel10.Size = New System.Drawing.Size(116, 40)
        Me.cmdChannel10.TabIndex = 9
        Me.cmdChannel10.Text = "Data storage method Table Set"
        Me.cmdChannel10.UseVisualStyleBackColor = True
        '
        'cmdChannel9
        '
        Me.cmdChannel9.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChannel9.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChannel9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChannel9.Location = New System.Drawing.Point(16, 406)
        Me.cmdChannel9.Name = "cmdChannel9"
        Me.cmdChannel9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChannel9.Size = New System.Drawing.Size(116, 43)
        Me.cmdChannel9.TabIndex = 8
        Me.cmdChannel9.Text = "Data Forward Table Set"
        Me.cmdChannel9.UseVisualStyleBackColor = True
        '
        'cmdChannel8
        '
        Me.cmdChannel8.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChannel8.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChannel8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChannel8.Location = New System.Drawing.Point(16, 312)
        Me.cmdChannel8.Name = "cmdChannel8"
        Me.cmdChannel8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChannel8.Size = New System.Drawing.Size(116, 40)
        Me.cmdChannel8.TabIndex = 7
        Me.cmdChannel8.Text = "SIO Set"
        Me.cmdChannel8.UseVisualStyleBackColor = True
        '
        'cmdChannel7
        '
        Me.cmdChannel7.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChannel7.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChannel7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChannel7.Location = New System.Drawing.Point(16, 264)
        Me.cmdChannel7.Name = "cmdChannel7"
        Me.cmdChannel7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChannel7.Size = New System.Drawing.Size(116, 40)
        Me.cmdChannel7.TabIndex = 6
        Me.cmdChannel7.Text = "Control Use/          Not Use Set"
        Me.cmdChannel7.UseVisualStyleBackColor = True
        '
        'cmdChannel6
        '
        Me.cmdChannel6.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChannel6.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChannel6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChannel6.Location = New System.Drawing.Point(16, 216)
        Me.cmdChannel6.Name = "cmdChannel6"
        Me.cmdChannel6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChannel6.Size = New System.Drawing.Size(116, 40)
        Me.cmdChannel6.TabIndex = 5
        Me.cmdChannel6.Text = "Cylinder Deviation Set"
        Me.cmdChannel6.UseVisualStyleBackColor = True
        '
        'cmdChannel5
        '
        Me.cmdChannel5.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChannel5.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChannel5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChannel5.Location = New System.Drawing.Point(16, 168)
        Me.cmdChannel5.Name = "cmdChannel5"
        Me.cmdChannel5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChannel5.Size = New System.Drawing.Size(116, 40)
        Me.cmdChannel5.TabIndex = 4
        Me.cmdChannel5.Text = "Run Hour Set"
        Me.cmdChannel5.UseVisualStyleBackColor = True
        '
        'cmdChannel4
        '
        Me.cmdChannel4.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChannel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChannel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChannel4.Location = New System.Drawing.Point(16, 120)
        Me.cmdChannel4.Name = "cmdChannel4"
        Me.cmdChannel4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChannel4.Size = New System.Drawing.Size(116, 40)
        Me.cmdChannel4.TabIndex = 3
        Me.cmdChannel4.Text = "Group Repose Set"
        Me.cmdChannel4.UseVisualStyleBackColor = True
        '
        'cmdChannel2
        '
        Me.cmdChannel2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChannel2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChannel2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChannel2.Location = New System.Drawing.Point(16, 72)
        Me.cmdChannel2.Name = "cmdChannel2"
        Me.cmdChannel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChannel2.Size = New System.Drawing.Size(116, 40)
        Me.cmdChannel2.TabIndex = 1
        Me.cmdChannel2.Text = "Terminal Input"
        Me.cmdChannel2.UseVisualStyleBackColor = True
        '
        'cmdChannel1
        '
        Me.cmdChannel1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChannel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChannel1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChannel1.Location = New System.Drawing.Point(16, 24)
        Me.cmdChannel1.Name = "cmdChannel1"
        Me.cmdChannel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChannel1.Size = New System.Drawing.Size(116, 40)
        Me.cmdChannel1.TabIndex = 0
        Me.cmdChannel1.Text = "Channel List Input"
        Me.cmdChannel1.UseVisualStyleBackColor = True
        '
        'cmdComposite
        '
        Me.cmdComposite.BackColor = System.Drawing.SystemColors.Control
        Me.cmdComposite.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdComposite.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdComposite.Location = New System.Drawing.Point(508, 644)
        Me.cmdComposite.Name = "cmdComposite"
        Me.cmdComposite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdComposite.Size = New System.Drawing.Size(116, 40)
        Me.cmdComposite.TabIndex = 10
        Me.cmdComposite.Text = "Composite Table Set"
        Me.cmdComposite.UseVisualStyleBackColor = True
        Me.cmdComposite.Visible = False
        '
        'cmdChannel3
        '
        Me.cmdChannel3.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChannel3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChannel3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChannel3.Location = New System.Drawing.Point(348, 648)
        Me.cmdChannel3.Name = "cmdChannel3"
        Me.cmdChannel3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChannel3.Size = New System.Drawing.Size(116, 36)
        Me.cmdChannel3.TabIndex = 2
        Me.cmdChannel3.Text = "Output Set"
        Me.cmdChannel3.UseVisualStyleBackColor = True
        Me.cmdChannel3.Visible = False
        '
        'fraSys
        '
        Me.fraSys.BackColor = System.Drawing.SystemColors.Control
        Me.fraSys.Controls.Add(Me.cmdOPS6)
        Me.fraSys.Controls.Add(Me.cmdSystem5)
        Me.fraSys.Controls.Add(Me.cmdSystem4)
        Me.fraSys.Controls.Add(Me.cmdSystem3)
        Me.fraSys.Controls.Add(Me.cmdSystem1)
        Me.fraSys.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSys.Location = New System.Drawing.Point(12, 412)
        Me.fraSys.Name = "fraSys"
        Me.fraSys.Padding = New System.Windows.Forms.Padding(0)
        Me.fraSys.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSys.Size = New System.Drawing.Size(148, 267)
        Me.fraSys.TabIndex = 1
        Me.fraSys.TabStop = False
        Me.fraSys.Text = "System Set"
        '
        'cmdSystem5
        '
        Me.cmdSystem5.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSystem5.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSystem5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSystem5.Location = New System.Drawing.Point(16, 123)
        Me.cmdSystem5.Name = "cmdSystem5"
        Me.cmdSystem5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSystem5.Size = New System.Drawing.Size(116, 40)
        Me.cmdSystem5.TabIndex = 3
        Me.cmdSystem5.Text = "GWS Set"
        Me.cmdSystem5.UseVisualStyleBackColor = True
        '
        'cmdSystem4
        '
        Me.cmdSystem4.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSystem4.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSystem4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSystem4.Location = New System.Drawing.Point(16, 218)
        Me.cmdSystem4.Name = "cmdSystem4"
        Me.cmdSystem4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSystem4.Size = New System.Drawing.Size(116, 40)
        Me.cmdSystem4.TabIndex = 4
        Me.cmdSystem4.Text = "Printer Set"
        Me.cmdSystem4.UseVisualStyleBackColor = True
        '
        'cmdSystem3
        '
        Me.cmdSystem3.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSystem3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSystem3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSystem3.Location = New System.Drawing.Point(16, 75)
        Me.cmdSystem3.Name = "cmdSystem3"
        Me.cmdSystem3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSystem3.Size = New System.Drawing.Size(116, 40)
        Me.cmdSystem3.TabIndex = 2
        Me.cmdSystem3.Text = "OPS Set"
        Me.cmdSystem3.UseVisualStyleBackColor = True
        '
        'cmdSystem1
        '
        Me.cmdSystem1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSystem1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSystem1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSystem1.Location = New System.Drawing.Point(16, 24)
        Me.cmdSystem1.Name = "cmdSystem1"
        Me.cmdSystem1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSystem1.Size = New System.Drawing.Size(116, 40)
        Me.cmdSystem1.TabIndex = 0
        Me.cmdSystem1.Text = "Overall Set"
        Me.cmdSystem1.UseVisualStyleBackColor = True
        '
        'cmdSystem2
        '
        Me.cmdSystem2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSystem2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSystem2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSystem2.Location = New System.Drawing.Point(188, 644)
        Me.cmdSystem2.Name = "cmdSystem2"
        Me.cmdSystem2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSystem2.Size = New System.Drawing.Size(116, 20)
        Me.cmdSystem2.TabIndex = 1
        Me.cmdSystem2.Text = "FCU Set"
        Me.cmdSystem2.UseVisualStyleBackColor = True
        Me.cmdSystem2.Visible = False
        '
        'fraFile
        '
        Me.fraFile.BackColor = System.Drawing.SystemColors.Control
        Me.fraFile.Controls.Add(Me.cmdFile5)
        Me.fraFile.Controls.Add(Me.cmdFile4)
        Me.fraFile.Controls.Add(Me.cmdFile1)
        Me.fraFile.Controls.Add(Me.cmdFile3)
        Me.fraFile.Controls.Add(Me.cmdFile2)
        Me.fraFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraFile.Location = New System.Drawing.Point(12, 130)
        Me.fraFile.Name = "fraFile"
        Me.fraFile.Padding = New System.Windows.Forms.Padding(0)
        Me.fraFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraFile.Size = New System.Drawing.Size(148, 263)
        Me.fraFile.TabIndex = 0
        Me.fraFile.TabStop = False
        Me.fraFile.Text = "File"
        '
        'cmdFile5
        '
        Me.cmdFile5.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFile5.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFile5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFile5.Location = New System.Drawing.Point(16, 216)
        Me.cmdFile5.Name = "cmdFile5"
        Me.cmdFile5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFile5.Size = New System.Drawing.Size(116, 40)
        Me.cmdFile5.TabIndex = 4
        Me.cmdFile5.Text = "22KConverter"
        Me.cmdFile5.UseVisualStyleBackColor = True
        '
        'cmdFile4
        '
        Me.cmdFile4.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFile4.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFile4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFile4.Location = New System.Drawing.Point(16, 168)
        Me.cmdFile4.Name = "cmdFile4"
        Me.cmdFile4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFile4.Size = New System.Drawing.Size(116, 40)
        Me.cmdFile4.TabIndex = 3
        Me.cmdFile4.Text = "Save as"
        Me.cmdFile4.UseVisualStyleBackColor = True
        '
        'cmdFile1
        '
        Me.cmdFile1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFile1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFile1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFile1.Location = New System.Drawing.Point(16, 24)
        Me.cmdFile1.Name = "cmdFile1"
        Me.cmdFile1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFile1.Size = New System.Drawing.Size(116, 40)
        Me.cmdFile1.TabIndex = 0
        Me.cmdFile1.Text = "New"
        Me.cmdFile1.UseVisualStyleBackColor = True
        '
        'cmdFile3
        '
        Me.cmdFile3.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFile3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFile3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFile3.Location = New System.Drawing.Point(16, 120)
        Me.cmdFile3.Name = "cmdFile3"
        Me.cmdFile3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFile3.Size = New System.Drawing.Size(116, 40)
        Me.cmdFile3.TabIndex = 2
        Me.cmdFile3.Text = "Save"
        Me.cmdFile3.UseVisualStyleBackColor = True
        '
        'cmdFile2
        '
        Me.cmdFile2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFile2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFile2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFile2.Location = New System.Drawing.Point(16, 72)
        Me.cmdFile2.Name = "cmdFile2"
        Me.cmdFile2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFile2.Size = New System.Drawing.Size(116, 40)
        Me.cmdFile2.TabIndex = 1
        Me.cmdFile2.Text = "Open"
        Me.cmdFile2.UseVisualStyleBackColor = True
        '
        'lblFileName
        '
        Me.lblFileName.BackColor = System.Drawing.SystemColors.Control
        Me.lblFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFileName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFileName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFileName.Location = New System.Drawing.Point(102, 24)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFileName.Size = New System.Drawing.Size(100, 21)
        Me.lblFileName.TabIndex = 42
        Me.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(231, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Ship No."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(38, 28)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(59, 12)
        Me.Label10.TabIndex = 39
        Me.Label10.Text = "File Name"
        '
        'lblFilePath
        '
        Me.lblFilePath.BackColor = System.Drawing.SystemColors.Control
        Me.lblFilePath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFilePath.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFilePath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilePath.Location = New System.Drawing.Point(102, 76)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFilePath.Size = New System.Drawing.Size(494, 21)
        Me.lblFilePath.TabIndex = 46
        Me.lblFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(38, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(59, 12)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "File Path"
        '
        'lblShipNoMachinery
        '
        Me.lblShipNoMachinery.BackColor = System.Drawing.SystemColors.Control
        Me.lblShipNoMachinery.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShipNoMachinery.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShipNoMachinery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShipNoMachinery.Location = New System.Drawing.Point(284, 24)
        Me.lblShipNoMachinery.Name = "lblShipNoMachinery"
        Me.lblShipNoMachinery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShipNoMachinery.Size = New System.Drawing.Size(312, 21)
        Me.lblShipNoMachinery.TabIndex = 47
        Me.lblShipNoMachinery.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblFileName)
        Me.GroupBox1.Controls.Add(Me.lblShipNoMachinery)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.lblFilePath)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(612, 110)
        Me.GroupBox1.TabIndex = 48
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "File Infomation"
        '
        'lblFileMode
        '
        Me.lblFileMode.BackColor = System.Drawing.SystemColors.Control
        Me.lblFileMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFileMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFileMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFileMode.Location = New System.Drawing.Point(245, 667)
        Me.lblFileMode.Name = "lblFileMode"
        Me.lblFileMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFileMode.Size = New System.Drawing.Size(69, 21)
        Me.lblFileMode.TabIndex = 49
        Me.lblFileMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFileMode.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(180, 671)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(59, 12)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "File Mode"
        Me.Label4.Visible = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(668, 644)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(116, 40)
        Me.cmdExit.TabIndex = 49
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'fraChk
        '
        Me.fraChk.BackColor = System.Drawing.SystemColors.Control
        Me.fraChk.Controls.Add(Me.cmdChkFileCompare)
        Me.fraChk.Controls.Add(Me.cmdChkChOutput)
        Me.fraChk.Controls.Add(Me.cmdChkChID)
        Me.fraChk.Controls.Add(Me.cmdChkChUse)
        Me.fraChk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraChk.Location = New System.Drawing.Point(652, 130)
        Me.fraChk.Name = "fraChk"
        Me.fraChk.Padding = New System.Windows.Forms.Padding(0)
        Me.fraChk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraChk.Size = New System.Drawing.Size(148, 175)
        Me.fraChk.TabIndex = 52
        Me.fraChk.TabStop = False
        Me.fraChk.Text = "Check"
        '
        'cmdChkFileCompare
        '
        Me.cmdChkFileCompare.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChkFileCompare.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChkFileCompare.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChkFileCompare.Location = New System.Drawing.Point(16, 116)
        Me.cmdChkFileCompare.Name = "cmdChkFileCompare"
        Me.cmdChkFileCompare.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChkFileCompare.Size = New System.Drawing.Size(116, 40)
        Me.cmdChkFileCompare.TabIndex = 52
        Me.cmdChkFileCompare.Text = "File Compare"
        Me.cmdChkFileCompare.UseVisualStyleBackColor = True
        '
        'cmdChkChOutput
        '
        Me.cmdChkChOutput.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChkChOutput.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChkChOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChkChOutput.Location = New System.Drawing.Point(16, 70)
        Me.cmdChkChOutput.Name = "cmdChkChOutput"
        Me.cmdChkChOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChkChOutput.Size = New System.Drawing.Size(116, 40)
        Me.cmdChkChOutput.TabIndex = 52
        Me.cmdChkChOutput.Text = "Channel Output Setting"
        Me.cmdChkChOutput.UseVisualStyleBackColor = True
        '
        'cmdChkChID
        '
        Me.cmdChkChID.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChkChID.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChkChID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChkChID.Location = New System.Drawing.Point(16, 24)
        Me.cmdChkChID.Name = "cmdChkChID"
        Me.cmdChkChID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChkChID.Size = New System.Drawing.Size(116, 40)
        Me.cmdChkChID.TabIndex = 52
        Me.cmdChkChID.Text = "Channel ID Table"
        Me.cmdChkChID.UseVisualStyleBackColor = True
        '
        'cmdChkChUse
        '
        Me.cmdChkChUse.BackColor = System.Drawing.SystemColors.Control
        Me.cmdChkChUse.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChkChUse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChkChUse.Location = New System.Drawing.Point(16, 162)
        Me.cmdChkChUse.Name = "cmdChkChUse"
        Me.cmdChkChUse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChkChUse.Size = New System.Drawing.Size(116, 19)
        Me.cmdChkChUse.TabIndex = 52
        Me.cmdChkChUse.Text = "Channel Use Table"
        Me.cmdChkChUse.UseVisualStyleBackColor = True
        Me.cmdChkChUse.Visible = False
        '
        'cmdCelar
        '
        Me.cmdCelar.Location = New System.Drawing.Point(668, 538)
        Me.cmdCelar.Name = "cmdCelar"
        Me.cmdCelar.Size = New System.Drawing.Size(116, 38)
        Me.cmdCelar.TabIndex = 53
        Me.cmdCelar.Text = "Clear"
        Me.cmdCelar.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 12)
        Me.Label3.TabIndex = 59
        Me.Label3.Text = "Alarm Level"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 12)
        Me.Label6.TabIndex = 58
        Me.Label6.Text = "TagNo"
        '
        'CmbTag
        '
        Me.CmbTag.FormattingEnabled = True
        Me.CmbTag.Location = New System.Drawing.Point(92, 18)
        Me.CmbTag.Name = "CmbTag"
        Me.CmbTag.Size = New System.Drawing.Size(71, 20)
        Me.CmbTag.TabIndex = 56
        '
        'GrpSys
        '
        Me.GrpSys.Controls.Add(Me.Label3)
        Me.GrpSys.Controls.Add(Me.Label6)
        Me.GrpSys.Controls.Add(Me.CmbAlmLvl)
        Me.GrpSys.Controls.Add(Me.CmbTag)
        Me.GrpSys.Location = New System.Drawing.Point(630, 11)
        Me.GrpSys.Name = "GrpSys"
        Me.GrpSys.Size = New System.Drawing.Size(177, 94)
        Me.GrpSys.TabIndex = 57
        Me.GrpSys.TabStop = False
        Me.GrpSys.Text = "SystemSet"
        '
        'CmbAlmLvl
        '
        Me.CmbAlmLvl.FormattingEnabled = True
        Me.CmbAlmLvl.Location = New System.Drawing.Point(92, 57)
        Me.CmbAlmLvl.Name = "CmbAlmLvl"
        Me.CmbAlmLvl.Size = New System.Drawing.Size(71, 20)
        Me.CmbAlmLvl.TabIndex = 57
        '
        'fraOtherTools
        '
        Me.fraOtherTools.Controls.Add(Me.cmdOtherTool)
        Me.fraOtherTools.Location = New System.Drawing.Point(495, 394)
        Me.fraOtherTools.Name = "fraOtherTools"
        Me.fraOtherTools.Size = New System.Drawing.Size(143, 62)
        Me.fraOtherTools.TabIndex = 58
        Me.fraOtherTools.TabStop = False
        Me.fraOtherTools.Text = "Other Tools"
        '
        'cmdLoadCurve
        '
        Me.cmdLoadCurve.Location = New System.Drawing.Point(16, 219)
        Me.cmdLoadCurve.Name = "cmdLoadCurve"
        Me.cmdLoadCurve.Size = New System.Drawing.Size(116, 39)
        Me.cmdLoadCurve.TabIndex = 6
        Me.cmdLoadCurve.Text = "Load Curve Set"
        Me.cmdLoadCurve.UseVisualStyleBackColor = True
        '
        'cmdOtherTool
        '
        Me.cmdOtherTool.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOtherTool.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOtherTool.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOtherTool.Location = New System.Drawing.Point(13, 18)
        Me.cmdOtherTool.Name = "cmdOtherTool"
        Me.cmdOtherTool.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOtherTool.Size = New System.Drawing.Size(116, 40)
        Me.cmdOtherTool.TabIndex = 5
        Me.cmdOtherTool.Text = "Other Tools Menu"
        Me.cmdOtherTool.UseVisualStyleBackColor = True
        '
        'btnJRCSbatch
        '
        Me.btnJRCSbatch.Location = New System.Drawing.Point(549, 581)
        Me.btnJRCSbatch.Name = "btnJRCSbatch"
        Me.btnJRCSbatch.Size = New System.Drawing.Size(75, 28)
        Me.btnJRCSbatch.TabIndex = 59
        Me.btnJRCSbatch.Text = "JRCS"
        Me.btnJRCSbatch.UseVisualStyleBackColor = True
        Me.btnJRCSbatch.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(508, 522)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(57, 29)
        Me.Button1.TabIndex = 60
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'btnPIDtrendSET
        '
        Me.btnPIDtrendSET.BackColor = System.Drawing.SystemColors.Control
        Me.btnPIDtrendSET.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnPIDtrendSET.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnPIDtrendSET.Location = New System.Drawing.Point(348, 568)
        Me.btnPIDtrendSET.Name = "btnPIDtrendSET"
        Me.btnPIDtrendSET.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnPIDtrendSET.Size = New System.Drawing.Size(116, 32)
        Me.btnPIDtrendSET.TabIndex = 61
        Me.btnPIDtrendSET.Text = "PID Trend SET"
        Me.btnPIDtrendSET.UseVisualStyleBackColor = True
        '
        'frmMenuMain
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(810, 692)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnPIDtrendSET)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnJRCSbatch)
        Me.Controls.Add(Me.cmdSystem2)
        Me.Controls.Add(Me.fraOtherTools)
        Me.Controls.Add(Me.GrpSys)
        Me.Controls.Add(Me.cmdCelar)
        Me.Controls.Add(Me.fraChk)
        Me.Controls.Add(Me.lblFileMode)
        Me.Controls.Add(Me.cmdOPS1)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.fraCmp)
        Me.Controls.Add(Me.fraPrt)
        Me.Controls.Add(Me.fraExt)
        Me.Controls.Add(Me.fraOps)
        Me.Controls.Add(Me.cmdChannel3)
        Me.Controls.Add(Me.fraSeq)
        Me.Controls.Add(Me.fraCh)
        Me.Controls.Add(Me.fraSys)
        Me.Controls.Add(Me.fraFile)
        Me.Controls.Add(Me.cmdComposite)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(30, 30)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMenuMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MAIN MENU"
        Me.fraCmp.ResumeLayout(False)
        Me.fraPrt.ResumeLayout(False)
        Me.fraExt.ResumeLayout(False)
        Me.fraOps.ResumeLayout(False)
        Me.fraSeq.ResumeLayout(False)
        Me.fraCh.ResumeLayout(False)
        Me.fraSys.ResumeLayout(False)
        Me.fraFile.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.fraChk.ResumeLayout(False)
        Me.GrpSys.ResumeLayout(False)
        Me.GrpSys.PerformLayout()
        Me.fraOtherTools.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdFile5 As System.Windows.Forms.Button
    Public WithEvents cmdFile4 As System.Windows.Forms.Button
    Public WithEvents lblFilePath As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblShipNoMachinery As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents lblFileMode As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents cmdErrorCheck As System.Windows.Forms.Button
    Public WithEvents cmdComposite As System.Windows.Forms.Button
    Public WithEvents fraChk As System.Windows.Forms.GroupBox
    Public WithEvents cmdChkChUse As System.Windows.Forms.Button
    Public WithEvents cmdChkFileCompare As System.Windows.Forms.Button
    Public WithEvents cmdChkChID As System.Windows.Forms.Button
    Public WithEvents cmdOPS5 As System.Windows.Forms.Button
    Public WithEvents cmdChkChOutput As System.Windows.Forms.Button
    Public WithEvents cmdSystem5 As System.Windows.Forms.Button
    Public WithEvents cmdOPS6 As System.Windows.Forms.Button
    Friend WithEvents cmdCelar As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CmbTag As System.Windows.Forms.ComboBox
    Friend WithEvents GrpSys As System.Windows.Forms.GroupBox
    Friend WithEvents CmbAlmLvl As System.Windows.Forms.ComboBox
    Friend WithEvents fraOtherTools As System.Windows.Forms.GroupBox
    Public WithEvents cmdOtherTool As System.Windows.Forms.Button
    Friend WithEvents btnJRCSbatch As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Public WithEvents btnPIDtrendSET As System.Windows.Forms.Button
    Public WithEvents cmdChannel12 As System.Windows.Forms.Button
    Public WithEvents cmdMeasuringCheck As System.Windows.Forms.Button
    Friend WithEvents cmdLoadCurve As System.Windows.Forms.Button
#End Region

End Class
