Public Class frmOpsGraphList

#Region "変数定義"

    ''グラフ設定構造体
    Private mudtSetOpsGraphWork As gTypSetOpsGraph = Nothing
    Private mudtSetOpsGraphNewMach As gTypSetOpsGraph = Nothing
    Private mudtSetOpsGraphNewCarg As gTypSetOpsGraph = Nothing

    ''グラフ設定データインポート用構造体
    Private mudtGraphDataMach As gTypImportGraphData = Nothing
    Private mudtGraphDataCarg As gTypImportGraphData = Nothing

    ''プルダウンメニュー構造体
    Private mudtSetOpsPulldownMenuNew As gTypSetOpsPulldownMenu = Nothing

    ''フリーグラフ用グリッド配列
    Private mgrdFree() As DataGridView

    ''ページタイプの前回値保存
    Private mintGraphTypeMach() As Integer = Nothing
    Private mintGraphTypeCarg() As Integer = Nothing

    ''フリーグラフの変更判定フラグ（TRUE:変更あり, FALSE:変更なし）
    Private mblnChangeFlg As Boolean = False

    ''初期化フラグ
    Private mblnInitFlg As Boolean

    'Ver2.1.3 Clear用フラグ
    Private mblClearFlg As Boolean
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
    Private Sub frmOpsGraphList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化フラグ
            mblnInitFlg = True
            mblClearFlg = False

            ''シートの表示／非表示設定
            fraGraph.Visible = True
            TabFree.Visible = False

            ''グリッドのコントロール配列を作成
            mgrdFree = New DataGridView() {grdFree1, grdFree2, grdFree3, grdFree4, grdFree5, _
                                           grdFree6, grdFree7, grdFree8, grdFree9, grdFree10}

            ''配列再定義
            Call mInitialArray()

            ''グリッドの初期化
            Call mInitialDataGrid(mgrdFree)

            ''Machinery/Cargoの情報を取得する
            Call mCopyStructure(gudt.SetOpsGraphM, mudtSetOpsGraphNewMach)
            Call mCopyStructure(gudt.SetOpsGraphC, mudtSetOpsGraphNewCarg)

            ''偏差、バー、アナログボタン選択
            optGraph.Checked = True

            ''Machinery/Cargoボタン設定
            Call gSetCombineControl(optMachinery, optCargo)

            ''画面表示
            If optMachinery.Checked Then Call mCopyStructure(mudtSetOpsGraphNewMach, mudtSetOpsGraphWork)
            If optCargo.Checked Then Call mCopyStructure(mudtSetOpsGraphNewCarg, mudtSetOpsGraphWork)
            Call mSetDisplay(mudtSetOpsGraphWork)

            ''フッターボタンの使用可/不可設定
            Call mSetControlFooterBtn()

            ''画面表示時のフォーカスを「Exit」にする
            ''Me.ActiveControl = Me.cmdExit

            ''初期化フラグ
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmOpsGraphExtGus_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try

            ''初期化フラグ
            mblnInitFlg = True

            ''画面表示時のセル選択を解除
            grdGraph.Rows(0).Cells(1).Selected = True

            ''初期化フラグ
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Exhaust, Bar, Analog Meter ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optGraph_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGraph.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            fraGraph.Visible = True
            TabFree.Visible = False
            cmdAnalogMater.Enabled = True

            ''フッターボタンの使用可/不可設定
            Call mSetControlFooterBtn()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Free Graph ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optFree_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFree.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            fraGraph.Visible = False
            TabFree.Visible = True
            cmdAnalogMater.Enabled = False

            ''フッターボタンの使用可/不可設定
            Call mSetControlFooterBtn()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Machineryボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optMachinery_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMachinery.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            ''グリッドの保留中の変更を全て適用させる
            Call grdGraph.EndEdit()

            If optMachinery.Checked Then ''Machinery選択

                ''Cargo情報の退避
                Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewCarg)

                ''Machinery情報を作業用構造体に設定
                Call mCopyStructure(mudtSetOpsGraphNewMach, mudtSetOpsGraphWork)

                ''画面表示
                Call mSetDisplay(mudtSetOpsGraphWork)

            ElseIf optCargo.Checked Then ''Cargo選択

                ''Machinery情報の退避
                Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewMach)

                ''Cargo情報を作業用構造体に設定
                Call mCopyStructure(mudtSetOpsGraphNewCarg, mudtSetOpsGraphWork)

                ''画面表示
                Call mSetDisplay(mudtSetOpsGraphWork)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Editボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click

        Try

            ''グリッドの保留中の変更を全て適用させる
            Call grdGraph.EndEdit()

            If optGraph.Checked Then

                ''---------------------
                '' ３種グラフ
                ''---------------------
                Dim intRowIndex As Integer = grdGraph.CurrentCell.RowIndex

                If optMachinery.Checked Then ''Machinery情報

                    ''選択したグラフタイプの画面表示
                    Call mDispOpsGraph(mudtSetOpsGraphWork, _
                                       CCInt(grdGraph.CurrentRow.Cells("cmbGraphType").Value), _
                                       intRowIndex)

                    ''表示したグラフタイプの保存
                    mintGraphTypeMach(intRowIndex) = CCInt(grdGraph.CurrentRow.Cells("cmbGraphType").Value)

                ElseIf optCargo.Checked Then ''Cargo情報

                    ''選択したグラフタイプの画面表示
                    Call mDispOpsGraph(mudtSetOpsGraphWork, _
                                       CCInt(grdGraph.CurrentRow.Cells("cmbGraphType").Value), _
                                       intRowIndex)

                    ''表示したグラフタイプの保存
                    mintGraphTypeCarg(intRowIndex) = CCInt(grdGraph.CurrentRow.Cells("cmbGraphType").Value)

                End If


            ElseIf optFree.Checked Then
                ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto

                ' ''---------------------
                ' '' フリーグラフ
                ' ''---------------------
                'Dim intOpsIndex As Integer = 0
                'Dim intRowIndex As Integer = 0
                'Dim udtMC As gEnmMachineryCargo = Nothing

                'If optMachinery.Checked Then
                '    udtMC = gEnmMachineryCargo.mcMachinery
                'ElseIf optCargo.Checked Then
                '    udtMC = gEnmMachineryCargo.mcCargo
                'End If

                ' ''OPS番号と設定番号を取得
                'Call mGetIndexFreeOpsSet(intOpsIndex, intRowIndex)

                ' ''レイアウト画面表示
                'Call frmOpsGraphFreeAlignment.gShow(mudtSetOpsGraphWork.udtGraphFreeRec(intOpsIndex).udtFreeGraphTitle(intRowIndex), Me, mblnChangeFlg, udtMC)

                ' ''フリーグラフの保存はレイアウト画面で行うので、ここでは構造体をコピーする
                ' ''（常に相違なしの状態を保つ）
                'For i = 0 To UBound(mudtSetOpsGraphWork.udtGraphFreeRec)

                '    For j As Integer = 0 To UBound(mudtSetOpsGraphWork.udtGraphFreeRec(i).udtFreeGraphTitle)

                '        ''Machinery情報
                '        If optMachinery.Checked Then Call mCopyOpsFree(mudtSetOpsGraphWork.udtGraphFreeRec(i).udtFreeGraphTitle(j), gudt.SetOpsGraphM.udtGraphFreeRec(i).udtFreeGraphTitle(j), True)

                '        ''Cargo情報
                '        If optCargo.Checked Then Call mCopyOpsFree(mudtSetOpsGraphWork.udtGraphFreeRec(i).udtFreeGraphTitle(j), gudt.SetOpsGraphC.udtGraphFreeRec(i).udtFreeGraphTitle(j), True)

                '    Next

                'Next

                ' ''フリーグラフに変更があった場合プルダウンメニューに自動反映させる
                'If mblnChangeFlg Then

                '    ''プルダウンメニューへ自動反映
                '    Call mImportGraphListData()

                '    ''変更フラグを無効にする
                '    mblnChangeFlg = False
                'End If

                ' ''画面設定
                'Call mSetDisplay(mudtSetOpsGraphWork)

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

            If MessageBox.Show("May I remove this graph? ", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If optGraph.Checked Then

                    ''-------------------
                    '' ３種グラフ
                    ''-------------------
                    Dim intRowIndex As Integer = grdGraph.CurrentCell.RowIndex
                    Dim intGraphType As Integer = CCInt(grdGraph.CurrentRow.Cells("cmbGraphType").Value)

                    ''各グラフの構造体データをクリア
                    Call mDeleteSetDataDetail(mudtSetOpsGraphWork, intGraphType, intRowIndex)

                    ''グラフタイトルの構造体データをクリア
                    Call gInitOpsGraphTitle(intRowIndex, mudtSetOpsGraphWork.udtGraphTitleRec(intRowIndex))

                    ''前回値のクリア
                    If optMachinery.Checked Then mintGraphTypeMach(intRowIndex) = gCstCodeOpsTitleGraphTypeNothing
                    If optCargo.Checked Then mintGraphTypeCarg(intRowIndex) = gCstCodeOpsTitleGraphTypeNothing

                    ''表示更新
                    Call mSetDisplay(mudtSetOpsGraphWork)

                    ''フッターボタンの使用可/不可設定
                    Call mSetControlFooterBtn()


                ElseIf optFree.Checked Then
                    ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto

                    ' ''-------------------
                    ' '' フリーグラフ
                    ' ''-------------------
                    'Dim intOpsIndex As Integer = 0
                    'Dim intRowIndex As Integer = 0

                    ' ''OPS番号と設定番号を取得
                    'Call mGetIndexFreeOpsSet(intOpsIndex, intRowIndex)

                    ' ''設定を初期化
                    'Call gInitOpsGraphFree(intOpsIndex, intRowIndex, mudtSetOpsGraphWork.udtGraphFreeRec(intOpsIndex).udtFreeGraphTitle(intRowIndex))

                    ' ''表示更新
                    'Call mSetDisplay(mudtSetOpsGraphWork)

                End If

            End If

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

            If MessageBox.Show("May I insert a graph here? ", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If optGraph.Checked Then ''３種グラフ

                    ''選択行から１行づつ下にずらしてコピー
                    For i As Integer = grdGraph.RowCount - 1 To grdGraph.CurrentCell.RowIndex Step -1

                        ''Machinery情報
                        If optMachinery.Checked Then Call mAddRowInfo(True, mudtSetOpsGraphWork, mintGraphTypeMach, i)

                        ''Cargo情報
                        If optCargo.Checked Then Call mAddRowInfo(False, mudtSetOpsGraphWork, mintGraphTypeCarg, i)

                    Next

                    ''表示更新
                    Call mSetDisplay(mudtSetOpsGraphWork)

                ElseIf optFree.Checked Then '' フリーグラフ
                    ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto

                    'Dim intOpsIndex As Integer = 0
                    'Dim intRowIndex As Integer = 0

                    ' ''OPS番号と設定番号を取得
                    'Call mGetIndexFreeOpsSet(intOpsIndex, intRowIndex)

                    ' ''選択行から１行づつ下にずらしてコピー
                    'For i As Integer = mgrdFree(intOpsIndex).RowCount - 1 To intRowIndex + 1 Step -1

                    '    With mudtSetOpsGraphWork.udtGraphFreeRec(intOpsIndex)
                    '        Call mCopyOpsFree(.udtFreeGraphTitle(i - 1), .udtFreeGraphTitle(i), False)
                    '    End With

                    'Next

                    ' ''表示更新
                    'Call mSetDisplay(mudtSetOpsGraphWork)

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Analog Meter Details ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdAnalogMater_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnalogMater.Click

        Try

            If frmOpsGraphAnalogMeterDetail.gShow(mudtSetOpsGraphWork.udtGraphAnalogMeterSettingRec, Me) = 0 Then

                ''設定値を比較用構造体に格納
                Call mSetStructureInfo()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： Saveボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try
            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を作業用構造体に格納
            Call mGetGridInfo(mudtSetOpsGraphWork.udtGraphTitleRec)

            ''設定値の保存
            If optMachinery.Checked Then Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewMach)
            If optCargo.Checked Then Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewCarg)

            ''データが変更されているかチェック
            blnMach = mChkStructureEquals(mudtSetOpsGraphNewMach, gudt.SetOpsGraphM)
            blnCarg = mChkStructureEquals(mudtSetOpsGraphNewCarg, gudt.SetOpsGraphC)

            ''データが変更されている場合
            If (Not blnMach) Or (Not blnCarg) Then

                ''変更された場合は設定を更新する
                If Not blnMach Then Call mCopyStructure(mudtSetOpsGraphNewMach, gudt.SetOpsGraphM)
                If Not blnCarg Then Call mCopyStructure(mudtSetOpsGraphNewCarg, gudt.SetOpsGraphC)

                ''グラフ設定データをプルダウンメニューへ自動反映させる
                Call mImportGraphListData()

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphC = 1
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphC = 1
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainC = 1
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainC = 1

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： Exitボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Clearクリック
    ' 返り値    : なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        Try
            mblClearFlg = True

            Me.Close()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    '--------------------------------------------------------------------
    ' 機能      : ｸﾞﾗﾌ設定ｸﾘｱ
    '--------------------------------------------------------------------
    Private Sub ClearGraphSetting(ByRef udtSetOpsGraph As gTypSetOpsGraph)

        Dim i As Integer
        Dim j As Integer

        With udtSetOpsGraph
            ''３種用グラフタイトル設定
            For i = LBound(udtSetOpsGraph.udtGraphTitleRec) To UBound(udtSetOpsGraph.udtGraphTitleRec)
                With .udtGraphTitleRec(i)
                    .bytNo = i + 1    ''グラフ番号(1～16)     '' Ver1.10.5 2016.05.09 0 → 番号を保存
                    .bytType = 0  ''グラフタイプ
                    .strSpare = ""    ''予備
                    .strName = ""     ''グラフ名称(半角32文字)
                End With
            Next

            ''偏差グラフ（排気ガスグラフ）設定
            For i = LBound(udtSetOpsGraph.udtGraphExhaustRec) To UBound(udtSetOpsGraph.udtGraphExhaustRec)
                With .udtGraphExhaustRec(i)
                    .bytNo = i + 1   ''グラフ番号(1～16)   '' Ver1.10.5 2016.05.09 0 → 番号を保存
                    .strSpare = ""      ''予備
                    .strTitle = ""      ''グラフタイトル(半角32文字)
                    .strItemUp = ""     ''グラフデータ名称（上段）(半角4文字)
                    .strItemDown = ""   ''グラフデータ名称（下段）(半角4文字)
                    .shtAveCh = 0       ''平均CH
                    .bytDevMark = 0     ''偏差目盛の上下限値(0～255)
                    .bytSpare = 0
                    .bytLine = 0        ''数値の分け方
                    .bytCyCnt = 0       ''シリンダの数
                    .strSpare2 = ""

                    ''シリンダグラフ情報
                    For j = LBound(.udtCylinder) To UBound(.udtCylinder)
                        .udtCylinder(j).shtChCylinder = 0     ''シリンダのCH番号
                        .udtCylinder(j).shtChDeviation = 0     ''偏差のCH番号
                        .udtCylinder(j).strTitle = ""           ''名称
                    Next

                    .strTcTitle = ""    ''T/Cグラフのタイトル
                    .strTcComm1 = ""    ''T/Cグラフのコメント1
                    .strTcComm2 = ""    ''T/Cグラフのコメント2
                    .bytTcCnt = 0       ''T/Cの数(1～8)
                    .strSpare3 = ""

                    ''T/Cグラフ情報
                    For j = LBound(.udtTurboCharger) To UBound(.udtTurboCharger)
                        .udtTurboCharger(j).shtChTurboCharger = 0   ''T/CのCH番号
                        .udtTurboCharger(j).strTitle = ""           ''T/CのCH番号に対する名称
                        .udtTurboCharger(j).bytSplitLine = 0        ''区切り線
                    Next
                End With
            Next

            ''バーグラフ設定
            For i = LBound(.udtGraphBarRec) To UBound(.udtGraphBarRec)
                With .udtGraphBarRec(i)
                    .bytNo = i + 1          ''グラフ番号(1～16)   '' Ver1.10.5 2016.05.09 0 → 番号を保存
                    .strSpare = ""
                    .strTitle = ""      ''グラフタイトル
                    .strItemUp = ""     ''グラフデータ名称（上段）
                    .strItemDown = ""   ''グラフデータ名称（下段）
                    .bytDisplay = 0     ''表示切替指定
                    .bytLine = 0        ''数値の分け方
                    .bytDevision = 0    ''分割数
                    .bytCyCnt = 0       ''シリンダの数


                    For j = LBound(.udtCylinder) To UBound(.udtCylinder)
                        .udtCylinder(j).shtChCylinder = 0   ''シリンダのCH番号
                        .udtCylinder(j).strTitle = ""       ''シリンダのCH番号に対する名称
                    Next
                End With
            Next

            ''アナログメーター
            For i = LBound(.udtGraphAnalogMeterRec) To UBound(.udtGraphAnalogMeterRec)
                With .udtGraphAnalogMeterRec(i)
                    .bytNo = i + 1          ''グラフ番号(1～16)   '' Ver1.10.5 2016.05.09 0 → 番号を保存
                    .bytMeterType = 0   ''表示タイプ
                    .strSpare = ""
                    .strTitle = ""      ''グラフタイトル

                    For j = LBound(.udtDetail) To UBound(.udtDetail)
                        .udtDetail(j).shtChNo = 0   ''CH番号
                        .udtDetail(j).bytScale = 0  ''目盛り分割数
                        .udtDetail(j).bytColor = 0  ''表示色
                    Next
                End With
            Next

            ''アナログメーター設定
            With .udtGraphAnalogMeterSettingRec
                .bytChNameDisplayPoint = 0  ''CH名称表示位置
                .bytChNameDisplayPoint = 0  ''CH名称表示位置
                .bytMarkNumericalValue = 0  ''目盛数値表示方法
                .bytPointerFrame = 0        ''指針の縁取り
                .bytPointerColorChange = 0  ''指針の色変更
                .strSpare = ""              ''予備
            End With


        End With

    End Sub


    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '--------------------------------------------------------------------
    Private Sub frmExtEccDeadmanAlarm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try
            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            ''グリッドの保留中の変更を全て適用させる
            Call grdGraph.EndEdit()

            ''設定値を作業用構造体に格納
            Call mGetGridInfo(mudtSetOpsGraphWork.udtGraphTitleRec)

            If mblClearFlg = True Then
                Call ClearGraphSetting(mudtSetOpsGraphNewMach)
                Call ClearGraphSetting(mudtSetOpsGraphNewCarg)
                Call ClearGraphSetting(mudtSetOpsGraphWork)
                mblClearFlg = False
            End If



            ''設定値の保存
            If optMachinery.Checked Then Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewMach)
            If optCargo.Checked Then Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewCarg)

            ''データが変更されているかチェック
            blnMach = mChkStructureEquals(mudtSetOpsGraphNewMach, gudt.SetOpsGraphM)
            blnCarg = mChkStructureEquals(mudtSetOpsGraphNewCarg, gudt.SetOpsGraphC)

            ''データが変更されている場合
            If (Not blnMach) Or (Not blnCarg) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        '入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更されている場合は設定を更新する
                        If Not blnMach Then Call mCopyStructure(mudtSetOpsGraphNewMach, gudt.SetOpsGraphM)
                        If Not blnCarg Then Call mCopyStructure(mudtSetOpsGraphNewCarg, gudt.SetOpsGraphC)

                        ''グラフ設定データをプルダウンメニューへ自動反映させる
                        Call mImportGraphListData()

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphM = 1
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphC = 1
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphM = 1
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphC = 1
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainM = 1
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainC = 1
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainM = 1
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainC = 1

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
    Private Sub frmOpsGraphList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "グリッドイベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： プルダウンリストの項目を変更した時の処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdGraph_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGraph.CellValueChanged

        Try

            ''処理を抜ける条件
            If mblnInitFlg Then Return ''初期化中は処理しない
            If e.RowIndex < 0 Or e.RowIndex > grdGraph.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdGraph.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            ''グラフタイプの種類を変更した時
            If grdGraph.CurrentCell.OwningColumn.Name = "cmbGraphType" Then

                If optMachinery.Checked Then

                    ''前回値の削除
                    Call mDeleteSetData(True, mintGraphTypeMach)

                    ''設定したグラフタイプの取得
                    Call mGetGridInfo(mudtSetOpsGraphWork.udtGraphTitleRec)

                    ''表示更新
                    Call mRefreshGraphData(mudtSetOpsGraphWork.udtGraphTitleRec)

                ElseIf optCargo.Checked Then

                    ''前回値の削除
                    Call mDeleteSetData(False, mintGraphTypeCarg)

                    ''設定したグラフタイプの取得
                    Call mGetGridInfo(mudtSetOpsGraphWork.udtGraphTitleRec)

                    ''表示更新
                    Call mRefreshGraphData(mudtSetOpsGraphWork.udtGraphTitleRec)

                End If

                ''フッターボタンの使用可/不可設定
                Call mSetControlFooterBtn()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッドダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub grdGraph_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
                            Handles grdGraph.CellDoubleClick, grdFree1.CellDoubleClick, grdFree2.CellDoubleClick, _
                                    grdFree3.CellDoubleClick, grdFree4.CellDoubleClick, grdFree5.CellDoubleClick, _
                                    grdFree6.CellDoubleClick, grdFree7.CellDoubleClick, grdFree8.CellDoubleClick, _
                                    grdFree9.CellDoubleClick, grdFree10.CellDoubleClick

        Try

            ''行数が0より小さい場合、もしくは最大行数より大きい場合は処理を抜ける
            If e.RowIndex < 0 Or e.RowIndex > grdGraph.RowCount - 1 Then Return

            ''コンボボックス列をダブルクリックした場合は処理を抜ける
            If optGraph.Checked Then

                ''排ガス、バーグラフ、アナログメーター画面
                If gChkCellIsCmb(grdGraph.CurrentCell.OwningColumn.Name) Then Return

            End If

            ''Editボタンクリックイベントを呼び出す
            Call cmdEdit_Click(cmdEdit, New EventArgs)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グリッド行ヘッダーダブルクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub grdGraph_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdGraph.RowHeaderMouseDoubleClick

        Try

            ''行数が0より小さい場合、もしくは最大行数より大きい場合は処理を抜ける
            If e.RowIndex < 0 Or e.RowIndex > grdGraph.RowCount - 1 Then Return

            ''Editボタンのクリックイベントを呼び出す
            Call cmdEdit_Click(cmdEdit, New EventArgs)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 行変更の感知（クリック、矢印キー）
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub grdGraph_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGraph.RowValidated

        Try

            ''処理を抜ける条件
            If mblnInitFlg Then Return ''初期化中は処理しない
            If e.RowIndex < 0 Or e.RowIndex > grdGraph.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdGraph.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            ''行変更の感知（行Indexを得るためにTimerをかませる）
            Call Timer1.Start()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region



#End Region

#Region "内部関数"


#Region "グラフ設定データをプルダウンメニューへ自動反映させる"

    '--------------------------------------------------------------------
    ' 機能      : グラフ設定データの自動反映処理
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : グラフ設定データをプルダウンメニューへ自動反映させる
    '--------------------------------------------------------------------
    Private Sub mImportGraphListData()

        Try

            ''グラフ設定データを取得する
            Call gMakeOpsDataStructure(True, mudtGraphDataMach)
            Call gMakeOpsDataStructure(False, mudtGraphDataCarg)

            ''取得したデータをプルダウンメニューへ自動反映させる
            Call mConvertStructureData(gudt.SetOpsPulldownMenuM, mudtGraphDataMach)
            'Ver2.0.6.5
            Call ViewData(gudt.SetOpsPulldownMenuM)


            ''Ext.VDU用メニューにも反映　2014.03.12
            'Call mConvertStructureData(gudt.SetOpsPulldownMenuC, mudtGraphDataCarg)
            Call mConvertStructureData(gudt.SetOpsPulldownMenuC, mudtGraphDataMach)
            'Ver2.0.6.5
            Call ViewData(gudt.SetOpsPulldownMenuC)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : グラフ設定データをプルダウンメニューへ自動反映
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) プルダウンメニュー構造体
    '           : ARG2 - (I ) グラフ設定データインポート用構造体
    ' 機能説明  : インポート用構造体からグローバル構造体へデータコンバート
    '--------------------------------------------------------------------
    Private Sub mConvertStructureData(ByRef udtPullDownNew As gTypSetOpsPulldownMenu, _
                                      ByVal udtGraphData As gTypImportGraphData)

        Try

            Dim i As Integer
            Dim blnSetDataGraph As Boolean = False   ''設定済データ有無判定フラグ（３種グラフ）
            Dim blnSetDataFree As Boolean = False    ''設定済データ有無判定フラグ（フリーグラフ）

            Dim GMainName As String                  'ｸﾞﾗﾌ名称
            Dim GMainNameMoji As String              'ｸﾞﾗﾌ名称(抜き出し)
            Dim GMainNameLen As Integer              '名称長さ
            Dim j As Integer

            Dim GMainNameMojiASC As Integer              'ｸﾞﾗﾌ名称(抜き出し)

            ''=================================================
            '' 設定済データに上書きインポート
            ''=================================================
            For i = 0 To UBound(udtPullDownNew.udtDetail)

                GMainName = ""

                GMainNameLen = Len(udtPullDownNew.udtDetail(i).strName)

                For j = 1 To GMainNameLen
                    GMainNameMoji = Mid(udtPullDownNew.udtDetail(i).strName, j, 1)

                    GMainNameMojiASC = Asc(GMainNameMoji)

                    If GMainNameMojiASC <> 0 Then
                        GMainName = GMainName + GMainNameMoji
                    End If
                Next

                ''設定済のプルダウンメニューがある場合
                If GMainName = "GRAPH" Then

                    ''設定済の３種グラフデータがある場合
                    If udtGraphData.intUseCountGraph <> 0 Then
                        Call mSetGraphData(udtPullDownNew.udtDetail(i), udtGraphData, i)       ''データコンバート
                        blnSetDataGraph = True                                              ''設定済データ有無判定フラグを有効にする
                    End If

                ElseIf udtPullDownNew.udtDetail(i).bytMenuNo1 = gCstCodeOpsPullDownMenuFree Then

                    ''設定済のフリーグラフデータがある場合
                    If udtGraphData.intUseCountFree <> 0 Then
                        Call mSetFreeGraphData(udtPullDownNew.udtDetail(i), udtGraphData)   ''データコンバート
                        blnSetDataFree = True                                               ''設定済データ有無判定フラグを有効にする
                    End If

                End If

            Next

            ''=================================================
            '' 設定済データがない場合は空いている行に新規設定
            ''=================================================

            ' ''３種グラフ
            'If Not blnSetDataGraph Then

            '    For i = 0 To UBound(udtPullDownNew.udtDetail)

            '        ''空いている行がないかチェック
            '        If udtPullDownNew.udtDetail(i).bytMenuNo1 = gCstCodeOpsPullDownMenuNothing Then

            '            If udtGraphData.intUseCountGraph <> 0 Then

            '                udtPullDownNew.udtDetail(i).bytMenuNo1 = gCstCodeOpsPullDownMenuGraph
            '                udtPullDownNew.udtDetail(i).strName = gGetComboItemName(gCstCodeOpsPullDownMenuGraph, gEnmComboType.ctOpsPulldownColumn1)
            '                udtPullDownNew.udtDetail(i).bytMenuType = gCstCodeOpsPullDownTypeSub
            '                udtPullDownNew.udtDetail(i).bytMenuSet = udtGraphData.intUseCountGraph
            '                Call mSetGraphData(udtPullDownNew.udtDetail(i), udtGraphData)

            '                Exit For

            '            End If

            '        End If

            '    Next i

            '    ''判定フラグを落とす
            '    blnSetDataGraph = False

            'End If

            ''フリーグラフ
            If Not blnSetDataFree Then

                For i = 0 To UBound(udtPullDownNew.udtDetail)

                    ''空いている行がないかチェック
                    If udtPullDownNew.udtDetail(i).bytMenuNo1 = gCstCodeOpsPullDownMenuNothing Then

                        If udtGraphData.intUseCountFree <> 0 Then

                            udtPullDownNew.udtDetail(i).bytMenuNo1 = gCstCodeOpsPullDownMenuFree
                            udtPullDownNew.udtDetail(i).strName = gGetComboItemName(gCstCodeOpsPullDownMenuFree, gEnmComboType.ctOpsPulldownColumn1)
                            udtPullDownNew.udtDetail(i).bytMenuType = gCstCodeOpsPullDownTypeSub
                            udtPullDownNew.udtDetail(i).bytMenuSet = udtGraphData.intUseCountGraph
                            Call mSetFreeGraphData(udtPullDownNew.udtDetail(i), udtGraphData)

                            Exit For

                        End If

                    End If

                Next i

                ''判定フラグを落とす
                blnSetDataFree = False

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ３種グラフデータの読込み
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) プルダウンメニュー構造体
    '           : ARG2 - (I ) グラフ設定データの中身を持つ構造体
    ' 機能説明  : OPS設定画面の３種グラフデータを読み込む
    '--------------------------------------------------------------------
    Private Sub mSetGraphData(ByRef hudtPulldownMenu As gTypSetOpsPulldownMenuRec, _
                              ByVal hudtGraphData As gTypImportGraphData, ByVal Line As Integer)

        Try
            'プルダウン仕様変更のため一部修正　T.Ueki

            Dim intRowCnt As Integer = 0
            Dim intMojiLen As Integer = 0

            hudtPulldownMenu.bytMenuType = gCstCodeOpsPullDownTypeSub
            hudtPulldownMenu.udtGroup(0).bytCount = hudtGraphData.intUseCountGraph

            For i As Integer = 0 To UBound(hudtGraphData.udtOpsGraph)

                intMojiLen = Len(hudtGraphData.udtOpsGraph(i).strGraphName)
                If intMojiLen <> 0 Then
                    '' 文字の後ろをNULLで埋める   2014.03.11
                    hudtPulldownMenu.udtGroup(0).udtSub(intRowCnt).strName = MojiMake(hudtGraphData.udtOpsGraph(i).strGraphName, 32)  ''SubMenu
                    hudtPulldownMenu.udtGroup(0).udtSub(intRowCnt).ViewNo1 = ViewDataExchange(intRowCnt + 51)           ''ScreenNo

                    'T.Ueki 処理項目1～4を「1」「6」「1」「0」に強制変更
                    hudtPulldownMenu.udtGroup(0).udtSub(intRowCnt).SubbytMenuType1 = CCbyte("1")                        ''処理項目1
                    hudtPulldownMenu.udtGroup(0).udtSub(intRowCnt).SubbytMenuType2 = CCbyte("6")                        ''処理項目2
                    hudtPulldownMenu.udtGroup(0).udtSub(intRowCnt).SubbytMenuType3 = CCbyte("1")                        ''処理項目3
                    hudtPulldownMenu.udtGroup(0).udtSub(intRowCnt).SubbytMenuType4 = CCbyte("0")                        ''処理項目4
                    intRowCnt += 1
                End If

            Next

            'Dim intRowCnt As Integer = 0

            'hudtPulldownMenu.bytMenuType = gCstCodeOpsPullDownTypeSub
            'hudtPulldownMenu.bytMenuSet = hudtGraphData.intUseCountGraph

            'For i As Integer = 0 To UBound(hudtGraphData.udtOpsGraph)

            '    ''空欄行は上段へ詰める
            '    If hudtGraphData.udtOpsGraph(i).intType <> gCstCodeOpsTitleGraphTypeNothing Then

            '        hudtPulldownMenu.udtGroup(0).udtSub(intRowCnt).strName = hudtGraphData.udtOpsGraph(i).strGraphName  ''SubMenu
            '        hudtPulldownMenu.udtGroup(0).udtSub(intRowCnt).ViewNo1 = intRowCnt + 1                          ''ScreenNo
            '        intRowCnt += 1

            '    End If

            'Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#Region "グラフ更新に伴うメニューの座標更新処理"
    'Ver2.0.6.5 ﾒﾆｭｰの座標更新処理
    'メニューデータ表示位置設定処理
    Private Sub ViewData(ByVal udtView As gTypSetOpsPulldownMenu)

        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim dd As Integer

        'メインメニューの座標設定()
        For i = 0 To Main_Menu_Max - 1

            udtView.udtDetail(i).tx = Main_Menu_Left + Main_Menu_DX * i
            udtView.udtDetail(i).ty = TitleDY
            udtView.udtDetail(i).bx = Main_Menu_Left + Main_Menu_DX * (i + 1)
            udtView.udtDetail(i).by = TitleDY + Main_Menu_DY

            '右5つの場合、ｸﾞﾙｰﾌﾟﾒﾆｭｰは右揃え
            If i > 6 Then
                udtView.udtDetail(i).groupviewx = Main_Menu_Left + Main_Menu_DX * (i + 1) - Group_Menu_DX
            Else
                udtView.udtDetail(i).groupviewx = Main_Menu_Left + Main_Menu_DX * i
            End If

            udtView.udtDetail(i).groupviewy = TitleDY + Main_Menu_DY

            udtView.udtDetail(i).groupsizex = Group_Menu_DX
            udtView.udtDetail(i).groupsizey = Group_Menu_DY * udtView.udtDetail(i).bytMenuSet

            ' グループメニュー座標を設定
            For j = 0 To Group_Menu_Max - 1
                udtView.udtDetail(i).udtGroup(j).grouptx = 0
                udtView.udtDetail(i).udtGroup(j).groupty = Group_Menu_DY * j
                udtView.udtDetail(i).udtGroup(j).groupbx = Group_Menu_DX
                udtView.udtDetail(i).udtGroup(j).groupby = Group_Menu_DY * (j + 1)

                If (udtView.udtDetail(i).bytMenuType = 1) Then     ' グループメニュー
                    'ｻﾌﾞﾒﾆｭｰは右揃え
                    If i > 6 Then
                        udtView.udtDetail(i).udtGroup(j).Subviewx = udtView.udtDetail(i).groupviewx - Sub_Menu_DX
                    Else
                        dd = Group_Menu_DX + Main_Menu_DX * i
                        If dd > Win7CXPOS Then
                            dd = Main_Menu_DX * i - Group_Menu_DX
                        End If
                        udtView.udtDetail(i).udtGroup(j).Subviewx = Main_Menu_Left + dd
                    End If

                ElseIf udtView.udtDetail(i).bytMenuType = 2 Or udtView.udtDetail(i).bytMenuType = 3 Then ' サブメニュー
                    '右5つの場合、ｻﾌﾞﾒﾆｭｰは右揃えとする
                    If i > 6 Then
                        dd = Main_Menu_Left + Main_Menu_DX * (i + 1) - Sub_Menu_DX
                    Else
                        dd = Main_Menu_DX * i + Main_Menu_Left
                        If dd + Sub_Menu_DX > Win7CXPOS Then
                            dd = Win7CXPOS - Sub_Menu_DX - Main_Menu_Left
                        End If
                    End If

                    udtView.udtDetail(i).udtGroup(j).Subviewx = dd
                Else                            ' メニューなし、MAINONLY
                    udtView.udtDetail(i).udtGroup(j).Subviewx = 0
                End If
                '                          
                udtView.udtDetail(i).udtGroup(j).Subviewy = TitleDY + Main_Menu_DY + Group_Menu_DY * j

                udtView.udtDetail(i).udtGroup(j).Subsizex = Sub_Menu_DX
                udtView.udtDetail(i).udtGroup(j).Subsizey = Sub_Menu_DY * udtView.udtDetail(i).udtGroup(j).bytCount

                ' サブメニュー座標を設定
                For k = 0 To Sub_Menu_Max - 1
                    udtView.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx = 0
                    udtView.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty = Sub_Menu_DY * k
                    udtView.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx = Sub_Menu_DX
                    udtView.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby = Sub_Menu_DY * (k + 1)

                    If (k >= udtView.udtDetail(i).udtGroup(j).bytCount) Then
                        udtView.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 = 0
                    End If
                Next k
            Next j
        Next i

        ' 不必要なデータをクリア
        Call data_clear(udtView)

        For i = 0 To Main_Menu_Max - 1
            udtView.udtDetail(i).strName = MojiMake(udtView.udtDetail(i).strName, Main_Menu_Byte)
            For j = 0 To Group_Menu_Max - 1
                udtView.udtDetail(i).udtGroup(j).strName = MojiMake(udtView.udtDetail(i).udtGroup(j).strName, Group_Menu_Byte)
                For k = 0 To Sub_Menu_Max - 1
                    udtView.udtDetail(i).udtGroup(j).udtSub(k).strName = MojiMake(udtView.udtDetail(i).udtGroup(j).udtSub(k).strName, Sub_Menu_Byte)
                Next k
            Next j
        Next i

    End Sub
    'メニューデータのメニュータイプを見て不要なメニューデータをクリア
    Private Sub data_clear(ByVal DataClear As gTypSetOpsPulldownMenu)
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer

        ' 不必要なデータをクリア
        For i = 0 To Main_Menu_Max - 1

            If DataClear.udtDetail(i).bytMenuType = 1 Then       ' グループメニュー
                For j = DataClear.udtDetail(i).bytMenuSet To Group_Menu_Max - 1
                    Call init_menu_data(2, i, j, 0, DataClear)
                Next j

                For j = 0 To Group_Menu_Max - 1     ' グループメニューのサブメニュー
                    For k = DataClear.udtDetail(i).udtGroup(j).bytCount To Sub_Menu_Max - 1
                        Call init_menu_data(3, i, j, k, DataClear)
                    Next k
                Next j

            ElseIf DataClear.udtDetail(i).bytMenuType = 2 Or DataClear.udtDetail(i).bytMenuType = 3 Then
                For j = 1 To Group_Menu_Max - 1
                    Call init_menu_data(2, i, j, 0, DataClear)
                Next j

                For j = DataClear.udtDetail(i).bytMenuSet To Group_Menu_Max - 1
                    Call init_menu_data(4, i, j, 0, DataClear)
                Next j

                For j = DataClear.udtDetail(i).udtGroup(0).bytCount To Sub_Menu_Max - 1
                    Call init_menu_data(3, i, 0, j, DataClear)
                Next j
            Else                            ' メニューなし、MAINONLY
                Call init_menu_data(1, i, 0, 0, DataClear)
            End If

        Next i
    End Sub
    'メニューデータを初期化（メニュー毎、グループ毎、サブ毎のそれぞれ）
    Private Sub init_menu_data(ByVal fg As Integer, ByVal m As Integer, ByVal g As Integer, ByVal s As Integer, ByVal initDataClear As gTypSetOpsPulldownMenu)

        Dim i As Integer
        Dim j As Integer
        Dim k As Integer

        If fg = 0 Then      ' 全クリア
            For i = 0 To Main_Menu_Max - 1
                initDataClear.udtDetail(i).strName = MojiMake("", 12)
                initDataClear.udtDetail(i).bytMenuType = 0
                initDataClear.udtDetail(i).tx = 0
                initDataClear.udtDetail(i).ty = 0
                initDataClear.udtDetail(i).bx = 0
                initDataClear.udtDetail(i).by = 0
                initDataClear.udtDetail(i).bytMenuSet = 0
                initDataClear.udtDetail(i).groupviewx = 0
                initDataClear.udtDetail(i).groupviewy = 0
                initDataClear.udtDetail(i).groupsizex = 0
                initDataClear.udtDetail(i).groupsizey = 0

                For j = 0 To Group_Menu_Max - 1
                    initDataClear.udtDetail(i).udtGroup(j).strName = MojiMake("", 24)
                    initDataClear.udtDetail(i).udtGroup(j).groupbytMenuType = 0
                    initDataClear.udtDetail(i).udtGroup(j).grouptx = 0
                    initDataClear.udtDetail(i).udtGroup(j).groupty = 0
                    initDataClear.udtDetail(i).udtGroup(j).groupbx = 0
                    initDataClear.udtDetail(i).udtGroup(j).groupby = 0
                    initDataClear.udtDetail(i).udtGroup(j).bytCount = 0
                    initDataClear.udtDetail(i).udtGroup(j).Subviewx = 0
                    initDataClear.udtDetail(i).udtGroup(j).Subviewy = 0
                    initDataClear.udtDetail(i).udtGroup(j).Subsizex = 0
                    initDataClear.udtDetail(i).udtGroup(j).Subsizey = 0

                    For k = 0 To Sub_Menu_Max - 1
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).strName = MojiMake("", 32)
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4 = 0
                        initDataClear.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode = 0
                    Next k
                Next j
            Next i

        ElseIf fg = 1 Then          '  指定メニュークリア
            initDataClear.udtDetail(m).bytMenuSet = 0
            initDataClear.udtDetail(m).groupviewx = 0
            initDataClear.udtDetail(m).groupviewy = 0
            initDataClear.udtDetail(m).groupsizex = 0
            initDataClear.udtDetail(m).groupsizey = 0

            For j = 0 To Group_Menu_Max - 1
                initDataClear.udtDetail(m).udtGroup(j).strName = MojiMake("", 24)
                initDataClear.udtDetail(m).udtGroup(j).groupbytMenuType = 0
                initDataClear.udtDetail(m).udtGroup(j).grouptx = 0
                initDataClear.udtDetail(m).udtGroup(j).groupty = 0
                initDataClear.udtDetail(m).udtGroup(j).groupbx = 0
                initDataClear.udtDetail(m).udtGroup(j).groupby = 0
                initDataClear.udtDetail(m).udtGroup(j).bytCount = 0
                initDataClear.udtDetail(m).udtGroup(j).Subviewx = 0
                initDataClear.udtDetail(m).udtGroup(j).Subviewy = 0
                initDataClear.udtDetail(m).udtGroup(j).Subsizex = 0
                initDataClear.udtDetail(m).udtGroup(j).Subsizey = 0

                For k = 0 To Sub_Menu_Max - 1
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).strName = MojiMake("", 32)
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubbytMenuType1 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubbytMenuType2 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubbytMenuType3 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubbytMenuType4 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubMenutx = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubMenuty = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubMenubx = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).SubMenuby = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).ViewNo1 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).ViewNo2 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).ViewNo3 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).ViewNo4 = 0
                    initDataClear.udtDetail(m).udtGroup(j).udtSub(k).bytKeyCode = 0
                Next k
            Next j

        ElseIf fg = 2 Then          '  指定グループクリア
            initDataClear.udtDetail(m).udtGroup(g).strName = MojiMake("", 24)
            initDataClear.udtDetail(m).udtGroup(g).groupbytMenuType = 0
            initDataClear.udtDetail(m).udtGroup(g).grouptx = 0
            initDataClear.udtDetail(m).udtGroup(g).groupty = 0
            initDataClear.udtDetail(m).udtGroup(g).groupbx = 0
            initDataClear.udtDetail(m).udtGroup(g).groupby = 0
            initDataClear.udtDetail(m).udtGroup(g).bytCount = 0
            initDataClear.udtDetail(m).udtGroup(g).Subviewx = 0
            initDataClear.udtDetail(m).udtGroup(g).Subviewy = 0
            initDataClear.udtDetail(m).udtGroup(g).Subsizex = 0
            initDataClear.udtDetail(m).udtGroup(g).Subsizey = 0

            For k = 0 To Sub_Menu_Max - 1
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).strName = MojiMake("", 32)
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubbytMenuType1 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubbytMenuType2 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubbytMenuType3 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubbytMenuType4 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubMenutx = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubMenuty = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubMenubx = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).SubMenuby = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).ViewNo1 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).ViewNo2 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).ViewNo3 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).ViewNo4 = 0
                initDataClear.udtDetail(m).udtGroup(g).udtSub(k).bytKeyCode = 0
            Next k

        ElseIf fg = 3 Then          '  指定サブメニュークリア
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).strName = MojiMake("", 32)
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubbytMenuType1 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubbytMenuType2 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubbytMenuType3 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubbytMenuType4 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubMenutx = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubMenuty = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubMenubx = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).SubMenuby = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).ViewNo1 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).ViewNo2 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).ViewNo3 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).ViewNo4 = 0
            initDataClear.udtDetail(m).udtGroup(g).udtSub(s).bytKeyCode = 0

        ElseIf fg = 4 Then
            initDataClear.udtDetail(m).bytMenuSet = 0
            initDataClear.udtDetail(m).groupviewx = 0
            initDataClear.udtDetail(m).groupviewy = 0
            initDataClear.udtDetail(m).groupsizex = 0
            initDataClear.udtDetail(m).groupsizey = 0

            For j = 0 To Group_Menu_Max - 1
                initDataClear.udtDetail(m).udtGroup(j).strName = MojiMake("", 24)
                initDataClear.udtDetail(m).udtGroup(j).groupbytMenuType = 0
                initDataClear.udtDetail(m).udtGroup(j).grouptx = 0
                initDataClear.udtDetail(m).udtGroup(j).groupty = 0
                initDataClear.udtDetail(m).udtGroup(j).groupbx = 0
                initDataClear.udtDetail(m).udtGroup(j).groupby = 0
            Next j

        End If
    End Sub
#End Region



    '--------------------------------------------------------------------
    ' 機能      : フリーグラフデータの読込み
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) プルダウンメニュー構造体
    '           : ARG2 - (I ) グラフ設定データの中身を持つ構造体
    ' 機能説明  : OPS設定画面のフリーグラフデータを読み込む
    '--------------------------------------------------------------------
    Private Sub mSetFreeGraphData(ByRef hudtPulldownMenu As gTypSetOpsPulldownMenuRec, _
                                  ByVal hudtGraphData As gTypImportGraphData)

        Try

            Dim intRowCnt As Integer = 0
            Dim intRowCntSub As Integer = 0

            hudtPulldownMenu.bytMenuType = gCstCodeOpsPullDownTypeGroup
            hudtPulldownMenu.bytMenuSet = hudtGraphData.intUseCountFree

            ''------------------------
            '' Tab毎の設定取得
            ''------------------------
            For i As Integer = 0 To gCstCodeFreeGraphTabCnt - 1

                ''Group画面
                If hudtGraphData.udtOpsFree(i).intUseCount <> 0 Then

                    hudtPulldownMenu.udtGroup(intRowCnt).strName = "OPS FreeGraph - " & i + 1
                    hudtPulldownMenu.udtGroup(intRowCnt).bytCount = hudtGraphData.udtOpsFree(i).intUseCount

                    ''Sub画面
                    For j As Integer = 0 To UBound(hudtGraphData.udtOpsFree(i).udtFreeGraphTitle)

                        With hudtPulldownMenu.udtGroup(intRowCnt).udtSub(intRowCntSub)

                            ''空欄行は上段へ詰める
                            If hudtGraphData.udtOpsFree(i).udtFreeGraphTitle(j).strGraphTitle <> "" Then

                                .strName = hudtGraphData.udtOpsFree(i).udtFreeGraphTitle(j).strGraphTitle   ''SubMenu
                                .viewno1 = intRowCntSub + 1                                             ''ScreenNo
                                intRowCntSub += 1

                            End If

                        End With

                    Next j

                    intRowCnt += 1      ''Group画面のカウントインクリメント
                    intRowCntSub = 0    ''Sub画面のカウント初期化

                End If

            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


    '--------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： ARG1 - (IO) グリッドのコントロール配列
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mInitialDataGrid(ByRef grdFree() As DataGridView)

        Try

            Dim i As Integer, j As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim Column1 As New DataGridViewComboBoxColumn : Column1.Name = "cmbGraphType"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtGraphTitle" : Column2.ReadOnly = True

            With grdGraph

                ''列
                .Columns.Clear()
                .Columns.Add(Column1) : .Columns.Add(Column2)
                .AllowUserToResizeColumns = False   ''列幅の変更不可

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "Graph Type" : .Columns(0).Width = 180
                .Columns(1).HeaderText = "Graph Title" : .Columns(1).Width = 350
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowCount = 17
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

                ''グラフタイトル列をReadOnly色設定
                For i = 0 To .RowCount - 1
                    .Rows(i).Cells("txtGraphTitle").Style.BackColor = gColorGridRowBackReadOnly
                Next

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.None

                ''コンボボックス初期設定
                Call gSetComboBox(Column1, gEnmComboType.ctOpsGraphListColumn2)

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdGraph)

            End With

            'Dim objGrid As DataGridView = Nothing
            Dim Column10 As New DataGridViewTextBoxColumn : Column10.Name = "txtFreeGraphTitle" : Column10.ReadOnly = True
            Dim Column20 As New DataGridViewTextBoxColumn : Column20.Name = "txtFreeGraphTitle" : Column20.ReadOnly = True
            Dim Column30 As New DataGridViewTextBoxColumn : Column30.Name = "txtFreeGraphTitle" : Column30.ReadOnly = True
            Dim Column40 As New DataGridViewTextBoxColumn : Column40.Name = "txtFreeGraphTitle" : Column40.ReadOnly = True
            Dim Column50 As New DataGridViewTextBoxColumn : Column50.Name = "txtFreeGraphTitle" : Column50.ReadOnly = True
            Dim Column60 As New DataGridViewTextBoxColumn : Column60.Name = "txtFreeGraphTitle" : Column60.ReadOnly = True
            Dim Column70 As New DataGridViewTextBoxColumn : Column70.Name = "txtFreeGraphTitle" : Column70.ReadOnly = True
            Dim Column80 As New DataGridViewTextBoxColumn : Column80.Name = "txtFreeGraphTitle" : Column80.ReadOnly = True
            Dim Column90 As New DataGridViewTextBoxColumn : Column90.Name = "txtFreeGraphTitle" : Column90.ReadOnly = True
            Dim Column100 As New DataGridViewTextBoxColumn : Column100.Name = "txtFreeGraphTitle" : Column100.ReadOnly = True


            ''----------------------
            '' フリーグラフ設定
            ''----------------------
            For j = 0 To UBound(grdFree)

                With grdFree(j)

                    Select Case j
                        Case 0
                            .Columns.Clear()
                            .Columns.Add(Column10)
                        Case 1
                            .Columns.Clear()
                            .Columns.Add(Column20)
                        Case 2
                            .Columns.Clear()
                            .Columns.Add(Column30)
                        Case 3
                            .Columns.Clear()
                            .Columns.Add(Column40)
                        Case 4
                            .Columns.Clear()
                            .Columns.Add(Column50)
                        Case 5
                            .Columns.Clear()
                            .Columns.Add(Column60)
                        Case 6
                            .Columns.Clear()
                            .Columns.Add(Column70)
                        Case 7
                            .Columns.Clear()
                            .Columns.Add(Column80)
                        Case 8
                            .Columns.Clear()
                            .Columns.Add(Column90)
                        Case 9
                            .Columns.Clear()
                            .Columns.Add(Column100)
                    End Select

                    ''列
                    .AllowUserToResizeColumns = False                                               ''列幅の変更不可
                    .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing  ''行ヘッダー幅の変更不可

                    ''全ての列の並び替えを禁止
                    For Each c As DataGridViewColumn In .Columns
                        c.SortMode = DataGridViewColumnSortMode.NotSortable
                    Next c

                    ''列ヘッダー
                    .Columns(0).HeaderText = "Graph Title" : .Columns(0).Width = 530
                    .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                    ''行
                    .RowCount = 17
                    .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                    .AllowUserToResizeRows = False      ''行の高さの変更不可
                    .AllowUserToDeleteRows = False      ''行の削除を不可にする

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

                    ''グラフタイトル列をReadOnly色設定
                    For i = 0 To .RowCount - 1
                        .Rows(i).Cells("txtFreeGraphTitle").Style.BackColor = gColorGridRowBackReadOnly
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
                    'Call gSetGridCopyAndPaste(grdFree)

                End With

            Next j


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

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
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) OPS設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetOpsGraph)

        Try

            ''グラフタイトル
            For i As Integer = 0 To UBound(udtSet.udtGraphTitleRec)

                With udtSet.udtGraphTitleRec(i)

                    ''※グラフ番号は変更がないため操作なし

                    ''グラフ名称
                    .strName = Trim(grdGraph.Rows(i).Cells("txtGraphTitle").Value)

                    ''グラフタイプ
                    .bytType = CCbyte(grdGraph.Rows(i).Cells("cmbGraphType").Value)

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) OPS設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetOpsGraph)

        Try

            ''grdGraph_CellValueChangedイベントの実行を止める
            mblnInitFlg = True

            For i As Integer = LBound(udtSet.udtGraphTitleRec) To UBound(udtSet.udtGraphTitleRec)
                With udtSet.udtGraphTitleRec(i)
                    grdGraph.Rows(i).Cells("cmbGraphType").Value = .bytType.ToString    ''グラフタイプ
                    grdGraph.Rows(i).Cells("txtGraphTitle").Value = Trim(.strName)      ''グラフ名称
                End With
            Next

            ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
            'For i As Integer = LBound(udtSet.udtGraphFreeRec) To UBound(udtSet.udtGraphFreeRec)
            '    For j As Integer = LBound(udtSet.udtGraphFreeRec(i).udtFreeGraphTitle) To UBound(udtSet.udtGraphFreeRec(i).udtFreeGraphTitle)
            '        With udtSet.udtGraphFreeRec(i).udtFreeGraphTitle(j)
            '            mgrdFree(i).Rows(j).Cells("txtFreeGraphTitle").Value = Trim(.strGraphTitle) ''グラフ名称
            '        End With
            '    Next
            'Next

            mblnInitFlg = False

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetOpsGraph, _
                               ByRef udtTarget As gTypSetOpsGraph)

        Try

            Dim i As Integer

            For i = 0 To UBound(udtTarget.udtGraphTitleRec)

                ''グラフタイトル
                Call mCopyOpsGraphDataTitle(udtSource.udtGraphTitleRec(i), udtTarget.udtGraphTitleRec(i))

                ''偏差グラフ
                Call mCopyOpsGraphDataExhaust(udtSource.udtGraphExhaustRec(i), udtTarget.udtGraphExhaustRec(i))

                ''バーグラフ
                Call mCopyOpsGraphDataBar(udtSource.udtGraphBarRec(i), udtTarget.udtGraphBarRec(i))

                ''アナログメータ
                Call mCopyOpsGraphDataAnalogMeter(udtSource.udtGraphAnalogMeterRec(i), udtTarget.udtGraphAnalogMeterRec(i))

            Next

            ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
            ' ''フリーグラフ
            'For i = 0 To UBound(udtTarget.udtGraphFreeRec)
            '    For j As Integer = 0 To UBound(udtTarget.udtGraphFreeRec(i).udtFreeGraphTitle)
            '        Call mCopyOpsFree(udtSource.udtGraphFreeRec(i).udtFreeGraphTitle(j), udtTarget.udtGraphFreeRec(i).udtFreeGraphTitle(j), True)
            '    Next
            'Next

            ''アナログメータ設定
            udtTarget.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint = udtSource.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint
            udtTarget.udtGraphAnalogMeterSettingRec.bytMarkNumericalValue = udtSource.udtGraphAnalogMeterSettingRec.bytMarkNumericalValue
            udtTarget.udtGraphAnalogMeterSettingRec.bytPointerFrame = udtSource.udtGraphAnalogMeterSettingRec.bytPointerFrame
            udtTarget.udtGraphAnalogMeterSettingRec.bytPointerColorChange = udtSource.udtGraphAnalogMeterSettingRec.bytPointerColorChange
            ''2011.06.09 仕様変更
            'udtTarget.udtGraphAnalogMeterSettingRec.bytSideColorSymbol = udtSource.udtGraphAnalogMeterSettingRec.bytSideColorSymbol

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 設定済データのコピー
    ' 引数      ： ARG1 - (I ) グラフタイプ（1：偏差　2：バー　3：アナログメーター）
    '           ： ARG2 - (I ) 処理中の行番号
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mCopyOpsGraphData(ByVal intGraphType As Integer, _
                                  ByVal intRow As Integer)

        Try

            With mudtSetOpsGraphWork

                Select Case intGraphType

                    Case gCstCodeOpsTitleGraphTypeExhaust

                        Call mCopyOpsGraphDataExhaust(.udtGraphExhaustRec(intRow - 1), .udtGraphExhaustRec(intRow))

                    Case gCstCodeOpsTitleGraphTypeBar

                        Call mCopyOpsGraphDataBar(.udtGraphBarRec(intRow - 1), .udtGraphBarRec(intRow))

                    Case gCstCodeOpsTitleGraphTypeAnalogMeter

                        Call mCopyOpsGraphDataAnalogMeter(.udtGraphAnalogMeterRec(intRow - 1), .udtGraphAnalogMeterRec(intRow))

                    Case Else

                End Select

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'グラフタイトル
    Private Sub mCopyOpsGraphDataTitle(ByRef udtSource As gTypSetOpsGraphTitle, _
                                       ByRef udtTarget As gTypSetOpsGraphTitle, _
                              Optional ByVal hblnNormalProcess As Boolean = True)

        Try

            ''Add処理の時はグラフ番号をコピーしない
            If hblnNormalProcess Then udtTarget.bytNo = udtSource.bytNo
            udtTarget.strName = udtSource.strName
            udtTarget.bytType = udtSource.bytType

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '偏差グラフ（排ガス）
    Private Sub mCopyOpsGraphDataExhaust(ByRef udtSource As gTypSetOpsGraphExhaust, _
                                         ByRef udtTarget As gTypSetOpsGraphExhaust, _
                                Optional ByVal hblnNormalProcess As Boolean = True)

        Try

            ''Add処理の時はグラフ番号をコピーしない
            If hblnNormalProcess Then udtTarget.bytNo = udtSource.bytNo
            udtTarget.strTitle = udtSource.strTitle
            udtTarget.strItemUp = udtSource.strItemUp
            udtTarget.strItemDown = udtSource.strItemDown

            udtTarget.shtAveCh = udtSource.shtAveCh
            udtTarget.bytDevMark = udtSource.bytDevMark
            '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'udtTarget.byt20Graph = udtSource.byt20Graph
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            udtTarget.bytLine = udtSource.bytLine

            udtTarget.strTcTitle = udtSource.strTcTitle
            udtTarget.strTcComm1 = udtSource.strTcComm1
            udtTarget.strTcComm2 = udtSource.strTcComm2

            ''Cylinder CH_NO.
            udtTarget.bytCyCnt = udtSource.bytCyCnt
            For i = 0 To UBound(udtTarget.udtCylinder)
                udtTarget.udtCylinder(i).shtChCylinder = udtSource.udtCylinder(i).shtChCylinder
                udtTarget.udtCylinder(i).shtChDeviation = udtSource.udtCylinder(i).shtChDeviation
                udtTarget.udtCylinder(i).strTitle = udtSource.udtCylinder(i).strTitle
            Next

            ''T/C CH_NO.
            udtTarget.bytTcCnt = udtSource.bytTcCnt
            For i = 0 To UBound(udtTarget.udtTurboCharger)
                udtTarget.udtTurboCharger(i).shtChTurboCharger = udtSource.udtTurboCharger(i).shtChTurboCharger
                udtTarget.udtTurboCharger(i).strTitle = udtSource.udtTurboCharger(i).strTitle
                udtTarget.udtTurboCharger(i).bytSplitLine = udtSource.udtTurboCharger(i).bytSplitLine
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'バーグラフ
    Private Sub mCopyOpsGraphDataBar(ByRef udtSource As gTypSetOpsGraphBar, _
                                     ByRef udtTarget As gTypSetOpsGraphBar, _
                            Optional ByVal hblnNormalProcess As Boolean = True)
        Try

            ''Add処理の時はグラフ番号をコピーしない
            If hblnNormalProcess Then udtTarget.bytNo = udtSource.bytNo
            udtTarget.strTitle = udtSource.strTitle
            udtTarget.strItemUp = udtSource.strItemUp
            udtTarget.strItemDown = udtSource.strItemDown
            udtTarget.bytCyCnt = udtSource.bytCyCnt
            '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph→表示切替に変更 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            udtTarget.bytDisplay = udtSource.bytDisplay
            '---------------------------------------------------------------------------------------------------
            'udtTarget.byt20Graph = udtSource.byt20Graph
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            udtTarget.bytLine = udtSource.bytLine
            udtTarget.bytDevision = udtSource.bytDevision

            ''バーグラフ詳細
            For i = 0 To UBound(udtTarget.udtCylinder)
                udtTarget.udtCylinder(i).shtChCylinder = udtSource.udtCylinder(i).shtChCylinder
                udtTarget.udtCylinder(i).strTitle = udtSource.udtCylinder(i).strTitle
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'アナログメーター
    Private Sub mCopyOpsGraphDataAnalogMeter(ByRef udtSource As gTypSetOpsGraphAnalogMeter, _
                                             ByRef udtTarget As gTypSetOpsGraphAnalogMeter, _
                                    Optional ByVal hblnNormalProcess As Boolean = True)
        Try

            ''Add処理の時はグラフ番号をコピーしない
            If hblnNormalProcess Then udtTarget.bytNo = udtSource.bytNo
            udtTarget.strTitle = udtSource.strTitle
            udtTarget.bytMeterType = udtSource.bytMeterType

            ''アナログメーター詳細
            For i = 0 To UBound(udtTarget.udtDetail)
                udtTarget.udtDetail(i).shtChNo = udtSource.udtDetail(i).shtChNo
                udtTarget.udtDetail(i).bytScale = udtSource.udtDetail(i).bytScale
                udtTarget.udtDetail(i).bytColor = udtSource.udtDetail(i).bytColor
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'フリーグラフ
    Private Sub mCopyOpsFree(ByRef udtSource As gTypSetOpsFreeGraphRec, _
                             ByRef udtTarget As gTypSetOpsFreeGraphRec, _
                             ByVal blnCopyNo As Boolean)

        Try

            If blnCopyNo Then

                ''グラフ番号
                udtTarget.bytGraphNo = udtSource.bytGraphNo

                ''OPS番号
                udtTarget.bytOpsNo = udtSource.bytOpsNo

            End If

            ''グラフタイトル
            udtTarget.strGraphTitle = udtSource.strGraphTitle

            ''フリーグラフ詳細
            For i As Integer = 0 To UBound(udtSource.udtFreeDetail)

                udtTarget.udtFreeDetail(i).bytType = udtSource.udtFreeDetail(i).bytType
                udtTarget.udtFreeDetail(i).shtChNo = udtSource.udtFreeDetail(i).shtChNo
                udtTarget.udtFreeDetail(i).bytTopPos = udtSource.udtFreeDetail(i).bytTopPos
                udtTarget.udtFreeDetail(i).bytIndicatorKind = udtSource.udtFreeDetail(i).bytIndicatorKind
                udtTarget.udtFreeDetail(i).shtIndicatorPattern = udtSource.udtFreeDetail(i).shtIndicatorPattern
                udtTarget.udtFreeDetail(i).bytScale = udtSource.udtFreeDetail(i).bytScale
                udtTarget.udtFreeDetail(i).bytColor = udtSource.udtFreeDetail(i).bytColor

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
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetOpsGraph, _
                                         ByVal udt2 As gTypSetOpsGraph) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.udtGraphTitleRec)

                ''----------------------
                '' グラフタイトル
                ''----------------------
                If udt1.udtGraphTitleRec(i).bytNo <> udt2.udtGraphTitleRec(i).bytNo Then Return False
                If udt1.udtGraphTitleRec(i).bytType <> udt2.udtGraphTitleRec(i).bytType Then Return False
                If Not gCompareString(udt1.udtGraphTitleRec(i).strName, udt2.udtGraphTitleRec(i).strName) Then Return False

                ''----------------------
                '' 偏差グラフ（排ガス）
                ''----------------------
                If udt1.udtGraphExhaustRec(i).bytNo <> udt2.udtGraphExhaustRec(i).bytNo Then Return False
                If Not gCompareString(udt1.udtGraphExhaustRec(i).strTitle, udt2.udtGraphExhaustRec(i).strTitle) Then Return False
                If Not gCompareString(udt1.udtGraphExhaustRec(i).strItemUp, udt2.udtGraphExhaustRec(i).strItemUp) Then Return False
                If Not gCompareString(udt1.udtGraphExhaustRec(i).strItemDown, udt2.udtGraphExhaustRec(i).strItemDown) Then Return False
                If udt1.udtGraphExhaustRec(i).shtAveCh <> udt2.udtGraphExhaustRec(i).shtAveCh Then Return False
                If udt1.udtGraphExhaustRec(i).bytDevMark <> udt2.udtGraphExhaustRec(i).bytDevMark Then Return False
                '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                'If udt1.udtGraphExhaustRec(i).byt20Graph <> udt2.udtGraphExhaustRec(i).byt20Graph Then Return False
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                If udt1.udtGraphExhaustRec(i).bytLine <> udt2.udtGraphExhaustRec(i).bytLine Then Return False

                ''Cylinder
                If udt1.udtGraphExhaustRec(i).bytCyCnt <> udt2.udtGraphExhaustRec(i).bytCyCnt Then Return False
                For j = 0 To UBound(udt1.udtGraphExhaustRec(i).udtCylinder)
                    If udt1.udtGraphExhaustRec(i).udtCylinder(j).shtChCylinder <> udt2.udtGraphExhaustRec(i).udtCylinder(j).shtChCylinder Then Return False
                    If udt1.udtGraphExhaustRec(i).udtCylinder(j).shtChDeviation <> udt2.udtGraphExhaustRec(i).udtCylinder(j).shtChDeviation Then Return False
                    'Verr2.0.5.9 スペースも比較対象とする
                    If Not gCompareString(udt1.udtGraphExhaustRec(i).udtCylinder(j).strTitle, udt2.udtGraphExhaustRec(i).udtCylinder(j).strTitle, False) Then Return False
                Next

                ''TurboCharger
                If Not gCompareString(udt1.udtGraphExhaustRec(i).strTcTitle, udt2.udtGraphExhaustRec(i).strTcTitle) Then Return False
                If Not gCompareString(udt1.udtGraphExhaustRec(i).strTcComm1, udt2.udtGraphExhaustRec(i).strTcComm1) Then Return False
                If Not gCompareString(udt1.udtGraphExhaustRec(i).strTcComm2, udt2.udtGraphExhaustRec(i).strTcComm2) Then Return False
                If udt1.udtGraphExhaustRec(i).bytTcCnt <> udt2.udtGraphExhaustRec(i).bytTcCnt Then Return False
                For j = 0 To UBound(udt1.udtGraphExhaustRec(i).udtTurboCharger)
                    If udt1.udtGraphExhaustRec(i).udtTurboCharger(j).shtChTurboCharger <> udt2.udtGraphExhaustRec(i).udtTurboCharger(j).shtChTurboCharger Then Return False
                    'Verr2.0.5.9 スペースも比較対象とする
                    If Not gCompareString(udt1.udtGraphExhaustRec(i).udtTurboCharger(j).strTitle, udt2.udtGraphExhaustRec(i).udtTurboCharger(j).strTitle, False) Then Return False
                    If udt1.udtGraphExhaustRec(i).udtTurboCharger(j).bytSplitLine <> udt2.udtGraphExhaustRec(i).udtTurboCharger(j).bytSplitLine Then Return False
                Next

                ''----------------------
                '' バーグラフ
                ''----------------------
                If udt1.udtGraphBarRec(i).bytNo <> udt2.udtGraphBarRec(i).bytNo Then Return False
                If Not gCompareString(udt1.udtGraphBarRec(i).strTitle, udt2.udtGraphBarRec(i).strTitle) Then Return False
                If Not gCompareString(udt1.udtGraphBarRec(i).strItemUp, udt2.udtGraphBarRec(i).strItemUp) Then Return False
                If Not gCompareString(udt1.udtGraphBarRec(i).strItemDown, udt2.udtGraphBarRec(i).strItemDown) Then Return False
                '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph→表示切替に変更 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                If udt1.udtGraphBarRec(i).bytDisplay <> udt2.udtGraphBarRec(i).bytDisplay Then Return False
                '---------------------------------------------------------------------------------------------------
                'If udt1.udtGraphBarRec(i).byt20Graph <> udt2.udtGraphBarRec(i).byt20Graph Then Return False
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                If udt1.udtGraphBarRec(i).bytLine <> udt2.udtGraphBarRec(i).bytLine Then Return False
                If udt1.udtGraphBarRec(i).bytDevision <> udt2.udtGraphBarRec(i).bytDevision Then Return False
                If udt1.udtGraphBarRec(i).bytCyCnt <> udt2.udtGraphBarRec(i).bytCyCnt Then Return False

                ''Cylinder
                For j = 0 To UBound(udt1.udtGraphBarRec(i).udtCylinder)
                    If udt1.udtGraphBarRec(i).udtCylinder(j).shtChCylinder <> udt2.udtGraphBarRec(i).udtCylinder(j).shtChCylinder Then Return False
                    'Verr2.0.5.9 スペースも比較対象とする
                    If Not gCompareString(udt1.udtGraphBarRec(i).udtCylinder(j).strTitle, udt2.udtGraphBarRec(i).udtCylinder(j).strTitle, False) Then Return False
                Next

                ''----------------------
                '' アナログメーター
                ''----------------------
                If udt1.udtGraphAnalogMeterRec(i).bytNo <> udt2.udtGraphAnalogMeterRec(i).bytNo Then Return False
                If Not gCompareString(udt1.udtGraphAnalogMeterRec(i).strTitle, udt2.udtGraphAnalogMeterRec(i).strTitle) Then Return False
                If udt1.udtGraphAnalogMeterRec(i).bytMeterType <> udt2.udtGraphAnalogMeterRec(i).bytMeterType Then Return False

                ''アナログメーター情報
                For j = 0 To UBound(udt1.udtGraphAnalogMeterRec(i).udtDetail)
                    If udt1.udtGraphAnalogMeterRec(i).udtDetail(j).shtChNo <> udt2.udtGraphAnalogMeterRec(i).udtDetail(j).shtChNo Then Return False
                    If udt1.udtGraphAnalogMeterRec(i).udtDetail(j).bytScale <> udt2.udtGraphAnalogMeterRec(i).udtDetail(j).bytScale Then Return False
                    If udt1.udtGraphAnalogMeterRec(i).udtDetail(j).bytColor <> udt2.udtGraphAnalogMeterRec(i).udtDetail(j).bytColor Then Return False
                Next

                ''----------------------
                ' アナログメータ設定
                ''----------------------
                If udt1.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint <> udt2.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint Then Return False
                If udt1.udtGraphAnalogMeterSettingRec.bytMarkNumericalValue <> udt2.udtGraphAnalogMeterSettingRec.bytMarkNumericalValue Then Return False
                If udt1.udtGraphAnalogMeterSettingRec.bytPointerFrame <> udt2.udtGraphAnalogMeterSettingRec.bytPointerFrame Then Return False
                If udt1.udtGraphAnalogMeterSettingRec.bytPointerColorChange <> udt2.udtGraphAnalogMeterSettingRec.bytPointerColorChange Then Return False
                ''2011.06.09 仕様変更
                'If udt1.udtGraphAnalogMeterSettingRec.bytSideColorSymbol <> udt2.udtGraphAnalogMeterSettingRec.bytSideColorSymbol Then Return False

                '-----------------------------------------
                ' フリーグラフ
                '-----------------------------------------
                ''フリーグラフの比較・保存はレイアウト画面で行う

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能説明  : 配列のサイズ指定
    ' 返り値    : なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Private Sub mInitialArray()

        Try

            Dim i As Integer, j As Integer

            ''構造体/配列定義
            Call mudtSetOpsGraphWork.InitArray()
            Call mudtSetOpsGraphNewMach.InitArray()
            Call mudtSetOpsGraphNewCarg.InitArray()
            ReDim mintGraphTypeMach(gCstCodeOpsGraphNo - 1)
            ReDim mintGraphTypeCarg(gCstCodeOpsGraphNo - 1)

            ''------------------------------------------
            '' ３種類グラフ
            ''------------------------------------------
            For i = LBound(mudtSetOpsGraphWork.udtGraphTitleRec) To UBound(mudtSetOpsGraphWork.udtGraphTitleRec)

                ''偏差グラフ（排ガス）
                Call mudtSetOpsGraphWork.udtGraphExhaustRec(i).InitArray()
                Call mudtSetOpsGraphNewMach.udtGraphExhaustRec(i).InitArray()
                Call mudtSetOpsGraphNewCarg.udtGraphExhaustRec(i).InitArray()

                ''バーグラフ
                Call mudtSetOpsGraphWork.udtGraphBarRec(i).InitArray()
                Call mudtSetOpsGraphNewMach.udtGraphBarRec(i).InitArray()
                Call mudtSetOpsGraphNewCarg.udtGraphBarRec(i).InitArray()

                ''アナログメーター
                Call mudtSetOpsGraphWork.udtGraphAnalogMeterRec(i).InitArray()
                Call mudtSetOpsGraphNewMach.udtGraphAnalogMeterRec(i).InitArray()
                Call mudtSetOpsGraphNewCarg.udtGraphAnalogMeterRec(i).InitArray()

                ''前回値削除用配列
                mintGraphTypeMach(i) = gudt.SetOpsGraphM.udtGraphTitleRec(i).bytType
                mintGraphTypeCarg(i) = gudt.SetOpsGraphC.udtGraphTitleRec(i).bytType

            Next

            ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
            ' ''------------------------------------------
            ' '' フリーグラフ
            ' ''------------------------------------------
            'For i = LBound(mudtSetOpsGraphWork.udtGraphFreeRec) To UBound(mudtSetOpsGraphWork.udtGraphFreeRec)
            '    mudtSetOpsGraphWork.udtGraphFreeRec(i).InitArray()
            '    mudtSetOpsGraphNewMach.udtGraphFreeRec(i).InitArray()
            '    mudtSetOpsGraphNewCarg.udtGraphFreeRec(i).InitArray()

            '    For j = 0 To UBound(mudtSetOpsGraphWork.udtGraphFreeRec)
            '        mudtSetOpsGraphWork.udtGraphFreeRec(j).InitArray()
            '        mudtSetOpsGraphNewMach.udtGraphFreeRec(j).InitArray()
            '        mudtSetOpsGraphNewCarg.udtGraphFreeRec(j).InitArray()

            '        For k = 0 To UBound(mudtSetOpsGraphWork.udtGraphFreeRec(j).udtFreeGraphTitle)
            '            Call mudtSetOpsGraphWork.udtGraphFreeRec(j).udtFreeGraphTitle(k).InitArray()
            '            Call mudtSetOpsGraphNewMach.udtGraphFreeRec(j).udtFreeGraphTitle(k).InitArray()
            '            Call mudtSetOpsGraphNewCarg.udtGraphFreeRec(j).udtFreeGraphTitle(k).InitArray()
            '        Next k

            '    Next j

            'Next i

            '------------------------------------------
            ' プルダウンメニュー（自動インポート機能）
            '------------------------------------------
            Call mudtSetOpsPulldownMenuNew.InitArray()
            For i = LBound(mudtSetOpsPulldownMenuNew.udtDetail) To UBound(mudtSetOpsPulldownMenuNew.udtDetail)
                Call mudtSetOpsPulldownMenuNew.udtDetail(i).InitArray()
                For j = LBound(mudtSetOpsPulldownMenuNew.udtDetail(i).udtGroup) To UBound(mudtSetOpsPulldownMenuNew.udtDetail(i).udtGroup)
                    Call mudtSetOpsPulldownMenuNew.udtDetail(i).udtGroup(j).InitArray()
                Next
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 選択したタイプのグラフ表示
    ' 引数      ： ARG1 - (IO) 作業用OPS設定構造体
    '           ： ARG2 - (I ) グラフタイプ
    '           ： ARG3 - (I ) 処理中の行番号
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDispOpsGraph(ByRef hudtOpsGraphWork As gTypSetOpsGraph, _
                              ByVal intGraphType As Integer, _
                              ByVal intRowIndex As Integer)

        Try

            With hudtOpsGraphWork

                ''グラフ毎にグラフタイトル構造体へタイトル設定を行う
                Select Case intGraphType

                    Case gCstCodeOpsTitleGraphTypeExhaust

                        If frmOpsGraphExtGus.gShow(.udtGraphExhaustRec(intRowIndex), intRowIndex, Me) = 0 Then

                            ''グラフタイトル構造体にタイトル名称を設定
                            .udtGraphTitleRec(intRowIndex).strName = .udtGraphExhaustRec(intRowIndex).strTitle

                            ''設定値を比較用構造体に格納
                            Call mSetStructureInfo()

                        End If

                    Case gCstCodeOpsTitleGraphTypeBar

                        If frmOpsGraphBarGraph.gShow(.udtGraphBarRec(intRowIndex), intRowIndex, Me) = 0 Then

                            ''グラフタイトル構造体にタイトル名称を設定
                            .udtGraphTitleRec(intRowIndex).strName = .udtGraphBarRec(intRowIndex).strTitle

                            ''設定値を比較用構造体に格納
                            Call mSetStructureInfo()

                        End If

                    Case gCstCodeOpsTitleGraphTypeAnalogMeter

                        If frmOpsGraphAnalogMaterList.gShow(.udtGraphAnalogMeterRec(intRowIndex), intRowIndex, Me) = 0 Then

                            ''グラフタイトル構造体にタイトル名称を設定
                            .udtGraphTitleRec(intRowIndex).strName = .udtGraphAnalogMeterRec(intRowIndex).strTitle

                            ''設定値を比較用構造体に格納
                            Call mSetStructureInfo()

                        End If

                    Case gCstCodeOpsTitleGraphTypeNothing

                        '何もしない

                End Select

                ''表示更新
                Call mSetDisplay(mudtSetOpsGraphWork)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : タイマー処理
    ' 返り値    : なし
    ' 引き数    : なし
    '----------------------------------------------------------------------------
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Try

            Timer1.Stop()

            ''フッターボタンの使用可/不可設定
            Call mSetControlFooterBtn()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 設定済（変更前）データ削除
    ' 引数      ： ARG1 - (I ) パート選択（TRUE:Machinery、FALSE:Cargo）
    '           ： ARG2 - (I ) グラフタイプ配列
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDeleteSetData(ByVal hblnMachinery As Boolean, _
                               ByVal hintGraphType() As Integer)

        Try

            ''grdGraph_CellValueChangedイベントの実行を止める
            mblnInitFlg = True

            ''行インデックスの取得
            Dim intRowIndex As Integer = grdGraph.CurrentCell.RowIndex

            ''グラフタイプに変更がない場合は削除を行わない
            If hintGraphType(intRowIndex) <> CCInt(grdGraph.CurrentRow.Cells("cmbGraphType").Value) Then

                ''変更前のグラフ設定データ削除
                Call mDeleteSetDataDetail(mudtSetOpsGraphWork, hintGraphType(intRowIndex), intRowIndex)

                ''設定データの削除に伴い、グラフタイトル設定データ削除
                Call gInitOpsGraphTitle(intRowIndex, mudtSetOpsGraphWork.udtGraphTitleRec(intRowIndex))

                ''該当構造体へ情報反映
                If hblnMachinery Then Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewMach)
                If Not hblnMachinery Then Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewCarg)

                ''前回値の設定
                hintGraphType(intRowIndex) = CCInt(grdGraph.CurrentRow.Cells("cmbGraphType").Value)

            End If

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Add処理した際のコピー元データ削除
    ' 引数      ： ARG1 - (I ) パート選択（TRUE:Machinery、FALSE:Cargo）
    '           ： ARG2 - (I ) グラフタイプ配列
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDeleteSourceData(ByVal hblnMachinery As Boolean, _
                                  ByVal hintRowIndex As Integer, _
                                  ByVal hintGraphType() As Integer)

        Try

            ''grdGraph_CellValueChangedイベントの実行を止める
            mblnInitFlg = True

            ''コピー元のデータ削除
            Call mDeleteSetDataDetail(mudtSetOpsGraphWork, hintGraphType(hintRowIndex), hintRowIndex)

            ''コピー元のグラフタイトルデータ削除
            Call gInitOpsGraphTitle(hintRowIndex, mudtSetOpsGraphWork.udtGraphTitleRec(hintRowIndex))

            ''該当構造体へ情報反映
            If hblnMachinery Then Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewMach)
            If Not hblnMachinery Then Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewCarg)

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： データ削除 詳細
    ' 引数      ： ARG1 - (IO) 作業用構造体
    '           ： ARG2 - (I ) グラフタイプ
    '           ： ARG3 - (I ) 処理中の行番号
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDeleteSetDataDetail(ByRef hudtSetOpsGraphWork As gTypSetOpsGraph, _
                                     ByVal intGraphType As Integer, _
                                     ByVal intRowIndex As Integer)

        Try

            With hudtSetOpsGraphWork

                Select Case intGraphType

                    Case gCstCodeOpsTitleGraphTypeExhaust

                        Call gInitOpsGraphExhaust(intRowIndex, .udtGraphExhaustRec(intRowIndex))

                    Case gCstCodeOpsTitleGraphTypeBar

                        Call gInitOpsGraphBar(intRowIndex, .udtGraphBarRec(intRowIndex))

                    Case gCstCodeOpsTitleGraphTypeAnalogMeter

                        Call gInitOpsGraphAnalogMeter(intRowIndex, .udtGraphAnalogMeterRec(intRowIndex))

                    Case gCstCodeOpsTitleGraphTypeNothing

                        Call gInitOpsGraphTitle(intRowIndex, .udtGraphTitleRec(intRowIndex))

                    Case Else

                End Select

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : グラフデータの表示更新
    ' 引き数    : ARG1 - (I ) グラフタイプ配列
    ' 返り値    : なし
    ' 機能説明  : グラフタイプ変更に伴う、表示データの更新
    '----------------------------------------------------------------------------
    Private Sub mRefreshGraphData(ByVal hudtTitle() As gTypSetOpsGraphTitle)

        Try

            mblnInitFlg = True

            With hudtTitle(grdGraph.CurrentCell.RowIndex)

                ''タイトル更新
                grdGraph.Rows(grdGraph.CurrentCell.RowIndex).Cells("txtGraphTitle").Value = .strName

            End With

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : フッターボタンの使用可/不可設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : コンボボックの状態からボタンの使用可/不可を設定する
    '----------------------------------------------------------------------------
    Private Sub mSetControlFooterBtn()

        Try

            If optGraph.Checked Then

                '=======================
                ' 偏差、バー、アナログ
                '=======================
                Dim intRowIndex As Integer = grdGraph.CurrentCell.RowIndex

                cmdEdit.Enabled = True
                cmdDelete.Enabled = True
                cmdAdd.Enabled = True

                ''Edit
                If CCInt(grdGraph.Rows(intRowIndex).Cells("cmbGraphType").Value) = gCstCodeOpsTitleGraphTypeNothing Then
                    cmdEdit.Enabled = False
                End If

                ''Delete
                If CCInt(grdGraph.Rows(intRowIndex).Cells("cmbGraphType").Value) = gCstCodeOpsTitleGraphTypeNothing Then
                    cmdDelete.Enabled = False
                End If

                ''Add
                If CCInt(grdGraph.Rows(intRowIndex).Cells("cmbGraphType").Value) = gCstCodeOpsTitleGraphTypeNothing Or _
                   (intRowIndex = grdGraph.RowCount - 1) Then
                    cmdAdd.Enabled = False
                End If

            ElseIf optFree.Checked Then
                ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto

                ''=======================
                '' フリー
                ''=======================
                'cmdEdit.Enabled = True
                'cmdDelete.Enabled = True
                'cmdAdd.Enabled = True

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : OPSインデックス、Setインデックス取得
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) OPSインデックス
    ' 　　　    : ARG2 - ( O) Setインデックス
    ' 機能説明  : 画面表示情報からOPS、Setインデックスを取得する
    '----------------------------------------------------------------------------
    Private Sub mGetIndexFreeOpsSet(ByRef intOpsIndex As Integer, _
                                    ByRef intSetIndex As Integer)

        Try

            ''タブ選択状態からOPSインデックスを取得
            intOpsIndex = TabFree.SelectedIndex

            ''表示中のグリッドの選択行インデックスを取得
            For i As Integer = 0 To UBound(mgrdFree)

                If i = intOpsIndex Then

                    intSetIndex = mgrdFree(i).CurrentCell.RowIndex

                    Exit For

                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値の取得
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 詳細設定画面の情報を該当構造体に設定
    '--------------------------------------------------------------------
    Private Sub mSetStructureInfo()

        Try

            If optMachinery.Checked Then

                ''Machinery構造体に設定
                Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewMach)

            ElseIf optCargo.Checked Then

                ''Cargo構造体に設定
                Call mCopyStructure(mudtSetOpsGraphWork, mudtSetOpsGraphNewCarg)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能      : 設定値の取得
    ' 引き数    : ARG1 - (I ) OPS設定-グラフタイプ構造体
    ' 返り値    : なし
    '----------------------------------------------------------------------------
    Private Sub mGetGridInfo(ByVal hudtTitle() As gTypSetOpsGraphTitle)

        Try

            mblnInitFlg = True

            With hudtTitle(grdGraph.CurrentCell.RowIndex)

                ''グラフタイトル構造体にグラフタイプを保存
                .bytType = CCInt(grdGraph.CurrentRow.Cells("cmbGraphType").Value)

            End With

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Add処理
    ' 引数      ： ARG1 - (I ) パート選択情報
    '           ： ARG2 - (IO) 作業用構造体
    '           ： ARG3 - (IO) 前回値保持用グラフタイプ配列
    '           ： ARG4 - (I ) 行Index
    ' 戻値      ： 選択した行情報を１つ下に追加
    '----------------------------------------------------------------------------
    Private Sub mAddRowInfo(ByVal hblnMachinery As Boolean, _
                            ByRef hudtOpsGraphWork As gTypSetOpsGraph, _
                            ByRef hintgraphType() As Integer, _
                            ByVal intRowIndex As Integer)

        Try

            ''最後尾に設定されている行データは破棄する
            If intRowIndex = grdGraph.RowCount - 1 Then Return

            With hudtOpsGraphWork

                ''３種グラフデータのコピー
                Select Case CCInt(grdGraph.Rows(intRowIndex).Cells("cmbGraphType").Value)
                    Case gCstCodeOpsTitleGraphTypeExhaust : Call mCopyOpsGraphDataExhaust(.udtGraphExhaustRec(intRowIndex), .udtGraphExhaustRec(intRowIndex + 1), False)
                    Case gCstCodeOpsTitleGraphTypeBar : Call mCopyOpsGraphDataBar(.udtGraphBarRec(intRowIndex), .udtGraphBarRec(intRowIndex + 1), False)
                    Case gCstCodeOpsTitleGraphTypeAnalogMeter : Call mCopyOpsGraphDataAnalogMeter(.udtGraphAnalogMeterRec(intRowIndex), .udtGraphAnalogMeterRec(intRowIndex + 1), False)
                End Select

                ''グラフタイトルのコピー
                Call mCopyOpsGraphDataTitle(.udtGraphTitleRec(intRowIndex), .udtGraphTitleRec(intRowIndex + 1), False)

                ''コピー先の配列に前回値を設定
                hintgraphType(intRowIndex + 1) = CCInt(grdGraph.Rows(intRowIndex).Cells("cmbGraphType").Value)

                ''コピー元のデータ削除
                If intRowIndex <> grdGraph.CurrentCell.RowIndex Then

                    ''データ削除
                    Call mDeleteSourceData(hblnMachinery, intRowIndex, hintgraphType)

                    ''コピー元の前回値削除
                    hintgraphType(intRowIndex) = gCstCodeOpsTitleGraphTypeNothing

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
