<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsGraphFreeAlignment
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

    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button


    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.LblLayout1 = New System.Windows.Forms.Label()
        Me.LblLayout2 = New System.Windows.Forms.Label()
        Me.LblLayout3 = New System.Windows.Forms.Label()
        Me.LblLayout4 = New System.Windows.Forms.Label()
        Me.LblLayout5 = New System.Windows.Forms.Label()
        Me.LblLayout6 = New System.Windows.Forms.Label()
        Me.LblLayout7 = New System.Windows.Forms.Label()
        Me.LblLayout8 = New System.Windows.Forms.Label()
        Me.LblLayout16 = New System.Windows.Forms.Label()
        Me.LblLayout15 = New System.Windows.Forms.Label()
        Me.LblLayout14 = New System.Windows.Forms.Label()
        Me.LblLayout13 = New System.Windows.Forms.Label()
        Me.LblLayout12 = New System.Windows.Forms.Label()
        Me.LblLayout11 = New System.Windows.Forms.Label()
        Me.LblLayout10 = New System.Windows.Forms.Label()
        Me.LblLayout9 = New System.Windows.Forms.Label()
        Me.LblLayout24 = New System.Windows.Forms.Label()
        Me.LblLayout23 = New System.Windows.Forms.Label()
        Me.LblLayout22 = New System.Windows.Forms.Label()
        Me.LblLayout21 = New System.Windows.Forms.Label()
        Me.LblLayout20 = New System.Windows.Forms.Label()
        Me.LblLayout19 = New System.Windows.Forms.Label()
        Me.LblLayout18 = New System.Windows.Forms.Label()
        Me.LblLayout17 = New System.Windows.Forms.Label()
        Me.LblLayout32 = New System.Windows.Forms.Label()
        Me.LblLayout31 = New System.Windows.Forms.Label()
        Me.LblLayout30 = New System.Windows.Forms.Label()
        Me.LblLayout29 = New System.Windows.Forms.Label()
        Me.LblLayout28 = New System.Windows.Forms.Label()
        Me.LblLayout27 = New System.Windows.Forms.Label()
        Me.LblLayout26 = New System.Windows.Forms.Label()
        Me.LblLayout25 = New System.Windows.Forms.Label()
        Me.txtGraphTitle = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.shpRect = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblSelectedTag = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(787, 523)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 39
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(663, 523)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 38
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'LblLayout1
        '
        Me.LblLayout1.BackColor = System.Drawing.Color.SeaGreen
        Me.LblLayout1.Location = New System.Drawing.Point(30, 30)
        Me.LblLayout1.Name = "LblLayout1"
        Me.LblLayout1.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout1.TabIndex = 40
        Me.LblLayout1.Tag = "1,1,1"
        Me.LblLayout1.Text = "COUNTER"
        '
        'LblLayout2
        '
        Me.LblLayout2.BackColor = System.Drawing.Color.SeaGreen
        Me.LblLayout2.Location = New System.Drawing.Point(134, 30)
        Me.LblLayout2.Name = "LblLayout2"
        Me.LblLayout2.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout2.TabIndex = 41
        Me.LblLayout2.Tag = "2,1,2"
        '
        'LblLayout3
        '
        Me.LblLayout3.BackColor = System.Drawing.Color.DarkKhaki
        Me.LblLayout3.Location = New System.Drawing.Point(238, 30)
        Me.LblLayout3.Name = "LblLayout3"
        Me.LblLayout3.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout3.TabIndex = 42
        Me.LblLayout3.Tag = "3,1,3"
        Me.LblLayout3.Text = "BAR GRAPH"
        '
        'LblLayout4
        '
        Me.LblLayout4.BackColor = System.Drawing.Color.Maroon
        Me.LblLayout4.Location = New System.Drawing.Point(342, 30)
        Me.LblLayout4.Name = "LblLayout4"
        Me.LblLayout4.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout4.TabIndex = 43
        Me.LblLayout4.Tag = "4,1,4"
        Me.LblLayout4.Text = "ANALOG GRAPH"
        '
        'LblLayout5
        '
        Me.LblLayout5.BackColor = System.Drawing.Color.Maroon
        Me.LblLayout5.Location = New System.Drawing.Point(446, 30)
        Me.LblLayout5.Name = "LblLayout5"
        Me.LblLayout5.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout5.TabIndex = 44
        Me.LblLayout5.Tag = "5,1,5"
        '
        'LblLayout6
        '
        Me.LblLayout6.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout6.Location = New System.Drawing.Point(550, 30)
        Me.LblLayout6.Name = "LblLayout6"
        Me.LblLayout6.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout6.TabIndex = 45
        Me.LblLayout6.Tag = "6,1,6"
        '
        'LblLayout7
        '
        Me.LblLayout7.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout7.Location = New System.Drawing.Point(654, 30)
        Me.LblLayout7.Name = "LblLayout7"
        Me.LblLayout7.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout7.TabIndex = 46
        Me.LblLayout7.Tag = "7,1,7"
        '
        'LblLayout8
        '
        Me.LblLayout8.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout8.Location = New System.Drawing.Point(758, 30)
        Me.LblLayout8.Name = "LblLayout8"
        Me.LblLayout8.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout8.TabIndex = 47
        Me.LblLayout8.Tag = "8,1,8"
        '
        'LblLayout16
        '
        Me.LblLayout16.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout16.Location = New System.Drawing.Point(758, 134)
        Me.LblLayout16.Name = "LblLayout16"
        Me.LblLayout16.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout16.TabIndex = 55
        Me.LblLayout16.Tag = "8,2,16"
        '
        'LblLayout15
        '
        Me.LblLayout15.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout15.Location = New System.Drawing.Point(654, 134)
        Me.LblLayout15.Name = "LblLayout15"
        Me.LblLayout15.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout15.TabIndex = 54
        Me.LblLayout15.Tag = "7,2,15"
        '
        'LblLayout14
        '
        Me.LblLayout14.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout14.Location = New System.Drawing.Point(550, 134)
        Me.LblLayout14.Name = "LblLayout14"
        Me.LblLayout14.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout14.TabIndex = 53
        Me.LblLayout14.Tag = "6,2,14"
        '
        'LblLayout13
        '
        Me.LblLayout13.BackColor = System.Drawing.Color.Maroon
        Me.LblLayout13.Location = New System.Drawing.Point(446, 134)
        Me.LblLayout13.Name = "LblLayout13"
        Me.LblLayout13.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout13.TabIndex = 52
        Me.LblLayout13.Tag = "5,2,13"
        '
        'LblLayout12
        '
        Me.LblLayout12.BackColor = System.Drawing.Color.Maroon
        Me.LblLayout12.Location = New System.Drawing.Point(342, 134)
        Me.LblLayout12.Name = "LblLayout12"
        Me.LblLayout12.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout12.TabIndex = 51
        Me.LblLayout12.Tag = "4,2,12"
        '
        'LblLayout11
        '
        Me.LblLayout11.BackColor = System.Drawing.Color.DarkKhaki
        Me.LblLayout11.Location = New System.Drawing.Point(238, 134)
        Me.LblLayout11.Name = "LblLayout11"
        Me.LblLayout11.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout11.TabIndex = 50
        Me.LblLayout11.Tag = "3,2,11"
        '
        'LblLayout10
        '
        Me.LblLayout10.BackColor = System.Drawing.Color.CadetBlue
        Me.LblLayout10.Location = New System.Drawing.Point(134, 134)
        Me.LblLayout10.Name = "LblLayout10"
        Me.LblLayout10.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout10.TabIndex = 49
        Me.LblLayout10.Tag = "2,2,10"
        Me.LblLayout10.Text = "INDICATOR"
        '
        'LblLayout9
        '
        Me.LblLayout9.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout9.Location = New System.Drawing.Point(30, 134)
        Me.LblLayout9.Name = "LblLayout9"
        Me.LblLayout9.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout9.TabIndex = 48
        Me.LblLayout9.Tag = "1,2,9"
        '
        'LblLayout24
        '
        Me.LblLayout24.BackColor = System.Drawing.Color.Maroon
        Me.LblLayout24.Location = New System.Drawing.Point(758, 238)
        Me.LblLayout24.Name = "LblLayout24"
        Me.LblLayout24.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout24.TabIndex = 63
        Me.LblLayout24.Tag = "8,3,24"
        '
        'LblLayout23
        '
        Me.LblLayout23.BackColor = System.Drawing.Color.Maroon
        Me.LblLayout23.Location = New System.Drawing.Point(654, 238)
        Me.LblLayout23.Name = "LblLayout23"
        Me.LblLayout23.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout23.TabIndex = 62
        Me.LblLayout23.Tag = "7,3,23"
        Me.LblLayout23.Text = "ANALOG GRAPH"
        '
        'LblLayout22
        '
        Me.LblLayout22.BackColor = System.Drawing.Color.DarkKhaki
        Me.LblLayout22.Location = New System.Drawing.Point(550, 238)
        Me.LblLayout22.Name = "LblLayout22"
        Me.LblLayout22.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout22.TabIndex = 61
        Me.LblLayout22.Tag = "6,3,22"
        Me.LblLayout22.Text = "BAR GRAPH"
        '
        'LblLayout21
        '
        Me.LblLayout21.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout21.Location = New System.Drawing.Point(446, 238)
        Me.LblLayout21.Name = "LblLayout21"
        Me.LblLayout21.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout21.TabIndex = 60
        Me.LblLayout21.Tag = "5,3,21"
        '
        'LblLayout20
        '
        Me.LblLayout20.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout20.Location = New System.Drawing.Point(342, 238)
        Me.LblLayout20.Name = "LblLayout20"
        Me.LblLayout20.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout20.TabIndex = 59
        Me.LblLayout20.Tag = "4,3,20"
        '
        'LblLayout19
        '
        Me.LblLayout19.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout19.Location = New System.Drawing.Point(238, 238)
        Me.LblLayout19.Name = "LblLayout19"
        Me.LblLayout19.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout19.TabIndex = 58
        Me.LblLayout19.Tag = "3,3,19"
        '
        'LblLayout18
        '
        Me.LblLayout18.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout18.Location = New System.Drawing.Point(134, 238)
        Me.LblLayout18.Name = "LblLayout18"
        Me.LblLayout18.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout18.TabIndex = 57
        Me.LblLayout18.Tag = "2,3,18"
        '
        'LblLayout17
        '
        Me.LblLayout17.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout17.Location = New System.Drawing.Point(30, 238)
        Me.LblLayout17.Name = "LblLayout17"
        Me.LblLayout17.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout17.TabIndex = 56
        Me.LblLayout17.Tag = "1,3,17"
        '
        'LblLayout32
        '
        Me.LblLayout32.BackColor = System.Drawing.Color.Maroon
        Me.LblLayout32.Location = New System.Drawing.Point(758, 342)
        Me.LblLayout32.Name = "LblLayout32"
        Me.LblLayout32.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout32.TabIndex = 71
        Me.LblLayout32.Tag = "8,4,32"
        '
        'LblLayout31
        '
        Me.LblLayout31.BackColor = System.Drawing.Color.Maroon
        Me.LblLayout31.Location = New System.Drawing.Point(654, 342)
        Me.LblLayout31.Name = "LblLayout31"
        Me.LblLayout31.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout31.TabIndex = 70
        Me.LblLayout31.Tag = "7,4,31"
        '
        'LblLayout30
        '
        Me.LblLayout30.BackColor = System.Drawing.Color.DarkKhaki
        Me.LblLayout30.Location = New System.Drawing.Point(550, 342)
        Me.LblLayout30.Name = "LblLayout30"
        Me.LblLayout30.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout30.TabIndex = 69
        Me.LblLayout30.Tag = "6,4,30"
        '
        'LblLayout29
        '
        Me.LblLayout29.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout29.Location = New System.Drawing.Point(446, 342)
        Me.LblLayout29.Name = "LblLayout29"
        Me.LblLayout29.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout29.TabIndex = 68
        Me.LblLayout29.Tag = "5,4,29"
        '
        'LblLayout28
        '
        Me.LblLayout28.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout28.Location = New System.Drawing.Point(342, 342)
        Me.LblLayout28.Name = "LblLayout28"
        Me.LblLayout28.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout28.TabIndex = 67
        Me.LblLayout28.Tag = "4,4,28"
        '
        'LblLayout27
        '
        Me.LblLayout27.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout27.Location = New System.Drawing.Point(238, 342)
        Me.LblLayout27.Name = "LblLayout27"
        Me.LblLayout27.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout27.TabIndex = 66
        Me.LblLayout27.Tag = "3,4,27"
        '
        'LblLayout26
        '
        Me.LblLayout26.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout26.Location = New System.Drawing.Point(134, 342)
        Me.LblLayout26.Name = "LblLayout26"
        Me.LblLayout26.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout26.TabIndex = 65
        Me.LblLayout26.Tag = "2,4,26"
        '
        'LblLayout25
        '
        Me.LblLayout25.BackColor = System.Drawing.Color.DarkGray
        Me.LblLayout25.Location = New System.Drawing.Point(30, 342)
        Me.LblLayout25.Name = "LblLayout25"
        Me.LblLayout25.Size = New System.Drawing.Size(100, 100)
        Me.LblLayout25.TabIndex = 64
        Me.LblLayout25.Tag = "1,4,25"
        '
        'txtGraphTitle
        '
        Me.txtGraphTitle.AcceptsReturn = True
        Me.txtGraphTitle.Location = New System.Drawing.Point(83, 12)
        Me.txtGraphTitle.MaxLength = 0
        Me.txtGraphTitle.Name = "txtGraphTitle"
        Me.txtGraphTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGraphTitle.Size = New System.Drawing.Size(375, 19)
        Me.txtGraphTitle.TabIndex = 72
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(13, 15)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(71, 12)
        Me.Label11.TabIndex = 73
        Me.Label11.Text = "Graph Title"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LblLayout32)
        Me.GroupBox1.Controls.Add(Me.LblLayout31)
        Me.GroupBox1.Controls.Add(Me.LblLayout30)
        Me.GroupBox1.Controls.Add(Me.LblLayout29)
        Me.GroupBox1.Controls.Add(Me.LblLayout28)
        Me.GroupBox1.Controls.Add(Me.LblLayout27)
        Me.GroupBox1.Controls.Add(Me.LblLayout26)
        Me.GroupBox1.Controls.Add(Me.LblLayout25)
        Me.GroupBox1.Controls.Add(Me.LblLayout24)
        Me.GroupBox1.Controls.Add(Me.LblLayout23)
        Me.GroupBox1.Controls.Add(Me.LblLayout22)
        Me.GroupBox1.Controls.Add(Me.LblLayout21)
        Me.GroupBox1.Controls.Add(Me.LblLayout20)
        Me.GroupBox1.Controls.Add(Me.LblLayout19)
        Me.GroupBox1.Controls.Add(Me.LblLayout18)
        Me.GroupBox1.Controls.Add(Me.LblLayout17)
        Me.GroupBox1.Controls.Add(Me.LblLayout16)
        Me.GroupBox1.Controls.Add(Me.LblLayout15)
        Me.GroupBox1.Controls.Add(Me.LblLayout14)
        Me.GroupBox1.Controls.Add(Me.LblLayout13)
        Me.GroupBox1.Controls.Add(Me.LblLayout12)
        Me.GroupBox1.Controls.Add(Me.LblLayout11)
        Me.GroupBox1.Controls.Add(Me.LblLayout10)
        Me.GroupBox1.Controls.Add(Me.LblLayout9)
        Me.GroupBox1.Controls.Add(Me.LblLayout8)
        Me.GroupBox1.Controls.Add(Me.LblLayout7)
        Me.GroupBox1.Controls.Add(Me.LblLayout6)
        Me.GroupBox1.Controls.Add(Me.LblLayout5)
        Me.GroupBox1.Controls.Add(Me.LblLayout4)
        Me.GroupBox1.Controls.Add(Me.LblLayout3)
        Me.GroupBox1.Controls.Add(Me.LblLayout2)
        Me.GroupBox1.Controls.Add(Me.LblLayout1)
        Me.GroupBox1.Controls.Add(Me.ShapeContainer1)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(884, 464)
        Me.GroupBox1.TabIndex = 76
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Layout"
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(3, 15)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.shpRect})
        Me.ShapeContainer1.Size = New System.Drawing.Size(878, 446)
        Me.ShapeContainer1.TabIndex = 72
        Me.ShapeContainer1.TabStop = False
        '
        'shpRect
        '
        Me.shpRect.BorderColor = System.Drawing.Color.Fuchsia
        Me.shpRect.BorderWidth = 4
        Me.shpRect.Location = New System.Drawing.Point(233, 11)
        Me.shpRect.Name = "shpRect"
        Me.shpRect.Size = New System.Drawing.Size(104, 104)
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Location = New System.Drawing.Point(143, 523)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(113, 33)
        Me.cmdDelete.TabIndex = 78
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEdit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEdit.Location = New System.Drawing.Point(19, 523)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEdit.Size = New System.Drawing.Size(113, 33)
        Me.cmdEdit.TabIndex = 77
        Me.cmdEdit.Text = "Edit"
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblSelectedTag)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(308, 514)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(213, 47)
        Me.GroupBox2.TabIndex = 79
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Visible False"
        Me.GroupBox2.Visible = False
        '
        'lblSelectedTag
        '
        Me.lblSelectedTag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSelectedTag.Location = New System.Drawing.Point(98, 19)
        Me.lblSelectedTag.Name = "lblSelectedTag"
        Me.lblSelectedTag.Size = New System.Drawing.Size(96, 19)
        Me.lblSelectedTag.TabIndex = 1
        Me.lblSelectedTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SelectedTag"
        '
        'frmOpsGraphFreeAlignment
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(915, 573)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtGraphTitle)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsGraphFreeAlignment"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FREE GRAPH SETUP"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblLayout1 As System.Windows.Forms.Label
    Friend WithEvents LblLayout2 As System.Windows.Forms.Label
    Friend WithEvents LblLayout3 As System.Windows.Forms.Label
    Friend WithEvents LblLayout4 As System.Windows.Forms.Label
    Friend WithEvents LblLayout5 As System.Windows.Forms.Label
    Friend WithEvents LblLayout6 As System.Windows.Forms.Label
    Friend WithEvents LblLayout7 As System.Windows.Forms.Label
    Friend WithEvents LblLayout8 As System.Windows.Forms.Label
    Friend WithEvents LblLayout16 As System.Windows.Forms.Label
    Friend WithEvents LblLayout15 As System.Windows.Forms.Label
    Friend WithEvents LblLayout14 As System.Windows.Forms.Label
    Friend WithEvents LblLayout13 As System.Windows.Forms.Label
    Friend WithEvents LblLayout12 As System.Windows.Forms.Label
    Friend WithEvents LblLayout11 As System.Windows.Forms.Label
    Friend WithEvents LblLayout10 As System.Windows.Forms.Label
    Friend WithEvents LblLayout9 As System.Windows.Forms.Label
    Friend WithEvents LblLayout24 As System.Windows.Forms.Label
    Friend WithEvents LblLayout23 As System.Windows.Forms.Label
    Friend WithEvents LblLayout22 As System.Windows.Forms.Label
    Friend WithEvents LblLayout21 As System.Windows.Forms.Label
    Friend WithEvents LblLayout20 As System.Windows.Forms.Label
    Friend WithEvents LblLayout19 As System.Windows.Forms.Label
    Friend WithEvents LblLayout18 As System.Windows.Forms.Label
    Friend WithEvents LblLayout17 As System.Windows.Forms.Label
    Friend WithEvents LblLayout32 As System.Windows.Forms.Label
    Friend WithEvents LblLayout31 As System.Windows.Forms.Label
    Friend WithEvents LblLayout30 As System.Windows.Forms.Label
    Friend WithEvents LblLayout29 As System.Windows.Forms.Label
    Friend WithEvents LblLayout28 As System.Windows.Forms.Label
    Friend WithEvents LblLayout27 As System.Windows.Forms.Label
    Friend WithEvents LblLayout26 As System.Windows.Forms.Label
    Friend WithEvents LblLayout25 As System.Windows.Forms.Label
    Public WithEvents txtGraphTitle As System.Windows.Forms.TextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents shpRect As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSelectedTag As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
#End Region

End Class
