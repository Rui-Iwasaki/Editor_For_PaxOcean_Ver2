<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChExtLANDetail
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

    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmbStopBit As System.Windows.Forms.ComboBox
    Public WithEvents cmbCommType2 As System.Windows.Forms.ComboBox
    Public WithEvents cmbCommType1 As System.Windows.Forms.ComboBox
    Public WithEvents txtNumberCH As System.Windows.Forms.TextBox
    Public WithEvents txtUseallyTransmit As System.Windows.Forms.TextBox
    Public WithEvents txtInitialTransmit As System.Windows.Forms.TextBox
    Public WithEvents txtUseallyTimeout As System.Windows.Forms.TextBox
    Public WithEvents txtInitialTimeout As System.Windows.Forms.TextBox
    Public WithEvents cmbDuplet As System.Windows.Forms.ComboBox
    Public WithEvents cmbDataBit As System.Windows.Forms.ComboBox
    Public WithEvents cmbParityBit As System.Windows.Forms.ComboBox
    Public WithEvents cmbCom As System.Windows.Forms.ComboBox
    Public WithEvents cmbMC As System.Windows.Forms.ComboBox
    Public WithEvents Label113 As System.Windows.Forms.Label
    Public WithEvents Label112 As System.Windows.Forms.Label
    Public WithEvents Label111 As System.Windows.Forms.Label
    Public WithEvents Label110 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmbPort As System.Windows.Forms.ComboBox
    Public WithEvents Label11 As System.Windows.Forms.Label

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.frame1 = New System.Windows.Forms.GroupBox()
        Me.cmdCommType2Set = New System.Windows.Forms.Button()
        Me.txtCommType2ManualInputValue = New System.Windows.Forms.TextBox()
        Me.txtExtComID = New System.Windows.Forms.TextBox()
        Me.cmbPriority = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdBinary = New System.Windows.Forms.Button()
        Me.txtRetryCount = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grdNode = New Editor.clsDataGridViewPlus()
        Me.cmbStopBit = New System.Windows.Forms.ComboBox()
        Me.cmbCommType2 = New System.Windows.Forms.ComboBox()
        Me.cmbCommType1 = New System.Windows.Forms.ComboBox()
        Me.txtNumberCH = New System.Windows.Forms.TextBox()
        Me.txtUseallyTransmit = New System.Windows.Forms.TextBox()
        Me.txtInitialTransmit = New System.Windows.Forms.TextBox()
        Me.txtUseallyTimeout = New System.Windows.Forms.TextBox()
        Me.txtInitialTimeout = New System.Windows.Forms.TextBox()
        Me.cmbDuplet = New System.Windows.Forms.ComboBox()
        Me.cmbDataBit = New System.Windows.Forms.ComboBox()
        Me.cmbParityBit = New System.Windows.Forms.ComboBox()
        Me.cmbCom = New System.Windows.Forms.ComboBox()
        Me.cmbMC = New System.Windows.Forms.ComboBox()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbPort = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdImport = New System.Windows.Forms.Button()
        Me.cmdMake = New System.Windows.Forms.Button()
        Me.grdCH = New Editor.clsDataGridViewPlus()
        Me.chkSioExt = New System.Windows.Forms.CheckBox()
        Me.btnBinaryExt = New System.Windows.Forms.Button()
        Me.frame1.SuspendLayout()
        CType(Me.grdNode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(657, 636)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 39
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(781, 636)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 40
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'frame1
        '
        Me.frame1.BackColor = System.Drawing.SystemColors.Control
        Me.frame1.Controls.Add(Me.cmdCommType2Set)
        Me.frame1.Controls.Add(Me.txtCommType2ManualInputValue)
        Me.frame1.Controls.Add(Me.txtExtComID)
        Me.frame1.Controls.Add(Me.cmbPriority)
        Me.frame1.Controls.Add(Me.Label3)
        Me.frame1.Controls.Add(Me.Label4)
        Me.frame1.Controls.Add(Me.cmdBinary)
        Me.frame1.Controls.Add(Me.txtRetryCount)
        Me.frame1.Controls.Add(Me.Label1)
        Me.frame1.Controls.Add(Me.grdNode)
        Me.frame1.Controls.Add(Me.cmbStopBit)
        Me.frame1.Controls.Add(Me.cmbCommType2)
        Me.frame1.Controls.Add(Me.cmbCommType1)
        Me.frame1.Controls.Add(Me.txtNumberCH)
        Me.frame1.Controls.Add(Me.txtUseallyTransmit)
        Me.frame1.Controls.Add(Me.txtInitialTransmit)
        Me.frame1.Controls.Add(Me.txtUseallyTimeout)
        Me.frame1.Controls.Add(Me.txtInitialTimeout)
        Me.frame1.Controls.Add(Me.cmbDuplet)
        Me.frame1.Controls.Add(Me.cmbDataBit)
        Me.frame1.Controls.Add(Me.cmbParityBit)
        Me.frame1.Controls.Add(Me.cmbCom)
        Me.frame1.Controls.Add(Me.cmbMC)
        Me.frame1.Controls.Add(Me.Label113)
        Me.frame1.Controls.Add(Me.Label5)
        Me.frame1.Controls.Add(Me.Label112)
        Me.frame1.Controls.Add(Me.Label111)
        Me.frame1.Controls.Add(Me.Label110)
        Me.frame1.Controls.Add(Me.Label19)
        Me.frame1.Controls.Add(Me.Label18)
        Me.frame1.Controls.Add(Me.Label17)
        Me.frame1.Controls.Add(Me.Label16)
        Me.frame1.Controls.Add(Me.Label15)
        Me.frame1.Controls.Add(Me.Label14)
        Me.frame1.Controls.Add(Me.Label13)
        Me.frame1.Controls.Add(Me.Label12)
        Me.frame1.Controls.Add(Me.Label10)
        Me.frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frame1.Location = New System.Drawing.Point(16, 64)
        Me.frame1.Name = "frame1"
        Me.frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frame1.Size = New System.Drawing.Size(420, 581)
        Me.frame1.TabIndex = 2
        Me.frame1.TabStop = False
        '
        'cmdCommType2Set
        '
        Me.cmdCommType2Set.Location = New System.Drawing.Point(366, 346)
        Me.cmdCommType2Set.Name = "cmdCommType2Set"
        Me.cmdCommType2Set.Size = New System.Drawing.Size(34, 20)
        Me.cmdCommType2Set.TabIndex = 42
        Me.cmdCommType2Set.Text = "Set"
        Me.cmdCommType2Set.UseVisualStyleBackColor = True
        '
        'txtCommType2ManualInputValue
        '
        Me.txtCommType2ManualInputValue.Location = New System.Drawing.Point(309, 347)
        Me.txtCommType2ManualInputValue.Name = "txtCommType2ManualInputValue"
        Me.txtCommType2ManualInputValue.Size = New System.Drawing.Size(51, 19)
        Me.txtCommType2ManualInputValue.TabIndex = 41
        Me.txtCommType2ManualInputValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtExtComID
        '
        Me.txtExtComID.AcceptsReturn = True
        Me.txtExtComID.Location = New System.Drawing.Point(336, 24)
        Me.txtExtComID.MaxLength = 0
        Me.txtExtComID.Name = "txtExtComID"
        Me.txtExtComID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtComID.Size = New System.Drawing.Size(64, 19)
        Me.txtExtComID.TabIndex = 40
        Me.txtExtComID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbPriority
        '
        Me.cmbPriority.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPriority.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPriority.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPriority.Location = New System.Drawing.Point(79, 24)
        Me.cmbPriority.Name = "cmbPriority"
        Me.cmbPriority.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPriority.Size = New System.Drawing.Size(97, 20)
        Me.cmbPriority.TabIndex = 37
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(192, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(89, 12)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "ExtComID (Hex)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(7, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Priority"
        '
        'cmdBinary
        '
        Me.cmdBinary.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBinary.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBinary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBinary.Location = New System.Drawing.Point(12, 543)
        Me.cmdBinary.Name = "cmdBinary"
        Me.cmdBinary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBinary.Size = New System.Drawing.Size(61, 23)
        Me.cmdBinary.TabIndex = 36
        Me.cmdBinary.Text = "Binary"
        Me.cmdBinary.UseVisualStyleBackColor = True
        '
        'txtRetryCount
        '
        Me.txtRetryCount.AcceptsReturn = True
        Me.txtRetryCount.Location = New System.Drawing.Point(335, 263)
        Me.txtRetryCount.MaxLength = 0
        Me.txtRetryCount.Name = "txtRetryCount"
        Me.txtRetryCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRetryCount.Size = New System.Drawing.Size(64, 19)
        Me.txtRetryCount.TabIndex = 23
        Me.txtRetryCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(192, 266)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(71, 12)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Retry Count"
        '
        'grdNode
        '
        Me.grdNode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdNode.Location = New System.Drawing.Point(79, 379)
        Me.grdNode.Name = "grdNode"
        Me.grdNode.RowTemplate.Height = 21
        Me.grdNode.Size = New System.Drawing.Size(320, 187)
        Me.grdNode.TabIndex = 35
        '
        'cmbStopBit
        '
        Me.cmbStopBit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStopBit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStopBit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStopBit.Location = New System.Drawing.Point(79, 223)
        Me.cmbStopBit.Name = "cmbStopBit"
        Me.cmbStopBit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStopBit.Size = New System.Drawing.Size(97, 20)
        Me.cmbStopBit.TabIndex = 10
        '
        'cmbCommType2
        '
        Me.cmbCommType2.BackColor = System.Drawing.SystemColors.Window
        Me.cmbCommType2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbCommType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCommType2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbCommType2.Location = New System.Drawing.Point(79, 324)
        Me.cmbCommType2.Name = "cmbCommType2"
        Me.cmbCommType2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbCommType2.Size = New System.Drawing.Size(321, 20)
        Me.cmbCommType2.TabIndex = 13
        '
        'cmbCommType1
        '
        Me.cmbCommType1.BackColor = System.Drawing.SystemColors.Window
        Me.cmbCommType1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbCommType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCommType1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbCommType1.Location = New System.Drawing.Point(79, 298)
        Me.cmbCommType1.Name = "cmbCommType1"
        Me.cmbCommType1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbCommType1.Size = New System.Drawing.Size(321, 20)
        Me.cmbCommType1.TabIndex = 12
        '
        'txtNumberCH
        '
        Me.txtNumberCH.AcceptsReturn = True
        Me.txtNumberCH.Location = New System.Drawing.Point(336, 223)
        Me.txtNumberCH.MaxLength = 0
        Me.txtNumberCH.Name = "txtNumberCH"
        Me.txtNumberCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumberCH.Size = New System.Drawing.Size(64, 19)
        Me.txtNumberCH.TabIndex = 22
        Me.txtNumberCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtUseallyTransmit
        '
        Me.txtUseallyTransmit.AcceptsReturn = True
        Me.txtUseallyTransmit.Location = New System.Drawing.Point(336, 183)
        Me.txtUseallyTransmit.MaxLength = 0
        Me.txtUseallyTransmit.Name = "txtUseallyTransmit"
        Me.txtUseallyTransmit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUseallyTransmit.Size = New System.Drawing.Size(64, 19)
        Me.txtUseallyTransmit.TabIndex = 21
        Me.txtUseallyTransmit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtInitialTransmit
        '
        Me.txtInitialTransmit.AcceptsReturn = True
        Me.txtInitialTransmit.Location = New System.Drawing.Point(336, 143)
        Me.txtInitialTransmit.MaxLength = 0
        Me.txtInitialTransmit.Name = "txtInitialTransmit"
        Me.txtInitialTransmit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInitialTransmit.Size = New System.Drawing.Size(64, 19)
        Me.txtInitialTransmit.TabIndex = 20
        Me.txtInitialTransmit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtUseallyTimeout
        '
        Me.txtUseallyTimeout.AcceptsReturn = True
        Me.txtUseallyTimeout.Location = New System.Drawing.Point(336, 103)
        Me.txtUseallyTimeout.MaxLength = 0
        Me.txtUseallyTimeout.Name = "txtUseallyTimeout"
        Me.txtUseallyTimeout.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUseallyTimeout.Size = New System.Drawing.Size(64, 19)
        Me.txtUseallyTimeout.TabIndex = 19
        Me.txtUseallyTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtInitialTimeout
        '
        Me.txtInitialTimeout.AcceptsReturn = True
        Me.txtInitialTimeout.Location = New System.Drawing.Point(336, 63)
        Me.txtInitialTimeout.MaxLength = 0
        Me.txtInitialTimeout.Name = "txtInitialTimeout"
        Me.txtInitialTimeout.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInitialTimeout.Size = New System.Drawing.Size(64, 19)
        Me.txtInitialTimeout.TabIndex = 18
        Me.txtInitialTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbDuplet
        '
        Me.cmbDuplet.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDuplet.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDuplet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDuplet.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDuplet.Location = New System.Drawing.Point(79, 263)
        Me.cmbDuplet.Name = "cmbDuplet"
        Me.cmbDuplet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDuplet.Size = New System.Drawing.Size(106, 20)
        Me.cmbDuplet.TabIndex = 11
        '
        'cmbDataBit
        '
        Me.cmbDataBit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbDataBit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataBit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDataBit.Location = New System.Drawing.Point(79, 183)
        Me.cmbDataBit.Name = "cmbDataBit"
        Me.cmbDataBit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDataBit.Size = New System.Drawing.Size(97, 20)
        Me.cmbDataBit.TabIndex = 9
        '
        'cmbParityBit
        '
        Me.cmbParityBit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbParityBit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbParityBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParityBit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbParityBit.Location = New System.Drawing.Point(79, 143)
        Me.cmbParityBit.Name = "cmbParityBit"
        Me.cmbParityBit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbParityBit.Size = New System.Drawing.Size(97, 20)
        Me.cmbParityBit.TabIndex = 7
        '
        'cmbCom
        '
        Me.cmbCom.BackColor = System.Drawing.SystemColors.Window
        Me.cmbCom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbCom.Location = New System.Drawing.Point(79, 103)
        Me.cmbCom.Name = "cmbCom"
        Me.cmbCom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbCom.Size = New System.Drawing.Size(97, 20)
        Me.cmbCom.TabIndex = 5
        '
        'cmbMC
        '
        Me.cmbMC.BackColor = System.Drawing.SystemColors.Window
        Me.cmbMC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbMC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbMC.Location = New System.Drawing.Point(79, 63)
        Me.cmbMC.Name = "cmbMC"
        Me.cmbMC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbMC.Size = New System.Drawing.Size(97, 20)
        Me.cmbMC.TabIndex = 3
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.BackColor = System.Drawing.SystemColors.Control
        Me.Label113.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label113.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label113.Location = New System.Drawing.Point(7, 226)
        Me.Label113.Name = "Label113"
        Me.Label113.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label113.Size = New System.Drawing.Size(53, 12)
        Me.Label113.TabIndex = 32
        Me.Label113.Text = "Stop bit"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(160, 350)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(143, 12)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Manual Input Value(Hex)"
        '
        'Label112
        '
        Me.Label112.AutoSize = True
        Me.Label112.BackColor = System.Drawing.SystemColors.Control
        Me.Label112.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label112.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label112.Location = New System.Drawing.Point(8, 327)
        Me.Label112.Name = "Label112"
        Me.Label112.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label112.Size = New System.Drawing.Size(59, 12)
        Me.Label112.TabIndex = 24
        Me.Label112.Text = "COMMType2"
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.BackColor = System.Drawing.SystemColors.Control
        Me.Label111.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label111.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label111.Location = New System.Drawing.Point(8, 301)
        Me.Label111.Name = "Label111"
        Me.Label111.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label111.Size = New System.Drawing.Size(59, 12)
        Me.Label111.TabIndex = 23
        Me.Label111.Text = "COMMType1"
        '
        'Label110
        '
        Me.Label110.AutoSize = True
        Me.Label110.BackColor = System.Drawing.SystemColors.Control
        Me.Label110.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label110.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label110.Location = New System.Drawing.Point(192, 226)
        Me.Label110.Name = "Label110"
        Me.Label110.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label110.Size = New System.Drawing.Size(101, 12)
        Me.Label110.TabIndex = 17
        Me.Label110.Text = "The number of CH"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(192, 183)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(131, 24)
        Me.Label19.TabIndex = 16
        Me.Label19.Text = "Transmit waiting time" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(usually)"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(192, 143)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(137, 24)
        Me.Label18.TabIndex = 15
        Me.Label18.Text = "Transmit waiting time " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(initial)"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(192, 103)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(101, 24)
        Me.Label17.TabIndex = 14
        Me.Label17.Text = "Receive time out" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(usually)"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(192, 63)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(101, 24)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "Receive time out" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(initial)"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(8, 266)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(65, 12)
        Me.Label15.TabIndex = 12
        Me.Label15.Text = "Duplex set"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(7, 186)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(53, 12)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "Data bit"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(8, 146)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(65, 12)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Parity bit"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(8, 106)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(47, 12)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "COM bps"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(8, 63)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(47, 12)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "M/C set"
        '
        'cmbPort
        '
        Me.cmbPort.BackColor = System.Drawing.SystemColors.Window
        Me.cmbPort.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPort.Enabled = False
        Me.cmbPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPort.Location = New System.Drawing.Point(95, 32)
        Me.cmbPort.Name = "cmbPort"
        Me.cmbPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbPort.Size = New System.Drawing.Size(97, 20)
        Me.cmbPort.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(16, 36)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(77, 12)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "LAN Port No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(441, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(95, 12)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "Transmission CH"
        '
        'cmdImport
        '
        Me.cmdImport.BackColor = System.Drawing.SystemColors.Control
        Me.cmdImport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdImport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdImport.Location = New System.Drawing.Point(774, 577)
        Me.cmdImport.Name = "cmdImport"
        Me.cmdImport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdImport.Size = New System.Drawing.Size(120, 32)
        Me.cmdImport.TabIndex = 38
        Me.cmdImport.Text = "CSV Import"
        Me.cmdImport.UseVisualStyleBackColor = True
        '
        'cmdMake
        '
        Me.cmdMake.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMake.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMake.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMake.Location = New System.Drawing.Point(444, 577)
        Me.cmdMake.Name = "cmdMake"
        Me.cmdMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMake.Size = New System.Drawing.Size(120, 32)
        Me.cmdMake.TabIndex = 43
        Me.cmdMake.Text = "Make Data"
        Me.cmdMake.UseVisualStyleBackColor = True
        '
        'grdCH
        '
        Me.grdCH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCH.Location = New System.Drawing.Point(444, 72)
        Me.grdCH.Name = "grdCH"
        Me.grdCH.RowTemplate.Height = 21
        Me.grdCH.Size = New System.Drawing.Size(450, 499)
        Me.grdCH.TabIndex = 37
        '
        'chkSioExt
        '
        Me.chkSioExt.AutoSize = True
        Me.chkSioExt.Location = New System.Drawing.Point(453, 625)
        Me.chkSioExt.Name = "chkSioExt"
        Me.chkSioExt.Size = New System.Drawing.Size(156, 16)
        Me.chkSioExt.TabIndex = 44
        Me.chkSioExt.Text = "Extension Table exists"
        Me.chkSioExt.UseVisualStyleBackColor = True
        '
        'btnBinaryExt
        '
        Me.btnBinaryExt.BackColor = System.Drawing.SystemColors.Control
        Me.btnBinaryExt.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnBinaryExt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBinaryExt.Location = New System.Drawing.Point(453, 646)
        Me.btnBinaryExt.Name = "btnBinaryExt"
        Me.btnBinaryExt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnBinaryExt.Size = New System.Drawing.Size(61, 23)
        Me.btnBinaryExt.TabIndex = 45
        Me.btnBinaryExt.Text = "Binary"
        Me.btnBinaryExt.UseVisualStyleBackColor = True
        '
        'frmChExtLANDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(898, 704)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnBinaryExt)
        Me.Controls.Add(Me.chkSioExt)
        Me.Controls.Add(Me.cmdMake)
        Me.Controls.Add(Me.cmdImport)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grdCH)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.frame1)
        Me.Controls.Add(Me.cmbPort)
        Me.Controls.Add(Me.Label11)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChExtLANDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "EXT LAN SETUP DETAILS"
        Me.frame1.ResumeLayout(False)
        Me.frame1.PerformLayout()
        CType(Me.grdNode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtRetryCount As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cmdBinary As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents cmdImport As System.Windows.Forms.Button
    Public WithEvents cmdMake As System.Windows.Forms.Button
    Friend WithEvents grdNode As Editor.clsDataGridViewPlus
    Friend WithEvents grdCH As Editor.clsDataGridViewPlus
    Public WithEvents txtExtComID As System.Windows.Forms.TextBox
    Public WithEvents cmbPriority As System.Windows.Forms.ComboBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdCommType2Set As System.Windows.Forms.Button
    Friend WithEvents txtCommType2ManualInputValue As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkSioExt As System.Windows.Forms.CheckBox
    Public WithEvents btnBinaryExt As System.Windows.Forms.Button
#End Region

End Class
