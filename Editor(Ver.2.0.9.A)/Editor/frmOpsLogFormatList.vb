Public Class frmOpsLogFormatList

#Region "定数定義"

    ''ページ区切り色
    Private mColorGridRowBackSplitPage As Color = Color.LavenderBlush

    ''1ページあたりの表示行数    
    Private Const mCstCodeRowCountInPage As Integer = 50

    ''1ページ目の非表示行数
    Private Const mCstCodeHideRowCnt As Integer = 3

    ''最大印字ページ数   
    Private Const mCstCodeMaxPageNo As Integer = 100

    ''プレビュー表示文字列
    Private Const mCstNamePreviewNothing As String = ""
    Private Const mCstNamePreviewCounterTitleLine1 As String = "NO. DESIGNATION"
    Private Const mCstNamePreviewCounterTitleLine2 As String = "COUNTER"
    Private Const mCstNamePreviewAnalogTitle As String = "NO. DESIGNATION"
    Private Const mCstNamePreviewSpace As String = ""
    Private Const mCstNamePreviewPage As String = ""
    Private Const mCstNamePreviewUnit As String = "UNIT"
    Private Const mCstNamePreviewTime As String = "**:**"
    Private Const mCstNamePreviewDate As String = "**/**"

    ''プレビュー表示文字列
    Private Const mCstNamePreviewUnitDefault As String = "Manual input"
    Private Const mCstNamePreviewUnitCustom As String = "ManInput"

    ''文字列長指定
    Private Const mCstCodeMaxStringLength5 As Integer = 5
    Private Const mCstCodeMaxStringLength8 As Integer = 8
    Private Const mCstCodeMaxStringLength9 As Integer = 9
    Private Const mCstCodeMaxStringLength27 As Integer = 27
    Private Const mCstCodeMaxStringLength30 As Integer = 30
    Private Const mCstCodeMaxStringLength36 As Integer = 36
    Private Const mCstCodeMaxStringLength42 As Integer = 42

    ''表示文字位置調整用（55文字/列）
    Private Const mCstNamePreviewBlank As String = "                                                       "

#End Region

#Region "変数定義"

    ''どちらのグリッドを編集しているかを指定するために使用
    Private Enum mEnmGrid
        Col1
        Col2
    End Enum

    ''LogFormat構造体
    Private mudtSetOpsLogFormatWork As gTypSetOpsLogFormat = Nothing
    Private mudtSetOpsLogFormatNewMach As gTypSetOpsLogFormat = Nothing
    Private mudtSetOpsLogFormatNewCarg As gTypSetOpsLogFormat = Nothing

    ''LogFormat CHID TBL構造体   ''☆2012/10/26 K.Tanigawa
    Private mudtSetOpsLogIdDataWork As gTypSetOpsLogIdData = Nothing
    Private mudtSetOpsLogIdDataNewMach As gTypSetOpsLogIdData = Nothing
    Private mudtSetOpsLogIdDataNewCarg As gTypSetOpsLogIdData = Nothing


    ''自動生成用構造体（RLフラグが有効になっているデータ）
    Private mudtSetChRLPulse As gTypLogFormatPickCH = Nothing   'パルスCH
    Private mudtSetChRLWork As gTypLogFormatPickCH = Nothing    '他CH
    Private mudtSetChIdoKeido As gTypLogFormatPickCH = Nothing  'アナログの緯度経度CH

    ''グループCH名称取得用構造体
    Private mudtChannelGroupMach As gTypChannelGroup = Nothing
    Private mudtChannelGroupCarg As gTypChannelGroup = Nothing
    Private mudtChannelGroupWork As gTypChannelGroup = Nothing

    ''構造体設定用のログフォーマット文字列（iniファイルより取得）
    Private mudtSaveFormatStrings() As gTypCodeName = Nothing

    ''編集モード（TRUE:グループ設定, FALSE:個別設定）
    Private mblnModeGroup As Boolean

    ''ページ毎の最大行番号を取得
    Private mintMaxRowNo(mCstCodeMaxPageNo - 1) As Integer

    ''スクロール同期フラグ
    Private mblnScrollSync As Boolean

    ''編集中のグリッドフラグ（TRUE：grdLogCol1、FALSE：grdLogCol2）
    Private mblnSelectGridCol1 As Boolean

    ''初期化フラグ
    Private mblnInitFlg As Boolean

    ''グリッドイベントの連続実行防止フラグ（CellValidated）
    Private mblnDispDetail As Boolean

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
    Private Sub frmOpsLogFormatList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''初期化フラグ
            mblnInitFlg = True
            gintRow = 0
            gintErrMsg = 0

            ''コンボ設定
            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)
            Call gSetComboBox(cmbSave, gEnmComboType.ctOpsLogFormatListSaveStrings)

            ''スクロール同期フラグにチェックを入れる
            chkScrollSync.Checked = True

            ''スクロール同期フラグ取得
            mblnScrollSync = IIf(chkScrollSync.Checked, True, False)

            ''グループボックス不可視化
            grpVisibleFalse.Visible = False

            ''ページ毎の最大行番号取得
            For i As Integer = 0 To UBound(mintMaxRowNo)
                mintMaxRowNo(i) = (mCstCodeRowCountInPage) + (mCstCodeRowCountInPage * i)
            Next

            ''構造体配列初期化
            Call mudtSetOpsLogFormatNewMach.InitArray()
            Call mudtSetOpsLogFormatNewCarg.InitArray()
            Call mudtSetOpsLogFormatWork.InitArray()
            ''Call mudtSetOpsLogIdDataNewMach.InitArray()     ''☆2012/10/26 K.Tanigawa
            ''Call mudtSetOpsLogIdDataNewCarg.InitArray()     ''☆2012/10/26 K.Tanigawa
            ''Call mudtSetOpsLogIdDataWork.InitArray()        ''☆2012/10/26 K.Tanigawa
            Call mInitialAutoSettingArray(mudtSetChRLPulse)
            Call mInitialAutoSettingArray(mudtSetChRLWork)
            Call mInitialAutoSettingArray(mudtSetChIdoKeido)    'Ver2.0.5.0 緯度経度

            ''ログフォーマットの情報を取得
            Call mCopyStructure(gudt.SetOpsLogFormatM, mudtSetOpsLogFormatNewMach)
            Call mCopyStructure(gudt.SetOpsLogFormatC, mudtSetOpsLogFormatNewCarg)

            ''ログフォーマットの情報を取得  ''☆2012/10/26 K.Tanigawa
            ''           Call mCopyStructure(gudt.SetOpsLogIdDataM, mudtSetOpsLogIdDataNewMach)
            ''           Call mCopyStructure(gudt.SetOpsLogIdDataC, mudtSetOpsLogIdDataNewCarg)

            ''チャンネルグループの名称情報を取得
            Call gMakeChannelGroupData(gudt.SetChInfo, mudtChannelGroupMach)
            Call gMakeChannelGroupData(gudt.SetChInfo, mudtChannelGroupCarg)
            Call gMakeChannelGroupData(gudt.SetChInfo, mudtChannelGroupWork)
            Call mMakeChannelGroupName(gudt.SetChGroupSetM, mudtChannelGroupMach)
            Call mMakeChannelGroupName(gudt.SetChGroupSetC, mudtChannelGroupCarg)
            Call gGetComboCodeName(mudtSaveFormatStrings, gEnmComboType.ctOpsLogFormatListSaveStrings)

            ''ログ印字フラグ[RL]が有効になっているCHをピックアップ
            Call mAutoSettingPickupRL(mudtSetChRLPulse, gCstCodeChTypePulse)
            Call mAutoSettingPickupRL(mudtSetChRLWork, gCstCodeChTypeNothing)   '' CHタイプをAnalogからNothingとする 2014.12.02
            Call mAutoSettingPickupRL(mudtSetChIdoKeido, gCstCodeChTypeAnalog)  'アナログ＝緯度経度

            ''Machinery/Cargoボタンの表示設定
            Call gSetCombineControl(optMachinery, optCargo)

            ''Machinery／Cargo 情報設定
            If optMachinery.Checked Then
                Call mCopyStructure(mudtSetOpsLogFormatNewMach, mudtSetOpsLogFormatWork)
                Call mCopyStructure(mudtChannelGroupMach, mudtChannelGroupWork)
            Else
                Call mCopyStructure(mudtSetOpsLogFormatNewCarg, mudtSetOpsLogFormatWork)
                Call mCopyStructure(mudtChannelGroupCarg, mudtChannelGroupWork)
            End If

            ''グリッド初期設定
            Call mInitialDataGrid()

            ''画面設定
            Call mSetDisplay(mudtSetOpsLogFormatWork)        ''構造体の情報表示
            Call mSetDisplayPreview(mudtSetOpsLogFormatWork) ''プレビューの表示

            ''モード設定（個別設定）
            optModeInd.Checked = True

            ''操作不可設定
            Call mControlRowEnable()

            Call SetOptionColor()       '' Ver1.9.3 2016.01.22 ｵﾌﾟｼｮﾝﾎﾞﾀﾝ色変更

            ''初期化フラグ
            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Machineryボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : パートの切替え
    '--------------------------------------------------------------------
    Private Sub optMach_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMachinery.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Return

            ''グリッドの行設定初期化
            Call mInitialDataGridSettings()

            ''設定値保存
            Call mSetStructure(mudtSetOpsLogFormatWork)

            If optMachinery.Checked Then

                ''Cargo情報の退避
                Call mCopyStructure(mudtSetOpsLogFormatWork, mudtSetOpsLogFormatNewCarg)

                ''Machineryチャンネルグループ情報を作業用構造体に設定
                Call mCopyStructure(mudtChannelGroupMach, mudtChannelGroupWork)

                ''作業用構造体にMachinery情報の設定
                Call mCopyStructure(mudtSetOpsLogFormatNewMach, mudtSetOpsLogFormatWork)

            ElseIf optCargo.Checked Then

                ''Machinery情報の退避
                Call mCopyStructure(mudtSetOpsLogFormatWork, mudtSetOpsLogFormatNewMach)

                ''Cargoチャンネルグループ情報を作業用構造体に設定
                Call mCopyStructure(mudtChannelGroupCarg, mudtChannelGroupWork)

                ''作業用構造体にCargo情報の設定
                Call mCopyStructure(mudtSetOpsLogFormatNewCarg, mudtSetOpsLogFormatWork)

            End If

            ''プレビュー表示
            Call mSetDisplay(mudtSetOpsLogFormatWork)
            Call mSetDisplayPreview(mudtSetOpsLogFormatWork)

            ''操作不可設定
            Call mControlRowEnable()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : モード選択ボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ログフォーマット自動設定ボタン
    '--------------------------------------------------------------------
    Private Sub optModeInd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optModeInd.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Exit Sub

            ''編集モードの選択
            If optModeInd.Checked Then

                '--------------
                ''個別設定
                '--------------
                mblnModeGroup = False

                ''[Counter設定]ボタン無効化
                cmdCounter.Enabled = False

            Else

                '--------------
                ''グループ設定
                '--------------
                mblnModeGroup = True

                ''[Counter設定]ボタン有効化
                cmdCounter.Enabled = True

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Counter設定ボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ログフォーマット自動設定ボタン
    '--------------------------------------------------------------------
    Private Sub cmdCounter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCounter.Click

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Exit Sub

            ''カウンター設定（パルスCH, 運転積算CH）を行う
            Call mAutoSettingCounter(mudtSetOpsLogFormatWork, mudtSetChRLPulse)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Datagridの同期フラグチェックボックスクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : Column1, 2 のスクロール同期
    '--------------------------------------------------------------------
    Private Sub chkScrollSync_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScrollSync.CheckedChanged

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Exit Sub

            ''同期フラグの値取得
            mblnScrollSync = IIf(chkScrollSync.Checked, True, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Insertボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 選択行から１行づつ下にコピー
    '--------------------------------------------------------------------
    Private Sub cmdInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInsert.Click

        Try

            Dim intRowIndex As Integer
            Dim blnFlgDeleteData As Boolean = False

            ''グリッドデータ取得
            If mblnSelectGridCol1 Then
                intRowIndex = grdLogCol1.CurrentCell.RowIndex
            Else
                intRowIndex = grdLogCol2.CurrentCell.RowIndex
            End If

            ''編集ページの最大行Index取得
            Dim intMaxRowIdxOfEditPage As Integer = mAddRowDataMaxRowIdxOfPage(intRowIndex)

            ''編集ページの最終行に設定データがあるかチェック
            If Trim(mudtSetOpsLogFormatWork.strCol1(intMaxRowIdxOfEditPage)) <> "" Or _
               Trim(mudtSetOpsLogFormatWork.strCol2(intMaxRowIdxOfEditPage)) <> "" Then
                blnFlgDeleteData = True
            End If

            ''[YES]以外を選択した時は処理を抜ける
            If blnFlgDeleteData Then
                If MessageBox.Show("There is data in the last line on a page." & vbNewLine & _
                                   "May I Delete it?", _
                                   Me.Text, _
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then Exit Sub
            End If

            ''グリッドの行設定初期化
            Call mInitialDataGridSettings()

            ''設定値を作業用構造体に格納
            Call mSetStructure(mudtSetOpsLogFormatWork)

            ''行挿入
            Call mAddRowData(mudtSetOpsLogFormatWork, intRowIndex, intMaxRowIdxOfEditPage)

            ''表示更新
            Call mSetDisplay(mudtSetOpsLogFormatWork)
            Call mSetDisplayPreview(mudtSetOpsLogFormatWork)

            ''操作不可設定
            Call mControlRowEnable()

            ''ボタン操作可否設定
            Call mControlEnableBtn()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Deleteボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 選択行から１行づつ上にコピー
    '--------------------------------------------------------------------
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

        Try

            Dim intRowIndex As Integer

            ''グリッドデータ取得
            If mblnSelectGridCol1 Then
                intRowIndex = grdLogCol1.CurrentCell.RowIndex
            Else
                intRowIndex = grdLogCol2.CurrentCell.RowIndex
            End If

            ''編集ページの最大行Index取得
            Dim intMaxRowIdxOfEditPage As Integer = mAddRowDataMaxRowIdxOfPage(intRowIndex)

            ''グリッドの行設定初期化
            Call mInitialDataGridSettings()

            ''行削除
            Call mDeleteRowData(mudtSetOpsLogFormatWork, intRowIndex, intMaxRowIdxOfEditPage)

            ''表示更新
            Call mSetDisplay(mudtSetOpsLogFormatWork)
            Call mSetDisplayPreview(mudtSetOpsLogFormatWork)

            ''操作不可設定
            Call mControlRowEnable()

            ''ボタン操作可否設定
            Call mControlEnableBtn()

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
            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を作業用構造体に格納
            Call mSetStructure(mudtSetOpsLogFormatWork)

            ''設定値の保存
            If optMachinery.Checked Then Call mCopyStructure(mudtSetOpsLogFormatWork, mudtSetOpsLogFormatNewMach)
            If optCargo.Checked Then Call mCopyStructure(mudtSetOpsLogFormatWork, mudtSetOpsLogFormatNewCarg)

            ''データが変更されているかチェック
            blnMach = mChkStructureEquals(mudtSetOpsLogFormatNewMach, gudt.SetOpsLogFormatM)
            blnCarg = mChkStructureEquals(mudtSetOpsLogFormatNewCarg, gudt.SetOpsLogFormatC)

            ''データが変更されている場合
            If (Not blnMach) Or (Not blnCarg) Then

                ''変更された場合は設定を更新する
                If Not blnMach Then Call mCopyStructure(mudtSetOpsLogFormatNewMach, gudt.SetOpsLogFormatM)
                If Not blnCarg Then Call mCopyStructure(mudtSetOpsLogFormatNewCarg, gudt.SetOpsLogFormatC)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsLogFormatM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsLogFormatC = 1
                If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogFormatM = 1
                If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogFormatC = 1

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Exitボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォームを閉じる
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
            'Ver2.0.2.8 ﾒｯｾｰｼﾞを出す
            If MessageBox.Show("Do you Clear the Log datas?", _
                                           Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Call gInitSetOpsLogFormat(mudtSetOpsLogFormatNewMach)
                Call gInitSetOpsLogFormat(mudtSetOpsLogFormatNewCarg)

                'グリッド初期化
                Call mInitialDataGrid()
                Call mSetDisplay(mudtSetOpsLogFormatNewMach)
            End If

            'Me.Close()
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
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try
            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            ''設定値を作業用構造体に格納
            Call mSetStructure(mudtSetOpsLogFormatWork)

            ''設定値の保存
            If optMachinery.Checked Then Call mCopyStructure(mudtSetOpsLogFormatWork, mudtSetOpsLogFormatNewMach)
            If optCargo.Checked Then Call mCopyStructure(mudtSetOpsLogFormatWork, mudtSetOpsLogFormatNewCarg)

            ''データが変更されているかチェック
            blnMach = mChkStructureEquals(mudtSetOpsLogFormatNewMach, gudt.SetOpsLogFormatM)
            blnCarg = mChkStructureEquals(mudtSetOpsLogFormatNewCarg, gudt.SetOpsLogFormatC)

            ''データが変更されている場合
            If (Not blnMach) Or (Not blnCarg) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        ''変更された場合は設定を更新する
                        If Not blnMach Then Call mCopyStructure(mudtSetOpsLogFormatNewMach, gudt.SetOpsLogFormatM)
                        If Not blnCarg Then Call mCopyStructure(mudtSetOpsLogFormatNewCarg, gudt.SetOpsLogFormatC)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytOpsLogFormatM = 1
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytOpsLogFormatC = 1
                        If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogIdDataM = 1 ''☆2012/10/26 K.Tanigawa
                        If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogIdDataC = 1

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

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォームのインスタンスを破棄する
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '--------------------------------------------------------------------
    ' 機能      : Optionボタン　クリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ログ特殊設定用のフォームを表示
    '--------------------------------------------------------------------
    Private Sub cmdOption_Click(sender As System.Object, e As System.EventArgs) Handles cmdOption.Click
        frmOpsLogOption.Show()
    End Sub


#Region "グリッドイベント"

    '--------------------------------------------------------------------
    ' 機能説明  ： スクロールイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： ２つのdatagridを縦方向に同期させる
    '--------------------------------------------------------------------
    Private Sub grdLogCol1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles grdLogCol1.Scroll

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Exit Sub

            ''同期フラグにチェックが入っていない場合は同期させない
            If mblnScrollSync Then
                grdLogCol2.FirstDisplayedScrollingRowIndex = grdLogCol1.FirstDisplayedScrollingRowIndex
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdLogCol2_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles grdLogCol2.Scroll

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Exit Sub

            ''同期フラグにチェックが入っていない場合は同期させない
            If mblnScrollSync Then
                grdLogCol1.FirstDisplayedScrollingRowIndex = grdLogCol2.FirstDisplayedScrollingRowIndex
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : クリックイベント
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : グリッド選択ラグ設定、Insert/Deleteボタン操作設定
    '--------------------------------------------------------------------
    Private Sub grdLogCol1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdLogCol1.Click

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Exit Sub

            Dim intRowIndex As Integer = grdLogCol1.CurrentCell.RowIndex

            ''編集ページの最大行Index取得
            Dim intMaxRowIdxOfEditPage As Integer = mAddRowDataMaxRowIdxOfPage(intRowIndex)

            ''grdCol1選択
            mblnSelectGridCol1 = True


            ''ボタン操作可否設定
            Call mControlEnableBtn(grdLogCol1)

            ''コピーペースト用
            gintRow = intRowIndex
            gintMaxRowOfEditPage = intMaxRowIdxOfEditPage

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdLogCol2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdLogCol2.Click

        Try

            ''初期化中は処理しない
            If mblnInitFlg Then Exit Sub

            Dim intRowIndex As Integer = grdLogCol2.CurrentCell.RowIndex

            ''編集ページの最大行Index取得
            Dim intMaxRowIdxOfEditPage As Integer = mAddRowDataMaxRowIdxOfPage(intRowIndex)

            ''grdCol2選択
            mblnSelectGridCol1 = False

            ''ボタン操作可否設定
            Call mControlEnableBtn(grdLogCol2)

            ''コピーペースト用
            gintRow = intRowIndex
            gintMaxRowOfEditPage = intMaxRowIdxOfEditPage

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ダブルクリックイベント
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ２列目だけダブルクリックイベントを有効にする
    '--------------------------------------------------------------------
    Private Sub grdLogCol1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdLogCol1.DoubleClick

        Try

            ''初期化中は処理を抜ける
            If mblnInitFlg Then Exit Sub

            ''グリッドの保留中の変更を全て適用させる
            Call grdLogCol1.EndEdit()

            ''NO項目、プレビュー項目のみ処理実施
            If grdLogCol1.CurrentCell.OwningColumn.Name = grdLogCol1.Columns(1).Name Then

                ''詳細画面の表示
                Call mDispLogFormatDetail(grdLogCol1, _
                                          mEnmGrid.Col1, _
                                          grdLogCol1.CurrentCell.RowIndex, _
                                          mblnModeGroup, _
                                          mudtSetChRLWork)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdLogCol2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdLogCol2.DoubleClick

        Try

            ''初期化中は処理を抜ける
            If mblnInitFlg Then Exit Sub

            ''グリッドの保留中の変更を全て適用させる
            Call grdLogCol2.EndEdit()

            ''NO項目、プレビュー項目のみ処理実施
            If grdLogCol2.CurrentCell.OwningColumn.Name = grdLogCol2.Columns(1).Name Then

                ''詳細画面の表示
                Call mDispLogFormatDetail(grdLogCol2, _
                                          mEnmGrid.Col2, _
                                          grdLogCol2.CurrentCell.RowIndex, _
                                          mblnModeGroup, _
                                          mudtSetChRLWork)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： プルダウンリストの項目を変更した時の処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub grdLogCol1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLogCol1.CellValueChanged

        Try

            ''処理を抜ける条件
            If mblnInitFlg Then Return
            If e.RowIndex < 0 Or e.RowIndex > grdLogCol1.RowCount - 1 Then Return
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdLogCol1.ColumnCount - 1 Then Return

            If grdLogCol1.CurrentCell.OwningColumn.Name = grdLogCol1.Columns(0).Name Then

                ''コンボボックス変更時に処理実施
                Call mChangePullDownMenu(grdLogCol1, e.RowIndex)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdLogCol2_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLogCol2.CellValueChanged

        Try

            ''処理を抜ける条件
            If mblnInitFlg Then Return
            If e.RowIndex < 0 Or e.RowIndex > grdLogCol2.RowCount - 1 Then Return
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdLogCol2.ColumnCount - 1 Then Return

            If grdLogCol2.CurrentCell.OwningColumn.Name = grdLogCol2.Columns(0).Name Then

                ''コンボボックス変更時に処理実施
                Call mChangePullDownMenu(grdLogCol2, e.RowIndex)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： 行変更の感知（クリック、矢印キー）
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub grdLogCol1_RowValidated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLogCol1.RowValidated

        Try

            ''処理を抜ける条件
            If mblnInitFlg Then Return

            ''grdCol1選択
            mblnSelectGridCol1 = True

            ''Insert/Deleteボタン操作設定
            Call Timer1.Start()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdLogCol2_RowValidated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLogCol2.RowValidated

        Try

            ''処理を抜ける条件
            If mblnInitFlg Then Return

            ''grdCol1選択
            mblnSelectGridCol1 = False

            ''Insert/Deleteボタン操作設定
            Call Timer1.Start()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力制限・入力値フォーマット"

    '--------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベントを発生させる
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub grdLogCol1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdLogCol1.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdLogCol1_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdLogCol1_KeyPress

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdLogCol2_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdLogCol2.EditingControlShowing

        Try

            Dim dgv As DataGridView = CType(sender, DataGridView)

            If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

                Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)

                ''イベントハンドラを削除
                RemoveHandler tb.KeyPress, AddressOf grdLogCol2_KeyPress

                ''イベントハンドラを追加する
                AddHandler tb.KeyPress, AddressOf grdLogCol2_KeyPress

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
    Private Sub grdLogCol1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdLogCol1.KeyPress

        Try

            Dim strColumnName As String = grdLogCol1.CurrentCell.OwningColumn.Name
            Dim intRowIndex As Integer = grdLogCol1.CurrentCell.RowIndex

            ''[txtColumn1Item]項目の入力制限
            If strColumnName = grdLogCol1.Columns(1).Name Then

                Select Case CCInt(grdLogCol1(0, intRowIndex).Value)
                    Case gCstCodeOpsLogFormatTypeGroup

                        ''入力桁数制限
                        e.Handled = gCheckTextInput(2, sender, e.KeyChar)

                        ''グリッドイベントの連続実行防止フラグ
                        mblnDispDetail = False

                    Case gCstCodeOpsLogFormatTypeCh

                        ''入力桁数制限
                        e.Handled = gCheckTextInput(5, sender, e.KeyChar)

                        ''グリッドイベントの連続実行防止フラグ
                        mblnDispDetail = False

                    Case Else

                        ''ChとGroup以外は入力不可
                        e.Handled = True

                End Select

            End If

            ''コピーペースト
            Call mDipsErrMsg()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdLogCol2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdLogCol2.KeyPress

        Try

            Dim strColumnName As String = grdLogCol2.CurrentCell.OwningColumn.Name
            Dim intRowIndex As Integer = grdLogCol2.CurrentCell.RowIndex

            ''[txtColumn2Item]項目の入力制限
            If strColumnName = grdLogCol2.Columns(1).Name Then

                Select Case CCInt(grdLogCol2(0, intRowIndex).Value)
                    Case gCstCodeOpsLogFormatTypeGroup

                        ''入力桁数制限
                        e.Handled = gCheckTextInput(2, sender, e.KeyChar)

                        ''グリッドイベントの連続実行防止フラグ
                        mblnDispDetail = False

                    Case gCstCodeOpsLogFormatTypeCh

                        ''入力桁数制限
                        e.Handled = gCheckTextInput(5, sender, e.KeyChar)

                        ''グリッドイベントの連続実行防止フラグ
                        mblnDispDetail = False

                    Case Else

                        ''ChとGroup以外は入力不可
                        e.Handled = True

                End Select

            End If

            ''コピーペースト
            Call mDipsErrMsg()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： 入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： CH／GROUPの入力NOをフォーマットする
    '--------------------------------------------------------------------
    Private Sub grdLogCol1_CellValidated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLogCol1.CellValidated

        Try

            ''処理を抜ける条件
            If mblnInitFlg Then Return
            If mblnDispDetail Then Return
            If e.RowIndex < 0 Or e.RowIndex > grdLogCol1.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdLogCol1.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            ''キャスト処理
            Dim dgv As DataGridView = CType(sender, DataGridView)

            ''No入力欄以外の場合は処理を抜ける
            If dgv.CurrentCell.OwningColumn.Name <> grdLogCol1.Columns(1).Name Then Exit Sub

            ''入力値のフォーマット処理
            Call mFormatStrings(grdLogCol1, _
                                mEnmGrid.Col1, _
                                e.RowIndex, _
                                mudtSetOpsLogFormatWork.strCol1)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub grdLogCol2_CellValidated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLogCol2.CellValidated

        Try

            ''処理を抜ける条件
            If mblnInitFlg Then Return
            If mblnDispDetail Then Return
            If e.RowIndex < 0 Or e.RowIndex > grdLogCol2.RowCount - 1 Then Return ''行数が0より小さい、もしくは最大行数より大きい場合
            If e.ColumnIndex < 0 Or e.ColumnIndex > grdLogCol2.ColumnCount - 1 Then Return ''列数が0より小さい、もしくは最大列数より大きい場合

            ''キャスト処理
            Dim dgv As DataGridView = CType(sender, DataGridView)

            ''No入力欄以外の場合は処理を抜ける
            If dgv.CurrentCell.OwningColumn.Name <> grdLogCol2.Columns(1).Name Then Exit Sub

            ''入力値のフォーマット処理
            Call mFormatStrings(grdLogCol2, _
                                mEnmGrid.Col2, _
                                e.RowIndex, _
                                mudtSetOpsLogFormatWork.strCol2)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Try

            Timer1.Stop()

            ''ボタン操作可否設定
            Call mControlEnableBtn()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

#Region "自動設定"

    '--------------------------------------------------------------------
    ' 機能      : ログフォーマットの自動設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) ログフォーマット構造体
    '           : ARG2 - (I ) 自動生成用構造体
    ' 機能説明  : ログフォーマットの自動設定処理
    '--------------------------------------------------------------------
    Private Sub mAutoSettingCounter(ByVal hudtSetLogFormat As gTypSetOpsLogFormat, _
                                    ByVal hudtSetChRL As gTypLogFormatPickCH)

        Try

            ''データクリア
            Call mAutoInitData(hudtSetLogFormat)

            ''[COUNTER TITLE]設定
            Call mAutoSettingCNTTITLE(mudtSetOpsLogFormatWork)

            ''RLが有効のCHの中でパルスCHのものをピックアップ（CH順）
            Call mAutoSettingSortPulse(hudtSetLogFormat, hudtSetChRL)

            ''RLが有効のCHの中で運転積算CHのものをピックアップ（CH順）
            Call mAutoSettingSortRev(hudtSetLogFormat, hudtSetChRL)

            'Ver2.0.5.0 緯度経度もピックアップ
            Call mAutoSettingSortIdoKeido(hudtSetLogFormat, mudtSetChIdoKeido)


            ''[ANALOG TITLE]設定
            Call mAutoSettingANATITLE(hudtSetLogFormat)

            ''画面設定
            Call mSetDisplay(hudtSetLogFormat)          ''構造体の情報表示
            Call mSetDisplayPreview(hudtSetLogFormat)   ''プレビューの表示
            Call mControlRowEnable()                    ''操作不可設定

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ログフォーマット構造体のデータ初期化
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) 自動生成用構造体
    ' 機能説明  : ログフォーマット構造体のデータを初期化する
    '--------------------------------------------------------------------
    Private Sub mAutoInitData(ByRef hudtSet As gTypSetOpsLogFormat)

        Try

            With hudtSet

                For i As Integer = LBound(.strCol1) To UBound(.strCol1)

                    ''データ初期化
                    .strCol1(i) = ""
                    .strCol2(i) = ""

                    ''ReadOnly解除
                    Call mControlEnableDataColClear(i)

                Next

                ''行カラーとページ区切り線
                Call mInitialDataGridColorAndSplit(grdLogCol1)
                Call mInitialDataGridColorAndSplit(grdLogCol2)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 定時ログ印字が有効になっている指定CHデータを取得
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) 自動生成用構造体
    '           : ARG2 - (I ) チャンネル種別コード
    ' 機能説明  : [RL]フラグが有効になっているCHをピックアップする
    '--------------------------------------------------------------------
    Private Sub mAutoSettingPickupRL(ByRef hudtset As gTypLogFormatPickCH, _
                                     ByVal hintChType As Integer)

        Try

            Dim intArrayNo As Integer

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon
                    'Ver2.0.5.0 緯度経度対応
                    Select Case hintChType
                        Case gCstCodeChTypeNothing
                            'パルス積算緯度経度、以外
                            '定時ログ印字フラグが有効でかつ、パルス、運転積算以外のデータを取得
                            If (.shtChType <> gCstCodeChTypePulse) And gBitCheck(.shtFlag2, 0) And _
                                (.shtData <> gCstCodeChDataTypeAnalogLatitude) And (.shtData <> gCstCodeChDataTypeAnalogLongitude) Then

                                hudtset.udtSetChRL(intArrayNo).intChType = .shtChType
                                hudtset.udtSetChRL(intArrayNo).blnChTypeRev = False
                                hudtset.udtSetChRL(intArrayNo).intGroupNo = .shtGroupNo
                                hudtset.udtSetChRL(intArrayNo).intChno = .shtChno

                                '配列のカウントをインクリメント（次の配列へ）
                                intArrayNo += 1
                            End If
                        Case gCstCodeChTypeAnalog
                            '緯度経度
                            If (.shtChType = gCstCodeChTypeAnalog) And gBitCheck(.shtFlag2, 0) And _
                                ((.shtData = gCstCodeChDataTypeAnalogLatitude) Or (.shtData = gCstCodeChDataTypeAnalogLongitude)) Then

                                hudtset.udtSetChRL(intArrayNo).intChType = .shtChType
                                hudtset.udtSetChRL(intArrayNo).blnChTypeRev = False
                                hudtset.udtSetChRL(intArrayNo).intGroupNo = .shtGroupNo
                                hudtset.udtSetChRL(intArrayNo).intChno = .shtChno

                                '配列のカウントをインクリメント（次の配列へ）
                                intArrayNo += 1
                            End If
                        Case Else
                            '定時ログ印字フラグが有効でかつ、指定のチャンネル種別コードのデータを取得
                            If (.shtChType = hintChType) And gBitCheck(.shtFlag2, 0) Then

                                hudtset.udtSetChRL(intArrayNo).intChType = .shtChType
                                hudtset.udtSetChRL(intArrayNo).blnChTypeRev = mCheckChRev(.shtData)
                                hudtset.udtSetChRL(intArrayNo).intGroupNo = .shtGroupNo
                                hudtset.udtSetChRL(intArrayNo).intChno = .shtChno
                                ''配列のカウントをインクリメント（次の配列へ）
                                intArrayNo += 1
                            End If
                    End Select

                    '' パルス、運転積算とそれ以外に変更     2014.12.02
                    'If hintChType = gCstCodeChTypeNothing Then
                    '    ''定時ログ印字フラグが有効でかつ、パルス、運転積算以外のデータを取得
                    '    If (.shtChType <> gCstCodeChTypePulse) And gBitCheck(.shtFlag2, 0) Then

                    '        hudtset.udtSetChRL(intArrayNo).intChType = .shtChType
                    '        hudtset.udtSetChRL(intArrayNo).blnChTypeRev = False
                    '        hudtset.udtSetChRL(intArrayNo).intGroupNo = .shtGroupNo
                    '        hudtset.udtSetChRL(intArrayNo).intChno = .shtChno

                    '        ''配列のカウントをインクリメント（次の配列へ）
                    '        intArrayNo += 1

                    '    End If
                    'Else
                    '    ''定時ログ印字フラグが有効でかつ、指定のチャンネル種別コードのデータを取得
                    '    If (.shtChType = hintChType) And gBitCheck(.shtFlag2, 0) Then

                    '        hudtset.udtSetChRL(intArrayNo).intChType = .shtChType
                    '        hudtset.udtSetChRL(intArrayNo).blnChTypeRev = mCheckChRev(.shtData)
                    '        hudtset.udtSetChRL(intArrayNo).intGroupNo = .shtGroupNo
                    '        hudtset.udtSetChRL(intArrayNo).intChno = .shtChno

                    '        ''配列のカウントをインクリメント（次の配列へ）
                    '        intArrayNo += 1

                    '    End If

                    'End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : パルスCHのピックアップ
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) ログフォーマット構造体
    '           : ARG2 - (I ) 自動生成用構造体
    ' 機能説明  : RLが有効のCHの中でパルスCHのものをピックアップ（CH順）
    '--------------------------------------------------------------------
    Private Sub mAutoSettingSortPulse(ByRef hudtSetLog As gTypSetOpsLogFormat, _
                                      ByVal hudtSetRL As gTypLogFormatPickCH)

        Try

            Dim intArrayNo As Integer = 2   ''CNTTITLEが配列0に設定されているため
            Dim strCH As String = ""
            Dim intChno() As Integer
            ReDim intChno(UBound(gudt.SetChInfo.udtChannel))

            ''ソート用のチャンネル番号の取得
            Call mSortChno(hudtSetRL, intChno, gCstCodeChTypePulse, False)

            ''チャンネル番号の並び替え
            Call Array.Sort(intChno)

            For i As Integer = 0 To UBound(intChno)
                If intChno(i) <> 0 Then
                    hudtSetLog.strCol1(intArrayNo) = gCstNameOpsLogFormatStringsCH & intChno(i).ToString("0000")
                    intArrayNo += 1
                End If
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 運転積算CHのピックアップ
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) ログフォーマット構造体
    '           : ARG2 - (I ) 自動生成用構造体
    ' 機能説明  : RLが有効のCHの中で運転積算CHのものをピックアップ（CH順）
    '--------------------------------------------------------------------
    Private Sub mAutoSettingSortRev(ByRef hudtSetLog As gTypSetOpsLogFormat, _
                                    ByVal hudtSetRL As gTypLogFormatPickCH)

        Try

            Dim intArrayNo As Integer
            Dim strCH As String = ""
            Dim intChno() As Integer
            ReDim intChno(UBound(gudt.SetChInfo.udtChannel))

            ''ソート用のチャンネル番号の取得
            Call mSortChno(hudtSetRL, intChno, gCstCodeChTypePulse, True)

            ''チャンネル番号の並び替え
            Call Array.Sort(intChno)

            ''現在使用している配列の最後のIndexを取得
            For i As Integer = 0 To UBound(hudtSetLog.strCol1)
                If hudtSetLog.strCol1(i) <> "" Then
                    intArrayNo = i
                End If
            Next

            'Ver2.0.1.9 intArrayNoが0の場合、ｶｳﾝﾀﾀｲﾄﾙのみのため、0ではなく、１とする
            If intArrayNo = 0 Then
                intArrayNo = 1
            End If

            ''運転積算CHのデータを設定する最初の配列Index設定（使用している最後のIndex＋1）
            intArrayNo += 1

            ''運転積算CHのデータを設定
            For j As Integer = 0 To UBound(hudtSetRL.udtSetChRL)
                If intChno(j) <> 0 Then
                    hudtSetLog.strCol1(intArrayNo) = gCstNameOpsLogFormatStringsCH & intChno(j).ToString("0000")
                    intArrayNo += 1
                End If
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'アナログCH、緯度経度のﾋﾟｯｸｱｯﾌﾟ
    Private Sub mAutoSettingSortIdoKeido(ByRef hudtSetLog As gTypSetOpsLogFormat, _
                                    ByVal hudtSetRL As gTypLogFormatPickCH)

        Try

            Dim intArrayNo As Integer
            Dim strCH As String = ""
            Dim intChno() As Integer
            ReDim intChno(UBound(gudt.SetChInfo.udtChannel))

            ''ソート用のチャンネル番号の取得
            Call mSortChno(hudtSetRL, intChno, gCstCodeChTypeAnalog, False)

            ''チャンネル番号の並び替え
            Call Array.Sort(intChno)

            ''現在使用している配列の最後のIndexを取得
            For i As Integer = 0 To UBound(hudtSetLog.strCol1)
                If hudtSetLog.strCol1(i) <> "" Then
                    intArrayNo = i
                End If
            Next

            'Ver2.0.1.9 intArrayNoが0の場合、ｶｳﾝﾀﾀｲﾄﾙのみのため、0ではなく、１とする
            If intArrayNo = 0 Then
                intArrayNo = 1
            End If

            'CHのデータを設定する最初の配列Index設定（使用している最後のIndex＋1）
            intArrayNo += 1

            '2以外なら空白行を打つ
            If intArrayNo > 2 And intChno(0) <> 0 Then
                cmbSave.SelectedIndex = gCstCodeOpsLogFormatTypeSpace
                hudtSetLog.strCol1(intArrayNo) = Trim(cmbSave.Text)
                intArrayNo += 1
            End If

            ''運転積算CHのデータを設定
            For j As Integer = 0 To UBound(hudtSetRL.udtSetChRL)
                If intChno(j) <> 0 Then
                    hudtSetLog.strCol1(intArrayNo) = gCstNameOpsLogFormatStringsCH & intChno(j).ToString("0000")
                    intArrayNo += 1
                End If
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 固定フォーマット（CNTTITLE設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) ログフォーマット構造体
    ' 機能説明  : 自動設定を行った際の固定フォーマット設定
    '--------------------------------------------------------------------
    Private Sub mAutoSettingCNTTITLE(ByRef hudtSetLog As gTypSetOpsLogFormat)

        Try

            ''[CNTTITLE]設定
            cmbSave.SelectedIndex = gCstCodeOpsLogFormatTypeCounterTitle
            hudtSetLog.strCol1(0) = Trim(cmbSave.Text)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 固定フォーマット（ANATITLE設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) ログフォーマット構造体
    ' 機能説明  : 自動設定を行った際の固定フォーマット設定
    '--------------------------------------------------------------------
    Private Sub mAutoSettingANATITLE(ByRef hudtSetLog As gTypSetOpsLogFormat)

        Try

            Dim intArrayNo As Integer

            ''現在使用している配列の最後のIndexを取得
            For i As Integer = 0 To UBound(hudtSetLog.strCol1)
                If hudtSetLog.strCol1(i) <> "" Then
                    intArrayNo = i
                End If
            Next

            ''[SPACE]を設定する配列Index設定（使用している最後のIndex＋1）
            If intArrayNo = 0 Then
                intArrayNo += 2
            Else
                intArrayNo += 1
            End If

            ''[SPACE]設定
            cmbSave.SelectedIndex = gCstCodeOpsLogFormatTypeSpace
            mudtSetOpsLogFormatWork.strCol1(intArrayNo) = Trim(cmbSave.Text)

            ''[ANALOG TITLE]を設定する配列Index設定（使用している最後のIndex＋1）
            intArrayNo += 1

            ''[ANALOG TITLE]設定
            cmbSave.SelectedIndex = gCstCodeOpsLogFormatTypeAnalogTitle
            mudtSetOpsLogFormatWork.strCol1(intArrayNo) = Trim(cmbSave.Text)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 運転積算CHの判定
    ' 返り値    : パルスCHのデータ種別（TRUE:運転積算CH, FALSE:パルスCH）
    ' 引き数    : ARG1 - (I ) データ種別コード
    ' 機能説明  : データ種別が運転積算の場合はTRUEを返す
    '--------------------------------------------------------------------
    Private Function mCheckChRev(ByVal hintChType As Integer) As Boolean

        Dim blnRtn As Boolean = False

        Try

            '' Ver1.11.8.3 2016.11.08 運転積算 通信CH追加
            '' Ver1.12.0.1 2017.01.13 運転積算種類追加
            If hintChType = gCstCodeChDataTypePulseRevoTotalHour Or _
               hintChType = gCstCodeChDataTypePulseRevoTotalMin Or _
               hintChType = gCstCodeChDataTypePulseRevoDayHour Or _
               hintChType = gCstCodeChDataTypePulseRevoDayMin Or _
               hintChType = gCstCodeChDataTypePulseRevoLapHour Or _
               hintChType = gCstCodeChDataTypePulseRevoLapMin Or _
                hintChType = gCstCodeChDataTypePulseRevoExtDev Or _
                hintChType = gCstCodeChDataTypePulseRevoExtDevTotalMin Or _
                hintChType = gCstCodeChDataTypePulseRevoExtDevDayHour Or _
                hintChType = gCstCodeChDataTypePulseRevoExtDevDayMin Or _
                hintChType = gCstCodeChDataTypePulseRevoExtDevLapHour Or _
                hintChType = gCstCodeChDataTypePulseRevoExtDevLapMin Then
                ''hintChType = gCstCodeChDataTypePulseExtDev Then

                blnRtn = True

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return blnRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : チャンネル番号のみの配列を作成する
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) パルスCHのみの構造体
    '           : ARG2 - ( O) チャンネル番号順にソートした配列
    '           : ARG3 - (I ) CHタイプ（TRUE:運転積算CH, FALSE:パルスCH）
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub mSortChno(ByVal hudtSetRL As gTypLogFormatPickCH, _
                          ByRef hintChno() As Integer, _
                          ByVal hintChType As Integer, _
                          ByVal hblnChTypeRev As Boolean)

        Try

            Dim intArrayCnt As Integer

            For i As Integer = 0 To UBound(hudtSetRL.udtSetChRL)

                If hudtSetRL.udtSetChRL(i).intChType = hintChType And _
                   hudtSetRL.udtSetChRL(i).blnChTypeRev = hblnChTypeRev And _
                   hudtSetRL.udtSetChRL(i).intChno <> 0 Then

                    hintChno(intArrayCnt) = hudtSetRL.udtSetChRL(i).intChno

                    intArrayCnt += 1

                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル番号のみの配列を作成する(アナログCH)
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) パルスCHのみの構造体
    '           : ARG2 - ( O) チャンネル番号順にソートした配列
    '           : ARG3 - (I ) CHタイプ（TRUE:運転積算CH, FALSE:パルスCH）
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub mSortGroupChno(ByVal hudtSetRL As gTypLogFormatPickCH, _
                               ByRef hintChno() As Integer, _
                               ByVal hintGroupNo As Integer)

        Try

            Dim intArrayCnt As Integer

            'Ver2.0.0.4
            '配列側のグループ番号は番号ではなく、添え字+1が格納されているため変更
            '必ずマシナリのため、M固定でOK
            Dim intGrNo As Integer = 0
            For j As Integer = 0 To UBound(gudt.SetChGroupSetM.udtGroup.udtGroupInfo) Step 1
                With gudt.SetChGroupSetM.udtGroup.udtGroupInfo(j)
                    If .shtGroupNo = hintGroupNo Then
                        intGrNo = j + 1
                        Exit For
                    End If
                End With
            Next j

            For i As Integer = 0 To UBound(hudtSetRL.udtSetChRL)

                If hudtSetRL.udtSetChRL(i).intChno <> 0 Then
                    'Ver2.0.0.4 グループ番号
                    'If hudtSetRL.udtSetChRL(i).intGroupNo = hintGroupNo Then    '' グループ番号チェック
                    If hudtSetRL.udtSetChRL(i).intGroupNo = intGrNo Then    '' グループ番号チェック

                        hintChno(intArrayCnt) = hudtSetRL.udtSetChRL(i).intChno
                        intArrayCnt += 1

                    End If
                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 配列の初期化
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) 自動生成用構造体 
    ' 機能説明  : 自動生成用配列の初期化
    '--------------------------------------------------------------------
    Private Sub mInitialAutoSettingArray(ByRef hudtset As gTypLogFormatPickCH)

        Try

            With hudtset

                ReDim .udtSetChRL(UBound(gudt.SetChInfo.udtChannel))

                For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)
                    .udtSetChRL(i).intGroupNo = 0
                    .udtSetChRL(i).intChno = 0
                Next

            End With

        Catch ex As Exception

        End Try

    End Sub

#Region "全自動設定機能"
    'Ver2.0.7.C
    '全自動設定
    ' １．Counterを設定
    ' ２．残りのデータを全点設定
    ' ３．左から右へ、右に入りきれない場合は、次のページへ
    Private Sub subSetAllLog()
        Try
            '>>>データクリア
            Call mAutoInitData(mudtSetOpsLogFormatWork)

            '>>>[COUNTER TITLE]設定
            Call mAutoSettingCNTTITLE(mudtSetOpsLogFormatWork)

            '>>>データピックアップ
            'RLが有効のCHの中でパルスCHのものをピックアップ（CH順）
            Call mAutoSettingSortPulse(mudtSetOpsLogFormatWork, mudtSetChRLPulse)
            'RLが有効のCHの中で運転積算CHのものをピックアップ（CH順）
            Call mAutoSettingSortRev(mudtSetOpsLogFormatWork, mudtSetChRLPulse)
            '緯度経度もピックアップ
            Call mAutoSettingSortIdoKeido(mudtSetOpsLogFormatWork, mudtSetChIdoKeido)

            '>>>[ANALOG TITLE]設定
            'Call mAutoSettingANATITLE(mudtSetOpsLogFormatWork)

            '>>>全Grp反映
            Call subSetAllGrp()


            '>>>画面反映
            Call mSetDisplay(mudtSetOpsLogFormatWork)           '構造体の情報表示
            Call mSetDisplayPreview(mudtSetOpsLogFormatWork)    'プレビューの表示
            Call mControlRowEnable()                            '操作不可設定

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    'Grp全点セット
    Private Sub subSetAllGrp()
        Try
            Dim intGrpCount As Integer

            'CHno格納変数
            Dim intChno() As Integer
            ReDim intChno(UBound(gudt.SetChInfo.udtChannel))

            Dim intArrayNo As Integer

            Dim intNowPage As Integer   '現在のページ(0～)
            Dim intNow1or2 As Integer   '現在の一覧(1=左,2=右)

            '開始行取得
            '現在使用している配列の最後のIndexを取得
            For i As Integer = 0 To UBound(mudtSetOpsLogFormatWork.strCol1)
                If mudtSetOpsLogFormatWork.strCol1(i) <> "" Then
                    intArrayNo = i
                End If
            Next i

            '今のページ
            intNowPage = 0
            For i As Integer = 0 To UBound(mintMaxRowNo)
                If mintMaxRowNo(i) > intArrayNo Then
                    intNowPage = i
                    Exit For
                End If
            Next i

            '1～36グループﾙｰﾌﾟ
            intNow1or2 = 1
            Dim iGrp As Integer
            For iGrp = 1 To 36 Step 1
                'ソート用変数初期化
                For i As Integer = 0 To UBound(intChno) Step 1
                    intChno(i) = 0
                Next i
                'ソート用のグループ単位のチャンネル番号の取得
                Call mSortGroupChno(mudtSetChRLWork, intChno, iGrp)
                'チャンネル番号の並び替え
                Call Array.Sort(intChno)

                '件数取得
                intGrpCount = 0
                For i As Integer = 0 To UBound(intChno) Step 1
                    If intChno(i) <> 0 Then
                        intGrpCount = intGrpCount + 1
                    End If
                Next i

                'GroupSet
                '件数があれば
                If intGrpCount > 0 Then
                    'SPACEとGROUPをプラス
                    intArrayNo = intArrayNo + 2
                    '１ページのMAXレコード数を超えた場合は左右切替
                    If intArrayNo > mintMaxRowNo(intNowPage) Then
                        If intNow1or2 = 1 Then
                            '左なら右にして、行を該当ページの頭へ
                            intNow1or2 = 2
                            If intNowPage <= 0 Then
                                intArrayNo = 3
                            Else
                                intArrayNo = mintMaxRowNo(intNowPage - 1)
                            End If
                        Else
                            '右なら左にして、ページを＋１で該当ページの頭へ
                            intNow1or2 = 1
                            intNowPage = intNowPage + 1
                            intArrayNo = mintMaxRowNo(intNowPage - 1)
                        End If
                    End If

                    '値セット
                    If intNow1or2 = 1 Then
                        '左=strCol1
                        ' SPACE
                        cmbSave.SelectedIndex = gCstCodeOpsLogFormatTypeSpace
                        mudtSetOpsLogFormatWork.strCol1(intArrayNo - 1) = Trim(cmbSave.Text)
                        'GROUP
                        mudtSetOpsLogFormatWork.strCol1(intArrayNo) = gCstNameOpsLogFormatStringsGROUP & iGrp.ToString("00")
                    Else
                        '右=strCol2
                        ' SPACE
                        cmbSave.SelectedIndex = gCstCodeOpsLogFormatTypeSpace
                        mudtSetOpsLogFormatWork.strCol2(intArrayNo - 1) = Trim(cmbSave.Text)
                        'GROUP
                        mudtSetOpsLogFormatWork.strCol2(intArrayNo) = gCstNameOpsLogFormatStringsGROUP & iGrp.ToString("00")
                    End If
                End If

                For i As Integer = 0 To UBound(intChno)
                    'チャンネル情報設定     ソートしたCHを使用
                    If intChno(i) <> 0 Then
                        intArrayNo += 1
                        '１ページのMAXレコード数を超えた場合は左右切替
                        If intArrayNo >= mintMaxRowNo(intNowPage) Then
                            If intNow1or2 = 1 Then
                                '左なら右にして、行を該当ページの頭へ
                                intNow1or2 = 2
                                If intNowPage <= 0 Then
                                    intArrayNo = 2
                                Else
                                    intArrayNo = mintMaxRowNo(intNowPage - 1)
                                End If
                            Else
                                '右なら左にして、ページを＋１で該当ページの頭へ
                                intNow1or2 = 1
                                intNowPage = intNowPage + 1
                                intArrayNo = mintMaxRowNo(intNowPage - 1)
                            End If
                        End If

                        '値セット
                        If intNow1or2 = 1 Then
                            '左=strCol1
                            mudtSetOpsLogFormatWork.strCol1(intArrayNo) = gCstNameOpsLogFormatStringsCH & intChno(i).ToString("0000")
                        Else
                            '右=strCol2
                            mudtSetOpsLogFormatWork.strCol2(intArrayNo) = gCstNameOpsLogFormatStringsCH & intChno(i).ToString("0000")
                        End If
                    End If

                Next i
            Next iGrp

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
#End Region

#End Region

#Region "設定値保存（設定値→構造体格納用文字列変換）"

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) ログフォーマット構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetOpsLogFormat)

        Try

            For i As Integer = 0 To UBound(udtSet.strCol1)

                ''grdLogCol1
                udtSet.strCol1(i) = mSaveStrings(mEnmGrid.Col1, i)

                ''grdLogCol2
                udtSet.strCol2(i) = mSaveStrings(mEnmGrid.Col2, i)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Function mSaveStrings(ByVal hudtSelectGrid As mEnmGrid, _
                                  ByVal hintRowIndex As Integer) As String

        Dim strRtn As String = ""

        Try

            Dim intPreviewType As Integer
            Dim intNo As Integer
            Dim strSaveStrings As String

            ''グリッドの情報取得
            Call mGetGridInfo(hudtSelectGrid, hintRowIndex, intPreviewType, intNo)

            ''フォーマット文字列取得
            cmbSave.SelectedIndex = intPreviewType
            strSaveStrings = cmbSave.Text

            ''フォーマット文字列の作成
            strRtn = mMakeSaveStringsDetail(intPreviewType, strSaveStrings, intNo)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

    Private Function mMakeSaveStringsDetail(ByVal hintPreviewType As Integer, _
                                            ByVal hstrSaveStrings As String, _
                                            ByVal hintNo As Integer) As String

        Dim strRtn As String = ""

        Try

            Select Case hintPreviewType
                Case gCstCodeOpsLogFormatTypeNothing : strRtn = hstrSaveStrings
                Case gCstCodeOpsLogFormatTypeCounterTitle : strRtn = hstrSaveStrings
                Case gCstCodeOpsLogFormatTypeAnalogTitle : strRtn = hstrSaveStrings
                Case gCstCodeOpsLogFormatTypeCh : strRtn = hstrSaveStrings & hintNo.ToString("0000")
                Case gCstCodeOpsLogFormatTypeGroup : strRtn = hstrSaveStrings & hintNo.ToString("00")
                Case gCstCodeOpsLogFormatTypeSpace : strRtn = hstrSaveStrings
                Case gCstCodeOpsLogFormatTypePage : strRtn = hstrSaveStrings
                Case gCstCodeOpsLogFormatTypeDate : strRtn = hstrSaveStrings
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 詳細画面の設定データを作業用構造体に保存する
    ' 返り値    : 作業用構造体設定用の文字列
    '           : ARG1 - (I ) 選択データグリッド
    '           : ARG2 - (I ) プレビュータイプ
    '           : ARG3 - (I ) 設定NO
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Function mSaveStringsFromDetailDlg(ByVal hintPreviewType As Integer, _
                                               ByVal hintNo As Integer) As String

        Dim strRtn As String = ""

        Try

            Dim strSaveStrings As String

            ''フォーマット文字列取得
            cmbSave.SelectedIndex = hintPreviewType
            strSaveStrings = cmbSave.Text

            ''フォーマット文字列の作成
            strRtn = mMakeSaveStringsDetail(hintPreviewType, strSaveStrings, hintNo)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

#End Region

#Region "設定値表示（構造体格納用文字列→画面表示処理）"

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) ログフォーマット構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetOpsLogFormat)

        Try

            For i As Integer = 0 To UBound(udtSet.strCol1)

                ''grdLogCol1
                Call mOpenSaveStrings(grdLogCol1, i, gGetString(udtSet.strCol1(i)))

                ''grdLogCol2
                Call mOpenSaveStrings(grdLogCol2, i, gGetString(udtSet.strCol2(i)))

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : プレビュー表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) ログフォーマット構造体
    ' 機能説明  : プレビュー文字列を作成し、表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplayPreview(ByVal udtSet As gTypSetOpsLogFormat)

        Try

            Dim i As Integer
            Dim intRowMaxGrid As Integer = UBound(udtSet.strCol1)

            '--------------
            ''grdLogCol1
            '--------------
            For i = 0 To UBound(udtSet.strCol1)

                ''プレビュー表示
                Call mSetPreviewData(mEnmGrid.Col1, i, intRowMaxGrid)

                'COUNTER TITLEは2行1セットなので、1行目処理したら次行は飛ばす
                If CCInt(grdLogCol1(0, i).Value) = gCstCodeOpsLogFormatTypeCounterTitle Then
                    '最大行数に達した時は処理を抜ける
                    If intRowMaxGrid = i Then Exit For
                    i += 1
                End If

            Next

            '--------------
            ''grdLogCol2
            '--------------
            For i = 0 To UBound(udtSet.strCol2)

                ''grd1でANA TITLEを設定している場合、grd2でもプレビュー表示を行う（NOTHING処理をスキップする）
                If CCInt(grdLogCol1(0, i).Value) <> gCstCodeOpsLogFormatTypeAnalogTitle Then

                    ''プレビュー表示
                    Call mSetPreviewData(mEnmGrid.Col2, i, intRowMaxGrid)

                    'COUNTER TITLEは2行1セットなので、1行目処理したら次行は飛ばす
                    If CCInt(grdLogCol2(0, i).Value) = gCstCodeOpsLogFormatTypeCounterTitle Then
                        '最大行数に達した時は処理を抜ける
                        If intRowMaxGrid = i Then Exit For
                        i += 1
                    End If

                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "プレビュー表示"

    Private Sub mSetPreviewData(ByVal hudtSelectGrid As mEnmGrid, _
                                ByVal hintRowIndex As Integer, _
                                ByVal hintRowMax As Integer)

        Try

            Dim intPreviewType As Integer
            Dim strPreview As String = ""
            Dim intNo As Integer

            ''グリッドの選択行情報取得
            Call mGetGridInfo(hudtSelectGrid, hintRowIndex, intPreviewType, intNo)

            ''プレビューデータ設定
            Select Case intPreviewType
                Case gCstCodeOpsLogFormatTypeNothing, _
                     gCstCodeOpsLogFormatTypeSpace, _
                     gCstCodeOpsLogFormatTypePage

                    ''NOTHING／SPACE／PAGE
                    Call mSetPreviewDataCommon(hudtSelectGrid, hintRowIndex, mCstNamePreviewNothing)

                Case gCstCodeOpsLogFormatTypeCounterTitle

                    ''COUNTER TITLE
                    strPreview = mCstNamePreviewCounterTitleLine1.PadRight(mCstCodeMaxStringLength36) & _
                                 " " & mCstNamePreviewUnit.PadRight(mCstCodeMaxStringLength8) & _
                                 " " & mCstNamePreviewTime.PadRight(mCstCodeMaxStringLength9)

                    Call mSetPreviewDataCommon(hudtSelectGrid, hintRowIndex, strPreview)
                    If hintRowIndex <> hintRowMax Then
                        Call mSetPreviewDataCommon(hudtSelectGrid, hintRowIndex + 1, mCstNamePreviewCounterTitleLine2)
                    End If

                Case gCstCodeOpsLogFormatTypeAnalogTitle

                    ''ANALOG TITLE
                    Call mSetPreviewDataAnalog(hintRowIndex, mCstNamePreviewAnalogTitle)

                Case gCstCodeOpsLogFormatTypeCh

                    ''CH
                    Call mSetPreviewDataCh(hudtSelectGrid, hintRowIndex, intPreviewType, intNo)

                Case gCstCodeOpsLogFormatTypeGroup

                    ''GROUP
                    Call mSetPreviewDataGroup(hudtSelectGrid, hintRowIndex, intNo)

                Case gCstCodeOpsLogFormatTypeDate

                    ''DATE
                    strPreview = mCstNamePreviewBlank.Substring(0, 46) & mCstNamePreviewDate
                    Call mSetPreviewDataCommon(hudtSelectGrid, hintRowIndex, strPreview)

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetPreviewDataCommon(ByVal hudtSelectGrid As mEnmGrid, _
                                      ByVal hintRowIndex As Integer, _
                                      ByVal hstrPreviewStrings As String)


        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            Select Case hudtSelectGrid
                Case mEnmGrid.Col1 : grdLogCol1(2, hintRowIndex).Value = hstrPreviewStrings
                Case mEnmGrid.Col2 : grdLogCol2(2, hintRowIndex).Value = hstrPreviewStrings
            End Select

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetPreviewDataGroup(ByVal hudtSelectGrid As mEnmGrid, _
                                     ByVal hintRowIndex As Integer, _
                                     ByVal hintGroupNo As Integer)


        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            Dim strGroupName As String

            Select Case hudtSelectGrid
                Case mEnmGrid.Col1

                    ''入力グループNOが範囲内の時に処理を実施
                    If (0 < hintGroupNo) And (hintGroupNo <= UBound(mudtChannelGroupWork.udtGroup)) Then

                        ''グループ名称取得
                        strGroupName = mudtChannelGroupWork.udtGroup(hintGroupNo - 1).strGroupName

                        ''プレビューデータ設定
                        grdLogCol1(2, hintRowIndex).Value = strGroupName

                    Else

                        ''プレビューデータクリア設定
                        grdLogCol1(2, hintRowIndex).Value = mCstNamePreviewNothing

                    End If

                Case mEnmGrid.Col2

                    ''入力グループNOが範囲内の時に処理を実施
                    If (0 < hintGroupNo) And (hintGroupNo <= UBound(mudtChannelGroupWork.udtGroup)) Then

                        ''グループ名称取得
                        strGroupName = mudtChannelGroupWork.udtGroup(hintGroupNo - 1).strGroupName

                        ''プレビューデータ設定
                        grdLogCol2(2, hintRowIndex).Value = strGroupName

                    Else

                        ''プレビューデータクリア設定
                        grdLogCol2(2, hintRowIndex).Value = mCstNamePreviewNothing

                    End If

            End Select

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetPreviewDataAnalog(ByVal hintRowIndex As Integer, _
                                      ByVal hstrPreviewStrings As String)


        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            grdLogCol1(2, hintRowIndex).Value = hstrPreviewStrings
            grdLogCol2(2, hintRowIndex).Value = hstrPreviewStrings

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetPreviewDataCh(ByVal hudtSelectGrid As mEnmGrid, _
                                  ByVal hintRowIndex As Integer, _
                                  ByVal hintPreviewType As Integer, _
                                  ByVal hintChNo As Integer)


        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            Dim strChNo As String = hintChNo.ToString("0000")
            Dim intChIndex As Integer = gConvChNoToChArrayId(hintChNo)
            Dim strItemName As String
            Dim strFigure As String
            Dim strSaveStrings As String = ""
            Dim strPreviewStrings As String = ""
            Dim intDp As Integer
            Dim intKeta As Integer
            Dim strUnit As String = ""
            Dim intSpace As Integer = 0

            ''入力チャンネルNOが範囲内の時に処理を実施
            If intChIndex <> -1 Then

                ''チャンネルNOが0以外の時に処理を実施
                If hintChNo <> 0 Then

                    ''アイテム名称取得
                    strItemName = gGetString(gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.strChitem)
                    If strItemName.Length > mCstCodeMaxStringLength30 Then
                        strItemName = strItemName.Substring(0, mCstCodeMaxStringLength27) & "..."
                    End If

                    ''小数点位置取得
                    Call mSetPreviewDataChFigureDetail(intChIndex, intDp, intKeta, strUnit)

                    ''数値表示文字列作成
                    strFigure = mSetPreviewDataChFigureFormat(intKeta, intDp)

                    'プレビュー文字列作成
                    ''CH名称の全角対応　ver.1.4.0 2011.09.26
                    ''PadLeftでは全角も1文字換算なので、必要数分スペースで埋める
                    intSpace = mCstCodeMaxStringLength30 - LenB(strItemName)
                    strPreviewStrings = (hintChNo.ToString("0000")).PadLeft(mCstCodeMaxStringLength5) & _
                                        " " & strItemName & New String(" "c, intSpace) & _
                                        " " & strUnit.PadRight(mCstCodeMaxStringLength8) & _
                                        " " & strFigure

                End If

            End If

            '-----------------------
            ''プレビューデータ設定
            '-----------------------
            Select Case hudtSelectGrid
                Case mEnmGrid.Col1 : grdLogCol1(2, hintRowIndex).Value = strPreviewStrings
                Case mEnmGrid.Col2 : grdLogCol2(2, hintRowIndex).Value = strPreviewStrings
            End Select

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : プレビュー表示（チャンネルの数値情報）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 行Index
    ' 　　　    : ARG2 - ( O) DecimalPoint
    ' 　　　    : ARG3 - ( O) 桁数
    ' 　　　    : ARG4 - ( O) 単位
    ' 機能説明  : パートボタンの表示設定を行う
    '--------------------------------------------------------------------
    Private Sub mSetPreviewDataChFigureDetail(ByVal hintChIndex As Integer, _
                                              ByRef hintDp As Integer, _
                                              ByRef hintKeta As Integer, _
                                              ByRef hstrUnit As String)

        Try

            Dim intDecimalPosition As Integer
            Dim strDecimalFormat As String
            Dim strMax As String

            ''処理を抜ける条件（チャンネルの配列数が異常時）
            If (hintChIndex < LBound(gudt.SetChInfo.udtChannel)) Or (hintChIndex > UBound(gudt.SetChInfo.udtChannel)) Then Exit Sub

            ''小数点桁数
            intDecimalPosition = gudt.SetChInfo.udtChannel(hintChIndex).AnalogDecimalPosition
            strDecimalFormat = "0.".PadRight(intDecimalPosition + 2, "0"c)
            hintDp = intDecimalPosition

            ''最大桁数
            strMax = gudt.SetChInfo.udtChannel(hintChIndex).AnalogRangeHigh.ToString
            hintKeta = strMax.Length

            ''単位取得
            Dim shtUnit As Short = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtUnit
            hstrUnit = mSetPreviewDataChUnit(gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon)
            If hstrUnit = mCstNamePreviewUnitDefault Then hstrUnit = mCstNamePreviewUnitCustom 'ManualInput
            If hstrUnit.Length > mCstCodeMaxStringLength8 Then hstrUnit = hstrUnit.Substring(0, 8) '8文字以上の時

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : プレビュー表示（チャンネルの数値フォーマット）
    ' 返り値    : フォーマット後の数値
    ' 引き数    : ARG1 - (I ) 桁数
    ' 　　　    : ARG2 - (I ) DecimalPoint
    ' 機能説明  : チャンネルのプレビューに表示する数値を作成する
    '--------------------------------------------------------------------
    Private Function mSetPreviewDataChFigureFormat(ByVal hintKeta As Integer, _
                                                   ByVal hintDp As Integer) As String

        Dim strRtn As String = ""

        Try

            Dim dblFig As Double = 0.0
            Dim strFig As String = ""
            Dim strDecimalFormat As String

            ''フォーマット
            strDecimalFormat = "0.".PadRight(hintDp + 2, "0"c)
            strFig = dblFig.ToString(strDecimalFormat)

            ''0→「*」置換え
            strFig = strFig.Replace(0, "*")

            ''最大9文字
            If strFig.Length > mCstCodeMaxStringLength9 Then
                strFig = strFig.Substring(0, mCstCodeMaxStringLength9)
            End If

            ''桁数合わせ
            strRtn = strFig.PadRight(hintKeta, "*")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : プレビュー表示（チャンネルの単位）
    ' 返り値    : 単位
    ' 引き数    : ARG1 - (I ) チャンネル設定構造体（共通）
    ' 機能説明  : チャンネルのプレビューに表示する単位を取得する
    '--------------------------------------------------------------------
    Private Function mSetPreviewDataChUnit(ByVal hudtChCommon As gTypSetChRecCommon) As String

        Dim strRtn As String = ""

        Try

            ''データ取得
            Dim shtChType As Short = hudtChCommon.shtChType     ''CH種別
            Dim shtUnit As Short = hudtChCommon.shtUnit         ''単位コード
            Dim shtDataType As Short = hudtChCommon.shtData     ''データ種別コード

            Select Case shtChType
                Case gCstCodeChTypeAnalog, gCstCodeChTypePulse

                    ''アナログ
                    ''特殊コード対応　ver.1.4.0 2011.09.26
                    If hudtChCommon.shtUnit <> gCstCodeChManualInputUnit Then
                        cmbUnit.SelectedValue = hudtChCommon.shtUnit.ToString   ''単位名称取得
                        strRtn = cmbUnit.Text                                   ''戻り値設定
                    Else
                        strRtn = gGetString(hudtChCommon.strUnit)                ''特殊コード対応
                    End If

                Case gCstCodeChTypeValve

                    ''バルブ－アナログ
                    If shtDataType = gCstCodeChDataTypeValveAI_AO1 Or _
                       shtDataType = gCstCodeChDataTypeValveAI_AO2 Or _
                       shtDataType = gCstCodeChDataTypeValveAI_DO1 Or _
                       shtDataType = gCstCodeChDataTypeValveAI_DO2 Then

                        ''特殊コード対応　ver.1.4.0 2011.09.26
                        If hudtChCommon.shtUnit <> gCstCodeChManualInputUnit Then
                            cmbUnit.SelectedValue = hudtChCommon.shtUnit.ToString   ''単位名称取得
                            strRtn = cmbUnit.Text                                   ''戻り値設定
                        Else
                            strRtn = gGetString(hudtChCommon.strUnit)                ''特殊コード対応
                        End If

                    End If

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

#End Region

#Region "設定ファイルオープン"

    Private Sub mOpenSaveStrings(ByVal objGrid As DataGridView, _
                                 ByVal hintRowIndex As Integer, _
                                 ByVal hstrLogFormat As String)

        Try

            Dim intPreviewType As Integer
            Dim intNo As Integer

            ''プレビュータイプの取得
            For i = 0 To UBound(mudtSaveFormatStrings)
                If hstrLogFormat = gGetString(mudtSaveFormatStrings(i).strName) Then
                    intPreviewType = i
                    Exit For
                ElseIf hstrLogFormat.Substring(0, 2) = gCstNameOpsLogFormatStringsGROUP Then
                    intPreviewType = gCstCodeOpsLogFormatTypeGroup
                    intNo = CCInt(hstrLogFormat.Substring(2))
                    Exit For
                ElseIf hstrLogFormat.Substring(0, 2) = gCstNameOpsLogFormatStringsCH Then
                    intPreviewType = gCstCodeOpsLogFormatTypeCh
                    intNo = CCInt(hstrLogFormat.Substring(2))
                    Exit For
                End If
            Next

            ''データ設定
            Select Case intPreviewType
                Case gCstCodeOpsLogFormatTypeNothing, _
                     gCstCodeOpsLogFormatTypeCounterTitle, _
                     gCstCodeOpsLogFormatTypeAnalogTitle, _
                     gCstCodeOpsLogFormatTypeSpace, _
                     gCstCodeOpsLogFormatTypePage, _
                     gCstCodeOpsLogFormatTypeDate

                    ''NOTHING／COUNTER TITLE／ANALOG TITLE／SPACE／PAGE／DATE
                    Call mOpenSaveStringsCommon(objGrid, hintRowIndex, intPreviewType)

                Case gCstCodeOpsLogFormatTypeCh, _
                     gCstCodeOpsLogFormatTypeGroup

                    ''CH／GROUP
                    Call mOpenSaveStringsGrAndCh(objGrid, hintRowIndex, intPreviewType, intNo)

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mOpenSaveStringsCommon(ByVal hobjGrid As DataGridView, _
                                       ByVal hintRowIndex As Integer, _
                                       ByVal hintType As Integer)


        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            hobjGrid(0, hintRowIndex).Value = hintType.ToString
            hobjGrid(1, hintRowIndex).Value = mCstNamePreviewNothing
            hobjGrid(2, hintRowIndex).Value = mCstNamePreviewNothing

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mOpenSaveStringsGrAndCh(ByVal hobjGrid As DataGridView, _
                                        ByVal hintRowIndex As Integer, _
                                        ByVal intCol1 As Integer, _
                                        ByVal intCol2 As Integer)

        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            hobjGrid(0, hintRowIndex).Value = intCol1.ToString
            If hobjGrid(0, hintRowIndex).Value = 3 Then     '' CH
                hobjGrid(1, hintRowIndex).Value = intCol2.ToString("0000")
            Else                                            ''GROUP
                hobjGrid(1, hintRowIndex).Value = intCol2.ToString("00")
            End If

            hobjGrid(2, hintRowIndex).Value = mCstNamePreviewNothing

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region

#Region "画面上のボタン処理"

    '--------------------------------------------------------------------
    ' 機能      : ｵﾌﾟｼｮﾝﾎﾞﾀﾝ色変更
    '               Ver1.9.3 2016.01.22 追加
    ' 返り値    : なし
    ' 引き数    : なし
    '--------------------------------------------------------------------
    Public Sub SetOptionColor()

        If gudt.SetSystem.udtSysPrinter.shtLogDrawNo = 0 Then
            cmdOption.BackColor = SystemColors.Control
        Else        '' ｵﾌﾟｼｮﾝ設定が入っている場合は色で知らせる
            cmdOption.BackColor = Color.PapayaWhip
        End If
    End Sub


    '--------------------------------------------------------------------
    ' 機能      : 行削除処理
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) 作業用ログフォーマット設定構造体
    '           : ARG2 - (I ) 行Index
    '--------------------------------------------------------------------
    Private Sub mDeleteRowData(ByRef hudtLogWork As gTypSetOpsLogFormat, _
                               ByVal hintRowIndex As Integer, _
                               ByVal hintMaxRowIndexOfEditPage As Integer)

        Try

            ''選択行から１行づつ上に詰めてコピー
            For i As Integer = hintRowIndex To hintMaxRowIndexOfEditPage
                'Ver2.0.2.0 行削除は片方のみ
                If mblnSelectGridCol1 Then
                    ''grdLogCol1
                    Call mDeleteRowDataDetail(hudtLogWork.strCol1, i, hintMaxRowIndexOfEditPage)
                Else
                    ''grdLogCol2
                    Call mDeleteRowDataDetail(hudtLogWork.strCol2, i, hintMaxRowIndexOfEditPage)
                End If
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 行削除処理 詳細
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) 作業用ログフォーマット設定構造体
    '           : ARG2 - (I ) 行Index
    '           : ARG3 - (I ) 行数
    '--------------------------------------------------------------------
    Private Sub mDeleteRowDataDetail(ByRef hudtLogWork() As String, _
                                     ByVal hintRowIndex As Integer, _
                                     ByVal hintRowCount As Integer)

        Try

            If hintRowIndex <> hintRowCount Then

                ''選択行から１行づつ上に詰めてコピー
                hudtLogWork(hintRowIndex) = hudtLogWork(hintRowIndex + 1)

                ''コピー先のデータクリア
                hudtLogWork(hintRowIndex + 1) = mCstNamePreviewNothing

            Else

                ''最終行の場合は選択行のデータクリアのみ
                hudtLogWork(hintRowIndex) = mCstNamePreviewNothing

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 行挿入処理
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) 作業用ログフォーマット設定構造体
    '           : ARG2 - (I ) 行Index
    '           : ARG3 - (I ) 編集ページの最大行Index
    '--------------------------------------------------------------------
    Private Sub mAddRowData(ByRef hudtLogWork As gTypSetOpsLogFormat, _
                            ByVal hintRowIndex As Integer, _
                            ByVal hintMaxRowIndexOfEditPage As Integer)


        Try
            'Ver2.0.6.1 選択行からコポーに変更 (hintRowIndex + 1)でなくする
            ''選択行から１行づつ下にコピー
            For i As Integer = hintMaxRowIndexOfEditPage To (hintRowIndex) Step -1
                'Ver2.0.2.0 Insertは片方の一覧のみ
                If mblnSelectGridCol1 Then
                    ''grdLogCol1
                    Call mAddRowDataDetail(hudtLogWork.strCol1, i, hintMaxRowIndexOfEditPage)
                Else
                    ''grdLogCol2
                    Call mAddRowDataDetail(hudtLogWork.strCol2, i, hintMaxRowIndexOfEditPage)
                End If
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 編集ページの最大行Indexを取得
    ' 返り値    : 編集ページの最大行Index
    ' 引き数    : ARG1 - (I ) 選択行Index
    '--------------------------------------------------------------------
    Private Function mAddRowDataMaxRowIdxOfPage(ByVal hintRowIdx As Integer) As Integer

        Dim intRtn As Integer = 0

        Try

            Dim intMaxRowIdxOfEditPage As Integer

            For i As Integer = 0 To UBound(mintMaxRowNo)

                If mintMaxRowNo(i) > hintRowIdx Then

                    If i = 0 Then

                        ''1ページ目は固定で非表示行あり
                        intMaxRowIdxOfEditPage = mintMaxRowNo(i) - 1 - mCstCodeHideRowCnt
                        Exit For

                    Else

                        intMaxRowIdxOfEditPage = mintMaxRowNo(i) - 1
                        Exit For

                    End If

                End If

            Next i

            intRtn = intMaxRowIdxOfEditPage

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 行挿入処理 詳細
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) 作業用ログフォーマット設定構造体
    '           : ARG2 - (I ) 行Index
    '           : ARG3 - (I ) 最後尾の行Index
    '--------------------------------------------------------------------
    Private Sub mAddRowDataDetail(ByRef hudtLogWork() As String, _
                                  ByVal hintRowIndex As Integer, _
                                  ByVal hintLastRowIndex As Integer)


        Try

            ''最後尾に設定されている行データは破棄する
            If hintRowIndex <> hintLastRowIndex Then

                ''選択行から１行づつ下に詰めてコピー
                hudtLogWork(hintRowIndex + 1) = hudtLogWork(hintRowIndex)

                ''コピー元データクリア
                hudtLogWork(hintRowIndex) = mCstNamePreviewNothing

            Else

                ''最終行の場合は選択行のデータクリアのみ
                hudtLogWork(hintRowIndex) = mCstNamePreviewNothing

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "詳細画面表示処理"

    '--------------------------------------------------------------------
    ' 機能説明  ： 詳細画面表示
    ' 引数      ： ARG1 - (I ) グリッドオブジェクト
    '           ： ARG2 - (I ) 操作グリッド
    '           ： ARG3 - (I ) 行Index
    '           ： ARG4 - (I ) 編集モード（TRUE:GROUP設定, FALSE:個別設定）
    '           ： ARG5 - (I ) 自動生成用構造体
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mDispLogFormatDetail(ByVal objGrid As DataGridView, _
                                     ByVal hudtSelectGrid As mEnmGrid, _
                                     ByVal hintRowIndex As Integer, _
                                     ByVal hblnModeGroup As Boolean, _
                                     ByVal hudtChRL As gTypLogFormatPickCH)

        Try

            Dim intPreviewType As Integer
            Dim intNo As Integer

            ''グリッドの選択行情報取得
            Call mGetGridInfo(hudtSelectGrid, hintRowIndex, intPreviewType, intNo)

            ''CHとGROUPで表示処理を分ける
            Select Case intPreviewType
                Case gCstCodeOpsLogFormatTypeCh

                    Call mDispLogFormatDetailCh(hudtSelectGrid, _
                                                hintRowIndex, _
                                                intPreviewType, _
                                                intNo, _
                                                hblnModeGroup, _
                                                hudtChRL)

                Case gCstCodeOpsLogFormatTypeGroup

                    Call mDispLogFormatDetailGroup(objGrid, _
                                                   hudtSelectGrid, _
                                                   hintRowIndex, _
                                                   intPreviewType, _
                                                   intNo, _
                                                   hblnModeGroup, _
                                                   hudtChRL)
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： 詳細画面表示 >> グループ画面
    ' 引数      ： ARG1 - (I ) グリッドオブジェクト
    '           ： ARG2 - (I ) 操作グリッド
    '           ： ARG3 - (I ) 行Index
    '           ： ARG4 - (I ) 設定タイプ
    '           ： ARG5 - (I ) グループ番号
    '           ： ARG6 - (I ) 編集モード（TRUE:GROUP設定, FALSE:個別設定）
    '           ： ARG7 - (I ) 自動生成用構造体
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mDispLogFormatDetailGroup(ByVal hobjGrid As DataGridView, _
                                          ByVal udtSelectGrid As mEnmGrid, _
                                          ByVal hintRowIndex As Integer, _
                                          ByVal hintType As Integer, _
                                          ByVal hintGroupNo As Integer, _
                                          ByVal hblnModeGroup As Boolean, _
                                          ByVal hudtChRL As gTypLogFormatPickCH)

        Try

            Dim intChNo As Integer
            Dim intAutoSetCounter As Integer

            ''詳細画面の表示処理
            If frmOpsLogFormatDetail.gShow(mudtChannelGroupWork, hintType, hintGroupNo, intChNo, hblnModeGroup, hudtChRL, Me) = 0 Then

                ''設定エラーの場合は処理を抜ける
                If mCheckSetErr(hblnModeGroup, udtSelectGrid, hintRowIndex, hintGroupNo, mudtSetOpsLogFormatWork, hudtChRL, intAutoSetCounter) Then Exit Sub

                ''プレビュー情報設定
                Select Case udtSelectGrid
                    Case mEnmGrid.Col1 : Call mAutoSetGroup(hobjGrid, mudtSetOpsLogFormatWork.strCol1, hudtChRL, hintRowIndex, hintGroupNo, intAutoSetCounter)
                    Case mEnmGrid.Col2 : Call mAutoSetGroup(hobjGrid, mudtSetOpsLogFormatWork.strCol2, hudtChRL, hintRowIndex, hintGroupNo, intAutoSetCounter)
                End Select

                ''操作不可設定クリア
                Call mInitialDataGridSettings()

                ''再表示
                Call mSetDisplay(mudtSetOpsLogFormatWork)
                Call mSetDisplayPreview(mudtSetOpsLogFormatWork)

                ''操作不可設定
                Call mControlRowEnable()

                ''グリッドイベントの連続実行防止フラグ（CellValidated）
                mblnDispDetail = True

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： 自動生成の継続可否チェック
    ' 引数      ： ARG1 - (I ) 編集モード（TRUE:グループ設定, FALSE:個別設定）
    '           ： ARG2 - (I ) 操作グリッド
    '           ： ARG3 - (I ) 行Index
    '           ： ARG4 - (I ) グループ番号
    '           ： ARG5 - (I ) ログフォーマット構造体
    '           ： ARG6 - (I ) 自動生成用構造体
    '           ： ARG7 - ( O) 自動生成されるチャンネル数
    ' 戻値      ： TRUE:上書き設定を行う, FALSE:何もせず処理を抜ける
    '--------------------------------------------------------------------
    Private Function mCheckSetErr(ByVal hblnModeGroup As Boolean, _
                                  ByVal hudtSelectGrid As mEnmGrid, _
                                  ByVal hintRowIndex As Integer, _
                                  ByVal hintGroupNo As Integer, _
                                  ByVal hudtLog As gTypSetOpsLogFormat, _
                                  ByVal hudtChRL As gTypLogFormatPickCH, _
                                  ByRef hintAutoSetCount As Integer) As Boolean

        Dim blnRtn As Boolean = False

        Try

            '編集ページの最大行Index取得
            Dim intMaxRowIndexOfPage As Integer = mAddRowDataMaxRowIdxOfPage(hintRowIndex)
            Dim intFlg As Integer
            Dim intFlgOverlapGrid1 As Integer
            Dim intFlgOverlapGrid2 As Integer

            ''重複チェックフラグ
            If hblnModeGroup Then
                intFlgOverlapGrid1 = mCheckOverlap(hintRowIndex, intMaxRowIndexOfPage, hintGroupNo, hudtLog.strCol1, hudtChRL, hintAutoSetCount)
                intFlgOverlapGrid2 = mCheckOverlap(hintRowIndex, intMaxRowIndexOfPage, hintGroupNo, hudtLog.strCol2, hudtChRL, hintAutoSetCount)
            End If

            Select Case hudtSelectGrid
                Case mEnmGrid.Col1 : intFlg = intFlgOverlapGrid1
                Case mEnmGrid.Col2 : intFlg = intFlgOverlapGrid2
            End Select

            ''[YES]以外を選択した時は処理を抜ける
            Select Case intFlg
                Case 1
                    If MessageBox.Show("The setting data protrudes beyond the page." & vbNewLine & _
                                       "May I continue operation?", _
                                       Me.Text, _
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        blnRtn = True
                    End If

                Case 2
                    If MessageBox.Show("The setting data exists. " & vbNewLine & _
                                       "May I overwrite it?", _
                                       Me.Text, _
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        blnRtn = True
                    End If

                Case 3
                    If MessageBox.Show("The setting data exceeds the number of maximum records." & vbNewLine & _
                                       "May I continue operation?", _
                                       Me.Text, _
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
                        blnRtn = True
                    End If

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return blnRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能説明  ： 重複データの有無チェック
    ' 引数      ： ARG1 - (I ) 行Index
    '           ： ARG2 - (I ) 編集ページの最大行Index取得
    '           ： ARG3 - (I ) グループ番号
    '           ： ARG4 - (I ) ログフォーマット構造体
    '           ： ARG5 - (I ) 自動生成用構造体
    '           ： ARG6 - ( O) 自動生成されるCH数
    ' 戻値      ： TRUE:重複データあり, FALSE:重複データなし
    '--------------------------------------------------------------------
    Private Function mCheckOverlap(ByVal hintRowIndex As Integer, _
                                   ByVal hintMaxRowIndexOfPage As Integer, _
                                   ByVal hintGroupNo As Integer, _
                                   ByVal hudtLog() As String, _
                                   ByVal hudtChRL As gTypLogFormatPickCH, _
                                   ByRef hintAutoSetCounter As Integer) As Integer

        Dim intRtn As Integer = 0

        Try

            Dim intCounter As Integer

            ''自動生成で設定されるＣＨ数の取得
            For i As Integer = 0 To UBound(hudtChRL.udtSetChRL)

                If hudtChRL.udtSetChRL(i).intGroupNo = hintGroupNo Then
                    intCounter += 1
                End If

            Next

            hintAutoSetCounter = intCounter

            ''設定データがページをはみ出る場合
            If intCounter <> 0 Then

                If hintRowIndex = UBound(hudtLog) Then
                    intRtn = 3
                    Return intRtn
                End If

            End If

            ''設定データがページをはみ出る場合
            If intCounter <> 0 Then

                If hintRowIndex + intCounter > hintMaxRowIndexOfPage Then
                    intRtn = 1
                    Return intRtn
                End If

            End If

            ''設定データがある場合
            If intCounter <> 0 Then

                ''自動設定されるＣＨエリアの確認
                For i = hintRowIndex + 1 To hintRowIndex + intCounter

                    If Trim(hudtLog(i)) <> "" Then
                        intRtn = 2
                        Return intRtn
                    End If

                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能説明  ： 自動生成処理
    ' 引数      ： ARG1 - (I ) グリッドオブジェクト
    '           ： ARG2 - (IO) ログフォーマット構造体
    '           ： ARG3 - (I ) 自動生成用構造体
    '           ： ARG4 - (I ) 行Index
    '           ： ARG5 - (I ) グループNO
    '           ： ARG6 - (I ) 自動生成されるチャンネル数
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mAutoSetGroup(ByVal hobjGrid As DataGridView, _
                              ByRef hudtOpsLog() As String, _
                              ByVal hudtChRL As gTypLogFormatPickCH, _
                              ByVal hintRowIndex As Integer, _
                              ByVal hintGroupNo As Integer, _
                              ByVal hintAutoSetCount As Integer)

        Try

            Dim intCnt As Integer
            Dim intLoopMax As Integer

            Dim intChno() As Integer    '' 2014.12.02
            ReDim intChno(UBound(gudt.SetChInfo.udtChannel))

            If mblnModeGroup Then

                '====================
                ''グループ設定モード
                '====================
                ''前回の自動生成情報削除
                Call mInitialPrevGroupInfo(hudtOpsLog, hudtChRL, hintRowIndex)

                ''編集ページの最大行Index取得
                Dim intMaxRowIdxOfEditPage As Integer = mAddRowDataMaxRowIdxOfPage(hintRowIndex)

                ''自動生成するCH設定数（上限ループ数設定）
                intLoopMax = intMaxRowIdxOfEditPage
                If hintAutoSetCount > UBound(hudtOpsLog) - hintRowIndex Then
                    intLoopMax = intMaxRowIdxOfEditPage - hintRowIndex
                End If

                ''グループ情報設定
                hudtOpsLog(hintRowIndex) = gCstNameOpsLogFormatStringsGROUP & hintGroupNo.ToString("00")

                ''自動生成処理
                If intLoopMax <> 0 Then

                    ''ソート用のグループ単位のチャンネル番号の取得    2014.12.02
                    Call mSortGroupChno(hudtChRL, intChno, hintGroupNo)

                    ''チャンネル番号の並び替え  2014.12.02
                    Call Array.Sort(intChno)

                    For i As Integer = 0 To UBound(intChno)

                        ''チャンネル情報設定     ソートしたCHを使用  2014.12.02
                        If intChno(i) <> 0 Then

                            intCnt += 1

                            ''最大レコード数を超えた場合は処理を抜ける
                            If hintRowIndex + intCnt > UBound(hudtOpsLog) Then Exit Sub

                            ''設定可能なセルの場合は処理を行う
                            If hobjGrid(0, hintRowIndex + intCnt).Style.BackColor <> gColorGridRowBackReadOnly Then
                                hudtOpsLog(hintRowIndex + intCnt) = gCstNameOpsLogFormatStringsCH & intChno(i).ToString("0000")
                            Else
                                '▼▼▼ 20110705 入力不可セルをにはCH設定を行わずに設定可能セルに設定を行う場合 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                                i -= 1
                                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                            End If

                        End If

                    Next i

                End If

            Else

                '====================
                ''個別設定モード
                '====================
                hudtOpsLog(hintRowIndex) = mSaveStringsFromDetailDlg(gCstCodeOpsLogFormatTypeGroup, hintGroupNo)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： 前回のグループ情報初期化
    ' 引数      ： ARG1 - (IO) ログフォーマット構造体
    '           ： ARG2 - (I ) 自動生成用構造体
    '           ： ARG3 - (I ) 行Index
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mInitialPrevGroupInfo(ByRef hudtOpsLog() As String, _
                                      ByVal hudtChRL As gTypLogFormatPickCH, _
                                      ByVal hintRowIndex As Integer)

        Try

            Dim intCounter As Integer
            Dim intGroupNo As Integer
            Dim strBuf As String = ""

            ''グループ番号取得
            If hudtOpsLog(hintRowIndex) <> "" Then
                strBuf = hudtOpsLog(hintRowIndex).Substring(2)
                intGroupNo = CCInt(strBuf)
            End If

            ''自動生成で設定されるＣＨ数の取得
            If intGroupNo <> 0 Then

                For i As Integer = 0 To UBound(hudtChRL.udtSetChRL)

                    If hudtChRL.udtSetChRL(i).intGroupNo = intGroupNo Then
                        intCounter += 1
                    End If

                Next

            End If

            ''前回のグループ情報データ初期化
            For i = hintRowIndex + 1 To ((hintRowIndex + 1) + intCounter) - 1
                hudtOpsLog(i) = ""
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： 詳細画面表示 >> チャンネル画面
    ' 引数      ： ARG1 - (I ) 選択グリッド
    '           ： ARG2 - (I ) 行Index
    '           ： ARG3 - (I ) プレビュータイプ
    '           ： ARG4 - (I ) チャンネルNO
    '           ： ARG5 - (I ) 編集モード（TURE:グループ設定, FALSE:個別設定）
    '           ： ARG6 - (I ) 自動生成用構造体
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mDispLogFormatDetailCh(ByVal udtSelectGrid As mEnmGrid, _
                                       ByVal hintRowIndex As Integer, _
                                       ByVal hintSelectType As Integer, _
                                       ByVal hintChNo As Integer, _
                                       ByVal hblnModeGroup As Boolean, _
                                       ByVal hudtChRL As gTypLogFormatPickCH)

        Try

            Dim intChIndex As Integer = gConvChNoToChArrayId(hintChNo.ToString("0000"))
            Dim intGroupNo As Integer

            If intChIndex <> -1 Then
                ''チャンネル情報が設定されている場合、グループ情報を取得
                If (0 < hintChNo) And (hintChNo <= 65535) Then
                    intGroupNo = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtGroupNo
                End If
            End If

            ''詳細画面の表示処理
            If frmOpsLogFormatDetail.gShow(mudtChannelGroupWork, _
                                           hintSelectType, _
                                           intGroupNo, _
                                           hintChNo, _
                                           hblnModeGroup, _
                                           hudtChRL, _
                                           Me) = 0 Then

                ''プレビュー情報設定
                With mudtSetOpsLogFormatWork

                    Select Case udtSelectGrid
                        Case mEnmGrid.Col1 : .strCol1(hintRowIndex) = mSaveStringsFromDetailDlg(gCstCodeOpsLogFormatTypeCh, hintChNo)
                        Case mEnmGrid.Col2 : .strCol2(hintRowIndex) = mSaveStringsFromDetailDlg(gCstCodeOpsLogFormatTypeCh, hintChNo)
                    End Select

                End With

                ''再表示
                Call mSetDisplay(mudtSetOpsLogFormatWork)
                Call mSetDisplayPreview(mudtSetOpsLogFormatWork)

                ''操作不可設定
                Call mControlRowEnable()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "セルのReadOnly設定"

    '--------------------------------------------------------------------
    ' 機能      : グリッドの行設定を初期化する
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グリッドオブジェクト
    ' 機能説明  : ReadOnly設定、読込み専用の背景色を初期化する
    '--------------------------------------------------------------------
    Private Sub mInitialDataGridSettings()

        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            ''ReadOnly設定解除
            For i As Integer = 0 To grdLogCol1.RowCount - 1
                grdLogCol1(0, i).ReadOnly = False
                grdLogCol1(1, i).ReadOnly = False
                grdLogCol2(0, i).ReadOnly = False
                grdLogCol2(1, i).ReadOnly = False
            Next

            ''行カラーとページ区切り線
            Call mInitialDataGridColorAndSplit(grdLogCol1)
            Call mInitialDataGridColorAndSplit(grdLogCol2)

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： セルの操作設定を行う
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mControlRowEnable()

        Try

            For i As Integer = 0 To UBound(mudtSetOpsLogFormatWork.strCol1)

                ''grdLogCol1
                Call mControlRowEnableGrid(grdLogCol1, i, CCInt(grdLogCol1(0, i).Value))

                'grdLogCol2
                Call mControlRowEnableGrid(grdLogCol2, i, CCInt(grdLogCol2(0, i).Value))

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： プレビュータイプを見て該当項目の操作不可設定を行う
    ' 引数      ： ARG1 - (I ) 選択グリッド
    '           ： ARG2 - (I ) 選択グリッド
    '           ： ARG3 - (I ) 行Index
    '           ： ARG4 - (I ) プレビュータイプ
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mControlRowEnableGrid(ByVal objGrid As DataGridView, _
                                      ByVal hintRowIndex As Integer, _
                                      ByVal hintPreviewType As Integer)

        Try

            Dim intCol0 As Integer = 0          ''PreviewType列
            Dim intCol1 As Integer = 1          ''No列
            Dim intCol2 As Integer = 2          ''Preview列
            Dim intClickPageMaxNo As Integer    ''[Page]設定したページの最大ページNO

            ''最大行数取得
            Dim intRowMax As Integer = UBound(mudtSetOpsLogFormatWork.strCol1)

            ''１ページ目については、タイトル分（最後の３行）を使用不可にする
            If hintRowIndex = mintMaxRowNo(0) - mCstCodeHideRowCnt + 0 Or _
               hintRowIndex = mintMaxRowNo(0) - mCstCodeHideRowCnt + 1 Or _
               hintRowIndex = mintMaxRowNo(0) - mCstCodeHideRowCnt + 2 Then

                Call mControlEnableDataCol1(hintRowIndex, intCol0)
                Call mControlEnableDataCol1(hintRowIndex, intCol1)
                Call mControlEnableDataCol2(hintRowIndex, intCol0)
                Call mControlEnableDataCol2(hintRowIndex, intCol1)

            Else

                ''データ設定
                Select Case hintPreviewType
                    Case gCstCodeOpsLogFormatTypeNothing, _
                         gCstCodeOpsLogFormatTypeSpace, _
                         gCstCodeOpsLogFormatTypeCh, _
                         gCstCodeOpsLogFormatTypeGroup, _
                         gCstCodeOpsLogFormatTypeDate

                        ''処理なし

                    Case gCstCodeOpsLogFormatTypeCounterTitle

                        If objGrid.Name = "grdLogCol1" Then
                            Call mControlEnableDataCol2(hintRowIndex, intCol0)
                            Call mControlEnableDataCol2(hintRowIndex, intCol1)
                            If hintRowIndex <> intRowMax Then
                                Call mControlEnableDataCol1(hintRowIndex + 1, intCol0)
                                Call mControlEnableDataCol1(hintRowIndex + 1, intCol1)
                                Call mControlEnableDataCol2(hintRowIndex + 1, intCol0)
                                Call mControlEnableDataCol2(hintRowIndex + 1, intCol1)
                            End If
                        Else
                            Call mControlEnableDataCol1(hintRowIndex, intCol0)
                            Call mControlEnableDataCol1(hintRowIndex, intCol1)
                            If hintRowIndex <> intRowMax Then
                                Call mControlEnableDataCol1(hintRowIndex + 1, intCol0)
                                Call mControlEnableDataCol1(hintRowIndex + 1, intCol1)
                                Call mControlEnableDataCol2(hintRowIndex + 1, intCol0)
                                Call mControlEnableDataCol2(hintRowIndex + 1, intCol1)
                            End If
                        End If

                    Case gCstCodeOpsLogFormatTypeAnalogTitle

                        If objGrid.Name = "grdLogCol1" Then
                            Call mControlEnableDataCol2(hintRowIndex, intCol0)
                            Call mControlEnableDataCol2(hintRowIndex, intCol1)
                        Else
                            Call mControlEnableDataCol1(hintRowIndex, intCol0)
                            Call mControlEnableDataCol1(hintRowIndex, intCol1)
                        End If

                    Case gCstCodeOpsLogFormatTypePage

                        ''区切りの最大行数取得
                        intClickPageMaxNo = mControlRowEnablePageMax(hintRowIndex)

                        If objGrid.Name = "grdLogCol1" Then
                            ''設定行から最大行まで操作不可設定
                            Call mControlEnableDataCol2(hintRowIndex, intCol0)
                            Call mControlEnableDataCol2(hintRowIndex, intCol1)
                            For i As Integer = hintRowIndex + 1 To intClickPageMaxNo - 1
                                Call mControlEnableDataCol1(i, intCol0)
                                Call mControlEnableDataCol1(i, intCol1)
                                Call mControlEnableDataCol2(i, intCol0)
                                Call mControlEnableDataCol2(i, intCol1)
                            Next

                        Else
                            ''設定行から最大行まで操作不可設定
                            Call mControlEnableDataCol1(hintRowIndex, intCol0)
                            Call mControlEnableDataCol1(hintRowIndex, intCol1)
                            For i As Integer = hintRowIndex + 1 To intClickPageMaxNo - 1
                                Call mControlEnableDataCol1(i, intCol0)
                                Call mControlEnableDataCol1(i, intCol1)
                                Call mControlEnableDataCol2(i, intCol0)
                                Call mControlEnableDataCol2(i, intCol1)
                            Next

                        End If

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： [PAGE]設定したページの最大行数を取得する
    ' 引数      ： ARG1 - (I ) 行Index
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Function mControlRowEnablePageMax(ByVal hintRowIndex As Integer) As Integer

        Try

            Dim intRtn As Integer = 0

            ''[PAGE]設定したページの最大行数を取得
            For intPageNo = 0 To mCstCodeMaxPageNo

                If hintRowIndex < mintMaxRowNo(intPageNo) Then
                    intRtn = mintMaxRowNo(intPageNo)
                    Exit For
                End If

            Next

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能説明  ： グリッドの操作不可設定解除
    ' 引数      ： ARG1 - (I ) 行Index
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mControlEnableDataColClear(ByVal intRow As Integer)

        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            grdLogCol1(1, intRow).ReadOnly = False
            grdLogCol2(1, intRow).ReadOnly = False

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： グリッドの操作不可設定
    ' 引数      ： ARG1 - (I ) 行Index
    '           ： ARG2 - (I ) 列NO
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mControlEnableDataCol1(ByVal intRow As Integer, _
                                       ByVal intCol As Integer)

        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            'Ver2.0.4.9 ﾛｸﾞフォーマット入力で一覧の入力不可ｴﾘｱ解放
            'grdLogCol1(intCol, intRow).ReadOnly = True
            'grdLogCol1(intCol, intRow).Style.BackColor = gColorGridRowBackReadOnly

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mControlEnableDataCol2(ByVal intRow As Integer, _
                                       ByVal intCol As Integer)

        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            'Ver2.0.4.9 ﾛｸﾞフォーマット入力で一覧の入力不可ｴﾘｱ解放
            'grdLogCol2(intCol, intRow).ReadOnly = True
            'grdLogCol2(intCol, intRow).Style.BackColor = gColorGridRowBackReadOnly

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "グリッドの設定値初期化"

    '--------------------------------------------------------------------
    ' 機能      : データ設定が可能かチェックを行う
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) ログフォーマット構造体
    '           : ARG2 - (I ) グリッドオブジェクト
    '           : ARG3 - (I ) 行Index
    ' 機能説明  : データ設定が可能かチェックを行う
    '--------------------------------------------------------------------
    Private Function mCheckPossibleToSetData(ByVal hudtLog As gTypSetOpsLogFormat, _
                                             ByVal hobjGrid As DataGridView, _
                                             ByVal hintRowIndex As Integer) As Integer

        Dim intRtn As Integer = 0

        Try

            Dim intType As Integer = CCInt(hobjGrid(0, hintRowIndex).Value)                     ''設定タイプ
            Dim intMaxRowIdxOfEditPage As Integer = mAddRowDataMaxRowIdxOfPage(hintRowIndex)    ''編集ページの最大行Index
            Dim intMaxRowIdx As Integer = UBound(mudtSetOpsLogFormatWork.strCol1)               ''最終行Index

            Select Case intType
                Case gCstCodeOpsLogFormatTypeNothing, _
                     gCstCodeOpsLogFormatTypeCh, _
                     gCstCodeOpsLogFormatTypeGroup, _
                     gCstCodeOpsLogFormatTypeSpace, _
                     gCstCodeOpsLogFormatTypeDate

                    ''チェック処理なし

                Case gCstCodeOpsLogFormatTypeCounterTitle

                    ''設定行が編集ページの最終行であるか判断
                    If intMaxRowIdx = hintRowIndex Then
                        intRtn = 2
                        Return intRtn
                    End If

                    ''設定行が編集ページの最終行であるか判断
                    If intMaxRowIdxOfEditPage = hintRowIndex Then
                        intRtn = 1
                        Return intRtn
                    End If

                    ''設定の重複判断
                    If hobjGrid.Name = "grdLogCol1" Then

                        ''グリッド１で[CNTTITLE]を設定した場合
                        If gGetString(hudtLog.strCol1(hintRowIndex + 1)) <> "" Or _
                           gGetString(hudtLog.strCol2(hintRowIndex)) <> "" Or _
                           gGetString(hudtLog.strCol2(hintRowIndex + 1)) <> "" Then
                            intRtn = 3
                            Return intRtn
                        End If

                    Else

                        ''グリッド２で[CNTTITLE]を設定した場合
                        If gGetString(hudtLog.strCol1(hintRowIndex)) <> "" Or _
                           gGetString(hudtLog.strCol1(hintRowIndex + 1)) <> "" Or _
                           gGetString(hudtLog.strCol2(hintRowIndex + 1)) <> "" Then
                            intRtn = 3
                            Return intRtn
                        End If

                    End If

                Case gCstCodeOpsLogFormatTypeAnalogTitle

                    ''設定の重複判断
                    If gGetString(hudtLog.strCol1(hintRowIndex)) <> "" Or _
                       gGetString(hudtLog.strCol2(hintRowIndex)) <> "" Then
                        intRtn = 3
                        Return intRtn
                    End If

                Case gCstCodeOpsLogFormatTypePage

                    ''設定の重複判断
                    For i As Integer = hintRowIndex To intMaxRowIdxOfEditPage

                        ''グリッド１で[PAGE]を設定した場合
                        If hobjGrid.Name = "grdLogCol1" Then

                            If i <> hintRowIndex Then
                                If gGetString(hudtLog.strCol1(i)) <> "" Then
                                    intRtn = 3
                                    Return intRtn
                                End If
                            End If

                            If gGetString(hudtLog.strCol2(i)) <> "" Then
                                intRtn = 3
                                Return intRtn
                            End If

                        End If

                        ''グリッド２で[PAGE]を設定した場合
                        If hobjGrid.Name = "grdLogCol2" Then

                            If gGetString(hudtLog.strCol1(i)) <> "" Then
                                intRtn = 3
                                Return intRtn
                            End If

                            If i <> hintRowIndex Then
                                If gGetString(hudtLog.strCol2(i)) <> "" Then
                                    intRtn = 3
                                    Return intRtn
                                End If
                            End If

                        End If

                    Next i

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : グリッドの設定値初期化
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グリッドオブジェクト
    '           : ARG2 - (I ) 行Index
    '           : ARG3 - (I ) プレビュータイプ
    ' 機能説明  : コンボボックスだけ設定して、他は初期化設定
    '--------------------------------------------------------------------
    Private Sub mInitialRowInfo(ByVal hobjGrid As DataGridView, _
                                ByVal hintRowIndex As Integer, _
                                ByVal hintPreviewType As Integer)

        Try

            ''初期化処理
            Select Case hintPreviewType
                Case gCstCodeOpsLogFormatTypeNothing : Call mInitialRowInfoCommon(hobjGrid, hintRowIndex, gCstCodeOpsLogFormatTypeNothing)
                Case gCstCodeOpsLogFormatTypeCounterTitle : Call mInitialRowInfoCounter(hobjGrid, hintRowIndex)
                Case gCstCodeOpsLogFormatTypeAnalogTitle : Call mInitialRowInfoAnalog(hobjGrid, hintRowIndex)
                Case gCstCodeOpsLogFormatTypeCh : Call mInitialRowInfoCommon(hobjGrid, hintRowIndex, gCstCodeOpsLogFormatTypeCh)
                Case gCstCodeOpsLogFormatTypeGroup : Call mInitialRowInfoCommon(hobjGrid, hintRowIndex, gCstCodeOpsLogFormatTypeGroup)
                Case gCstCodeOpsLogFormatTypeSpace : Call mInitialRowInfoCommon(hobjGrid, hintRowIndex, gCstCodeOpsLogFormatTypeSpace)
                Case gCstCodeOpsLogFormatTypePage : Call mInitialRowInfoPage(hobjGrid, hintRowIndex)
                Case gCstCodeOpsLogFormatTypeDate : Call mInitialRowInfoCommon(hobjGrid, hintRowIndex, gCstCodeOpsLogFormatTypeDate)
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mInitialRowInfoCommon(ByVal objGrid As DataGridView, _
                                      ByVal hintRowIndex As Integer, _
                                      ByVal hintSelectType As Integer)


        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            objGrid(0, hintRowIndex).Value = hintSelectType.ToString

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mInitialRowInfoCounter(ByVal objGrid As DataGridView, _
                                       ByVal hintRowIndex As Integer)


        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            If objGrid.Name = "grdLogCol1" Then
                ''grdLogCol1で[CNT TITLE]を設定した場合
                Call mOpenSaveStringsCommon(grdLogCol1, hintRowIndex, gCstCodeOpsLogFormatTypeCounterTitle)
                Call mOpenSaveStringsCommon(grdLogCol2, hintRowIndex, gCstCodeOpsLogFormatTypeNothing)
            Else
                ''grdLogCol2で[CNT TITLE]を設定した場合
                Call mOpenSaveStringsCommon(grdLogCol1, hintRowIndex, gCstCodeOpsLogFormatTypeNothing)
                Call mOpenSaveStringsCommon(grdLogCol2, hintRowIndex, gCstCodeOpsLogFormatTypeCounterTitle)
            End If

            ''最終行に[CNTTITLE]を設定した際の処理
            If hintRowIndex <> UBound(mudtSetOpsLogFormatWork.strCol1) Then
                Call mOpenSaveStringsCommon(grdLogCol1, hintRowIndex + 1, gCstCodeOpsLogFormatTypeNothing)
                Call mOpenSaveStringsCommon(grdLogCol2, hintRowIndex + 1, gCstCodeOpsLogFormatTypeNothing)
            End If

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mInitialRowInfoAnalog(ByVal objGrid As DataGridView, _
                                  ByVal hintRowIndex As Integer)


        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            If objGrid.Name = "grdLogCol1" Then
                ''grdLogCol1で[ANA TITLE]を設定した場合
                Call mOpenSaveStringsCommon(grdLogCol1, hintRowIndex, gCstCodeOpsLogFormatTypeAnalogTitle)
                Call mOpenSaveStringsCommon(grdLogCol2, hintRowIndex, gCstCodeOpsLogFormatTypeNothing)
            Else
                ''grdLogCol2で[ANA TITLE]を設定した場合
                Call mOpenSaveStringsCommon(grdLogCol1, hintRowIndex, gCstCodeOpsLogFormatTypeNothing)
                Call mOpenSaveStringsCommon(grdLogCol2, hintRowIndex, gCstCodeOpsLogFormatTypeAnalogTitle)
            End If

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mInitialRowInfoPage(ByVal objGrid As DataGridView, _
                                ByVal hintRowIndex As Integer)


        Try

            ''グリッドイベントの停止
            mblnInitFlg = True

            ''区切りの最大行数取得
            Dim intClickPageMaxNo As Integer = mControlRowEnablePageMax(hintRowIndex)

            If objGrid.Name = "grdLogCol1" Then
                ''grdLogCol1で[PAGE]を設定した場合
                Call mOpenSaveStringsCommon(grdLogCol1, hintRowIndex, gCstCodeOpsLogFormatTypePage)
                Call mOpenSaveStringsCommon(grdLogCol2, hintRowIndex, gCstCodeOpsLogFormatTypeNothing)
            Else
                ''grdLogCol2で[PAGE]を設定した場合
                Call mOpenSaveStringsCommon(grdLogCol1, hintRowIndex, gCstCodeOpsLogFormatTypeNothing)
                Call mOpenSaveStringsCommon(grdLogCol2, hintRowIndex, gCstCodeOpsLogFormatTypePage)
            End If

            ''[PAGE]設定行＋１から最大行まで設定値クリア
            For i As Integer = hintRowIndex + 1 To intClickPageMaxNo - 1
                Call mOpenSaveStringsCommon(grdLogCol1, i, gCstCodeOpsLogFormatTypeNothing)
                Call mOpenSaveStringsCommon(grdLogCol2, i, gCstCodeOpsLogFormatTypeNothing)
            Next

            mblnInitFlg = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


    '--------------------------------------------------------------------
    ' 機能説明  ： グリッドを初期化する
    ' 引数      ： なし
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mInitialDataGrid()

        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle

            Dim intRowNo As Integer

            Dim Column1 As New DataGridViewComboBoxColumn : Column1.Name = "cmbColumn1Type"
            Dim Column2 As New DataGridViewTextBoxColumn : Column2.Name = "txtColumn1Item"
            Dim Column3 As New DataGridViewTextBoxColumn : Column3.Name = "txtColumn1Preview"
            Dim Column4 As New DataGridViewComboBoxColumn : Column4.Name = "cmbColumn2Type"
            Dim Column5 As New DataGridViewTextBoxColumn : Column5.Name = "txtColumn2Item"
            Dim Column6 As New DataGridViewTextBoxColumn : Column6.Name = "txtColumn2Preview"

            Column1.FlatStyle = FlatStyle.Popup
            Column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column3.ReadOnly = True
            Column4.FlatStyle = FlatStyle.Popup
            Column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Column6.ReadOnly = True

            With grdLogCol1

                ''列
                .Columns.Clear()
                .Columns.Add(Column1)
                .Columns.Add(Column2)
                .Columns.Add(Column3)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列幅
                .Columns(0).Width = 85
                .Columns(1).Width = 38
                .Columns(2).Width = 338

                ''列ヘッダー不可視化
                .ColumnHeadersVisible = False

                ''行
                .RowCount = 600 + 1                 ''行数＋ヘッダー
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                'Ver2.0.4.1行ﾍｯﾀﾞｰ表示。番号は改ページ単位
                '行ヘッダー不可視化
                .RowHeadersVisible = True
                '行ヘッダー
                intRowNo = 1
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = intRowNo.ToString
                    If mCstCodeRowCountInPage <= intRowNo Then
                        intRowNo = 0
                    End If
                    intRowNo = intRowNo + 1
                Next

                .RowHeadersWidth = 50

                ''行カラーとページ区切り線
                Call mInitialDataGridColorAndSplit(grdLogCol1)

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                ''コンボボックス設定  
                Call gSetComboBox(Column1, gEnmComboType.ctOpsLogFormatListColumn1Type)

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdLogCol1)

            End With

            With grdLogCol2

                ''列
                .Columns.Clear()
                .Columns.Add(Column4)
                .Columns.Add(Column5)
                .Columns.Add(Column6)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列幅
                .Columns(0).Width = 85
                .Columns(1).Width = 38
                .Columns(2).Width = 338

                ''列ヘッダー不可視化
                .ColumnHeadersVisible = False

                ''行
                .RowCount = 600 + 1                 ''行数＋ヘッダー
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                'Ver2.0.4.1行ﾍｯﾀﾞｰ表示。番号は改ページ単位
                ''行ヘッダー不可視化
                .RowHeadersVisible = True
                '行ヘッダー
                intRowNo = 1
                For i = 1 To .RowCount
                    .Rows(i - 1).HeaderCell.Value = intRowNo.ToString
                    If mCstCodeRowCountInPage <= intRowNo Then
                        intRowNo = 0
                    End If
                    intRowNo = intRowNo + 1
                Next

                .RowHeadersWidth = 50

                ''行カラーとページ区切り線
                Call mInitialDataGridColorAndSplit(grdLogCol2)

                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                ''コンボボックス設定  
                Call gSetComboBox(Column4, gEnmComboType.ctOpsLogFormatListColumn2Type)

                ''コピー＆ペースト共通設定
                Call gSetGridCopyAndPaste(grdLogCol2)

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : グリッドの行カラーとページ区切り線設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グリッドオブジェクト
    ' 機能説明  : グリッドの行カラーとページ区切り線を設定する
    '--------------------------------------------------------------------
    Private Sub mInitialDataGridColorAndSplit(ByVal objGrid As DataGridView)

        Try

            ''行の背景色設定
            For i = 0 To UBound(mudtSetOpsLogFormatWork.strCol1)
                Call mSetRowColor(objGrid, i)
            Next

            ''プレビュー列の線設定
            For i = 0 To objGrid.RowCount - 1

                ''通常は枠線なし
                objGrid(2, i) = New clsDataGridViewBorderPaint

                ''ページ毎に線を入れる
                For j As Integer = 0 To UBound(mintMaxRowNo)

                    If mintMaxRowNo(j) = (i + 1) Then
                        objGrid(2, i) = New clsDataGridViewBorderPaintBottom
                        Exit For
                    End If

                Next j

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : グリッドの行間色設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グリッドオブジェクト
    '           : ARG2 - (I ) 行Index
    ' 機能説明  : 選択した行に空欄行を追加する
    '--------------------------------------------------------------------
    Private Sub mSetRowColor(ByVal objGrid As DataGridView, _
                             ByVal hintRowIndex As Integer)

        Try

            If hintRowIndex Mod 2 <> 0 Then

                ''ページ毎の色分け
                If mSetRowColorCheckPageNoOdd(hintRowIndex) Then
                    ''奇数
                    objGrid.Rows(hintRowIndex).Cells(0).Style.BackColor = gColorGridRowBack
                    objGrid.Rows(hintRowIndex).Cells(1).Style.BackColor = gColorGridRowBack
                Else
                    ''偶数
                    objGrid.Rows(hintRowIndex).Cells(0).Style.BackColor = mColorGridRowBackSplitPage
                    objGrid.Rows(hintRowIndex).Cells(1).Style.BackColor = mColorGridRowBackSplitPage
                End If

            Else
                objGrid.Rows(hintRowIndex).Cells(0).Style.BackColor = gColorGridRowBackBase
                objGrid.Rows(hintRowIndex).Cells(1).Style.BackColor = gColorGridRowBackBase
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 奇数ページか偶数ページかの判断
    ' 返り値    : TRUE:奇数ページ, FALSE偶数ページ
    ' 引き数    : ARG1 - (I ) 行番号
    ' 機能説明  : 処理中の行が奇数ページか偶数ページかを判断する
    '--------------------------------------------------------------------
    Private Function mSetRowColorCheckPageNoOdd(ByVal hintRowNo As Integer) As Boolean

        Dim blnRtn As Boolean = False

        Try

            Dim intPreviewPageNo As Integer

            ''表示ページ番号取得
            intPreviewPageNo = mGetPageNo(hintRowNo)

            ''ページ判断
            If intPreviewPageNo Mod 2 <> 0 Then
                ''奇数
                blnRtn = True
            Else
                ''偶数
                blnRtn = False
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return blnRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 表示ページ番号取得
    ' 返り値    : 表示ページ番号
    ' 引き数    : ARG1 - (I ) 行番号
    ' 機能説明  : 処理中のページ番号
    '--------------------------------------------------------------------
    Private Function mGetPageNo(ByVal hintRow As Integer) As Integer

        Dim intRtn As Integer = 0

        Try

            If mintMaxRowNo(0) > hintRow Then

                ''1ページ目
                intRtn = 1

            Else

                ''2ページ目以降
                For i As Integer = 0 To mCstCodeMaxPageNo - 1

                    ''上限ページ数
                    If mintMaxRowNo(i) > hintRow Then

                        ''下限ページ数
                        For j As Integer = mCstCodeMaxPageNo - 1 To 0 Step -1

                            If hintRow > mintMaxRowNo(j) Then
                                intRtn = i + 1
                                Exit For
                            End If

                        Next j

                        ''ページ番号を取得している場合はFor文を抜ける
                        If intRtn <> 0 Then
                            Exit For
                        End If

                    End If

                Next i

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            Dim i As Integer

            '--------------
            ''grdLogCol1
            '--------------
            For i = 0 To grdLogCol1.RowCount - 1

                ''選択されているプレビュータイプによって入力制限処理を分岐
                Select Case grdLogCol1(0, i).Value
                    Case gCstCodeOpsLogFormatTypeGroup

                        ''GroupNo 入力レンジ確認
                        If Not gChkInputNum(grdLogCol1(1, i), 0, 99, "Group No.", i + 1, True, True) Then Return False

                    Case gCstCodeOpsLogFormatTypeCh

                        ''ChNo 入力レンジ確認
                        If Not gChkInputNum(grdLogCol1(1, i), 0, 65535, "CH No.", i + 1, True, True) Then Return False

                End Select

            Next

            '--------------
            ''grdLogCol2
            '--------------
            For i = 0 To grdLogCol2.RowCount - 1

                ''選択されているプレビュータイプによって入力制限処理を分岐
                Select Case grdLogCol2(0, i).Value
                    Case gCstCodeOpsLogFormatTypeGroup

                        ''GroupNo 入力レンジ確認
                        If Not gChkInputNum(grdLogCol2(1, i), 0, 36, "Group No.", i + 1, True, True) Then Return False

                    Case gCstCodeOpsLogFormatTypeCh

                        ''ChNo 入力レンジ確認
                        If Not gChkInputNum(grdLogCol2(1, i), 0, 65535, "CH No.", i + 1, True, True) Then Return False

                End Select

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 構造体複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元
    ' 　　　    : ARG2 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypSetOpsLogFormat, _
                               ByRef udtTarget As gTypSetOpsLogFormat)

        Try

            For i As Integer = LBound(udtTarget.strCol1) To UBound(udtTarget.strCol1)

                udtTarget.strCol1(i) = udtSource.strCol1(i)
                udtTarget.strCol2(i) = udtSource.strCol2(i)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mCopyStructure(ByVal udtSource As gTypChannelGroup, _
                               ByRef udtTarget As gTypChannelGroup)
        Try

            For i As Integer = LBound(udtTarget.udtGroup) To UBound(udtTarget.udtGroup)

                With udtTarget.udtGroup(i)

                    .intDataCnt = udtSource.udtGroup(i).intDataCnt
                    .strGroupName = udtSource.udtGroup(i).strGroupName
                    .udtChannelData = udtSource.udtGroup(i).udtChannelData

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) Mach構造体
    '           : ARG2 - (I ) Carg構造体
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetOpsLogFormat, _
                                         ByVal udt2 As gTypSetOpsLogFormat) As Boolean

        Try

            For i As Integer = LBound(udt1.strCol1) To UBound(udt1.strCol1)

                If Not gCompareString(udt1.strCol1(i), udt2.strCol1(i)) Then Return False
                If Not gCompareString(udt1.strCol2(i), udt2.strCol2(i)) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : グループCH名称取得
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グループCH設定構造体  （StructureConst）
    ' 　　　    : ARG2 - ( O) グループCH構造体      （Common）
    ' 機能説明  : グループCH設定構造体からグループ名称を取得する
    '--------------------------------------------------------------------
    Private Sub mMakeChannelGroupName(ByVal udtSetChannelGroup As gTypSetChGroupSet, _
                                      ByRef udtChannelGroup As gTypChannelGroup)
        Dim gno As Integer

        Try

            For i = 0 To UBound(udtSetChannelGroup.udtGroup.udtGroupInfo)
                '' Ver1.12.0.5 2017.02.02 修正
                gno = udtSetChannelGroup.udtGroup.udtGroupInfo(i).shtGroupNo

                If gno > 0 And gno <= 36 Then
                    udtChannelGroup.udtGroup(gno - 1).strGroupName = gGetString(udtSetChannelGroup.udtGroup.udtGroupInfo(i).strName1) & _
                                                                gGetString(udtSetChannelGroup.udtGroup.udtGroupInfo(i).strName2) & _
                                                                gGetString(udtSetChannelGroup.udtGroup.udtGroupInfo(i).strName3)
                End If

                ' ''グループ名称
                'udtChannelGroup.udtGroup(i).strGroupName = gGetString(udtSetChannelGroup.udtGroup.udtGroupInfo(i).strName1) & _
                '                                           gGetString(udtSetChannelGroup.udtGroup.udtGroupInfo(i).strName2) & _
                '                                           gGetString(udtSetChannelGroup.udtGroup.udtGroupInfo(i).strName3)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  ： グリッドの選択行情報取得
    ' 引数      ： ARG1 - (I ) 選択グリッド名称
    '           ： ARG2 - ( O) 行Index
    '           ： ARG3 - ( O) プレビュータイプ
    '           ： ARG4 - ( O) 番号（チャンネルNO/グループNO）
    ' 戻値      ： なし
    '--------------------------------------------------------------------
    Private Sub mGetGridInfo(ByVal udtSelectGrid As mEnmGrid, _
                             ByVal hintRowIndex As Integer, _
                             ByRef hintPreviewType As Integer, _
                             ByRef hintNo As Integer)

        Try

            ''選択したグリッドの情報を取得
            Select Case udtSelectGrid
                Case mEnmGrid.Col1

                    ''grdLogCol1
                    hintPreviewType = CCInt(grdLogCol1(0, hintRowIndex).Value)
                    hintNo = CCInt(grdLogCol1(1, hintRowIndex).Value)

                Case mEnmGrid.Col2

                    ''grdLogCol2
                    hintPreviewType = CCInt(grdLogCol2(0, hintRowIndex).Value)
                    hintNo = CCInt(grdLogCol2(1, hintRowIndex).Value)

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 入力値のフォーマット処理
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グリッドオブジェクト
    '           : ARG2 - (I ) 操作グリッド
    '           : ARG3 - (I ) 行Index
    '           : ARG4 - (I ) ログフォーマット構造体
    ' 機能説明  : 入力値のフォーマット処理を行う
    '--------------------------------------------------------------------
    Private Sub mFormatStrings(ByVal objGrid As DataGridView, _
                               ByVal hudtGrid As mEnmGrid, _
                               ByVal hintRowIndex As Integer, _
                               ByVal hudtLogFormat() As String)

        Try

            ''入力値が数値の場合フォーマット処理を行う
            If IsNumeric(objGrid(1, hintRowIndex).Value) Then

                ''設定値の取得
                Dim intSetNo As Integer = CCInt(objGrid(1, hintRowIndex).Value)

                Select Case CCInt(objGrid(0, hintRowIndex).Value)
                    Case gCstCodeOpsLogFormatTypeGroup

                        Dim intAutoSetCounter As Integer

                        ''グリッドイベントの連続実行防止
                        If hudtLogFormat(hintRowIndex).Substring(2) = intSetNo Then Exit Sub

                        ''入力値フォーマット
                        objGrid(1, hintRowIndex).Value = intSetNo.ToString("00")

                        ''設定エラーの場合は処理を抜ける
                        If mCheckSetErr(mblnModeGroup, hudtGrid, hintRowIndex, intSetNo, mudtSetOpsLogFormatWork, mudtSetChRLWork, intAutoSetCounter) Then Exit Sub

                        ''グループ情報の設定
                        Call mAutoSetGroup(objGrid, hudtLogFormat, mudtSetChRLWork, hintRowIndex, intSetNo, intAutoSetCounter)

                    Case gCstCodeOpsLogFormatTypeCh

                        ''グリッドイベントの連続実行防止
                        If hudtLogFormat(hintRowIndex).Substring(2) = intSetNo Then Exit Sub

                        ''入力値フォーマット
                        objGrid(1, hintRowIndex).Value = intSetNo.ToString("0000")

                        ''チャンネル情報の設定
                        hudtLogFormat(hintRowIndex) = gCstNameOpsLogFormatStringsCH & intSetNo

                End Select

                ''操作不可設定クリア
                Call mInitialDataGridSettings()

                ''表示切り換え処理
                Call mSetDisplay(mudtSetOpsLogFormatWork)
                Call mSetDisplayPreview(mudtSetOpsLogFormatWork)

                ''操作不可設定
                Call mControlRowEnable()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : プルダウンメニューの変更処理
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) グリッドオブジェクト
    '           : ARG2 - (I ) 行Index
    ' 機能説明  : プルダウンメニューを変更した際の処理を行う
    '--------------------------------------------------------------------
    Private Sub mChangePullDownMenu(ByVal objGrid As DataGridView, _
                                    ByVal hintRowIndex As Integer)

        Try

            Dim intType As Integer = CCInt(objGrid(0, hintRowIndex).Value)
            Dim intErrFlg As Integer = mCheckPossibleToSetData(mudtSetOpsLogFormatWork, objGrid, hintRowIndex)

            ''データ設定が可能かチェックする
            Select Case intErrFlg
                Case 1

                    If MessageBox.Show("The setting data protrudes beyond the page." & vbNewLine & _
                                       "May I continue operation?", _
                                       Me.Text, _
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then Exit Sub

                Case 2

                    If MessageBox.Show("The setting data exceeds the number of maximum records." & vbNewLine & _
                                       "May I continue operation?", _
                                       Me.Text, _
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then Exit Sub

                Case 3

                    If MessageBox.Show("The setting data exists. " & vbNewLine & _
                                       "May I overwrite it?", _
                                       Me.Text, _
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then Exit Sub

            End Select

            ''データ設定
            Call mInitialRowInfo(objGrid, hintRowIndex, intType)

            ''設定値を作業用構造体に格納
            Call mSetStructure(mudtSetOpsLogFormatWork)

            ''グリッドの操作不可設定初期化
            Call mInitialDataGridSettings()

            ''データ表示
            Call mSetDisplay(mudtSetOpsLogFormatWork)
            Call mSetDisplayPreview(mudtSetOpsLogFormatWork)

            ''操作不可設定
            Call mControlRowEnable()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ボタンコントロール設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : InsertとDeleteボタンのコントロール設定
    '--------------------------------------------------------------------
    Private Sub mControlEnableBtn()

        Try

            If mblnSelectGridCol1 Then
                Call mControlEnableBtn(grdLogCol1)
            Else
                Call mControlEnableBtn(grdLogCol2)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mControlEnableBtn(ByVal objGrid As DataGridView)

        Try

            ''ポジション
            Dim x As Integer = objGrid.CurrentCellAddress.X
            Dim y As Integer = objGrid.CurrentCellAddress.Y

            ''編集ページの最大行Index取得
            Dim intMaxRowIndexOfEditPage As Integer = mAddRowDataMaxRowIdxOfPage(objGrid.CurrentCell.RowIndex)

            ''ボタンコントロール
            If (objGrid(x, y).OwningColumn.DataGridView.CurrentCell.Style.BackColor = gColorGridRowBackReadOnly) Or _
               (objGrid.CurrentCell.OwningColumn.Name = objGrid.Columns(2).Name) Then

                cmdDelete.Enabled = False
                cmdInsert.Enabled = False

            ElseIf (objGrid.CurrentCell.RowIndex = intMaxRowIndexOfEditPage) Then

                cmdDelete.Enabled = True
                cmdInsert.Enabled = False

            Else

                cmdDelete.Enabled = True
                cmdInsert.Enabled = True

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : コピーペーストキャンセル時のメッセージ
    ' 返り値    : なし
    ' 引き数    : なし（※ARG1 - (I ) エラーメッセージコード）
    ' 機能説明  : コピーペーストキャンセル時に表示するメッセージ
    '--------------------------------------------------------------------
    Private Sub mDipsErrMsg()

        Try

            Select Case gintErrMsg
                Case 1

                    MessageBox.Show("The copy data protrudes beyond the page." & vbNewLine & _
                                    "The copy operation stopped. ", _
                                    Me.Text, _
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)

            End Select

            ''エラーメッセージフラグ初期化
            gintErrMsg = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


    '全自動設定ボタン
    Private Sub btnALL_Click(sender As System.Object, e As System.EventArgs) Handles btnALL.Click
        Call subSetAllLog()
    End Sub



End Class

''datagrid：プレビューセルの枠線を消す
Class clsDataGridViewBorderPaint

    Inherits DataGridViewTextBoxCell

    Protected Overrides Sub PaintBorder(ByVal graphics As Graphics, _
                                        ByVal clipBounds As Rectangle, _
                                        ByVal bounds As Rectangle, _
                                        ByVal cellStyle As DataGridViewCellStyle, _
                                        ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle)

        advancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
        MyBase.PaintBorder(graphics, clipBounds, bounds, cellStyle, advancedBorderStyle)

    End Sub
End Class

''datagrid：プレビューセルの下線を引く
Class clsDataGridViewBorderPaintBottom

    Inherits DataGridViewTextBoxCell

    Protected Overrides Sub PaintBorder(ByVal graphics As Graphics, _
                                        ByVal clipBounds As Rectangle, _
                                        ByVal bounds As Rectangle, _
                                        ByVal cellStyle As DataGridViewCellStyle, _
                                        ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle)

        advancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single
        MyBase.PaintBorder(graphics, clipBounds, bounds, cellStyle, advancedBorderStyle)

    End Sub
End Class