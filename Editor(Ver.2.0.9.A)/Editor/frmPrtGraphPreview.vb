Public Class frmPrtGraphPreview

#Region "定数定義"

    'ループ用定数定義
    Private mintIdxCol1 As Integer = 0
    Private mintIdxCol2 As Integer = 1
    Private mintIdxCol3 As Integer = 2
    Private mintIdxCol4 As Integer = 3
    Private mintIdxCol5 As Integer = 4
    Private mintIdxCol6 As Integer = 5

    'グラフ枠表示位置
    Private Const mCstMarginX_Frame As Single = 5
    Private Const mCstMarginY_Frame As Single = 1

    'グラフタイトル表示位置
    Private Const mCstMarginX_GraphTitle As Single = 30
    Private Const mCstMarginY_GraphTitle As Single = 27
    Private Const mCstMarginX_GraphTitleFree As Single = 50
    Private Const mCstMarginY_GraphTitleFree As Single = 24

    'X軸　HistoryNo
    Private Const mCstMarginX_HistoryNo As Single = 840

    'Y軸　ShipNo, HistoryNo
    Private Const mCstMarginY_OptionLabel As Single = 721

    'X軸　図形書出し位置
    Private Const mCstMarginX_DrawStartPos As Single = 15

    'バーとバーの間
    Private Const mCstCodeGraphBarSpan As Single = 37



#Region "排気ガス"

    'グラフの書き出し位置
    Private Const mCstMarginX_ExhGasStartPosDraw As Single = 125
    Private Const mCstMarginY_ExhGasStartPosUpperDraw As Single = 80    '偏差グラフ
    Private Const mCstMarginY_ExhGasStartPosLowerDraw As Single = 400   'Cylinder, T/C

    'グラフタイトルの書き出し位置
    Private Const mCstMarginY_ExhGasTcTitle As Single = 360

    'グラフ設定（共通）
    Private Const mCstCodeExhGasGraphWidth As Single = 925
    Private Const mCstCodeExhGasGraphHeight As Single = 60


#Region "偏差グラフ"

    '表示するバーの設定（高さ・幅）
    Private Const mCstCodeExhGasGraphUpperBarHeight As Single = 30
    Private Const mCstCodeExhGasGraphUpperBarWidth As Single = 20

    '20Graph -> 行高
    Private Const mCstMarginY_ExhGasGraphUpperLabelLine1 As Single = 285 '290 2.0.8.B
    Private Const mCstMarginY_ExhGasGraphUpperLabelLine2 As Single = 300 '305

#End Region

#Region "Cyl,T/Cグラフ"

    '表示するバーの設定（高さ・幅）
    Private Const mCstCodeExhGasGraphLowerBarHeight As Single = 50
    Private Const mCstCodeExhGasGraphLowerBarWidth As Single = 20

    'T/Cタイトル高
    Private Const mCstCodeExhGasGraphTcTitleHeight As Single = 30
    Private Const mCstCodeExhGasGraphTcTitleWidth As Single = 300

    '20Graph -> 行高
    Private Const mCstMarginY_ExhGasLowerGraphLabelLine1Asta As Single = 645 '650    2.0.8.B
    Private Const mCstMarginY_ExhGasLowerGraphLabelLine1Title As Single = 665 '675
    Private Const mCstMarginY_ExhGasLowerGraphLabelLine2Asta As Single = 655 '660
    Private Const mCstMarginY_ExhGasLowerGraphLabelLine2Title As Single = 675 '685


#End Region

#End Region

#Region "バーグラフ"

    'グラフの書き出し位置
    Private Const mCstMarginX_BarGraphStartPosDraw As Single = 125
    Private Const mCstMarginY_BarGraphStartPosDraw As Single = 80

    'バーの書き出し位置
    Private Const mCstMarginY_BarGraphStartPosDrawBar As Single = 639

    'ラベルの書き出し位置
    Private Const mCstMarginX_BarGraphLabel As Single = 10 '3   '' 2015.03.17　3 → 10 
    Private Const mCstMarginY_BarGraphLabel As Single = 100

    'グラフ設定（行間隔や幅など）
    Private Const mCstCodeBarGraphWidth As Single = 925
    Private Const mCstCodeBarGraphHeight4 As Single = 140
    Private Const mCstCodeBarGraphHeight6 As Single = 93
    Private Const mCstCodeBarGraphHeight3_5 As Single = 186
    Private Const mCstCodeBarGraphHeight3_5detail As Single = 37.2

    '表示するバーの設定（高さ・幅）
    Private Const mCstCodeBarGraphBarHeight As Single = 170
    Private Const mCstCodeBarGraphBarWidth As Single = 20

#End Region

#Region "アナログメーター"
    'METER表示調整　2013.07.24 K.Fujimoto
    'BMP横幅
    Private Const mCstCodeAnalogMeterBmpWidth As Single = 250 '264
    Private Const mCstCodeAnalogMeterBmpHight As Integer = 250

    '１行目／２行目の高さ
    'T.uekiメータ画像差し替えによる設定値調整(デフォルト 58 387)
    Private Const mCstMarginY_AnalogMeterRow1 As Integer = 150
    Private Const mCstMarginY_AnalogMeterRow2 As Integer = 406 '416

    '8分割表示の下線
    'T.uekiメータ画像差し替えによる設定値調整(デフォルト 659 329.5)
    Private Const mCstMarginX_AnalogMeterPos As Integer = 36
    Private Const mCstMarginX_AnalogMeterDX As Integer = 6
    Private Const mCstMarginY_AnalogMeterFrame As Integer = 506 '530
    Private Const mCstMarginY_AnalogMeterFrameHalf As Integer = 250 '265

    '8分割表示 110 350 680
    Private Const mCstMarginX_AnalogMeterDiv8ChNo As Single = 195
    Private Const mCstMarginY_AnalogMeterDiv8ChNoUpper As Single = 364 '377
    Private Const mCstMarginY_AnalogMeterDiv8ChNoLower As Single = 620 '642

    'T.uekiメータ画像差し替えによる設定値調整(デフォルト 70 80 71 87 409 401 417)
    Private Const mCstMarginX_AnalogMeterDiv8ItemName As Single = 70
    Private Const mCstMarginY_AnalogMeterDiv8ItemNameUpperSingle As Single = 157
    Private Const mCstMarginY_AnalogMeterDiv8ItemNameLowerSingle As Single = 413 '425

    'T.uekiメータ画像差し替えによる設定値調整(デフォルト 85 158 207 124 188)
    Private Const mCstMarginX_AnalogMeterDiv8RangeMin As Single = 64 '95
    Private Const mCstMarginX_AnalogMeterDiv8RangeMax As Single = 108 ' 130
    Private Const mCstMarginY_AnalogMeterDiv8Range As Single = 186 '200
    Private Const mCstMarginX_AnalogMeterDiv8Unit As Single = 124
    Private Const mCstMarginY_AnalogMeterDiv8Unit As Single = 156 '166

    '2分割表示
    Private Const mCstMarginX_AnalogMeterDiv2ChNo As Single = 230
    Private Const mCstMarginY_AnalogMeterDiv2ChNo As Single = 640
    Private Const mCstMarginX_AnalogMeterDiv2ItemName As Single = 50
    Private Const mCstMarginY_AnalogMeterDiv2ItemName As Single = 98

    Private Const mCstMarginX_AnalogMeterDiv2RangeMin As Single = 160
    Private Const mCstMarginX_AnalogMeterDiv2RangeMax As Single = 280
    Private Const mCstMarginY_AnalogMeterDiv2Range As Single = 485
    Private Const mCstMarginY_AnalogMeterDiv2Unit As Single = 440


#End Region

#Region "フリーグラフ"

    'BMP横幅
    Private Const mCstCodeFreeBmpWidthAnalogMeter As Single = 230
    Private Const mCstCodeFreeBmpWidthBar As Single = 107
    Private Const mCstCodeFreeBmpWidthIndicator As Single = 106

    '基準位置
    Private Const mCstMarginX_FreeDefPos As Single = 50
    Private Const mCstMarginY_FreeDefPos As Single = 140

    'ブロック番号
    Private Const mCstCodeFreeBlockNo1 As Integer = 1
    Private Const mCstCodeFreeBlockNo9 As Integer = 9
    Private Const mCstCodeFreeBlockNo17 As Integer = 17
    Private Const mCstCodeFreeBlockNo25 As Integer = 25
    Private Const mCstCodeFreeBlockNo32 As Integer = 32

    'ブロックの幅
    Private Const mCstCodeFreeBlockWidth As Single = 119

    'ブロックの高さ
    Private Const mCstCodeFreeBlockHeight As Single = 118



#Region "グラフ詳細"

    '--------------------
    ''アナログメーター
    '--------------------
    'チャンネルNO
    Private Const mCstCodeFreeAnalogHeightChNo As Single = 205

    '上下限値
    Private Const mCstCodeFreeAnalogHeightRangeMax As Single = 117
    Private Const mCstCodeFreeAnalogHeightRangeMin As Single = 50
    Private Const mCstCodeFreeAnalogHeightRange As Single = 144

    '単位
    Private Const mCstCodeFreeAnalogHeightUnit As Single = 124


    '--------------------
    ''バーグラフ
    '--------------------
    'チャンネルNO
    Private Const mCstMarginX_FreeBarChNo As Single = 25
    Private Const mCstMarginY_FreeBarChNo As Single = 208

    '単位
    Private Const mCstMarginX_FreeBarUnit As Single = 15
    Private Const mCstMarginY_FreeBarUnit As Single = 186

    '上下限値
    Private Const mCstMarginX_FreeBarHeightRange As Single = 44
    Private Const mCstMarginY_FreeBarHeightRangeMax As Single = 7
    Private Const mCstMarginY_FreeBarHeightRangeMin As Single = 175


    '--------------------
    ''インジケータ
    '--------------------
    'チャンネルNO
    Private Const mCstMarginX_FreeIndicatorChNo As Single = 40
    Private Const mCstMarginY_FreeIndicatorChNo As Single = 83

    'ステータス
    Private Const mCstMarginX_FreeIndicatorStatus As Single = 40
    Private Const mCstMarginY_FreeIndicatorStatus As Single = 64

    '--------------------
    ''カウンタ
    '--------------------
    'チャンネルNO
    Private Const mCstMarginX_FreeCounterChNo As Single = 55
    Private Const mCstMarginY_FreeCounterChNo As Single = 72

    'ステータス
    Private Const mCstMarginX_FreeCounterStatus As Single = 25
    Private Const mCstMarginY_FreeCounterStatus As Single = 44

    '中心線
    Private Const mCstMarginX_FreeCounterStatusLabelCenter As Single = 83
    Private Const mCstMarginX_FreeCounterStatusLabelStart As Single = 70
    Private Const mCstMarginX_FreeCounterStatusLabelStartMotor As Single = 55

    '単位
    Private Const mCstMarginX_FreeCounterUnit As Single = 149
    Private Const mCstMarginY_FreeCounterUnit As Single = 43


#End Region



#End Region


#End Region

#Region "変数定義"

    ''船番取得
    Private mudtPrintSetChGroupSetM As gTypSetChGroupSet
    Private mudtPrintSetChGroupSetC As gTypSetChGroupSet

    ''OPSグラフ設定
    Private mudtSetOpsGraph As gTypSetOpsGraph
    Private mudtSetOpsGraphTitle As gTypSetOpsGraphTitle
    Private mudtSetOpsGraphExhaust As gTypSetOpsGraphExhaust
    Private mudtSetOpsGraphBar As gTypSetOpsGraphBar
    Private mudtSetOpsGraphAnalogMeter As gTypSetOpsGraphAnalogMeter
    Private mudtSetOpsFreeGraph As gTypSetOpsFreeGraph      ' 2013.07.22 グラフとフリーグラフと分離  K.Fujimoto

    ''印刷モード
    Private mintPrintMode As Integer

    ''シリンダー数
    Private mintCylinderCnt As Integer

    ''ターボチャージャー数
    Private mintTurboChagerCnt As Integer

    ''グラフタイプ
    Private mBytGraphType As Byte

    ''グラフ番号
    Private mintGroupNo As Integer

    ''アナログメーター数
    Private IntAna As Integer

    ''バーグラフ Cylinder数（最大24本）
    Private mintBarGraphCylCnt As Integer

    ''ヒストリカル番号
    Private mstrHistoryNo As String

    ''シップ番号の表示非表示
    Private mblnShipNo As Boolean

    ''Part選択情報（True:Machinery、False:Cargo）
    Private mblnSelectMachinery As Boolean

    ''フリーグラフナンバー
    Private mintFreeGraphNo As Integer

    'Ver2.0.2.8
    Private mblALLdata As Boolean   '一括印字残ﾃﾞｰﾀ存在するしないﾌﾗｸﾞ
    Private mintALLdata As Integer
    Private mintALLlast As Integer

#Region "Image変数定義"

    'グラフ枠
    Private imgPathFrameExhBarAnalog As Image   '排気ガス、バー、アナログメーター
    Private imgPathFrameFree As Image           'フリーグラフ

    '---------------------
    ''アナログメーター
    '---------------------
    'BMPサイズ 1/8
    Private imgPathAnalog8_3 As Image
    Private imgPathAnalog8_4 As Image
    Private imgPathAnalog8_5 As Image
    Private imgPathAnalog8_6 As Image
    Private imgPathAnalog8_7 As Image

    'BMPサイズ 1/2
    Private imgPathAnalog2_3 As Image
    Private imgPathAnalog2_4 As Image
    Private imgPathAnalog2_5 As Image
    Private imgPathAnalog2_6 As Image
    Private imgPathAnalog2_7 As Image

    '---------------------
    ''フリーグラフ
    '---------------------
    'Counter
    Private imgPathFreeCounter As Image

    'Indicator
    Private imgPathFreeIndicatorData As Image
    Private imgPathFreeIndicatorAlarm As Image
    Private imgPathFreeIndicatorRepose As Image
    Private imgPathFreeIndicatorSensorFail As Image

    'AnalogMeter
    Private imgPathFreeMeterScale3 As Image
    Private imgPathFreeMeterScale4 As Image
    Private imgPathFreeMeterScale5 As Image
    Private imgPathFreeMeterScale6 As Image
    Private imgPathFreeMeterScale7 As Image

    'Bar
    Private imgPathFreeBar3 As Image
    Private imgPathFreeBar4 As Image
    Private imgPathFreeBar5 As Image
    Private imgPathFreeBar6 As Image
    Private imgPathFreeBar7 As Image

#End Region


#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) OPSグラフ設定構造体
    '           : ARG2 - (I ) 3種類グラフ：行数/OPS：タブ番号
    '           : ARG3 - (I ) グラフタイプ
    '           : ARG4 - (I ) HistoryNo名
    '           : ARG5 - (I ) ShipNo表示フラグ
    '           : ARG6 - (I ) フリーグラフ タブ番号
    '           : ARG7 - (I ) 印刷モード（0:Print、1:Preview）
    '           : ARG8 - (I ) Part選択（True:Machinery、False:Cargo）
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow(ByRef udtOpsGraphType As gTypSetOpsGraph, _
                     ByVal i As Integer, _
                     ByVal ByGrpType As Byte, _
                     ByVal hstrHistoryNo As String, _
                     ByVal BoShNo As Boolean, _
                     ByVal intFreeGraphNo As Integer, _
                     ByVal intPrintMode As Integer, _
                     ByVal hblnSelectMachinery As Boolean)

        Try

            ''-------------------------------
            '' 引数の保存
            ''-------------------------------
            mstrHistoryNo = hstrHistoryNo               'ヒストリカル番号取得
            mblnShipNo = BoShNo                         'シップ番号表示/非表示取得
            mblnSelectMachinery = hblnSelectMachinery   'パート選択情報
            mBytGraphType = ByGrpType                   'グラフタイプ取得
            mintGroupNo = i                             'グラフ番号取得（3種類グラフ：行数/OPS：タブ番号）
            mintPrintMode = intPrintMode                '印刷モード
            mintFreeGraphNo = intFreeGraphNo            'フリーグラフ番号(1～16)


            ''-------------------------------
            '' 構造体情報取得
            ''-------------------------------
            mudtPrintSetChGroupSetM = gudt.SetChGroupSetM
            mudtPrintSetChGroupSetC = gudt.SetChGroupSetC
            mudtSetOpsGraph = udtOpsGraphType

            ''-------------------------------
            '' グラフ設定
            ''-------------------------------
            Call mSetImagePath() 'グラフ表示用画像設定

            'コンボ設定
            Call gSetComboBox(cmbUnit, gEnmComboType.ctChListChannelListUnit)                   '単位
            Call gSetComboBox(cmbStatusAnalog, gEnmComboType.ctChListChannelListStatusAnalog)   'ステータス種別：アナログ
            Call gSetComboBox(cmbStatusDigital, gEnmComboType.ctChListChannelListStatusDigital) 'ステータス種別：デジタル

            ''グラフタイプの構成設定取得
            Select Case mBytGraphType

                Case gCstCodePrintGraphViewGraphTypeExhaust

                    ''排ガス
                    mudtSetOpsGraphExhaust = mudtSetOpsGraph.udtGraphExhaustRec(mintGroupNo)

                    ''シリンダーグラフのグラフ数を取得
                    mintCylinderCnt = CInt(mudtSetOpsGraphExhaust.bytCyCnt)

                    ''ターボチャージャーグラフのグラフ数を取得
                    mintTurboChagerCnt = CInt(mudtSetOpsGraphExhaust.bytTcCnt)

                Case gCstCodePrintGraphViewGraphTypeBar

                    ''棒グラフ
                    mudtSetOpsGraphBar = mudtSetOpsGraph.udtGraphBarRec(mintGroupNo)

                    ''棒グラフのグラフ数を取得
                    mintBarGraphCylCnt = CInt(mudtSetOpsGraphBar.bytCyCnt)

                Case gCstCodePrintGraphViewGraphTypeAnalogMeter

                    ''アナログメーター
                    mudtSetOpsGraphAnalogMeter = mudtSetOpsGraph.udtGraphAnalogMeterRec(mintGroupNo)

                Case gCstCodePrintGraphViewGraphTypeFree

                    ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
                    ' ''フリーグラフ
                    'mudtSetOpsGraphFree = mudtSetOpsGraph.udtGraphFreeRec(mintGroupNo)

            End Select

            ''-------------------------------
            '' 印刷/プレビュー表示判断
            ''-------------------------------
            'Ver2.0.0.2 印刷のモードにALLモード追加
            'If intPrintMode = gCstCodePrintGraphViewPrintModePrint Then
            '    ''印刷
            '    Call cmdbPrint_Click(cmdbPrint, New EventArgs)
            '    Me.Close()
            'Else
            '    ''プレビュー表示
            '    Me.ShowDialog()
            'End If
            Select Case intPrintMode
                Case gCstCodePrintGraphViewPrintModePrint
                    ''印刷
                    Call cmdbPrint_Click(cmdbPrint, New EventArgs)
                    Me.Close()
                Case gCstCodePrintGraphViewPrintModePreview
                    'プレビュー表示
                    Me.ShowDialog()
                Case gCstCodePrintGraphViewPrintModeAllPrint
                    '一括印刷
                    Call cmdbAllPrint()
                    Me.Close()
                Case Else
            End Select
            

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： PRINT ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdbPrint.Click

        Try

            Dim PrintDialog1 As New PrintDialog()
            PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()

            ''「ファイルへ出力」チェックボックスを無効にする
            PrintDialog1.AllowPrintToFile = False
            PrintDialog1.UseEXDialog = True         '' 64bit版対応 2014.09.18

            ''印刷ダイアログを表示
            If PrintDialog1.ShowDialog() = DialogResult.OK Then

                ''PrintDocumentオブジェクトの作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                ''Ver2.0.8.7 綴じ代調整　画像変更
                ''印刷設定
                pd.OriginAtMargins = True
                pd.DefaultPageSettings.Landscape = True                                     ''用紙の向き（True:横、False:縦）
                pd.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName   ''使用プリンタ名称
                pd.PrinterSettings.Copies = PrintDialog1.PrinterSettings.Copies             ''印刷枚数
                pd.DefaultPageSettings.Margins.Top = 70 '50                                     ''余白設定
                pd.DefaultPageSettings.Margins.Left = 50                                    ''余白設定
                pd.DefaultPageSettings.Margins.Right = 50                                   ''余白設定
                pd.DefaultPageSettings.Margins.Bottom = 50                                  ''余白設定

                ''PrintPageイベントハンドラの追加
                AddHandler pd.PrintPage, AddressOf pd_PrintPage

                ''印刷を開始する
                pd.Print()

                ''PrintPageイベントハンドラを削除
                RemoveHandler pd.PrintPage, AddressOf pd_PrintPage

                ''印刷プレビューダイアログを表示
                ''PrintPreviewDialog1.Document = pd
                ''PrintPreviewDialog1.ShowDialog()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 一括印刷機能
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    'Ver2.0.0.2
    Private Sub cmdbAllPrint()
        Try

            Dim PrintDialog1 As New PrintDialog()
            PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()

            ''「ファイルへ出力」チェックボックスを無効にする
            PrintDialog1.AllowPrintToFile = False
            PrintDialog1.UseEXDialog = True         '' 64bit版対応 2014.09.18

            ''印刷ダイアログを表示
            If PrintDialog1.ShowDialog() = DialogResult.OK Then

                ''PrintDocumentオブジェクトの作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                ''Ver2.0.8.7 綴じ代調整　画像変更
                ''印刷設定
                pd.OriginAtMargins = True
                pd.DefaultPageSettings.Landscape = True                                     ''用紙の向き（True:横、False:縦）
                pd.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName   ''使用プリンタ名称
                pd.PrinterSettings.Copies = PrintDialog1.PrinterSettings.Copies             ''印刷枚数
                pd.DefaultPageSettings.Margins.Top = 70 '50                                     ''余白設定
                pd.DefaultPageSettings.Margins.Left = 50                                    ''余白設定
                pd.DefaultPageSettings.Margins.Right = 50                                   ''余白設定
                pd.DefaultPageSettings.Margins.Bottom = 50                                  ''余白設定

                Dim i As Integer = 0
                Dim strTitle As String = ""
                ''PrintPageイベントハンドラの追加
                AddHandler pd.PrintPage, AddressOf pd_PrintPage


                'Ver2.0.2.8 グラフ一括印字統合化
                '>>>最終添え字ゲット
                mintALLlast = 0
                For i = 0 To UBound(mudtSetOpsGraph.udtGraphTitleRec)
                    strTitle = gGetString(mudtSetOpsGraph.udtGraphTitleRec(i).strName)
                    If strTitle <> "" Then
                        mintALLlast = i
                    End If
                Next i
                '>>>最初の添え字ゲット
                mintALLdata = 0
                For i = 0 To UBound(mudtSetOpsGraph.udtGraphTitleRec)
                    strTitle = gGetString(mudtSetOpsGraph.udtGraphTitleRec(i).strName)
                    If strTitle <> "" Then
                        'データが存在すれば、該当データを印刷用領域へ格納して、印刷処理
                        mBytGraphType = mudtSetOpsGraph.udtGraphTitleRec(i).bytType
                        mintGroupNo = i
                        Select Case mBytGraphType
                            Case gCstCodePrintGraphViewGraphTypeExhaust
                                ''排ガス
                                mudtSetOpsGraphExhaust = mudtSetOpsGraph.udtGraphExhaustRec(i)
                                ''シリンダーグラフのグラフ数を取得
                                mintCylinderCnt = CInt(mudtSetOpsGraphExhaust.bytCyCnt)
                                ''ターボチャージャーグラフのグラフ数を取得
                                mintTurboChagerCnt = CInt(mudtSetOpsGraphExhaust.bytTcCnt)
                            Case gCstCodePrintGraphViewGraphTypeBar
                                ''棒グラフ
                                mudtSetOpsGraphBar = mudtSetOpsGraph.udtGraphBarRec(i)
                                ''棒グラフのグラフ数を取得
                                mintBarGraphCylCnt = CInt(mudtSetOpsGraphBar.bytCyCnt)
                            Case gCstCodePrintGraphViewGraphTypeAnalogMeter
                                ''アナログメーター
                                mudtSetOpsGraphAnalogMeter = mudtSetOpsGraph.udtGraphAnalogMeterRec(i)
                        End Select
                        mintALLdata = i
                        Exit For
                    End If
                Next i


                ''印刷を開始する
                pd.Print()

                ''PrintPageイベントハンドラを削除
                RemoveHandler pd.PrintPage, AddressOf pd_PrintPage
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    Private Sub pd_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)

        Try
            'Ver2.0.2.8 グラフ一括印字統合化
            If mintPrintMode = gCstCodePrintGraphViewPrintModeAllPrint Then
                ''グラフ作成
                Call mDrawGraphics(e.Graphics)
                If mintALLlast = mintALLdata Then
                    e.HasMorePages = False
                Else
                    e.HasMorePages = True
                    '次のページ
                    Dim strTitle As String
                    For i As Integer = mintALLdata + 1 To mintALLlast
                        strTitle = gGetString(mudtSetOpsGraph.udtGraphTitleRec(i).strName)
                        If strTitle <> "" Then
                            'データが存在すれば、該当データを印刷用領域へ格納して、印刷処理
                            mBytGraphType = mudtSetOpsGraph.udtGraphTitleRec(i).bytType
                            mintGroupNo = i
                            Select Case mBytGraphType
                                Case gCstCodePrintGraphViewGraphTypeExhaust
                                    ''排ガス
                                    mudtSetOpsGraphExhaust = mudtSetOpsGraph.udtGraphExhaustRec(i)
                                    ''シリンダーグラフのグラフ数を取得
                                    mintCylinderCnt = CInt(mudtSetOpsGraphExhaust.bytCyCnt)
                                    ''ターボチャージャーグラフのグラフ数を取得
                                    mintTurboChagerCnt = CInt(mudtSetOpsGraphExhaust.bytTcCnt)
                                Case gCstCodePrintGraphViewGraphTypeBar
                                    ''棒グラフ
                                    mudtSetOpsGraphBar = mudtSetOpsGraph.udtGraphBarRec(i)
                                    ''棒グラフのグラフ数を取得
                                    mintBarGraphCylCnt = CInt(mudtSetOpsGraphBar.bytCyCnt)
                                Case gCstCodePrintGraphViewGraphTypeAnalogMeter
                                    ''アナログメーター
                                    mudtSetOpsGraphAnalogMeter = mudtSetOpsGraph.udtGraphAnalogMeterRec(i)
                            End Select
                            mintALLdata = i
                            Exit For
                        End If
                    Next i
                End If
            Else
                ''グラフ作成
                Call mDrawGraphics(e.Graphics)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： CLOSE ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdbClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdbClose.Click

        Try

            Me.Dispose()
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： プレビュー画面表示
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub imgPrevie_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles imgPrevie.Paint

        Try

            ''グラフ作成
            Call mDrawGraphics(e.Graphics)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能説明  ： グラフ作成
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawGraphics(ByVal objGraphics As System.Drawing.Graphics)

        Try

            '表示するグラフタイプ
            Select Case mBytGraphType
                Case gCstCodePrintGraphViewGraphTypeExhaust

                    '排ガスグラフ
                    Call ExhGasGrpPreview(objGraphics)

                    '船番
                    Call mDrawGraphicsShipNo(objGraphics, mblnShipNo, mCstMarginX_DrawStartPos)

                Case gCstCodePrintGraphViewGraphTypeBar

                    'バーグラフ
                    Call BarGrpPreview(objGraphics)

                    '船番
                    Call mDrawGraphicsShipNo(objGraphics, mblnShipNo, mCstMarginX_DrawStartPos)

                Case gCstCodePrintGraphViewGraphTypeAnalogMeter

                    'アナログメーター
                    Call AnalogMeterGrpPreview(objGraphics)

                    '船番
                    Call mDrawGraphicsShipNo(objGraphics, mblnShipNo, mCstMarginX_DrawStartPos)

                Case gCstCodePrintGraphViewGraphTypeFree

                    'フリーグラフ
                    Call FreeGrpPreview(objGraphics)

                    '船番
                    Call mDrawGraphicsShipNo(objGraphics, mblnShipNo, mCstMarginX_DrawStartPos - 10)

            End Select

            'ヒストリカルナンバー
            If mstrHistoryNo <> "" Then
                objGraphics.DrawString("History No : " & mstrHistoryNo, _
                                       gFnt12, _
                                       gFntColorBlack, _
                                       mCstMarginX_HistoryNo, _
                                       mCstMarginY_OptionLabel)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 船番の表示
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ShipNo表示フラグ
    '           ： ARG3 - (I ) ShipNoを表示する高さ
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawGraphicsShipNo(ByVal objGraph As System.Drawing.Graphics, _
                                    ByVal hblnShipNo As Boolean, _
                                    ByVal hsngHeight As Single)

        Try

            If hblnShipNo = True Then

                Dim strShipNoView As String = ""

                'Mach/Carg 船番取得
                'Ver2.0.4.9「^」は消す
                If mblnSelectMachinery Then strShipNoView = mudtPrintSetChGroupSetM.udtGroup.strShipNo.Replace("^", "")
                If Not mblnSelectMachinery Then strShipNoView = mudtPrintSetChGroupSetC.udtGroup.strShipNo.Replace("^", "")

                '船番表示
                objGraph.DrawString(strShipNoView, gFnt12, gFntColorBlack, hsngHeight, mCstMarginY_OptionLabel)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#Region "排気ガスグラフ表示"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 排気ガスグラフ作成
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub ExhGasGrpPreview(ByVal objGraph As System.Drawing.Graphics)

        Try

            'フレーム表示
            objGraph.DrawImage(imgPathFrameExhBarAnalog, mCstMarginX_Frame, mCstMarginY_Frame, imgPathFrameExhBarAnalog.Width, imgPathFrameExhBarAnalog.Height)

            'グラフタイトル表示
            Dim strTitle As String = Format(mintGroupNo + 1, "00") & "  " & mudtSetOpsGraphExhaust.strTitle
            objGraph.DrawString(strTitle, gFnt12, gFntColorBlack, mCstMarginX_GraphTitle, mCstMarginY_GraphTitle)

            '上段グラフ表示（偏差グラフ）
            Call ExhGasGrpPreviewDrawUpper(objGraph)

            '下段グラフ表示（Cylinder, T/C）
            Call ExhGasGrpPreviewDrawLower(objGraph)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "排気ガスグラフ表示 詳細"


#Region "上段グラフ（偏差）"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 排気ガスグラフ　上段グラフ表示
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub ExhGasGrpPreviewDrawUpper(ByVal objGraph As System.Drawing.Graphics)

        Try

            Dim strGraphDiv(5) As String
            Dim strChNo As String = "(" & Format(mudtSetOpsGraphExhaust.shtAveCh, "0000") & ")"

            'ラベル表示
            objGraph.DrawString("AVE", gFnt14, gFntColorBlack, mCstMarginX_ExhGasStartPosDraw - 71, 151)
            objGraph.DrawString("***", gFnt12, gFntColorBlack, mCstMarginX_ExhGasStartPosDraw - 66, 172)

            If gudt.SetSystem.udtSysOps.shtTagMode = 1 Then     ' Ver1.8.5  2015.12.01 ﾀｸﾞ表示追加
                strChNo = GetTagNoFromCHNo(mudtSetOpsGraphExhaust.shtAveCh)
            End If

            objGraph.DrawString(strChNo, gFnt8, gFntColorBlack, mCstMarginX_ExhGasStartPosDraw - 74, 188)


            '目盛取得
            Call ExhGasGrpPreviewDrawUpperDivision(strGraphDiv)

            '図形/目盛表示（グラフ共通）
            Call ExhGasGrpPreviewDrawGraph(objGraph, _
                                           mCstMarginX_ExhGasStartPosDraw, _
                                           mCstMarginY_ExhGasStartPosUpperDraw, _
                                           mCstCodeExhGasGraphWidth, _
                                           mCstCodeExhGasGraphHeight - 10, _
                                           strGraphDiv)

            '偏差グラフ 表示詳細
            Call ExhGasGrpPreviewDrawUpperDetail(objGraph, mCstMarginX_ExhGasStartPosDraw + 11, 180)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 排気ガスグラフ　上段グラフ表示詳細
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始座標軸 X
    '           ： ARG3 - (I ) Draw開始座標軸 Y
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub ExhGasGrpPreviewDrawUpperDetail(ByVal objGraph As System.Drawing.Graphics, _
                                                ByVal sngX As Single, _
                                                ByVal sngY As Single)
        Dim CYLKosu As Integer
        Dim Kankaku As Integer

        Try

            Dim strChNo As String = ""
            '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'Dim bln20Graph As Boolean = IIf(mudtSetOpsGraphExhaust.byt20Graph, True, False)
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            Dim byt20GraphLine As Byte = mudtSetOpsGraphExhaust.bytLine

            'T.ueki シリンダ数により表示幅変更
            If mintTurboChagerCnt = 0 Then
                CYLKosu = mintCylinderCnt
            Else
                If mintTurboChagerCnt > 4 Then
                    CYLKosu = mintCylinderCnt + mintTurboChagerCnt
                Else
                    CYLKosu = mintCylinderCnt + 4
                End If
            End If

            Select Case CYLKosu
                Case Is <= 8
                    Kankaku = 113
                Case Is <= 12
                    Kankaku = 73
                Case Is <= 16
                    Kankaku = 57
                Case Is <= 20
                    Kankaku = 44
                Case Is <= 24
                    Kankaku = 36
            End Select

            For i As Integer = 1 To mintCylinderCnt

                '未設定の場合はグラフ表示をスキップ
                If gGetString(mudtSetOpsGraphExhaust.udtCylinder(i - 1).shtChDeviation) <> "0" Then

                    'チャンネル表示
                    If gudt.SetSystem.udtSysOps.shtTagMode = 1 Then     ' Ver1.8.3  2015.11.26 ﾀｸﾞ表示追加
                        strChNo = GetTagNoFromCHNo(mudtSetOpsGraphExhaust.udtCylinder(i - 1).shtChDeviation)
                    Else
                        strChNo = Format(mudtSetOpsGraphExhaust.udtCylinder(i - 1).shtChDeviation, "0000")
                    End If
                    strChNo = "(" & strChNo & ")"
                    'objGraph.DrawString(strChNo, gFnt7, gFntColorBlack, sngX - 5, 325)
                    objGraph.DrawString(strChNo, gFnt7, gFntColorBlack, sngX - 5, 315)  ' 2.0.8.B

                    If i Mod 2 = 0 Then '偶数行

                        'バー表示
                        'ueki
                        objGraph.FillRectangle(gFntColorBlack, _
                                               sngX + 5, _
                                               sngY, _
                                               mCstCodeExhGasGraphUpperBarWidth, _
                                               mCstCodeExhGasGraphUpperBarHeight)

                        '***表示位置
                        If (byt20GraphLine = gCstCodeOpsExhGraphLine2) Then

                            Select Case CYLKosu
                                Case Is <= 20
                                    '2行表示
                                    objGraph.DrawString("******", _
                                                        gFnt9, _
                                                        gFntColorBlack, _
                                                        sngX - 11, _
                                                        mCstMarginY_ExhGasGraphUpperLabelLine2)
                                Case Is <= 24
                                    '2行表示
                                    objGraph.DrawString("****", _
                                                        gFnt9, _
                                                        gFntColorBlack, _
                                                        sngX - 4, _
                                                        mCstMarginY_ExhGasGraphUpperLabelLine1)
                                Case Else
                                    '2行表示
                                    objGraph.DrawString("****", _
                                                        gFnt9, _
                                                        gFntColorBlack, _
                                                        sngX - 4, _
                                                        mCstMarginY_ExhGasGraphUpperLabelLine1)
                            End Select
                            
                        Else

                            Select Case CYLKosu
                                Case Is <= 16
                                    '1行表示
                                    objGraph.DrawString("******", _
                                                        gFnt9, _
                                                        gFntColorBlack, _
                                                        sngX - 11, _
                                                        mCstMarginY_ExhGasGraphUpperLabelLine1)
                                Case Is <= 20
                                    '1行表示
                                    objGraph.DrawString("****", _
                                                        gFnt9, _
                                                        gFntColorBlack, _
                                                        sngX - 4, _
                                                        mCstMarginY_ExhGasGraphUpperLabelLine1)
                                Case Else
                                    '1行表示
                                    objGraph.DrawString("****", _
                                                        gFnt9, _
                                                        gFntColorBlack, _
                                                        sngX - 4, _
                                                        mCstMarginY_ExhGasGraphUpperLabelLine1)
                            End Select
                        End If

                    Else '奇数行

                            'バー表示
                            objGraph.FillRectangle(gFntColorBlack, _
                                                   sngX + 5, _
                                                   sngY - mCstCodeExhGasGraphUpperBarHeight, _
                                                   mCstCodeExhGasGraphUpperBarWidth, _
                                                   mCstCodeExhGasGraphUpperBarHeight)

                        '***表示
                        Select Case CYLKosu
                            Case Is <= 16
                                objGraph.DrawString("******", _
                                                    gFnt9, _
                                                    gFntColorBlack, _
                                                    sngX - 11, _
                                                    mCstMarginY_ExhGasGraphUpperLabelLine1)
                            Case Is <= 20
                                objGraph.DrawString("****", _
                                                    gFnt9, _
                                                    gFntColorBlack, _
                                                    sngX - 4, _
                                                    mCstMarginY_ExhGasGraphUpperLabelLine1)
                            Case Else
                                objGraph.DrawString("****", _
                                                    gFnt9, _
                                                    gFntColorBlack, _
                                                    sngX - 4, _
                                                    mCstMarginY_ExhGasGraphUpperLabelLine1)
                        End Select
                       
                    End If

                End If

                '次のグラフ描画位置へ
                'sngX = sngX + mCstCodeGraphBarSpan
                sngX = sngX + Kankaku

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 排気ガスグラフ　上段グラフ目盛
    ' 引数      ： ARG1 - (　O) 目盛値
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub ExhGasGrpPreviewDrawUpperDivision(ByRef hstrDivision() As String)

        Try

            ''偏差目盛の取得
            Dim bytDiv As Byte = mudtSetOpsGraphExhaust.bytDevMark

            ''中間値の取得（整数値）
            Dim bytDivHalf As Byte = Math.Round(bytDiv / 2, MidpointRounding.AwayFromZero)

            ''目盛設定
            hstrDivision(0) = " " & bytDiv.ToString

            ' 4分割できる場合に表示　2015.03.17
            If (bytDiv * 2) Mod 4 = 0 Then
                hstrDivision(1) = " " & bytDivHalf.ToString
            End If

            hstrDivision(2) = ""

            ' 4分割できる場合に表示　2015.03.17
            If (bytDiv * 2) Mod 4 = 0 Then
                hstrDivision(3) = "-" & bytDivHalf.ToString
            End If

            hstrDivision(4) = "-" & bytDiv.ToString

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "下段グラフ（Cylinder, T/C）"

    '----------------------------------------------------------------------------
    ' 機能説明  ： 排気ガスグラフ　下段グラフ表示
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub ExhGasGrpPreviewDrawLower(ByVal objGraph As System.Drawing.Graphics)

        Try

            Dim strGraphDiv(5) As String
            Dim strUnit As String = ""

            '目盛・単位取得
            Call ExhGasGrpPreviewDrawLowerDivision(strGraphDiv, strUnit)

            '単位表示
            '2015.03.17 単位文字を8文字に統一、位置調整(OPSに合わせる)
            'strUnit = "[" & strUnit & "]"
            strUnit = "[" & strUnit.PadRight(8) & "]"
            strUnit = Microsoft.VisualBasic.Right(Space(20) & strUnit, 20)
            'objGraph.DrawString(strUnit, gFnt11, gFntColorBlack, mCstMarginX_ExhGasStartPosDraw - 213, 370)
            objGraph.DrawString(strUnit, gFnt11, gFntColorBlack, mCstMarginX_ExhGasStartPosDraw - 180, 370)

            ''図形/目盛描画（グラフ共通）
            Call ExhGasGrpPreviewDrawGraph(objGraph, _
                                           mCstMarginX_ExhGasStartPosDraw, _
                                           mCstMarginY_ExhGasStartPosLowerDraw, _
                                           mCstCodeExhGasGraphWidth, _
                                           mCstCodeExhGasGraphHeight, _
                                           strGraphDiv)

            'Cylinderグラフ表示
            Call ExhGasGrpPreviewDrawLowerCylinder(objGraph, mCstMarginX_ExhGasStartPosDraw + 11, 640)

            'T/Cグラフ表示
            Call ExhGasGrpPreviewDrawLowerTC(objGraph, mCstMarginX_ExhGasStartPosDraw - 26, 640)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 排気ガスグラフ　シリンダーグラフ表示
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始座標軸 X
    '           ： ARG3 - (I ) Draw開始座標軸 Y
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub ExhGasGrpPreviewDrawLowerCylinder(ByVal objGraph As System.Drawing.Graphics, _
                                                  ByVal sngX As Single, _
                                                  ByVal sngY As Single)

        Dim CYLKosu As Integer
        Dim Kankaku As Integer

        Try

            Dim strChNo As String = ""
            Dim strCylTitle As String = ""
            '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'Dim bln20Graph As Boolean = IIf(mudtSetOpsGraphExhaust.byt20Graph, True, False)
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            Dim byt20GraphLine As Byte = mudtSetOpsGraphExhaust.bytLine
            'Dim sngAdj As Single = mCstCodeGraphBarSpan / 2 'タイトルの表示調整
            Dim sngAdj As Single = Kankaku / 2 'タイトルの表示調整

            If mintCylinderCnt > 0 Then

                '--------------------------
                ''ItemUp, ItemDownラベル
                '--------------------------
                With mudtSetOpsGraphExhaust

                    '文字は左詰め表示。文字列長が多少増えても吸収出来るようPadLeft実施
                    If (byt20GraphLine = gCstCodeOpsExhGraphLine2) Then

                        '2行表示 座標修正　2.0.8.B
                        objGraph.DrawString((Trim(.strItemUp).PadRight(4)).PadLeft(10), _
                                            gFnt10, _
                                            gFntColorBlack, _
                                            mCstMarginX_ExhGasStartPosDraw - 95, _
                                            mCstMarginY_ExhGasLowerGraphLabelLine1Title)
                        objGraph.DrawString((Trim(.strItemDown).PadRight(4)).PadLeft(10), _
                                            gFnt10, _
                                            gFntColorBlack, _
                                            mCstMarginX_ExhGasStartPosDraw - 95, _
                                            mCstMarginY_ExhGasLowerGraphLabelLine2Title)
                    Else

                        '1行表示
                        objGraph.DrawString((Trim(.strItemUp).PadRight(4)).PadLeft(10), _
                                            gFnt10, _
                                            gFntColorBlack, _
                                            mCstMarginX_ExhGasStartPosDraw - 95, _
                                            mCstMarginY_ExhGasLowerGraphLabelLine1Title)
                        objGraph.DrawString((Trim(.strItemDown).PadRight(4)).PadLeft(10), _
                                            gFnt10, _
                                            gFntColorBlack, _
                                            mCstMarginX_ExhGasStartPosDraw - 95, _
                                            mCstMarginY_ExhGasLowerGraphLabelLine2Title)
                    End If

                End With

                '--------------------------
                ''バーグラフ詳細
                '--------------------------

                'T.ueki シリンダ数により表示幅変更
                If mintTurboChagerCnt = 0 Then
                    CYLKosu = mintCylinderCnt
                Else
                    If mintTurboChagerCnt > 4 Then
                        CYLKosu = mintCylinderCnt + mintTurboChagerCnt
                    Else
                        CYLKosu = mintCylinderCnt + 4
                    End If
                End If

                Select Case CYLKosu
                    Case Is <= 8
                        Kankaku = 113
                    Case Is <= 12
                        Kankaku = 73
                    Case Is <= 16
                        Kankaku = 57
                    Case Is <= 20
                        Kankaku = 44
                    Case Is <= 24
                        Kankaku = 36
                End Select

                For i As Integer = 1 To mintCylinderCnt

                    '未設定の場合はグラフ表示をスキップ
                    If gGetString(mudtSetOpsGraphExhaust.udtCylinder(i - 1).shtChCylinder) <> "0" Then

                        'CH番号表示
                        If gudt.SetSystem.udtSysOps.shtTagMode = 1 Then     ' Ver1.8.3  2015.11.26 ﾀｸﾞ表示追加  '' Ver1.8.5.1 2015.12.02 DEV CHになっていたので修正
                            strChNo = GetTagNoFromCHNo(mudtSetOpsGraphExhaust.udtCylinder(i - 1).shtChCylinder)
                        Else
                            strChNo = Format(mudtSetOpsGraphExhaust.udtCylinder(i - 1).shtChCylinder, "0000")
                        End If

                        strChNo = "(" & strChNo & ")"
                        'objGraph.DrawString(strChNo, gFnt7, gFntColorBlack, sngX - 5, 703)
                        objGraph.DrawString(strChNo, gFnt7, gFntColorBlack, sngX - 5, 693)  '' 2.0.8.B

                        'タイトル取得
                        strCylTitle = gGetString(mudtSetOpsGraphExhaust.udtCylinder(i - 1).strTitle)
                        sngAdj = (mCstCodeGraphBarSpan / 2) - (strCylTitle.Length * gFntScale7 / 2) - 5 '-5は微調整の結果値

                        If i Mod 2 = 0 Then '偶数行

                            'バー表示
                            objGraph.FillRectangle(gFntColorBlack, _
                                                   sngX + 5, _
                                                   sngY - mCstCodeExhGasGraphLowerBarHeight, _
                                                   mCstCodeExhGasGraphLowerBarWidth, _
                                                   mCstCodeExhGasGraphLowerBarHeight)

                            '「***」とタイトル表示（20Graph設定によって表示行数を変える）
                            If (byt20GraphLine = gCstCodeOpsExhGraphLine2) Then

                                Select Case CYLKosu
                                    Case Is <= 20
                                        '2行表示
                                        objGraph.DrawString("******", _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX - 11, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine2Asta)
                                        objGraph.DrawString(strCylTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine2Title)
                                    Case Is <= 24
                                        '2行表示
                                        objGraph.DrawString("****", _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX - 4, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine2Asta)
                                        objGraph.DrawString(strCylTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine2Title)
                                    Case Else
                                        '2行表示
                                        objGraph.DrawString("****", _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX - 4, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine2Asta)
                                        objGraph.DrawString(strCylTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine2Title)
                                End Select

                            Else

                                '1行表示

                                Select Case CYLKosu
                                    Case Is <= 16
                                        '1行表示
                                        objGraph.DrawString("******", _
                                                   gFnt9, _
                                                   gFntColorBlack, _
                                                   sngX - 11, _
                                                   mCstMarginY_ExhGasLowerGraphLabelLine1Asta)

                                        objGraph.DrawString(strCylTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine1Title)
                                    Case Is <= 20
                                        objGraph.DrawString("****", _
                                                   gFnt9, _
                                                   gFntColorBlack, _
                                                   sngX - 4, _
                                                   mCstMarginY_ExhGasLowerGraphLabelLine1Asta)

                                        objGraph.DrawString(strCylTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine1Title)
                                    Case Else
                                        objGraph.DrawString("****", _
                                                   gFnt9, _
                                                   gFntColorBlack, _
                                                   sngX - 4, _
                                                   mCstMarginY_ExhGasLowerGraphLabelLine1Asta)

                                        objGraph.DrawString(strCylTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine1Title)
                                End Select

                            End If

                        Else '奇数行

                            'バー表示
                            objGraph.FillRectangle(gFntColorBlack, _
                                                   sngX + 5, _
                                                   sngY - (mCstCodeExhGasGraphLowerBarHeight + 30), _
                                                   mCstCodeExhGasGraphLowerBarWidth, _
                                                   mCstCodeExhGasGraphLowerBarHeight + 30)
                            '***表示
                            Select Case CYLKosu
                                Case Is <= 16
                                    '1行表示
                                    objGraph.DrawString("******", _
                                               gFnt9, _
                                               gFntColorBlack, _
                                               sngX - 11, _
                                               mCstMarginY_ExhGasLowerGraphLabelLine1Asta)

                                Case Is <= 20
                                    objGraph.DrawString("****", _
                                               gFnt9, _
                                               gFntColorBlack, _
                                               sngX - 4, _
                                               mCstMarginY_ExhGasLowerGraphLabelLine1Asta)
                                Case Else
                                    objGraph.DrawString("****", _
                                               gFnt9, _
                                               gFntColorBlack, _
                                               sngX - 4, _
                                               mCstMarginY_ExhGasLowerGraphLabelLine1Asta)
                            End Select


                            'タイトル表示
                            objGraph.DrawString(strCylTitle, gFnt9, gFntColorBlack, sngX + sngAdj, mCstMarginY_ExhGasLowerGraphLabelLine1Title)

                        End If

                    End If

                        '次のグラフ描画位置へ
                        'sngX = sngX + mCstCodeGraphBarSpan
                        sngX = sngX + Kankaku

                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 排気ガスグラフ　ターボチャージャーグラフ表示
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始座標軸 X
    '           ： ARG3 - (I ) Draw開始座標軸 Y
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub ExhGasGrpPreviewDrawLowerTC(ByVal objGraph As System.Drawing.Graphics, _
                                            ByVal sngX As Single, _
                                            ByVal sngY As Single)

        Try

            Dim i As Integer
            Dim CYLKosu As Integer
            Dim Kankaku As Integer
            Dim AreaCunt As Integer
            Dim TCAreaView As Integer
            Dim TCComm1View As Integer
            Dim TCComm2View As Integer

            Dim strChNo As String = "", strTcTitle As String = ""

            'T.Ueki
            Dim strTcComm1 As String = "", strTcComm2 As String = ""

            '' Ver1.8.2 2015.11.19
            Dim nTCWidth As Integer
            Dim nWidth As Integer
            Dim nTCEndX As Integer
            Dim nEndX As Integer
            Dim nLength As Double
            ''//

            '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'Dim bln20Graph As Boolean = IIf(mudtSetOpsGraphExhaust.byt20Graph, True, False)
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            Dim byt20GraphLine As Byte = mudtSetOpsGraphExhaust.bytLine
            Dim sngAdj As Single    'タイトルの表示調整

            '-----------------------------
            ''T/Cグラフ描画
            '-----------------------------
            If mintTurboChagerCnt > 0 Then

                'T.ueki シリンダ数により表示幅変更
                If mintTurboChagerCnt = 0 Then
                    CYLKosu = mintCylinderCnt
                Else
                    If mintTurboChagerCnt > 4 Then
                        CYLKosu = mintCylinderCnt + mintTurboChagerCnt
                    Else
                        CYLKosu = mintCylinderCnt + 4
                    End If
                End If

                Select Case CYLKosu
                    Case Is <= 8
                        Kankaku = 115
                        AreaCunt = 8
                    Case Is <= 12
                        Kankaku = 75
                        AreaCunt = 12
                    Case Is <= 16
                        Kankaku = 59
                        AreaCunt = 16
                    Case Is <= 20
                        Kankaku = 46
                        AreaCunt = 20
                    Case Is <= 24
                        Kankaku = 38
                        AreaCunt = 24
                End Select

                'T/C 描画開始位置演算
                If mintTurboChagerCnt <= 4 Then
                    For i = 1 To AreaCunt - 4
                        sngX = sngX + Kankaku
                    Next
                Else
                    For i = 1 To AreaCunt - mintTurboChagerCnt
                        sngX = sngX + Kankaku
                    Next
                End If


                'CylinderとT/Cの間を少しあける
                sngX += 30

                '開始線表示
                objGraph.DrawLine(Pens.Black, sngX - 20, 400, sngX - 20, 640)
                

                '-----------------------------
                ''T/Cタイトル表示
                '-----------------------------
                If mintTurboChagerCnt > 0 Then

                    'T/Cタイトル取得
                    strTcTitle = gGetString(mudtSetOpsGraphExhaust.strTcTitle)

                    strTcComm1 = gGetString(mudtSetOpsGraphExhaust.strTcComm1)
                    strTcComm2 = gGetString(mudtSetOpsGraphExhaust.strTcComm2)

                    'タイトルが空欄の時は表示しない
                    If strTcTitle <> "" Then

                        ''T/C図形
                        'objGraph.DrawRectangle(Pens.Black, _
                        '                       mCstMarginX_ExhGasStartPosDraw + 625, _
                        '                       mCstMarginY_ExhGasTcTitle, _
                        '                       mCstCodeExhGasGraphTcTitleWidth, _
                        '                       mCstCodeExhGasGraphTcTitleHeight)
                        '' Ver1.8.2 2015.11.19 ｾﾝﾀｰ表示になっていない不具合修正
                        '' 　　　和文未確認
                        If mintTurboChagerCnt <= 4 Then
                            nTCEndX = sngX + Kankaku * 4
                            nEndX = sngX + Kankaku * 2
                        Else
                            nTCEndX = sngX + Kankaku * mintTurboChagerCnt
                            nEndX = sngX + Kankaku * (mintTurboChagerCnt / 2)
                        End If

                        nTCWidth = nTCEndX - sngX
                        nLength = LenB(strTcTitle) * gFntScale12
                        TCAreaView = sngX + (nTCWidth / 2 - nLength / 2)
                        ''If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then       ' 和文
                        ''TCAreaView = (sngX - 20) + (945 - (sngX - 20)) / 2
                        ''//

                        'T/Cタイトル
                        objGraph.DrawString(strTcTitle, _
                                            gFnt12, _
                                            gFntColorBlack, _
                                             TCAreaView, _
                                            mCstMarginY_ExhGasTcTitle + 6)


                        'コメント表示　T.Ueki
                        '' Ver1.8.2 2015.11.19 T/Cのｺﾒﾝﾄがｾﾝﾀｰ表示しない不具合修正
                        '' 　　　和文未確認
                        If mintTurboChagerCnt <= 4 Then     '' ｺﾒﾝﾄ表示幅算出
                            nWidth = mCstCodeExhGasGraphLowerBarWidth * 2 + Kankaku
                        Else
                            nWidth = mCstCodeExhGasGraphLowerBarWidth * (mintTurboChagerCnt / 2) + Kankaku * (mintTurboChagerCnt / 2)
                        End If

                        nLength = LenB(strTcComm1) * gFntScale12        '' 描画文字幅　和文はﾌｫﾝﾄを変える? その場合は文字幅が変わるので修正が必要
                        TCComm1View = sngX + ((nWidth / 2) - (nLength / 2))
                        nLength = LenB(strTcComm2) * gFntScale12
                        TCComm2View = nEndX + ((nWidth / 2) - (nLength / 2))
                        ''TCComm1View = ((sngX - 20) + (945 - (sngX - 20)) / 3)
                        ''TCComm2View = (sngX - 20) + ((945 - (sngX - 20)) / 3) + (((945 - (sngX - 20)) / 3)) * 2
                        ''//

                        objGraph.DrawString(strTcComm1, _
                                            gFnt9, _
                                            gFntColorBlack, _
                                             TCComm1View, _
                                            mCstMarginY_ExhGasTcTitle + 350) '+360 → 350 2.0.8.B


                        objGraph.DrawString(strTcComm2, _
                                            gFnt9, _
                                            gFntColorBlack, _
                                             TCComm2View, _
                                            mCstMarginY_ExhGasTcTitle + 350) '+360 → 350 2.0.8.B

                    End If

                End If

                'T/Cグラフ詳細
                For i = 1 To mintTurboChagerCnt

                    '未設定の場合はグラフ表示をスキップ
                    If gGetString(mudtSetOpsGraphExhaust.udtTurboCharger(i - 1).shtChTurboCharger) <> "0" Then

                        'CH番号取得
                        'CH番号表示
                        If gudt.SetSystem.udtSysOps.shtTagMode = 1 Then     ' Ver1.8.3  2015.11.26 ﾀｸﾞ表示追加
                            strChNo = GetTagNoFromCHNo(mudtSetOpsGraphExhaust.udtTurboCharger(i - 1).shtChTurboCharger)
                        Else
                            strChNo = Format(mudtSetOpsGraphExhaust.udtTurboCharger(i - 1).shtChTurboCharger, "0000")
                        End If

                        strChNo = "(" & strChNo & ")"
                        'objGraph.DrawString(strChNo, gFnt7, gFntColorBlack, sngX - 5, 703)
                        objGraph.DrawString(strChNo, gFnt7, gFntColorBlack, sngX - 5, 693)  '' 2.0.8.B

                        'タイトル
                        strTcTitle = gGetString(mudtSetOpsGraphExhaust.udtTurboCharger(i - 1).strTitle)
                        '' Ver1.8.2.1  2015.11.19  和文仕様時、文字数ではなくﾊﾞｲﾄ数を取得する必要があるため修正
                        ''sngAdj = (mCstCodeGraphBarSpan / 2) - (strTcTitle.Length * gFntScale7 / 2) - 5   '-5は微調整の結果値
                        sngAdj = (mCstCodeGraphBarSpan / 2) - (LenB(strTcTitle) * gFntScale7 / 2) - 5
                        ''//

                        If i Mod 2 = 0 Then '偶数行

                            'バー表示
                            objGraph.FillRectangle(gFntColorBlack, _
                                                   sngX + 5, _
                                                   sngY - mCstCodeExhGasGraphLowerBarHeight, _
                                                   mCstCodeExhGasGraphLowerBarWidth, _
                                                   mCstCodeExhGasGraphLowerBarHeight)

                            '「***」とタイトル表示
                            If (byt20GraphLine = gCstCodeOpsExhGraphLine2) Then

                                '2行表示
                                Select Case CYLKosu
                                    Case Is <= 20
                                        '1行表示
                                        objGraph.DrawString("******", _
                                                    gFnt9, _
                                                    gFntColorBlack, _
                                                    sngX - 11, _
                                                    mCstMarginY_ExhGasLowerGraphLabelLine2Asta)
                                        objGraph.DrawString(strTcTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine2Title)
                                    Case Is <= 24
                                        objGraph.DrawString("****", _
                                                    gFnt9, _
                                                    gFntColorBlack, _
                                                    sngX - 4, _
                                                    mCstMarginY_ExhGasLowerGraphLabelLine2Asta)
                                        objGraph.DrawString(strTcTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine2Title)
                                    Case Else
                                        objGraph.DrawString("****", _
                                                     gFnt9, _
                                                     gFntColorBlack, _
                                                     sngX - 4, _
                                                     mCstMarginY_ExhGasLowerGraphLabelLine2Asta)
                                        objGraph.DrawString(strTcTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine2Title)
                                End Select

                            Else

                                Select Case CYLKosu
                                    Case Is <= 16
                                        '1行表示
                                        objGraph.DrawString("******", _
                                                    gFnt9, _
                                                    gFntColorBlack, _
                                                    sngX - 11, _
                                                    mCstMarginY_ExhGasLowerGraphLabelLine1Asta)
                                        objGraph.DrawString(strTcTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine1Title)
                                    Case Is <= 20
                                        objGraph.DrawString("****", _
                                                    gFnt9, _
                                                    gFntColorBlack, _
                                                    sngX - 4, _
                                                    mCstMarginY_ExhGasLowerGraphLabelLine1Asta)
                                        objGraph.DrawString(strTcTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine1Title)
                                    Case Else
                                        objGraph.DrawString("****", _
                                                     gFnt9, _
                                                     gFntColorBlack, _
                                                     sngX - 4, _
                                                     mCstMarginY_ExhGasLowerGraphLabelLine1Asta)
                                        objGraph.DrawString(strTcTitle, _
                                                            gFnt9, _
                                                            gFntColorBlack, _
                                                            sngX + sngAdj, _
                                                            mCstMarginY_ExhGasLowerGraphLabelLine1Title)
                                End Select

                            End If

                        Else '奇数行

                            'バー表示
                            objGraph.FillRectangle(gFntColorBlack, _
                                                   sngX + 5, _
                                                   sngY - (mCstCodeExhGasGraphLowerBarHeight + 30), _
                                                   mCstCodeExhGasGraphLowerBarWidth, _
                                                   mCstCodeExhGasGraphLowerBarHeight + 30)
                            '***表示
                            Select Case CYLKosu
                                Case Is <= 16
                                    '1行表示
                                    objGraph.DrawString("******", _
                                               gFnt9, _
                                               gFntColorBlack, _
                                               sngX - 11, _
                                               mCstMarginY_ExhGasLowerGraphLabelLine1Asta)

                                Case Is <= 20
                                    objGraph.DrawString("****", _
                                               gFnt9, _
                                               gFntColorBlack, _
                                               sngX - 4, _
                                               mCstMarginY_ExhGasLowerGraphLabelLine1Asta)
                                Case Else
                                    objGraph.DrawString("****", _
                                               gFnt9, _
                                               gFntColorBlack, _
                                               sngX - 4, _
                                               mCstMarginY_ExhGasLowerGraphLabelLine1Asta)
                            End Select

                            'タイトル表示
                            objGraph.DrawString(strTcTitle, gFnt9, gFntColorBlack, sngX + sngAdj, mCstMarginY_ExhGasLowerGraphLabelLine1Title)

                        End If

                    End If

                    ''Ver2.0.8.7 CH設定に関係なくSplit Lineを描画 2018.08.06
                    'SplitLine表示
                    If mudtSetOpsGraphExhaust.udtTurboCharger(i - 1).bytSplitLine = 1 Then
                        'Ver2.0.3.7 線は点線 X位置は、グラフ幅20+開始線からグラフ左端まで25=45
                        'objGraph.DrawLine(Pens.Black, sngX + 32, 400, sngX + 32, 640)
                        Dim blackPen As New Pen(Color.Black)
                        blackPen.DashStyle = Drawing2D.DashStyle.Dash
                        objGraph.DrawLine(blackPen, sngX + 45, 400, sngX + 45, 640)
                        blackPen.Dispose()
                    End If

                        '次のグラフ描画位置へ
                        sngX = sngX + Kankaku

                Next i

            End If



        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 排気ガスグラフ　下段グラフ目盛
    ' 引数      ： ARG1 - ( O) 目盛
    '           ： ARG2 - ( O) 単位
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub ExhGasGrpPreviewDrawLowerDivision(ByRef hstrDivision() As String, _
                                                  ByRef hstrUnit As String)

        Try

            Dim dblRangeH As Double
            Dim dblRangeMH As Double
            Dim dblRangeMM As Double
            Dim dblRangeML As Double
            Dim dblRangeL As Double

            Dim strRangeH As String = ""
            Dim strRangeMH As String = ""
            Dim strRangeMM As String = ""
            Dim strRangeML As String = ""
            Dim strRangeL As String = ""

            Dim intDecimalPosition As Integer   '小数点桁位置
            Dim strDecimalFormat As String = "" 'フォーマットタイプ
            Dim intChNo As Integer              'Cylに設定されている先頭アドレス

            '目盛初期化
            For i = 0 To UBound(hstrDivision)
                hstrDivision(i) = "0"
            Next

            'Cylの最初に設定されているチャンネルNOを取得する
            With mudtSetOpsGraphExhaust
                For i = LBound(.udtCylinder) To UBound(.udtCylinder)
                    If .udtCylinder(i).shtChCylinder <> 0 Then
                        'チャンネルNO取得
                        intChNo = .udtCylinder(i).shtChCylinder
                        Exit For
                    End If
                Next
            End With

            '上下限値の目盛と単位を取得する
            If (0 < intChNo) And (intChNo <= 65535) Then

                'チャンネルの配列Indexを取得する
                Dim intArrayIdx As Integer = gConvChNoToChArrayId(intChNo)

                'デジタル系のCHの場合は処理を抜ける
                If mCheckChTypeDigital(intArrayIdx) Then Exit Sub

                '処理を抜ける条件
                If (0 <= intArrayIdx) And (intArrayIdx < 3000) Then

                    '単位取得
                    Call mGetUnit(hstrUnit, intArrayIdx)

                    '小数点桁数取得
                    Call mGetDecimalInfo(intArrayIdx, intDecimalPosition, strDecimalFormat)

                    'H
                    dblRangeH = gudt.SetChInfo.udtChannel(intArrayIdx).AnalogRangeHigh / (10 ^ intDecimalPosition)
                    ''strRangeH = dblRangeH.ToString(strDecimalFormat)
                    strRangeH = CInt(dblRangeH).ToString        '' Ver1.11.3 2016.08.04  目盛は整数表示とする

                    'L
                    dblRangeL = gudt.SetChInfo.udtChannel(intArrayIdx).AnalogRangeLow / (10 ^ intDecimalPosition)
                    ''strRangeL = dblRangeL.ToString(strDecimalFormat)
                    strRangeL = CInt(dblRangeL).ToString        '' Ver1.11.3 2016.08.04  目盛は整数表示とする

                    'MH
                    ' 4分割できる場合に表示　2015.03.17
                    If (dblRangeH - dblRangeL) Mod 4 = 0 Then
                        dblRangeMH = dblRangeL + Math.Round(((dblRangeH - dblRangeL) / 4) * 3, MidpointRounding.AwayFromZero)
                        ''strRangeMH = dblRangeMH.ToString(strDecimalFormat)
                        strRangeMH = CInt(dblRangeMH).ToString      '' Ver1.11.3 2016.08.04  目盛は整数表示とする
                    End If

                    'MM
                    ' 2分割できる場合に表示　2015.03.17
                    If (dblRangeH - dblRangeL) Mod 2 = 0 Then
                        dblRangeMM = dblRangeL + Math.Round(((dblRangeH - dblRangeL) / 4) * 2, MidpointRounding.AwayFromZero)
                        ''strRangeMM = dblRangeMM.ToString(strDecimalFormat)
                        strRangeMM = CInt(dblRangeMM).ToString      '' Ver1.11.3 2016.08.04  目盛は整数表示とする  
                    End If

                    'ML
                    ' 4分割できる場合に表示　2015.03.17
                    If (dblRangeH - dblRangeL) Mod 4 = 0 Then
                        dblRangeML = dblRangeL + Math.Round(((dblRangeH - dblRangeL) / 4) * 1, MidpointRounding.AwayFromZero)
                        ''strRangeML = dblRangeML.ToString(strDecimalFormat)
                        strRangeML = CInt(dblRangeML).ToString      '' Ver1.11.3 2016.08.04  目盛は整数表示とする
                    End If

                End If

            End If

            '目盛設定
            hstrDivision(0) = strRangeH
            hstrDivision(1) = strRangeMH
            hstrDivision(2) = strRangeMM
            hstrDivision(3) = strRangeML
            hstrDivision(4) = strRangeL

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


    '----------------------------------------------------------------------------
    ' 機能説明  ： 排気ガスグラフ　グラフ表示（共通）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始座標軸 X
    '           ： ARG3 - (I ) Draw開始座標軸 Y
    '           ： ARG4 - (I ) グラフ枠の横幅
    '           ： ARG5 - (I ) グラフ枠の高さ
    '           ： ARG6 - (I ) グラフ目盛
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub ExhGasGrpPreviewDrawGraph(ByVal objGraph As System.Drawing.Graphics, _
                                          ByVal sngX As Single, _
                                          ByVal sngY As Single, _
                                          ByVal sngGraphWidth As Single, _
                                          ByVal sngGraphHeight As Single, _
                                          ByVal strDivision() As String)

        Try

            Dim i As Integer
            Dim strStrings As String = ""
            Dim sngStartPos As Single
            Dim sngLabelPos As Single = 110     '目盛表示開始位置
            Dim sngDistance As Single = 10      'グラフと目盛の間の距離

            '描画初期位置
            sngStartPos = sngY

            For i = mintIdxCol1 To mintIdxCol4

                '上位4点の目盛表示（右詰）
                strStrings = Microsoft.VisualBasic.Right(Space(10) & strDivision(i), 10)
                objGraph.DrawString(strStrings, gFnt9, gFntColorBlack, sngX - sngLabelPos, sngY - 8)

                objGraph.DrawLine(Pens.Black, sngX - 25, sngY, sngX - sngDistance, sngY)
                objGraph.DrawRectangle(Pens.Black, sngX, sngY, sngGraphWidth, sngGraphHeight)
                sngY = sngY + sngGraphHeight

            Next

            '下限目盛の表示（右詰）
            strStrings = Microsoft.VisualBasic.Right(Space(10) & strDivision(i), 10)
            objGraph.DrawString(strStrings, gFnt9, gFntColorBlack, sngX - sngLabelPos, sngY - 8)

            objGraph.DrawLine(Pens.Black, sngX - (sngDistance + 15), sngY, sngX - sngDistance, sngY)
            objGraph.DrawLine(Pens.Black, sngX - sngDistance, sngStartPos, sngX - sngDistance, sngY)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#End Region



#End Region

#Region "Barグラフ表示"

    '----------------------------------------------------------------------------
    ' 機能説明  ： バーグラフ作成
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub BarGrpPreview(ByVal objGraph As System.Drawing.Graphics)

        Try

            'フレーム表示
            objGraph.DrawImage(imgPathFrameExhBarAnalog, mCstMarginX_Frame, mCstMarginY_Frame, imgPathFrameExhBarAnalog.Width, imgPathFrameExhBarAnalog.Height)

            'グラフタイトル表示
            Dim strTitle As String = Format(mintGroupNo + 1, "00") & "  " & mudtSetOpsGraphBar.strTitle
            objGraph.DrawString(strTitle, gFnt12, gFntColorBlack, mCstMarginX_GraphTitle, mCstMarginY_GraphTitle)

            'ラベル表示（ItemUp, ItemDown）
            Call BarGrpPreviewDrawLabel(objGraph)

            '分割数によって表示切換え
            Select Case mudtSetOpsGraphBar.bytDevision
                Case gCstCodeOpsBarGraphDivision4 : Call BarGrpPreviewType4(objGraph)       '4分割
                Case gCstCodeOpsBarGraphDivision6 : Call BarGrpPreviewType6(objGraph)       '6分割
                Case gCstCodeOpsBarGraphDivision3_5 : Call BarGrpPreviewType3_5(objGraph)   '3×5分割
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "バーグラフ表示詳細"

    '----------------------------------------------------------------------------
    ' 機能説明  ： バーグラフ（4分割）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub BarGrpPreviewType4(ByVal objGraph As System.Drawing.Graphics)

        Try

            Dim strGraphDiv(10) As String
            Dim strUnit As String = ""

            '目盛取得
            Call BarGrpPreviewType4Dvision(strGraphDiv, strUnit)

            'ラベル表示
            '2015.03.17 単位文字を8文字に統一、位置調整(OPSに合わせる)
            'strUnit = "[" & strUnit & "]"
            strUnit = "[" & strUnit.PadRight(8) & "]"
            'strUnit = strUnit.PadLeft(10)
            objGraph.DrawString(strUnit, gFnt11, gFntColorBlack, mCstMarginX_BarGraphLabel, mCstMarginY_BarGraphLabel)

            '図形/目盛表示
            Call BarGrpPreviewDrawGraph(objGraph, _
                                        gCstCodeOpsBarGraphDivision4, _
                                        mCstMarginX_BarGraphStartPosDraw, _
                                        mCstMarginY_BarGraphStartPosDraw, _
                                        mCstCodeBarGraphWidth, _
                                        mCstCodeBarGraphHeight4, _
                                        strGraphDiv)
            'グラフ表示 詳細
            Call BarGrpPreviewDrawGraphDetail(objGraph, _
                                              mCstMarginX_BarGraphStartPosDraw + 11, _
                                              mCstMarginY_BarGraphStartPosDrawBar + 1)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： バーグラフ（6分割）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub BarGrpPreviewType6(ByVal objGraph As System.Drawing.Graphics)

        Try

            Dim strGraphDiv(10) As String
            Dim strUnit As String = ""

            '目盛取得
            Call BarGrpPreviewType6Dvision(strGraphDiv, strUnit)

            'ラベル表示
            '2015.03.17 単位文字を8文字に統一、位置調整(OPSに合わせる)
            'strUnit = "[" & strUnit & "]"
            strUnit = "[" & strUnit.PadRight(8) & "]"
            'strUnit = strUnit.PadLeft(10)
            objGraph.DrawString(strUnit, gFnt11, gFntColorBlack, mCstMarginX_BarGraphLabel, mCstMarginY_BarGraphLabel)

            '図形/目盛表示
            Call BarGrpPreviewDrawGraph(objGraph, _
                                        gCstCodeOpsBarGraphDivision6, _
                                        mCstMarginX_BarGraphStartPosDraw, _
                                        mCstMarginY_BarGraphStartPosDraw, _
                                        mCstCodeBarGraphWidth, _
                                        mCstCodeBarGraphHeight6, _
                                        strGraphDiv)

            'グラフ表示 詳細
            Call BarGrpPreviewDrawGraphDetail(objGraph, _
                                              mCstMarginX_BarGraphStartPosDraw + 11, _
                                              mCstMarginY_BarGraphStartPosDrawBar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： バーグラフ（3×5分割）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub BarGrpPreviewType3_5(ByVal objGraph As System.Drawing.Graphics)

        Try

            Dim strGraphDiv(10) As String
            Dim strUnit As String = ""

            '目盛取得
            Call BarGrpPreviewType3_5Dvision(strGraphDiv, strUnit)

            'ラベル表示
            '2015.03.17 単位文字を8文字に統一、位置調整(OPSに合わせる)
            'strUnit = "[" & strUnit & "]"
            strUnit = "[" & strUnit.PadRight(8) & "]"
            'strUnit = strUnit.PadLeft(10)
            objGraph.DrawString(strUnit, gFnt11, gFntColorBlack, mCstMarginX_BarGraphLabel, mCstMarginY_BarGraphLabel)

            '図形/目盛表示
            Call BarGrpPreviewDrawGraph(objGraph, _
                                        gCstCodeOpsBarGraphDivision3_5, _
                                        mCstMarginX_BarGraphStartPosDraw, _
                                        mCstMarginY_BarGraphStartPosDraw, _
                                        mCstCodeBarGraphWidth, _
                                        mCstCodeBarGraphHeight3_5, _
                                        strGraphDiv)

            'グラフ表示 詳細
            Call BarGrpPreviewDrawGraphDetail(objGraph, _
                                              mCstMarginX_BarGraphStartPosDraw + 11, _
                                              mCstMarginY_BarGraphStartPosDrawBar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： バーグラフ　グラフ表示
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 分割タイプ
    '           ： ARG3 - (I ) Draw開始座標軸 X
    '           ： ARG4 - (I ) Draw開始座標軸 Y
    '           ： ARG5 - (I ) グラフ枠の横幅
    '           ： ARG6 - (I ) グラフ枠の高さ
    '           ： ARG7 - (I ) 目盛
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub BarGrpPreviewDrawGraph(ByVal objGraphics As System.Drawing.Graphics, _
                                       ByVal hintGraphType As Integer, _
                                       ByVal sngX As Single, _
                                       ByVal sngY As Single, _
                                       ByVal sngGraphWidth As Single, _
                                       ByVal sngGraphHeight As Single, _
                                       ByVal strDivision() As String)

        Try

            Dim i As Integer
            Dim strStrings As String
            Dim intLoopIdx As Integer = 1
            Dim sngStartPos As Single
            Dim sngLabelPos As Single = 110     '目盛表示開始位置
            Dim sngDistance As Single = 10      'グラフと目盛の間の距離

            '最初の高さ保持
            sngStartPos = sngY

            '分割タイプによってループ回数を変える
            Select Case hintGraphType
                Case gCstCodeOpsBarGraphDivision4 : intLoopIdx = mintIdxCol4
                Case gCstCodeOpsBarGraphDivision6 : intLoopIdx = mintIdxCol6
                Case gCstCodeOpsBarGraphDivision3_5 : intLoopIdx = mintIdxCol3
            End Select

            For i = mintIdxCol1 To intLoopIdx

                '上位4点の目盛表示
                strStrings = Microsoft.VisualBasic.Right(Space(10) & strDivision(i), 10)
                objGraphics.DrawString(strStrings, gFnt9, gFntColorBlack, sngX - sngLabelPos, sngY - 8)

                objGraphics.DrawLine(Pens.Black, sngX - 25, sngY, sngX - sngDistance, sngY)
                objGraphics.DrawRectangle(Pens.Black, sngX, sngY, sngGraphWidth, sngGraphHeight)

                '3×5分割の場合は、細かい目盛を表示する
                If hintGraphType = gCstCodeOpsBarGraphDivision3_5 Then
                    objGraphics.DrawLine(Pens.Black, sngX - 16, sngY + mCstCodeBarGraphHeight3_5detail * 1, sngX - sngDistance, sngY + mCstCodeBarGraphHeight3_5detail * 1)
                    objGraphics.DrawLine(Pens.Black, sngX - 16, sngY + mCstCodeBarGraphHeight3_5detail * 2, sngX - sngDistance, sngY + mCstCodeBarGraphHeight3_5detail * 2)
                    objGraphics.DrawLine(Pens.Black, sngX - 16, sngY + mCstCodeBarGraphHeight3_5detail * 3, sngX - sngDistance, sngY + mCstCodeBarGraphHeight3_5detail * 3)
                    objGraphics.DrawLine(Pens.Black, sngX - 16, sngY + mCstCodeBarGraphHeight3_5detail * 4, sngX - sngDistance, sngY + mCstCodeBarGraphHeight3_5detail * 4)
                End If

                '次のグラフ描画位置へ
                sngY = sngY + sngGraphHeight

            Next

            '下限目盛の表示
            strStrings = Microsoft.VisualBasic.Right(Space(10) & strDivision(i), 10)
            objGraphics.DrawString(strStrings, gFnt9, gFntColorBlack, sngX - sngLabelPos, sngY - 8)

            objGraphics.DrawLine(Pens.Black, sngX - (sngDistance + 15), sngY, sngX - sngDistance, sngY)
            objGraphics.DrawLine(Pens.Black, sngX - sngDistance, sngStartPos, sngX - sngDistance, sngY)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： バーグラフ　グラフ表示詳細
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Draw開始座標軸 X
    '           ： ARG3 - (I ) Draw開始座標軸 Y
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub BarGrpPreviewDrawGraphDetail(ByVal objGraphics As System.Drawing.Graphics, _
                                             ByVal sngX As Single, _
                                             ByVal sngY As Single)

        Dim BarKosu As Integer
        Dim BarKankaku As Integer

        Try

            Dim i As Integer
            Dim strChNo As String = ""
            Dim strCylTitle As String = ""
            '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph→表示切替に変更 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'Dim bln20Graph As Boolean = IIf(mudtSetOpsGraphBar.byt20Graph, True, False)
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            Dim byt20GraphLine As Byte = mudtSetOpsGraphBar.bytLine
            Dim sngAdj As Single = mCstCodeGraphBarSpan / 2 'タイトルの表示調整

            BarKosu = mintBarGraphCylCnt

            Select Case BarKosu
                Case Is <= 8
                    BarKankaku = 113
                Case Is <= 12
                    BarKankaku = 73
                Case Is <= 16
                    BarKankaku = 57
                Case Is <= 20
                    BarKankaku = 44
                Case Is <= 24
                    BarKankaku = 36
            End Select

            If mintBarGraphCylCnt > 0 Then

                For i = 1 To mintBarGraphCylCnt

                    '未設定の場合はグラフ表示をスキップ
                    If gGetString(mudtSetOpsGraphBar.udtCylinder(i - 1).shtChCylinder) <> "0" Then

                        'チャンネル表示
                        'チャンネル表示
                        If gudt.SetSystem.udtSysOps.shtTagMode = 1 Then     ' Ver1.8.3  2015.11.26 ﾀｸﾞ表示追加
                            strChNo = "(" & GetTagNoFromCHNo(mudtSetOpsGraphBar.udtCylinder(i - 1).shtChCylinder) & ")"
                        Else
                            strChNo = "(" & Format(mudtSetOpsGraphBar.udtCylinder(i - 1).shtChCylinder, "0000") & ")"
                        End If

                        objGraphics.DrawString(strChNo, gFnt7, gFntColorBlack, sngX - 5, 700) '6

                        'タイトル
                        strCylTitle = gGetString(mudtSetOpsGraphBar.udtCylinder(i - 1).strTitle)
                        'Ver2.0.3.1 文字数をバイト数にすることで日本語もセンタリングできるように修正
                        Dim intBytesu As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(strCylTitle)
                        'sngAdj = (mCstCodeGraphBarSpan / 2) - (strCylTitle.Length * gFntScale7 / 2) - 5  '-5は微調整の結果値
                        sngAdj = (mCstCodeGraphBarSpan / 2) - (intBytesu * gFntScale7 / 2) - 5  '-5は微調整の結果値

                        If i Mod 2 = 0 Then '偶数行

                            'バー表示
                            objGraphics.FillRectangle(gFntColorBlack, _
                                                      sngX + 5, _
                                                      sngY - mCstCodeBarGraphBarHeight, _
                                                      mCstCodeBarGraphBarWidth, _
                                                      mCstCodeBarGraphBarHeight)

                            If (byt20GraphLine = gCstCodeOpsBarGraphLine2) Then
                                '2行表示
                                Select Case BarKosu
                                    Case Is <= 16
                                        '1行表示
                                        objGraphics.DrawString("******", gFnt9, gFntColorBlack, sngX - 11, 645) '2.0.8.B 650→645
                                    Case Is <= 20
                                        objGraphics.DrawString("****", gFnt9, gFntColorBlack, sngX - 4, 645) '2.0.8.B 650→645
                                    Case Else
                                        objGraphics.DrawString("****", gFnt9, gFntColorBlack, sngX - 4, 645) '2.0.8.B 650→645
                                End Select
                                ' objGraphics.DrawString("***", gFnt12, gFntColorBlack, sngX - 2, 660)            '***表示



                                objGraphics.DrawString(strCylTitle, gFnt9, gFntColorBlack, sngX + sngAdj, 675)  'タイトル表示 '2.0.8.B 685→675
                            Else
                                '1行表示
                                Select Case BarKosu
                                    Case Is <= 16
                                        '1行表示
                                        objGraphics.DrawString("******", gFnt9, gFntColorBlack, sngX - 11, 645) '2.0.8.B 650→645
                                    Case Is <= 20
                                        objGraphics.DrawString("****", gFnt9, gFntColorBlack, sngX - 4, 645) '2.0.8.B 650→645
                                    Case Else
                                        objGraphics.DrawString("****", gFnt9, gFntColorBlack, sngX - 4, 645) '2.0.8.B 650→645
                                End Select
                                'objGraphics.DrawString("***", gFnt12, gFntColorBlack, sngX - 2, 650)            '***表示
                                objGraphics.DrawString(strCylTitle, gFnt9, gFntColorBlack, sngX + sngAdj, 665)  'タイトル表示  '2.0.8.B 675→665
                            End If


                        Else '奇数行

                            'バー表示
                            objGraphics.FillRectangle(gFntColorBlack, _
                                                      sngX + 5, _
                                                      sngY - (mCstCodeBarGraphBarHeight + 70), _
                                                      mCstCodeBarGraphBarWidth, _
                                                      mCstCodeBarGraphBarHeight + 70)

                            '***表示
                            Select Case BarKosu
                                Case Is <= 16
                                    '1行表示
                                    objGraphics.DrawString("******", gFnt9, gFntColorBlack, sngX - 11, 645) '2.0.8.B 650→645
                                Case Is <= 20
                                    objGraphics.DrawString("****", gFnt9, gFntColorBlack, sngX - 4, 645) '2.0.8.B 650→645
                                Case Else
                                    objGraphics.DrawString("****", gFnt9, gFntColorBlack, sngX - 4, 645) '2.0.8.B 650→645
                            End Select
                            'objGraphics.DrawString("***", gFnt12, gFntColorBlack, sngX - 2, 650)

                            'タイトル表示
                            objGraphics.DrawString(strCylTitle, gFnt9, gFntColorBlack, sngX + sngAdj, 665)  '2.0.8.B 675→665

                        End If

                    End If

                        '次のグラフ描画位置へ
                        sngX = sngX + BarKankaku

                Next i

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： バーグラフ　ItemUp, ItemDownラベル表示
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub BarGrpPreviewDrawLabel(ByVal objGraph As System.Drawing.Graphics)

        Try

            Dim strItemUp As String = "", strItemDown As String = ""
            '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph→表示切替に変更 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            'Dim bln20Graph As Boolean = IIf(mudtSetOpsGraphBar.byt20Graph, True, False)
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
            Dim byt20GraphLine As Byte = mudtSetOpsGraphBar.bytLine

            '処理を抜ける条件
            If (mintBarGraphCylCnt < 0) Or (24 < mintBarGraphCylCnt) Then Exit Sub

            With mudtSetOpsGraphBar

                '文字は左詰め表示。文字列長が多少増えても吸収出来るよう最後にPadLeftを実施
                If (byt20GraphLine = gCstCodeOpsBarGraphLine2) Then

                    '2行表示
                    strItemUp = (Trim(.strItemUp).PadRight(4)).PadLeft(10)
                    objGraph.DrawString(strItemUp, gFnt10, gFntColorBlack, mCstMarginX_BarGraphStartPosDraw - 95, 665)      '' 675 → 665    2.0.8.B
                    strItemDown = (Trim(.strItemDown).PadRight(4)).PadLeft(10)
                    objGraph.DrawString(strItemDown, gFnt10, gFntColorBlack, mCstMarginX_BarGraphStartPosDraw - 95, 675)    '' 685 → 675    2.0.8.B

                Else

                    '1行表示
                    strItemUp = (Trim(.strItemUp).PadRight(4)).PadLeft(10)
                    objGraph.DrawString(strItemUp, gFnt10, gFntColorBlack, mCstMarginX_BarGraphStartPosDraw - 95, 665)      '' 675 → 665    2.0.8.B
                    strItemDown = (Trim(.strItemDown).PadRight(4)).PadLeft(10)
                    objGraph.DrawString(strItemDown, gFnt10, gFntColorBlack, mCstMarginX_BarGraphStartPosDraw - 95, 675)    '' 685 → 675    2.0.8.B

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub




#Region "分割別 目盛計算"

    '----------------------------------------------------------------------------
    ' 機能説明  ： バーグラフ　目盛・単位（4分割）
    ' 引数      ： ARG1 - (　O) 目盛値
    '           ： ARG2 - (　O) 単位
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub BarGrpPreviewType4Dvision(ByRef hstrDivision() As String, _
                                          ByRef hstrUnit As String)

        Try

            Dim i As Integer
            Dim intChNo As Integer

            Dim dblRangeH As Double
            Dim dblRangeMH As Double
            Dim dblRangeMM As Double
            Dim dblRangeML As Double
            Dim dblRangeL As Double

            Dim strRangeH As String = ""
            Dim strRangeMH As String = ""
            Dim strRangeMM As String = ""
            Dim strRangeML As String = ""
            Dim strRangeL As String = ""

            Dim intDecimalPosition As Integer
            Dim strDecimalFormat As String = ""

            '目盛初期化
            For i = 0 To UBound(hstrDivision)
                hstrDivision(i) = "0"
            Next

            '最初に設定されているチャンネルNOを取得する
            With mudtSetOpsGraphBar
                For i = LBound(.udtCylinder) To UBound(.udtCylinder)
                    If .udtCylinder(i).shtChCylinder <> 0 Then
                        'チャンネルNO取得
                        intChNo = .udtCylinder(i).shtChCylinder
                        Exit For
                    End If
                Next
            End With

            '上下限値の目盛と単位を取得する
            If (0 < intChNo) And (intChNo <= 65535) Then

                'チャンネルの配列Indexを取得する
                Dim intArrayIdx As Integer = gConvChNoToChArrayId(intChNo)

                'デジタル系のCHの場合は処理を抜ける
                If mCheckChTypeDigital(intArrayIdx) Then Exit Sub

                '表示切換え設定
                Dim intRangeType As Integer = mudtSetOpsGraphBar.bytDisplay

                If (0 <= intArrayIdx) And (intArrayIdx < 3000) Then

                    '表示レンジの場合分け
                    If intRangeType = gCstCodeOpsBarGraphRangeTypeMeasure Then

                        ''==================
                        '' 計測レンジ表示
                        ''==================
                        '単位取得
                        Call mGetUnit(hstrUnit, intArrayIdx)

                        '小数点桁数取得
                        Call mGetDecimalInfo(intArrayIdx, intDecimalPosition, strDecimalFormat)

                        'H
                        dblRangeH = gudt.SetChInfo.udtChannel(intArrayIdx).AnalogRangeHigh / (10 ^ intDecimalPosition)
                        strRangeH = dblRangeH.ToString(strDecimalFormat)

                        'L
                        dblRangeL = gudt.SetChInfo.udtChannel(intArrayIdx).AnalogRangeLow / (10 ^ intDecimalPosition)
                        strRangeL = dblRangeL.ToString(strDecimalFormat)

                        'MH
                        ' 4分割できる場合に表示　2013.07.23 K.Fujimoto
                        If (dblRangeH - dblRangeL) Mod 4 = 0 Then
                            dblRangeMH = dblRangeL + Math.Round(((dblRangeH - dblRangeL) / 4) * 3, MidpointRounding.AwayFromZero)
                            strRangeMH = dblRangeMH.ToString(strDecimalFormat)
                        End If

                        'MM
                        ' 2分割できる場合に表示　2013.07.23 K.Fujimoto
                        If (dblRangeH - dblRangeL) Mod 2 = 0 Then
                            dblRangeMM = dblRangeL + Math.Round(((dblRangeH - dblRangeL) / 4) * 2, MidpointRounding.AwayFromZero)
                            strRangeMM = dblRangeMM.ToString(strDecimalFormat)
                        End If

                        'ML
                        ' 4分割できる場合に表示　2013.07.23 K.Fujimoto
                        If (dblRangeH - dblRangeL) Mod 4 = 0 Then
                            dblRangeML = dblRangeL + Math.Round(((dblRangeH - dblRangeL) / 4) * 1, MidpointRounding.AwayFromZero)
                            strRangeML = dblRangeML.ToString(strDecimalFormat)
                        End If


                    Else

                        ''==================
                        '' 100分率表示
                        ''==================
                        hstrUnit = "%"

                        strRangeH = "100"
                        strRangeMH = " 75"
                        strRangeMM = " 50"
                        strRangeML = " 25"
                        strRangeL = "  0"

                    End If

                End If

            End If

            '目盛設定
            hstrDivision(0) = strRangeH
            hstrDivision(1) = strRangeMH
            hstrDivision(2) = strRangeMM
            hstrDivision(3) = strRangeML
            hstrDivision(4) = strRangeL
            hstrDivision(5) = ""
            hstrDivision(6) = ""

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： バーグラフ　目盛・単位（6分割）
    ' 引数      ： ARG1 - (　O) 目盛値
    '           ： ARG2 - (　O) 単位
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub BarGrpPreviewType6Dvision(ByRef hstrDivision() As String, _
                                          ByRef hstrUnit As String)

        Try

            Dim i As Integer
            Dim intChNo As Integer

            Dim dblRangeHH As Double
            Dim dblRangeH As Double
            Dim dblRangeMH As Double
            Dim dblRangeMM As Double
            Dim dblRangeML As Double
            Dim dblRangeL As Double
            Dim dblRangeLL As Double

            Dim strRangeHH As String = ""
            Dim strRangeH As String = ""
            Dim strRangeMH As String = ""
            Dim strRangeMM As String = ""
            Dim strRangeML As String = ""
            Dim strRangeL As String = ""
            Dim strRangeLL As String = ""

            Dim intDecimalPosition As Integer
            Dim strDecimalFormat As String = ""

            '目盛初期化
            For i = 0 To UBound(hstrDivision)
                hstrDivision(i) = "0"
            Next

            '最初に設定されているチャンネルNOを取得する
            With mudtSetOpsGraphBar
                For i = LBound(.udtCylinder) To UBound(.udtCylinder)
                    If .udtCylinder(i).shtChCylinder <> 0 Then
                        'チャンネルNO取得
                        intChNo = .udtCylinder(i).shtChCylinder
                        Exit For
                    End If
                Next
            End With

            '上下限値の目盛と単位を取得する
            If (0 < intChNo) And (intChNo <= 65535) Then

                'チャンネルの配列Indexを取得する
                Dim intArrayIdx As Integer = gConvChNoToChArrayId(intChNo)

                'デジタル系のCHの場合は処理を抜ける
                If mCheckChTypeDigital(intArrayIdx) Then Exit Sub

                '表示切換え設定
                Dim intRangeType As Integer = mudtSetOpsGraphBar.bytDisplay

                If (0 <= intArrayIdx) And (intArrayIdx < 3000) Then

                    '表示レンジの場合分け
                    If intRangeType = gCstCodeOpsBarGraphRangeTypeMeasure Then

                        ''==================
                        '' 計測レンジ表示
                        ''==================
                        '単位取得
                        Call mGetUnit(hstrUnit, intArrayIdx)

                        '小数点桁数取得
                        Call mGetDecimalInfo(intArrayIdx, intDecimalPosition, strDecimalFormat)

                        'HH
                        dblRangeHH = gudt.SetChInfo.udtChannel(intArrayIdx).AnalogRangeHigh / (10 ^ intDecimalPosition)
                        strRangeHH = dblRangeHH.ToString(strDecimalFormat)

                        'LL
                        dblRangeLL = gudt.SetChInfo.udtChannel(intArrayIdx).AnalogRangeLow / (10 ^ intDecimalPosition)
                        strRangeLL = dblRangeLL.ToString(strDecimalFormat)

                        'H
                        dblRangeH = dblRangeLL + Math.Round(((dblRangeHH - dblRangeLL) / 6) * 5, MidpointRounding.AwayFromZero)
                        strRangeH = dblRangeH.ToString(strDecimalFormat)

                        'MH
                        dblRangeMH = dblRangeLL + Math.Round(((dblRangeHH - dblRangeLL) / 6) * 4, MidpointRounding.AwayFromZero)
                        strRangeMH = dblRangeMH.ToString(strDecimalFormat)

                        'MM
                        dblRangeMM = dblRangeLL + Math.Round(((dblRangeHH - dblRangeLL) / 6) * 3, MidpointRounding.AwayFromZero)
                        strRangeMM = dblRangeMM.ToString(strDecimalFormat)

                        'ML
                        dblRangeML = dblRangeLL + Math.Round(((dblRangeHH - dblRangeLL) / 6) * 2, MidpointRounding.AwayFromZero)
                        strRangeML = dblRangeML.ToString(strDecimalFormat)

                        'L
                        dblRangeL = dblRangeLL + Math.Round(((dblRangeHH - dblRangeLL) / 6) * 1, MidpointRounding.AwayFromZero)
                        strRangeL = dblRangeL.ToString(strDecimalFormat)

                    Else

                        ''==================
                        '' 100分率表示
                        ''==================
                        hstrUnit = "%"

                        strRangeHH = "100"
                        strRangeH = ""
                        strRangeMH = ""
                        strRangeMM = " 50"
                        strRangeML = ""
                        strRangeL = ""
                        strRangeLL = "  0"

                    End If

                End If

            End If

            '目盛設定
            hstrDivision(0) = strRangeHH
            hstrDivision(1) = strRangeH
            hstrDivision(2) = strRangeMH
            hstrDivision(3) = strRangeMM
            hstrDivision(4) = strRangeML
            hstrDivision(5) = strRangeL
            hstrDivision(6) = strRangeLL

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： バーグラフ　目盛・単位（3×5分割）
    ' 引数      ： ARG1 - (　O) 目盛値
    '           ： ARG2 - (　O) 単位
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub BarGrpPreviewType3_5Dvision(ByRef hstrDivision() As String, _
                                            ByRef hstrUnit As String)

        Try

            Dim i As Integer
            Dim intChNo As Integer

            Dim dblRangeH As Double
            Dim dblRangeMH As Double
            Dim dblRangeML As Double
            Dim dblRangeL As Double

            Dim strRangeH As String = ""
            Dim strRangeMH As String = ""
            Dim strRangeML As String = ""
            Dim strRangeL As String = ""

            Dim intDecimalPosition As Integer
            Dim strDecimalFormat As String = ""

            '目盛初期化
            For i = 0 To UBound(hstrDivision)
                hstrDivision(i) = "0"
            Next

            '最初に設定されているチャンネルNOを取得する
            With mudtSetOpsGraphBar
                For i = LBound(.udtCylinder) To UBound(.udtCylinder)
                    If .udtCylinder(i).shtChCylinder <> 0 Then
                        'チャンネルNO取得
                        intChNo = .udtCylinder(i).shtChCylinder
                        Exit For
                    End If
                Next
            End With

            '上下限値の目盛と単位を取得する
            If (0 < intChNo) And (intChNo <= 65535) Then

                'チャンネルの配列Indexを取得する
                Dim intArrayIdx As Integer = gConvChNoToChArrayId(intChNo)

                'デジタル系のCHの場合は処理を抜ける
                If mCheckChTypeDigital(intArrayIdx) Then Exit Sub

                '表示切換え設定
                Dim intRangeType As Integer = mudtSetOpsGraphBar.bytDisplay

                'レンジ取得
                If (0 <= intArrayIdx) And (intArrayIdx < 3000) Then

                    '表示レンジの場合分け
                    If intRangeType = gCstCodeOpsBarGraphRangeTypeMeasure Then

                        ''==================
                        '' 計測レンジ表示
                        ''==================
                        '単位取得
                        Call mGetUnit(hstrUnit, intArrayIdx)

                        '小数点桁数取得
                        Call mGetDecimalInfo(intArrayIdx, intDecimalPosition, strDecimalFormat)

                        'H
                        dblRangeH = gudt.SetChInfo.udtChannel(intArrayIdx).AnalogRangeHigh / (10 ^ intDecimalPosition)
                        strRangeH = dblRangeH.ToString(strDecimalFormat)

                        'L
                        dblRangeL = gudt.SetChInfo.udtChannel(intArrayIdx).AnalogRangeLow / (10 ^ intDecimalPosition)
                        strRangeL = dblRangeL.ToString(strDecimalFormat)

                        'MH
                        dblRangeMH = dblRangeL + Math.Round(((dblRangeH - dblRangeL) / 3) * 2, MidpointRounding.AwayFromZero)
                        strRangeMH = dblRangeMH.ToString(strDecimalFormat)

                        'ML
                        dblRangeML = dblRangeL + Math.Round(((dblRangeH - dblRangeL) / 3) * 1, MidpointRounding.AwayFromZero)
                        strRangeML = dblRangeML.ToString(strDecimalFormat)

                    Else

                        ''==================
                        '' 100分率表示
                        ''==================
                        hstrUnit = "%"

                        strRangeH = "100"
                        strRangeMH = ""
                        strRangeML = ""
                        strRangeL = "  0"

                    End If

                End If

            End If

            '目盛設定
            hstrDivision(0) = strRangeH
            hstrDivision(1) = strRangeMH
            hstrDivision(2) = strRangeML
            hstrDivision(3) = strRangeL

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region



#End Region

#Region "アナログメーター表示"

    '----------------------------------------------------------------------------
    ' 機能説明  ： アナログメーターグラフ表示
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub AnalogMeterGrpPreview(ByVal objGraph As System.Drawing.Graphics)

        Try

            'フレーム表示
            objGraph.DrawImage(imgPathFrameExhBarAnalog, mCstMarginX_Frame, mCstMarginY_Frame, imgPathFrameExhBarAnalog.Width, imgPathFrameExhBarAnalog.Height)

            'グラフタイトル表示
            Dim strTitle As String = Format(mintGroupNo + 1, "00") & "  " & mudtSetOpsGraphAnalogMeter.strTitle
            objGraph.DrawString(strTitle, gFnt12, gFntColorBlack, mCstMarginX_GraphTitle, mCstMarginY_GraphTitle)

            'メータータイプによって表示切換え
            Select Case mudtSetOpsGraphAnalogMeter.bytMeterType
                Case gCstCodeOpsAnalogMeterMeterType8 : Call AnalogMeterGrpPreviewType8(objGraph)          '8表示

                    'T.Ueki 8画面表示のみとするため未使用
                    'Case gCstCodeOpsAnalogMeterMeterType1_4 : Call AnalogMeterGrpPreviewType1_4(objGraph)      '1:4表示
                    'Case gCstCodeOpsAnalogMeterMeterType4_1 : Call AnalogMeterGrpPreviewType4_1(objGraph)      '4:1表示
                    'Case gCstCodeOpsAnalogMeterMeterType2_1_2 : Call AnalogMeterGrpPreviewType2_1_2(objGraph)  '2:1:2表示
                    'Case gCstCodeOpsAnalogMeterMeterType2 : Call AnalogMeterGrpPreviewType2(objGraph)          '2表示
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "アナログメーター表示 詳細"

    '----------------------------------------------------------------------------
    ' 機能説明  ： アナログメーターグラフ（8表示）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub AnalogMeterGrpPreviewType8(ByVal objGraph As System.Drawing.Graphics)

        Try

            '8表示の表示順序

            '　　1   2   3   4　← 列番号
            '　-----------------
            '　| 1 | 2 | 3 | 4 |
            '　|---|---|---|---|
            '　| 5 | 6 | 7 | 8 |
            '　-----------------

            Dim intDispOrder As Integer = 0

            For i As Integer = mintIdxCol1 To mintIdxCol4
                'Ver2.0.7.M (新デザイン)新デザイン対応
                If g_bytNEWDES = 1 Then
                    '新デザインは枠線無し
                Else
                    '8分割の下書き枠線描画 ----------------------------------------------------------------
                    '表示処理変更 2013.07.24 K.Fujimoto
                    objGraph.DrawRectangle(Pens.Black, _
                                           mCstMarginX_AnalogMeterPos + (i * (mCstCodeAnalogMeterBmpWidth + mCstMarginX_AnalogMeterDX)), _
                                           mCstMarginY_AnalogMeterRow1, _
                                           mCstCodeAnalogMeterBmpWidth, _
                                           mCstCodeAnalogMeterBmpHight)
                    objGraph.DrawRectangle(Pens.Black, _
                                           mCstMarginX_AnalogMeterPos + (i * (mCstCodeAnalogMeterBmpWidth + mCstMarginX_AnalogMeterDX)), _
                                           mCstMarginY_AnalogMeterRow1 + (mCstMarginY_AnalogMeterFrameHalf + mCstMarginX_AnalogMeterDX), _
                                           mCstCodeAnalogMeterBmpWidth, _
                                           mCstCodeAnalogMeterBmpHight)
                    '--------------------------------------------------------------------------------------
                End If

                'メーターの表示順序
                intDispOrder += 1

                '1～4列目：8分割グラフ表示
                Call AnalogMeterGrpPreviewUseBmp1_8(objGraph, i, intDispOrder, False)

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'T.Ueki 8画面表示のみとするため未使用
    ''----------------------------------------------------------------------------
    '' 機能説明  ： アナログメーターグラフ（1：4表示）
    '' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub AnalogMeterGrpPreviewType1_4(ByVal objGraph As System.Drawing.Graphics)

    '    Try

    '        '1:4表示の表示順序

    '        '　　1   2   3   4　← 列番号
    '        '　-----------------
    '        '　|       | 2 | 3 |
    '        '　|   1   |-------|
    '        '　|       | 4 | 5 |
    '        '　-----------------

    '        Dim intDispOrder As Integer = 0

    '        For i As Integer = mintIdxCol1 To mintIdxCol4

    '            Select Case i

    '                Case mintIdxCol1

    '                    'メーターの表示順序
    '                    intDispOrder += 1

    '                    '1列目：2分割グラフ表示
    '                    Call AnalogMeterGrpPreviewDiv2(objGraph, i, intDispOrder)

    '                Case mintIdxCol2

    '                    '2列目：何もなし（2分割グラフで使用）

    '                Case mintIdxCol3, mintIdxCol4

    '                    'メーターの表示順序
    '                    intDispOrder += 1

    '                    '3,4列目：8分割グラフ表示
    '                    Call AnalogMeterGrpPreviewUseBmp1_8(objGraph, i, intDispOrder)

    '            End Select

    '        Next

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： アナログメーターグラフ（4：1表示）
    '' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub AnalogMeterGrpPreviewType4_1(ByVal objGraph As System.Drawing.Graphics)

    '    Try

    '        '4:1表示の表示順序

    '        '　　1   2   3   4　← 列番号
    '        '　-----------------
    '        '　| 1 | 2 |       |
    '        '　|-------|   5   |
    '        '　| 3 | 4 |       |
    '        '　-----------------

    '        Dim intDispOrder As Integer = 0

    '        For i As Integer = mintIdxCol1 To mintIdxCol4

    '            Select Case i

    '                Case mintIdxCol1, mintIdxCol2

    '                    'メーターの表示順序
    '                    intDispOrder += 1

    '                    '1,2列目：8分割グラフ表示
    '                    Call AnalogMeterGrpPreviewUseBmp1_8(objGraph, i, intDispOrder)

    '                Case mintIdxCol3

    '                    'メーターの表示順序
    '                    intDispOrder = 5

    '                    '3列目：2分割グラフ表示
    '                    Call AnalogMeterGrpPreviewDiv2(objGraph, i, intDispOrder)

    '                Case mintIdxCol4

    '                    '4列目：何もなし（2分割グラフで使用）

    '            End Select

    '        Next

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： アナログメーターグラフ（2：1：2表示）
    '' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub AnalogMeterGrpPreviewType2_1_2(ByVal objGraph As System.Drawing.Graphics)

    '    Try

    '        '2:1:2表示の表示順序

    '        '　　1   2   3   4　← 列番号
    '        '　-----------------
    '        '　| 1 |       | 2 |
    '        '　|---|   5   |---|
    '        '　| 3 |       | 4 |
    '        '　-----------------

    '        Dim intDispOrder As Integer = 0

    '        For i As Integer = mintIdxCol1 To mintIdxCol4

    '            Select Case i

    '                Case mintIdxCol1

    '                    'メーターの表示順序
    '                    intDispOrder = 1

    '                    '1列目：8分割グラフ表示
    '                    Call AnalogMeterGrpPreviewUseBmp1_8(objGraph, i, intDispOrder)

    '                Case mintIdxCol2

    '                    'メーターの表示順序
    '                    intDispOrder = 5

    '                    '2列目：2分割グラフ表示
    '                    Call AnalogMeterGrpPreviewDiv2(objGraph, i, intDispOrder)

    '                Case mintIdxCol3

    '                    '3列目：何もなし（2分割グラフで使用）

    '                Case mintIdxCol4

    '                    'メーターの表示順序
    '                    intDispOrder = 2

    '                    '4列目：8分割グラフ表示
    '                    Call AnalogMeterGrpPreviewUseBmp1_8(objGraph, i, intDispOrder)

    '            End Select

    '        Next

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    ''----------------------------------------------------------------------------
    '' 機能説明  ： アナログメーターグラフ（2表示）
    '' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub AnalogMeterGrpPreviewType2(ByVal objGraph As System.Drawing.Graphics)

    '    Try

    '        '2表示の表示順序

    '        '　　1   2   3   4　← 列番号
    '        '　-----------------
    '        '　|       |       |
    '        '　|   1   |   2   |
    '        '　|       |       |
    '        '　-----------------

    '        Dim intDispOrder As Integer = 0

    '        For intColIndex As Integer = mintIdxCol1 To mintIdxCol4

    '            Select Case intColIndex

    '                Case mintIdxCol1

    '                    'メーターの表示順序
    '                    intDispOrder = 1

    '                    '1列目：2分割グラフ表示
    '                    Call AnalogMeterGrpPreviewDiv2(objGraph, intColIndex, intDispOrder)

    '                Case mintIdxCol2, mintIdxCol4

    '                    '2,4列目：何もなし（2分割グラフで使用）

    '                Case mintIdxCol3

    '                    'メーターの表示順序
    '                    intDispOrder = 2

    '                    '3列目：2分割グラフ表示
    '                    Call AnalogMeterGrpPreviewDiv2(objGraph, intColIndex, intDispOrder)

    '            End Select

    '        Next

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： アナログメーターグラフ 列描画（8分割×1列）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 列Index
    '           ： ARG3 - (I ) 表示Index
    '           ： ARG4 - (I ) TRUE:8表示メーター、FALSE:他メーター
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub AnalogMeterGrpPreviewUseBmp1_8(ByVal objGraph As System.Drawing.Graphics, _
                                               ByVal intColIndex As Integer, _
                                               ByVal intDispIndex As Integer, _
                                      Optional ByVal hblnNotType8 As Boolean = True)

        Try

            Dim strChNo As String = ""              'CH番号
            Dim strItemName As String = ""          'CH名称
            Dim bytScale As Byte                    '目盛分割数
            Dim intStartChIdxLower As Integer = 0   '下段に表示するグラフの開始Indexを指定
            Dim sngHalfStringLengthChno As Single   '文字列長の半分の位置（Chno）
            Dim sngHalfStringLength As Single       '文字列長の半分の位置（ItemName）
            Dim sngX_Chno As Single                 '書出し位置（Cnno）
            Dim sngX_ItemName As Single             '書出し位置（ItemName）
            Dim sngHalfBmpWidth As Single = mCstCodeAnalogMeterBmpWidth / 2  'BMPの半分の位置
            Dim posX As Integer

            'アナログメータ表示調整    2013.07.24  K.Fujimoto
            posX = mCstMarginX_AnalogMeterPos + (intColIndex * (mCstCodeAnalogMeterBmpWidth + mCstMarginX_AnalogMeterDX))

            '==================
            ' 上段設定
            '==================
            'CH番号の取得
            strChNo = gGetString(mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex - 1).shtChNo)

            If strChNo <> "0" Then

                '目盛分割数の取得
                If mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex - 1).bytScale = 0 Then '0の場合、分割数自動設定 hori
                    bytScale = frmOpsGraphAnalogMaterList.fnSetScale(strChNo)
                Else
                    bytScale = mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex - 1).bytScale
                End If
                'メーター毎の表示設定（画像、レンジ）
                Select Case bytScale
                    Case gCstCodeOpsAnalogMeterScale3 : Call AnalogMeterGrpPreviewUseBmp1_8Scale3(objGraph, intColIndex, mCstMarginY_AnalogMeterRow1, strChNo)
                    Case gCstCodeOpsAnalogMeterScale4 : Call AnalogMeterGrpPreviewUseBmp1_8Scale4(objGraph, intColIndex, mCstMarginY_AnalogMeterRow1, strChNo)
                    Case gCstCodeOpsAnalogMeterScale5 : Call AnalogMeterGrpPreviewUseBmp1_8Scale5(objGraph, intColIndex, mCstMarginY_AnalogMeterRow1, strChNo)
                    Case gCstCodeOpsAnalogMeterScale6 : Call AnalogMeterGrpPreviewUseBmp1_8Scale6(objGraph, intColIndex, mCstMarginY_AnalogMeterRow1, strChNo)
                    Case gCstCodeOpsAnalogMeterScale7 : Call AnalogMeterGrpPreviewUseBmp1_8Scale7(objGraph, intColIndex, mCstMarginY_AnalogMeterRow1, strChNo)
                End Select

                'チャンネル番号表示
                If gudt.SetSystem.udtSysOps.shtTagMode = 1 Then     ' Ver1.8.3  2015.11.26 ﾀｸﾞ表示追加
                    strChNo = "(" & GetTagNoFromCHNo(mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex - 1).shtChNo) & ")"
                    sngHalfStringLengthChno = (strChNo.Length * gFntScale9) / 2  '文字列長の半分の位置
                Else
                    strChNo = "(" & Format(mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex - 1).shtChNo, "0000") & ")"
                    sngHalfStringLengthChno = (strChNo.Length * gFntScale12) / 2  '文字列長の半分の位置
                End If

                sngX_Chno = sngHalfBmpWidth - sngHalfStringLengthChno         '書出し位置

                'T.ueki チャンネル番号黒表示に修正
                If gudt.SetSystem.udtSysOps.shtTagMode = 1 Then     ' Ver1.8.3  2015.11.26 ﾀｸﾞ名称印字時はﾌｫﾝﾄを小さくする
                    objGraph.DrawString(strChNo, _
                                gFnt9, _
                                gFntColorBlack, _
                                posX + sngX_Chno, _
                                mCstMarginY_AnalogMeterDiv8ChNoUpper)
                Else
                    objGraph.DrawString(strChNo, _
                                    gFnt12, _
                                    gFntColorBlack, _
                                    posX + sngX_Chno, _
                                    mCstMarginY_AnalogMeterDiv8ChNoUpper)
                End If


                'チャンネル名称表示
                strItemName = gGetChNoToChName(mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex - 1).shtChNo)

                '▼▼▼ 20110705 小さいアナログメーターのチャンネル名表示を１行にするにはこの辺を修正する ▼▼▼▼

                '-----------
                ''1行表示
                '-----------
                ' メータ詳細設定による表示位置    2013.07.24  K.Fujimoto
                If mudtSetOpsGraph.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint = 1 Then       '' LEFT
                    sngX_ItemName = 5
                ElseIf mudtSetOpsGraph.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint = 3 Then   '' RIGHT
                    '' Ver1.8.2 2015.11.19 和文の場合はｻｲｽﾞが異なっていたので修正
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then
                        sngHalfStringLength = 244 - ((LenB(strItemName) * gFntScale9j) + 2)
                    Else
                        sngHalfStringLength = 244 - ((strItemName.Length * gFntScale9) + 2)
                    End If
                    ''//
                    sngX_ItemName = 3 + sngHalfStringLength
                Else                                    '' CENTER
                    '文字列長の半分の位置
                    '' Ver1.8.2 2015.11.19 和文の場合はｻｲｽﾞが異なっていたので修正
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then
                        sngHalfStringLength = (LenB(strItemName) * gFntScale9j) / 2
                    Else
                        sngHalfStringLength = (strItemName.Length * gFntScale9) / 2
                    End If
                    ''//
                    sngX_ItemName = sngHalfBmpWidth - sngHalfStringLength           '書出し位置
                End If

                If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then     '' 和文表示の場合  2014.05.19
                    objGraph.DrawString(strItemName, _
                                    gFnt9j, _
                                    gFntColorBlack, _
                                    posX + sngX_ItemName, _
                                    mCstMarginY_AnalogMeterDiv8ItemNameUpperSingle)
                Else
                    objGraph.DrawString(strItemName, _
                                    gFnt9, _
                                    gFntColorBlack, _
                                    posX + sngX_ItemName, _
                                    mCstMarginY_AnalogMeterDiv8ItemNameUpperSingle)
                End If

                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            End If

                '==================
                ' 下段設定
                '==================
                '下段に表示するChIdxの設定
                If hblnNotType8 Then intStartChIdxLower = 2
                If Not hblnNotType8 Then intStartChIdxLower = 4

                'CH番号の取得
                strChNo = gGetString(mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex + intStartChIdxLower - 1).shtChNo)

                If strChNo <> "0" Then


                    '目盛分割数の取得
                If mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex + intStartChIdxLower - 1).bytScale = 0 Then '0の場合、分割数自動設定 hori
                    bytScale = frmOpsGraphAnalogMaterList.fnSetScale(strChNo)
                Else
                    bytScale = mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex + intStartChIdxLower - 1).bytScale
                End If

                'メーター毎の表示設定（画像、レンジ）
                Select Case bytScale
                    Case gCstCodeOpsAnalogMeterScale3 : Call AnalogMeterGrpPreviewUseBmp1_8Scale3(objGraph, intColIndex, mCstMarginY_AnalogMeterRow2, strChNo)
                    Case gCstCodeOpsAnalogMeterScale4 : Call AnalogMeterGrpPreviewUseBmp1_8Scale4(objGraph, intColIndex, mCstMarginY_AnalogMeterRow2, strChNo)
                    Case gCstCodeOpsAnalogMeterScale5 : Call AnalogMeterGrpPreviewUseBmp1_8Scale5(objGraph, intColIndex, mCstMarginY_AnalogMeterRow2, strChNo)
                    Case gCstCodeOpsAnalogMeterScale6 : Call AnalogMeterGrpPreviewUseBmp1_8Scale6(objGraph, intColIndex, mCstMarginY_AnalogMeterRow2, strChNo)
                    Case gCstCodeOpsAnalogMeterScale7 : Call AnalogMeterGrpPreviewUseBmp1_8Scale7(objGraph, intColIndex, mCstMarginY_AnalogMeterRow2, strChNo)
                End Select

                'チャンネル番号表示
                If gudt.SetSystem.udtSysOps.shtTagMode = 1 Then     ' Ver1.8.3  2015.11.26 ﾀｸﾞ表示追加
                    strChNo = "(" & GetTagNoFromCHNo(mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex + intStartChIdxLower - 1).shtChNo) & ")"
                    sngHalfStringLengthChno = (strChNo.Length * gFntScale9) / 2  '文字列長の半分の位置
                Else
                    strChNo = "(" & Format(mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex + intStartChIdxLower - 1).shtChNo, "0000") & ")"
                    sngHalfStringLengthChno = (strChNo.Length * gFntScale12) / 2  '文字列長の半分の位置
                End If

                sngX_Chno = sngHalfBmpWidth - sngHalfStringLengthChno         '書出し位置
                'T.ueki チャンネル番号黒表示に修正
                If gudt.SetSystem.udtSysOps.shtTagMode = 1 Then     ' Ver1.8.3  2015.11.26 ﾀｸﾞ名称印字時はﾌｫﾝﾄを小さくする
                    objGraph.DrawString(strChNo, _
                                gFnt9, _
                                gFntColorBlack, _
                                posX + sngX_Chno, _
                                mCstMarginY_AnalogMeterDiv8ChNoLower)
                Else
                    objGraph.DrawString(strChNo, _
                                        gFnt12, _
                                        gFntColorBlack, _
                                        posX + sngX_Chno, _
                                        mCstMarginY_AnalogMeterDiv8ChNoLower)
                End If

                'チャンネル名称表示
                strItemName = gGetChNoToChName(mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex + intStartChIdxLower - 1).shtChNo)


                '-----------
                ''1行表示
                '-----------
                ' メータ詳細設定による表示位置    2013.07.24  K.Fujimoto
                If mudtSetOpsGraph.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint = 1 Then       '' LEFT
                    sngX_ItemName = 5
                ElseIf mudtSetOpsGraph.udtGraphAnalogMeterSettingRec.bytChNameDisplayPoint = 3 Then   '' RIGHT
                    '' Ver1.8.2 2015.11.19 和文の場合はｻｲｽﾞが異なっていたので修正
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then
                        sngHalfStringLength = 244 - ((LenB(strItemName) * gFntScale9j) + 2)
                    Else
                        sngHalfStringLength = 244 - ((strItemName.Length * gFntScale9) + 2)
                    End If

                    sngX_ItemName = 3 + sngHalfStringLength
                Else                                    '' CENTER
                    '文字列長の半分の位置
                    '' Ver1.8.2 2015.11.19 和文の場合はｻｲｽﾞが異なっていたので修正
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then
                        sngHalfStringLength = (LenB(strItemName) * gFntScale9j) / 2
                    Else
                        sngHalfStringLength = (strItemName.Length * gFntScale9) / 2
                    End If

                    sngX_ItemName = sngHalfBmpWidth - sngHalfStringLength           '書出し位置
                End If

                If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then     '' 和文表示の場合  2014.05.19
                    objGraph.DrawString(strItemName, _
                                    gFnt9j, _
                                    gFntColorBlack, _
                                    posX + sngX_ItemName, _
                                    mCstMarginY_AnalogMeterDiv8ItemNameLowerSingle)
                Else
                    objGraph.DrawString(strItemName, _
                                    gFnt9, _
                                    gFntColorBlack, _
                                    posX + sngX_ItemName, _
                                    mCstMarginY_AnalogMeterDiv8ItemNameLowerSingle)
                End If


            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： アナログメーターグラフ 列描画（2分割×1列）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 列Index
    '           ： ARG3 - (I ) 表示Index
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub AnalogMeterGrpPreviewDiv2(ByVal objGraph As System.Drawing.Graphics, _
                                          ByVal intColIndex As Integer, _
                                          ByVal intDispIndex As Integer)

        Try

            'BMPの半分の位置
            Dim sngHalfBmpWidth As Single = (mCstCodeAnalogMeterBmpWidth * 2) / 2

            'CH番号の取得
            Dim strChNo As String = gGetString(mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex - 1).shtChNo)

            If strChNo <> "0" Then

                '目盛分割数の取得
                Dim bytScale As Byte = mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex - 1).bytScale

                'メーター毎の表示設定（画像、レンジ）
                Select Case bytScale
                    Case gCstCodeOpsAnalogMeterScale3 : Call AnalogMeterGrpPreviewUseBmp1_2Scale3(objGraph, intColIndex, mCstMarginY_AnalogMeterRow1, strChNo)
                    Case gCstCodeOpsAnalogMeterScale4 : Call AnalogMeterGrpPreviewUseBmp1_2Scale4(objGraph, intColIndex, mCstMarginY_AnalogMeterRow1, strChNo)
                    Case gCstCodeOpsAnalogMeterScale5 : Call AnalogMeterGrpPreviewUseBmp1_2Scale5(objGraph, intColIndex, mCstMarginY_AnalogMeterRow1, strChNo)
                    Case gCstCodeOpsAnalogMeterScale6 : Call AnalogMeterGrpPreviewUseBmp1_2Scale6(objGraph, intColIndex, mCstMarginY_AnalogMeterRow1, strChNo)
                    Case gCstCodeOpsAnalogMeterScale7 : Call AnalogMeterGrpPreviewUseBmp1_2Scale7(objGraph, intColIndex, mCstMarginY_AnalogMeterRow1, strChNo)
                End Select

                'チャンネル番号表示
                strChNo = "(" & Format(mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex - 1).shtChNo, "0000") & ")"
                objGraph.DrawString(strChNo, _
                                    gFnt16, _
                                    gFntColorWhite, _
                                    mCstMarginX_AnalogMeterDiv2ChNo + intColIndex * mCstCodeAnalogMeterBmpWidth, _
                                    mCstMarginY_AnalogMeterDiv2ChNo)

                'チャンネル名称表示
                Dim strItemName As String = gGetChNoToChName(mudtSetOpsGraphAnalogMeter.udtDetail(intDispIndex - 1).shtChNo)
                Dim sngHalfStringLength As Single = (strItemName.Length * gFntScale14) / 2  '文字列長の半分の位置
                Dim sngX_ItemName As Single = sngHalfBmpWidth - sngHalfStringLength         '書出し位置

                objGraph.DrawString(strItemName, _
                                    gFnt16, _
                                    gFntColorBlack, _
                                    mCstMarginX_DrawStartPos + (intColIndex * mCstCodeAnalogMeterBmpWidth) + sngX_ItemName, _
                                    mCstMarginY_AnalogMeterDiv2ItemName)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "スケール毎の処理"

    '----------------------------------------------------------------------------
    ' 機能説明  ： スケール毎の処理 ＠BMP 1/8サイズ
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 列Index
    '           ： ARG3 - (I ) 表示する高さ
    '           ： ARG4 - (I ) チャンネル番号
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub AnalogMeterGrpPreviewUseBmp1_8Scale3(ByVal objGraph As System.Drawing.Graphics, _
                                                     ByVal hintColIndex As Integer, _
                                                     ByVal hsngHeight As Single, _
                                                     ByVal hstrChNo As String)

        Try

            Dim strMax As String = "", strMin As String = "", strUnit As String = ""
            Dim posX As Integer

            Dim MeterType As Integer = 3

            'アナログメータ表示調整    2013.07.24  K.Fujimoto
            posX = mCstMarginX_AnalogMeterPos + (hintColIndex * (mCstCodeAnalogMeterBmpWidth + mCstMarginX_AnalogMeterDX))

            'アナログメーター画像表示
            objGraph.DrawImage(imgPathAnalog8_3, _
                               posX, _
                               hsngHeight, _
                               imgPathAnalog8_3.Width, _
                               imgPathAnalog8_3.Height)

            '上下限値と単位の取得
            Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit, MeterType)

            '引数追加　T.Ueki 2015/4/27
            'Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit)

            '上限値
            objGraph.DrawString(strMax.PadLeft(9), _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + mCstMarginX_AnalogMeterDiv8RangeMax, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Range)

            '下限値
            objGraph.DrawString(strMin.PadRight(9), _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + mCstMarginX_AnalogMeterDiv8RangeMin, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Range)

            '単位
            Dim sngHalfBmpWidth As Single = mCstCodeAnalogMeterBmpWidth / 2             'BMPの半分の位置
            Dim sngHalfStringLength As Single = (strUnit.Length * gFntScale10) / 2      '文字列長の半分の位置
            Dim sngMarginX_Unit As Single = sngHalfBmpWidth - sngHalfStringLength       '書出し位置
            objGraph.DrawString(strUnit, _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + sngMarginX_Unit, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Unit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub AnalogMeterGrpPreviewUseBmp1_8Scale4(ByVal objGraph As System.Drawing.Graphics, _
                                                     ByVal hintColIndex As Integer, _
                                                     ByVal hsngHeight As Single, _
                                                     ByVal hstrChNo As String)

        Try

            Dim strMax As String = "", strMin As String = "", strUnit As String = ""
            Dim posX As Integer

            Dim MeterType As Integer = 4

            'アナログメータ表示調整    2013.07.24  K.Fujimoto
            posX = mCstMarginX_AnalogMeterPos + (hintColIndex * (mCstCodeAnalogMeterBmpWidth + mCstMarginX_AnalogMeterDX))

            'アナログメーター画像表示
            objGraph.DrawImage(imgPathAnalog8_4, _
                               posX, _
                               hsngHeight, _
                               imgPathAnalog8_3.Width, _
                               imgPathAnalog8_3.Height)

            '上下限値と単位の取得
            Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit, MeterType)

            '引数追加　T.Ueki 2015/4/27
            'Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit)

            '上限値
            objGraph.DrawString(strMax.PadLeft(9), _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + mCstMarginX_AnalogMeterDiv8RangeMax, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Range)

            '下限値
            objGraph.DrawString(strMin.PadRight(9), _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + mCstMarginX_AnalogMeterDiv8RangeMin, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Range)

            '単位
            Dim sngHalfBmpWidth As Single = mCstCodeAnalogMeterBmpWidth / 2             'BMPの半分の位置
            Dim sngHalfStringLength As Single = (strUnit.Length * gFntScale10) / 2      '文字列長の半分の位置
            Dim sngMarginX_Unit As Single = sngHalfBmpWidth - sngHalfStringLength       '書出し位置
            objGraph.DrawString(strUnit, _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + sngMarginX_Unit, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Unit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub AnalogMeterGrpPreviewUseBmp1_8Scale5(ByVal objGraph As System.Drawing.Graphics, _
                                                     ByVal hintColIndex As Integer, _
                                                     ByVal hsngHeight As Single, _
                                                     ByVal hstrChNo As String)

        Try

            Dim strMax As String = "", strMin As String = "", strUnit As String = ""
            Dim posX As Integer

            'T.Ueki
            Dim MeterType As Integer = 5

            'アナログメータ表示調整    2013.07.24  K.Fujimoto
            posX = mCstMarginX_AnalogMeterPos + (hintColIndex * (mCstCodeAnalogMeterBmpWidth + mCstMarginX_AnalogMeterDX))

            'アナログメーター画像表示
            objGraph.DrawImage(imgPathAnalog8_5, _
                               posX, _
                               hsngHeight, _
                               imgPathAnalog8_3.Width, _
                               imgPathAnalog8_3.Height)

            '上下限値と単位の取得
            Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit, MeterType)

            '引数追加　T.Ueki 2015/4/27
            'Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit)


            '上限値
            objGraph.DrawString(strMax.PadLeft(9), _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + mCstMarginX_AnalogMeterDiv8RangeMax, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Range)

            '下限値
            objGraph.DrawString(strMin.PadRight(9), _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + mCstMarginX_AnalogMeterDiv8RangeMin, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Range)

            '単位
            Dim sngHalfBmpWidth As Single = mCstCodeAnalogMeterBmpWidth / 2             'BMPの半分の位置
            Dim sngHalfStringLength As Single = (strUnit.Length * gFntScale10) / 2      '文字列長の半分の位置
            Dim sngMarginX_Unit As Single = sngHalfBmpWidth - sngHalfStringLength       '書出し位置
            objGraph.DrawString(strUnit, _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + sngMarginX_Unit, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Unit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub AnalogMeterGrpPreviewUseBmp1_8Scale6(ByVal objGraph As System.Drawing.Graphics, _
                                                     ByVal hintColIndex As Integer, _
                                                     ByVal hsngHeight As Single, _
                                                     ByVal hstrChNo As String)

        Try

            Dim strMax As String = "", strMin As String = "", strUnit As String = ""
            Dim posX As Integer

            Dim MeterType As Integer = 6

            'アナログメータ表示調整    2013.07.24  K.Fujimoto
            posX = mCstMarginX_AnalogMeterPos + (hintColIndex * (mCstCodeAnalogMeterBmpWidth + mCstMarginX_AnalogMeterDX))

            'アナログメーター画像表示
            objGraph.DrawImage(imgPathAnalog8_6, _
                               posX, _
                               hsngHeight, _
                               imgPathAnalog8_3.Width, _
                               imgPathAnalog8_3.Height)

            '上下限値と単位の取得
            Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit, MeterType)

            '引数追加　T.Ueki 2015/4/27
            'Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit)

            '上限値
            objGraph.DrawString(strMax.PadLeft(9), _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + mCstMarginX_AnalogMeterDiv8RangeMax, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Range)

            '下限値
            objGraph.DrawString(strMin.PadRight(9), _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + mCstMarginX_AnalogMeterDiv8RangeMin, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Range)

            '単位
            Dim sngHalfBmpWidth As Single = mCstCodeAnalogMeterBmpWidth / 2             'BMPの半分の位置
            Dim sngHalfStringLength As Single = (strUnit.Length * gFntScale10) / 2      '文字列長の半分の位置
            Dim sngMarginX_Unit As Single = sngHalfBmpWidth - sngHalfStringLength       '書出し位置
            objGraph.DrawString(strUnit, _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + sngMarginX_Unit, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Unit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub AnalogMeterGrpPreviewUseBmp1_8Scale7(ByVal objGraph As System.Drawing.Graphics, _
                                                     ByVal hintColIndex As Integer, _
                                                     ByVal hsngHeight As Single, _
                                                     ByVal hstrChNo As String)

        Try

            Dim strMax As String = "", strMin As String = "", strUnit As String = ""
            Dim posX As Integer

            Dim MeterType As Integer = 7

            'アナログメータ表示調整    2013.07.24  K.Fujimoto
            posX = mCstMarginX_AnalogMeterPos + (hintColIndex * (mCstCodeAnalogMeterBmpWidth + mCstMarginX_AnalogMeterDX))

            'アナログメーター画像表示
            objGraph.DrawImage(imgPathAnalog8_7, _
                               posX, _
                               hsngHeight, _
                               imgPathAnalog8_3.Width, _
                               imgPathAnalog8_3.Height)

            '上下限値と単位の取得
            Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit, MeterType)

            '引数追加　T.Ueki 2015/4/27
            'Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit)

            '上限値
            objGraph.DrawString(strMax.PadLeft(9), _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + mCstMarginX_AnalogMeterDiv8RangeMax, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Range)

            '下限値
            objGraph.DrawString(strMin.PadRight(9), _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + mCstMarginX_AnalogMeterDiv8RangeMin, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Range)

            '単位
            Dim sngHalfBmpWidth As Single = mCstCodeAnalogMeterBmpWidth / 2             'BMPの半分の位置
            Dim sngHalfStringLength As Single = (strUnit.Length * gFntScale10) / 2      '文字列長の半分の位置
            Dim sngMarginX_Unit As Single = sngHalfBmpWidth - sngHalfStringLength       '書出し位置
            objGraph.DrawString(strUnit, _
                                gFnt10, _
                                gFntColorBlack, _
                                posX + sngMarginX_Unit, _
                                hsngHeight + mCstMarginY_AnalogMeterDiv8Unit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



    '----------------------------------------------------------------------------
    ' 機能説明  ： スケール毎の処理 ＠BMP 1/2サイズ
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 列Index
    '           ： ARG3 - (I ) 表示する高さ
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub AnalogMeterGrpPreviewUseBmp1_2Scale3(ByVal objGraph As System.Drawing.Graphics, _
                                                     ByVal hintColIndex As Integer, _
                                                     ByVal hsngHeight As Single, _
                                                     ByVal hstrChNo As String)

        Try

            Dim strMax As String = "", strMin As String = "", strUnit As String = ""

            Dim MeterType As Integer = 3

            'アナログメーター画像表示
            objGraph.DrawImage(imgPathAnalog2_3, _
                               mCstMarginX_DrawStartPos + hintColIndex * mCstCodeAnalogMeterBmpWidth, _
                               mCstMarginY_AnalogMeterRow1, _
                               imgPathAnalog2_3.Width, _
                               imgPathAnalog2_3.Height)

            '上下限値と単位の取得
            Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit, MeterType)

            '引数追加　T.Ueki 2015/4/27
            'Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit)

            '上限値
            objGraph.DrawString(strMax.PadLeft(12), _
                                gFnt12, _
                                gFntColorBlack, _
                                mCstMarginX_AnalogMeterDiv2RangeMax + (hintColIndex * mCstCodeAnalogMeterBmpWidth), _
                                mCstMarginY_AnalogMeterDiv2Range)

            '下限値
            objGraph.DrawString(strMin.PadRight(12), _
                                gFnt12, _
                                gFntColorBlack, _
                                mCstMarginX_AnalogMeterDiv2RangeMin + (hintColIndex * mCstCodeAnalogMeterBmpWidth), _
                                mCstMarginY_AnalogMeterDiv2Range)

            '単位
            Dim sngHalfBmpWidth As Single = (mCstCodeAnalogMeterBmpWidth * 2) / 2       'BMPの半分の位置
            Dim sngHalfStringLength As Single = (strUnit.Length * gFntScale14) / 2      '文字列長の半分の位置
            Dim sngMarginX_Unit As Single = sngHalfBmpWidth - sngHalfStringLength       '書出し位置
            objGraph.DrawString(strUnit, _
                                gFnt14, _
                                gFntColorBlack, _
                                mCstMarginX_DrawStartPos + (hintColIndex * mCstCodeAnalogMeterBmpWidth) + sngMarginX_Unit, _
                                mCstMarginY_AnalogMeterDiv2Unit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub AnalogMeterGrpPreviewUseBmp1_2Scale4(ByVal objGraph As System.Drawing.Graphics, _
                                                     ByVal hintColIndex As Integer, _
                                                     ByVal hsngHeight As Single, _
                                                     ByVal hstrChNo As String)

        Try

            Dim strMax As String = "", strMin As String = "", strUnit As String = ""

            Dim MeterType As Integer = 4

            'アナログメーター画像表示
            objGraph.DrawImage(imgPathAnalog2_4, _
                               mCstMarginX_DrawStartPos + hintColIndex * mCstCodeAnalogMeterBmpWidth, _
                               mCstMarginY_AnalogMeterRow1, _
                               imgPathAnalog2_3.Width, _
                               imgPathAnalog2_3.Height)

            '上下限値と単位の取得
            Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit, MeterType)

            '引数追加　T.Ueki 2015/4/27
            'Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit)

            '上限値
            objGraph.DrawString(strMax.PadLeft(12), _
                                gFnt12, _
                                gFntColorBlack, _
                                mCstMarginX_AnalogMeterDiv2RangeMax + (hintColIndex * mCstCodeAnalogMeterBmpWidth), _
                                mCstMarginY_AnalogMeterDiv2Range)

            '下限値
            objGraph.DrawString(strMin.PadRight(12), _
                                gFnt12, _
                                gFntColorBlack, _
                                mCstMarginX_AnalogMeterDiv2RangeMin + (hintColIndex * mCstCodeAnalogMeterBmpWidth), _
                                mCstMarginY_AnalogMeterDiv2Range)

            '単位
            Dim sngHalfBmpWidth As Single = (mCstCodeAnalogMeterBmpWidth * 2) / 2       'BMPの半分の位置
            Dim sngHalfStringLength As Single = (strUnit.Length * gFntScale14) / 2      '文字列長の半分の位置
            Dim sngMarginX_Unit As Single = sngHalfBmpWidth - sngHalfStringLength       '書出し位置
            objGraph.DrawString(strUnit, _
                                gFnt14, _
                                gFntColorBlack, _
                                mCstMarginX_DrawStartPos + (hintColIndex * mCstCodeAnalogMeterBmpWidth) + sngMarginX_Unit, _
                                mCstMarginY_AnalogMeterDiv2Unit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub AnalogMeterGrpPreviewUseBmp1_2Scale5(ByVal objGraph As System.Drawing.Graphics, _
                                                     ByVal hintColIndex As Integer, _
                                                     ByVal hsngHeight As Single, _
                                                     ByVal hstrChNo As String)

        Try

            Dim strMax As String = "", strMin As String = "", strUnit As String = ""

            Dim MeterType As Integer = 5

            'アナログメーター画像表示
            objGraph.DrawImage(imgPathAnalog2_5, _
                               mCstMarginX_DrawStartPos + hintColIndex * mCstCodeAnalogMeterBmpWidth, _
                               mCstMarginY_AnalogMeterRow1, _
                               imgPathAnalog2_3.Width, _
                               imgPathAnalog2_3.Height)

            '上下限値と単位の取得
            Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit, MeterType)

            '引数追加　T.Ueki 2015/4/27
            'Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit)

            '上限値
            objGraph.DrawString(strMax.PadLeft(12), _
                                gFnt12, _
                                gFntColorBlack, _
                                mCstMarginX_AnalogMeterDiv2RangeMax + (hintColIndex * mCstCodeAnalogMeterBmpWidth), _
                                mCstMarginY_AnalogMeterDiv2Range)

            '下限値
            objGraph.DrawString(strMin.PadRight(12), _
                                gFnt12, _
                                gFntColorBlack, _
                                mCstMarginX_AnalogMeterDiv2RangeMin + (hintColIndex * mCstCodeAnalogMeterBmpWidth), _
                                mCstMarginY_AnalogMeterDiv2Range)

            '単位
            Dim sngHalfBmpWidth As Single = (mCstCodeAnalogMeterBmpWidth * 2) / 2       'BMPの半分の位置
            Dim sngHalfStringLength As Single = (strUnit.Length * gFntScale14) / 2      '文字列長の半分の位置
            Dim sngMarginX_Unit As Single = sngHalfBmpWidth - sngHalfStringLength       '書出し位置
            objGraph.DrawString(strUnit, _
                                gFnt14, _
                                gFntColorBlack, _
                                mCstMarginX_DrawStartPos + (hintColIndex * mCstCodeAnalogMeterBmpWidth) + sngMarginX_Unit, _
                                mCstMarginY_AnalogMeterDiv2Unit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub AnalogMeterGrpPreviewUseBmp1_2Scale6(ByVal objGraph As System.Drawing.Graphics, _
                                                     ByVal hintColIndex As Integer, _
                                                     ByVal hsngHeight As Single, _
                                                     ByVal hstrChNo As String)

        Try

            Dim strMax As String = "", strMin As String = "", strUnit As String = ""

            Dim MeterType As Integer = 6

            'アナログメーター画像表示
            objGraph.DrawImage(imgPathAnalog2_6, _
                               mCstMarginX_DrawStartPos + hintColIndex * mCstCodeAnalogMeterBmpWidth, _
                               mCstMarginY_AnalogMeterRow1, _
                               imgPathAnalog2_3.Width, _
                               imgPathAnalog2_3.Height)

            '上下限値と単位の取得
            Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit, MeterType)

            '引数追加　T.Ueki 2015/4/27
            'Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit)

            '上限値
            objGraph.DrawString(strMax.PadLeft(12), _
                                gFnt12, _
                                gFntColorBlack, _
                                mCstMarginX_AnalogMeterDiv2RangeMax + (hintColIndex * mCstCodeAnalogMeterBmpWidth), _
                                mCstMarginY_AnalogMeterDiv2Range)

            '下限値
            objGraph.DrawString(strMin.PadRight(12), _
                                gFnt12, _
                                gFntColorBlack, _
                                mCstMarginX_AnalogMeterDiv2RangeMin + (hintColIndex * mCstCodeAnalogMeterBmpWidth), _
                                mCstMarginY_AnalogMeterDiv2Range)

            '単位
            Dim sngHalfBmpWidth As Single = (mCstCodeAnalogMeterBmpWidth * 2) / 2       'BMPの半分の位置
            Dim sngHalfStringLength As Single = (strUnit.Length * gFntScale14) / 2      '文字列長の半分の位置
            Dim sngMarginX_Unit As Single = sngHalfBmpWidth - sngHalfStringLength       '書出し位置
            objGraph.DrawString(strUnit, _
                                gFnt14, _
                                gFntColorBlack, _
                                mCstMarginX_DrawStartPos + (hintColIndex * mCstCodeAnalogMeterBmpWidth) + sngMarginX_Unit, _
                                mCstMarginY_AnalogMeterDiv2Unit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub AnalogMeterGrpPreviewUseBmp1_2Scale7(ByVal objGraph As System.Drawing.Graphics, _
                                                     ByVal hintColIndex As Integer, _
                                                     ByVal hsngHeight As Single, _
                                                     ByVal hstrChNo As String)

        Try

            Dim strMax As String = "", strMin As String = "", strUnit As String = ""

            Dim MeterType As Integer = 7

            'アナログメーター画像表示
            objGraph.DrawImage(imgPathAnalog2_7, _
                               mCstMarginX_DrawStartPos + hintColIndex * mCstCodeAnalogMeterBmpWidth, _
                               mCstMarginY_AnalogMeterRow1, _
                               imgPathAnalog2_3.Width, _
                               imgPathAnalog2_3.Height)

            '上下限値と単位の取得
            Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit, MeterType)

            '引数追加　T.Ueki 2015/4/27
            'Call mGetLimitAndUnit(hstrChNo, strMax, strMin, strUnit)

            '上限値
            objGraph.DrawString(strMax.PadLeft(12), _
                                gFnt12, _
                                gFntColorBlack, _
                                mCstMarginX_AnalogMeterDiv2RangeMax + (hintColIndex * mCstCodeAnalogMeterBmpWidth), _
                                mCstMarginY_AnalogMeterDiv2Range)

            '下限値
            objGraph.DrawString(strMin.PadRight(12), _
                                gFnt12, _
                                gFntColorBlack, _
                                mCstMarginX_AnalogMeterDiv2RangeMin + (hintColIndex * mCstCodeAnalogMeterBmpWidth), _
                                mCstMarginY_AnalogMeterDiv2Range)

            '単位
            Dim sngHalfBmpWidth As Single = (mCstCodeAnalogMeterBmpWidth * 2) / 2       'BMPの半分の位置
            Dim sngHalfStringLength As Single = (strUnit.Length * gFntScale14) / 2      '文字列長の半分の位置
            Dim sngMarginX_Unit As Single = sngHalfBmpWidth - sngHalfStringLength       '書出し位置
            objGraph.DrawString(strUnit, _
                                gFnt14, _
                                gFntColorBlack, _
                                mCstMarginX_DrawStartPos + (hintColIndex * mCstCodeAnalogMeterBmpWidth) + sngMarginX_Unit, _
                                mCstMarginY_AnalogMeterDiv2Unit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#End Region



#End Region



#End Region

#Region "フリーグラフ表示"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フリーグラフ作成
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreview(ByVal objGraph As System.Drawing.Graphics)

        Try
            ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
            'Dim intChkUseBlock(mCstCodeFreeBlockNo32) As Integer    'ブロックの使用箇所
            'Dim strGraphTitle As String                             'グラフタイトル
            'Dim intRowIndex As Integer                              '行位置（0～3）
            'Dim intColIndex As Integer                              '列位置（0～7）

            ''グラフの枠表示
            'objGraph.DrawImage(imgPathFrameFree, mCstMarginX_Frame, mCstMarginY_Frame, imgPathFrameFree.Width, imgPathFrameFree.Height)

            ''グラフ名称表示
            'strGraphTitle = mudtSetOpsGraphFree.udtFreeGraphTitle(mintFreeGraphNo).strGraphTitle
            'strGraphTitle = Format(mintFreeGraphNo + 1, "00") & "  " & strGraphTitle
            'objGraph.DrawString(strGraphTitle, gFnt12, gFntColorBlack, mCstMarginX_GraphTitleFree, mCstMarginY_GraphTitleFree)

            ''ブロック毎の表示処理
            'For i As Integer = mCstCodeFreeBlockNo1 To mCstCodeFreeBlockNo32

            '    'ブロックで使用している箇所は処理をスキップする
            '    '（使用している場合はintUseChkFlg(i)に「1」が設定される）
            '    If intChkUseBlock(i) = 0 Then

            '        'ポジションの取得（Indexで保持）
            '        If i < mCstCodeFreeBlockNo9 Then
            '            intRowIndex = 0
            '            intColIndex = i - mCstCodeFreeBlockNo1
            '        ElseIf mCstCodeFreeBlockNo9 <= i And i < mCstCodeFreeBlockNo17 Then
            '            intRowIndex = 1
            '            intColIndex = i - mCstCodeFreeBlockNo9
            '        ElseIf mCstCodeFreeBlockNo17 <= i And i < mCstCodeFreeBlockNo25 Then
            '            intRowIndex = 2
            '            intColIndex = i - mCstCodeFreeBlockNo17
            '        ElseIf mCstCodeFreeBlockNo25 <= i And i <= mCstCodeFreeBlockNo32 Then
            '            intRowIndex = 3
            '            intColIndex = i - mCstCodeFreeBlockNo25
            '        End If

            '        'グラフタイプによって表示処理を分ける
            '        Select Case mudtSetOpsGraphFree.udtFreeGraphTitle(mintFreeGraphNo).udtFreeDetail(i - 1).bytType
            '            Case gCstCodeOpsFreeGrapTypeCounter

            '                intChkUseBlock(i) = 1
            '                intChkUseBlock(i + 1) = 1

            '                'カウンタ
            '                Call FreeGrpPreviewCounter(objGraph, i, intRowIndex, intColIndex)

            '            Case gCstCodeOpsFreeGrapTypeBar

            '                intChkUseBlock(i) = 1
            '                intChkUseBlock(i + 8) = 1

            '                'バーグラフ
            '                Call FreeGrpPreviewBar(objGraph, i, intRowIndex, intColIndex)

            '            Case gCstCodeOpsFreeGrapTypeAnalog

            '                intChkUseBlock(i) = 1
            '                intChkUseBlock(i + 1) = 1
            '                intChkUseBlock(i + 8) = 1
            '                intChkUseBlock(i + 9) = 1

            '                'アナログメーター
            '                Call FreeGrpPreviewAnalogMeter(objGraph, i, intRowIndex, intColIndex)

            '            Case gCstCodeOpsFreeGrapTypeIndicator

            '                intChkUseBlock(i) = 1

            '                'インジケータ（表示灯）
            '                Call FreeGrpPreviewIndicator(objGraph, i, intRowIndex, intColIndex)

            '        End Select

            '    End If

            'Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フリーグラフ >> カウンタ／アナログ／バー／インジケータ
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ループカウント   （1～32）
    '           ： ARG3 - (I ) ブロック行Index  （0～3）
    '           ： ARG4 - (I ) ブロック列Index  （0～7）
    ' 戻値      ： なし
    ' 備考      ： アナログ
    '           ： デジタル
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewCounter(ByVal objGraph As System.Drawing.Graphics, _
                                      ByVal hintLoopCnt As Integer, _
                                      ByVal hintRowIndex As Integer, _
                                      ByVal hintColIndex As Integer)

        Try
            ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
            ''カウンタ画像
            'objGraph.DrawImage(imgPathFreeCounter, _
            '                   mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
            '                   mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex)

            ''チャンネルNO
            'Dim intChNo As Integer = mudtSetOpsGraphFree.udtFreeGraphTitle(mintFreeGraphNo).udtFreeDetail(hintLoopCnt - 1).shtChNo
            'objGraph.DrawString("(" & Format(intChNo, "0000") & ")", _
            '                     gFnt9, _
            '                     gFntColorBlack, _
            '                     mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + mCstMarginX_FreeCounterChNo, _
            '                     mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeCounterChNo)

            ''ステータス・単位
            'Call FreeGrpPreviewCounterStatus(objGraph, hintRowIndex, hintColIndex, intChNo)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewBar(ByVal objGraph As System.Drawing.Graphics, _
                                  ByVal hintLoopCnt As Integer, _
                                  ByVal hintRowIndex As Integer, _
                                  ByVal hintColIndex As Integer)

        Try
            ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
            'Dim strMax As String = "", strMin As String = "", strUnit As String = ""

            ''目盛分割数
            'Dim bytScale As Byte = mudtSetOpsGraphFree.udtFreeGraphTitle(mintFreeGraphNo).udtFreeDetail(hintLoopCnt - 1).bytScale

            ''バー毎の表示設定
            'Select Case bytScale
            '    Case gCstCodeOpsBarGraphScale3 : Call FreeGrpPreviewBarScale3(objGraph, hintRowIndex, hintColIndex)
            '    Case gCstCodeOpsBarGraphScale4 : Call FreeGrpPreviewBarScale4(objGraph, hintRowIndex, hintColIndex)
            '    Case gCstCodeOpsBarGraphScale5 : Call FreeGrpPreviewBarScale5(objGraph, hintRowIndex, hintColIndex)
            '    Case gCstCodeOpsBarGraphScale6 : Call FreeGrpPreviewBarScale6(objGraph, hintRowIndex, hintColIndex)
            '    Case gCstCodeOpsBarGraphScale7 : Call FreeGrpPreviewBarScale7(objGraph, hintRowIndex, hintColIndex)
            'End Select

            ''チャンネルNO
            'Dim intChNo As Integer = mudtSetOpsGraphFree.udtFreeGraphTitle(mintFreeGraphNo).udtFreeDetail(hintLoopCnt - 1).shtChNo
            'Dim strChNo As String = "(" & Format(intChNo, "0000") & ")"
            'Dim sngHalfBmpWidth As Single = mCstCodeFreeBmpWidthBar / 2             'BMPの半分の位置
            'Dim sngHalfStringLength As Single = (strChNo.Length * gFntScale8) / 2   '文字列長の半分の位置
            'Dim sngMarginX_Chno As Single = sngHalfBmpWidth - sngHalfStringLength   '書出し位置
            'objGraph.DrawString(strChNo, _
            '                    gFnt9, _
            '                    gFntColorBlack, _
            '                    mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + sngMarginX_Chno, _
            '                    mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeBarChNo)

            ''上下限値
            'Call mGetLimitAndUnit(Format(intChNo, "0000"), strMax, strMin, strUnit)
            'objGraph.DrawString(strMax.PadRight(9), _
            '                    gFnt7, _
            '                    gFntColorBlack, _
            '                    mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + mCstMarginX_FreeBarHeightRange, _
            '                    mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeBarHeightRangeMax)
            'objGraph.DrawString(strMin.PadRight(9), _
            '                    gFnt7, _
            '                    gFntColorBlack, _
            '                    mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + mCstMarginX_FreeBarHeightRange, _
            '                    mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeBarHeightRangeMin)

            ''単位
            'objGraph.DrawString(strUnit.PadLeft(2), _
            '                    gFnt8, _
            '                    gFntColorBlack, _
            '                    mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + mCstMarginX_FreeBarUnit, _
            '                    mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeBarUnit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewAnalogMeter(ByVal objGraph As System.Drawing.Graphics, _
                                          ByVal hintLoopCnt As Integer, _
                                          ByVal hintRowIndex As Integer, _
                                          ByVal hintColIndex As Integer)

        Try
            ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
            'Dim strMax As String = "", strMin As String = "", strUnit As String = ""

            ''目盛分割数
            'Dim bytScale As Byte = mudtSetOpsGraphFree.udtFreeGraphTitle(mintFreeGraphNo).udtFreeDetail(hintLoopCnt - 1).bytScale

            ''アナログメーターの表示
            'Select Case bytScale
            '    Case gCstCodeOpsAnalogMeterScale3 : Call FreeGrpPreviewAnalogMeterScale3(objGraph, hintRowIndex, hintColIndex)
            '    Case gCstCodeOpsAnalogMeterScale4 : Call FreeGrpPreviewAnalogMeterScale4(objGraph, hintRowIndex, hintColIndex)
            '    Case gCstCodeOpsAnalogMeterScale5 : Call FreeGrpPreviewAnalogMeterScale5(objGraph, hintRowIndex, hintColIndex)
            '    Case gCstCodeOpsAnalogMeterScale6 : Call FreeGrpPreviewAnalogMeterScale6(objGraph, hintRowIndex, hintColIndex)
            '    Case gCstCodeOpsAnalogMeterScale7 : Call FreeGrpPreviewAnalogMeterScale7(objGraph, hintRowIndex, hintColIndex)
            'End Select

            ''チャンネルNO
            'Dim intChNo As Integer = mudtSetOpsGraphFree.udtFreeGraphTitle(mintFreeGraphNo).udtFreeDetail(hintLoopCnt - 1).shtChNo
            'objGraph.DrawString("(" & Format(intChNo, "0000") & ")", _
            '                    gFnt9, _
            '                    gFntColorBlack, _
            '                    mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + 10, _
            '                    mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstCodeFreeAnalogHeightChNo)

            ''上下限値
            'Call mGetLimitAndUnit(Format(intChNo, "0000"), strMax, strMin, strUnit)
            'objGraph.DrawString(strMax.PadLeft(9), _
            '                    gFnt8, _
            '                    gFntColorBlack, _
            '                    mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + mCstCodeFreeAnalogHeightRangeMax, _
            '                    mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstCodeFreeAnalogHeightRange)
            'objGraph.DrawString(strMin.PadRight(9), _
            '                    gFnt8, _
            '                    gFntColorBlack, _
            '                    mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + mCstCodeFreeAnalogHeightRangeMin, _
            '                    mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstCodeFreeAnalogHeightRange)

            ''単位
            'Dim sngHalfBmpWidth As Single = mCstCodeFreeBmpWidthAnalogMeter / 2         'BMPの半分の位置
            'Dim sngHalfStringLength As Single = (strUnit.Length * gFntScale10) / 2      '文字列長の半分の位置
            'Dim sngMarginX_Unit As Single = (sngHalfBmpWidth - sngHalfStringLength) - 2 '書出し位置
            'objGraph.DrawString(strUnit, _
            '                    gFnt12, _
            '                    gFntColorBlack, _
            '                    mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + sngMarginX_Unit, _
            '                    mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstCodeFreeAnalogHeightUnit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewIndicator(ByVal objGraph As System.Drawing.Graphics, _
                                        ByVal hintLoopCnt As Integer, _
                                        ByVal hintRowIndex As Integer, _
                                        ByVal hintColIndex As Integer)

        Try
            ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
            'Dim strChNo As String
            'Dim strMax As String = "", strMin As String = "", strUnit As String = ""
            'Dim strStatusAnalog As String = "", strStatusDigital As String = ""
            'Dim sngHalfBmpWidth As Single = mCstCodeFreeBmpWidthIndicator / 2   'BMPの半分の位置
            'Dim sngHalfStringLength As Single                                   '文字列長の半分の位置
            'Dim sngMarginX_Chno As Single                                       '書出し位置

            ''チャンネルNOの取得
            'Dim intChNo As Integer = mudtSetOpsGraphFree.udtFreeGraphTitle(mintFreeGraphNo).udtFreeDetail(hintLoopCnt - 1).shtChNo

            ''目盛分割数の取得
            'Dim bytScale As Byte = mudtSetOpsGraphFree.udtFreeGraphTitle(mintFreeGraphNo).udtFreeDetail(hintLoopCnt - 1).bytIndicatorKind

            ''インジケータ表示
            'Select Case bytScale
            '    Case gCstNameOpsFreeIndKindData : Call FreeGrpPreviewIndicatorData(objGraph, hintRowIndex, hintColIndex)
            '    Case gCstNameOpsFreeIndKindAlarm : Call FreeGrpPreviewIndicatorAlarm(objGraph, hintRowIndex, hintColIndex)
            '    Case gCstNameOpsFreeIndKindRepose : Call FreeGrpPreviewIndicatorRepose(objGraph, hintRowIndex, hintColIndex)
            '    Case gCstNameOpsFreeIndKindSensor : Call FreeGrpPreviewIndicatorSensor(objGraph, hintRowIndex, hintColIndex)
            'End Select

            ''グラフタイプが「Not Set」の場合はチャンネル番号表示する必要なし
            'If bytScale <> gCstNameOpsFreeIndKindNoSet Then

            '    'チャンネルNO表示
            '    strChNo = "(" & Format(intChNo, "0000") & ")"
            '    sngHalfStringLength = (strChNo.Length * gFntScale8) / 2       '文字列長の半分の位置
            '    sngMarginX_Chno = sngHalfBmpWidth - sngHalfStringLength       '書出し位置
            '    objGraph.DrawString(strChNo, _
            '                        gFnt9, _
            '                        gFntColorBlack, _
            '                        mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + sngMarginX_Chno, _
            '                        mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeIndicatorChNo)

            '    'ステータス表示
            '    Call FreeGrpPreviewIndicatorStatus(objGraph, hintRowIndex, hintColIndex, intChNo)

            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "カウンタ詳細"

    '----------------------------------------------------------------------------
    ' 機能説明  ： カウンタ >> ステータス表示
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ブロック行Index
    '           ： ARG3 - (I ) ブロック列Index
    '           ： ARG4 - (I ) チャンネルNO
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewCounterStatus(ByVal objGraph As System.Drawing.Graphics, _
                                            ByVal hintRowIndex As Integer, _
                                            ByVal hintColIndex As Integer, _
                                            ByVal hintChNo As Integer)
        Try

            'チャンネルの配列Index取得
            Dim intChIndex As Integer = gConvChNoToChArrayId(Format(hintChNo, "0000"))

            If intChIndex <> -1 Then

                'チャンネル種別の取得
                Dim shtChType As Short = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtChType

                'データ種別の取得
                Dim shtDataType As Short = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtData

                Select Case shtChType

                    Case gCstCodeChTypeAnalog, _
                         gCstCodeChTypePulse

                        'ステータス表示なし

                        '単位表示
                        Call FreeGrpPreviewCounterUnit(objGraph, hintRowIndex, hintColIndex, intChIndex)

                    Case gCstCodeChTypeDigital, _
                         gCstCodeChTypeComposite

                        'ステータス表示
                        Call FreeGrpPreviewCounterStatusDigital(objGraph, hintRowIndex, hintColIndex, intChIndex)

                        '単位表示なし

                    Case gCstCodeChTypeMotor

                        'ステータス表示
                        Call FreeGrpPreviewCounterStatusMotor(objGraph, hintRowIndex, hintColIndex, intChIndex)

                        '単位表示なし

                    Case gCstCodeChTypeValve

                        If shtDataType = gCstCodeChDataTypeValveDI_DO Then 'DIDO

                            'ステータス表示
                            Call FreeGrpPreviewCounterStatusDigital(objGraph, hintRowIndex, hintColIndex, intChIndex)

                            '単位表示なし

                        Else 'AIDO/AIAO

                            'ステータス表示なし

                            '単位表示
                            Call FreeGrpPreviewCounterUnit(objGraph, hintRowIndex, hintColIndex, intChIndex)

                        End If

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： カウンタ >> ステータス表示（デジタル関係）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ブロック行Index
    '           ： ARG3 - (I ) ブロック列Index
    '           ： ARG4 - (I ) チャンネルの配列Index
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewCounterStatusDigital(ByVal objGraph As System.Drawing.Graphics, _
                                                   ByVal hintRowIndex As Integer, _
                                                   ByVal hintColIndex As Integer, _
                                                   ByVal hintChIndex As Integer)

        Try

            Dim strStatus As String

            'ステータス種別コード取得
            Dim shtStatus As Short = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus

            'ステータス文字列取得
            If shtStatus = gCstCodeChManualInputStatus Then

                'ManualInput
                strStatus = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.strStatus

            Else

                'その他選択文字列
                cmbStatusDigital.SelectedValue = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus
                strStatus = cmbStatusDigital.Text

            End If

            'ステータス表示
            Dim sngHalfStringLength As Single = (strStatus.Length * gFntScale10) / 2                    '文字列長の半分の位置
            Dim sngX_Status As Single = mCstMarginX_FreeCounterStatusLabelCenter - sngHalfStringLength  'ステータス書出し位置
            objGraph.DrawString(strStatus, _
                                gFnt8, _
                                gFntColorBlack, _
                                mCstMarginX_FreeCounterStatusLabelStart + (mCstCodeFreeBlockWidth * hintColIndex) + sngX_Status, _
                                mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeCounterStatus)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： カウンタ >> ステータス表示（モーター）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ブロック行Index
    '           ： ARG3 - (I ) ブロック列Index
    '           ： ARG4 - (I ) チャンネルの配列Index
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewCounterStatusMotor(ByVal objGraph As System.Drawing.Graphics, _
                                                 ByVal hintRowIndex As Integer, _
                                                 ByVal hintColIndex As Integer, _
                                                 ByVal hintChIndex As Integer)

        Try

            Dim strStatus As String

            'Ver2.0.0.2 モーター種別増加 R Device ADD
            If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeMotorDevice Or _
                gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeMotorRDevice Then

                'DeviceOperation
                ' 2013.07.22 MO表示変更  K.Fujimoto
                'strStatus = "RUN/STOP"
                strStatus = "RUN"

            ElseIf gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus = gCstCodeChManualInputStatus Then

                'Manual Input
                strStatus = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.strStatus

            Else

                'ステータス取得
                strStatus = mGetStatusMotor(hintChIndex)

            End If

            'ステータス表示
            Dim sngHalfStringLength As Single = (strStatus.Length * gFntScale10) / 2                    '文字列長の半分の位置
            Dim sngX_Status As Single = mCstMarginX_FreeCounterStatusLabelCenter - sngHalfStringLength  'ステータス書出し位置
            objGraph.DrawString(strStatus, _
                                gFnt8, _
                                gFntColorBlack, _
                                mCstMarginX_FreeCounterStatusLabelStartMotor + (mCstCodeFreeBlockWidth * hintColIndex) + sngX_Status, _
                                mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeCounterStatus)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： カウンタ >> 単位表示
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ブロック行Index
    '           ： ARG3 - (I ) ブロック列Index
    '           ： ARG4 - (I ) チャンネルの配列Index
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewCounterUnit(ByVal objGraph As System.Drawing.Graphics, _
                                          ByVal hintRowIndex As Integer, _
                                          ByVal hintColIndex As Integer, _
                                          ByVal hintChIndex As Integer)

        Try

            Dim strUnit As String

            '単位種別コード取得
            Dim shtUnit As Short = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtUnit

            '単位文字列取得
            If shtUnit = gCstCodeChManualInputUnit Then

                'ManualInput
                strUnit = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.strUnit

            Else

                'その他選択文字列
                cmbUnit.SelectedValue = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtUnit
                strUnit = cmbUnit.Text

            End If

            '単位表示
            objGraph.DrawString(strUnit, _
                                gFnt10, _
                                gFntColorBlack, _
                                mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + mCstMarginX_FreeCounterUnit, _
                                mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeCounterUnit)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#End Region

#Region "アナログメーター詳細"

    '----------------------------------------------------------------------------
    ' 機能説明  ： アナログメーター（分割数毎の処理）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ブロック行Index
    '           ： ARG3 - (I ) ブロック列Index
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewAnalogMeterScale3(ByVal objGraph As System.Drawing.Graphics, _
                                                ByVal hintRowIndex As Integer, _
                                                ByVal hintColIndex As Integer)

        Try

            'アナログメーター画像
            objGraph.DrawImage(imgPathFreeMeterScale3, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeMeterScale3.Width, _
                               imgPathFreeMeterScale3.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewAnalogMeterScale4(ByVal objGraph As System.Drawing.Graphics, _
                                                ByVal hintRowIndex As Integer, _
                                                ByVal hintColIndex As Integer)

        Try

            'アナログメーター画像
            objGraph.DrawImage(imgPathFreeMeterScale4, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeMeterScale4.Width, _
                               imgPathFreeMeterScale4.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewAnalogMeterScale5(ByVal objGraph As System.Drawing.Graphics, _
                                                ByVal hintRowIndex As Integer, _
                                                ByVal hintColIndex As Integer)

        Try

            'アナログメーター画像
            objGraph.DrawImage(imgPathFreeMeterScale5, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeMeterScale5.Width, _
                               imgPathFreeMeterScale5.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewAnalogMeterScale6(ByVal objGraph As System.Drawing.Graphics, _
                                                ByVal hintRowIndex As Integer, _
                                                ByVal hintColIndex As Integer)

        Try

            'アナログメーター画像
            objGraph.DrawImage(imgPathFreeMeterScale6, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeMeterScale6.Width, _
                               imgPathFreeMeterScale6.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewAnalogMeterScale7(ByVal objGraph As System.Drawing.Graphics, _
                                                ByVal hintRowIndex As Integer, _
                                                ByVal hintColIndex As Integer)

        Try

            'アナログメーター画像
            objGraph.DrawImage(imgPathFreeMeterScale7, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeMeterScale7.Width, _
                               imgPathFreeMeterScale7.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "バーグラフ詳細"

    '----------------------------------------------------------------------------
    ' 機能説明  ： バーグラフ（分割数毎の処理）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ブロック行Index
    '           ： ARG3 - (I ) ブロック列Index
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewBarScale3(ByVal objGraph As System.Drawing.Graphics, _
                                        ByVal hintRowIndex As Integer, _
                                        ByVal hintColIndex As Integer)

        Try

            'バーグラフ画像
            objGraph.DrawImage(imgPathFreeBar3, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeBar3.Width, _
                               imgPathFreeBar3.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewBarScale4(ByVal objGraph As System.Drawing.Graphics, _
                                        ByVal hintRowIndex As Integer, _
                                        ByVal hintColIndex As Integer)

        Try

            'バーグラフ画像
            objGraph.DrawImage(imgPathFreeBar4, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeBar4.Width, _
                               imgPathFreeBar4.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewBarScale5(ByVal objGraph As System.Drawing.Graphics, _
                                        ByVal hintRowIndex As Integer, _
                                        ByVal hintColIndex As Integer)

        Try

            'バーグラフ画像
            objGraph.DrawImage(imgPathFreeBar5, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeBar5.Width, _
                               imgPathFreeBar5.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewBarScale6(ByVal objGraph As System.Drawing.Graphics, _
                                        ByVal hintRowIndex As Integer, _
                                        ByVal hintColIndex As Integer)

        Try

            'バーグラフ画像
            objGraph.DrawImage(imgPathFreeBar6, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeBar6.Width, _
                               imgPathFreeBar6.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewBarScale7(ByVal objGraph As System.Drawing.Graphics, _
                                        ByVal hintRowIndex As Integer, _
                                        ByVal hintColIndex As Integer)

        Try

            'バーグラフ画像
            objGraph.DrawImage(imgPathFreeBar7, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeBar7.Width, _
                               imgPathFreeBar7.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "インジケータ詳細"

    '----------------------------------------------------------------------------
    ' 機能説明  ： インジケータ >> ステータス表示
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ブロック行Index
    '           ： ARG3 - (I ) ブロック列Index
    '           ： ARG4 - (I ) チャンネルNO
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewIndicatorStatus(ByVal objGraph As System.Drawing.Graphics, _
                                              ByVal hintRowIndex As Integer, _
                                              ByVal hintColIndex As Integer, _
                                              ByVal hintChNo As Integer)
        Try

            'チャンネルの配列Index取得
            Dim intChIndex As Integer = gConvChNoToChArrayId(Format(hintChNo, "0000"))

            If intChIndex <> -1 Then

                'チャンネル種別の取得
                Dim shtChType As Short = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtChType

                'データ種別の取得
                Dim shtDataType As Short = gudt.SetChInfo.udtChannel(intChIndex).udtChCommon.shtData

                Select Case shtChType
                    Case gCstCodeChTypeComposite, gCstCodeChTypePulse

                        'コンポジット/パルスは「ステータス表示」なし

                    Case gCstCodeChTypeDigital ', gCstCodeChTypeSystem

                        'デジタル/システム
                        Call FreeGrpPreviewIndicatorStatusDigital(objGraph, hintRowIndex, hintColIndex, intChIndex)

                    Case gCstCodeChTypeAnalog

                        'アナログ
                        Call FreeGrpPreviewIndicatorStatusAnalog(objGraph, hintRowIndex, hintColIndex, intChIndex)

                    Case gCstCodeChTypeMotor

                        'モーター
                        Call FreeGrpPreviewIndicatorStatusMotor(objGraph, hintRowIndex, hintColIndex, intChIndex)

                    Case gCstCodeChTypeValve

                        'バルブ
                        If shtDataType = gCstCodeChDataTypeValveDI_DO Then

                            'デジタル
                            Call FreeGrpPreviewIndicatorStatusDigital(objGraph, hintRowIndex, hintColIndex, intChIndex)

                        ElseIf shtDataType = gCstCodeChDataTypeValveAI_AO1 Or _
                               shtDataType = gCstCodeChDataTypeValveAI_AO2 Or _
                               shtDataType = gCstCodeChDataTypeValveAI_DO1 Or _
                               shtDataType = gCstCodeChDataTypeValveAI_DO2 Then

                            'アナログ
                            Call FreeGrpPreviewIndicatorStatusAnalog(objGraph, hintRowIndex, hintColIndex, intChIndex)

                            '------------------------------------------------------
                            '※バルブの下記データ種別については[ステータス表示]なし
                            '    アナログ4-20mA      ：0x0020
                            '    デジタル            ：0x0021
                            '    デジタル(JACOM-22)  ：0x0040
                            '    デジタル(延長警報盤)：0x0041
                            '------------------------------------------------------

                        End If

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： インジケータ >> ステータス表示（デジタル関係）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ブロック行Index
    '           ： ARG3 - (I ) ブロック列Index
    '           ： ARG4 - (I ) チャンネルの配列Index
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewIndicatorStatusDigital(ByVal objGraph As System.Drawing.Graphics, _
                                                     ByVal hintRowIndex As Integer, _
                                                     ByVal hintColIndex As Integer, _
                                                     ByVal hintChIndex As Integer)

        Try

            'BMPの半分の位置
            Dim sngHalfBmpWidth As Single = mCstCodeFreeBmpWidthIndicator / 2

            'ステータス取得
            cmbStatusDigital.SelectedValue = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus
            Dim strStatus As String = cmbStatusDigital.Text

            'ステータス表示
            Dim sngHalfStringLength As Single = (strStatus.Length * gFntScale6) / 2     '文字列長の半分の位置
            Dim sngMarginX_Status As Single = sngHalfBmpWidth - sngHalfStringLength     '書出し位置
            objGraph.DrawString(strStatus, _
                                gFnt7, _
                                gFntColorBlack, _
                                mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + sngMarginX_Status, _
                                mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeIndicatorStatus)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： インジケータ >> ステータス表示（アナログ関係）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ブロック行Index
    '           ： ARG3 - (I ) ブロック列Index
    '           ： ARG4 - (I ) チャンネルの配列Index
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewIndicatorStatusAnalog(ByVal objGraph As System.Drawing.Graphics, _
                                                    ByVal hintRowIndex As Integer, _
                                                    ByVal hintColIndex As Integer, _
                                                    ByVal hintChIndex As Integer)

        Try

            'BMPの半分の位置
            Dim sngHalfBmpWidth As Single = mCstCodeFreeBmpWidthIndicator / 2

            'ステータス取得
            cmbStatusAnalog.SelectedValue = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus
            Dim strStatus As String = cmbStatusAnalog.Text

            Dim sngHalfStringLength As Single = (strStatus.Length * gFntScale6) / 2     '文字列長の半分の位置
            Dim sngX_Status As Single = sngHalfBmpWidth - sngHalfStringLength           '書出し位置
            objGraph.DrawString(strStatus, _
                                gFnt7, _
                                gFntColorBlack, _
                                mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + sngX_Status, _
                                mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeIndicatorStatus)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： インジケータ >> ステータス表示（モーター）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ブロック行Index
    '           ： ARG3 - (I ) ブロック列Index
    '           ： ARG4 - (I ) チャンネルの配列Index
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewIndicatorStatusMotor(ByVal objGraph As System.Drawing.Graphics, _
                                                   ByVal hintRowIndex As Integer, _
                                                   ByVal hintColIndex As Integer, _
                                                   ByVal hintChIndex As Integer)

        Try

            'BMPの半分の位置
            Dim sngHalfBmpWidth As Single = mCstCodeFreeBmpWidthIndicator / 2

            'ステータス取得
            Dim strStatus As String = mGetStatusMotor(hintChIndex)

            'ステータス表示
            Dim sngHalfStringLength As Single = (strStatus.Length * gFntScale6) / 2     '文字列長の半分の位置
            Dim sngX_Status As Single = sngHalfBmpWidth - sngHalfStringLength           '書出し位置
            objGraph.DrawString(strStatus, _
                                gFnt7, _
                                gFntColorBlack, _
                                mCstMarginX_FreeDefPos + (mCstCodeFreeBlockWidth * hintColIndex) + sngX_Status, _
                                mCstMarginY_FreeDefPos + (mCstCodeFreeBlockHeight * hintRowIndex) + mCstMarginY_FreeIndicatorStatus)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



#Region "画像表示"

    '----------------------------------------------------------------------------
    ' 機能説明  ： インジケータ（タイプ毎の処理）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) ブロック行Index
    '           ： ARG3 - (I ) ブロック列Index
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub FreeGrpPreviewIndicatorData(ByVal objGraph As System.Drawing.Graphics, _
                                            ByVal hintRowIndex As Integer, _
                                            ByVal hintColIndex As Integer)
        Try

            'インジケータ画像
            objGraph.DrawImage(imgPathFreeIndicatorData, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeIndicatorData.Width, _
                               imgPathFreeIndicatorData.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewIndicatorAlarm(ByVal objGraph As System.Drawing.Graphics, _
                                             ByVal hintRowIndex As Integer, _
                                             ByVal hintColIndex As Integer)

        Try

            'インジケータ画像
            objGraph.DrawImage(imgPathFreeIndicatorAlarm, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeIndicatorAlarm.Width, _
                               imgPathFreeIndicatorAlarm.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewIndicatorRepose(ByVal objGraph As System.Drawing.Graphics, _
                                              ByVal hintRowIndex As Integer, _
                                              ByVal hintColIndex As Integer)

        Try

            'インジケータ画像
            objGraph.DrawImage(imgPathFreeIndicatorRepose, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeIndicatorRepose.Width, _
                               imgPathFreeIndicatorRepose.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub FreeGrpPreviewIndicatorSensor(ByVal objGraph As System.Drawing.Graphics, _
                                              ByVal hintRowIndex As Integer, _
                                              ByVal hintColIndex As Integer)

        Try

            'インジケータ画像
            objGraph.DrawImage(imgPathFreeIndicatorSensorFail, _
                               mCstMarginX_FreeDefPos + mCstCodeFreeBlockWidth * hintColIndex, _
                               mCstMarginY_FreeDefPos + mCstCodeFreeBlockHeight * hintRowIndex, _
                               imgPathFreeIndicatorSensorFail.Width, _
                               imgPathFreeIndicatorSensorFail.Height)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region



#End Region



#End Region



    '----------------------------------------------------------------------------
    ' 機能説明  ： イメージファイルのパス設定
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mSetImagePath()

        Try

            'グラフ枠
            'Ver2.0.7.M (新デザイン)アナログメーター新デザイン対応
            'Ver2.0.8.7 綴じ代調整　画像変更
            If g_bytNEWDES = 1 Then
                imgPathFrameExhBarAnalog = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphExhBar_NEW)
            Else
                imgPathFrameExhBarAnalog = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphExhBar)
            End If

            imgPathFrameFree = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeFrame)

            '3種グラフ：BMP 1/8サイズ
            'Ver2.0.7.M (新デザイン)アナログメーター新デザイン対応
            If g_bytNEWDES = 1 Then
                imgPathAnalog8_3 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog8_3_NEW)
                imgPathAnalog8_4 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog8_4_NEW)
                imgPathAnalog8_5 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog8_5_NEW)
                imgPathAnalog8_6 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog8_6_NEW)
                imgPathAnalog8_7 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog8_7_NEW)
            Else
                imgPathAnalog8_3 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog8_3)
                imgPathAnalog8_4 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog8_4)
                imgPathAnalog8_5 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog8_5)
                imgPathAnalog8_6 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog8_6)
                imgPathAnalog8_7 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog8_7)
            End If

            '3種グラフ：BMP 1/2サイズ
            imgPathAnalog2_3 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog2_3)
            imgPathAnalog2_4 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog2_4)
            imgPathAnalog2_5 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog2_5)
            imgPathAnalog2_6 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog2_6)
            imgPathAnalog2_7 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphAnalog2_7)

            'フリーグラフ：Counter
            imgPathFreeCounter = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeCounter)

            'フリーグラフ：Indicator
            imgPathFreeIndicatorData = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeIndicatorData)
            imgPathFreeIndicatorAlarm = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeIndicatorAlarm)
            imgPathFreeIndicatorRepose = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeIndicatorRepose)
            imgPathFreeIndicatorSensorFail = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeIndicatorSensorFail)

            'フリーグラフ：AnalogMeter
            imgPathFreeMeterScale3 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeAnalogMeterScale3)
            imgPathFreeMeterScale4 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeAnalogMeterScale4)
            imgPathFreeMeterScale5 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeAnalogMeterScale5)
            imgPathFreeMeterScale6 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeAnalogMeterScale6)
            imgPathFreeMeterScale7 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeAnalogMeterScale7)

            'フリーグラフ：Bar
            imgPathFreeBar3 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeBar3)
            imgPathFreeBar4 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeBar4)
            imgPathFreeBar5 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeBar5)
            imgPathFreeBar6 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeBar6)
            imgPathFreeBar7 = System.Drawing.Image.FromFile(gGetAppPath() & gCstPathPrtGraphFreeBar7)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 上下限値、単位の取得
    ' 引数      ： ARG1 - (I ) チャンネル番号
    '           ： ARG2 - ( O) 上限値
    '           ： ARG3 - ( O) 下限値
    '           ： ARG4 - ( O) 単位
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mGetLimitAndUnit(ByVal hstrChNo As String, _
                                 ByRef hstrMax As String, _
                                 ByRef hstrMin As String, _
                                 ByRef hstrUnit As String, _
                                 ByRef MeterType As Integer)

        Try

            Dim dblRangeMax As Double
            Dim dblRangeMin As Double
            Dim intDecimalPosition As Integer   '小数点桁位置
            Dim strDecimalFormat As String      'フォーマットタイプ

            Dim Metermod As Integer
            Dim RengeScale As Double

            Dim strKETA As String = ""  'Ver2.0.6.5


            '上下限値の目盛と単位を取得する
            If (0 < hstrChNo) And (hstrChNo <= 65535) Then

                'チャンネルの配列Indexを取得する
                Dim intChIdx As Integer = gConvChNoToChArrayId(hstrChNo)

                'チャンネルIndexが正常範囲の時に処理を実施
                If (LBound(gudt.SetChInfo.udtChannel) <= intChIdx) And (intChIdx <= UBound(gudt.SetChInfo.udtChannel)) Then

                    '単位取得
                    Call mGetUnit(hstrUnit, intChIdx)

                    '小数点桁取得
                    intDecimalPosition = gudt.SetChInfo.udtChannel(intChIdx).AnalogDecimalPosition
                    strDecimalFormat = "0.".PadRight(intDecimalPosition + 2, "0"c)

                    '上限値
                    dblRangeMax = gudt.SetChInfo.udtChannel(intChIdx).AnalogRangeHigh / (10 ^ intDecimalPosition)

                    '下限値
                    dblRangeMin = gudt.SetChInfo.udtChannel(intChIdx).AnalogRangeLow / (10 ^ intDecimalPosition)

                    'スケール範囲をチェックする T.Ueki 2015/4/27
                    RengeScale = dblRangeMax - dblRangeMin

                    If mudtSetOpsGraph.udtGraphAnalogMeterSettingRec.bytMarkNumericalValue = 2 Then     '短縮 2013.07.24 K.Fujimoto

                        strKETA = "0.0" 'Ver2.0.6.5

                        '2015/4/27 T.Ueki メータ分割種類の小数点
                        Metermod = (RengeScale * 100) Mod (MeterType * 100)

                        'Ver2.0.6.5
                        '区切りが0.05の場合、小数点一けたではなく二けた
                        If (RengeScale * 100) / (MeterType * 100) < 0.1 Then
                            strKETA = "0.00"
                        End If

                        If Metermod <> 0 Then
                            hstrMax = dblRangeMax.ToString(strKETA)
                        Else
                            hstrMax = dblRangeMax.ToString
                        End If

                    Else
                        hstrMax = dblRangeMax.ToString(strDecimalFormat)
                    End If

                    
                    If mudtSetOpsGraph.udtGraphAnalogMeterSettingRec.bytMarkNumericalValue = 2 Then     '短縮 2013.07.24 K.Fujimoto

                        '2015/4/27 T.Ueki 表示方法改正
                        If dblRangeMin.ToString <> "0" Then

                            If Metermod <> 0 Then
                                hstrMin = dblRangeMin.ToString(strKETA)
                            Else
                                hstrMin = dblRangeMin.ToString
                            End If
                        Else

                            hstrMin = dblRangeMin.ToString

                        End If
                    Else
                        hstrMin = dblRangeMin.ToString(strDecimalFormat)
                    End If

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： モーターのステータス取得
    ' 引数      ： ARG1 - (I ) チャンネルの配列Index
    ' 戻値      ： ステータス情報
    '----------------------------------------------------------------------------
    Private Function mGetStatusMotor(ByVal hintChIndex As Integer) As String

        Dim strRtn As String = ""

        Try

            Dim mMotorStatusNormal() As String = Nothing
            Dim mMotorStatusAbnormal() As String = Nothing
            Dim strwk() As String
            Dim strStatus As String

            'データ種別コードの取得
            Dim intChData As Integer = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData

            'Ver2.0.0.2 モーター種別増加 R Device ADD
            If intChData = gCstCodeChDataTypeMotorDevice Or intChData = gCstCodeChDataTypeMotorRDevice Then

                'DeviceOperation
                ' 2013.07.22 MO表示変更  K.Fujimoto
                'strStatus = "RUN/STOP"
                strStatus = "RUN"

            Else

                'モーターチャンネルのステータス種別コードを取得
                Call GetStatusMotor2(mMotorStatusNormal, mMotorStatusAbnormal, "StatusMotor")

                ''ステータス種別コード（Status I）の取得
                Dim intStatus As Integer = gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtStatus

                'ステータス取得
                If intStatus = gCstCodeChManualInputStatus Then

                    'Manual Input
                    strStatus = gGetString(gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.strStatus)

                Else

                    ''データ種別によって、表示するステータス種別が異なる
                    ''　[データ種別]運転・手動停止 → [ステータス種別]備考欄'/'の左側表示
                    ''　[データ種別]運転・異常　　 → [ステータス種別]備考欄'/'の右側表示
                    'Ver2.0.0.2 モーター種別増加
                    If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData <= gCstCodeChDataTypeMotorManRunK _
                        Or _
                       (gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData >= gCstCodeChDataTypeMotorRManRun _
                        And _
                        gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData <= gCstCodeChDataTypeMotorRManRunK _
                        ) _
                        Then

                        '------------------
                        ''運転・手動停止
                        '------------------
                        ''引き算の結果、0より小さければ空文字返す
                        If intStatus - gCstCodeChStatusTypeMotorRun < 0 Then
                            strStatus = ""
                        Else
                            strwk = mMotorStatusNormal(intStatus - gCstCodeChStatusTypeMotorRun).Split("_")
                            strStatus = strwk(0) '1番目のステータスを取得
                        End If

                    Else

                        '------------------
                        ''運転・異常
                        '------------------
                        ''引き算の結果、0より小さければ空文字返す
                        If intStatus - gCstCodeChStatusTypeMotorRun < 0 Then
                            strStatus = ""
                        Else
                            strwk = mMotorStatusAbnormal(intStatus - gCstCodeChStatusTypeMotorRun).Split("_")
                            strStatus = strwk(0) '1番目のステータスを取得
                        End If

                    End If

                End If

            End If

            '戻値設定
            strRtn = strStatus.Trim

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 単位取得
    ' 引数      ： ARG1 - ( O) 単位
    '           ： ARG2 - (I ) チャンネルの配列番号
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mGetUnit(ByRef hstrUnit As String, _
                         ByVal hintArrayIndex As Integer)

        Try

            Dim shtUnit As Short = gudt.SetChInfo.udtChannel(hintArrayIndex).udtChCommon.shtUnit

            If shtUnit = gCstCodeChManualInputUnit Then
                'ManualInput
                hstrUnit = gGetString(gudt.SetChInfo.udtChannel(hintArrayIndex).udtChCommon.strUnit)
            Else
                'その他選択文字列
                cmbUnit.SelectedValue = gudt.SetChInfo.udtChannel(hintArrayIndex).udtChCommon.shtUnit
                hstrUnit = cmbUnit.Text

                'T.Ueki
                If hstrUnit = "C" Then
                    hstrUnit = "ﾟC"
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Decimal情報取得
    ' 引数      ： ARG1 - (I  ) CH配列Index
    '           ： ARG2 - (　O) 少数点以下桁数
    '           ： ARG3 - (　O) フォーマット文字列
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mGetDecimalInfo(ByVal hintArrayIdx As Integer, _
                                ByRef hintDecimalPosition As Integer, _
                                ByRef hstrDecimalFormat As String)

        Try

            ''返り値初期化
            hintDecimalPosition = 0
            hstrDecimalFormat = ""

            If gudt.SetChInfo.udtChannel(hintArrayIdx).udtChCommon.shtChType = gCstCodeChTypeAnalog Or _
               gudt.SetChInfo.udtChannel(hintArrayIdx).udtChCommon.shtChType = gCstCodeChTypePulse Then

                'アナログ／パルス
                hintDecimalPosition = gudt.SetChInfo.udtChannel(hintArrayIdx).AnalogDecimalPosition
                hstrDecimalFormat = "0.".PadRight(hintDecimalPosition + 2, "0"c)

            ElseIf gudt.SetChInfo.udtChannel(hintArrayIdx).udtChCommon.shtChType = gCstCodeChTypeValve And _
                   gudt.SetChInfo.udtChannel(hintArrayIdx).udtChCommon.shtChType = gCstCodeChDataTypeValveAI_DO1 Then

                'バルブ：AIDO1
                hintDecimalPosition = gudt.SetChInfo.udtChannel(hintArrayIdx).AnalogDecimalPosition
                hstrDecimalFormat = "0.".PadRight(hintDecimalPosition + 2, "0"c)

            ElseIf gudt.SetChInfo.udtChannel(hintArrayIdx).udtChCommon.shtChType = gCstCodeChTypeValve And _
                   gudt.SetChInfo.udtChannel(hintArrayIdx).udtChCommon.shtChType = gCstCodeChDataTypeValveAI_DO2 Then

                'バルブ：AIDO2
                hintDecimalPosition = gudt.SetChInfo.udtChannel(hintArrayIdx).AnalogDecimalPosition
                hstrDecimalFormat = "0.".PadRight(hintDecimalPosition + 2, "0"c)

            ElseIf gudt.SetChInfo.udtChannel(hintArrayIdx).udtChCommon.shtChType = gCstCodeChTypeValve And _
                   gudt.SetChInfo.udtChannel(hintArrayIdx).udtChCommon.shtChType = gCstCodeChDataTypeValveAI_AO1 Then

                'バルブ：AIAO1
                hintDecimalPosition = gudt.SetChInfo.udtChannel(hintArrayIdx).AnalogDecimalPosition
                hstrDecimalFormat = "0.".PadRight(hintDecimalPosition + 2, "0"c)

            ElseIf gudt.SetChInfo.udtChannel(hintArrayIdx).udtChCommon.shtChType = gCstCodeChTypeValve And _
                   gudt.SetChInfo.udtChannel(hintArrayIdx).udtChCommon.shtChType = gCstCodeChDataTypeValveAI_AO2 Then

                'バルブ：AIAO2
                hintDecimalPosition = gudt.SetChInfo.udtChannel(hintArrayIdx).AnalogDecimalPosition
                hstrDecimalFormat = "0.".PadRight(hintDecimalPosition + 2, "0"c)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： チャンネルがデジタル系かアナログ系かを判断
    ' 引数      ： ARG1 - (I  ) CH配列Index
    ' 戻値      ： TRUE：デジタル系, FALSE：アナログ系
    '----------------------------------------------------------------------------
    Private Function mCheckChTypeDigital(ByVal hintChIndex As Integer) As Boolean

        Dim blnRtn As Boolean = False

        Try

            If hintChIndex <> -1 Then

                If gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtChType = gCstCodeChTypeDigital Or _
                   gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtChType = gCstCodeChTypeMotor Or _
                   gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtChType = gCstCodeChTypeComposite Then

                    'デジタルCH, モーターCH, コンポジットCH の場合
                    blnRtn = True

                ElseIf gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtChType = gCstCodeChTypeValve And _
                       gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Then

                    'バルブ：DIDO の場合
                    blnRtn = True

                ElseIf gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtChType = gCstCodeChTypeValve And _
                       gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveDO Then

                    'バルブ：Digital の場合
                    blnRtn = True

                ElseIf gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtChType = gCstCodeChTypeValve And _
                       gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveJacom Then

                    'バルブ：Jacom の場合
                    blnRtn = True

                ElseIf gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtChType = gCstCodeChTypeValve And _
                       gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveJacom55 Then

                    'バルブ：Jacom55 の場合
                    blnRtn = True

                ElseIf gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtChType = gCstCodeChTypeValve And _
                       gudt.SetChInfo.udtChannel(hintChIndex).udtChCommon.shtData = gCstCodeChDataTypeValveExt Then

                    'バルブ：ExtPanel の場合
                    blnRtn = True

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return blnRtn

    End Function

#End Region

End Class