Public Class frmSysPrinter

#Region "変数定義"

    Private mudtSetSysPrinterNew As gTypSetSysPrinter
    'Private mudtSetSysPrinterOld As gTypSetSysPrinter

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

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ：
    ' 戻値      ：
    '----------------------------------------------------------------------------
    Private Sub frmSysPrinter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            ''コンボボックス初期設定
            Call gSetComboBox(cmbLogPrinter1, gEnmComboType.ctSysPrinterLogPrinter1)
            Call gSetComboBox(cmbLogPrinter2, gEnmComboType.ctSysPrinterLogPrinter2)
            Call gSetComboBox(cmbAlarmPrinter1, gEnmComboType.ctSysPrinterAlarmPrinter1)
            Call gSetComboBox(cmbAlarmPrinter2, gEnmComboType.ctSysPrinterAlarmPrinter2)
            Call gSetComboBox(cmbAlarmPrintType, gEnmComboType.ctSysPrinterPrinterType)
            Call gSetComboBox(cmbHCPrinter, gEnmComboType.ctSysPrinterHcPrinter)

            'Ver2.0.6.5
            Call gSetComboBox(cmbPrinterNameL1, gEnmComboType.ctSysPrinterPrinterName)
            Call gSetComboBox(cmbPrinterNameL2, gEnmComboType.ctSysPrinterPrinterName)
            Call gSetComboBox(cmbPrinterNameHC, gEnmComboType.ctSysPrinterPrinterName)

            ''構造体配列初期化
            mudtSetSysPrinterNew.InitArray()
            'For i As Integer = LBound(mudtSetSysPrinterNew.udtPrinterDetail) To UBound(mudtSetSysPrinterNew.udtPrinterDetail)
            '    mudtSetSysPrinterNew.udtPrinterDetail(i).InitArray()
            'Next

            ''画面設定
            Call mSetDisplay(gudt.SetSystem.udtSysPrinter)


            '■外販
            '外販の場合、IPアドレス固定、PrinterPart非表示、右側非表示
            If gintNaiGai = 1 Then
                'LP1 IP 192.168.61.41
                txtLogPrinterIP11.Text = "192"
                txtLogPrinterIP12.Text = "168"
                txtLogPrinterIP13.Text = "61"
                txtLogPrinterIP14.Text = "41"
                'LP2 IP 192.168.61.51
                txtLogPrinterIP21.Text = "192"
                txtLogPrinterIP22.Text = "168"
                txtLogPrinterIP23.Text = "61"
                txtLogPrinterIP24.Text = "51"
                'AP1 IP 192.168.61.40
                txtAlarmPrinterIP11.Text = "192"
                txtAlarmPrinterIP12.Text = "168"
                txtAlarmPrinterIP13.Text = "61"
                txtAlarmPrinterIP14.Text = "40"
                'AP2 IP 192.168.61.50
                txtAlarmPrinterIP21.Text = "192"
                txtAlarmPrinterIP22.Text = "168"
                txtAlarmPrinterIP23.Text = "61"
                txtAlarmPrinterIP24.Text = "50"
                'HC IP 192.168.61.61
                txtHcPrinterIP1.Text = "192"
                txtHcPrinterIP2.Text = "168"
                txtHcPrinterIP3.Text = "61"
                txtHcPrinterIP4.Text = "61"
                'PrinterPart
                Label12.Visible = False
                chkLogPrinter1Machinery.Visible = False
                chkLogPrinter1Cargo.Visible = False
                Label38.Visible = False
                chkLogPrinter2Machinery.Visible = False
                chkLogPrinter2Cargo.Visible = False
                Label4.Visible = False
                chkHCPrinterMachinery.Visible = False
                chkHCPrinterCargo.Visible = False
                Label11.Visible = False
                chkAlarmPrinter1Machinery.Visible = False
                chkAlarmPrinter1Cargo.Visible = False
                Label42.Visible = False
                chkAlarmPrinter2Machinery.Visible = False
                chkAlarmPrinter2Cargo.Visible = False
                '右側
                chkNoonLog.Visible = False
                chkDemandLog.Visible = False
                chkMachineryCargoPrint.Visible = False
                Label17.Visible = False
                txtAutoCnt.Visible = False
            End If

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

            ''入力チェック
            If Not mChkInput() Then Return

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetSysPrinterNew)

            ''データが変更されているかチェック
            'If (Not mudtSetSysPrinterNew.Equals(mudtSetSysPrinterOld)) Then
            If Not mChkStructureEquals(mudtSetSysPrinterNew, gudt.SetSystem.udtSysPrinter) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetSysPrinterNew, gudt.SetSystem.udtSysPrinter)

                '■外販
                '外販の場合
                '　ログプリンタが無い場合、SYSTEMのLOG TIME SETTINGは消す
                If gintNaiGai = 1 Then
                    'ﾒﾆｭｰのSYSTEMをﾃﾞﾌｫﾙﾄコピー
                    Call subSetDefaultValue("SYSTEM")

                    gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainM = 1
                    gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainM = 1
                    gudt.SetEditorUpdateInfo.udtSave.bytOpsManuMainC = 1
                    gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainC = 1


                    With gudt.SetSystem.udtSysPrinter
                        If .udtPrinterDetail(0).bytPrinter = 0 And _
                            .udtPrinterDetail(1).bytPrinter = 0 And _
                            .udtPrinterDetail(2).bytPrinter = 0 And _
                            .udtPrinterDetail(3).bytPrinter = 0 Then

                            'SYSTEMの場所を指定
                            Dim intTable As Integer = -1
                            Dim strName As String = ""
                            With gudt.SetOpsPulldownMenuM
                                For i As Integer = 0 To UBound(.udtDetail) Step 1
                                    '名称から0x00を取り除く
                                    strName = .udtDetail(i).strName.Replace(Chr(&H0), Chr(&H20)).Trim
                                    If strName = "SYSTEM" Then
                                        '名称を比較し、一致すればその場所を格納
                                        intTable = i
                                        Exit For
                                    End If
                                Next i
                            End With
                            'LOG TIME SETTIGNを消す
                            Dim intBasyo As Integer = -1
                            For i As Integer = 0 To UBound(gudt.SetOpsPulldownMenuM.udtDetail(intTable).udtGroup(1).udtSub) Step 1
                                With gudt.SetOpsPulldownMenuM.udtDetail(intTable).udtGroup(1)
                                    If .udtSub(i).ViewNo1 = data_exchange(93) Then
                                        intBasyo = i
                                        Exit For
                                    End If
                                End With
                            Next i
                            If intBasyo <> -1 Then
                                With gudt.SetOpsPulldownMenuM.udtDetail(intTable).udtGroup(1)
                                    .bytCount = .bytCount - 1
                                    For i = intBasyo To UBound(.udtSub) - 1 Step 1
                                        .udtSub(i).strName = .udtSub(i + 1).strName
                                        .udtSub(i).SubbytMenuType1 = .udtSub(i + 1).SubbytMenuType1
                                        .udtSub(i).SubbytMenuType2 = .udtSub(i + 1).SubbytMenuType2
                                        .udtSub(i).SubbytMenuType3 = .udtSub(i + 1).SubbytMenuType3
                                        .udtSub(i).SubbytMenuType4 = .udtSub(i + 1).SubbytMenuType4
                                        .udtSub(i).SubYobi1 = .udtSub(i + 1).SubYobi1
                                        .udtSub(i).SubYobi2 = .udtSub(i + 1).SubYobi2
                                        .udtSub(i).bytKeyCode = .udtSub(i + 1).bytKeyCode
                                        .udtSub(i).SubYobi4 = .udtSub(i + 1).SubYobi4
                                        '
                                        .udtSub(i).ViewNo1 = .udtSub(i + 1).ViewNo1
                                        .udtSub(i).ViewNo2 = .udtSub(i + 1).ViewNo2
                                        .udtSub(i).ViewNo3 = .udtSub(i + 1).ViewNo3
                                        .udtSub(i).ViewNo4 = .udtSub(i + 1).ViewNo4
                                        .udtSub(i).SubMenutx = .udtSub(i + 1).SubMenutx
                                        .udtSub(i).SubMenuty = .udtSub(i + 1).SubMenuty
                                        .udtSub(i).SubMenubx = .udtSub(i + 1).SubMenubx
                                        .udtSub(i).SubMenuby = .udtSub(i + 1).SubMenuby
                                    Next i
                                End With
                            End If
                        End If
                    End With
                End If

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSystem = 1

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "■外販 メニュー編集で使用する"
    '該当ﾃﾞﾌｫﾙﾄ値をファイルから読み込んでｺﾋﾟｰ
    Private Sub subSetDefaultValue(pstrName As String)
        Try
            Dim strName As String = ""
            Dim i As Integer = 0

            Dim mudtDefOPS As gTypSetOpsPulldownMenu = Nothing
            Dim mudtDefVDU As gTypSetOpsPulldownMenu = Nothing
            'デフォルトファイルを読み込む
            Call gLoadMenuMain(mudtDefOPS)
            Call gLoadMenuMain(mudtDefVDU, True)

            '該当変数の場所を指定
            Dim intTable As Integer = -1
            With mudtDefOPS
                For i = 0 To UBound(.udtDetail) Step 1
                    '名称から0x00を取り除く
                    strName = .udtDetail(i).strName.Replace(Chr(&H0), Chr(&H20)).Trim
                    If strName = pstrName Then
                        '名称を比較し、一致すればその場所を格納
                        intTable = i
                        Exit For
                    End If
                Next i
            End With

            '該当値をｺﾋﾟｰ
            If intTable <> -1 Then
                'OPS
                Call mCopyDefTable(mudtDefOPS, gudt.SetOpsPulldownMenuM, intTable)
                'VDU
                Call mCopyDefTable(mudtDefVDU, gudt.SetOpsPulldownMenuC, intTable)
            End If
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    'テーブルコピー（subSetDefaultValue関数から呼ばれるのみ）
    Private Sub mCopyDefTable(ByVal udtSource As gTypSetOpsPulldownMenu, _
                               ByRef udtTarget As gTypSetOpsPulldownMenu, _
                               pintTable As Integer)

        Try
            Dim i As Integer = pintTable

            ''メインメニュー
            udtTarget.udtDetail(i).strName = udtSource.udtDetail(i).strName             ''メインメニュー名称
            udtTarget.udtDetail(i).tx = udtSource.udtDetail(i).tx                       ''メインメニューの動作開始地点（左上X座標）
            udtTarget.udtDetail(i).ty = udtSource.udtDetail(i).ty                       ''メインメニューの動作開始地点（左上Y座標)
            udtTarget.udtDetail(i).bx = udtSource.udtDetail(i).bx                       ''メインメニューの動作開始地点（右下X座標）
            udtTarget.udtDetail(i).by = udtSource.udtDetail(i).by                       ''メインメニューの動作開始地点（右下Y座標）
            udtTarget.udtDetail(i).OPSSTFLG1 = udtSource.udtDetail(i).OPSSTFLG1         ''OPS禁止フラグ
            udtTarget.udtDetail(i).OPSSTFLG2 = udtSource.udtDetail(i).OPSSTFLG2         ''OPS禁止フラグ
            udtTarget.udtDetail(i).Spare1 = udtSource.udtDetail(i).Spare1               ''予備1
            udtTarget.udtDetail(i).Spare2 = udtSource.udtDetail(i).Spare2               ''予備2
            udtTarget.udtDetail(i).Spare3 = udtSource.udtDetail(i).Spare3               ''予備3
            udtTarget.udtDetail(i).Spare4 = udtSource.udtDetail(i).Spare4               ''予備4
            udtTarget.udtDetail(i).Spare5 = udtSource.udtDetail(i).Spare5               ''予備5
            udtTarget.udtDetail(i).bytMenuType = udtSource.udtDetail(i).bytMenuType     ''メニュータイプ
            udtTarget.udtDetail(i).Yobi1 = udtSource.udtDetail(i).Yobi1                 ''セレクトされているグループメニュー番号(未使用)
            udtTarget.udtDetail(i).Yobi2 = udtSource.udtDetail(i).Yobi2                 ''セレクトされているグループメニュー番号(保持型)(未使用)
            udtTarget.udtDetail(i).bytMenuSet = udtSource.udtDetail(i).bytMenuSet       ''グループメニューセット数
            udtTarget.udtDetail(i).groupviewx = udtSource.udtDetail(i).groupviewx       ''グループメニューの表示位置X
            udtTarget.udtDetail(i).groupviewy = udtSource.udtDetail(i).groupviewy       ''グループメニューの表示位置Y
            udtTarget.udtDetail(i).groupsizex = udtSource.udtDetail(i).groupsizex       ''グループメニューの横サイズ位置
            udtTarget.udtDetail(i).groupsizey = udtSource.udtDetail(i).groupsizey       ''グループメニューの縦サイズ位置

            ''サブグループ
            For j As Integer = 0 To UBound(udtSource.udtDetail(i).udtGroup)
                udtTarget.udtDetail(i).udtGroup(j).strName = udtSource.udtDetail(i).udtGroup(j).strName                     ''サブメニュー名称
                udtTarget.udtDetail(i).udtGroup(j).grouptx = udtSource.udtDetail(i).udtGroup(j).grouptx                     ''グループメニューの動作開始地点（左上X座標）
                udtTarget.udtDetail(i).udtGroup(j).groupty = udtSource.udtDetail(i).udtGroup(j).groupty                     ''グループメニューの動作開始地点（左上Y座標)
                udtTarget.udtDetail(i).udtGroup(j).groupbx = udtSource.udtDetail(i).udtGroup(j).groupbx                     ''グループメニューの動作開始地点（右下X座標）
                udtTarget.udtDetail(i).udtGroup(j).groupby = udtSource.udtDetail(i).udtGroup(j).groupby                     ''グループメニューの動作開始地点（右下Y座標）
                udtTarget.udtDetail(i).udtGroup(j).groupSpare1 = udtSource.udtDetail(i).udtGroup(j).groupSpare1             ''予備1
                udtTarget.udtDetail(i).udtGroup(j).groupSpare2 = udtSource.udtDetail(i).udtGroup(j).groupSpare2             ''予備2
                udtTarget.udtDetail(i).udtGroup(j).groupSpare3 = udtSource.udtDetail(i).udtGroup(j).groupSpare3             ''予備3
                udtTarget.udtDetail(i).udtGroup(j).groupSpare4 = udtSource.udtDetail(i).udtGroup(j).groupSpare4             ''予備4
                udtTarget.udtDetail(i).udtGroup(j).groupbytMenuType = udtSource.udtDetail(i).udtGroup(j).groupbytMenuType   ''メニュータイプ(処理項目・未使用))
                udtTarget.udtDetail(i).udtGroup(j).SubgroupYobi1 = udtSource.udtDetail(i).udtGroup(j).SubgroupYobi1         ''セレクトされているサブメニュー番号(未使用)
                udtTarget.udtDetail(i).udtGroup(j).SubgroupYobi2 = udtSource.udtDetail(i).udtGroup(j).SubgroupYobi2         ''セレクトされているサブメニュー番号(保持型)(未使用)
                udtTarget.udtDetail(i).udtGroup(j).bytCount = udtSource.udtDetail(i).udtGroup(j).bytCount                   ''サブメニュー数
                udtTarget.udtDetail(i).udtGroup(j).Subviewx = udtSource.udtDetail(i).udtGroup(j).Subviewx                   ''サブメニューの表示位置X
                udtTarget.udtDetail(i).udtGroup(j).Subviewy = udtSource.udtDetail(i).udtGroup(j).Subviewy                   ''サブメニューの表示位置Y
                udtTarget.udtDetail(i).udtGroup(j).Subsizex = udtSource.udtDetail(i).udtGroup(j).Subsizex                   ''サブメニューの横サイズ位置
                udtTarget.udtDetail(i).udtGroup(j).Subsizey = udtSource.udtDetail(i).udtGroup(j).Subsizey                   ''サブメニューの縦サイズ位置


                ''サブメニュー
                For k As Integer = 0 To UBound(udtSource.udtDetail(i).udtGroup(j).udtSub)
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).strName = udtSource.udtDetail(i).udtGroup(j).udtSub(k).strName                         ''サブメニュー名称
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType1         ''メニュータイプ(Bラベル処理項目1)
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType2         ''メニュータイプ(Bラベル処理項目2)
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType3         ''メニュータイプ(Bラベル処理項目3)
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubbytMenuType4         ''メニュータイプ(Bラベル処理項目4)
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubYobi1 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubYobi1                       ''画面モード（未使用）
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubYobi2 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubYobi2                       ''現在操作可能な画面の表示位置（未使用）
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode = udtSource.udtDetail(i).udtGroup(j).udtSub(k).bytKeyCode                   ''キーコード
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubYobi4 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubYobi4                       ''予備

                    '画面設定数の反転
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1          ''画面番号0(データを反転する）
                    'udtTarget.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1 = DataExchange(gGet2Byte(udtSource.udtDetail(i).udtGroup(j).udtSub(k).ViewNo1))           ''画面番号0(データを反転する）
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).ViewNo2                         ''画面番号1（未使用）
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).ViewNo3                         ''画面番号2（未使用）
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4 = udtSource.udtDetail(i).udtGroup(j).udtSub(k).ViewNo4                         ''画面番号3（未使用）
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubMenutx                     ''サブメニューの動作開始地点（左上X座標）
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubMenuty                     ''サブメニューの動作開始地点（左上Y座標)
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubMenubx                     ''サブメニューの動作開始地点（右下X座標）
                    udtTarget.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby = udtSource.udtDetail(i).udtGroup(j).udtSub(k).SubMenuby                     ''サブメニューの動作開始地点（右下Y座標）
                Next k

            Next j

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region



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
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetSysPrinterNew)

            ''データが変更されているかチェック
            'If (Not mudtSetSysPrinterNew.Equals(mudtSetSysPrinterOld)) Then
            If Not mChkStructureEquals(mudtSetSysPrinterNew, gudt.SetSystem.udtSysPrinter) Then

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
                        Call mCopyStructure(mudtSetSysPrinterNew, gudt.SetSystem.udtSysPrinter)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytSystem = 1

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

    'Ver2.0.6.5 PrinterNameを選択すると該当するDriver,Deviceが入る処理
    Private Sub cmbPrinterNameL1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbPrinterNameL1.SelectedIndexChanged
        Try
            Dim strRet As String = ""
            Dim strSplit() As String = Nothing
            With cmbPrinterNameL1
                '0以上なら、入力補助
                If .SelectedValue > 0 Then
                    strRet = gDriverDeviceChar(.SelectedValue)
                End If

                If strRet <> "" Then
                    strSplit = strRet.Split(",")
                    If strSplit.Length >= 2 Then
                        txtLogPrinter1Driver.Text = strSplit(0)
                        txtLogPrinter1Device.Text = strSplit(1)
                    End If
                End If
            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub cmbPrinterNameL2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbPrinterNameL2.SelectedIndexChanged
        Try
            Dim strRet As String = ""
            Dim strSplit() As String = Nothing
            With cmbPrinterNameL2
                '0以上なら、入力補助
                If .SelectedValue > 0 Then
                    strRet = gDriverDeviceChar(.SelectedValue)
                End If

                If strRet <> "" Then
                    strSplit = strRet.Split(",")
                    If strSplit.Length >= 2 Then
                        txtLogPrinter2Driver.Text = strSplit(0)
                        txtLogPrinter2Device.Text = strSplit(1)
                    End If
                End If
            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    Private Sub cmbPrinterNameHC_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbPrinterNameHC.SelectedIndexChanged
        Try
            Dim strRet As String = ""
            Dim strSplit() As String = Nothing
            With cmbPrinterNameHC
                '0以上なら、入力補助
                If .SelectedValue > 0 Then
                    strRet = gDriverDeviceChar(.SelectedValue)
                End If

                If strRet <> "" Then
                    strSplit = strRet.Split(",")
                    If strSplit.Length >= 2 Then
                        txtHCPrinterDriver.Text = strSplit(0)
                        txtHCPrinterDevice.Text = strSplit(1)
                    End If
                End If
            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#Region "入力関連"

#Region "入力制限"

    '----------------------------------------------------------------------------
    ' 機能説明  ： IP KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtIP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtLogPrinterIP11.KeyPress, txtLogPrinterIP12.KeyPress, txtLogPrinterIP13.KeyPress, txtLogPrinterIP14.KeyPress, _
        txtLogPrinterIP21.KeyPress, txtLogPrinterIP22.KeyPress, txtLogPrinterIP23.KeyPress, txtLogPrinterIP24.KeyPress, _
        txtAlarmPrinterIP11.KeyPress, txtAlarmPrinterIP12.KeyPress, txtAlarmPrinterIP13.KeyPress, txtAlarmPrinterIP14.KeyPress, _
        txtAlarmPrinterIP21.KeyPress, txtAlarmPrinterIP22.KeyPress, txtAlarmPrinterIP23.KeyPress, txtAlarmPrinterIP24.KeyPress, _
        txtHcPrinterIP1.KeyPress, txtHcPrinterIP2.KeyPress, txtHcPrinterIP3.KeyPress, txtHcPrinterIP4.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Driver Maker KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtDriver_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtLogPrinter1Driver.KeyPress, _
        txtLogPrinter2Driver.KeyPress, _
        txtAlarmPrinter1Driver.KeyPress, _
        txtAlarmPrinter2Driver.KeyPress, _
        txtHCPrinterDriver.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Driver Maker KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtMaker_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
        txtLogPrinter1Device.KeyPress, _
        txtLogPrinter2Device.KeyPress, _
        txtAlarmPrinter1Device.KeyPress, _
        txtAlarmPrinter2Device.KeyPress, _
        txtHCPrinterDevice.KeyPress

        Try

            e.Handled = gCheckTextInput(32, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtAutoCnt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAutoCnt.KeyPress

        Try
            ' 2013.07.22 自動印字最大数追加  K.Fujimoto
            e.Handled = gCheckTextInput(3, sender, e.KeyChar)
            'e.Handled = gCheckTextInput(3, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "入力チェック"

    ''----------------------------------------------------------------------------
    '' 機能説明  ： CH No. 入力チェック
    '' 引数      ： なし
    '' 戻値      ： なし
    ''----------------------------------------------------------------------------
    'Private Sub txtIP_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _
    '    txtLogPrinterIP11.Validating, txtLogPrinterIP12.Validating, txtLogPrinterIP13.Validating, txtLogPrinterIP14.Validating, _
    '    txtLogPrinterIP21.Validating, txtLogPrinterIP22.Validating, txtLogPrinterIP23.Validating, txtLogPrinterIP24.Validating, _
    '    txtAlarmPrinterIP11.Validating, txtAlarmPrinterIP12.Validating, txtAlarmPrinterIP13.Validating, txtAlarmPrinterIP14.Validating, _
    '    txtAlarmPrinterIP21.Validating, txtAlarmPrinterIP22.Validating, txtAlarmPrinterIP23.Validating, txtAlarmPrinterIP24.Validating, _
    '    txtHcPrinterIP1.Validating, txtHcPrinterIP2.Validating, txtHcPrinterIP3.Validating, txtHcPrinterIP4.Validating

    '    e.Cancel = gChkTextNumSpan(0, 255, sender.Text)

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

            '=============
            ''IPAddress
            '=============
            ''共通数値入力チェック
            If Not mChkInputIP(txtLogPrinterIP11, txtLogPrinterIP12, txtLogPrinterIP13, txtLogPrinterIP14, "LogPrinter1 IP") Then Return False
            If Not mChkInputIP(txtLogPrinterIP21, txtLogPrinterIP22, txtLogPrinterIP23, txtLogPrinterIP24, "LogPrinter2 IP") Then Return False
            If Not mChkInputIP(txtAlarmPrinterIP11, txtAlarmPrinterIP12, txtAlarmPrinterIP13, txtAlarmPrinterIP14, "AlarmPrinter1 IP") Then Return False
            If Not mChkInputIP(txtAlarmPrinterIP21, txtAlarmPrinterIP22, txtAlarmPrinterIP23, txtAlarmPrinterIP24, "AlarmPrinter2 IP") Then Return False
            If Not mChkInputIP(txtHcPrinterIP1, txtHcPrinterIP2, txtHcPrinterIP3, txtHcPrinterIP4, "HcPrinter IP") Then Return False

            '=============
            ''Driver,Maker
            '=============
            ''共通テキスト入力チェック
            If Not gChkInputText(txtLogPrinter1Driver, "LogPrinterDriver1", True, True) Then Return False
            If Not gChkInputText(txtLogPrinter2Driver, "LogPrinterDriver2", True, True) Then Return False
            If Not gChkInputText(txtLogPrinter1Device, "LogPrinterDriver1", True, True) Then Return False
            If Not gChkInputText(txtLogPrinter2Device, "LogPrinterDriver2", True, True) Then Return False
            If Not gChkInputText(txtAlarmPrinter1Driver, "AlarmPrinterDriver1", True, True) Then Return False
            If Not gChkInputText(txtAlarmPrinter2Driver, "AlarmPrinterDriver2", True, True) Then Return False
            If Not gChkInputText(txtAlarmPrinter1Device, "AlarmPrinterDriver1", True, True) Then Return False
            If Not gChkInputText(txtAlarmPrinter2Device, "AlarmPrinterDriver2", True, True) Then Return False
            If Not gChkInputText(txtHCPrinterDriver, "HCPrinterDriver1", True, True) Then Return False
            If Not gChkInputText(txtHCPrinterDevice, "HCPrinterDriver2", True, True) Then Return False

            ' 2013.07.22 自動印字最大数追加  K.Fujimoto
            If Not gChkInputNum(txtAutoCnt, 1, 100, "Auto Print Events per page", True, True) Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    Private Function mChkInputIP(ByRef txt1 As TextBox, _
                                 ByRef txt2 As TextBox, _
                                 ByRef txt3 As TextBox, _
                                 ByRef txt4 As TextBox, _
                                 ByVal strName As String) As Boolean

        Try

            ''全て入力なしの場合は入力OK
            If Trim(txt1.Text) = "" And _
               Trim(txt2.Text) = "" And _
               Trim(txt3.Text) = "" And _
               Trim(txt4.Text) = "" Then

                Return True

            Else

                ''共通数値入力チェック
                If Not gChkInputNum(txt1, 0, 255, strName & "1", False, True) Then Return False
                If Not gChkInputNum(txt2, 0, 255, strName & "2", False, True) Then Return False
                If Not gChkInputNum(txt3, 0, 255, strName & "3", False, True) Then Return False
                If Not gChkInputNum(txt4, 0, 255, strName & "4", False, True) Then Return False

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetSysPrinter)

        Try

            With mudtSetSysPrinterNew

                '' 2013.07.22 自動印字最大数追加  K.Fujimoto
                .shtAutoCnt = CCUInt16(txtAutoCnt.Text)

                'Ver2.0.6.4 Systemの言語から判定するように変更 英語=1,日本語=3
                ''英数・日本語設定
                '.shtPrintType = cmbAlarmPrintType.SelectedValue
                Select Case gudt.SetSystem.udtSysSystem.shtLanguage
                    Case 0
                        '英語
                        .shtPrintType = 1
                    Case 1
                        '日本語
                        .shtPrintType = 3
                    Case Else
                        .shtPrintType = 0
                End Select


                ''イベントプリント
                .shtEventPrint = IIf(chkEventPrintNone.Checked, 1, 0)

                ''ヌーンログ下線
                .shtNoonUnder = IIf(chkNoonLog.Checked, 1, 0)

                ''デマンドログ改ページ
                .shtDemandPage = IIf(chkDemandLog.Checked, 1, 0)

                ''Machinery/Cargo印字
                .shtMachineryCargoPrint = IIf(chkMachineryCargoPrint.Checked, 1, 0)

                '' 自動印字する場合のデータ個数(Cargo)2019.02.06
                .shtAutoCntCargo = .shtAutoCnt

                ''ログプリンタ１
                With .udtPrinterDetail(0)
                    .bytPrinter = cmbLogPrinter1.SelectedValue                                                      ''プリンタ有無
                    ''プリンタ有無により自動設定　ver.1.4.0 2011.09.21
                    ''無し以外は印字有りとする
                    .shtPrintUse = gBitSet(.shtPrintUse, 0, IIf(.bytPrinter <> 0, True, False))                     ''印字有無（通常）
                    .shtPrintUse = gBitSet(.shtPrintUse, 1, IIf(chkLogPrinter1EnableBackup.Checked, True, False))   ''印字有無（バックアップ）
                    .shtPrintUse = gBitSet(.shtPrintUse, 2, IIf(chkLogPrinter1PaperSizeA3.Checked, True, False))    ''印字用紙サイズA3  2011.12.13 K.Tanigawa
                    .shtPrintUse = gBitSet(.shtPrintUse, 3, IIf(chkLogPrinter1Color.Checked, True, False))          ''カラー選択 T.Ueki
                    .shtPart = gBitSet(.shtPart, 0, IIf(chkLogPrinter1Machinery.Checked, True, False))              ''印字パート（Machinery）
                    .shtPart = gBitSet(.shtPart, 1, IIf(chkLogPrinter1Cargo.Checked, True, False))                  ''印字パート（Cargo）
                    .strDriver = txtLogPrinter1Driver.Text                                                          ''ドライバ
                    .strDevice = txtLogPrinter1Device.Text                                                          ''デバイス
                    .bytIP1 = CCbyte(txtLogPrinterIP11.Text)                                                        ''IPアドレス
                    .bytIP2 = CCbyte(txtLogPrinterIP12.Text)                                                        ''IPアドレス
                    .bytIP3 = CCbyte(txtLogPrinterIP13.Text)                                                        ''IPアドレス
                    .bytIP4 = CCbyte(txtLogPrinterIP14.Text)                                                        ''IPアドレス

                End With

                ''ログプリンタ２
                With .udtPrinterDetail(1)
                    .bytPrinter = cmbLogPrinter2.SelectedValue                                                      ''プリンタ有無
                    ''プリンタ有無により自動設定　ver.1.4.0 2011.09.21
                    ''無し以外は印字有りとする
                    .shtPrintUse = gBitSet(.shtPrintUse, 0, IIf(.bytPrinter <> 0, True, False))                     ''印字有無（通常）
                    .shtPrintUse = gBitSet(.shtPrintUse, 1, IIf(chkLogPrinter2EnableBackup.Checked, True, False))   ''印字有無（バックアップ）
                    .shtPrintUse = gBitSet(.shtPrintUse, 2, IIf(chkLogPrinter2PaperSizeA3.Checked, True, False))         ''印字用紙カラー  2011.12.13 K.Tanigawa　
                    .shtPrintUse = gBitSet(.shtPrintUse, 3, IIf(chkLogPrinter2Color.Checked, True, False))          ''カラー選択 T.Ueki
                    .shtPart = gBitSet(.shtPart, 0, IIf(chkLogPrinter2Machinery.Checked, True, False))              ''印字パート（Machinery）
                    .shtPart = gBitSet(.shtPart, 1, IIf(chkLogPrinter2Cargo.Checked, True, False))                  ''印字パート（Cargo）
                    .strDriver = txtLogPrinter2Driver.Text                                                          ''ドライバ
                    .strDevice = txtLogPrinter2Device.Text                                                           ''デバイス
                    .bytIP1 = CCbyte(txtLogPrinterIP21.Text)                                                        ''IPアドレス
                    .bytIP2 = CCbyte(txtLogPrinterIP22.Text)                                                        ''IPアドレス
                    .bytIP3 = CCbyte(txtLogPrinterIP23.Text)                                                        ''IPアドレス
                    .bytIP4 = CCbyte(txtLogPrinterIP24.Text)                                                        ''IPアドレス
                End With

                ''アラームプリンタ１
                With .udtPrinterDetail(2)
                    .bytPrinter = cmbAlarmPrinter1.SelectedValue                                                    ''プリンタ有無
                    ''プリンタ有無により自動設定　ver.1.4.0 2011.09.21
                    ''無し以外は印字有りとする
                    .shtPrintUse = gBitSet(.shtPrintUse, 0, IIf(.bytPrinter <> 0, True, False))                     ''印字有無（通常）
                    .shtPrintUse = gBitSet(.shtPrintUse, 1, IIf(chkAlarmPrinter1EnableBackup.Checked, True, False)) ''印字有無（バックアップ）
                    .shtPart = gBitSet(.shtPart, 0, IIf(chkAlarmPrinter1Machinery.Checked, True, False))                  ''印字パート（Machinery）
                    .shtPart = gBitSet(.shtPart, 1, IIf(chkAlarmPrinter1Cargo.Checked, True, False))                ''印字パート（Cargo）
                    .strDriver = txtAlarmPrinter1Driver.Text                                                        ''ドライバ
                    .strDevice = txtAlarmPrinter1Device.Text                                                         ''デバイス
                    .bytIP1 = CCbyte(txtAlarmPrinterIP11.Text)                                                      ''IPアドレス
                    .bytIP2 = CCbyte(txtAlarmPrinterIP12.Text)                                                      ''IPアドレス
                    .bytIP3 = CCbyte(txtAlarmPrinterIP13.Text)                                                      ''IPアドレス
                    .bytIP4 = CCbyte(txtAlarmPrinterIP14.Text)                                                      ''IPアドレス
                End With

                ''アラームプリンタ２
                With .udtPrinterDetail(3)
                    .bytPrinter = cmbAlarmPrinter2.SelectedValue                                                    ''プリンタ有無
                    ''プリンタ有無により自動設定　ver.1.4.0 2011.09.21
                    ''無し以外は印字有りとする
                    .shtPrintUse = gBitSet(.shtPrintUse, 0, IIf(.bytPrinter <> 0, True, False))                     ''印字有無（通常）
                    .shtPrintUse = gBitSet(.shtPrintUse, 1, IIf(chkAlarmPrinter2EnableBackup.Checked, True, False)) ''印字有無（バックアップ）
                    .shtPart = gBitSet(.shtPart, 0, IIf(chkAlarmPrinter2Machinery.Checked, True, False))                  ''印字パート（Machinery）
                    .shtPart = gBitSet(.shtPart, 1, IIf(chkAlarmPrinter2Cargo.Checked, True, False))                ''印字パート（Cargo）
                    .strDriver = txtAlarmPrinter2Driver.Text                                                        ''ドライバ
                    .strDevice = txtAlarmPrinter2Device.Text                                                         ''デバイス
                    .bytIP1 = CCbyte(txtAlarmPrinterIP21.Text)                                                      ''IPアドレス
                    .bytIP2 = CCbyte(txtAlarmPrinterIP22.Text)                                                      ''IPアドレス
                    .bytIP3 = CCbyte(txtAlarmPrinterIP23.Text)                                                      ''IPアドレス
                    .bytIP4 = CCbyte(txtAlarmPrinterIP24.Text)                                                      ''IPアドレス
                End With

                ''HCプリンタ
                With .udtPrinterDetail(4)
                    .bytPrinter = cmbHCPrinter.SelectedValue                                                        ''プリンタ有無
                    ''プリンタ有無により自動設定　ver.1.4.0 2011.09.21
                    ''無し以外は印字有りとする
                    .shtPrintUse = gBitSet(.shtPrintUse, 0, IIf(.bytPrinter <> 0, True, False))                     ''印字有無（通常）
                    .shtPrintUse = gBitSet(.shtPrintUse, 1, IIf(chkHcPrinter1EnableBackup.Checked, True, False))    ''印字有無（バックアップ）
                    .shtPrintUse = gBitSet(.shtPrintUse, 2, IIf(chkHCPrinter1PaperSizeA3.Checked, True, False))    ''印字用紙サイズA3  2011.12.13 K.Tanigawa
                    .shtPart = gBitSet(.shtPart, 0, IIf(chkHCPrinterMachinery.Checked, True, False))                      ''印字パート（Machinery）
                    .shtPart = gBitSet(.shtPart, 1, IIf(chkHCPrinterCargo.Checked, True, False))                    ''印字パート（Cargo）
                    .strDriver = txtHCPrinterDriver.Text                                                            ''ドライバ
                    .strDevice = txtHCPrinterDevice.Text                                                             ''デバイス
                    .bytIP1 = CCbyte(txtHcPrinterIP1.Text)                                                          ''IPアドレス
                    .bytIP2 = CCbyte(txtHcPrinterIP2.Text)                                                          ''IPアドレス
                    .bytIP3 = CCbyte(txtHcPrinterIP3.Text)                                                          ''IPアドレス
                    .bytIP4 = CCbyte(txtHcPrinterIP4.Text)                                                          ''IPアドレス
                End With

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) システム設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetSysPrinter)

        Try

            With udtSet

                ' 2013.07.22 自動印字最大数追加  K.Fujimoto
                txtAutoCnt.Text = .shtAutoCnt

                ''英数・日本語設定
                cmbAlarmPrintType.SelectedValue = .shtPrintType

                ''イベントプリント
                chkEventPrintNone.Checked = IIf(.shtEventPrint = 1, True, False)

                ''ヌーンログ下線
                chkNoonLog.Checked = IIf(.shtNoonUnder = 1, True, False)

                ''デマンドログ改ページ
                chkDemandLog.Checked = IIf(.shtDemandPage = 1, True, False)

                ''Machinery/Cargo印字
                chkMachineryCargoPrint.Checked = IIf(.shtMachineryCargoPrint = 1, True, False)

                ''ログプリンタ１
                With .udtPrinterDetail(0)
                    cmbLogPrinter1.SelectedValue = .bytPrinter                                              ''プリンタ有無
                    chkLogPrinter1EnableBackup.Checked = IIf(gBitCheck(.shtPrintUse, 1), True, False)       ''印字有無（バックアップ）
                    chkLogPrinter1PaperSizeA3.Checked = IIf(gBitCheck(.shtPrintUse, 2), True, False)        ''ページサイズA3 2011.12.13 K.Tanigawa
                    chkLogPrinter1Color.Checked = IIf(gBitCheck(.shtPrintUse, 3), True, False)
                    chkLogPrinter1Machinery.Checked = IIf(gBitCheck(.shtPart, 0), True, False)              ''印字パート（Machinery）
                    chkLogPrinter1Cargo.Checked = IIf(gBitCheck(.shtPart, 1), True, False)                  ''印字パート（Cargo）
                    txtLogPrinter1Driver.Text = .strDriver                                                  ''ドライバ
                    txtLogPrinter1Device.Text = .strDevice                                                   ''デバイス
                    txtLogPrinterIP11.Text = .bytIP1                                                        ''IPアドレス
                    txtLogPrinterIP12.Text = .bytIP2                                                        ''IPアドレス
                    txtLogPrinterIP13.Text = .bytIP3                                                        ''IPアドレス
                    txtLogPrinterIP14.Text = .bytIP4                                                        ''IPアドレス
                End With

                ''ログプリンタ２
                With .udtPrinterDetail(1)
                    cmbLogPrinter2.SelectedValue = .bytPrinter                                              ''プリンタ有無
                    chkLogPrinter2EnableBackup.Checked = IIf(gBitCheck(.shtPrintUse, 1), True, False)       ''印字有無（バックアップ）
                    chkLogPrinter2PaperSizeA3.Checked = IIf(gBitCheck(.shtPrintUse, 2), True, False)        ''ページサイズA3 2011.12.13 K.Tanigawa
                    chkLogPrinter2Color.Checked = IIf(gBitCheck(.shtPrintUse, 3), True, False)
                    chkLogPrinter2Machinery.Checked = IIf(gBitCheck(.shtPart, 0), True, False)              ''印字パート（Machinery）
                    chkLogPrinter2Cargo.Checked = IIf(gBitCheck(.shtPart, 1), True, False)                  ''印字パート（Cargo）
                    txtLogPrinter2Driver.Text = .strDriver                                                  ''ドライバ
                    txtLogPrinter2Device.Text = .strDevice                                                   ''デバイス
                    txtLogPrinterIP21.Text = .bytIP1                                                        ''IPアドレス
                    txtLogPrinterIP22.Text = .bytIP2                                                        ''IPアドレス
                    txtLogPrinterIP23.Text = .bytIP3                                                        ''IPアドレス
                    txtLogPrinterIP24.Text = .bytIP4                                                        ''IPアドレス
                End With

                ''アラームプリンタ１
                With .udtPrinterDetail(2)
                    cmbAlarmPrinter1.SelectedValue = .bytPrinter                                            ''プリンタ有無
                    chkAlarmPrinter1EnableBackup.Checked = IIf(gBitCheck(.shtPrintUse, 1), True, False)     ''印字有無（バックアップ）
                    chkAlarmPrinter1Machinery.Checked = IIf(gBitCheck(.shtPart, 0), True, False)                  ''印字パート（Machinery）
                    chkAlarmPrinter1Cargo.Checked = IIf(gBitCheck(.shtPart, 1), True, False)                ''印字パート（Cargo）
                    txtAlarmPrinter1Driver.Text = .strDriver                                                ''ドライバ
                    txtAlarmPrinter1Device.Text = .strDevice                                                 ''デバイス
                    txtAlarmPrinterIP11.Text = .bytIP1                                                      ''IPアドレス
                    txtAlarmPrinterIP12.Text = .bytIP2                                                      ''IPアドレス
                    txtAlarmPrinterIP13.Text = .bytIP3                                                      ''IPアドレス
                    txtAlarmPrinterIP14.Text = .bytIP4                                                      ''IPアドレス
                End With

                ''アラームプリンタ２
                With .udtPrinterDetail(3)
                    cmbAlarmPrinter2.SelectedValue = .bytPrinter                                            ''プリンタ有無
                    chkAlarmPrinter2EnableBackup.Checked = IIf(gBitCheck(.shtPrintUse, 1), True, False)     ''印字有無（バックアップ）
                    chkAlarmPrinter2Machinery.Checked = IIf(gBitCheck(.shtPart, 0), True, False)                  ''印字パート（Machinery）
                    chkAlarmPrinter2Cargo.Checked = IIf(gBitCheck(.shtPart, 1), True, False)                ''印字パート（Cargo）
                    txtAlarmPrinter2Driver.Text = .strDriver                                                ''ドライバ
                    txtAlarmPrinter2Device.Text = .strDevice                                                 ''デバイス
                    txtAlarmPrinterIP21.Text = .bytIP1                                                      ''IPアドレス
                    txtAlarmPrinterIP22.Text = .bytIP2                                                      ''IPアドレス
                    txtAlarmPrinterIP23.Text = .bytIP3                                                      ''IPアドレス
                    txtAlarmPrinterIP24.Text = .bytIP4                                                      ''IPアドレス
                End With

                ''HCプリンタ
                With .udtPrinterDetail(4)
                    cmbHCPrinter.SelectedValue = .bytPrinter                                                ''プリンタ有無
                    chkHcPrinter1EnableBackup.Checked = IIf(gBitCheck(.shtPrintUse, 1), True, False)        ''印字有無（バックアップ）
                    chkHCPrinter1PaperSizeA3.Checked = IIf(gBitCheck(.shtPrintUse, 2), True, False)         ''ページサイズA3 2011.12.13 K.Tanigawa
                    chkHCPrinterMachinery.Checked = IIf(gBitCheck(.shtPart, 0), True, False)                ''印字パート（Machinery）
                    chkHCPrinterCargo.Checked = IIf(gBitCheck(.shtPart, 1), True, False)                    ''印字パート（Cargo）
                    txtHCPrinterDriver.Text = .strDriver                                                    ''ドライバ
                    txtHCPrinterDevice.Text = .strDevice                                                     ''デバイス
                    txtHcPrinterIP1.Text = .bytIP1                                                          ''IPアドレス
                    txtHcPrinterIP2.Text = .bytIP2                                                          ''IPアドレス
                    txtHcPrinterIP3.Text = .bytIP3                                                          ''IPアドレス
                    txtHcPrinterIP4.Text = .bytIP4                                                          ''IPアドレス
                End With

            End With

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetSysPrinter, _
                               ByRef udtTarget As gTypSetSysPrinter)

        Try

            With udtTarget

                udtTarget.shtDemandPage = udtSource.shtDemandPage
                udtTarget.shtEventPrint = udtSource.shtEventPrint
                udtTarget.shtNoonUnder = udtSource.shtNoonUnder
                udtTarget.shtAutoCnt = udtSource.shtAutoCnt  ' 2013.07.22 自動印字最大数追加  K.Fujimoto
                udtTarget.shtAutoCntCargo = udtSource.shtAutoCntCargo 'Ver2.0.8.O　2019.07.04　自動印字最大文字数追加(Cargo)　岩﨑
                udtTarget.shtPrintType = udtSource.shtPrintType
                udtTarget.shtMachineryCargoPrint = udtSource.shtMachineryCargoPrint

                For i As Integer = LBound(udtTarget.udtPrinterDetail) To UBound(udtTarget.udtPrinterDetail)
                    udtTarget.udtPrinterDetail(i) = udtSource.udtPrinterDetail(i)
                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG1 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetSysPrinter, _
                                         ByVal udt2 As gTypSetSysPrinter) As Boolean

        Try

            If udt1.shtDemandPage <> udt2.shtDemandPage Then Return False
            If udt1.shtEventPrint <> udt2.shtEventPrint Then Return False
            If udt1.shtNoonUnder <> udt2.shtNoonUnder Then Return False
            If udt1.shtAutoCnt <> udt2.shtAutoCnt Then Return False ' 2013.07.22 自動印字最大数追加  K.Fujimoto
            If udt1.shtAutoCntCargo <> udt2.shtAutoCntCargo Then Return False ' Ver2.0.8.O 　2019.07.04 自動印字最大数追加(Cargo)
            If udt1.shtPrintType <> udt2.shtPrintType Then Return False
            If udt1.shtMachineryCargoPrint <> udt2.shtMachineryCargoPrint Then Return False

            For i As Integer = LBound(udt1.udtPrinterDetail) To UBound(udt1.udtPrinterDetail)
                If udt1.udtPrinterDetail(i).shtPart <> udt2.udtPrinterDetail(i).shtPart Then Return False
                If udt1.udtPrinterDetail(i).shtPrintUse <> udt2.udtPrinterDetail(i).shtPrintUse Then Return False
                If udt1.udtPrinterDetail(i).bytPrinter <> udt2.udtPrinterDetail(i).bytPrinter Then Return False
                If Not gCompareString(udt1.udtPrinterDetail(i).strDriver, udt2.udtPrinterDetail(i).strDriver) Then Return False
                If Not gCompareString(udt1.udtPrinterDetail(i).strDevice, udt2.udtPrinterDetail(i).strDevice) Then Return False
                If udt1.udtPrinterDetail(i).bytIP1 <> udt2.udtPrinterDetail(i).bytIP1 Then Return False
                If udt1.udtPrinterDetail(i).bytIP2 <> udt2.udtPrinterDetail(i).bytIP2 Then Return False
                If udt1.udtPrinterDetail(i).bytIP3 <> udt2.udtPrinterDetail(i).bytIP3 Then Return False
                If udt1.udtPrinterDetail(i).bytIP4 <> udt2.udtPrinterDetail(i).bytIP4 Then Return False
            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region


End Class
