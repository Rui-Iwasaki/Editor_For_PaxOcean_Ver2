Public Class frmChTerminalList

#Region "変数定義"

    Private mudtSetFuNew As gTypSetFu
    Private mudtSetChDispNew As gTypSetChDisp

    ''FCU/FU Type iniファイルから設定可能タブ数を獲得する
    Private mintTBCount() As Integer = Nothing

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow()

        Try

            ''本画面表示
            Call gShowFormModelessForCloseWait1(Me)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChTerminalList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''グリッドを初期化する
            Call mInitialDataGrid_1()

            ''配列再定義
            mudtSetFuNew.InitArray()
            For i As Integer = LBound(mudtSetFuNew.udtFu) To UBound(mudtSetFuNew.udtFu)
                mudtSetFuNew.udtFu(i).InitArray()
            Next

            mudtSetChDispNew.InitArray()
            For i As Integer = LBound(mudtSetChDispNew.udtChDisp) To UBound(mudtSetChDispNew.udtChDisp)
                mudtSetChDispNew.udtChDisp(i).InitArray()
                For j As Integer = LBound(mudtSetChDispNew.udtChDisp(i).udtSlotInfo) To UBound(mudtSetChDispNew.udtChDisp(i).udtSlotInfo)
                    mudtSetChDispNew.udtChDisp(i).udtSlotInfo(j).InitArray()
                Next
            Next

            ''画面設定
            Call mSetDisplay(gudt.SetFu, gudt.SetChDisp)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChTerminalList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    


    '----------------------------------------------------------------------------
    ' 機能説明  ： Exitボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Saveボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetFuNew, mudtSetChDispNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetFu, mudtSetFuNew, gudt.SetChDisp, mudtSetChDispNew) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure1(mudtSetFuNew, gudt.SetFu)
                Call mCopyStructure2(mudtSetChDispNew, gudt.SetChDisp)
                'Ver2.0.5.2 OUT_FUｱﾄﾞﾚｽの更新
                Call subSetFUadrOUT()

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytFuChannel = 1
                gudt.SetEditorUpdateInfo.udtSave.bytChDisp = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytFuChannel = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytChDisp = 1
            Else
                'Ver2.0.5.9 OUT FUアドレスの更新は強制
                Call subSetFUadrOUT()
                gblnUpdateAll = True
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '--------------------------------------------------------------------
    Private Sub frmChTerminalList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetFuNew, mudtSetChDispNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetFu, mudtSetFuNew, gudt.SetChDisp, mudtSetChDispNew) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更されている場合は設定を更新する
                        Call mCopyStructure1(mudtSetFuNew, gudt.SetFu)
                        Call mCopyStructure2(mudtSetChDispNew, gudt.SetChDisp)
                        'Ver2.0.5.2 OUT_FUｱﾄﾞﾚｽの更新
                        Call subSetFUadrOUT()

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytFuChannel = 1
                        gudt.SetEditorUpdateInfo.udtSave.bytChDisp = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytFuChannel = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytChDisp = 1

                    Case Windows.Forms.DialogResult.No

                        ''何もしない

                    Case Windows.Forms.DialogResult.Cancel

                        ''画面を閉じない
                        e.Cancel = True

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： セルの値が変更されて、変更が保存されていない場合に発生するイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： セルの値を保存する
    ' 備考      ： グリッド内 Use チェックボックスの CellValueChanged イベントを発生させるための処理
    '----------------------------------------------------------------------------
    Private Sub grdTerminal_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTerminal1.CurrentCellDirtyStateChanged

        If sender.CurrentCellAddress.X = 0 AndAlso sender.IsCurrentRowDirty Then
            sender.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド　クリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： Setボタン クリック時に端子設定画面を表示する
    '              Useチェック時に対象行を設定可にする
    '----------------------------------------------------------------------------
    Private Sub grdTerminal_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
                                                         Handles grdTerminal1.CellValueChanged, grdTerminal1.CellContentClick

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim strFCU As String, strFuNo As String, strFcuFuName As String
            Dim strType As String, strPortNo As String
            Dim intFuNo As Integer, strFuName As String, intType As Integer
            Dim intMode As Integer, iFlag As Integer = 0
            Dim intRow As Integer, intCol As Integer
            Dim intFuNoFirst As Integer = 0, intPortNoFirst As Integer = 0
            Dim intFuNoEnd As Integer = 0, intPortNoEnd As Integer = 0
            Dim intCnt As Integer = 0
            Dim shtTerInfo As Short

            If e.ColumnIndex < 0 Or e.RowIndex < 0 Then Exit Sub

            intRow = e.RowIndex : intCol = e.ColumnIndex

            If dgv.Columns(intCol).Name.Substring(0, 3) = "cmd" Then '----------------

                If dgv(intCol - 1, intRow).FormattedValue = "" Then Exit Sub

                '設定可能な端子台の最初と最後をサーチする
                For i As Integer = 0 To dgv.RowCount - 1

                    For j As Integer = 6 To dgv.ColumnCount - 2 Step 2

                        If dgv(j, i).FormattedValue <> "" Then

                            If intCnt = 0 Then
                                intFuNoFirst = i
                                intPortNoFirst = Val(dgv.Columns(j + 1).Name.Substring(6, 1))

                                intFuNoEnd = i
                                intPortNoEnd = Val(dgv.Columns(j + 1).Name.Substring(6, 1))
                            Else
                                intFuNoEnd = i
                                intPortNoEnd = Val(dgv.Columns(j + 1).Name.Substring(6, 1))
                            End If
                            intCnt += 1

                        End If

                    Next

                Next

                Do Until iFlag = 9

                    'T.Ueki ローカル数値化のため変更
                    If dgv(intCol - 1, intRow).FormattedValue <> "" Then

                        strFCU = "FCU_1"
                        strFuNo = dgv.Rows(intRow).HeaderCell.Value

                        If strFuNo.Length = 3 Then
                            intFuNo = 0     ''FCU
                            strFuName = "0"
                        Else
                            If strFuNo.Substring(2, 1) = " " Then
                                Select Case strFuNo.Substring(3, 1)
                                    Case "1" : intFuNo = 1
                                    Case "2" : intFuNo = 2
                                    Case "3" : intFuNo = 3
                                    Case "4" : intFuNo = 4
                                    Case "5" : intFuNo = 5
                                    Case "6" : intFuNo = 6
                                    Case "7" : intFuNo = 7
                                    Case "8" : intFuNo = 8
                                    Case "9" : intFuNo = 9
                                    Case Else
                                End Select
                                strFuName = strFuNo.Substring(3, 1)
                            Else
                                Select Case strFuNo.Substring(2, 2)
                                    Case "10" : intFuNo = 10
                                    Case "11" : intFuNo = 11
                                    Case "12" : intFuNo = 12
                                    Case "13" : intFuNo = 13
                                    Case "14" : intFuNo = 14
                                    Case "15" : intFuNo = 15
                                    Case "16" : intFuNo = 16
                                    Case "17" : intFuNo = 17
                                    Case "18" : intFuNo = 18
                                    Case "19" : intFuNo = 19
                                    Case "20" : intFuNo = 20
                                    Case Else
                                End Select
                                strFuName = strFuNo.Substring(2, 2)
                            End If
                        End If

                        '' DO基板の端子台設定追加　ver1.4.0 2011.07.29
                        '' RY基板の端子台設定追加　2013.10.25
                        'Ver2.0.8.1 M200Aも派生
                        'If dgv(intCol - 1, intRow).Value > 100 Then               ' TMRY (DO:1)
                        '    intType = 1 '' DO固定とする
                        'Else                                                      ' その他
                        '    intType = dgv(intCol - 1, intRow).Value
                        'End If
                        If dgv(intCol - 1, intRow).Value > 100 Then               ' TMRY (DO:1)とM200A派生
                        	'Ver.2.0.8.P
                        	'基板種類で分ける
                            If dgv(intCol - 1, intRow).Value <> 341 Then
                                intType = Val(RightB(dgv(intCol - 1, intRow).Value.ToString, 1)) '' 末尾一けた目
                            Else
                                intType = 341
                            End If
                        Else                                                      ' その他
                            intType = dgv(intCol - 1, intRow).Value
                        End If

                        strFcuFuName = dgv(1, intRow).Value
                        strType = dgv(intCol - 1, intRow).FormattedValue
                        strPortNo = dgv.Columns(intCol).Name.Substring(6, 1)
                        intMode = 0

						'Ver.2.0.8.P
                        shtTerInfo = gudt.SetFu.udtFu(intFuNo).udtSlotInfo(Val(strPortNo) - 1).shtTerinf

                            ''端子画面へ --------------------------------------------------------------------------------------
                        frmChTerminalDetail.gShow(strFCU, intFuNo, strFuName, strFcuFuName, intType, strType, strPortNo, _
                                                  intFuNoFirst, intPortNoFirst, intFuNoEnd, intPortNoEnd, intMode, shtTerInfo, Me)
                            ''-------------------------------------------------------------------------------------------------
                        End If

                    If intMode = 0 Then
                        ''LOOP End
                        iFlag = 9

                    ElseIf intMode = 1 Then
                        ''Next スロット
                        If intCol = dgv.ColumnCount - 1 Then
                            intRow += 1
                            intCol = 7
                        Else
                            intCol += 2
                        End If
                        dgv(intCol, intRow).Selected = True

                    ElseIf intMode = 2 Then
                        ''Before スロット
                        If intCol = 7 Then
                            intRow -= 1
                            intCol = dgv.ColumnCount - 1
                        Else
                            intCol -= 2
                        End If
                        dgv(intCol, intRow).Selected = True

                    End If

                Loop

            ElseIf dgv.Columns(e.ColumnIndex).Name = "chkUse" Then  '----------------

                dgv.EndEdit()

                ''使用可/不可　切替
                ''If dgv(0, e.RowIndex).Value = False Then
                'If dgv(0, e.RowIndex).EditedFormattedValue = True Then

                '    For i As Integer = 1 To 5

                '        dgv(i, e.RowIndex).ReadOnly = False
                '        If e.RowIndex Mod 2 <> 0 Then
                '            dgv(i, e.RowIndex).Style.BackColor = gColorGridRowBack
                '        Else
                '            dgv(i, e.RowIndex).Style.BackColor = gColorGridRowBackBase
                '        End If
                '    Next

                'Else

                '    dgv(1, e.RowIndex).Value = ""
                '    dgv(2, e.RowIndex).Value = ""
                '    dgv(3, e.RowIndex).Value = False
                '    dgv(4, e.RowIndex).Value = ""
                '    dgv(5, e.RowIndex).Value = ""

                '    ''TBn:スロット種別
                '    For i As Integer = 0 To 7
                '        dgv(6 + i * 2, e.RowIndex).Value = ""
                '    Next

                '    For i As Integer = 1 To dgv.ColumnCount - 1
                '        dgv(i, e.RowIndex).ReadOnly = True
                '        dgv(i, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
                '    Next

                'End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： FCU/FU Typeを変更した場合
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： TB1～8の設定可能数を制限する
    '---------------------------------------------------------------------------
    Private Sub grdTerminal_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
                Handles grdTerminal1.CellValueChanged

        Try

            If e.RowIndex < 0 Then Exit Sub

            If e.ColumnIndex = 0 Then
                ''chkUse

                '使用可/不可　切替
                Dim dgv As DataGridView = CType(sender, DataGridView)

                If dgv(0, e.RowIndex).Value = True Then
                    'If dgv(0, e.RowIndex).EditedFormattedValue = True Then

                    For i As Integer = 1 To 5

                        dgv(i, e.RowIndex).ReadOnly = False
                        If e.RowIndex Mod 2 <> 0 Then
                            dgv(i, e.RowIndex).Style.BackColor = gColorGridRowBack
                        Else
                            dgv(i, e.RowIndex).Style.BackColor = gColorGridRowBackBase
                        End If
                    Next

                    '' FCUの場合、型式固定　ver.1.4.0 2011.09.27
                    If e.RowIndex = 0 Then
                        dgv(2, e.RowIndex).ReadOnly = True
                        dgv(2, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
                        dgv(2, e.RowIndex).Value = "13"     ''2014.10.09
                    Else
                        'Ver2.0.2.6 FCU以外のName Plateは「G-15-1」が自動で入る
                        dgv(4, e.RowIndex).Value = "G-15-1"
                    End If

                Else

                    dgv(1, e.RowIndex).Value = ""
                    dgv(2, e.RowIndex).Value = ""
                    dgv(3, e.RowIndex).Value = False
                    dgv(4, e.RowIndex).Value = ""
                    dgv(5, e.RowIndex).Value = ""

                    ''TBn:スロット種別
                    For i As Integer = 0 To 7
                        dgv(6 + i * 2, e.RowIndex).Value = ""
                    Next

                    For i As Integer = 1 To dgv.ColumnCount - 1
                        dgv(i, e.RowIndex).ReadOnly = True
                        dgv(i, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
                    Next

                End If

            ElseIf e.ColumnIndex = 2 Then
                ''FCU/FU Type
                Dim dgv As DataGridView = CType(sender, DataGridView)
                Dim intSetCnt As Integer = mintTBCount(CCInt(dgv(e.ColumnIndex, e.RowIndex).Value))
                Dim intCol As Integer

                For i As Integer = 0 To 7

                    intCol = 6 + i * 2

                    If i + 1 <= intSetCnt Then
                        ''設定可
                        dgv(intCol, e.RowIndex).ReadOnly = False

                        If e.RowIndex Mod 2 <> 0 Then
                            dgv(intCol, e.RowIndex).Style.BackColor = gColorGridRowBack
                        Else
                            dgv(intCol, e.RowIndex).Style.BackColor = gColorGridRowBackBase
                        End If
                    Else
                        ''設定不可
                        dgv(intCol, e.RowIndex).Value = ""
                        dgv(intCol, e.RowIndex).ReadOnly = True
                        dgv(intCol, e.RowIndex).Style.BackColor = gColorGridRowBackReadOnly
                    End If

                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTerminal1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdTerminal1.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim strColumnName As String

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdTerminal1_KeyPress

                ''該当する列ならイベントハンドラを追加する
                strColumnName = dgv.CurrentCell.OwningColumn.Name

                If strColumnName.Substring(0, 3) = "txt" Then

                    AddHandler tb.KeyPress, AddressOf grdTerminal1_KeyPress

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyDownイベントの前に発生
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Private Sub grdTerminal1_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles grdTerminal1.PreviewKeyDown

    '    'Ctrl + V  
    '    If (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.V Then

    '        If grdTerminal1.CurrentCell.OwningColumn.Name = "chkUse" Then

    '            Call grdTerminal_CellContentClick(grdTerminal1, New DataGridViewCellEventArgs(0, grdTerminal1.CurrentCell.RowIndex))

    '        End If

    '    End If

    '    'If Asc(e.KeyChar) = 22 Then 'Ctl + V
    '    '    If grdTerminal1.CurrentCell.OwningColumn.Name = "chkUse" Then
    '    '        Call grdTerminal_CellContentClick(grdTerminal1, New DataGridViewCellEventArgs(0, grdTerminal1.CurrentCell.RowIndex))
    '    '    End If
    '    'End If

    'End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTerminal1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdTerminal1.KeyPress

        Try

            ''KeyDownイベントを再度Call(USEを変更後、ReadOnlyを解除して再度ペースト)
            If Asc(e.KeyChar) = 22 Then 'Ctl + V

                If grdTerminal1.CurrentCell.OwningColumn.Name = "chkUse" Then

                    Dim cls As New clsDataGridViewPlus

                    ' クリップボードの内容から複数行のCOPYをした場合の行数をGET
                    Dim clipText As String = Clipboard.GetText()

                    ' 改行を変換
                    clipText = clipText.Replace(vbCrLf, vbLf)
                    clipText = clipText.Replace(vbCr, vbLf)

                    ' 改行で分割
                    Dim lines() As String = clipText.Split(vbLf)

                    For i As Integer = 0 To UBound(lines)

                        Call grdTerminal_CellContentClick(grdTerminal1, New DataGridViewCellEventArgs(0, grdTerminal1.CurrentCell.RowIndex + i))

                    Next

                    Call cls.DataGridViewPlus_KeyDown(grdTerminal1, New KeyEventArgs(Keys.Control + Keys.V))
                    cls.Dispose()

                End If

            End If

            If Asc(e.KeyChar) >= 0 And Asc(e.KeyChar) <= 31 Then Exit Sub

            If grdTerminal1.CurrentCell.ReadOnly Then Exit Sub

            Dim strColumnName As String = grdTerminal1.CurrentCell.OwningColumn.Name

            Select Case strColumnName

                Case "txtName", "txtNamePlate", "txtRemarks"

                    Dim dgv As DataGridViewTextBoxEditingControl = CType(sender, DataGridViewTextBoxEditingControl)
                    e.Handled = gCheckTextInput(16, dgv, e.KeyChar, False)

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドエラー
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdTerminal1_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdTerminal1.DataError

        Try

            ''エラーが発生した時に、元の値に戻るようにする
            e.Cancel = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdTerminal2_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)

        Try

            ''エラーが発生した時に、元の値に戻るようにする
            e.Cancel = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            Dim i As Integer

            For i = 0 To grdTerminal1.Rows.Count - 1
                If Not gChkInputText(grdTerminal1(1, i), "FCU/FU Name", i + 1, True, True) Then Return False
                If Not gChkInputText(grdTerminal1(4, i), "Name Plate", i + 1, True, True) Then Return False
                If Not gChkInputText(grdTerminal1(5, i), "Remarks", i + 1, True, True) Then Return False

                ''FCU以外はFCUの型式選択不可　ver.1.4.0 2011.09.27
                If i <> 0 Then
                    If grdTerminal1(2, i).Value = "13" Then ' 2014.10.09
                        Call MessageBox.Show(grdTerminal1(2, i).FormattedValue & " cannot set it to FCU/FU TYPE." & vbNewLine & vbNewLine & _
                                         "[ Col ] " & "FCU/FU TYPE" & " " & vbNewLine & "[ Row ] " & i + 1, _
                                         "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If
                End If
            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) FU設定構造体
    '           : ARG2 - (I ) チャンネル情報データ構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByVal udtSetFu As gTypSetFu, _
                              ByVal udtSetDisp As gTypSetChDisp)

        Try
            Dim DoSlotType(20, 8) As Byte 'Ver.2.0.8.P

            grdTerminal1.EndEdit()

            For i As Integer = 0 To grdTerminal1.Rows.Count - 1

                With grdTerminal1.Rows(i)

                    For j As Integer = 1 To grdTerminal1.ColumnCount - 1
                        .Cells(j).ReadOnly = False
                    Next

                    ''ＦＵ 使用／未使用フラグ
                    udtSetFu.udtFu(i).shtUse = IIf(.Cells(0).Value = True, 1, 0)

                    ''CanBus
                    udtSetFu.udtFu(i).shtCanBus = If(.Cells(3).Value = True, 1, 0)

                    ''TBn:スロット種別
                    For j As Integer = 0 To 7
                        '' DO基板の端子台設定追加　ver1.4.0 2011.07.29
                        '' RY基板の端子台設定追加　2013.10.25
                        'Ver2.0.8.1 M200Aにも派生基板追加
                        'If Val(.Cells(6 + j * 2).Value) > 100 Then                                          ' TMRY (DO:1)
                        '    udtSetFu.udtFu(i).udtSlotInfo(j).shtType = 1 ' DO固定とする
                        'Else                                                                                ' その他
                        '    udtSetFu.udtFu(i).udtSlotInfo(j).shtType = Val(.Cells(6 + j * 2).Value)
                        'End If
                        If Val(.Cells(6 + j * 2).Value) > 100 Then
                            '派生基板の場合、下一桁が該当基板であるため
                            '下一桁を格納
                            udtSetFu.udtFu(i).udtSlotInfo(j).shtType = Val(RightB(.Cells(6 + j * 2).Value.ToString, 1))
                        Else
                            udtSetFu.udtFu(i).udtSlotInfo(j).shtType = Val(.Cells(6 + j * 2).Value)
                        End If


                        '' DO基板
                        If udtSetFu.udtFu(i).udtSlotInfo(j).shtType = gCstCodeFuSlotTypeDO Then     'スロット種別DO
                            'Ver2.0.1.3 LIKE演算子に変換
                            If .Cells(6 + j * 2).FormattedValue Like "*(TMDO)" Then       ' TMDO
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 1
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRYa)" Then  ' TMRY
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 2
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRYb)" Then  ' TMRY
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 3
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRYc)" Then  ' TMRY
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 4
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRYd)" Then  ' TMRY
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 5
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRY-1a)" Then ' TMRY-1a
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 6
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRY-1b)" Then ' TMRY-1b
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 7
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRY-1c)" Then ' TMRY-1c
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 8
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRY-1d)" Then ' TMRY-1d
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 9
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRY-2a)" Then ' TMRY-2a
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 10
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRY-2b)" Then ' TMRY-2b
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 11
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRY-2c)" Then ' TMRY-2c
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 12
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(TMRY-2d)" Then ' TMRY-2d
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 13
                            ElseIf .Cells(6 + j * 2).FormattedValue Like "*(Select)" Then ' Ver.2.0.8.P 端子台アレンジ
                                DoSlotType(i, j) = 1
                                If gudt.SetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 0 Then
                                    'M03A(Select)を選択するも、端子台の詳細設定が入っていない場合、
                                    'TMDO2枚設定を仮で入れる
                                    udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 2020
                                End If
                            Else                                                        ' その他
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 0
                            End If
                        Else
                            'Ver2.0.8.1 M200Aにも派生基板あり
                            If udtSetFu.udtFu(i).udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAI_K Then
                                If .Cells(6 + j * 2).FormattedValue Like "*(TMK-1)" Then
                                    udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 1  'TMK-1
                                Else
                                    udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 0
                                End If
                            Else
                                udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 0
                            End If
                        End If
                    Next

                    ''FCU/FU名称
                    udtSetDisp.udtChDisp(i).strFuName = .Cells(1).Value

                    ''FCU/FU種類
                    udtSetDisp.udtChDisp(i).strFuType = .Cells(2).FormattedValue

                    ''FCU/FU盤名
                    udtSetDisp.udtChDisp(i).strNamePlate = .Cells(4).Value

                    ''コメント
                    udtSetDisp.udtChDisp(i).strRemarks = .Cells(5).Value

                End With

            Next
			
            'Ver.2.0.8.P
            Dim shtTerinf As Short
            Dim ushtTerinf As UShort
            For i = 0 To 20
                For j = 0 To 8 - 1
                    If DoSlotType(i, j) = 1 Then
                        shtTerinf = gudt.SetFu.udtFu(i).udtSlotInfo(j).shtTerinf
                        ushtTerinf = Convert.ToUInt16(shtTerinf.ToString("X4"), 16)
                        If ushtTerinf > 20 Then
                            udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = gudt.SetFu.udtFu(i).udtSlotInfo(j).shtTerinf
                        End If
                    End If
                Next
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) FU設定構造体
    '           : ARG2 - (I ) チャンネル情報データ構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSetFu As gTypSetFu, _
                            ByVal udtSetDisp As gTypSetChDisp)

        Try

            For i As Integer = 0 To grdTerminal1.Rows.Count - 1

                With grdTerminal1.Rows(i)

                    ''ＦＵ 使用／未使用フラグ
                    .Cells(0).Value = IIf(udtSetFu.udtFu(i).shtUse = 1, True, False)

                    ''使用可の場合はセルロックをはずす
                    If .Cells(0).Value = True Then

                        For j As Integer = 1 To grdTerminal1.ColumnCount - 1
                            grdTerminal1(j, i).ReadOnly = False
                            If i Mod 2 <> 0 Then
                                grdTerminal1(j, i).Style.BackColor = gColorGridRowBack
                            Else
                                grdTerminal1(j, i).Style.BackColor = gColorGridRowBackBase
                            End If
                        Next

                        '' FCUの場合、型式固定　ver.1.4.0 2011.09.27
                        If i = 0 Then
                            grdTerminal1(2, 0).ReadOnly = True
                            grdTerminal1(2, 0).Style.BackColor = gColorGridRowBackReadOnly

                            ''FU使用/未使用フラグ(use)を設定した時点でCellValueChangedイベントが発生しFCU型式セット
                            ''FCU型式が設定されるとIO設定可否がセットされるが本関数でクリアされるため再セットする
                            ''IO設定可否
                            For j As Integer = 0 To 7
                                If j + 1 <= mintTBCount(13) Then
                                    '2015/5/26 T.Ueki ｽﾛｯﾄ設定ミス
                                    'If j + 1 <= mintTBCount(9) Then
                                    ''設定可
                                    grdTerminal1((6 + j * 2), 0).ReadOnly = False
                                    grdTerminal1((6 + j * 2), 0).Style.BackColor = gColorGridRowBackBase
                                Else
                                    ''設定不可
                                    grdTerminal1((6 + j * 2), 0).ReadOnly = True
                                    grdTerminal1((6 + j * 2), 0).Style.BackColor = gColorGridRowBackReadOnly
                                End If
                            Next
                        End If

                    End If

                    ''CanBus
                    .Cells(3).Value = IIf(udtSetFu.udtFu(i).shtCanBus = 1, True, False)

                    ''TBn:スロット種別
                    For j As Integer = 0 To 7
                        '' DO基板の端子台設定追加　ver1.4.0 2011.07.29
                        '' RY基板の端子台設定追加　2013.10.25
                        If udtSetFu.udtFu(i).udtSlotInfo(j).shtType = gCstCodeFuSlotTypeDO Then                     'スロット種別DO
                            If udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 1 Then                                  'TMDO
                                .Cells(6 + j * 2).Value = udtSetFu.udtFu(i).udtSlotInfo(j).shtType.ToString
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 2 Then                              'TMRY
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 100).ToString 'TMRYa:101 (DO:1)
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 3 Then
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 110).ToString 'TMRYb:111 (DO:1)
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 4 Then
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 120).ToString 'TMRYc:121 (DO:1)
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 5 Then
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 130).ToString 'TMRYd:131 (DO:1)
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 6 Then
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 200).ToString 'TMRY-1a:101 (DO:1)
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 7 Then
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 210).ToString 'TMRY-1b:111 (DO:1)
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 8 Then
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 220).ToString 'TMRY-1c:121 (DO:1)
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 9 Then
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 230).ToString 'TMRY-1d:131 (DO:1)
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 10 Then
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 300).ToString 'TMRY-2a:101 (RY:1)
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 11 Then
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 310).ToString 'TMRY-2b:111 (DO:1)
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 12 Then
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 320).ToString 'TMRY-2c:121 (DO:1)
                            ElseIf udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 13 Then
                                .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 330).ToString 'TMRY-2d:131 (DO:1)
                            ElseIf Not (udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf >= 0 And udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf < 20) Then 'Ver.2.0.8.P 端子台アレンジ
                                .Cells(6 + j * 2).Value = "341" 'DO Select
                            Else                                                                                    'その他
                                .Cells(6 + j * 2).Value = udtSetFu.udtFu(i).udtSlotInfo(j).shtType.ToString
                            End If
                        Else                                                                                        'その他
                            'Ver2.0.8.1 M200Aにも派生基板
                            If udtSetFu.udtFu(i).udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAI_K Then
                                If udtSetFu.udtFu(i).udtSlotInfo(j).shtTerinf = 1 Then
                                    .Cells(6 + j * 2).Value = (udtSetFu.udtFu(i).udtSlotInfo(j).shtType + 100).ToString
                                Else
                                    .Cells(6 + j * 2).Value = udtSetFu.udtFu(i).udtSlotInfo(j).shtType.ToString
                                End If
                            Else
                                .Cells(6 + j * 2).Value = udtSetFu.udtFu(i).udtSlotInfo(j).shtType.ToString
                            End If
                        End If
                    Next

                    ''FCU/FU名称
                    If udtSetDisp.udtChDisp(i).strFuName = Nothing Then
                        .Cells(1).Value = ""
                    Else
                        .Cells(1).Value = gGetString(udtSetDisp.udtChDisp(i).strFuName)
                    End If

                    ''FCU/FU種類
                    If udtSetDisp.udtChDisp(i).strFuType = Nothing Then
                        .Cells(2).Value = "0"
                    Else
                        cmbType.Text = gGetString(udtSetDisp.udtChDisp(i).strFuType)
                        .Cells(2).Value = cmbType.SelectedValue
                    End If

                    ''FCU/FU盤名
                    If udtSetDisp.udtChDisp(i).strNamePlate = Nothing Then
                        .Cells(4).Value = ""
                    Else
                        .Cells(4).Value = gGetString(udtSetDisp.udtChDisp(i).strNamePlate)
                    End If

                    ''コメント
                    If udtSetDisp.udtChDisp(i).strRemarks = Nothing Then
                        .Cells(5).Value = ""
                    Else
                        .Cells(5).Value = gGetString(udtSetDisp.udtChDisp(i).strRemarks)
                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元
    ' 　　　    : ARG1 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure1(ByVal udtSource As gTypSetFu, _
                               ByRef udtTarget As gTypSetFu)

        Try

            For i As Integer = LBound(udtSource.udtFu) To UBound(udtSource.udtFu)

                udtTarget.udtFu(i).shtUse = udtSource.udtFu(i).shtUse       ''ＦＵ 使用／未使用フラグ
                udtTarget.udtFu(i).shtCanBus = udtSource.udtFu(i).shtCanBus ''CanBus

                For j As Integer = LBound(udtSource.udtFu(i).udtSlotInfo) To UBound(udtSource.udtFu(i).udtSlotInfo)

                    udtTarget.udtFu(i).udtSlotInfo(j).shtType = udtSource.udtFu(i).udtSlotInfo(j).shtType   ''スロット種別

                    '' 端子台設定　ver1.4.0 2011.07.29
                    udtTarget.udtFu(i).udtSlotInfo(j).shtTerinf = udtSource.udtFu(i).udtSlotInfo(j).shtTerinf   '' 端子台設定

                Next
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mCopyStructure2(ByVal udtSource As gTypSetChDisp, _
                               ByRef udtTarget As gTypSetChDisp)

        Try

            For i As Integer = LBound(udtSource.udtChDisp) To UBound(udtSource.udtChDisp)

                udtTarget.udtChDisp(i).strFuName = udtSource.udtChDisp(i).strFuName
                udtTarget.udtChDisp(i).strFuType = udtSource.udtChDisp(i).strFuType
                udtTarget.udtChDisp(i).strNamePlate = udtSource.udtChDisp(i).strNamePlate
                udtTarget.udtChDisp(i).strRemarks = udtSource.udtChDisp(i).strRemarks

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG2 - (I ) 構造体２
    ' 　　　    : ARG3 - (I ) 構造体３
    ' 　　　    : ARG4 - (I ) 構造体４
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetFu, ByVal udt2 As gTypSetFu, _
                                          ByVal udt3 As gTypSetChDisp, ByVal udt4 As gTypSetChDisp) As Boolean

        Try

            For i As Integer = LBound(udt1.udtFu) To UBound(udt1.udtFu)

                If udt1.udtFu(i).shtUse <> udt2.udtFu(i).shtUse Then Return False

                If udt1.udtFu(i).shtCanBus <> udt2.udtFu(i).shtCanBus Then Return False

                For j As Integer = LBound(udt1.udtFu(i).udtSlotInfo) To UBound(udt1.udtFu(i).udtSlotInfo)

                    If udt1.udtFu(i).udtSlotInfo(j).shtType <> udt2.udtFu(i).udtSlotInfo(j).shtType Then Return False

                    '' 端子台設定　ver1.4.0 2011.07.29
                    If udt1.udtFu(i).udtSlotInfo(j).shtTerinf <> udt2.udtFu(i).udtSlotInfo(j).shtTerinf Then Return False

                Next

            Next

            For i As Integer = LBound(udt3.udtChDisp) To UBound(udt3.udtChDisp)

                If Not gCompareString(Trim(udt3.udtChDisp(i).strFuName), _
                                          Trim(udt4.udtChDisp(i).strFuName)) Then Return False

                If Not gCompareString(Trim(udt3.udtChDisp(i).strFuType), _
                                          Trim(udt4.udtChDisp(i).strFuType)) Then Return False

                If Not gCompareString(Trim(udt3.udtChDisp(i).strNamePlate), _
                                          Trim(udt4.udtChDisp(i).strNamePlate)) Then Return False

                If Not gCompareString(Trim(udt3.udtChDisp(i).strRemarks), _
                                          Trim(udt4.udtChDisp(i).strRemarks)) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する(FCU_1)
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid_1()

        Try
            Dim i As Integer, j As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewCheckBoxColumn : Column1.Name = "chkUse"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtName"
            Dim Column3 As New DataGridViewComboBoxColumn : Column3.Name = "cmb" : Column3.FlatStyle = FlatStyle.Popup
            Dim Column4 As New DataGridViewCheckBoxColumn : Column4.Name = "chkCanBus"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtNamePlate"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtRemarks"
            Dim Column7 As New DataGridViewComboBoxColumn : Column7.Name = "cmb" : Column7.FlatStyle = FlatStyle.Popup
            Dim Column8 As New DataGridViewButtonColumn : Column8.Name = "cmdSet1"
            Column8.UseColumnTextForButtonValue = True : Column8.Text = "Set"
            Dim Column9 As New DataGridViewComboBoxColumn : Column9.Name = "cmb" : Column9.FlatStyle = FlatStyle.Popup
            Dim Column10 As New DataGridViewButtonColumn : Column10.Name = "cmdSet2"
            Column10.UseColumnTextForButtonValue = True : Column10.Text = "Set"
            Dim Column11 As New DataGridViewComboBoxColumn : Column11.Name = "cmb" : Column11.FlatStyle = FlatStyle.Popup
            Dim Column12 As New DataGridViewButtonColumn : Column12.Name = "cmdSet3"
            Column12.UseColumnTextForButtonValue = True : Column12.Text = "Set"
            Dim Column13 As New DataGridViewComboBoxColumn : Column13.Name = "cmb" : Column13.FlatStyle = FlatStyle.Popup
            Dim Column14 As New DataGridViewButtonColumn : Column14.Name = "cmdSet4"
            Column14.UseColumnTextForButtonValue = True : Column14.Text = "Set"
            Dim Column15 As New DataGridViewComboBoxColumn : Column15.Name = "cmb" : Column15.FlatStyle = FlatStyle.Popup
            Dim Column16 As New DataGridViewButtonColumn : Column16.Name = "cmdSet5"
            Column16.UseColumnTextForButtonValue = True : Column16.Text = "Set"
            Dim Column17 As New DataGridViewComboBoxColumn : Column17.Name = "cmb" : Column17.FlatStyle = FlatStyle.Popup
            Dim Column18 As New DataGridViewButtonColumn : Column18.Name = "cmdSet6"
            Column18.UseColumnTextForButtonValue = True : Column18.Text = "Set"
            Dim Column19 As New DataGridViewComboBoxColumn : Column19.Name = "cmb" : Column19.FlatStyle = FlatStyle.Popup
            Dim Column20 As New DataGridViewButtonColumn : Column20.Name = "cmdSet7"
            Column20.UseColumnTextForButtonValue = True : Column20.Text = "Set"
            Dim Column21 As New DataGridViewComboBoxColumn : Column21.Name = "cmb" : Column21.FlatStyle = FlatStyle.Popup
            Dim Column22 As New DataGridViewButtonColumn : Column22.Name = "cmdSet8"
            Column22.UseColumnTextForButtonValue = True : Column22.Text = "Set"

            With grdTerminal1

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .Columns.Add(Column7) : .Columns.Add(Column8) : .Columns.Add(Column9)
                .Columns.Add(Column10) : .Columns.Add(Column11)
                .Columns.Add(Column12) : .Columns.Add(Column13) : .Columns.Add(Column14)
                .Columns.Add(Column15) : .Columns.Add(Column16) : .Columns.Add(Column17)
                .Columns.Add(Column18) : .Columns.Add(Column19) : .Columns.Add(Column20)
                .Columns.Add(Column21) : .Columns.Add(Column22)

                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                'Ver2.0.1.6 コンボセルの幅を増やす
                Dim intCboWith As Integer = 110 '90
                .Columns(0).HeaderText = "USE" : .Columns(0).Width = 38
                .Columns(1).HeaderText = "FCU/FU Name" : .Columns(1).Width = 110
                .Columns(2).HeaderText = "FCU/FU Type" : .Columns(2).Width = 120
                .Columns(3).HeaderText = "CanBus" : .Columns(3).Width = 45
                .Columns(4).HeaderText = "Name Plate" : .Columns(4).Width = 110
                .Columns(5).HeaderText = "Remarks" : .Columns(5).Width = 110
                .Columns(6).HeaderText = "SLOT1" : .Columns(6).Width = intCboWith
                .Columns(7).HeaderText = "" : .Columns(7).Width = 32
                .Columns(8).HeaderText = "SLOT2" : .Columns(8).Width = intCboWith
                .Columns(9).HeaderText = "" : .Columns(9).Width = 32
                .Columns(10).HeaderText = "SLOT3" : .Columns(10).Width = intCboWith
                .Columns(11).HeaderText = "" : .Columns(11).Width = 32
                .Columns(12).HeaderText = "SLOT4" : .Columns(12).Width = intCboWith
                .Columns(13).HeaderText = "" : .Columns(13).Width = 32
                .Columns(14).HeaderText = "SLOT5" : .Columns(14).Width = intCboWith
                .Columns(15).HeaderText = "" : .Columns(15).Width = 32
                .Columns(16).HeaderText = "SLOT6" : .Columns(16).Width = intCboWith
                .Columns(17).HeaderText = "" : .Columns(17).Width = 32
                .Columns(18).HeaderText = "SLOT7" : .Columns(18).Width = intCboWith
                .Columns(19).HeaderText = "" : .Columns(19).Width = 32
                .Columns(20).HeaderText = "SLOT8" : .Columns(20).Width = intCboWith
                .Columns(21).HeaderText = "" : .Columns(21).Width = 32
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 22
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                'T.Ueki
                ''行ヘッダー
                .Rows(0).HeaderCell.Value = "FCU"
                .Rows(1).HeaderCell.Value = "FU 1"
                .Rows(2).HeaderCell.Value = "FU 2"
                .Rows(3).HeaderCell.Value = "FU 3"
                .Rows(4).HeaderCell.Value = "FU 4"
                .Rows(5).HeaderCell.Value = "FU 5"
                .Rows(6).HeaderCell.Value = "FU 6"
                .Rows(7).HeaderCell.Value = "FU 7"
                .Rows(8).HeaderCell.Value = "FU 8"
                .Rows(9).HeaderCell.Value = "FU 9"
                .Rows(10).HeaderCell.Value = "FU10"
                .Rows(11).HeaderCell.Value = "FU11"
                .Rows(12).HeaderCell.Value = "FU12"
                .Rows(13).HeaderCell.Value = "FU13"
                .Rows(14).HeaderCell.Value = "FU14"
                .Rows(15).HeaderCell.Value = "FU15"
                .Rows(16).HeaderCell.Value = "FU16"
                .Rows(17).HeaderCell.Value = "FU17"
                .Rows(18).HeaderCell.Value = "FU18"
                .Rows(19).HeaderCell.Value = "FU19"
                .Rows(20).HeaderCell.Value = "FU20"
                .RowHeadersWidth = 60

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Horizontal

                .DefaultCellStyle.NullValue = ""

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdTerminal1)

                ''FCU/FU TYPEコンボ　初期値
                Call gSetComboBoxPlus(Column3, gEnmComboType.ctChTerminalListColumn3, mintTBCount)
                Call gSetComboBox(cmbType, gEnmComboType.ctChTerminalListColumn3)   ''非表示

                ''TBコンボ　初期値
                Call gSetComboBox(Column7, gEnmComboType.ctChTerminalListColumn)
                Call gSetComboBox(Column9, gEnmComboType.ctChTerminalListColumn)
                Call gSetComboBox(Column11, gEnmComboType.ctChTerminalListColumn)
                Call gSetComboBox(Column13, gEnmComboType.ctChTerminalListColumn)
                Call gSetComboBox(Column15, gEnmComboType.ctChTerminalListColumn)
                Call gSetComboBox(Column17, gEnmComboType.ctChTerminalListColumn)
                Call gSetComboBox(Column19, gEnmComboType.ctChTerminalListColumn)
                Call gSetComboBox(Column21, gEnmComboType.ctChTerminalListColumn)

                ''USE 以外はロック
                For i = 0 To .Rows.Count - 1
                    For j = 1 To .Columns.Count - 1
                        .Rows(i).Cells(j).ReadOnly = True
                        .Rows(i).Cells(j).Style.BackColor = gColorGridRowBackReadOnly
                    Next
                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    'Ver2.0.5.2
    '計測点リストのOUTｱﾄﾞﾚｽがOUT基板じゃない場合消す
    'OUT基板が削除された場合の処置だが保存時常に全なめする
    Private Sub subSetFUadrOUT()
        Try
            Dim aryFUout As ArrayList = New ArrayList
            Dim strFUadr As String = ""
            Dim i As Integer
            Dim j As Integer
            Dim x As Integer
            'Dim blOUTch As Boolean
            Dim blDel As Boolean = False

            Dim strTerFUadr() As String = Nothing
            Dim intFU As Integer = 0
            Dim intSlot As Integer = 0
            Dim intTerFU As Integer = 0
            Dim intTerSlot As Integer = 0


            '>>>OUT基板のFUｱﾄﾞﾚｽを格納(DO基板とAO基板)
            With gudt.SetFu
                For i = 0 To UBound(.udtFu) Step 1
                    For j = 0 To UBound(.udtFu(i).udtSlotInfo) Step 1
                        If .udtFu(i).udtSlotInfo(j).shtType = gCstCodeFuSlotTypeDO Or _
                            .udtFu(i).udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAO Then
                            strFUadr = i.ToString & "," & (j + 1).ToString
                            aryFUout.Add(strFUadr)
                        End If
                    Next j
                Next i
            End With

            'Ver2.0.6.0 DEL ｺﾝﾊﾟｲﾙ時にエラーでひっかけるのみにとどめる
            ''>>>計測点リスト全点でOUT設定があるCHのFU判定
            'With gudt.SetChInfo
            '    '計測点リスト全点ﾙｰﾌﾟ
            '    For i = 0 To UBound(.udtChannel) Step 1
            '        '計測点リストが
            '        ' モーター
            '        ' バルブ DIDO,AIAO,DIAO,AO,DO
            '        'の場合のみ対象。FUｱﾄﾞﾚｽを取得しておく
            '        blOUTch = False
            '        Select Case .udtChannel(i).udtChCommon.shtChType
            '            Case gCstCodeChTypeMotor
            '                'モーター
            '                blOUTch = True
            '                intFU = .udtChannel(i).MotorFuNo
            '                intSlot = .udtChannel(i).MotorPortNo
            '            Case gCstCodeChTypeValve
            '                'バルブ
            '                Select Case .udtChannel(i).udtChCommon.shtData
            '                    Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO
            '                        'DIDO
            '                        blOUTch = True
            '                        intFU = .udtChannel(i).ValveDiDoFuNo
            '                        intSlot = .udtChannel(i).ValveDiDoPortNo
            '                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
            '                        'AIDO
            '                        blOUTch = True
            '                        intFU = .udtChannel(i).ValveAiDoFuNo
            '                        intSlot = .udtChannel(i).ValveAiDoPortNo
            '                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2, gCstCodeChDataTypeValveAO_4_20
            '                        'AIAO
            '                        blOUTch = True
            '                        intFU = .udtChannel(i).ValveAiAoFuNo
            '                        intSlot = .udtChannel(i).ValveAiAoPortNo
            '                End Select
            '        End Select
            '        'アドレスがFFなら何もしない
            '        If blOUTch = True Then
            '            If intFU = &HFFFF And intSlot = &HFFFF Then
            '                blOUTch = False
            '            End If
            '        End If

            '        If blOUTch = True Then
            '            '基板に同じFUがあるならOK。そうでないならFUをｸﾘｱ
            '            blDel = False
            '            For j = 0 To aryFUout.Count - 1 Step 1
            '                '基板のFuｱﾄﾞﾚｽを1件取り出し
            '                strFUadr = aryFUout(j)
            '                strTerFUadr = strFUadr.Split(",")
            '                intTerFU = CInt(strTerFUadr(0))
            '                intTerSlot = CInt(strTerFUadr(1))
            '                If intFU = intTerFU And intSlot = intTerSlot Then
            '                    blDel = True
            '                    Exit For
            '                End If
            '            Next j

            '            If blDel = False Then
            '                '該当基板が無いため計測点側のFUｱﾄﾞﾚｽ消去
            '                Select Case .udtChannel(i).udtChCommon.shtChType
            '                    Case gCstCodeChTypeMotor
            '                        'モーター
            '                        .udtChannel(i).MotorFuNo = &HFFFF
            '                        .udtChannel(i).MotorPortNo = &HFFFF
            '                        .udtChannel(i).MotorPin = &HFFFF
            '                        .udtChannel(i).MotorPinNo = 0
            '                    Case gCstCodeChTypeValve
            '                        'バルブ
            '                        Select Case .udtChannel(i).udtChCommon.shtData
            '                            Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO
            '                                'DIDO
            '                                .udtChannel(i).ValveDiDoFuNo = &HFFFF
            '                                .udtChannel(i).ValveDiDoPortNo = &HFFFF
            '                                .udtChannel(i).ValveDiDoPin = &HFFFF
            '                                .udtChannel(i).ValveDiDoPinNo = 0
            '                            Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
            '                                'AIDO
            '                                .udtChannel(i).ValveAiDoFuNo = &HFFFF
            '                                .udtChannel(i).ValveAiDoPortNo = &HFFFF
            '                                .udtChannel(i).ValveAiDoPin = &HFFFF
            '                                .udtChannel(i).ValveAiDoPinNo = 0
            '                            Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2, gCstCodeChDataTypeValveAO_4_20
            '                                'AIAO
            '                                .udtChannel(i).ValveAiAoFuNo = &HFFFF
            '                                .udtChannel(i).ValveAiAoPortNo = &HFFFF
            '                                .udtChannel(i).ValveAiAoPin = &HFFFF
            '                                .udtChannel(i).ValveAiAoPinNo = 0
            '                        End Select
            '                End Select
            '            End If
            '        End If
            '    Next i
            'End With

            '>>>OUTputとAndOrテーブルのｸﾘｱ
            With gudt.SetChOutput
                '該当FUアドレスかチェック
                For i = 0 To UBound(.udtCHOutPut) Step 1
                    intFU = .udtCHOutPut(i).bytFuno
                    intSlot = .udtCHOutPut(i).bytPortno
                    '基板に同じFUがあるならOK。そうでないならFUをｸﾘｱ
                    blDel = False
                    For j = 0 To aryFUout.Count - 1 Step 1
                        '基板のFuｱﾄﾞﾚｽを1件取り出し
                        strFUadr = aryFUout(j)
                        strTerFUadr = strFUadr.Split(",")
                        intTerFU = CInt(strTerFUadr(0))
                        intTerSlot = CInt(strTerFUadr(1))
                        If intFU = intTerFU And intSlot = intTerSlot Then
                            blDel = True
                            Exit For
                        End If
                    Next j
                    'FFなら何もしない
                    If blDel = False Then
                        If intFU = &HFF And intSlot = &HFF Then
                            blDel = True
                        End If
                    End If

                    If blDel = False Then
                        'type<>0　＝論理CHのためAndOrテーブルもｸﾘｱ
                        If .udtCHOutPut(i).bytType <> 0 Then
                            'AndOrテーブルもｸﾘｱ
                            For x = 0 To UBound(gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr) Step 1
                                gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr(x).shtSysno = 0
                                gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr(x).shtChid = 0
                                gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr(x).bytSpare = 0
                                gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr(x).bytStatus = 0
                                gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr(x).shtMask = 0
                            Next x
                        End If
                        '出力テーブルｸﾘｱ
                        .udtCHOutPut(i).shtSysno = 0
                        .udtCHOutPut(i).shtChid = 0
                        .udtCHOutPut(i).bytType = 0
                        .udtCHOutPut(i).bytStatus = 0
                        .udtCHOutPut(i).shtMask = 0
                        .udtCHOutPut(i).bytOutput = 0
                        .udtCHOutPut(i).bytFuno = gCstCodeChNotSetFuNoByte
                        .udtCHOutPut(i).bytPortno = gCstCodeChNotSetFuPortByte
                        .udtCHOutPut(i).bytPin = gCstCodeChNotSetFuPinByte

                    End If
                Next i
            End With

            'Ver2.0.7.7
            '>>>AndOrテーブルのクリア
            'ChOutに無いのにAndOrにある＝ゴミのため削除
            With gudt.SetChAndOr
                For i = 0 To UBound(.udtCHOut) Step 1
                    For j = 0 To UBound(.udtCHOut(i).udtCHAndOr) Step 1
                        If .udtCHOut(i).udtCHAndOr(j).shtChid <> 0 Then
                            'CHIDが入っているAndOrテーブルで
                            blDel = False
                            For x = 0 To UBound(gudt.SetChOutput.udtCHOutPut) Step 1
                                If gudt.SetChOutput.udtCHOutPut(x).shtChid - 1 = i Then
                                    blDel = True
                                End If
                            Next x
                            'なければゴミとしてAndOr削除
                            If blDel = False Then
                                .udtCHOut(i).udtCHAndOr(j).shtSysno = 0
                                .udtCHOut(i).udtCHAndOr(j).shtChid = 0
                                .udtCHOut(i).udtCHAndOr(j).bytSpare = 0
                                .udtCHOut(i).udtCHAndOr(j).bytStatus = 0
                                .udtCHOut(i).udtCHAndOr(j).shtMask = 0
                            End If
                        End If
                    Next j
                Next i
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region

    '----------------------------------------------------------------------------
    '  DO端子設定の保存
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Friend Sub mSetDoTermSetting(setvalue As UShort, funo As Integer, portno As Integer)

        mudtSetFuNew.udtFu(funo).udtSlotInfo(portno).shtTerinf = CShort(Val("&H" & Hex(setvalue)))

    End Sub

    '----------------------------------------------------------------------------
    '  DO端子設定の変更有無確認
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Friend Function mChkFuDoTermSetting(FuNo As Integer, PortNo As Integer) As Boolean

        If mudtSetFuNew.udtFu(FuNo).udtSlotInfo(PortNo).shtTerinf > 20 Or mudtSetFuNew.udtFu(FuNo).udtSlotInfo(PortNo).shtTerinf < 0 Then
            If gudt.SetFu.udtFu(FuNo).udtSlotInfo(PortNo).shtTerinf <> mudtSetFuNew.udtFu(FuNo).udtSlotInfo(PortNo).shtTerinf Then
                Return True
            End If
        End If

        Return False

    End Function

    '----------------------------------------------------------------------------
    '  DO端子設定の現在設定を仮セット
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Friend Sub mSetDoTermSettingForTemp(FuNo As Integer, PortNo As Integer)
        mudtSetFuNew.udtFu(FuNo).udtSlotInfo(PortNo).shtTerinf = gudt.SetFu.udtFu(FuNo).udtSlotInfo(PortNo).shtTerinf
    End Sub

    '----------------------------------------------------------------------------
    '  DO端子設定の本保存
    '
    '   Ver.2.0.8.P
    '----------------------------------------------------------------------------
    Friend Sub mSaveFuDoTermSetting()

        Dim i, j As Integer

        For i = 0 To 20
            For j = 0 To 8 - 1
                If mudtSetFuNew.udtFu(i).udtSlotInfo(j).shtTerinf <> 0 Then
                    If gudt.SetFu.udtFu(i).udtSlotInfo(j).shtTerinf <> mudtSetFuNew.udtFu(i).udtSlotInfo(j).shtTerinf Then
                        gudt.SetFu.udtFu(i).udtSlotInfo(j).shtTerinf = mudtSetFuNew.udtFu(i).udtSlotInfo(j).shtTerinf
                    End If
                End If
            Next
        Next
    End Sub

End Class
