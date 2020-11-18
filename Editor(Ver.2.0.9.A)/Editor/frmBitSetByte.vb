Public Class frmBitSetByte

#Region "変数定義"

    Dim mintMode As Integer             ''0:bit0 ～ bit7 / 1:bit0 ～ bit8
    Dim mintRtn As Integer              ''ボタンフラグ
    Dim mintSet As Integer              ''設定値

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK  0:キャンセル
    ' 引き数    : ARG1 - (IO) 設定値
    ' 引き数    : ARG2 - (I ) 設定モード(1=9ビット,　0=8ビット)
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef intSet As Integer, _
                          ByVal intMode As Integer, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''引数保存
            mintSet = intSet
            mintMode = intMode

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 1 Then
                intSet = mintSet
            End If

            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmBitSetByte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            chkBit0.Checked = IIf(gBitCheck(mintSet, 0), True, False)
            chkBit1.Checked = IIf(gBitCheck(mintSet, 1), True, False)
            chkBit2.Checked = IIf(gBitCheck(mintSet, 2), True, False)
            chkBit3.Checked = IIf(gBitCheck(mintSet, 3), True, False)
            chkBit4.Checked = IIf(gBitCheck(mintSet, 4), True, False)
            chkBit5.Checked = IIf(gBitCheck(mintSet, 5), True, False)
            chkBit6.Checked = IIf(gBitCheck(mintSet, 6), True, False)
            chkBit7.Checked = IIf(gBitCheck(mintSet, 7), True, False)

            If mintMode = 1 Then

                chkBit8.Checked = IIf(gBitCheck(mintSet, 8), True, False)

                chkBit8.Visible = True

            Else
                chkBit8.Visible = False

                chkBit0.Left -= 20
                chkBit1.Left -= 20
                chkBit2.Left -= 20
                chkBit3.Left -= 20
                chkBit4.Left -= 20
                chkBit5.Left -= 20
                chkBit6.Left -= 20
                chkBit7.Left -= 20

            End If

            lblValue.Text = mintSet

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub chkBit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBit0.CheckedChanged, _
                                                                                                          chkBit1.CheckedChanged, _
                                                                                                          chkBit2.CheckedChanged, _
                                                                                                          chkBit3.CheckedChanged, _
                                                                                                          chkBit4.CheckedChanged, _
                                                                                                          chkBit5.CheckedChanged, _
                                                                                                          chkBit6.CheckedChanged, _
                                                                                                          chkBit7.CheckedChanged, _
                                                                                                          chkBit8.CheckedChanged
        Dim intwk As Integer
        Try


            intwk = gBitSet(intwk, 0, chkBit0.Checked)
            intwk = gBitSet(intwk, 1, chkBit1.Checked)
            intwk = gBitSet(intwk, 2, chkBit2.Checked)
            intwk = gBitSet(intwk, 3, chkBit3.Checked)
            intwk = gBitSet(intwk, 4, chkBit4.Checked)
            intwk = gBitSet(intwk, 5, chkBit5.Checked)
            intwk = gBitSet(intwk, 6, chkBit6.Checked)
            intwk = gBitSet(intwk, 7, chkBit7.Checked)

            If mintMode = 1 Then
                intwk = gBitSet(intwk, 8, chkBit8.Checked)
            End If

            lblValue.Text = intwk


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChControlUseNotuseDetail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : OKボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            mintSet = 0

            mintSet = gBitSet(mintSet, 0, chkBit0.Checked)
            mintSet = gBitSet(mintSet, 1, chkBit1.Checked)
            mintSet = gBitSet(mintSet, 2, chkBit2.Checked)
            mintSet = gBitSet(mintSet, 3, chkBit3.Checked)
            mintSet = gBitSet(mintSet, 4, chkBit4.Checked)
            mintSet = gBitSet(mintSet, 5, chkBit5.Checked)
            mintSet = gBitSet(mintSet, 6, chkBit6.Checked)
            mintSet = gBitSet(mintSet, 7, chkBit7.Checked)

            If mintMode = 1 Then
                mintSet = gBitSet(mintSet, 8, chkBit8.Checked)
            End If

            mintRtn = 1
            Me.Close()

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

#End Region

End Class