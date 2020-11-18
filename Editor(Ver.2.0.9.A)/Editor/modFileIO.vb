Module modFileIO

    '--------------------------------------------------------------------
    ' 機能      : ヘッダーレコード作成
    ' 返り値    : なし
    ' 引き数    : ARG1 - (O ) ヘッダーレコード構造体
    ' 　　　    : ARG2 - ( I) ファイルバージョン
    ' 　　　    : ARG3 - ( I) レコード数
    ' 機能説明  : ヘッダーレコードを作成する
    '--------------------------------------------------------------------
    Public Sub gMakeHeader(ByRef udtHeader As gTypSetHeader, _
                           ByVal strFileVersion As String, _
                           ByVal shtRecs As UShort, _
                  Optional ByVal shtSize1 As UShort = 0, _
                  Optional ByVal shtSize2 As UShort = 0, _
                  Optional ByVal shtSize3 As UShort = 0, _
                  Optional ByVal shtSize4 As UShort = 0, _
                  Optional ByVal shtSize5 As UShort = 0)

        Try

            With udtHeader

                .strVersion = Microsoft.VisualBasic.Right("00000000" & strFileVersion, 8)
                .strDate = Now.ToString("yyyyMMdd")
                .strTime = Now.ToString("HHmm")
                .shtRecs = shtRecs
                .shtSize1 = shtSize1
                .shtSize2 = shtSize2
                .shtSize3 = shtSize3
                .shtSize4 = shtSize4
                .shtSize5 = shtSize5

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル設定レコード数取得
    ' 返り値    : チャンネル設定レコード数
    ' 引き数    : ARG1 - (I ) チャンネル設定構造体
    ' 機能説明  : チャンネル設定レコード数を取得する
    '--------------------------------------------------------------------
    Public Function gGetRecCntChannel(ByVal udtSetChannel As gTypSetChInfo) As Integer

        Try

            Dim intCnt As Integer = 0

            ''CH数ではなく、レコード数を保存　ver.1.4.0 2011.09.27
            'For i As Integer = 0 To UBound(udtSetChannel.udtChannel)

            '    If udtSetChannel.udtChannel(i).udtChCommon.shtChid <> 0 Or _
            '       udtSetChannel.udtChannel(i).udtChCommon.shtChno <> 0 Then
            '        intCnt += 1
            '    End If

            'Next

            ''チャンネル設定構造体を下から上へ検索し、最後にチャンネルが設定されている位置を保存する
            For i As Integer = UBound(udtSetChannel.udtChannel) To 0 Step -1
                With udtSetChannel.udtChannel(i).udtChCommon
                    If .shtChid <> 0 And .shtChno <> 0 And .shtChType <> gCstCodeChTypeNothing Then
                        intCnt = i + 1
                        Exit For
                    End If
                End With
            Next

            Return intCnt

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : コンポジット設定レコード数取得
    ' 返り値    : コンポジット設定レコード数
    ' 引き数    : ARG1 - (I ) コンポジット設定構造体
    ' 機能説明  : コンポジット設定レコード数を取得する
    '--------------------------------------------------------------------
    Public Function gGetRecCntComposite(ByVal udtComposite As gTypSetChComposite) As Integer

        Try

            Dim intCnt As Integer = 0

            For i As Integer = 0 To UBound(udtComposite.udtComposite)

                If udtComposite.udtComposite(i).shtChid <> 0 Then

                    intCnt += 1

                End If

            Next

            Return intCnt

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#Region "ID - NO 変換関連"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（出力チャンネル設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) 出力チャンネル設定構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelChOutput(ByRef udtSet As gTypSetChOutput, _
                                    ByRef intErrCnt As Integer, _
                                    ByRef strMsg() As String, _
                                    ByVal udtConvMode As gEnmConvMode, _
                                    ByVal blnEnglish As Boolean)

        Try

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtCHOutPut)

                With udtSet.udtCHOutPut(i)

                    ''TypeがCHデータの時のみ処理を行う
                    ''（CHデータ以外（論理出力or又はand）の時は、CH NOではなく論理出力IDが入っているため）
                    If .bytType = gCstCodeFuOutputChTypeCh Then

                        '=======================
                        ''チャンネル変換
                        '=======================
                        Call mConvChannelOne(.shtChid, .shtSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（グループリポーズ設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) グループリポーズ設定構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelChGroupRepose(ByRef udtSet As gTypSetChGroupRepose, _
                                         ByRef intErrCnt As Integer, _
                                         ByRef strMsg() As String, _
                                         ByVal udtConvMode As gEnmConvMode, _
                                         ByVal blnEnglish As Boolean)
        Try

            Dim intDmy As Integer

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtRepose)

                With udtSet.udtRepose(i)

                    '=======================
                    ''チャンネル変換
                    '=======================
                    Call mConvChannelOne(.shtChId, intDmy, intErrCnt, strMsg, udtConvMode, blnEnglish)

                    For j As Integer = 0 To UBound(.udtReposeInf)

                        With .udtReposeInf(j)

                            '=======================
                            ''チャンネル変換
                            '=======================
                            Call mConvChannelOne(.shtChId, intDmy, intErrCnt, strMsg, udtConvMode, blnEnglish)

                        End With

                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（論理出力設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) 論理出力設定構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelChAndOr(ByRef udtSet As gTypSetChAndOr, _
                                   ByRef intErrCnt As Integer, _
                                   ByRef strMsg() As String, _
                                   ByVal udtConvMode As gEnmConvMode, _
                                   ByVal blnEnglish As Boolean)

        Try

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtCHOut)

                For j As Integer = 0 To UBound(udtSet.udtCHOut(i).udtCHAndOr)

                    With udtSet.udtCHOut(i).udtCHAndOr(j)

                        '=======================
                        ''チャンネル変換
                        '=======================
                        Call mConvChannelOne(.shtChid, .shtSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)

                    End With

                Next

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（SIO設定CH設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) SIO設定CH設定構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelChSIOCh(ByRef udtSet As gTypSetChSioCh, _
                                   ByRef intErrCnt As Integer, _
                                   ByRef strMsg() As String, _
                                   ByVal udtConvMode As gEnmConvMode, _
                                   ByVal blnEnglish As Boolean)

        Try

            Dim intDmy As Integer
            Dim intChIdNo As Integer

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtSioChRec)

                With udtSet.udtSioChRec(i)

                    ''区切り文字 2014.01.14
                    If .shtChId = &HFFFE Or .shtChNo = 0 Then   '' 処理無し

                        ''チャンネルが設定されている時のみ処理する
                    ElseIf .shtChId <> 0 Or .shtChNo <> 0 Then

                        '=======================
                        ''チャンネル変換
                        '=======================
                        Select Case udtConvMode
                            Case gEnmConvMode.cmID_NO : intChIdNo = .shtChId
                            Case gEnmConvMode.cmNO_ID : intChIdNo = .shtChNo
                        End Select

                        Call mConvChannelOne(intChIdNo, intDmy, intErrCnt, strMsg, udtConvMode, blnEnglish)

                        Select Case udtConvMode
                            Case gEnmConvMode.cmID_NO : .shtChNo = intChIdNo
                            Case gEnmConvMode.cmNO_ID : .shtChId = intChIdNo
                        End Select

                    Else
                        'Exit For
                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（GWS設定CH設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) SIO設定CH設定構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelChGwsCh(ByRef udtSet As gTypSetOpsGwsCh, _
                                   ByRef intErrCnt As Integer, _
                                   ByRef strMsg() As String, _
                                   ByVal udtConvMode As gEnmConvMode, _
                                   ByVal blnEnglish As Boolean)

        Try

            Dim intDmy As Integer
            Dim intChIdNo As Integer

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtGwsFileRec)
                For j As Integer = 0 To UBound(udtSet.udtGwsFileRec(i).udtGwsChRec)

                    With udtSet.udtGwsFileRec(i).udtGwsChRec(j)

                        ''チャンネルが設定されている時のみ処理する
                        If .shtChId <> 0 Or .shtChNo <> 0 Then

                            '=======================
                            ''チャンネル変換
                            '=======================
                            Select Case udtConvMode
                                Case gEnmConvMode.cmID_NO : intChIdNo = .shtChId
                                Case gEnmConvMode.cmNO_ID : intChIdNo = .shtChNo
                            End Select

                            Call mConvChannelOne(intChIdNo, intDmy, intErrCnt, strMsg, udtConvMode, blnEnglish)

                            Select Case udtConvMode
                                Case gEnmConvMode.cmID_NO : .shtChNo = intChIdNo
                                Case gEnmConvMode.cmNO_ID : .shtChId = intChIdNo
                            End Select

                        Else
                            'Exit For
                        End If

                    End With

                Next
            Next



        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（PID用ﾄﾚﾝﾄﾞ）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) PID用ﾄﾚﾝﾄﾞ構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelChTrendPID(ByRef udtSet As gTypSetOpsTrendGraph, _
                                   ByRef intErrCnt As Integer, _
                                   ByRef strMsg() As String, _
                                   ByVal udtConvMode As gEnmConvMode, _
                                   ByVal blnEnglish As Boolean)

        Try

            Dim intDmy As Integer
            Dim intChIdNo As Integer

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtTrendGraphRec)
                For j As Integer = 0 To UBound(udtSet.udtTrendGraphRec(i).udtTrendGraphRecChno)

                    With udtSet.udtTrendGraphRec(i).udtTrendGraphRecChno(j)

                        ''チャンネルが設定されている時のみ処理する
                        If .shtChno <> 0 Then

                            '=======================
                            ''チャンネル変換
                            '=======================
                            intChIdNo = .shtChno

                            Call mConvChannelOne(intChIdNo, intDmy, intErrCnt, strMsg, udtConvMode, blnEnglish)

                            .shtChno = intChIdNo
                        Else
                            'Exit For
                        End If

                    End With

                Next
            Next



        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（運転積算チャンネルトリガ設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) チャンネルデータ設定構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelChRevoTrriger(ByRef udtSet As gTypSetChInfo, _
                                         ByRef intErrCnt As Integer, _
                                         ByRef strMsg() As String, _
                                         ByVal udtConvMode As gEnmConvMode, _
                                         ByVal blnEnglish As Boolean)

        Try

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtChannel)

                With udtSet.udtChannel(i)

                    ''パルス積算CHの場合
                    If .udtChCommon.shtChType = gCstCodeChTypePulse Then

                        ''データ種別が運転積算の場合
                        '' Ver1.11.8.3 2016.11.08 通信CH追加
                        'If .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalHour _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalMin _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayHour _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayMin _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapHour _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapMin _
                        'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDev Then
                        '' Ver1.12.0.1 2017.01.13 関数に変更
                        If gChkRunHourCH(.udtChCommon._shtChno) Then
                            '=======================
                            ''トリガチャンネル変換
                            '=======================
                            Call mConvChannelOne(.RevoTrigerChid, .RevoTrigerSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)

                        End If

                    End If


                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（積算データ設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) 積算データ設定構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelChRunHour(ByRef udtSet As gTypSetChRunHour, _
                                     ByRef intErrCnt As Integer, _
                                     ByRef strMsg() As String, _
                                     ByVal udtConvMode As gEnmConvMode, _
                                     ByVal blnEnglish As Boolean)

        Try

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    '=======================
                    ''計測チャンネル変換
                    '=======================
                    Call mConvChannelOne(.shtChid, .shtSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)

                    '=======================
                    ''トリガチャンネル変換
                    '=======================
                    Call mConvChannelOne(.shtTrgChid, .shtTrgSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（シーケンス設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) シーケンス設定構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelSeqSequence(ByRef udtSet As gTypSetSeqSet, _
                                       ByRef intErrCnt As Integer, _
                                       ByRef strMsg() As String, _
                                       ByVal udtConvMode As gEnmConvMode, _
                                       ByVal blnEnglish As Boolean)

        Try

            Dim intDmy As Integer = 0
            Dim udtSequenceLogicSub() As gTypCodeName = Nothing

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    If .shtLogicType <> 0 Then

                        ''シーケンスロジックサブ設定取得
                        Call gGetComboCodeName(udtSequenceLogicSub, gEnmComboType.ctSeqSetDetailLogic, Format(.shtLogicType, "00"))

                        For j As Integer = 0 To UBound(udtSequenceLogicSub)

                            ''１番目の項目が１の場合（=CH Noの指定）'' 条件追加 Ver.1.4.7 K.Tanigawa
                            If (udtSequenceLogicSub(j).shtCode = gCstCodeSeqLogicSubDataTypeChNo) Or ((.shtLogicType = 26 And .shtUseCh(j) = 1) _
                                                                 Or (.shtLogicType = 27 And .shtUseCh(j) = 1)) Then

                                '=======================
                                ''チャンネル変換
                                '=======================
                                Call mConvChannelOne(.shtLogicItem(j), intDmy, intErrCnt, strMsg, udtConvMode, blnEnglish)

                            End If

                        Next



                    End If

                    '=======================
                    ''出力チャンネル変換
                    '=======================
                    Call mConvChannelOne(.shtOutChid, .shtOutSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)

                    '=======================
                    ''入力チャンネル変換
                    '=======================
                    For j As Integer = 0 To UBound(.udtInput)
                        With .udtInput(j)
                            If ((.shtChSelect <> gCstCodeSeqChSelectCalc) And (.shtChSelect <> gCstCodeSeqChSelectFixed) And (.shtChSelect <> gCstCodeSeqChSelectManual)) Then  '' 定数6 追加　ver1.4.4 2012.05.07
                                Call mConvChannelOne(.shtChid, .shtSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)
                            ElseIf ((.shtChSelect = gCstCodeSeqChSelectManual) And (.bytStatus < 49 Or .bytStatus > 54)) Then '' 定数5 0x31～0x36は無効 Ver1.4.6 2012.08.09 K.Tanigawa
                                Call mConvChannelOne(.shtChid, .shtSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)
                            End If
                        End With
                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（排ガス演算）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) 排ガス演算設定構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelExhGus(ByRef udtSet As gTypSetChExhGus, _
                                  ByRef intErrCnt As Integer, _
                                  ByRef strMsg() As String, _
                                  ByVal udtConvMode As gEnmConvMode, _
                                  ByVal blnEnglish As Boolean)

        Try

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtExhGusRec)

                With udtSet.udtExhGusRec(i)

                    '=======================
                    ''平均チャンネル変換
                    '=======================
                    Call mConvChannelOne(.shtAveChid, .shtAveSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)

                    '=======================
                    ''リポーズチャンネル変換
                    '=======================
                    Call mConvChannelOne(.shtRepChid, .shtRepSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)

                    '=======================
                    ''シリンダチャンネル変換
                    '=======================
                    For j As Integer = 0 To UBound(.udtExhGusCyl)
                        With .udtExhGusCyl(j)
                            Call mConvChannelOne(.shtChid, .shtSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)
                        End With
                    Next

                    '=======================
                    ''偏差チャンネル変換
                    '=======================
                    For j As Integer = 0 To UBound(.udtExhGusDev)
                        With .udtExhGusDev(j)
                            Call mConvChannelOne(.shtChid, .shtSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)
                        End With
                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（データ保存テーブル設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) データ保存テーブル設定構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelDataSaveTable(ByRef udtSet As gTypSetChDataSave, _
                                         ByRef intErrCnt As Integer, _
                                         ByRef strMsg() As String, _
                                         ByVal udtConvMode As gEnmConvMode, _
                                         ByVal blnEnglish As Boolean)

        Try

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtDetail)

                With udtSet.udtDetail(i)

                    '=======================
                    ''チャンネル変換
                    '=======================
                    Call mConvChannelOne(.shtChid, .shtSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（コンポジット設定）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) コンポジット設定構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelComposite(ByRef udtSet As gTypSetChComposite, _
                                     ByRef intErrCnt As Integer, _
                                     ByRef strMsg() As String, _
                                     ByVal udtConvMode As gEnmConvMode, _
                                     ByVal blnEnglish As Boolean)

        Try

            Dim intDmy As Integer

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtComposite)

                With udtSet.udtComposite(i)

                    Try
                        '=======================
                        ''チャンネル変換
                        '=======================
                        Call mConvChannelOne(.shtChid, intDmy, intErrCnt, strMsg, udtConvMode, blnEnglish)

                    Catch ex As Exception
                        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
                    End Try

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（演算式テーブル）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) 演算式テーブル構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelOpeExp(ByRef udtSet As gTypSetSeqOperationExpression, _
                                  ByRef intErrCnt As Integer, _
                                  ByRef strMsg() As String, _
                                  ByVal udtConvMode As gEnmConvMode, _
                                  ByVal blnEnglish As Boolean)

        Try

            Dim shtChid As UShort
            Dim shtSysno As UShort
            Dim bytAryWK() As Byte = Nothing

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtTables)

                With udtSet.udtTables(i)

                    For j As Integer = 0 To UBound(.udtAryInf)

                        With .udtAryInf(j)

                            If .shtType = gCstCodeSeqFixTypeChData _
                            Or .shtType = gCstCodeSeqFixTypeLowSet _
                            Or .shtType = gCstCodeSeqFixTypeHighSet _
                            Or .shtType = gCstCodeSeqFixTypeLLSet _
                            Or .shtType = gCstCodeSeqFixTypeHHSet Then

                                '=======================
                                ''チャンネル変換
                                '=======================
                                ''バイト配列から設定されているCH NOを取得
                                shtChid = gConnect2Byte(.bytInfo(2), .bytInfo(3))
                                'shtChid = BitConverter.ToInt16(.bytInfo, 2)

                                ''チャンネル変換
                                Call mConvChannelOne(shtChid, shtSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)

                                ''SYSTEM NO をバイト配列に設定
                                Erase bytAryWK
                                bytAryWK = BitConverter.GetBytes(shtSysno)
                                Call gCopyByteArray(bytAryWK, .bytInfo, 0)

                                ''CH ID をバイト配列に設定
                                Erase bytAryWK
                                bytAryWK = BitConverter.GetBytes(shtChid)
                                Call gCopyByteArray(bytAryWK, .bytInfo, 2)

                            End If

                        End With

                    Next

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（表示名設定データ）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) チャンネル情報データ(表示名設定データ)構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    '--------------------------------------------------------------------
    Public Sub gConvChannelChDisp(ByRef udtSet As gTypSetChDisp, _
                                  ByRef intErrCnt As Integer, _
                                  ByRef strMsg() As String, _
                                  ByVal udtConvMode As gEnmConvMode, _
                                  ByVal blnEnglish As Boolean)

        Try

            Dim intDmy As Integer

            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing

            For i As Integer = 0 To UBound(udtSet.udtChDisp)

                For ii As Integer = 0 To UBound(udtSet.udtChDisp(i).udtSlotInfo)

                    For j As Integer = 0 To UBound(udtSet.udtChDisp(i).udtSlotInfo(ii).udtPinInfo)

                        With udtSet.udtChDisp(i).udtSlotInfo(ii).udtPinInfo(j)

                            Try
                                '=======================
                                ''チャンネル変換
                                '=======================
                                Call mConvChannelOne(.shtChid, intDmy, intErrCnt, strMsg, udtConvMode, blnEnglish)

                            Catch ex As Exception
                                Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
                            End Try

                        End With

                    Next j

                Next ii

            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換（Log formatCHIDテーブル）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) Log formatテーブル構造体
    ' 機能説明  : CH NO → ID 変換と SYSTEM NO の設定を行う
    ' ☆ 2012.10.26 K.Tanigawa
    '--------------------------------------------------------------------
    Public Sub gConvChannelLogIDChg(ByRef udtSetOpsLogFormat As gTypSetOpsLogFormat, _
                                    ByRef udtSetOpsLogIdData As gTypSetOpsLogIdData, _
                                  ByRef intErrCnt As Integer, _
                                  ByRef strMsg() As String, _
                                  ByVal udtConvMode As gEnmConvMode, _
                                  ByVal blnEnglish As Boolean)


        ' 内容は未作成 －－－－－－－－－－－－－－－－－－－－


        Try

            Dim strErrMsg() As String = Nothing
            Dim strChNo As String
            Dim shtChid As Integer
            Dim shtChType As Integer
            Dim shtSysno As Short      '' SYSTEM No.
            Dim SetCode As Short       '' コード変換後のデータ
            Dim SetPage As Short       '' Log Page/配列No.
            Dim SetIDCnt As Short      '' Page単位の順番
            Dim LogText As String       '' 比較するLogコード
            Dim test As Short

            'T.Ueki
            Dim PageFlg As Boolean      ''ANALOG設定の場合：True、COUNTER TITLE設定の場合：False
            Dim CntFlg As Boolean       ''COUNT TITLE検出後True、それ以外はFalse
            Dim NumCommand As Short     ''コマンド数を入れる変数 hori


            ''OUT引数初期化
            intErrCnt = 0
            strMsg = Nothing
            strChNo = ""
            shtChid = 0
            shtChType = 0
            shtSysno = 0
            SetPage = 0
            SetIDCnt = 1

            'T.Ueki
            PageFlg = False
            CntFlg = False

            For i As Integer = 0 To (UBound(udtSetOpsLogFormat.strCol1) + UBound(udtSetOpsLogFormat.strCol2)) / 2

                For pos As Integer = 0 To 1

                    LogText = ""
                    If pos = 0 Then  ' 左列
                        LogText = udtSetOpsLogFormat.strCol1(i)
                    Else             ' 右列
                        LogText = udtSetOpsLogFormat.strCol2(i)
                    End If


                    ''CH番号設定の場合
                    If Mid(Trim(LogText), 1, 2) = "CH" Then

                        ''CH番号取得
                        strChNo = Mid(Trim(LogText), 3, Len(Trim(LogText)))
                        ''CH番号がCHリストに存在する場合
                        If gExistChIDNo(strChNo, shtChType, shtChid) Then

                            '=============================
                            ''チャンネル変換/CHIDチェック
                            '=============================
                            Call mConvChannelOne(strChNo, shtSysno, intErrCnt, strMsg, udtConvMode, blnEnglish)

                            SetCode = shtChid       '' CHIDの取得

                        End If

                    Else

                        ''COUNTER TITLE設定の場合
                        If Mid(Trim(LogText), 1, 3) = "CNT" Then
                            SetCode = -255   '' &HFF01

                            ''SMS-50総合試験　No.60 修正箇所（コード削除）T.Ueki
                            PageFlg = False

                        End If

                        ''CH番号設定の場合
                        If Mid(Trim(LogText), 1, 3) = "ANA" Then
                            SetCode = -254    '' &HFF02

                            ''SMS-50総合試験　No.60 修正箇所（コード削除）T.Ueki
                            PageFlg = True

                        End If

                        ''GR番号設定の場合
                        If Mid(Trim(LogText), 1, 2) = "GR" Then
                            SetCode = -512 + Mid(Trim(LogText), 3, Len(Trim(LogText)))    ''&FE00 + GR No.
                        End If

                        ''SPACE設定の場合
                        If Mid(Trim(LogText), 1, 2) = "SP" Then
                            SetCode = -1     '' &HFFFF
                        End If

                        ''CH番号設定の場合
                        If Mid(Trim(LogText), 1, 4) = "DATA" Then
                            '' Machinery/Cargo 切り替え
                            SetCode = -16   '' &HFFF0   2014.07.08 修正 -15(&HFFF1) → -16(&HFFF0)
                        End If

                        ''Page設定の場合
                        If Mid(Trim(LogText), 1, 4) = "PAGE" Then
                            '' Page切り換え
                            SetCode = -2    '' &HFFFE
                        End If

                        '' ""設定の場合
                        If Mid(Trim(LogText), 1, 4) = "" Or Mid(Trim(LogText), 1, 8) = "        " Then
                            '' 何もしない
                            SetCode = 0         '&H0000
                        End If

                    End If

                    If SetCode <> &H0 Then
                        ' 構造体へのコートの詰め込み

                        test = CShort(SetCode)
                        udtSetOpsLogIdData.shtLogChTbl(SetPage * 600 + SetIDCnt) = test
                        SetIDCnt += 1

                        If SetCode = -254 Then      '' &HFF02 "ANATITLE"

                            udtSetOpsLogIdData.shtLogChTbl(SetPage * 600 + SetIDCnt) = SetCode
                            SetIDCnt += 1
                            '' 左辺有りで、右辺自動セット
                        End If

                        If SetCode = -2 Or SetCode = -16 Then      '' &HFFFE "PAGE" or &HFFF0 "DATA"    2014.07.08

                            ''SMS-50総合試験　No.60 修正箇所（コード削除）T.Ueki
                            If PageFlg = True Then
                                udtSetOpsLogIdData.shtLogChTbl(SetPage * 600 + SetIDCnt) = SetCode
                                SetIDCnt += 1
                                '' udtSetOpsLogIdData.shtLogChTbl(SetPage * 600) = SetCode
                            End If

                            ''SMS-50総合試験　No.60 修正箇所「1ページ目のコマンド数が0になっている」IF文内から外へ移設　T.Ueki
                            udtSetOpsLogIdData.shtLogChTbl(SetPage * 600) = SetIDCnt - 1
                            '' 次ページ先頭
                            SetPage += 1
                            '' Ver1.12.0.8  2017.02.22 MAXﾍﾟｰｼﾞ追加
                            If SetPage >= gCstLogMaxPage Then
                                Exit For
                            End If
                            ''//

                            SetIDCnt = 1

                        End If

                        SetCode = &H0   'CLR
                    End If


                Next
                '' Ver1.12.0.8  2017.02.22 MAXﾍﾟｰｼﾞ追加
                If SetPage >= gCstLogMaxPage Then
                    Exit For
                End If
                ''//
            Next

            '' Ver1.12.0.8  2017.02.22 MAXﾍﾟｰｼﾞ追加
            If SetPage >= gCstLogMaxPage Then
                SetPage = 9
            End If
            ''//


            '' 最終ページのID数
            If SetCode <> -2 Then
                udtSetOpsLogIdData.shtLogChTbl(SetPage * 600) = SetIDCnt - 1

                If udtSetOpsLogIdData.shtLogChTbl(SetPage * 600 + (SetIDCnt - 1)) = -2 Then  '' &HFFFE "PAGE"
                    udtSetOpsLogIdData.shtLogChTbl(SetPage * 600) = SetIDCnt - 1
                End If
            Else

            End If

            ''SetPage:上記のFor文内で加算処理が済んだため固定値
            For i As Integer = 0 To SetPage
                ''コマンドの先頭がCOUNTER TITLE だった場合
                If udtSetOpsLogIdData.shtLogChTbl((i * 600) + 1) = -255 Then
                    CntFlg = True
                End If
                ''コマンドの先頭がANALOG TITLE
                If udtSetOpsLogIdData.shtLogChTbl((i * 600) + 1) = -254 And CntFlg = True Then
                    ''一つ前のページ(COUNTER TITLE)のコマンド数を代入
                    NumCommand = udtSetOpsLogIdData.shtLogChTbl((i - 1) * 600)
                    ''最後のコマンドがPAGEだったときのみ
                    If udtSetOpsLogIdData.shtLogChTbl((i - 1) * 600 + NumCommand) = -2 Then
                        ''COUNTER TITLEの最後のコマンド(:PAGE)の直後にSPACEを入れる
                        udtSetOpsLogIdData.shtLogChTbl((i - 1) * 600 + (NumCommand + 1)) = -1
                        udtSetOpsLogIdData.shtLogChTbl((i - 1) * 600 + (NumCommand + 2)) = -1
                        ''COUNTER TITLEのコマンド数に+2
                        udtSetOpsLogIdData.shtLogChTbl((i - 1) * 600) += 2
                        ''一度処理した後フラグをオフにし、処理を繰り返さない
                        CntFlg = False
                        Exit For
                    End If
                End If
            Next

            ''結果表示
            If intErrCnt = 0 Then

            Else

                ''失敗時

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



    '--------------------------------------------------------------------
    ' 機能      : チャンネルID-No変換
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) CHID 又は CHNO
    ' 　　　    : ARG2 - ( O) システムNo
    ' 　　　    : ARG3 - ( O) エラーカウント
    ' 　　　    : ARG4 - ( O) メッセージ
    ' 　　　    : ARG5 - ( O) 変換モード
    ' 　　　    : ARG6 - ( O) 英語フラグ
    ' 機能説明  : チャンネルID-Noの変換を行う
    '--------------------------------------------------------------------
    Private Sub mConvChannelOne(ByRef intChIdNo As Integer, _
                                ByRef intSysno As Integer, _
                                ByRef intErrCnt As Integer, _
                                ByRef strMsg() As String, _
                                ByRef udtConvMode As gEnmConvMode, _
                                ByVal blnEnglish As Boolean)

        Try

            Dim intwk As Integer

            '=======================
            ''出力チャンネル変換
            '=======================
            Select Case udtConvMode
                Case gEnmConvMode.cmNO_ID : intwk = gConvChNoToChId(intChIdNo)
                Case gEnmConvMode.cmID_NO : intwk = gConvChIdToChNo(intChIdNo)
            End Select

            Select Case intwk
                Case 0

                    '===========
                    ''変換なし
                    '===========
                    ''CH ID or NO と SYSTEM NO に 0 をセット
                    intChIdNo = 0
                    intSysno = 0

                Case -1

                    '===========
                    ''変換失敗
                    '===========
                    ''変換出来なかった場合はエラーメッセージ
                    ReDim Preserve strMsg(intErrCnt)
                    Select Case udtConvMode
                        Case gEnmConvMode.cmNO_ID : strMsg(intErrCnt) = IIf(blnEnglish, "CH NO [" & intChIdNo & "] doesn't exist.", "チャンネル番号 [" & intChIdNo & "] は存在しません。")
                        Case gEnmConvMode.cmID_NO : strMsg(intErrCnt) = IIf(blnEnglish, "CH ID [" & intChIdNo & "] doesn't exist.", "チャンネル番号 [" & intChIdNo & "] は存在しません。")
                    End Select

                    ''CH ID or NO と SYSTEM NO に 0 をセット
                    intChIdNo = 0
                    intSysno = 0

                    intErrCnt += 1

                Case Else

                    '===========
                    ''変換成功
                    '===========
                    ''CH ID or NO と SYSTEM NO をセット
                    intChIdNo = intwk

                    Select Case udtConvMode
                        Case gEnmConvMode.cmNO_ID : intSysno = gConvChIdToSysNo(intwk)
                        Case gEnmConvMode.cmID_NO : intSysno = gConvChNoToSysNo(intwk)
                    End Select

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#End Region


End Module






