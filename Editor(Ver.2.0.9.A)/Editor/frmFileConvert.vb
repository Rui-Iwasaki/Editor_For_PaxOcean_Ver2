Imports System.IO
Imports System.Reflection

Public Class frmFileConvert

#Region "既設_定数定義"

    ''既設の画面タイプ
    Private Const mCstCodeOpsGraphTypeNothing As Integer = &H0
    Private Const mCstCodeOpsGraphTypeExhaust As Integer = &H1
    Private Const mCstCodeOpsGraphTypeBarNormal As Integer = &H2
    Private Const mCstCodeOpsGraphTypeBarPercent As Integer = &H4
    Private Const mCstCodeOpsGraphTypeAnalogMeter As Integer = &H10
    Private Const mCstCodeOpsGraphTypeBar6dig As Integer = &H8
    Private Const mCstCodeOpsGraphTypeBar6digPercent As Integer = &H20

#End Region

#Region "変数定義"

    ''端子台情報
    ''  0～20:　Fu No(A～T)
    ''  0～7 :　TB1～TB8
    ''  0～2 :  0:行数   1:アナログ=1 OR デジタル(DI)=2 ORデジタル(DO)=3 OR アナログ(AO)=4   2:基板種類
    Private mSlotInfo(20, 7, 2) As Integer

    Private udtOpsDataAnalog() As gTypConvertOpsGraphData = Nothing     ''T10：排気ガス・バーグラフ
    Private udtOpsDataExhBar() As gTypConvertOpsGraphData = Nothing     ''T11：アナログメーター
    Private udtOpsData() As gTypConvertOps = Nothing                    ''コンバート用OPS設定構造体

    ''モーターのステータス情報格納
    Private mMotorStatus1() As String
    Private mMotorStatus2() As String
    Private mMotorBitPos1() As String
    Private mMotorBitPos2() As String

    Private mCanubusFunc As Integer = 0

    Private mstrFileName As String = ""
    Private mbln22kFileRead As Boolean = False
    Private mblnExtFileRead As Boolean = False

    ''22kファイルパス ------------------------------------------
    Private mstr22kFilePathT07 As String        ''システム設定
    Private mstr22kFilePathT09 As String        ''システム設定２
    Private mstr22kFilePathT999 As String       ''システム設定(プリンタIP)
    Private mstr22kFilePathT08 As String        ''OPSオプション設定(グループ表示位置)
    Private mstr22kFilePathGrs As String        ''グループ設定
    Private mstr22kFilePathMpt As String        ''チャンネル情報
    Private mstr22kFilePathUni As String        ''端子台情報(スロット種別）
    Private mstr22kFilePathRyt As String        ''リレー端子台情報
    Private mstr22kFilePathCan As String        ''CANBUS L/U
    Private mstr22kFilePathLpf As String        ''端子台情報
    Private mstr22kFilePathCmj As String        ''DOケーブルマーク情報
    Private mstr22kFilePathT20 As String        ''DO設定
    Private mstr22kFilePathT21 As String        ''論理出力設定
    Private mstr22kFilePathT22 As String        ''AO設定
    Private mstr22kFilePathT52 As String        ''運転積算設定
    Private mstr22kFilePathT25 As String        ''リポーズ設定
    Private mstr22kFilePathT19 As String        ''排ガス演算設定
    Private mstr22kFilePathT46 As String        ''コントロール使用可/不可設定
    Private mstr22kFilePathSIO As String        ''SIO設定
    Private mstr22kFilePathS1 As String         ''SIO設定CH設定 S1
    Private mstr22kFilePathS2 As String         ''SIO設定CH設定 S2
    Private mstr22kFilePathS3 As String         ''SIO設定CH設定 S3
    Private mstr22kFilePathS4 As String         ''SIO設定CH設定 S4
    Private mstr22kFilePathS5 As String         ''SIO設定CH設定 S5
    Private mstr22kFilePathS6 As String         ''SIO設定CH設定 S6
    Private mstr22kFilePathS7 As String         ''SIO設定CH設定 S7
    Private mstr22kFilePathS8 As String         ''SIO設定CH設定 S8
    Private mstr22kFilePathS9 As String         ''SIO設定CH設定 S9
    Private mstr22kFilePathT38 As String        ''データ転送テーブル設定
    Private mstr22kFilePathDST As String        ''データ保存テーブル設定
    Private mstr22kFilePathSks As String        ''シーケンス設定
    Private mstr22kFilePathRin As String        ''リニアライズテーブル設定
    Private mstr22kFilePathT36 As String        ''演算式テーブル設定1
    Private mstr22kFilePathTei As String        ''演算式テーブル設定2
    Private mstr22kFilePathT10 As String        ''OPS設定_バーグラフ・排ガスグラフ
    Private mstr22kFilePathT11 As String        ''OPS設定_アナログメータ
    Private mstr22kFilePathT12 As String        ''OPS設定_グラフ表示用 CHID
    Private mstr22kFilePathT101 As String       ''OPS設定_プルダウンメニュー(OPS)

    Private mstr22kFilePathT108 As String       ''OPS設定_セレクションメニュー(OPS)
    Private mstr22kFilePathT118 As String       ''OPS設定_セレクションメニュー(OPS)ボタン名称

    'T.Ueki 2014/5/29
    Private mstr22kFilePathT701 As String       ''GWS設定_通信管理テーブル
    Private mstr22kFilePathT702 As String       ''GWS設定_通信チャンネルID設定テーブル

    Private mstr22kFilePathLTT As String        ''ログフォーマット
    Private mstr22kFilePathEXT As String        ''延長警報設定

    ''22k読み込み用のbyte配列 ----------------------------------
    Private mbyt22kFileArrayT07() As Byte       ''システム設定
    Private mbyt22kFileArrayT09() As Byte       ''システム設定２
    Private mbyt22kFileArrayT999() As Byte      ''システム設定(プリンタIP)
    Private mbyt22kFileArrayT08() As Byte       ''OPSオプション設定
    Private mbyt22kFileArrayGrs() As Byte       ''グループ設定
    Private mbyt22kFileArrayMpt() As Byte       ''チャンネル情報
    Private mbyt22kFileArrayUni() As Byte       ''端子台情報(スロット種別）
    Private mbyt22kFileArrayRyt() As Byte       ''リレー端子台情報
    Private mbyt22kFileArrayCan() As Byte       ''CANBUS L/U
    Private mbyt22kFileArrayLpf() As Byte       ''端子台情報
    Private mbyt22kFileArrayCmj() As Byte       ''DOケーブルマーク情報
    Private mbyt22kFileArrayT20() As Byte       ''DO設定
    Private mbyt22kFileArrayT21() As Byte       ''論理出力設定
    Private mbyt22kFileArrayT22() As Byte       ''AO設定
    Private mbyt22kFileArrayT52() As Byte       ''運転積算
    Private mbyt22kFileArrayT25() As Byte       ''リポーズ設定
    Private mbyt22kFileArrayT19() As Byte       ''排ガス演算設定
    Private mbyt22kFileArrayT46() As Byte       ''コントロール使用可/不可設定
    Private mbyt22kFileArraySIO() As Byte       ''SIO設定
    Private mbyt22kFileArrayS1() As Byte        ''SIO設定CH設定 S1
    Private mbyt22kFileArrayS2() As Byte        ''SIO設定CH設定 S2
    Private mbyt22kFileArrayS3() As Byte        ''SIO設定CH設定 S3
    Private mbyt22kFileArrayS4() As Byte        ''SIO設定CH設定 S4
    Private mbyt22kFileArrayS5() As Byte        ''SIO設定CH設定 S5
    Private mbyt22kFileArrayS6() As Byte        ''SIO設定CH設定 S6
    Private mbyt22kFileArrayS7() As Byte        ''SIO設定CH設定 S7
    Private mbyt22kFileArrayS8() As Byte        ''SIO設定CH設定 S8
    Private mbyt22kFileArrayS9() As Byte        ''SIO設定CH設定 S9
    Private mbyt22kFileArrayT38() As Byte       ''データ転送テーブル設定
    Private mbyt22kFileArrayDST() As Byte       ''データ保存テーブル設定
    Private mbyt22kFileArraySks() As Byte       ''シーケンス設定
    Private mbyt22kFileArrayRin() As Byte       ''リニアライズテーブル設定
    Private mbyt22kFileArrayT36() As Byte       ''演算式テーブル設定1
    Private mbyt22kFileArrayTei() As Byte       ''演算式テーブル設定2
    Private mbyt22kFileArrayT10() As Byte       ''OPS設定_バーグラフ・排ガスグラフ
    Private mbyt22kFileArrayT11() As Byte       ''OPS設定_アナログメータ
    Private mbyt22kFileArrayT12() As Byte       ''OPS設定_グラフ表示用 CHID
    Private mbyt22kFileArrayT101() As Byte      ''OPS設定_プルダウンメニュー(OPS)

    Private mbyt22kFileArrayT108() As Byte      ''OPS設定_セレクションメニュー(OPS)
    Private mbyt22kFileArrayT118() As Byte      ''OPS設定_セレクションメニュー(OPS)

    'T.Ueki 2014/5/29
    Private mbyt22kFileArrayT701() As Byte      ''GWS設定_通信管理テーブル
    Private mbyt22kFileArrayT702() As Byte      ''GWS設定_通信チャンネルID設定テーブル

    Private mbyt22kFileArrayEXT() As Byte       ''延長警報設定


    Private mstrAstNorCH As String              ' */を変換した場合のCHno格納
#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmFileConvert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            prgBar.Visible = False
            prgBar.Minimum = 0
            prgBar.Maximum = 3050
            prgBar.Value = 0

            lblMessage.Text = ""
            lstMsg.Items.Clear()

            cmdConvert.Enabled = False

            GroupBox1.Top -= 30
            GroupBox3.Top -= 30

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChListValve_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

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

    '--------------------------------------------------------------------
    ' 機能      : 22kフォルダの検索ダイアログの表示
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : コンバート対象ファイルフォルダを獲得する
    '--------------------------------------------------------------------
    Private Sub cmdOpen22k_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen22k.Click

        Try

            Dim drresult As DialogResult

            With FolderBrowserDialog1

                drresult = .ShowDialog()

                If drresult = Windows.Forms.DialogResult.OK Then

                    ''22k---------------------------------
                    ''選択済みフォルダパス SET
                    lblOpenFile22k.Text = .SelectedPath

                    ''既設エディタ情報を変数に設定
                    Call minit22kVals()

                    mbln22kFileRead = True

                    ''Ext---------------------------------
                    ''選択済みフォルダパス SET
                    'lblOpenFileExt.Text = .SelectedPath

                    mstr22kFilePathEXT = .SelectedPath & "\EXT.DAT"

                    mblnExtFileRead = True

                    ''------------------------------------
                    ''Convertボタンを有効にする
                    cmdConvert.Enabled = True

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Convertボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : コンバート処理を開始する
    '--------------------------------------------------------------------
    Private Sub cmdConvert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConvert.Click

        Try

            ''プログレスバー初期化
            prgBar.Value = 0
            prgBar.Visible = True

            ''リストボックスクリア
            Call lstMsg.Items.Clear()

            ''プログレスバーの表示により、その他のオブジェクトを下にずらす
            GroupBox1.Top += 30 : GroupBox3.Top += 30
            Me.Refresh()

            ''出力構造体初期化
            Call gInitOutputStructure(gudt)
            prgBar.Value += 1 : prgBar.Refresh()

            ''22kファイル コンバート開始 -----------------------------------------
            If mbln22kFileRead Then

                ''既設のデータをバイト配列に読込み後、構造体に値をセットする

                ''システム設定
                If mread22kFile(mstr22kFilePathT07, mbyt22kFileArrayT07) = 0 Then
                    Call msetStructureT07(gudt.SetSystem)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                If mread22kFile(mstr22kFilePathT09, mbyt22kFileArrayT09) = 0 Then
                    Call msetStructureT09(gudt.SetSystem)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                If mread22kFile(mstr22kFilePathT999, mbyt22kFileArrayT999) = 0 Then
                    Call msetStructureT999(gudt.SetSystem)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''グループ設定
                If mread22kFile(mstr22kFilePathGrs, mbyt22kFileArrayGrs) = 0 Then
                    Call msetStructureGrs(gudt.SetChGroupSetM)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''OPSオプション設定(グループ表示位置)
                If mread22kFile(mstr22kFilePathT08, mbyt22kFileArrayT08) = 0 Then
                    Call msetStructureT08()
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''SIO設定
                If mread22kFile(mstr22kFilePathSIO, mbyt22kFileArraySIO) = 0 Then
                    Call msetStructureSIO(gudt.SetChSio, gudt.SetChSioCh)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''SIO設定CH設定
                If mread22kFile(mstr22kFilePathS1, mbyt22kFileArrayS1) = 0 Or _
                   mread22kFile(mstr22kFilePathS2, mbyt22kFileArrayS2) = 0 Or _
                   mread22kFile(mstr22kFilePathS3, mbyt22kFileArrayS3) = 0 Or _
                   mread22kFile(mstr22kFilePathS4, mbyt22kFileArrayS4) = 0 Or _
                   mread22kFile(mstr22kFilePathS5, mbyt22kFileArrayS5) = 0 Or _
                   mread22kFile(mstr22kFilePathS6, mbyt22kFileArrayS6) = 0 Or _
                   mread22kFile(mstr22kFilePathS7, mbyt22kFileArrayS7) = 0 Or _
                   mread22kFile(mstr22kFilePathS8, mbyt22kFileArrayS8) = 0 Or _
                   mread22kFile(mstr22kFilePathS9, mbyt22kFileArrayS9) = 0 Then
                    Call msetStructureSioCh(gudt.SetChSioCh)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''端子台情報(スロット情報)
                If mread22kFile(mstr22kFilePathUni, mbyt22kFileArrayUni) = 0 And _
                   mread22kFile(mstr22kFilePathRyt, mbyt22kFileArrayRyt) = 0 And _
                   mread22kFile(mstr22kFilePathCan, mbyt22kFileArrayCan) = 0 Then
                    Call msetStructureUni(gudt.SetFu, gudt.SetChDisp)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''端子台情報
                If mread22kFile(mstr22kFilePathLpf, mbyt22kFileArrayLpf) = 0 Then
                    Call msetStructureLpf(gudt.SetChDisp)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''チャンネル情報
                If mread22kFile(mstr22kFilePathMpt, mbyt22kFileArrayMpt) = 0 Then
                    Call msetStructureMpt(gudt.SetChInfo, gudt.SetChDisp, gudt.SetChGroupSetM)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''DO設定, 論理出力設定
                If mread22kFile(mstr22kFilePathT20, mbyt22kFileArrayT20) = 0 And _
                   mread22kFile(mstr22kFilePathT21, mbyt22kFileArrayT21) = 0 And _
                   mread22kFile(mstr22kFilePathCmj, mbyt22kFileArrayCmj) = 0 Then
                    Call msetStructureT20(gudt.SetChOutput, gudt.SetChAndOr)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''AO設定はAO出力CH(アドレス設定)が必要につきコンバートしない
                ''AO設定
                'If mread22kFile(mstr22kFilePathT22, mbyt22kFileArrayT22) = 0 Then
                '    Call msetStructureT22(gudt.SetChDisp)
                'End If
                'prgBar.Value += 1 : prgBar.Refresh()

                ''運転積算
                If mread22kFile(mstr22kFilePathT52, mbyt22kFileArrayT52) = 0 Then
                    Call msetStructureT52(gudt.SetChRunHour, gudt.SetChInfo)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''リポーズ設定
                If mread22kFile(mstr22kFilePathT25, mbyt22kFileArrayT25) = 0 Then
                    Call msetStructureT25(gudt.SetChGroupRepose)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''排ガス演算設定
                If mread22kFile(mstr22kFilePathT19, mbyt22kFileArrayT19) = 0 Then
                    Call msetStructureT19(gudt.SetChExhGus)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''コントロール使用可/不可設定（既設の情報はMachineryに設定する）
                If mread22kFile(mstr22kFilePathT46, mbyt22kFileArrayT46) = 0 Then
                    Call msetStructureT46(gudt.SetChCtrlUseM)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''データ転送テーブル設定
                If mread22kFile(mstr22kFilePathT38, mbyt22kFileArrayT38) = 0 Then
                    Call msetStructureT38(gudt.SetChDataForward)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''データ保存テーブル設定
                If mread22kFile(mstr22kFilePathDST, mbyt22kFileArrayDST) = 0 Then
                    Call msetStructureDST(gudt.SetChDataSave)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''シーケンス設定
                If mread22kFile(mstr22kFilePathSks, mbyt22kFileArraySks) = 0 Then
                    Call msetStructureSks(gudt.SetSeqSet)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''リニアライズテーブル設定
                If mread22kFile(mstr22kFilePathRin, mbyt22kFileArrayRin) = 0 Then
                    Call msetStructureRin(gudt.SetSeqLinear)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''演算式テーブル設定1,2
                If mread22kFile(mstr22kFilePathT36, mbyt22kFileArrayT36) = 0 And _
                   mread22kFile(mstr22kFilePathTei, mbyt22kFileArrayTei) = 0 Then
                    Call msetStructureT36(gudt.SetSeqOpeExp)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''OPS設定（T10, T11, T12）
                If mread22kFile(mstr22kFilePathT10, mbyt22kFileArrayT10) = 0 And _
                   mread22kFile(mstr22kFilePathT11, mbyt22kFileArrayT11) = 0 And _
                   mread22kFile(mstr22kFilePathT12, mbyt22kFileArrayT12) = 0 Then

                    prgBar.Value += 1

                    ''バイト配列データをコンバート用OPS構造体に設定
                    Call mMakeOpsStructure(udtOpsData, udtOpsDataExhBar, udtOpsDataAnalog) : prgBar.Value += 1

                    ''グローバル構造体に設定
                    Call mSetStructureOps(udtOpsData) : prgBar.Value += 1

                End If

                'OPS設定(T101)
                'If mread22kFile(mstr22kFilePathT101, mbyt22kFileArrayT101) = 0 Then
                '    Call msetStructureT101(gudt.SetOpsPulldownMenuM)
                'End If

                ''MainMenu.datを読み込む(デフォルト)　ver.1.4.0 2011.09.21
                'Ver2.0.1.8 VDUは違うMainMenu.datを読み込む
                Call gLoadMenuMain(gudt.SetOpsPulldownMenuM)            'OPS
                Call gLoadMenuMain(gudt.SetOpsPulldownMenuC, True)      'VDU

                ''OPS設定(T108)
                'If mread22kFile(mstr22kFilePathT108, mbyt22kFileArrayT108) = 0 Then
                '    Call msetStructureT108(gudt.SetOpsSelectionMenuM)
                'End If

                ''OPS設定(T118)
                'If mread22kFile(mstr22kFilePathT118, mbyt22kFileArrayT118) = 0 Then
                '    Call msetStructureT118(gudt.SetOpsSelectionMenuM)
                'End If

                ''SelectionMenu.datを読み込む(デフォルト)　2015.01.20
                Call gLoadSelectionMenuMain(gudt.SetOpsSelectionMenuM)
                Call gLoadSelectionMenuMain(gudt.SetOpsSelectionMenuC)

                prgBar.Value += 1 : prgBar.Refresh()

                'T.Ueki 2014/5/29
                ''GWS設定
                If mread22kFile(mstr22kFilePathT701, mbyt22kFileArrayT701) = 0 Then
                    Call msetStructureGWS(gudt.SetSystem)
                End If
                prgBar.Value += 1 : prgBar.Refresh()

                ''ログフォーマット(LTT)  テキストファイル
                Call msetStructureLTT()

            End If

            ''延長警報ファイル コンバート開始 ------------------------------------
            If mblnExtFileRead Then

                ''延長警報盤設定
                If mread22kFile(mstr22kFilePathEXT, mbyt22kFileArrayEXT) = 0 Then
                    Call msetStructureExtAlarm(gudt.SetExtAlarm, gudt.SetExtTimerSet, gudt.SetExtTimerName)
                End If

                prgBar.Value += 1 : prgBar.Refresh()

            End If
            ''---------------------------------------------------------------------

            prgBar.Value = 3050

            lblMessage.Text = "Finished converting."
            lblMessage.Refresh()

            MsgBox("Finished converting.  ")

            '「*/」の特殊変換をした場合ﾒｯｾｰｼﾞ表示
            If mstrAstNorCH <> "" Then
                MsgBox("Analog Status Special Convert." & vbCrLf & vbCrLf & mstrAstNorCH)
            End If

            ''全ファイルの更新フラグを立てる
            gblnUpdateAll = True
            Call gInitSetEditorUpdateInfo(gudt.SetEditorUpdateInfo)

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

#Region "システム設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : システム設定(T07)
    '--------------------------------------------------------------------
    Private Sub msetStructureT07(ByRef udtSet As gTypSetSystem)

        Try
            With udtSet.udtSysSystem

                ''システムクロック
                .shtClock = mbyt22kFileArrayT07(160)

                ''日付フォーマット
                .shtDate = mbyt22kFileArrayT07(161)

                ''日本語対応
                .shtLanguage = mbyt22kFileArrayT07(164)

                ''コンバイン有無
                .shtCombineUse = IIf(gBitCheck(mbyt22kFileArrayT07(172), 0), 1, 0)
                .shtCombineUse = IIf(gBitCheck(mbyt22kFileArrayT07(172), 2), 2, .shtCombineUse)

                ''コンバインセパレート
                .shtCombineSeparate = IIf(gBitCheck(mbyt22kFileArrayT07(172), 1), 1, 0)

                '▼▼▼ 20110330 .shtStatus削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                ' ''システムステータス設定
                '.shtStatus = gBitSet(.shtStatus, 0, IIf(gBitCheck(mbyt22kFileArrayT07(163), 0), False, True))   ''システムステータス代表画面作成
                '.shtStatus = gBitSet(.shtStatus, 1, IIf(gBitCheck(mbyt22kFileArrayT07(163), 1), True, False))   ''FCU A/B 反転
                '.shtStatus = gBitSet(.shtStatus, 2, IIf(gBitCheck(mbyt22kFileArrayT07(163), 2), True, False))   ''OPSステータス画面自動作成
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ''CANBUS FUNCTION
                mCanubusFunc = IIf(mbyt22kFileArrayT07(198) <> 0, 1, 0)

                'GWS1設定
                If mbyt22kFileArrayT07(62) <> 0 Then
                    If mbyt22kFileArrayT07(62) - 128 <> 0 Then
                        .shtGWS1 = gBitSet(.shtGWS1, 0, True)   ''GWS有無
                    End If
                    'ver1.4.0 2011.08.25
                    '.shtGWS1 = gBitSet(.shtGWS1, 1, IIf(gBitCheck(mbyt22kFileArrayT07(62), 7), True, False))    ''Ethernet Line A Only
                    If gBitCheck(mbyt22kFileArrayT07(62), 7) Then
                        .shtGWS1 = gBitSet(.shtGWS1, 1, True)   ''Ethernet Line A Only
                    Else
                        .shtGWS1 = gBitSet(.shtGWS1, 2, True)   ''Ethernet Line A and B
                    End If
                End If

                ''GWS2設定
                If mbyt22kFileArrayT07(63) <> 0 Then
                    If mbyt22kFileArrayT07(63) - 128 <> 0 Then
                        .shtGWS2 = gBitSet(.shtGWS2, 0, True)   ''GWS有無
                    End If
                    'ver1.4.0 2011.08.25
                    '.shtGWS2 = gBitSet(.shtGWS2, 1, IIf(gBitCheck(mbyt22kFileArrayT07(63), 7), True, False))    ''Ethernet Line A Only
                    If gBitCheck(mbyt22kFileArrayT07(63), 7) Then
                        .shtGWS2 = gBitSet(.shtGWS2, 1, True)   ''Ethernet Line A Only
                    Else
                        .shtGWS2 = gBitSet(.shtGWS2, 2, True)   ''Ethernet Line A and B
                    End If
                End If

            End With

            With udtSet.udtSysFcu

                ''FCU番号に変更につき、22Kのコンバート処理なし ver.1.4.0 2011.09.29
                ''FCU台数
                '.shtFcuNo = mbyt22kFileArrayT07(82)
                .shtFcuNo = 1

                ''拡張基板 ver1.4.0 2011.08.25
                .shtFcuExtendBord = mbyt22kFileArrayT07(135)

                '▼▼▼ 20110330 .shtLogBackup削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                ' ''イベントログバックアップ
                '.shtLogBackup = mbyt22kFileArrayT07(200)
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            End With

            With udtSet.udtSysOps

                ''各機器遠隔操作許可
                .shtControl = mbyt22kFileArrayT07(166)

                ''EXTグループ,グループリポーズ変更許可
                .shtProhibition = mbyt22kFileArrayT07(174)

                ''アラーム表示方法
                .shtAlarm = mbyt22kFileArrayT07(173) + 1

            End With

            ''OPS 10台
            For i As Integer = 0 To 9

                With udtSet.udtSysOps.udtOpsDetail(i)

                    ''OPS接続有無
                    .shtExist = IIf(gBitCheck(mbyt22kFileArrayT07(96 + i), 0), 1, 0)

                    If .shtExist = 1 Then

                        ''アラーム表示モード
                        If gBitCheck(mbyt22kFileArrayT07(96 + i), 4) Then .shtAlarmDisp = 1 ''AUTO

                        If gBitCheck(mbyt22kFileArrayT07(96 + i), 5) Then .shtAlarmDisp = 0 ''INHIBIT

                        If gBitCheck(mbyt22kFileArrayT07(96 + i), 2) Then
                            .shtAlarmDisp = 2   ''WINDOW
                        End If

                        ''OPS設定変更
                        If gBitCheck(mbyt22kFileArrayT07(96 + i), 3) Then .shtEnable = 1

                        ''遠隔操作
                        If gBitCheck(mbyt22kFileArrayT07(96 + i), 1) Then
                            .shtControl = gBitSet(.shtControl, 1, True)     ''Initial
                        End If

                        ''通信Aラインのみ使用
                        .shtEtherA = IIf(gBitCheck(mbyt22kFileArrayT07(32 + i), 7), 1, 0)

                        ''OPS解像度
                        .shtResolution = mbyt22kFileArrayT07(162) + 1
                    End If

                End With

            Next

            ''プリンタ設定
            With udtSet.udtSysPrinter

                '' '' ''用紙サイズA3（ログプリンタ）   中止 2011.12.13 K.Tanigawa
                '' ''.shtPageA3 = IIf(gBitCheck(mbyt22kFileArrayT07(541), 2), 1, 0)

                ''英数・日本語設定（アラームプリンタ）
                If mbyt22kFileArrayT07(165) = 0 Then
                    .shtPrintType = 3   ''DoubleSize(Reduction) 全角はないので全角縮小とする
                ElseIf mbyt22kFileArrayT07(165) = 1 Then
                    .shtPrintType = 1   ''SingleSize
                ElseIf mbyt22kFileArrayT07(165) = 2 Then
                    .shtPrintType = 3   ''Double(Reduction)
                End If

                ''イベントプリント（アラームプリンタ）
                .shtEventPrint = IIf(mbyt22kFileArrayT07(167) = 0, 0, 1)

                ''ヌーンログ下線
                .shtNoonUnder = IIf(gBitCheck(mbyt22kFileArrayT07(541), 5), 1, 0)

                ''デマンドログ改ページ
                .shtDemandPage = IIf(gBitCheck(mbyt22kFileArrayT07(541), 6), 1, 0)

                ''Machinery/Cargo印字
                .shtMachineryCargoPrint = IIf(gBitCheck(mbyt22kFileArrayT07(541), 3), 1, 0)

                ''Log Pr1
                If gBitCheck(mbyt22kFileArrayT07(129), 0) Then

                    ''プリンタ有無(1:NADA 2:NEC 3:Ethernet)
                    If gBitCheck(mbyt22kFileArrayT07(129), 1) Then
                        ''ネットワーク
                        .udtPrinterDetail(0).bytPrinter = 3
                    Else
                        ''シリアル
                        If gBitCheck(mbyt22kFileArrayT07(541), 4) Then
                            .udtPrinterDetail(0).bytPrinter = 2
                        Else
                            .udtPrinterDetail(0).bytPrinter = 1
                        End If
                    End If

                    .udtPrinterDetail(0).shtPrintUse = gBitSet(.udtPrinterDetail(0).shtPrintUse, 0, True)   ''印字有り
                    .udtPrinterDetail(0).shtPart = gBitSet(.udtPrinterDetail(0).shtPart, 0, IIf(gBitCheck(mbyt22kFileArrayT07(129), 4), True, False))   ''Machinery
                    .udtPrinterDetail(0).shtPart = gBitSet(.udtPrinterDetail(0).shtPart, 1, IIf(gBitCheck(mbyt22kFileArrayT07(129), 5), True, False))   ''Cargo

                End If

                ''Log Pr2
                If gBitCheck(mbyt22kFileArrayT07(131), 0) Then

                    ''プリンタ有無(1:NADA 3:Ethernet)
                    If gBitCheck(mbyt22kFileArrayT07(131), 1) Then
                        ''ネットワーク
                        .udtPrinterDetail(1).bytPrinter = 3
                    Else
                        ''シリアル
                        .udtPrinterDetail(1).bytPrinter = 1
                    End If

                    .udtPrinterDetail(1).shtPrintUse = gBitSet(.udtPrinterDetail(1).shtPrintUse, 0, True)   ''印字有り
                    .udtPrinterDetail(1).shtPart = gBitSet(.udtPrinterDetail(1).shtPart, 0, IIf(gBitCheck(mbyt22kFileArrayT07(131), 4), True, False))   ''Machinery
                    .udtPrinterDetail(1).shtPart = gBitSet(.udtPrinterDetail(1).shtPart, 1, IIf(gBitCheck(mbyt22kFileArrayT07(131), 5), True, False))   ''Cargo

                End If

                ''Alarm Pr1
                If gBitCheck(mbyt22kFileArrayT07(128), 0) Then

                    ''プリンタ有無
                    .udtPrinterDetail(2).bytPrinter = 1

                    .udtPrinterDetail(2).shtPrintUse = gBitSet(.udtPrinterDetail(2).shtPrintUse, 0, True)   ''印字有り
                    .udtPrinterDetail(2).shtPart = gBitSet(.udtPrinterDetail(2).shtPart, 0, IIf(gBitCheck(mbyt22kFileArrayT07(128), 4), True, False))   ''Machinery
                    .udtPrinterDetail(2).shtPart = gBitSet(.udtPrinterDetail(2).shtPart, 1, IIf(gBitCheck(mbyt22kFileArrayT07(128), 5), True, False))   ''Cargo

                End If

                ''Alarm Pr2
                If gBitCheck(mbyt22kFileArrayT07(130), 0) Then

                    ''プリンタ有無
                    .udtPrinterDetail(3).bytPrinter = 1

                    .udtPrinterDetail(3).shtPrintUse = gBitSet(.udtPrinterDetail(3).shtPrintUse, 0, True)   ''印字有り
                    .udtPrinterDetail(3).shtPart = gBitSet(.udtPrinterDetail(3).shtPart, 0, IIf(gBitCheck(mbyt22kFileArrayT07(130), 4), True, False))   ''Machinery
                    .udtPrinterDetail(3).shtPart = gBitSet(.udtPrinterDetail(3).shtPart, 1, IIf(gBitCheck(mbyt22kFileArrayT07(130), 5), True, False))   ''Cargo

                End If

                ''HC
                If gBitCheck(mbyt22kFileArrayT07(130), 0) Then

                    ''プリンタ有無
                    .udtPrinterDetail(4).bytPrinter = 1

                    .udtPrinterDetail(4).shtPrintUse = gBitSet(.udtPrinterDetail(4).shtPrintUse, 0, True)   ''印字有り
                    .udtPrinterDetail(4).shtPart = gBitSet(.udtPrinterDetail(4).shtPart, 0, IIf(gBitCheck(mbyt22kFileArrayT07(132), 4), True, False))   ''Machinery
                    .udtPrinterDetail(4).shtPart = gBitSet(.udtPrinterDetail(4).shtPart, 1, IIf(gBitCheck(mbyt22kFileArrayT07(132), 5), True, False))   ''Cargo

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : システム設定・プリンタ(T999)
    '--------------------------------------------------------------------
    Private Sub msetStructureT999(ByRef udtSet As gTypSetSystem)

        Try
            Dim bytArray() As Byte
            Dim strValue As String
            Dim strArray() As String

            With udtSet.udtSysPrinter.udtPrinterDetail(0)   ''ログプリンタ1

                ''ネットワークプリンタの場合のみ設定
                If .bytPrinter = 3 Then

                    ''プリンタデバイス
                    ReDim bytArray(31)
                    For i As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(i) = mbyt22kFileArrayT999(0 + i)
                    Next
                    .strDevice = mByte2String(32, bytArray)

                    ''プリンタドライバ
                    ReDim bytArray(15)
                    For i As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(i) = mbyt22kFileArrayT999(32 + i)
                    Next
                    .strDriver = mByte2String(16, bytArray)

                    ''IPアドレス
                    ReDim bytArray(15)
                    For i As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(i) = mbyt22kFileArrayT999(48 + i)
                    Next
                    strValue = mByte2String(16, bytArray)

                    strArray = strValue.Split(".")
                    If UBound(strArray) = 3 Then
                        .bytIP1 = CCInt(strArray(0))
                        .bytIP2 = CCInt(strArray(1))
                        .bytIP3 = CCInt(strArray(2))
                        .bytIP4 = CCInt(strArray(3))
                    End If

                End If

            End With

            With udtSet.udtSysPrinter.udtPrinterDetail(4)   ''ハードコピープリンタ1

                ''ハードコピープリンタ有りの場合のみ設定
                If .bytPrinter = 1 Then

                    ''プリンタデバイス
                    ReDim bytArray(31)
                    For i As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(i) = mbyt22kFileArrayT999(0 + 64 + i)
                    Next
                    .strDevice = mByte2String(32, bytArray)

                    ''プリンタドライバ
                    ReDim bytArray(15)
                    For i As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(i) = mbyt22kFileArrayT999(32 + 64 + i)
                    Next
                    .strDriver = mByte2String(16, bytArray)

                    ''IPアドレス
                    ReDim bytArray(15)
                    For i As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(i) = mbyt22kFileArrayT999(48 + 64 + i)
                    Next
                    strValue = mByte2String(16, bytArray)

                    strArray = strValue.Split(".")
                    If UBound(strArray) = 3 Then
                        .bytIP1 = CCInt(strArray(0))
                        .bytIP2 = CCInt(strArray(1))
                        .bytIP3 = CCInt(strArray(2))
                        .bytIP4 = CCInt(strArray(3))
                    End If

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : システム設定(T09)
    '--------------------------------------------------------------------
    Private Sub msetStructureT09(ByRef udtSet As gTypSetSystem)

        ''OPS 10台
        For i As Integer = 0 To 9

            With udtSet.udtSysOps.udtOpsDetail(i)

                If .shtExist = 1 Then

                    ''コントロール制限
                    .shtControlFlag = gBitSet(.shtControlFlag, 0, gBitCheck(mbyt22kFileArrayT09(22 + i * 32), 0))   ''ECC
                    .shtControlFlag = gBitSet(.shtControlFlag, 1, gBitCheck(mbyt22kFileArrayT09(22 + i * 32), 1))   ''EMC
                    .shtControlFlag = gBitSet(.shtControlFlag, 2, gBitCheck(mbyt22kFileArrayT09(22 + i * 32), 2))   ''WCC
                    .shtControlFlag = gBitSet(.shtControlFlag, 3, gBitCheck(mbyt22kFileArrayT09(22 + i * 32), 3))   ''

                    ''コントロール入力禁止
                    ''22Kは4バイトなので2バイト分のみ変換する
                    .shtControlProhFlag = gConnect2Byte(mbyt22kFileArrayT09(28 + i * 32), mbyt22kFileArrayT09(28 + i * 32))

                    ''オペレーションパネル
                    .shtOperaionPanel = IIf(gBitCheck(mbyt22kFileArrayT09(5 + i * 32), 0), 0, 1)

                    ''調光機能
                    .shtAdjustLight = IIf(gBitCheck(mbyt22kFileArrayT09(5 + i * 32), 1), 1, 0)

                    ''HATTELAND製液晶接続
                    .shtHatteland = IIf(gBitCheck(mbyt22kFileArrayT09(8 + i * 32), 0), 1, 0)

                    ''印字パート
                    .shtPrintPart = gBitSet(.shtPrintPart, 0, gBitCheck(mbyt22kFileArrayT09(9 + i * 32), 4))   ''Machinery
                    .shtPrintPart = gBitSet(.shtPrintPart, 1, gBitCheck(mbyt22kFileArrayT09(9 + i * 32), 5))   ''Cargo

                    ''OPS表示モード（コンバイン時のみ）
                    .shtOpsType = gBitSet(.shtOpsType, 0, gBitCheck(mbyt22kFileArrayT09(7 + i * 32), 0))   ''Machinery
                    .shtOpsType = gBitSet(.shtOpsType, 1, gBitCheck(mbyt22kFileArrayT09(7 + i * 32), 1))   ''Cargo

                    ''起動モード
                    If gBitCheck(mbyt22kFileArrayT09(23 + i * 32), 0) Then
                        .shtBootMode = gBitSet(.shtBootMode, 1, True)   ''Cargo
                    Else
                        .shtBootMode = gBitSet(.shtBootMode, 0, True)   ''Machinery
                    End If

                    ''リポーズサマリ
                    .shtRepSum = IIf(gBitCheck(mbyt22kFileArrayT09(24 + i * 32), 0), 1, 0)

                    ''遠隔操作
                    If mbyt22kFileArrayT09(4 + i * 32) = 1 Then
                        .shtControl = gBitSet(.shtControl, 0, True)     ''Func
                    End If

                End If

            End With

        Next

    End Sub

#End Region

#Region "グループ設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : グループ設定(Grs)
    '--------------------------------------------------------------------
    Private Sub msetStructureGrs(ByRef udtSet As gTypSetChGroupSet)

        Try

            Dim bytArray(25) As Byte    ''22Kはグループ名称26Byte
            Dim strRtn As String
            Dim p1 As Integer, p2 As Integer
            Dim strWork As String

            With udtSet.udtGroup

                For i As Integer = 0 To gCstChannelGroupMax - 1

                    ''グループ名称
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayGrs(i * 28 + j)   '28：26文字+CRLF(0D,0A)
                    Next j
                    strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                    strRtn = gGetString(strRtn)


                    If strRtn.Length <= 16 Then

                        p1 = strRtn.IndexOf("^")    ''改行コード
                        If p1 >= 0 Then
                            .udtGroupInfo(i).strName1 = strRtn.Substring(0, p1)    ''1行目

                            p2 = strRtn.Substring(p1 + 1).IndexOf("^")
                            If p2 >= 0 Then
                                .udtGroupInfo(i).strName2 = strRtn.Substring(p1 + 1, p2)    ''2行目
                                .udtGroupInfo(i).strName3 = strRtn.Substring(p1 + p2 + 2)   ''3行目

                            Else
                                .udtGroupInfo(i).strName2 = strRtn.Substring(p1 + 1) ''2行目
                            End If

                        Else
                            .udtGroupInfo(i).strName1 = strRtn                  ''1行目
                        End If

                    Else

                        p1 = strRtn.IndexOf("^")
                        If p1 >= 0 Then

                            strWork = strRtn.Substring(0, p1)
                            If strWork.Length > 16 Then
                                .udtGroupInfo(i).strName1 = strWork.Substring(0, 16)    ''1行目
                                .udtGroupInfo(i).strName2 = strRtn.Substring(16)        ''2行目

                                p2 = strRtn.Substring(p1 + 1).IndexOf("^")
                                If p2 >= 0 Then
                                    .udtGroupInfo(i).strName3 = strRtn.Substring(p1 + 1, p2)    ''3行目
                                Else
                                    .udtGroupInfo(i).strName3 = strRtn.Substring(p1 + 1)        ''3行目
                                End If

                            Else
                                .udtGroupInfo(i).strName1 = strWork         ''1行目

                                p2 = strRtn.Substring(p1 + 1).IndexOf("^")
                                If p2 >= 0 Then
                                    .udtGroupInfo(i).strName2 = strRtn.Substring(p1 + 1, p2)    ''2行目
                                    .udtGroupInfo(i).strName3 = strRtn.Substring(p1 + p2 + 2)   ''3行目
                                Else
                                    strWork = strRtn.Substring(p1 + 1)
                                    If strWork.Length > 16 Then
                                        .udtGroupInfo(i).strName2 = strRtn.Substring(p1 + 1, 16)        ''2行目
                                        .udtGroupInfo(i).strName3 = strRtn.Substring(p1 + p2 + 2 + 16)  ''3行目
                                    Else
                                        .udtGroupInfo(i).strName2 = strRtn.Substring(p1 + 1)            ''2行目
                                    End If

                                End If

                            End If

                        Else
                            .udtGroupInfo(i).strName1 = strRtn.Substring(0, 16) ''1行目
                            .udtGroupInfo(i).strName2 = strRtn.Substring(16)    ''2行目
                        End If

                    End If

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "GWS通信管理設定"

    'T.Ueki
    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : GWS通信管理設定
    '--------------------------------------------------------------------
    Private Sub msetStructureGWS(ByRef udtSet As gTypSetSystem)

        Try

            Dim bytArray(16) As Byte    ''22Kはグループ名称26Byte

            Dim i As Integer
            Dim ir As Integer
            Dim ix As Integer
            Dim iy As Integer
            Dim iz As Integer
            Dim intRecSize As Integer = 160
            Dim IPAddress As String
            Dim IPAddressLen As Integer
            Dim IPAddressMid As String
            Dim strIPAdd(3) As String

            Dim CommType2 As Byte

            Dim GWSBitSet As Byte


            For i = 0 To 1

                With udtSet.udtSysGws.udtGwsDetail(i)

                    ''動作ﾀｲﾌﾟおよびIPｱﾄﾞﾚｽ

                    ''動作ﾀｲﾌﾟ
                    .shtGwsType = mbyt22kFileArrayT701(i * 32 + intRecSize)

                    ReDim bytArray(16)
                    For ir = LBound(bytArray) To UBound(bytArray)
                        bytArray(ir) = mbyt22kFileArrayT701(ir + (11 + i * 32 + intRecSize))
                    Next ir

                    IPAddress = gGetString(mByte2String(16, bytArray))

                    IPAddressLen = Len(IPAddress)

                    iy = 0
                    For ix = 1 To IPAddressLen
                        IPAddressMid = Mid(IPAddress, ix, 1)

                        If IPAddressMid <> "." Then
                            strIPAdd(iy) = strIPAdd(iy) + IPAddressMid
                        Else
                            iy = iy + 1
                        End If

                    Next

                    ''IPｱﾄﾞﾚｽ1
                    .bytIP1 = Val(strIPAdd(0))

                    ''IPｱﾄﾞﾚｽ2
                    .bytIP2 = Val(strIPAdd(1))

                    ''IPｱﾄﾞﾚｽ3
                    .bytIP3 = Val(strIPAdd(2))

                    ''IPｱﾄﾞﾚｽ4
                    .bytIP4 = Val(strIPAdd(3))

                    'IPｱﾄﾞﾚｽ初期化
                    For j = 0 To 3
                        strIPAdd(j) = ""
                    Next

                    ''予備
                    For iz = 0 To 3
                        .shtSpare(iz) = 0
                    Next

                    CommType2 = mbyt22kFileArrayT701(3 + i * 32 + intRecSize)

                    If CommType2 = 1 Then
                        .udtGwsFileInfo(i).bytSetFlg = gBitSet(CommType2, 1, True)
                    End If

                    If gBitCheck(mbyt22kFileArrayT701(29 + i * 32 + intRecSize), 1) = True Then
                        .udtGwsFileInfo(0).bytType = 1
                        .udtGwsFileInfo(1).bytType = 2
                        .udtGwsFileInfo(2).bytType = 3
                        .udtGwsFileInfo(3).bytType = 4
                    End If

                    GWSBitSet = mbyt22kFileArrayT701(29 + i * 32 + intRecSize)

                    If gBitCheck(GWSBitSet, 2) = True Then
                        .udtGwsFileInfo(i).bytSetFlg = gBitSet(GWSBitSet, 2, True)
                    End If

                    If gBitCheck(GWSBitSet, 3) = True Then
                        .udtGwsFileInfo(i).bytSetFlg = gBitSet(GWSBitSet, 2, True)
                    End If

                    If gBitCheck(GWSBitSet, 4) = True Then
                        .udtGwsFileInfo(i).bytSetFlg = gBitSet(GWSBitSet, 2, True)
                    End If

                    'If gBitCheck(mbyt22kFileArrayT701(29 + i * 32 + intRecSize), 2) = True Then
                    '    .udtGwsFileInfo(0).bytSetFlg = gBitValue(.udtGwsFileInfo(0).bytSetFlg, 2)
                    '    .udtGwsFileInfo(1).bytSetFlg = gBitValue(.udtGwsFileInfo(1).bytSetFlg, 2)
                    '    .udtGwsFileInfo(2).bytSetFlg = gBitValue(.udtGwsFileInfo(2).bytSetFlg, 2)
                    '    .udtGwsFileInfo(3).bytSetFlg = gBitValue(.udtGwsFileInfo(3).bytSetFlg, 2)
                    'End If

                    'If gBitCheck(mbyt22kFileArrayT701(29 + i * 32 + intRecSize), 3) = True Then
                    '    .udtGwsFileInfo(0).bytSetFlg = gBitValue(.udtGwsFileInfo(0).bytSetFlg, 3)
                    '    .udtGwsFileInfo(1).bytSetFlg = gBitValue(.udtGwsFileInfo(1).bytSetFlg, 3)
                    'End If

                    'If gBitCheck(mbyt22kFileArrayT701(29 + i * 32 + intRecSize), 4) = True Then
                    '    .udtGwsFileInfo(0).bytSetFlg = gBitValue(.udtGwsFileInfo(0).bytSetFlg, 4)
                    '    .udtGwsFileInfo(1).bytSetFlg = gBitValue(.udtGwsFileInfo(1).bytSetFlg, 4)
                    'End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : OPSオプション設定
    '--------------------------------------------------------------------
    Private Sub msetStructureT08()

        Try
            Dim intGroup(gCstChannelGroupMax - 1) As Integer
            Dim intColor(gCstChannelGroupMax - 1) As Integer
            Dim j As Integer = 0
            Dim intGrpNo As Integer


            'With gudt.SetChGroupSetM.udtGroup

            ''グループ表示位置
            For i As Integer = 0 To gCstChannelGroupMax - 1
                intGroup(i) = &HFFFF
                gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).shtDisplayPosition = &HFFFF
                gudt.SetChGroupSetC.udtGroup.udtGroupInfo(i).shtDisplayPosition = &HFFFF
            Next

            For i As Integer = 0 To gCstChannelGroupMax - 1

                intGrpNo = mbyt22kFileArrayT08(3136 + i)
                If intGrpNo <> 255 Then

                    If intGrpNo > 0 Then

                        intGroup(intGrpNo - 1) = i + 1

                        If mbyt22kFileArrayT08(3272 + i) = 1 Then               '青(MACHINERY)

                            'T.Ueki 指定色変更
                            intColor(intGrpNo - 1) = 1
                            'intColor(mbyt22kFileArrayT08(3136 + i) - 1) = 5
                        ElseIf mbyt22kFileArrayT08(3272 + i) = 20 Then          '緑(CARGO)
                            intColor(intGrpNo - 1) = 2
                        Else
                            intColor(intGrpNo - 1) = 0
                        End If

                    End If

                End If

                'gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).shtDisplayPosition = intGroup(i)   '' 2015.01.20
                'gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).shtColor = intColor(i)

            Next

            '' ポジション変更に対応   2015.02.04
            For i As Integer = 0 To gCstChannelGroupMax - 1
                gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).shtDisplayPosition = intGroup(i)   '' 2015.01.20
                gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).shtColor = intColor(i)
            Next

            'T.Ueki 表示仕様変更
            '' MachineryとCARGOに分ける
            'For i As Integer = 0 To gCstChannelGroupMax - 1

            '    If intColor(i) = 2 Then         '' CARGO
            '        gudt.SetChGroupSetC.udtGroup.udtGroupInfo(j).shtDisplayPosition = j + 1     'CARGOに先頭よりデータセット
            '        gudt.SetChGroupSetC.udtGroup.udtGroupInfo(j).shtColor = intColor(i)

            '        '' MACHINERYに設定されているグループ名称をCARGOに移行
            '        gudt.SetChGroupSetC.udtGroup.udtGroupInfo(j).strName1 = gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).strName1
            '        gudt.SetChGroupSetC.udtGroup.udtGroupInfo(j).strName2 = gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).strName2
            '        gudt.SetChGroupSetC.udtGroup.udtGroupInfo(j).strName3 = gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).strName3

            '        gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).strName1 = ""
            '        gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).strName2 = ""
            '        gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).strName3 = ""

            '        '' CARGOパート表示位置カウント
            '        j += 1

            '    Else                            '' MACHINERY
            '        gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).shtDisplayPosition = intGroup(i)
            '        gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).shtColor = intColor(i)
            '    End If
            'Next

            'End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "チャンネル設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : チャンネル情報
    '--------------------------------------------------------------------
    Private Sub msetStructureMpt(ByRef udtSet As gTypSetChInfo, _
                                 ByRef udtSetSlot As gTypSetChDisp, _
                                 ByRef udtSetGroup As gTypSetChGroupSet)

        Dim Flag1 As Boolean = False, Flag2 As Boolean = False
        Dim i As Integer = 0, idx As Integer, intCnt As Integer = 0, cnt As Integer = 0
        Dim idxBK As Integer
        Dim intDispPos As Integer = 1, intGroupNoBK As Integer = 0
        Dim intValue As Integer, p As Integer = 0, p2 As Integer = 0, p3 As Integer = 0
        Dim intPinNo As Integer
        Dim lngValue As Long
        Dim strValue As String, strValue2 As String
        Dim bytArray() As Byte
        Dim strINS As String = ""
        Dim intHH As Integer, intH As Integer, intL As Integer, intLL As Integer
        Dim strAnalogAlarmStatus As String
        Dim strAnalogAlarmStatusH As String
        Dim strAnalogAlarmStatusL As String
        Dim COMM_Flag As Boolean = False
        Dim WORK_Flag As Boolean = False
        Dim intValue2 As Integer
        Dim intMotorCnt As Integer

        Try
            mstrAstNorCH = ""

            ''ヘッダー部(Ship No/Draw No/Comment)
            idx = 0
            idxBK = 0
            Do Until Flag1

                If mbyt22kFileArrayMpt(idx) = CInt("&H0D") And mbyt22kFileArrayMpt(idx + 1) = CInt("&H0A") Then

                    If intCnt = 0 Then
                        ''Ship No (1行目:40文字)
                        ReDim bytArray(idx - idxBK + 1)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idxBK + j)
                        Next
                        strValue = mByte2String(UBound(bytArray) + 1, bytArray)
                        udtSetGroup.udtGroup.strShipNo = strValue

                        idxBK = idx + 2

                    ElseIf intCnt = 1 Then
                        ''Draw No (2行目:8文字)
                        ReDim bytArray(idx - idxBK + 1)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idxBK + j)
                        Next
                        strValue = mByte2String(UBound(bytArray) + 1, bytArray)
                        udtSetGroup.udtGroup.strDrawNo = strValue

                        idxBK = idx + 2

                    ElseIf intCnt = 2 Then
                        ''Comment (3行目:32文字)
                        ReDim bytArray(idx - idxBK + 1)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idxBK + j)
                        Next
                        strValue = mByte2String(UBound(bytArray) + 1, bytArray)
                        udtSetGroup.udtGroup.strComment = strValue

                    End If

                    intCnt += 1
                    idx += 2
                Else
                    idx += 1
                End If

                If intCnt = 3 Then Flag1 = True
                If idx > UBound(mbyt22kFileArrayMpt) Then Flag1 = True

            Loop

            ''図番、船番、コメントは区別はパート区別なし　ver.1.4.0 2011.09.21
            ''Cargo側にMachineryの情報をコピー
            gudt.SetChGroupSetC.udtGroup.strShipNo = udtSetGroup.udtGroup.strShipNo
            gudt.SetChGroupSetC.udtGroup.strDrawNo = udtSetGroup.udtGroup.strDrawNo
            gudt.SetChGroupSetC.udtGroup.strComment = udtSetGroup.udtGroup.strComment

            Flag1 = False
            intCnt = 0

            Do Until Flag1

                With udtSet.udtChannel(i)

                    ''チャンネルデータをカウントアップ
                    intCnt += 1

                    ''通信CHフラグクリア
                    COMM_Flag = False

                    ''ワークCHフラグクリア
                    WORK_Flag = False

                    ''Dmy
                    .udtChCommon.shtFlag1 = gBitSet(.udtChCommon.shtFlag1, 0, IIf(mbyt22kFileArrayMpt(idx) = 49, True, False))

                    ''Sys No
                    ReDim bytArray(0)
                    bytArray(0) = mbyt22kFileArrayMpt(idx + 1)
                    .udtChCommon.shtSysno = CCInt(mByte2String(1, bytArray))

                    ''CH No
                    ReDim bytArray(3)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayMpt(idx + 2 + j)
                    Next j
                    .udtChCommon.shtChno = CCShort(mByte2String(4, bytArray))

                    If .udtChCommon.shtChno = 2101 Then
                        Dim debugA As Integer = 0
                    End If

                    ''MPTファイルにCHIDはない　ver.1.4.0 2011.09.29
                    ''CH IDはコンパイル時にセット
                    ''CH ID
                    .udtChCommon.shtChid = 0

                    ''Item Name
                    ReDim bytArray(29)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayMpt(idx + 6 + j)
                    Next j
                    .udtChCommon.strChitem = mByte2String(30, bytArray)

                    ''Remarks
                    ReDim bytArray(15)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayMpt(idx + 165 + j)
                    Next j
                    .udtChCommon.strRemark = mByte2String(16, bytArray)

                    ''Group No
                    .udtChCommon.shtGroupNo = intCnt \ 99
                    If intCnt Mod 99 > 0 Then .udtChCommon.shtGroupNo += 1

                    ''行番号（グループ内表示位置）
                    If intGroupNoBK <> .udtChCommon.shtGroupNo Then intDispPos = 1
                    intGroupNoBK = .udtChCommon.shtGroupNo

                    '表示位置
                    .udtChCommon.shtDispPos = intDispPos

                    ''SIO
                    ReDim bytArray(2)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayMpt(idx + 157 + j)
                    Next j
                    .udtChCommon.shtOutPort = CCInt(mByte2String(3, bytArray))

                    ''GWS
                    ReDim bytArray(3)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayMpt(idx + 160 + j)
                    Next j
                    .udtChCommon.shtGwsPort = CCInt(mByte2String(4, bytArray))

                    ''SC
                    .udtChCommon.shtFlag1 = gBitSet(.udtChCommon.shtFlag1, 1, IIf(mbyt22kFileArrayMpt(idx + 130) = 111, True, False))   '111='o'

                    ''RL
                    .udtChCommon.shtFlag2 = gBitSet(.udtChCommon.shtFlag2, 0, IIf(mbyt22kFileArrayMpt(idx + 126) = 111, True, False))   '111='o'

                    ''AC
                    .udtChCommon.shtFlag2 = gBitSet(.udtChCommon.shtFlag2, 3, IIf(mbyt22kFileArrayMpt(idx + 131) = 111, True, False))   '111='o'

                    ''EP
                    .udtChCommon.shtFlag2 = gBitSet(.udtChCommon.shtFlag2, 2, IIf(mbyt22kFileArrayMpt(idx + 129) = 111, True, False))   '111='o'

                    ''Prt1
                    .udtChCommon.shtFlag2 = gBitSet(.udtChCommon.shtFlag2, 4, IIf(mbyt22kFileArrayMpt(idx + 132) = 111, True, False))   '111='o'

                    ''Prt2
                    .udtChCommon.shtFlag2 = gBitSet(.udtChCommon.shtFlag2, 5, IIf(mbyt22kFileArrayMpt(idx + 133) = 111, True, False))   '111='o'


                    ''CH Type <-- INS
                    ReDim bytArray(2)
                    bytArray(0) = mbyt22kFileArrayMpt(idx + 71)
                    bytArray(1) = mbyt22kFileArrayMpt(idx + 72)
                    bytArray(2) = mbyt22kFileArrayMpt(idx + 73)

                    strINS = Trim(mByte2String(3, bytArray))  ''入力信号 INS

                    Select Case strINS

                        Case "TR", "KL", "KJ", "PT", "AI", "BI", "MT", "RP", "DV"  '' "TX3"
                            ''アナログ "AI", "TR", "PT", "DV", "MT", "RP"
                            .udtChCommon.shtChType = gCstCodeChTypeAnalog

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusAnalog)
                            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)

                            .udtChCommon.shtPinNo = 1   ''計測点数(デフォルト設定)     2014.04.24
                        Case "NC", "NO"
                            ''デジタル "NC", "NO"
                            .udtChCommon.shtChType = gCstCodeChTypeDigital

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusDigital)

                            'Data Type
                            If strINS = "NC" Then
                                .udtChCommon.shtData = gCstCodeChDataTypeDigitalNC
                            ElseIf strINS = "NO" Then
                                .udtChCommon.shtData = gCstCodeChDataTypeDigitalNO
                            End If

                            .udtChCommon.shtPinNo = 1   ''計測点数(デフォルト設定)     2014.04.24
                        Case "M1", "M2", "MO"
                            ''モーター "MO", "M1"
                            .udtChCommon.shtChType = gCstCodeChTypeMotor

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)

                            .MotorFuNo = gCstCodeChNotSetFuNo
                            .MotorPortNo = gCstCodeChNotSetFuPort
                            .MotorPin = gCstCodeChNotSetFuPin

                            .MotorAlarmDelay = gCstCodeChMotorDelayTimerNothing
                            .MotorAlarmExtGroup = gCstCodeChMotorExtGroupNothing
                            .MotorAlarmGroupRepose1 = gCstCodeChMotorGroupRepose1Nothing
                            .MotorAlarmGroupRepose2 = gCstCodeChMotorGroupRepose2Nothing

                        Case "PU", "P1", "P2", "PUD", "P1D", "P2D", "RH", "R2", "RHD", "R2D", "RHL", "R2L"
                            ''パルス
                            .udtChCommon.shtChType = gCstCodeChTypePulse

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusPulse)
                            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)

                            'Data Type
                            Select Case strINS
                                Case "PU" : .udtChCommon.shtData = gCstCodeChDataTypePulseTotal1_1
                                Case "P1" : .udtChCommon.shtData = gCstCodeChDataTypePulseTotal1_10
                                Case "P2" : .udtChCommon.shtData = gCstCodeChDataTypePulseTotal1_100
                                Case "PUD" : .udtChCommon.shtData = gCstCodeChDataTypePulseDay1_1
                                Case "P1D" : .udtChCommon.shtData = gCstCodeChDataTypePulseDay1_10
                                Case "P2D" : .udtChCommon.shtData = gCstCodeChDataTypePulseDay1_100
                                Case "RH" : .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalHour
                                Case "R2" : .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalMin
                                Case "RHD" : .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayHour
                                Case "R2D" : .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayMin
                                Case "RHL" : .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapHour
                                Case "R2L" : .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapMin
                            End Select

                            .udtChCommon.shtPinNo = 1           ''計測点数(デフォルト設定)     2014.04.24
                            .udtChCommon.shtM_ReposeSet = 1     ''カウンタCHはM.REP設定可能  　2014.04.24
                    End Select


                    ''Status
                    strAnalogAlarmStatus = ""
                    strAnalogAlarmStatusH = ""
                    strAnalogAlarmStatusL = ""
                    ReDim bytArray(11)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayMpt(idx + 38 + j)
                    Next j
                    strValue = gGetString(mByte2String(12, bytArray))

                    'アナログCHのｽﾃｰﾀｽのみ特殊変換
                    If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then
                        If strValue = "NOR/*" Then
                            'NOR/*はそのまま
                        Else
                            If (strValue.IndexOf("*/") > -1) Or (strValue.IndexOf("/*") > -1) Then
                                mstrAstNorCH = mstrAstNorCH & .udtChCommon.shtChno & vbCrLf
                            End If
                            'mstrAstNorCH
                            '「*/」と「/*」は「NOR/」「/NOR」へ変える
                            strValue = strValue.Replace("*/", "NOR/")
                            strValue = strValue.Replace("/*", "/NOR")
                        End If
                    End If



                    If strValue <> "" Then
                        If strValue.Substring(0, 1) = "#" Then
                            strValue = strValue.Substring(1)
                            ''仮設定対応　ver.1.4.0 2011.09.29
                            .DummyCommonStatusName = True
                        End If
                    End If

                    intValue = cmbStatus.FindStringExact(strValue.Trim)     '' 検索時スペース削除    2013.07.23  K.Fujimoto
                    If intValue >= 0 Then
                        cmbStatus.SelectedIndex = intValue
                        .udtChCommon.shtStatus = cmbStatus.SelectedValue
                        .udtChCommon.strStatus = ""

                    Else
                        .udtChCommon.shtStatus = gCstCodeChManualInputStatus    ''手入力

                        If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then
                            'アナログステータスの分割処理追加   2015.03.11
                            'strAnalogAlarmStatus = mByte2String(12, bytArray)
                            strValue = mByte2String(12, bytArray)

                            p = strValue.IndexOf("NOR/")
                            If p = 0 Then
                                strAnalogAlarmStatus = strValue.Substring(p + 4).Trim
                            Else
                                strAnalogAlarmStatusL = strValue.Substring(0, p - 1)
                                strAnalogAlarmStatusH = strValue.Substring(p + 4).Trim
                            End If

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeDigital Then
                            ''デジタルは8byte+8byteに分ける
                            strValue = mByte2String(12, bytArray)

                            '' Ver1.9.6 2016.02.03  ｽﾃｰﾀｽ取得処理変更
                            Dim strStatus1 As String = ""
                            Dim strStatus2 As String = ""

                            GetStatusString(strValue, strStatus1, strStatus2)

                            .udtChCommon.strStatus = strStatus1.PadRight(8) & strStatus2.PadRight(8)

                            ''p = strValue.IndexOf("/")
                            ''If p >= 0 Then
                            ''    .udtChCommon.strStatus = strValue.Substring(0, p).PadRight(8) & strValue.Substring(p + 1).PadRight(8)
                            ''Else
                            ''    .udtChCommon.strStatus = strValue
                            ''End If
                        Else
                            .udtChCommon.strStatus = mByte2String(12, bytArray)
                        End If
                    End If

                    ''モーターCHの場合、ステータスからデータタイプを判別する 06-28
                    If strINS = "M1" Then
                        .udtChCommon.shtData = .udtChCommon.shtStatus - 32
                    ElseIf strINS = "M2" Then
                        .udtChCommon.shtData = .udtChCommon.shtStatus - 16
                    ElseIf strINS = "MO" Then
                        .udtChCommon.shtStatus = 20
                        .udtChCommon.shtData = gCstCodeChDataTypeMotorDevice
                    End If

                    If .udtChCommon.shtChType = gCstCodeChTypeMotor Then ''モーター 2014.04.24変更 K.Fujimoto

                        ''計測点個数 : モーターステータスにより設定 ----------------
                        Dim intKubun As Integer = 0
                        Dim strMotorStatus() As String = Nothing
                        Dim strwk() As String = Nothing

                        If .udtChCommon.shtData >= gCstCodeChDataTypeMotorManRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorManRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                            intKubun = 1
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorAbnorRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorAbnorRunK Then    'Ver2.0.0.2 モーター種別増加 JをKへ
                            intKubun = 2

                            'Ver2.0.0.2 モーター種別増加 START
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorRManRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorRManRunK Then
                            intKubun = 1
                        ElseIf .udtChCommon.shtData >= gCstCodeChDataTypeMotorRAbnorRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorRAbnorRunK Then
                            intKubun = 2
                            'Ver2.0.0.2 モーター種別増加 END

                        Else
                            intKubun = 0
                        End If

                        If intKubun = 0 Then
                            .udtChCommon.shtPinNo = 1
                            intMotorCnt = 1
                        Else
                            Call GetStatusMotor(strMotorStatus, intKubun) ''モーターチャンネルのステータス情報を獲得する
                            strwk = strMotorStatus(cmbStatus.SelectedIndex).ToString.Split("_") ''「_」区切りの文字列取得

                            intValue = 0
                            For ii As Integer = LBound(strwk) To UBound(strwk)
                                If strwk(ii) <> "" Then
                                    intValue = ii + 1
                                End If
                            Next
                            .udtChCommon.shtPinNo = intValue
                            intMotorCnt = intValue
                        End If
                    End If


                    ''Filter Coef
                    If .udtChCommon.shtChType = gCstCodeChTypeDigital Then
                        .DigitalDiFilter = 12   '' フィルタ定数 "1" → "12" に変更    2014.04.17
                    ElseIf .udtChCommon.shtChType = gCstCodeChTypeMotor Then
                        .MotorDiFilter = 12     '' フィルタ定数 "1" → "12" に変更    2014.04.17
                    ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then
                        '.ValveDiDoDiFilter = 1
                    ElseIf .udtChCommon.shtChType = gCstCodeChTypePulse Then
                        .PulseDiFilter = 2      '' フィルタ定数 "1" → "2" に変更     2014.04.17
                    End If

                    ''S -----------------------------------------
                    ReDim bytArray(0)
                    bytArray(0) = mbyt22kFileArrayMpt(idx + 116)    ''R or D or W
                    strValue = gGetString(mByte2String(1, bytArray))
                    If strValue = "R" Then
                        Select Case .udtChCommon.shtChType
                            Case gCstCodeChTypeAnalog       ''アナログ
                                .udtChCommon.shtData = gCstCodeChDataTypeAnalogModbus

                            Case gCstCodeChTypeDigital      ''デジタル
                                If .udtChCommon.shtData = gCstCodeChDataTypeDigitalNC Then
                                    .udtChCommon.shtData = gCstCodeChDataTypeDigitalModbusNC
                                ElseIf .udtChCommon.shtData = gCstCodeChDataTypeDigitalNO Then
                                    .udtChCommon.shtData = gCstCodeChDataTypeDigitalModbusNO
                                End If

                            Case gCstCodeChTypeMotor        ''モーター
                                .udtChCommon.shtChType = gCstCodeChTypeDigital
                                .udtChCommon.shtData = gCstCodeChDataTypeDigitalModbusNO
                                .udtChCommon.shtStatus = &H14
                                .udtChCommon.strStatus = ""

                            Case gCstCodeChTypePulse        ''パルス、運転積算
                                .udtChCommon.shtData = gCstCodeChDataTypePulseExtDev
                        End Select

                        COMM_Flag = True

                    ElseIf strValue = "W" Then   ''WORK
                        .udtChCommon.shtFlag1 = gBitSet(.udtChCommon.shtFlag1, 2, True)
                        WORK_Flag = True
                    End If

                    ''FU Address
                    Dim intDmySet As Integer = 0
                    ReDim bytArray(0)
                    bytArray(0) = mbyt22kFileArrayMpt(idx + 119)    ''A ～ T, #
                    strValue = mByte2String(1, bytArray)

                    If strValue = "#" Then
                        bytArray(0) = mbyt22kFileArrayMpt(idx + 119 + 1)    ''A ～ T
                        strValue = mByte2String(1, bytArray)
                        intDmySet = 1
                        ''仮設定対応　ver.1.4.0 2011.09.29
                        .DummyCommonFuAddress = True
                    End If

                    .udtChCommon.shtFuno = gGetFuNo(strValue)
                    '------------------------------------------

                    ReDim bytArray(4)
                    bytArray(0) = mbyt22kFileArrayMpt(idx + 121 + intDmySet)
                    bytArray(1) = mbyt22kFileArrayMpt(idx + 122 + intDmySet)
                    bytArray(2) = mbyt22kFileArrayMpt(idx + 123 + intDmySet)
                    bytArray(3) = mbyt22kFileArrayMpt(idx + 124 + intDmySet)
                    bytArray(4) = mbyt22kFileArrayMpt(idx + 125 + intDmySet)
                    strValue2 = mByte2String(5, bytArray)

                    intValue = CCInt(strValue2)


                    ''FU Address
                    If COMM_Flag = True Then                                '' 通信CH

                        intValue2 = 0
                        For j As Integer = 0 To 8
                            If mbyt22kFileArraySIO(j * 256 + 0) = 1 Then    ''通信設定有り
                                For k As Integer = 0 To 7
                                    ''アドレス
                                    If mbyt22kFileArraySIO(j * 256 + 20 + k) = (.udtChCommon.shtFuno + &H30) Then
                                        intValue2 = j + 1                   ''ポート番号取得
                                        Exit For
                                    End If
                                Next
                            End If

                            If intValue2 <> 0 Then
                                Exit For
                            End If

                        Next

                        .udtChCommon.shtFuno = 0                ''外部機器は常に0
                        .udtChCommon.shtPortno = intValue2      ''シリアルポート番号
                        .udtChCommon.shtPin = intValue          ''ID

                    ElseIf WORK_Flag = True Then
                        ''ワークCHの場合、アドレス設定不要（22Kの場合はワークCHでも設定がある)    2015.01.19
                        .udtChCommon.shtFuno = gCstCodeChNotSetFuNo
                        .udtChCommon.shtPortno = gCstCodeChNotSetFuPort
                        .udtChCommon.shtPin = gCstCodeChNotSetFuPin

                    ElseIf strValue = "Z" Then
                        .udtChCommon.shtPortno = gCstCodeChNotSetFuPort
                        .udtChCommon.shtPin = gCstCodeChNotSetFuPin

                        .udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus    '' 機器状態(システムCH)設定

                        Call mSetSystemDevice(udtSet.udtChannel(i), intValue)

                    ElseIf strValue = "T" And mCanubusFunc = 1 Then     '' T and CANBUS設定有り

                        ' CANBUSの場合、アドレス設定を変更　T(20) → ID    2013.07.25 K.Fujimoto
                        .udtChCommon.shtFuno = 0
                        .udtChCommon.shtPortno = 0
                        .udtChCommon.shtPin = intValue

                        If .udtChCommon.shtChType = gCstCodeChTypeDigital Then      ''デジタル
                            If .udtChCommon.shtData = gCstCodeChDataTypeDigitalNC Then
                                .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacomNC
                            ElseIf .udtChCommon.shtData = gCstCodeChDataTypeDigitalNO Then
                                .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacomNO
                            End If

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeMotor Then    ''モーター
                            .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeAnalog Then   ''アナログ
                            .udtChCommon.shtData = gCstCodeChDataTypeAnalogJacom

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypePulse Then    ''パルス・運転積算
                            .udtChCommon.shtPortno = 1
                            ' CANBUSの場合、アドレス設定を変更　T(20) → WORK  2013.07.25 K.Fujimoto
                            .udtChCommon.shtFuno = gCstCodeChNotSetFuNo
                            .udtChCommon.shtPortno = gCstCodeChNotSetFuPort
                            .udtChCommon.shtPin = gCstCodeChNotSetFuPin
                            .udtChCommon.shtFlag1 = gBitSet(.udtChCommon.shtFlag1, 2, True)
                        End If


                    ElseIf .udtChCommon.shtFuno = gCstCodeChNotSetFuNo Then
                        .udtChCommon.shtPortno = gCstCodeChNotSetFuPort
                        .udtChCommon.shtPin = gCstCodeChNotSetFuPin
                    Else

                        cnt = 0
                        For j As Integer = 0 To 4   'TB1 ～ TB5 

                            If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then
                                ''アナログ
                                If mSlotInfo(.udtChCommon.shtFuno, j, 1) = 1 Then

                                    cnt += 1
                                    If mSlotInfo(.udtChCommon.shtFuno, j, 0) * cnt >= intValue Then

                                        .udtChCommon.shtPortno = j + 1

                                        If cnt = 1 Then
                                            .udtChCommon.shtPin = intValue
                                        Else
                                            .udtChCommon.shtPin = intValue - mSlotInfo(.udtChCommon.shtFuno, j, 0) * (cnt - 1)
                                        End If
                                        Exit For

                                    End If

                                End If

                            Else
                                ''デジタル
                                If mSlotInfo(.udtChCommon.shtFuno, j, 1) = 2 Then

                                    cnt += 1
                                    If mSlotInfo(.udtChCommon.shtFuno, j, 0) * cnt >= intValue Then

                                        .udtChCommon.shtPortno = j + 1

                                        If cnt = 1 Then
                                            .udtChCommon.shtPin = intValue
                                        Else
                                            .udtChCommon.shtPin = intValue - mSlotInfo(.udtChCommon.shtFuno, j, 0) * (cnt - 1)
                                        End If
                                        Exit For

                                    End If

                                End If

                            End If

                        Next

                        ''端子台名称情報

                        ''スロット種別
                        If .udtChCommon.shtPin = gCstCodeChNotSetFuPin Then
                            .udtChCommon.shtFuno = gCstCodeChNotSetFuNo
                            .udtChCommon.shtPortno = gCstCodeChNotSetFuPort
                        Else

                            cnt = 0
                            intPinNo = .udtChCommon.shtPin - 1
                            '' 3線式のみ3行表示　ver1.4.0
                            If gudt.SetFu.udtFu(.udtChCommon.shtFuno).udtSlotInfo(.udtChCommon.shtPortno - 1).shtType = gCstCodeFuSlotTypeAI_3 Then
                                ''3行毎
                                intPinNo = intPinNo * 3
                                cnt = 3
                            ElseIf .udtChCommon.shtChType = gCstCodeChTypeMotor Then ''モーター 2014.04.24変更 K.Fujimoto
                                If intMotorCnt > 1 Then
                                    ''端子情報に設定(MOTOR点数分)
                                    For j As Integer = 1 To intMotorCnt - 1
                                        udtSetSlot.udtChDisp(.udtChCommon.shtFuno).udtSlotInfo(.udtChCommon.shtPortno - 1).udtPinInfo(.udtChCommon.shtPin - 1 + j).shtChid = .udtChCommon.shtChno
                                    Next j
                                End If

                                cnt = intMotorCnt
                            Else
                                cnt = 1
                            End If

                            For ii As Integer = 0 To cnt - 1

                                With udtSetSlot.udtChDisp(.udtChCommon.shtFuno). _
                                 udtSlotInfo(.udtChCommon.shtPortno - 1). _
                                 udtPinInfo(intPinNo + ii)

                                    ReDim bytArray(15)

                                    ''WireMark
                                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                                        bytArray(j) = mbyt22kFileArrayMpt(idx + 181 + ii * 50 + j)
                                    Next j
                                    'Ver2.0.6.5 2個CHがある場合に消えてしまうため、既に値が入ってたら書かない
                                    If NZfS(.strWireMark) = "" Then
                                        .strWireMark = mByte2String(16, bytArray)
                                    End If

                                    ReDim bytArray(13)
                                    ''WireMarkClass
                                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                                        bytArray(j) = mbyt22kFileArrayMpt(idx + 197 + ii * 50 + j)
                                    Next j
                                    'Ver2.0.6.5 2個CHがある場合に消えてしまうため、既に値が入ってたら書かない
                                    If NZfS(.strWireMarkClass) = "" Then
                                        .strWireMarkClass = mByte2String(14, bytArray)
                                    End If
                                    ReDim bytArray(15)
                                    ''Dest
                                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                                        bytArray(j) = mbyt22kFileArrayMpt(idx + 215 + ii * 50 + j)
                                    Next j
                                    'Ver2.0.6.5 2個CHがある場合に消えてしまうため、既に値が入ってたら書かない
                                    If NZfS(.strDest) = "" Then
                                        .strDest = mByte2String(16, bytArray)
                                    End If

                                    ReDim bytArray(1)

                                    ''CoreNoIn
                                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                                        bytArray(j) = mbyt22kFileArrayMpt(idx + 211 + ii * 50 + j)
                                    Next j
                                    .strCoreNoIn = mByte2String(2, bytArray)

                                    ''CoreNoCom
                                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                                        bytArray(j) = mbyt22kFileArrayMpt(idx + 213 + ii * 50 + j)
                                    Next j
                                    .strCoreNoCom = mByte2String(2, bytArray)

                                End With

                            Next ii

                            ''端子情報に設定
                            If udtSetSlot.udtChDisp(.udtChCommon.shtFuno).udtSlotInfo(.udtChCommon.shtPortno - 1).udtPinInfo(.udtChCommon.shtPin - 1).shtChid = 0 Then
                                udtSetSlot.udtChDisp(.udtChCommon.shtFuno).udtSlotInfo(.udtChCommon.shtPortno - 1).udtPinInfo(.udtChCommon.shtPin - 1).shtChid = .udtChCommon.shtChno
                            End If

                        End If



                    End If

                    ''アナログCH --------------------------------------------------------------
                    .udtChCommon.shtSignal = gCstCodeChAnalogSignalNothing      '' 入力信号
                    If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then

                        ''Data Type
                        If .udtChCommon.shtData = gCstCodeChDataTypeAnalogJacom Then    ''JACOMは除外

                        ElseIf strINS = "KL" Or strINS = "KJ" Then
                            ''K
                            .udtChCommon.shtData = gCstCodeChDataTypeAnalogK

                        ElseIf strINS = "PT" Then
                            ''4_20mA
                            .udtChCommon.shtData = gCstCodeChDataTypeAnalog4_20mA
                            .udtChCommon.shtSignal = gCstCodeChAnalogSignalPT

                        ElseIf strINS = "TR" Then
                            ''Pt100
                            If .udtChCommon.shtFuno <> gCstCodeChNotSetFuNo Then
                                If mSlotInfo(.udtChCommon.shtFuno, .udtChCommon.shtPortno - 1, 2) = gCstCodeFuSlotTypeAI_2 Then
                                    .udtChCommon.shtData = gCstCodeChDataTypeAnalog2Pt
                                ElseIf mSlotInfo(.udtChCommon.shtFuno, .udtChCommon.shtPortno - 1, 2) = gCstCodeFuSlotTypeAI_3 Then
                                    .udtChCommon.shtData = gCstCodeChDataTypeAnalog3Pt
                                End If
                            End If

                        ElseIf strINS = "AI" Or strINS = "TX3" Or strINS = "BI" Then    '' 2015.01.19 BI追加
                            If .udtChCommon.shtFuno <> gCstCodeChNotSetFuNo Then
                                ''1_5V or 4_20mA
                                If mSlotInfo(.udtChCommon.shtFuno, .udtChCommon.shtPortno - 1, 2) = gCstCodeFuSlotTypeAI_1_5 Then
                                    ''1_5V
                                    .udtChCommon.shtData = gCstCodeChDataTypeAnalog1_5v
                                ElseIf mSlotInfo(.udtChCommon.shtFuno, .udtChCommon.shtPortno - 1, 2) = gCstCodeFuSlotTypeAI_4_20 Then
                                    ''4_20mA
                                    .udtChCommon.shtData = gCstCodeChDataTypeAnalog4_20mA
                                    .udtChCommon.shtSignal = gCstCodeChAnalogSignalAI
                                End If
                            Else                                                        '' デフォルトを4-20mAとする  2014.04.24
                                ''4_20mA
                                .udtChCommon.shtData = gCstCodeChDataTypeAnalog4_20mA
                                .udtChCommon.shtSignal = gCstCodeChAnalogSignalAI
                            End If

                        ElseIf strINS = "MT" Then
                            ''排ガス偏差平均値
                            .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhAve

                        ElseIf strINS = "RP" Then
                            ''排ガス偏差リポーズ
                            .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose

                        ElseIf strINS = "DV" Then
                            ''排ガス偏差
                            .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev
                            ''偏差のステータスを変換   2015.03.11
                            If .udtChCommon.shtStatus = 65 Then     '' NOR/HIGH
                                .udtChCommon.shtStatus = 67         '' LOW/NOR/HIGH
                            End If
                        End If

                        ''Unit
                        ReDim bytArray(5)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idx + 62 + j)
                        Next j
                        strValue = mByte2String(6, bytArray)

                        If strValue <> "" Then
                            If strValue.Substring(0, 1) = "#" Then
                                strValue = strValue.Substring(1)
                                ''仮設定対応　ver.1.4.0 2011.09.29
                                .DummyCommonUnitName = True
                            End If
                        End If

                        'Ver2.0.4.3 unitで大文字小文字区別
                        intValue = fnBackCmb(cmbUnit, strValue.Trim)
                        'intValue = cmbUnit.FindStringExact(strValue.Trim)   '' 検索時スペース削除    2013.07.23  K.Fujimoto
                        If intValue >= 0 Then
                            cmbUnit.SelectedIndex = intValue
                            .udtChCommon.shtUnit = cmbUnit.SelectedValue
                        Else
                            .udtChCommon.shtUnit = gCstCodeChManualInputUnit
                            .udtChCommon.strUnit = strValue
                        End If

                        ''Decimal Position
                        .AnalogDecimalPosition = get_dec_position(idx)

                        ''Range
                        ReDim bytArray(11)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idx + 50 + j)
                        Next j
                        strValue = gGetString(mByte2String(12, bytArray))

                        If strValue <> "" Then
                            If strValue.Substring(0, 1) = "#" Then
                                strValue = strValue.Substring(1)
                                ''仮設定対応　ver.1.4.0 2011.09.29
                                .DummyRangeScale = True
                            End If
                        End If

                        If strValue.Length >= 3 Then
                            If strValue.Substring(0, 2) = "+-" Then

                                strValue2 = strValue.Substring(2)
                                If .AnalogDecimalPosition > 0 Then
                                    lngValue = CCDouble(strValue2) * (10 ^ .AnalogDecimalPosition)
                                    If lngValue.ToString.Length > 9 Then        ''桁あふれ回避の為に9桁まで
                                        lngValue = CCLong(lngValue.ToString.Substring(lngValue.ToString.Length - 9))
                                    End If
                                    .AnalogRangeLow = -1 * lngValue
                                    .AnalogRangeHigh = lngValue
                                Else
                                    .AnalogRangeLow = CCInt("-" & strValue.Substring(2))
                                    .AnalogRangeHigh = CCInt(strValue.Substring(2))
                                End If

                            Else
                                p = strValue.IndexOf("/")
                                If p > 0 Then

                                    ''RangeLow
                                    strValue2 = strValue.Substring(0, p)
                                    If .AnalogDecimalPosition > 0 Then
                                        lngValue = CCDouble(strValue2) * (10 ^ .AnalogDecimalPosition)
                                        If lngValue.ToString.Length > 9 Then        ''桁あふれ回避の為に9桁まで
                                            lngValue = CCLong(lngValue.ToString.Substring(lngValue.ToString.Length - 9))
                                        End If
                                        .AnalogRangeLow = lngValue
                                    Else
                                        .AnalogRangeLow = CCInt(strValue2)
                                    End If

                                    ''RangeHigh
                                    strValue2 = strValue.Substring(p + 1)
                                    If .AnalogDecimalPosition > 0 Then
                                        lngValue = CCDouble(strValue2) * (10 ^ .AnalogDecimalPosition)
                                        If lngValue.ToString.Length > 9 Then        ''桁あふれ回避の為に9桁まで
                                            lngValue = CCLong(lngValue.ToString.Substring(lngValue.ToString.Length - 9))
                                        End If
                                        .AnalogRangeHigh = lngValue

                                    Else
                                        .AnalogRangeHigh = CCInt(strValue2)
                                    End If

                                End If
                            End If
                        End If

                        If .udtChCommon.shtData = gCstCodeChDataTypeAnalog2Pt Or _
                           .udtChCommon.shtData = gCstCodeChDataTypeAnalog3Pt Then

                            If .AnalogRangeLow = 0 Then

                                If .AnalogRangeHigh = 700 Then
                                    .AnalogRangeType = gCstCodeChRangeAnalogPt0_700
                                ElseIf .AnalogRangeHigh = 600 Then
                                    .AnalogRangeType = gCstCodeChRangeAnalogPt0_600
                                ElseIf .AnalogRangeHigh = 200 Then
                                    .AnalogRangeType = gCstCodeChRangeAnalogPt0_200
                                ElseIf .AnalogRangeHigh = 150 Then
                                    .AnalogRangeType = gCstCodeChRangeAnalogPt0_150
                                Else
                                    .AnalogRangeType = gCstCodeChRangeAnalogPt0_700
                                End If

                            ElseIf .AnalogRangeLow = -50 Then
                                .AnalogRangeType = gCstCodeChRangeAnalogPt50_50

                            ElseIf .AnalogRangeLow = -70 Then
                                .AnalogRangeType = gCstCodeChRangeAnalogPt70_80
                            End If

                        End If

                        intHH = 0 : intH = 0 : intL = 0 : intLL = 0

                        ''Value L
                        ReDim bytArray(5)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idx + 76 + j)
                        Next j
                        strValue = gGetString(mByte2String(6, bytArray))

                        If strValue <> "" Then
                            If strValue.Substring(0, 1) = "#" Then
                                strValue = strValue.Substring(1)
                                ''仮設定対応　ver.1.4.0 2011.09.29
                                .DummyValueL = True
                            End If
                        End If

                        If strValue <> "" And strValue <> "-" Then
                            .AnalogLoUse = 1 ''Use L
                        End If

                        'Ver2.0.7.H 桁数何桁でも小数点制御
                        'If strValue.Length >= 3 Then
                        If .AnalogDecimalPosition > 0 Then
                            lngValue = CCDouble(strValue) * (10 ^ .AnalogDecimalPosition)
                            If lngValue.ToString.Length > 9 Then        ''桁あふれ回避の為に9桁まで
                                lngValue = CCLong(lngValue.ToString.Substring(lngValue.ToString.Length - 9))
                            End If
                            .AnalogLoValue = lngValue
                        Else
                            .AnalogLoValue = CCInt(strValue)
                        End If
                        'Else
                        '.AnalogLoValue = CCInt(strValue)
                        'End If


                        ''Value H
                        ReDim bytArray(5)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idx + 82 + j)
                        Next j
                        strValue = gGetString(mByte2String(6, bytArray))

                        If strValue <> "" Then
                            If strValue.Substring(0, 1) = "#" Then
                                strValue = strValue.Substring(1)
                                ''仮設定対応　ver.1.4.0 2011.09.29
                                .DummyValueH = True
                            End If
                        End If

                        If strValue <> "" And strValue <> "-" Then
                            .AnalogHiUse = 1 ''Use H
                        End If

                        'Ver2.0.7.H 桁数何桁でも小数点制御
                        'If strValue.Length >= 3 Then
                        If .AnalogDecimalPosition > 0 Then
                            lngValue = CCDouble(strValue) * (10 ^ .AnalogDecimalPosition)
                            If lngValue.ToString.Length > 9 Then        ''桁あふれ回避の為に9桁まで
                                lngValue = CCLong(lngValue.ToString.Substring(lngValue.ToString.Length - 9))
                            End If
                            .AnalogHiValue = lngValue
                        Else
                            .AnalogHiValue = CCInt(strValue)
                        End If
                        'Else
                        '.AnalogHiValue = CCInt(strValue)
                        'End If

                        ''Value LL
                        ReDim bytArray(5)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idx + 88 + j)
                        Next j
                        strValue = gGetString(mByte2String(6, bytArray))

                        If strValue <> "" Then
                            If strValue.Substring(0, 1) = "#" Then
                                strValue = strValue.Substring(1)
                                ''仮設定対応　ver.1.4.0 2011.09.29
                                .DummyValueLL = True
                            End If
                        End If

                        If strValue <> "" And strValue <> "-" Then
                            .AnalogLoLoUse = 1 ''Use LL
                        End If

                        'Ver2.0.7.H 桁数何桁でも小数点制御
                        'If strValue.Length >= 3 Then
                        If .AnalogDecimalPosition > 0 Then
                            lngValue = CCDouble(strValue) * (10 ^ .AnalogDecimalPosition)
                            If lngValue.ToString.Length > 9 Then        ''桁あふれ回避の為に9桁まで
                                lngValue = CCLong(lngValue.ToString.Substring(lngValue.ToString.Length - 9))
                            End If
                            .AnalogLoLoValue = lngValue
                        Else
                            .AnalogLoLoValue = CCInt(strValue)
                        End If
                        'Else
                        '.AnalogLoLoValue = CCInt(strValue)
                        'End If

                        ''Value HH
                        ReDim bytArray(5)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idx + 94 + j)
                        Next j
                        strValue = gGetString(mByte2String(6, bytArray))

                        If strValue <> "" Then
                            If strValue.Substring(0, 1) = "#" Then
                                strValue = strValue.Substring(1)
                                ''仮設定対応　ver.1.4.0 2011.09.29
                                .DummyValueHH = True
                            End If
                        End If

                        If strValue <> "" And strValue <> "-" Then
                            .AnalogHiHiUse = 1 ''Use HH
                        End If

                        'Ver2.0.7.H 桁数何桁でも小数点制御
                        'If strValue.Length >= 3 Then
                        If .AnalogDecimalPosition > 0 Then
                            lngValue = CCDouble(strValue) * (10 ^ .AnalogDecimalPosition)
                            If lngValue.ToString.Length > 9 Then        ''桁あふれ回避の為に9桁まで
                                lngValue = CCLong(lngValue.ToString.Substring(lngValue.ToString.Length - 9))
                            End If
                            .AnalogHiHiValue = lngValue
                        Else
                            .AnalogHiHiValue = CCInt(strValue)
                        End If
                        'Else
                        '.AnalogHiHiValue = CCInt(strValue)
                        'End If

                        ''Normal Lo
                        ReDim bytArray(5)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idx + 100 + j)
                        Next j
                        strValue = gGetString(mByte2String(6, bytArray))

                        If strValue <> "" Then
                            If strValue.Substring(0, 1) = "#" Then
                                strValue = strValue.Substring(1)
                                ''仮設定対応　ver.1.4.0 2011.09.29
                                .DummyRangeNormalLo = True
                            End If
                        End If

                        If strValue = "" Then
                            ''設定無し
                            .AnalogNormalLow = gCstCodeChAlalogNormalRangeNothingLo : p = 0
                        Else
                            If strValue.Length >= 3 Then
                                If .AnalogDecimalPosition > 0 Then
                                    p = strValue.Substring(p + 1).Length     ''P2 <-- 小数点以下桁数
                                    lngValue = CCDouble(strValue) * (10 ^ .AnalogDecimalPosition)
                                    If lngValue.ToString.Length > 9 Then        ''桁あふれ回避の為に9桁まで
                                        lngValue = CCLong(lngValue.ToString.Substring(lngValue.ToString.Length - 9))
                                    End If
                                    .AnalogNormalLow = lngValue
                                Else
                                    .AnalogNormalLow = CCInt(strValue)
                                End If
                            Else
                                .AnalogNormalLow = CCInt(strValue)
                            End If
                        End If

                        ''Normal Hi
                        ReDim bytArray(5)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idx + 106 + j)
                        Next j
                        strValue = gGetString(mByte2String(6, bytArray))

                        If strValue <> "" Then
                            If strValue.Substring(0, 1) = "#" Then
                                strValue = strValue.Substring(1)
                                ''仮設定対応　ver.1.4.0 2011.09.29
                                .DummyRangeNormalHi = True
                            End If
                        End If

                        If strValue = "" Then
                            ''設定無し
                            .AnalogNormalHigh = gCstCodeChAlalogNormalRangeNothingHi
                        Else
                            If strValue.Length >= 3 Then
                                If .AnalogDecimalPosition > 0 Then
                                    p = strValue.Substring(p + 1).Length     ''P2 <-- 小数点以下桁数
                                    lngValue = CCDouble(strValue) * (10 ^ .AnalogDecimalPosition)
                                    If lngValue.ToString.Length > 9 Then        ''桁あふれ回避の為に9桁まで
                                        lngValue = CCLong(lngValue.ToString.Substring(lngValue.ToString.Length - 9))
                                    End If
                                    .AnalogNormalHigh = lngValue
                                Else
                                    .AnalogNormalHigh = CCInt(strValue)
                                End If
                            Else
                                .AnalogNormalHigh = CCInt(strValue)
                            End If
                        End If


                        ''Display1 <-- Strig
                        ReDim bytArray(0)
                        bytArray(0) = mbyt22kFileArrayMpt(idx + 156)
                        'Ver2.0.3.6 アナログのStringは全てゼロとする
                        '.AnalogString = CCInt(mByte2String(1, bytArray))
                        .AnalogString = 0


                        ''Display3 <-- Center Graph
                        ReDim bytArray(0)
                        bytArray(0) = mbyt22kFileArrayMpt(idx + 136)
                        '' 2013.11.29 修正
                        '.AnalogDisplay3 = gBitSet(.AnalogDisplay3, 0, IIf(CCInt(mByte2String(1, bytArray)) = 111, False, True))
                        .AnalogDisplay3 = gBitSet(.AnalogDisplay3, 0, IIf((mByte2String(1, bytArray) = "o"), True, False))

                        ''偏差CH バーグラフセンター表示をONにする ver1.4.0 2011.09.20
                        If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev Then
                            .AnalogDisplay3 = gBitSet(.AnalogDisplay3, 0, True)
                        End If
                    End If

                    ''パルス　-----------------------------------------------------------------
                    If .udtChCommon.shtChType = gCstCodeChTypePulse Then

                        ''Unit
                        ReDim bytArray(5)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idx + 62 + j)
                        Next j
                        'Ver2.0.4.3 unitで大文字小文字区別
                        intValue = fnBackCmb(cmbUnit, mByte2String(6, bytArray).Trim)
                        'intValue = cmbUnit.FindStringExact(mByte2String(6, bytArray).Trim)  '' 検索時スペース削除    2013.07.23  K.Fujimoto
                        If intValue >= 0 Then
                            cmbUnit.SelectedIndex = intValue
                            .udtChCommon.shtUnit = cmbUnit.SelectedValue
                        Else
                            .udtChCommon.shtUnit = gCstCodeChManualInputUnit
                            .udtChCommon.strUnit = mByte2String(6, bytArray)
                        End If

                        ''Range
                        ReDim bytArray(11)
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayMpt(idx + 50 + j)
                        Next j
                        strValue = gGetString(mByte2String(12, bytArray))

                        If strValue <> "" Then
                            If strValue.Substring(0, 1) = "#" Then
                                strValue = strValue.Substring(1)
                                ''仮設定対応　ver.1.4.0 2011.09.29
                                .DummyRangeScale = True
                            End If
                        End If

                        If strValue.Length >= 3 Then

                            p2 = strValue.IndexOf(".")  ''小数点有りかを判断
                            If p2 > 0 Then
                                p2 = strValue.Substring(p2 + 1).Length     ''P2 <-- 小数点以下桁数
                            Else
                                p2 = 0
                            End If

                            ''Decimal Position
                            If .udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Then
                                .PulseDecPoint = p2
                            Else
                                .RevoDecPoint = p2
                            End If

                        End If

                    End If

                    ''------------------------------------------------------------------------
                    ''Delay Timer
                    Dim intDelay As Integer = 0
                    ReDim bytArray(2)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayMpt(idx + 68 + j)
                    Next j
                    strValue = mByte2String(3, bytArray)

                    If strValue <> "" Then
                        If strValue.Substring(0, 1) = "#" Then
                            strValue = strValue.Substring(1)
                            ''仮設定対応　ver.1.4.0 2011.09.29
                            .DummyCommonDelay = True
                        End If

                        ''SEC/MIN
                        If strValue.IndexOf("m") > 0 Then
                            .udtChCommon.shtFlag1 = gBitSet(.udtChCommon.shtFlag1, 3, True)
                            strValue = strValue.Remove(strValue.IndexOf("m"))
                        ElseIf mbyt22kFileArrayMpt(idx + 135) = 111 Then
                            .udtChCommon.shtFlag1 = gBitSet(.udtChCommon.shtFlag1, 3, True)
                        End If


                    End If

                    ' ハイフン設定時の処理追加　2013.07.23 K.Fujimoto
                    If strValue.Substring(0, 1) = "-" Then
                        intDelay = 255
                    Else
                        intValue = CCInt(strValue)
                        If intValue < 0 Then intValue = 0
                        intDelay = intValue
                    End If

                    ''EXT.G
                    Dim intExtG As Integer = 0
                    ReDim bytArray(1)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayMpt(idx + 74 + j)
                    Next j
                    strValue = mByte2String(2, bytArray)

                    If strValue <> "" Then
                        If strValue.Substring(0, 1) = "#" Then
                            strValue = strValue.Substring(1)
                            ''仮設定対応　ver.1.4.0 2011.09.29
                            .DummyCommonExtGroup = True
                        End If
                    End If

                    ' ハイフン設定時の処理追加　2013.07.23 K.Fujimoto
                    If strValue.Substring(0, 1) = "-" Then
                        intExtG = 255
                    Else
                        intExtG = CCInt(strValue)
                    End If

                    ''G.Rep1
                    Dim intGrep1 As Integer = 0
                    ReDim bytArray(1)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayMpt(idx + 112 + j)
                    Next j
                    strValue = mByte2String(2, bytArray)

                    If strValue <> "" Then
                        If strValue.Substring(0, 1) = "#" Then
                            strValue = strValue.Substring(1)
                            ''仮設定対応　ver.1.4.0 2011.09.29
                            .DummyCommonGroupRepose1 = True
                        End If
                    End If

                    ' G.REP1 追加
                    intGrep1 = CCInt(strValue)

                    ''G.Rep2
                    Dim intGrep2 As Integer = 0
                    ReDim bytArray(1)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayMpt(idx + 114 + j)
                    Next j
                    strValue = mByte2String(2, bytArray)

                    If strValue <> "" Then
                        If strValue.Substring(0, 1) = "#" Then
                            strValue = strValue.Substring(1)
                            ''仮設定対応　ver.1.4.0 2011.09.29
                            .DummyCommonGroupRepose2 = True
                        End If
                    End If

                    intGrep2 = CCInt(strValue)

                    If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then

                        .AnalogHiHiDelay = gCstCodeChAnalogDelayTimerNothing
                        .AnalogHiDelay = gCstCodeChAnalogDelayTimerNothing
                        .AnalogLoLoDelay = gCstCodeChAnalogDelayTimerNothing
                        .AnalogLoDelay = gCstCodeChAnalogDelayTimerNothing
                        .AnalogSensorFailDelay = gCstCodeChAnalogDelayTimerNothing

                        .AnalogHiHiExtGroup = gCstCodeChAnalogExtGroupNothing
                        .AnalogHiExtGroup = gCstCodeChAnalogExtGroupNothing
                        .AnalogLoLoExtGroup = gCstCodeChAnalogExtGroupNothing
                        .AnalogLoExtGroup = gCstCodeChAnalogExtGroupNothing
                        .AnalogSensorFailExtGroup = gCstCodeChAnalogExtGroupNothing

                        .AnalogHiHiGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing
                        .AnalogHiGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing
                        .AnalogLoLoGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing
                        .AnalogLoGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing
                        .AnalogSensorFailGroupRepose1 = gCstCodeChAnalogGroupRepose1Nothing

                        .AnalogHiHiGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing
                        .AnalogHiGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing
                        .AnalogLoLoGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing
                        .AnalogLoGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing
                        .AnalogSensorFailGroupRepose2 = gCstCodeChAnalogGroupRepose2Nothing

                        If .AnalogHiHiUse = 1 Then
                            .AnalogHiHiDelay = intDelay
                            .AnalogHiHiExtGroup = IIf(intExtG = 255, gCstCodeChAnalogExtGroupNothing, intExtG)
                            .AnalogHiHiGroupRepose1 = IIf(intExtG = 255, gCstCodeChAnalogGroupRepose1Nothing, intGrep1)
                            .AnalogHiHiGroupRepose2 = IIf(intExtG = 255, gCstCodeChAnalogGroupRepose2Nothing, intGrep2)
                            'アナログステータスの分割処理追加   2015.03.11
                            If strAnalogAlarmStatusH <> "" Then
                                .AnalogHiHiStatusInput = strAnalogAlarmStatusH
                            Else
                                .AnalogHiHiStatusInput = strAnalogAlarmStatus
                            End If
                            .AnalogHiHiManualReposeSet = 1
                        End If

                        If .AnalogHiUse = 1 Then
                            .AnalogHiDelay = intDelay
                            .AnalogHiExtGroup = IIf(intExtG = 255, gCstCodeChAnalogExtGroupNothing, intExtG)
                            .AnalogHiGroupRepose1 = IIf(intExtG = 255, gCstCodeChAnalogGroupRepose1Nothing, intGrep1)
                            .AnalogHiGroupRepose2 = IIf(intExtG = 255, gCstCodeChAnalogGroupRepose2Nothing, intGrep2)
                            'アナログステータスの分割処理追加   2015.03.11
                            If strAnalogAlarmStatusH <> "" Then
                                .AnalogHiStatusInput = strAnalogAlarmStatusH
                            Else
                                .AnalogHiStatusInput = strAnalogAlarmStatus
                            End If
                            .AnalogHiManualReposeSet = 1
                        End If

                        If .AnalogLoUse = 1 Then
                            .AnalogLoDelay = intDelay
                            .AnalogLoExtGroup = IIf(intExtG = 255, gCstCodeChAnalogExtGroupNothing, intExtG)
                            .AnalogLoGroupRepose1 = IIf(intExtG = 255, gCstCodeChAnalogGroupRepose1Nothing, intGrep1)
                            .AnalogLoGroupRepose2 = IIf(intExtG = 255, gCstCodeChAnalogGroupRepose2Nothing, intGrep2)
                            'アナログステータスの分割処理追加   2015.03.11
                            If strAnalogAlarmStatusL <> "" Then
                                .AnalogLoStatusInput = strAnalogAlarmStatusL
                            Else
                                .AnalogLoStatusInput = strAnalogAlarmStatus
                            End If
                            .AnalogLoManualReposeSet = 1
                        End If

                        If .AnalogLoLoUse = 1 Then
                            .AnalogLoLoDelay = intDelay
                            .AnalogLoLoExtGroup = IIf(intExtG = 255, gCstCodeChAnalogExtGroupNothing, intExtG)
                            .AnalogLoLoGroupRepose1 = IIf(intExtG = 255, gCstCodeChAnalogGroupRepose1Nothing, intGrep1)
                            .AnalogLoLoGroupRepose2 = IIf(intExtG = 255, gCstCodeChAnalogGroupRepose2Nothing, intGrep2)
                            'アナログステータスの分割処理追加   2015.03.11
                            If strAnalogAlarmStatusL <> "" Then
                                .AnalogLoLoStatusInput = strAnalogAlarmStatusL
                            Else
                                .AnalogLoLoStatusInput = strAnalogAlarmStatus
                            End If
                            .AnalogLoLoManualReposeSet = 1
                        End If

                        If mbyt22kFileArrayMpt(idx + 127) = 111 Or mbyt22kFileArrayMpt(idx + 128) = 111 Then
                            .AnalogSensorFailUse = 1
                            '.AnalogSensorFailDelay = intDelay
                            'Ver2.0.3.6 センサフェイルディレイはゼロとする
                            '.AnalogSensorFailDelay = IIf(intDelay = 255, 0, intDelay)   '' センサのみのときはDLY設定がないので0とする　2015.03.11
                            .AnalogSensorFailDelay = 0

                            .AnalogSensorFailExtGroup = IIf(intExtG = 255, gCstCodeChAnalogExtGroupNothing, intExtG)
                            .AnalogSensorFailManualReposeSet = 1
                            '' センサー有り追加 2013.11.29
                            .AnalogDisplay3 = gBitSet(.AnalogDisplay3, 1, True)
                            .AnalogDisplay3 = gBitSet(.AnalogDisplay3, 2, True)
                        End If

                        ''flag2(Bit1) <-- アラームが有りでflag2のAC(Bit3)がOFFの場合のみONにする --------------------
                        If .AnalogHiHiUse = 1 Or .AnalogHiUse = 1 Or .AnalogLoUse = 1 Or .AnalogLoLoUse = 1 Or .AnalogSensorFailUse = 1 Then
                            If gBitCheck(.udtChCommon.shtFlag2, 3) = False Then ''AC
                                .udtChCommon.shtFlag2 = gBitSet(.udtChCommon.shtFlag2, 1, True)
                            End If
                        End If

                        ''仮設定対応(共通→アナログ)　ver.1.4.0 2011.09.29
                        If .DummyCommonDelay = True Then        ''DELAY
                            .DummyDelayHH = .DummyCommonDelay
                            .DummyDelayH = .DummyCommonDelay
                            .DummyDelayL = .DummyCommonDelay
                            .DummyDelayLL = .DummyCommonDelay
                            .DummyCommonDelay = False
                        End If

                        If .DummyCommonExtGroup = True Then     ''Ext.Group
                            .DummyExtGrHH = .DummyCommonExtGroup
                            .DummyExtGrH = .DummyCommonExtGroup
                            .DummyExtGrL = .DummyCommonExtGroup
                            .DummyExtGrLL = .DummyCommonExtGroup
                            .DummyCommonExtGroup = False
                        End If

                        If .DummyCommonGroupRepose1 = True Then ''Group Repose1
                            .DummyGRep1HH = .DummyCommonGroupRepose1
                            .DummyGRep1H = .DummyCommonGroupRepose1
                            .DummyGRep1L = .DummyCommonGroupRepose1
                            .DummyGRep1LL = .DummyCommonGroupRepose1
                            .DummyCommonGroupRepose1 = False
                        End If

                        If .DummyCommonGroupRepose2 = True Then ''Group Repose2
                            .DummyGRep2HH = .DummyCommonGroupRepose2
                            .DummyGRep2H = .DummyCommonGroupRepose2
                            .DummyGRep2L = .DummyCommonGroupRepose2
                            .DummyGRep2LL = .DummyCommonGroupRepose2
                            .DummyCommonGroupRepose2 = False
                        End If

                    Else
                        .udtChCommon.shtDelay = IIf(intDelay = 255, gCstCodeChCommonDelayTimerNothing, intDelay)
                        .udtChCommon.shtExtGroup = IIf(intExtG = 255, gCstCodeChCommonExtGroupNothing, intExtG)
                        .udtChCommon.shtGRepose1 = IIf(intExtG = 255, gCstCodeChCommonGroupRepose1Nothing, intGrep1)
                        .udtChCommon.shtGRepose2 = IIf(intExtG = 255, gCstCodeChCommonGroupRepose2Nothing, intGrep2)
                    End If

                    '' アラーム有無
                    If .udtChCommon.shtChType = gCstCodeChTypeDigital Then  ''デジタル
                        If .udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then
                            .SystemUse = IIf(intExtG = 255, 0, 1)

                            ''flag2(Bit1) <-- アラームが有りでflag2のAC(Bit3)がOFFの場合のみONにする ----
                            If .SystemUse = 1 Then
                                .udtChCommon.shtM_ReposeSet = 1
                                If gBitCheck(.udtChCommon.shtFlag2, 3) = False Then
                                    .udtChCommon.shtFlag2 = gBitSet(.udtChCommon.shtFlag2, 1, True)
                                End If
                            End If
                        Else
                            .DigitalUse = IIf(intExtG = 255, 0, 1)

                            ''flag2(Bit1) <-- アラームが有りでflag2のAC(Bit3)がOFFの場合のみONにする ----
                            If .DigitalUse = 1 Then
                                .udtChCommon.shtM_ReposeSet = 1
                                If gBitCheck(.udtChCommon.shtFlag2, 3) = False Then ''AC
                                    .udtChCommon.shtFlag2 = gBitSet(.udtChCommon.shtFlag2, 1, True)
                                End If
                            End If
                        End If

                    End If

                    If .udtChCommon.shtChType = gCstCodeChTypeMotor Then    ''モーター
                        .MotorUse = IIf(intExtG = 255, 0, 1)

                        ''flag2(Bit1) <-- アラームが有りでflag2のAC(Bit3)がOFFの場合のみONにする---------
                        If .MotorUse = 1 Then
                            .udtChCommon.shtM_ReposeSet = 1
                            If gBitCheck(.udtChCommon.shtFlag2, 3) = False Then ''AC
                                .udtChCommon.shtFlag2 = gBitSet(.udtChCommon.shtFlag2, 1, True)
                            End If
                        End If
                    End If



                    i += 1

                    ''------------------------------------------------------------------------
                    idx += 433  ''1CH = 433 Byte
                    If idx > UBound(mbyt22kFileArrayMpt) Then Flag1 = True

                    If Flag1 = False Then

                        Flag2 = False
                        Do Until Flag2

                            If mbyt22kFileArrayMpt(idx) = CInt("&H0D") And mbyt22kFileArrayMpt(idx + 1) = CInt("&H0A") Then

                                ''チャンネルデータをカウントアップ
                                intCnt += 1

                                ''表示位置カウントアップ
                                intDispPos += 1

                                idx += 2
                                If idx > UBound(mbyt22kFileArrayMpt) Then
                                    Flag2 = True : Flag1 = True
                                End If

                            Else
                                Flag2 = True
                                ''表示位置カウントアップ
                                intDispPos += 1
                            End If

                        Loop

                    End If

                End With

                ''プログレスバー更新
                prgBar.Value += 1 : prgBar.Refresh()

            Loop

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : バイト配列を文字列に変換する
    ' 返り値    : 文字列
    ' 引き数    : ARG1 - (I ) バイト配列サイズ
    ' 　　　    : ARG1 - (I ) バイト配列
    ' 機能説明  : Byte配列のデータを文字列に変換する
    '
    ' 2015.10.22 Ver1.7.5  private →　public 変更
    '--------------------------------------------------------------------
    Public Function mByte2String(ByVal intByteSize As Integer, ByVal bytTarget() As Byte) As String

        Try

            Dim bytArray(intByteSize - 1) As Byte
            Dim strRtn As String

            For i As Integer = LBound(bytArray) To UBound(bytArray)
                If bytTarget(i) = 0 Then
                    bytArray(i) = 32    'スペース
                Else
                    bytArray(i) = bytTarget(i)
                End If

            Next

            strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)

            mByte2String = strRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            mByte2String = ""
        End Try

    End Function

#End Region

#Region "端子台(スロット)設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : FU設定
    '--------------------------------------------------------------------
    Private Sub msetStructureUni(ByRef udtSetFu As gTypSetFu, _
                                 ByRef udtSetChDisp As gTypSetChDisp)

        Try
            Dim bytArray() As Byte
            Dim strValue As String = ""
            Dim intTBcnt As Integer = 0, idx As Integer = 0
            Dim intRYType As Integer = 0
            Dim intRYCount As Integer = 0
            Dim bytCanbus As Byte

            ''FCU/FU Type iniファイルから設定可能タブ数を獲得する
            Dim intTBList() As Integer = Nothing
            Call gSetComboBoxPlus(cmbWork, gEnmComboType.ctChTerminalListColumn3, intTBList)

            bytCanbus = mbyt22kFileArrayCan(0)

            ''既設はAからTの20個、今回は+FCUで21個
            For i As Integer = LBound(udtSetFu.udtFu) To UBound(udtSetFu.udtFu) - 1

                If i = 19 And mCanubusFunc = 1 Then     '' T and CANBUS設定有り　L/Uに設定しない
                    Exit For
                End If

                With udtSetFu.udtFu(i + 1)

                    ''スロット情報
                    For j As Integer = LBound(.udtSlotInfo) To UBound(.udtSlotInfo)

                        ''RYタイプ
                        intRYType = mbyt22kFileArrayRyt(0 + i * 8 + j)  '' 0:RY, 1:RY-1, 2:RY-2

                        ''スロット種別
                        If mbyt22kFileArrayUni(4 + i * 20 + j) <> 0 Then
                            Select Case mbyt22kFileArrayUni(4 + i * 20 + j)
                                Case &H20     ''DI 20 32
                                    .udtSlotInfo(j).shtType = gCstCodeFuSlotTypeDI
                                    mSlotInfo(i + 1, j, 0) = 64     ''行数
                                    mSlotInfo(i + 1, j, 1) = 2      ''デジタル DI
                                    mSlotInfo(i + 1, j, 2) = gCstCodeFuSlotTypeDI

                                    .shtUse = 1 ''ＦＵ 使用／未使用フラグ

                                Case &HB0    ''DO B0 176
                                    .udtSlotInfo(j).shtType = gCstCodeFuSlotTypeDO
                                    .udtSlotInfo(j).shtTerinf = 1                   ''TMDO
                                    mSlotInfo(i + 1, j, 0) = 64
                                    mSlotInfo(i + 1, j, 1) = 3      ''デジタル DO
                                    mSlotInfo(i + 1, j, 2) = gCstCodeFuSlotTypeDO

                                    .shtUse = 1 ''ＦＵ 使用／未使用フラグ

                                Case &H1A   ''K
                                    ''LCU-M200    アナログ
                                    .udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAI_K
                                    mSlotInfo(i + 1, j, 0) = 16
                                    mSlotInfo(i + 1, j, 1) = 1
                                    mSlotInfo(i + 1, j, 2) = gCstCodeFuSlotTypeAI_K

                                    .shtUse = 1 ''ＦＵ 使用／未使用フラグ

                                Case &H1B, &H1C     ''3 Line 1C 28
                                    ''LCU-M110    アナログ　温度
                                    .udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAI_3
                                    mSlotInfo(i + 1, j, 0) = 16
                                    mSlotInfo(i + 1, j, 1) = 1
                                    mSlotInfo(i + 1, j, 2) = gCstCodeFuSlotTypeAI_3

                                    .shtUse = 1 ''ＦＵ 使用／未使用フラグ

                                Case &H15, &H16, &H17     ''4-20 17 23
                                    ''LCU-M400    アナログ
                                    .udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAI_4_20
                                    mSlotInfo(i + 1, j, 0) = 16
                                    mSlotInfo(i + 1, j, 1) = 1
                                    mSlotInfo(i + 1, j, 2) = gCstCodeFuSlotTypeAI_4_20

                                    .shtUse = 1 ''ＦＵ 使用／未使用フラグ

                                Case &H10     ''1-5 10 16
                                    ''LCU-M500    アナログ
                                    .udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAI_1_5
                                    mSlotInfo(i + 1, j, 0) = 16
                                    mSlotInfo(i + 1, j, 1) = 1
                                    mSlotInfo(i + 1, j, 2) = gCstCodeFuSlotTypeAI_1_5

                                    .shtUse = 1 ''ＦＵ 使用／未使用フラグ

                                Case &H11, &H12, &H13, &H14
                                    ''LCU-M100    アナログ　温度
                                    .udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAI_2
                                    mSlotInfo(i + 1, j, 0) = 16
                                    mSlotInfo(i + 1, j, 1) = 1
                                    mSlotInfo(i + 1, j, 2) = gCstCodeFuSlotTypeAI_2

                                    .shtUse = 1 ''ＦＵ 使用／未使用フラグ

                                Case &HA0, &HA1, &HA2, &HA3
                                    ''LCU-M30   AO
                                    .udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAO
                                    mSlotInfo(i + 1, j, 0) = 8
                                    mSlotInfo(i + 1, j, 1) = 4
                                    mSlotInfo(i + 1, j, 2) = gCstCodeFuSlotTypeAO

                                    .shtUse = 1 ''ＦＵ 使用／未使用フラグ

                                Case &HB1, &HB2, &HB3, &HB4
                                    ''LCU-TMRYa   ①線式　1～64　→　DI or DO
                                    ''LCU-TMRYb   ①線式　1～64　→　DI or DO
                                    ''LCU-TMRYc   ①線式　1～64　→　DI or DO
                                    ''LCU-TMRYd   ①線式　1～64　→　DI or DO
                                    intRYCount = mbyt22kFileArrayUni(4 + i * 20 + j) - &HB0
                                    .udtSlotInfo(j).shtType = gCstCodeFuSlotTypeDO
                                    .udtSlotInfo(j).shtTerinf = 1 + (intRYType * 4) + intRYCount    ''TMRY   2015.02.04
                                    mSlotInfo(i + 1, j, 0) = 64
                                    mSlotInfo(i + 1, j, 1) = 3      ''デジタル DO
                                    mSlotInfo(i + 1, j, 2) = gCstCodeFuSlotTypeDO

                                    .shtUse = 1 ''ＦＵ 使用／未使用フラグ

                            End Select
                        End If

                    Next

                    ''CANBUS L/U
                    If bytCanbus <> &H30 Then               ''0:CANBUS設定なし
                        If ((bytCanbus - &H41) = i) Then    ''A～:CANBUS L/U
                            .shtCanBus = 1
                        End If
                    End If

                End With

                ''FU単位の表示名称
                With udtSetChDisp.udtChDisp(i + 1)

                    ''FCU/FU名称
                    ReDim bytArray(7)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayUni(4 + i * 20 + 10 + j)
                    Next j
                    strValue = gGetString(mByte2String(8, bytArray))

                    If strValue <> "" Then
                        .strFuName = gGetString(mByte2String(8, bytArray))
                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "端子台設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 能説明  : 端子台情報設定
    '--------------------------------------------------------------------
    Private Sub msetStructureLpf(ByRef udtSetChDisp As gTypSetChDisp)

        Try

            Dim bytArray() As Byte
            Dim strValue As String = ""

            ''22kはAからTの20個、今回は+FCUで21個

            For i As Integer = LBound(udtSetChDisp.udtChDisp) To UBound(udtSetChDisp.udtChDisp) - 1

                If i = 19 And mCanubusFunc = 1 Then     '' T and CANBUS設定有り　L/Uに設定しない
                    Exit For
                End If

                With udtSetChDisp.udtChDisp(i + 1)

                    'Field St. Type
                    Select Case mbyt22kFileArrayLpf(i * 64)
                        Case 1  ''SMS-U650A-1
                            .strFuType = "SMS-U5650-5"
                        Case 2  ''SMS-U650A-2
                            .strFuType = "SMS-U5650-5"
                        Case 3  ''SMS-U650A-3
                            .strFuType = "SMS-U5650-5"
                        Case 4  ''SMS-U650A-10
                            .strFuType = "SMS-U5650-13"
                        Case 5  ''SMS-U650A-11
                            .strFuType = "SMS-U5650-2"
                        Case 6  ''SMS-U650A-12
                            .strFuType = "SMS-U5650-3"
                        Case 7  ''SMS-U650A-13
                            .strFuType = "SMS-U5650-5"
                        Case 8  ''SMS-U650A-10P
                            .strFuType = "SMS-U5650-13P"
                        Case 9  ''SMS-U650A-14
                            .strFuType = "SMS-U5650-15"
                    End Select

                    ''Name Plate
                    ReDim bytArray(9)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayLpf(2 + i * 64 + j)
                    Next j
                    strValue = gGetString(mByte2String(10, bytArray))

                    If strValue <> "" Then .strNamePlate = strValue

                    ''Remarks(22kは20Byteだが今回は16Byte）
                    ReDim bytArray(15)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayLpf(12 + i * 64 + j)
                    Next j
                    strValue = gGetString(mByte2String(16, bytArray))

                    If strValue <> "" Then .strRemarks = strValue

                    ''Field St. Type
                    Call gSetComboBox(cmbWork, gEnmComboType.ctChTerminalListColumn3)

                End With

            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "DO設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : DO設定
    '--------------------------------------------------------------------
    Private Sub msetStructureT20(ByRef udtSetChOutput As gTypSetChOutput, _
                                 ByRef udtSetChAndOr As gTypSetChAndOr)

        Try

            Dim bytArray() As Byte
            Dim intValue As Integer, cnt As Integer = 0
            Dim flgSkip As Integer = 0

            Dim intChID As Integer = 0, intSystemNo As Integer = 0
            Dim intChType As Integer = 0, intStatus As Integer = 0, intDataType As Integer = 0
            Dim intCompIdx As Integer = 0, intPinNo As Integer = 0
            Dim strStatus As String = ""
            Dim blnAnalogUse(5) As Boolean, blnValveUse(6) As Boolean
            Dim intMask22k As Integer = 0

            Dim strwk() As String = Nothing, strValue As String = ""
            Dim strbp() As String = Nothing

            Dim bytfuno As Byte

            For i As Integer = LBound(udtSetChOutput.udtCHOutPut) To UBound(udtSetChOutput.udtCHOutPut)

                flgSkip = 0

                If i = 192 Then Exit For

                With udtSetChOutput.udtCHOutPut(i)

                    ''RTB-DOのアドレスは変換できないので先にアドレスチェックする  2013.08.07  K.Fujimoto
                    ''アドレス変換できない場合は設定しない
                    ''FU Address
                    ReDim bytArray(0)
                    bytArray(0) = mbyt22kFileArrayT20(9 + i * 16)    ''A ～ T
                    bytfuno = gGetFuNo(mByte2String(1, bytArray), True)
                    If bytfuno = gCstCodeChNotSetFuNoByte Then
                        flgSkip = 1
                    End If

                    If flgSkip = 0 Then

                        If mbyt22kFileArrayT20(3 + i * 16) = 128 Then       ''hex"80"

                            ''OR 出力 No
                            intValue = mbyt22kFileArrayT20(2 + i * 16)
                            .shtChid = intValue
                            intMask22k = mbyt22kFileArrayT20(5 + i * 16)

                            ''論理出力設定 -------------------------------------------------------------
                            Call msetStructureT21(udtSetChAndOr, intValue - 1, intChID, intMask22k)
                            ''--------------------------------------------------------------------------

                            ''Type
                            .bytType = 1    ''OR

                        ElseIf mbyt22kFileArrayT20(3 + i * 16) = 129 Then   ''hex"81"

                            ''AND 出力 No
                            intValue = mbyt22kFileArrayT20(2 + i * 16)
                            .shtChid = intValue
                            intMask22k = mbyt22kFileArrayT20(5 + i * 16)

                            ''論理出力設定 --------------------------------------------------------------
                            Call msetStructureT21(udtSetChAndOr, intValue - 1, intChID, intMask22k)
                            ''---------------------------------------------------------------------------

                            ''Type
                            .bytType = 2    ''AND

                        Else
                            ''CH No
                            intValue = gConnect2Byte(mbyt22kFileArrayT20(2 + i * 16), mbyt22kFileArrayT20(3 + i * 16))

                            If intValue = 0 Then
                                flgSkip = 1
                            Else
                                .shtChid = BitConverter.ToInt16(BitConverter.GetBytes(intValue), 0)
                                intChID = .shtChid

                                ''Type
                                .bytType = 0    ''CH
                            End If

                        End If
                    End If


                    If flgSkip = 0 Then

                        ''チャンネル情報 GET
                        If intChID > 0 Then
                            Call mGetChInfo(intChID, intSystemNo, intChType, intStatus, _
                                            intDataType, intCompIdx, intPinNo, strStatus, _
                                            blnAnalogUse, blnValveUse)
                        End If

                        ''System No
                        .shtSysno = gConnect2Byte(mbyt22kFileArrayT20(0 + i * 16), mbyt22kFileArrayT20(1 + i * 16))

                        ''Status(Output Movement)
                        If mbyt22kFileArrayT20(4 + i * 16) = 0 Then
                            .bytStatus = 0  ''<Alalm>

                            ''Mask
                            .shtMask = gBitSet(.shtMask, 0, IIf(gBitCheck(mbyt22kFileArrayT20(5 + i * 16), 0), True, False))    'L-set
                            .shtMask = gBitSet(.shtMask, 1, IIf(gBitCheck(mbyt22kFileArrayT20(5 + i * 16), 1), True, False))    'H-set
                            '.shtMask = gBitSet(.shtMask, 5, IIf(gBitCheck(mbyt22kFileArrayT20(5 + i * 16), 4), True, False))    'Sensor
                            '.shtMask = gBitSet(.shtMask, 0, IIf(mbyt22kFileArrayT20(5 + i * 16) = 0, True, False))              'Alarm

                            If mbyt22kFileArrayT20(5 + i * 16) = 0 Then     ''Alarm

                                If intChType = gCstCodeChTypeAnalog Then
                                    ''対象CHの.useが1のアラームのビットを全てONにする
                                    Dim mcstBitAnalog() As Integer = {-1, 3, 1, 0, 2, 5, 6}
                                    Dim intMask As Integer = 0
                                    For j As Integer = LBound(blnAnalogUse) To UBound(blnAnalogUse)
                                        intMask = gBitSet(intMask, mcstBitAnalog(j + 1), blnAnalogUse(j))
                                    Next
                                    .shtMask = intMask
                                Else
                                    .shtMask = gBitSet(.shtMask, 0, True)
                                End If

                            End If

                        ElseIf mbyt22kFileArrayT20(4 + i * 16) = 2 Then
                            .bytStatus = 2  ''<ON/OFF>

                            ''Mask
                            .shtMask = gBitSet(.shtMask, 6, IIf(mbyt22kFileArrayT20(5 + i * 16) = 0, True, False))
                            '.shtMask = gBitSet(.shtMask, 1, IIf(mbyt22kFileArrayT20(5 + i * 16) = 1, True, False))    'Off
                            '.shtMask = gBitSet(.shtMask, 0, IIf(mbyt22kFileArrayT20(5 + i * 16) = 0, True, False))    'On

                        ElseIf mbyt22kFileArrayT20(4 + i * 16) = 1 Then
                            .bytStatus = 1  ''<Motor>

                            ''Mask
                            Erase strwk
                            Erase strbp

                            ''ステータス情報を獲得する
                            Call GetStatusMotor2(mMotorStatus1, mMotorStatus2, "StatusMotor", mMotorBitPos1, mMotorBitPos2)

                            Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                            cmbStatus.SelectedValue = intStatus

                            If cmbStatus.SelectedIndex >= 0 Then

                                If intStatus = gCstCodeChManualInputStatus.ToString Then
                                    ''手入力

                                Else
                                    ''「_」区切りの文字列取得
                                    If intDataType >= gCstCodeChDataTypeMotorManRun And _
                                       intDataType <= gCstCodeChDataTypeMotorManRunK Then   'Ver2.0.0.2 モーター種別増加 JをKへ

                                        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                        strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")

                                    Else
                                        'Ver2.0.0.2 モーター種別増加 Rの処理を追加
                                        If intDataType >= gCstCodeChDataTypeMotorRManRun And _
                                            intDataType <= gCstCodeChDataTypeMotorRManRunK Then
                                            '正常Rは正常ステータス扱い
                                            strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                            strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")
                                        Else
                                            strwk = mMotorStatus2(cmbStatus.SelectedIndex).ToString.Split("_")
                                            strbp = mMotorBitPos2(cmbStatus.SelectedIndex).ToString.Split("_")
                                        End If
                                    End If
                                    ''取得したデータビット位置に基づいてマスクビットの作成
                                    Dim intMask As Integer = 0
                                    For j = 0 To UBound(strwk)
                                        intMask = gBitSet(intMask, CCInt(strbp(j)), gBitCheck(mbyt22kFileArrayT20(5 + i * 16), j + 1))
                                    Next
                                    .shtMask = intMask

                                End If

                            End If

                            End If

                        ''Output
                        If mbyt22kFileArrayT20(6 + i * 16) = 1 Then         ''Alalm

                            If mbyt22kFileArrayT20(11 + i * 16) = 0 Then
                                .bytOutput = 1  ''ALM(FT,LT)
                            ElseIf mbyt22kFileArrayT20(11 + i * 16) = 1 Then
                                .bytOutput = 2  ''ALM(FT,-)
                            ElseIf mbyt22kFileArrayT20(11 + i * 16) = 2 Then
                                .bytOutput = 3  ''ALM(-,LT)
                            ElseIf mbyt22kFileArrayT20(11 + i * 16) = 3 Then
                                .bytOutput = 4  ''ALM(-,-)
                            End If

                        ElseIf mbyt22kFileArrayT20(6 + i * 16) = 2 Then     ''CH
                            .bytOutput = 5      ''CH(-,-)

                        ElseIf mbyt22kFileArrayT20(6 + i * 16) = 3 Then     ''RUN

                            .bytOutput = 6      ''RUN(-,LT)
                        End If

                        ''FU Address
                        .bytFuno = bytfuno

                        If .bytFuno = gCstCodeChNotSetFuNoByte Then
                            .bytPortno = gCstCodeChNotSetFuPortByte
                            .bytPin = gCstCodeChNotSetFuPinByte
                        Else
                            intValue = mbyt22kFileArrayT20(10 + i * 16)

                            cnt = 0
                            For j As Integer = 0 To 4   'TB1 ～ TB5 

                                ''デジタル DO
                                If mSlotInfo(.bytFuno, j, 1) = 3 Then

                                    cnt += 1
                                    If mSlotInfo(.bytFuno, j, 0) * cnt >= intValue Then

                                        .bytPortno = j + 1

                                        If cnt = 1 Then
                                            .bytPin = intValue
                                        Else
                                            .bytPin = intValue - mSlotInfo(.bytFuno, j, 0) * (cnt - 1)
                                        End If

                                        With gudt.SetChDisp.udtChDisp(.bytFuno).udtSlotInfo(.bytPortno - 1).udtPinInfo(.bytPin - 1)

                                            ''端子情報に設定
                                            If mbyt22kFileArrayT20(3 + i * 16) < 128 Then  '' OR/AND除く
                                                If .shtChid = 0 Then
                                                    .shtChid = intChID
                                                End If
                                            End If

                                            ''ケーブルマーク設定
                                            ''WireMark
                                            ReDim bytArray(15)
                                            For k As Integer = LBound(bytArray) To UBound(bytArray)
                                                bytArray(k) = mbyt22kFileArrayCmj(0 + i * 48 + k)
                                            Next
                                            'Ver2.0.6.5 2個CHがある場合に消えてしまうため、既に値が入ってたら書かない
                                            If NZfS(.strWireMark) = "" Then
                                                .strWireMark = mByte2String(16, bytArray)
                                            End If


                                            ''WireMarkClass
                                            ReDim bytArray(11)
                                            For k As Integer = LBound(bytArray) To UBound(bytArray)
                                                bytArray(k) = mbyt22kFileArrayCmj(16 + i * 48 + k)
                                            Next
                                            'Ver2.0.6.5 2個CHがある場合に消えてしまうため、既に値が入ってたら書かない
                                            If NZfS(.strWireMarkClass) = "" Then
                                                .strWireMarkClass = mByte2String(12, bytArray)
                                            End If

                                            ''CoreNoIn
                                            ReDim bytArray(1)
                                            For k As Integer = LBound(bytArray) To UBound(bytArray)
                                                bytArray(k) = mbyt22kFileArrayCmj(28 + i * 48 + k)
                                            Next
                                            .strCoreNoIn = mByte2String(2, bytArray)

                                            ''CoreNoCom
                                            For k As Integer = LBound(bytArray) To UBound(bytArray)
                                                bytArray(k) = mbyt22kFileArrayCmj(30 + i * 48 + k)
                                            Next
                                            .strCoreNoCom = mByte2String(2, bytArray)

                                            ''Dest
                                            ReDim bytArray(15)
                                            For k As Integer = LBound(bytArray) To UBound(bytArray)
                                                bytArray(k) = mbyt22kFileArrayCmj(32 + i * 48 + k)
                                            Next
                                            'Ver2.0.6.5 2個CHがある場合に消えてしまうため、既に値が入ってたら書かない
                                            If NZfS(.strDest) = "" Then
                                                .strDest = mByte2String(16, bytArray)
                                            End If

                                        End With

                                        Exit For

                                    End If

                                End If

                            Next
                        End If

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "論理出力設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) 論理出力設定構造体
    '           : ARG2 - (I ) 22Kで使用しているOR/ANDが混在しているインデックス
    '           : ARG3 - (IO) 先頭のCH No.
    '           : ARG4 - (I ) マスク値
    ' 機能説明  : 論理出力設定
    '--------------------------------------------------------------------
    Private Sub msetStructureT21(ByRef udtSetChAndOr As gTypSetChAndOr, _
                                 ByVal T20Index As Integer, _
                                 ByRef intChNo As Integer, _
                                 ByVal intMask22k As Integer)

        Try

            Dim intValue As Integer = 0
            Dim intIndex As Integer = 0

            Dim intChID As Integer = 0, intSystemNo As Integer = 0
            Dim intChType As Integer = 0, intStatus As Integer = 0, intDataType As Integer = 0
            Dim intCompIdx As Integer = 0, intPinNo As Integer = 0
            Dim strStatus As String = ""
            Dim blnAnalogUse(5) As Boolean, blnValveUse(6) As Boolean

            Dim strwk() As String = Nothing
            Dim strbp() As String = Nothing

            
            intIndex = T20Index

            For i As Integer = 0 To 23

                With udtSetChAndOr.udtCHOut(intIndex).udtCHAndOr(i)

                    ''System No
                    .shtSysno = gConnect2Byte(mbyt22kFileArrayT21(0 + T20Index * 192 + i * 8), mbyt22kFileArrayT21(1 + T20Index * 192 + i * 8))

                    ''CH No
                    intValue = gConnect2Byte(mbyt22kFileArrayT21(2 + T20Index * 192 + i * 8), mbyt22kFileArrayT21(3 + T20Index * 192 + i * 8))
                    .shtChid = BitConverter.ToInt16(BitConverter.GetBytes(intValue), 0)

                    If i = 0 Then intChNo = .shtChid

                    ''Status
                    If mbyt22kFileArrayT21(4 + T20Index * 192 + i * 8) = 0 Then
                        .bytStatus = 0  ''Alalm
                    ElseIf mbyt22kFileArrayT21(4 + T20Index * 192 + i * 8) = 2 Then
                        .bytStatus = 2  ''ON/OFF
                    ElseIf mbyt22kFileArrayT21(4 + T20Index * 192 + i * 8) = 1 Then
                        .bytStatus = 1  ''Motor
                    End If

                    ''Mask
                    intChID = .shtChid
                    ''チャンネル情報 GET
                    If intChID > 0 Then
                        Call mGetChInfo(intChID, intSystemNo, intChType, intStatus, _
                                        intDataType, intCompIdx, intPinNo, strStatus, _
                                        blnAnalogUse, blnValveUse)
                    End If

                    If .bytStatus = 0 Then
                        ''<Alalm>

                        ''Mask
                        .shtMask = gBitSet(.shtMask, 0, IIf(gBitCheck(intMask22k, 0), True, False))    'L-set
                        .shtMask = gBitSet(.shtMask, 1, IIf(gBitCheck(intMask22k, 1), True, False))    'H-set

                        If intMask22k = 0 Then     ''Alarm

                            If intChType = gCstCodeChTypeAnalog Then
                                ''対象CHの.useが1のアラームのビットを全てONにする
                                Dim mcstBitAnalog() As Integer = {-1, 3, 1, 0, 2, 5, 6}
                                Dim intMask As Integer = 0
                                For j As Integer = LBound(blnAnalogUse) To UBound(blnAnalogUse)
                                    intMask = gBitSet(intMask, mcstBitAnalog(j + 1), blnAnalogUse(j))
                                Next
                                .shtMask = intMask
                            Else
                                .shtMask = gBitSet(.shtMask, 0, True)
                            End If

                        End If

                    ElseIf .bytStatus = 2 Then
                        ''<ON/OFF>

                        ''Mask
                        .shtMask = gBitSet(.shtMask, 6, IIf(intMask22k = 0, True, False))

                    ElseIf .bytStatus = 1 Then
                        ''<Motor>
                        ''Mask
                        Erase strwk
                        Erase strbp

                        ''ステータス情報を獲得する
                        Call GetStatusMotor2(mMotorStatus1, mMotorStatus2, "StatusMotor", mMotorBitPos1, mMotorBitPos2)

                        Call gSetComboBox(cmbStatus, gEnmComboType.ctChListChannelListStatusMotor)
                        cmbStatus.SelectedValue = intStatus

                        If cmbStatus.SelectedIndex >= 0 Then

                            If intStatus = gCstCodeChManualInputStatus.ToString Then
                                ''手入力

                            Else
                                ''「_」区切りの文字列取得
                                If intDataType >= gCstCodeChDataTypeMotorManRun And _
                                   intDataType <= gCstCodeChDataTypeMotorManRunK Then   'Ver2.0.0.2 モーター種別増加 JをKへ

                                    strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                    strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")

                                Else
                                    'Ver2.0.0.2 モーター種別増加 Rの処理を追加
                                    If intDataType >= gCstCodeChDataTypeMotorRManRun And _
                                        intDataType <= gCstCodeChDataTypeMotorRManRunK Then
                                        '正常Rは正常ステータス扱い
                                        strwk = mMotorStatus1(cmbStatus.SelectedIndex).ToString.Split("_")
                                        strbp = mMotorBitPos1(cmbStatus.SelectedIndex).ToString.Split("_")
                                    Else
                                        strwk = mMotorStatus2(cmbStatus.SelectedIndex).ToString.Split("_")
                                        strbp = mMotorBitPos2(cmbStatus.SelectedIndex).ToString.Split("_")
                                    End If
                                End If

                                ''取得したデータビット位置に基づいてマスクビットの作成
                                Dim intMask As Integer = 0
                                For j = 0 To UBound(strwk)
                                    intMask = gBitSet(intMask, CCInt(strbp(j)), gBitCheck(intMask22k, j + 1))
                                Next
                                .shtMask = intMask

                            End If

                        End If

                        End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "AO設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : AO設定
    '--------------------------------------------------------------------
    Private Sub msetStructureT22(ByRef udtSetSlot As gTypSetChDisp)

        Try

            Dim bytArray() As Byte
            Dim intValue As Integer, cnt As Integer
            Dim intSysno As Integer
            Dim intCHNo As Integer
            Dim intFuno As Integer
            Dim intPortno As Integer
            Dim intPinno As Integer

            For i As Integer = 0 To 63

                ''System No
                intSysno = gConnect2Byte(mbyt22kFileArrayT22(0 + i * 12), mbyt22kFileArrayT22(1 + i * 12))

                ''CH No
                intValue = gConnect2Byte(mbyt22kFileArrayT22(2 + i * 12), mbyt22kFileArrayT22(3 + i * 12))
                intCHNo = BitConverter.ToInt16(BitConverter.GetBytes(intValue), 0)

                ''FU Address
                ReDim bytArray(0)
                bytArray(0) = mbyt22kFileArrayT22(10 + i * 12)    ''A ～ T
                intFuno = gGetFuNo(mByte2String(1, bytArray), True)

                If intFuno = gCstCodeChNotSetFuNoByte Then
                    '.bytPortno = gCstCodeChNotSetFuPortByte
                    '.bytPin = gCstCodeChNotSetFuPinByte
                Else
                    intValue = mbyt22kFileArrayT22(11 + i * 12)

                    cnt = 0
                    For j As Integer = 0 To 4   'TB1 ～ TB5 

                        ''アナログ
                        If mSlotInfo(intFuno, j, 1) = 4 Then

                            cnt += 1
                            If mSlotInfo(intFuno, j, 0) * cnt >= intValue Then

                                intPortno = j + 1

                                If cnt = 1 Then
                                    intPinno = intValue

                                Else
                                    intPinno = intValue - mSlotInfo(intFuno, j, 0) * (cnt - 1)
                                End If

                                udtSetSlot.udtChDisp(intFuno).udtSlotInfo(intPortno - 1).udtPinInfo(intPinno - 1).shtChid = intCHNo

                                Exit For
                            End If

                        End If
                    Next
                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "運転積算設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 運転積算設定(T52)
    '--------------------------------------------------------------------
    Private Sub msetStructureT52(ByRef udtSet As gTypSetChRunHour, ByVal udtCh As gTypSetChInfo)

        Try
            Dim intValue As Integer

            For i As Integer = LBound(udtSet.udtDetail) To UBound(udtSet.udtDetail)

                ''22Kは128行までしかない
                If i = 128 Then Exit For

                With udtSet.udtDetail(i)

                    ''RH CH
                    intValue = gConnect2Byte(mbyt22kFileArrayT52(2 + i * 16), mbyt22kFileArrayT52(3 + i * 16))
                    .shtChid = intValue

                    ''Trigger CH
                    intValue = gConnect2Byte(mbyt22kFileArrayT52(8 + i * 16), mbyt22kFileArrayT52(9 + i * 16))
                    .shtTrgChid = intValue

                    If mbyt22kFileArrayT52(10 + i * 16) = 2 Then
                        ''Dig

                        ''Status
                        .shtStatus = 48

                        ''Mask
                        If mbyt22kFileArrayT52(11 + i * 16) = 0 Then
                            .shtMask = 0
                            .shtMask = gBitSet(.shtMask, 6, True)

                        Else
                            .shtMask = 0
                        End If

                    ElseIf mbyt22kFileArrayT52(10 + i * 16) = 1 Then

                        If .shtTrgChid > 0 Then

                            ''Status
                            .shtStatus = mGetStatus(.shtTrgChid, udtCh)

                        End If

                        If .shtStatus <> -1 Then
                            ''Mask
                            .shtMask = mbyt22kFileArrayT52(11 + i * 16)
                        End If

                    End If


                End With

            Next

            ''チャンネル構造体のトリガチャンネルを設定する
            Call mSetTrrigerChannelInfo(udtSet, udtCh)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Function mGetStatus(ByVal intCH As Integer, ByVal udtCh As gTypSetChInfo) As Integer

        Try
            Dim intStatus As Integer = 0

            For i As Integer = 0 To UBound(udtCh.udtChannel)

                With udtCh.udtChannel(i).udtChCommon

                    If intCH = .shtChno Then

                        If .shtChType = gCstCodeChTypeMotor Then
                            'intStatus = .shtStatus
                            intStatus = .shtData

                            If .shtData >= 223 And .shtData <= 226 Then
                                intStatus = -1
                            End If
                        Else
                            intStatus = 48
                        End If

                        Exit For

                    End If

                End With

            Next

            Return intStatus


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Sub mSetTrrigerChannelInfo(ByVal udtRunHour As gTypSetChRunHour, _
                                       ByRef udtChannelInfo As gTypSetChInfo)
        Try

            For i As Integer = 0 To UBound(udtChannelInfo.udtChannel)

                With udtChannelInfo.udtChannel(i)

                    ''パルス積算チャンネル
                    If .udtChCommon.shtChType = gCstCodeChTypePulse Then

                        ''データタイプが積算運転時間
                        If .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalHour _
                        Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalMin _
                        Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayHour _
                        Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayMin _
                        Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapHour _
                        Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapMin Then

                            ''チャンネルが設定されている場合
                            If .udtChCommon.shtChno <> 0 Then

                                ''トリガチャンネル情報初期化
                                .RevoTrigerSysno = 0
                                .RevoTrigerChid = 0

                                For j As Integer = 0 To UBound(udtRunHour.udtDetail)

                                    ''チャンネル番号が同じ場合
                                    If CInt(.udtChCommon.shtChno) = CInt(udtRunHour.udtDetail(j).shtChid) Then

                                        ''トリガチャンネルを設定
                                        .RevoTrigerChid = CInt(udtRunHour.udtDetail(j).shtTrgChid)
                                        Exit For

                                    End If

                                Next

                            End If

                        End If

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "リポーズ設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : グループリポーズ設定(T25)
    '--------------------------------------------------------------------
    Private Sub msetStructureT25(ByRef udtSet As gTypSetChGroupRepose)

        Try

            Dim intValue As Integer

            For i As Integer = LBound(udtSet.udtRepose) To UBound(udtSet.udtRepose)

                With udtSet.udtRepose(i)

                    ''データ種別コード
                    Select Case mbyt22kFileArrayT25(5 + i * 8)
                        Case 0
                            .shtData = 0    ''Normal
                        Case 1
                            .shtData = 3    ''MOTOR
                        Case 2
                            .shtData = 1    ''OR
                        Case 3
                            .shtData = 2    ''AND
                        Case 130
                            .shtData = 4    ''AND2 + OR
                    End Select

                    ''リポーズCH
                    intValue = gConnect2Byte(mbyt22kFileArrayT25(2 + i * 8), mbyt22kFileArrayT25(3 + i * 8))
                    If intValue <> 0 Then

                        .shtChId = intValue

                        For j As Integer = 0 To 5

                            ''CH
                            .udtReposeInf(j).shtChId = gConnect2Byte(mbyt22kFileArrayT25(i * 48 + 384 + 2 + j * 8), mbyt22kFileArrayT25(i * 48 + 384 + 3 + j * 8))

                            ''マスク値
                            .udtReposeInf(j).bytMask = mbyt22kFileArrayT25(i * 48 + 384 + 5 + j * 8)

                        Next

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "排ガス演算設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 排ガス演算設定(T19)
    '--------------------------------------------------------------------
    Private Sub msetStructureT19(ByRef udtSet As gTypSetChExhGus)

        Try

            For i As Integer = LBound(udtSet.udtExhGusRec) To UBound(udtSet.udtExhGusRec)

                With udtSet.udtExhGusRec(i)

                    ''ｼﾘﾝﾀﾞ本数
                    .shtNum = gConnect2Byte(mbyt22kFileArrayT19(i * 202 + 0), mbyt22kFileArrayT19(i * 202 + 1))

                    ''平均値出力CH  SYSTEM_NO.
                    .shtAveSysno = gConnect2Byte(mbyt22kFileArrayT19(i * 202 + 2), mbyt22kFileArrayT19(i * 202 + 3))

                    ''平均値出力CH  CH
                    .shtAveChid = gConnect2Byte(mbyt22kFileArrayT19(i * 202 + 4), mbyt22kFileArrayT19(i * 202 + 5))

                    ''リポーズCH    SYSTEM_NO.
                    .shtRepSysno = gConnect2Byte(mbyt22kFileArrayT19(i * 202 + 6), mbyt22kFileArrayT19(i * 202 + 7))

                    ''リポーズCH    CH
                    .shtRepChid = gConnect2Byte(mbyt22kFileArrayT19(i * 202 + 8), mbyt22kFileArrayT19(i * 202 + 9))

                    ''シリンダCH設定
                    For j As Integer = LBound(.udtExhGusCyl) To UBound(.udtExhGusCyl)

                        .udtExhGusCyl(j).shtSysno = gConnect2Byte(mbyt22kFileArrayT19(i * 202 + 10 + j * 4), mbyt22kFileArrayT19(i * 202 + 11 + j * 4))
                        .udtExhGusCyl(j).shtChid = gConnect2Byte(mbyt22kFileArrayT19(i * 202 + 12 + j * 4), mbyt22kFileArrayT19(i * 202 + 13 + j * 4))

                    Next

                    ''偏差CH設定
                    For j As Integer = LBound(.udtExhGusDev) To UBound(.udtExhGusDev)

                        .udtExhGusDev(j).shtSysno = gConnect2Byte(mbyt22kFileArrayT19(i * 202 + 106 + j * 4), mbyt22kFileArrayT19(i * 202 + 107 + j * 4))
                        .udtExhGusDev(j).shtChid = gConnect2Byte(mbyt22kFileArrayT19(i * 202 + 108 + j * 4), mbyt22kFileArrayT19(i * 202 + 109 + j * 4))

                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "コントロール使用可/不可設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : コントロール使用可/不可設定(T46)
    '--------------------------------------------------------------------
    Private Sub msetStructureT46(ByRef udtSet As gTypSetChCtrlUse)

        Try

            Dim intCnt As Integer = 0
            Dim intIndex As Integer = 0

            '' ver2.0.8.F 2018.12.18
            'For i As Integer = LBound(udtSet.udtCtrlUseNotuseRec) To UBound(udtSet.udtCtrlUseNotuseRec)
            For i As Integer = 0 To 31

                With udtSet.udtCtrlUseNotuseRec(i)

                    intCnt += 1

                    ''項目番号
                    .shtNo = intCnt

                    ''条件数
                    .shtCount = mbyt22kFileArrayT46(i * 2 + 0)

                    ''条件種類
                    Select Case mbyt22kFileArrayT46(i * 2 + 1)
                        Case 0 : .bytFlg = 3    ''OR:Not Use
                        Case 1 : .bytFlg = 1    ''AND:Not Use
                        Case 128 : .bytFlg = 4  ''OR:Use
                        Case 129 : .bytFlg = 2  ''AND:Use
                    End Select

                    If .shtCount > 0 Then

                        ''詳細設定
                        For j As Integer = 0 To .shtCount - 1

                            ''CH
                            .udtUseNotuseDetails(j).shtChno = gConnect4Byte(mbyt22kFileArrayT46(64 + intIndex * 8 + 3), _
                                                                            mbyt22kFileArrayT46(64 + intIndex * 8 + 2), _
                                                                            mbyt22kFileArrayT46(64 + intIndex * 8 + 1), _
                                                                            mbyt22kFileArrayT46(64 + intIndex * 8 + 0))
                            ''条件タイプ
                            Select Case mbyt22kFileArrayT46(64 + intIndex * 8 + 4)
                                Case 128 : .udtUseNotuseDetails(j).bytType = 1
                                Case 129 : .udtUseNotuseDetails(j).bytType = 2
                                Case 130 : .udtUseNotuseDetails(j).bytType = 3
                                Case 131 : .udtUseNotuseDetails(j).bytType = 4
                                Case 2 : .udtUseNotuseDetails(j).bytType = 5
                                Case 4 : .udtUseNotuseDetails(j).bytType = 6
                            End Select

                            ''ビット条件
                            .udtUseNotuseDetails(j).shtBit = mbyt22kFileArrayT46(64 + intIndex * 8 + 5)

                            ''Process1
                            .udtUseNotuseDetails(j).shtProcess1 = mbyt22kFileArrayT46(64 + intIndex * 8 + 6)

                            ''Process2
                            .udtUseNotuseDetails(j).shtProcess2 = mbyt22kFileArrayT46(64 + intIndex * 8 + 7)

                            intIndex += 1
                        Next

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "SIO設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : SIO設定(SIO)
    '--------------------------------------------------------------------
    Private Sub msetStructureSIO(ByRef udtSet As gTypSetChSio, _
                                 ByRef udtSetSioCh() As gTypSetChSioCh)

        Try

            For i As Integer = 0 To 8

                With udtSet.udtVdr(i)

                    If mbyt22kFileArraySIO(i * 256 + 0) = 1 Then

                        ''使用有無
                        .shtPort = 1

                        ''System No
                        .shtSysno = mbyt22kFileArraySIO(i * 256 + 1)

                        ''i/o種類
                        If mbyt22kFileArraySIO(i * 256 + 8) = 2 Then
                            .shtCommType1 = 1   ''Receive Only
                        ElseIf mbyt22kFileArraySIO(i * 256 + 8) = 19 Then
                            .shtCommType1 = 2   ''Command Recieve -> Data Transmission
                        ElseIf mbyt22kFileArraySIO(i * 256 + 8) = 3 Then
                            .shtCommType1 = 3   ''Command Transmission -> Data Recieve
                        ElseIf mbyt22kFileArraySIO(i * 256 + 8) = 4 Then
                            .shtCommType1 = 4   ''Data Recieve -> ACK Transmission
                        ElseIf mbyt22kFileArraySIO(i * 256 + 8) = 18 Then
                            .shtCommType1 = 5   ''Transmission Only
                        End If

                        ''通信種類
                        .shtCommType2 = mbyt22kFileArraySIO(i * 256 + 9)

                        ''回線情報---------------------------------------------------
                        ''回線種類
                        .udtCommInf.shtComm = 1     ''固定

                        ''データビット
                        If mbyt22kFileArraySIO(i * 256 + 4) = 8 Then        ''8bit
                            .udtCommInf.shtDataBit = 0
                        ElseIf mbyt22kFileArraySIO(i * 256 + 4) = 7 Then    ''7bit
                            .udtCommInf.shtDataBit = 1
                        End If

                        ''パリティ
                        If mbyt22kFileArraySIO(i * 256 + 3) = 3 Then        ''Even
                            .udtCommInf.shtParity = 2
                        ElseIf mbyt22kFileArraySIO(i * 256 + 3) = 1 Then    ''Non
                            .udtCommInf.shtParity = 0
                        ElseIf mbyt22kFileArraySIO(i * 256 + 3) = 2 Then    ''Odd
                            .udtCommInf.shtParity = 1
                        End If

                        ''ストップビット
                        If mbyt22kFileArraySIO(i * 256 + 5) = 1 Then        ''1bit
                            .udtCommInf.shtStop = 1
                        ElseIf mbyt22kFileArraySIO(i * 256 + 5) = 2 Then    ''1.5bit
                            .udtCommInf.shtStop = 0
                        ElseIf mbyt22kFileArraySIO(i * 256 + 5) = 3 Then    ''2bit
                            .udtCommInf.shtStop = 2
                        End If

                        ''通信速度
                        If mbyt22kFileArraySIO(i * 256 + 2) = 0 Then
                            .udtCommInf.shtComBps = 1   ''2400
                        ElseIf mbyt22kFileArraySIO(i * 256 + 2) = 1 Then
                            .udtCommInf.shtComBps = 2   ''4800
                        ElseIf mbyt22kFileArraySIO(i * 256 + 2) = 2 Then
                            .udtCommInf.shtComBps = 3   ''7200
                        ElseIf mbyt22kFileArraySIO(i * 256 + 2) = 3 Then
                            .udtCommInf.shtComBps = 4   ''9600
                        ElseIf mbyt22kFileArraySIO(i * 256 + 2) = 4 Then
                            .udtCommInf.shtComBps = 5   ''14400
                        ElseIf mbyt22kFileArraySIO(i * 256 + 2) = 5 Then
                            .udtCommInf.shtComBps = 6   ''19200
                        ElseIf mbyt22kFileArraySIO(i * 256 + 2) = 6 Then
                            .udtCommInf.shtComBps = 7   ''38400
                        ElseIf mbyt22kFileArraySIO(i * 256 + 2) = 7 Then
                            .udtCommInf.shtComBps = 8   ''57600
                        ElseIf mbyt22kFileArraySIO(i * 256 + 2) = 8 Then
                            .udtCommInf.shtComBps = 9   ''76800
                        End If
                        ''--------------------------------------------------------------

                        ''受信タイムアウト起動時
                        .shtReceiveInit = mbyt22kFileArraySIO(i * 256 + 12)

                        ''受信タイムアウト起動後
                        .shtReceiveUseally = mbyt22kFileArraySIO(i * 256 + 13)

                        ''送信間隔起動時
                        .shtSendInit = mbyt22kFileArraySIO(i * 256 + 14)

                        ''送信間隔起動後
                        .shtSendUseally = mbyt22kFileArraySIO(i * 256 + 15)

                        ''リトライ回数
                        '.shtRetry

                        ''Duplex設定
                        If mbyt22kFileArraySIO(i * 256 + 11) = 0 Then
                            .shtDuplexSet = 1   ''Full
                        ElseIf mbyt22kFileArraySIO(i * 256 + 11) = 1 Then
                            .shtDuplexSet = 0   ''Half
                        End If

                        ''送信CH
                        .shtSendCH = gConnect2Byte(mbyt22kFileArraySIO(i * 256 + 17), mbyt22kFileArraySIO(i * 256 + 16))

                        ''ノード情報 ----------------------------------------------------
                        For j As Integer = 0 To 7

                            ''使用有無
                            .udtNode(j).shtCheck = IIf(mbyt22kFileArraySIO(i * 256 + 20 + j) = 0, 0, 1)

                            ''アドレス
                            .udtNode(j).shtAddress = mbyt22kFileArraySIO(i * 256 + 20 + j)
                        Next
                        ''--------------------------------------------------------------

                    End If

                End With

            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : SIO設定CH設定
    '--------------------------------------------------------------------
    Private Sub msetStructureSioCh(ByRef udtSetSioCh() As gTypSetChSioCh)

        Try
            Dim bytArray(4) As Byte
            Dim bytType As Byte

            ''S1
            If IsNothing(mbyt22kFileArrayS1) = False Then
                With udtSetSioCh(0)

                    For j As Integer = 0 To UBound(.udtSioChRec)
                        ''CH No
                        bytType = mbyt22kFileArrayS1(j * 7)
                        For ii As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(ii) = mbyt22kFileArrayS1(1 + j * 7 + ii)
                        Next

                        If bytType = &H2A Then  ''*CHNO
                            .udtSioChRec(j).shtChNo = CCShort(mByte2String(4, bytArray))
                        Else
                            If mByte2String(3, bytArray) = "END" Then Exit For
                        End If


                    Next j

                End With
            End If

            ''S2
            If IsNothing(mbyt22kFileArrayS2) = False Then
                With udtSetSioCh(1)

                    For j As Integer = 0 To UBound(.udtSioChRec)
                        ''CH No
                        bytType = mbyt22kFileArrayS2(j * 7)
                        For ii As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(ii) = mbyt22kFileArrayS2(1 + j * 7 + ii)
                        Next

                        If bytType = &H2A Then  ''*CHNO
                            .udtSioChRec(j).shtChNo = CCShort(mByte2String(4, bytArray))
                        Else
                            If mByte2String(3, bytArray) = "END" Then Exit For
                        End If

                    Next j

                End With
            End If

            ''S3
            If IsNothing(mbyt22kFileArrayS3) = False Then
                With udtSetSioCh(2)

                    For j As Integer = 0 To UBound(.udtSioChRec)
                        ''CH No
                        bytType = mbyt22kFileArrayS3(j * 7)
                        For ii As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(ii) = mbyt22kFileArrayS3(1 + j * 7 + ii)
                        Next

                        If bytType = &H2A Then  ''*CHNO
                            .udtSioChRec(j).shtChNo = CCShort(mByte2String(4, bytArray))
                        Else
                            If mByte2String(3, bytArray) = "END" Then Exit For
                        End If
                    Next j

                End With
            End If

            ''S4
            If IsNothing(mbyt22kFileArrayS4) = False Then
                With udtSetSioCh(3)

                    For j As Integer = 0 To UBound(.udtSioChRec)
                        ''CH No
                        bytType = mbyt22kFileArrayS4(j * 7)
                        For ii As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(ii) = mbyt22kFileArrayS4(1 + j * 7 + ii)
                        Next

                        If bytType = &H2A Then  ''*CHNO
                            .udtSioChRec(j).shtChNo = CCShort(mByte2String(4, bytArray))
                        Else
                            If mByte2String(3, bytArray) = "END" Then Exit For
                        End If
                    Next j

                End With
            End If

            ''S5
            If IsNothing(mbyt22kFileArrayS5) = False Then
                With udtSetSioCh(4)

                    For j As Integer = 0 To UBound(.udtSioChRec)
                        ''CH No
                        bytType = mbyt22kFileArrayS5(j * 7)
                        For ii As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(ii) = mbyt22kFileArrayS5(1 + j * 7 + ii)
                        Next

                        If bytType = &H2A Then  ''*CHNO
                            .udtSioChRec(j).shtChNo = CCShort(mByte2String(4, bytArray))
                        Else
                            If mByte2String(3, bytArray) = "END" Then Exit For
                        End If
                    Next j

                End With
            End If

            ''S6
            If IsNothing(mbyt22kFileArrayS6) = False Then
                With udtSetSioCh(5)

                    For j As Integer = 0 To UBound(.udtSioChRec)
                        ''CH No
                        bytType = mbyt22kFileArrayS6(j * 7)
                        For ii As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(ii) = mbyt22kFileArrayS6(1 + j * 7 + ii)
                        Next

                        If bytType = &H2A Then  ''*CHNO
                            .udtSioChRec(j).shtChNo = CCShort(mByte2String(4, bytArray))
                        Else
                            If mByte2String(3, bytArray) = "END" Then Exit For
                        End If
                    Next j

                End With
            End If

            ''S7
            If IsNothing(mbyt22kFileArrayS7) = False Then
                With udtSetSioCh(6)

                    For j As Integer = 0 To UBound(.udtSioChRec)
                        ''CH No
                        bytType = mbyt22kFileArrayS7(j * 7)
                        For ii As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(ii) = mbyt22kFileArrayS7(1 + j * 7 + ii)
                        Next

                        If bytType = &H2A Then  ''*CHNO
                            .udtSioChRec(j).shtChNo = CCShort(mByte2String(4, bytArray))
                        Else
                            If mByte2String(3, bytArray) = "END" Then Exit For
                        End If
                    Next j

                End With
            End If

            ''S8
            If IsNothing(mbyt22kFileArrayS8) = False Then
                With udtSetSioCh(7)

                    For j As Integer = 0 To UBound(.udtSioChRec)
                        ''CH No
                        bytType = mbyt22kFileArrayS8(j * 7)
                        For ii As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(ii) = mbyt22kFileArrayS8(1 + j * 7 + ii)
                        Next

                        If bytType = &H2A Then  ''*CHNO
                            .udtSioChRec(j).shtChNo = CCShort(mByte2String(4, bytArray))
                        Else
                            If mByte2String(3, bytArray) = "END" Then Exit For
                        End If
                    Next j

                End With
            End If

            ''S9
            If IsNothing(mbyt22kFileArrayS9) = False Then
                With udtSetSioCh(8)

                    For j As Integer = 0 To UBound(.udtSioChRec)
                        ''CH No
                        bytType = mbyt22kFileArrayS9(j * 7)
                        For ii As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(ii) = mbyt22kFileArrayS9(1 + j * 7 + ii)
                        Next

                        If bytType = &H2A Then  ''*CHNO
                            .udtSioChRec(j).shtChNo = CCShort(mByte2String(4, bytArray))
                        Else
                            If mByte2String(3, bytArray) = "END" Then Exit For
                        End If
                    Next j

                End With
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "データ転送テーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : データ転送テーブル設定(T38)
    '--------------------------------------------------------------------
    Private Sub msetStructureT38(ByRef udtSet As gTypSetChDataForward)

        Try

            Dim strValue As String
            Dim bytArray() As Byte
            Dim intCnt As Integer = 0

            For i As Integer = LBound(udtSet.udtDetail) To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    ''データコード
                    .shtDataCode = mbyt22kFileArrayT38(i * 16 + 0)

                    ''データサブコード
                    .shtDataSubCode = mbyt22kFileArrayT38(i * 16 + 1)

                    ''オフセットアドレス(OPS→FCU)
                    ReDim bytArray(0)
                    strValue = ""
                    For j As Integer = 0 To 3
                        strValue += Hex(mbyt22kFileArrayT38(i * 16 + 7 - j)).PadLeft(2, "0")
                    Next
                    .intOffsetToFCU = Convert.ToInt64(strValue, 16)

                    ''オフセットアドレス(FCU→OPS)
                    ReDim bytArray(0)
                    strValue = ""
                    For j As Integer = 0 To 3
                        strValue += Hex(mbyt22kFileArrayT38(i * 16 + 11 - j)).PadLeft(2, "0")
                    Next
                    .intOffsetToOPS = Convert.ToInt64(strValue, 16)

                    ' ''データ値 (22Kはデータサイズ1項目につき、両方とも同じデータとする）
                    .shtSizeToFCU = gConnect2Byte(mbyt22kFileArrayT38(i * 16 + 12), mbyt22kFileArrayT38(i * 16 + 13))
                    .shtSizeToOps = gConnect2Byte(mbyt22kFileArrayT38(i * 16 + 12), mbyt22kFileArrayT38(i * 16 + 13))

                    ''以下項目無し
                    ' ''演算式のテーブルNO.
                    '.shtTableNo = mbyt22kFileArrayT38(i * 16 + 14)

                    ' ''CFへの保存有無
                    'If mbyt22kFileArrayT38(i * 16 + 2) = 255 Then
                    '    .shtSave = 1
                    'ElseIf mbyt22kFileArrayT38(i * 16 + 2) = 0 Then
                    '    .shtSave = 0
                    'End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "データ保存テーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : データ保存テーブル設定(DST)
    '--------------------------------------------------------------------
    Private Sub msetStructureDST(ByRef udtSet As gTypSetChDataSave)

        Try
            Dim SngDefault As Single
            Dim decimal_p As UInt16 = 0

            For i As Integer = LBound(udtSet.udtDetail) To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    ''CH
                    .shtSysno = gConnect2Byte(mbyt22kFileArrayDST(i * 10), mbyt22kFileArrayDST(1 + i * 10))
                    .shtChid = gConnect2Byte(mbyt22kFileArrayDST(2 + 0 + i * 10), mbyt22kFileArrayDST(2 + 1 + i * 10))

                    ''デフォルト値
                    ' ver1.4.0 2011.07.22 型変更　gConnect4ByteSingle → gConnect4Byte
                    '          2011.09.26 22Kはfloat型につき、SingleからIntegerに変換
                    SngDefault = gConnect4ByteSingle(mbyt22kFileArrayDST(2 + 2 + i * 10), _
                                                     mbyt22kFileArrayDST(2 + 3 + i * 10), _
                                                     mbyt22kFileArrayDST(2 + 4 + i * 10), _
                                                     mbyt22kFileArrayDST(2 + 5 + i * 10))

                    ''CH小数点位置取得
                    decimal_p = gGetChNoToDecimalPoint(.shtChid)

                    ''桁合わせ
                    .intDefault = Int(SngDefault * (10 ^ decimal_p) + 0.5)

                    ''立上げ時のデータ保存方法
                    .shtSet = mbyt22kFileArrayDST(2 + 6 + i * 10)

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "シーケンス設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : シーケンス設定(sks)
    '--------------------------------------------------------------------
    Private Sub msetStructureSks(ByRef udtSet As gTypSetSeqSet)

        Try
            Dim bytArray() As Byte
            Dim intFlag As Integer = 0
            Dim intCnt As Integer = 0, i As Integer = 0, cnt As Integer = 0
            Dim intValue As Integer, intMask As Integer
            Dim intKubun As Integer

            Do Until intFlag = 9

                If mbyt22kFileArraySks(i) = &HD And mbyt22kFileArraySks(i + 1) = &HA Then   ''0D0A
                    intFlag = 0
                    i += 1
                    intCnt += 1

                    If intCnt >= 1024 Then intFlag = 9

                Else

                    If intFlag <> 1 Then

                        With udtSet.udtDetail(intCnt)

                            ''シーケンスＩＤ
                            ReDim bytArray(4)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 0 + j)
                            Next j
                            .shtId = CCInt(mByte2String(5, bytArray))

                            ''シーケンスＩＤ(使用有無)
                            gudt.SetSeqID.shtID(intCnt) = 1

                            ''出力ロジックタイプ
                            ReDim bytArray(1)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 149 + j)
                            Next
                            intValue = CCInt(mByte2String(2, bytArray))
                            Select Case intValue
                                Case 0 : .shtLogicType = 1      ''AND
                                Case 1 : .shtLogicType = 2      ''OR
                                Case 3 : .shtLogicType = 3      ''D Latch
                                Case 4 : .shtLogicType = 4      ''AND-OR
                                Case 5 : .shtLogicType = 5      ''OR-AND
                                Case 6 : .shtLogicType = 6      ''AND-AND-OR
                                Case 20 : .shtLogicType = 7     ''Analog through
                                Case 21 : .shtLogicType = 8     ''Analog gate
                                Case 22 : .shtLogicType = 9     ''Analog gate(OFF change)
                                Case 23 : .shtLogicType = 10    ''Analog multiplexer
                                Case 40 : .shtLogicType = 11    ''average logic
                                Case 41 : .shtLogicType = 12    ''timer subtraction(1 input)
                                Case 42 : .shtLogicType = 13    ''timer subtraction(2 input)
                                Case 43 : .shtLogicType = 14    ''condition addition
                                Case 44 : .shtLogicType = 15    ''linear table logic
                                Case 45 : .shtLogicType = 16    ''calculate logic
                                Case 46 : .shtLogicType = 17    ''data comparison
                                Case 47 : .shtLogicType = 18    ''event timer
                                Case 49 : .shtLogicType = 19    ''pulse count
                                Case 50 : .shtLogicType = 19    ''pulse count(ADD)
                            End Select

                            ''ロジック項目
                            For ii As Integer = 0 To 4
                                ReDim bytArray(4)
                                For j As Integer = LBound(bytArray) To UBound(bytArray)
                                    bytArray(j) = mbyt22kFileArraySks(i + 194 + ii * 5 + j)
                                Next j
                                .shtLogicItem(ii) = CCInt(mByte2String(5, bytArray))
                            Next

                            If .shtLogicType = 15 Then                      ''linear table logic
                                .shtLogicItem(0) = .shtLogicItem(0) + 1     ''22Kはテーブル0スタートにつき+1
                            End If

                            ''CH Output
                            ''CH
                            ReDim bytArray(4)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 155 + j)
                            Next j
                            .shtOutChid = CCInt(mByte2String(5, bytArray))

                            ''出力ステータス
                            ReDim bytArray(4)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 160 + j)
                            Next j
                            intValue = CCInt(mByte2String(5, bytArray))
                            If intValue = 0 Or intValue = 1 Then
                                .bytOutStatus = intValue + 1
                            ElseIf intValue = 9 Or intValue = 10 Or intValue = 11 Then
                                .bytOutStatus = intValue - 6
                            End If

                            ''出力データ
                            ReDim bytArray(4)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 165 + j)
                            Next j
                            intValue = CCInt(mByte2String(5, bytArray))
                            .shtOutData = BitConverter.ToInt16(BitConverter.GetBytes(intValue), 0)

                            ''出力データタイプ
                            ReDim bytArray(0)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 171 + j)
                            Next j
                            .bytOutDataType = CCInt(mByte2String(1, bytArray)) + 1

                            ''出力オフディレイ
                            ReDim bytArray(4)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 174 + j)
                            Next j
                            .shtOutDelay = CCInt(mByte2String(5, bytArray))

                            ''出力反転
                            ReDim bytArray(0)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 170 + j)
                            Next j
                            .bytOutInv = CCInt(mByte2String(1, bytArray))


                            ''チャンネル使用有無
                            ReDim bytArray(0)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 186 + j)
                            Next j
                            intValue = CCInt(mByte2String(1, bytArray))
                            .shtUseCh(0) = IIf(gBitCheck(intValue, 0), 1, 0)
                            .shtUseCh(1) = IIf(gBitCheck(intValue, 1), 1, 0)
                            .shtUseCh(2) = IIf(gBitCheck(intValue, 2), 1, 0)
                            .shtUseCh(3) = IIf(gBitCheck(intValue, 3), 1, 0)
                            .shtUseCh(4) = IIf(gBitCheck(intValue, 4), 1, 0)

                            ''出力タイプ
                            ReDim bytArray(0)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 188 + j)
                            Next j
                            .bytOutType = CCInt(mByte2String(1, bytArray)) + 1

                            ''出力ワンショット時間
                            ReDim bytArray(2)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 191 + j)
                            Next j
                            .bytOneShot = CCInt(mByte2String(3, bytArray))

                            '▼▼▼ 20110330 処理継続中止（コンバートでは何もしない）▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                            '.bytContine = 
                            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                            ''FU Address
                            ReDim bytArray(0)
                            bytArray(0) = mbyt22kFileArraySks(i + 181)    ''A ～ T
                            If (bytArray(0) >= &H41 And bytArray(0) <= &H54) Then
                                .bytFuno = gGetFuNo(mByte2String(1, bytArray))
                            Else
                                .bytFuno = gCstCodeChNotSetFuNoByte
                            End If


                            ''FU Address
                            If .bytFuno = gCstCodeChNotSetFuNoByte Then
                                .bytPort = gCstCodeChNotSetFuPortByte
                                .bytPin = gCstCodeChNotSetFuPinByte
                            Else
                                ReDim bytArray(2)
                                bytArray(0) = mbyt22kFileArraySks(i + 183)
                                bytArray(1) = mbyt22kFileArraySks(i + 184)
                                bytArray(2) = mbyt22kFileArraySks(i + 185)
                                intValue = CCInt(mByte2String(3, bytArray))

                                cnt = 0
                                For j As Integer = 0 To 4   'TB1 ～ TB5 

                                    If .bytOutType = 3 Then   ''A/O
                                        ''アナログ
                                        If mSlotInfo(.bytFuno, j, 1) = 4 Then

                                            cnt += 1
                                            If mSlotInfo(.bytFuno, j, 0) * cnt >= intValue Then

                                                .bytPort = j + 1

                                                If cnt = 1 Then
                                                    .bytPin = intValue
                                                Else
                                                    .bytPin = intValue - mSlotInfo(.bytFuno, j, 0) * (cnt - 1)
                                                End If
                                                Exit For

                                            End If

                                        End If

                                    Else
                                        ''デジタル
                                        If mSlotInfo(.bytFuno, j, 1) = 3 Then

                                            cnt += 1
                                            If mSlotInfo(.bytFuno, j, 0) * cnt >= intValue Then

                                                .bytPort = j + 1

                                                If cnt = 1 Then
                                                    .bytPin = intValue
                                                Else
                                                    .bytPin = intValue - mSlotInfo(.bytFuno, j, 0) * (cnt - 1)
                                                End If
                                                Exit For

                                            End If

                                        End If

                                    End If

                                Next

                            End If

                            ''Input Set
                            For ii As Integer = 0 To 7

                                ''CH
                                ReDim bytArray(4)
                                For j As Integer = LBound(bytArray) To UBound(bytArray)
                                    bytArray(j) = mbyt22kFileArraySks(i + 6 + ii * 18 + j)
                                Next j
                                .udtInput(ii).shtChid = CCShort(mByte2String(5, bytArray))

                                ''Input Type
                                ReDim bytArray(1)
                                For j As Integer = LBound(bytArray) To UBound(bytArray)
                                    bytArray(j) = mbyt22kFileArraySks(i + 14 + ii * 18 + j)
                                Next j
                                .udtInput(ii).bytType = CCInt(mByte2String(2, bytArray))

                                ''Input Mask
                                ReDim bytArray(4)
                                For j As Integer = LBound(bytArray) To UBound(bytArray)
                                    bytArray(j) = mbyt22kFileArraySks(i + 17 + ii * 18 + j)
                                Next j
                                intMask = CCInt(mByte2String(5, bytArray))

                                ''その他
                                ReDim bytArray(0)
                                For j As Integer = LBound(bytArray) To UBound(bytArray)
                                    bytArray(j) = mbyt22kFileArraySks(i + 22 + ii * 18 + j)
                                Next j
                                intKubun = CCInt(mByte2String(1, bytArray))

                                ''Status
                                ReDim bytArray(2)
                                For j As Integer = LBound(bytArray) To UBound(bytArray)
                                    bytArray(j) = mbyt22kFileArraySks(i + 11 + ii * 18 + j)
                                Next j
                                intValue = CCInt(mByte2String(3, bytArray))

                                Select Case intValue

                                    Case 0  ''Data
                                        If intKubun = 1 Then        ''Analog
                                            .udtInput(ii).shtChSelect = 1
                                            .udtInput(ii).bytStatus = &H11
                                            .udtInput(ii).shtMask = &HFFFF

                                        ElseIf intKubun = 2 Then        ''Digital
                                            .udtInput(ii).shtChSelect = 1
                                            .udtInput(ii).bytStatus = &H12
                                            .udtInput(ii).shtMask = &H40

                                        ElseIf intKubun = 3 Then    ''Motor(ST/BY)
                                            .udtInput(ii).shtChSelect = 1
                                            .udtInput(ii).bytStatus = &H15
                                            .udtInput(ii).shtMask = intMask

                                        ElseIf intKubun = 6 Then    ''Pulse
                                            .udtInput(ii).shtChSelect = 1
                                            .udtInput(ii).bytStatus = &H13
                                            .udtInput(ii).shtMask = &HFFFF

                                        ElseIf intKubun = 7 Then    ''Running
                                            .udtInput(ii).shtChSelect = 1
                                            .udtInput(ii).bytStatus = &H14
                                            .udtInput(ii).shtMask = &HFFFF

                                        End If

                                    Case 12 ''Data/High
                                        If intMask = 64 Then
                                            .udtInput(ii).shtChSelect = 1
                                            .udtInput(ii).bytStatus = &H1E
                                            .udtInput(ii).shtMask = &H0
                                        End If

                                    Case 13 ''Data/Low
                                        If intMask = 64 Then
                                            .udtInput(ii).shtChSelect = 1
                                            .udtInput(ii).bytStatus = &H1F
                                            .udtInput(ii).shtMask = &H0
                                        End If

                                    Case 1  ''Alarm
                                        If intKubun = 1 Then        ''Analog
                                            If intMask = 1 Then         ''Low
                                                .udtInput(ii).shtChSelect = 2
                                                .udtInput(ii).bytStatus = &H21
                                                .udtInput(ii).shtMask = &H1

                                            ElseIf intMask = 2 Then     ''High
                                                .udtInput(ii).shtChSelect = 2
                                                .udtInput(ii).bytStatus = &H21
                                                .udtInput(ii).shtMask = &H2

                                            ElseIf intMask = 3 Then     ''Low & High
                                                .udtInput(ii).shtChSelect = 2
                                                .udtInput(ii).bytStatus = &H21
                                                .udtInput(ii).shtMask = &H3

                                            ElseIf intMask = 16 Then    ''Sensor
                                                .udtInput(ii).shtChSelect = 2
                                                .udtInput(ii).bytStatus = &H21
                                                .udtInput(ii).shtMask = &H70
                                            End If

                                        ElseIf intKubun = 2 Then    ''Digital
                                            .udtInput(ii).shtChSelect = 2
                                            .udtInput(ii).bytStatus = &H22
                                            .udtInput(ii).shtMask = &H1

                                        ElseIf intKubun = 3 Then    ''Motor
                                            .udtInput(ii).shtChSelect = 2
                                            .udtInput(ii).bytStatus = &H23
                                            .udtInput(ii).shtMask = &H1

                                        End If

                                    Case 14 ''Alarm/High
                                        .udtInput(ii).shtChSelect = 2   '1
                                        .udtInput(ii).bytStatus = &H2E
                                        .udtInput(ii).shtMask = &H0

                                    Case 15 ''Alarm/Low
                                        .udtInput(ii).shtChSelect = 2   '1
                                        .udtInput(ii).bytStatus = &H2F
                                        .udtInput(ii).shtMask = &H0

                                    Case 16 ''Calc/Ope1
                                        .udtInput(ii).shtChSelect = 3
                                        .udtInput(ii).bytStatus = &H31
                                        .udtInput(ii).shtMask = &H0

                                    Case 17 ''Calc/Ope2
                                        .udtInput(ii).shtChSelect = 3
                                        .udtInput(ii).bytStatus = &H32
                                        .udtInput(ii).shtMask = &H0

                                    Case 18 ''Calc/Ope3
                                        .udtInput(ii).shtChSelect = 3
                                        .udtInput(ii).bytStatus = &H33
                                        .udtInput(ii).shtMask = &H0

                                    Case 19 ''Calc/Ope4
                                        .udtInput(ii).shtChSelect = 3
                                        .udtInput(ii).bytStatus = &H34
                                        .udtInput(ii).shtMask = &H0

                                    Case 20 ''Calc/Ope5
                                        .udtInput(ii).shtChSelect = 3
                                        .udtInput(ii).bytStatus = &H35
                                        .udtInput(ii).shtMask = &H0

                                    Case 30 ''EXT Group Out
                                        .udtInput(ii).shtChSelect = 4
                                        .udtInput(ii).bytStatus = &H41
                                        .udtInput(ii).shtMask = intMask

                                    Case 31 ''EXT BZ Out
                                        .udtInput(ii).shtChSelect = 4
                                        .udtInput(ii).bytStatus = &H42
                                        .udtInput(ii).shtMask = intMask

                                    Case Else   ''Manual input
                                        .udtInput(ii).shtChSelect = 5
                                        .udtInput(ii).bytStatus = intValue
                                        .udtInput(ii).shtMask = intMask
                                End Select

                                If .udtInput(ii).shtChid > 10000 Then       ''SEQ番号設定
                                    .udtInput(ii).shtChSelect = 3
                                    .udtInput(ii).bytStatus = &H31
                                    .udtInput(ii).shtMask = &H0
                                End If

                            Next ii

                            ''備考
                            ReDim bytArray(15)
                            For j As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArraySks(i + 220 + j)
                            Next j
                            .strRemarks = mByte2String(16, bytArray)

                        End With

                        intFlag = 1
                    End If

                End If

                i += 1
            Loop

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "リニアライズテーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : リニアライズテーブル設定(Rin)
    '--------------------------------------------------------------------
    Private Sub msetStructureRin(ByRef udtSet As gTypSetSeqLinear)

        Try

            Dim bytArray() As Byte
            Dim intFlag As Integer = 0
            Dim p As Integer = 0, p_bk As Integer = 0
            Dim intTableNo As Integer = 1, intNo As Integer = 0
            Dim intCnt As Integer = 0, dblValue As Double = 0

            Do Until intFlag = 9

                If mbyt22kFileArrayRin(p) = &H2C Then   ''カンマ -------------------------------------------------

                    intNo += 1

                    ''リニアライズテーブル値（Ｘ）
                    Erase bytArray : intCnt = 0
                    For i As Integer = p_bk To p - 1
                        ReDim Preserve bytArray(intCnt)
                        bytArray(intCnt) = mbyt22kFileArrayRin(i)
                        intCnt += 1
                    Next
                    dblValue = CCDouble(mByte2String(intCnt, bytArray))
                    udtSet.udtTables(intTableNo - 1).udtRow(intNo - 1).sngPtX = dblValue

                    p_bk = p + 1

                ElseIf mbyt22kFileArrayRin(p) = &HD And mbyt22kFileArrayRin(p + 1) = &HA Then   ''0D0A -----------

                    If intFlag <> 1 Then
                        If intNo > 0 Then
                            ''リニアライズテーブル値（Ｙ）
                            Erase bytArray : intCnt = 0
                            For i As Integer = p_bk To p - 1
                                ReDim Preserve bytArray(intCnt)
                                bytArray(intCnt) = mbyt22kFileArrayRin(i)
                                intCnt += 1
                            Next
                            dblValue = CCDouble(mByte2String(intCnt, bytArray))
                            udtSet.udtTables(intTableNo - 1).udtRow(intNo - 1).sngPtY = dblValue
                        End If

                        p_bk = p + 2

                    ElseIf intFlag = 1 Then
                        intFlag = 0
                        p_bk = p + 2
                    End If

                ElseIf mbyt22kFileArrayRin(p) = &H40 Then    ''@ ---------------------------------------------------

                    If mbyt22kFileArrayRin(p + 1) = &H50 And mbyt22kFileArrayRin(p + 2) = &H41 And mbyt22kFileArrayRin(p + 3) = &H53 Then
                        ''PAS --------
                        udtSet.udtPoints(intTableNo - 1).shtPoint = intNo   ''ポイント数
                        intTableNo += 1
                        intNo = 0
                        intFlag = 1

                    ElseIf mbyt22kFileArrayRin(p + 1) = &H45 And mbyt22kFileArrayRin(p + 2) = &H4E And mbyt22kFileArrayRin(p + 3) = &H44 Then
                        ''END --------
                        udtSet.udtPoints(intTableNo - 1).shtPoint = intNo   ''ポイント数
                        intFlag = 9
                    End If

                End If

                If p >= UBound(mbyt22kFileArrayRin) - 1 Then intFlag = 9

                p += 1

            Loop

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "演算式テーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 演算式テーブル設定(T36, tei)
    '--------------------------------------------------------------------
    Private Sub msetStructureT36(ByRef udtSet As gTypSetSeqOperationExpression)

        Try
            Dim bytArray() As Byte
            Dim intValue As Integer
            Dim dblValue As Double

            For i As Integer = 0 To UBound(udtSet.udtTables)

                With udtSet.udtTables(i)

                    ''演算式
                    ReDim bytArray(31)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayT36(i * 32 + j)
                    Next j
                    .strExp = mByte2String(32, bytArray)
                    '.strExp = gGetString(mByte2String(32, bytArray))

                    For j As Integer = 0 To 15

                        ''定数種類
                        If mbyt22kFileArrayT36(2048 + i * 128 + j * 8) = 0 Then
                            .udtAryInf(j).shtType = 1   ''定数 Fixed number

                        ElseIf mbyt22kFileArrayT36(2048 + i * 128 + j * 8) = 1 Then
                            .udtAryInf(j).shtType = 0   ''CH

                        ElseIf mbyt22kFileArrayT36(2048 + i * 128 + j * 8) = 2 Then
                            .udtAryInf(j).shtType = 3   ''LowSet

                        ElseIf mbyt22kFileArrayT36(2048 + i * 128 + j * 8) = 3 Then
                            .udtAryInf(j).shtType = 4   ''HighSet
                        Else
                            .udtAryInf(j).shtType = 0   ''CH
                        End If

                        If .udtAryInf(j).shtType = 1 Then
                            ''定数
                            .udtAryInf(j).bytInfo(0) = mbyt22kFileArrayT36(2048 + 4 + i * 128 + j * 8)
                            .udtAryInf(j).bytInfo(1) = mbyt22kFileArrayT36(2048 + 5 + i * 128 + j * 8)
                            .udtAryInf(j).bytInfo(2) = mbyt22kFileArrayT36(2048 + 6 + i * 128 + j * 8)
                            .udtAryInf(j).bytInfo(3) = mbyt22kFileArrayT36(2048 + 7 + i * 128 + j * 8)

                        Else
                            ''CH
                            dblValue = gConnect4ByteSingle(mbyt22kFileArrayT36(2048 + 4 + i * 128 + j * 8), mbyt22kFileArrayT36(2048 + 5 + i * 128 + j * 8), _
                                                           mbyt22kFileArrayT36(2048 + 6 + i * 128 + j * 8), mbyt22kFileArrayT36(2048 + 7 + i * 128 + j * 8))
                            intValue = dblValue
                            gSeparat2Byte(intValue, .udtAryInf(j).bytInfo(2), .udtAryInf(j).bytInfo(3))

                        End If
                    Next

                    ''VariableName
                    For j As Integer = 0 To 7

                        ReDim bytArray(7)
                        For ii As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(ii) = mbyt22kFileArrayTei(i * 192 + j * 8 + ii)
                        Next
                        .strVariavleName(j) = mByte2String(8, bytArray)
                    Next

                    ''定数名称
                    For j As Integer = 0 To 15

                        ''定数名称
                        ReDim bytArray(7)
                        For ii As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(ii) = mbyt22kFileArrayTei(i * 192 + 64 + j * 8 + ii)
                        Next
                        .udtAryInf(j).strFixNum = mByte2String(8, bytArray)

                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "OPS設定"

#Region "コンバート用構造体の作成"

    '--------------------------------------------------------------------
    ' 機能      : コンバート用OPS構造体作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) コンバート用のOPS設定構造体
    '           : ARG2 - (IO) 排気ガス・バーグラフの既設データを読み込む構造体
    '           : ARG3 - (IO) アナログメーターの既設データを読み込む構造体
    ' 機能説明  : コンバート用のOPS設定構造体に既設データを設定する
    '--------------------------------------------------------------------
    Private Sub mMakeOpsStructure(ByRef hudtOpsData() As gTypConvertOps, _
                                  ByRef hudtOpsExhBar() As gTypConvertOpsGraphData, _
                                  ByRef hudtOpsAnalog() As gTypConvertOpsGraphData)

        Try

            Dim i As Integer
            Dim intLoopCntAnalog As Integer = 0
            Dim intLoopCntExgBar As Integer = 0

            ''--------------------------------------------------------
            '' 配列再定義
            ''--------------------------------------------------------
            ReDim hudtOpsExhBar(gCstCodeOpsGraphNo - 1)
            ReDim hudtOpsAnalog(gCstCodeOpsGraphNo - 1)
            ReDim hudtOpsData(gCstCodeOpsGraphNo - 1)
            For i = LBound(hudtOpsData) To UBound(hudtOpsData)
                ReDim hudtOpsExhBar(i).udtGraphDataDetail(gCstCodeOpsRowCountExhBar - 1)
                ReDim hudtOpsExhBar(i).udtTcGraphInfo(gCstCodeOpsRowCountTC - 1)

                ReDim hudtOpsAnalog(i).udtGraphDataDetail(gCstCodeOpsRowCountAnalog - 1)
                ReDim hudtOpsAnalog(i).udtTcGraphInfo(gCstCodeOpsRowCountTC - 1)

                ReDim hudtOpsData(i).udtGraphData.udtGraphDataDetail(gCstCodeOpsRowCountExhBar - 1)
                ReDim hudtOpsData(i).udtGraphData.udtTcGraphInfo(gCstCodeOpsRowCountTC - 1)
            Next

            ''--------------------------------------------------------
            '' バイト配列ファイルよりデータ取得
            ''--------------------------------------------------------

            ''３種グラフデータの情報取得
            For i = 0 To UBound(hudtOpsData)
                Call mMakeOpsStructureT10(hudtOpsExhBar(i), i)  ''排気ガス・バーグラフ
                Call mMakeOpsStructureT11(hudtOpsAnalog(i), i)  ''アナログメーター
            Next

            ''CHデータの情報取得
            Call mMakeOpsStructureT12(hudtOpsData, hudtOpsExhBar)

            ''--------------------------------------------------------
            '' コンバート用OPS構造体作成
            ''--------------------------------------------------------
            For i = LBound(hudtOpsData) To UBound(hudtOpsData)

                If hudtOpsData(i).intGraphType <> 0 Then

                    ''構造体の構造上、アナログメーターとその他（排気ガス・バーグラフ）で分ける
                    Select Case hudtOpsData(i).intGraphType

                        Case mCstCodeOpsGraphTypeAnalogMeter

                            Call mMakeStructure(hudtOpsData(i), hudtOpsAnalog(intLoopCntAnalog), True)
                            intLoopCntAnalog += 1

                        Case mCstCodeOpsGraphTypeExhaust, _
                              mCstCodeOpsGraphTypeBarNormal, _
                              mCstCodeOpsGraphTypeBarPercent, _
                              mCstCodeOpsGraphTypeBar6dig, _
                              mCstCodeOpsGraphTypeBar6digPercent

                            Call mMakeStructure(hudtOpsData(i), hudtOpsExhBar(intLoopCntExgBar))
                            intLoopCntExgBar += 1

                    End Select

                End If

            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタのCH情報取得
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) OPS設定_CHデータ構造体
    ' 機能説明  : OPS設定構造体に既設で使用しているCHデータを設定する
    '--------------------------------------------------------------------
    Private Sub mMakeOpsStructureT12(ByRef hudtOpsData() As gTypConvertOps, ByVal hudtOpsExhBar() As gTypConvertOpsGraphData)

        Try

            Dim intStartRowIndex As Integer     ''データ読込み位置
            Dim intRoopCntDev As Integer        ''偏差CHの配列ループ
            Dim strValue As String
            Dim i As Integer, j As Integer, k As Integer
            Dim intGraphcnt As Integer             ''グラフ区切り

            ''CH情報の取得開始
            For i = 0 To UBound(hudtOpsData)

                ''設定画面情報
                hudtOpsData(i).intGraphType = mbyt22kFileArrayT12(i)                ''画面コード
                hudtOpsData(i).strGraphType = SetGraphType(mbyt22kFileArrayT12(i))  ''画面名称概要（構造体見た時に分かりやすくするため）

                ''------------------------------
                '' CylCH(T/C、AnalogMeter) 
                '' ※偏差グラフの'Cyl-CH'と'T/C-CH'の分割処理は、グローバル構造体に設定する所で実施
                ''------------------------------

                With hudtOpsExhBar(i)
                    ''バーグラフ本数（シリンダ本数）によりT/Cタイトル取得位置可変
                    If (.intCyCnt + .intTcCnt) <= 12 Then
                        intGraphcnt = 12 - .intTcCnt
                    ElseIf .intCyCnt <= 16 Then
                        intGraphcnt = 16 - .intTcCnt
                    ElseIf .intCyCnt <= 20 Then
                        intGraphcnt = 20 - .intTcCnt
                    ElseIf .intCyCnt <= 24 Then
                        intGraphcnt = 24 - .intTcCnt
                    End If
                End With

                k = 0
                For j = 0 To gCstCodeOpsRowCountExhBar - 1

                    ''スタート配列番号
                    intStartRowIndex = (198 * i) + (4 * j)

                    With hudtOpsData(i).udtGraphData.udtGraphDataDetail(j)

                        'If j <= hudtOpsExhBar(i).intCyCnt - 1 Then
                        If j <= intGraphcnt - 1 Then

                            ''SYSTEM_NO（※OPS設定画面では未使用のためコメントアウト）
                            'intSystemNo = gConnect2Byte(mbyt22kFileArrayT12(16 + intStartRowIndex), mbyt22kFileArrayT12(17 + intStartRowIndex))

                            ''CH_NO(資料：CH_ID)
                            .strChno = gConnect2Byte(mbyt22kFileArrayT12(18 + intStartRowIndex), mbyt22kFileArrayT12(19 + intStartRowIndex)).ToString("0000")

                        Else
                            ''T/C CH No.
                            If hudtOpsExhBar(i).intTcCnt <> 0 then
                                If k <= hudtOpsExhBar(i).intTcCnt - 1 Then

                                    strValue = gConnect2Byte(mbyt22kFileArrayT12(18 + intStartRowIndex), mbyt22kFileArrayT12(19 + intStartRowIndex)).ToString("0000")
                                    If strValue <> "0000" Then
                                        hudtOpsData(i).udtGraphData.udtTcGraphInfo(k).strTcChNo = strValue
                                        k += 1
                                    End If

                                End If
                            End If

                        End If

                    End With

                Next j

                ''------------------------------
                '' 偏差CH
                ''------------------------------
                intRoopCntDev = 0
                For k = gCstCodeOpsRowCountExhBar To gCstCodeOpsRowCountExhBar + (gCstCodeOpsRowCountExhBar - 1)

                    ''スタート配列番号
                    intStartRowIndex = (198 * i) + (4 * k)

                    With hudtOpsData(i).udtGraphData.udtGraphDataDetail(intRoopCntDev)

                        ''SYSTEM_NO（※OPS設定画面では未使用のためコメントアウト）
                        'intSystemNo = gConnect2Byte(mbyt22kFileArrayT12(16 + intStartRowIndex), mbyt22kFileArrayT12(17 + intStartRowIndex))

                        ''CH_ID
                        .strExhBarChnoDev = gConnect2Byte(mbyt22kFileArrayT12(18 + intStartRowIndex), mbyt22kFileArrayT12(19 + intStartRowIndex)).ToString("0000")

                    End With

                    intRoopCntDev += 1

                Next k

                ''------------------------------
                '' 平均CH
                ''------------------------------
                With hudtOpsData(i).udtGraphData

                    ''スタート配列番号
                    intStartRowIndex = (198 * i) + 208

                    ''SYSTEM_NO（※OPS設定画面では未使用のためコメントアウト）
                    'intSystemNo = gConnect2Byte(mbyt22kFileArrayT12(0 + intStartRowIndex), mbyt22kFileArrayT12(1 + intStartRowIndex))

                    ''CH_ID
                    .strChNoAve = gConnect2Byte(mbyt22kFileArrayT12(2 + intStartRowIndex), mbyt22kFileArrayT12(3 + intStartRowIndex)).ToString("0000")

                End With

            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタの排気ガス・バーグラフ情報取得
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) OPS設定_CHデータ構造体
    ' 　　　    : ARG2 - (I ) ループIndex
    ' 機能説明  : 既設の排気ガス・バーグラフ情報をコンバート用OPS設定構造体へ格納
    '--------------------------------------------------------------------
    Private Sub mMakeOpsStructureT10(ByRef hudtOpsData As gTypConvertOpsGraphData, _
                                     ByVal intPageIndex As Integer)

        Try

            Dim i As Integer
            Dim intStartRowIndex As Integer     ''データを読込む配列番号の初期値
            Dim intDivDevHigh As Integer        ''偏差　目盛上限値
            Dim bytArray() As Byte
            Dim intTcStartPos As Integer        ''T/Cタイトル取得位置

            ''データの読込み開始配列番号
            intStartRowIndex = 280 * intPageIndex

            With hudtOpsData

                ''ページNo
                .intPageNo = mbyt22kFileArrayT10(8 + intStartRowIndex)

                ''バーグラフ本数（シリンダ本数）
                .intCyCnt = mbyt22kFileArrayT10(10 + intStartRowIndex)

                ''T/C本数（1～4）
                .intTcCnt = mbyt22kFileArrayT10(11 + intStartRowIndex)

                ''画面タイトル
                ReDim bytArray(25)
                For i = LBound(bytArray) To UBound(bytArray)
                    bytArray(i) = mbyt22kFileArrayT10(12 + intStartRowIndex + i)
                Next
                .strTitle = mByte2String(26, bytArray)

                ''偏差　目盛上限値
                intDivDevHigh = gConnect2Byte(mbyt22kFileArrayT10(46 + intStartRowIndex), mbyt22kFileArrayT10(47 + intStartRowIndex))
                If (intDivDevHigh > 0) And (intDivDevHigh < 255) Then
                    .intDevMark = intDivDevHigh
                Else
                    .intDevMark = 0
                End If


                ''バーグラフ種類　※既設と新設で割当て数値が1つズレている点注意
                ''　[既] 0：4分割, 1：6分割, 2：3×5分割   
                ''　[新] 1：4分割, 2：6分割, 3：3×5分割
                .intDevision = mbyt22kFileArrayT10(52 + intStartRowIndex) + 1


                ''20Graph設定/Line設定
                If .intCyCnt > 20 Then  ''24分割は2Line表示
                    .int20Graph = 0
                    .intLine = gCstCodeOpsExhGraphLine2
                Else
                    ''（0：20Graph未使用、2：20Graph使用で2Line選択、3：20Graph使用で1Line選択）
                    Select Case mbyt22kFileArrayT10(54 + intStartRowIndex)
                        Case 0
                            .int20Graph = 0
                            .intLine = gCstCodeOpsExhGraphLine1
                        Case 2
                            .int20Graph = 0
                            .intLine = gCstCodeOpsExhGraphLine2
                        Case 3
                            .int20Graph = 0
                            .intLine = gCstCodeOpsExhGraphLine1
                        Case Else
                            .int20Graph = 0
                            .intLine = gCstCodeOpsExhGraphLine1
                    End Select
                End If


                ''データ名称（上段）
                ReDim bytArray(3)
                For i = LBound(bytArray) To UBound(bytArray)
                    bytArray(i) = mbyt22kFileArrayT10(56 + intStartRowIndex + i)
                Next
                .strItemUp = mByte2String(4, bytArray)
                

                ''データ名称（下段）
                ReDim bytArray(3)
                For i = LBound(bytArray) To UBound(bytArray)
                    bytArray(i) = mbyt22kFileArrayT10(60 + intStartRowIndex + i)
                Next
                .strItemDown = mByte2String(4, bytArray)


                ''===============================
                '' グラフ設定
                ''===============================
                ''既設構造：|ClC|C|･・・|T|T|T|
                ''                       ∟T/C数は可変
                For i = 0 To CInt(gCstCodeOpsRowCountExhBar - 1)

                    With hudtOpsData.udtGraphDataDetail(i)

                        ''タイトル
                        ReDim bytArray(3)
                        For j = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayT10((64 + 8 * i) + intStartRowIndex + j)
                        Next
                        .strExhBarTitle = mByte2String(4, bytArray)

                    End With

                Next

                ''T/C　画面タイトル
                ReDim bytArray(15)
                For i = LBound(bytArray) To UBound(bytArray)
                    bytArray(i) = mbyt22kFileArrayT10(256 + intStartRowIndex + i)
                Next
                .strTcTitle = mByte2String(16, bytArray)

                ''T/C　コメント1
                ReDim bytArray(3)
                For i = LBound(bytArray) To UBound(bytArray)
                    bytArray(i) = mbyt22kFileArrayT10(272 + intStartRowIndex + i)
                Next
                .strTcComm1 = mByte2String(4, bytArray)

                ''T/C　コメント2
                ReDim bytArray(3)
                For i = LBound(bytArray) To UBound(bytArray)
                    bytArray(i) = mbyt22kFileArrayT10(276 + intStartRowIndex + i)
                Next
                .strTcComm2 = mByte2String(4, bytArray)


                ''T/C グラフ情報
                ''バーグラフ本数（シリンダ本数）によりT/Cタイトル取得位置可変
                If (.intCyCnt + .intTcCnt) <= 12 Then
                    intTcStartPos = 8 * (12 - .intTcCnt) + 64
                ElseIf .intCyCnt <= 16 Then
                    intTcStartPos = 8 * (16 - .intTcCnt) + 64
                ElseIf .intCyCnt <= 20 Then
                    intTcStartPos = 8 * (20 - .intTcCnt) + 64
                ElseIf .intCyCnt <= 24 Then
                    intTcStartPos = 8 * (24 - .intTcCnt) + 64
                End If

                'For i = 0 To gCstCodeOpsRowCountTC - 1
                If .intTcCnt <> 0 Then
                    For i = 0 To .intTcCnt - 1

                        With hudtOpsData.udtTcGraphInfo(i)

                            ReDim bytArray(3)
                            For j = LBound(bytArray) To UBound(bytArray)
                                bytArray(j) = mbyt22kFileArrayT10((intTcStartPos + 8 * i) + intStartRowIndex + j)
                            Next
                            .strTcTitle = mByte2String(4, bytArray)

                            ''T/C グラフ情報(分割線)
                            Select Case mbyt22kFileArrayT10(53 + intStartRowIndex)
                                Case 0
                                    .intTcSplit = 0
                                Case 1
                                    If i = hudtOpsData.intTcCnt - 2 Then
                                        .intTcSplit = 1
                                    Else
                                        .intTcSplit = 0
                                    End If
                                Case 2
                                    If i = hudtOpsData.intTcCnt - 3 Then
                                        .intTcSplit = 1
                                    Else
                                        .intTcSplit = 0
                                    End If
                                Case 3
                                    If i = hudtOpsData.intTcCnt - 4 Then
                                        .intTcSplit = 1
                                    Else
                                        .intTcSplit = 0
                                    End If
                            End Select

                        End With

                    Next
                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタのアナログメータ情報取得
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) コンバート用OPS設定構造体
    ' 　　　    : ARG2 - (I ) ループIndex
    ' 機能説明  : 既設のアナログメータ情報をコンバート用OPS設定構造体へ格納
    '--------------------------------------------------------------------
    Private Sub mMakeOpsStructureT11(ByRef hudtOpsData As gTypConvertOpsGraphData, _
                                     ByVal intPageIndex As Integer)

        Try

            Dim intStartRowIndex As Integer     ''データを読込む配列番号の初期値
            Dim bytArray() As Byte


            ''データの読込み開始配列番号
            intStartRowIndex = 64 * intPageIndex

            ''=======================================================================
            '' アナログメーター設定（設定は1ヶ所確認すれば良し）
            '' ※'CH名称表示位置'と'目盛数値'は既設と新設で値が1つズレているので注意
            ''=======================================================================
            With hudtOpsData.udtAnalogMeterSetting

                .intChNameDisplayPoint = mbyt22kFileArrayT11(2) + 1                     ''CH名称 表示位置   （[新] 1：左詰め, 2：中央, 3：右詰め）
                .intMarkNumericalValue = mbyt22kFileArrayT11(3) + 1                     ''目盛数値          （[新] 1：ノーマル　2：短縮）
                .intPointerFrame = IIf(gBitCheck(mbyt22kFileArrayT11(4), 5), 1, 0)      ''指針に縁取りあり
                .intPointerColor = IIf(gBitCheck(mbyt22kFileArrayT11(4), 4), 1, 0)      ''指針色変更
                '.intSideColorSymbol = IIf(gBitCheck(mbyt22kFileArrayT11(4), 0), 1, 0)   ''名称横の色シンボル表示

            End With

            ''=======================================================================
            '' アナログメーター
            ''=======================================================================
            With hudtOpsData

                ''ページNO.
                .intPageNo = mbyt22kFileArrayT11(16 + intStartRowIndex)

                ''メーター種類
                .intMeterType = mbyt22kFileArrayT11(17 + intStartRowIndex)

                ''画面タイトル
                ReDim bytArray(25)
                For i As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(i) = mbyt22kFileArrayT11(20 + intStartRowIndex + i)
                Next
                .strTitle = mByte2String(26, bytArray)


                ''----------------------------
                '' アナログメーター詳細
                ''----------------------------
                For i = 0 To CInt(gCstCodeOpsRowCountAnalog - 1)

                    With hudtOpsData.udtGraphDataDetail(i)

                        .intAnalogScale = mbyt22kFileArrayT11((48 + 4 * i) + intStartRowIndex)  ''目盛分割数
                        .intAnalogColor = mbyt22kFileArrayT11((49 + 4 * i) + intStartRowIndex)  ''表示色

                    End With

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : コンバート用OPS構造体へデータ転送
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) コンバート用のOPS設定構造体
    ' 　　　    : ARG2 - (IO) 既設エディタのデータを読み込んだ構造体
    ' 　　　    : ARG3 - (I ) グラフタイプ  （TRUE：アナログメーター、FALSE：その他）
    ' 機能説明  : 仮の既設データ読込み用構造体からOPS設定構造体へデータを移す
    '--------------------------------------------------------------------
    Private Sub mMakeStructure(ByRef hudtOpsData As gTypConvertOps, _
                               ByRef hudtOpsByteStructure As gTypConvertOpsGraphData, _
                      Optional ByVal hblnGraphType As Integer = False)

        Try

            Dim i As Integer

            With hudtOpsData.udtGraphData

                ''アナログメーターの場合
                If hblnGraphType Then

                    ''アナログメーター設定
                    With .udtAnalogMeterSetting

                        .intChNameDisplayPoint = hudtOpsByteStructure.udtAnalogMeterSetting.intChNameDisplayPoint   ''CH名称 表示位置
                        .intMarkNumericalValue = hudtOpsByteStructure.udtAnalogMeterSetting.intMarkNumericalValue   ''目盛数値
                        .intPointerFrame = hudtOpsByteStructure.udtAnalogMeterSetting.intPointerFrame               ''指針に縁取りあり
                        .intPointerColor = hudtOpsByteStructure.udtAnalogMeterSetting.intPointerColor               ''指針色変更
                        '.intSideColorSymbol = hudtOpsByteStructure.udtAnalogMeterSetting.intSideColorSymbol         ''名称横の色シンボル表示

                    End With

                    ''アナログメーター
                    .strTitle = hudtOpsByteStructure.strTitle
                    .intMeterType = hudtOpsByteStructure.intMeterType

                    ''アナログメーター設定詳細
                    For i = LBound(hudtOpsByteStructure.udtGraphDataDetail) To gCstCodeOpsRowCountAnalog - 1

                        With hudtOpsData.udtGraphData.udtGraphDataDetail(i)

                            .intAnalogScale = hudtOpsByteStructure.udtGraphDataDetail(i).intAnalogScale
                            .intAnalogColor = hudtOpsByteStructure.udtGraphDataDetail(i).intAnalogColor

                        End With

                    Next

                Else

                    .strTitle = hudtOpsByteStructure.strTitle
                    .strItemUp = hudtOpsByteStructure.strItemUp
                    .strItemDown = hudtOpsByteStructure.strItemDown

                    .strTcTitle = hudtOpsByteStructure.strTcTitle
                    .strTcComm1 = hudtOpsByteStructure.strTcComm1
                    .strTcComm2 = hudtOpsByteStructure.strTcComm2

                    .intCyCnt = hudtOpsByteStructure.intCyCnt
                    .intTcCnt = hudtOpsByteStructure.intTcCnt

                    .intDevision = hudtOpsByteStructure.intDevision
                    .intDevMark = hudtOpsByteStructure.intDevMark
                    '.int20Graph = hudtOpsByteStructure.int20Graph
                    .intLine = hudtOpsByteStructure.intLine

                    ''グラフ設定詳細
                    For i = LBound(hudtOpsByteStructure.udtGraphDataDetail) To UBound(hudtOpsByteStructure.udtGraphDataDetail)

                        With hudtOpsData.udtGraphData.udtGraphDataDetail(i)

                            .strExhBarTitle = hudtOpsByteStructure.udtGraphDataDetail(i).strExhBarTitle

                        End With

                    Next

                    For i = LBound(hudtOpsByteStructure.udtTcGraphInfo) To UBound(hudtOpsByteStructure.udtTcGraphInfo)

                        With hudtOpsData.udtGraphData.udtTcGraphInfo(i)

                            .strTcTitle = hudtOpsByteStructure.udtTcGraphInfo(i).strTcTitle

                        End With
                    Next

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 画面の概要名称設定
    ' 返り値    : 画面名称
    ' 引き数    : ARG1 - (I ) グラフコード 
    ' 機能説明  : 画面コードだとグラフタイプが分かりにくいため、名称を取得
    '--------------------------------------------------------------------
    Private Function SetGraphType(ByVal intHexGraphType As Integer) As String

        Dim strRtn As String = ""

        Try

            Select Case intHexGraphType

                Case mCstCodeOpsGraphTypeNothing : strRtn = ""
                Case mCstCodeOpsGraphTypeExhaust : strRtn = "ExhaustGas"
                Case mCstCodeOpsGraphTypeBarNormal : strRtn = "Bar"
                Case mCstCodeOpsGraphTypeBarPercent : strRtn = "Bar"
                Case mCstCodeOpsGraphTypeAnalogMeter : strRtn = "AnalogMeter"
                Case mCstCodeOpsGraphTypeBar6dig : strRtn = "Bar"
                Case mCstCodeOpsGraphTypeBar6digPercent : strRtn = "Bar"

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

#End Region

#Region "グローバル構造体へデータ設定"

    '--------------------------------------------------------------------
    ' 機能      : グローバル構造体へデータ設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) OPS設定_CHデータ構造体
    ' 機能説明  : 既設のOPS設定情報をグローバル構造体に設定
    '--------------------------------------------------------------------
    Private Sub mSetStructureOps(ByVal hudtOpsData() As gTypConvertOps)

        Try

            For i = LBound(hudtOpsData) To UBound(hudtOpsData)

                With hudtOpsData(i)

                    ''グラフ設定していれば、詳細情報をグローバル構造体に設定
                    If .intGraphType <> 0 Then

                        ''グラフの種類によって設定処理を分ける（1→排気ガス、16→アナログメータ、その他→バーグラフ）
                        Select Case .intGraphType

                            Case mCstCodeOpsGraphTypeExhaust

                                ''排気ガスのグローバル構造体へデータ設定
                                Call mSetStructureOpsGraphExhaust(gudt.SetOpsGraphM.udtGraphExhaustRec(i), hudtOpsData(i).udtGraphData, i)

                                ''グラフタイトルのグローバル構造体へデータ設定
                                Call mSetStructureOpsTitle(gudt.SetOpsGraphM.udtGraphTitleRec, _
                                                           gudt.SetOpsGraphM, _
                                                           i, _
                                                           mCstCodeOpsGraphTypeExhaust)

                            Case mCstCodeOpsGraphTypeBarNormal, _
                                 mCstCodeOpsGraphTypeBarPercent, _
                                 mCstCodeOpsGraphTypeBar6dig, _
                                 mCstCodeOpsGraphTypeBar6digPercent

                                ''バーグラフのグローバル構造体へデータ設定
                                Call mSetStructureOpsGraphBar(gudt.SetOpsGraphM.udtGraphBarRec, hudtOpsData(i).udtGraphData, i, .intGraphType)

                                ''グラフタイトルのグローバル構造体へデータ設定
                                Call mSetStructureOpsTitle(gudt.SetOpsGraphM.udtGraphTitleRec, _
                                                           gudt.SetOpsGraphM, _
                                                           i, _
                                                           mCstCodeOpsGraphTypeBarNormal)

                            Case mCstCodeOpsGraphTypeAnalogMeter

                                ''アナログメーター、アナログメーター設定のグローバル構造体へデータ設定
                                Call mSetStructureOpsGraphAnalog(gudt.SetOpsGraphM, hudtOpsData(i).udtGraphData, i)

                                ''グラフタイトルのグローバル構造体へデータ設定
                                Call mSetStructureOpsTitle(gudt.SetOpsGraphM.udtGraphTitleRec, _
                                                           gudt.SetOpsGraphM, _
                                                           i, _
                                                           mCstCodeOpsGraphTypeAnalogMeter)

                        End Select

                    End If

                End With

            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : グラフタイトル設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) グラフタイトル構造体
    '           : ARG2 - (I ) グラフ設定構造体
    '           : ARG3 - (I ) 設定するページ番号
    '           : ARG4 - (I ) グラフタイプ  （1：排気ガスグラフ、2：バーグラフ、16：アナログメータ）
    ' 機能説明  : 排気ガスのグラフタイトルをグラフタイトル構造体にコピー
    '--------------------------------------------------------------------
    Private Sub mSetStructureOpsTitle(ByRef hudtTarget() As gTypSetOpsGraphTitle, _
                                      ByVal hudtSource As gTypSetOpsGraph, _
                                      ByVal intPageNo As Integer, _
                                      ByVal intGraphType As Integer)

        Try

            With hudtTarget(intPageNo)

                Select Case intGraphType
                    Case mCstCodeOpsGraphTypeExhaust

                        .strName = hudtSource.udtGraphExhaustRec(intPageNo).strTitle    ''タイトル
                        .bytType = gCstCodeOpsTitleGraphTypeExhaust                     ''グラフタイプ

                    Case mCstCodeOpsGraphTypeAnalogMeter

                        .strName = hudtSource.udtGraphAnalogMeterRec(intPageNo).strTitle
                        .bytType = gCstCodeOpsTitleGraphTypeAnalogMeter

                    Case mCstCodeOpsGraphTypeBarNormal

                        .strName = hudtSource.udtGraphBarRec(intPageNo).strTitle
                        .bytType = gCstCodeOpsTitleGraphTypeBar

                End Select

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 排気ガスグラフ詳細をグローバル構造体へ設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) 排ガスグラフ構造体
    '           : ARG2 - (I ) コンバート様OPS設定構造体
    '           : ARG3 - (I ) ループで回している行番号
    ' 機能説明  : 既設の排気ガスグラフ情報をグローバル構造体へ設定
    '--------------------------------------------------------------------
    Private Sub mSetStructureOpsGraphExhaust(ByRef hudtTarget As gTypSetOpsGraphExhaust, _
                                             ByVal hudtSource As gTypConvertOpsGraphData, _
                                             ByVal intRowIndex As Integer)

        Try

            Dim intCylCnt As Integer = 0
            Dim intTcCnt As Integer = 0
            Dim intBlankRowCnt As Integer = 0

            With hudtTarget

                '.strTitle = gGetString(hudtSource.strTitle)         ''グラフタイトル
                '.strItemUp = gGetString(hudtSource.strItemUp)       ''データ名称（上段）
                '.strItemDown = gGetString(hudtSource.strItemDown)   ''データ名称（下段）
                '.strTcTitle = gGetString(hudtSource.strTcTitle)     ''T/C画面タイトル
                .strTitle = hudtSource.strTitle         ''グラフタイトル
                .strItemUp = hudtSource.strItemUp       ''データ名称（上段）
                .strItemDown = hudtSource.strItemDown   ''データ名称（下段）
                .strTcTitle = hudtSource.strTcTitle     ''T/C画面タイトル
                .strTcComm1 = hudtSource.strTcComm1     ''T/Cコメント1  2015.02.04
                .strTcComm2 = hudtSource.strTcComm2     ''T/Cコメント2  2015.02.04

                .shtAveCh = gGetString(hudtSource.strChNoAve)       ''平均CH_NO
                .bytDevMark = CCbyte(hudtSource.intDevMark)         ''偏差目盛 上下限値
                '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                '.byt20Graph = CCbyte(hudtSource.int20Graph)         ''グラフ20本区切り
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                .bytLine = CCbyte(hudtSource.intLine)               ''数値の分け方

                ''分割線は既設と新設で仕様が違うため、コンバート処理をスキップ

                ''===============================
                '' グラフ詳細設定
                ''===============================
                intCylCnt = CCInt(hudtSource.intCyCnt)
                intTcCnt = CCInt(hudtSource.intTcCnt)

                ''カウント値が範囲内の時だけ処理を実施する
                If (intCylCnt >= 0 And intCylCnt <= gCstCodeOpsRowCountExhBar) And _
                   (intTcCnt >= 0 And intTcCnt <= gCstCodeOpsRowCountAnalog) And _
                   ((intCylCnt + intTcCnt) >= 0 And (intCylCnt + intTcCnt) <= gCstCodeOpsRowCountExhBar) Then

                    .bytCyCnt = CCbyte(hudtSource.intCyCnt)     ''シリンダ数
                    .bytTcCnt = CCbyte(hudtSource.intTcCnt)     ''本数（1～4）

                    ''-------------------------------
                    '' シリンダグラフ情報
                    ''-------------------------------
                    For i = 0 To intCylCnt - 1

                        With hudtTarget.udtCylinder(i)

                            .shtChCylinder = CCShort(hudtSource.udtGraphDataDetail(i).strChno)              ''シリンダCH
                            .shtChDeviation = CCShort(hudtSource.udtGraphDataDetail(i).strExhBarChnoDev)    ''偏差CH
                            '.strTitle = gGetString(hudtSource.udtGraphDataDetail(i).strExhBarTitle)         ''タイトル
                            .strTitle = hudtSource.udtGraphDataDetail(i).strExhBarTitle        ''タイトル

                        End With

                    Next

                    ''-------------------------------
                    '' T/Cグラフ情報
                    ''-------------------------------
                    ''既設のCyl-CHとT/C-CHのデータ格納ルールについて。＠*****.t12
                    ''資料には『T/Cはシリンダの後に詰める』とあるが詰まっていない!!!

                    ''《格納例》
                    ''Cyl数　T/C数　空欄行数
                    '' 15　　 4   　　　1　→処理①
                    '' 16　　 4     　　0
                    ''--(ここで処理を分ける)-----------
                    '' 17　　 4     　　3　
                    '' 18　　 4     　　2　→処理②
                    '' 19　　 4     　　1
                    '' 20　　 4   　  　0

                    If (intCylCnt + intTcCnt) <= (gCstCodeOpsRowCountExhBar - intTcCnt) Then

                        ''処理①
                        intBlankRowCnt = (gCstCodeOpsRowCountExhBar - .bytTcCnt) - (.bytCyCnt + .bytTcCnt)  ''空欄行数設定
                        Call mSetTcInfo(hudtTarget, hudtSource, intBlankRowCnt)                             ''T/Cグラフ情報設定

                    Else

                        ''処理②
                        intBlankRowCnt = gCstCodeOpsRowCountExhBar - (.bytCyCnt + .bytTcCnt)                ''空欄行数設定
                        Call mSetTcInfo(hudtTarget, hudtSource, intBlankRowCnt)                             ''T/Cグラフ情報設定

                    End If

                Else

                    Call MessageBox.Show("There are invalid data in [" & mstrFileName & ".t12]", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub

                    ''範囲外のデータの時は、カウント値に0を設定して処理を抜ける
                    .bytCyCnt = 0
                    .bytTcCnt = 0

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try


    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 排気ガスグラフ詳細をグローバル構造体へ設定 T/C設定詳細
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) OPS設定 排気ガスグローバル構造体
    '           : ARG2 - (I ) OPS設定 コンバート用構造体
    '           : ARG3 - (I ) 空欄行数
    ' 機能説明  : 既設の排気ガスグラフ情報をグローバル構造体へ設定
    '--------------------------------------------------------------------
    Private Sub mSetTcInfo(ByRef hudtTarget As gTypSetOpsGraphExhaust, _
                           ByVal hudtSource As gTypConvertOpsGraphData, _
                           ByVal intBlankRowCnt As Integer)

        Try

            Dim i As Integer
            'Dim intLoopCntTc As Integer = 0
            'Dim intCylCnt As Integer = CCInt(hudtSource.intCyCnt)
            'Dim intTcCnt As Integer = CCInt(hudtSource.intTcCnt)

            With hudtTarget

                For i = 0 To hudtSource.intTcCnt - 1

                    .udtTurboCharger(i).shtChTurboCharger = CCShort(hudtSource.udtTcGraphInfo(i).strTcChNo)
                    .udtTurboCharger(i).strTitle = hudtSource.udtTcGraphInfo(i).strTcTitle

                Next

                ' ''「Cylinderチャンネル」と「T/Cチャンネル」間の空白行注意
                'For i = intCylCnt + intBlankRowCnt To ((intCylCnt + intBlankRowCnt) + intTcCnt) - 1

                '    With hudtTarget.udtTurboCharger(intLoopCntTc)

                '        .shtChTurboCharger = CCShort(hudtSource.udtGraphDataDetail(i).strChno)      ''シリンダCH
                '        .strTitle = gGetString(hudtSource.udtGraphDataDetail(i).strExhBarTitle)     ''タイトル

                '    End With

                '    intLoopCntTc += 1

                'Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : バーグラフ詳細をグローバル構造体へ設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) バーグラフ構造体
    '           : ARG2 - (I ) 設定するページ番号
    '           : ARG3 - (I ) ループで回している行番号
    ' 機能説明  : 既設のバーグラフ情報をグローバル構造体へ設定
    '--------------------------------------------------------------------
    Private Sub mSetStructureOpsGraphBar(ByRef hudtTarget() As gTypSetOpsGraphBar, _
                                         ByVal hudtSource As gTypConvertOpsGraphData, _
                                         ByVal intRowIndex As Integer, _
                                         ByVal intGraphType As Integer)

        Try

            With hudtTarget(intRowIndex)

                '.strTitle = gGetString(hudtSource.strTitle)         ''画面タイトル
                '.strItemUp = gGetString(hudtSource.strItemUp)       ''データ名称（上段）
                '.strItemDown = gGetString(hudtSource.strItemDown)   ''データ名称（下段）
                .strTitle = hudtSource.strTitle         ''画面タイトル
                .strItemUp = hudtSource.strItemUp       ''データ名称（上段）
                .strItemDown = hudtSource.strItemDown   ''データ名称（下段）
                '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph→表示切替に変更 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                '.byt20Graph = CCbyte(hudtSource.int20Graph)         ''グラフ20本区切り
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                If (intGraphType = mCstCodeOpsGraphTypeBarPercent) Or (intGraphType = mCstCodeOpsGraphTypeBar6digPercent) Then
                    .bytDisplay = 1     ''百分率(0-100%)表示
                Else
                    .bytDisplay = 0     ''計測レンジ表示
                End If

                .bytLine = CCbyte(hudtSource.intLine)               ''数値の分け方
                .bytDevision = CCbyte(hudtSource.intDevision)       ''バーグラフ種類
                .bytCyCnt = CCbyte(hudtSource.intCyCnt)             ''シリンダ本数


                ''-------------------------------
                '' シリンダグラフ情報
                ''-------------------------------
                For i = 0 To UBound(.udtCylinder)

                    With hudtTarget(intRowIndex).udtCylinder(i)

                        .shtChCylinder = CCShort(hudtSource.udtGraphDataDetail(i).strChno)      ''シリンダCH
                        '.strTitle = gGetString(hudtSource.udtGraphDataDetail(i).strExhBarTitle) ''タイトル
                        .strTitle = hudtSource.udtGraphDataDetail(i).strExhBarTitle ''タイトル

                    End With

                Next


            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : アナログメーター設定・詳細をグローバル構造体へ設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) OPS設定構造体
    '           : ARG2 - (I ) 設定するページ番号
    '           : ARG3 - (I ) ループで回している行番号
    ' 機能説明  : 既設のアナログメーター設定・情報をグローバル構造体へ設定
    '--------------------------------------------------------------------
    Private Sub mSetStructureOpsGraphAnalog(ByRef hudtTarget As gTypSetOpsGraph, _
                                            ByVal hudtSource As gTypConvertOpsGraphData, _
                                            ByVal intRowIndex As Integer)

        Try

            ''===============================
            '' アナログメーター設定 
            ''===============================
            With hudtTarget.udtGraphAnalogMeterSettingRec

                .bytChNameDisplayPoint = CCbyte(hudtSource.udtAnalogMeterSetting.intChNameDisplayPoint)     ''CH名称表示位置
                .bytMarkNumericalValue = CCbyte(hudtSource.udtAnalogMeterSetting.intMarkNumericalValue)     ''目盛数値
                .bytPointerFrame = CCbyte(hudtSource.udtAnalogMeterSetting.intPointerFrame)                 ''指針に縁取りあり
                .bytPointerColorChange = CCbyte(hudtSource.udtAnalogMeterSetting.intPointerColor)           ''指針色変更
                ''2011.06.09 仕様変更
                '.bytSideColorSymbol = CCbyte(hudtSource.udtAnalogMeterSetting.intSideColorSymbol)           ''名称横の色シンボル表示

            End With

            ''===============================
            '' アナログメーター
            ''===============================
            With hudtTarget.udtGraphAnalogMeterRec(intRowIndex)

                '.strTitle = gGetString(hudtSource.strTitle.Trim)    ''タイトル
                .strTitle = hudtSource.strTitle    ''タイトル
                .bytMeterType = CCbyte(hudtSource.intMeterType)     ''アナログメータータイプ

                ''-------------------------------
                '' メーター情報詳細
                ''-------------------------------
                For i As Integer = 0 To UBound(hudtTarget.udtGraphAnalogMeterRec(intRowIndex).udtDetail)

                    With hudtTarget.udtGraphAnalogMeterRec(intRowIndex).udtDetail(i)

                        .shtChNo = hudtSource.udtGraphDataDetail(i).strChno

                        ''目盛分割数
                        ''　既設レンジ：2～7 → 1、2の時は強制的に新設のデフォルト値3を設定
                        ''　新設レンジ：3～7
                        If (CCbyte(hudtSource.udtGraphDataDetail(i).intAnalogScale) = 0) Then
                            .bytScale = 0
                        ElseIf (CCbyte(hudtSource.udtGraphDataDetail(i).intAnalogScale) = 1) Or _
                               (CCbyte(hudtSource.udtGraphDataDetail(i).intAnalogScale) = 2) Then
                            .bytScale = 3
                        Else
                            .bytScale = CCbyte(hudtSource.udtGraphDataDetail(i).intAnalogScale)
                        End If

                        ''表示色
                        .bytColor = CCbyte(hudtSource.udtGraphDataDetail(i).intAnalogColor)

                    End With

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "プルダウンメニュー"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : プルダウンメニュー設定(T101)
    '           : 2015.01.20    2byte変換の追加、文字列のスペース削除
    '--------------------------------------------------------------------
    Private Sub msetStructureT101(ByRef udtSet As gTypSetOpsPulldownMenu)

        Try
            Dim bytArray() As Byte
            Dim intRecSize As Integer = 11984

            Call gSetComboBox(cmbWork, gEnmComboType.ctOpsPulldownColumn1)

            '**************************************
            'メインメニュー内容読込
            '**************************************
            For i As Integer = 0 To 11

                With udtSet.udtDetail(i)

                    ''メインメニュー名称
                    ReDim bytArray(11)
                    For j As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(j) = mbyt22kFileArrayT101(j + (i * intRecSize))
                    Next j
                    .strName = MojiMake(gGetString(mByte2String(12, bytArray), True), 12)

                    ''メインメニュー動作開始地点（左上X座標）
                    .tx = gConnect2ByteS(mbyt22kFileArrayT101(12 + (i * intRecSize)), _
                                        mbyt22kFileArrayT101(13 + (i * intRecSize)))

                    ''メインメニュー動作開始地点（左下Y座標）
                    .ty = gConnect2ByteS(mbyt22kFileArrayT101(14 + (i * intRecSize)), _
                                        mbyt22kFileArrayT101(15 + (i * intRecSize)))

                    ''メインメニュー動作開始地点（右上X座標）
                    .bx = gConnect2ByteS(mbyt22kFileArrayT101(16 + (i * intRecSize)), _
                                        mbyt22kFileArrayT101(17 + (i * intRecSize)))

                    ''メインメニュー動作開始地点（右下Y座標）
                    .by = gConnect2ByteS(mbyt22kFileArrayT101(18 + (i * intRecSize)), _
                                        mbyt22kFileArrayT101(19 + (i * intRecSize)))

                    ''OPS禁止フラグ（SMS50追加項目）
                    .OPSSTFLG1 = 0

                    ''OPS禁止フラグ（SMS50追加項目）
                    .OPSSTFLG2 = 0

                    ''予備（SMS50追加項目）
                    .bytMenuNo1 = 0

                    ''予備1（SMS50追加項目）
                    .Spare1 = 0

                    ''予備2（SMS50追加項目）
                    .Spare2 = 0

                    ''予備3（SMS50追加項目）
                    .Spare3 = 0

                    ''予備4（SMS50追加項目）
                    .Spare4 = 0

                    ''予備5（SMS50追加項目）
                    .Spare5 = 0

                    ''処理項目1
                    .bytMenuType = mbyt22kFileArrayT101(20 + (i * intRecSize))

                    ''グループメニュー番号
                    .Yobi1 = mbyt22kFileArrayT101(21 + (i * intRecSize))

                    ''グループメニュー番号（保持型）
                    .Yobi2 = mbyt22kFileArrayT101(22 + (i * intRecSize))

                    ''グループメニュー数
                    .bytMenuSet = mbyt22kFileArrayT101(23 + (i * intRecSize))

                    ''グループメニュー表示X位置
                    .groupviewx = gConnect2ByteS(mbyt22kFileArrayT101(24 + (i * intRecSize)), _
                                                mbyt22kFileArrayT101(25 + (i * intRecSize)))

                    ''グループメニュー表示Y位置
                    .groupviewy = gConnect2ByteS(mbyt22kFileArrayT101(26 + (i * intRecSize)), _
                                                mbyt22kFileArrayT101(27 + (i * intRecSize)))

                    ''グループメニュー横サイズ位置
                    .groupsizex = gConnect2ByteS(mbyt22kFileArrayT101(28 + (i * intRecSize)), _
                                                mbyt22kFileArrayT101(29 + (i * intRecSize)))

                    ''グループメニュー縦サイズ位置
                    .groupsizey = gConnect2ByteS(mbyt22kFileArrayT101(30 + (i * intRecSize)), _
                                                mbyt22kFileArrayT101(31 + (i * intRecSize)))

                    '**************************************
                    'サブグループ内容読込
                    '**************************************
                    For j As Integer = 0 To 11

                        With udtSet.udtDetail(i).udtGroup(j)

                            ''サブメニューグループ名称
                            ReDim bytArray(23)
                            For ii As Integer = LBound(bytArray) To UBound(bytArray)
                                bytArray(ii) = mbyt22kFileArrayT101(32 + (i * intRecSize) + (j * 996) + ii)
                            Next
                            .strName = MojiMake(gGetString(mByte2String(24, bytArray), True), 24)

                            ''グループメニュー動作開始地点(左上X座標))
                            .grouptx = gConnect2ByteS(mbyt22kFileArrayT101(56 + (i * intRecSize) + (j * 996)), _
                                                      mbyt22kFileArrayT101(57 + (i * intRecSize) + (j * 996)))

                            ''グループメニュー動作開始地点(左上Y座標))
                            .groupty = gConnect2ByteS(mbyt22kFileArrayT101(58 + (i * intRecSize) + (j * 996)), _
                                                      mbyt22kFileArrayT101(59 + (i * intRecSize) + (j * 996)))

                            ''グループメニュー動作開始地点(右下X座標))
                            .groupbx = gConnect2ByteS(mbyt22kFileArrayT101(60 + (i * intRecSize) + (j * 996)), _
                                                      mbyt22kFileArrayT101(61 + (i * intRecSize) + (j * 996)))

                            ''グループメニュー動作開始地点(右下X座標))
                            .groupby = gConnect2ByteS(mbyt22kFileArrayT101(62 + (i * intRecSize) + (j * 996)), _
                                                      mbyt22kFileArrayT101(63 + (i * intRecSize) + (j * 996)))

                            ''予備（SMS50追加項目）
                            .groupSpare1 = 0

                            ''予備（SMS50追加項目）
                            .groupSpare2 = 0

                            ''予備（SMS50追加項目）
                            .groupSpare3 = 0

                            ''予備（SMS50追加項目）
                            .groupSpare4 = 0

                            ''処理項目1(未使用)
                            .groupbytMenuType = mbyt22kFileArrayT101(64 + (i * intRecSize) + (j * 996))

                            ''サブメニュー番号(未使用)
                            .SubgroupYobi1 = mbyt22kFileArrayT101(65 + (i * intRecSize) + (j * 996))

                            ''サブメニュー番号(保持型)(未使用)
                            .SubgroupYobi2 = mbyt22kFileArrayT101(66 + (i * intRecSize) + (j * 996))

                            ''サブメニュー数
                            .bytCount = mbyt22kFileArrayT101(67 + (i * intRecSize) + (j * 996))

                            ''サブメニュー表示X位置
                            .Subviewx = gConnect2ByteS(mbyt22kFileArrayT101(68 + (i * intRecSize) + (j * 996)), _
                                                       mbyt22kFileArrayT101(69 + (i * intRecSize) + (j * 996)))

                            ''サブメニュー表示Y位置
                            .Subviewy = gConnect2ByteS(mbyt22kFileArrayT101(70 + (i * intRecSize) + (j * 996)), _
                                                       mbyt22kFileArrayT101(71 + (i * intRecSize) + (j * 996)))

                            ''サブメニュー横サイズ位置
                            .Subsizex = gConnect2ByteS(mbyt22kFileArrayT101(72 + (i * intRecSize) + (j * 996)), _
                                                       mbyt22kFileArrayT101(73 + (i * intRecSize) + (j * 996)))

                            ''サブメニュー縦サイズ位置
                            .Subsizey = gConnect2ByteS(mbyt22kFileArrayT101(74 + (i * intRecSize) + (j * 996)), _
                                                       mbyt22kFileArrayT101(75 + (i * intRecSize) + (j * 996)))

                            '**************************************
                            'サブメニュー内容読込
                            '**************************************
                            For k As Integer = 0 To 16

                                With udtSet.udtDetail(i).udtGroup(j).udtSub(k)

                                    ''サブメニューグループ名称
                                    ReDim bytArray(31)
                                    For jj As Integer = LBound(bytArray) To UBound(bytArray)
                                        bytArray(jj) = mbyt22kFileArrayT101(76 + (i * intRecSize) + (j * 996) + (k * 56) + jj)
                                    Next
                                    .strName = MojiMake(gGetString(mByte2String(32, bytArray), True), 32)

                                    ''処理項目1
                                    .SubbytMenuType1 = mbyt22kFileArrayT101(108 + (i * intRecSize) + (j * 996) + (k * 56))

                                    ''処理項目2
                                    .SubbytMenuType2 = mbyt22kFileArrayT101(109 + (i * intRecSize) + (j * 996) + (k * 56))

                                    ''処理項目3
                                    .SubbytMenuType3 = mbyt22kFileArrayT101(110 + (i * intRecSize) + (j * 996) + (k * 56))

                                    ''処理項目4
                                    .SubbytMenuType4 = mbyt22kFileArrayT101(111 + (i * intRecSize) + (j * 996) + (k * 56))

                                    ''画面モード(未使用)
                                    .SubYobi1 = mbyt22kFileArrayT101(112 + (i * intRecSize) + (j * 996) + (k * 56))

                                    ''表示位置(未使用)
                                    .SubYobi2 = mbyt22kFileArrayT101(113 + (i * intRecSize) + (j * 996) + (k * 56))

                                    ''予備(未使用)
                                    .bytKeyCode = mbyt22kFileArrayT101(114 + (i * intRecSize) + (j * 996) + (k * 56))

                                    ''予備(未使用)
                                    .SubYobi4 = mbyt22kFileArrayT101(115 + (i * intRecSize) + (j * 996) + (k * 56))

                                    ''画面番号0
                                    .ViewNo1 = gConnect2ByteS(mbyt22kFileArrayT101(116 + (i * intRecSize) + (j * 996) + (k * 56)), _
                                                              mbyt22kFileArrayT101(117 + (i * intRecSize) + (j * 996) + (k * 56)))

                                    ''画面番号1(未使用)
                                    .ViewNo2 = gConnect2ByteS(mbyt22kFileArrayT101(118 + (i * intRecSize) + (j * 996) + (k * 56)), _
                                                              mbyt22kFileArrayT101(119 + (i * intRecSize) + (j * 996) + (k * 56)))

                                    ''画面番号2(未使用)
                                    .ViewNo3 = gConnect2ByteS(mbyt22kFileArrayT101(120 + (i * intRecSize) + (j * 996) + (k * 56)), _
                                                              mbyt22kFileArrayT101(121 + (i * intRecSize) + (j * 996) + (k * 56)))

                                    ''画面番号3(未使用)
                                    .ViewNo4 = gConnect2ByteS(mbyt22kFileArrayT101(122 + (i * intRecSize) + (j * 996) + (k * 56)), _
                                                              mbyt22kFileArrayT101(123 + (i * intRecSize) + (j * 996) + (k * 56)))

                                    ''サブメニュー動作開始地点(左上X座標))
                                    .SubMenutx = gConnect2ByteS(mbyt22kFileArrayT101(124 + (i * intRecSize) + (j * 996) + (k * 56)), _
                                                                mbyt22kFileArrayT101(125 + (i * intRecSize) + (j * 996) + (k * 56)))

                                    ''サブメニュー動作開始地点(左上Y座標))
                                    .SubMenuty = gConnect2ByteS(mbyt22kFileArrayT101(126 + (i * intRecSize) + (j * 996) + (k * 56)), _
                                                                mbyt22kFileArrayT101(127 + (i * intRecSize) + (j * 996) + (k * 56)))

                                    ''サブメニュー動作開始地点(右下X座標))
                                    .SubMenubx = gConnect2ByteS(mbyt22kFileArrayT101(128 + (i * intRecSize) + (j * 996) + (k * 56)), _
                                                                mbyt22kFileArrayT101(129 + (i * intRecSize) + (j * 996) + (k * 56)))

                                    ''サブメニュー動作開始地点(右下X座標))
                                    .SubMenuby = gConnect2ByteS(mbyt22kFileArrayT101(130 + (i * intRecSize) + (j * 996) + (k * 56)), _
                                                                mbyt22kFileArrayT101(131 + (i * intRecSize) + (j * 996) + (k * 56)))

                                End With
                            Next

                        End With
                    Next
                End With
            Next


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "セレクションメニュー"

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : セレクションメニュー設定(T108)
    '--------------------------------------------------------------------
    Private Sub msetStructureT108(ByRef udtSet As gTypSetOpsSelectionMenu)

        Try
            Dim bytArray() As Byte

            Dim bytView1 As Byte
            Dim bytView2 As Byte

            Dim VNo As Integer = 0

            '**************************************
            '画面番号内容読込
            '**************************************
            For i As Integer = 0 To 401 Step 2

                With udtSet.udtOpsSelectionOffSetRec(VNo)

                    ''画面番号1～201
                    bytView1 = mbyt22kFileArrayT108(i)
                    bytView2 = mbyt22kFileArrayT108(i + 1)
                    .ViewNo = gConnect2Byte(bytView1, bytView2)

                    VNo = VNo + 1
                End With
            Next

            For j As Integer = 0 To 79

                With udtSet.udtOpsSelectionSetViewRec(j)

                    ''セレクション名称(Editer内部のみ使用)
                    ReDim bytArray(7)
                    For ii As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(ii) = mbyt22kFileArrayT108(402 + (j * 88) + ii)
                    Next
                    .SelectName = mByte2String(8, bytArray)

                    For k As Integer = 0 To 9

                        With udtSet.udtOpsSelectionSetViewRec(j).udtKey(k)

                            ''処理項目1
                            .BytNameType1 = mbyt22kFileArrayT108(410 + (j * 88) + (k * 8))

                            ''処理項目2
                            .BytNameType2 = mbyt22kFileArrayT108(411 + (j * 88) + (k * 8))

                            ''処理項目3
                            .BytNameType3 = mbyt22kFileArrayT108(412 + (j * 88) + (k * 8))

                            ''処理項目4
                            .BytNameType4 = mbyt22kFileArrayT108(413 + (j * 88) + (k * 8))

                            ''画面番号
                            '.BytSelectName = mbyt22kFileArrayT108(414 + (j * 88) + (k * 8))
                            bytView1 = mbyt22kFileArrayT108(414 + (j * 88) + (k * 8))
                            bytView2 = mbyt22kFileArrayT108(415 + (j * 88) + (k * 8))

                            'If bytView1 <> 0 Or bytView2 <> 0 Then
                            '    MsgBox("a")
                            'End If


                            .BytSelectName = gConnect2ByteS(bytView1, bytView2)

                            ''名称コード
                            .NameCode = mbyt22kFileArrayT108(416 + (j * 88) + (k * 8))

                            ''予備
                            .Yobi1 = mbyt22kFileArrayT108(417 + (j * 88) + (k * 8))

                        End With
                    Next
                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : セレクションメニュー設定(T118)
    '--------------------------------------------------------------------
    Private Sub msetStructureT118(ByRef udtSet As gTypSetOpsSelectionMenu)

        Try
            Dim bytArray() As Byte

            For j As Integer = 0 To 99

                With udtSet.udtOpsSelectionMenuNameKeyRec(j)

                    ReDim bytArray(15)
                    For i As Integer = LBound(bytArray) To UBound(bytArray)
                        bytArray(i) = mbyt22kFileArrayT118(i + (j * 16))
                    Next
                    .SelectMenuKeyName = mByte2String(16, bytArray)
                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ログフォーマット"
    '--------------------------------------------------------------------
    ' 機能      : 既設エディタ情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ログフォーマット設定(LTT)
    '--------------------------------------------------------------------
    Private Sub msetStructureLTT()

        Try
            Dim strBuffer As String = "", str22kValue As String = ""
            Dim strCol1 As String = "", strCol2 As String = ""
            Dim intCnt As Integer = 0, intPageCnt As Integer = 0
            Dim intPart As Integer = 0

            If System.IO.File.Exists(mstr22kFilePathLTT) = False Then Exit Sub

            Using sr As New System.IO.StreamReader(mstr22kFilePathLTT, System.Text.Encoding.GetEncoding("Shift_JIS"))

                Do Until sr.EndOfStream

                    ''1行 READ
                    strBuffer = sr.ReadLine

                    str22kValue = Trim(strBuffer)

                    Call mLogFormat(str22kValue, strCol1, strCol2)

                    If strCol1 <> "DATA" And strCol2 <> "DATA" Then             ''パート切替
                        If intPart = 1 Then                                     ''パート判別(CARGO)
                            gudt.SetOpsLogFormatC.strCol1(intCnt) = strCol1
                            gudt.SetOpsLogFormatC.strCol2(intCnt) = strCol2
                        Else                                                    ''(MACHINERY)
                            gudt.SetOpsLogFormatM.strCol1(intCnt) = strCol1
                            gudt.SetOpsLogFormatM.strCol2(intCnt) = strCol2
                        End If
                    End If



                    If strCol1 = "CNTTITLE" Or strCol2 = "CNTTITLE" Then
                        ''次の行は空き
                        intCnt += 1 : intPageCnt += 1
                        If intCnt >= 600 Then Exit Do

                        If intPart = 1 Then                                 ''パート判別(CARGO)
                            gudt.SetOpsLogFormatC.strCol1(intCnt) = ""
                            gudt.SetOpsLogFormatC.strCol2(intCnt) = ""

                        Else                                                ''(MACHINERY)
                            gudt.SetOpsLogFormatM.strCol1(intCnt) = ""
                            gudt.SetOpsLogFormatM.strCol2(intCnt) = ""
                        End If


                    ElseIf strCol1 = "PAGE" Or strCol2 = "PAGE" Then
                        ''1Page = 50行
                        For i As Integer = intPageCnt + 1 To 49
                            intCnt += 1 : intPageCnt += 1
                            If intCnt >= 600 Then Exit Do

                            If intPart = 1 Then                             ''パート判別(CARGO)
                                gudt.SetOpsLogFormatC.strCol1(intCnt) = ""
                                gudt.SetOpsLogFormatC.strCol2(intCnt) = ""
                            Else                                            ''(MACHINERY)
                                gudt.SetOpsLogFormatM.strCol1(intCnt) = ""
                                gudt.SetOpsLogFormatM.strCol2(intCnt) = ""
                            End If

                        Next

                    ElseIf strCol1 = "DATA" Or strCol2 = "DATA" Then        ''パート切替
                        intPart = 1     '' CARGOパート

                    End If

                    If strCol1 = "DATA" Or strCol2 = "DATA" Then            ''パート切替
                        intCnt = 0
                        intPageCnt = 0
                    Else
                        intCnt += 1 : intPageCnt += 1
                    End If

                    If intPageCnt >= 50 Then intPageCnt = 0 ''1Pageは50行
                    If intCnt >= 600 Then Exit Do

                Loop

            End Using

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mLogFormat(ByVal str22k As String, ByRef strCol1 As String, ByRef strCol2 As String)

        Try
            Dim p As Integer

            p = str22k.IndexOf("/")
            If p < 0 Then
                ''1列
                Call mgetNewFormat(str22k, strCol1)
                strCol2 = ""

            ElseIf p = 0 Then
                strCol1 = ""
                Call mgetNewFormat(str22k.Substring(p + 1), strCol2)

            Else
                ''2列
                Call mgetNewFormat(str22k.Substring(0, p), strCol1)
                Call mgetNewFormat(str22k.Substring(p + 1), strCol2)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mgetNewFormat(ByVal str22kValue As String, ByRef strNewValue As String)

        Try

            strNewValue = ""

            If str22kValue = "@CNTTITLE" Then
                strNewValue = "CNTTITLE"

            ElseIf str22kValue = "@ANATITLE" Then
                strNewValue = "ANATITLE"

            ElseIf str22kValue.Substring(0, 3) = "@GR" Then
                strNewValue = "GR"
                strNewValue += CCInt(str22kValue.Substring(3)).ToString("00")

            ElseIf str22kValue.Substring(0, 1) = "*" Then
                strNewValue = "CH"
                strNewValue += Val(str22kValue.Substring(1)).ToString("0000")

            ElseIf str22kValue = "@SPLINE" Then
                strNewValue = "SPACE"

            ElseIf str22kValue = "@SPCE" Then
                strNewValue = "SPACE"

            ElseIf str22kValue = "@PAGE" Then
                strNewValue = "PAGE"

            ElseIf str22kValue = "@DATA" Then
                strNewValue = "DATA"

            Else
                strNewValue = ""
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region

#Region "延長警報設定"

    '--------------------------------------------------------------------
    ' 機能      : 既設延長警報情報をグローバル構造体に設定
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 延長警報設定()
    '--------------------------------------------------------------------
    Private Sub msetStructureExtAlarm(ByRef udtSet As gTypSetExtAlarm, _
                                      ByRef udtSetTm As gTypSetExtTimerSet, _
                                      ByRef udtSetTmName As gTypSetExtTimerName)

        Try
            Dim intValue As Integer = 0
            Dim intEnum As Integer = 0

            ''延長警報設定構造体
            With udtSet.udtExtAlarmCommon

                ''コンバイン設定
                .shtCombineSet = mbyt22kFileArrayEXT(18)

                ''Duty機能有無
                .shtDutyFunc = IIf(mbyt22kFileArrayEXT(864) = 0, 0, 1)

                ''川汽仕様(特殊仕様)設定
                .shtDutyMethod = IIf(mbyt22kFileArrayEXT(865) = 0, 0, 1)

                ''Group Effect 機能
                .shtEffect = mbyt22kFileArrayEXT(866)

                ''NVルール
                .shtNv = IIf(mbyt22kFileArrayEXT(867) = 0, 0, 1)

                ''Duty part選択
                If UBound(mbyt22kFileArrayEXT) >= 1620 Then
                    For i As Integer = 0 To 7
                        .shtPart1 = gBitSet(.shtPart1, i, IIf(gBitCheck(mbyt22kFileArrayEXT(1621), i), True, False))        ''1-8
                        .shtPart1 = gBitSet(.shtPart1, i + 8, IIf(gBitCheck(mbyt22kFileArrayEXT(1620), i), True, False))    ''9-15

                        .shtPart2 = gBitSet(.shtPart2, i, IIf(gBitCheck(mbyt22kFileArrayEXT(1623), i), True, False))        ''1-8
                        .shtPart2 = gBitSet(.shtPart2, i + 8, IIf(gBitCheck(mbyt22kFileArrayEXT(1622), i), True, False))    ''9-15
                    Next
                End If

                ''Eeengineer Call機能 
                .shtEeengineerCall = gBitSet(.shtEeengineerCall, 0, IIf(mbyt22kFileArrayEXT(880) = 0, False, True)) ''機能有無
                .shtEeengineerCall = gBitSet(.shtEeengineerCall, 1, IIf(mbyt22kFileArrayEXT(881) = 0, False, True)) ''選択SW有無
                .shtEeengineerCall = gBitSet(.shtEeengineerCall, 2, IIf(mbyt22kFileArrayEXT(882) = 0, False, True)) ''Accept

                If mbyt22kFileArrayEXT(884) <> 0 Then
                    .shtEeengineerCall = gBitSet(.shtEeengineerCall, 3, True) ''自動出力
                    .shtEeengineerCall = gBitSet(.shtEeengineerCall, 4, False) ''自動出力
                Else
                    If mbyt22kFileArrayEXT(880) <> 0 Then
                        .shtEeengineerCall = gBitSet(.shtEeengineerCall, 3, False) ''自動出力
                        .shtEeengineerCall = gBitSet(.shtEeengineerCall, 4, True) ''自動出力
                    Else
                        .shtEeengineerCall = gBitSet(.shtEeengineerCall, 3, False) ''自動出力
                        .shtEeengineerCall = gBitSet(.shtEeengineerCall, 4, False) ''自動出力
                    End If

                End If

                '▼▼▼ 20110330 ファイルデータ１７版対応に伴い強制的に0を設定 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                '.shtEeengineerCall = gBitSet(.shtEeengineerCall, 3, False) ''自動出力
                '.shtEeengineerCall = gBitSet(.shtEeengineerCall, 4, False) ''自動出力
                ''-----------------------------------------------------------------------------------------------------------

                ''.shtEeengineerCall = gBitSet(.shtEeengineerCall, 3, IIf(mbyt22kFileArrayEXT(883) = 0, True, False)) ''CallTimer
                ''.shtEeengineerCall = gBitSet(.shtEeengineerCall, 4, IIf(mbyt22kFileArrayEXT(884) = 0, True, False)) ''CallTimer通知
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                .shtEeengineerCall = gBitSet(.shtEeengineerCall, 5, IIf(mbyt22kFileArrayEXT(16) = 0, True, False)) ''Accept Pattern


                ''Patrol Man Call機能有無
                .shtPatrolCall = gBitSet(.shtPatrolCall, 0, IIf(mbyt22kFileArrayEXT(896) = 0, False, True)) ''機能有無
                .shtPatrolCall = gBitSet(.shtPatrolCall, 1, IIf(mbyt22kFileArrayEXT(897) = 0, False, True)) ''SW使用有無
                .shtPatrolCall = gBitSet(.shtPatrolCall, 2, IIf(mbyt22kFileArrayEXT(898) = 0, False, True)) ''Call set 出力方法
                .shtPatrolCall = gBitSet(.shtPatrolCall, 3, IIf(mbyt22kFileArrayEXT(899) = 0, False, True)) ''Alarm set 出力方法
                '.shtPatrolCall = gBitSet(.shtPatrolCall, 4, IIf(mbyt22kFileArrayEXT(900) = 0, False, True)) ''Dead Man シーケンス設定

                ''Dead Man Alarm 使用有無
                .shtDeadAlarm = IIf(mbyt22kFileArrayEXT(912) = 0, 0, 1)

                ''アラームランプ数
                .shtLamps = mbyt22kFileArrayEXT(1)

                ''ブザーパターン
                .shtBuzzer = mbyt22kFileArrayEXT(4)

                ''グループ出力パターン
                .shtGrpOut = mbyt22kFileArrayEXT(5)

                ''Group Effect 設定（12個）
                .shtGrpEffct = gBitSet(.shtGrpEffct, 0, IIf(gBitCheck(mbyt22kFileArrayEXT(9), 0), True, False))
                .shtGrpEffct = gBitSet(.shtGrpEffct, 1, IIf(gBitCheck(mbyt22kFileArrayEXT(9), 1), True, False))
                .shtGrpEffct = gBitSet(.shtGrpEffct, 2, IIf(gBitCheck(mbyt22kFileArrayEXT(9), 2), True, False))
                .shtGrpEffct = gBitSet(.shtGrpEffct, 3, IIf(gBitCheck(mbyt22kFileArrayEXT(9), 3), True, False))
                .shtGrpEffct = gBitSet(.shtGrpEffct, 4, IIf(gBitCheck(mbyt22kFileArrayEXT(9), 4), True, False))
                .shtGrpEffct = gBitSet(.shtGrpEffct, 5, IIf(gBitCheck(mbyt22kFileArrayEXT(9), 5), True, False))
                .shtGrpEffct = gBitSet(.shtGrpEffct, 6, IIf(gBitCheck(mbyt22kFileArrayEXT(9), 6), True, False))
                .shtGrpEffct = gBitSet(.shtGrpEffct, 7, IIf(gBitCheck(mbyt22kFileArrayEXT(9), 7), True, False))
                .shtGrpEffct = gBitSet(.shtGrpEffct, 8, IIf(gBitCheck(mbyt22kFileArrayEXT(10), 0), True, False))
                .shtGrpEffct = gBitSet(.shtGrpEffct, 9, IIf(gBitCheck(mbyt22kFileArrayEXT(10), 1), True, False))
                .shtGrpEffct = gBitSet(.shtGrpEffct, 10, IIf(gBitCheck(mbyt22kFileArrayEXT(10), 2), True, False))
                .shtGrpEffct = gBitSet(.shtGrpEffct, 11, IIf(gBitCheck(mbyt22kFileArrayEXT(10), 3), True, False))

                ''Fire Sound Group 設定（12個)
                .shtGrpFire = gBitSet(.shtGrpFire, 0, IIf(gBitCheck(mbyt22kFileArrayEXT(11), 0), True, False))
                .shtGrpFire = gBitSet(.shtGrpFire, 1, IIf(gBitCheck(mbyt22kFileArrayEXT(11), 1), True, False))
                .shtGrpFire = gBitSet(.shtGrpFire, 2, IIf(gBitCheck(mbyt22kFileArrayEXT(11), 2), True, False))
                .shtGrpFire = gBitSet(.shtGrpFire, 3, IIf(gBitCheck(mbyt22kFileArrayEXT(11), 3), True, False))
                .shtGrpFire = gBitSet(.shtGrpFire, 4, IIf(gBitCheck(mbyt22kFileArrayEXT(11), 4), True, False))
                .shtGrpFire = gBitSet(.shtGrpFire, 5, IIf(gBitCheck(mbyt22kFileArrayEXT(11), 5), True, False))
                .shtGrpFire = gBitSet(.shtGrpFire, 6, IIf(gBitCheck(mbyt22kFileArrayEXT(11), 6), True, False))
                .shtGrpFire = gBitSet(.shtGrpFire, 7, IIf(gBitCheck(mbyt22kFileArrayEXT(11), 7), True, False))
                .shtGrpFire = gBitSet(.shtGrpFire, 8, IIf(gBitCheck(mbyt22kFileArrayEXT(12), 0), True, False))
                .shtGrpFire = gBitSet(.shtGrpFire, 9, IIf(gBitCheck(mbyt22kFileArrayEXT(12), 1), True, False))
                .shtGrpFire = gBitSet(.shtGrpFire, 10, IIf(gBitCheck(mbyt22kFileArrayEXT(12), 2), True, False))
                .shtGrpFire = gBitSet(.shtGrpFire, 11, IIf(gBitCheck(mbyt22kFileArrayEXT(12), 3), True, False))

                ''ｸﾞﾙｰﾌﾟｱﾗｰﾑﾗﾝﾌﾟ出力　選択
                .shtGrpAlarm = IIf(mbyt22kFileArrayEXT(13) = 0, 0, 1)

                ''Fire ブザーパターン
                .shtFireBuzzer = IIf(mbyt22kFileArrayEXT(19) = 1, 0, 1)

                ''EXTグループアラーム出力設定
                For i As Integer = 0 To 11

                    For j As Integer = 0 To 7
                        .intGroupType(i) = gBitSet(.intGroupType(i), j, IIf(gBitCheck(mbyt22kFileArrayEXT(800 + i * 4), j), True, False))
                    Next

                    For j As Integer = 0 To 7
                        .intGroupType(i) = gBitSet(.intGroupType(i), j + 8, IIf(gBitCheck(mbyt22kFileArrayEXT(801 + i * 4), j), True, False))
                    Next

                    For j As Integer = 0 To 7
                        .intGroupType(i) = gBitSet(.intGroupType(i), j + 16, IIf(gBitCheck(mbyt22kFileArrayEXT(802 + i * 4), j), True, False))
                    Next

                Next

                ''Mach/Cargo
                If UBound(mbyt22kFileArrayEXT) >= 1616 Then
                    For k As Integer = 0 To 7
                        .intGroupType(k) = gBitSet(.intGroupType(k), 24, IIf(gBitCheck(mbyt22kFileArrayEXT(1617), k), True, False))
                        .intGroupType(k) = gBitSet(.intGroupType(k), 25, IIf(gBitCheck(mbyt22kFileArrayEXT(1619), k), True, False))
                    Next

                    For k As Integer = 0 To 3
                        .intGroupType(k + 8) = gBitSet(.intGroupType(k + 8), 24, IIf(gBitCheck(mbyt22kFileArrayEXT(1616), k), True, False))
                        .intGroupType(k + 8) = gBitSet(.intGroupType(k + 8), 25, IIf(gBitCheck(mbyt22kFileArrayEXT(1618), k), True, False))
                    Next
                End If

                '▼▼▼ 20110330 ファイルデータ１７版対応に伴い強制的に0を設定 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                ''特殊(川汽)仕様 (W/H)
                'intValue = mbyt22kFileArrayEXT(6)
                .shtSpecialWh = IIf(mbyt22kFileArrayEXT(6) = 0, 0, 13)

                ''特殊(川汽)仕様 (P/R)
                'intValue = mbyt22kFileArrayEXT(8)
                .shtSpecialPr = IIf(mbyt22kFileArrayEXT(8) = 0, 0, 14)

                ''特殊(川汽)仕様 (C/E)
                'intValue = mbyt22kFileArrayEXT(7)
                .shtSpecialCe = IIf(mbyt22kFileArrayEXT(7) = 0, 0, 15)
                '-----------------------------------------------------------------------------------------------------------
                ' ''特殊(川汽)仕様 (W/H)
                'intValue = mbyt22kFileArrayEXT(6)
                '.shtSpecialWh = intValue - 128

                ' ''特殊(川汽)仕様 (P/R)
                'intValue = mbyt22kFileArrayEXT(8)
                '.shtSpecialPr = intValue - 128

                ' ''特殊(川汽)仕様 (C/E)
                'intValue = mbyt22kFileArrayEXT(7)
                'If intValue > 128 Then
                '    .shtSpecialCe = intValue - 128
                'Else
                '    .shtSpecialCe = intValue
                'End If
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ''Eeengineer call 設定
                intValue = mbyt22kFileArrayEXT(885)
                .shtEngCall = IIf(intValue = 78, 0, intValue + 1)


                '' Ver1.8.7 2015.12.10  特殊設定追加　0を設定する
                .Option1 = 0
                .Option2 = 0
                .Option3 = 0

                ''LCD EXT グループ表示設定
                If UBound(mbyt22kFileArrayEXT) >= 2976 Then

                    For i As Integer = 0 To 11
                        ''警報グループNo.
                        .udtExtGroup(i).shtGroup = mbyt22kFileArrayEXT(2976 + 0 + (i * 14))

                        ''マーク番号
                        .udtExtGroup(i).shtMark = mbyt22kFileArrayEXT(2976 + 1 + (i * 14))

                        ''グループ名称
                        Dim bytArray(11) As Byte
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayEXT(2976 + 2 + (i * 14) + j)
                        Next j
                        .udtExtGroup(i).strGroupName = mByte2String(12, bytArray)
                    Next

                End If

                If UBound(mbyt22kFileArrayEXT) >= 2048 Then
                    For i As Integer = 0 To 29
                        ''DUTY名称(LCD設定)
                        Dim bytArray(2) As Byte
                        For j As Integer = LBound(bytArray) To UBound(bytArray)
                            bytArray(j) = mbyt22kFileArrayEXT(2048 + j + (i * 4))
                        Next j
                        .udtExtDuty(i).strDutyName = mByte2String(3, bytArray)
                    Next
                End If

            End With

            ''延長警報盤個別設定
            For i As Integer = 0 To 15

                With udtSet.udtExtAlarm(i)

                    ''延長警報パネル通信ID番号
                    If mbyt22kFileArrayEXT(32 + (i * 16)) = 255 Then            ''FF以降はID無し
                        intEnum = i + 1
                        .shtNo = 0
                    Else
                        If intEnum = 0 Then
                            .shtNo = mbyt22kFileArrayEXT(32 + (i * 16)) + 1     ''22K:0-15 → 1-16
                            udtSet.udtExtAlarmCommon.shtUse(i) = 1              ''使用フラグON   2013.08.07  K.Fujimoto
                        Else
                            .shtNo = 0
                        End If
                    End If

                    'Re Alarm設定有無
                    .shtReAlarm = IIf(mbyt22kFileArrayEXT(32 + 3 + (i * 16)) = 255, 1, 0)   'FF:有

                    'ブザーカット有無
                    .shtBuzzCut = IIf(mbyt22kFileArrayEXT(32 + 5 + (i * 16)) = 255, 1, 0)   'FF:有

                    'フリーエンジニア有無
                    .shtFreeEng = IIf(mbyt22kFileArrayEXT(32 + 7 + (i * 16)) = 255, 1, 0)   'FF:有

                    ''オプション
                    .shtOption = mbyt22kFileArrayEXT(32 + 8 + (i * 16))

                    ''パネルタイプ
                    .shtPanel = mbyt22kFileArrayEXT(32 + 9 + (i * 16))

                    ''パート設定
                    .shtPart = mbyt22kFileArrayEXT(1624 + i)

                    ''Eeengineer Call No 設定
                    intValue = mbyt22kFileArrayEXT(32 + 2 + (i * 16))
                    .shtEngNo = IIf(intValue > 15, 0, intValue + 1)

                    ''Duty 番号, Duty ブザーストップ　動作設定(1:全室  0:個別)
                    intValue = mbyt22kFileArrayEXT(32 + 1 + (i * 16))
                    If intValue > 128 Then
                        .shtDuty = intValue - 128
                        .shtDutyBuzz = 0
                    Else
                        .shtDuty = IIf(intValue = 78, 0, intValue)
                        .shtDutyBuzz = 1
                    End If

                    ''Watch LED 表示方法選択
                    .shtWatchLed = mbyt22kFileArrayEXT(32 + 6 + (i * 16))

                    ''LED表示方法選択
                    .shtLedOut = mbyt22kFileArrayEXT(32 + 4 + (i * 16))

                    ''BZ Delay Timer Set(LED12個分の遅延タイマ値)
                    For j As Integer = 0 To 11

                        .shtLedTimer(j) = gConnect2Byte(mbyt22kFileArrayEXT(289 + (i * 32) + (j * 2)), mbyt22kFileArrayEXT(288 + (i * 32) + (j * 2)))

                    Next

                End With

            Next

            If UBound(mbyt22kFileArrayEXT) >= 2640 Then
                For i As Integer = 0 To 19
                    With udtSet.udtExtAlarm(i)
                        ''表示位置(LCD設定)
                        For j As Integer = 0 To 7
                            .shtPosition(j) = mbyt22kFileArrayEXT(2640 + j + (i * 8))
                        Next
                    End With
                Next
            End If

            ''タイマ設定構造体
            If UBound(mbyt22kFileArrayEXT) >= 1792 Then

                With udtSetTm

                    Dim dblvalue As Double = 0

                    If mbyt22kFileArrayEXT(1792) = 1 Then

                        .udtTimerInfo(0).shtType = 3    ''種類:エンジニアコール
                        .udtTimerInfo(0).bytIndex = 1   ''タイマ表示名称テーブル レコード番号
                        .udtTimerInfo(0).bytPart = 0    ''パート　Mach      追加 ver.1.4.4 2012.05.08
                        .udtTimerInfo(0).shtTimeDisp = 1 ''分秒切替設定(22Kは分)

                        ''初期値
                        dblvalue = gConnect2Byte(mbyt22kFileArrayEXT(1796), mbyt22kFileArrayEXT(1797))
                        dblvalue = dblvalue * 0.1   ''単位は分
                        dblvalue = dblvalue * 60    ''秒 <-- 分
                        .udtTimerInfo(0).shtInit = dblvalue

                        ''下限値
                        dblvalue = gConnect2Byte(mbyt22kFileArrayEXT(1798), mbyt22kFileArrayEXT(1799))
                        dblvalue = dblvalue * 0.1   ''単位は分
                        dblvalue = dblvalue * 60    ''秒 <-- 分
                        .udtTimerInfo(0).shtLow = dblvalue

                        ''上限値
                        dblvalue = gConnect2Byte(mbyt22kFileArrayEXT(1800), mbyt22kFileArrayEXT(1801))
                        dblvalue = dblvalue * 0.1   ''単位は分
                        dblvalue = dblvalue * 60    ''秒 <-- 分
                        .udtTimerInfo(0).shtHigh = dblvalue

                        ''タイマ表示名称
                        udtSetTmName.udtTimerRec(0).strName = "ENGINEER CALL TIMER"

                    End If

                    If mbyt22kFileArrayEXT(1808) = 1 Then

                        .udtTimerInfo(1).shtType = 1    ''種類:デッドマンアラーム１
                        .udtTimerInfo(1).bytIndex = 2   ''タイマ表示名称テーブル レコード番号
                        .udtTimerInfo(1).bytPart = 0    ''パート　Mach      追加 ver.1.4.4 2012.05.08
                        .udtTimerInfo(1).shtTimeDisp = 1 ''分秒切替設定(22Kは分)

                        ''初期値
                        dblvalue = gConnect2Byte(mbyt22kFileArrayEXT(1812), mbyt22kFileArrayEXT(1813))
                        dblvalue = dblvalue * 0.1   ''単位は分
                        dblvalue = dblvalue * 60    ''秒 <-- 分
                        .udtTimerInfo(1).shtInit = dblvalue

                        ''下限値
                        dblvalue = gConnect2Byte(mbyt22kFileArrayEXT(1814), mbyt22kFileArrayEXT(1815))
                        dblvalue = dblvalue * 0.1   ''単位は分
                        dblvalue = dblvalue * 60    ''秒 <-- 分
                        .udtTimerInfo(1).shtLow = dblvalue

                        ''上限値
                        dblvalue = gConnect2Byte(mbyt22kFileArrayEXT(1816), mbyt22kFileArrayEXT(1817))
                        dblvalue = dblvalue * 0.1   ''単位は分
                        dblvalue = dblvalue * 60    ''秒 <-- 分
                        .udtTimerInfo(1).shtHigh = dblvalue

                        ''タイマ表示名称
                        udtSetTmName.udtTimerRec(1).strName = "DEAD MAN CALL 1 TIMER"

                    End If

                    If mbyt22kFileArrayEXT(1824) = 1 Then

                        .udtTimerInfo(2).shtType = 2    ''種類:デッドマンアラーム２
                        .udtTimerInfo(2).bytIndex = 3   ''タイマ表示名称テーブル レコード番号
                        .udtTimerInfo(2).bytPart = 0    ''パート　Mach      追加 ver.1.4.4 2012.05.08
                        .udtTimerInfo(2).shtTimeDisp = 1 ''分秒切替設定(22Kは分)

                        ''初期値
                        dblvalue = gConnect2Byte(mbyt22kFileArrayEXT(1828), mbyt22kFileArrayEXT(1829))
                        dblvalue = dblvalue * 0.1   ''単位は分
                        dblvalue = dblvalue * 60    ''秒 <-- 分
                        .udtTimerInfo(2).shtInit = dblvalue

                        ''下限値
                        dblvalue = gConnect2Byte(mbyt22kFileArrayEXT(1830), mbyt22kFileArrayEXT(1831))
                        dblvalue = dblvalue * 0.1   ''単位は分
                        dblvalue = dblvalue * 60    ''秒 <-- 分
                        .udtTimerInfo(2).shtLow = dblvalue

                        ''上限値
                        dblvalue = gConnect2Byte(mbyt22kFileArrayEXT(1832), mbyt22kFileArrayEXT(1833))
                        dblvalue = dblvalue * 0.1   ''単位は分
                        dblvalue = dblvalue * 60    ''秒 <-- 分
                        .udtTimerInfo(2).shtHigh = dblvalue

                        ''タイマ表示名称
                        udtSetTmName.udtTimerRec(2).strName = "DEAD MAN CALL 2 TIMER"

                    End If

                End With

            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "共通関数"

    ' -------------------------------------------------------------------
    ' 機能説明      : 既設エディタ情報を変数に設定
    ' 引数          : なし
    ' 戻り値        : なし
    ' -------------------------------------------------------------------
    Private Sub minit22kVals()

        Try

            ''設定ファイルの保存されているフォルダ名
            Dim intpathLen As Integer = lblOpenFile22k.Text.Split("\").Length
            Dim strfileNum As String = lblOpenFile22k.Text.Split("\")(intpathLen - 1)

            ''ファイル名 + 拡張子 ----------------------------------------------
            mstr22kFilePathT07 = lblOpenFile22k.Text & "\" & strfileNum & ".t07"
            mstr22kFilePathT08 = lblOpenFile22k.Text & "\" & strfileNum & ".t08"
            mstr22kFilePathT999 = lblOpenFile22k.Text & "\" & strfileNum & ".t999"
            mstr22kFilePathT09 = lblOpenFile22k.Text & "\" & strfileNum & ".t09"
            mstr22kFilePathT10 = lblOpenFile22k.Text & "\" & strfileNum & ".t10"
            mstr22kFilePathT11 = lblOpenFile22k.Text & "\" & strfileNum & ".t11"
            mstr22kFilePathT12 = lblOpenFile22k.Text & "\" & strfileNum & ".t12"
            mstr22kFilePathT20 = lblOpenFile22k.Text & "\" & strfileNum & ".t20"
            mstr22kFilePathT21 = lblOpenFile22k.Text & "\" & strfileNum & ".t21"
            mstr22kFilePathT22 = lblOpenFile22k.Text & "\" & strfileNum & ".t22"
            mstr22kFilePathT52 = lblOpenFile22k.Text & "\" & strfileNum & ".t52"
            mstr22kFilePathT25 = lblOpenFile22k.Text & "\" & strfileNum & ".t25"
            mstr22kFilePathT19 = lblOpenFile22k.Text & "\" & strfileNum & ".t19"
            mstr22kFilePathT46 = lblOpenFile22k.Text & "\" & strfileNum & ".t46"
            mstr22kFilePathSIO = lblOpenFile22k.Text & "\" & "SIO.DAT"
            mstr22kFilePathS1 = lblOpenFile22k.Text & "\" & strfileNum & ".S1"
            mstr22kFilePathS2 = lblOpenFile22k.Text & "\" & strfileNum & ".S2"
            mstr22kFilePathS3 = lblOpenFile22k.Text & "\" & strfileNum & ".S3"
            mstr22kFilePathS4 = lblOpenFile22k.Text & "\" & strfileNum & ".S4"
            mstr22kFilePathS5 = lblOpenFile22k.Text & "\" & strfileNum & ".S5"
            mstr22kFilePathS6 = lblOpenFile22k.Text & "\" & strfileNum & ".S6"
            mstr22kFilePathS7 = lblOpenFile22k.Text & "\" & strfileNum & ".S7"
            mstr22kFilePathS8 = lblOpenFile22k.Text & "\" & strfileNum & ".S8"
            mstr22kFilePathS9 = lblOpenFile22k.Text & "\" & strfileNum & ".S9"
            mstr22kFilePathT38 = lblOpenFile22k.Text & "\" & strfileNum & ".t38"
            mstr22kFilePathT36 = lblOpenFile22k.Text & "\" & strfileNum & ".t36"
            mstr22kFilePathT101 = lblOpenFile22k.Text & "\" & strfileNum & ".t101"

            mstr22kFilePathT108 = lblOpenFile22k.Text & "\" & strfileNum & ".t108"
            mstr22kFilePathT118 = lblOpenFile22k.Text & "\" & strfileNum & ".t118"

            'T.Ueki 2014/5/30 GWS追加
            mstr22kFilePathT701 = lblOpenFile22k.Text & "\" & strfileNum & ".t701"
            mstr22kFilePathT702 = lblOpenFile22k.Text & "\" & strfileNum & ".t702"

            mstr22kFilePathLTT = lblOpenFile22k.Text & "\" & strfileNum & ".LTT"

            mstr22kFilePathGrs = lblOpenFile22k.Text & "\" & strfileNum & ".grs"
            mstr22kFilePathMpt = lblOpenFile22k.Text & "\" & strfileNum & ".MPT"
            mstr22kFilePathUni = lblOpenFile22k.Text & "\" & strfileNum & ".uni"
            mstr22kFilePathRyt = lblOpenFile22k.Text & "\" & strfileNum & ".ryt"
            mstr22kFilePathCan = lblOpenFile22k.Text & "\" & strfileNum & ".can"
            mstr22kFilePathLpf = lblOpenFile22k.Text & "\" & strfileNum & ".lpf"
            mstr22kFilePathDST = lblOpenFile22k.Text & "\" & strfileNum & ".DST"
            mstr22kFilePathRin = lblOpenFile22k.Text & "\" & strfileNum & ".rin"
            mstr22kFilePathTei = lblOpenFile22k.Text & "\" & strfileNum & ".tei"
            mstr22kFilePathSks = lblOpenFile22k.Text & "\" & strfileNum & ".sks"
            mstr22kFilePathCmj = lblOpenFile22k.Text & "\" & strfileNum & ".cmj"
            ''------------------------------------------------------------------

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 既設の22kファイル読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) ファイルパス
    ' 　　　    : ARG2 - ( O) ファイル情報格納Byte配列
    ' 機能説明  : 既設の22kファイルをByte配列に格納する
    '--------------------------------------------------------------------
    Private Function mread22kFile(ByVal strfilePath As String, _
                                  ByRef udtSetByteArray() As Byte) As Integer

        Dim intRtn As Integer = 0
        Dim intFileNo As Integer

        ''設定ファイルの保存されているファイル名
        Dim intpathLen As Integer = strfilePath.Split("\").Length
        Dim strfileName As String = strfilePath.Split("\")(intpathLen - 1)

        ''ファイル名の保持
        mstrFileName = strfileName

        ''メッセージ更新
        lblMessage.Text = "Converting " & strfileName & " ..."
        lblMessage.Refresh()

        ''ファイル存在確認
        If Not System.IO.File.Exists(strfilePath) Then

            Call mAddMsgList("Conversion Error!! The file [" & strfileName & "] doesn't exist.")
            intRtn = -1

        Else

            Try
                ''読み込み用のbyte配列領域 初期化
                Dim fi As New System.IO.FileInfo(strfilePath)
                ReDim udtSetByteArray(fi.Length - 1)

                ''ファイル読込み
                intFileNo = FreeFile()
                FileOpen(intFileNo, strfilePath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
                FileGet(intFileNo, udtSetByteArray)
                FileClose(intFileNo)

                ''メッセージ出力
                Call mAddMsgList("Conversion succeeded.  [" & strfileName & "]")

            Catch ex As Exception
                Call mAddMsgList("Read Error!! [" & strfileName & "] " & ex.Message & "")
                intRtn = -1
            End Try

        End If

        mread22kFile = intRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : メッセージ追加
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) メッセージ
    ' 機能説明  : ログリストにログを追加する
    '--------------------------------------------------------------------
    Private Sub mAddMsgList(ByVal strMsg As String)

        Try

            Call lstMsg.Items.Add(strMsg)

            ''画面表示ログが指定行数以上の場合に最終行を削除する
            If lstMsg.Items.Count > 1000 Then
                Call lstMsg.Items.RemoveAt(lstMsg.Items.Count - 1)
            End If

            Call lstMsg.Refresh()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報を取得する
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 取得対象のチャンネル番号
    '           : ARG2 - (IO) システム番号
    '           : ARG3 - (IO) チャンネルタイプ
    '           : ARG4 - (IO) ステータス種別コード
    '           : ARG5 - (IO) データタイプ
    '           : ARG6 - (IO) コンポジットインデックス
    '           : ARG7 - (IO) 計測点番号
    '           : ARG8 - (IO) ステータス（手入力値）
    '           : ARG9 - (IO) アナログ.useの値
    '           : ARG10- (IO) バルブ.useの値
    '--------------------------------------------------------------------
    Private Sub mGetChInfo(ByVal intChNo As Integer, _
                           ByRef intSystemNo As Integer, _
                           ByRef intChType As Integer, _
                           ByRef intStatus As Integer, _
                           ByRef intDataType As Integer, _
                           ByRef intCompIdx As Integer, _
                           ByRef intPinNo As Integer, _
                           ByRef strStatus As String, _
                           ByRef blnAnalogUse() As Boolean, _
                           ByRef blnValveUse() As Boolean)

        Try

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If intChNo = .udtChCommon.shtChno Then

                        intSystemNo = .udtChCommon.shtSysno
                        intChType = .udtChCommon.shtChType
                        intStatus = .udtChCommon.shtStatus
                        strStatus = gGetString(.udtChCommon.strStatus)
                        intDataType = .udtChCommon.shtData

                        If intChType = gCstCodeChTypeAnalog Then
                            blnAnalogUse(0) = IIf(.AnalogHiHiUse = 1, True, False)
                            blnAnalogUse(1) = IIf(.AnalogHiUse = 1, True, False)
                            blnAnalogUse(2) = IIf(.AnalogLoUse = 1, True, False)
                            blnAnalogUse(3) = IIf(.AnalogLoLoUse = 1, True, False)
                            blnAnalogUse(4) = IIf(.AnalogSensorFailUse = 1, True, False)    ''Under
                            blnAnalogUse(5) = IIf(.AnalogSensorFailUse = 1, True, False)    ''Over

                        ElseIf intChType = gCstCodeChTypeValve Then

                            If intDataType = gCstCodeChDataTypeValveAI_DO1 Or intDataType = gCstCodeChDataTypeValveAI_DO2 Then
                                blnValveUse(0) = IIf(.ValveAiDoHiHiUse = 1, True, False)
                                blnValveUse(1) = IIf(.ValveAiDoHiUse = 1, True, False)
                                blnValveUse(2) = IIf(.ValveAiDoLoUse = 1, True, False)
                                blnValveUse(3) = IIf(.ValveAiDoLoLoUse = 1, True, False)
                                blnValveUse(4) = IIf(.ValveAiDoSensorFailUse = 1, True, False)  ''Under
                                blnValveUse(5) = IIf(.ValveAiDoSensorFailUse = 1, True, False)  ''Over
                                blnValveUse(6) = IIf(.ValveAiDoAlarmUse = 1, True, False)       ''Feedback Alarm

                            ElseIf intDataType = gCstCodeChDataTypeValveAI_AO1 Or intDataType = gCstCodeChDataTypeValveAI_AO2 Or _
                                   intDataType = gCstCodeChDataTypeValveAO_4_20 Then
                                blnValveUse(0) = IIf(.ValveAiAoHiHiUse = 1, True, False)
                                blnValveUse(1) = IIf(.ValveAiAoHiUse = 1, True, False)
                                blnValveUse(2) = IIf(.ValveAiAoLoUse = 1, True, False)
                                blnValveUse(3) = IIf(.ValveAiAoLoLoUse = 1, True, False)
                                blnValveUse(4) = IIf(.ValveAiAoSensorFailUse = 1, True, False)  ''Under
                                blnValveUse(5) = IIf(.ValveAiAoSensorFailUse = 1, True, False)  ''Over
                                blnValveUse(6) = IIf(.ValveAiAoAlarmUse = 1, True, False)       ''Feedback Alarm
                            End If

                        ElseIf intChType = gCstCodeChTypeComposite Then

                            intPinNo = .udtChCommon.shtPinNo
                            intCompIdx = .CompositeTableIndex   ''コンポジット設定テーブルインデックス

                        End If

                        Exit For
                    End If
                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    ' -------------------------------------------------------------------
    ' 機能説明      : 既設エディタ情報を変数に設定
    ' 引数          : なし
    ' 戻り値        : なし
    ' -------------------------------------------------------------------
    Private SUB mSetSystemDevice(ByRef udtChannel As gTypSetChRec, ByVal intPinNo As Integer) 

        Try
            Dim intDevice As Integer = 0
            Dim udtKiki() As gTypCodeName = Nothing
            Dim DeviceUse(15) As Boolean
            Dim DeviceCode(15) As String
            Dim DeviceName(15) As String

            If intPinNo >= 10 And intPinNo <= 11 Then           ''FCU
                intDevice = intPinNo + 3
            ElseIf intPinNo >= 15 And intPinNo <= 18 Then       ''PRINTER
                If intPinNo = 15 Then                           ''AP1
                    intDevice = 41
                ElseIf intPinNo = 16 Then                       ''LP1
                    intDevice = 39
                ElseIf intPinNo = 17 Then                       ''AP2
                    intDevice = 42
                ElseIf intPinNo = 18 Then                       ''LP2
                    intDevice = 41
                End If
            ElseIf intPinNo >= 21 And intPinNo <= 22 Then       ''LU TRNS
                intDevice = 38
            ElseIf intPinNo >= 23 And intPinNo <= 31 Then       ''COMM (SIO)
                intDevice = intPinNo + 22
            ElseIf intPinNo >= 43 And intPinNo <= 62 Then       ''L/U
                intDevice = intPinNo - 25
            ElseIf intPinNo >= 103 And intPinNo <= 118 Then     ''RTB-DI/DO
                intDevice = 15
            ElseIf intPinNo >= 134 And intPinNo <= 143 Then     ''OPS
                intDevice = intPinNo - 133
            ElseIf intPinNo >= 164 And intPinNo <= 165 Then     ''GWS
                intDevice = intPinNo - 153
            ElseIf intPinNo = 176 Then                          ''EXT ALM P(TOTAL)
                intDevice = 61
            ElseIf intPinNo >= 177 And intPinNo <= 206 Then     ''EXT ALM P
                intDevice = intPinNo - 115
            End If

            If intDevice <> 0 Then
                With udtChannel

                    Call gGetComboCodeName(udtKiki, _
                                           gEnmComboType.ctChListChannelListDeviceStatus, _
                                           intDevice.ToString("00"))

                    For j As Integer = 0 To 15
                        If j <= UBound(udtKiki) Then
                            DeviceUse(j) = True
                            DeviceCode(j) = udtKiki(j).shtCode
                            DeviceName(j) = udtKiki(j).strName
                        Else
                            DeviceUse(j) = False
                            DeviceCode(j) = ""
                            DeviceName(j) = ""
                        End If
                    Next j

                    ''Device Status --------------------------------------------
                    .SystemInfoStatusUse01 = DeviceUse(0)
                    .SystemInfoKikiCode01 = Val(DeviceCode(0))
                    .SystemInfoStatusName01 = gGetString(DeviceName(0))

                    .SystemInfoStatusUse02 = DeviceUse(1)
                    .SystemInfoKikiCode02 = Val(DeviceCode(1))
                    .SystemInfoStatusName02 = gGetString(DeviceName(1))

                    .SystemInfoStatusUse03 = DeviceUse(2)
                    .SystemInfoKikiCode03 = Val(DeviceCode(2))
                    .SystemInfoStatusName03 = gGetString(DeviceName(2))

                    .SystemInfoStatusUse04 = DeviceUse(3)
                    .SystemInfoKikiCode04 = Val(DeviceCode(3))
                    .SystemInfoStatusName04 = gGetString(DeviceName(3))

                    .SystemInfoStatusUse05 = DeviceUse(4)
                    .SystemInfoKikiCode05 = Val(DeviceCode(4))
                    .SystemInfoStatusName05 = gGetString(DeviceName(4))

                    .SystemInfoStatusUse06 = DeviceUse(5)
                    .SystemInfoKikiCode06 = Val(DeviceCode(5))
                    .SystemInfoStatusName06 = gGetString(DeviceName(5))

                    .SystemInfoStatusUse07 = DeviceUse(6)
                    .SystemInfoKikiCode07 = Val(DeviceCode(6))
                    .SystemInfoStatusName07 = gGetString(DeviceName(6))

                    .SystemInfoStatusUse08 = DeviceUse(7)
                    .SystemInfoKikiCode08 = Val(DeviceCode(7))
                    .SystemInfoStatusName08 = gGetString(DeviceName(7))

                    .SystemInfoStatusUse09 = DeviceUse(8)
                    .SystemInfoKikiCode09 = Val(DeviceCode(8))
                    .SystemInfoStatusName09 = gGetString(DeviceName(8))

                    .SystemInfoStatusUse10 = DeviceUse(9)
                    .SystemInfoKikiCode10 = Val(DeviceCode(9))
                    .SystemInfoStatusName10 = gGetString(DeviceName(9))

                    .SystemInfoStatusUse11 = DeviceUse(10)
                    .SystemInfoKikiCode11 = Val(DeviceCode(10))
                    .SystemInfoStatusName11 = gGetString(DeviceName(10))

                    .SystemInfoStatusUse12 = DeviceUse(11)
                    .SystemInfoKikiCode12 = Val(DeviceCode(11))
                    .SystemInfoStatusName12 = gGetString(DeviceName(11))

                    .SystemInfoStatusUse13 = DeviceUse(12)
                    .SystemInfoKikiCode13 = Val(DeviceCode(12))
                    .SystemInfoStatusName13 = gGetString(DeviceName(12))

                    .SystemInfoStatusUse14 = DeviceUse(13)
                    .SystemInfoKikiCode14 = Val(DeviceCode(13))
                    .SystemInfoStatusName14 = gGetString(DeviceName(13))

                    .SystemInfoStatusUse15 = DeviceUse(14)
                    .SystemInfoKikiCode15 = Val(DeviceCode(14))
                    .SystemInfoStatusName15 = gGetString(DeviceName(14))

                    .SystemInfoStatusUse16 = DeviceUse(15)
                    .SystemInfoKikiCode16 = Val(DeviceCode(15))
                    .SystemInfoStatusName16 = gGetString(DeviceName(15))


                End With
            End If



        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '--------------------------------------------------------------------
    ' 機能      : アナログ小数点位置取得
    ' 返り値    : 小数点位置
    ' 引き数    : ARG1 - (I ) ファイルパス
    ' 　　　    : ARG2 - ( O) ファイル情報格納Byte配列
    ' 機能説明  : レンジ、SET値から小数点位置を取得する
    '--------------------------------------------------------------------
    Private Function get_dec_position(ByVal idx As Integer) As Integer

        Dim bytArray() As Byte
        Dim strValue As String
        Dim strValue2 As String
        Dim p As Integer = 0, p2 As Integer = 0, p3 As Integer = 0
        Dim intRtn As Integer = 0

        Try

            ''Range
            ReDim bytArray(11)
            For j As Integer = LBound(bytArray) To UBound(bytArray)
                bytArray(j) = mbyt22kFileArrayMpt(idx + 50 + j)
            Next j
            strValue = gGetString(mByte2String(12, bytArray))

            If strValue.Length >= 3 Then
                If strValue.Substring(0, 2) = "+-" Then

                    strValue2 = strValue.Substring(2)
                    p2 = strValue2.IndexOf(".")  ''小数点有りかを判断
                    If p2 > 0 Then
                        p2 = strValue2.Substring(p2 + 1).Length     ''P2 <-- 小数点以下桁数
                    Else
                        p2 = 0
                    End If

                    ''Decimal Position
                    intRtn = p2

                Else
                    p = strValue.IndexOf("/")
                    If p > 0 Then

                        ''RangeLow
                        strValue2 = strValue.Substring(0, p)
                        p2 = strValue2.IndexOf(".")  ''小数点有りかを判断
                        If p2 > 0 Then
                            p2 = strValue2.Substring(p2 + 1).Length     ''P2 <-- 小数点以下桁数
                        Else
                            p2 = 0
                        End If

                        ''Decimal Position
                        intRtn = p2

                        ''RangeHigh
                        strValue2 = strValue.Substring(p + 1)
                        p3 = strValue2.IndexOf(".")  ''小数点有りかを判断
                        If p3 > 0 Then
                            p3 = strValue2.Substring(p3 + 1).Length ''P3 <-- 小数点以下桁数
                        Else
                            p3 = 0
                        End If

                        ''Decimal Position
                        If p3 > p2 Then intRtn = p3

                    End If
                End If
            End If

            ''Value L
            ReDim bytArray(5)
            For j As Integer = LBound(bytArray) To UBound(bytArray)
                bytArray(j) = mbyt22kFileArrayMpt(idx + 76 + j)
            Next j
            strValue = gGetString(mByte2String(6, bytArray))

            If strValue.Length >= 3 Then
                p = strValue.IndexOf(".")  ''小数点有りかを判断
                If p > 0 Then
                    p = strValue.Substring(p + 1).Length     ''P2 <-- 小数点以下桁数
                Else
                    p = 0
                End If
            Else
                p = 0
            End If

            If p > intRtn Then intRtn = p ''Decimal Position

            ''Value H
            ReDim bytArray(5)
            For j As Integer = LBound(bytArray) To UBound(bytArray)
                bytArray(j) = mbyt22kFileArrayMpt(idx + 82 + j)
            Next j
            strValue = gGetString(mByte2String(6, bytArray))

            If strValue.Length >= 3 Then
                p = strValue.IndexOf(".")  ''小数点有りかを判断
                If p > 0 Then
                    p = strValue.Substring(p + 1).Length     ''P2 <-- 小数点以下桁数
                Else
                    p = 0
                End If
            Else
                p = 0
            End If

            If p > intRtn Then intRtn = p ''Decimal Position

            ''Value LL
            ReDim bytArray(5)
            For j As Integer = LBound(bytArray) To UBound(bytArray)
                bytArray(j) = mbyt22kFileArrayMpt(idx + 88 + j)
            Next j
            strValue = gGetString(mByte2String(6, bytArray))

            If strValue.Length >= 3 Then
                p = strValue.IndexOf(".")  ''小数点有りかを判断
                If p > 0 Then
                    p = strValue.Substring(p + 1).Length     ''P2 <-- 小数点以下桁数
                Else
                    p = 0
                End If
            Else
                p = 0
            End If

            If p > intRtn Then intRtn = p ''Decimal Position

            ''Value HH
            ReDim bytArray(5)
            For j As Integer = LBound(bytArray) To UBound(bytArray)
                bytArray(j) = mbyt22kFileArrayMpt(idx + 94 + j)
            Next j
            strValue = gGetString(mByte2String(6, bytArray))

            If strValue.Length >= 3 Then
                p = strValue.IndexOf(".")  ''小数点有りかを判断
                If p > 0 Then
                    p = strValue.Substring(p + 1).Length     ''P2 <-- 小数点以下桁数
                Else
                    p = 0
                End If
            Else
                p = 0
            End If

            If p > intRtn Then intRtn = p ''Decimal Position

            ''Normal Lo
            ReDim bytArray(5)
            For j As Integer = LBound(bytArray) To UBound(bytArray)
                bytArray(j) = mbyt22kFileArrayMpt(idx + 100 + j)
            Next j
            strValue = gGetString(mByte2String(6, bytArray))

            If strValue.Length >= 3 Then
                p = strValue.IndexOf(".")  ''小数点有りかを判断
                If p > 0 Then
                    p = strValue.Substring(p + 1).Length     ''P2 <-- 小数点以下桁数
                Else
                    p = 0
                End If
            Else
                p = 0
            End If

            If p > intRtn Then intRtn = p ''Decimal Position

            ''Normal Hi
            ReDim bytArray(5)
            For j As Integer = LBound(bytArray) To UBound(bytArray)
                bytArray(j) = mbyt22kFileArrayMpt(idx + 106 + j)
            Next j
            strValue = gGetString(mByte2String(6, bytArray))

            If strValue.Length >= 3 Then
                p = strValue.IndexOf(".")  ''小数点有りかを判断
                If p > 0 Then
                    p = strValue.Substring(p + 1).Length     ''P2 <-- 小数点以下桁数
                Else
                    p = 0
                End If
            Else
                p = 0
            End If

            If p > intRtn Then intRtn = p ''Decimal Position

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#End Region

End Class
