Module modcommon2

    Public CopyFromTempFolder As String

    Public CopyToTempFolder As String

    'アプリケーションパス
    Public AppPass As String

    'アプリケーションパス書込み用テキストフォルダ
    Public AppPassTXT As String

    ''エリアコピー用
    Public gCopyChList(gCstOneGroupChannelMax - 1, 115) As String
    Public gCopyDummyChList(gCstOneGroupChannelMax - 1, 115) As Boolean

    ' ver2.0.8.C 2018.11.14  116→117
    'Public gChkChList(gCstChListColPosAlmMimic) As String
    Public gChkChList(gCstChListColPosFlagMotorColor) As String

    ' タグ表示モードフラグ    Ver1.7.5 2015.10.22
    ' 今後拡張の予定あり
    Public g_TagDispFg As Byte

    Public blnAllRowFlg As Boolean = False
    Public intgridlineCount As Integer

    ' Ver1.8.3 2015.11.26  ｴﾃﾞｨﾀ設定追加
    Public g_bytFUSet As Byte                      '  FU端子表印刷ﾌﾗｸﾞ '' Iniﾌｧｲﾙより読み込み

    ' Ver1.10.5 2016.05.09  ｴﾃﾞｨﾀ設定追加
    Public g_bytNotCombine As Byte                      '  CHﾘｽﾄ印刷 ｺﾝﾊﾞｲﾝ仕様時標準ﾓｰﾄﾞ印刷ﾌﾗｸﾞ '' Iniﾌｧｲﾙより読み込み

    'Ver2.0.0.6 Output設定とグリーンマーク設定
    Public g_bytOutputPrint As Byte
    Public g_bytGreenMarkPrint As Byte
    'Ver2.0.1.7 OR CH設定
    Public g_bytOrAndPrint As Byte

    'Ver2.0.4.2 計測点リスト印字順
    Public g_bytChListOrder As Byte

    'Ver2.0.8.N 計測点リスト R,W,J,SのINSGを印字するしない
    Public g_bytChListINSGprint As Byte

    'Ver2.0.4.4 端子表印字レンジ印刷するしない
    Public g_bytTermRange As Byte

    'Ver2.0.7.4 基板ﾊﾞｰｼﾞｮﾝ印刷するしない
    Public g_bytTerVer As Byte

    Public g_bytTerDIMsg As Byte

    Public blnInputErrFlg As Boolean
    Public blnInputCautionFlg As Boolean

    Public strWKCH As Integer


    'Ver2.0.7.C
    Public g_bytSIOport As Byte                      ' (外販)SIOポート拡張ﾌﾗｸﾞ Iniﾌｧｲﾙより読み込み

    'Ver2.0.7.M 保安モードフラグ
    Public g_bytHOAN As Byte                        '(保安庁)保安庁か否かフラグ Iniﾌｧｲﾙより読込

    'Ver2.0.7.M 新デザインモードフラグ
    Public g_bytNEWDES As Byte                      '(新デザイン)新デザインか否かフラグ Iniﾌｧｲﾙより読込

    'Ver2.0.8.D 新デザインモードフラグ 2018.12.13 倉重
    Public g_bytGREPNUM As Byte                     '(ｸﾞﾙｰﾌﾟﾘﾎﾟｰｽﾞ)48or72管理フラグ Iniﾌｧｲﾙより読込
    Public g_bytSrcGREPNUM As Byte                  '(ｸﾞﾙｰﾌﾟﾘﾎﾟｰｽﾞ比較元)48or72管理フラグ Iniﾌｧｲﾙより読込

    'Ver2.0.8.I 端子表説明文 英和逆転フラグ 2019.02.21 倉重
    Public g_bytExoTxtEtoJ As Byte                  '端子表英和切替フラグ 0:逆転無し 1:逆転あり Iniファイルより読込


#Region "FUアドレス関連"

    'T.Ueki
    '-------------------------------------------------------------------- 
    ' 機能      : FUアドレス変換処理
    '--------------------------------------------------------------------
    Public Function FUAdd(ByVal Address As String, ByVal FUNNP As Integer) As String


        If Address.Length >= 3 Then

            If Address.Length = 5 Then
                If FUNNP = 1 Then
                    FUAdd = Address.Substring(0, 2)
                ElseIf FUNNP = 2 Then
                    FUAdd = Address.Substring(2, 1)
                Else
                    FUAdd = Address.Substring(3)
                End If
            Else
                If FUNNP = 1 Then
                    FUAdd = Address.Substring(0, 1)
                ElseIf FUNNP = 2 Then
                    FUAdd = Address.Substring(1, 1)
                Else
                    FUAdd = Address.Substring(2)
                End If
            End If
        Else
            FUAdd = ""
        End If

    End Function

    'T.Ueki
    '-------------------------------------------------------------------- 
    ' 機能      : FUアドレス入力調査処理
    '--------------------------------------------------------------------
    Public Function FUAddINPUT(ByVal FUNOPORTPIN As String, ByVal Kind As Integer, ByVal DType As String) As Integer

        Dim FUAddress As Integer
        Dim FUNo As Integer
        Dim FUPort As Integer
        Dim FUPin As Integer
        Dim NoLenSerch As Integer
        Dim PortLenSerch As Integer
        Dim PinLenSerch As Integer
        Dim TypeData As Integer

        TypeData = Val(DType)

        FUAddress = Len(FUNOPORTPIN)

        'EXT Deviceの場合はFU番号「0」固定
        Select Case FUAddress
            Case 4
                FUNOPORTPIN = "0" + FUNOPORTPIN
                NoLenSerch = 2
                PortLenSerch = 3
                PinLenSerch = 4
            Case 5
                NoLenSerch = 1
                PortLenSerch = 2
                PinLenSerch = 3
            Case Else
                FUNOPORTPIN = "0" + FUNOPORTPIN
                NoLenSerch = 1
                PortLenSerch = 3
                PinLenSerch = 4
        End Select

        Select Case Kind
            Case 1
                FUNo = Val(Mid(FUNOPORTPIN, 1, NoLenSerch))
                If FUNo >= 0 And FUNo <= 20 Then
                    FUAddINPUT = FUNo
                Else
                    FUAddINPUT = 0
                End If
            Case 2
                FUPort = Val(Mid(FUNOPORTPIN, PortLenSerch, 1))
                If FUPort >= 1 And FUPort <= 8 Then
                    FUAddINPUT = FUPort
                Else
                    FUAddINPUT = 0
                End If
            Case 3
                FUPin = Val(Mid(FUNOPORTPIN, PinLenSerch, FUAddress))
                If FUPin >= 1 And FUPin <= 65535 Then
                    FUAddINPUT = FUPin
                Else
                    FUAddINPUT = 0
                End If
        End Select

    End Function

    '--------------------------------------------------------------------
    '画面番号変更
    '--------------------------------------------------------------------
    Public Function ViewDataExchange(ByVal ViewNo1 As Integer) As Short

        Try

            Dim bytValue1, bytValue2 As Byte
            Dim bytArray(1) As Byte

            Call gSeparat2Byte(ViewNo1, bytValue1, bytValue2)

            bytArray(0) = bytValue2
            bytArray(1) = bytValue1

            ViewDataExchange = BitConverter.ToInt16(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Public Sub ChIDSearch(ByVal CHNo As Integer)

        'Dim a As gTypSetChGroupRepose
        'Dim b As gTypSetChGroupRepose

        'Dim i As Integer

        'Dim InputCH As Integer


        'Dim c(5000) As Integer
        'Dim d(5000) As Integer


        'If gudt.SetChConvNow.udtChConv <> gudt.SetChConvPrev.udtChConv Then

        'End If

        'a.udtRepose = gudt.SetChConvNow.udtChConv
        'b.udtRepose = gudt.SetChConvPrev.udtChConv

        'c(1) = a.udtRepose(1).shtChId

        'd(1) = b.udtRepose(1).shtChId


        'b = a.udtRepose(1).shtChId

        'If CHNo = a.udtRepose(1) Then
        'a()
        'End If

        'If c(1) <> d(1) Then Return False

        'ChIDSearch = True


    End Sub

    'T.Ueki
    '-------------------------------------------------------------------- 
    ' 機能      : 小数点調査処理
    '--------------------------------------------------------------------
    Public Function DecimalPoint(ByVal RangeTXT As String) As Boolean

        Dim RangeTXTLen As Integer
        Dim DecPoint As String
        Dim i As Integer

        DecimalPoint = False
        RangeTXTLen = Len(RangeTXT)

        For i = 1 To RangeTXTLen

            DecPoint = Mid(RangeTXT, i, 1)

            If DecPoint = "." Then
                DecimalPoint = True
                Exit For
            Else
                DecimalPoint = False
            End If
        Next

    End Function

    'T.Ueki
    '-------------------------------------------------------------------- 
    ' 機能      : 小数点調査処理
    '--------------------------------------------------------------------
    Public Function intDecimalPointSch(ByVal RangeTXT As String) As Integer

        Dim RangeTXTLen As Integer
        Dim DecPoint As String
        Dim i As Integer

        intDecimalPointSch = 0
        RangeTXTLen = Len(RangeTXT)

        If RangeTXTLen <> 0 Then

            If 0 <= RangeTXT.IndexOf(".") Then

                For i = 1 To RangeTXTLen
                    DecPoint = Mid(RangeTXT, i, 1)

                    If DecPoint = "." Then
                        intDecimalPointSch = RangeTXTLen - intDecimalPointSch - 1
                        Exit For
                    Else
                        intDecimalPointSch = intDecimalPointSch + 1
                    End If
                Next

            Else
                intDecimalPointSch = 0
            End If
        Else
            intDecimalPointSch = 0
        End If

    End Function

    '--------------------------------------------------------------------
    ' プルダウンコンボボックス数値取得処理
    '--------------------------------------------------------------------
    Public Function MenuStrName(ByVal name As String) As Integer

        Try

            Dim MenuName As String
            Dim MenuNameString As String = ""
            Dim MenuNameLen As Integer = 0
            Dim i As Integer

            MenuNameLen = Len(Trim(name))

            'Ver2.0.5.5
            For i = 1 To MenuNameLen

                MenuName = Mid(name, i, 1)

                'Select Case MenuName
                '    Case "A" To "Z"
                '        MenuNameString = MenuNameString + MenuName
                '    Case Else
                '        Exit For
                'End Select
                Select Case Asc(MenuName)
                    Case &H0
                        Exit For
                    Case Else
                        MenuNameString = MenuNameString + MenuName
                End Select
            Next
            MenuNameString = MenuNameString.ToUpper

            Select Case MenuNameString

                Case "VIEW"
                    MenuStrName = 1
                Case "TREND"
                    MenuStrName = 2
                Case "FREE"
                    MenuStrName = 3
                Case "SUMMARY"
                    MenuStrName = 4
                Case "HISTORY"
                    MenuStrName = 5
                Case "GRAPH"
                    MenuStrName = 6
                Case "MIMIC"
                    MenuStrName = 7
                Case "CONTROL"
                    MenuStrName = 8
                Case "CALCU"
                    MenuStrName = 9
                Case "PRINT"
                    MenuStrName = 10
                Case "SYSTEM"
                    MenuStrName = 11
                Case "STATUS"
                    MenuStrName = 12
                Case "HELP"
                    MenuStrName = 13
                Case "EXIT"
                    MenuStrName = 14
                Case "TCS"
                    MenuStrName = 15    '' 2015.01.19
                Case "J-S/ECO"
                    MenuStrName = 16
                Case "PID CTRL"
                    MenuStrName = 17    'Ver2.0.7.7 PIDコントロール追加
                Case "S/B SEQ"
                    MenuStrName = 18    'Ver2.0.7.I スタンバイシーケンス追加

                    'Ver2.0.7.H 保安庁日本語対応追加
                Case "全体監視"
                    MenuStrName = 1
                Case "ﾄﾚﾝﾄﾞ"
                    MenuStrName = 2
                Case "ﾌﾘｰ"
                    MenuStrName = 3
                Case "ｻﾏﾘ表示"
                    MenuStrName = 4
                Case "履歴"
                    MenuStrName = 5
                Case "ｸﾞﾗﾌ"
                    MenuStrName = 6
                Case "情報表示"
                    MenuStrName = 7
                Case "CONTROL"
                    MenuStrName = 8
                Case "CALCU"
                    MenuStrName = 9
                Case "保存"
                    MenuStrName = 10
                Case "ｼｽﾃﾑ"
                    MenuStrName = 11
                Case "ｽﾃｰﾀｽ"
                    MenuStrName = 12
                Case "ﾍﾙﾌﾟ"
                    MenuStrName = 13
                    '-

                Case Else
                    MenuStrName = 0
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '************************************************************************************************************'
    '文字にＮＵＬＬを入れて返す
    '************************************************************************************************************'
    Public Function MojiMake(ByVal Moji As String, ByVal maxbyte As Integer) As String

        Dim i As Integer
        Dim MojiLen As Integer
        Dim MaxKaisu As Integer
        Dim MojiNull As String

        MojiLen = LenB(Moji)

        MaxKaisu = maxbyte - MojiLen

        MojiNull = ""

        For i = 0 To MaxKaisu
            MojiNull = MojiNull + Chr(0)
        Next

        MojiMake = Moji + MojiNull

    End Function

#End Region


    'Ver2.0.1.2
    'アナログCHで、ｽﾃｰﾀｽのHH,H,L,LLを一行の文字列にする関数
    Public Function fnGetManuSTATUS(pt As gTypSetChRec) As String
        '戻りは「LL/L/NOR/H/HH」の形式となる
        Dim strRet As String = ""
        Dim strVal As String = ""

        'LOLO
        strVal = NZf(pt.AnalogLoLoStatusInput)
        If strVal <> "" Then If Asc(strVal) = 0 Then strVal = ""
        If strVal.Trim <> "" Then
            strRet = strRet & strVal.Trim & "/"
        End If

        'LO
        strVal = NZf(pt.AnalogLoStatusInput)
        If strVal <> "" Then If Asc(strVal) = 0 Then strVal = ""
        If strVal.Trim <> "" Then
            strRet = strRet & strVal.Trim & "/"
        End If

        'NOR
        'Ver2.0.7.M (保安庁)
        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様追加 hori
            strRet = strRet & "正常"
        Else
            strRet = strRet & "NOR"
        End If

        'HI
        strVal = NZf(pt.AnalogHiStatusInput)
        If strVal <> "" Then If Asc(strVal) = 0 Then strVal = ""
        If strVal.Trim <> "" Then
            strRet = strRet & "/" & strVal.Trim
        End If

        'HIHI
        strVal = NZf(pt.AnalogHiHiStatusInput)
        If strVal <> "" Then If Asc(strVal) = 0 Then strVal = ""
        If strVal.Trim <> "" Then
            strRet = strRet & "/" & strVal.Trim
        End If

        'NORのみ＝マニュアルステータス無しの場合は空白を戻す
        'Ver2.0.7.M (保安庁)
        'If strRet = "NOR" Then
        If strRet = "NOR" Or strRet = "正常" Then
            strRet = ""
        End If

        Return strRet
    End Function
    Public Function fnGetManuSTATUS_4(pstrLL As String, pstrL As String, pstrH As String, pstrHH As String) As String
        '戻りは「LL/L/NOR/H/HH」の形式となる
        Dim strRet As String = ""
        Dim strVal As String = ""


        'LOLO
        strVal = NZf(pstrLL)
        If strVal <> "" Then If Asc(strVal) = 0 Then strVal = ""
        If strVal.Trim <> "" Then
            strRet = strRet & strVal.Trim & "/"
        End If

        'LO
        strVal = NZf(pstrL)
        If strVal <> "" Then If Asc(strVal) = 0 Then strVal = ""
        If strVal.Trim <> "" Then
            strRet = strRet & strVal.Trim & "/"
        End If

        'NOR
        'Ver2.0.7.M (保安庁)
        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様追加 hori
            strRet = strRet & "正常"
        Else
            strRet = strRet & "NOR"
        End If

        'HI
        strVal = NZf(pstrH)
        If strVal <> "" Then If Asc(strVal) = 0 Then strVal = ""
        If strVal.Trim <> "" Then
            strRet = strRet & "/" & strVal.Trim
        End If

        'HIHI
        strVal = NZf(pstrHH)
        If strVal <> "" Then If Asc(strVal) = 0 Then strVal = ""
        If strVal.Trim <> "" Then
            strRet = strRet & "/" & strVal.Trim
        End If

        'NORのみ＝マニュアルステータス無しの場合は空白を戻す
        'Ver2.0.7.M (保安庁)
        'If strRet = "NOR" Then
        If strRet = "NOR" Or strRet = "正常" Then
            strRet = ""
        End If

        Return strRet
    End Function




    '************************************************************************************************************'
    'Sub:data_exchange
    '   目的説明:
    '               文字にＮＵＬＬを入れて返す
    '   入力引数:
    '               menu()(メインメニュー配列)
    '   戻 り 値:
    '               なし
    '************************************************************************************************************'
    '
    Function data_exchange(x As Integer) As Integer

        Dim dd As Integer
        Dim d1, d2 As String
        Dim s(3) As Byte
        Dim bytValue1, bytValue2 As Byte

        Call gSeparat2Byte(x, bytValue1, bytValue2)

        s(0) = bytValue1
        s(1) = bytValue2

        d1 = Hex(s(0) And &HFF)
        d1 = Hex(Val("&H" & d1) * (2 ^ 8))
        d2 = Hex(s(1) And &HFF)
        dd = Val("&H" & d1) + Val("&H" & d2)

        data_exchange = dd

    End Function

    'Ver2.0.0.7
    'Null、Nothing等の空文字を""に変換する関数
    Public Function NZf(ByVal a As String) As String
        If IsNull(a) Then
            NZf = ""
        Else
            NZf = a
        End If
    End Function
    Public Function IsNull(ByVal text As String) As Boolean
        'Nothing判定
        If IsNothing(text) = True Then
            Return True
        End If
        '空文字判定
        If text Is String.Empty Then
            Return True
        End If

        Return False
    End Function

    'Ver2.0.1.8 NZf関数経由で""だと０を返す
    Public Function NZfZero(ByVal a As String) As String
        Dim strA As String = NZf(a)

        If strA = "" Then
            Return "0"
        End If

        Return strA
    End Function

    'Ver2.0.3.2 一文字目が0x00でも””とするNZf
    Public Function NZfS(ByVal a As String) As String
        If IsNull(a) Then
            NZfS = ""
        Else
            If a.Length <= 0 Then
                NZfS = ""
            Else
                If Asc(a(0)) = 0 Then
                    NZfS = ""
                Else
                    NZfS = a
                End If
            End If
        End If
    End Function

    '大文字小文字区別するFindStringExact
    Public Function fnBackCmb(cmbData As ComboBox, pstrData As String) As Integer
        Dim intRet As Integer = -1
        Dim i As Integer = 0

        For i = 0 To cmbData.Items.Count - 1 Step 1
            If cmbData.Items.Item(i).row.itemarray(1) = pstrData Then
                intRet = i
                Exit For
            End If
        Next i


        Return intRet
    End Function

#Region "ﾀｸﾞ表示"     ' 2015.10.22 Ver1.7.5 追加

    '************************************************************************************************************'
    ' ﾀｸﾞ表示ﾓｰﾄﾞによりMAX数取得
    '************************************************************************************************************'
    Public Function GetTagSize() As Integer

        If gudt.SetSystem.udtSysOps.shtTagMode = 1 Then
            GetTagSize = 8
        Else
            GetTagSize = 16
        End If

    End Function

    '************************************************************************************************************'
    ' ChNo.より　ﾀｸﾞNo.取得　　Ver1.8.3  2015.11.26 追加
    '************************************************************************************************************'
    Public Function GetTagNoFromCHNo(ByVal nCHNo As Integer) As String

        Dim strTagNo As String
        Dim strTemp As String
        Dim iFind As Integer

        strTagNo = ""

        For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)
            With gudt.SetChInfo.udtChannel(i)
                If .udtChCommon.shtChno = nCHNo Then
                    '' Ver1.8.5.1 2015.12.02 ｽﾍﾟｰｽ削除処理追加
                    ''   後ろ1ﾊﾞｲﾄのみｽﾍﾟｰｽの場合、Trim関数でも削除されていないので無理矢理処理を追加
                    strTemp = GetTagNo(gudt.SetChInfo.udtChannel(i))

                    iFind = strTemp.IndexOf(" "c)
                    If iFind = -1 Then     '' ｽﾍﾟｰｽが存在しないので、そのまま代入
                        strTagNo = strTemp
                    Else
                        strTagNo = strTemp.Substring(0, iFind)
                    End If

                    Exit For
                End If
            End With
        Next

        Return strTagNo

    End Function

    '************************************************************************************************************'
    ' ﾀｸﾞNo.取得
    '************************************************************************************************************'
    Public Function GetTagNo(ByVal udtChannel As gTypSetChRec) As String

        Dim strTag As String

        strTag = ""

        Select Case udtChannel.udtChCommon.shtChType
            Case gCstCodeChTypeAnalog   ' ｱﾅﾛｸﾞ
                strTag = udtChannel.AnalogTagNo
            Case gCstCodeChTypeDigital  ' ﾃﾞｼﾞﾀﾙ
                If udtChannel.udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then  ' ｼｽﾃﾑ
                    strTag = udtChannel.SystemTagNo
                Else
                    strTag = udtChannel.DigitalTagNo
                End If
            Case gCstCodeChTypeMotor    ' ﾓｰﾀｰ
                strTag = udtChannel.MotorTagNo
            Case gCstCodeChTypeValve    ' ﾊﾞﾙﾌﾞ
                Select Case udtChannel.udtChCommon.shtData
                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2
                        strTag = udtChannel.ValveAiDoTagNo
                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20
                        strTag = udtChannel.ValveAiAoTagNo
                    Case Else
                        strTag = udtChannel.ValveDiDoTagNo
                End Select
            Case gCstCodeChTypeComposite    ' ｺﾝﾎﾟｼﾞｯﾄ
                strTag = udtChannel.CompositeTagNo
            Case gCstCodeChTypePulse
                If udtChannel.udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                  udtChannel.udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then
                    strTag = udtChannel.PulseTagNo
                Else
                    strTag = udtChannel.RevoTagNo
                End If
        End Select

        Return Trim(strTag)     '' 2015.11.12 Ver1.7.8 ｽﾍﾟｰｽ削除

    End Function

    '************************************************************************************************************'
    ' ﾀｸﾞNo.設定
    '************************************************************************************************************'
    Public Sub SetTagNo(ByRef udtChannel As gTypSetChRec, ByVal strTagNo As String)

        Select Case udtChannel.udtChCommon.shtChType
            Case gCstCodeChTypeAnalog   ' ｱﾅﾛｸﾞ
                udtChannel.AnalogTagNo = strTagNo
            Case gCstCodeChTypeDigital  ' ﾃﾞｼﾞﾀﾙ
                If udtChannel.udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then  ' ｼｽﾃﾑ
                    udtChannel.SystemTagNo = strTagNo
                Else
                    udtChannel.DigitalTagNo = strTagNo
                End If
            Case gCstCodeChTypeMotor    ' ﾓｰﾀｰ
                udtChannel.MotorTagNo = strTagNo
            Case gCstCodeChTypeValve    ' ﾊﾞﾙﾌﾞ
                Select Case udtChannel.udtChCommon.shtData
                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2
                        udtChannel.ValveAiDoTagNo = strTagNo
                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20
                        udtChannel.ValveAiAoTagNo = strTagNo
                    Case Else
                        udtChannel.ValveDiDoTagNo = strTagNo
                End Select
            Case gCstCodeChTypeComposite    ' ｺﾝﾎﾟｼﾞｯﾄ
                udtChannel.CompositeTagNo = strTagNo
            Case gCstCodeChTypePulse
                If udtChannel.udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                  udtChannel.udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then
                    udtChannel.PulseTagNo = strTagNo
                Else
                    udtChannel.RevoTagNo = strTagNo
                End If
            Case gCstCodeChTypePID
                udtChannel.PidTagNo = strTagNo
        End Select
    End Sub

    '************************************************************************************************************'
    ' ﾀｸﾞNo.入力ﾁｪｯｸ
    '************************************************************************************************************'
    Public Function ChkTagInput(txtTagNo As String) As Boolean
        Dim intMaxSize As Short

        If gudt.SetSystem.udtSysOps.shtTagMode = 1 Then     ' ﾀｸﾞ追加ﾓｰﾄﾞ
            intMaxSize = 6
        Else
            intMaxSize = 16
        End If

        ' 入力ｻｲｽﾞﾁｪｯｸ
        If txtTagNo.Length > intMaxSize Then
            Call MessageBox.Show("TagNo Size =" & intMaxSize & "OVER", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return True

    End Function

    '************************************************************************************************************'
    ' 端子印刷　初期設定
    '************************************************************************************************************'
    Public Sub SetTermPrintSetting()

        If gudt.SetSystem.udtSysOps.shtTagMode = 0 Then     ' 標準

            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori
                frmPrtTerminalPreview.mCstLabelDigitalCh1Jpn = "CH"
                frmPrtTerminalPreview.mCstLabelAnalogCh1Jpn = "CH"
            Else
                frmPrtTerminalPreview.mCstLabelDigitalCh = "CHNO"
                frmPrtTerminalPreview.mCstLabelAnalogCh = "CHNO"
            End If

            frmPrtTerminalPreview.mCstPosDigitalAdd = 65    ' 画面左端からの "ADD" 描画開始位置
            frmPrtTerminalPreview.mCstPosAnalogAdd = 65
            gCstFrameTerminalLeft = 40      ''端子台　画面左端からのX位置
            gCstFrameTerminalWidth = 710

        Else        ' ﾀｸﾞ表示
            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori
                frmPrtTerminalPreview.mCstLabelDigitalCh1Jpn = "TAG"
                frmPrtTerminalPreview.mCstLabelAnalogCh1Jpn = "TAG"
            Else
                frmPrtTerminalPreview.mCstLabelDigitalCh = "TAGNO"
                frmPrtTerminalPreview.mCstLabelAnalogCh = "TAGNO"
            End If

            frmPrtTerminalPreview.mCstPosDigitalAdd = 45    ' 画面左端からの "ADD" 描画開始位置
            frmPrtTerminalPreview.mCstPosAnalogAdd = 45
            gCstFrameTerminalLeft = 25      ''端子台　画面左端からのX位置
            gCstFrameTerminalWidth = 725
        End If

    End Sub

    '************************************************************************************************************'
    ' ﾀｸﾞNo. ｽﾍﾟｰｽ削除処理
    '
    '   Ver1.9.0 2015.12.16 追加
    '************************************************************************************************************'
    Public Sub DeleteTagSpace(ByRef strTag As String)
        Dim strTemp As String
        Dim iFind As Integer

        strTemp = strTag

        iFind = strTemp.IndexOf(" "c)
        If iFind = -1 Then     '' ｽﾍﾟｰｽが存在しないので、そのまま代入
            strTag = strTemp
        Else
            strTag = strTemp.Substring(0, iFind)
        End If
    End Sub


#End Region


#Region "ｺﾝﾊﾟｲﾙ前処理"     ' 2015.10.24 Ver1.7.5 追加

    '************************************************************************************************************'
    ' ｺﾝﾊﾟｲﾙ前の設定調整
    '************************************************************************************************************'
    Public Sub SetDataProc()

        Dim intChIndex As Integer

        For intChIndex = 0 To gCstChannelIdMax - 1
            With gudt.SetChInfo.udtChannel(intChIndex)
                Select Case .udtChCommon.shtChType
                    Case gCstCodeChTypeAnalog   ' ｱﾅﾛｸﾞ
                        Call SetAnalogData(gudt.SetChInfo.udtChannel(intChIndex))
                    Case gCstCodeChTypeDigital  ' ﾃﾞｼﾞﾀﾙ
                        Call SetDigitalData(gudt.SetChInfo.udtChannel(intChIndex))
                    Case gCstCodeChTypeMotor    ' ﾓｰﾀｰ
                        Call SetMotorData(gudt.SetChInfo.udtChannel(intChIndex))
                End Select

            End With
        Next

    End Sub

    '************************************************************************************************************'
    ' ｱﾅﾛｸﾞ設定
    '************************************************************************************************************'
    Public Sub SetAnalogData(ByRef udtChannel As gTypSetChRec)

        'Dim FuType As String
        Dim intFuNo As Integer = 0
        Dim intFuPort As Integer = 0
        Dim intDataType As Integer = 0

        Try
            Return      ' Ver1.7.7 2015.11.10

            ' ﾉｰﾏﾙﾚﾝｼﾞがH/Lとも0ならば設定なしとする
            If udtChannel.AnalogNormalHigh = 0 And udtChannel.AnalogNormalLow = 0 Then
                udtChannel.AnalogNormalHigh = gCstCodeChAlalogNormalRangeNothingHi
                udtChannel.AnalogNormalLow = gCstCodeChAlalogNormalRangeNothingLo
            End If

            ' ｱﾅﾛｸﾞ基板の種類からﾃﾞｰﾀ種類を設定する
            If udtChannel.udtChCommon.shtData < gCstCodeChDataTypeAnalogJacom Then        ' FU入力のみﾁｪｯｸする
                intFuNo = udtChannel.udtChCommon.shtFuno
                intFuPort = udtChannel.udtChCommon.shtPortno

                If intFuPort < 1 Then       ' Ver1.7.7 2015.11.10 　ｴﾗｰﾁｪｯｸ追加
                    Return
                End If

                If Not (intFuNo = gCstCodeChNotSetFuNo Or intFuPort = gCstCodeChNotSetFuPort) Then
                    intDataType = 0
                    'FuType = gGetFuBordType(udtChannel, gEnmIOType.ioInput, gBitCheck(udtChannel.udtChCommon.shtFlag1, gCstCodeChCommonFlagBitPosWk))


                    Select Case gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intFuPort - 1).shtType
                        Case gCstCodeFuSlotTypeAI_2 ' 2線式Pt
                            intDataType = gCstCodeChDataTypeAnalog2Pt
                            'udtChannel.udtChCommon.shtChType = gCstCodeChDataTypeAnalog2Jpt
                        Case gCstCodeFuSlotTypeAI_3 ' 3線式Pt
                            intDataType = gCstCodeChDataTypeAnalog3Pt
                            'udtChannel.udtChCommon.shtChType = gCstCodeChDataTypeAnalog3Jpt
                        Case gCstCodeFuSlotTypeAI_1_5   ' 1-5V
                            intDataType = gCstCodeChDataTypeAnalog1_5v
                        Case gCstCodeFuSlotTypeAI_4_20  ' 4-20mA
                            intDataType = gCstCodeChDataTypeAnalog4_20mA
                        Case gCstCodeFuSlotTypeAI_K ' K入力
                            intDataType = gCstCodeChDataTypeAnalogK
                    End Select

                    'Public Const gCstCodeChDataTypeAnalogPT4_20mA As Integer = &H7A     ' ver1.4.0 2011.07.29

                    If intDataType <> 0 And udtChannel.udtChCommon.shtData <> intDataType Then
                        udtChannel.udtChCommon.shtData = intDataType
                    End If
                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '************************************************************************************************************'
    ' ﾃﾞｼﾞﾀﾙ設定
    '************************************************************************************************************'
    Public Sub SetDigitalData(ByRef udtChannel As gTypSetChRec)
        If udtChannel.DigitalDiFilter = 0 Then
            udtChannel.DigitalDiFilter = 12     ' DIﾌｨﾙﾀｰは12固定
        End If

    End Sub

    '************************************************************************************************************'
    ' ﾓｰﾀｰ設定
    '************************************************************************************************************'
    Public Sub SetMotorData(ByRef udtChannel As gTypSetChRec)
        If udtChannel.MotorDiFilter = 0 Then
            udtChannel.MotorDiFilter = 12       ' DIﾌｨﾙﾀｰは12固定
        End If

    End Sub

#End Region


#Region "ﾛｲﾄﾞ表示"     ' 2015.11.12 Ver1.7.8 追加

    '************************************************************************************************************'
    ' ﾛｲﾄﾞ表示対応　ｱﾗｰﾑﾚﾍﾞﾙ番号取得
    '************************************************************************************************************'
    Public Function GetAlmLevelNo(ByVal udtChannel As gTypSetChRec) As Integer

        Dim nType As Integer

        Select Case udtChannel.udtChCommon.shtChType
            Case gCstCodeChTypeAnalog   ' ｱﾅﾛｸﾞ
                nType = udtChannel.AnalogLRMode
            Case gCstCodeChTypeDigital  ' ﾃﾞｼﾞﾀﾙ
                If udtChannel.udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then  ' ｼｽﾃﾑ
                    nType = udtChannel.SystemLRMode
                Else
                    nType = udtChannel.DigitalLRMode
                End If
            Case gCstCodeChTypeMotor    ' ﾓｰﾀｰ
                nType = udtChannel.MotorLRMode
            Case gCstCodeChTypeValve    ' ﾊﾞﾙﾌﾞ
                Select Case udtChannel.udtChCommon.shtData
                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2
                        nType = udtChannel.ValveAiDoLRMode
                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20
                        nType = udtChannel.ValveAiAoLRMode
                    Case Else
                        nType = udtChannel.ValveDiDoLRMode
                End Select
            Case gCstCodeChTypeComposite    ' ｺﾝﾎﾟｼﾞｯﾄ
                nType = udtChannel.CompositeLRMode
            Case gCstCodeChTypePulse
                If udtChannel.udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                  udtChannel.udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then
                    nType = udtChannel.PulseLRMode
                Else
                    nType = udtChannel.RevoLRMode
                End If
        End Select

        Return nType



    End Function

    '************************************************************************************************************'
    ' ﾛｲﾄﾞ表示対応　ｱﾗｰﾑﾚﾍﾞﾙ番号取得
    '************************************************************************************************************'
    Public Function GetAlmLevelName(ByVal udtChannel As gTypSetChRec) As String

        Dim strAlmLevel As String
        Dim nType As Integer

        nType = GetAlmLevelNo(udtChannel)

        Select Case nType
            Case 1
                strAlmLevel = "EMG ALARM"
            Case 2
                strAlmLevel = "ALARM"
            Case 3
                strAlmLevel = "WARNING"
            Case 4
                strAlmLevel = "CAUTION"
            Case Else
                strAlmLevel = ""
        End Select

        Return strAlmLevel
    End Function
#End Region

#Region "MimicAlm"     '南日本M761対応

    '************************************************************************************************************'
    ' MimicAlm表示対応　Mimic番号取得
    '************************************************************************************************************'
    Public Function GetMimicAlm(ByVal udtChannel As gTypSetChRec) As String

        Dim nType As String = ""

        Select Case udtChannel.udtChCommon.shtChType
            Case gCstCodeChTypeAnalog   ' ｱﾅﾛｸﾞ
                nType = udtChannel.AnalogAlmMimic
            Case gCstCodeChTypeDigital  ' ﾃﾞｼﾞﾀﾙ
                If udtChannel.udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then  ' ｼｽﾃﾑ
                    nType = udtChannel.SystemAlmMimic
                Else
                    nType = udtChannel.DigitalAlmMimic
                End If
            Case gCstCodeChTypeMotor    ' ﾓｰﾀｰ
                nType = udtChannel.MotorAlmMimic
            Case gCstCodeChTypeValve    ' ﾊﾞﾙﾌﾞ
                Select Case udtChannel.udtChCommon.shtData
                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2
                        nType = udtChannel.ValveAiDoAlmMimic
                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValveAO_4_20
                        nType = udtChannel.ValveAiAoAlmMimic
                    Case Else
                        nType = udtChannel.ValveDiDoAlmMimic
                End Select
            Case gCstCodeChTypeComposite    ' ｺﾝﾎﾟｼﾞｯﾄ
                nType = udtChannel.CompositeAlmMimic
            Case gCstCodeChTypePulse
                If udtChannel.udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                  udtChannel.udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then
                    nType = udtChannel.PulseAlmMimic
                Else
                    nType = udtChannel.RevoAlmMimic
                End If
            Case Else
                nType = 0
        End Select

        If nType = 0 Then
            nType = ""
        End If

        Return nType
    End Function

#End Region


#Region "ｴﾃﾞｨﾀ設定読み込み"     ' 2015.11.26 Ver1.8.3 追加

    '************************************************************************************************************'
    ' Iniﾌｧｲﾙ読み込み
    '************************************************************************************************************'
    Public Sub ReadEditIni(ByVal strIniPath As String)

        Dim strTemp As String

        If System.IO.File.Exists(strIniPath) Then
            strTemp = GetIni("System", "FUPrint", "1", strIniPath)
            g_bytFUSet = CByte(strTemp)

            '' Ver1.10.5 2016.05.09 ｺﾝﾊﾞｲﾝ設定 CHﾘｽﾄ印刷ﾓｰﾄﾞ
            strTemp = GetIni("System", "NotCombinPrint", "0", strIniPath)
            g_bytNotCombine = CByte(strTemp)
            ''//

            'Ver2.0.0.6 Output設定とグリーンマークの印刷ﾓｰﾄﾞ
            strTemp = GetIni("System", "OutputPrint", "0", strIniPath)
            g_bytOutputPrint = CByte(strTemp)
            strTemp = GetIni("System", "GreenMarkPrint", "0", strIniPath)
            g_bytGreenMarkPrint = CByte(strTemp)
            'Ver2.0.1.7 OrAndの印刷ﾓｰﾄﾞ
            strTemp = GetIni("System", "OrAndPrint", "0", strIniPath)
            g_bytOrAndPrint = CByte(strTemp)
            'Ver2.0.4.2 ChListの印字順番モード
            strTemp = GetIni("System", "ChListOrderPrint", "0", strIniPath)
            g_bytChListOrder = CByte(strTemp)
            'Ver2.0.8.N R,W,J,SのINSGを印字するしない
            strTemp = GetIni("System", "ChListINSGPrint", "0", strIniPath)
            g_bytChListINSGprint = CByte(strTemp)
            'Ver2.0.4.4 端子表印字のレンジ印刷するしない
            strTemp = GetIni("System", "TermRangePrint", "0", strIniPath)
            g_bytTermRange = CByte(strTemp)
            'Ver2.0.7.4 基板ﾊﾞｰｼﾞｮﾝ印刷するしない
            strTemp = GetIni("System", "TermVersionPrint", "0", strIniPath)
            g_bytTerVer = CByte(strTemp)

            'Ver2.0.8.7 DI端子表に共通コモンのメッセージ
            strTemp = GetIni("System", "TermDICommonMsg", "0", strIniPath)
            g_bytTerDIMsg = CByte(strTemp)

            'Ver2.0.7.C (外販)SIOポート拡張ﾌﾗｸﾞ
            strTemp = GetIni("System", "SIOport", "0", strIniPath)
            g_bytSIOport = CByte(strTemp)

            'Ver2.0.7.M (保安)保安庁ﾌﾗｸﾞ
            strTemp = GetIni("System", "Hoan", "0", strIniPath)
            g_bytHOAN = CByte(strTemp)


            'Ver2.0.7.M (新デザイン)新デザインﾌﾗｸﾞ
            strTemp = GetIni("System", "Newdes", "0", strIniPath)
            g_bytNEWDES = CByte(strTemp)

            'Ver2.0.8.E 2018.12.13グループリポーズ追加
            strTemp = GetIni("System", "GREPNUM", "0", strIniPath)
            g_bytGREPNUM = CByte(strTemp)

            'Ver2.0.8.I 2019.02.21端子表説明文 英和逆転フラグ追加
            strTemp = GetIni("System", "ExEtoJ", "0", strIniPath)
            g_bytExoTxtEtoJ = CByte(strTemp)

        Else        '' ﾃﾞﾌｫﾙﾄは新ﾀｲﾌﾟ
            g_bytFUSet = 1
            g_bytNotCombine = 0     '' Ver1.10.5 2016.05.09 ｺﾝﾊﾞｲﾝ設定 CHﾘｽﾄ印刷ﾓｰﾄﾞ 初期値

            'Ver2.0.0.6 Outputとグリーンマーク
            g_bytOutputPrint = 0
            g_bytGreenMarkPrint = 0
            'Ver2.0.1.7 OrAnd
            g_bytOrAndPrint = 0
            g_bytChListOrder = 0
            g_bytChListINSGprint = 0  'Ver2.0.8.N R,W,J,SのINSGを印字するしない
            g_bytTermRange = 1
            g_bytTerVer = 1
            g_bytTerDIMsg = 1   'Ver2.0.8.7 DI端子表に共通コモンのメッセージ

            'Ver2.0.7.C (外販)SIOポート拡張ﾌﾗｸﾞ
            g_bytSIOport = 0

            'Ver2.0.7.M (保安)保安庁ﾌﾗｸﾞ
            g_bytHOAN = 0

            'Ver2.0.7.M (新デザイン)新デザインﾌﾗｸﾞ
            g_bytNEWDES = 0

            'Ver2.0.8.E 2018.12.13グループリポーズ追加
            g_bytGREPNUM = 0

            'Ver2.0.8.I 2019.02.21端子表説明文 英和逆転フラグ
            g_bytExoTxtEtoJ = 0
        End If

    End Sub

    '************************************************************************************************************'
    ' 機能 Iniﾌｧｲﾙ読み込み
    ' 用途 比較元のファイル読み込み時にiniファイルを読み込む関数
    ' 履歴　2018.12.13 倉重 新規作成
    '************************************************************************************************************'
    Public Sub ReadEditRepIni(ByVal strIniPath As String)
        Dim strTemp As String

        If System.IO.File.Exists(strIniPath) Then
            strTemp = GetIni("System", "GREPNUM", "0", strIniPath)
            g_bytSrcGREPNUM = CByte(strTemp)
        Else        '' ﾃﾞﾌｫﾙﾄは新ﾀｲﾌﾟ
            g_bytSrcGREPNUM = 0

        End If
    End Sub

    '************************************************************************************************************'
    ' Iniﾌｧｲﾙ書き込み
    '************************************************************************************************************'
    Public Sub SaveEditIni(ByVal strIniPath As String)

        Dim strPath As String

        '' Iniﾌｧｲﾙが存在しない場合はIniﾌｫﾙﾀﾞからｺﾋﾟｰ
        If System.IO.File.Exists(strIniPath) = False Then
            strPath = gGetDirNameIniFile() & "\" & gCstIniFile

            System.IO.File.Copy(strPath, strIniPath)
        End If

        PutIni("System", "FUPrint", g_bytFUSet, strIniPath)        '' FU印字ﾀｲﾌﾟ

        PutIni("System", "NotCombinPrint", g_bytNotCombine, strIniPath)   '' Ver1.10.5 2016.05.09 ｺﾝﾊﾞｲﾝ設定 CHﾘｽﾄ印刷ﾓｰﾄﾞ

        'Ver2.0.0.6 Output設定とグリーンマーク
        PutIni("System", "OutputPrint", g_bytOutputPrint, strIniPath)
        PutIni("System", "GreenMarkPrint", g_bytGreenMarkPrint, strIniPath)
        'Ver2.0.1.7 OrAnd
        PutIni("System", "OrAndPrint", g_bytOrAndPrint, strIniPath)

        'Ver2.0.4.8
        '計測点リスト印字順番
        PutIni("System", "ChListOrderPrint", g_bytChListOrder, strIniPath)

        'Ver2.0.8.N R,W,J,SのINSGを印字するしない
        PutIni("System", "ChListINSGPrint", g_bytChListINSGprint, strIniPath)

        '端子表のレンジ印字
        PutIni("System", "TermRangePrint", g_bytTermRange, strIniPath)

        'Ver2.0.7.4
        '基板ﾊﾞｰｼﾞｮﾝ印刷
        PutIni("System", "TermVersionPrint", g_bytTerVer, strIniPath)

        'Ver2.0.8.7
        'DI端子表に共通コモンのメッセージ
        PutIni("System", "TermDICommonMsg", g_bytTerDIMsg, strIniPath)

        'Ver2.0.7.C
        PutIni("System", "SIOport", g_bytSIOport, strIniPath)        '(外販)SIOポート拡張ﾌﾗｸﾞ

        'Ver2.0.7.M
        PutIni("System", "Hoan", g_bytHOAN, strIniPath)             '(保安庁)保安庁ﾌﾗｸﾞ

        'Ver2.0.7.M
        PutIni("System", "Newdes", g_bytNEWDES, strIniPath)         '(新デザイン)新デザインﾌﾗｸﾞ

        'Ver2.0.8.E 2018.12.13
        PutIni("System", "GREPNUM", g_bytGREPNUM, strIniPath)         'グループリポーズ追加

        'Ver2.0.8.I 2019.02.21
        PutIni("System", "ExEtoJ", g_bytExoTxtEtoJ, strIniPath)         '端子表説明文 英和逆転フラグ
    End Sub

    '****************************************
    'INIファイルから参照したいキーの値を取得する
    'ApName   : セクション名
    'KeyName  : 項目名
    'Default  : 項目が存在しない場合の初期値
    'FileName : 参照ファイル名
    '****************************************
    Public Function GetIni(ByVal ApName As String, _
                          ByVal KeyName As String, _
                          ByVal Defaults As String, _
                          ByVal Filename As String _
                         ) As String

        Dim strResult As New System.Text.StringBuilder
        strResult.Capacity = 256   'バッファのサイズを指定

        Call GetPrivateProfileString(ApName, KeyName, Defaults, _
                                 strResult, strResult.Capacity, _
                                 Filename)

        GetIni = strResult.ToString()

    End Function

    '****************************************
    'INIファイルに新たなキーの値を書込む
    '   ※既存のキーがあれば更新・なければ新規作成する
    'ApName   : セクション名
    'KeyName  : 項目名
    'Param    : 更新する値
    'FileName : 書出ファイル名
    '****************************************
    Public Sub PutIni(ByVal ApName As String, _
                      ByVal KeyName As String, _
                      ByVal Param As String, _
                      ByVal Filename As String)

        Call WritePrivateProfileString(ApName, KeyName, _
                                       Param, Filename)
    End Sub

#End Region


#Region "ｽﾃｰﾀｽ分割処理"     ' 2016.02.03 Ver1.9.6 追加

    '--------------------------------------------------------------------
    ' 機能      : ｽﾃｰﾀｽ文字列分割
    ' 返り値    : なし
    ' 引き数    : ARG1 - 分割前ステータス文字列
    ' 　　　    : ARG2 - 分割後ステータス1
    ' 　　　    : ARG3 - 分割後ステータス2
    ' 機能説明  :   "/"　が1つしか存在しない場合は"/"で区切る
    '               "/"が複数存在する場合、ステータス文字列内に*が含まれる場合は"*"の前の"/"にて区切る
    '               それ以外は最後の"/"にて区切る
    '--------------------------------------------------------------------
    Public Sub SplitStatusString(ByVal strStatus As String, ByRef strStatus1 As String, ByRef strStatus2 As String)
        Dim nStartIndex As Integer
        Dim nEndStartIndex As Integer
        Dim nTemp As Integer
        Dim nData As Integer
        Dim nAst As Integer
        Dim tempStr As String

        tempStr = Trim(strStatus)
        nStartIndex = tempStr.IndexOf("/")
        If nStartIndex = -1 Then    '' "/" が含まれない
            strStatus1 = tempStr
            strStatus2 = ""

            Return
        End If

        nAst = tempStr.IndexOf("*")

        If nAst <> -1 Then      '' "*"が存在する場合は"*"の前の区切り
            nTemp = tempStr.IndexOf("/", nAst)
            If nTemp <> -1 Then     '' "*"の後ろの"/"
                strStatus1 = tempStr.Substring(0, nTemp)
                strStatus2 = tempStr.Substring(nTemp + 1)
            Else
                nTemp = tempStr.LastIndexOf("/", nAst)
                strStatus1 = tempStr.Substring(0, nTemp)
                strStatus2 = tempStr.Substring(nTemp + 1)
            End If

            Return
        End If

        nTemp = tempStr.IndexOf("/", nStartIndex + 1)
        If nTemp = -1 Then      '' "/"が1つしか存在しない
            strStatus1 = tempStr.Substring(0, nStartIndex)
            strStatus2 = tempStr.Substring(nStartIndex + 1)

            Return
        End If


        Do While True
            nEndStartIndex = nTemp
            nData = nTemp + 1
            nTemp = tempStr.IndexOf("/", nData)
            If nTemp = -1 Then  '' "/" が無くなったらﾙｰﾌﾟを抜ける
                Exit Do
            End If
        Loop

        strStatus1 = tempStr.Substring(0, nEndStartIndex)
        strStatus2 = tempStr.Substring(nEndStartIndex + 1)

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ｽﾃｰﾀｽ文字列分割
    ' 返り値    : なし
    ' 引き数    : ARG1 - 分割前ステータス文字列
    ' 　　　    : ARG2 - 分割後ステータス1
    ' 　　　    : ARG3 - 分割後ステータス2
    ' 機能説明  :   "/"　が1つしか存在しない場合は"/"で区切る
    '               "/"が複数存在する場合、ステータス文字列内に*が含まれる場合は"*"の前の"/"にて区切る
    '               それ以外は最後の"/"にて区切る
    '--------------------------------------------------------------------
    Public Sub GetStatusString(ByVal strStatus As String, ByRef strStatus1 As String, ByRef strStatus2 As String)

        Dim strTemp As String

        '' ｽﾃｰﾀｽ文字列取得
        SplitStatusString(strStatus, strStatus1, strStatus2)

        '' 文字数ﾁｪｯｸ
        'Ver2.0.7.L
        If LenB(strStatus1) > 8 Then
            'strTemp = strStatus1.Substring(0, 8)
            strTemp = MidB(strStatus1, 0, 8)
            strStatus1 = strTemp
        End If

        If LenB(strStatus2) > 8 Then
            'strTemp = strStatus2.Substring(0, 8)
            strTemp = MidB(strStatus2, 0, 8)
            strStatus2 = strTemp
        End If

    End Sub
#End Region


#Region "ｽﾃｰﾀｽ分割処理"     ' 2016.02.17 Ver1.9.7 追加

    '--------------------------------------------------------------------
    ' 機能      : ｺﾝﾄﾛｰﾙ USE/NOT Use 変換
    ' 返り値    : True : Tempﾌｧｲﾙ作成済み    False : Tempﾌｧｲﾙ作成無し
    ' 引き数    : strFullPath - ｺﾝﾄﾛｰﾙ可・不可ﾃｰﾌﾞﾙ ﾌﾙﾊﾟｽ  ﾌｧｲﾙｻｲｽﾞが旧ﾀｲﾌﾟの場合はTempﾊﾟｽに書き換える
    ' 　　　    : strPathBase - ﾌｫﾙﾀﾞﾊﾟｽ
    ' 　　　    : strCurFileName - ﾌｧｲﾙ名称
    ' 　　　    : SaveFg - 保存ﾌﾗｸﾞ
    ' 機能説明  :   ﾃｰﾌﾞﾙMAX点数 32 → 128の拡張に伴い、過去のﾌｧｲﾙを参照すると、ｻｲｽﾞが異なるためｴﾗｰが発生する
    '               このため、Tempﾌｫﾙﾀﾞ直下に拡張したﾃﾞｰﾀを作成し、再読み込みを行うための変換処理
    '--------------------------------------------------------------------
    Public Function SaveTempCtrlUseNotUseFile(ByRef strFullPath As String, ByVal strPathBase As String, _
                                                ByVal strCurFileName As String, ByVal SaveFg As Byte) As Boolean

        Dim fs As New System.IO.FileStream(strFullPath, System.IO.FileMode.Open, IO.FileAccess.Read)
        Dim nLength As Long = fs.Length
        Dim strTemp As String
        Dim nTemp As Integer
        Dim nIndex As Integer = 0

        Dim lngFsize As Long = 0

        'Ver2.0.0.7 128→256さらにファイル拡張
        Try

            '' 変換後のﾃﾞｰﾀならば処理を抜ける
            If nLength <> gOldUseNotUseFileSize _
                And nLength <> gOldUseNotUseFileSize2 Then
                fs.Close()
                Return False
            End If

            'ファイルサイズを特定する
            lngFsize = nLength


            ''フルパス作成
            If SaveFg = 0 Then      '' Ver1.0.3 2016.03.09  比較時はTempﾌｫﾙﾀﾞを変更
                'Ver2.0.2.0 Tempﾌｧｲﾙとする
                'strTemp = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileVersion) & "\Temp"
                strTemp = strPathBase
            Else
                '' Saveﾌｫﾙﾀﾞ名を探す
                Do While True
                    nTemp = strPathBase.IndexOf("Save", nIndex + 1)
                    If nTemp = -1 Then
                        Exit Do
                    End If

                    nIndex = nTemp
                Loop

                '' Tempﾌｫﾙﾀﾞﾊﾟｽ取得
                strTemp = strPathBase.Substring(0, nIndex) & "Temp"
            End If

            '' Ver1.10.3 2016.03.09  Tempﾌｫﾙﾀﾞが見つからない場合はｴﾗｰを表示
            If Not System.IO.Directory.Exists(strTemp) Then
                MessageBox.Show("Not Find Temp Folder")
                Return False
            End If
            ''//

            If SaveFg = 0 Then
                strTemp = strTemp & "\" & "temp_" & strCurFileName
            Else
                strTemp = strTemp & "\" & strCurFileName
            End If


            Dim fs1 As New System.IO.FileStream(strTemp, System.IO.FileMode.Create, IO.FileAccess.Write)
            'Dim os(gOldUseNotUseFileSize - 1) As Byte
            Dim os(lngFsize - 1) As Byte
            Dim ns((gAmxControlUseNotUse - 32) * 326 - 1) As Byte

            '' 既存ﾃﾞｰﾀ読込み
            'fs.Read(os, 0, gOldUseNotUseFileSize)
            fs.Read(os, 0, lngFsize)
            fs.Close()

            '' 新規ﾃﾞｰﾀ書込み
            fs1.Seek(0, IO.SeekOrigin.Begin)
            'fs1.Write(os, 0, gOldUseNotUseFileSize)
            fs1.Write(os, 0, lngFsize)
            fs1.Seek(0, IO.SeekOrigin.End)
            fs1.Write(ns, 0, ns.Length)
            fs1.Close()

            '' 保存ﾌﾗｸﾞｾｯﾄ
            If SaveFg = 1 Then
                gudt.SetEditorUpdateInfo.udtSave.bytCtrlUseNotuseM = 1
                gudt.SetEditorUpdateInfo.udtSave.bytCtrlUseNotuseC = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytCtrlUseNotuseM = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytCtrlUseNotuseC = 1
            End If

            strFullPath = strTemp

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))

            Return False
        End Try


    End Function
#End Region


#Region "単位　ﾁｪｯｸ処理"     ' 2016.02.20 Ver1.9.8 追加

    '--------------------------------------------------------------------
    ' 機能      : 単位ﾁｪｯｸ
    ' 返り値    : True : 既存ｺｰﾄﾞ    False : 手入力
    ' 引き数    : strUnit - 単位
    ' 機能説明  :   rpm と RPM を区別する必要があるため追加
    '--------------------------------------------------------------------
    Public Function ChkUnitType(ByVal strUnit As String) As Boolean

        If strUnit = "RPM" Then
            Return False
        End If

        Return True

    End Function

#End Region



#Region "PIDコントロールがあるか否か判定関数"
    Public Function fnChkPID() As Boolean
        Dim blRet As Boolean = False
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim x As Integer = 0

            With gudt.SetOpsPulldownMenuM
                For i = 0 To UBound(.udtDetail) Step 1
                    For j = 0 To UBound(.udtDetail(i).udtGroup) Step 1
                        For x = 0 To UBound(.udtDetail(i).udtGroup(j).udtSub) Step 1
                            If .udtDetail(i).udtGroup(j).udtSub(x).SubbytMenuType2 = 110 Then
                                'PROCSEE ITEM2 に「110」があればPIDコントロールがある
                                Return True
                            End If
                        Next x
                    Next j
                Next i
            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return blRet
    End Function

    Public Function fnChkCountPID() As Integer
        Dim intRet As Integer = 0
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim x As Integer = 0

            With gudt.SetOpsPulldownMenuM
                For i = 0 To UBound(.udtDetail) Step 1
                    For j = 0 To UBound(.udtDetail(i).udtGroup) Step 1
                        For x = 0 To UBound(.udtDetail(i).udtGroup(j).udtSub) Step 1
                            If .udtDetail(i).udtGroup(j).udtSub(x).SubbytMenuType2 = 110 Then
                                'PROCSEE ITEM2 に「110」があればPIDコントロールがある
                                intRet = intRet + 1
                            End If
                        Next x
                    Next j
                Next i
            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRet
    End Function

    Public Function fnGetPIDctrlTitle(ByRef pstrTitle() As String) As Integer
        Dim intRet As Integer = 0
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim x As Integer = 0

            With gudt.SetOpsPulldownMenuM
                For i = 0 To UBound(.udtDetail) Step 1
                    For j = 0 To UBound(.udtDetail(i).udtGroup) Step 1
                        For x = 0 To UBound(.udtDetail(i).udtGroup(j).udtSub) Step 1
                            If .udtDetail(i).udtGroup(j).udtSub(x).SubbytMenuType2 = 110 Then
                                'PROCSEE ITEM2 に「110」があればPIDコントロールがある
                                pstrTitle(intRet) = .udtDetail(i).udtGroup(j).udtSub(x).strName
                                intRet = intRet + 1
                            End If
                        Next x
                    Next j
                Next i
            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return intRet
    End Function
#End Region

End Module
