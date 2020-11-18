Public Class frmSeqSetSequenceDetail_GAI

#Region "変数定義"
    '計測点情報
    Private Structure mChannelStr
        Dim SYSNo As String                     ''SYSTEM No
        Dim CHNo As String                      ''CH番号
        Dim CHNo_temp As String                 ''CH番号  Ver1.8.5.2  2015.12.02  ﾀｸﾞ表示時の補助表示用
        Dim CHItem As String                    ''CH名称
        Dim Status As String                    ''ステータス
        Dim Range As String                     ''レンジ
        Dim Unit As String                      ''単位
        Dim AlmInf() As mAlarmInfoStr           ''アラーム情報
        Dim INSIG As String                     ''INPUT SIGNAL
        Dim SIGType As String                   ''SIGNAL TYPE
        Dim OUTSIG As String                    ''OUTPUT SIGNAL
        Dim INAdd As String                     ''INPUT ADDRESS
        Dim OUTAdd As String                    ''OUTPUT ADDRESS
        Dim AL As String                        ''AL
        Dim RL As String                        ''RL
        Dim ShareType As String                 ''共有CHタイプ
        Dim ShareCHNo As String                 ''共有CH No
        Dim Remarks As String                   ''REMARKS
        Dim AlmLevel As String                  ''ﾛｲﾄﾞ対応表示追加　2015.11.12 Ver1.7.8
        Dim TermCount As Integer                ''端子数  Ver1.11.9.2 2016.11.26追加
        Dim OUT As String                       'Ver2.0.0.4 Output設定アリの場合「o」となる
    End Structure
    'アラーム情報
    Private Structure mAlarmInfoStr
        Dim Value As String                     ''警報値
        Dim ExtGrp As String                    ''Ext. Group No
        Dim Delay As String                     ''Delay
        Dim GrpRep1 As String                   ''Group Repose 1
        Dim GrpRep2 As String                   ''Group Repose 2
    End Structure


    Dim mintRtn As Integer
    Dim mblnFlg As Boolean
    Dim mudtSequenceSetDetail As gTypSetSeqSetRec

    Private praryCHLIST As ArrayList    '計測点CHno(配列格納順)
    Private aryOutCHNo As ArrayList

    Private mudtSetSequenceSet As gTypSetSeqSet
#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK  0:キャンセル
    ' 引き数    : ARG1 - (IO) シーケンス設定構造体
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtSequenceSet As gTypSetSeqSet, ByRef udtSequenceSetDetail As gTypSetSeqSetRec, ByRef frmOwner As Form) As Integer

        Try

            ''引数保存
            mudtSequenceSetDetail = udtSequenceSetDetail

            mudtSetSequenceSet = udtSequenceSet


            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 1 Then
                udtSequenceSetDetail = mudtSequenceSetDetail
            End If

            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmSeqSetSequenceDetail_GAI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            '計測点リストのCHNoを配列順に格納(CHnoから配列番号を取得するために使用)
            praryCHLIST = New ArrayList
            With gudt.SetChInfo
                'チャンネル番号を配列化
                For i As Integer = 0 To UBound(.udtChannel)
                    praryCHLIST.Add(.udtChannel(i).udtChCommon.shtChno.ToString("0000"))
                Next i
            End With
            Call subSetAllOutCH()


            ''コンボボックス初期設定
            Call gSetComboBox(cmbChStatus, gEnmComboType.ctSeqSetDetailStatus)
            Call gSetComboBox(cmbChOutputType, gEnmComboType.ctSeqSetDetailDataType)
            Call gSetComboBox(cmbFcuFuOutputType, gEnmComboType.ctSeqSetDetailOutputType)

            ''グリッド 初期設定
            Call mInitialDataGrid()
            Call subInitGridInChInfo()
            Call subInitGridOutChInfo()

            ''ロジックテキスト背景色
            txtLogic.BackColor = gColorGridRowBackReadOnly

            ''画面表示
            Call mSetDisplay(mudtSequenceSetDetail)

            'ロジック画像表示処理
            Call subSetLogicGazo(mudtSequenceSetDetail.shtLogicType)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "grdCH"
    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCH.CellDoubleClick

        Try

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            '入力不可ならば何もしない
            If grdCH(1, e.RowIndex).ReadOnly = True Then Exit Sub

            Dim udtInputCopy As gTypSetSeqSetRecInput

            '手入力による変更を考慮して該当行のデータを変数へ反映
            Dim i As Integer = e.RowIndex
            udtInputCopy = mudtSequenceSetDetail.udtInput(i)
            With udtInputCopy

                .shtChid = IIf(Trim(grdCH(1, i).Value) = "", 0, Trim(grdCH(1, i).Value))
                'STATUSはHEXで入力されている
                '.bytStatus = IIf(Trim(grdCH(2, i).Value) = "", 0, Trim(grdCH(2, i).Value))
                If Trim(grdCH(2, i).Value) = "" Then
                    .bytStatus = 0
                Else
                    .bytStatus = Convert.ToInt32(Trim(grdCH(2, i).Value), 16)
                End If

                'TYPEは文字列で入力されているため変換
                '.bytType = IIf(Trim(grdCH(3, i).Value) = "", 0, Trim(grdCH(3, i).Value))
                If Trim(grdCH(3, i).Value) = "" Then
                    .bytType = 0
                Else
                    .bytType = mGetTypeCode(Trim(grdCH(3, i).Value))
                End If

                'MASKは4桁HEXで入力されている
                '.shtMask = IIf(Trim(grdCH(4, i).Value) = "", 0, Trim(grdCH(3, i).Value))
                If Trim(grdCH(4, i).Value) = "" Then
                    .shtMask = 0
                Else
                    .shtMask = Convert.ToInt32(Trim(grdCH(4, i).Value), 16)
                End If
            End With
            
            If frmSeqSetInputData_GAI.gShow(e.RowIndex + 1, mudtSetSequenceSet, udtInputCopy, mudtSequenceSetDetail.shtLogicType, Me) = 0 Then

                ''InputCH情報表示
                mudtSequenceSetDetail.udtInput(e.RowIndex) = udtInputCopy
                Call mDispInputInfo(e.RowIndex, mudtSequenceSetDetail.udtInput(e.RowIndex))

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdCH_SelectionChanged(sender As Object, e As System.EventArgs) Handles grdCH.SelectionChanged
        Try
            'grdCHとgrdInChInfoの選択行を連動させる処理

            'InChInfoがまだ生成されてない場合は処理ぬけ
            If grdInChInfo.RowCount - 1 <= 0 Then
                Exit Sub
            End If

            'InChInfoの選択をクリア
            For i As Integer = 0 To grdInChInfo.RowCount - 1 Step 1
                grdInChInfo.Rows(i).Selected = False
            Next i

            'InChInfoの選択行をgrdCHの選択と同じ行にする
            For Each r As DataGridViewCell In grdCH.SelectedCells
                grdInChInfo.Rows(r.RowIndex).Selected = True
            Next r

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdCH.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdCH_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdCH_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdCH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdCH.KeyPress

        Try
            'グリット内への入力時の制御

            '選択セルの名称取得
            Dim strColumnName As String = grdCH.CurrentCell.OwningColumn.Name

            Select Case strColumnName
                Case "txtChNo"
                    '[CHNo] MAX5桁の数値のみ
                    e.Handled = gCheckTextInput(5, sender, e.KeyChar, True)
                Case "txtStatus"
                    '[txtStatus] MAX2桁の数値とA-Fのみ(HEX)
                    '文字を大文字に変換
                    Select Case e.KeyChar
                        Case "a"c : e.KeyChar = "A"c
                        Case "b"c : e.KeyChar = "B"c
                        Case "c"c : e.KeyChar = "C"c
                        Case "d"c : e.KeyChar = "D"c
                        Case "e"c : e.KeyChar = "E"c
                        Case "f"c : e.KeyChar = "F"c
                    End Select
                    '16進のみ。2桁
                    e.Handled = gCheckTextInput(2, sender, e.KeyChar, False, , , , "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F")
                Case "txtInputMask"
                    '[txtInputMask] MAX4桁の数値とA-Fのみ(HEX)
                    '文字を大文字に変換
                    Select Case e.KeyChar
                        Case "a"c : e.KeyChar = "A"c
                        Case "b"c : e.KeyChar = "B"c
                        Case "c"c : e.KeyChar = "C"c
                        Case "d"c : e.KeyChar = "D"c
                        Case "e"c : e.KeyChar = "E"c
                        Case "f"c : e.KeyChar = "F"c
                    End Select
                    '16進のみ。4桁
                    e.Handled = gCheckTextInput(4, sender, e.KeyChar, False, , , , "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F")
            End Select


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    Private Sub grdCH_CellValidating(sender As Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdCH.CellValidating
        Try
            'グリット入力直後の入力値妥当性チェック

            If e.RowIndex < 0 Or e.ColumnIndex < 1 Then Exit Sub

            Dim strValue As String
            strValue = grdCH(e.ColumnIndex, e.RowIndex).EditedFormattedValue

            'ChNo取得
            Dim intChNo As Integer = CCUInt16(grdCH(1, e.RowIndex).Value)

            '選択セルの名称取得
            Dim strColumnName As String = grdCH.CurrentCell.OwningColumn.Name

            If NZfS(strValue).Trim <> "" Then
                'ChNo未指定の場合エラー
                If intChNo <= 0 And e.ColumnIndex > 1 Then
                    MsgBox("ChNo Not input.", MsgBoxStyle.Exclamation, "CONTROL SEQUENCE")
                    e.Cancel = True : Exit Sub
                End If

                Select Case strColumnName
                    Case "txtChNo"
                        '[CHNo] MAX5桁の数値のみ 101～11024
                        If strValue.Length > 5 Or (CCInt(strValue) > 11024 Or CCInt(strValue) < 101) Then
                            MsgBox("The value is illegal.", MsgBoxStyle.Exclamation, "CONTROL SEQUENCE")
                            e.Cancel = True : Exit Sub
                        Else
                            '正常CHNoの場合、InChInfo更新
                            Dim strRet As String = ""
                            Dim strSplit() As String = Nothing
                            strRet = fnGetCHdata(CCInt(strValue).ToString("0000"))
                            strSplit = strRet.Split(vbTab)
                            If strSplit.Length >= 5 Then
                                grdInChInfo(1, e.RowIndex).Value = CCInt(strValue).ToString("0000")
                                grdInChInfo(2, e.RowIndex).Value = strSplit(0)
                                grdInChInfo(3, e.RowIndex).Value = strSplit(1)
                                grdInChInfo(4, e.RowIndex).Value = strSplit(2)
                                grdInChInfo(5, e.RowIndex).Value = strSplit(3)
                                grdInChInfo(6, e.RowIndex).Value = strSplit(4)

                                Call subSetTxtInCh(e.RowIndex, CCInt(strValue).ToString("0000"))
                            Else
                                grdInChInfo(1, e.RowIndex).Value = ""
                                grdInChInfo(2, e.RowIndex).Value = ""
                                grdInChInfo(3, e.RowIndex).Value = ""
                                grdInChInfo(4, e.RowIndex).Value = ""
                                grdInChInfo(5, e.RowIndex).Value = ""
                                grdInChInfo(6, e.RowIndex).Value = ""

                                Call subSetTxtInCh(e.RowIndex, "")
                            End If
                        End If
                End Select
            Else
                '未入力はOK
                Select strColumnName
                    Case "txtChNo"
                        grdCH(2, e.RowIndex).Value = ""
                        grdCH(3, e.RowIndex).Value = ""
                        grdCH(4, e.RowIndex).Value = ""
                        '
                        grdInChInfo(1, e.RowIndex).Value = ""
                        grdInChInfo(2, e.RowIndex).Value = ""
                        grdInChInfo(3, e.RowIndex).Value = ""
                        grdInChInfo(4, e.RowIndex).Value = ""
                        grdInChInfo(5, e.RowIndex).Value = ""
                        grdInChInfo(6, e.RowIndex).Value = ""
                End Select
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region

#Region "grdLogic"
    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdLogic_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLogic.CellDoubleClick
        Try
            Dim intBack As Integer
            Dim strRow As String
            Dim intRow As Integer

            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

            'liner,operatボタン両方が非表示なら何もしない
            If cmdLinear.Visible = False And cmdOperation.Visible = False Then
                Exit Sub
            End If

            'Linear ONの場合
            If cmdLinear.Visible = True Then
                strRow = NZfS(grdLogic(1, e.RowIndex).Value)
                If strRow = "" Then
                    intRow = 0
                Else
                    If IsNumeric(strRow) = False Then
                        intRow = 0
                    Else
                        intRow = CInt(strRow) - 1
                        If intRow < 0 Or intRow > 255 Then
                            intRow = 0
                        End If
                    End If
                End If
                Call frmSeqLinearTable_GAI.gShow(intRow, intBack, Me)
            End If

            'Operation ONの場合
            If cmdOperation.Visible = True Then
                strRow = NZfS(grdLogic(1, e.RowIndex).Value)
                If strRow = "" Then
                    intRow = 0
                Else
                    If IsNumeric(strRow) = False Then
                        intRow = 0
                    Else
                        intRow = CInt(strRow) - 1
                        If intRow < 0 Or intRow > 63 Then
                            intRow = 0
                        End If
                    End If
                End If
                Call frmSeqOperationFixed_GAI.gShow(intRow, intBack, Me)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdLogic_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdLogic.KeyPress

        Try

            ''選択セルの名称取得
            Dim strColumnName As String = grdLogic.CurrentCell.OwningColumn.Name

            ''[TABLE_NO.]
            If strColumnName = "txtTableNo" Then
                ' ver1.4.0 2011.07.19 Soft SwitchロジックのP_2入力の場合は小数点入力可
                ' ver1.4.7 2012.08.03 K.Tanigawa チェックボックスがTrueの場合はCHNo.なので小数点不可
                If mudtSequenceSetDetail.shtLogicType = 26 And grdLogic.CurrentCellAddress.Y = 0 And grdLogic(2, grdLogic.CurrentCellAddress.Y).Value <> True Then
                    e.Handled = gCheckTextInput(4, sender, e.KeyChar, True, False, True)
                Else
                    e.Handled = gCheckTextInput(5, sender, e.KeyChar)
                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント発生時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdLogic_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLogic.GotFocus

        Try

            Dim bytSet As Byte

            If Not mblnFlg Then Return

            ''選択セルの名称取得
            Dim strColumnName As String = grdLogic.CurrentCell.OwningColumn.Name

            ''[TABLE_NO.]
            If strColumnName = "txtTableNo" Then

                ''Code が 2（Bit指定）の場合
                If grdLogic(8, grdLogic.CurrentCell.RowIndex).Value = 2 Then

                    ''値取得
                    If gConvNullToZero(grdLogic(1, grdLogic.CurrentCell.RowIndex).Value) = 0 Then
                        bytSet = 0
                    ElseIf gConvNullToZero(grdLogic(1, grdLogic.CurrentCell.RowIndex).Value) > 255 Then
                        bytSet = 255
                    Else
                        bytSet = gConvNullToZero(grdLogic(1, grdLogic.CurrentCell.RowIndex).Value)
                    End If

                    ''GotFocusイベント重複抑制
                    mblnFlg = False

                    ''Bit設定画面表示
                    If frmBitSetByte.gShow(bytSet, 0, Me) = 1 Then
                        grdLogic(1, grdLogic.CurrentCell.RowIndex).Value = bytSet
                        grdLogic.EndEdit()
                    End If


                End If

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
    Private Sub grdLogic_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdLogic.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdLogic_KeyPress
                RemoveHandler tb.GotFocus, AddressOf grdLogic_GotFocus

                ''該当する列ならイベントハンドラを追加する
                If dgv.CurrentCell.OwningColumn.Name = "txtTableNo" Then

                    AddHandler tb.KeyPress, AddressOf grdLogic_KeyPress
                    AddHandler tb.GotFocus, AddressOf grdLogic_GotFocus

                    ''GotFocusイベント重複抑制
                    mblnFlg = True

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "grdChInfo"

#End Region


    '----------------------------------------------------------------------------
    ' 機能説明  ： Select（ロジック）ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click

        Try

            Dim strSelectLogicCode As String = ""
            Dim strSelectLogicName As String = ""
            Dim udtSequenceLogic() As gTypCodeName = Nothing
            Dim udtSequenceLogicSub() As gTypCodeName = Nothing

            ''シーケンスロジック設定取得
            Call gGetComboCodeName(udtSequenceLogic, gEnmComboType.ctSeqSetDetailLogic)

            If frmSeqSetSequenceLogic.gShow(udtSequenceLogic, strSelectLogicCode, strSelectLogicName, Me) = 0 Then

                ''テキストに表示
                txtLogic.Text = strSelectLogicName
                mudtSequenceSetDetail.shtLogicType = CCShort(strSelectLogicCode)

                ''シーケンスロジックサブ設定取得
                'Call gGetComboCodeName(udtSequenceLogicSub, gEnmComboType.ctSeqSetDetailLogic, Format(CInt(strSelectLogicCode), "00"))

                ''サブ情報表示
                'For i As Integer = 0 To UBound(udtSequenceLogicSub)
                '    grdLogic(0, i).Value = udtSequenceLogicSub(i).strName
                '    grdLogic(1, i).Value = ""

                '    grdLogic(3, i).Value = udtSequenceLogicSub(i).strOption1
                '    grdLogic(4, i).Value = udtSequenceLogicSub(i).strOption2
                '    grdLogic(5, i).Value = udtSequenceLogicSub(i).strOption3
                '    grdLogic(6, i).Value = udtSequenceLogicSub(i).strOption4
                '    grdLogic(7, i).Value = udtSequenceLogicSub(i).strOption5
                '    grdLogic(8, i).Value = udtSequenceLogicSub(i).shtCode

                'Next

                Call mSetDisplay(mudtSequenceSetDetail)

                'ロジック画像表示処理
                Call subSetLogicGazo(mudtSequenceSetDetail.shtLogicType)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Deleteボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 選択行の設定を削除する
    '--------------------------------------------------------------------
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        Try

            ''選択セルの行位置が0より小さい、もしくは最大行数より大きい場合は処理を抜ける
            If grdCH.CurrentCell.RowIndex < 0 Or _
               grdCH.CurrentCell.RowIndex > grdCH.RowCount - 1 Then Return

            If MessageBox.Show("Do you delete the selected input set?", _
                               Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                ''シーケンスIDリセット
                Call gInitSetSeqSequenceInputOne(mudtSequenceSetDetail.udtInput(grdCH.CurrentCell.RowIndex))

                ''画面更新
                Call mDispInputInfo(grdCH.CurrentCell.RowIndex, mudtSequenceSetDetail.udtInput(grdCH.CurrentCell.RowIndex))

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Linearボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdLinear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLinear.Click

        Try
            Dim intBack As Integer
            Call frmSeqLinearTable_GAI.gShow(0, intBack, Me)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Operationボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOperation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOperation.Click

        Try
            Dim intBack As Integer
            Call frmSeqOperationFixed_GAI.gShow(0, intBack, Me)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Remarksテキストキープレス
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtRemarks_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress

        e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： OKボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値格納
            Call mSetStructure(mudtSequenceSetDetail)

            mintRtn = 1
            Call Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Cancelボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmSeqSetSequenceDetail_GAI_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "入力関連"

#Region "入力制限"

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No. KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChNo.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Output Data KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtOutputData_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChOutputData.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Output Offdelay KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtOutputOffdelay_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChOutputOffdelay.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Output Offdelay KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtOutputTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFcuFuOutputTime.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： txtFcuFuFuNo KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtFcuFuFuNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFcuFuFuNo.KeyPress

        Try

            e.Handled = gChkInputKeyFuNo(sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： txtPortNo KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtFcuFuPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFcuFuPortNo.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： txtTerminalNo KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtTerminalNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFcuFuTerminalNo.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力チェック"

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No. フォーマット
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtChNo.Validated

        Try

            If IsNumeric(txtChNo.Text) Then

                txtChNo.Text = Integer.Parse(txtChNo.Text).ToString("0000")

                'OutChInfo表示
                Dim strRet As String = ""
                Dim strSplit() As String = Nothing
                strRet = fnGetCHdata(txtChNo.Text)
                strSplit = strRet.Split(vbTab)
                If strSplit.Length >= 5 Then
                    grdOutChInfo(0, 0).Value = txtChNo.Text
                    grdOutChInfo(1, 0).Value = strSplit(0)
                    grdOutChInfo(2, 0).Value = strSplit(1)
                    grdOutChInfo(3, 0).Value = strSplit(2)
                    grdOutChInfo(4, 0).Value = strSplit(3)
                    grdOutChInfo(5, 0).Value = strSplit(4)
                Else
                    grdOutChInfo(0, 0).Value = ""
                    grdOutChInfo(1, 0).Value = ""
                    grdOutChInfo(2, 0).Value = ""
                    grdOutChInfo(3, 0).Value = ""
                    grdOutChInfo(4, 0).Value = ""
                    grdOutChInfo(5, 0).Value = ""
                End If
            Else
                grdOutChInfo(0, 0).Value = ""
                grdOutChInfo(1, 0).Value = ""
                grdOutChInfo(2, 0).Value = ""
                grdOutChInfo(3, 0).Value = ""
                grdOutChInfo(4, 0).Value = ""
                grdOutChInfo(5, 0).Value = ""
            End If
            Call subSetTxtOut()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtFcuFuFuNo_Validated(sender As Object, e As System.EventArgs) Handles txtFcuFuFuNo.Validated
        Try
            Call subSetTxtOut()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtFcuFuPortNo_Validated(sender As Object, e As System.EventArgs) Handles txtFcuFuPortNo.Validated
        Try
            Call subSetTxtOut()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub txtFcuFuTerminalNo_Validated(sender As Object, e As System.EventArgs) Handles txtFcuFuTerminalNo.Validated
        Try
            Call subSetTxtOut()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub


    ''----------------------------------------------------------------------------
    '' 機能説明  ： CH No. 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtChNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtChNo.Validating

    '    e.Cancel = gChkTextNumSpan(0, 65535, txtChNo.Text)

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： Output Data 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtOutputData_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtChOutputData.Validating

    '    e.Cancel = gChkTextNumSpan(0, 65535, txtChOutputData.Text)

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： Output Offdelay 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtOutputOffdelay_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtChOutputOffdelay.Validating

    '    e.Cancel = gChkTextNumSpan(0, 36000, txtChOutputOffdelay.Text)

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： txtOutputTime 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtOutputTime_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFcuFuOutputTime.Validating

    '    e.Cancel = gChkTextNumSpan(0, 250, txtFcuFuOutputTime.Text)

    'End Sub


    ''----------------------------------------------------------------------------
    '' 機能説明  ： txtTerminalNo 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtFcuFuFuNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFcuFuFuNo.Validating

    '    'e.Cancel = gChkTextNumSpan(0, 20, txtFcuFuFuNo.Text)

    'End Sub

    'Private Sub txtFcuFuPortNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFcuFuPortNo.Validating

    '    e.Cancel = gChkTextNumSpan(1, 8, txtFcuFuPortNo.Text)

    'End Sub

    'Private Sub txtFcuFuTerminalNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFcuFuTerminalNo.Validating

    '    e.Cancel = gChkTextNumSpan(1, 64, txtFcuFuTerminalNo.Text)

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub grdLogic_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdLogic.CellValidating

    '    Dim dgv As DataGridView = CType(sender, DataGridView)
    '    Dim strColumnName = dgv.Columns(e.ColumnIndex).Name

    '    ''[TABLE_NO.]
    '    If strColumnName = "txtTableNo" Then
    '        e.Cancel = gChkTextNumSpan(0, 32767, e.FormattedValue)
    '    End If

    'End Sub

#End Region

#End Region

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

            Dim intMin As Integer = 0
            Dim intMax As Integer = 0
            Dim intVal As Integer = 0
            Dim intChk As Integer = 0
            Dim blnFlg As Boolean = False
            Dim strChkNum As String = ""
            Dim strlen As Integer = 0
            Dim dp As Integer = 0

            ''共通FUアドレス入力チェック
            If Not gChkInputFuAddress(txtFcuFuFuNo, txtFcuFuPortNo, txtFcuFuTerminalNo, 64, True, True) Then Return False

            ''共通数値入力チェック
            If Not gChkInputNum(txtChNo, 0, 65535, "Output CH No.", True, True) Then Return False
            If Not gChkInputNum(txtChOutputData, 0, 65535, "Output Data", True, True) Then Return False
            If Not gChkInputNum(txtChOutputOffdelay, 0, 36000, "Output Offdelay", True, True) Then Return False
            If Not gChkInputNum(txtFcuFuOutputTime, 0, 200, "One shot output time", True, True) Then Return False

            ''ロジックテーブル入力チェック
            For i As Integer = 0 To grdLogic.RowCount - 1

                If Trim(grdLogic(0, i).Value) = "" Then

                    ''項目がない場合は強制的に空白をセット
                    grdLogic(1, i).Value = ""

                Else

                    ''数値以外
                    If Not IsNumeric(Trim(grdLogic(1, i).Value)) Then
                        Call MessageBox.Show("Please input the numerical value" & vbNewLine & vbNewLine & _
                                             "[ Item ] " & Trim(grdLogic(0, i).Value) & vbNewLine & _
                                             "[ Row ] " & i + 1, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End If

                    ''CH番号だったら範囲チェックしない。　Ver1.4.6 2012.07.31 K.Tanigawa
                    If (grdLogic(2, i).Value) = True Then

                        '' CHNo.は、チェックしない。
                        Return True
                    End If


                    ''オプション項目の３番目をチェック
                    Select Case Trim(grdLogic(3, i).Value)
                        Case 1

                            ''１の場合は入力範囲チェック
                            intMin = CInt(Trim(grdLogic(4, i).Value))
                            intMax = CInt(Trim(grdLogic(5, i).Value))
                            intVal = CInt(Trim(grdLogic(1, i).Value))

                            ' ver1.4.0 2011.07.19 Soft SwitchロジックのP_2入力の場合は小数点入力可
                            ' 小数点チェック時にIntで引き渡すと四捨五入される、関数内でdoubleに変換
                            'If gChkTextNumSpan(intMin, intMax, intVal, True, "[ Item ] " & Trim(grdLogic(0, i).Value) & vbNewLine & "[ Row ] " & i + 1) Then
                            If gChkTextNumSpan(intMin, intMax, Trim(grdLogic(1, i).Value), True, "[ Item ] " & Trim(grdLogic(0, i).Value) & vbNewLine & "[ Row ] " & i + 1) Then
                                Return False
                            End If

                            ' ver1.4.0 2011.07.19 Soft SwitchロジックのP_2入力の場合は小数点入力可
                            ' ver1.4.7 2012.08.03 K.Tanigawa チェックボックスがTrueの場合はCHNo.なので小数点不可
                            If mudtSequenceSetDetail.shtLogicType = 26 And i = 0 And grdLogic(2, i).Value <> True Then
                                strlen = Len(Trim(grdLogic(1, i).Value))
                                dp = InStr(Trim(grdLogic(1, i).Value), ".")
                                If dp > 0 Then
                                    If Len(Mid(grdLogic(1, i).Value, dp + 1, strlen - dp)) > 1 Then
                                        Call MessageBox.Show("Please set number among." & vbNewLine & vbNewLine & "[ Item ] " & Trim(grdLogic(0, i).Value) & vbNewLine & "[ Row ] " & i + 1, _
                                                "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Return False
                                    End If
                                End If

                            End If

                        Case 2

                            ''２の場合は入力固定値チェック
                            blnFlg = False
                            For j As Integer = 4 To (grdLogic.ColumnCount - 1) - 1 ''最後の -1 は一番後ろにあるCodeの分

                                ''指定文字が無くなったらチェック終了
                                If Trim(grdLogic(j, i).Value) = "" Then
                                    Exit For
                                Else

                                    ''指定文字かチェック
                                    intVal = CInt(Trim(grdLogic(1, i).Value))
                                    intChk = CInt(Trim(grdLogic(j, i).Value))

                                    If intVal = intChk Then
                                        blnFlg = True
                                        Exit For
                                    End If

                                    strChkNum &= intChk & " or "

                                End If

                            Next

                            ''全て一致しなかった場合
                            If Not blnFlg Then
                                Call MessageBox.Show("Please input the " & Mid(strChkNum, 1, strChkNum.Length - 4) & vbNewLine & vbNewLine & _
                                                     "[ Item ] " & Trim(grdLogic(0, i).Value) & vbNewLine & _
                                                     "[ Row ] " & i + 1, "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return False
                            End If

                    End Select

                End If

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#Region "設定値格納"

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) シーケンス設定詳細構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetSeqSetRec)

        Try

            With udtSet

                ''シーケンスID
                .shtId = CCShort(txtSeqID.Text)

                '===================
                ''LogicSetフレーム
                '===================
                ''ロジックタイプ
                ''↑はSelectボタンクリックイベントで構造体に設定済み

                ''演算参照テーブル
                For i As Integer = 0 To grdLogic.Rows.Count - 1
                    ' ver1.4.0 2011.07.19 Soft SwitchロジックのP_2入力の場合は小数点入力可
                    ' ver1.4.7 2012.08.03 K.Tanigawa チェックボックスがTrueの場合はCHNo.なので小数点不可
                    If (mudtSequenceSetDetail.shtLogicType = 26 And i = 0 And grdLogic(2, i).Value <> True) Then
                        .shtLogicItem(i) = CCUInt16(Trim(grdLogic(1, i).Value) * 10)
                    Else
                        .shtLogicItem(i) = CCUInt16(Trim(grdLogic(1, i).Value))
                    End If

                    .shtUseCh(i) = IIf(grdLogic(2, i).Value, 1, 0)
                Next

                '===================
                ''CH Outputフレーム
                '===================
                .shtOutChid = CCUInt16(Trim(txtChNo.Text))

                If optIOSelInput.Checked Then
                    .bytOutIoSelect = 1
                ElseIf optIOSelOutput.Checked Then
                    .bytOutIoSelect = 2
                Else
                    .bytOutIoSelect = 0
                End If

                .bytOutStatus = cmbChStatus.SelectedValue
                '.shtOutData = CCShort(Trim(txtChOutputData.Text))
                .shtOutData = BitConverter.ToInt16(BitConverter.GetBytes(CCUInt32(txtChOutputData.Text)), 0)

                .bytOutDataType = cmbChOutputType.SelectedValue

                '.shtOutDelay = CCShort(Trim(txtChOutputOffdelay.Text))
                .shtOutDelay = BitConverter.ToInt16(BitConverter.GetBytes(CCUInt32(txtChOutputOffdelay.Text)), 0)

                .bytOutInv = IIf(optChNonInvert.Checked, 0, 1)

                '===================
                ''OutputFcuFuフレーム
                '===================
                .bytFuno = gGetFuNo(txtFcuFuFuNo.Text, True)
                .bytPort = IIf(CCbyte(Trim(txtFcuFuPortNo.Text)) = 0, gCstCodeChNotSetFuPortByte, CCbyte(Trim(txtFcuFuPortNo.Text)))
                .bytPin = IIf(CCbyte(Trim(txtFcuFuTerminalNo.Text)) = 0, gCstCodeChNotSetFuPinByte, CCbyte(Trim(txtFcuFuTerminalNo.Text)))
                .bytOutType = cmbFcuFuOutputType.SelectedValue
                .bytOneShot = CCbyte(Trim(txtFcuFuOutputTime.Text))

                '===================
                ''Contineフレーム
                '===================
                .bytContine = IIf(optContinuance.Checked, 1, 0)

                '===================
                ''InputSetフレーム
                '===================
                For i As Integer = LBound(udtSet.udtInput) To UBound(udtSet.udtInput)

                    With .udtInput(i)

                        .shtChid = IIf(Trim(grdCH(1, i).Value) = "", 0, Trim(grdCH(1, i).Value))
                        'STATUSはHEXで入力されている
                        '.bytStatus = IIf(Trim(grdCH(2, i).Value) = "", 0, Trim(grdCH(2, i).Value))
                        If Trim(grdCH(2, i).Value) = "" Then
                            .bytStatus = 0
                        Else
                            .bytStatus = Convert.ToInt32(Trim(grdCH(2, i).Value), 16)
                        End If

                        'TYPEは文字列で入力されているため変換
                        '.bytType = IIf(Trim(grdCH(3, i).Value) = "", 0, Trim(grdCH(3, i).Value))
                        If Trim(grdCH(3, i).Value) = "" Then
                            .bytType = 0
                        Else
                            .bytType = mGetTypeCode(Trim(grdCH(3, i).Value))
                        End If

                        'MASKは4桁HEXで入力されている
                        '.shtMask = IIf(Trim(grdCH(4, i).Value) = "", 0, Trim(grdCH(3, i).Value))
                        If Trim(grdCH(4, i).Value) = "" Then
                            .shtMask = 0
                        Else
                            .shtMask = Convert.ToInt32(Trim(grdCH(4, i).Value), 16)
                        End If

                        'StatusとChSelectが不一致の場合
                        'ChSelectにマニュアルを格納する
                        Select Case .bytStatus And &HF0
                            Case &H0
                                '完全に未設定とする
                                .shtChSelect = 0
                            Case &H10
                                'DATA
                                If .shtChSelect <> gCstCodeSeqChSelectData Then
                                    .shtChSelect = gCstCodeSeqChSelectManual
                                End If
                            Case &H20
                                'ALARM
                                If .shtChSelect <> gCstCodeSeqChSelectAnalog Then
                                    .shtChSelect = gCstCodeSeqChSelectManual
                                End If
                            Case &H30
                                'CalcInput
                                If .shtChSelect <> gCstCodeSeqChSelectCalc Then
                                    .shtChSelect = gCstCodeSeqChSelectManual
                                End If
                            Case &H40
                                'ExtGroupInput
                                If .shtChSelect <> gCstCodeSeqChSelectExtGroup Then
                                    .shtChSelect = gCstCodeSeqChSelectManual
                                End If
                            Case Else
                                'ManualInput
                                If .shtChSelect <> gCstCodeSeqChSelectManual Then
                                    .shtChSelect = gCstCodeSeqChSelectManual
                                End If
                        End Select
                    End With
                Next

                '===================
                ''Remarksフレーム
                '===================
                .strRemarks = txtRemarks.Text

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "設定値表示"

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) シーケンス設定詳細構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetSeqSetRec)

        Try

            Dim udtSequenceLogicSub() As gTypCodeName = Nothing
            Dim intvar As UInt16 = 0

            With udtSet

                ''シーケンスID
                txtSeqID.Text = .shtId

                '===================
                ''LogicSetフレーム
                '===================
                ''ロジック名
                txtLogic.Text = gGetComboItemName(.shtLogicType, gEnmComboType.ctSeqSetDetailLogic)

                If .shtLogicType <> 0 Then

                    ''シーケンスロジックサブ設定取得
                    Call gGetComboCodeName(udtSequenceLogicSub, gEnmComboType.ctSeqSetDetailLogic, Format(.shtLogicType, "00"))

                    ''サブ情報表示
                    For i As Integer = 0 To UBound(udtSequenceLogicSub)
                        grdLogic(0, i).Value = udtSequenceLogicSub(i).strName
                        grdLogic(3, i).Value = udtSequenceLogicSub(i).strOption1
                        grdLogic(4, i).Value = udtSequenceLogicSub(i).strOption2
                        grdLogic(5, i).Value = udtSequenceLogicSub(i).strOption3
                        grdLogic(6, i).Value = udtSequenceLogicSub(i).strOption4
                        grdLogic(7, i).Value = udtSequenceLogicSub(i).strOption5
                        grdLogic(8, i).Value = udtSequenceLogicSub(i).shtCode
                    Next

                    ''演算参照テーブル
                    For i As Integer = 0 To grdLogic.Rows.Count - 1
                        ' ver1.4.0 2011.07.19 Soft SwitchロジックのP_2入力の場合は小数点入力可
                        ' ver1.4.7 2012.08.03 K.Tanigawa チェックボックスがTrueの場合はCHNo.なので小数点不可

                        If .shtLogicType = 26 And i = 0 And IIf(.shtUseCh(i) = 0, False, True) <> True Then
                            intvar = IIf(.shtLogicItem(i) = 0, IIf(Trim(grdLogic(0, i).Value) = "", "", .shtLogicItem(i)), .shtLogicItem(i))
                            grdLogic(1, i).Value = (intvar / 10).ToString("0.0")
                        Else
                            grdLogic(1, i).Value = IIf(.shtLogicItem(i) = 0, IIf(Trim(grdLogic(0, i).Value) = "", "", .shtLogicItem(i)), .shtLogicItem(i))
                        End If

                        grdLogic(2, i).Value = IIf(.shtUseCh(i) = 0, False, True)
                    Next


                End If

                '===================
                ''CH Outputフレーム
                '===================
                txtChNo.Text = gConvZeroToNull(.shtOutChid, "0000")
                'OutChInfo表示
                Dim strRet As String = ""
                Dim strSplit() As String = Nothing
                strRet = fnGetCHdata(txtChNo.Text)
                strSplit = strRet.Split(vbTab)
                If strSplit.Length >= 5 Then
                    grdOutChInfo(0, 0).Value = txtChNo.Text
                    grdOutChInfo(1, 0).Value = strSplit(0)
                    grdOutChInfo(2, 0).Value = strSplit(1)
                    grdOutChInfo(3, 0).Value = strSplit(2)
                    grdOutChInfo(4, 0).Value = strSplit(3)
                    grdOutChInfo(5, 0).Value = strSplit(4)
                Else
                    grdOutChInfo(0, 0).Value = ""
                    grdOutChInfo(1, 0).Value = ""
                    grdOutChInfo(2, 0).Value = ""
                    grdOutChInfo(3, 0).Value = ""
                    grdOutChInfo(4, 0).Value = ""
                    grdOutChInfo(5, 0).Value = ""
                End If
                Call subSetTxtOut()


                Select Case .bytOutIoSelect
                    Case 0 ''設定なし
                        optIOSelInput.Checked = False
                        optIOSelOutput.Checked = False
                    Case 1 ''入力側
                        optIOSelInput.Checked = True
                        optIOSelOutput.Checked = False
                    Case 2 ''出力側
                        optIOSelInput.Checked = False
                        optIOSelOutput.Checked = True
                End Select

                cmbChStatus.SelectedValue = .bytOutStatus
                txtChOutputData.Text = gGet2Byte(.shtOutData)
                'txtChOutputData.Text = .shtOutData

                cmbChOutputType.SelectedValue = .bytOutDataType

                txtChOutputOffdelay.Text = gGet2Byte(.shtOutDelay)
                'txtChOutputOffdelay.Text = .shtOutDelay

                optChNonInvert.Checked = IIf(.bytOutInv = 0, True, False)
                optChInvert.Checked = IIf(.bytOutInv = 1, True, False)

                'Ver2.0.7.X
                'optIOSelInput
                Select Case .shtLogicType
                    Case 29, 30, 31, 32
                        'Logic Valve,Motorは、Outputを認めるが
                        ' それ以外はInput Onlu
                        optIOSelOutput.Enabled = True
                    Case Else
                        optIOSelInput.Checked = True
                        optIOSelOutput.Enabled = False
                End Select
                '-


                '===================
                ''OutputFcuFuフレーム
                '===================
                txtFcuFuFuNo.Text = gGetFuName2(.bytFuno)
                txtFcuFuPortNo.Text = IIf(.bytPort = gCstCodeChNotSetFuPortByte, "", .bytPort)
                txtFcuFuTerminalNo.Text = IIf(.bytPin = gCstCodeChNotSetFuPinByte, "", .bytPin)
                cmbFcuFuOutputType.SelectedValue = .bytOutType
                txtFcuFuOutputTime.Text = .bytOneShot

                '===================
                ''Contineフレーム
                '===================
                Select Case .bytContine
                    Case 0
                        optContinuance.Checked = False
                        optDiscontinuance.Checked = True
                    Case 1
                        optContinuance.Checked = True
                        optDiscontinuance.Checked = False
                End Select

                '===================
                ''InputSetフレーム
                '===================
                For i As Integer = LBound(udtSet.udtInput) To UBound(udtSet.udtInput)

                    ''InputCH情報表示
                    Call mDispInputInfo(i, udtSet.udtInput(i))

                Next

                '===================
                ''Remarksフレーム
                '===================
                txtRemarks.Text = .strRemarks


                '入力制御
                Call subInpCTRL(.shtLogicType)
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#Region "入力制御"
    '入力制御
    Private Sub subInpCTRL(pintLogic As Integer)
        Try
            Select Case pintLogic
                Case 1 'digital AND
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 2 'digital OR
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 3 'digital D Latch
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 4 'digital AND-OR
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 5 'digital OR-AND
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 6 'digital AND-AND-OR
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 7 'analog through
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(1)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 8 'analog gate(ON/OFF)
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(2)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 9 'analog gate(an output data change at the Gate OFF)
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(2)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 10 'analog multiplexer
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(3)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 11 'average logic
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(3)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 12 'time subtraction(1input)
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(1)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 13 'time subtraction(2input)
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(2)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 14 'conditional addition
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 15 'linear table logic
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(1)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = True
                    cmdOperation.Visible = False
                Case 16 'calculate logic
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = True
                Case 17 'data comparison
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(2)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 18 'event timer
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(4)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 19 'logic pulse count
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(1)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 20 'Pulse count
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 21 'calculate Running hour
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 22 'Clear Running hour
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(1)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 23 'Save Date
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(1)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 24 'Save Time
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(1)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = False
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 25 'Pulse double lap
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(1)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 26 'Soft Switch
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(3)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 27 'Position Control
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 28 'Integer Calculate Logic
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(8)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = True
                Case 29 'Valve(AI-DO)
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(2)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 30 'Valve(AI-AO)
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(2)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 31 'Valve(DI-DO)
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(0)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
                Case 32 'Motor(Input-Output)
                    '>>>InpCH 入力可能な件数指定
                    Call subSetGRDCH(0)
                    '>>>Logic GRID,Liner,Opera
                    grdLogic.Visible = True
                    cmdLinear.Visible = False
                    cmdOperation.Visible = False
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    'InpCHの入力制御
    Private Sub subSetGRDCH(pintYukoSu As Integer)
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0

            '全点無効にする
            For i = 0 To grdCH.RowCount - 1 Step 1
                For j = 1 To grdCH.ColumnCount - 1 Step 1
                    grdCH(j, i).ReadOnly = True
                    grdCH(j, i).Style.BackColor = gColorGridRowBackReadOnly
                Next j
            Next i

            '有効件数(引数)分有効にする
            For i = 0 To pintYukoSu - 1 Step 1
                For j = 1 To grdCH.ColumnCount - 1 Step 1
                    grdCH(j, i).ReadOnly = False
                    grdCH(j, i).Style.BackColor = Nothing
                Next j
            Next i
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))

        End Try
    End Sub
#End Region

    Private Sub mDispInputInfo(ByVal intRowIndex As Integer, ByVal udtSequenceSetInput As gTypSetSeqSetRecInput)

        Try

            With udtSequenceSetInput

                If .shtChid = 0 Then

                    ''未設定の場合は空白表示
                    grdCH(1, intRowIndex).Value = ""
                    grdCH(2, intRowIndex).Value = ""
                    grdCH(3, intRowIndex).Value = ""
                    grdCH(4, intRowIndex).Value = ""
                    '
                    grdInChInfo(1, intRowIndex).Value = ""
                    grdInChInfo(2, intRowIndex).Value = ""
                    grdInChInfo(3, intRowIndex).Value = ""
                    grdInChInfo(4, intRowIndex).Value = ""
                    grdInChInfo(5, intRowIndex).Value = ""
                    grdInChInfo(6, intRowIndex).Value = ""

                    Call subSetTxtInCh(intRowIndex, "")
                Else

                    grdCH(1, intRowIndex).Value = IIf(.shtChid = 0, "", Format(.shtChid, "0000"))
                    grdCH(2, intRowIndex).Value = Microsoft.VisualBasic.Right("0000" & Hex(.bytStatus), 2)
                    grdCH(3, intRowIndex).Value = mGetTypeName(.bytType)
                    grdCH(4, intRowIndex).Value = Microsoft.VisualBasic.Right("0000" & Hex(.shtMask), 4)

                    'InChInfo表示
                    Dim strRet As String = ""
                    Dim strSplit() As String = Nothing
                    strRet = fnGetCHdata(grdCH(1, intRowIndex).Value)
                    strSplit = strRet.Split(vbTab)
                    If strSplit.Length >= 5 Then
                        grdInChInfo(1, intRowIndex).Value = grdCH(1, intRowIndex).Value
                        grdInChInfo(2, intRowIndex).Value = strSplit(0)
                        grdInChInfo(3, intRowIndex).Value = strSplit(1)
                        grdInChInfo(4, intRowIndex).Value = strSplit(2)
                        grdInChInfo(5, intRowIndex).Value = strSplit(3)
                        grdInChInfo(6, intRowIndex).Value = strSplit(4)

                        Call subSetTxtInCh(intRowIndex, grdCH(1, intRowIndex).Value)
                    Else
                        grdInChInfo(1, intRowIndex).Value = ""
                        grdInChInfo(2, intRowIndex).Value = ""
                        grdInChInfo(3, intRowIndex).Value = ""
                        grdInChInfo(4, intRowIndex).Value = ""
                        grdInChInfo(5, intRowIndex).Value = ""
                        grdInChInfo(6, intRowIndex).Value = ""

                        Call subSetTxtInCh(intRowIndex, "")
                    End If
                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub subSetTxtInCh(pintIdx As Integer, pstrData As String)
        Try
            Select Case pintIdx
                Case 0
                    txtInCH01.Text = pstrData
                Case 1
                    txtInCh02.Text = pstrData
                Case 2
                    txtInCh03.Text = pstrData
                Case 3
                    txtInCh04.Text = pstrData
                Case 4
                    txtInCh05.Text = pstrData
                Case 5
                    txtInCh06.Text = pstrData
                Case 6
                    txtInCh07.Text = pstrData
                Case 7
                    txtInCh08.Text = pstrData
            End Select
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub subSetTxtOut()
        Try
            'OutCH
            txtOutCh.Text = txtChNo.Text
            'FU
            Dim strFu As String = txtFcuFuFuNo.Text.Trim
            Dim strPort As String = txtFcuFuPortNo.Text.Trim
            Dim strTer As String = txtFcuFuTerminalNo.Text.Trim
            Dim strOut As String = ""
            '3つとも空白なら空白
            If strFu = "" And strPort = "" And strTer = "" Then
                strOut = ""
            Else
                strOut = strFu & "-" & strPort & "-" & strTer
            End If

            txtOutFU.Text = strOut

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Function mGetTypeName(ByVal intType As Integer) As String

        Select Case intType
            Case gCstCodeSeqInputTypeNonInvert
                Return "non invert"
            Case gCstCodeSeqInputTypeInvert
                Return "invert"
            Case gCstCodeSeqInputTypeOneShot
                Return "one shot"
        End Select

        Return "???"

    End Function
    'mGetTypeNameの逆
    Private Function mGetTypeCode(ByVal pstrType As String) As Integer

        Select Case pstrType
            Case "non invert"
                Return gCstCodeSeqInputTypeNonInvert
            Case "invert"
                Return gCstCodeSeqInputTypeInvert
            Case "one shot"
                Return gCstCodeSeqInputTypeOneShot
        End Select

        Return 0

    End Function

#End Region

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtItem"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtTableNo"
            Dim Column3 As New DataGridViewCheckBoxColumn : Column3.Name = "chkUse"
            Dim Column4 As New DataGridViewTextBoxColumn : Column4.Name = "txtOption1"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtOption2"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtOption3"
            Dim Column7 As New DataGridViewTextBoxColumn : Column7.Name = "txtOption4"
            Dim Column8 As New DataGridViewTextBoxColumn : Column8.Name = "txtOption5"
            Dim Column9 As New DataGridViewTextBoxColumn : Column9.Name = "txtCode"

            Column1.ReadOnly = True
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            With grdLogic

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3)
                .Columns.Add(Column4) : .Columns.Add(Column5) : .Columns.Add(Column6) : .Columns.Add(Column7) : .Columns.Add(Column8) : .Columns.Add(Column9)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).Width = 148
                .Columns(1).Width = 90
                .Columns(2).Width = 90

                .Columns(3).Visible = False
                .Columns(4).Visible = False
                .Columns(5).Visible = False
                .Columns(6).Visible = False
                .Columns(7).Visible = False
                .Columns(8).Visible = False

                .ColumnHeadersVisible = False

                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 6
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                ''行ヘッダー
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersWidth = 50
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtItem").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtOption1").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtOption2").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtOption3").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtOption4").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtOption5").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtCode").Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdLogic)

            End With

            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtNo"
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtChNo"
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtStatus"
            Dim Column13 As New DataGridViewComboBoxColumn : Column13.Name = "txtInputType" : Column13.FlatStyle = FlatStyle.Popup
            Dim Column14 As New DataGridViewTextBoxColumn : Column14.Name = "txtInputMask"

            With grdCH

                ''列
                .Columns.Clear()
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                Column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column12.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column13.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column14.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column10.ReadOnly = True

                ''列ヘッダー
                .Columns(0).HeaderText = "No." : .Columns(0).Width = 40
                .Columns(1).HeaderText = "Data" : .Columns(1).Width = 80
                .Columns(2).HeaderText = "Status(Hex)" : .Columns(2).Width = 80
                .Columns(3).HeaderText = "Input Type" : .Columns(3).Width = 100
                .Columns(4).HeaderText = "Input Mask(Hex)" : .Columns(4).Width = 105
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 9
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                For i = 1 To .Rows.Count
                    .Rows(i - 1).Cells(0).Value = i.ToString("00")
                Next

                ''行ヘッダー
                .RowHeadersVisible = False

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtNo").Style.BackColor = gColorGridRowBackReadOnly
                    '.Rows(i).Cells("txtChNo").Style.BackColor = gColorGridRowBackReadOnly
                    '.Rows(i).Cells("txtStatus").Style.BackColor = gColorGridRowBackReadOnly
                    '.Rows(i).Cells("txtInputType").Style.BackColor = gColorGridRowBackReadOnly
                    '.Rows(i).Cells("txtInputMask").Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdCH)

                '.ReadOnly = True

                'txtInputTypeコンボ 初期設定
                'コンボは、iniﾌｧｲﾙではなく自作とする
                With Column13
                    .Items.Add("non invert")
                    .Items.Add("invert")
                    .Items.Add("one shot")
                    .Items.Add("")
                End With

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'InChInfoグリッドを初期化する
    Private Sub subInitGridInChInfo()
        Try
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column00 As New DataGridViewTextBoxColumn : Column00.Name = "txtNo"
            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtChNo"
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtChName"
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtStatus"
            Dim Column13 As New DataGridViewTextBoxColumn : Column13.Name = "txtRange"
            Dim Column14 As New DataGridViewTextBoxColumn : Column14.Name = "txtUnit"
            Dim Column15 As New DataGridViewTextBoxColumn : Column15.Name = "txtINSIG"

            'CHNo,名称,STATUS,ﾚﾝｼﾞ,UNIT,INSIGの表形式とする。
            With grdInChInfo

                ''列
                .Columns.Clear()
                .Columns.Add(Column00)
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14) : .Columns.Add(Column15)
                '.AllowUserToResizeColumns = False   ''列幅の変更可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing


                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                Column00.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column12.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column13.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column14.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column15.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column00.ReadOnly = True
                Column10.ReadOnly = True
                Column11.ReadOnly = True
                Column12.ReadOnly = True
                Column13.ReadOnly = True
                Column14.ReadOnly = True
                Column15.ReadOnly = True

                '列ヘッダー
                .Columns(0).HeaderText = "No." : .Columns(0).Width = 40
                .Columns(1).HeaderText = "CHNo." : .Columns(1).Width = 40
                .Columns(2).HeaderText = "CH Name" : .Columns(2).Width = 100
                .Columns(3).HeaderText = "Status" : .Columns(3).Width = 80
                .Columns(4).HeaderText = "Range" : .Columns(4).Width = 80
                .Columns(5).HeaderText = "Unit" : .Columns(5).Width = 60
                .Columns(6).HeaderText = "INSIG" : .Columns(6).Width = 40
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング


                ''行
                .RowCount = 9
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                For i = 1 To .Rows.Count
                    .Rows(i - 1).Cells(0).Value = i.ToString("00")
                Next

                ''行ヘッダー
                .RowHeadersVisible = False

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtNo").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtChNo").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtChName").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtStatus").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtRange").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtUnit").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtINSIG").Style.BackColor = gColorGridRowBackReadOnly
                Next i

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Horizontal

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdCH)

                .ReadOnly = True

            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    'OutChInfoグリッドを初期化する
    Private Sub subInitGridOutChInfo()
        Try
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtChNo"
            Dim Column11 As New DataGridViewTextBoxColumn : Column11.Name = "txtChName"
            Dim Column12 As New DataGridViewTextBoxColumn : Column12.Name = "txtStatus"
            Dim Column13 As New DataGridViewTextBoxColumn : Column13.Name = "txtRange"
            Dim Column14 As New DataGridViewTextBoxColumn : Column14.Name = "txtUnit"
            Dim Column15 As New DataGridViewTextBoxColumn : Column15.Name = "txtINSIG"

            'CHNo,名称,STATUS,ﾚﾝｼﾞ,UNIT,INSIGの表形式とする。
            With grdOutChInfo

                ''列
                .Columns.Clear()
                .Columns.Add(Column10) : .Columns.Add(Column11) : .Columns.Add(Column12)
                .Columns.Add(Column13) : .Columns.Add(Column14) : .Columns.Add(Column15)
                '.AllowUserToResizeColumns = False   ''列幅の変更可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing


                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                Column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column12.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column13.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column14.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column15.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column10.ReadOnly = True
                Column11.ReadOnly = True
                Column12.ReadOnly = True
                Column13.ReadOnly = True
                Column14.ReadOnly = True
                Column15.ReadOnly = True

                '列ヘッダー
                .Columns(0).HeaderText = "CHNo." : .Columns(0).Width = 40
                .Columns(1).HeaderText = "CH Name" : .Columns(1).Width = 100
                .Columns(2).HeaderText = "Status" : .Columns(2).Width = 80
                .Columns(3).HeaderText = "Range" : .Columns(3).Width = 80
                .Columns(4).HeaderText = "Unit" : .Columns(4).Width = 60
                .Columns(5).HeaderText = "INSIG" : .Columns(5).Width = 40
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング


                ''行
                .RowCount = 2
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                ''行ヘッダー
                .RowHeadersVisible = False

                ''偶数行の背景色を変える
                cellStyle.BackColor = gColorGridRowBack
                For i = 0 To .Rows.Count - 1
                    If i Mod 2 <> 0 Then
                        .Rows(i).DefaultCellStyle = cellStyle
                    End If
                Next

                ''ReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtChNo").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtChName").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtStatus").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtRange").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtUnit").Style.BackColor = gColorGridRowBackReadOnly
                    .Rows(i).Cells("txtINSIG").Style.BackColor = gColorGridRowBackReadOnly
                Next i

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Horizontal

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdCH)

                .ReadOnly = True

            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Logic画像表示処理
    Private Sub subSetLogicGazo(pintLogic As Integer)
        Try
            Select Case pintLogic
                Case 0
                    picLogic.Image = Nothing
                Case 1
                    picLogic.Image = My.Resources.L01
                Case 2
                    picLogic.Image = My.Resources.L02
                Case 3
                    picLogic.Image = My.Resources.L03
                Case 4
                    picLogic.Image = My.Resources.L04
                Case 5
                    picLogic.Image = My.Resources.L05
                Case 6
                    picLogic.Image = My.Resources.L06
                Case 7
                    picLogic.Image = My.Resources.L07
                Case 8
                    picLogic.Image = My.Resources.L08
                Case 9
                    picLogic.Image = My.Resources.L09
                Case 10
                    picLogic.Image = My.Resources.L10
                Case 11
                    picLogic.Image = My.Resources.L11
                Case 12
                    picLogic.Image = My.Resources.L12
                Case 13
                    picLogic.Image = My.Resources.L13
                Case 14
                    picLogic.Image = My.Resources.L14
                Case 15
                    picLogic.Image = My.Resources.L15
                Case 16
                    picLogic.Image = My.Resources.L16
                Case 17
                    picLogic.Image = My.Resources.L17
                Case 18
                    picLogic.Image = My.Resources.L18
                Case 19
                    picLogic.Image = My.Resources.L19
                Case 20
                    picLogic.Image = My.Resources.L20
                Case 21
                    picLogic.Image = My.Resources.L21
                Case 22
                    picLogic.Image = My.Resources.L22
                Case 23
                    picLogic.Image = My.Resources.L23
                Case 24
                    picLogic.Image = My.Resources.L24
                Case 25
                    picLogic.Image = My.Resources.L25
                Case 26
                    picLogic.Image = My.Resources.L26
                Case 27
                    picLogic.Image = My.Resources.L27
                Case 28
                    picLogic.Image = My.Resources.L28
                Case 29
                    picLogic.Image = My.Resources.L29
                Case 30
                    picLogic.Image = My.Resources.L30
                Case 31
                    picLogic.Image = My.Resources.L31
                Case 32
                    picLogic.Image = My.Resources.L32
                Case 33
                    picLogic.Image = My.Resources.L33
                Case 34
                    picLogic.Image = My.Resources.L34
                Case 35
                    picLogic.Image = My.Resources.L35
                Case 36
                    picLogic.Image = My.Resources.L36
                Case 37
                    picLogic.Image = My.Resources.L37
                Case 38
                    picLogic.Image = My.Resources.L38
                Case 39
                    picLogic.Image = My.Resources.L39
                Case 40
                    picLogic.Image = My.Resources.L40
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'CHnoを元にデータを戻す
    '戻り値は各項目をタブ区切りで戻す
    '　戻り値＝名称,STATUS,ﾚﾝｼﾞ,UNIT,INSIGの表形式とする。
    Private Function fnGetCHdata(pstrCHNo As String) As String
        Dim strRet As String = ""

        'CHデータ
        Dim CHStr As mChannelStr
        '配列初期化
        Erase CHStr.AlmInf
        ReDim CHStr.AlmInf(9)
        '初期化
        CHStr.SYSNo = "" : CHStr.CHNo = "" : CHStr.CHItem = ""
        CHStr.Status = "" : CHStr.Range = "" : CHStr.Unit = ""
        For k As Integer = 0 To 9
            CHStr.AlmInf(k).Value = ""
            CHStr.AlmInf(k).ExtGrp = ""
            CHStr.AlmInf(k).Delay = ""
            CHStr.AlmInf(k).GrpRep1 = ""
            CHStr.AlmInf(k).GrpRep2 = ""
        Next
        CHStr.INSIG = "" : CHStr.SIGType = "" : CHStr.OUTSIG = ""
        CHStr.INAdd = "" : CHStr.OUTAdd = "" : CHStr.AL = ""
        CHStr.RL = "" : CHStr.ShareType = "" : CHStr.ShareCHNo = ""
        CHStr.Remarks = "" : CHStr.AlmLevel = "" : CHStr.CHNo_temp = ""
        CHStr.OUT = ""

        Dim strCHname As String = ""
        Dim strStatus As String = ""
        Dim strRange As String = ""
        Dim strUnit As String = ""
        Dim strINSIG As String = ""

        Try
            Dim idx As Integer = 0
            idx = praryCHLIST.IndexOf(pstrCHNo)
            If idx < 0 Then
                '該当CHnoが存在しないなら空白で戻す
                Return strRet
            End If

            'CH表示文字列取得
            Call mMakeDrawCHData(gudt.SetChInfo.udtChannel(idx), CHStr)
            '>>>CH名称
            strCHname = CHStr.CHItem
            '>>>STATUS
            strStatus = CHStr.Status
            '>>>RANGE
            strRange = CHStr.Range
            '>>>UNIT
            strUnit = CHStr.Unit
            '>>>INSIG
            strINSIG = CHStr.INSIG

            '印刷用にタブ区切り格納
            strRet = ""
            strRet = strRet & strCHname & vbTab
            strRet = strRet & strStatus & vbTab
            strRet = strRet & strRange & vbTab
            strRet = strRet & strUnit & vbTab
            strRet = strRet & strINSIG

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRet
    End Function
#Region "計測点データ取得関数"
    '--------------------------------------------------------------------
    ' 機能      : CH表示データ作成
    ' 返り値    : 文字列
    ' 引き数    : ARG1 - (I ) CHデータ
    ' 機能説明  : NULLなどの不要な情報を取り除いた文字列を返す
    ' 履歴      : 計測点印刷機能からの移植とカスタマイズ
    '--------------------------------------------------------------------
    Private Sub mMakeDrawCHData(ByVal udtChannel As gTypSetChRec, ByRef CHStr As mChannelStr)

        Try
            Dim strTemp As String = ""
            Dim intSpace As Integer = 0
            Dim intDecimalP As Integer = 0
            Dim strDecimalFormat As String = ""
            Dim dblValue As Double = 0
            Dim dblLowValue As Double = 0
            Dim dblHiValue As Double = 0
            Dim strHH As String = ""
            Dim strH As String = ""
            Dim strL As String = ""
            Dim strLL As String = ""
            Dim intLen As Integer = 0
            Dim intCompIndex As Integer = 0
            Dim intStatusExist As Integer = 0
            Dim nLen As Integer = 0     '' Vver1.8.1 2015.11.18
            Dim nCenter As Integer = 1  '' Ver1.10.1 2016.02.29 力率表示対応

            ''初期化
            CHStr.SYSNo = "" : CHStr.CHNo = "" : CHStr.CHItem = ""
            CHStr.Status = "" : CHStr.Range = "" : CHStr.Unit = ""
            For k As Integer = 0 To 9
                CHStr.AlmInf(k).Value = ""
                CHStr.AlmInf(k).ExtGrp = ""
                CHStr.AlmInf(k).Delay = ""
                CHStr.AlmInf(k).GrpRep1 = ""
                CHStr.AlmInf(k).GrpRep2 = ""
            Next
            CHStr.INSIG = "" : CHStr.SIGType = "" : CHStr.OUTSIG = ""
            CHStr.INAdd = "" : CHStr.OUTAdd = "" : CHStr.AL = ""
            CHStr.RL = "" : CHStr.ShareType = "" : CHStr.ShareCHNo = ""
            CHStr.Remarks = ""
            CHStr.AlmLevel = ""     '' Ver1.7.8 2015.11.12 　ﾛｲﾄﾞ対応表示追加
            CHStr.CHNo_temp = ""    '' Ver1.8.5.2  2015.12.02  ﾀｸﾞ表示時補助用変数追加
            CHStr.TermCount = 0     '' Ver1.11.9.3 2016.11.26 追加
            CHStr.OUT = ""          '' Ver2.0.0.4 Output設定がある場合「o」表記

            With udtChannel

                If .udtChCommon.shtChno = 0 Then    ''CH番号無し
                    Return
                End If

                ''☆Common---------------------------------------------------------------------------------------
                ''SYS
                CHStr.SYSNo = .udtChCommon.shtSysno.ToString.Trim

                ''CHNo
                CHStr.CHNo = gGet2Byte(.udtChCommon.shtChno).ToString("0000")
                CHStr.CHNo_temp = CHStr.CHNo    '' Ver1.8.5.2  2015.12.02 ﾀｸﾞ表示時補助用変数追加

                ''ITEM NAME
                ''アイテム名称取得
                CHStr.CHItem = gGetString(.udtChCommon.strChitem)


                ''UNIT
                If .udtChCommon.shtUnit <> gCstCodeChManualInputUnit Then
                    'Ver2.0.1.6 ■ﾃﾞｼﾞﾀﾙとモーターは「*」は表示無し
                    Select Case .udtChCommon.shtChType
                        Case gCstCodeChTypeDigital, gCstCodeChTypeMotor
                            If .udtChCommon.shtUnit = 0 Then
                                '「*」
                                CHStr.Unit = ""
                            Else
                                Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
                                cmbUnit.SelectedValue = .udtChCommon.shtUnit.ToString
                                CHStr.Unit = cmbUnit.Text
                            End If
                        Case Else
                            '上記以外は「*」を表示
                            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
                            cmbUnit.SelectedValue = .udtChCommon.shtUnit.ToString
                            CHStr.Unit = cmbUnit.Text
                    End Select
                Else
                    CHStr.Unit = gGetString(.udtChCommon.strUnit)         ''特殊コード対応
                End If
                ''Cを℃に変換
                If CHStr.Unit = "C" Then
                    CHStr.Unit = "ﾟC"
                End If

                ''FU ADDRESS(IN)
                'CHStr.INAdd = mPrtConvFuAddress(.udtChCommon.shtFuno, .udtChCommon.shtPortno, .udtChCommon.shtPin)

                ' 2015.09.15 M.Kaihara
                ' FUアドレスが不定値の際、リスト印字すると65535の数値が表示（印字）される不具合を修正。
                ' FUアドレス不定値(0xFFFF)の際はFUアドレス表示文字を消す。
                If .udtChCommon.shtPortno = &HFFFF Or .udtChCommon.shtPin = &HFFFF Then
                    CHStr.INAdd = ""
                End If

                ''RL(ログ印字)
                CHStr.RL = IIf(gBitCheck(.udtChCommon.shtFlag2, 0), "o", "")    ''RL

                ''REMARKS
                CHStr.Remarks = gGetString(.udtChCommon.strRemark)

                ' 2015.10.16 ﾀｸﾞ表示向け　強制的に差し替え
                ' 2015.10.22 Ver1.7.5 標準時とﾀｸﾞ表示時にて分ける
                If gudt.SetSystem.udtSysOps.shtTagMode <> 0 Then     ' CH表示でない場合
                    CHStr.CHNo = GetTagNo(udtChannel)
                End If
                '//

                ' 2015.11.12 Ver1.7.8 ﾛｲﾄﾞ表示 ｱﾗｰﾑﾚﾍﾞﾙ取得
                If gudt.SetSystem.udtSysOps.shtLRMode <> 0 Then
                    CHStr.AlmLevel = GetAlmLevelName(udtChannel)
                End If
                '//

                ''Share Type ■Share対応
                If .udtChCommon.shtShareType = 1 Then
                    CHStr.ShareType = "L"
                ElseIf .udtChCommon.shtShareType = 2 Then
                    CHStr.ShareType = "R"
                ElseIf .udtChCommon.shtShareType = 3 Then
                    CHStr.ShareType = "S"
                Else
                    CHStr.ShareType = ""
                End If

                CHStr.ShareCHNo = gGet2Byte(.udtChCommon.shtShareChid).ToString("0000")




                'Ver2.0.0.7 Output設定
                'モーターとバルブは「o」しない。それ以外は出す
                Select Case .udtChCommon.shtChType  ''CH種別
                    Case gCstCodeChTypeMotor, gCstCodeChTypeValve
                    Case Else
                        CHStr.OUT = fnGetOrAnd(.udtChCommon.shtChno)
                End Select


                ''☆CH種別毎---------------------------------------------------------------------------------------
                Select Case .udtChCommon.shtChType  ''CH種別

                    Case gCstCodeChTypeAnalog       ''アナログ
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypeAnalogK
                                CHStr.INSIG = "K"
                            Case gCstCodeChDataTypeAnalog2Pt, gCstCodeChDataTypeAnalog2Jpt, _
                                 gCstCodeChDataTypeAnalog3Pt, gCstCodeChDataTypeAnalog3Jpt
                                CHStr.INSIG = "TR"
                            Case gCstCodeChDataTypeAnalog1_5v
                                CHStr.INSIG = "AI"
                            Case gCstCodeChDataTypeAnalog4_20mA
                                If .udtChCommon.shtSignal = 2 Then
                                    CHStr.INSIG = "PT"
                                Else
                                    CHStr.INSIG = "AI"
                                End If
                            Case gCstCodeChDataTypeAnalogPT4_20mA
                                CHStr.INSIG = "PT"
                            Case gCstCodeChDataTypeAnalogJacom
                                CHStr.INSIG = "AI"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM-"
                                Else
                                    CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If
                            Case gCstCodeChDataTypeAnalogJacom55
                                CHStr.INSIG = "AI"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM55-"
                                Else
                                    CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If

                            Case gCstCodeChDataTypeAnalogModbus
                                CHStr.INSIG = "AI"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypeAnalogExhAve
                                CHStr.INSIG = "MT"                      '' 2013.10.25 追加
                            Case gCstCodeChDataTypeAnalogExhRepose
                                CHStr.INSIG = "RP"                      '' 2013.10.25 追加
                            Case gCstCodeChDataTypeAnalogExtDev
                                CHStr.INSIG = "DV"                      '' 2013.10.25 追加
                            Case Else
                                '緯度、経度はAIの通信とする。Rangeの頭にN/S,E/Wをつけることで判別
                                CHStr.INSIG = "AI"
                        End Select

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''Decimal Position --------------------------------------------
                        intDecimalP = .AnalogDecimalPosition
                        strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                        ''Range -------------------------------------------------------
                        ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                        If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                            CHStr.Range = ""
                        Else
                            If .udtChCommon.shtData >= gCstCodeChDataTypeAnalog2Pt And _
                               .udtChCommon.shtData <= gCstCodeChDataTypeAnalog3Jpt Then

                                ''Range Type(2,3線式)     2014.05.19
                                'dblLowValue = .AnalogRangeLow
                                'dblHiValue = .
                                dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)
                                CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)
                            Else
                                ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)

                                dblLowValue = .AnalogRangeLow / (10 ^ intDecimalP)
                                dblHiValue = .AnalogRangeHigh / (10 ^ intDecimalP)

                                '' Ver1.10.1 2016.02.29 力率表示対応
                                '' Ver1.10.1.1 2016.03.02 比較時のかっこ漏れ
                                ''If (.udtChCommon.shtFlag1 And &H20) = &H20 Then
                                If gBitCheck(.udtChCommon.shtFlag1, 5) Then
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & nCenter.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range

                                ElseIf gBitCheck(.udtChCommon.shtFlag1, 8) Then     '' Ver1.11.9.3 2016.11.26 P/S表示対応
                                    CHStr.Range = "P/S" & dblHiValue.ToString(strDecimalFormat)

                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeAnalogLatitude Then     '' Ver1.10.6 2016.06.06 緯度追加
                                    CHStr.Range = "N/S" & dblHiValue.ToString(strDecimalFormat) 'Ver2.0.4.4 緯度はN/S (E/Wになっていた)
                                    CHStr.SIGType = "R"     '' Ver1.11.0 2016.07.11 緯度・経度CHは通信とする
                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeAnalogLongitude Then    '' Ver1.10.6 2016.06.06 経度追加
                                    CHStr.Range = "E/W" & dblHiValue.ToString(strDecimalFormat) 'Ver2.0.4.4 経度はE/W (N/Sになっていた)
                                    CHStr.SIGType = "R"     '' Ver1.11.0 2016.07.11 緯度・経度CHは通信とする

                                Else
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range
                                End If

                            End If

                            'Ver2.0.0.4
                            'グリーンマーク(ノーマルレンジ)対応
                            '設定アリの場合、「G」を付ける
                            If (.AnalogNormalHigh <> gCstCodeChAlalogNormalRangeNothingHi And .AnalogNormalHigh <> 0) Or _
                                (.AnalogNormalLow <> gCstCodeChAlalogNormalRangeNothingLo And .AnalogNormalLow <> 0) Then
                                'Ver2.0.0.6 グリーンマークは設定ONではないと印刷しない　場所は、Remarksの頭に追加とする（仮）
                                If g_bytGreenMarkPrint = 1 Then
                                    CHStr.Remarks = "G:" & CHStr.Remarks
                                End If
                            End If

                        End If

                        ''Value -------------------------------------------------------
                        If .AnalogHiHiUse = 0 Then      ''Use HH アラーム無し
                            CHStr.AlmInf(0).Value = ""
                        Else
                            dblValue = .AnalogHiHiValue / (10 ^ intDecimalP)    ''Value HH
                            CHStr.AlmInf(0).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        If .AnalogHiUse = 0 Then        ''Use H  アラーム無し
                            CHStr.AlmInf(1).Value = ""
                        Else
                            dblValue = .AnalogHiValue / (10 ^ intDecimalP)      ''Value H
                            CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        If .AnalogLoUse = 0 Then        ''Use L  アラーム無し
                            CHStr.AlmInf(2).Value = ""
                        Else
                            dblValue = .AnalogLoValue / (10 ^ intDecimalP)      ''Value L
                            CHStr.AlmInf(2).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        If .AnalogLoLoUse = 0 Then      ''Use LL アラーム無し
                            CHStr.AlmInf(3).Value = ""
                        Else
                            dblValue = .AnalogLoLoValue / (10 ^ intDecimalP)    ''Value LL
                            CHStr.AlmInf(3).Value = dblValue.ToString(strDecimalFormat)
                        End If

                        'Ver2.0.0.0 2016.12.06 ｾﾝｻｰﾌｪｲﾙ対応
                        'NotUse=空白
                        'Useだがｾﾝｻｰﾌｪｲﾙ無し=N
                        'Under=U
                        'Over=O
                        'Under,Over両方=o
                        If .AnalogSensorFailUse = 0 Then
                            CHStr.AlmInf(4).Value = ""
                        Else
                            If gBitCheck(.AnalogDisplay3, 1) And gBitCheck(.AnalogDisplay3, 2) Then
                                '両方
                                CHStr.AlmInf(4).Value = "o"
                            Else
                                If gBitCheck(.AnalogDisplay3, 1) Then
                                    'Underのみ
                                    CHStr.AlmInf(4).Value = "UDR"
                                Else
                                    If gBitCheck(.AnalogDisplay3, 2) Then
                                        'Overのみ
                                        CHStr.AlmInf(4).Value = "OVR"
                                    Else
                                        'Useだが無し
                                        CHStr.AlmInf(4).Value = "NON"
                                    End If
                                End If
                            End If
                        End If


                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(0).ExtGrp = IIf(.AnalogHiHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogHiHiExtGroup)                ''EXT.G HH
                        CHStr.AlmInf(1).ExtGrp = IIf(.AnalogHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogHiExtGroup)                    ''EXT.G H
                        CHStr.AlmInf(2).ExtGrp = IIf(.AnalogLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogLoExtGroup)                    ''EXT.G L
                        CHStr.AlmInf(3).ExtGrp = IIf(.AnalogLoLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogLoLoExtGroup)                ''EXT.G LL
                        CHStr.AlmInf(4).ExtGrp = IIf(.AnalogSensorFailExtGroup = gCstCodeChAnalogExtGroupNothing, "", .AnalogSensorFailExtGroup)    ''EXT.G SF

                        ''G Repose 1 --------------------------------------------------
                        CHStr.AlmInf(0).GrpRep1 = IIf(.AnalogHiHiGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogHiHiGroupRepose1)   ''G.Rep1 HH
                        CHStr.AlmInf(1).GrpRep1 = IIf(.AnalogHiGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogHiGroupRepose1)       ''G.Rep1 H
                        CHStr.AlmInf(2).GrpRep1 = IIf(.AnalogLoGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogLoGroupRepose1)       ''G.Rep1 L
                        CHStr.AlmInf(3).GrpRep1 = IIf(.AnalogLoLoGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing, "", .AnalogLoLoGroupRepose1)   ''G.Rep1 LL

                        ''G Repose 2 --------------------------------------------------
                        CHStr.AlmInf(0).GrpRep2 = IIf(.AnalogHiHiGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogHiHiGroupRepose2)   ''G.Rep2 HH
                        CHStr.AlmInf(1).GrpRep2 = IIf(.AnalogHiGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogHiGroupRepose2)       ''G.Rep2 H
                        CHStr.AlmInf(2).GrpRep2 = IIf(.AnalogLoGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogLoGroupRepose2)       ''G.Rep2 L
                        CHStr.AlmInf(3).GrpRep2 = IIf(.AnalogLoLoGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing, "", .AnalogLoLoGroupRepose2)   ''G.Rep2 LL

                        ''Delay -------------------------------------------------------
                        CHStr.AlmInf(0).Delay = IIf(.AnalogHiHiDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogHiHiDelay)                     ''Delay HH
                        CHStr.AlmInf(1).Delay = IIf(.AnalogHiDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogHiDelay)                         ''Delay H
                        CHStr.AlmInf(2).Delay = IIf(.AnalogLoDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogLoDelay)                         ''Delay L
                        CHStr.AlmInf(3).Delay = IIf(.AnalogLoLoDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogLoLoDelay)                     ''Delay LL
                        CHStr.AlmInf(4).Delay = IIf(.AnalogSensorFailDelay = gCstCodeChAnalogDelayTimerNothing, "", .AnalogSensorFailDelay)         ''Delay SF

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            For i As Integer = 0 To 4
                                If CHStr.AlmInf(i).Delay <> "" Then
                                    CHStr.AlmInf(i).Delay += strTemp
                                End If
                            Next
                        End If

                        ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                            cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                            CHStr.Status = cmbStatus.Text

                            '' Ver1.9.0 2015.12.16 DV CHの場合、ｽﾃｰﾀｽを変更
                            If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Then
                                If .udtChCommon.shtStatus = &H43 Then     '' LOW/NOR/HIGHならば差し替え
                                    CHStr.Status = "NOR/HIGH"
                                End If
                            End If
                        Else
                            strHH = gGetString(.AnalogHiHiStatusInput)     ''特殊コード対応
                            strH = gGetString(.AnalogHiStatusInput)        ''特殊コード対応
                            strL = gGetString(.AnalogLoStatusInput)        ''特殊コード対応
                            strLL = gGetString(.AnalogLoLoStatusInput)     ''特殊コード対応

                            '' 2015.11.18  Ver1.8.1  ｽﾃｰﾀｽは未定の場合もあるので、表示方法変更
                            If LenB(strHH) = 0 And LenB(strH) = 0 And LenB(strL) = 0 And LenB(strLL) = 0 Then
                                strTemp = ""
                            Else
                                '' Ver1.9.0 2015.12.16 DV CHの場合、ｽﾃｰﾀｽを変更
                                If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Then
                                    '' Ver1.9.8 2016.02.20 ｽﾃｰﾀｽﾁｪｯｸ方法変更
                                    If LenB(strLL) = 0 And LenB(strHH) = 0 Then     '' LLｽﾃｰﾀｽがない場合、NOR/HIGH
                                        If LenB(strH) = 0 Then
                                            strTemp = "NOR/" & strL
                                        Else
                                            strTemp = "NOR/" & strH
                                        End If
                                    Else            '' NOR/HIGH/HH
                                        If LenB(strHH) = 0 Then
                                            strTemp = "NOR/" & strL & "/" & strLL
                                        Else
                                            strTemp = "NOR/" & strH & "/" & strHH
                                        End If

                                    End If
                                Else
                                    strTemp = ""    '' Ver1.11.5 2016.09.06  初期化追加

                                    '' Ver1.8.6.2  2015.12.04  ﾌﾗｸﾞは参照せずにｽﾃｰﾀｽが設定されていれば表示する
                                    ''                          設定値が決まっていなくてもｽﾃｰﾀｽのみ決まっていることがあるので
                                    ''If .AnalogLoLoUse = 1 And LenB(strLL) <> 0 Then    '' LLｽﾃｰﾀｽあり
                                    If LenB(strLL) <> 0 Then    '' LLｽﾃｰﾀｽあり
                                        strTemp += strLL & "/"
                                    Else
                                        strTemp = ""
                                    End If

                                    ''If .AnalogLoUse = 1 And LenB(strL) <> 0 Then    '' Lｽﾃｰﾀｽあり
                                    If LenB(strL) <> 0 Then    '' Lｽﾃｰﾀｽあり
                                        strTemp += strL & "/"
                                    End If

                                    strTemp += "NOR/"

                                    ''If .AnalogHiUse = 1 And LenB(strH) <> 0 Then    '' Hｽﾃｰﾀｽあり
                                    If LenB(strH) <> 0 Then    '' Hｽﾃｰﾀｽあり
                                        strTemp += strH & "/"
                                    End If

                                    ''If .AnalogHiHiUse = 1 And LenB(strHH) <> 0 Then    '' HHｽﾃｰﾀｽあり
                                    If LenB(strHH) <> 0 Then    '' HHｽﾃｰﾀｽあり
                                        strTemp += strHH
                                    End If

                                    If strTemp = "NOR/" Then    '' NORのみならばｽﾃｰﾀｽ未定とする
                                        strTemp = ""
                                    Else
                                        '' 文字列の最後尾ならば"/"を削除する
                                        nLen = LenB(strTemp)
                                        'Ver2.0.7.L
                                        'If strTemp.Substring(nLen - 1) = "/" Then
                                        If MidB(strTemp, nLen - 1) = "/" Then
                                            'strTemp = strTemp.Remove(nLen - 1)
                                            strTemp = MidB(strTemp, 0, nLen - 1)
                                        End If
                                    End If
                                End If


                            End If

                            CHStr.Status = strTemp

                        End If

                        If .AnalogHiHiUse = 1 Or .AnalogHiUse = 1 Or .AnalogLoUse = 1 Or .AnalogLoLoUse = 1 Or .AnalogSensorFailUse = 1 Then
                            '排ガスリポーズはアラームではないので除外（フラグは必要)   2013.07.25 K.Fujimoto
                            If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                CHStr.AL = ""
                            Else
                                CHStr.AL = "o"
                            End If
                        Else
                            CHStr.AL = ""
                        End If

                    Case gCstCodeChTypeDigital      ''デジタル
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypeDigitalNC
                                CHStr.INSIG = "NC"
                            Case gCstCodeChDataTypeDigitalNO
                                CHStr.INSIG = "NO"
                            Case gCstCodeChDataTypeDigitalJacomNC
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM-"
                                Else
                                    CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If
                            Case gCstCodeChDataTypeDigitalJacom55NC
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM55-"
                                Else
                                    CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If
                            Case gCstCodeChDataTypeDigitalJacomNO
                                CHStr.INSIG = "NO"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM-"
                                Else
                                    CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If
                            Case gCstCodeChDataTypeDigitalJacom55NO
                                CHStr.INSIG = "NO"
                                CHStr.SIGType = "J"
                                If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                    CHStr.INAdd = "JACOM55-"
                                Else
                                    CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                                End If
                            Case gCstCodeChDataTypeDigitalModbusNC
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypeDigitalModbusNO
                                CHStr.INSIG = "NO"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypeDigitalExt
                                CHStr.INSIG = "NO"
                            Case gCstCodeChDataTypeDigitalDeviceStatus
                                CHStr.INSIG = "NC"
                                CHStr.SIGType = "S"
                        End Select

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                        ''G Repose 1 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                        ''G Repose 2 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                        ''Delay --------------------------------------------------------
                        CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            If CHStr.AlmInf(1).Delay <> "" Then
                                CHStr.AlmInf(1).Delay += strTemp
                            End If
                        End If

                        ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                            cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                            CHStr.Status = cmbStatus.Text

                        Else
                            strTemp = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                            If strTemp.Length > 8 Then
                                CHStr.Status = strTemp.Substring(0, 8).Trim & "/" & strTemp.Substring(8).Trim
                            Else
                                CHStr.Status = Trim(strTemp)
                            End If

                        End If

                        If .DigitalUse = 1 Then
                            CHStr.AL = "o"
                        Else
                            CHStr.AL = ""
                        End If

                    Case gCstCodeChTypeMotor        ''モーター
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        If .udtChCommon.shtData >= gCstCodeChDataTypeMotorManRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorManRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                            CHStr.INSIG = "M1"
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorAbnorRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorAbnorRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                            CHStr.INSIG = "M2"


                            'Ver2.0.0.2 モーター種別増加 START
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorRManRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorRManRunK Then
                            CHStr.INSIG = "M1"
                            CHStr.SIGType = "R"
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorRAbnorRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorRAbnorRunK Then
                            CHStr.INSIG = "M2"
                            CHStr.SIGType = "R"
                            'Ver2.0.0.2 モーター種別増加 END


                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorDevice Then
                            CHStr.INSIG = "MO"

                            'Ver2.0.0.2 モーター種別増加 START
                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorRDevice Then
                            CHStr.INSIG = "MO"
                            CHStr.SIGType = "R"
                            'Ver2.0.0.2 モーター種別増加 END

                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom Then
                            CHStr.INSIG = "MO"
                            CHStr.SIGType = "J"
                            If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                CHStr.INAdd = "JACOM-"
                            Else
                                CHStr.INAdd = "JACOM-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                            End If
                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom55 Then
                            CHStr.INSIG = "MO"
                            CHStr.SIGType = "J"
                            If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                CHStr.INAdd = "JACOM55-"
                            Else
                                CHStr.INAdd = "JACOM55-" & .udtChCommon.shtPin.ToString   ''FU ADDRESS(IN)
                            End If
                        End If

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                        ''G Repose 1 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                        ''G Repose 2 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                        ''Delay --------------------------------------------------------
                        CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                        ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                            ' 2013.07.22 MO表示変更  K.Fujimoto
                            If .udtChCommon.shtStatus = 20 Then     ' MO
                                CHStr.Status = "RUN"
                            Else
                                Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                                cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                CHStr.Status = cmbStatus.Text
                            End If

                        Else
                            CHStr.Status = ""
                        End If

                        'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                        'If .MotorUse = 1 Then
                        If .MotorUse = 1 Or .MotorAlarmUse = 1 Then
                            CHStr.AL = "o"
                        Else
                            CHStr.AL = ""
                        End If

                        ''FU ADDRESS(OUT)
                        'CHStr.OUTAdd = mPrtConvFuAddress(.MotorFuNo, .MotorPortNo, .MotorPin)

                        If .MotorAlarmUse = 1 Then  ''フィードバックアラーム有り

                            '2015/4/23 T.Ueki Feedback タイマー msec→secに表示
                            CHStr.AlmInf(9).Value = Val(.MotorFeedback) / 10
                            'CHStr.AlmInf(9).Value = .MotorFeedback
                            CHStr.AlmInf(9).ExtGrp = IIf(.MotorAlarmExtGroup = gCstCodeChMotorExtGroupNothing, "", .MotorAlarmExtGroup)                 ''EXT.G
                            CHStr.AlmInf(9).GrpRep1 = IIf(.MotorAlarmGroupRepose1 = gCstCodeChMotorGroupRepose1Nothing, "", .MotorAlarmGroupRepose1)    ''G.Rep1
                            CHStr.AlmInf(9).GrpRep2 = IIf(.MotorAlarmGroupRepose2 = gCstCodeChMotorGroupRepose2Nothing, "", .MotorAlarmGroupRepose2)    ''G.Rep2
                            CHStr.AlmInf(9).Delay = IIf(.MotorAlarmDelay = gCstCodeChMotorGroupRepose2Nothing, "", .MotorAlarmDelay)                    ''DELAY
                        End If

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            If CHStr.AlmInf(1).Delay <> "" Then
                                CHStr.AlmInf(1).Delay += strTemp
                            End If
                            If CHStr.AlmInf(9).Delay <> "" Then
                                CHStr.AlmInf(9).Delay += strTemp
                            End If
                        End If

                        'Ver2.0.7.2
                        'モーターは、OutFuAdrが設定されていないとOUTSigは出さない
                        If CHStr.OUTAdd <> "" Then
                            If .MotorControl = 1 Then
                                CHStr.OUTSIG = "DOP"
                            Else
                                CHStr.OUTSIG = "DOM"
                            End If
                        End If
                        '-

                    Case gCstCodeChTypeValve        ''バルブ
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""

                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypeValveDI_DO   ''DI/DO
                                CHStr.INSIG = "DC3"

                                ''デジタルコンポジット設定テーブルインデックス ----------------
                                intCompIndex = .ValveCompositeTableIndex
                                CHStr.Status = ""
                                intStatusExist = 0
                                CHStr.AL = ""

                                '' Ver1.11.9.3 2016.11.26 ｽﾃｰﾀｽ取得処理追加
                                If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)
                                    cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                    CHStr.Status = cmbStatus.Text
                                    intStatusExist = -1
                                End If

                                With gudt.SetChComposite.udtComposite(intCompIndex - 1)     ''コンポジットテーブル参照

                                    For i As Integer = 0 To 8
                                        ''EXT Group ---------------------------------------------------
                                        CHStr.AlmInf(i).ExtGrp = IIf(.udtCompInf(i).bytExtGroup = gCstCodeChCompExtGroupNothing, "", .udtCompInf(i).bytExtGroup)
                                        ''G Repose 1 ---------------------------------------------------
                                        CHStr.AlmInf(i).GrpRep1 = IIf(.udtCompInf(i).bytGRepose1 = gCstCodeChCompGroupRepose1Nothing, "", .udtCompInf(i).bytGRepose1)
                                        ''G Repose 2 ---------------------------------------------------
                                        CHStr.AlmInf(i).GrpRep2 = IIf(.udtCompInf(i).bytGRepose2 = gCstCodeChCompGroupRepose2Nothing, "", .udtCompInf(i).bytGRepose2)
                                        ''Delay --------------------------------------------------------
                                        CHStr.AlmInf(i).Delay = IIf(.udtCompInf(i).bytDelay = gCstCodeChCompDelayTimerNothing, "", .udtCompInf(i).bytDelay)

                                        ''Status ------------------------------------------------------
                                        If intStatusExist <> -1 Then        '' Ver1.11.9.3 2016.11.26 ｽﾃｰﾀｽ取得済
                                            If intStatusExist = 1 Then
                                                CHStr.Status += "/"
                                            End If
                                            If (.udtCompInf(i).strStatusName <> "") And (gBitCheck(.udtCompInf(i).bytAlarmUse, 0)) Then
                                                CHStr.Status += .udtCompInf(i).strStatusName
                                                intStatusExist = 1
                                            Else
                                                CHStr.Status += ""
                                            End If
                                        End If

                                        If gBitCheck(.udtCompInf(i).bytAlarmUse, 1) Then
                                            CHStr.AL = "o"
                                        End If
                                    Next

                                End With

                                ''FU ADDRESS(OUT)
                                'CHStr.OUTAdd = mPrtConvFuAddress(.ValveDiDoFuNo, .ValveDiDoPortNo, .ValveDiDoPin)

                                If .ValveDiDoAlarmUse = 1 Then  ''フィードバックアラーム有り
                                    'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                                    CHStr.AL = "o"

                                    '2015/4/23 T.Ueki Feedback タイマー msec→secに表示 
                                    CHStr.AlmInf(9).Value = Val(.ValveDiDoFeedback) / 10
                                    'CHStr.AlmInf(9).Value = .ValveDiDoFeedback                                                                                          ''Value
                                    CHStr.AlmInf(9).ExtGrp = IIf(.ValveDiDoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveDiDoAlarmExtGroup)                 ''EXT.G
                                    CHStr.AlmInf(9).GrpRep1 = IIf(.ValveDiDoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveDiDoAlarmGroupRepose1)    ''G.Rep1
                                    CHStr.AlmInf(9).GrpRep2 = IIf(.ValveDiDoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveDiDoAlarmGroupRepose2)    ''G.Rep2
                                    CHStr.AlmInf(9).Delay = IIf(.ValveDiDoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveDiDoAlarmDelay)                    ''DELAY
                                End If

                                ''Delay タイマー切替
                                strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                                If strTemp = "m" Then
                                    For i As Integer = 0 To 9
                                        If CHStr.AlmInf(i).Delay <> "" Then
                                            CHStr.AlmInf(i).Delay += strTemp
                                        End If
                                    Next
                                End If

                                'Ver2.0.7.2
                                'DIDO,AIDO,DO時、OutFuAdrが設定されていなくてもOUTSigは出す
                                'If CHStr.OUTAdd <> "" Then
                                If .ValveDiDoControl = 1 Then
                                    CHStr.OUTSIG = "DOP"
                                Else
                                    CHStr.OUTSIG = "DOM"
                                End If
                                'End If

                                CHStr.TermCount = .udtChCommon.shtPinNo       '' 端子数  Ver1.11.9.3 2016.11.26 追加

                            Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
                                If .udtChCommon.shtSignal = 2 Then
                                    CHStr.INSIG = "PT"
                                Else
                                    CHStr.INSIG = "AI"
                                End If

                                ''Decimal Position --------------------------------------------
                                intDecimalP = .ValveAiDoDecimalPosition
                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)
                                ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                                If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                                    CHStr.Range = ""
                                Else
                                    dblLowValue = .ValveAiDoRangeLow / (10 ^ intDecimalP)
                                    dblHiValue = .ValveAiDoRangeHigh / (10 ^ intDecimalP)
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range

                                    'Ver2.0.0.4
                                    'グリーンマーク(ノーマルレンジ)対応
                                    '設定アリの場合、「G」を付ける
                                    If (.ValveAiDoNormalHigh <> gCstCodeChAlalogNormalRangeNothingHi And .ValveAiDoNormalHigh <> 0) Or _
                                        (.ValveAiDoNormalLow <> gCstCodeChAlalogNormalRangeNothingLo And .ValveAiDoNormalLow <> 0) Then
                                        'Ver2.0.0.6 グリーンマークは設定ONではないと印刷しない
                                        If g_bytGreenMarkPrint = 1 Then
                                            CHStr.Range = "G " & CHStr.Range
                                        End If
                                    End If

                                End If


                                ''Value -------------------------------------------------------
                                If .ValveAiDoHiHiUse = 0 Then      ''Use HH アラーム無し
                                    CHStr.AlmInf(0).Value = ""
                                Else
                                    dblValue = .ValveAiDoHiHiValue / (10 ^ intDecimalP)    ''Value HH
                                    CHStr.AlmInf(0).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiDoHiUse = 0 Then        ''Use H  アラーム無し
                                    CHStr.AlmInf(1).Value = ""
                                Else
                                    dblValue = .ValveAiDoHiValue / (10 ^ intDecimalP)      ''Value H
                                    CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiDoLoUse = 0 Then        ''Use L  アラーム無し
                                    CHStr.AlmInf(2).Value = ""
                                Else
                                    dblValue = .ValveAiDoLoValue / (10 ^ intDecimalP)      ''Value L
                                    CHStr.AlmInf(2).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiDoLoLoUse = 0 Then      ''Use LL アラーム無し
                                    CHStr.AlmInf(3).Value = ""
                                Else
                                    dblValue = .ValveAiDoLoLoValue / (10 ^ intDecimalP)    ''Value LL
                                    CHStr.AlmInf(3).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiDoSensorFailUse = 0 Then
                                    CHStr.AlmInf(4).Value = ""
                                Else
                                    CHStr.AlmInf(4).Value = "o"
                                End If


                                ''EXT Group ---------------------------------------------------
                                CHStr.AlmInf(0).ExtGrp = IIf(.ValveAiDoHiHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoHiHiExtGroup)                ''EXT.G HH
                                CHStr.AlmInf(1).ExtGrp = IIf(.ValveAiDoHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoHiExtGroup)                    ''EXT.G H
                                CHStr.AlmInf(2).ExtGrp = IIf(.ValveAiDoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoLoExtGroup)                    ''EXT.G L
                                CHStr.AlmInf(3).ExtGrp = IIf(.ValveAiDoLoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoLoLoExtGroup)                ''EXT.G LL
                                CHStr.AlmInf(4).ExtGrp = IIf(.ValveAiDoSensorFailExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoSensorFailExtGroup)    ''EXT.G SF

                                ''G Repose 1 --------------------------------------------------
                                CHStr.AlmInf(0).GrpRep1 = IIf(.ValveAiDoHiHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoHiHiGroupRepose1)   ''G.Rep1 HH
                                CHStr.AlmInf(1).GrpRep1 = IIf(.ValveAiDoHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoHiGroupRepose1)       ''G.Rep1 H
                                CHStr.AlmInf(2).GrpRep1 = IIf(.ValveAiDoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoLoGroupRepose1)       ''G.Rep1 L
                                CHStr.AlmInf(3).GrpRep1 = IIf(.ValveAiDoLoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoLoLoGroupRepose1)   ''G.Rep1 LL

                                ''G Repose 2 --------------------------------------------------
                                CHStr.AlmInf(0).GrpRep2 = IIf(.ValveAiDoHiHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoHiHiGroupRepose2)   ''G.Rep2 HH
                                CHStr.AlmInf(1).GrpRep2 = IIf(.ValveAiDoHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoHiGroupRepose2)       ''G.Rep2 H
                                CHStr.AlmInf(2).GrpRep2 = IIf(.ValveAiDoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoLoGroupRepose2)       ''G.Rep2 L
                                CHStr.AlmInf(3).GrpRep2 = IIf(.ValveAiDoLoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoLoLoGroupRepose2)   ''G.Rep2 LL

                                ''Delay -------------------------------------------------------
                                CHStr.AlmInf(0).Delay = IIf(.ValveAiDoHiHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoHiHiDelay)                     ''Delay HH
                                CHStr.AlmInf(1).Delay = IIf(.ValveAiDoHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoHiDelay)                         ''Delay H
                                CHStr.AlmInf(2).Delay = IIf(.ValveAiDoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoLoDelay)                         ''Delay L
                                CHStr.AlmInf(3).Delay = IIf(.ValveAiDoLoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoLoLoDelay)                     ''Delay LL
                                CHStr.AlmInf(4).Delay = IIf(.ValveAiDoSensorFailDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiDoSensorFailDelay)         ''Delay SF

                                ''Status -----------------------------------------------------
                                If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                    cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                    CHStr.Status = cmbStatus.Text

                                Else
                                    strHH = gGetString(.ValveAiDoHiHiStatusInput)     ''特殊コード対応
                                    strH = gGetString(.ValveAiDoHiStatusInput)        ''特殊コード対応
                                    strL = gGetString(.ValveAiDoLoStatusInput)        ''特殊コード対応
                                    strLL = gGetString(.ValveAiDoLoLoStatusInput)     ''特殊コード対応

                                    ''2015.03.12 HIGH,LOWの並び順を変更
                                    If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                                       (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                                        strTemp = ""
                                    Else
                                        strTemp = "NOR/"
                                    End If

                                    ''LL/Lステータス
                                    ''HIGH,LOWの両ステータスがある場合はLL/L、LOWのみはL/LL
                                    If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                                       (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                                        If .AnalogLoLoUse = 1 Then
                                            strTemp += strLL
                                        End If
                                        If .AnalogLoUse = 1 Then
                                            If .AnalogLoLoUse = 1 Then
                                                strTemp += "/" & strL
                                            Else
                                                strTemp += strL
                                            End If
                                        End If
                                    Else
                                        If .AnalogLoUse = 1 Then
                                            strTemp += strL
                                        End If
                                        If .AnalogLoLoUse = 1 Then
                                            If .AnalogLoUse = 1 Then
                                                strTemp += "/" & strLL
                                            Else
                                                strTemp += strLL
                                            End If
                                        End If
                                    End If

                                    ''HIGH,LOWの両ステータスがある場合は中間に"NOR"
                                    If (.AnalogLoUse = 1 Or .AnalogLoLoUse = 1) And _
                                       (.AnalogHiUse = 1 Or .AnalogHiHiUse = 1) Then
                                        strTemp += "/NOR/"
                                    End If

                                    ''H/HHステータス
                                    If .AnalogHiUse = 1 Then
                                        strTemp += strH
                                    End If
                                    If .AnalogHiHiUse = 1 Then
                                        If .AnalogHiUse = 1 Then
                                            strTemp += "/" & strHH
                                        Else
                                            strTemp += strHH
                                        End If
                                    End If

                                    CHStr.Status = strTemp

                                End If

                                If .ValveAiDoHiHiUse = 1 Or .ValveAiDoHiUse = 1 Or .ValveAiDoLoUse = 1 Or .ValveAiDoLoLoUse = 1 Or .ValveAiDoSensorFailUse = 1 Then
                                    CHStr.AL = "o"
                                Else
                                    CHStr.AL = ""
                                End If

                                ''FU ADDRESS(OUT)
                                'CHStr.OUTAdd = mPrtConvFuAddress(.ValveAiDoFuNo, .ValveAiDoPortNo, .ValveAiDoPin)

                                If .ValveAiDoAlarmUse = 1 Then  ''フィードバックアラーム有り
                                    'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                                    CHStr.AL = "o"

                                    CHStr.AlmInf(9).Value = Val(.ValveAiDoFeedback) / 10    '' Ver1.11.9.9 2016.12.19
                                    'CHStr.AlmInf(9).Value = .ValveAiDoFeedback                                                                                          ''Value
                                    CHStr.AlmInf(9).ExtGrp = IIf(.ValveAiDoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiDoAlarmExtGroup)                 ''EXT.G
                                    CHStr.AlmInf(9).GrpRep1 = IIf(.ValveAiDoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiDoAlarmGroupRepose1)    ''G.Rep1
                                    CHStr.AlmInf(9).GrpRep2 = IIf(.ValveAiDoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiDoAlarmGroupRepose2)    ''G.Rep2
                                    CHStr.AlmInf(9).Delay = IIf(.ValveAiDoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveAiDoAlarmDelay)                      ''DELAY
                                End If

                                ''Delay タイマー切替
                                strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                                If strTemp = "m" Then
                                    For i As Integer = 0 To 4
                                        If CHStr.AlmInf(i).Delay <> "" Then
                                            CHStr.AlmInf(i).Delay += strTemp
                                        End If
                                    Next
                                    If CHStr.AlmInf(9).Delay <> "" Then
                                        CHStr.AlmInf(9).Delay += strTemp
                                    End If
                                End If

                                'Ver2.0.7.2
                                'DIDO,AIDO,DO時、OutFuAdrが設定されていなくてもOUTSigは出す
                                'If CHStr.OUTAdd <> "" Then
                                If .ValveAiDoOutControl = 1 Then
                                    CHStr.OUTSIG = "DOP"
                                Else
                                    CHStr.OUTSIG = "DOM"
                                End If
                                'End If


                            Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2    ''AI/AO
                                If .udtChCommon.shtSignal = 2 Then
                                    CHStr.INSIG = "PT"
                                Else
                                    CHStr.INSIG = "AI"
                                End If

                                ''Decimal Position --------------------------------------------
                                intDecimalP = .ValveAiAoDecimalPosition
                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)
                                ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                                If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                                    CHStr.Range = ""
                                Else
                                    dblLowValue = .ValveAiAoRangeLow / (10 ^ intDecimalP)
                                    dblHiValue = .ValveAiAoRangeHigh / (10 ^ intDecimalP)
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range

                                    'Ver2.0.0.4
                                    'グリーンマーク(ノーマルレンジ)対応
                                    '設定アリの場合、「G」を付ける
                                    If (.ValveAiAoNormalHigh <> gCstCodeChAlalogNormalRangeNothingHi And .ValveAiAoNormalHigh <> 0) Or _
                                        (.ValveAiAoNormalLow <> gCstCodeChAlalogNormalRangeNothingLo And .ValveAiAoNormalLow <> 0) Then
                                        'Ver2.0.0.6 グリーンマークは設定ONではないと印刷しない
                                        If g_bytGreenMarkPrint = 1 Then
                                            CHStr.Range = "G " & CHStr.Range
                                        End If
                                    End If
                                End If

                                ''Value -------------------------------------------------------
                                If .ValveAiAoHiHiUse = 0 Then      ''Use HH アラーム無し
                                    CHStr.AlmInf(0).Value = ""
                                Else
                                    dblValue = .ValveAiAoHiHiUse / (10 ^ intDecimalP)    ''Value HH
                                    CHStr.AlmInf(0).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiAoHiUse = 0 Then        ''Use H  アラーム無し
                                    CHStr.AlmInf(1).Value = ""
                                Else
                                    dblValue = .ValveAiAoHiUse / (10 ^ intDecimalP)      ''Value H
                                    CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiAoLoUse = 0 Then        ''Use L  アラーム無し
                                    CHStr.AlmInf(2).Value = ""
                                Else
                                    dblValue = .ValveAiAoLoUse / (10 ^ intDecimalP)      ''Value L
                                    CHStr.AlmInf(2).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiAoLoLoUse = 0 Then      ''Use LL アラーム無し
                                    CHStr.AlmInf(3).Value = ""
                                Else
                                    dblValue = .ValveAiAoLoLoUse / (10 ^ intDecimalP)    ''Value LL
                                    CHStr.AlmInf(3).Value = dblValue.ToString(strDecimalFormat)
                                End If

                                If .ValveAiAoSensorFailUse = 0 Then
                                    CHStr.AlmInf(4).Value = ""
                                Else
                                    CHStr.AlmInf(4).Value = "o"
                                End If


                                ''EXT Group ---------------------------------------------------
                                CHStr.AlmInf(0).ExtGrp = IIf(.ValveAiAoHiHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoHiHiExtGroup)                ''EXT.G HH
                                CHStr.AlmInf(1).ExtGrp = IIf(.ValveAiAoHiExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoHiExtGroup)                    ''EXT.G H
                                CHStr.AlmInf(2).ExtGrp = IIf(.ValveAiAoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoLoExtGroup)                    ''EXT.G L
                                CHStr.AlmInf(3).ExtGrp = IIf(.ValveAiAoLoLoExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoLoLoExtGroup)                ''EXT.G LL
                                CHStr.AlmInf(4).ExtGrp = IIf(.ValveAiAoSensorFailExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoSensorFailExtGroup)    ''EXT.G SF

                                ''G Repose 1 --------------------------------------------------
                                CHStr.AlmInf(0).GrpRep1 = IIf(.ValveAiAoHiHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoHiHiGroupRepose1)   ''G.Rep1 HH
                                CHStr.AlmInf(1).GrpRep1 = IIf(.ValveAiAoHiGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoHiGroupRepose1)       ''G.Rep1 H
                                CHStr.AlmInf(2).GrpRep1 = IIf(.ValveAiAoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoLoGroupRepose1)       ''G.Rep1 L
                                CHStr.AlmInf(3).GrpRep1 = IIf(.ValveAiAoLoLoGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoLoLoGroupRepose1)   ''G.Rep1 LL

                                ''G Repose 2 --------------------------------------------------
                                CHStr.AlmInf(0).GrpRep2 = IIf(.ValveAiAoHiHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoHiHiGroupRepose2)   ''G.Rep2 HH
                                CHStr.AlmInf(1).GrpRep2 = IIf(.ValveAiAoHiGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoHiGroupRepose2)       ''G.Rep2 H
                                CHStr.AlmInf(2).GrpRep2 = IIf(.ValveAiAoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoLoGroupRepose2)       ''G.Rep2 L
                                CHStr.AlmInf(3).GrpRep2 = IIf(.ValveAiAoLoLoGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoLoLoGroupRepose2)   ''G.Rep2 LL

                                ''Delay -------------------------------------------------------
                                CHStr.AlmInf(0).Delay = IIf(.ValveAiAoHiHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoHiHiDelay)                     ''Delay HH
                                CHStr.AlmInf(1).Delay = IIf(.ValveAiAoHiDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoHiDelay)                         ''Delay H
                                CHStr.AlmInf(2).Delay = IIf(.ValveAiAoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoLoDelay)                         ''Delay L
                                CHStr.AlmInf(3).Delay = IIf(.ValveAiAoLoLoDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoLoLoDelay)                     ''Delay LL
                                CHStr.AlmInf(4).Delay = IIf(.ValveAiAoSensorFailDelay = gCstCodeChValveDelayTimerNothing, "", .ValveAiAoSensorFailDelay)         ''Delay SF

                                ''Status -----------------------------------------------------
                                If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                                    Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                                    cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                                    CHStr.Status = cmbStatus.Text

                                Else
                                    strHH = gGetString(.ValveAiAoHiHiStatusInput)     ''特殊コード対応
                                    strH = gGetString(.ValveAiAoHiStatusInput)        ''特殊コード対応
                                    strL = gGetString(.ValveAiAoLoStatusInput)        ''特殊コード対応
                                    strLL = gGetString(.ValveAiAoLoLoStatusInput)     ''特殊コード対応


                                    If .ValveAiAoHiHiUse = 1 Then
                                        strTemp = strHH
                                        intLen = strTemp.Length
                                    End If
                                    If .ValveAiAoHiUse = 1 Then
                                        If .ValveAiAoHiHiUse = 1 Then
                                            strTemp += "/" & strH
                                        Else
                                            strTemp += strH
                                        End If
                                        intLen += strTemp.Length
                                    End If
                                    If .ValveAiAoHiHiUse = 1 Or .ValveAiAoHiUse = 1 Then
                                        strTemp += "/NOR"
                                    Else
                                        strTemp += "NOR"
                                    End If
                                    If .ValveAiAoLoUse = 1 Or .ValveAiAoLoLoUse = 1 Then
                                        strTemp += "/"
                                    End If
                                    If .ValveAiAoLoUse = 1 Then
                                        strTemp += strL
                                    End If
                                    If .ValveAiAoLoLoUse = 1 Then
                                        If .ValveAiAoLoUse = 1 Then
                                            strTemp += "/" & strLL
                                        Else
                                            strTemp += strLL
                                        End If
                                    End If

                                    CHStr.Status = strTemp

                                End If

                                If .ValveAiAoHiHiUse = 1 Or .ValveAiAoHiUse = 1 Or .ValveAiAoLoUse = 1 Or .ValveAiAoLoLoUse = 1 Or .ValveAiAoSensorFailUse = 1 Then
                                    CHStr.AL = "o"
                                Else
                                    CHStr.AL = ""
                                End If

                                ''FU ADDRESS(OUT)
                                'CHStr.OUTAdd = mPrtConvFuAddress(.ValveAiAoFuNo, .ValveAiAoPortNo, .ValveAiAoPin)
                                If .ValveAiAoAlarmUse = 1 Then  ''フィードバックアラーム有り
                                    'Ver2.0.7.3 ﾌｨｰﾄﾞﾊﾞｯｸｱﾗｰﾑもAL対象
                                    CHStr.AL = "o"

                                    CHStr.AlmInf(9).Value = Val(.ValveAiAoFeedback) / 10    '' Ver1.11.9.9 2016.12.19
                                    'CHStr.AlmInf(9).Value = .ValveAiAoFeedback                                                                                          ''Value
                                    CHStr.AlmInf(9).ExtGrp = IIf(.ValveAiAoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoAlarmExtGroup)                 ''EXT.G
                                    CHStr.AlmInf(9).GrpRep1 = IIf(.ValveAiAoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoAlarmGroupRepose1)    ''G.Rep1
                                    CHStr.AlmInf(9).GrpRep2 = IIf(.ValveAiAoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoAlarmGroupRepose2)    ''G.Rep2
                                    CHStr.AlmInf(9).Delay = IIf(.ValveAiAoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveAiAoAlarmDelay)                      ''DELAY
                                End If

                                ''Delay タイマー切替
                                strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                                If strTemp = "m" Then
                                    For i As Integer = 0 To 4
                                        If CHStr.AlmInf(i).Delay <> "" Then
                                            CHStr.AlmInf(i).Delay += strTemp
                                        End If
                                    Next
                                    If CHStr.AlmInf(9).Delay <> "" Then
                                        CHStr.AlmInf(9).Delay += strTemp
                                    End If
                                End If

                                If CHStr.OUTAdd <> "" Then
                                    CHStr.OUTSIG = "AO"
                                End If

                            Case gCstCodeChDataTypeValveAO_4_20 ''AO
                                ''Decimal Position --------------------------------------------
                                intDecimalP = .ValveAiAoDecimalPosition
                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)

                                ''Range (K, 1-5 V, 4-20 mA, Exhaust Gus, 外部機器)
                                ' 2015.11.16  Ver1.7.9 ﾚﾝｼﾞ未設定処理追加  L/Hとも0の場合は未定とする
                                If .AnalogRangeLow = 0 And .AnalogRangeHigh = 0 Then
                                    CHStr.Range = ""
                                Else
                                    dblLowValue = .ValveAiAoRangeLow / (10 ^ intDecimalP)
                                    dblHiValue = .ValveAiAoRangeHigh / (10 ^ intDecimalP)
                                    CHStr.Range = dblLowValue.ToString(strDecimalFormat) & "/" & dblHiValue.ToString(strDecimalFormat)  ''Range
                                End If

                                ''FU ADDRESS(OUT)
                                CHStr.OUTAdd = gConvFuAddress(.ValveAiAoFuNo, .ValveAiAoPortNo, .ValveAiAoPin)

                                'CHStr.AlmInf(9).ExtGrp = IIf(.ValveAiAoAlarmExtGroup = gCstCodeChValveExtGroupNothing, "", .ValveAiAoAlarmExtGroup)                 ''EXT.G
                                'CHStr.AlmInf(9).GrpRep1 = IIf(.ValveAiAoAlarmGroupRepose1 = gCstCodeChValveGroupRepose1Nothing, "", .ValveAiAoAlarmGroupRepose1)    ''G.Rep1
                                'CHStr.AlmInf(9).GrpRep2 = IIf(.ValveAiAoAlarmGroupRepose2 = gCstCodeChValveGroupRepose2Nothing, "", .ValveAiAoAlarmGroupRepose2)    ''G.Rep2
                                'CHStr.AlmInf(9).Delay = IIf(.ValveAiAoAlarmDelay = gCstCodeChMotorDelayTimerNothing, "", .ValveAiAoAlarmDelay)                      ''DELAY

                                If CHStr.OUTAdd <> "" Then
                                    CHStr.OUTSIG = "AO"
                                End If


                            Case gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55, gCstCodeChDataTypeValveExt      ''DO

                                ''FU ADDRESS(OUT)
                                'CHStr.OUTAdd = mPrtConvFuAddress(.ValveDiDoFuNo, .ValveDiDoPortNo, .ValveDiDoPin)


                                'Ver2.0.7.2
                                'DIDO,AIDO,DO時、OutFuAdrが設定されていなくてもOUTSigは出す
                                'If CHStr.OUTAdd <> "" Then
                                If .ValveDiDoControl = 1 Then
                                    CHStr.OUTSIG = "DOP"
                                Else
                                    CHStr.OUTSIG = "DOM"
                                End If
                                'End If

                                If .udtChCommon.shtData = gCstCodeChDataTypeValveJacom Then
                                    CHStr.SIGType = "J"
                                    If .ValveDiDoPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                        CHStr.OUTAdd = "JACOM-"
                                    Else
                                        CHStr.OUTAdd = "JACOM-" & .ValveDiDoPin.ToString   ''FU ADDRESS(OUT)
                                    End If

                                End If

                                If .udtChCommon.shtData = gCstCodeChDataTypeValveJacom55 Then
                                    CHStr.SIGType = "J"
                                    If .ValveDiDoPin = gCstCodeChNotSetFuPin Then      ' 2015.11.16 Ver1.7.9 ｱﾄﾞﾚｽ未定の場合は印字しない
                                        CHStr.OUTAdd = "JACOM55-"
                                    Else
                                        CHStr.OUTAdd = "JACOM55-" & .ValveDiDoPin.ToString   ''FU ADDRESS(OUT)
                                    End If

                                End If

                        End Select

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If



                    Case gCstCodeChTypeComposite    ''コンポジット
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        If .udtChCommon.shtData = gCstCodeChDataTypeCompTankLevel Then              '' 代表ステータス
                            CHStr.INSIG = "DC1"

                            ''EXT Group ---------------------------------------------------
                            CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                            ''G Repose 1 ---------------------------------------------------
                            CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                            ''G Repose 2 ---------------------------------------------------
                            CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                            ''Delay --------------------------------------------------------
                            CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                            ''Status ------------------------------------------------------
                            strTemp = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                            If strTemp.Length > 8 Then
                                CHStr.Status = strTemp.Substring(0, 8).Trim & "/" & strTemp.Substring(8).Trim
                            Else
                                CHStr.Status = Trim(strTemp)
                            End If

                            ''デジタルコンポジット設定テーブルインデックス ----------------
                            intCompIndex = .CompositeTableIndex
                            CHStr.AL = ""

                            With gudt.SetChComposite.udtComposite(intCompIndex - 1)     ''コンポジットテーブル参照

                                For i As Integer = 0 To 8
                                    If gBitCheck(.udtCompInf(i).bytAlarmUse, 1) Then
                                        CHStr.AL = "o"
                                    End If
                                Next

                            End With

                        ElseIf .udtChCommon.shtData = gCstCodeChDataTypeCompTankLevelIndevi Then    '' 個別ステータス
                            CHStr.INSIG = "DC2"

                            ''デジタルコンポジット設定テーブルインデックス ----------------
                            intCompIndex = .CompositeTableIndex
                            CHStr.Status = ""
                            intStatusExist = 0
                            CHStr.AL = ""

                            With gudt.SetChComposite.udtComposite(intCompIndex - 1)     ''コンポジットテーブル参照

                                For i As Integer = 0 To 8
                                    ''EXT Group ---------------------------------------------------
                                    CHStr.AlmInf(i).ExtGrp = IIf(.udtCompInf(i).bytExtGroup = gCstCodeChCompExtGroupNothing, "", .udtCompInf(i).bytExtGroup)
                                    ''G Repose 1 ---------------------------------------------------
                                    CHStr.AlmInf(i).GrpRep1 = IIf(.udtCompInf(i).bytGRepose1 = gCstCodeChCompGroupRepose1Nothing, "", .udtCompInf(i).bytGRepose1)
                                    ''G Repose 2 ---------------------------------------------------
                                    CHStr.AlmInf(i).GrpRep2 = IIf(.udtCompInf(i).bytGRepose2 = gCstCodeChCompGroupRepose2Nothing, "", .udtCompInf(i).bytGRepose2)
                                    ''Delay --------------------------------------------------------
                                    CHStr.AlmInf(i).Delay = IIf(.udtCompInf(i).bytDelay = gCstCodeChCompDelayTimerNothing, "", .udtCompInf(i).bytDelay)

                                    ''Status ------------------------------------------------------
                                    If intStatusExist = 1 Then
                                        CHStr.Status += "/"
                                    End If
                                    If (.udtCompInf(i).strStatusName <> "") And (gBitCheck(.udtCompInf(i).bytAlarmUse, 0)) Then
                                        CHStr.Status += .udtCompInf(i).strStatusName
                                        intStatusExist = 1
                                    Else
                                        CHStr.Status += ""
                                    End If

                                    If gBitCheck(.udtCompInf(i).bytAlarmUse, 1) Then
                                        CHStr.AL = "o"
                                    End If
                                Next

                            End With

                        End If

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            For i As Integer = 0 To 8
                                If CHStr.AlmInf(i).Delay <> "" Then
                                    CHStr.AlmInf(i).Delay += strTemp
                                End If
                            Next
                        End If


                    Case gCstCodeChTypePulse        ''パルス
                        ''INSIG, SIGTYPE
                        CHStr.SIGType = ""
                        Select Case .udtChCommon.shtData
                            Case gCstCodeChDataTypePulseTotal1_1
                                CHStr.INSIG = "PU"
                            Case gCstCodeChDataTypePulseTotal1_10
                                CHStr.INSIG = "P1"
                            Case gCstCodeChDataTypePulseTotal1_100
                                CHStr.INSIG = "P2"
                            Case gCstCodeChDataTypePulseDay1_1
                                CHStr.INSIG = "PUD"
                            Case gCstCodeChDataTypePulseDay1_10
                                CHStr.INSIG = "P1D"
                            Case gCstCodeChDataTypePulseDay1_100
                                CHStr.INSIG = "P2D"
                            Case gCstCodeChDataTypePulseRevoTotalHour
                                CHStr.INSIG = "RH"
                            Case gCstCodeChDataTypePulseRevoTotalMin
                                CHStr.INSIG = "R2"
                            Case gCstCodeChDataTypePulseRevoDayHour
                                CHStr.INSIG = "RHD"
                            Case gCstCodeChDataTypePulseRevoDayMin
                                CHStr.INSIG = "R2D"
                            Case gCstCodeChDataTypePulseRevoLapHour
                                CHStr.INSIG = "RHL"
                            Case gCstCodeChDataTypePulseRevoLapMin
                                CHStr.INSIG = "R2L"
                            Case gCstCodeChDataTypePulseExtDev
                                CHStr.INSIG = "PU"      '' Ver1.11.8.5 2016.11.10 "RH" → "PU"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDev   '' Ver1.11.8.3 2016.11.08 運転積算 通信CH追加
                                'Ver2.0.1.9 CHG
                                'CHStr.INSIG = "RH"
                                CHStr.INSIG = "R2"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDevTotalMin    '' Ver1.12.0.1 2017.01.13 
                                'CHStr.INSIG = "R2"
                                'CHStr.INSIG = "R2T" 'Ver2.0.5.9 ↑とかぶるため、打開策としてT付
                                CHStr.INSIG = "R2"  'Ver2.0.6.0 正しくは、「R2」である。上が違う
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDevDayHour     '' Ver1.12.0.1 2017.01.13 
                                CHStr.INSIG = "RHD"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDevDayMin      '' Ver1.12.0.1 2017.01.13
                                CHStr.INSIG = "R2D"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDevLapHour     '' Ver1.12.0.1 2017.01.13 
                                CHStr.INSIG = "RHL"
                                CHStr.SIGType = "R"
                            Case gCstCodeChDataTypePulseRevoExtDevLapMin      '' Ver1.12.0.1 2017.01.13 
                                CHStr.INSIG = "R2L"
                                CHStr.SIGType = "R"
                        End Select

                        ''ワークCH
                        If gBitCheck(.udtChCommon.shtFlag1, 2) Then
                            CHStr.SIGType = "W"
                        End If

                        ''EXT Group ---------------------------------------------------
                        CHStr.AlmInf(1).ExtGrp = IIf(gGet2Byte(.udtChCommon.shtExtGroup) = gCstCodeChCommonExtGroupNothing, "", .udtChCommon.shtExtGroup)        ''EXT.G H

                        ''G Repose 1 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep1 = IIf(gGet2Byte(.udtChCommon.shtGRepose1) = gCstCodeChCommonGroupRepose1Nothing, "", .udtChCommon.shtGRepose1)   ''G.Rep1 H

                        ''G Repose 2 ---------------------------------------------------
                        CHStr.AlmInf(1).GrpRep2 = IIf(gGet2Byte(.udtChCommon.shtGRepose2) = gCstCodeChCommonGroupRepose2Nothing, "", .udtChCommon.shtGRepose2)   ''G.Rep2 H

                        ''Delay --------------------------------------------------------
                        CHStr.AlmInf(1).Delay = IIf(gGet2Byte(.udtChCommon.shtDelay) = gCstCodeChCommonDelayTimerNothing, "", .udtChCommon.shtDelay)

                        ''Delay タイマー切替
                        strTemp = IIf(gBitCheck(.udtChCommon.shtFlag1, 3), "m", "")
                        If strTemp = "m" Then
                            If CHStr.AlmInf(1).Delay <> "" Then
                                CHStr.AlmInf(1).Delay += strTemp
                            End If
                        End If

                        ''Status -----------------------------------------------------
                        If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then   ''ステータス種別
                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusPulse)
                            cmbStatus.SelectedValue = .udtChCommon.shtStatus.ToString
                            CHStr.Status = cmbStatus.Text
                        Else
                            strTemp = mGetString(.udtChCommon.strStatus)     ''特殊コード対応
                            If strTemp.Length > 8 Then
                                CHStr.Status = "NOR/" & strTemp.Substring(0, 8).Trim
                            Else
                                CHStr.Status = "NOR/" & Trim(strTemp)
                            End If

                        End If

                        If .udtChCommon.shtData >= gCstCodeChDataTypePulseTotal1_1 And .udtChCommon.shtData <= gCstCodeChDataTypePulseDay1_100 Or _
                           .udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then

                            ''Decimal Position --------------------------------------------
                            intDecimalP = .PulseDecPoint
                            If intDecimalP = 0 Then
                                'Ver2.0.6.5 9が7個
                                'Ver2.0.7.E DecPoint無しは9が8個
                                CHStr.Range = "99999999"    '"9999999" '"99999999"
                                strDecimalFormat = ""
                            Else
                                If intDecimalP <= 6 Then
                                    CHStr.Range = ".".PadRight(intDecimalP + 1, "9"c)
                                    strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)
                                Else
                                    CHStr.Range = ".".PadRight(7, "9"c)
                                    strDecimalFormat = "0.".PadRight(8, "0"c)
                                End If
                            End If

                            CHStr.Range = CHStr.Range.PadLeft(8, "9"c)

                            If .PulseUse = 1 Then
                                dblValue = .PulseValue / (10 ^ intDecimalP)      ''Value H
                                CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                CHStr.AL = "o"
                            Else
                                CHStr.AlmInf(1).Value = ""
                                CHStr.AL = ""
                            End If
                        Else
                            ''Decimal Position --------------------------------------------
                            intDecimalP = .RevoDecPoint
                            If intDecimalP = 0 Then
                                'Ver2.0.6.5 9が7個
                                'Ver2.0.7.E DecPoint無しは9が8個
                                CHStr.Range = "99999999"    '"9999999" '"99999999"
                                strDecimalFormat = ""
                            Else
                                CHStr.Range = "99999.59"
                                strDecimalFormat = "0.".PadRight(intDecimalP + 2, "0"c)
                            End If

                            If .RevoUse = 1 Then        ''Value H
                                '' Ver1.9.1 2015.12.22  積算CHのｱﾗｰﾑ設定値の印字が異なる不具合修正
                                If intDecimalP = 2 Then     '' 時分
                                    dblValue = ((.RevoValue And &HFFFFFF00) >> 8)                       ''時
                                    dblValue = dblValue + (.RevoValue And &HFF) / (10 ^ intDecimalP)    ''時 + 分(小数点以下値)
                                Else
                                    dblValue = .RevoValue
                                End If

                                If dblValue = 0 Then
                                    CHStr.AlmInf(1).Value = ""
                                Else
                                    CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                End If
                                ''//
                                ''dblValue = .RevoValue / (10 ^ intDecimalP)      ''Value H
                                ''CHStr.AlmInf(1).Value = dblValue.ToString(strDecimalFormat)
                                CHStr.AL = "o"
                            Else
                                CHStr.AlmInf(1).Value = ""
                                CHStr.AL = ""
                            End If
                        End If

                End Select

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))

        End Try

    End Sub
    '--------------------------------------------------------------------
    ' 機能      : 文字列取得
    ' 返り値    : 変換後文字列
    ' 引き数    : ARG1 - (I ) 変換元文字列
    ' 機能説明  : NULLなどの不要な情報を取り除いた文字列を返す
    '--------------------------------------------------------------------
    Private Function mGetString(ByVal strInput As String, _
                      Optional ByVal blnTrim As Boolean = True) As String

        Try

            Dim strRtn As String

            strRtn = strInput
            strRtn = Replace(strRtn, vbNullChar, "")

            If blnTrim Then
                'strRtn = Trim(strRtn)
                If strRtn <> Nothing Then
                    strRtn = strRtn.TrimEnd
                Else
                    strRtn = ""
                End If
            End If

            Return strRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return strInput
        End Try

    End Function
    'OUT系全CHNo格納関数：起動時に一回呼ぶのみ
    Private Sub subSetAllOutCH()
        aryOutCHNo = New ArrayList

        '>>>OUT設定テーブルと、AndOrテーブルから
        '出力チャンネル設定の単体CHに入っているか探す
        With gudt.SetChOutput
            For i As Integer = 0 To UBound(.udtCHOutPut) Step 1
                '0なら処理しない
                If .udtCHOutPut(i).shtChid <> 0 Then
                    'タイプがCHデータ
                    If .udtCHOutPut(i).bytType = gCstCodeFuOutputChTypeCh Then
                        '0以外なら格納
                        aryOutCHNo.Add(CType(.udtCHOutPut(i).shtChid, Integer))
                    Else
                        'タイプが論理CH
                        For j As Integer = 0 To UBound(gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr) Step 1
                            If gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr(j).shtChid <> 0 Then
                                '格納
                                aryOutCHNo.Add(CType(gudt.SetChAndOr.udtCHOut(.udtCHOutPut(i).shtChid - 1).udtCHAndOr(j).shtChid, Integer))
                            End If
                        Next j
                    End If
                End If
            Next i
        End With

        '>>>SeqSetテーブルから
        With gudt.SetSeqSet
            For i = 0 To UBound(.udtDetail) Step 1
                'シーケンスIDがゼロではないこと
                If .udtDetail(i).shtId <> 0 Then
                    'OUTChが1～9999の範囲なら格納
                    If .udtDetail(i).shtOutChid > 0 And .udtDetail(i).shtOutChid < 10000 Then
                        aryOutCHNo.Add(CType(.udtDetail(i).shtOutChid, Integer))
                    End If
                    '8CHのCHが1～9999の範囲なら格納
                    For j = 0 To UBound(.udtDetail(i).udtInput) Step 1
                        If .udtDetail(i).udtInput(j).shtChid > 0 And .udtDetail(i).udtInput(j).shtChid < 10000 Then
                            aryOutCHNo.Add(CType(.udtDetail(i).udtInput(j).shtChid, Integer))
                        End If
                    Next j
                End If
            Next i
        End With

    End Sub
    'CHnoOutput設定されているか否か戻す
    Private Function fnGetOrAnd(pintCHNO As Integer) As String
        Dim strRet As String = ""

        '配列に存在すれば、「o」をなければ空白を戻す
        If aryOutCHNo.Contains(pintCHNO) = True Then
            strRet = "o"
        End If

        Return strRet
    End Function
#End Region

#End Region

    
End Class
