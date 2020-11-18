Public Class frmClear

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

    Private Sub frmClear_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click



        If MsgBox("Start Setting Clear?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then

            If chkSIO1.Checked = True Then       ' SIOﾎﾟｰﾄ1 ｸﾘｱ
                Call ClearSIOSetting(1)
            End If

            If chkSIO2.Checked = True Then       ' SIOﾎﾟｰﾄ2 ｸﾘｱ
                Call ClearSIOSetting(2)
            End If

            If chkSIO3.Checked = True Then       ' SIOﾎﾟｰﾄ3 ｸﾘｱ
                Call ClearSIOSetting(3)
            End If

            If chkSIO4.Checked = True Then       ' SIOﾎﾟｰﾄ4 ｸﾘｱ
                Call ClearSIOSetting(4)
            End If

            If chkSIO5.Checked = True Then       ' SIOﾎﾟｰﾄ5 ｸﾘｱ
                Call ClearSIOSetting(5)
            End If

            If chkSIO6.Checked = True Then       ' SIOﾎﾟｰﾄ6 ｸﾘｱ
                Call ClearSIOSetting(6)
            End If

            If chkSIO7.Checked = True Then       ' SIOﾎﾟｰﾄ7 ｸﾘｱ
                Call ClearSIOSetting(7)
            End If

            If chkSIO8.Checked = True Then       ' SIOﾎﾟｰﾄ8 ｸﾘｱ
                Call ClearSIOSetting(8)
            End If

            If chkSIO9.Checked = True Then       ' SIOﾎﾟｰﾄ9 ｸﾘｱ
                Call ClearSIOSetting(9)
            End If

            If chkSequence.Checked = True Then  ' ｼｰｹﾝｽ設定
                Call ClearSeqSetting()
            End If

            If chkCHOr.Checked = True Then      ' CH OR/AND設定
                Call ClearCHORhSetting()
                gudt.SetEditorUpdateInfo.udtSave.bytOrAnd = 1
            End If

            If chkLog.Checked = True Then       ' ﾛｸﾞ設定削除
                Call gInitSetOpsLogFormat(gudt.SetOpsLogFormatM)
                gudt.SetEditorUpdateInfo.udtSave.bytOpsLogFormatM = 1
                gudt.SetEditorUpdateInfo.udtSave.bytOpsLogIdDataM = 1

                Call gInitSetOpsLogFormat(gudt.SetOpsLogFormatC)
                gudt.SetEditorUpdateInfo.udtSave.bytOpsLogFormatC = 1
                gudt.SetEditorUpdateInfo.udtSave.bytOpsLogIdDataC = 1
            End If

            If chkGraph.Checked = True Then     ' ｸﾞﾗﾌ設定削除
                Call ClearGraphSetting(gudt.SetOpsGraphM)
                gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphM = 1
                Call ClearGraphSetting(gudt.SetOpsGraphC)
                gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphC = 1
            End If

            ' CH変換削除
            Call ClearCHConvertSetting(gudt.SetChConvPrev)
            gudt.SetEditorUpdateInfo.udtSave.bytChConvPrev = 1
            Call ClearCHConvertSetting(gudt.SetChConvNow)
            gudt.SetEditorUpdateInfo.udtSave.bytChConvNow = 1

            gblnUpdateAll = True

            Call Me.Close()

        End If

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 通信設定ｸﾘｱ
    ' 返り値    : なし
    ' 引き数    : ﾎﾟｰﾄ番号
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub ClearSIOSetting(intPort As Integer)

        gudt.SetChSio.shtNum(intPort - 1) = 0        ' 送信CH数0
        gInitSetChSioVdr(gudt.SetChSio.udtVdr(intPort - 1)) ' 通信設定ｸﾘｱ

        ' 送信ﾘｽﾄ　ｸﾘｱ
        With gudt.SetChSioCh(intPort - 1)
            For i As Integer = 0 To UBound(.udtSioChRec)

                .udtSioChRec(i).shtChId = 0
                .udtSioChRec(i).shtChNo = 0
            Next
        End With

        gudt.SetEditorUpdateInfo.udtSave.bytChSioCh(intPort - 1) = 1
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ｼｰｹﾝｽ設定ｸﾘｱ
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub ClearSeqSetting()
        Dim i As Integer
        Dim j As Integer

        For i = LBound(gudt.SetSeqSet.udtDetail) To UBound(gudt.SetSeqSet.udtDetail)
            With gudt.SetSeqSet.udtDetail(i)
                If .shtLogicType <> 0 Then      ' ﾛｼﾞｯｸが設定されていれば削除
                    .shtId = 0
                    .shtLogicType = 0

                    '' 入力ﾁｪｯｸ
                    For j = 0 To UBound(.udtInput)
                        With .udtInput(j)

                            .shtSysno = 0
                            .shtChid = 0            ''CHID
                            .shtChSelect = 0        ''CH選択
                            .shtIoSelect = 0        ''入出力区分
                            .bytStatus = 0          ''参照ステータス
                            .bytType = 0            ''タイプ
                            .shtMask = 0
                            .shtAnalogType = 0
                            .strSpare = 0
                        End With
                    Next

                    .strRemarks = ""

                    For j = LBound(.shtUseCh) To UBound(.shtUseCh)
                        .shtUseCh(j) = 0
                    Next

                    ' 出力設定
                    .shtOutSysno = 0
                    .shtOutChid = 0
                    .shtOutData = 0         ''出力データ
                    .shtOutDelay = 0        ''出力オフディレイ
                    .bytOutStatus = 0       ''出力ステータス
                    .bytOutIoSelect = 0     ''入出力区分
                    .bytOutDataType = 0     ''出力データタイプ
                    .bytOutInv = 0          ''出力反転
                    .bytFuno = 0            ''FU　番号
                    .bytPort = 0            ''FU ポート番号
                    .bytPin = 0             ''FU　計測点位置
                    .bytPinNo = 0           ''FU　計測点個数
                    .bytOutType = 0         ''出力タイプ
                    .bytOneShot = 0         ''出力ワンショット時間
                    .bytContine = 0         ''処理継続中止
                    .bytSpare1 = 0          ''備考
                End If

            End With
        Next

        '' ｼｰｹﾝｽID
        For i = LBound(gudt.SetSeqID.shtID) To UBound(gudt.SetSeqID.shtID)
            gudt.SetSeqID.shtID(i) = 0
        Next

        gudt.SetEditorUpdateInfo.udtSave.bytSeqSequenceSet = 1
        gudt.SetEditorUpdateInfo.udtSave.bytSeqSequenceID = 1

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ｸﾞﾗﾌ設定ｸﾘｱ
    ' 返り値    : なし
    ' 引き数    : ﾎﾟｰﾄ番号
    ' 機能説明  : 
    ' 備考      : 
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
    ' 機能      : CH OR設定ｸﾘｱ
    ' 返り値    : なし
    ' 引き数    : 
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub ClearCHORhSetting()

        For i As Integer = 0 To UBound(gudt.SetChAndOr.udtCHOut)

            For j As Integer = 0 To UBound(gudt.SetChAndOr.udtCHOut(i).udtCHAndOr)
                With gudt.SetChAndOr.udtCHOut(i).udtCHAndOr(j)
                    .shtSysno = 0       ''SYSTEM No.
                    .shtChid = 0        ''CH ID
                    .bytSpare = 0       ''予備
                    .bytStatus = 0      ''ステータス種類
                    .shtMask = 0        ''マスクデータ
                End With
            Next

        Next
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : CH 変換設定ｸﾘｱ
    ' 返り値    : なし
    ' 引き数    : 
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Private Sub ClearCHConvertSetting(ByRef ConvertData As gTypSetChConv)

        For i As Integer = LBound(ConvertData.udtChConv) To UBound(ConvertData.udtChConv)
            ConvertData.udtChConv(i).shtChid = 0
        Next
    End Sub

    Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
        Call Me.Close()
    End Sub
End Class