Public Class frmChOutputAoDetail

#Region "変数定義"

    ''キャンセルフラグ
    Private mintCancelFlag As Integer

    Public Structure mAoInfo
        Public Row As Integer
        Public No As Integer
        Public Sysno As String      ''SYSTEM No.
        Public Chid As String       ''CH ID 又は 論理出力 ID
        Public Funo As String       ''FU 番号
        Public Portno As String     ''FU ポート番号
        Public Pin As String        ''FU 計測点番号
        Public Core1 As String      ''Core No1
        Public Core2 As String      ''Core No2
        Public CableMark1 As String ''Cable Mark 1
        Public CableMark2 As String ''Cable Mark 2
        Public CableDest As String  ''Cable Dest
    End Structure
    Private mAoDetail As mAoInfo

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数(端子台設定画面から)
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (I ) 構造体インデックス
    '           : ARG2 - (IO) アナログ出力情報
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShowTerminal(ByVal hRowNo As Integer, _
                                  ByRef hAoDetail As frmChTerminalDetail.mDoInfo, _
                                  ByRef frmOwner As Form) As Integer

        Try

            With hAoDetail
                mAoDetail.Row = 0
                mAoDetail.No = hRowNo
                mAoDetail.Sysno = .Sysno
                mAoDetail.Chid = .Chid
                mAoDetail.Funo = .Funo
                mAoDetail.Portno = .Portno
                mAoDetail.Pin = .Pin
                mAoDetail.Core1 = .Core1
                mAoDetail.Core2 = .Core2
                mAoDetail.CableMark1 = .CableMark1
                mAoDetail.CableMark2 = .CableMark2
                mAoDetail.CableDest = .CableDest
            End With


            Call gShowFormModelessForCloseWait2(Me, frmOwner)


            If mintCancelFlag = 0 Then

                With hAoDetail
                    .Sysno = mAoDetail.Sysno
                    .Chid = mAoDetail.Chid
                    .Funo = mAoDetail.Funo
                    .Portno = mAoDetail.Portno
                    .Pin = mAoDetail.Pin
                    .Core1 = mAoDetail.Core1
                    .Core2 = mAoDetail.Core2
                    .CableMark1 = mAoDetail.CableMark1
                    .CableMark2 = mAoDetail.CableMark2
                    .CableDest = mAoDetail.CableDest
                End With

            End If

            gShowTerminal = mintCancelFlag

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数(AO SETTINGから)
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (I ) 行番号
    '           : ARG2 - (IO) アナログ出力情報
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal hRowNo As Integer, ByRef hAoDetail As frmChOutputAoList.mAoInfo) As Integer

        Try

            With hAoDetail
                mAoDetail.Row = hRowNo
                mAoDetail.No = hRowNo
                mAoDetail.Sysno = .Sysno
                mAoDetail.Chid = .Chid
                mAoDetail.Funo = .Funo
                mAoDetail.Portno = .Portno
                mAoDetail.Pin = .Pin
            End With


            Me.ShowDialog()


            If mintCancelFlag = 0 Then

                With hAoDetail
                    .Sysno = mAoDetail.Sysno
                    .Chid = mAoDetail.Chid
                    .Funo = mAoDetail.Funo
                    .Portno = mAoDetail.Portno
                    .Pin = mAoDetail.Pin
                End With

            End If

            gShow = mintCancelFlag

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChOutputAoDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''データ設定
            With mAoDetail

                If .Row = 0 Then
                    ''端子台から
                    lblRowNo.Visible = False
                    lblNo.Visible = False
                Else
                    lblNo.Text = .Row + 1
                End If

                txtCHNo.Text = IIf(.Chid = 0, "", Val(.Chid).ToString("0000"))

                txtCoreNoPlus.Text = .Core1.Trim
                txtCoreNoMinus.Text = .Core2.Trim
                txtCableOut.Text = .CableMark1.Trim
                txtCom.Text = .CableMark2.Trim
                txtDist.Text = .CableDest.Trim

                txtFuNo.Text = gGetFuName2(.Funo)
                txtPortNo.Text = IIf(.Portno = gCstCodeChNotSetFuPortByte, "", .Portno)
                txtPin.Text = IIf(.Pin = gCstCodeChNotSetFuPinByte, "", CInt(.Pin).ToString("00"))

            End With

            mintCancelFlag = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChOutputAoDetail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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

            mintCancelFlag = 1
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： OKボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            With mAoDetail

                ''チャンネルNO → チャンネルID 変換
                .Chid = Val(txtCHNo.Text)

                .Core1 = txtCoreNoPlus.Text
                .Core2 = txtCoreNoMinus.Text
                .CableMark1 = txtCableOut.Text
                .CableMark2 = txtCom.Text
                .CableDest = txtDist.Text

                .Funo = gGetFuNo(txtFuNo.Text)
                .Portno = txtPortNo.Text
                .Pin = txtPin.Text

            End With

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No. KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtCHNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCHNo.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No.入力値をフォーマットする
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtCHNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCHNo.Validated

        Try

            If IsNumeric(txtCHNo.Text) Then
                txtCHNo.Text = Integer.Parse(txtCHNo.Text).ToString("0000")
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Core No(+) KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtCoreNoPlus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCoreNoPlus.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Core No(-) KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtCoreNoMinus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCoreNoMinus.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Cable OUT KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtCableOut_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCableOut.KeyPress

        Try

            e.Handled = gCheckTextInput(10, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： COM KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtCom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCom.KeyPress

        Try

            e.Handled = gCheckTextInput(10, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： DIST KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtDist_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDist.KeyPress

        Try

            e.Handled = gCheckTextInput(10, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： FU Address 入力制限
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtFuNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFuNo.KeyPress

        Try

            e.Handled = gChkInputKeyFuNo(sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtPortNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPortNo.KeyPress

        Try

            e.Handled = gCheckTextInput(1, sender, e.KeyChar, True, False, False, False, "1,2,3,4,5,6,7,8")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtPin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPin.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



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
            ''共通テキスト入力チェック
            If Not gChkInputText(txtCoreNoPlus, "CoreNo(+)", True, True) Then Return False
            If Not gChkInputText(txtCoreNoMinus, "CoreNo(-)", True, True) Then Return False
            If Not gChkInputText(txtCableOut, "Cable OUT", True, True) Then Return False
            If Not gChkInputText(txtCom, "COM", True, True) Then Return False
            If Not gChkInputText(txtDist, "DEST", True, True) Then Return False

            ''共通数値入力チェック
            If Not gChkInputNum(txtCHNo, 1, 65535, "CH No", False, True) Then Return False

            ''共通FUアドレス入力チェック
            '' Ver1.9.8 2016.02.20 FUｱﾄﾞﾚｽ入力ﾁｪｯｸを外す
            ''If Not gChkInputFuAddress(txtFuNo, txtPortNo, txtPin, 16, True, True) Then Return False


            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class
