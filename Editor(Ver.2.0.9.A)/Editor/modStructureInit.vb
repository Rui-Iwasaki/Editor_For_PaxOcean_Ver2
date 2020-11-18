Module modStructureInit

#Region "システム設定"

    Public Sub gInitSetSystem(ByRef udtSetSystem As gTypSetSystem)

        Try

            '========================
            ''システム設定
            '========================
            With udtSetSystem.udtSysSystem

                .shtClock = 0               ''システムクロック：外部
                .shtDate = 1                ''日付フォーマット：dd/mm/yy
                .shtLanguage = 0            ''言語：英語
                .shtManual = 0              ''取扱説明書(言語):英語     2015.02.05
                .shtgl_spec = 0             ''GL船級仕様                2015/5/27 T.Ueki
                .strShipName = ""           ''船名：空白

                ''コンバイン設定
                .shtCombineUse = 0          ''コンバイン有無：無し
                .shtCombineSeparate = 0     ''fs/bsセパレート：無し

                '▼▼▼ 20110330 .shtStatus削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                ' ''システムステータス設定
                '.shtStatus = gBitSet(.shtStatus, 0, False) ''システムステータス代表画面作成有無：無し
                '.shtStatus = gBitSet(.shtStatus, 1, False) ''FCU A/B反転：無し
                '.shtStatus = gBitSet(.shtStatus, 2, False) ''OPSステータス画面自動作成有無：無し
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ''GWS1
                .shtGWS1 = gBitSet(.shtGWS1, 0, False)  ''GWS有無：有り
                .shtGWS1 = gBitSet(.shtGWS1, 1, False)  ''EthernetLine：× A only
                .shtGWS1 = gBitSet(.shtGWS1, 2, False)  ''EthernetLine：× A and B

                ''GWS2
                .shtGWS2 = gBitSet(.shtGWS2, 0, False)  ''GWS有無：無し
                .shtGWS2 = gBitSet(.shtGWS2, 1, False)  ''EthernetLine：× A only
                .shtGWS2 = gBitSet(.shtGWS2, 2, False)  ''EthernetLine：× A and B

                ''配列初期化
                .InitArray()

            End With

            '========================
            ''FCU設定
            '========================
            With udtSetSystem.udtSysFcu

                '▼▼▼ 20110330 .shtLogBackup削除対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                '.shtLogBackup = 0       ''イベントログバックアップ：無し
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                '.shtCanbus = 0          ''CANBUS対応：無し
                '.shtModbus = 0          ''MODBUS対応：無し
                .shtFcuCnt = 1          ''FCU台数：１   2011.12.13 K.Tanigawa
                .shtFcuNo = 1            ''FCU番号：１
                .shtCrrectTime = 1      ''収集周期：1x100ms     初期値1に変更 ver.1.4.0 2011.09.29
                .shtFcuExtendBord = 0   ''FCU拡張ボード：無し
                .shtShareChUse = 0      ''共有CH使用：無し
                .shtFCUOption = 0       '' 通信用拡張FCU  Ver1.9.3 2016.01.21 追加
                .shtFCU2Flg = 0         'Ver2.0.7.V
                ''配列初期化
                .InitArray()

            End With

            '========================
            ''OPS設定
            '========================
            With udtSetSystem.udtSysOps

                ''共通設定
                .shtControl = 0         ''遠隔操作：無し
                .shtProhibition = 0     ''Extグループ、グループリポーズ変更禁止：変更許可
                .shtChannelEdit = 1     ''CHデータ変更禁止：変更許可
                .shtAlarm = 1           ''アラーム表示方法：Active Only
                .shtDuty = 0            ''Duty使用可/不可：不可
                .shtContOnlyFlag = 0    ''コントロール　1台インターロック：無し   2015.01.19
                .shtAlarm_Order = 0     ''Auto Alarm表示順序　2015/5/27 T.Ueki
                .shtTagMode = 0         ''TagNo表示ﾓｰﾄﾞ　2015.10.22 Ver1.7.5
                .shtLRMode = 0          ''ﾛｲﾄﾞ表示ﾓｰﾄﾞ　2015.11.12 Ver1.7.8

                ''配列数初期化
                .InitArray()

                ''詳細設定
                For i As Integer = LBound(.udtOpsDetail) To UBound(.udtOpsDetail)

                    With .udtOpsDetail(i)

                        .shtExist = 0
                        .shtAlarmDisp = 1
                        .shtEnable = 0
                        .shtControl = gBitSet(.shtControl, 0, False)
                        .shtControl = gBitSet(.shtControl, 1, False)
                        .shtControlFlag = gBitSet(.shtControlFlag, 0, False)
                        .shtControlFlag = gBitSet(.shtControlFlag, 1, False)
                        .shtControlFlag = gBitSet(.shtControlFlag, 2, False)
                        .shtControlFlag = gBitSet(.shtControlFlag, 3, False)
                        'Ver2.0.7.R 5,6bit解放
                        .shtControlFlag = gBitSet(.shtControlFlag, 5, False)
                        .shtControlFlag = gBitSet(.shtControlFlag, 6, False)
                        '-
                        .shtControlProhFlag = 0
                        .shtOperaionPanel = 0
                        .shtAdjustLight = 0
                        .shtHatteland = 0
                        .shtPrintPart = gBitSet(.shtPrintPart, 0, False)
                        .shtPrintPart = gBitSet(.shtPrintPart, 1, False)
                        .shtOpsType = gBitSet(.shtOpsType, 0, False)
                        .shtOpsType = gBitSet(.shtOpsType, 1, False)
                        .shtBootMode = gBitSet(.shtBootMode, 0, False)
                        .shtBootMode = gBitSet(.shtBootMode, 1, False)
                        .shtRepSum = 0
                        .shtEtherA = 0
                        .shtResolution = 1

                        ''配列数初期化
                        .InitArray()

                    End With

                Next

            End With

            '========================
            ''GWS設定
            '========================
            With udtSetSystem.udtSysGws

                ''配列数初期化
                .InitArray()


                ''詳細設定
                For i As Integer = LBound(.udtGwsDetail) To UBound(.udtGwsDetail)

                    With .udtGwsDetail(i)

                        .shtGwsType = 0
                        .bytIP1 = 0
                        .bytIP2 = 0
                        .bytIP3 = 0
                        .bytIP4 = 0

                        ''配列数初期化
                        .InitArray()

                        For ir As Integer = LBound(.udtGwsFileInfo) To UBound(.udtGwsFileInfo)

                            With .udtGwsFileInfo(ir)
                                .bytType = 0
                                .bytSetFlg = 0
                                .bytBkupCnt = 0
                                .bytspare = 0
                                .shtInterval = 0
                            End With
                        Next

                    End With
                Next

            End With

            '========================
            ''Printer設定
            '========================
            With udtSetSystem.udtSysPrinter

                ''共通設定

                .shtAutoCnt = 100   '' 2013.07.22 自動印字最大数追加  K.Fujimoto
                .shtPrintType = 1   ''英数・日本語設定：SingleSize
                .shtEventPrint = 0  ''イベントプリント：無し   Ver2.0.2.8 有→無
                .shtNoonUnder = 0   ''ヌーンログ下線：無し
                .shtDemandPage = 0  ''デマンドログ改ページ：無し
                .shtMachineryCargoPrint = 0  ''Machinery/Cargo印字：無し
                .shtLogDrawNo = 0   '' ﾛｸﾞ印字下絵番号　Ver1.9.3 2016.01.22 
                .shtAutoCntCargo = 100 '' 2019.02.05 自動印字最大数カーゴ  倉重

                ''配列数初期化
                .InitArray()

                ''ログプリンタ１
                With .udtPrinterDetail(0)
                    .bytPrinter = 0                                 ''プリンタ有無：無し
                    .shtPrintUse = gBitSet(.shtPrintUse, 0, False)  ''印字有無（通常）：無し　ver.1.4.0 2011.09.28 有→無
                    .shtPrintUse = gBitSet(.shtPrintUse, 1, True)   ''印字有無（バックアップ）：有り
                    .shtPrintUse = gBitSet(.shtPrintUse, 2, False)  ''A3用紙指定 ： 無し　2011.12.13 K.Tanigwa
                    .shtPart = gBitSet(.shtPart, 0, False)          ''印字パート（Machinery）：無し
                    .shtPart = gBitSet(.shtPart, 1, False)          ''印字パート（Cargo）：無し
                    .strDriver = ""                                 ''ドライバ：空白
                    .strDevice = ""                                 ''デバイス：空白
                    .bytIP1 = 0                                     ''IPアドレス：無し
                    .bytIP2 = 0                                     ''IPアドレス：無し
                    .bytIP3 = 0                                     ''IPアドレス：無し
                    .bytIP4 = 0                                     ''IPアドレス：無し
                    '.InitArray()                                    ''配列数初期化
                End With

                ''ログプリンタ２
                With .udtPrinterDetail(1)
                    .bytPrinter = 0                                 ''プリンタ有無：無し
                    .shtPrintUse = gBitSet(.shtPrintUse, 0, False)  ''印字有無（通常）：無し　ver.1.4.0 2011.09.28 有→無
                    .shtPrintUse = gBitSet(.shtPrintUse, 1, False)   ''印字有無（バックアップ）：無し   Ver2.0.2.8 有→無
                    .shtPrintUse = gBitSet(.shtPrintUse, 2, False)  ''A3用紙指定 ： 無し　2011.12.13 K.Tanigwa
                    .shtPart = gBitSet(.shtPart, 0, False)          ''印字パート（Machinery）：無し
                    .shtPart = gBitSet(.shtPart, 1, False)          ''印字パート（Cargo）：無し
                    .strDriver = ""                                 ''ドライバ：空白
                    .strDevice = ""                                 ''デバイス：空白
                    .bytIP1 = 0                                     ''IPアドレス：無し
                    .bytIP2 = 0                                     ''IPアドレス：無し
                    .bytIP3 = 0                                     ''IPアドレス：無し
                    .bytIP4 = 0                                     ''IPアドレス：無し
                    '.InitArray()                                    ''配列数初期化
                End With

                ''アラームプリンタ１
                With .udtPrinterDetail(2)
                    .bytPrinter = 0                                 ''プリンタ有無：無し
                    .shtPrintUse = gBitSet(.shtPrintUse, 0, False)  ''印字有無（通常）：無し　ver.1.4.0 2011.09.28 有→無
                    .shtPrintUse = gBitSet(.shtPrintUse, 1, True)   ''印字有無（バックアップ）：有り
                    .shtPrintUse = gBitSet(.shtPrintUse, 2, False)  ''A3用紙指定 ： 無し　2011.12.13 K.Tanigwa
                    .shtPart = gBitSet(.shtPart, 0, False)          ''印字パート（Machinery）：無し
                    .shtPart = gBitSet(.shtPart, 1, False)          ''印字パート（Cargo）：無し
                    .strDriver = ""                                 ''ドライバ：空白
                    .strDevice = ""                                 ''デバイス：空白
                    .bytIP1 = 0                                     ''IPアドレス：無し
                    .bytIP2 = 0                                     ''IPアドレス：無し
                    .bytIP3 = 0                                     ''IPアドレス：無し
                    .bytIP4 = 0                                     ''IPアドレス：無し
                    '.InitArray()                                    ''配列数初期化
                End With

                ''アラームプリンタ２
                With .udtPrinterDetail(3)
                    .bytPrinter = 0                                 ''プリンタ有無：無し
                    .shtPrintUse = gBitSet(.shtPrintUse, 0, False)  ''印字有無（通常）：無し　ver.1.4.0 2011.09.28 有→無
                    .shtPrintUse = gBitSet(.shtPrintUse, 1, False)   ''印字有無（バックアップ）：無し Ver2.0.2.8 有→無
                    .shtPrintUse = gBitSet(.shtPrintUse, 2, False)  ''A3用紙指定 ： 無し　2011.12.13 K.Tanigwa
                    .shtPart = gBitSet(.shtPart, 0, False)          ''印字パート（Machinery）：無し
                    .shtPart = gBitSet(.shtPart, 1, False)          ''印字パート（Cargo）：無し
                    .strDriver = ""                                 ''ドライバ：空白
                    .strDevice = ""                                 ''デバイス：空白
                    .bytIP1 = 0                                     ''IPアドレス：無し
                    .bytIP2 = 0                                     ''IPアドレス：無し
                    .bytIP3 = 0                                     ''IPアドレス：無し
                    .bytIP4 = 0                                     ''IPアドレス：無し
                    '.InitArray()                                    ''配列数初期化
                End With

                ''HCプリンタ
                With .udtPrinterDetail(4)
                    .bytPrinter = 0                                 ''プリンタ有無：有り
                    .shtPrintUse = gBitSet(.shtPrintUse, 0, False)  ''印字有無（通常）：無し　ver.1.4.0 2011.09.28 有→無
                    .shtPrintUse = gBitSet(.shtPrintUse, 1, False)  ''印字有無（バックアップ）：無し　ver.1.4.0 2011.09.28 有→無
                    .shtPrintUse = gBitSet(.shtPrintUse, 2, False)  ''A3用紙指定 ： 無し　2011.12.13 K.Tanigwa
                    .shtPart = gBitSet(.shtPart, 0, False)           ''印字パート（Machinery）：無し Ver2.0.2.8 有→無
                    .shtPart = gBitSet(.shtPart, 1, False)           ''印字パート（Cargo）：無し Ver2.0.2.8 有→無
                    .strDriver = ""                                 ''ドライバ：空白
                    .strDevice = ""                                 ''デバイス：空白
                    .bytIP1 = 0                                     ''IPアドレス：無し
                    .bytIP2 = 0                                     ''IPアドレス：無し
                    .bytIP3 = 0                                     ''IPアドレス：無し
                    .bytIP4 = 0                                     ''IPアドレス：無し
                    '.InitArray()                                    ''配列数初期化
                End With

                ''予備
                With .udtPrinterDetail(5)
                    .bytPrinter = 0                                 ''プリンタ有無：無し
                    .shtPrintUse = gBitSet(.shtPrintUse, 0, False)  ''印字有無（通常）：無し
                    .shtPrintUse = gBitSet(.shtPrintUse, 1, False)  ''印字有無（バックアップ）：無し
                    .shtPrintUse = gBitSet(.shtPrintUse, 2, False)  ''A3用紙指定 ： 無し　2011.12.13 K.Tanigwa
                    .shtPart = gBitSet(.shtPart, 0, False)          ''印字パート（Machinery）：無し
                    .shtPart = gBitSet(.shtPart, 1, False)          ''印字パート（Cargo）：無し
                    .strDriver = ""                                 ''ドライバ：空白
                    .strDevice = ""                                 ''デバイス：空白
                    .bytIP1 = 0                                     ''IPアドレス：無し
                    .bytIP2 = 0                                     ''IPアドレス：無し
                    .bytIP3 = 0                                     ''IPアドレス：無し
                    .bytIP4 = 0                                     ''IPアドレス：無し
                    '.InitArray()                                    ''配列数初期化
                End With

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "FU設定"

    Public Sub gInitSetFuChannelDisp(ByRef udtSetFu As gTypSetFu)

        Try

            ''配列数初期化
            udtSetFu.InitArray()

            For i As Integer = LBound(udtSetFu.udtFu) To UBound(udtSetFu.udtFu)

                With udtSetFu.udtFu(i)

                    .shtUse = 0                     ''FU 使用/未使用フラグ：未使用
                    .shtCanBus = 0                  ''CanBus：無し

                    ''配列数初期化
                    .InitArray()

                    For j As Integer = LBound(.udtSlotInfo) To UBound(.udtSlotInfo)

                        .udtSlotInfo(j).shtType = 0 ''スロット種別：空き
                        .udtSlotInfo(j).shtTerinf = 0 ''端子台設定：区別なし　ver.1.4.0 2011.07.29
                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "チャンネル情報データ"

    Public Sub gInitSetChDisp(ByRef udtSetChDisp As gTypSetChDisp)

        Try

            ''配列数初期化
            udtSetChDisp.InitArray()

            For i As Integer = LBound(udtSetChDisp.udtChDisp) To UBound(udtSetChDisp.udtChDisp)

                With udtSetChDisp.udtChDisp(i)

                    .strFuName = ""         ''FCU/FU名称：空白
                    .strFuType = ""         ''FCU/FU種類：空白
                    .strNamePlate = ""      ''FCU/FU盤名：空白
                    .strRemarks = ""        ''コメント：空白

                    ''配列数初期化
                    .InitArray()

                    For j As Integer = LBound(.udtSlotInfo) To UBound(.udtSlotInfo)

                        '.udtSlotInfo(j).strCableMark = ""       ''CableMark：空白
                        '.udtSlotInfo(j).strCableClass = ""      ''CableClass：空白
                        '.udtSlotInfo(j).strDestination = ""     ''Destination：空白

                        ''配列数初期化
                        .udtSlotInfo(j).InitArray()

                        'For ii As Integer = LBound(.udtSlotInfo(j).udtTerminalInfo) To UBound(.udtSlotInfo(j).udtTerminalInfo)

                        '    .udtSlotInfo(j).udtTerminalInfo(ii).strCableMark = ""       ''CableMark
                        '    .udtSlotInfo(j).udtTerminalInfo(ii).strCableClass = ""      ''CableClass
                        '    .udtSlotInfo(j).udtTerminalInfo(ii).strDestination = ""     ''Destination
                        '    .udtSlotInfo(j).udtTerminalInfo(ii).shtPinCnt = 0           ''計測点設定数
                        '    .udtSlotInfo(j).udtTerminalInfo(ii).shtSpare = 0            ''予備

                        'Next ii

                        For ii As Integer = LBound(.udtSlotInfo(j).udtPinInfo) To UBound(.udtSlotInfo(j).udtPinInfo)

                            .udtSlotInfo(j).udtPinInfo(ii).strCoreNoIn = ""         ''CoreNoIn      ：空白
                            .udtSlotInfo(j).udtPinInfo(ii).strCoreNoCom = ""        ''CoreNoCom     ：空白
                            .udtSlotInfo(j).udtPinInfo(ii).strWireMark = ""         ''WireMark      ：空白
                            .udtSlotInfo(j).udtPinInfo(ii).strWireMarkClass = ""    ''WireMarkClass ：空白
                            .udtSlotInfo(j).udtPinInfo(ii).strDest = ""             ''Dest          ：空白
                            .udtSlotInfo(j).udtPinInfo(ii).shtTerminalNo = 0        ''端子台番号    ：設定なし
                            .udtSlotInfo(j).udtPinInfo(ii).shtChid = 0              ''CHID          ：設定なし

                        Next ii

                    Next j

                End With

            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "チャンネル情報"

    Public Sub gInitSetChannelDisp(ByRef udtSetChInfo As gTypSetChInfo)

        Try

            ''配列数初期化
            udtSetChInfo.InitArray()

            For i As Integer = LBound(udtSetChInfo.udtChannel) To UBound(udtSetChInfo.udtChannel)

                '-----------------------------------------------------------------------
                Call gInitSetChannelDispOne(udtSetChInfo.udtChannel(i))
                '-----------------------------------------------------------------------
                ''上を無効にして下を有効にすると、3000CHが自動で作成される
                '-----------------------------------------------------------------------
                'Call gInitSetChannelDispOneDebug3000ch(udtSetChInfo.udtChannel(i), i)
                '-----------------------------------------------------------------------

            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Public Sub gInitSetChannelDispOne(ByRef udtChannel As gTypSetChRec)

        Try

            '========================
            'CH共通項目
            '========================
            With udtChannel.udtChCommon

                .shtChid = 0            ''CH ID
                .shtGroupNo = 0         ''グループNo
                .shtDispPos = 0         ''表示位置
                .shtSysno = 0           ''System No(Machinery : 0 Cargo : 1 Common : 2)
                .shtChno = 0            ''CH No
                .strChitem = ""         ''CHアイテム名称：空白
                .strRemark = ""         ''備考：空白

                .shtExtGroup = &HFFFF   ''延長警報グループ：無し
                .shtDelay = &HFFFF      ''ディレイタイマ値（アラーム継続時間）:ゼロ
                .shtGRepose1 = &HFFFF   ''グループリポーズ１：無し
                .shtGRepose2 = &HFFFF   ''グループリポーズ２：無し
                .shtM_Repose = 0        ''マニュアルリポーズ：不可

                .shtChType = 0          ''CH Type：無し
                .shtData = 0            ''データ種別コード
                .shtUnit = 0            ''単位種別コード:

                .shtFlag1 = 0           ''動作設定１
                .shtFlag2 = 0           ''動作設定２
                .shtStatus = 0          ''ステータス種別コード

                .shtFuno = &HFFFF       ''FU　番号
                .shtPortno = &HFFFF     ''FU　ポート番号
                .shtPin = &HFFFF        ''FU　計測点番号
                'Ver2.0.2.6 触らないように変更
                '.shtPinNo = 0           ''計測点個数
                .shtEccFunc = 0         ''延長警報盤ECC入出力機能種別コード

                .shtOutPort = 0         ''SIOポート使用有無：無し
                .shtGwsPort = 0         ''GWSポート使用有無：無し
                .strUnit = ""           ''単位種別名称：空白
                .strStatus = ""         ''ステータス名称：空白

                .shtShareType = 0       ''共通CH Local/Remote設定：設定なし
                .shtShareChid = 0       ''リモートCH No ：設定なし
                'Ver2.0.2.6 触らないように変更
                '.shtM_ReposeSet = 0     ''マニュアルリポーズ：設定不可
                .shtSignal = 0          ''入力信号：区別なし　ver.1.4.0 2011.07.29

            End With

            '========================
            'CH個別項目
            '========================
            udtChannel.InitArray()

            For j As Integer = LBound(udtChannel.udtChTypeData) To UBound(udtChannel.udtChTypeData)

                udtChannel.udtChTypeData(j) = 0

            Next j

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Public Sub gInitSetChannelDispOneDebug3000ch(ByRef udtChannel As gTypSetChRec, ByVal i As Integer)

        Try

            Dim intGroupNo As Integer
            Static intDispPos As Integer

            intGroupNo = (i \ 100) + 1
            intDispPos = intDispPos + 1

            If intDispPos = 101 Then
                intDispPos = 1
            End If

            '========================
            'CH共通項目
            '========================
            With udtChannel.udtChCommon

                .shtChid = 0            ''CH ID
                .shtGroupNo = intGroupNo         ''グループNo
                .shtDispPos = intDispPos         ''表示位置
                .shtSysno = 0           ''System No(Machinery : 0 Cargo : 1 Common : 2)
                .shtChno = i + 1 + 100            ''CH No
                .strChitem = ""         ''CHアイテム名称：空白
                .strRemark = ""         ''備考：空白

                .shtExtGroup = &HFFFF   ''延長警報グループ：無し
                .shtDelay = &HFFFF      ''ディレイタイマ値（アラーム継続時間）:ゼロ
                .shtGRepose1 = &HFFFF   ''グループリポーズ１：無し
                .shtGRepose2 = &HFFFF   ''グループリポーズ２：無し
                .shtM_Repose = 0        ''マニュアルリポーズ：不可

                .shtChType = 2          ''CH Type：無し
                .shtData = 16           ''データ種別コード
                .shtUnit = 0            ''単位種別コード:

                .shtFlag1 = 0           ''動作設定１
                .shtFlag2 = 0           ''動作設定２
                .shtStatus = 64         ''ステータス種別コード

                .shtFuno = &HFFFF       ''FU　番号
                .shtPortno = &HFFFF     ''FU　ポート番号
                .shtPin = &HFFFF        ''FU　計測点番号
                .shtPinNo = 0           ''計測点個数
                .shtEccFunc = 0         ''延長警報盤ECC入出力機能種別コード

                .shtOutPort = 0         ''SIOポート使用有無：無し
                .shtGwsPort = 0         ''GWSポート使用有無：無し
                .strUnit = ""           ''単位種別名称：空白
                .strStatus = ""         ''ステータス名称：空白

                .shtShareType = 0       ''共通CH Local/Remote設定：設定なし
                .shtShareChid = 0       ''リモートCH No ：設定なし
                .shtM_ReposeSet = 0     ''マニュアルリポーズ：設定不可

            End With

            '========================
            'CH個別項目
            '========================
            udtChannel.InitArray()

            For j As Integer = LBound(udtChannel.udtChTypeData) To UBound(udtChannel.udtChTypeData)

                udtChannel.udtChTypeData(j) = 0

            Next j

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "コンポジット設定"

    Public Sub gInitSetComposite(ByRef udtSetChComposite As gTypSetChComposite)

        Try

            ''配列数初期化
            udtSetChComposite.InitArray()

            For i As Integer = LBound(udtSetChComposite.udtComposite) To UBound(udtSetChComposite.udtComposite)

                With udtSetChComposite.udtComposite(i)

                    Call gInitSetCompositeRec(udtSetChComposite.udtComposite(i))

                End With

            Next


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Public Sub gInitSetCompositeRec(ByRef udtSetChCompositeRec As gTypSetChCompositeRec)

        With udtSetChCompositeRec

            .shtChid = 0
            .shtDiFilter = 0

            ''配列数初期化
            .InitArray()

            For j As Integer = LBound(.udtCompInf) To UBound(.udtCompInf)

                .udtCompInf(j).bytBitPattern = 0
                .udtCompInf(j).bytAlarmUse = 0
                .udtCompInf(j).bytDelay = gCstCodeChCompDelayTimerNothing
                .udtCompInf(j).bytExtGroup = gCstCodeChCompExtGroupNothing
                .udtCompInf(j).bytGRepose1 = gCstCodeChCompGroupRepose1Nothing
                .udtCompInf(j).bytGRepose2 = gCstCodeChCompGroupRepose2Nothing
                .udtCompInf(j).strStatusName = ""
                .udtCompInf(j).bytManualReposeState = 0
                .udtCompInf(j).bytManualReposeSet = 0

            Next

        End With

    End Sub

#End Region

#Region "出力チャンネル設定"

    Public Sub gInitSetCHOutPut(ByRef udtSetChOutput As gTypSetChOutput)

        Try

            ''配列数初期化
            udtSetChOutput.InitArray()

            For i As Integer = LBound(udtSetChOutput.udtCHOutPut) To UBound(udtSetChOutput.udtCHOutPut)

                With udtSetChOutput.udtCHOutPut(i)

                    .shtSysno = 0       ''SYSTEM No.
                    .shtChid = 0        ''CH ID 又は 論理出力 ID
                    .bytType = 0        ''CH、論理出力チャネルデータ
                    .bytStatus = 0      ''Output Movement
                    .shtMask = 0        ''Output Movement マスクデータ（ビットパターン）
                    .bytOutput = 0      ''CH OUT Type Setup 255
                    .bytFuno = gCstCodeChNotSetFuNoByte     ''FU 番号
                    .bytPortno = gCstCodeChNotSetFuPortByte ''FU ポート番号
                    .bytPin = gCstCodeChNotSetFuPinByte     ''FU 計測点番号

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "論理出力設定"

    Public Sub gInitSetCHAndOr(ByRef udtSetChAndOr As gTypSetChAndOr)

        Try

            ''配列数初期化
            udtSetChAndOr.InitArray()

            For i As Integer = LBound(udtSetChAndOr.udtCHOut) To UBound(udtSetChAndOr.udtCHOut)

                ''配列数初期化
                udtSetChAndOr.udtCHOut(i).InitArray()

                For j As Integer = LBound(udtSetChAndOr.udtCHOut(i).udtCHAndOr) To UBound(udtSetChAndOr.udtCHOut(i).udtCHAndOr)

                    With udtSetChAndOr.udtCHOut(i).udtCHAndOr(j)

                        .shtSysno = 0       ''SYSTEM No.
                        .shtChid = 0        ''CH ID
                        .bytStatus = 0      ''ステータス種類
                        .shtMask = 0        ''マスクデータ

                    End With

                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "積算データ設定ファイル"

    Public Sub gInitSetChRunHour(ByRef udtSetChRunHour As gTypSetChRunHour)

        Try

            ''配列数初期化
            Call udtSetChRunHour.InitArray()

            ''ポイント数初期化
            For i As Integer = LBound(udtSetChRunHour.udtDetail) To UBound(udtSetChRunHour.udtDetail)

                With udtSetChRunHour.udtDetail(i)

                    .shtSysno = 0
                    .shtChid = 0
                    .shtTrgSysno = 0
                    .shtTrgChid = 0
                    .shtStatus = 0
                    .shtMask = 0
                    .shtSpare1 = 0
                    .shtSpare2 = 0

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "グループ設定"

    Public Sub gInitSetGroupDisp(ByRef udtSetChGroupSet As gTypSetChGroupSet)

        Try

            With udtSetChGroupSet.udtGroup

                .strDrawNo = ""                 ''Draw No.：空白
                .strShipNo = ""                 ''船名    ：空白
                .strComment = ""                ''コメント：空白

                ''配列数初期化
                .InitArray()

                For j As Integer = LBound(.udtGroupInfo) To UBound(.udtGroupInfo)

                    .udtGroupInfo(j).shtGroupNo = j + 1         ''グループ番号     ：1～36
                    .udtGroupInfo(j).strName1 = ""              ''グループ名称1行目：空白
                    .udtGroupInfo(j).strName2 = ""              ''グループ名称2行目：空白
                    .udtGroupInfo(j).strName3 = ""              ''グループ名称3行目：空白
                    .udtGroupInfo(j).shtColor = 0               ''カラー設定       ：白
                    .udtGroupInfo(j).shtDisplayPosition = j + 1 ''表示位置         ：1～36

                Next j

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "リポーズ入力設定"

    Public Sub gInitSetRepose(ByRef udtSetChGroupRepose As gTypSetChGroupRepose)

        Try

            udtSetChGroupRepose.InitArray()   ''配列初期化(構造体全体)

            For i As Integer = 0 To UBound(udtSetChGroupRepose.udtRepose)

                With udtSetChGroupRepose.udtRepose(i)

                    .InitArray()                        ''配列初期化

                    .shtChId = 0                        ''CH ID
                    .shtData = 0                        ''データ種別コード      （0:Normal  1:OR    2:AND   3:MOTOR 4:AND2 + OR）

                    '========================
                    '詳細画面
                    '========================
                    For j = 0 To UBound(.udtReposeInf)
                        With .udtReposeInf(j)

                            .shtChId = 0                ''CH ID
                            .bytMask = 0                ''Alarm Group No：24個分のBit設定（00～07）
                            .bytSpare = 0               ''予備

                        End With
                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "排ガス演算処理テーブル"

    Public Sub gInitSetExhGus(ByRef udtSetChExhGus As gTypSetChExhGus)

        Try

            ''配列数初期化
            udtSetChExhGus.InitArray()
            For i = 0 To UBound(udtSetChExhGus.udtExhGusRec)
                udtSetChExhGus.udtExhGusRec(i).InitArray()
            Next

            For i = 0 To UBound(udtSetChExhGus.udtExhGusRec)
                With udtSetChExhGus.udtExhGusRec(i)

                    .shtNum = 0                             ''ｼﾘﾝﾀﾞ本数                 (0～24)
                    .shtSpare = 0                           ''予備
                    .shtAveSysno = 0                        ''平均値出力CH  SYSTEM No.  (0:Machinery　1:Cargo　　2:共用)
                    .shtAveChid = 0                         ''平均値出力CH　CH ID       (0:設定なし   1～3000)
                    .shtRepSysno = 0                        ''リポーズCH    SYSTEM No.  (0:Machinery　1:Cargo　　2:共用)
                    .shtRepChid = 0                         ''リポーズCH    CH ID       (0:設定なし   1～3000)

                    ''シリンダCH設定
                    For j = 0 To UBound(.udtExhGusCyl)
                        With .udtExhGusCyl(j)
                            .shtSysno = 0                   ''No.n  SYSTEM No.   (0:Machinery　1:Cargo　　2:共用)
                            .shtChid = 0                    ''No.n  CH ID        (0:設定なし   1～3000)
                        End With
                    Next

                    ''偏差CH設定
                    For j = 0 To UBound(.udtExhGusDev)
                        With .udtExhGusDev(j)
                            .shtSysno = 0                   ''No.n  SYSTEM No.   (0:Machinery　1:Cargo　　2:共用)
                            .shtChid = 0                    ''No.n  CH ID        (0:設定なし   1～3000)
                        End With
                    Next

                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "コントロール使用可／不可設定"

    Public Sub gInitSetCtrlUseNotuse(ByRef udtSetChCtrlUse As gTypSetChCtrlUse)

        Try

            ''配列数初期化
            udtSetChCtrlUse.InitArray()
            For i As Integer = 0 To UBound(udtSetChCtrlUse.udtCtrlUseNotuseRec)
                udtSetChCtrlUse.udtCtrlUseNotuseRec(i).InitArray()
            Next

            For i As Integer = 0 To UBound(udtSetChCtrlUse.udtCtrlUseNotuseRec)
                With udtSetChCtrlUse.udtCtrlUseNotuseRec(i)

                    .shtNo = i + 1                                  ''項目番号      （1～32）
                    .shtCount = 0                                   ''条件数        （0～32）
                    .bytFlg = 0                                     ''条件種類      （1：AND:Not Use 2：AND:Use   3：OR:Not Use 4：OR:Use）
                    .bytSpare = 0                                   ''予備

                    For j = 0 To UBound(.udtUseNotuseDetails)
                        With .udtUseNotuseDetails(j)

                            .shtChno = 0                            ''CH NO.        （1～65535）
                            .bytType = 0                            ''条件タイプ    （1：NOR 2：ABNOR 3：BITAND 4：BITOR 5：AND 6：OR）
                            .bytSpare = 0                           ''予備
                            .shtBit = 0                             ''ビット条件    （0～255）
                            .shtProcess1 = 0                        ''Process1      （0～255）
                            .shtProcess2 = 0                        ''Process2      （0～255）

                        End With
                    Next

                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "データ転送テーブル設定"

    Public Sub gInitSetChDataForwardTableSet(ByRef udtSetChDataForward As gTypSetChDataForward)

        Try

            ''配列数初期化
            Call udtSetChDataForward.InitArray()
            For i = LBound(udtSetChDataForward.udtDetail) To UBound(udtSetChDataForward.udtDetail)
                Call udtSetChDataForward.udtDetail(i).InitArray()
            Next

            ''ポイント数初期化
            For i = LBound(udtSetChDataForward.udtDetail) To UBound(udtSetChDataForward.udtDetail)

                With udtSetChDataForward.udtDetail(i)

                    .shtDataCode = 0        ''データコード
                    .shtDataSubCode = 0     ''データサブコード
                    .intOffsetToFCU = 0     ''（OPS→FCU）オフセットアドレス
                    .shtSizeToFCU = 0       ''（OPS→FCU）データサイズ
                    .intOffsetToOPS = 0     ''（FCU→OPS）オフセットアドレス
                    .shtSizeToOps = 0       ''（FCU→OPS）データサイズ

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "データ保存テーブル設定"

    Public Sub gInitSetChDataSaveTable(ByRef udtSetChDataSave As gTypSetChDataSave)

        Try

            ''配列数初期化
            Call udtSetChDataSave.InitArray()

            ''ポイント数初期化
            For i As Integer = LBound(udtSetChDataSave.udtDetail) To UBound(udtSetChDataSave.udtDetail)

                With udtSetChDataSave.udtDetail(i)
                    .shtSysno = 0       ''SYSTEM_NO.
                    .shtChid = 0        ''CH_ID
                    .intDefault = 0     ''デフォルト値
                    .shtSet = 0         ''立ち上げ時のデータ保存方法
                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "SIO設定（外部機器VDR情報設定）"

    Public Sub gInitSetChSio(ByRef udtSetChSio As gTypSetChSio)

        Try

            With udtSetChSio

                ''配列数初期化
                .InitArray()

                ''チャンネル設定数レコード
                For i As Integer = LBound(.shtNum) To UBound(.shtNum)
                    .shtNum(i) = 0
                Next

                ''VDR情報
                For i As Integer = LBound(.udtVdr) To UBound(.udtVdr)

                    Call gInitSetChSioVdr(.udtVdr(i))

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Public Sub gInitSetChSioVdr(ByRef udtVdr As gTypSetChSioVdr)

        Try

            With udtVdr

                .shtPort = 0                      ''ポート番号
                .shtExtComID = 0                  ''外部機器識別子
                .shtPriority = 0                  ''優先度
                .shtSysno = 0                     ''SYSTEM NO
                .shtCommType1 = 0                 ''i/o種類
                .udtCommInf.shtComm = 1           ''回線情報（回線種類）（固定 1:SIO）
                .udtCommInf.shtDataBit = 0        ''回線情報（データビット）
                .udtCommInf.shtParity = 0         ''回線情報（パリティ）
                .udtCommInf.shtStop = 0           ''回線情報（ストップビット）
                .udtCommInf.shtComBps = 1         ''回線情報（通信速度）
                .udtCommInf.shtSpare1 = 0         ''回線情報（予備）
                .udtCommInf.shtSpare2 = 0         ''回線情報（予備）
                .udtCommInf.shtSpare3 = 0         ''回線情報（予備）
                .shtCommType2 = 0                 ''通信種類
                .shtReceiveInit = 0               ''受信タイムアウト（秒）起動時
                .shtReceiveUseally = 0            ''受信タイムアウト（秒）起動後
                .shtSendInit = 0                  ''送信間隔（秒）起動時
                .shtSendUseally = 0               ''送信間隔（秒）起動後
                .shtRetry = 0                     ''リトライ回数
                .shtDuplexSet = 0                 ''Duplex 設定
                .shtSendCH = 0                    ''送信CH

                ''配列数初期化
                .InitArray()

                ''ノード情報
                For j As Integer = LBound(.udtNode) To UBound(.udtNode)
                    .udtNode(j).shtCheck = 0
                    .udtNode(j).shtAddress = 0
                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "SIO設定（外部機器VDR情報設定）CH設定データ"

    Public Sub gInitSetChSioCh(ByRef udtSetChSioCh() As gTypSetChSioCh)

        Try

            ReDim udtSetChSioCh(gCstCntChSioPort - 1)

            For i As Integer = 0 To UBound(udtSetChSioCh)

                Call gInitSetChSioChDetail(udtSetChSioCh(i))

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Public Sub gInitSetChSioChDetail(ByRef udtSetChSioCh As gTypSetChSioCh)

        Try

            Call udtSetChSioCh.InitArray()

            For j As Integer = 0 To UBound(udtSetChSioCh.udtSioChRec)

                With udtSetChSioCh.udtSioChRec(j)

                    .shtChId = 0
                    .shtChNo = 0
                    .shtSpare2 = 0
                    .shtSpare = 0

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'Ver2.0.5.8 
    Public Sub gInitSetChSioExt(ByRef udtSetChSioExt() As gTypSetChSioExt)

        Try
            ReDim udtSetChSioExt(gCstCntChSioVDRPort - 1)

            For i As Integer = 0 To UBound(udtSetChSioExt) Step 1
                Call udtSetChSioExt(i).InitArray()
                For j As Integer = 0 To UBound(udtSetChSioExt(i).bytSioExtRec)
                    udtSetChSioExt(i).bytSioExtRec(j) = 0
                Next j
            Next i


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "延長警報"

    Public Sub gInitSetExtAlarm(ByRef udtSetExtAlarm As gTypSetExtAlarm)

        Try

            ''配列数初期化
            udtSetExtAlarm.InitArray()                     ''構造体全体
            udtSetExtAlarm.udtExtAlarmCommon.InitArray()   ''共通設定

            '========================
            ''延長警報盤：共通設定
            '========================
            With udtSetExtAlarm.udtExtAlarmCommon

                ''LED Panel Set画面
                For i = 0 To UBound(.shtUse)
                    .shtUse(i) = 0                                      ''延長警報盤使用有無    （0：無し       1：有り）
                Next

                ''延長警報 メニュー画面
                .shtCombineSet = 0                                      ''コンバイン設定        （0：標準       1：コンバイン）

                ''Duty Set画面
                .shtDutyFunc = 0                                        ''Duty機能有無          （0：無し       1：有り）
                .shtDutyMethod = 0                                      ''特殊仕様(川汽)設定    （0：無し       1：有り）
                .shtEffect = 0                                          ''Group Effect機能      （0：Normal     1：Fix      2：Ext Output）
                .shtNv = 0                                              ''NVルール              （0：無し       1：有り）
                .shtPart1 = 0                                           ''DutyPart選択（1～15） （0：無し       1：有り）
                .shtPart2 = 0                                           ''DutyPart選択（1～15） （0：無し       1：有り）

                ''Eeengineer Call画面
                .shtEeengineerCall = gBitSet(.shtEeengineerCall, 0, False)  ''機能有無              （0：無し       1：有り）
                .shtEeengineerCall = gBitSet(.shtEeengineerCall, 1, False)  ''選択SW有無            （0：無し       1：有り）
                .shtEeengineerCall = gBitSet(.shtEeengineerCall, 2, False)  ''自動出力              （Bit3:0 + Bit4:0 = Acc 、 Bit3:1 + Bit4:0 = Acc + Ext 、 Bit3:0 + Bit4:1 = Ext）
                .shtEeengineerCall = gBitSet(.shtEeengineerCall, 3, False)  ''自動出力              （Bit3:0 + Bit4:0 = Acc 、 Bit3:1 + Bit4:0 = Acc + Ext 、 Bit3:0 + Bit4:1 = Ext）
                .shtEeengineerCall = gBitSet(.shtEeengineerCall, 4, False)  ''CallTimer通知         （0：Ext+Accom  1：Ext）
                .shtEeengineerCall = gBitSet(.shtEeengineerCall, 5, False)  ''Accept Pattern        （0：or         1：and）

                ''Patrol Man Call画面
                .shtPatrolCall = gBitSet(.shtPatrolCall, 0, False)      ''機能有無              （0：無し       1：有り）
                .shtPatrolCall = gBitSet(.shtPatrolCall, 1, False)      ''SW使用有無            （0：無し       1：有り）
                .shtPatrolCall = gBitSet(.shtPatrolCall, 2, False)      ''CallSet出力方法       （0：無し       1：有り）
                .shtPatrolCall = gBitSet(.shtPatrolCall, 3, False)      ''AlarmSet出力方法      （0：無し       1：有り）
                .shtPatrolCall = gBitSet(.shtPatrolCall, 4, False)      ''Dead Man              （0：無し       1：有り）

                ''Dead Man Call画面
                .shtDeadAlarm = 0                                       ''DeadManAlm使用有無    （0：無し       1：有り）

                ''System Set画面
                .shtLamps = 8                                           ''アラームランプ数      （8～12）
                .shtBuzzer = 1                                          ''ブザーパターン        （1～3）
                .shtGrpOut = 0                                          ''グループ出力パターン  （標準：0       パターン：1～4）
                .shtGrpEffct = 0                                        ''GrpEffect （12個）    （0：OFF        1：ON）
                .shtGrpFire = 0                                         ''FireSoundGrp（12個）  （0：OFF        1：ON）
                .shtGrpAlarm = 0                                        ''GrpAlmLump出力選択    （0：Normal     1：EXT Same output）
                .shtFireBuzzer = 0                                      ''Fireブザーパターン    （0：continuous 1：intermission）
                .shtRsv = 0                                             ''予備

                ''Alarm Group画面
                For i = 0 To UBound(.intGroupType)                      ''EXTグループアラーム出力設定（Bit00～23    1：ON    0：OFF ）
                    .intGroupType(i) = 0                                ''Machinery                  （Bit24        0：なし  1：有り）
                Next                                                    ''Cargo設定                  （Bit25        0：なし  1：有り）

                ''Panel Set画面
                .shtSpecialWh = 0                                       ''特殊(川汽)仕様 (W/H)
                .shtSpecialPr = 0                                       ''特殊(川汽)仕様 (P/R)
                .shtSpecialCe = 0                                       ''特殊(川汽)仕様 (C/E)
                .shtEngCall = 0                                         ''Eeengineer Call設定
                .Option1 = 0                                            '' 特殊設定1  Ver1.8.7 2015.12.10
                .Option2 = 0                                            '' 特殊設定2  Ver1.8.7 2015.12.10
                .Option3 = 0                                            '' 特殊設定3  Ver1.8.7 2015.12.10

                ''EXT Group Display画面
                For i = 0 To UBound(.udtExtGroup)
                    With .udtExtGroup(i)
                        .shtGroup = i + 1                               ''警報グループ番号（LCD設定）（1～12 ）
                        .shtMark = i + 1                                ''マーク番号（LCD設定）      （1～255   0：設定なし）
                        .strGroupName = ""                              ''グループ名称（LCD設定）
                    End With
                Next

                ''LCD Duty Display画面
                For i = 0 To UBound(.udtExtDuty)
                    .udtExtDuty(i).strDutyName = ""                     ''Duty名称  (1～3文字設定   予備5byte)
                Next

                ''予備
                For i = 0 To UBound(.shtSpare)
                    .shtSpare(i) = 0                                    ''予備
                Next

            End With


            '========================
            ''延長警報盤：個別設定
            '========================
            For i As Integer = LBound(udtSetExtAlarm.udtExtAlarm) To UBound(udtSetExtAlarm.udtExtAlarm)

                With udtSetExtAlarm.udtExtAlarm(i)

                    .InitArray()                            ''配列数初期化

                    ''Panel Set画面
                    .strPlace = ""                          ''設置場所                  （14 byte)
                    .shtNo = 0                              ''延長警報パネル通信ID番号  （0：パネル無し 1～20  ）
                    .shtReAlarm = 0                         ''Re Alarm設定有無          （0：無し       1：有り）
                    .shtBuzzCut = 0                         ''ブザーカット有無          （0：無し       1：有り）
                    .shtFreeEng = 0                         ''フリーエンジニア有無      （0：無し       1：有り）
                    .shtOption = 0                          ''オプション                （0～255）
                    .shtPanel = 0                           ''パネルタイプ              （0：LED        1：LCD ）
                    .shtPart = gBitSet(.shtPart, 0, False)  ''パート設定：Machinery     （0：無し       1：有り）※bit2～7：0 予備
                    .shtPart = gBitSet(.shtPart, 1, False)  ''パート設定：Cargo         （0：無し       1：有り）

                    ''LED Pattern画面
                    .shtEngNo = 0                           ''EeengineerCallNo設定        （1～255）
                    .shtDuty = 0                            ''Duty番号                  （1～255）
                    .shtDutyBuzz = 0                        ''Duty Buz Stop動作設定     （0：個別       1：全室）
                    .shtWatchLed = 0                        ''Watch LED 表示方法選択    （0：なし　1：UNMANのみ　2:MANのみ　3:MAN+UNMAN）
                    .shtLedOut = 0                          ''LED表示方法               （0～13） 

                    ''LCD Ext Group Display画面
                    For j = 0 To UBound(.shtLedTimer)
                        .shtLedTimer(j) = 0                 ''12個分の遅延タイマ値      （0～1800）
                    Next

                    ''LCD Duty Display画面
                    For j = 0 To UBound(.shtPosition)       ''表示位置（LCD設定）       （1～30）
                        .shtPosition(j) = 0
                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "タイマ設定"

    Public Sub gInitSetTimer(ByRef udtSetExtTimerSet As gTypSetExtTimerSet)

        Try

            ''配列数初期化
            udtSetExtTimerSet.InitArray()

            For i As Integer = 0 To UBound(udtSetExtTimerSet.udtTimerInfo)

                With udtSetExtTimerSet.udtTimerInfo(i)
                    .shtType = 0                ''種類          （0：未使用　1：ﾃﾞｯﾄﾞﾏﾝｱﾗｰﾑ1　2:ﾃﾞｯﾄﾞﾏﾝｱﾗｰﾑ2　3:ｴﾝｼﾞﾆｱｺｰﾙ 4～16：予備）
                    .bytIndex = i + 1           ''レコード番号  （1～16）
                    .bytPart = 0                ''パート       　(0:Mach、1:Cargo、2:共用)      追加 ver.1.4.4 2012.05.08
                    .shtTimeDisp = 0            ''分/秒切替設定 （0:秒、1:分）
                    .shtInit = Nothing          ''初期値        （1～600 秒）
                    .shtLow = Nothing           ''下限値        （1～600 秒）
                    .shtHigh = Nothing          ''上限値        （1～600 秒）
                End With

            Next

            ''１行目：ﾃﾞｯﾄﾞﾏﾝｱﾗｰﾑ1
            With udtSetExtTimerSet.udtTimerInfo(0)
                .shtType = 0                    ''種類
                .bytIndex = 1                   ''レコード番号
                .bytPart = 0                    ''パート           追加 ver.1.4.4 2012.05.08
                .shtTimeDisp = 0                ''分/秒切替設定
                .shtInit = 0                    ''初期値
                .shtLow = 0                     ''下限値
                .shtHigh = 0                    ''上限値
            End With

            ''２行目：ﾃﾞｯﾄﾞﾏﾝｱﾗｰﾑ2
            With udtSetExtTimerSet.udtTimerInfo(1)
                .shtType = 0                    ''種類
                .bytIndex = 2                   ''レコード番号
                .bytPart = 0                    ''パート           追加 ver.1.4.4 2012.05.08
                .shtTimeDisp = 0                ''分/秒切替設定
                .shtInit = 0                    ''初期値
                .shtLow = 0                     ''下限値
                .shtHigh = 0                    ''上限値
            End With

            ''３行目：ｴﾝｼﾞﾆｱｺｰﾙ
            With udtSetExtTimerSet.udtTimerInfo(2)
                .shtType = 0                    ''種類
                .bytIndex = 3                   ''レコード番号
                .bytPart = 0                    ''パート           追加 ver.1.4.4 2012.05.08
                .shtTimeDisp = 0                ''分/秒切替設定
                .shtInit = 0                    ''初期値
                .shtLow = 0                     ''下限値
                .shtHigh = 0                    ''上限値
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "タイマ表示名称設定"

    Public Sub gInitSetTimerName(ByRef udtSetExtTimerName As gTypSetExtTimerName)

        Try

            ''配列数初期化
            udtSetExtTimerName.InitArray()

            For i As Integer = 0 To UBound(udtSetExtTimerName.udtTimerRec)

                With udtSetExtTimerName.udtTimerRec(i)

                    .strName = ""               ''名称（半角32文字）

                End With

            Next

            ' ''１行目：ﾃﾞｯﾄﾞﾏﾝｱﾗｰﾑ1
            'With udtSetExtTimerName.udtTimerRec(0)
            '    .strName = "Timer1"                   ''名称
            'End With

            ' ''２行目:ﾃﾞｯﾄﾞﾏﾝｱﾗｰﾑ12
            'With udtSetExtTimerName.udtTimerRec(1)
            '    .strName = "Timer2"                   ''名称
            'End With

            ' ''３行目：ｴﾝｼﾞﾆｱｺｰﾙ
            'With udtSetExtTimerName.udtTimerRec(2)
            '    .strName = "Timer3"                   ''名称
            'End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "シーケンス設定"

    Public Sub gInitSetSeqSequence(ByRef udtSetSeqID As gTypSetSeqID, ByRef udtSetSeqSet As gTypSetSeqSet)

        Try

            ''配列数初期化
            Call udtSetSeqID.InitArray()
            Call udtSetSeqSet.InitArray()

            ''シーケンスID初期化
            For i As Integer = LBound(udtSetSeqID.shtID) To UBound(udtSetSeqID.shtID)

                Call gInitSetSeqSequenceIDOne(udtSetSeqID.shtID(i))

            Next

            ''シーケンス設定初期化
            For i As Integer = LBound(udtSetSeqSet.udtDetail) To UBound(udtSetSeqSet.udtDetail)

                Call gInitSetSeqSequenceDetailOne(i + 1 + 10000, udtSetSeqSet.udtDetail(i))

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Public Sub gInitSetSeqSequenceIDOne(ByRef shtID As Short)

        Try

            shtID = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Public Sub gInitSetSeqSequenceDetailOne(ByVal intSeqID As Integer, _
                                            ByRef udtSeqSequenceSetDetail As gTypSetSeqSetRec)

        Try

            With udtSeqSequenceSetDetail

                .shtId = intSeqID                                       ''シーケンスＩＤ
                .shtLogicType = 0                                       ''出力ロジックタイプ
                .strRemarks = ""                                        ''備考
                .shtOutSysno = 0                                        ''SYSTEM No.
                .shtOutChid = 0                                         ''CH ID
                .bytOutStatus = 0                                       ''出力ステータス
                .bytOutIoSelect = 0                                     ''入出力区分
                .shtOutData = 0                                         ''出力データ
                .shtOutDelay = 0                                        ''出力オフディレイ
                .bytOutDataType = 0                                     ''出力データタイプ
                .bytOutInv = 0                                          ''出力反転
                .bytFuno = 255                                          ''FU　番号
                .bytPort = 255                                          ''FU ポート番号
                .bytPin = 255                                           ''FU　計測点位置
                .bytPinNo = 1                                           ''FU　計測点位置
                .bytOutType = 0                                         ''出力タイプ
                .bytOneShot = 0                                         ''出力ワンショット時間
                .bytContine = 0                                         ''処理継続中止

                ''配列数初期化
                .InitArray()

                ''演算参照テーブル
                For j As Integer = LBound(._shtLogicItem) To UBound(._shtLogicItem)
                    .shtLogicItem(j) = 0
                Next

                ''チャンネル使用有無
                For j As Integer = LBound(.shtUseCh) To UBound(.shtUseCh)
                    .shtUseCh(j) = 0
                Next

                ''入力CH情報
                For j As Integer = LBound(.udtInput) To UBound(.udtInput)
                    Call gInitSetSeqSequenceInputOne(.udtInput(j))
                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Public Sub gInitSetSeqSequenceInputOne(ByRef udtSeqSequenceSetInput As gTypSetSeqSetRecInput)

        Try

            ''入力CH情報
            With udtSeqSequenceSetInput

                .shtSysno = 0                                   ''SYSTEM No.
                .shtChid = 0                                    ''CH ID
                .shtIoSelect = 0                                ''入出力区分
                .shtChSelect = 0                                ''CH選択
                .bytStatus = 0                                  ''参照ステータス
                .bytType = 0                                    ''タイプ
                .shtMask = 0                                    ''マスク値
                .shtAnalogType = 1                              ''アナログ入力種別

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#End Region

#Region "リニアライズテーブル"

    Public Sub gInitSetSeqLinearTable(ByRef udtSetSeqLinear As gTypSetSeqLinear)

        Try

            ''配列数初期化
            Call udtSetSeqLinear.InitArray()

            ''ポイント数初期化
            For i As Integer = LBound(udtSetSeqLinear.udtPoints) To UBound(udtSetSeqLinear.udtPoints)
                udtSetSeqLinear.udtPoints(i).shtPoint = 0
            Next

            ''リニアライズテーブル初期化
            For i As Integer = LBound(udtSetSeqLinear.udtTables) To UBound(udtSetSeqLinear.udtTables)

                ''配列数初期化
                Call udtSetSeqLinear.udtTables(i).InitArray()

                For j As Integer = LBound(udtSetSeqLinear.udtTables(i).udtRow) To UBound(udtSetSeqLinear.udtTables(i).udtRow)
                    udtSetSeqLinear.udtTables(i).udtRow(j).sngPtX = 0
                    udtSetSeqLinear.udtTables(i).udtRow(j).sngPtY = 0
                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "演算式テーブル"

    Public Sub gInitSetSeqOperationExpression(ByRef udtSetSeqOpeExp As gTypSetSeqOperationExpression)

        Try

            ''配列数初期化
            Call udtSetSeqOpeExp.InitArray()

            ''詳細初期化
            For i As Integer = LBound(udtSetSeqOpeExp.udtTables) To UBound(udtSetSeqOpeExp.udtTables)

                ''演算式初期化
                udtSetSeqOpeExp.udtTables(i).strExp = ""

                ''配列数初期化
                Call udtSetSeqOpeExp.udtTables(i).InitArray()

                ''VariableName初期化
                For j As Integer = LBound(udtSetSeqOpeExp.udtTables(i).strVariavleName) To UBound(udtSetSeqOpeExp.udtTables(i).strVariavleName)
                    udtSetSeqOpeExp.udtTables(i).strVariavleName(j) = ""
                Next

                ''AryInf初期化
                For j As Integer = LBound(udtSetSeqOpeExp.udtTables(i).udtAryInf) To UBound(udtSetSeqOpeExp.udtTables(i).udtAryInf)
                    udtSetSeqOpeExp.udtTables(i).udtAryInf(j).shtType = 0
                    Call udtSetSeqOpeExp.udtTables(i).udtAryInf(j).InitArray()
                    udtSetSeqOpeExp.udtTables(i).udtAryInf(j).strFixNum = ""
                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "OPS画面タイトル"

    Public Sub gInitSetOpsDisp(ByRef udtSetOpsScreenTitle As gTypSetOpsScreenTitle)

        Try

            Dim udtCodeName() As gTypCodeName = Nothing

            ''配列数初期化
            udtSetOpsScreenTitle.InitArray()

            ''iniファイルからスクリーンタイトル取得
            If gGetComboCodeName(udtCodeName, gEnmComboType.ctOpsScreenTitle) = 0 Then

                For i As Integer = LBound(udtCodeName) To UBound(udtCodeName)
                    udtSetOpsScreenTitle.udtOpsScreenTitle(i).bytScreenNo = udtCodeName(i).shtCode
                    udtSetOpsScreenTitle.udtOpsScreenTitle(i).strScreenName = udtCodeName(i).strName
                Next

            End If

            'udtSetScreenTitle.udtOpsScreenTitle(0).strScreenName = "OVER VIEW"
            'udtSetScreenTitle.udtOpsScreenTitle(1).strScreenName = "GROUP CALL"
            'udtSetScreenTitle.udtOpsScreenTitle(2).strScreenName = "CHANNEL CALL"
            'udtSetScreenTitle.udtOpsScreenTitle(3).strScreenName = "TREND"
            'udtSetScreenTitle.udtOpsScreenTitle(4).strScreenName = "TREND GRAPH CHANGE"
            'udtSetScreenTitle.udtOpsScreenTitle(5).strScreenName = "FREE DISPLAY"
            'udtSetScreenTitle.udtOpsScreenTitle(6).strScreenName = "FREE GRAPH CHANGE"
            'udtSetScreenTitle.udtOpsScreenTitle(7).strScreenName = "ALARM SUMMARY"
            'udtSetScreenTitle.udtOpsScreenTitle(8).strScreenName = "SENSOR FAILURE SUMMARY"
            'udtSetScreenTitle.udtOpsScreenTitle(9).strScreenName = "MANUAL REPOSE SUMMARY"
            'udtSetScreenTitle.udtOpsScreenTitle(10).strScreenName = "ALARM HISTORY"
            'udtSetScreenTitle.udtOpsScreenTitle(11).strScreenName = "SENSOR FAILURE HISTORY"
            'udtSetScreenTitle.udtOpsScreenTitle(12).strScreenName = "OPERATION HISTORY"
            'udtSetScreenTitle.udtOpsScreenTitle(13).strScreenName = "EVENT HISTORY"
            'udtSetScreenTitle.udtOpsScreenTitle(14).strScreenName = "GRAPH(DEVIATION GRAPH)"
            'udtSetScreenTitle.udtOpsScreenTitle(15).strScreenName = "GRAPH(BAR GRAPH)"
            'udtSetScreenTitle.udtOpsScreenTitle(16).strScreenName = "GRAPH(ANALOG METER)"
            'udtSetScreenTitle.udtOpsScreenTitle(17).strScreenName = "FREE GRAPH"
            'udtSetScreenTitle.udtOpsScreenTitle(18).strScreenName = "FREE GRAPH(DRAW)"
            'udtSetScreenTitle.udtOpsScreenTitle(19).strScreenName = "FREE GRAPH(CHANNEL SET)"
            'udtSetScreenTitle.udtOpsScreenTitle(20).strScreenName = "SYSTEM STATUS"
            'udtSetScreenTitle.udtOpsScreenTitle(21).strScreenName = "OPS STATUS"
            'udtSetScreenTitle.udtOpsScreenTitle(22).strScreenName = "FCU STATUS"
            'udtSetScreenTitle.udtOpsScreenTitle(23).strScreenName = "FU TERMINAL BOARD"
            'udtSetScreenTitle.udtOpsScreenTitle(24).strScreenName = "FU STATUS"
            'udtSetScreenTitle.udtOpsScreenTitle(25).strScreenName = "SYSTEM CONFIGRATION"
            'udtSetScreenTitle.udtOpsScreenTitle(26).strScreenName = "LOG TIME CHANGE"
            'udtSetScreenTitle.udtOpsScreenTitle(27).strScreenName = "VERSION DISPLAY"
            'udtSetScreenTitle.udtOpsScreenTitle(28).strScreenName = "HELP"
            'udtSetScreenTitle.udtOpsScreenTitle(29).strScreenName = "ENGINEER MENU"
            'udtSetScreenTitle.udtOpsScreenTitle(30).strScreenName = "LOG FORMAT SET"
            'udtSetScreenTitle.udtOpsScreenTitle(31).strScreenName = "LOG FORMAT(SET)"
            'udtSetScreenTitle.udtOpsScreenTitle(32).strScreenName = "ANALOG CALIBRATION"
            'udtSetScreenTitle.udtOpsScreenTitle(33).strScreenName = "CHANNEL EDIT"
            'udtSetScreenTitle.udtOpsScreenTitle(34).strScreenName = "AUTO ALARM"
            'udtSetScreenTitle.udtOpsScreenTitle(35).strScreenName = "WINDOW ALARM"
            'udtSetScreenTitle.udtOpsScreenTitle(36).strScreenName = "MIMIC"
            'udtSetScreenTitle.udtOpsScreenTitle(37).strScreenName = "MIMIC VIEW"
            'udtSetScreenTitle.udtOpsScreenTitle(38).strScreenName = "FOUR DIVIDED"

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "プルダウンメニュー"

    Public Sub gInitSetOpsPulldownMenu(ByRef udtSetOpsPulldownMenu As gTypSetOpsPulldownMenu)

        Try

            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim k As Integer = 0

            ''配列数初期化
            Call udtSetOpsPulldownMenu.InitArray()

            ''メインメニュー
            For i = 0 To UBound(udtSetOpsPulldownMenu.udtDetail)
                udtSetOpsPulldownMenu.udtDetail(i).InitArray()

                ''サブメニューグループ
                For j = 0 To UBound(udtSetOpsPulldownMenu.udtDetail(i).udtGroup)
                    udtSetOpsPulldownMenu.udtDetail(i).udtGroup(j).InitArray()
                Next

            Next

            ''メインメニュー設定
            For i = 0 To UBound(udtSetOpsPulldownMenu.udtDetail)

                With udtSetOpsPulldownMenu.udtDetail(i)

                    .strName = ""       ''メインメニュー名称        
                    .tx = 0
                    .ty = 0
                    .bx = 0
                    .by = 0
                    .OPSSTFLG1 = 0
                    .OPSSTFLG2 = 0
                    .bytMenuNo1 = 0
                    .Spare1 = 0
                    .Spare2 = 0
                    .Spare3 = 0
                    .Spare4 = 0
                    .Spare5 = 0
                    .bytMenuType = 0    ''メニュータイプ            (0～3)
                    .Yobi1 = 0
                    .Yobi2 = 0
                    .bytMenuSet = 0     ''メインメニューセット数    (0～17)
                    .groupviewx = 0
                    .groupviewy = 0
                    .groupsizex = 0
                    .groupsizey = 0

                    ''サブメニュー
                    For j = 0 To UBound(.udtGroup)
                        Call gInitOpsPulldownGroup(j, .udtGroup(j))

                        ''サブグループ
                        For k = 0 To UBound(.udtGroup(j).udtSub)
                            Call gInitOpsPulldownSub(k, .udtGroup(j).udtSub(k))
                        Next
                    Next

                End With

            Next


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <サブグループ>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) サブメニューの処理行番号
    '           : ARG2 - (IO) サブグループ構造体
    '--------------------------------------------------------------------
    Public Sub gInitOpsPulldownGroup(ByVal intSubIndex As Integer, _
                                     ByRef udtGroup As gTypSetOpsPulldownMenuGroup)

        Try

            With udtGroup

                .strName = ""               ''サブグループ名称      (半角11文字)
                .bytCount = 0               ''サブグループ設定数    (0～17)
                .grouptx = 0
                .groupty = 0
                .groupbx = 0
                .groupby = 0
                .groupSpare1 = 0
                .groupSpare2 = 0
                .groupSpare3 = 0
                .groupSpare4 = 0
                .groupbytMenuType = 0
                .SubgroupYobi1 = 0
                .SubgroupYobi2 = 0
                .bytCount = 0
                .Subviewx = 0
                .Subviewy = 0
                .Subsizex = 0
                .Subsizey = 0

                ''サブメニュー設定
                For i = 0 To UBound(udtGroup.udtSub)
                    Call gInitOpsPulldownSub(i, .udtSub(i))
                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <サブメニュー>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中の行番号
    '           : ARG2 - (IO) サブメニュー構造体
    '--------------------------------------------------------------------
    Public Sub gInitOpsPulldownSub(ByVal intCurIndex As Integer, _
                                   ByRef udtSub As gTypSetOpsPulldownMenuSub)

        Try

            With udtSub

                .strName = ""               ''サブメニュー名称  (半角11文字)
                .SubbytMenuType1 = 0
                .SubbytMenuType2 = 0
                .SubbytMenuType3 = 0
                .SubbytMenuType4 = 0
                .SubYobi1 = 0
                .SubYobi2 = 0
                .bytKeyCode = 0             ''キーコード        (0～255)
                .SubYobi4 = 0
                .ViewNo1 = 0
                .ViewNo2 = 0
                .ViewNo3 = 0
                .ViewNo4 = 0
                .SubMenutx = 0
                .SubMenuty = 0
                .SubMenubx = 0
                .SubMenuby = 0

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "セレクションメニュー"

    Public Sub gInitSetOpsSelectionMenu(ByRef udtSetOpsSelectionMenu As gTypSetOpsSelectionMenu)

        Try

            Dim strLine() As String = Nothing
            Dim strwk() As String = Nothing


            Dim k As Integer = 0

            ''配列数初期化
            udtSetOpsSelectionMenu.InitArray()

            For i As Integer = 0 To UBound(udtSetOpsSelectionMenu.udtOpsSelectionOffSetRec)
                udtSetOpsSelectionMenu.udtOpsSelectionOffSetRec(i).ViewNo = 0
            Next

            ''サブメニューグループ
            For j = 0 To UBound(udtSetOpsSelectionMenu.udtOpsSelectionSetViewRec)
                udtSetOpsSelectionMenu.udtOpsSelectionSetViewRec(j).InitArray()

                ' ''サブメニューグループ
                'For j = 0 To UBound(udtSetOpsSelectionMenu.udtOpsSelectionSetViewRec(i).udtKey)
                '    udtSetOpsSelectionMenu.udtOpsSelectionSetViewRec(i).udtKey(j).InitArray()
                'Next



            Next


            ''メインメニュー設定
            For i = 0 To UBound(udtSetOpsSelectionMenu.udtOpsSelectionSetViewRec)

                With udtSetOpsSelectionMenu.udtOpsSelectionSetViewRec(i)

                    .SelectName = ""       ''メインメニュー名称        

                    ''サブメニュー
                    For j = 0 To UBound(.udtKey)
                        Call gInitOpsSelectionSetView(j, .udtKey(j))
                    Next

                End With

            Next

            ''iniファイルからスクリーン設定タイトル取得
            If gGetIniFileLine(strLine, gEnmComboType.ctOpsSelectionSelectionDefault) = 0 Then

                For i As Integer = 0 To UBound(strLine)

                    ''使用有無は初期化では 0 を設定
                    udtSetOpsSelectionMenu.udtOpsSelectionMenuNameKeyRec(i).SelectMenuKeyName = 0
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <サブメニュー>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中の行番号
    '           : ARG2 - (IO) サブメニュー構造体
    '--------------------------------------------------------------------
    Public Sub gInitOpsSelectionSetView(ByVal intCurIndex As Integer, _
                                   ByRef udtSub As gTypSetOpsSelectionMenuKey)

        Try

            With udtSub

                .BytNameType1 = 0               ''サブメニュー名称  (半角11文字)
                .BytNameType2 = 0
                .BytNameType3 = 0
                .BytNameType4 = 0
                .BytSelectName = 0
                .NameCode = 0
                .Yobi1 = 0

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#End Region

#Region "OPS設定"

    Public Sub gInitSetOpsGraph(ByRef udtSetOpsGraph As gTypSetOpsGraph)

        ''配列数初期化
        udtSetOpsGraph.InitArray()     ''構造体全体

        ''グラフタイトル
        For i = 0 To UBound(udtSetOpsGraph.udtGraphTitleRec)
            udtSetOpsGraph.udtGraphExhaustRec(i).InitArray()       ''配列数初期化
            Call gInitOpsGraphTitle(i, udtSetOpsGraph.udtGraphTitleRec(i))
        Next

        ''偏差グラフ
        For i = 0 To UBound(udtSetOpsGraph.udtGraphExhaustRec)
            udtSetOpsGraph.udtGraphBarRec(i).InitArray()           ''配列数初期化
            Call gInitOpsGraphExhaust(i, udtSetOpsGraph.udtGraphExhaustRec(i))
        Next

        ''バーグラフ
        For i As Integer = 0 To UBound(udtSetOpsGraph.udtGraphBarRec)
            udtSetOpsGraph.udtGraphAnalogMeterRec(i).InitArray()   ''配列数初期化
            Call gInitOpsGraphBar(i, udtSetOpsGraph.udtGraphBarRec(i))
        Next

        ''アナログメーター
        For i As Integer = 0 To UBound(udtSetOpsGraph.udtGraphAnalogMeterRec)
            udtSetOpsGraph.udtGraphAnalogMeterRec(i).InitArray()   ''配列数初期化
            Call gInitOpsGraphAnalogMeter(i, udtSetOpsGraph.udtGraphAnalogMeterRec(i))
        Next

        ' 2013.07.22 グラフとフリーグラフと分離(以下コメント）  K.Fujimoto
        ' ''フリーグラフ
        'For i As Integer = 0 To UBound(udtSetOpsGraph.udtGraphFreeRec)
        '    udtSetOpsGraph.udtGraphFreeRec(i).InitArray()          ''配列数初期化
        '    For j As Integer = 0 To UBound(udtSetOpsGraph.udtGraphFreeRec(i).udtFreeGraphTitle)
        '        udtSetOpsGraph.udtGraphFreeRec(i).udtFreeGraphTitle(j).InitArray() ''配列数初期化
        '        Call gInitOpsGraphFree(i, j, udtSetOpsGraph.udtGraphFreeRec(i).udtFreeGraphTitle(j))
        '    Next
        'Next

        ''アナログメーター設定
        With udtSetOpsGraph.udtGraphAnalogMeterSettingRec

            .bytChNameDisplayPoint = 2      ''CH名称表示位置    (1:Left、2:Center、3:Right)
            .bytMarkNumericalValue = 2      ''目盛数値表示方法  (1:Normal、2:Short)      '' Ver1.9.0 2015.12.17 初期値 1 → 2に変更 
            .bytPointerFrame = 0            ''指針の縁取り      (0:No、1:Yes)
            .bytPointerColorChange = 0      ''指針の色変更      (0:No、1:Yes)
            '.bytSideColorSymbol = 0         ''シンボル表示有無  (0:No、1:Yes)
            .strSpare = ""

        End With


    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <グラフタイトル>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中の行番号
    '           : ARG2 - (IO) グラフタイトル構造体
    '--------------------------------------------------------------------
    Public Sub gInitOpsGraphTitle(ByVal intCurIndex As Integer, _
                                  ByRef udtTitle As gTypSetOpsGraphTitle)

        Try

            With udtTitle

                .bytNo = intCurIndex + 1        ''グラフ番号(1～16)
                .bytType = 0                    ''グラフタイプ(1:Exhaust、2:Bar、3:AnalogMeter)
                .strName = ""                   ''グラフ名称(半角26文字)
                .strSpare = ""                  ''予備

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <偏差グラフ（排ガス）>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中の行番号
    '           : ARG2 - (IO) 偏差グラフ構造体
    '--------------------------------------------------------------------
    Public Sub gInitOpsGraphExhaust(ByVal intCurIndex As Integer, _
                                    ByRef udtExt As gTypSetOpsGraphExhaust)

        Try

            With udtExt

                .bytNo = intCurIndex + 1        ''グラフ番号                (1～16)
                .strSpare = ""
                .strTitle = ""                  ''グラフタイトル            (半角26文字)
                .strItemUp = ""                 ''グラフデータ名称（上段）  (半角4文字)
                .strItemDown = ""               ''グラフデータ名称（下段）  (半角4文字)

                .shtAveCh = 0                   ''平均CH
                .bytDevMark = 0                 ''偏差目盛の上下限値        (0～255)
                '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                '.byt20Graph = 0                 ''グラフ20本区切り          (0:OFF、1：ON)
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                .bytLine = 0                    ''Line設定                  (0:設定なし、1:1Line、2:2LINE)

                ''デフォルト"TURBO CHARGER"設定追加　ver.1.4.0 2011.09.21
                '.strTcTitle = ""                ''T/Cグラフのタイトル       (半角16文字)
                .strTcTitle = "TURBO CHARGER"    ''T/Cグラフのタイトル       (半角16文字)

                .strSpare2 = ""
                .strSpare3 = ""
                .strTcComm1 = ""
                .strTcComm2 = ""

                ''Cylinder CH_NO.
                .bytCyCnt = 0                   ''シリンダの数  (1～24)
                For i = 0 To UBound(.udtCylinder)
                    Call gInitOpsGraphExhaustCylinder(.udtCylinder(i))
                Next

                ''T/C CH_NO.
                .bytTcCnt = 0                   ''T/Cの数       (1～8)
                For i = 0 To UBound(.udtTurboCharger)
                    Call gInitOpsGraphExhaustTurboCharger(.udtTurboCharger(i))
                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <偏差グラフ（排ガス）> < シリンダチャンネル >
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) ターボチャージャー構造体
    '--------------------------------------------------------------------
    Public Sub gInitOpsGraphExhaustCylinder(ByRef udtCylinder As gTypSetOpsGraphExhaustCylinder)

        With udtCylinder

            .shtChCylinder = 0      ''Cyl CH_NO.    (1～65535)
            .shtChDeviation = 0     ''Dev CH_NO.    (1～65535)
            .strTitle = ""          ''名称          (半角5文字)

        End With

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <偏差グラフ（排ガス）> < ターボチャージャーチャンネル >
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) ターボチャージャー構造体
    '--------------------------------------------------------------------
    Public Sub gInitOpsGraphExhaustTurboCharger(ByRef udtTurboCharger As gTypSetOpsGraphExhaustTurboCharger)

        With udtTurboCharger

            .shtChTurboCharger = 0  ''T/C CH_NO.    (1～65535)
            .strTitle = ""          ''名称          (半角5文字)
            .bytSplitLine = 0       ''T/C 区切り線  (0：なし　1：あり)

        End With

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <バーグラフ>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中の行番号
    '           : ARG2 - (IO) バーグラフ構造体
    '--------------------------------------------------------------------
    Public Sub gInitOpsGraphBar(ByVal intCurIndex As Integer, _
                                ByRef udtBar As gTypSetOpsGraphBar)

        Try

            With udtBar

                .bytNo = intCurIndex + 1        ''グラフ番号        (1～16)
                .strTitle = ""                  ''グラフタイトル    (半角26文字)
                .strItemUp = ""                 ''データ名称（上段）(半角4文字)
                .strItemDown = ""               ''データ名称（下段）(半角4文字)
                .bytCyCnt = 0                   ''シリンダの数      (1～24)
                '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph→表示切替に変更 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                .bytDisplay = 0                 ''表示切替指定（0:計測点レンジ、1:百分率）
                '---------------------------------------------------------------------------------------------------
                '.byt20Graph = 0                 ''グラフ20本区切り  (0:OFF、1：ON)
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                .bytLine = 0                    ''数値の分け方      (0:設定なし、1:1Line、2:2LINE)
                .bytDevision = 0                ''分割数            (1:４分割、2:６分割、3:３ｘ５分割)

                .strSpare = ""

                ''詳細初期化
                For i As Integer = 0 To UBound(.udtCylinder)

                    With .udtCylinder(i)

                        .shtChCylinder = 0      ''CH番号            (1～65535)
                        .strTitle = ""          ''名称              (半角5文字)

                    End With

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <アナログメーター>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中の行番号
    '           : ARG2 - (IO) アナログメーター構造体
    '--------------------------------------------------------------------
    Public Sub gInitOpsGraphAnalogMeter(ByVal intCurIndex As Integer, _
                                        ByRef udtMeter As gTypSetOpsGraphAnalogMeter)

        Try

            With udtMeter

                .bytNo = intCurIndex + 1        ''グラフ番号        (1～16)
                .strTitle = ""                  ''グラフタイトル    (26文字)
                .bytMeterType = 1               ''表示タイプ        (1, 8表示    2, 1:4表示     3, 4:1表示  4, 2:1:2表示    5, 2表示）
                .strSpare = ""

                ''詳細初期化
                For i As Integer = 0 To UBound(.udtDetail)

                    With .udtDetail(i)

                        .shtChNo = 0            ''CH番号            (1～65535)
                        .bytScale = 0           ''目盛り分割数      (3～7)
                        .bytColor = 0           ''表示色            (0～255)

                    End With

                Next

            End With


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <フリーグラフ>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中のOPS番号
    ' 　　　    : ARG2 - (I ) 処理中の行番号
    '           : ARG3 - (IO) フリーグラフ構造体
    '--------------------------------------------------------------------
    Public Sub gInitOpsGraphFree(ByVal intOpsIndex As Integer, _
                                 ByVal intRowIndex As Integer, _
                                 ByRef udtFreeGraph As gTypSetOpsFreeGraphRec)

        Try

            With udtFreeGraph

                .bytOpsNo = intOpsIndex + 1
                .bytGraphNo = intRowIndex + 1
                .strGraphTitle = ""
                .strSpare = ""

                ''配列数初期化
                .InitArray()

                ''詳細初期化
                For i As Integer = LBound(.udtFreeDetail) To UBound(.udtFreeDetail)

                    Call gInitOpsGraphFreeDetail(i, .udtFreeDetail(i))

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能説明  : データ削除 <フリーグラフ詳細>
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 処理中のOPS番号
    ' 　　　    : ARG2 - (I ) 処理中の行番号
    '           : ARG3 - (IO) フリーグラフ構造体
    '--------------------------------------------------------------------
    Public Sub gInitOpsGraphFreeDetail(ByVal intIndex As Integer, ByRef udtFreeGraphDetail As gTypSetOpsFreeGraphDetail)

        Try

            With udtFreeGraphDetail

                .bytType = 0                ''グラフタイプ
                .bytTopPos = intIndex + 1   ''先頭位置
                .shtChNo = 0                ''CH番号
                .bytIndicatorKind = 0       ''表示種別
                .shtIndicatorPattern = 0    ''表示マスク
                .bytScale = 0               ''メモリ分割数
                .bytColor = 0               ''表示色
                .strSpare = ""

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#End Region

#Region "フリーグラフ"

    Public Sub gInitSetOpsFreeGraph(ByRef udtSetOpsFreeGraph As gTypSetOpsFreeGraph)

        Try

            ''配列数初期化
            udtSetOpsFreeGraph.InitArray()

            For i As Integer = LBound(udtSetOpsFreeGraph.udtFreeGraphRec) To UBound(udtSetOpsFreeGraph.udtFreeGraphRec)

                udtSetOpsFreeGraph.udtFreeGraphRec(i).bytOpsNo = 0
                udtSetOpsFreeGraph.udtFreeGraphRec(i).bytGraphNo = 0
                udtSetOpsFreeGraph.udtFreeGraphRec(i).strGraphTitle = ""

                udtSetOpsFreeGraph.udtFreeGraphRec(i).InitArray()
                For j As Integer = 0 To UBound(udtSetOpsFreeGraph.udtFreeGraphRec(i).udtFreeDetail)

                    With udtSetOpsFreeGraph.udtFreeGraphRec(i).udtFreeDetail(j)
                        .bytType = 0                ''グラフタイプ
                        .bytTopPos = 0              ''先頭位置
                        .shtChNo = 0                ''CH番号
                        .bytIndicatorKind = 0       ''表示種別
                        .shtIndicatorPattern = 0    ''表示マスク
                        .bytScale = 0               ''メモリ分割数
                        .bytColor = 0               ''表示色
                        .strSpare = ""
                    End With

                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "フリーディスプレイ"

    Public Sub gInitSetOpsFreeDisplay(ByRef udtSetOpsFreeDisplay As gTypSetOpsFreeDisplay)

        Try

            ''配列数初期化
            udtSetOpsFreeDisplay.InitArray()

            For i As Integer = LBound(udtSetOpsFreeDisplay.udtFreeDisplayRec) To UBound(udtSetOpsFreeDisplay.udtFreeDisplayRec)

                udtSetOpsFreeDisplay.udtFreeDisplayRec(i).bytOps = 0
                udtSetOpsFreeDisplay.udtFreeDisplayRec(i).bytPage = 0
                udtSetOpsFreeDisplay.udtFreeDisplayRec(i).strPageTitle = ""

                udtSetOpsFreeDisplay.udtFreeDisplayRec(i).InitArray()
                For j As Integer = 0 To UBound(udtSetOpsFreeDisplay.udtFreeDisplayRec(i).udtFreeDisplayRecChno)

                    udtSetOpsFreeDisplay.udtFreeDisplayRec(i).udtFreeDisplayRecChno(j).shtChno = 0

                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "トレンドグラフ"

    Public Sub gInitSetOpsTrendGraph(ByRef udtSetOpsTrendGraph As gTypSetOpsTrendGraph)

        Try

            ''配列数初期化
            udtSetOpsTrendGraph.InitArray()

            For i As Integer = LBound(udtSetOpsTrendGraph.udtTrendGraphRec) To UBound(udtSetOpsTrendGraph.udtTrendGraphRec)

                udtSetOpsTrendGraph.udtTrendGraphRec(i).bytOps = 0          ''OPS番号
                udtSetOpsTrendGraph.udtTrendGraphRec(i).bytNo = 0           ''グラフ番号
                udtSetOpsTrendGraph.udtTrendGraphRec(i).bytSpare = 0        ''予備
                udtSetOpsTrendGraph.udtTrendGraphRec(i).strPageTitle = ""   ''グラフタイトル
                udtSetOpsTrendGraph.udtTrendGraphRec(i).bytSnpType = 0      ''サンプリング時間 種別
                udtSetOpsTrendGraph.udtTrendGraphRec(i).bytSnpTime = 0      ''サンプリング時間 時間値
                udtSetOpsTrendGraph.udtTrendGraphRec(i).shtTrgUse = 0       ''トリガ CH有無
                udtSetOpsTrendGraph.udtTrendGraphRec(i).shtTrgChno = 0      ''CH NO
                udtSetOpsTrendGraph.udtTrendGraphRec(i).shtTrgSelect = 0    ''トリガ 種別
                udtSetOpsTrendGraph.udtTrendGraphRec(i).shtTrgSet = 0       ''トリガ 比較条件
                udtSetOpsTrendGraph.udtTrendGraphRec(i).shtTrgValue = 0     ''トリガ 値
                udtSetOpsTrendGraph.udtTrendGraphRec(i).shtDelay = 0        ''ディレイポイント値

                udtSetOpsTrendGraph.udtTrendGraphRec(i).InitArray()
                For j As Integer = 0 To UBound(udtSetOpsTrendGraph.udtTrendGraphRec(i).udtTrendGraphRecChno)

                    udtSetOpsTrendGraph.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtChno = 0
                    udtSetOpsTrendGraph.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtMask = 0

                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ログフォーマット"

    Public Sub gInitSetOpsLogFormat(ByRef udtSetOpsLogFormat As gTypSetOpsLogFormat)

        Try

            ''配列数初期化
            udtSetOpsLogFormat.InitArray()

            For i As Integer = LBound(udtSetOpsLogFormat.strCol1) To UBound(udtSetOpsLogFormat.strCol1)

                udtSetOpsLogFormat.strCol1(i) = ""
                udtSetOpsLogFormat.strCol2(i) = ""

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region

#Region "ログフォーマット CHID"   '' ☆2012/10/26 K.Tanigawa

    Public Sub gInitSetOpsLogIdData(ByRef udtSetOpsLogIdData As gTypSetOpsLogIdData)
        ' チャンネルIDテーブル構造体
        Try

            ''配列数初期化
            udtSetOpsLogIdData.InitArray()

            For i As Integer = LBound(udtSetOpsLogIdData.shtLogChTbl) To UBound(udtSetOpsLogIdData.shtLogChTbl)

                '' udtSetOpsLogIdData.shtLogChTbl(i).shtLogIdData = 0    ''#####

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region

#Region "ログ オプション設定"    '' Ver1.9.3

    Public Sub gInitSetOpsLogOption(ByRef udtOpsLogOption As gTypLogOption)

        Try

            ''配列数初期化
            udtOpsLogOption.InitArray()

            udtOpsLogOption.bytSetting = 0      '' 設定種別

            For k As Integer = 0 To UBound(udtOpsLogOption.udtSpare)    '' 予備
                udtOpsLogOption.udtSpare(k) = 0
            Next

            For i As Integer = LBound(udtOpsLogOption.udtLogOption) To UBound(udtOpsLogOption.udtLogOption)

                With udtOpsLogOption.udtLogOption(i)
                    .InitArray()        ''配列数初期化

                    .shtCHNo = 0          '' CHNo.
                    .bytType = 0        '' ﾀｲﾌﾟ
                    .bytSpare = 0       '' 予備

                    For j As Integer = 0 To UBound(.gLogPosition)   '' 座標
                        .gLogPosition(j).shtPosX = 0
                        .gLogPosition(j).shtPosY = 0
                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region

#Region "GWS設定 CH設定データ"     '' 2014.02.04

    Public Sub gInitSetOpsGwsCh(ByRef udtSetGwsCh As gTypSetOpsGwsCh)

        Try

            ReDim udtSetGwsCh.udtGwsFileRec(gCstCntOpsGwsPort - 1)

            For i As Integer = 0 To UBound(udtSetGwsCh.udtGwsFileRec)

                Call gInitSetOpsGwsChDetail(udtSetGwsCh.udtGwsFileRec(i))

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Public Sub gInitSetOpsGwsChDetail(ByRef udtSetGwsCh As gTypSetOpsGwsFileRec)

        Try

            Call udtSetGwsCh.InitArray()

            For j As Integer = 0 To UBound(udtSetGwsCh.udtGwsChRec)

                With udtSetGwsCh.udtGwsChRec(j)

                    .shtChId = 0
                    .shtChNo = 0

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "GWS設定データ"



#End Region

#Region "負荷曲線設定"    'hori 20200323

    Public Sub gInitSetOpsLoadCurve(ByRef udtLoadCurve As gTypLoadCurve)

        Try

            udtLoadCurve.InitArray()

            ''配列数初期化
            For i As Integer = 0 To UBound(udtLoadCurve.CrvSet)
                udtLoadCurve.CrvSet(i).InitArray()
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region


#Region "CH変換テーブル"

    Public Sub gInitSetChConv(ByRef udtChConv As gTypSetChConv)

        Try

            ''配列数初期化
            udtChConv.InitArray()

            For i As Integer = LBound(udtChConv.udtChConv) To UBound(udtChConv.udtChConv)

                udtChConv.udtChConv(i).shtChid = 0

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ログ印字設定"

    Public Sub gInitSetOtherLogTime(ByRef udtLogTime As gTypSetOtherLogTime)

        Try

            ''配列数初期化
            udtLogTime.udtLogTimeRec.InitArray()

            ' ''デフォルトデータ作成用（プリンタあり）
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(0).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(0).bytTimeHH = 16
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(0).bytTimeMM = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(0).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(1).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(1).bytTimeHH = 20
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(1).bytTimeMM = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(1).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(2).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(2).bytTimeHH = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(2).bytTimeMM = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(2).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(3).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(3).bytTimeHH = 4
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(3).bytTimeMM = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(3).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(4).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(4).bytTimeHH = 8
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(4).bytTimeMM = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(4).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeReport(0).bytUse = 1
            'udtLogTime.udtLogTimeRec.udtLogTimeReport(0).bytTimeHH = 12
            'udtLogTime.udtLogTimeRec.udtLogTimeReport(0).bytTimeMM = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeReport(0).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeInterval(0).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeInterval(0).bytTimeHH = 1
            'udtLogTime.udtLogTimeRec.udtLogTimeInterval(0).bytSpare = 0

            ''デフォルトデータ作成用（プリンタなし）
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(0).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(0).bytTimeHH = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(0).bytTimeMM = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(0).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(1).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(1).bytTimeHH = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(1).bytTimeMM = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(1).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(2).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(2).bytTimeHH = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(2).bytTimeMM = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(2).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(3).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(3).bytTimeHH = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(3).bytTimeMM = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(3).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(4).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(4).bytTimeHH = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(4).bytTimeMM = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeRegular(4).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeReport(0).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeReport(0).bytTimeHH = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeReport(0).bytTimeMM = 255
            'udtLogTime.udtLogTimeRec.udtLogTimeReport(0).bytSpare = 0

            'udtLogTime.udtLogTimeRec.udtLogTimeInterval(0).bytUse = 0
            'udtLogTime.udtLogTimeRec.udtLogTimeInterval(0).bytTimeHH = 1
            'udtLogTime.udtLogTimeRec.udtLogTimeInterval(0).bytSpare = 0

            For i As Integer = LBound(udtLogTime.udtLogTimeRec.udtLogTimeRegular) To UBound(udtLogTime.udtLogTimeRec.udtLogTimeRegular)

                udtLogTime.udtLogTimeRec.udtLogTimeRegular(i).bytUse = 0
                udtLogTime.udtLogTimeRec.udtLogTimeRegular(i).bytTimeHH = 0
                udtLogTime.udtLogTimeRec.udtLogTimeRegular(i).bytTimeMM = 0
                udtLogTime.udtLogTimeRec.udtLogTimeRegular(i).bytSpare = 0

            Next

            For i As Integer = LBound(udtLogTime.udtLogTimeRec.udtLogTimeReport) To UBound(udtLogTime.udtLogTimeRec.udtLogTimeReport)

                udtLogTime.udtLogTimeRec.udtLogTimeReport(i).bytUse = 0
                udtLogTime.udtLogTimeRec.udtLogTimeReport(i).bytTimeHH = 0
                udtLogTime.udtLogTimeRec.udtLogTimeReport(i).bytTimeMM = 0
                udtLogTime.udtLogTimeRec.udtLogTimeReport(i).bytSpare = 0

            Next

            For i As Integer = LBound(udtLogTime.udtLogTimeRec.udtLogTimeInterval) To UBound(udtLogTime.udtLogTimeRec.udtLogTimeInterval)

                udtLogTime.udtLogTimeRec.udtLogTimeInterval(i).bytUse = 0
                udtLogTime.udtLogTimeRec.udtLogTimeInterval(i).bytTimeHH = 0
                udtLogTime.udtLogTimeRec.udtLogTimeInterval(i).bytSpare = 0

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ファイル更新情報"

    Public Sub gInitSetEditorUpdateInfo(ByRef udtSetEditorUpdateInfo As gTypSetEditorUpdateInfo)

        Try

            Call gInitSetEditorUpdateInfoRec(udtSetEditorUpdateInfo.udtSave)
            Call gInitSetEditorUpdateInfoRec(udtSetEditorUpdateInfo.udtCompile)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Public Sub gInitSetEditorUpdateInfoRec(ByRef udtSetEditorUpdateInfoRec As gTypSetEditorUpdateInfoRec)

        Try

            With udtSetEditorUpdateInfoRec

                .bytSystem = 1                    ''システム設定データ
                .bytFuChannel = 1                 ''FU チャンネル情報
                .bytChDisp = 1                    ''チャンネル情報データ（表示名設定
                .bytChannel = 1                   ''チャンネル情報
                .bytComposite = 1                 ''コンポジット情報
                .bytGroupM = 1                    ''グループ設定
                .bytGroupC = 1                    ''グループ設定
                .bytRepose = 1                    ''リポーズ入力設定
                .bytOutPut = 1                    ''出力チャンネル設定
                .bytOrAnd = 1                     ''論理出力設定
                .bytChRunHour = 1                 ''積算データ設定
                .bytCtrlUseNotuseM = 1            ''コントロール使用可／不可設定
                .bytCtrlUseNotuseC = 1            ''コントロール使用可／不可設定
                .bytChSio = 1                     ''SIO設定
                .bytExhGus = 1                    ''排ガス処理演算設定
                .bytExtAlarm = 1                  ''延長警報
                .bytTimer = 1                     ''タイマ設定
                .bytTimerName = 1                 ''タイマ表示名称設定
                .bytSeqSequenceID = 1             ''シーケンスID
                .bytSeqSequenceSet = 1            ''シーケンス設定
                .bytSeqLinear = 1                 'リニアライズテーブル
                .bytSeqOperationExpression = 1    '演算式テーブル
                .bytChDataSaveTable = 1           ''データ保存テーブル設定
                .bytChDataForwardTableSet = 1     ''データ転送テーブル設定
                .bytOpsScreenTitleM = 1           ''OPSスクリーンタイトル
                .bytOpsScreenTitleC = 1           ''OPSスクリーンタイトル
                .bytOpsManuMainM = 1                 ''プルダウンメニュー
                .bytOpsManuMainC = 1                 ''プルダウンメニュー
                .bytOpsGraphM = 1                 ''OPSグラフ設定
                .bytOpsGraphC = 1                 ''OPSグラフ設定
                .bytOpsFreeDisplayM = 1           ''フリーディスプレイ
                .bytOpsFreeDisplayC = 1           ''フリーディスプレイ
                .bytOpsTrendGraphM = 1            ''トレンドグラフ
                .bytOpsTrendGraphC = 1            ''トレンドグラフ
                .bytOpsTrendGraphPID = 1            ''トレンドグラフ
                .bytOpsLogFormatM = 1             ''ログフォーマット
                .bytOpsLogFormatC = 1             ''ログフォーマット
                .bytChConvNow = 1                 ''CH変換テーブル
                .bytChConvPrev = 1                ''CH変換テーブル
                .bytOpsLogIdDataM = 1             ''ログフォーマットCHID  ''☆2012/10/26 K.Tanigawa
                .bytOpsLogIdDataC = 1             ''ログフォーマットCHID  ''☆2012/10/26 K.Tanigawa
                .bytOpsLogOption = 1              '' ﾛｸﾞｵﾌﾟｼｮﾝ設定  Ver1.9.3 2016.01.25
                .strSpare = ""                    ''予備（全フラグ + 予備 = 100 byte）

                ''配列数初期化
                .InitArray()

                ''SIO通信チャンネル設定1～16
                For i As Integer = 0 To UBound(.bytChSioCh)
                    .bytChSioCh(i) = 1
                Next

                'Ver2.0.6.1
                'SIO拡張
                For i As Integer = 0 To UBound(.bytChSioExt)
                    .bytChSioExt(i) = 1
                Next i

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Module
