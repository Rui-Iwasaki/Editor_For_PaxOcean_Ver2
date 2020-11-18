Public Class frmOpsPulldownSub_GAI_Print
    '■外販
    'PRINTのみで起動するサブ画面

#Region "構造体"
    'PRINT詳細
    Private Structure tpPRINTdetail
        Dim strName As String
        Dim intNo1 As Integer
        Dim intNo2 As Integer
        Dim intNo3 As Integer
        Dim intNo4 As Integer
        Dim intScreenNo As Integer
        Dim intKey As Integer
    End Structure
#End Region

#Region "変数"
    Private mudtMenuSubNew As gTypSetOpsPulldownMenuGroup = Nothing

    'サブグループ数（画面表示関数で取得）
    Private mintRowCnt As Integer

    '戻り値
    Private mintRtn As Integer


    'PRINT詳細データ
    Private mudtPrintDtl(6) As tpPRINTdetail
#End Region

#Region "画面"

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) プルダウンサブメニュー設定構造体
    '           : ARG2 - (I ) セットカウント数
    '           : ARG3 - (IO) フォーム
    ' 機能説明  : 本画面を表示する
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtMenuSub As gTypSetOpsPulldownMenuGroup, _
                          ByVal intSetCnt As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''ボタン選択フラグ初期化
            mintRtn = 1

            ''引数保存
            mudtMenuSubNew = udtMenuSub

            ''サブグループ数
            mintRowCnt = intSetCnt

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then
                udtMenuSub = mudtMenuSubNew
            End If

            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

    Private Sub frmOpsPulldownSub_GAI_Print_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            'PRINT詳細データ生成
            Call subSetPrintDtl()

            '画面設定
            Call mSetDisplay(mudtMenuSubNew)

            'チェック制御
            Call mSetDispONOFF()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub frmOpsPulldownSub_GAI_Print_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Me.Dispose()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub cmdOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdOk.Click
        Try
            '設定値の保存
            Call mSetStructure(mudtMenuSubNew)

            mintRtn = 0
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region

#Region "関数"
    'PRINT詳細値格納
    Private Sub subSetPrintDtl()
        Try
            '1:DEMAND TIME LOG
            mudtPrintDtl(0).strName = "DEMAND TIME LOG"
            mudtPrintDtl(0).intNo1 = 30
            mudtPrintDtl(0).intNo2 = 12
            mudtPrintDtl(0).intNo3 = 0
            mudtPrintDtl(0).intNo4 = 0
            mudtPrintDtl(0).intScreenNo = DataExchange2(1)
            mudtPrintDtl(0).intKey = 0
            '2:SET VALUE & DELAY TIMER
            mudtPrintDtl(1).strName = "SET VALUE & DELAY TIMER"
            mudtPrintDtl(1).intNo1 = 30
            mudtPrintDtl(1).intNo2 = 12
            mudtPrintDtl(1).intNo3 = 0
            mudtPrintDtl(1).intNo4 = 0
            mudtPrintDtl(1).intScreenNo = DataExchange2(2)
            mudtPrintDtl(1).intKey = 0
            '3:MONOCHROME HARD COPY
            mudtPrintDtl(2).strName = "MONOCHROME HARD COPY"
            mudtPrintDtl(2).intNo1 = 30
            mudtPrintDtl(2).intNo2 = 17
            mudtPrintDtl(2).intNo3 = 0
            mudtPrintDtl(2).intNo4 = 0
            mudtPrintDtl(2).intScreenNo = DataExchange2(3)
            mudtPrintDtl(2).intKey = 0
            '4:COLOR HARD COPY
            mudtPrintDtl(3).strName = "COLOR HARD COPY"
            mudtPrintDtl(3).intNo1 = 30
            mudtPrintDtl(3).intNo2 = 17
            mudtPrintDtl(3).intNo3 = 0
            mudtPrintDtl(3).intNo4 = 0
            mudtPrintDtl(3).intScreenNo = DataExchange2(4)
            mudtPrintDtl(3).intKey = 0
            '5:SAVE USB MEMORY
            mudtPrintDtl(4).strName = "SAVE USB MEMORY"
            mudtPrintDtl(4).intNo1 = 30
            mudtPrintDtl(4).intNo2 = 19
            mudtPrintDtl(4).intNo3 = 0
            mudtPrintDtl(4).intNo4 = 0
            mudtPrintDtl(4).intScreenNo = DataExchange2(0)
            mudtPrintDtl(4).intKey = 0
            '6:ALARM, RECOVERY LOG
            mudtPrintDtl(5).strName = "ALARM, RECOVERY LOG"
            mudtPrintDtl(5).intNo1 = 30
            mudtPrintDtl(5).intNo2 = 12
            mudtPrintDtl(5).intNo3 = 0
            mudtPrintDtl(5).intNo4 = 0
            mudtPrintDtl(5).intScreenNo = DataExchange2(11)
            mudtPrintDtl(5).intKey = 0
            '7:SAVE DEMAND TIME LOG
            mudtPrintDtl(6).strName = "SAVE DEMAND TIME LOG"
            mudtPrintDtl(6).intNo1 = 30
            mudtPrintDtl(6).intNo2 = 49
            mudtPrintDtl(6).intNo3 = 2
            mudtPrintDtl(6).intNo4 = 1
            mudtPrintDtl(6).intScreenNo = DataExchange2(0)
            mudtPrintDtl(6).intKey = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '変数から画面を表示する
    Private Sub mSetDisplay(ByVal udtMenuGrp As gTypSetOpsPulldownMenuGroup)
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strName As String = ""


            Dim intPrintDtl As Integer = -1

            For i = 0 To UBound(udtMenuGrp.udtSub)

                With udtMenuGrp.udtSub(i)
                    '名称から0x00を取り除く
                    strName = .strName.Replace(Chr(&H0), Chr(&H20)).Trim

                    intPrintDtl = -1
                    For j = 0 To UBound(mudtPrintDtl) Step 1
                        If strName = mudtPrintDtl(j).strName Then
                            intPrintDtl = j
                            Exit For
                        End If
                    Next j

                    If intPrintDtl <> -1 Then
                        Select Case intPrintDtl
                            Case 0
                                chkPrint1.Checked = True
                            Case 1
                                chkPrint2.Checked = True
                            Case 2
                                chkPrint3.Checked = True
                            Case 3
                                chkPrint4.Checked = True
                            Case 4
                                chkPrint5.Checked = True
                            Case 5
                                chkPrint6.Checked = True
                            Case 6
                                chkPrint7.Checked = True
                        End Select
                    End If
                End With

            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'プリンタ設定変数から、チェックとEnableを自動制御
    Private Sub mSetDispONOFF()
        Try
            'LP,APが無い場合
            With gudt.SetSystem.udtSysPrinter
                If .udtPrinterDetail(0).bytPrinter = 0 And _
                    .udtPrinterDetail(1).bytPrinter = 0 And _
                    .udtPrinterDetail(2).bytPrinter = 0 And _
                    .udtPrinterDetail(3).bytPrinter = 0 Then

                    '設定無し
                    chkPrint1.Checked = False
                    chkPrint2.Checked = False
                    chkPrint6.Checked = False
                    chkPrint7.Checked = False
                    '設定禁止
                    chkPrint1.Enabled = False
                    chkPrint2.Enabled = False
                    chkPrint6.Enabled = False
                    chkPrint7.Enabled = False

                End If
            End With

            'LP,HCが無い場合
            With gudt.SetSystem.udtSysPrinter
                If .udtPrinterDetail(0).bytPrinter = 0 And _
                    .udtPrinterDetail(1).bytPrinter = 0 And _
                    .udtPrinterDetail(4).bytPrinter = 0 Then

                    '設定無し
                    chkPrint3.Checked = False
                    chkPrint4.Checked = False
                    '設定禁止
                    chkPrint3.Enabled = False
                    chkPrint4.Enabled = False
                End If
            End With

            'HCが無いかつ、ログプリンタはあるがカラープリントが無い場合
            Dim blPflg As Boolean = True
            With gudt.SetSystem.udtSysPrinter
                If .udtPrinterDetail(4).bytPrinter = 0 Then
                    If .udtPrinterDetail(0).bytPrinter <> 0 And _
                        gBitCheck(.udtPrinterDetail(0).shtPrintUse, 3) = False Then
                        blPflg = False
                    End If
                    If .udtPrinterDetail(1).bytPrinter <> 0 And _
                        gBitCheck(.udtPrinterDetail(1).shtPrintUse, 3) = False Then
                        blPflg = False
                    End If

                    '設定無し
                    chkPrint4.Checked = blPflg
                    '設定禁止
                    chkPrint4.Enabled = blPflg

                End If
            End With



        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    '画面を変数へ保存する
    Private Sub mSetStructure(ByRef udtMenuGrp As gTypSetOpsPulldownMenuGroup)

        Try
            Dim i As Integer = 0
            Dim intCount As Integer = 0

            '一旦全クリアして、再度保存する
            '>>>一旦クリア
            udtMenuGrp.bytCount = 0
            For i = 0 To UBound(udtMenuGrp.udtSub) Step 1
                With udtMenuGrp.udtSub(i)
                    .strName = MojiMake("", 32)
                    .SubbytMenuType1 = 0
                    .SubbytMenuType2 = 0
                    .SubbytMenuType3 = 0
                    .SubbytMenuType4 = 0
                    .SubMenutx = 0
                    .SubMenuty = 0
                    .SubMenubx = 0
                    .SubMenuby = 0
                    .ViewNo1 = 0
                    .ViewNo2 = 0
                    .ViewNo3 = 0
                    .ViewNo4 = 0
                    .bytKeyCode = 0
                End With
            Next i
            '>>>値格納
            If chkPrint1.Checked = True Then
                With mudtPrintDtl(0)
                    udtMenuGrp.udtSub(intCount).strName = .strName
                    udtMenuGrp.udtSub(intCount).SubbytMenuType1 = .intNo1
                    udtMenuGrp.udtSub(intCount).SubbytMenuType2 = .intNo2
                    udtMenuGrp.udtSub(intCount).SubbytMenuType3 = .intNo3
                    udtMenuGrp.udtSub(intCount).SubbytMenuType4 = .intNo4
                    udtMenuGrp.udtSub(intCount).ViewNo1 = .intScreenNo
                    udtMenuGrp.udtSub(intCount).bytKeyCode = .intKey
                End With
                intCount = intCount + 1
            End If
            If chkPrint2.Checked = True Then
                With mudtPrintDtl(1)
                    udtMenuGrp.udtSub(intCount).strName = .strName
                    udtMenuGrp.udtSub(intCount).SubbytMenuType1 = .intNo1
                    udtMenuGrp.udtSub(intCount).SubbytMenuType2 = .intNo2
                    udtMenuGrp.udtSub(intCount).SubbytMenuType3 = .intNo3
                    udtMenuGrp.udtSub(intCount).SubbytMenuType4 = .intNo4
                    udtMenuGrp.udtSub(intCount).ViewNo1 = .intScreenNo
                    udtMenuGrp.udtSub(intCount).bytKeyCode = .intKey
                End With
                intCount = intCount + 1
            End If
            If chkPrint3.Checked = True Then
                With mudtPrintDtl(2)
                    udtMenuGrp.udtSub(intCount).strName = .strName
                    udtMenuGrp.udtSub(intCount).SubbytMenuType1 = .intNo1
                    udtMenuGrp.udtSub(intCount).SubbytMenuType2 = .intNo2
                    udtMenuGrp.udtSub(intCount).SubbytMenuType3 = .intNo3
                    udtMenuGrp.udtSub(intCount).SubbytMenuType4 = .intNo4
                    udtMenuGrp.udtSub(intCount).ViewNo1 = .intScreenNo
                    udtMenuGrp.udtSub(intCount).bytKeyCode = .intKey
                End With
                intCount = intCount + 1
            End If
            If chkPrint4.Checked = True Then
                With mudtPrintDtl(3)
                    udtMenuGrp.udtSub(intCount).strName = .strName
                    udtMenuGrp.udtSub(intCount).SubbytMenuType1 = .intNo1
                    udtMenuGrp.udtSub(intCount).SubbytMenuType2 = .intNo2
                    udtMenuGrp.udtSub(intCount).SubbytMenuType3 = .intNo3
                    udtMenuGrp.udtSub(intCount).SubbytMenuType4 = .intNo4
                    udtMenuGrp.udtSub(intCount).ViewNo1 = .intScreenNo
                    udtMenuGrp.udtSub(intCount).bytKeyCode = .intKey
                End With
                intCount = intCount + 1
            End If
            If chkPrint5.Checked = True Then
                With mudtPrintDtl(4)
                    udtMenuGrp.udtSub(intCount).strName = .strName
                    udtMenuGrp.udtSub(intCount).SubbytMenuType1 = .intNo1
                    udtMenuGrp.udtSub(intCount).SubbytMenuType2 = .intNo2
                    udtMenuGrp.udtSub(intCount).SubbytMenuType3 = .intNo3
                    udtMenuGrp.udtSub(intCount).SubbytMenuType4 = .intNo4
                    udtMenuGrp.udtSub(intCount).ViewNo1 = .intScreenNo
                    udtMenuGrp.udtSub(intCount).bytKeyCode = .intKey
                End With
                intCount = intCount + 1
            End If
            If chkPrint6.Checked = True Then
                With mudtPrintDtl(5)
                    udtMenuGrp.udtSub(intCount).strName = .strName
                    udtMenuGrp.udtSub(intCount).SubbytMenuType1 = .intNo1
                    udtMenuGrp.udtSub(intCount).SubbytMenuType2 = .intNo2
                    udtMenuGrp.udtSub(intCount).SubbytMenuType3 = .intNo3
                    udtMenuGrp.udtSub(intCount).SubbytMenuType4 = .intNo4
                    udtMenuGrp.udtSub(intCount).ViewNo1 = .intScreenNo
                    udtMenuGrp.udtSub(intCount).bytKeyCode = .intKey
                End With
                intCount = intCount + 1
            End If
            If chkPrint7.Checked = True Then
                With mudtPrintDtl(6)
                    udtMenuGrp.udtSub(intCount).strName = .strName
                    udtMenuGrp.udtSub(intCount).SubbytMenuType1 = .intNo1
                    udtMenuGrp.udtSub(intCount).SubbytMenuType2 = .intNo2
                    udtMenuGrp.udtSub(intCount).SubbytMenuType3 = .intNo3
                    udtMenuGrp.udtSub(intCount).SubbytMenuType4 = .intNo4
                    udtMenuGrp.udtSub(intCount).ViewNo1 = .intScreenNo
                    udtMenuGrp.udtSub(intCount).bytKeyCode = .intKey
                End With
                intCount = intCount + 1
            End If
            udtMenuGrp.bytCount = intCount

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '画面番号変更(反転)
    Private Function DataExchange2(ByVal ViewNo1 As Integer) As Short

        Try

            Dim bytValue1, bytValue2 As Byte
            Dim bytArray(1) As Byte

            Call gSeparat2Byte(ViewNo1, bytValue1, bytValue2)

            bytArray(0) = bytValue2
            bytArray(1) = bytValue1

            DataExchange2 = BitConverter.ToInt16(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function
#End Region


End Class