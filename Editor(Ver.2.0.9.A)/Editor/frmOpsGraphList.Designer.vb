<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsGraphList
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

    Public WithEvents cmdAnalogMater As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdEdit As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.cmdAnalogMater = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.TabFree = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.grdFree1 = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.grdFree2 = New System.Windows.Forms.DataGridView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.grdFree3 = New System.Windows.Forms.DataGridView()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.grdFree4 = New System.Windows.Forms.DataGridView()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.grdFree5 = New System.Windows.Forms.DataGridView()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.grdFree6 = New System.Windows.Forms.DataGridView()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.grdFree7 = New System.Windows.Forms.DataGridView()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.grdFree8 = New System.Windows.Forms.DataGridView()
        Me.TabPage9 = New System.Windows.Forms.TabPage()
        Me.grdFree9 = New System.Windows.Forms.DataGridView()
        Me.TabPage10 = New System.Windows.Forms.TabPage()
        Me.grdFree10 = New System.Windows.Forms.DataGridView()
        Me.optGraph = New System.Windows.Forms.RadioButton()
        Me.optFree = New System.Windows.Forms.RadioButton()
        Me.fraGraph = New System.Windows.Forms.GroupBox()
        Me.grdGraph = New Editor.clsDataGridViewPlus()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.optCargo = New System.Windows.Forms.RadioButton()
        Me.optMachinery = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.TabFree.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grdFree1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdFree2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.grdFree3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.grdFree4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.grdFree5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        CType(Me.grdFree6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage7.SuspendLayout()
        CType(Me.grdFree7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage8.SuspendLayout()
        CType(Me.grdFree8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage9.SuspendLayout()
        CType(Me.grdFree9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage10.SuspendLayout()
        CType(Me.grdFree10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraGraph.SuspendLayout()
        CType(Me.grdGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdAnalogMater
        '
        Me.cmdAnalogMater.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAnalogMater.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAnalogMater.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAnalogMater.Location = New System.Drawing.Point(159, 551)
        Me.cmdAnalogMater.Name = "cmdAnalogMater"
        Me.cmdAnalogMater.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAnalogMater.Size = New System.Drawing.Size(113, 38)
        Me.cmdAnalogMater.TabIndex = 7
        Me.cmdAnalogMater.Text = "Analog Meter Details"
        Me.cmdAnalogMater.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Location = New System.Drawing.Point(397, 507)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(113, 38)
        Me.cmdAdd.TabIndex = 6
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Location = New System.Drawing.Point(278, 507)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(113, 38)
        Me.cmdDelete.TabIndex = 5
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEdit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEdit.Location = New System.Drawing.Point(159, 507)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEdit.Size = New System.Drawing.Size(113, 38)
        Me.cmdEdit.TabIndex = 4
        Me.cmdEdit.Text = "Edit"
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(397, 551)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 38)
        Me.cmdExit.TabIndex = 1
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(278, 551)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 38)
        Me.cmdSave.TabIndex = 8
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'TabFree
        '
        Me.TabFree.Controls.Add(Me.TabPage1)
        Me.TabFree.Controls.Add(Me.TabPage2)
        Me.TabFree.Controls.Add(Me.TabPage3)
        Me.TabFree.Controls.Add(Me.TabPage4)
        Me.TabFree.Controls.Add(Me.TabPage5)
        Me.TabFree.Controls.Add(Me.TabPage6)
        Me.TabFree.Controls.Add(Me.TabPage7)
        Me.TabFree.Controls.Add(Me.TabPage8)
        Me.TabFree.Controls.Add(Me.TabPage9)
        Me.TabFree.Controls.Add(Me.TabPage10)
        Me.TabFree.Location = New System.Drawing.Point(12, 75)
        Me.TabFree.Name = "TabFree"
        Me.TabFree.SelectedIndex = 0
        Me.TabFree.Size = New System.Drawing.Size(650, 412)
        Me.TabFree.TabIndex = 10
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage1.Controls.Add(Me.grdFree1)
        Me.TabPage1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(642, 386)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "OPS #1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'grdFree1
        '
        Me.grdFree1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFree1.Location = New System.Drawing.Point(28, 14)
        Me.grdFree1.Name = "grdFree1"
        Me.grdFree1.RowTemplate.Height = 21
        Me.grdFree1.Size = New System.Drawing.Size(582, 356)
        Me.grdFree1.TabIndex = 100
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage2.Controls.Add(Me.grdFree2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(642, 386)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "OPS #2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'grdFree2
        '
        Me.grdFree2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFree2.Location = New System.Drawing.Point(28, 14)
        Me.grdFree2.Name = "grdFree2"
        Me.grdFree2.RowTemplate.Height = 21
        Me.grdFree2.Size = New System.Drawing.Size(582, 356)
        Me.grdFree2.TabIndex = 2
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage3.Controls.Add(Me.grdFree3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(642, 386)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "OPS #3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'grdFree3
        '
        Me.grdFree3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFree3.Location = New System.Drawing.Point(28, 14)
        Me.grdFree3.Name = "grdFree3"
        Me.grdFree3.RowTemplate.Height = 21
        Me.grdFree3.Size = New System.Drawing.Size(582, 356)
        Me.grdFree3.TabIndex = 2
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage4.Controls.Add(Me.grdFree4)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(642, 386)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "OPS #4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'grdFree4
        '
        Me.grdFree4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFree4.Location = New System.Drawing.Point(28, 14)
        Me.grdFree4.Name = "grdFree4"
        Me.grdFree4.RowTemplate.Height = 21
        Me.grdFree4.Size = New System.Drawing.Size(582, 356)
        Me.grdFree4.TabIndex = 2
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage5.Controls.Add(Me.grdFree5)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(642, 386)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "OPS #5"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'grdFree5
        '
        Me.grdFree5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFree5.Location = New System.Drawing.Point(28, 14)
        Me.grdFree5.Name = "grdFree5"
        Me.grdFree5.RowTemplate.Height = 21
        Me.grdFree5.Size = New System.Drawing.Size(582, 356)
        Me.grdFree5.TabIndex = 2
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage6.Controls.Add(Me.grdFree6)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(642, 386)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "OPS #6"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'grdFree6
        '
        Me.grdFree6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFree6.Location = New System.Drawing.Point(28, 14)
        Me.grdFree6.Name = "grdFree6"
        Me.grdFree6.RowTemplate.Height = 21
        Me.grdFree6.Size = New System.Drawing.Size(582, 356)
        Me.grdFree6.TabIndex = 2
        '
        'TabPage7
        '
        Me.TabPage7.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage7.Controls.Add(Me.grdFree7)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(642, 386)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "OPS #7"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'grdFree7
        '
        Me.grdFree7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFree7.Location = New System.Drawing.Point(28, 14)
        Me.grdFree7.Name = "grdFree7"
        Me.grdFree7.RowTemplate.Height = 21
        Me.grdFree7.Size = New System.Drawing.Size(582, 356)
        Me.grdFree7.TabIndex = 2
        '
        'TabPage8
        '
        Me.TabPage8.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage8.Controls.Add(Me.grdFree8)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(642, 386)
        Me.TabPage8.TabIndex = 7
        Me.TabPage8.Text = "OPS #8"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'grdFree8
        '
        Me.grdFree8.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFree8.Location = New System.Drawing.Point(28, 14)
        Me.grdFree8.Name = "grdFree8"
        Me.grdFree8.RowTemplate.Height = 21
        Me.grdFree8.Size = New System.Drawing.Size(582, 356)
        Me.grdFree8.TabIndex = 2
        '
        'TabPage9
        '
        Me.TabPage9.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage9.Controls.Add(Me.grdFree9)
        Me.TabPage9.Location = New System.Drawing.Point(4, 22)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Size = New System.Drawing.Size(642, 386)
        Me.TabPage9.TabIndex = 8
        Me.TabPage9.Text = "OPS #9"
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'grdFree9
        '
        Me.grdFree9.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFree9.Location = New System.Drawing.Point(28, 14)
        Me.grdFree9.Name = "grdFree9"
        Me.grdFree9.RowTemplate.Height = 21
        Me.grdFree9.Size = New System.Drawing.Size(582, 356)
        Me.grdFree9.TabIndex = 3
        '
        'TabPage10
        '
        Me.TabPage10.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage10.Controls.Add(Me.grdFree10)
        Me.TabPage10.Location = New System.Drawing.Point(4, 22)
        Me.TabPage10.Name = "TabPage10"
        Me.TabPage10.Size = New System.Drawing.Size(642, 386)
        Me.TabPage10.TabIndex = 9
        Me.TabPage10.Text = "OPS #10"
        Me.TabPage10.UseVisualStyleBackColor = True
        '
        'grdFree10
        '
        Me.grdFree10.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFree10.Location = New System.Drawing.Point(28, 14)
        Me.grdFree10.Name = "grdFree10"
        Me.grdFree10.RowTemplate.Height = 21
        Me.grdFree10.Size = New System.Drawing.Size(582, 356)
        Me.grdFree10.TabIndex = 4
        '
        'optGraph
        '
        Me.optGraph.Appearance = System.Windows.Forms.Appearance.Button
        Me.optGraph.BackColor = System.Drawing.SystemColors.Control
        Me.optGraph.Cursor = System.Windows.Forms.Cursors.Default
        Me.optGraph.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGraph.Location = New System.Drawing.Point(12, 19)
        Me.optGraph.Name = "optGraph"
        Me.optGraph.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optGraph.Size = New System.Drawing.Size(107, 40)
        Me.optGraph.TabIndex = 1
        Me.optGraph.Text = "Exhaust, Bar, Analog Meter"
        Me.optGraph.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optGraph.UseVisualStyleBackColor = True
        '
        'optFree
        '
        Me.optFree.Appearance = System.Windows.Forms.Appearance.Button
        Me.optFree.BackColor = System.Drawing.SystemColors.Control
        Me.optFree.Cursor = System.Windows.Forms.Cursors.Default
        Me.optFree.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optFree.Location = New System.Drawing.Point(125, 19)
        Me.optFree.Name = "optFree"
        Me.optFree.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optFree.Size = New System.Drawing.Size(107, 40)
        Me.optFree.TabIndex = 3
        Me.optFree.Text = "Free Graph"
        Me.optFree.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optFree.UseVisualStyleBackColor = True
        Me.optFree.Visible = False
        '
        'fraGraph
        '
        Me.fraGraph.Controls.Add(Me.grdGraph)
        Me.fraGraph.Location = New System.Drawing.Point(12, 75)
        Me.fraGraph.Name = "fraGraph"
        Me.fraGraph.Size = New System.Drawing.Size(650, 412)
        Me.fraGraph.TabIndex = 100
        Me.fraGraph.TabStop = False
        '
        'grdGraph
        '
        Me.grdGraph.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdGraph.Location = New System.Drawing.Point(32, 37)
        Me.grdGraph.Name = "grdGraph"
        Me.grdGraph.RowTemplate.Height = 21
        Me.grdGraph.Size = New System.Drawing.Size(582, 355)
        Me.grdGraph.TabIndex = 100
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'optCargo
        '
        Me.optCargo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optCargo.Appearance = System.Windows.Forms.Appearance.Button
        Me.optCargo.BackColor = System.Drawing.SystemColors.Control
        Me.optCargo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCargo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCargo.Location = New System.Drawing.Point(132, 10)
        Me.optCargo.Name = "optCargo"
        Me.optCargo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCargo.Size = New System.Drawing.Size(113, 40)
        Me.optCargo.TabIndex = 154
        Me.optCargo.TabStop = True
        Me.optCargo.Text = "Cargo"
        Me.optCargo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optCargo.UseVisualStyleBackColor = True
        '
        'optMachinery
        '
        Me.optMachinery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optMachinery.Appearance = System.Windows.Forms.Appearance.Button
        Me.optMachinery.BackColor = System.Drawing.SystemColors.Control
        Me.optMachinery.Cursor = System.Windows.Forms.Cursors.Default
        Me.optMachinery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMachinery.Location = New System.Drawing.Point(13, 10)
        Me.optMachinery.Name = "optMachinery"
        Me.optMachinery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optMachinery.Size = New System.Drawing.Size(113, 40)
        Me.optMachinery.TabIndex = 153
        Me.optMachinery.TabStop = True
        Me.optMachinery.Text = "Machinery"
        Me.optMachinery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optMachinery.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.optMachinery)
        Me.Panel1.Controls.Add(Me.optCargo)
        Me.Panel1.Location = New System.Drawing.Point(417, 9)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(254, 60)
        Me.Panel1.TabIndex = 156
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClear.Location = New System.Drawing.Point(16, 531)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClear.Size = New System.Drawing.Size(48, 38)
        Me.cmdClear.TabIndex = 157
        Me.cmdClear.Text = "Graph Clear"
        Me.cmdClear.UseVisualStyleBackColor = True
        '
        'frmOpsGraphList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(674, 608)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cmdAnalogMater)
        Me.Controls.Add(Me.fraGraph)
        Me.Controls.Add(Me.TabFree)
        Me.Controls.Add(Me.optFree)
        Me.Controls.Add(Me.optGraph)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsGraphList"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "GRAPH SETUP"
        Me.TabFree.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.grdFree1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.grdFree2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.grdFree3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.grdFree4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.grdFree5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        CType(Me.grdFree6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage7.ResumeLayout(False)
        CType(Me.grdFree7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage8.ResumeLayout(False)
        CType(Me.grdFree8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage9.ResumeLayout(False)
        CType(Me.grdFree9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage10.ResumeLayout(False)
        CType(Me.grdFree10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraGraph.ResumeLayout(False)
        CType(Me.grdGraph, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabFree As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grdFree1 As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage9 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage10 As System.Windows.Forms.TabPage
    Public WithEvents optGraph As System.Windows.Forms.RadioButton
    Public WithEvents optFree As System.Windows.Forms.RadioButton
    Friend WithEvents fraGraph As System.Windows.Forms.GroupBox
    Friend WithEvents grdFree2 As System.Windows.Forms.DataGridView
    Friend WithEvents grdFree3 As System.Windows.Forms.DataGridView
    Friend WithEvents grdFree4 As System.Windows.Forms.DataGridView
    Friend WithEvents grdFree5 As System.Windows.Forms.DataGridView
    Friend WithEvents grdFree6 As System.Windows.Forms.DataGridView
    Friend WithEvents grdFree7 As System.Windows.Forms.DataGridView
    Friend WithEvents grdFree8 As System.Windows.Forms.DataGridView
    Friend WithEvents grdFree9 As System.Windows.Forms.DataGridView
    Friend WithEvents grdFree10 As System.Windows.Forms.DataGridView
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents grdGraph As Editor.clsDataGridViewPlus
    Public WithEvents optCargo As System.Windows.Forms.RadioButton
    Public WithEvents optMachinery As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents cmdClear As System.Windows.Forms.Button
#End Region

End Class
