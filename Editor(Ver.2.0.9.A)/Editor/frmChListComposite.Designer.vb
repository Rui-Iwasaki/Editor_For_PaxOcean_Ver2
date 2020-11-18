<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChListComposite
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

    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button


    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtAlmMimic = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cmbAlmLvl = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtTagNo = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmbTime = New System.Windows.Forms.ComboBox()
        Me.chkMrepose = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtShareChid = New System.Windows.Forms.TextBox()
        Me.lblShareChid = New System.Windows.Forms.Label()
        Me.lblShareType = New System.Windows.Forms.Label()
        Me.cmbShareType = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblBitSet = New System.Windows.Forms.Label()
        Me.txtSP = New System.Windows.Forms.TextBox()
        Me.txtPLC = New System.Windows.Forms.TextBox()
        Me.txtEP = New System.Windows.Forms.TextBox()
        Me.txtAC = New System.Windows.Forms.TextBox()
        Me.txtRL = New System.Windows.Forms.TextBox()
        Me.txtWK = New System.Windows.Forms.TextBox()
        Me.txtGWS = New System.Windows.Forms.TextBox()
        Me.txtSio = New System.Windows.Forms.TextBox()
        Me.txtSC = New System.Windows.Forms.TextBox()
        Me.txtDmy = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtDelayTimer = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtExtGroup = New System.Windows.Forms.TextBox()
        Me.txtGRep1 = New System.Windows.Forms.TextBox()
        Me.txtGRep2 = New System.Windows.Forms.TextBox()
        Me.cmbSysNo = New System.Windows.Forms.ComboBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtChNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cmdCompositeEdit = New System.Windows.Forms.Button()
        Me.txtStatus2 = New System.Windows.Forms.TextBox()
        Me.txtStatus1 = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.txtCompositeIndex = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtFilterCoeficient = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.grdAnyMap = New Editor.clsDataGridViewPlus()
        Me.grdBitStatusMap = New Editor.clsDataGridViewPlus()
        Me.lblDi8 = New System.Windows.Forms.Label()
        Me.lblDi6 = New System.Windows.Forms.Label()
        Me.lblDi7 = New System.Windows.Forms.Label()
        Me.lblDi1 = New System.Windows.Forms.Label()
        Me.lblDi2 = New System.Windows.Forms.Label()
        Me.lblDi5 = New System.Windows.Forms.Label()
        Me.lblDi3 = New System.Windows.Forms.Label()
        Me.lblDi4 = New System.Windows.Forms.Label()
        Me.txtPin = New System.Windows.Forms.TextBox()
        Me.txtPortNo = New System.Windows.Forms.TextBox()
        Me.txtFuNo = New System.Windows.Forms.TextBox()
        Me.cmbDataType = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtBitCount = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmdBeforeCH = New System.Windows.Forms.Button()
        Me.cmdNextCH = New System.Windows.Forms.Button()
        Me.lblDummy = New System.Windows.Forms.Label()
        Me.TabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdAnyMap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdBitStatusMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(860, 512)
        Me.cmdCancel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 4
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(740, 512)
        Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 3
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabPage1)
        Me.TabControl.Controls.Add(Me.TabPage2)
        Me.TabControl.Location = New System.Drawing.Point(8, 12)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(968, 492)
        Me.TabControl.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.Transparent
        Me.TabPage1.Controls.Add(Me.txtAlmMimic)
        Me.TabPage1.Controls.Add(Me.Label28)
        Me.TabPage1.Controls.Add(Me.Label27)
        Me.TabPage1.Controls.Add(Me.cmbAlmLvl)
        Me.TabPage1.Controls.Add(Me.Label22)
        Me.TabPage1.Controls.Add(Me.txtTagNo)
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Controls.Add(Me.cmbTime)
        Me.TabPage1.Controls.Add(Me.chkMrepose)
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.cmbSysNo)
        Me.TabPage1.Controls.Add(Me.txtRemarks)
        Me.TabPage1.Controls.Add(Me.Label36)
        Me.TabPage1.Controls.Add(Me.txtItemName)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.txtChNo)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Label37)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(960, 466)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Common"
        '
        'txtAlmMimic
        '
        Me.txtAlmMimic.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtAlmMimic.Location = New System.Drawing.Point(285, 373)
        Me.txtAlmMimic.MaxLength = 0
        Me.txtAlmMimic.Name = "txtAlmMimic"
        Me.txtAlmMimic.Size = New System.Drawing.Size(48, 19)
        Me.txtAlmMimic.TabIndex = 128
        Me.txtAlmMimic.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtAlmMimic.Visible = False
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(283, 356)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(101, 12)
        Me.Label28.TabIndex = 129
        Me.Label28.Text = "Fire Alarm Mimic"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label28.Visible = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(306, 140)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(71, 12)
        Me.Label27.TabIndex = 127
        Me.Label27.Text = "Alarm Level"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbAlmLvl
        '
        Me.cmbAlmLvl.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAlmLvl.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbAlmLvl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlmLvl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbAlmLvl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAlmLvl.Location = New System.Drawing.Point(383, 137)
        Me.cmbAlmLvl.Name = "cmbAlmLvl"
        Me.cmbAlmLvl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAlmLvl.Size = New System.Drawing.Size(97, 20)
        Me.cmbAlmLvl.TabIndex = 126
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(200, 53)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(47, 12)
        Me.Label22.TabIndex = 125
        Me.Label22.Text = "Tag No."
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTagNo
        '
        Me.txtTagNo.AcceptsReturn = True
        Me.txtTagNo.Location = New System.Drawing.Point(251, 50)
        Me.txtTagNo.MaxLength = 16
        Me.txtTagNo.Name = "txtTagNo"
        Me.txtTagNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTagNo.Size = New System.Drawing.Size(109, 19)
        Me.txtTagNo.TabIndex = 124
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(292, 184)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(119, 12)
        Me.Label21.TabIndex = 123
        Me.Label21.Text = "Unit of Delay Timer"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbTime
        '
        Me.cmbTime.BackColor = System.Drawing.SystemColors.Window
        Me.cmbTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbTime.Location = New System.Drawing.Point(416, 180)
        Me.cmbTime.Name = "cmbTime"
        Me.cmbTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbTime.Size = New System.Drawing.Size(64, 20)
        Me.cmbTime.TabIndex = 5
        '
        'chkMrepose
        '
        Me.chkMrepose.BackColor = System.Drawing.SystemColors.Control
        Me.chkMrepose.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMrepose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMrepose.Location = New System.Drawing.Point(269, 329)
        Me.chkMrepose.Name = "chkMrepose"
        Me.chkMrepose.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkMrepose.Size = New System.Drawing.Size(120, 19)
        Me.chkMrepose.TabIndex = 113
        Me.chkMrepose.Text = "Manual Repose"
        Me.chkMrepose.UseVisualStyleBackColor = True
        Me.chkMrepose.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtShareChid)
        Me.GroupBox5.Controls.Add(Me.lblShareChid)
        Me.GroupBox5.Controls.Add(Me.lblShareType)
        Me.GroupBox5.Controls.Add(Me.cmbShareType)
        Me.GroupBox5.Location = New System.Drawing.Point(20, 312)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(228, 80)
        Me.GroupBox5.TabIndex = 6
        Me.GroupBox5.TabStop = False
        '
        'txtShareChid
        '
        Me.txtShareChid.AcceptsReturn = True
        Me.txtShareChid.Location = New System.Drawing.Point(108, 48)
        Me.txtShareChid.MaxLength = 0
        Me.txtShareChid.Name = "txtShareChid"
        Me.txtShareChid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtShareChid.Size = New System.Drawing.Size(48, 19)
        Me.txtShareChid.TabIndex = 2
        Me.txtShareChid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblShareChid
        '
        Me.lblShareChid.AutoSize = True
        Me.lblShareChid.BackColor = System.Drawing.SystemColors.Control
        Me.lblShareChid.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShareChid.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShareChid.Location = New System.Drawing.Point(12, 52)
        Me.lblShareChid.Name = "lblShareChid"
        Me.lblShareChid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShareChid.Size = New System.Drawing.Size(83, 12)
        Me.lblShareChid.TabIndex = 106
        Me.lblShareChid.Text = "Remote CH No."
        Me.lblShareChid.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblShareType
        '
        Me.lblShareType.AutoSize = True
        Me.lblShareType.BackColor = System.Drawing.SystemColors.Control
        Me.lblShareType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShareType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShareType.Location = New System.Drawing.Point(32, 24)
        Me.lblShareType.Name = "lblShareType"
        Me.lblShareType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShareType.Size = New System.Drawing.Size(65, 12)
        Me.lblShareType.TabIndex = 105
        Me.lblShareType.Text = "Share Type"
        Me.lblShareType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbShareType
        '
        Me.cmbShareType.BackColor = System.Drawing.SystemColors.Window
        Me.cmbShareType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbShareType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbShareType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbShareType.Location = New System.Drawing.Point(108, 20)
        Me.cmbShareType.Name = "cmbShareType"
        Me.cmbShareType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbShareType.Size = New System.Drawing.Size(88, 20)
        Me.cmbShareType.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.lblBitSet)
        Me.GroupBox3.Controls.Add(Me.txtSP)
        Me.GroupBox3.Controls.Add(Me.txtPLC)
        Me.GroupBox3.Controls.Add(Me.txtEP)
        Me.GroupBox3.Controls.Add(Me.txtAC)
        Me.GroupBox3.Controls.Add(Me.txtRL)
        Me.GroupBox3.Controls.Add(Me.txtWK)
        Me.GroupBox3.Controls.Add(Me.txtGWS)
        Me.GroupBox3.Controls.Add(Me.txtSio)
        Me.GroupBox3.Controls.Add(Me.txtSC)
        Me.GroupBox3.Controls.Add(Me.txtDmy)
        Me.GroupBox3.Controls.Add(Me.Label25)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox3.Location = New System.Drawing.Point(20, 224)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox3.Size = New System.Drawing.Size(400, 84)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Flag"
        '
        'lblBitSet
        '
        Me.lblBitSet.AutoSize = True
        Me.lblBitSet.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitSet.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBitSet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitSet.Location = New System.Drawing.Point(75, 66)
        Me.lblBitSet.Name = "lblBitSet"
        Me.lblBitSet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBitSet.Size = New System.Drawing.Size(101, 12)
        Me.lblBitSet.TabIndex = 83
        Me.lblBitSet.Text = "Enter the bitset"
        Me.lblBitSet.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblBitSet.Visible = False
        '
        'txtSP
        '
        Me.txtSP.AcceptsReturn = True
        Me.txtSP.Location = New System.Drawing.Point(348, 44)
        Me.txtSP.MaxLength = 0
        Me.txtSP.Name = "txtSP"
        Me.txtSP.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSP.Size = New System.Drawing.Size(28, 19)
        Me.txtSP.TabIndex = 9
        Me.txtSP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPLC
        '
        Me.txtPLC.AcceptsReturn = True
        Me.txtPLC.Location = New System.Drawing.Point(312, 44)
        Me.txtPLC.MaxLength = 0
        Me.txtPLC.Name = "txtPLC"
        Me.txtPLC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPLC.Size = New System.Drawing.Size(28, 19)
        Me.txtPLC.TabIndex = 8
        Me.txtPLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtEP
        '
        Me.txtEP.AcceptsReturn = True
        Me.txtEP.Location = New System.Drawing.Point(276, 44)
        Me.txtEP.MaxLength = 0
        Me.txtEP.Name = "txtEP"
        Me.txtEP.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtEP.Size = New System.Drawing.Size(28, 19)
        Me.txtEP.TabIndex = 7
        Me.txtEP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAC
        '
        Me.txtAC.AcceptsReturn = True
        Me.txtAC.Location = New System.Drawing.Point(240, 44)
        Me.txtAC.MaxLength = 0
        Me.txtAC.Name = "txtAC"
        Me.txtAC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtAC.Size = New System.Drawing.Size(28, 19)
        Me.txtAC.TabIndex = 6
        Me.txtAC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRL
        '
        Me.txtRL.AcceptsReturn = True
        Me.txtRL.Location = New System.Drawing.Point(204, 44)
        Me.txtRL.MaxLength = 0
        Me.txtRL.Name = "txtRL"
        Me.txtRL.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtRL.Size = New System.Drawing.Size(28, 19)
        Me.txtRL.TabIndex = 5
        Me.txtRL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWK
        '
        Me.txtWK.AcceptsReturn = True
        Me.txtWK.Location = New System.Drawing.Point(168, 44)
        Me.txtWK.MaxLength = 0
        Me.txtWK.Name = "txtWK"
        Me.txtWK.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtWK.Size = New System.Drawing.Size(28, 19)
        Me.txtWK.TabIndex = 4
        Me.txtWK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtGWS
        '
        Me.txtGWS.AcceptsReturn = True
        Me.txtGWS.Location = New System.Drawing.Point(132, 44)
        Me.txtGWS.MaxLength = 0
        Me.txtGWS.Name = "txtGWS"
        Me.txtGWS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtGWS.Size = New System.Drawing.Size(28, 19)
        Me.txtGWS.TabIndex = 3
        Me.txtGWS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSio
        '
        Me.txtSio.AcceptsReturn = True
        Me.txtSio.Location = New System.Drawing.Point(96, 44)
        Me.txtSio.MaxLength = 0
        Me.txtSio.Name = "txtSio"
        Me.txtSio.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSio.Size = New System.Drawing.Size(28, 19)
        Me.txtSio.TabIndex = 2
        Me.txtSio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSC
        '
        Me.txtSC.AcceptsReturn = True
        Me.txtSC.Location = New System.Drawing.Point(60, 44)
        Me.txtSC.MaxLength = 0
        Me.txtSC.Name = "txtSC"
        Me.txtSC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSC.Size = New System.Drawing.Size(28, 19)
        Me.txtSC.TabIndex = 1
        Me.txtSC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDmy
        '
        Me.txtDmy.AcceptsReturn = True
        Me.txtDmy.Location = New System.Drawing.Point(24, 44)
        Me.txtDmy.MaxLength = 0
        Me.txtDmy.Name = "txtDmy"
        Me.txtDmy.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtDmy.Size = New System.Drawing.Size(28, 19)
        Me.txtDmy.TabIndex = 0
        Me.txtDmy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(352, 24)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(29, 12)
        Me.Label25.TabIndex = 81
        Me.Label25.Text = "LOCK"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(312, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(23, 12)
        Me.Label4.TabIndex = 80
        Me.Label4.Text = "PLC"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(276, 24)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(17, 12)
        Me.Label23.TabIndex = 79
        Me.Label23.Text = "EP"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(240, 24)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(17, 12)
        Me.Label24.TabIndex = 78
        Me.Label24.Text = "AC"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(204, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(17, 12)
        Me.Label9.TabIndex = 77
        Me.Label9.Text = "RL"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(168, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 76
        Me.Label5.Text = "WK"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(132, 24)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(23, 12)
        Me.Label14.TabIndex = 75
        Me.Label14.Text = "GWS"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(96, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(23, 12)
        Me.Label15.TabIndex = 74
        Me.Label15.Text = "SIO"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(60, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(17, 12)
        Me.Label12.TabIndex = 73
        Me.Label12.Text = "SC"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(24, 24)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(23, 12)
        Me.Label26.TabIndex = 72
        Me.Label26.Text = "Dmy"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox4.Controls.Add(Me.txtDelayTimer)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.txtExtGroup)
        Me.GroupBox4.Controls.Add(Me.txtGRep1)
        Me.GroupBox4.Controls.Add(Me.txtGRep2)
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox4.Location = New System.Drawing.Point(21, 137)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox4.Size = New System.Drawing.Size(256, 79)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Alarm"
        '
        'txtDelayTimer
        '
        Me.txtDelayTimer.AcceptsReturn = True
        Me.txtDelayTimer.Location = New System.Drawing.Point(76, 45)
        Me.txtDelayTimer.MaxLength = 0
        Me.txtDelayTimer.Name = "txtDelayTimer"
        Me.txtDelayTimer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayTimer.Size = New System.Drawing.Size(48, 19)
        Me.txtDelayTimer.TabIndex = 1
        Me.txtDelayTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(76, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(35, 12)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Delay"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(24, 27)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(35, 12)
        Me.Label16.TabIndex = 58
        Me.Label16.Text = "EXT.G"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(132, 27)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(41, 12)
        Me.Label17.TabIndex = 57
        Me.Label17.Text = "G REP1"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(184, 27)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(41, 12)
        Me.Label18.TabIndex = 56
        Me.Label18.Text = "G REP2"
        '
        'txtExtGroup
        '
        Me.txtExtGroup.AcceptsReturn = True
        Me.txtExtGroup.Location = New System.Drawing.Point(22, 45)
        Me.txtExtGroup.MaxLength = 0
        Me.txtExtGroup.Name = "txtExtGroup"
        Me.txtExtGroup.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGroup.TabIndex = 0
        Me.txtExtGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1
        '
        Me.txtGRep1.AcceptsReturn = True
        Me.txtGRep1.Location = New System.Drawing.Point(130, 45)
        Me.txtGRep1.MaxLength = 0
        Me.txtGRep1.Name = "txtGRep1"
        Me.txtGRep1.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep1.TabIndex = 2
        Me.txtGRep1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep2
        '
        Me.txtGRep2.AcceptsReturn = True
        Me.txtGRep2.Location = New System.Drawing.Point(184, 45)
        Me.txtGRep2.MaxLength = 0
        Me.txtGRep2.Name = "txtGRep2"
        Me.txtGRep2.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep2.TabIndex = 3
        Me.txtGRep2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbSysNo
        '
        Me.cmbSysNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSysNo.FormattingEnabled = True
        Me.cmbSysNo.Location = New System.Drawing.Point(95, 21)
        Me.cmbSysNo.Name = "cmbSysNo"
        Me.cmbSysNo.Size = New System.Drawing.Size(110, 20)
        Me.cmbSysNo.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.Location = New System.Drawing.Point(95, 104)
        Me.txtRemarks.MaxLength = 16
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(265, 19)
        Me.txtRemarks.TabIndex = 3
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(31, 107)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label36.Size = New System.Drawing.Size(47, 12)
        Me.Label36.TabIndex = 112
        Me.Label36.Text = "Remarks"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.Location = New System.Drawing.Point(95, 77)
        Me.txtItemName.MaxLength = 30
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(265, 19)
        Me.txtItemName.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(21, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 110
        Me.Label3.Text = "Item Name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtChNo
        '
        Me.txtChNo.AcceptsReturn = True
        Me.txtChNo.Location = New System.Drawing.Point(95, 50)
        Me.txtChNo.MaxLength = 0
        Me.txtChNo.Name = "txtChNo"
        Me.txtChNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtChNo.Size = New System.Drawing.Size(48, 19)
        Me.txtChNo.TabIndex = 1
        Me.txtChNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(42, 53)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 108
        Me.Label7.Text = "CH No."
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(40, 24)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(47, 12)
        Me.Label37.TabIndex = 107
        Me.Label37.Text = "Sys No."
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.Transparent
        Me.TabPage2.Controls.Add(Me.cmdCompositeEdit)
        Me.TabPage2.Controls.Add(Me.txtStatus2)
        Me.TabPage2.Controls.Add(Me.txtStatus1)
        Me.TabPage2.Controls.Add(Me.lblStatus)
        Me.TabPage2.Controls.Add(Me.cmdSelect)
        Me.TabPage2.Controls.Add(Me.txtCompositeIndex)
        Me.TabPage2.Controls.Add(Me.Label20)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.lblDi8)
        Me.TabPage2.Controls.Add(Me.lblDi6)
        Me.TabPage2.Controls.Add(Me.lblDi7)
        Me.TabPage2.Controls.Add(Me.lblDi1)
        Me.TabPage2.Controls.Add(Me.lblDi2)
        Me.TabPage2.Controls.Add(Me.lblDi5)
        Me.TabPage2.Controls.Add(Me.lblDi3)
        Me.TabPage2.Controls.Add(Me.lblDi4)
        Me.TabPage2.Controls.Add(Me.txtPin)
        Me.TabPage2.Controls.Add(Me.txtPortNo)
        Me.TabPage2.Controls.Add(Me.txtFuNo)
        Me.TabPage2.Controls.Add(Me.cmbDataType)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.txtBitCount)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(960, 466)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Composite"
        '
        'cmdCompositeEdit
        '
        Me.cmdCompositeEdit.Location = New System.Drawing.Point(194, 94)
        Me.cmdCompositeEdit.Name = "cmdCompositeEdit"
        Me.cmdCompositeEdit.Size = New System.Drawing.Size(57, 24)
        Me.cmdCompositeEdit.TabIndex = 145
        Me.cmdCompositeEdit.Text = "Edit"
        Me.cmdCompositeEdit.UseVisualStyleBackColor = True
        '
        'txtStatus2
        '
        Me.txtStatus2.AcceptsReturn = True
        Me.txtStatus2.Location = New System.Drawing.Point(508, 12)
        Me.txtStatus2.MaxLength = 0
        Me.txtStatus2.Name = "txtStatus2"
        Me.txtStatus2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatus2.Size = New System.Drawing.Size(72, 19)
        Me.txtStatus2.TabIndex = 12
        '
        'txtStatus1
        '
        Me.txtStatus1.AcceptsReturn = True
        Me.txtStatus1.Location = New System.Drawing.Point(434, 12)
        Me.txtStatus1.MaxLength = 0
        Me.txtStatus1.Name = "txtStatus1"
        Me.txtStatus1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatus1.Size = New System.Drawing.Size(72, 19)
        Me.txtStatus1.TabIndex = 11
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatus.Location = New System.Drawing.Point(356, 16)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.Size = New System.Drawing.Size(71, 12)
        Me.lblStatus.TabIndex = 153
        Me.lblStatus.Text = "Status Name"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdSelect
        '
        Me.cmdSelect.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelect.Location = New System.Drawing.Point(257, 94)
        Me.cmdSelect.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSelect.Size = New System.Drawing.Size(64, 24)
        Me.cmdSelect.TabIndex = 17
        Me.cmdSelect.Text = "SELECT"
        Me.cmdSelect.UseVisualStyleBackColor = True
        Me.cmdSelect.Visible = False
        '
        'txtCompositeIndex
        '
        Me.txtCompositeIndex.AcceptsReturn = True
        Me.txtCompositeIndex.Location = New System.Drawing.Point(140, 96)
        Me.txtCompositeIndex.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCompositeIndex.MaxLength = 0
        Me.txtCompositeIndex.Name = "txtCompositeIndex"
        Me.txtCompositeIndex.ReadOnly = True
        Me.txtCompositeIndex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompositeIndex.Size = New System.Drawing.Size(48, 19)
        Me.txtCompositeIndex.TabIndex = 152
        Me.txtCompositeIndex.TabStop = False
        Me.txtCompositeIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(16, 100)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label20.Size = New System.Drawing.Size(113, 12)
        Me.Label20.TabIndex = 151
        Me.Label20.Text = "Composite Table No"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtFilterCoeficient)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.grdAnyMap)
        Me.GroupBox1.Controls.Add(Me.grdBitStatusMap)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 136)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(865, 316)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detail (Only Display)"
        '
        'txtFilterCoeficient
        '
        Me.txtFilterCoeficient.AcceptsReturn = True
        Me.txtFilterCoeficient.Location = New System.Drawing.Point(619, 277)
        Me.txtFilterCoeficient.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtFilterCoeficient.MaxLength = 0
        Me.txtFilterCoeficient.Name = "txtFilterCoeficient"
        Me.txtFilterCoeficient.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFilterCoeficient.Size = New System.Drawing.Size(42, 19)
        Me.txtFilterCoeficient.TabIndex = 140
        Me.txtFilterCoeficient.TabStop = False
        Me.txtFilterCoeficient.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(617, 261)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(107, 12)
        Me.Label6.TabIndex = 143
        Me.Label6.Text = "Filter Coeficient"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(667, 280)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(47, 12)
        Me.Label19.TabIndex = 144
        Me.Label19.Text = "* 4msec"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(20, 245)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(251, 12)
        Me.Label1.TabIndex = 142
        Me.Label1.Text = "When Bit Status doesn’t apply to any map"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(20, 28)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(89, 12)
        Me.Label13.TabIndex = 141
        Me.Label13.Text = "Bit Status Map"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grdAnyMap
        '
        Me.grdAnyMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAnyMap.Location = New System.Drawing.Point(13, 261)
        Me.grdAnyMap.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.grdAnyMap.Name = "grdAnyMap"
        Me.grdAnyMap.RowTemplate.Height = 21
        Me.grdAnyMap.Size = New System.Drawing.Size(592, 40)
        Me.grdAnyMap.TabIndex = 139
        '
        'grdBitStatusMap
        '
        Me.grdBitStatusMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdBitStatusMap.Location = New System.Drawing.Point(13, 44)
        Me.grdBitStatusMap.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.grdBitStatusMap.Name = "grdBitStatusMap"
        Me.grdBitStatusMap.RowTemplate.Height = 21
        Me.grdBitStatusMap.Size = New System.Drawing.Size(832, 187)
        Me.grdBitStatusMap.TabIndex = 138
        '
        'lblDi8
        '
        Me.lblDi8.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi8.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi8.Location = New System.Drawing.Point(828, 67)
        Me.lblDi8.Name = "lblDi8"
        Me.lblDi8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi8.Size = New System.Drawing.Size(80, 23)
        Me.lblDi8.TabIndex = 149
        Me.lblDi8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi6
        '
        Me.lblDi6.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi6.Location = New System.Drawing.Point(668, 67)
        Me.lblDi6.Name = "lblDi6"
        Me.lblDi6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi6.Size = New System.Drawing.Size(80, 23)
        Me.lblDi6.TabIndex = 147
        Me.lblDi6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi7
        '
        Me.lblDi7.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi7.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi7.Location = New System.Drawing.Point(748, 67)
        Me.lblDi7.Name = "lblDi7"
        Me.lblDi7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi7.Size = New System.Drawing.Size(80, 23)
        Me.lblDi7.TabIndex = 148
        Me.lblDi7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi1
        '
        Me.lblDi1.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi1.Location = New System.Drawing.Point(268, 67)
        Me.lblDi1.Name = "lblDi1"
        Me.lblDi1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi1.Size = New System.Drawing.Size(80, 23)
        Me.lblDi1.TabIndex = 142
        Me.lblDi1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi2
        '
        Me.lblDi2.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi2.Location = New System.Drawing.Point(348, 67)
        Me.lblDi2.Name = "lblDi2"
        Me.lblDi2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi2.Size = New System.Drawing.Size(80, 23)
        Me.lblDi2.TabIndex = 143
        Me.lblDi2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi5
        '
        Me.lblDi5.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi5.Location = New System.Drawing.Point(588, 67)
        Me.lblDi5.Name = "lblDi5"
        Me.lblDi5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi5.Size = New System.Drawing.Size(80, 23)
        Me.lblDi5.TabIndex = 146
        Me.lblDi5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi3
        '
        Me.lblDi3.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi3.Location = New System.Drawing.Point(428, 67)
        Me.lblDi3.Name = "lblDi3"
        Me.lblDi3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi3.Size = New System.Drawing.Size(80, 23)
        Me.lblDi3.TabIndex = 144
        Me.lblDi3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDi4
        '
        Me.lblDi4.BackColor = System.Drawing.SystemColors.Control
        Me.lblDi4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDi4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDi4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDi4.Location = New System.Drawing.Point(508, 67)
        Me.lblDi4.Name = "lblDi4"
        Me.lblDi4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDi4.Size = New System.Drawing.Size(80, 23)
        Me.lblDi4.TabIndex = 145
        Me.lblDi4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPin
        '
        Me.txtPin.AcceptsReturn = True
        Me.txtPin.Location = New System.Drawing.Point(223, 68)
        Me.txtPin.MaxLength = 0
        Me.txtPin.Name = "txtPin"
        Me.txtPin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPin.Size = New System.Drawing.Size(40, 19)
        Me.txtPin.TabIndex = 16
        '
        'txtPortNo
        '
        Me.txtPortNo.AcceptsReturn = True
        Me.txtPortNo.Location = New System.Drawing.Point(181, 68)
        Me.txtPortNo.MaxLength = 0
        Me.txtPortNo.Name = "txtPortNo"
        Me.txtPortNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNo.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNo.TabIndex = 15
        '
        'txtFuNo
        '
        Me.txtFuNo.AcceptsReturn = True
        Me.txtFuNo.Location = New System.Drawing.Point(139, 68)
        Me.txtFuNo.MaxLength = 0
        Me.txtFuNo.Name = "txtFuNo"
        Me.txtFuNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNo.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNo.TabIndex = 14
        '
        'cmbDataType
        '
        Me.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataType.FormattingEnabled = True
        Me.cmbDataType.Location = New System.Drawing.Point(140, 13)
        Me.cmbDataType.Name = "cmbDataType"
        Me.cmbDataType.Size = New System.Drawing.Size(188, 20)
        Me.cmbDataType.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(71, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(59, 12)
        Me.Label8.TabIndex = 138
        Me.Label8.Text = "Data Type"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBitCount
        '
        Me.txtBitCount.AcceptsReturn = True
        Me.txtBitCount.Location = New System.Drawing.Point(140, 41)
        Me.txtBitCount.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtBitCount.MaxLength = 0
        Me.txtBitCount.Name = "txtBitCount"
        Me.txtBitCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBitCount.Size = New System.Drawing.Size(48, 19)
        Me.txtBitCount.TabIndex = 13
        Me.txtBitCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(20, 72)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(101, 12)
        Me.Label10.TabIndex = 121
        Me.Label10.Text = "Start FU Address"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(40, 43)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(89, 12)
        Me.Label11.TabIndex = 120
        Me.Label11.Text = "Terminal Count"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdBeforeCH
        '
        Me.cmdBeforeCH.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBeforeCH.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBeforeCH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBeforeCH.Location = New System.Drawing.Point(12, 512)
        Me.cmdBeforeCH.Name = "cmdBeforeCH"
        Me.cmdBeforeCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBeforeCH.Size = New System.Drawing.Size(113, 33)
        Me.cmdBeforeCH.TabIndex = 1
        Me.cmdBeforeCH.Text = "Before CH"
        Me.cmdBeforeCH.UseVisualStyleBackColor = True
        '
        'cmdNextCH
        '
        Me.cmdNextCH.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNextCH.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNextCH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNextCH.Location = New System.Drawing.Point(136, 512)
        Me.cmdNextCH.Name = "cmdNextCH"
        Me.cmdNextCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNextCH.Size = New System.Drawing.Size(113, 33)
        Me.cmdNextCH.TabIndex = 2
        Me.cmdNextCH.Text = "Next CH"
        Me.cmdNextCH.UseVisualStyleBackColor = True
        '
        'lblDummy
        '
        Me.lblDummy.AutoSize = True
        Me.lblDummy.Location = New System.Drawing.Point(878, 2)
        Me.lblDummy.Name = "lblDummy"
        Me.lblDummy.Size = New System.Drawing.Size(95, 12)
        Me.lblDummy.TabIndex = 11
        Me.lblDummy.Text = "F5:DummySetting"
        '
        'frmChListComposite
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(982, 555)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblDummy)
        Me.Controls.Add(Me.cmdBeforeCH)
        Me.Controls.Add(Me.cmdNextCH)
        Me.Controls.Add(Me.TabControl)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 30)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChListComposite"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CHANNEL LIST DIGITAL COMPOSITE"
        Me.TabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdAnyMap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdBitStatusMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Public WithEvents txtDelayTimer As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents txtExtGroup As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1 As System.Windows.Forms.TextBox
    Public WithEvents txtGRep2 As System.Windows.Forms.TextBox
    Friend WithEvents cmbSysNo As System.Windows.Forms.ComboBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtChNo As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents cmbDataType As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtBitCount As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents txtSP As System.Windows.Forms.TextBox
    Public WithEvents txtPLC As System.Windows.Forms.TextBox
    Public WithEvents txtEP As System.Windows.Forms.TextBox
    Public WithEvents txtAC As System.Windows.Forms.TextBox
    Public WithEvents txtRL As System.Windows.Forms.TextBox
    Public WithEvents txtWK As System.Windows.Forms.TextBox
    Public WithEvents txtGWS As System.Windows.Forms.TextBox
    Public WithEvents txtSio As System.Windows.Forms.TextBox
    Public WithEvents txtSC As System.Windows.Forms.TextBox
    Public WithEvents txtDmy As System.Windows.Forms.TextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents txtPin As System.Windows.Forms.TextBox
    Public WithEvents txtPortNo As System.Windows.Forms.TextBox
    Public WithEvents txtFuNo As System.Windows.Forms.TextBox
    Public WithEvents lblDi1 As System.Windows.Forms.Label
    Public WithEvents lblDi2 As System.Windows.Forms.Label
    Public WithEvents lblDi5 As System.Windows.Forms.Label
    Public WithEvents lblDi3 As System.Windows.Forms.Label
    Public WithEvents lblDi4 As System.Windows.Forms.Label
    Public WithEvents lblDi8 As System.Windows.Forms.Label
    Public WithEvents lblDi6 As System.Windows.Forms.Label
    Public WithEvents lblDi7 As System.Windows.Forms.Label
    Public WithEvents cmdBeforeCH As System.Windows.Forms.Button
    Public WithEvents cmdNextCH As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Public WithEvents txtShareChid As System.Windows.Forms.TextBox
    Public WithEvents lblShareChid As System.Windows.Forms.Label
    Public WithEvents lblShareType As System.Windows.Forms.Label
    Public WithEvents cmbShareType As System.Windows.Forms.ComboBox
    Public WithEvents chkMrepose As System.Windows.Forms.CheckBox
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents cmbTime As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents txtFilterCoeficient As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents grdAnyMap As Editor.clsDataGridViewPlus
    Friend WithEvents grdBitStatusMap As Editor.clsDataGridViewPlus
    Public WithEvents cmdSelect As System.Windows.Forms.Button
    Public WithEvents txtCompositeIndex As System.Windows.Forms.TextBox
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents cmdCompositeEdit As System.Windows.Forms.Button
    Public WithEvents lblBitSet As System.Windows.Forms.Label
    Public WithEvents txtStatus2 As System.Windows.Forms.TextBox
    Public WithEvents txtStatus1 As System.Windows.Forms.TextBox
    Friend WithEvents lblDummy As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents txtTagNo As System.Windows.Forms.TextBox
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents cmbAlmLvl As System.Windows.Forms.ComboBox
    Public WithEvents txtAlmMimic As System.Windows.Forms.TextBox
    Public WithEvents Label28 As System.Windows.Forms.Label
#End Region

End Class
