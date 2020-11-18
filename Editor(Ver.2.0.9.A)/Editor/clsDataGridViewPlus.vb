Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Windows.Forms

Public Class clsDataGridViewPlus
    Inherits System.Windows.Forms.DataGridView

    ' ----------------------------------------------------------------------------------
    ' DataGridViewのカラム幅をファイルへ保存／読込するための実装↓↓↓↓↓↓↓↓↓↓↓↓
    ' ----------------------------------------------------------------------------------
    ''' <summary>
    ''' DataGridViewのカラム幅をXML形式でシリアライズするためのクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ColWidths
        ''' <summary>
        ''' カラム幅s
        ''' </summary>
        Public Widths As Integer()
    End Class

    ''' <summary>
    ''' DataGridViewのカラム幅をファイルへ保存
    ''' </summary>
    ''' <remarks>DataGridViewのカラム幅をXML形式でシリアライズ</remarks>
    Public Sub SaveColWidths()
        Try
            ' EXEファイルのPATH
            Dim ExePath As String = System.AppDomain.CurrentDomain.BaseDirectory
            ' XMLファイルのPATH
            Dim XmlPath As String = ExePath & "\" & Me.Parent.Name & "_" & Me.Name & ".xml"
            ' XMLファイルオープン
            Dim sw As StreamWriter = New StreamWriter(XmlPath, False, Encoding.Default)
            Try
                ' シリアライザー
                Dim serializer As New XmlSerializer(GetType(ColWidths))
                ' DataGridViewのカラム幅取得
                Dim colw As New ColWidths
                ReDim colw.Widths(Me.Columns.Count - 1)
                For i As Integer = 0 To Me.Columns.Count - 1
                    colw.Widths(i) = Me.Columns(i).Width
                Next
                ' XMLファイル保存
                serializer.Serialize(sw, colw)
            Catch ex As Exception
            Finally
                ' XMLファイルクローズ
                If sw Is Nothing = False Then sw.Close()
            End Try
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' DataGridViewのカラム幅を前回保存したファイルから読込
    ''' </summary>
    ''' <remarks>DataGridViewのカラム幅をXML形式でデシリアライズ</remarks>
    Public Sub ReadColWidths()
        Try
            ' EXEファイルのPATH
            Dim ExePath As String = System.AppDomain.CurrentDomain.BaseDirectory
            ' XMLファイルのPATH
            Dim XmlPath As String = ExePath & "\" & Me.Parent.Name & "_" & Me.Name & ".xml"
            ' XMLファイルオープン
            Dim sr As StreamReader = New StreamReader(XmlPath, Encoding.Default)
            Try
                ' シリアライザー
                Dim serializer As New XmlSerializer(GetType(ColWidths))
                ' XMLファイル読込
                Dim colw As New ColWidths
                colw = CType(serializer.Deserialize(sr), ColWidths)
                ' DataGridViewにカラム幅設定
                For i As Integer = 0 To Me.Columns.Count - 1
                    Me.Columns(i).Width = colw.Widths(i)
                Next
            Catch ex As Exception
            Finally
                ' XMLファイルクローズ
                If sr Is Nothing = False Then sr.Close()
            End Try
        Catch ex As Exception
        End Try
    End Sub
    ' ----------------------------------------------------------------------------------
    ' DataGridViewのカラム幅をファイルへ保存／読込するための実装↑↑↑↑↑↑↑↑↑↑↑↑
    ' ----------------------------------------------------------------------------------

    ' ----------------------------------------------------------------------------------
    ' DataGridViewの各カラムへの入力可能文字を制限するための実装↓↓↓↓↓↓↓↓↓↓↓↓
    ' ----------------------------------------------------------------------------------
    ''' <summary>
    ''' カラムへの入力可能文字を指定するための配列
    ''' </summary>
    ''' <remarks>ColumnChars(0)="1234567890"</remarks>
    Public ColumnChars() As String
    ''' <summary>
    ''' 編集中のカラム番号
    ''' </summary>
    ''' <remarks></remarks>
    Private _editingColumn As Integer
    ''' <summary>
    ''' 編集中のTextBoxEditingControl
    ''' </summary>
    ''' <remarks></remarks>
    Private _editingCtrl As DataGridViewTextBoxEditingControl

    ''' <summary>
    ''' セルが編集中になった時の処理
    ''' </summary>
    ''' <param name="sender">イベントの発生元</param>
    ''' <param name="e">イベントの情報</param>
    ''' <remarks>編集中のTextBoxEditingControlにKeyPressイベント設定</remarks>
    Private Sub DataGridViewPlus_EditingControlShowing( _
        ByVal sender As Object, ByVal e As DataGridViewEditingControlShowingEventArgs) _
        Handles Me.EditingControlShowing
        ' 編集中のカラム番号を保存
        _editingColumn = CType(sender, DataGridView).CurrentCellAddress.X
        Try
            ' 編集中のTextBoxEditingControlにKeyPressイベント設定
            _editingCtrl = CType(e.Control, DataGridViewTextBoxEditingControl)
            AddHandler _editingCtrl.KeyPress, AddressOf DataGridViewPlus_CellKeyPress
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' セルの編集が終わった時の処理
    ''' </summary>
    ''' <param name="sender">イベントの発生元</param>
    ''' <param name="e">イベントの情報</param>
    ''' <remarks>編集中のTextBoxEditingControlからKeyPressイベント削除</remarks>
    Private Sub DataGridViewPlus_CellEndEdit(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs) Handles Me.CellEndEdit
        If _editingCtrl Is Nothing = False Then
            ' 編集中のTextBoxEditingControlからKeyPressイベント削除
            RemoveHandler _editingCtrl.KeyPress, AddressOf DataGridViewPlus_CellKeyPress
            _editingCtrl = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 編集中のTextBoxEditingControlのKeyPressの処理
    ''' </summary>
    ''' <param name="sender">イベントの発生元</param>
    ''' <param name="e">イベントの情報</param>
    ''' <remarks>力可能文字の判定</remarks>
    Private Sub DataGridViewPlus_CellKeyPress( _
        ByVal sender As Object, ByVal e As KeyPressEventArgs)
        ' カラムへの入力可能文字を指定するための配列が指定されているかチェック
        If IsArray(ColumnChars) Then
            ' カラムへの入力可能文字を指定するための配列数チェック
            If ColumnChars.GetLength(0) - 1 >= _editingColumn Then
                ' カラムへの入力可能文字が指定されているかチェック
                If ColumnChars(_editingColumn) <> "" Then
                    ' カラムへの入力可能文字かチェック
                    If InStr(ColumnChars(_editingColumn), e.KeyChar) <= 0 AndAlso _
                       e.KeyChar <> Chr(Keys.Back) Then
                        ' カラムへの入力可能文字では無いので無効
                        e.Handled = True
                    End If
                End If
            End If
        End If
    End Sub
    ' ----------------------------------------------------------------------------------
    ' DataGridViewの各カラムへの入力可能文字を制限するための実装↑↑↑↑↑↑↑↑↑↑↑↑
    ' ----------------------------------------------------------------------------------

    ' ----------------------------------------------------------------------------------
    ' DataGridViewでCtrl-Vキー押下時にクリップボードから貼り付けるための実装↓↓↓↓↓↓
    ' DataGridViewでDelやBackspaceキー押下時にセルの内容を消去するための実装↓↓↓↓↓↓
    ' ----------------------------------------------------------------------------------
    Public Sub DataGridViewPlus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Dim dgv As DataGridView = CType(sender, DataGridView)
        Dim x As Integer = dgv.CurrentCellAddress.X
        Dim y As Integer = dgv.CurrentCellAddress.Y
        Dim intRow As Integer = 0
        Dim intCol As Integer = 0

        If e.KeyCode = Keys.Delete Then

            '==========================
            ''Deleteの場合
            '==========================
            For i As Integer = 0 To dgv.SelectedCells.Count - 1

                If dgv(dgv.SelectedCells(i).ColumnIndex, dgv.SelectedCells(i).RowIndex).OwningColumn.Name.Substring(0, 3) = "txt" Then

                    ''ReadOnly設定の所は消去不可
                    If Not dgv(dgv.SelectedCells(i).ColumnIndex, dgv.SelectedCells(i).RowIndex).OwningColumn.ReadOnly Then

                        ''ReadOnly色設定の所は消去不可
                        If dgv(dgv.SelectedCells(i).ColumnIndex, dgv.SelectedCells(i).RowIndex).OwningColumn.DataGridView.CurrentCell.Style.BackColor <> gColorGridRowBackReadOnly Then


                            ''セルの内容を消去
                            dgv(dgv.SelectedCells(i).ColumnIndex, dgv.SelectedCells(i).RowIndex).Value = ""


                        End If

                    End If

                End If

            Next

        ElseIf e.KeyCode = Keys.Back Then

            '==========================
            ''BackSpaceの場合
            '==========================
            If dgv(x, y).OwningColumn.Name.Substring(0, 3) = "txt" Then

                ''ReadOnly設定の所は消去不可
                If Not dgv(x, y).OwningColumn.ReadOnly Then

                    ''ReadOnly色設定の所は消去不可
                    If dgv(x, y).OwningColumn.DataGridView.CurrentCell.Style.BackColor <> gColorGridRowBackReadOnly Then

                        ''セルの内容を消去
                        dgv(x, y).Value = ""

                    End If

                End If

            End If

        ElseIf (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.V Then

            '==========================
            ''Ctrl+Vの場合
            '==========================
            If dgv.ColumnCount = dgv.SelectedCells.Count Then
                x = 0
            End If

            ' クリップボードの内容を取得
            Dim clipText As String = Clipboard.GetText()

            ' 改行を変換
            clipText = clipText.Replace(vbCrLf, vbLf)
            clipText = clipText.Replace(vbCr, vbLf)

            ' 改行で分割
            Dim lines() As String = clipText.Split(vbLf)

            '------------------------
            ''ログフォーマットの場合
            '------------------------
            If dgv.Name = "grdLogCol1" Or _
               dgv.Name = "grdLogCol2" Then

                Dim intCopyCount As Integer = UBound(lines)

                If gintRow + intCopyCount > gintMaxRowOfEditPage Then
                    'Ver2.0.4.9 コポペ制限解除
                    'gintErrMsg = 1
                    'Exit Sub
                End If

            End If

            '------------------------
            ''CHリストの場合
            '------------------------
             Dim intChCnt As Integer = 0
            If dgv.Name = "grdCHList" Then

                For i = 0 To dgv.RowCount - 1
                    If (dgv(gCstChListColPosChType, i).Value <> Nothing) And (Val(dgv(gCstChListColPosChType, i).Value) <> 0) Then intChCnt += 1
                Next

            End If

            '行全体選択後の行全体の張付け
            If blnAllRowFlg = True And x = 0 Then

                Dim intgCopyListCol As Integer = 0

                'コピー開始行から複数行
                For intRowNo As Integer = 0 To intgridlineCount

                    'すべての列
                    For intgCopyListCol = 0 To 115 '114
                        dgv(intgCopyListCol, intRowNo + y).Value = gCopyChList(intRowNo, intgCopyListCol)
                    Next

                    'すべての列(ダミー)
                    For intgCopyListCol = 0 To 115 '87
                        If gCopyDummyChList(intRowNo, intgCopyListCol) = True Then
                            dgv(intgCopyListCol, intRowNo + y).Style.BackColor = gCstDummySetColorDummy
                        Else
                            'Ver2.0.2.4 非dummyの場合色を戻す処置
                            dgv(intgCopyListCol, intRowNo + y).Style.BackColor = gDummyGetChangeBackColorGrid2(dgv.Rows(intRowNo + y).Cells(intgCopyListCol).Style.BackColor, intRowNo + y, intgCopyListCol, dgv)
                        End If
                    Next

                Next

                '単体選択後の張付け
            ElseIf blnAllRowFlg = False Then

                Dim r As Integer
                Dim nflag As Boolean = True

                For r = 0 To lines.GetLength(0) - 1

                    ' 最後のNULL行をコピーするかどうか
                    If r >= lines.GetLength(0) - 1 And "".Equals(lines(r)) And nflag = False Then
                        Exit For
                    End If

                    If "".Equals(lines(r)) = False Then
                        nflag = False
                    End If

                    ' タブで分割
                    Dim vals() As String = lines(r).Split(vbTab)

                    ' 各セルの値を設定
                    Dim c As Integer = 0
                    Dim c2 As Integer = 0
                    Dim iniValue As Integer = 0

                    ''CH Listで行コピーのみ先頭に行ヘッダーが付く　----------------------------------------------

                    If dgv.Name = "grdCHList" And ((vals.GetLength(0) = gCstChListPowerFactorRow + 2)) Then
                        If dgv(gCstChListColPosChType, y + r).Value = Nothing Or Val(dgv(gCstChListColPosChType, y + r).Value) = 0 Then

                            ''CHの総数が3000件を超えていないか？
                            If r + intChCnt + gMaxChannel >= gCstChannelIdMax Then
                                MsgBox("The Channel exceeds " & gCstChannelIdMax.ToString & ".", MsgBoxStyle.Exclamation, "Channel List")
                                Exit Sub
                            End If

                        End If

                        iniValue = 1    ''配列の開始を1にする
                    End If


                    For c = iniValue To vals.GetLength(0) - 1

                        ' セルが存在しなければ貼り付けない
                        If Not (x + c2 >= 0 And x + c2 < dgv.ColumnCount And y + r >= 0 And y + r < dgv.RowCount) Then
                            Continue For
                        End If

                        ' 非表示セルには貼り付けない
                        If dgv(x + c2, y + r).Visible = False Then
                            c = c ' - 1
                            Continue For
                        End If

                        '' 貼り付け処理(入力可能文字チェック無しの時)------------
                        '' 行追加モード&(最終行の時は行追加)
                        'If y + r = dgv.RowCount - 1 And _
                        '   dgv.AllowUserToAddRows = True Then
                        '    dgv.RowCount = dgv.RowCount + 1
                        'End If
                        '' 貼り付け
                        'dgv(x + c2, y + r).Value = vals(c)
                        ' ------------------------------------------------------
                        ' 貼り付け処理(入力可能文字チェック有りの時)------------
                        Dim pststr As String = ""

                        For i As Long = 0 To vals(c).Length - 1

                            _editingColumn = x + c2

                            Dim tmpe As KeyPressEventArgs = New KeyPressEventArgs(vals(c).Substring(i, 1))

                            tmpe.Handled = False
                            DataGridViewPlus_CellKeyPress(sender, tmpe)

                            If tmpe.Handled = False Then
                                pststr = pststr & vals(c).Substring(i, 1)
                            End If

                        Next

                        ' 行追加モード＆最終行の時は行追加
                        If y + r = dgv.RowCount - 1 And _
                           dgv.AllowUserToAddRows = True Then
                            dgv.RowCount = dgv.RowCount + 1
                        End If

                        ' 行列位置取得
                        intCol = x + c2
                        intRow = y + r

                        ''貼り付け先のセルの背景色が入力不可ではない場合
                        If dgv.Rows(intRow).Cells(intCol).Style.BackColor <> gColorGridRowBackReadOnly Then

                            ''貼り付け先のセルタイプによって分岐
                            Select Case dgv(intCol, intRow).OwningColumn.CellType.Name

                                Case "DataGridViewComboBoxCell"

                                    '=====================
                                    ''コンボボックス
                                    '=====================
                                    Dim grdCombo As New System.Windows.Forms.DataGridViewComboBoxCell
                                    Dim objDataRowView As System.Data.DataRowView
                                    Dim intSetValue As Integer
                                    Dim blnSet As Boolean = False

                                    ''ComboBoxCell取得
                                    grdCombo = CType(dgv(intCol, intRow), DataGridViewComboBoxCell)

                                    ''貼り付ける文字がコンボのリストに存在するか確認する
                                    blnSet = False
                                    For i As Integer = 0 To grdCombo.Items.Count - 1

                                        ''DataRowView取得
                                        objDataRowView = grdCombo.Items(i)

                                        ''貼り付ける文字とコンボの文字が同じ場合
                                        If objDataRowView.Row.ItemArray(1) = pststr Then

                                            ''設定値を保存
                                            intSetValue = objDataRowView.Row.ItemArray(0)
                                            blnSet = True

                                        End If

                                    Next

                                    ''設定値が保存されている場合
                                    If blnSet Then

                                        ''コンボに値を設定
                                        dgv(intCol, intRow).Value = CStr(intSetValue)

                                    End If

                                Case "DataGridViewCheckBoxCell"

                                    '=====================
                                    ''チェックボックス
                                    '=====================
                                    ''貼り付ける文字がTrue、Falseで値を設定する
                                    Select Case pststr
                                        Case "False" : dgv(intCol, intRow).Value = False
                                        Case "True" : dgv(intCol, intRow).Value = True
                                    End Select

                                Case "DataGridViewTextBoxCell"

                                    '=====================
                                    ''テキストボックス
                                    '=====================
                                    ''そのまま貼り付ける
                                    dgv(intCol, intRow).Value = pststr

                                Case "DataGridViewButtonCell"

                                    '=====================
                                    ''ボタン
                                    '=====================
                                    ''何もしない

                            End Select

                            ''入力を確定させる
                            dgv.EndEdit()

                        End If

                        ' 次のセルへ
                        c2 = c2 + 1

                    Next

                Next
            End If
        End If
    End Sub
    ' ----------------------------------------------------------------------------------
    ' DataGridViewでCtrl-Vキー押下時にクリップボードから貼り付けるための実装↑↑↑↑↑↑
    ' DataGridViewでDelやBackspaceキー押下時にセルの内容を消去するための実装↑↑↑↑↑↑
    ' ----------------------------------------------------------------------------------


    '--------------------------------------------------------------------
    ' 機能      : 仮設定セルの色変えをする
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) CHリストグリッド
    '           : ARG2 - (I ) コピー元 行位置
    '           : ARG3 - (I ) コピー先 行位置
    ' 機能説明  : コピー元のセル背景色をコピー先セルへ移植する
    '--------------------------------------------------------------------
    Private Sub mSetChListBackColor(ByVal dgv As DataGridView, ByVal intRowFrom As Integer, ByVal intRowTo As Integer)

        Try

            For i As Integer = 0 To dgv.ColumnCount - 1

                dgv(i, intRowTo).Style.BackColor = mSetCellBackColor(dgv, i, intRowFrom, intRowTo)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : セル背景色の獲得
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) CHリストグリッド
    '           : ARG2 - (I ) 対象カラム位置
    '           : ARG3 - (I ) コピー元 行位置
    '           : ARG3 - (I ) コピー先 行位置
    ' 機能説明  : コピー元のセル背景色からコピー先セル背景色を獲得する
    '--------------------------------------------------------------------
    Private Function mSetCellBackColor(ByVal dgv As DataGridView, ByVal intCol As Integer, _
                                  ByVal intRowFrom As Integer, ByVal intRowTo As Integer) As Color

        Try

            If dgv(intCol, intRowFrom).Style.BackColor = gCstDummySetColorDummy Then
                Return gCstDummySetColorDummy

            ElseIf dgv(intCol, intRowFrom).Style.BackColor = gColorGridRowBackReadOnly Then
                Return gColorGridRowBackReadOnly
            Else
                If intRowTo Mod 2 <> 0 Then

                    If intRowTo >= 0 And intRowTo <= 19 Then
                        Return gColorGridRowBack
                    ElseIf intRowTo >= 20 And intRowTo <= 39 Then
                        Return Color.LavenderBlush
                    ElseIf intRowTo >= 40 And intRowTo <= 59 Then
                        Return Color.Lavender
                    ElseIf intRowTo >= 60 And intRowTo <= 79 Then
                        Return Color.Beige
                    ElseIf intRowTo >= 80 And intRowTo <= 99 Then
                        Return Color.Honeydew
                    End If

                Else
                    Return gColorGridRowBackBase
                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


    Private Sub clsDataGridViewPlus_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleCreated

        'Dim dgv As DataGridView = CType(sender, DataGridView)
        'dgv.Font = New Font("ＭＳ ゴシック", 9)

    End Sub


    '--------------------------------------------------------------------
    ' 機能      : セル背景色の設定
    ' 返り値    : なし
    ' 引き数    : dgv          CHリストグリッド
    '           : intCopyRow　 ｺﾋﾟｰ元行
    '           : intRow　　　 ｺﾋﾟｰ先行
    ' 機能説明  : Ver1.9.0  2015.12.16　追加
    '--------------------------------------------------------------------
    Private Sub SetDummyColor(ByVal dgv As DataGridView, ByVal intCopyRow As Integer, ByVal intCopyCol As Integer, _
                                ByVal intRow As Integer, ByVal intCol As Integer)

        If gCopyChList(intCopyRow, intCopyCol) = 1 Then
            dgv(intCol, intRow).Style.BackColor = gCstDummySetColorDummy
        End If


    End Sub
End Class
