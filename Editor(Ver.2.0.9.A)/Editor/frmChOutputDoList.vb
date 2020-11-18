Public Class frmChOutputDoList

#Region "変数定義"

    Private mudtSetCHOutPut As gTypSetCHOutPut
    Private mudtSetCHAndOr As gTypSetCHAndOr

    Private mudtSetCHOutPutNew As gTypSetCHOutput
    Private mudtSetCHAndOrNew As gTypSetChAndOr

    ''デジタル出力情報格納
    Public Structure mDoInfo
        Public No As Integer
        Public Sysno As String      ''SYSTEM No.
        Public Chid As String       ''CH ID 又は 論理出力 ID
        Public Type As String       ''CHデータ、論理出力チャネルデータ
        Public Status As String     ''Output Movement
        Public Mask As Integer      ''Output Movement マスクデータ（ビットパターン）
        Public Output As String     ''CH OUT Type Setup
        Public Funo As String       ''FU 番号
        Public Portno As String     ''FU ポート番号
        Public Pin As String        ''FU 計測点番号
    End Structure
    Public mDoDetail As mDoInfo

    ''論理出力情報格納(24チャンネル分)
    Public Structure mOrAndInfo
        Public Sysno As String      ''SYSTEM No.
        Public Chid As String       ''CH ID
        Public Status As String     ''ステータス種類
        Public Mask As Integer      ''マスクデータ
    End Structure
    Public mOrAndDetail(23) As mOrAndInfo

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChOutputDoList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim i As Integer

            ''グリッドを初期化する
            Call mInitialDataGrid()

            ''コンボボックス初期化
            Call gSetComboBox(cmbChOutType, gEnmComboType.ctChOutputDoChOutType)
            Call gSetComboBox(cmbOutputMovement, gEnmComboType.ctChOutputDoStatus)

            ''配列再定義
            mudtSetCHOutPut.InitArray()

            mudtSetCHAndOr.InitArray()
            For i = LBound(mudtSetCHAndOr.udtCHOut) To UBound(mudtSetCHAndOr.udtCHOut)
                mudtSetCHAndOr.udtCHOut(i).InitArray()
            Next

            mudtSetCHOutPutNew.InitArray()

            mudtSetCHAndOrNew.InitArray()
            For i = LBound(mudtSetCHAndOrNew.udtCHOut) To UBound(mudtSetCHAndOrNew.udtCHOut)
                mudtSetCHAndOrNew.udtCHOut(i).InitArray()
            Next

            ''構造体複製
            Call mCopyStructure1(gudt.SetChOutput, mudtSetCHOutPut)
            Call mCopyStructure1(gudt.SetChOutput, mudtSetCHOutPutNew)

            Call mCopyStructure2(gudt.SetCHAndOr, mudtSetCHAndOr)
            Call mCopyStructure2(gudt.SetCHAndOr, mudtSetCHAndOrNew)


            ''画面設定
            Call mSetDisplay(mudtSetCHOutPut)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdDO_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDO.CellDoubleClick

        Try

            If e.RowIndex < 0 Then Exit Sub

            ''詳細画面表示
            Call mEdit(e.RowIndex)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： cmdEditボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click

        Try

            If grdDO.CurrentRow.Index < 0 Then Exit Sub

            ''詳細画面表示
            Call mEdit(grdDO.CurrentRow.Index)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Addボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        Try

            If grdDO.CurrentRow.Index < 0 Then Exit Sub

            If MsgBox("May I insert CHOUT here ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "DO insertion") = MsgBoxResult.Yes Then

                ''一覧を1行づつ下にずらす
                Dim intRow As Integer = grdDO.CurrentRow.Index
                Dim rw, col As Integer

                For rw = grdDO.RowCount - 2 To intRow Step -1

                    For col = 0 To grdDO.ColumnCount - 1
                        grdDO(col, rw + 1).Value = grdDO(col, rw).Value
                    Next

                    mudtSetCHOutPutNew.udtCHOutPut(rw + 1) = mudtSetCHOutPutNew.udtCHOutPut(rw)

                Next

                For col = 0 To grdDO.ColumnCount - 1
                    grdDO(col, intRow).Value = ""
                Next

                With mudtSetCHOutPutNew.udtCHOutPut(intRow)
                    .shtSysno = 0
                    .shtChid = 0
                    .bytType = 0
                    .bytStatus = 0
                    .shtMask = 0
                    .bytOutput = 0
                    .bytFuno = 255
                    .bytPortno = 255
                    .bytPin = 255
                End With

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Deleteボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        Try

            If grdDO.CurrentRow.Index < 0 Then Exit Sub

            If MsgBox("May I remove this CHOUT ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "DO Delete") = MsgBoxResult.Yes Then

                ''一覧を1行づつ上にずらす
                Dim intRow As Integer = grdDO.CurrentRow.Index
                Dim rw, col As Integer

                For rw = intRow + 1 To grdDO.RowCount - 1

                    For col = 0 To grdDO.ColumnCount - 1
                        grdDO(col, rw - 1).Value = grdDO(col, rw).Value
                    Next

                    mudtSetCHOutPutNew.udtCHOutPut(rw - 1) = mudtSetCHOutPutNew.udtCHOutPut(rw)

                Next

                For col = 0 To grdDO.ColumnCount - 1
                    grdDO(col, grdDO.RowCount - 1).Value = ""
                Next

                With mudtSetCHOutPutNew.udtCHOutPut(grdDO.RowCount - 1)
                    .shtSysno = 0
                    .shtChid = 0
                    .bytType = 0
                    .bytStatus = 0
                    .shtMask = 0
                    .bytOutput = 0
                    .bytFuno = 255
                    .bytPortno = 255
                    .bytPin = 255
                End With

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Saveボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetCHOutPut, mudtSetCHOutPutNew, mudtSetCHAndOr, mudtSetCHAndOrNew) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure1(mudtSetCHOutPutNew, gudt.SetChOutput)
                Call mCopyStructure2(mudtSetCHAndOrNew, gudt.SetCHAndOr)

                Call mCopyStructure1(mudtSetCHOutPutNew, mudtSetCHOutPut)
                Call mCopyStructure2(mudtSetCHAndOrNew, mudtSetCHAndOr)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

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
    Private Sub frmChOutputDoList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''データが変更されているかチェック
            If Not mChkStructureEquals(mudtSetCHOutPut, mudtSetCHOutPutNew, mudtSetCHAndOr, mudtSetCHAndOrNew) Then

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
                        Call mCopyStructure1(mudtSetCHOutPutNew, gudt.SetChOutput)
                        Call mCopyStructure2(mudtSetCHAndOrNew, gudt.SetChAndOr)

                        Call mCopyStructure1(mudtSetCHOutPutNew, mudtSetCHOutPut)
                        Call mCopyStructure2(mudtSetCHAndOrNew, mudtSetCHAndOr)

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
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChOutputDoList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： cmdExitボタンクリック
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

            ''この画面は入力チェックなし
            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 出力チャンネル設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSetChOutPut As gTypSetCHOutput)

        Try

            For i As Integer = 0 To grdDO.RowCount - 1

                With grdDO.Rows(i)

                    If udtSetChOutPut.udtCHOutPut(i).shtChid > 0 And (udtSetChOutPut.udtCHOutPut(i).bytOutput <> 255) Then

                        ''Type
                        cmbChOutType.SelectedValue = udtSetChOutPut.udtCHOutPut(i).bytOutput.ToString
                        .Cells(0).Value = cmbChOutType.Text

                        ''Fu Address
                        .Cells(1).Value = gGetFuName2(udtSetChOutPut.udtCHOutPut(i).bytFuno)
                        .Cells(2).Value = IIf(udtSetChOutPut.udtCHOutPut(i).bytPortno = 255, "", udtSetChOutPut.udtCHOutPut(i).bytPortno)
                        .Cells(3).Value = IIf(udtSetChOutPut.udtCHOutPut(i).bytPin = 255, "", udtSetChOutPut.udtCHOutPut(i).bytPin)

                        ''チャンネルID → チャンネルNO 変換(変換場所)
                        '.Cells(4).Value = gConvChIdToChNo(udtSetChOutPut.udtCHOutPut(i).shtChid, True)
                        .Cells(4).Value = udtSetChOutPut.udtCHOutPut(i).shtChid.ToString("0000")

                        ''Status
                        cmbOutputMovement.SelectedValue = udtSetChOutPut.udtCHOutPut(i).bytStatus.ToString
                        .Cells(5).Value = cmbOutputMovement.Text

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
    Private Sub mCopyStructure1(ByVal udtSource As gTypSetCHOutput, _
                                ByRef udtTarget As gTypSetCHOutput)

        Try

            For i As Integer = LBound(udtSource.udtCHOutPut) To UBound(udtSource.udtCHOutPut)

                udtTarget.udtCHOutPut(i).shtSysno = udtSource.udtCHOutPut(i).shtSysno
                udtTarget.udtCHOutPut(i).shtChid = udtSource.udtCHOutPut(i).shtChid
                udtTarget.udtCHOutPut(i).bytType = udtSource.udtCHOutPut(i).bytType
                udtTarget.udtCHOutPut(i).bytStatus = udtSource.udtCHOutPut(i).bytStatus
                udtTarget.udtCHOutPut(i).shtMask = udtSource.udtCHOutPut(i).shtMask
                udtTarget.udtCHOutPut(i).bytOutput = udtSource.udtCHOutPut(i).bytOutput
                udtTarget.udtCHOutPut(i).bytFuno = udtSource.udtCHOutPut(i).bytFuno
                udtTarget.udtCHOutPut(i).bytPortno = udtSource.udtCHOutPut(i).bytPortno
                udtTarget.udtCHOutPut(i).bytPin = udtSource.udtCHOutPut(i).bytPin

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mCopyStructure2(ByVal udtSource As gTypSetChAndOr, _
                                ByRef udtTarget As gTypSetChAndOr)

        Try

            For i As Integer = LBound(udtSource.udtCHOut) To UBound(udtSource.udtCHOut)

                For j As Integer = LBound(udtSource.udtCHOut(i).udtCHAndOr) To UBound(udtSource.udtCHOut(i).udtCHAndOr)

                    udtTarget.udtCHOut(i).udtCHAndOr(j).shtSysno = udtSource.udtCHOut(i).udtCHAndOr(j).shtSysno
                    udtTarget.udtCHOut(i).udtCHAndOr(j).shtChid = udtSource.udtCHOut(i).udtCHAndOr(j).shtChid
                    udtTarget.udtCHOut(i).udtCHAndOr(j).bytStatus = udtSource.udtCHOut(i).udtCHAndOr(j).bytStatus
                    udtTarget.udtCHOut(i).udtCHAndOr(j).shtMask = udtSource.udtCHOut(i).udtCHAndOr(j).shtMask

                Next

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetCHOutput, ByVal udt2 As gTypSetCHOutput, _
                                         ByVal udt3 As gTypSetChAndOr, ByVal udt4 As gTypSetChAndOr) As Boolean

        Try

            For i As Integer = LBound(udt1.udtCHOutPut) To UBound(udt1.udtCHOutPut)

                If udt1.udtCHOutPut(i).shtSysno <> udt2.udtCHOutPut(i).shtSysno Then Return False
                If udt1.udtCHOutPut(i).shtChid <> udt2.udtCHOutPut(i).shtChid Then Return False
                If udt1.udtCHOutPut(i).bytType <> udt2.udtCHOutPut(i).bytType Then Return False
                If udt1.udtCHOutPut(i).bytStatus <> udt2.udtCHOutPut(i).bytStatus Then Return False
                If udt1.udtCHOutPut(i).shtMask <> udt2.udtCHOutPut(i).shtMask Then Return False
                If udt1.udtCHOutPut(i).bytOutput <> udt2.udtCHOutPut(i).bytOutput Then Return False
                If udt1.udtCHOutPut(i).bytFuno <> udt2.udtCHOutPut(i).bytFuno Then Return False
                If udt1.udtCHOutPut(i).bytPortno <> udt2.udtCHOutPut(i).bytPortno Then Return False
                If udt1.udtCHOutPut(i).bytPin <> udt2.udtCHOutPut(i).bytPin Then Return False

            Next

            For i As Integer = LBound(udt3.udtCHOut) To UBound(udt3.udtCHOut)

                For j As Integer = LBound(udt3.udtCHOut(i).udtCHAndOr) To UBound(udt3.udtCHOut(i).udtCHAndOr)

                    If udt3.udtCHOut(i).udtCHAndOr(j).shtSysno <> udt4.udtCHOut(i).udtCHAndOr(j).shtSysno Then Return False
                    If udt3.udtCHOut(i).udtCHAndOr(j).shtChid <> udt4.udtCHOut(i).udtCHAndOr(j).shtChid Then Return False
                    If udt3.udtCHOut(i).udtCHAndOr(j).bytStatus <> udt4.udtCHOut(i).udtCHAndOr(j).bytStatus Then Return False
                    If udt3.udtCHOut(i).udtCHAndOr(j).shtMask <> udt4.udtCHOut(i).udtCHAndOr(j).shtMask Then Return False

                Next

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 詳細画面表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 行番号
    ' 機能説明  : 対象行の詳細設定画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub mEdit(ByVal intRow As Integer)

        Try

            Dim i, j, ii As Integer
            Dim flgOrAndBK As Integer = 0
            Dim intOrAndIndexBK As Integer = 0
            Dim intOrAndIndex As Integer = 0
            Dim intStart As Integer, intEnd As Integer
            Dim intStep1 As Integer = 0, intStep2 As Integer = 0


            With mudtSetCHOutPutNew.udtCHOutPut(intRow)

                ''デジタル出力情報
                mDoDetail.No = intRow + 1
                mDoDetail.Sysno = .shtSysno
                mDoDetail.Chid = .shtChid.ToString("0000")
                mDoDetail.Type = .bytType

                cmbOutputMovement.SelectedValue = .bytStatus.ToString
                mDoDetail.Status = cmbOutputMovement.Text

                mDoDetail.Mask = .shtMask

                cmbChOutType.SelectedValue = .bytOutput.ToString
                mDoDetail.Output = cmbChOutType.Text

                mDoDetail.Funo = .bytFuno
                mDoDetail.Portno = .bytPortno
                mDoDetail.Pin = .bytPin

                ''論理出力チャンネルの場合は論理出力データも渡す
                If .bytType = 1 Or .bytType = 2 Then

                    If .bytType = 1 Then
                        ''OR
                        intStart = 0 : intEnd = 31

                    ElseIf .bytType = 2 Then
                        ''AND
                        intStart = 32 : intEnd = 32 + 16 - 1
                    End If

                    For i = intStart To intEnd

                        If mudtSetCHAndOrNew.udtCHOut(i).udtCHAndOr(0).shtChid = .shtChid Then

                            For j = 0 To 23

                                mOrAndDetail(j).Sysno = mudtSetCHAndOrNew.udtCHOut(i).udtCHAndOr(j).shtSysno
                                mOrAndDetail(j).Chid = mudtSetCHAndOrNew.udtCHOut(i).udtCHAndOr(j).shtChid
                                mOrAndDetail(j).Status = mudtSetCHAndOrNew.udtCHOut(i).udtCHAndOr(j).bytStatus
                                mOrAndDetail(j).Mask = mudtSetCHAndOrNew.udtCHOut(i).udtCHAndOr(j).shtMask

                            Next j

                            intOrAndIndexBK = i
                            flgOrAndBK = .bytType
                            Exit For

                        End If

                    Next i

                End If

                ''デジタル出力情報詳細画面表示 ==========================
                If frmChOutputDoDetail.gShow(intRow, mDoDetail, mOrAndDetail) = 0 Then

                    If mDoDetail.Chid <> 0 Then

                        ''論理出力チャンネルの場合

                        'If mDoDetail.Type <> 0 Then
                        '    ''論理出力設定ファイルの配列のインデックス
                        '    .shtChid()

                        'End If

                        If mDoDetail.Type = 1 And flgOrAndBK = 1 Then intStep1 = 1 : intStep2 = 0 ''前回と同じ
                        If mDoDetail.Type = 2 And flgOrAndBK = 2 Then intStep1 = 1 : intStep2 = 0 ''前回と同じ

                        If mDoDetail.Type = 1 And flgOrAndBK = 2 Then intStep1 = 3 : intStep2 = 2 ''AndとORが逆
                        If mDoDetail.Type = 2 And flgOrAndBK = 1 Then intStep1 = 3 : intStep2 = 2 ''AndとORが逆

                        If mDoDetail.Type = 0 And (flgOrAndBK = 1 Or flgOrAndBK = 2) Then intStep1 = 3 : intStep2 = 0 ''前回AndかOrがあったが今回はなし
                        If mDoDetail.Type <> 0 And (flgOrAndBK = 0) Then intStep1 = 2 : intStep2 = 0 ''前回はなかったが今回はAndかOrがある

                        For ii = 0 To 1

                            If ii = 1 Then intStep1 = intStep2

                            If intStep1 = 1 Then
                                ''前と同じ論理出力チャンネルなので同じ位置に上書き
                                For i = 0 To 23
                                    With mudtSetCHAndOrNew.udtCHOut(intOrAndIndexBK).udtCHAndOr(i)

                                        .shtSysno = mOrAndDetail(i).Sysno
                                        .shtChid = mOrAndDetail(i).Chid

                                        cmbOutputMovement.Text = mOrAndDetail(i).Status
                                        .bytStatus = cmbOutputMovement.SelectedValue

                                        .shtMask = mOrAndDetail(i).Mask
                                    End With
                                Next

                            ElseIf intStep1 = 2 Then
                                ''新規で論理出力チャンネル
                                If mDoDetail.Type = 1 Then
                                    ''OR
                                    intStart = 0 : intEnd = 31

                                ElseIf mDoDetail.Type = 2 Then
                                    ''AND
                                    intStart = 32 : intEnd = 32 + 16 - 1
                                End If

                                For i = intStart To intEnd

                                    If mudtSetCHAndOrNew.udtCHOut(i).udtCHAndOr(0).shtChid = 0 Then

                                        For j = 0 To 23
                                            With mudtSetCHAndOrNew.udtCHOut(i).udtCHAndOr(j)
                                                .shtSysno = mOrAndDetail(j).Sysno
                                                .shtChid = mOrAndDetail(j).Chid

                                                cmbOutputMovement.Text = mOrAndDetail(j).Status
                                                .bytStatus = cmbOutputMovement.SelectedValue

                                                .shtMask = mOrAndDetail(j).Mask
                                            End With
                                        Next j

                                        flgOrAndBK = mDoDetail.Type
                                        Exit For

                                    End If
                                Next i

                                ''論理出力設定ファイルが一杯なので保存出来ない！
                                If flgOrAndBK = 0 Then
                                    If mDoDetail.Type = 1 Then
                                        MsgBox("The logical output configuration file is full.  [OR] Data", MsgBoxStyle.Exclamation, "DO SetUp List")
                                        Exit Sub
                                    ElseIf mDoDetail.Type = 2 Then
                                        MsgBox("The logical output configuration file is full.  [AND] Data", MsgBoxStyle.Exclamation, "DO SetUp List")
                                        Exit Sub
                                    End If
                                End If

                            ElseIf intStep1 = 3 Then
                                ''前は論理出力チャンネルだったので、痕跡を消す
                                For i = 0 To 23
                                    With mudtSetCHAndOrNew.udtCHOut(intOrAndIndexBK).udtCHAndOr(i)
                                        .shtSysno = 0
                                        .shtChid = 0
                                        .bytStatus = 0
                                        .shtMask = 0
                                    End With
                                Next

                            End If

                        Next ii



                        ''デジタル出力情報更新
                        .shtSysno = mDoDetail.Sysno

                        'If mDoDetail.Type = 0 Then .shtChid = mDoDetail.Chid
                        .shtChid = mDoDetail.Chid

                        .bytType = mDoDetail.Type

                        cmbOutputMovement.Text = mDoDetail.Status
                        .bytStatus = cmbOutputMovement.SelectedValue

                        .shtMask = mDoDetail.Mask

                        cmbChOutType.Text = mDoDetail.Output
                        .bytOutput = cmbChOutType.SelectedValue

                        .bytFuno = mDoDetail.Funo
                        .bytPortno = mDoDetail.Portno
                        .bytPin = mDoDetail.Pin

                        ''一覧を更新
                        grdDO.Rows(intRow).Cells(0).Value = mDoDetail.Output
                        grdDO.Rows(intRow).Cells(1).Value = gGetFuName2(mDoDetail.Funo)
                        grdDO.Rows(intRow).Cells(2).Value = mDoDetail.Portno
                        grdDO.Rows(intRow).Cells(3).Value = mDoDetail.Pin

                        ''チャンネルID → チャンネルNO 変換
                        grdDO.Rows(intRow).Cells(4).Value = IIf(mDoDetail.Chid = 0, "", Val(mDoDetail.Chid).ToString("0000"))

                        grdDO.Rows(intRow).Cells(5).Value = mDoDetail.Status

                    End If

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtType"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtFcuFuAdd1"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtFcuFuAdd2"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtFcuFuAdd3"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtChNo"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtSatus"

            Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Column6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdHead

                ''列
                .Columns.Clear()
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())
                .Columns.Add(New DataGridViewCheckBoxColumn())

                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''列ヘッダー
                .Columns(0).HeaderText = "TYPE"
                .Columns(0).Width = 80

                .Columns(1).HeaderText = "FCU/FU Add"
                .Columns(1).Width = 160

                .Columns(2).HeaderText = "CH No."
                .Columns(2).Width = 80

                .Columns(3).HeaderText = "Status"
                .Columns(3).Width = 120

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 1
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可

                ''行ヘッダー
                .RowHeadersWidth = 60

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

            End With

            With grdDO

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .ColumnHeadersVisible = False
                .Columns(0).Width = 80
                .Columns(1).Width = 50
                .Columns(2).Width = 55
                .Columns(3).Width = 55
                .Columns(4).Width = 80
                .Columns(5).Width = 120
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 193
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 60
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

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
                .ScrollBars = ScrollBars.Vertical

                ''行選択モード
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ReadOnly = True    ''書込み不可！
                .MultiSelect = False

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
